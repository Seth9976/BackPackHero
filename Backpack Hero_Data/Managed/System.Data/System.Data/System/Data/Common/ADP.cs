using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.SqlServer.Server;

namespace System.Data.Common
{
	// Token: 0x02000313 RID: 787
	internal static class ADP
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x000A4B74 File Offset: 0x000A2D74
		internal static Timer UnsafeCreateTimer(TimerCallback callback, object state, int dueTime, int period)
		{
			bool flag = false;
			Timer timer;
			try
			{
				if (!ExecutionContext.IsFlowSuppressed())
				{
					ExecutionContext.SuppressFlow();
					flag = true;
				}
				timer = new Timer(callback, state, dueTime, period);
			}
			finally
			{
				if (flag)
				{
					ExecutionContext.RestoreFlow();
				}
			}
			return timer;
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000A4BB8 File Offset: 0x000A2DB8
		internal static Task<bool> TrueTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP._trueTask) == null)
				{
					task = (ADP._trueTask = Task.FromResult<bool>(true));
				}
				return task;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x000A4BCF File Offset: 0x000A2DCF
		internal static Task<bool> FalseTask
		{
			get
			{
				Task<bool> task;
				if ((task = ADP._falseTask) == null)
				{
					task = (ADP._falseTask = Task.FromResult<bool>(false));
				}
				return task;
			}
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0001245E File Offset: 0x0001065E
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x000A4BE6 File Offset: 0x000A2DE6
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x000A4BF3 File Offset: 0x000A2DF3
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x000A4C00 File Offset: 0x000A2E00
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000A4C0E File Offset: 0x000A2E0E
		internal static ArgumentException Argument(string error, Exception inner)
		{
			ArgumentException ex = new ArgumentException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000A4C1D File Offset: 0x000A2E1D
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000A4C3A File Offset: 0x000A2E3A
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000A4C49 File Offset: 0x000A2E49
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000A4C57 File Offset: 0x000A2E57
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000A4C66 File Offset: 0x000A2E66
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000A4C74 File Offset: 0x000A2E74
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000A4C7D File Offset: 0x000A2E7D
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x000A4C8C File Offset: 0x000A2E8C
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000A4C9A File Offset: 0x000A2E9A
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x000A4CA7 File Offset: 0x000A2EA7
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000A4CB8 File Offset: 0x000A2EB8
		internal static bool RemoveStringQuotes(string quotePrefix, string quoteSuffix, string quotedString, out string unquotedString)
		{
			int num = ((quotePrefix != null) ? quotePrefix.Length : 0);
			int num2 = ((quoteSuffix != null) ? quoteSuffix.Length : 0);
			if (num2 + num == 0)
			{
				unquotedString = quotedString;
				return true;
			}
			if (quotedString == null)
			{
				unquotedString = quotedString;
				return false;
			}
			int length = quotedString.Length;
			if (length < num + num2)
			{
				unquotedString = quotedString;
				return false;
			}
			if (num > 0 && !quotedString.StartsWith(quotePrefix, StringComparison.Ordinal))
			{
				unquotedString = quotedString;
				return false;
			}
			if (num2 > 0)
			{
				if (!quotedString.EndsWith(quoteSuffix, StringComparison.Ordinal))
				{
					unquotedString = quotedString;
					return false;
				}
				unquotedString = quotedString.Substring(num, length - (num + num2)).Replace(quoteSuffix + quoteSuffix, quoteSuffix);
			}
			else
			{
				unquotedString = quotedString.Substring(num, length - num);
			}
			return true;
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000A4D53 File Offset: 0x000A2F53
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, string value, string method)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is not supported by the {2} method.", type.Name, value, method), type.Name);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A4D72 File Offset: 0x000A2F72
		internal static InvalidOperationException DataAdapter(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x000A4D72 File Offset: 0x000A2F72
		private static InvalidOperationException Provider(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x000A4D7A File Offset: 0x000A2F7A
		internal static ArgumentException InvalidMultipartName(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\".", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000A4D93 File Offset: 0x000A2F93
		internal static ArgumentException InvalidMultipartNameIncorrectUsageOfQuotes(string property, string value)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", incorrect usage of quotes.", property, value));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000A4DAC File Offset: 0x000A2FAC
		internal static ArgumentException InvalidMultipartNameToManyParts(string property, string value, int limit)
		{
			ArgumentException ex = new ArgumentException(SR.Format("{0} \"{1}\", the current limit of \"{2}\" is insufficient.", property, value, limit));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000A4DCB File Offset: 0x000A2FCB
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw ADP.ArgumentNull(parameterName);
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000A4DD8 File Offset: 0x000A2FD8
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType && !ADP.s_securityType.IsAssignableFrom(type);
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x000A4E40 File Offset: 0x000A3040
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType;
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x000A4E95 File Offset: 0x000A3095
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is invalid.", type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000A4EBE File Offset: 0x000A30BE
		internal static ArgumentException ConnectionStringSyntax(int index)
		{
			return ADP.Argument(SR.Format("Format of the initialization string does not conform to specification starting at index {0}.", index));
		}

		// Token: 0x060023DB RID: 9179 RVA: 0x000A4ED5 File Offset: 0x000A30D5
		internal static ArgumentException KeywordNotSupported(string keyword)
		{
			return ADP.Argument(SR.Format("Keyword not supported: '{0}'.", keyword));
		}

		// Token: 0x060023DC RID: 9180 RVA: 0x000A4EE7 File Offset: 0x000A30E7
		internal static ArgumentException ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return ADP.Argument(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", fromType.FullName, toType.FullName), innerException);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000A4F05 File Offset: 0x000A3105
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return ADP.InvalidConnectionOptionValue(key, null);
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000A4F0E File Offset: 0x000A310E
		internal static Exception InvalidConnectionOptionValue(string key, Exception inner)
		{
			return ADP.Argument(SR.Format("Invalid value for key '{0}'.", key), inner);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x000A4F21 File Offset: 0x000A3121
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(SR.Format("Attempted to remove an {0} that is not contained by this {1}.", itemType.Name, collection.GetType().Name));
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A4F43 File Offset: 0x000A3143
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, SR.Format("The {0} only accepts non-null {1} type objects.", collection.Name, itemType.Name));
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x000A4F61 File Offset: 0x000A3161
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(SR.Format("Invalid index {0} for this {1} with Count={2}.", index.ToString(CultureInfo.InvariantCulture), collection.Name, count.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x000A4F90 File Offset: 0x000A3190
		internal static IndexOutOfRangeException CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return ADP.IndexOutOfRange(SR.Format("An {0} with {1} '{2}' is not contained by this {3}.", new object[] { itemType.Name, propertyName, propertyValue, collection.Name }));
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x000A4FC1 File Offset: 0x000A31C1
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(SR.Format("The {0} only accepts non-null {1} type objects, not {2} objects.", collection.Name, itemType.Name, invalidValue.GetType().Name));
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000A4FEC File Offset: 0x000A31EC
		private static string ConnectionStateMsg(ConnectionState state)
		{
			switch (state)
			{
			case ConnectionState.Closed:
				break;
			case ConnectionState.Open:
				return "The connection's current state is open.";
			case ConnectionState.Connecting:
				return "The connection's current state is connecting.";
			case ConnectionState.Open | ConnectionState.Connecting:
			case ConnectionState.Executing:
				goto IL_0046;
			case ConnectionState.Open | ConnectionState.Executing:
				return "The connection's current state is executing.";
			default:
				if (state == (ConnectionState.Open | ConnectionState.Fetching))
				{
					return "The connection's current state is fetching.";
				}
				if (state != (ConnectionState.Connecting | ConnectionState.Broken))
				{
					goto IL_0046;
				}
				break;
			}
			return "The connection's current state is closed.";
			IL_0046:
			return SR.Format("The connection's current state: {0}.", state.ToString());
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x000A5056 File Offset: 0x000A3256
		internal static Exception StreamClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to {0} when stream is closed.", method));
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x000A5068 File Offset: 0x000A3268
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(quotePrefix))
			{
				stringBuilder.Append(quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
			{
				stringBuilder.Append(unQuotedString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x000A50C0 File Offset: 0x000A32C0
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x000A50C0 File Offset: 0x000A32C0
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x000A50E2 File Offset: 0x000A32E2
		internal static Exception InternalError(ADP.InternalErrorCode internalError)
		{
			return ADP.InvalidOperation(SR.Format("Internal .Net Framework Data Provider error {0}.", (int)internalError));
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000A50F9 File Offset: 0x000A32F9
		internal static Exception DataReaderClosed([CallerMemberName] string method = "")
		{
			return ADP.InvalidOperation(SR.Format("Invalid attempt to call {0} when reader is closed.", method));
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000A510B File Offset: 0x000A330B
		internal static ArgumentOutOfRangeException InvalidSourceBufferIndex(int maxLen, long srcOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid source buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), srcOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000A5135 File Offset: 0x000A3335
		internal static ArgumentOutOfRangeException InvalidDestinationBufferIndex(int maxLen, int dstOffset, string parameterName)
		{
			return ADP.ArgumentOutOfRange(SR.Format("Invalid destination buffer (size of {0}) offset: {1}", maxLen.ToString(CultureInfo.InvariantCulture), dstOffset.ToString(CultureInfo.InvariantCulture)), parameterName);
		}

		// Token: 0x060023ED RID: 9197 RVA: 0x000A515F File Offset: 0x000A335F
		internal static IndexOutOfRangeException InvalidBufferSizeOrIndex(int numBytes, int bufferIndex)
		{
			return ADP.IndexOutOfRange(SR.Format("Buffer offset '{1}' plus the bytes available '{0}' is greater than the length of the passed in buffer.", numBytes.ToString(CultureInfo.InvariantCulture), bufferIndex.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060023EE RID: 9198 RVA: 0x000A5188 File Offset: 0x000A3388
		internal static Exception InvalidDataLength(long length)
		{
			return ADP.IndexOutOfRange(SR.Format("Data length '{0}' is less than 0.", length.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060023EF RID: 9199 RVA: 0x000A51A5 File Offset: 0x000A33A5
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x060023F0 RID: 9200 RVA: 0x000A51BC File Offset: 0x000A33BC
		internal static int DstCompare(string strA, string strB)
		{
			return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
		}

		// Token: 0x060023F1 RID: 9201 RVA: 0x000A51D1 File Offset: 0x000A33D1
		internal static bool IsEmptyArray(string[] array)
		{
			return array == null || array.Length == 0;
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x000A51E0 File Offset: 0x000A33E0
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x060023F3 RID: 9203 RVA: 0x000A520C File Offset: 0x000A340C
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange("Specified SeekOrigin value is invalid.", parameterName);
		}

		// Token: 0x060023F4 RID: 9204 RVA: 0x000A5219 File Offset: 0x000A3419
		internal static void SetCurrentTransaction(Transaction transaction)
		{
			Transaction.Current = transaction;
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000A5221 File Offset: 0x000A3421
		internal static Task<T> CreatedTaskWithCancellation<T>()
		{
			return Task.FromCanceled<T>(new CancellationToken(true));
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000A522E File Offset: 0x000A342E
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000A523B File Offset: 0x000A343B
		internal static DataException Data(string message)
		{
			DataException ex = new DataException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x000A5249 File Offset: 0x000A3449
		internal static void CheckArgumentLength(string value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty string for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x060023F9 RID: 9209 RVA: 0x000A526B File Offset: 0x000A346B
		internal static void CheckArgumentLength(Array value, string parameterName)
		{
			ADP.CheckArgumentNull(value, parameterName);
			if (value.Length == 0)
			{
				throw ADP.Argument(SR.Format("Expecting non-empty array for '{0}' parameter.", parameterName));
			}
		}

		// Token: 0x060023FA RID: 9210 RVA: 0x000A528D File Offset: 0x000A348D
		internal static ArgumentOutOfRangeException InvalidAcceptRejectRule(AcceptRejectRule value)
		{
			return ADP.InvalidEnumerationValue(typeof(AcceptRejectRule), (int)value);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000A529F File Offset: 0x000A349F
		internal static ArgumentOutOfRangeException InvalidCatalogLocation(CatalogLocation value)
		{
			return ADP.InvalidEnumerationValue(typeof(CatalogLocation), (int)value);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000A52B1 File Offset: 0x000A34B1
		internal static ArgumentOutOfRangeException InvalidConflictOptions(ConflictOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(ConflictOption), (int)value);
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x000A52C3 File Offset: 0x000A34C3
		internal static ArgumentOutOfRangeException InvalidDataRowState(DataRowState value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowState), (int)value);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000A52D5 File Offset: 0x000A34D5
		internal static ArgumentOutOfRangeException InvalidKeyRestrictionBehavior(KeyRestrictionBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(KeyRestrictionBehavior), (int)value);
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x000A52E7 File Offset: 0x000A34E7
		internal static ArgumentOutOfRangeException InvalidLoadOption(LoadOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(LoadOption), (int)value);
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x000A52F9 File Offset: 0x000A34F9
		internal static ArgumentOutOfRangeException InvalidMissingMappingAction(MissingMappingAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingMappingAction), (int)value);
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x000A530B File Offset: 0x000A350B
		internal static ArgumentOutOfRangeException InvalidMissingSchemaAction(MissingSchemaAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingSchemaAction), (int)value);
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000A531D File Offset: 0x000A351D
		internal static ArgumentOutOfRangeException InvalidRule(Rule value)
		{
			return ADP.InvalidEnumerationValue(typeof(Rule), (int)value);
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x000A532F File Offset: 0x000A352F
		internal static ArgumentOutOfRangeException InvalidSchemaType(SchemaType value)
		{
			return ADP.InvalidEnumerationValue(typeof(SchemaType), (int)value);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x000A5341 File Offset: 0x000A3541
		internal static ArgumentOutOfRangeException InvalidStatementType(StatementType value)
		{
			return ADP.InvalidEnumerationValue(typeof(StatementType), (int)value);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x000A5353 File Offset: 0x000A3553
		internal static ArgumentOutOfRangeException InvalidUpdateStatus(UpdateStatus value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateStatus), (int)value);
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x000A5365 File Offset: 0x000A3565
		internal static ArgumentOutOfRangeException NotSupportedStatementType(StatementType value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(StatementType), value.ToString(), method);
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x000A5384 File Offset: 0x000A3584
		internal static ArgumentException InvalidKeyname(string parameterName)
		{
			return ADP.Argument("Invalid keyword, contain one or more of 'no characters', 'control characters', 'leading or trailing whitespace' or 'leading semicolons'.", parameterName);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x000A5391 File Offset: 0x000A3591
		internal static ArgumentException InvalidValue(string parameterName)
		{
			return ADP.Argument("The value contains embedded nulls (\\\\u0000).", parameterName);
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x000A539E File Offset: 0x000A359E
		internal static Exception WrongType(Type got, Type expected)
		{
			return ADP.Argument(SR.Format("Expecting argument of type {1}, but received type {0}.", got.ToString(), expected.ToString()));
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000A53BB File Offset: 0x000A35BB
		internal static Exception CollectionUniqueValue(Type itemType, string propertyName, string propertyValue)
		{
			return ADP.Argument(SR.Format("The {0}.{1} is required to be unique, '{2}' already exists in the collection.", itemType.Name, propertyName, propertyValue));
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x000A53D4 File Offset: 0x000A35D4
		internal static InvalidOperationException MissingSelectCommand(string method)
		{
			return ADP.Provider(SR.Format("The SelectCommand property has not been initialized before calling '{0}'.", method));
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x000A4D72 File Offset: 0x000A2F72
		private static InvalidOperationException DataMapping(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x000A53E6 File Offset: 0x000A35E6
		internal static InvalidOperationException ColumnSchemaExpression(string srcColumn, string cacheColumn)
		{
			return ADP.DataMapping(SR.Format("The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.", srcColumn, cacheColumn));
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x000A53F9 File Offset: 0x000A35F9
		internal static InvalidOperationException ColumnSchemaMismatch(string srcColumn, Type srcType, DataColumn column)
		{
			return ADP.DataMapping(SR.Format("Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.", new object[]
			{
				srcColumn,
				srcType.Name,
				column.ColumnName,
				column.DataType.Name
			}));
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000A5434 File Offset: 0x000A3634
		internal static InvalidOperationException ColumnSchemaMissing(string cacheColumn, string tableName, string srcColumn)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				return ADP.InvalidOperation(SR.Format("Missing the DataColumn '{0}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
			}
			return ADP.DataMapping(SR.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x000A5463 File Offset: 0x000A3663
		internal static InvalidOperationException MissingColumnMapping(string srcColumn)
		{
			return ADP.DataMapping(SR.Format("Missing SourceColumn mapping for '{0}'.", srcColumn));
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x000A5475 File Offset: 0x000A3675
		internal static InvalidOperationException MissingTableSchema(string cacheTable, string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing the '{0}' DataTable for the '{1}' SourceTable.", cacheTable, srcTable));
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x000A5488 File Offset: 0x000A3688
		internal static InvalidOperationException MissingTableMapping(string srcTable)
		{
			return ADP.DataMapping(SR.Format("Missing SourceTable mapping: '{0}'", srcTable));
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000A549A File Offset: 0x000A369A
		internal static InvalidOperationException MissingTableMappingDestination(string dstTable)
		{
			return ADP.DataMapping(SR.Format("Missing TableMapping when TableMapping.DataSetTable='{0}'.", dstTable));
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x000A54AC File Offset: 0x000A36AC
		internal static Exception InvalidSourceColumn(string parameter)
		{
			return ADP.Argument("SourceColumn is required to be a non-empty string.", parameter);
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x000A54B9 File Offset: 0x000A36B9
		internal static Exception ColumnsAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataColumnMappingCollection), typeof(DataColumnMapping));
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x000A54D5 File Offset: 0x000A36D5
		internal static Exception ColumnsDataSetColumn(string cacheColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "DataSetColumn", cacheColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x000A54F6 File Offset: 0x000A36F6
		internal static Exception ColumnsIndexInt32(int index, IColumnMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000A550A File Offset: 0x000A370A
		internal static Exception ColumnsIndexSource(string srcColumn)
		{
			return ADP.CollectionIndexString(typeof(DataColumnMapping), "SourceColumn", srcColumn, typeof(DataColumnMappingCollection));
		}

		// Token: 0x06002419 RID: 9241 RVA: 0x000A552B File Offset: 0x000A372B
		internal static Exception ColumnsIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x000A553D File Offset: 0x000A373D
		internal static Exception ColumnsIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x000A554F File Offset: 0x000A374F
		internal static Exception ColumnsUniqueSourceColumn(string srcColumn)
		{
			return ADP.CollectionUniqueValue(typeof(DataColumnMapping), "SourceColumn", srcColumn);
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000A5566 File Offset: 0x000A3766
		internal static Exception NotADataColumnMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataColumnMappingCollection), typeof(DataColumnMapping), value);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000A5582 File Offset: 0x000A3782
		internal static Exception InvalidSourceTable(string parameter)
		{
			return ADP.Argument("SourceTable is required to be a non-empty string", parameter);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x000A558F File Offset: 0x000A378F
		internal static Exception TablesAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataTableMappingCollection), typeof(DataTableMapping));
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x000A55AB File Offset: 0x000A37AB
		internal static Exception TablesDataSetTable(string cacheTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "DataSetTable", cacheTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000A54F6 File Offset: 0x000A36F6
		internal static Exception TablesIndexInt32(int index, ITableMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000A55CC File Offset: 0x000A37CC
		internal static Exception TablesIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000A55DE File Offset: 0x000A37DE
		internal static Exception TablesIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataTableMapping), collection);
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x000A55F0 File Offset: 0x000A37F0
		internal static Exception TablesSourceIndex(string srcTable)
		{
			return ADP.CollectionIndexString(typeof(DataTableMapping), "SourceTable", srcTable, typeof(DataTableMappingCollection));
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x000A5611 File Offset: 0x000A3811
		internal static Exception TablesUniqueSourceTable(string srcTable)
		{
			return ADP.CollectionUniqueValue(typeof(DataTableMapping), "SourceTable", srcTable);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000A5628 File Offset: 0x000A3828
		internal static Exception NotADataTableMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataTableMappingCollection), typeof(DataTableMapping), value);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x000A5644 File Offset: 0x000A3844
		internal static InvalidOperationException UpdateConnectionRequired(StatementType statementType, bool isRowUpdatingCommand)
		{
			string text;
			if (!isRowUpdatingCommand)
			{
				switch (statementType)
				{
				case StatementType.Insert:
					text = "Update requires the InsertCommand to have a connection object. The Connection property of the InsertCommand has not been initialized.";
					goto IL_004A;
				case StatementType.Update:
					text = "Update requires the UpdateCommand to have a connection object. The Connection property of the UpdateCommand has not been initialized.";
					goto IL_004A;
				case StatementType.Delete:
					text = "Update requires the DeleteCommand to have a connection object. The Connection property of the DeleteCommand has not been initialized.";
					goto IL_004A;
				}
				throw ADP.InvalidStatementType(statementType);
			}
			text = "Update requires the command clone to have a connection object. The Connection property of the command clone has not been initialized.";
			IL_004A:
			return ADP.InvalidOperation(text);
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000A56A1 File Offset: 0x000A38A1
		internal static InvalidOperationException ConnectionRequired_Res(string method)
		{
			return ADP.InvalidOperation("ADP_ConnectionRequired_" + method);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000A56B4 File Offset: 0x000A38B4
		internal static InvalidOperationException UpdateOpenConnectionRequired(StatementType statementType, bool isRowUpdatingCommand, ConnectionState state)
		{
			string text;
			if (isRowUpdatingCommand)
			{
				text = "Update requires the updating command to have an open connection object. {1}";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Insert:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Update:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				case StatementType.Delete:
					text = "Update requires the {0}Command to have an open connection object. {1}";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(SR.Format(text, ADP.ConnectionStateMsg(state)));
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x000A5712 File Offset: 0x000A3912
		internal static ArgumentException UnwantedStatementType(StatementType statementType)
		{
			return ADP.Argument(SR.Format("The StatementType {0} is not expected here.", statementType.ToString()));
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x000A5730 File Offset: 0x000A3930
		internal static Exception FillSchemaRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("FillSchema: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x000A573D File Offset: 0x000A393D
		internal static Exception InvalidMaxRecords(string parameter, int max)
		{
			return ADP.Argument(SR.Format("The MaxRecords value of {0} is invalid; the value must be >= 0.", max.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x000A575B File Offset: 0x000A395B
		internal static Exception InvalidStartRecord(string parameter, int start)
		{
			return ADP.Argument(SR.Format("The StartRecord value of {0} is invalid; the value must be >= 0.", start.ToString(CultureInfo.InvariantCulture)), parameter);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x000A5779 File Offset: 0x000A3979
		internal static Exception FillRequires(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000A5781 File Offset: 0x000A3981
		internal static Exception FillRequiresSourceTableName(string parameter)
		{
			return ADP.Argument("Fill: expected a non-empty string for the SourceTable name.", parameter);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000A578E File Offset: 0x000A398E
		internal static Exception FillChapterAutoIncrement()
		{
			return ADP.InvalidOperation("Hierarchical chapter columns must map to an AutoIncrement DataColumn.");
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000A579A File Offset: 0x000A399A
		internal static InvalidOperationException MissingDataReaderFieldType(int index)
		{
			return ADP.DataAdapter(SR.Format("DataReader.GetFieldType({0}) returned null.", index));
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000A57B1 File Offset: 0x000A39B1
		internal static InvalidOperationException OnlyOneTableForStartRecordOrMaxRecords()
		{
			return ADP.DataAdapter("Only specify one item in the dataTables array when using non-zero values for startRecords or maxRecords.");
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000A5779 File Offset: 0x000A3979
		internal static ArgumentNullException UpdateRequiresNonNullDataSet(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000A57BD File Offset: 0x000A39BD
		internal static InvalidOperationException UpdateRequiresSourceTable(string defaultSrcTableName)
		{
			return ADP.InvalidOperation(SR.Format("Update unable to find TableMapping['{0}'] or DataTable '{0}'.", defaultSrcTableName));
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000A57CF File Offset: 0x000A39CF
		internal static InvalidOperationException UpdateRequiresSourceTableName(string srcTable)
		{
			return ADP.InvalidOperation(SR.Format("Update: expected a non-empty SourceTable name.", srcTable));
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000A5779 File Offset: 0x000A3979
		internal static ArgumentNullException UpdateRequiresDataTable(string parameter)
		{
			return ADP.ArgumentNull(parameter);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000A57E4 File Offset: 0x000A39E4
		internal static Exception UpdateConcurrencyViolation(StatementType statementType, int affected, int expected, DataRow[] dataRows)
		{
			string text;
			switch (statementType)
			{
			case StatementType.Update:
				text = "Concurrency violation: the UpdateCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Delete:
				text = "Concurrency violation: the DeleteCommand affected {0} of the expected {1} records.";
				break;
			case StatementType.Batch:
				text = "Concurrency violation: the batched command affected {0} of the expected {1} records.";
				break;
			default:
				throw ADP.InvalidStatementType(statementType);
			}
			DBConcurrencyException ex = new DBConcurrencyException(SR.Format(text, affected.ToString(CultureInfo.InvariantCulture), expected.ToString(CultureInfo.InvariantCulture)), null, dataRows);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000A5854 File Offset: 0x000A3A54
		internal static InvalidOperationException UpdateRequiresCommand(StatementType statementType, bool isRowUpdatingCommand)
		{
			string text;
			if (isRowUpdatingCommand)
			{
				text = "Update requires the command clone to be valid.";
			}
			else
			{
				switch (statementType)
				{
				case StatementType.Select:
					text = "Auto SQL generation during Update requires a valid SelectCommand.";
					break;
				case StatementType.Insert:
					text = "Update requires a valid InsertCommand when passed DataRow collection with new rows.";
					break;
				case StatementType.Update:
					text = "Update requires a valid UpdateCommand when passed DataRow collection with modified rows.";
					break;
				case StatementType.Delete:
					text = "Update requires a valid DeleteCommand when passed DataRow collection with deleted rows.";
					break;
				default:
					throw ADP.InvalidStatementType(statementType);
				}
			}
			return ADP.InvalidOperation(text);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000A58B1 File Offset: 0x000A3AB1
		internal static ArgumentException UpdateMismatchRowTable(int i)
		{
			return ADP.Argument(SR.Format("DataRow[{0}] is from a different DataTable than DataRow[0].", i.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000A58CE File Offset: 0x000A3ACE
		internal static DataException RowUpdatedErrors()
		{
			return ADP.Data("RowUpdatedEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000A58DA File Offset: 0x000A3ADA
		internal static DataException RowUpdatingErrors()
		{
			return ADP.Data("RowUpdatingEvent: Errors occurred; no additional is information available.");
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000A58E6 File Offset: 0x000A3AE6
		internal static InvalidOperationException ResultsNotAllowedDuringBatch()
		{
			return ADP.DataAdapter("When batching, the command's UpdatedRowSource property value of UpdateRowSource.FirstReturnedRecord or UpdateRowSource.Both is invalid.");
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000A58F2 File Offset: 0x000A3AF2
		internal static InvalidOperationException DynamicSQLJoinUnsupported()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against multiple base tables.");
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000A58FE File Offset: 0x000A3AFE
		internal static InvalidOperationException DynamicSQLNoTableInfo()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.");
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000A590A File Offset: 0x000A3B0A
		internal static InvalidOperationException DynamicSQLNoKeyInfoDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000A5916 File Offset: 0x000A3B16
		internal static InvalidOperationException DynamicSQLNoKeyInfoUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000A5922 File Offset: 0x000A3B22
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000A592E File Offset: 0x000A3B2E
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000A593A File Offset: 0x000A3B3A
		internal static InvalidOperationException DynamicSQLNestedQuote(string name, string quote)
		{
			return ADP.InvalidOperation(SR.Format("Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.", name, quote));
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000A594D File Offset: 0x000A3B4D
		internal static InvalidOperationException NoQuoteChange()
		{
			return ADP.InvalidOperation("The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.");
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000A5959 File Offset: 0x000A3B59
		internal static InvalidOperationException MissingSourceCommand()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand property needs to be initialized.");
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x000A5965 File Offset: 0x000A3B65
		internal static InvalidOperationException MissingSourceCommandConnection()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand.Connection property needs to be initialized;");
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000A5974 File Offset: 0x000A3B74
		internal static DataRow[] SelectAdapterRows(DataTable dataTable, bool sorted)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			DataRowCollection rows = dataTable.Rows;
			foreach (object obj in rows)
			{
				DataRowState dataRowState = ((DataRow)obj).RowState;
				if (dataRowState != DataRowState.Added)
				{
					if (dataRowState != DataRowState.Deleted)
					{
						if (dataRowState == DataRowState.Modified)
						{
							num3++;
						}
					}
					else
					{
						num2++;
					}
				}
				else
				{
					num++;
				}
			}
			DataRow[] array = new DataRow[num + num2 + num3];
			if (sorted)
			{
				num3 = num + num2;
				num2 = num;
				num = 0;
				using (IEnumerator enumerator = rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataRow dataRow = (DataRow)obj2;
						DataRowState dataRowState = dataRow.RowState;
						if (dataRowState != DataRowState.Added)
						{
							if (dataRowState != DataRowState.Deleted)
							{
								if (dataRowState == DataRowState.Modified)
								{
									array[num3++] = dataRow;
								}
							}
							else
							{
								array[num2++] = dataRow;
							}
						}
						else
						{
							array[num++] = dataRow;
						}
					}
					return array;
				}
			}
			int num4 = 0;
			foreach (object obj3 in rows)
			{
				DataRow dataRow2 = (DataRow)obj3;
				if ((dataRow2.RowState & (DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					array[num4++] = dataRow2;
					if (num4 == array.Length)
					{
						break;
					}
				}
			}
			return array;
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000A5B00 File Offset: 0x000A3D00
		internal static void BuildSchemaTableInfoTableNames(string[] columnNameArray)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNameArray.Length);
			int num = columnNameArray.Length;
			int num2 = columnNameArray.Length - 1;
			while (0 <= num2)
			{
				string text = columnNameArray[num2];
				if (text != null && 0 < text.Length)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					int num3;
					if (dictionary.TryGetValue(text, out num3))
					{
						num = Math.Min(num, num3);
					}
					dictionary[text] = num2;
				}
				else
				{
					columnNameArray[num2] = string.Empty;
					num = num2;
				}
				num2--;
			}
			int num4 = 1;
			for (int i = num; i < columnNameArray.Length; i++)
			{
				string text2 = columnNameArray[i];
				if (text2.Length == 0)
				{
					columnNameArray[i] = "Column";
					num4 = ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, num4);
				}
				else
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					if (i != dictionary[text2])
					{
						ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, 1);
					}
				}
			}
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000A5BE4 File Offset: 0x000A3DE4
		private static int GenerateUniqueName(Dictionary<string, int> hash, ref string columnName, int index, int uniqueIndex)
		{
			string text;
			for (;;)
			{
				text = columnName + uniqueIndex.ToString(CultureInfo.InvariantCulture);
				string text2 = text.ToLower(CultureInfo.InvariantCulture);
				if (hash.TryAdd(text2, index))
				{
					break;
				}
				uniqueIndex++;
			}
			columnName = text;
			return uniqueIndex;
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000A5C28 File Offset: 0x000A3E28
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x000A5C38 File Offset: 0x000A3E38
		internal static Exception ExceptionWithStackTrace(Exception e)
		{
			try
			{
				throw e;
			}
			catch (Exception ex)
			{
			}
			Exception ex;
			return ex;
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x000A5C5C File Offset: 0x000A3E5C
		internal static IndexOutOfRangeException IndexOutOfRange(int value)
		{
			return new IndexOutOfRangeException(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x000A5C6F File Offset: 0x000A3E6F
		internal static IndexOutOfRangeException IndexOutOfRange()
		{
			return new IndexOutOfRangeException();
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x000A5C76 File Offset: 0x000A3E76
		internal static TimeoutException TimeoutException(string error)
		{
			return new TimeoutException(error);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x000A5C7E File Offset: 0x000A3E7E
		internal static InvalidOperationException InvalidOperation(string error, Exception inner)
		{
			return new InvalidOperationException(error, inner);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x000A5C87 File Offset: 0x000A3E87
		internal static OverflowException Overflow(string error)
		{
			return ADP.Overflow(error, null);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x000A5C90 File Offset: 0x000A3E90
		internal static OverflowException Overflow(string error, Exception inner)
		{
			return new OverflowException(error, inner);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x000A5C99 File Offset: 0x000A3E99
		internal static TypeLoadException TypeLoad(string error)
		{
			TypeLoadException ex = new TypeLoadException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x000A5CA7 File Offset: 0x000A3EA7
		internal static PlatformNotSupportedException DbTypeNotSupported(string dbType)
		{
			return new PlatformNotSupportedException(SR.GetString("Type {0} is not supported on this platform.", new object[] { dbType }));
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x000A5CC2 File Offset: 0x000A3EC2
		internal static InvalidCastException InvalidCast()
		{
			return new InvalidCastException();
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000A5CC9 File Offset: 0x000A3EC9
		internal static IOException IO(string error)
		{
			return new IOException(error);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000A5CD1 File Offset: 0x000A3ED1
		internal static IOException IO(string error, Exception inner)
		{
			return new IOException(error, inner);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000A5CDA File Offset: 0x000A3EDA
		internal static ObjectDisposedException ObjectDisposed(object instance)
		{
			return new ObjectDisposedException(instance.GetType().Name);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x000A5CEC File Offset: 0x000A3EEC
		internal static Exception DataTableDoesNotExist(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection '{0}' is missing from the metadata XML.", new object[] { collectionName }));
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000A5D07 File Offset: 0x000A3F07
		internal static InvalidOperationException MethodCalledTwice(string method)
		{
			return new InvalidOperationException(SR.GetString("The method '{0}' cannot be called more than once for the same execution.", new object[] { method }));
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x000A5D22 File Offset: 0x000A3F22
		internal static ArgumentOutOfRangeException InvalidCommandType(CommandType value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000A5D34 File Offset: 0x000A3F34
		internal static ArgumentOutOfRangeException InvalidIsolationLevel(IsolationLevel value)
		{
			return ADP.InvalidEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000A5D46 File Offset: 0x000A3F46
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			return ADP.InvalidEnumerationValue(typeof(ParameterDirection), (int)value);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000A5D58 File Offset: 0x000A3F58
		internal static Exception TooManyRestrictions(string collectionName)
		{
			return ADP.Argument(SR.GetString("More restrictions were provided than the requested schema ('{0}') supports.", new object[] { collectionName }));
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x000A5D73 File Offset: 0x000A3F73
		internal static ArgumentOutOfRangeException InvalidUpdateRowSource(UpdateRowSource value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateRowSource), (int)value);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x000A5D85 File Offset: 0x000A3F85
		internal static ArgumentException InvalidMinMaxPoolSizeValues()
		{
			return ADP.Argument(SR.GetString("Invalid min or max pool size values, min pool size cannot be greater than the max pool size."));
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x000A5D96 File Offset: 0x000A3F96
		internal static InvalidOperationException NoConnectionString()
		{
			return ADP.InvalidOperation(SR.GetString("The ConnectionString property has not been initialized."));
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x000A5DA7 File Offset: 0x000A3FA7
		internal static Exception MethodNotImplemented([CallerMemberName] string methodName = "")
		{
			return global::System.NotImplemented.ByDesignWithMessage(methodName);
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x000A5DAF File Offset: 0x000A3FAF
		internal static Exception QueryFailed(string collectionName, Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("Unable to build the '{0}' collection because execution of the SQL query failed. See the inner exception for details.", new object[] { collectionName }), e);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x000A5DCB File Offset: 0x000A3FCB
		internal static Exception InvalidConnectionOptionValueLength(string key, int limit)
		{
			return ADP.Argument(SR.GetString("The value's length for key '{0}' exceeds it's limit of '{1}'.", new object[] { key, limit }));
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x000A5DEF File Offset: 0x000A3FEF
		internal static Exception MissingConnectionOptionValue(string key, string requiredAdditionalKey)
		{
			return ADP.Argument(SR.GetString("Use of key '{0}' requires the key '{1}' to be present.", new object[] { key, requiredAdditionalKey }));
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x000A5E0E File Offset: 0x000A400E
		internal static Exception PooledOpenTimeout()
		{
			return ADP.InvalidOperation(SR.GetString("Timeout expired.  The timeout period elapsed prior to obtaining a connection from the pool.  This may have occurred because all pooled connections were in use and max pool size was reached."));
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x000A5E1F File Offset: 0x000A401F
		internal static Exception NonPooledOpenTimeout()
		{
			return ADP.TimeoutException(SR.GetString("Timeout attempting to open the connection.  The time period elapsed prior to attempting to open the connection has been exceeded.  This may have occurred because of too many simultaneous non-pooled connection attempts."));
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000A5E30 File Offset: 0x000A4030
		internal static InvalidOperationException TransactionConnectionMismatch()
		{
			return ADP.Provider(SR.GetString("The transaction is either not associated with the current connection or has been completed."));
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000A5E41 File Offset: 0x000A4041
		internal static InvalidOperationException TransactionRequired(string method)
		{
			return ADP.Provider(SR.GetString("{0} requires the command to have a transaction when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized.", new object[] { method }));
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x000A5E5C File Offset: 0x000A405C
		internal static Exception CommandTextRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: CommandText property has not been initialized", new object[] { method }));
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x000A5E77 File Offset: 0x000A4077
		internal static Exception NoColumns()
		{
			return ADP.Argument(SR.GetString("The schema table contains no columns."));
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000A5E88 File Offset: 0x000A4088
		internal static InvalidOperationException ConnectionRequired(string method)
		{
			return ADP.InvalidOperation(SR.GetString("{0}: Connection property has not been initialized.", new object[] { method }));
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000A5EA3 File Offset: 0x000A40A3
		internal static InvalidOperationException OpenConnectionRequired(string method, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("{0} requires an open and available Connection. {1}", new object[]
			{
				method,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000A5EC7 File Offset: 0x000A40C7
		internal static Exception OpenReaderExists()
		{
			return ADP.OpenReaderExists(null);
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x000A5ECF File Offset: 0x000A40CF
		internal static Exception OpenReaderExists(Exception e)
		{
			return ADP.InvalidOperation(SR.GetString("There is already an open DataReader associated with this Command which must be closed first."), e);
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000A5EE1 File Offset: 0x000A40E1
		internal static Exception NonSeqByteAccess(long badIndex, long currIndex, string method)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid {2} attempt at dataIndex '{0}'.  With CommandBehavior.SequentialAccess, you may only read from dataIndex '{1}' or greater.", new object[]
			{
				badIndex.ToString(CultureInfo.InvariantCulture),
				currIndex.ToString(CultureInfo.InvariantCulture),
				method
			}));
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000A5F1A File Offset: 0x000A411A
		internal static Exception InvalidXml()
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid."));
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000A5F2B File Offset: 0x000A412B
		internal static Exception NegativeParameter(string parameterName)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid value for argument '{0}'. The value must be greater than or equal to 0.", new object[] { parameterName }));
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x000A5F46 File Offset: 0x000A4146
		internal static Exception InvalidXmlMissingColumn(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {0} collection must contain a {1} column and it must be a string column.", new object[] { collectionName, columnName }));
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x000A5F65 File Offset: 0x000A4165
		internal static Exception InvalidMetaDataValue()
		{
			return ADP.Argument(SR.GetString("Invalid value for this metadata."));
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x000A5F76 File Offset: 0x000A4176
		internal static InvalidOperationException NonSequentialColumnAccess(int badCol, int currCol)
		{
			return ADP.InvalidOperation(SR.GetString("Invalid attempt to read from column ordinal '{0}'.  With CommandBehavior.SequentialAccess, you may only read from column ordinal '{1}' or greater.", new object[]
			{
				badCol.ToString(CultureInfo.InvariantCulture),
				currCol.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000A5FAB File Offset: 0x000A41AB
		internal static Exception InvalidXmlInvalidValue(string collectionName, string columnName)
		{
			return ADP.Argument(SR.GetString("The metadata XML is invalid. The {1} column of the {0} collection must contain a non-empty string.", new object[] { collectionName, columnName }));
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000A5FCA File Offset: 0x000A41CA
		internal static Exception CollectionNameIsNotUnique(string collectionName)
		{
			return ADP.Argument(SR.GetString("There are multiple collections named '{0}'.", new object[] { collectionName }));
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000A5FE5 File Offset: 0x000A41E5
		internal static Exception InvalidCommandTimeout(int value, [CallerMemberName] string property = "")
		{
			return ADP.Argument(SR.GetString("Invalid CommandTimeout value {0}; the value must be >= 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }), property);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000A600C File Offset: 0x000A420C
		internal static Exception UninitializedParameterSize(int index, Type dataType)
		{
			return ADP.InvalidOperation(SR.GetString("{1}[{0}]: the Size property has an invalid size of 0.", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				dataType.Name
			}));
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000A603B File Offset: 0x000A423B
		internal static Exception UnableToBuildCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("Unable to build schema collection '{0}';", new object[] { collectionName }));
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000A6056 File Offset: 0x000A4256
		internal static Exception PrepareParameterType(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all parameters to have an explicitly set type.", new object[] { cmd.GetType().Name }));
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000A607B File Offset: 0x000A427B
		internal static Exception UndefinedCollection(string collectionName)
		{
			return ADP.Argument(SR.GetString("The requested collection ({0}) is not defined.", new object[] { collectionName }));
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000A6096 File Offset: 0x000A4296
		internal static Exception UnsupportedVersion(string collectionName)
		{
			return ADP.Argument(SR.GetString(" requested collection ({0}) is not supported by this version of the provider.", new object[] { collectionName }));
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000A60B1 File Offset: 0x000A42B1
		internal static Exception AmbigousCollectionName(string collectionName)
		{
			return ADP.Argument(SR.GetString("The collection name '{0}' matches at least two collections with the same name but with different case, but does not match any of them exactly.", new object[] { collectionName }));
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000A60CC File Offset: 0x000A42CC
		internal static Exception PrepareParameterSize(DbCommand cmd)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.", new object[] { cmd.GetType().Name }));
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000A60F1 File Offset: 0x000A42F1
		internal static Exception PrepareParameterScale(DbCommand cmd, string type)
		{
			return ADP.InvalidOperation(SR.GetString("{0}.Prepare method requires parameters of type '{1}' have an explicitly set Precision and Scale.", new object[]
			{
				cmd.GetType().Name,
				type
			}));
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x000A611A File Offset: 0x000A431A
		internal static Exception MissingDataSourceInformationColumn()
		{
			return ADP.Argument(SR.GetString("One of the required DataSourceInformation tables columns is missing."));
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000A612B File Offset: 0x000A432B
		internal static Exception IncorrectNumberOfDataSourceInformationRows()
		{
			return ADP.Argument(SR.GetString("The DataSourceInformation table must contain exactly one row."));
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000A613C File Offset: 0x000A433C
		internal static Exception MismatchedAsyncResult(string expectedMethod, string gotMethod)
		{
			return ADP.InvalidOperation(SR.GetString("Mismatched end method call for asyncResult.  Expected call to {0} but {1} was called instead.", new object[] { expectedMethod, gotMethod }));
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000A615B File Offset: 0x000A435B
		internal static Exception ClosedConnectionError()
		{
			return ADP.InvalidOperation(SR.GetString("Invalid operation. The connection is closed."));
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x000A616C File Offset: 0x000A436C
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("The connection was not closed. {0}", new object[] { ADP.ConnectionStateMsg(state) }));
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000A618C File Offset: 0x000A438C
		internal static Exception TransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Connection currently has transaction enlisted.  Finish current transaction and retry."));
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000A619D File Offset: 0x000A439D
		internal static Exception LocalTransactionPresent()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot enlist in the transaction because a local transaction is in progress on the connection.  Finish local transaction and retry."));
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x000A61AE File Offset: 0x000A43AE
		internal static Exception OpenConnectionPropertySet(string property, ConnectionState state)
		{
			return ADP.InvalidOperation(SR.GetString("Not allowed to change the '{0}' property. {1}", new object[]
			{
				property,
				ADP.ConnectionStateMsg(state)
			}));
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000A61D2 File Offset: 0x000A43D2
		internal static Exception EmptyDatabaseName()
		{
			return ADP.Argument(SR.GetString("Database cannot be null, the empty string, or string of only whitespace."));
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000A61E3 File Offset: 0x000A43E3
		internal static Exception MissingRestrictionColumn()
		{
			return ADP.Argument(SR.GetString("One or more of the required columns of the restrictions collection is missing."));
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x000A61F4 File Offset: 0x000A43F4
		internal static Exception InternalConnectionError(ADP.ConnectionError internalError)
		{
			return ADP.InvalidOperation(SR.GetString("Internal DbConnection Error: {0}", new object[] { (int)internalError }));
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000A6214 File Offset: 0x000A4414
		internal static Exception InvalidConnectRetryCountValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryCount value (should be 0-255)."));
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000A6225 File Offset: 0x000A4425
		internal static Exception MissingRestrictionRow()
		{
			return ADP.Argument(SR.GetString("A restriction exists for which there is no matching row in the restrictions collection."));
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x000A6236 File Offset: 0x000A4436
		internal static Exception InvalidConnectRetryIntervalValue()
		{
			return ADP.Argument(SR.GetString("Invalid ConnectRetryInterval value (should be 1-60)."));
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x000A6247 File Offset: 0x000A4447
		internal static InvalidOperationException AsyncOperationPending()
		{
			return ADP.InvalidOperation(SR.GetString("Can not start another operation while there is an asynchronous operation pending."));
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000A6258 File Offset: 0x000A4458
		internal static IOException ErrorReadingFromStream(Exception internalException)
		{
			return ADP.IO(SR.GetString("An error occurred while reading."), internalException);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000A626A File Offset: 0x000A446A
		internal static ArgumentException InvalidDataType(TypeCode typecode)
		{
			return ADP.Argument(SR.GetString("The parameter data type of {0} is invalid.", new object[] { typecode.ToString() }));
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000A6291 File Offset: 0x000A4491
		internal static ArgumentException UnknownDataType(Type dataType)
		{
			return ADP.Argument(SR.GetString("No mapping exists from object type {0} to a known managed provider native type.", new object[] { dataType.FullName }));
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000A62B1 File Offset: 0x000A44B1
		internal static ArgumentException DbTypeNotSupported(DbType type, Type enumtype)
		{
			return ADP.Argument(SR.GetString("No mapping exists from DbType {0} to a known {1}.", new object[]
			{
				type.ToString(),
				enumtype.Name
			}));
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000A62E4 File Offset: 0x000A44E4
		internal static ArgumentException UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string text = "Unable to handle an unknown TypeCode {0} returned by Type {1}.";
			object[] array = new object[2];
			int num = 0;
			int num2 = (int)typeCode;
			array[num] = num2.ToString(CultureInfo.InvariantCulture);
			array[1] = dataType.FullName;
			return ADP.Argument(SR.GetString(text, array));
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x000A6320 File Offset: 0x000A4520
		internal static ArgumentException InvalidOffsetValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Offset value '{0}'. The value must be greater than or equal to 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x000A6346 File Offset: 0x000A4546
		internal static ArgumentException InvalidSizeValue(int value)
		{
			return ADP.Argument(SR.GetString("Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.", new object[] { value.ToString(CultureInfo.InvariantCulture) }));
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x000A636C File Offset: 0x000A456C
		internal static ArgumentException ParameterValueOutOfRange(decimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[] { value.ToString(null) }));
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x000A638E File Offset: 0x000A458E
		internal static ArgumentException ParameterValueOutOfRange(SqlDecimal value)
		{
			return ADP.Argument(SR.GetString("Parameter value '{0}' is out of range.", new object[] { value.ToString() }));
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x000A63B5 File Offset: 0x000A45B5
		internal static ArgumentException VersionDoesNotSupportDataType(string typeName)
		{
			return ADP.Argument(SR.GetString("The version of SQL Server in use does not support datatype '{0}'.", new object[] { typeName }));
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x000A63D0 File Offset: 0x000A45D0
		internal static Exception ParameterConversionFailed(object value, Type destType, Exception inner)
		{
			string @string = SR.GetString("Failed to convert parameter value from a {0} to a {1}.", new object[]
			{
				value.GetType().Name,
				destType.Name
			});
			Exception ex;
			if (inner is ArgumentException)
			{
				ex = new ArgumentException(@string, inner);
			}
			else if (inner is FormatException)
			{
				ex = new FormatException(@string, inner);
			}
			else if (inner is InvalidCastException)
			{
				ex = new InvalidCastException(@string, inner);
			}
			else if (inner is OverflowException)
			{
				ex = new OverflowException(@string, inner);
			}
			else
			{
				ex = inner;
			}
			return ex;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x000A6450 File Offset: 0x000A4650
		internal static Exception ParametersMappingIndex(int index, DbParameterCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x000A6464 File Offset: 0x000A4664
		internal static Exception ParametersSourceIndex(string parameterName, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionIndexString(parameterType, "ParameterName", parameterName, collection.GetType());
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x000A6478 File Offset: 0x000A4678
		internal static Exception ParameterNull(string parameter, DbParameterCollection collection, Type parameterType)
		{
			return ADP.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00058EFE File Offset: 0x000570FE
		internal static Exception UndefinedPopulationMechanism(string populationMechanism)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000A6487 File Offset: 0x000A4687
		internal static Exception InvalidParameterType(DbParameterCollection collection, Type parameterType, object invalidValue)
		{
			return ADP.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x000A6496 File Offset: 0x000A4696
		internal static Exception ParallelTransactionsNotSupported(DbConnection obj)
		{
			return ADP.InvalidOperation(SR.GetString("{0} does not support parallel transactions.", new object[] { obj.GetType().Name }));
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x000A64BB File Offset: 0x000A46BB
		internal static Exception TransactionZombied(DbTransaction obj)
		{
			return ADP.InvalidOperation(SR.GetString("This {0} has completed; it is no longer usable.", new object[] { obj.GetType().Name }));
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x000A64E0 File Offset: 0x000A46E0
		internal static Delegate FindBuilder(MulticastDelegate mcd)
		{
			if (mcd != null)
			{
				foreach (Delegate @delegate in mcd.GetInvocationList())
				{
					if (@delegate.Target is DbCommandBuilder)
					{
						return @delegate;
					}
				}
			}
			return null;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x000A651C File Offset: 0x000A471C
		internal static void TimerCurrent(out long ticks)
		{
			ticks = DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x000A6538 File Offset: 0x000A4738
		internal static long TimerCurrent()
		{
			return DateTime.UtcNow.ToFileTimeUtc();
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000A6552 File Offset: 0x000A4752
		internal static long TimerFromSeconds(int seconds)
		{
			checked
			{
				return unchecked((long)seconds) * 10000000L;
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000A655D File Offset: 0x000A475D
		internal static long TimerFromMilliseconds(long milliseconds)
		{
			return checked(milliseconds * 10000L);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000A6567 File Offset: 0x000A4767
		internal static bool TimerHasExpired(long timerExpire)
		{
			return ADP.TimerCurrent() > timerExpire;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000A6574 File Offset: 0x000A4774
		internal static long TimerRemaining(long timerExpire)
		{
			long num = ADP.TimerCurrent();
			return checked(timerExpire - num);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x000A658A File Offset: 0x000A478A
		internal static long TimerRemainingMilliseconds(long timerExpire)
		{
			return ADP.TimerToMilliseconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x000A6597 File Offset: 0x000A4797
		internal static long TimerRemainingSeconds(long timerExpire)
		{
			return ADP.TimerToSeconds(ADP.TimerRemaining(timerExpire));
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x000A65A4 File Offset: 0x000A47A4
		internal static long TimerToMilliseconds(long timerValue)
		{
			return timerValue / 10000L;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x000A65AE File Offset: 0x000A47AE
		private static long TimerToSeconds(long timerValue)
		{
			return timerValue / 10000000L;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000A65B8 File Offset: 0x000A47B8
		internal static string MachineName()
		{
			return Environment.MachineName;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000A65BF File Offset: 0x000A47BF
		internal static Transaction GetCurrentTransaction()
		{
			return Transaction.Current;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000A65C6 File Offset: 0x000A47C6
		internal static bool IsDirection(DbParameter value, ParameterDirection condition)
		{
			return condition == (condition & value.Direction);
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000A65D4 File Offset: 0x000A47D4
		internal static void IsNullOrSqlType(object value, out bool isNull, out bool isSqlType)
		{
			if (value == null || value == DBNull.Value)
			{
				isNull = true;
				isSqlType = false;
				return;
			}
			INullable nullable = value as INullable;
			if (nullable != null)
			{
				isNull = nullable.IsNull;
				isSqlType = value is SqlBinary || value is SqlBoolean || value is SqlByte || value is SqlBytes || value is SqlChars || value is SqlDateTime || value is SqlDecimal || value is SqlDouble || value is SqlGuid || value is SqlInt16 || value is SqlInt32 || value is SqlInt64 || value is SqlMoney || value is SqlSingle || value is SqlString;
				return;
			}
			isNull = false;
			isSqlType = false;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000A668D File Offset: 0x000A488D
		internal static Version GetAssemblyVersion()
		{
			if (ADP.s_systemDataVersion == null)
			{
				ADP.s_systemDataVersion = new Version("4.6.57.0");
			}
			return ADP.s_systemDataVersion;
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000A66B0 File Offset: 0x000A48B0
		internal static bool IsAzureSqlServerEndpoint(string dataSource)
		{
			int i = dataSource.LastIndexOf(',');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			i = dataSource.LastIndexOf('\\');
			if (i >= 0)
			{
				dataSource = dataSource.Substring(0, i);
			}
			dataSource = dataSource.Trim();
			for (i = 0; i < ADP.AzureSqlServerEndpoints.Length; i++)
			{
				if (dataSource.EndsWith(ADP.AzureSqlServerEndpoints[i], StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000A6718 File Offset: 0x000A4918
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			return ADP.InvalidEnumerationValue(typeof(DataRowVersion), (int)value);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000A672A File Offset: 0x000A492A
		internal static ArgumentException SingleValuedProperty(string propertyName, string value)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The only acceptable value for the property '{0}' is '{1}'.", new object[] { propertyName, value }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000A674F File Offset: 0x000A494F
		internal static ArgumentException DoubleValuedProperty(string propertyName, string value1, string value2)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("The acceptable values for the property '{0}' are '{1}' or '{2}'.", new object[] { propertyName, value1, value2 }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000A6778 File Offset: 0x000A4978
		internal static ArgumentException InvalidPrefixSuffix()
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified QuotePrefix and QuoteSuffix values do not match."));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000A678F File Offset: 0x000A498F
		internal static ArgumentOutOfRangeException InvalidCommandBehavior(CommandBehavior value)
		{
			return ADP.InvalidEnumerationValue(typeof(CommandBehavior), (int)value);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000A67A1 File Offset: 0x000A49A1
		internal static void ValidateCommandBehavior(CommandBehavior value)
		{
			if (value < CommandBehavior.Default || (CommandBehavior.SingleResult | CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo | CommandBehavior.SingleRow | CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection) < value)
			{
				throw ADP.InvalidCommandBehavior(value);
			}
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000A67B3 File Offset: 0x000A49B3
		internal static ArgumentOutOfRangeException NotSupportedCommandBehavior(CommandBehavior value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(CommandBehavior), value.ToString(), method);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000A67D2 File Offset: 0x000A49D2
		internal static ArgumentException BadParameterName(string parameterName)
		{
			ArgumentException ex = new ArgumentException(SR.GetString("Specified parameter name '{0}' is not valid.", new object[] { parameterName }));
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000A67F4 File Offset: 0x000A49F4
		internal static Exception DeriveParametersNotSupported(IDbCommand value)
		{
			return ADP.DataAdapter(SR.GetString("{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType. {1}.", new object[]
			{
				value.GetType().Name,
				value.CommandType.ToString()
			}));
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000A683B File Offset: 0x000A4A3B
		internal static Exception NoStoredProcedureExists(string sproc)
		{
			return ADP.InvalidOperation(SR.GetString("The stored procedure '{0}' doesn't exist.", new object[] { sproc }));
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000A6856 File Offset: 0x000A4A56
		internal static InvalidOperationException TransactionCompletedButNotDisposed()
		{
			return ADP.Provider(SR.GetString("The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements."));
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000A6867 File Offset: 0x000A4A67
		internal static ArgumentOutOfRangeException InvalidUserDefinedTypeSerializationFormat(Format value)
		{
			return ADP.InvalidEnumerationValue(typeof(Format), (int)value);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000A6879 File Offset: 0x000A4A79
		internal static ArgumentOutOfRangeException NotSupportedUserDefinedTypeSerializationFormat(Format value, string method)
		{
			return ADP.NotSupportedEnumerationValue(typeof(Format), value.ToString(), method);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000A6898 File Offset: 0x000A4A98
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName, object value)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, value, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000A68A8 File Offset: 0x000A4AA8
		internal static ArgumentException InvalidArgumentLength(string argumentName, int limit)
		{
			return ADP.Argument(SR.GetString("The length of argument '{0}' exceeds its limit of '{1}'.", new object[] { argumentName, limit }));
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000A68CC File Offset: 0x000A4ACC
		internal static ArgumentException MustBeReadOnly(string argumentName)
		{
			return ADP.Argument(SR.GetString("{0} must be marked as read only.", new object[] { argumentName }));
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000A68E7 File Offset: 0x000A4AE7
		internal static InvalidOperationException InvalidMixedUsageOfSecureAndClearCredential()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot use Credential with UserID, UID, Password, or PWD connection string keywords."));
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000A68F8 File Offset: 0x000A4AF8
		internal static ArgumentException InvalidMixedArgumentOfSecureAndClearCredential()
		{
			return ADP.Argument(SR.GetString("Cannot use Credential with UserID, UID, Password, or PWD connection string keywords."));
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000A6909 File Offset: 0x000A4B09
		internal static InvalidOperationException InvalidMixedUsageOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot use Credential with Integrated Security connection string keyword."));
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000A691A File Offset: 0x000A4B1A
		internal static ArgumentException InvalidMixedArgumentOfSecureCredentialAndIntegratedSecurity()
		{
			return ADP.Argument(SR.GetString("Cannot use Credential with Integrated Security connection string keyword."));
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000A692B File Offset: 0x000A4B2B
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndIntegratedSecurity()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the AccessToken property if the 'Integrated Security' connection string keyword has been set to 'true' or 'SSPI'."));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000A693C File Offset: 0x000A4B3C
		internal static InvalidOperationException InvalidMixedUsageOfAccessTokenAndUserIDPassword()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the AccessToken property if 'UserID', 'UID', 'Password', or 'PWD' has been specified in connection string."));
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000A694D File Offset: 0x000A4B4D
		internal static Exception InvalidMixedUsageOfCredentialAndAccessToken()
		{
			return ADP.InvalidOperation(SR.GetString("Cannot set the Credential property if the AccessToken property is already set."));
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x00005AE9 File Offset: 0x00003CE9
		internal static bool NeedManualEnlistment()
		{
			return false;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000A695E File Offset: 0x000A4B5E
		internal static bool IsEmpty(string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000A6966 File Offset: 0x000A4B66
		internal static Exception DatabaseNameTooLong()
		{
			return ADP.Argument(SR.GetString("The argument is too long."));
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0008209E File Offset: 0x0008029E
		internal static int StringLength(string inputString)
		{
			if (inputString == null)
			{
				return 0;
			}
			return inputString.Length;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000A6977 File Offset: 0x000A4B77
		internal static Exception NumericToDecimalOverflow()
		{
			return ADP.InvalidCast(SR.GetString("The numerical value is too large to fit into a 96 bit decimal."));
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000A6988 File Offset: 0x000A4B88
		internal static Exception OdbcNoTypesFromProvider()
		{
			return ADP.InvalidOperation(SR.GetString("The ODBC provider did not return results from SQLGETTYPEINFO."));
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000A6999 File Offset: 0x000A4B99
		internal static ArgumentException InvalidRestrictionValue(string collectionName, string restrictionName, string restrictionValue)
		{
			return ADP.Argument(SR.GetString("'{2}' is not a valid value for the '{1}' restriction of the '{0}' schema collection.", new object[] { collectionName, restrictionName, restrictionValue }));
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000A69BC File Offset: 0x000A4BBC
		internal static Exception DataReaderNoData()
		{
			return ADP.InvalidOperation(SR.GetString("No data exists for the row/column."));
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000A69CD File Offset: 0x000A4BCD
		internal static Exception ConnectionIsDisabled(Exception InnerException)
		{
			return ADP.InvalidOperation(SR.GetString("The connection has been disabled."), InnerException);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000A69DF File Offset: 0x000A4BDF
		internal static Exception OffsetOutOfRangeException()
		{
			return ADP.InvalidOperation(SR.GetString("Offset must refer to a location within the value."));
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000A69F0 File Offset: 0x000A4BF0
		internal static InvalidOperationException QuotePrefixNotSet(string method)
		{
			return ADP.InvalidOperation(Res.GetString("{0} requires open connection when the quote prefix has not been set.", new object[] { method }));
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000A6A0B File Offset: 0x000A4C0B
		internal static string GetFullPath(string filename)
		{
			return Path.GetFullPath(filename);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000A6A13 File Offset: 0x000A4C13
		internal static InvalidOperationException InvalidDataDirectory()
		{
			return ADP.InvalidOperation(SR.GetString("The DataDirectory substitute is not a string."));
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000A6A24 File Offset: 0x000A4C24
		internal static void EscapeSpecialCharacters(string unescapedString, StringBuilder escapedString)
		{
			foreach (char c in unescapedString)
			{
				if (".$^{[(|)*+?\\]".IndexOf(c) >= 0)
				{
					escapedString.Append("\\");
				}
				escapedString.Append(c);
			}
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000A6A6E File Offset: 0x000A4C6E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr IntPtrOffset(IntPtr pbase, int offset)
		{
			checked
			{
				if (4 == ADP.PtrSize)
				{
					return (IntPtr)(pbase.ToInt32() + offset);
				}
				return (IntPtr)(pbase.ToInt64() + unchecked((long)offset));
			}
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x000A6A96 File Offset: 0x000A4C96
		internal static Exception InvalidXMLBadVersion()
		{
			return ADP.Argument(Res.GetString("Invalid Xml; can only parse elements of version one."));
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x000A6AA7 File Offset: 0x000A4CA7
		internal static Exception NotAPermissionElement()
		{
			return ADP.Argument(Res.GetString("Given security element is not a permission element."));
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x000A6AB8 File Offset: 0x000A4CB8
		internal static Exception PermissionTypeMismatch()
		{
			return ADP.Argument(Res.GetString("Type mismatch."));
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000A6AC9 File Offset: 0x000A4CC9
		internal static ArgumentOutOfRangeException InvalidPermissionState(PermissionState value)
		{
			return ADP.InvalidEnumerationValue(typeof(PermissionState), (int)value);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000A6ADB File Offset: 0x000A4CDB
		internal static ConfigurationException Configuration(string message)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000A6AE9 File Offset: 0x000A4CE9
		internal static ConfigurationException Configuration(string message, XmlNode node)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message, node);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000A6AF8 File Offset: 0x000A4CF8
		internal static ArgumentException ConfigProviderNotFound()
		{
			return ADP.Argument(Res.GetString("Unable to find the requested .Net Framework Data Provider.  It may not be installed."));
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000A6B09 File Offset: 0x000A4D09
		internal static InvalidOperationException ConfigProviderInvalid()
		{
			return ADP.InvalidOperation(Res.GetString("The requested .Net Framework Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type."));
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000A6B1A File Offset: 0x000A4D1A
		internal static ConfigurationException ConfigProviderNotInstalled()
		{
			return ADP.Configuration(Res.GetString("Failed to find or load the registered .Net Framework Data Provider."));
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x000A6B2B File Offset: 0x000A4D2B
		internal static ConfigurationException ConfigProviderMissing()
		{
			return ADP.Configuration(Res.GetString("The missing .Net Framework Data Provider's assembly qualified name is required."));
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		internal static ConfigurationException ConfigBaseNoChildNodes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Child nodes not allowed."), node);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000A6B4E File Offset: 0x000A4D4E
		internal static ConfigurationException ConfigBaseElementsOnly(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Only elements allowed."), node);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000A6B60 File Offset: 0x000A4D60
		internal static ConfigurationException ConfigUnrecognizedAttributes(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized attribute '{0}'.", new object[] { node.Attributes[0].Name }), node);
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x000A6B8C File Offset: 0x000A4D8C
		internal static ConfigurationException ConfigUnrecognizedElement(XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Unrecognized element."), node);
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x000A6B9E File Offset: 0x000A4D9E
		internal static ConfigurationException ConfigSectionsUnique(string sectionName)
		{
			return ADP.Configuration(Res.GetString("The '{0}' section can only appear once per config file.", new object[] { sectionName }));
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000A6BB9 File Offset: 0x000A4DB9
		internal static ConfigurationException ConfigRequiredAttributeMissing(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' not found.", new object[] { name }), node);
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x000A6BD5 File Offset: 0x000A4DD5
		internal static ConfigurationException ConfigRequiredAttributeEmpty(string name, XmlNode node)
		{
			return ADP.Configuration(Res.GetString("Required attribute '{0}' cannot be empty.", new object[] { name }), node);
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x000A6BF1 File Offset: 0x000A4DF1
		internal static Exception OleDb()
		{
			return new NotImplementedException("OleDb is not implemented.");
		}

		// Token: 0x040017C1 RID: 6081
		private static Task<bool> _trueTask;

		// Token: 0x040017C2 RID: 6082
		private static Task<bool> _falseTask;

		// Token: 0x040017C3 RID: 6083
		internal const CompareOptions DefaultCompareOptions = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040017C4 RID: 6084
		internal const int DefaultConnectionTimeout = 15;

		// Token: 0x040017C5 RID: 6085
		private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

		// Token: 0x040017C6 RID: 6086
		private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040017C7 RID: 6087
		private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

		// Token: 0x040017C8 RID: 6088
		private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

		// Token: 0x040017C9 RID: 6089
		private static readonly Type s_accessViolationType = typeof(AccessViolationException);

		// Token: 0x040017CA RID: 6090
		private static readonly Type s_securityType = typeof(SecurityException);

		// Token: 0x040017CB RID: 6091
		internal const string ConnectionString = "ConnectionString";

		// Token: 0x040017CC RID: 6092
		internal const string DataSetColumn = "DataSetColumn";

		// Token: 0x040017CD RID: 6093
		internal const string DataSetTable = "DataSetTable";

		// Token: 0x040017CE RID: 6094
		internal const string Fill = "Fill";

		// Token: 0x040017CF RID: 6095
		internal const string FillSchema = "FillSchema";

		// Token: 0x040017D0 RID: 6096
		internal const string SourceColumn = "SourceColumn";

		// Token: 0x040017D1 RID: 6097
		internal const string SourceTable = "SourceTable";

		// Token: 0x040017D2 RID: 6098
		internal const string Parameter = "Parameter";

		// Token: 0x040017D3 RID: 6099
		internal const string ParameterName = "ParameterName";

		// Token: 0x040017D4 RID: 6100
		internal const string ParameterSetPosition = "set_Position";

		// Token: 0x040017D5 RID: 6101
		internal const int DefaultCommandTimeout = 30;

		// Token: 0x040017D6 RID: 6102
		internal const float FailoverTimeoutStep = 0.08f;

		// Token: 0x040017D7 RID: 6103
		internal static readonly string StrEmpty = "";

		// Token: 0x040017D8 RID: 6104
		internal const int CharSize = 2;

		// Token: 0x040017D9 RID: 6105
		private static Version s_systemDataVersion;

		// Token: 0x040017DA RID: 6106
		internal static readonly string[] AzureSqlServerEndpoints = new string[]
		{
			SR.GetString(".database.windows.net"),
			SR.GetString(".database.cloudapi.de"),
			SR.GetString(".database.usgovcloudapi.net"),
			SR.GetString(".database.chinacloudapi.cn")
		};

		// Token: 0x040017DB RID: 6107
		internal const int DecimalMaxPrecision = 29;

		// Token: 0x040017DC RID: 6108
		internal const int DecimalMaxPrecision28 = 28;

		// Token: 0x040017DD RID: 6109
		internal static readonly IntPtr PtrZero = new IntPtr(0);

		// Token: 0x040017DE RID: 6110
		internal static readonly int PtrSize = IntPtr.Size;

		// Token: 0x040017DF RID: 6111
		internal const string BeginTransaction = "BeginTransaction";

		// Token: 0x040017E0 RID: 6112
		internal const string ChangeDatabase = "ChangeDatabase";

		// Token: 0x040017E1 RID: 6113
		internal const string CommitTransaction = "CommitTransaction";

		// Token: 0x040017E2 RID: 6114
		internal const string CommandTimeout = "CommandTimeout";

		// Token: 0x040017E3 RID: 6115
		internal const string DeriveParameters = "DeriveParameters";

		// Token: 0x040017E4 RID: 6116
		internal const string ExecuteReader = "ExecuteReader";

		// Token: 0x040017E5 RID: 6117
		internal const string ExecuteNonQuery = "ExecuteNonQuery";

		// Token: 0x040017E6 RID: 6118
		internal const string ExecuteScalar = "ExecuteScalar";

		// Token: 0x040017E7 RID: 6119
		internal const string GetSchema = "GetSchema";

		// Token: 0x040017E8 RID: 6120
		internal const string GetSchemaTable = "GetSchemaTable";

		// Token: 0x040017E9 RID: 6121
		internal const string Prepare = "Prepare";

		// Token: 0x040017EA RID: 6122
		internal const string RollbackTransaction = "RollbackTransaction";

		// Token: 0x040017EB RID: 6123
		internal const string QuoteIdentifier = "QuoteIdentifier";

		// Token: 0x040017EC RID: 6124
		internal const string UnquoteIdentifier = "UnquoteIdentifier";

		// Token: 0x02000314 RID: 788
		internal enum InternalErrorCode
		{
			// Token: 0x040017EE RID: 6126
			UnpooledObjectHasOwner,
			// Token: 0x040017EF RID: 6127
			UnpooledObjectHasWrongOwner,
			// Token: 0x040017F0 RID: 6128
			PushingObjectSecondTime,
			// Token: 0x040017F1 RID: 6129
			PooledObjectHasOwner,
			// Token: 0x040017F2 RID: 6130
			PooledObjectInPoolMoreThanOnce,
			// Token: 0x040017F3 RID: 6131
			CreateObjectReturnedNull,
			// Token: 0x040017F4 RID: 6132
			NewObjectCannotBePooled,
			// Token: 0x040017F5 RID: 6133
			NonPooledObjectUsedMoreThanOnce,
			// Token: 0x040017F6 RID: 6134
			AttemptingToPoolOnRestrictedToken,
			// Token: 0x040017F7 RID: 6135
			ConvertSidToStringSidWReturnedNull = 10,
			// Token: 0x040017F8 RID: 6136
			AttemptingToConstructReferenceCollectionOnStaticObject = 12,
			// Token: 0x040017F9 RID: 6137
			AttemptingToEnlistTwice,
			// Token: 0x040017FA RID: 6138
			CreateReferenceCollectionReturnedNull,
			// Token: 0x040017FB RID: 6139
			PooledObjectWithoutPool,
			// Token: 0x040017FC RID: 6140
			UnexpectedWaitAnyResult,
			// Token: 0x040017FD RID: 6141
			SynchronousConnectReturnedPending,
			// Token: 0x040017FE RID: 6142
			CompletedConnectReturnedPending,
			// Token: 0x040017FF RID: 6143
			NameValuePairNext = 20,
			// Token: 0x04001800 RID: 6144
			InvalidParserState1,
			// Token: 0x04001801 RID: 6145
			InvalidParserState2,
			// Token: 0x04001802 RID: 6146
			InvalidParserState3,
			// Token: 0x04001803 RID: 6147
			InvalidBuffer = 30,
			// Token: 0x04001804 RID: 6148
			UnimplementedSMIMethod = 40,
			// Token: 0x04001805 RID: 6149
			InvalidSmiCall,
			// Token: 0x04001806 RID: 6150
			SqlDependencyObtainProcessDispatcherFailureObjectHandle = 50,
			// Token: 0x04001807 RID: 6151
			SqlDependencyProcessDispatcherFailureCreateInstance,
			// Token: 0x04001808 RID: 6152
			SqlDependencyProcessDispatcherFailureAppDomain,
			// Token: 0x04001809 RID: 6153
			SqlDependencyCommandHashIsNotAssociatedWithNotification,
			// Token: 0x0400180A RID: 6154
			UnknownTransactionFailure = 60
		}

		// Token: 0x02000315 RID: 789
		internal enum ConnectionError
		{
			// Token: 0x0400180C RID: 6156
			BeginGetConnectionReturnsNull,
			// Token: 0x0400180D RID: 6157
			GetConnectionReturnsNull,
			// Token: 0x0400180E RID: 6158
			ConnectionOptionsMissing,
			// Token: 0x0400180F RID: 6159
			CouldNotSwitchToClosedPreviouslyOpenedState
		}
	}
}
