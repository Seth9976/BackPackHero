using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200015D RID: 349
	internal static class SqlClientDiagnosticListenerExtensions
	{
		// Token: 0x06001126 RID: 4390 RVA: 0x00054C1C File Offset: 0x00052E1C
		public static Guid WriteCommandBefore(this DiagnosticListener @this, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandBefore"))
			{
				Guid guid = Guid.NewGuid();
				string text = "System.Data.SqlClient.WriteCommandBefore";
				Guid guid2 = guid;
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(text, new
				{
					OperationId = guid2,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00054C78 File Offset: 0x00052E78
		public static void WriteCommandAfter(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandAfter"))
			{
				string text = "System.Data.SqlClient.WriteCommandAfter";
				SqlConnection connection = sqlCommand.Connection;
				Guid? guid = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null);
				SqlStatistics statistics = sqlCommand.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = guid,
					Command = sqlCommand,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00054CDC File Offset: 0x00052EDC
		public static void WriteCommandError(this DiagnosticListener @this, Guid operationId, SqlCommand sqlCommand, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteCommandError"))
			{
				string text = "System.Data.SqlClient.WriteCommandError";
				SqlConnection connection = sqlCommand.Connection;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null),
					Command = sqlCommand,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00054D30 File Offset: 0x00052F30
		public static Guid WriteConnectionOpenBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteConnectionOpenBefore", new
				{
					OperationId = guid,
					Operation = operation,
					Connection = sqlConnection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00054D6F File Offset: 0x00052F6F
		public static void WriteConnectionOpenAfter(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenAfter"))
			{
				string text = "System.Data.SqlClient.WriteConnectionOpenAfter";
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00054DAE File Offset: 0x00052FAE
		public static void WriteConnectionOpenError(this DiagnosticListener @this, Guid operationId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionOpenError"))
			{
				@this.Write("System.Data.SqlClient.WriteConnectionOpenError", new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = sqlConnection.ClientConnectionId,
					Connection = sqlConnection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x00054DE0 File Offset: 0x00052FE0
		public static Guid WriteConnectionCloseBefore(this DiagnosticListener @this, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseBefore"))
			{
				Guid guid = Guid.NewGuid();
				string text = "System.Data.SqlClient.WriteConnectionCloseBefore";
				Guid guid2 = guid;
				Guid clientConnectionId = sqlConnection.ClientConnectionId;
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = guid2,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00054E37 File Offset: 0x00053037
		public static void WriteConnectionCloseAfter(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseAfter"))
			{
				string text = "System.Data.SqlClient.WriteConnectionCloseAfter";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00054E74 File Offset: 0x00053074
		public static void WriteConnectionCloseError(this DiagnosticListener @this, Guid operationId, Guid clientConnectionId, SqlConnection sqlConnection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteConnectionCloseError"))
			{
				string text = "System.Data.SqlClient.WriteConnectionCloseError";
				SqlStatistics statistics = sqlConnection.Statistics;
				@this.Write(text, new
				{
					OperationId = operationId,
					Operation = operation,
					ConnectionId = clientConnectionId,
					Connection = sqlConnection,
					Statistics = ((statistics != null) ? statistics.GetDictionary() : null),
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00054EBC File Offset: 0x000530BC
		public static Guid WriteTransactionCommitBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionCommitBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00054EFC File Offset: 0x000530FC
		public static void WriteTransactionCommitAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00054F25 File Offset: 0x00053125
		public static void WriteTransactionCommitError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionCommitError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionCommitError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00054F50 File Offset: 0x00053150
		public static Guid WriteTransactionRollbackBefore(this DiagnosticListener @this, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackBefore"))
			{
				Guid guid = Guid.NewGuid();
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackBefore", new
				{
					OperationId = guid,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
				return guid;
			}
			return Guid.Empty;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00054F92 File Offset: 0x00053192
		public static void WriteTransactionRollbackAfter(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackAfter"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackAfter", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00054FC0 File Offset: 0x000531C0
		public static void WriteTransactionRollbackError(this DiagnosticListener @this, Guid operationId, IsolationLevel isolationLevel, SqlConnection connection, string transactionName, Exception ex, [CallerMemberName] string operation = "")
		{
			if (@this.IsEnabled("System.Data.SqlClient.WriteTransactionRollbackError"))
			{
				@this.Write("System.Data.SqlClient.WriteTransactionRollbackError", new
				{
					OperationId = operationId,
					Operation = operation,
					IsolationLevel = isolationLevel,
					Connection = connection,
					TransactionName = transactionName,
					Exception = ex,
					Timestamp = Stopwatch.GetTimestamp()
				});
			}
		}

		// Token: 0x04000B60 RID: 2912
		public const string DiagnosticListenerName = "SqlClientDiagnosticListener";

		// Token: 0x04000B61 RID: 2913
		private const string SqlClientPrefix = "System.Data.SqlClient.";

		// Token: 0x04000B62 RID: 2914
		public const string SqlBeforeExecuteCommand = "System.Data.SqlClient.WriteCommandBefore";

		// Token: 0x04000B63 RID: 2915
		public const string SqlAfterExecuteCommand = "System.Data.SqlClient.WriteCommandAfter";

		// Token: 0x04000B64 RID: 2916
		public const string SqlErrorExecuteCommand = "System.Data.SqlClient.WriteCommandError";

		// Token: 0x04000B65 RID: 2917
		public const string SqlBeforeOpenConnection = "System.Data.SqlClient.WriteConnectionOpenBefore";

		// Token: 0x04000B66 RID: 2918
		public const string SqlAfterOpenConnection = "System.Data.SqlClient.WriteConnectionOpenAfter";

		// Token: 0x04000B67 RID: 2919
		public const string SqlErrorOpenConnection = "System.Data.SqlClient.WriteConnectionOpenError";

		// Token: 0x04000B68 RID: 2920
		public const string SqlBeforeCloseConnection = "System.Data.SqlClient.WriteConnectionCloseBefore";

		// Token: 0x04000B69 RID: 2921
		public const string SqlAfterCloseConnection = "System.Data.SqlClient.WriteConnectionCloseAfter";

		// Token: 0x04000B6A RID: 2922
		public const string SqlErrorCloseConnection = "System.Data.SqlClient.WriteConnectionCloseError";

		// Token: 0x04000B6B RID: 2923
		public const string SqlBeforeCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitBefore";

		// Token: 0x04000B6C RID: 2924
		public const string SqlAfterCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitAfter";

		// Token: 0x04000B6D RID: 2925
		public const string SqlErrorCommitTransaction = "System.Data.SqlClient.WriteTransactionCommitError";

		// Token: 0x04000B6E RID: 2926
		public const string SqlBeforeRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackBefore";

		// Token: 0x04000B6F RID: 2927
		public const string SqlAfterRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackAfter";

		// Token: 0x04000B70 RID: 2928
		public const string SqlErrorRollbackTransaction = "System.Data.SqlClient.WriteTransactionRollbackError";
	}
}
