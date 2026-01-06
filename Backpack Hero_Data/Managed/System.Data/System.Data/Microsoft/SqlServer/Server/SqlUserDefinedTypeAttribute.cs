using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a type definition in an assembly as a user-defined type (UDT) in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003CA RID: 970
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class SqlUserDefinedTypeAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined type (UDT), used to confirm that the given type is a UDT and to indicate the storage format of the UDT.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the type.</param>
		// Token: 0x06002F25 RID: 12069 RVA: 0x000CB12C File Offset: 0x000C932C
		public SqlUserDefinedTypeAttribute(Format format)
		{
			if (format == Format.Unknown)
			{
				throw ADP.NotSupportedUserDefinedTypeSerializationFormat(format, "format");
			}
			if (format - Format.Native > 1)
			{
				throw ADP.InvalidUserDefinedTypeSerializationFormat(format);
			}
			this.m_format = format;
		}

		/// <summary>The maximum size of the instance, in bytes.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the instance.</returns>
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002F26 RID: 12070 RVA: 0x000CB159 File Offset: 0x000C9359
		// (set) Token: 0x06002F27 RID: 12071 RVA: 0x000CB161 File Offset: 0x000C9361
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1)
				{
					throw ADP.ArgumentOutOfRange("MaxByteSize");
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether all instances of this user-defined type are the same length.</summary>
		/// <returns>true if all instances of this type are the same length; otherwise false.</returns>
		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002F28 RID: 12072 RVA: 0x000CB179 File Offset: 0x000C9379
		// (set) Token: 0x06002F29 RID: 12073 RVA: 0x000CB181 File Offset: 0x000C9381
		public bool IsFixedLength
		{
			get
			{
				return this.m_IsFixedLength;
			}
			set
			{
				this.m_IsFixedLength = value;
			}
		}

		/// <summary>Indicates whether the user-defined type is byte ordered.</summary>
		/// <returns>true if the user-defined type is byte ordered; otherwise false.</returns>
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000CB18A File Offset: 0x000C938A
		// (set) Token: 0x06002F2B RID: 12075 RVA: 0x000CB192 File Offset: 0x000C9392
		public bool IsByteOrdered
		{
			get
			{
				return this.m_IsByteOrdered;
			}
			set
			{
				this.m_IsByteOrdered = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> value representing the serialization format.</returns>
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x000CB19B File Offset: 0x000C939B
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the method used to validate instances of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the method used to validate instances of the user-defined type.</returns>
		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000CB1A3 File Offset: 0x000C93A3
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000CB1AB File Offset: 0x000C93AB
		public string ValidationMethodName
		{
			get
			{
				return this.m_ValidationMethodName;
			}
			set
			{
				this.m_ValidationMethodName = value;
			}
		}

		/// <summary>The SQL Server name of the user-defined type.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the SQL Server name of the user-defined type.</returns>
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000CB1B4 File Offset: 0x000C93B4
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000CB1BC File Offset: 0x000C93BC
		public string Name
		{
			get
			{
				return this.m_fName;
			}
			set
			{
				this.m_fName = value;
			}
		}

		// Token: 0x04001BDA RID: 7130
		private int m_MaxByteSize;

		// Token: 0x04001BDB RID: 7131
		private bool m_IsFixedLength;

		// Token: 0x04001BDC RID: 7132
		private bool m_IsByteOrdered;

		// Token: 0x04001BDD RID: 7133
		private Format m_format;

		// Token: 0x04001BDE RID: 7134
		private string m_fName;

		// Token: 0x04001BDF RID: 7135
		internal const int YukonMaxByteSizeValue = 8000;

		// Token: 0x04001BE0 RID: 7136
		private string m_ValidationMethodName;
	}
}
