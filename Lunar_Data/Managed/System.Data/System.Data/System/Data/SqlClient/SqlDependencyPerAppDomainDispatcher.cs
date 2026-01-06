using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001A9 RID: 425
	internal class SqlDependencyPerAppDomainDispatcher : MarshalByRefObject
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x00065EE8 File Offset: 0x000640E8
		private SqlDependencyPerAppDomainDispatcher()
		{
			this._dependencyIdToDependencyHash = new Dictionary<string, SqlDependency>();
			this._notificationIdToDependenciesHash = new Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList>();
			this._commandHashToNotificationId = new Dictionary<string, string>();
			this._timeoutTimer = ADP.UnsafeCreateTimer(new TimerCallback(SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback), null, -1, -1);
			this.SubscribeToAppDomainUnload();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00003DF6 File Offset: 0x00001FF6
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00065F48 File Offset: 0x00064148
		internal void AddDependencyEntry(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				this._dependencyIdToDependencyHash.Add(dep.Id, dep);
			}
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00065F94 File Offset: 0x00064194
		internal string AddCommandEntry(string commandHash, SqlDependency dep)
		{
			string text = string.Empty;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(dep.Id))
				{
					if (this._commandHashToNotificationId.TryGetValue(commandHash, out text))
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
						if (!this._notificationIdToDependenciesHash.TryGetValue(text, out dependencyList))
						{
							throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyCommandHashIsNotAssociatedWithNotification);
						}
						if (!dependencyList.Contains(dep))
						{
							dependencyList.Add(dep);
						}
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0};{1}", SqlDependency.AppDomainKey, Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture));
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList2 = new SqlDependencyPerAppDomainDispatcher.DependencyList(commandHash);
						dependencyList2.Add(dep);
						this._commandHashToNotificationId.Add(commandHash, text);
						this._notificationIdToDependenciesHash.Add(text, dependencyList2);
					}
				}
			}
			return text;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00066080 File Offset: 0x00064280
		internal void InvalidateCommandID(SqlNotification sqlNotification)
		{
			List<SqlDependency> list = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				list = this.LookupCommandEntryWithRemove(sqlNotification.Key);
				if (list != null)
				{
					foreach (SqlDependency sqlDependency in list)
					{
						this.LookupDependencyEntryWithRemove(sqlDependency.Id);
						this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
					}
				}
			}
			if (list != null)
			{
				foreach (SqlDependency sqlDependency2 in list)
				{
					try
					{
						sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
					}
				}
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0006618C File Offset: 0x0006438C
		internal void InvalidateServer(string server, SqlNotification sqlNotification)
		{
			List<SqlDependency> list = new List<SqlDependency>();
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				foreach (KeyValuePair<string, SqlDependency> keyValuePair in this._dependencyIdToDependencyHash)
				{
					SqlDependency value = keyValuePair.Value;
					if (value.ContainsServer(server))
					{
						list.Add(value);
					}
				}
				foreach (SqlDependency sqlDependency in list)
				{
					this.LookupDependencyEntryWithRemove(sqlDependency.Id);
					this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
				}
			}
			foreach (SqlDependency sqlDependency2 in list)
			{
				try
				{
					sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(ex);
				}
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000662E0 File Offset: 0x000644E0
		internal SqlDependency LookupDependencyEntry(string id)
		{
			if (id == null)
			{
				throw ADP.ArgumentNull("id");
			}
			if (string.IsNullOrEmpty(id))
			{
				throw SQL.SqlDependencyIdMismatch();
			}
			SqlDependency sqlDependency = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					sqlDependency = this._dependencyIdToDependencyHash[id];
				}
			}
			return sqlDependency;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00066354 File Offset: 0x00064554
		private void LookupDependencyEntryWithRemove(string id)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._dependencyIdToDependencyHash.ContainsKey(id))
				{
					this._dependencyIdToDependencyHash.Remove(id);
					if (this._dependencyIdToDependencyHash.Count == 0)
					{
						this._timeoutTimer.Change(-1, -1);
						this._sqlDependencyTimeOutTimerStarted = false;
					}
				}
			}
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000663CC File Offset: 0x000645CC
		private List<SqlDependency> LookupCommandEntryWithRemove(string notificationId)
		{
			SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (this._notificationIdToDependenciesHash.TryGetValue(notificationId, out dependencyList))
				{
					this._notificationIdToDependenciesHash.Remove(notificationId);
					this._commandHashToNotificationId.Remove(dependencyList.CommandHash);
				}
			}
			return dependencyList;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00066438 File Offset: 0x00064638
		private void RemoveDependencyFromCommandToDependenciesHash(SqlDependency dependency)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher.DependencyList> keyValuePair in this._notificationIdToDependenciesHash)
				{
					SqlDependencyPerAppDomainDispatcher.DependencyList value = keyValuePair.Value;
					if (value.Remove(dependency) && value.Count == 0)
					{
						list.Add(keyValuePair.Key);
						list2.Add(keyValuePair.Value.CommandHash);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					this._notificationIdToDependenciesHash.Remove(list[i]);
					this._commandHashToNotificationId.Remove(list2[i]);
				}
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00066534 File Offset: 0x00064734
		internal void StartTimer(SqlDependency dep)
		{
			object instanceLock = this._instanceLock;
			lock (instanceLock)
			{
				if (!this._sqlDependencyTimeOutTimerStarted)
				{
					this._timeoutTimer.Change(15000, 15000);
					this._nextTimeout = dep.ExpirationTime;
					this._sqlDependencyTimeOutTimerStarted = true;
				}
				else if (this._nextTimeout > dep.ExpirationTime)
				{
					this._nextTimeout = dep.ExpirationTime;
				}
			}
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000665C0 File Offset: 0x000647C0
		private static void TimeoutTimerCallback(object state)
		{
			object obj = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			SqlDependency[] array;
			lock (obj)
			{
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count == 0)
				{
					return;
				}
				if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout > DateTime.UtcNow)
				{
					return;
				}
				array = new SqlDependency[SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count];
				SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Values.CopyTo(array, 0);
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime dateTime = DateTime.MaxValue;
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].ExpirationTime <= utcNow)
				{
					try
					{
						array[i].Invalidate(SqlNotificationType.Change, SqlNotificationInfo.Error, SqlNotificationSource.Timeout);
						goto IL_00E0;
					}
					catch (Exception ex)
					{
						if (!ADP.IsCatchableExceptionType(ex))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(ex);
						goto IL_00E0;
					}
					goto IL_00C0;
				}
				goto IL_00C0;
				IL_00E0:
				i++;
				continue;
				IL_00C0:
				if (array[i].ExpirationTime < dateTime)
				{
					dateTime = array[i].ExpirationTime;
				}
				array[i] = null;
				goto IL_00E0;
			}
			obj = SqlDependencyPerAppDomainDispatcher.SingletonInstance._instanceLock;
			lock (obj)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Remove(array[j].Id);
					}
				}
				if (dateTime < SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout)
				{
					SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout = dateTime;
				}
			}
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000094D4 File Offset: 0x000076D4
		private void SubscribeToAppDomainUnload()
		{
		}

		// Token: 0x04000DAF RID: 3503
		internal static readonly SqlDependencyPerAppDomainDispatcher SingletonInstance = new SqlDependencyPerAppDomainDispatcher();

		// Token: 0x04000DB0 RID: 3504
		internal object _instanceLock = new object();

		// Token: 0x04000DB1 RID: 3505
		private Dictionary<string, SqlDependency> _dependencyIdToDependencyHash;

		// Token: 0x04000DB2 RID: 3506
		private Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList> _notificationIdToDependenciesHash;

		// Token: 0x04000DB3 RID: 3507
		private Dictionary<string, string> _commandHashToNotificationId;

		// Token: 0x04000DB4 RID: 3508
		private bool _sqlDependencyTimeOutTimerStarted;

		// Token: 0x04000DB5 RID: 3509
		private DateTime _nextTimeout;

		// Token: 0x04000DB6 RID: 3510
		private Timer _timeoutTimer;

		// Token: 0x020001AA RID: 426
		private sealed class DependencyList : List<SqlDependency>
		{
			// Token: 0x060014B2 RID: 5298 RVA: 0x00066760 File Offset: 0x00064960
			internal DependencyList(string commandHash)
			{
				this.CommandHash = commandHash;
			}

			// Token: 0x04000DB7 RID: 3511
			public readonly string CommandHash;
		}
	}
}
