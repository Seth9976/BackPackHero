using System;
using System.Collections;
using System.Globalization;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Provides trace event data specific to a thread and a process.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022C RID: 556
	public class TraceEventCache
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00047289 File Offset: 0x00045489
		internal Guid ActivityId
		{
			get
			{
				return Trace.CorrelationManager.ActivityId;
			}
		}

		/// <summary>Gets the call stack for the current thread.</summary>
		/// <returns>A string containing stack trace information. This value can be an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" />
		/// </PermissionSet>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00047295 File Offset: 0x00045495
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				return this.stackTrace;
			}
		}

		/// <summary>Gets the correlation data, contained in a stack. </summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> containing correlation data.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000472B0 File Offset: 0x000454B0
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		/// <summary>Gets the date and time at which the event trace occurred.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure whose value is a date and time expressed in Coordinated Universal Time (UTC).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x000472BC File Offset: 0x000454BC
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		/// <summary>Gets the unique identifier of the current process.</summary>
		/// <returns>The system-generated unique identifier of the current process.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x000472E1 File Offset: 0x000454E1
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		/// <summary>Gets a unique identifier for the current managed thread.  </summary>
		/// <returns>A string that represents a unique integer identifier for this managed thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x000472E8 File Offset: 0x000454E8
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>The tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00047307 File Offset: 0x00045507
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00047324 File Offset: 0x00045524
		private static void InitProcessInfo()
		{
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00047374 File Offset: 0x00045574
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00047382 File Offset: 0x00045582
		internal static string GetProcessName()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processName;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00047390 File Offset: 0x00045590
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x040009D2 RID: 2514
		private static volatile int processId;

		// Token: 0x040009D3 RID: 2515
		private static volatile string processName;

		// Token: 0x040009D4 RID: 2516
		private long timeStamp = -1L;

		// Token: 0x040009D5 RID: 2517
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x040009D6 RID: 2518
		private string stackTrace;
	}
}
