using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x020001E1 RID: 481
	internal class SqlUdtInfo
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00070968 File Offset: 0x0006EB68
		private SqlUdtInfo(SqlUserDefinedTypeAttribute attr)
		{
			this.SerializationFormat = attr.Format;
			this.IsByteOrdered = attr.IsByteOrdered;
			this.IsFixedLength = attr.IsFixedLength;
			this.MaxByteSize = attr.MaxByteSize;
			this.Name = attr.Name;
			this.ValidationMethodName = attr.ValidationMethodName;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000709C3 File Offset: 0x0006EBC3
		internal static SqlUdtInfo GetFromType(Type target)
		{
			SqlUdtInfo sqlUdtInfo = SqlUdtInfo.TryGetFromType(target);
			if (sqlUdtInfo == null)
			{
				throw InvalidUdtException.Create(target, "no UDT attribute");
			}
			return sqlUdtInfo;
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000709DC File Offset: 0x0006EBDC
		internal static SqlUdtInfo TryGetFromType(Type target)
		{
			if (SqlUdtInfo.s_types2UdtInfo == null)
			{
				SqlUdtInfo.s_types2UdtInfo = new Dictionary<Type, SqlUdtInfo>();
			}
			SqlUdtInfo sqlUdtInfo = null;
			if (!SqlUdtInfo.s_types2UdtInfo.TryGetValue(target, out sqlUdtInfo))
			{
				object[] customAttributes = target.GetCustomAttributes(typeof(SqlUserDefinedTypeAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					sqlUdtInfo = new SqlUdtInfo((SqlUserDefinedTypeAttribute)customAttributes[0]);
				}
				SqlUdtInfo.s_types2UdtInfo.Add(target, sqlUdtInfo);
			}
			return sqlUdtInfo;
		}

		// Token: 0x04000F36 RID: 3894
		internal readonly Format SerializationFormat;

		// Token: 0x04000F37 RID: 3895
		internal readonly bool IsByteOrdered;

		// Token: 0x04000F38 RID: 3896
		internal readonly bool IsFixedLength;

		// Token: 0x04000F39 RID: 3897
		internal readonly int MaxByteSize;

		// Token: 0x04000F3A RID: 3898
		internal readonly string Name;

		// Token: 0x04000F3B RID: 3899
		internal readonly string ValidationMethodName;

		// Token: 0x04000F3C RID: 3900
		[ThreadStatic]
		private static Dictionary<Type, SqlUdtInfo> s_types2UdtInfo;
	}
}
