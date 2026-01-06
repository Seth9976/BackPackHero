using System;
using System.Data.Common;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates that the type should be registered as a user-defined aggregate. The properties on the attribute reflect the physical attributes used when the type is registered with SQL Server. This class cannot be inherited.</summary>
	// Token: 0x020003C8 RID: 968
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public sealed class SqlUserDefinedAggregateAttribute : Attribute
	{
		/// <summary>A required attribute on a user-defined aggregate, used to indicate that the given type is a user-defined aggregate and the storage format of the user-defined aggregate.</summary>
		/// <param name="format">One of the <see cref="T:Microsoft.SqlServer.Server.Format" /> values representing the serialization format of the aggregate.</param>
		// Token: 0x06002F17 RID: 12055 RVA: 0x000CB063 File Offset: 0x000C9263
		public SqlUserDefinedAggregateAttribute(Format format)
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

		/// <summary>The maximum size, in bytes, of the aggregate instance.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the maximum size of the aggregate instance.</returns>
		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002F18 RID: 12056 RVA: 0x000CB097 File Offset: 0x000C9297
		// (set) Token: 0x06002F19 RID: 12057 RVA: 0x000CB09F File Offset: 0x000C929F
		public int MaxByteSize
		{
			get
			{
				return this.m_MaxByteSize;
			}
			set
			{
				if (value < -1 || value > 8000)
				{
					throw ADP.ArgumentOutOfRange(Res.GetString("range: 0-8000"), "MaxByteSize", value);
				}
				this.m_MaxByteSize = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to duplicates.</summary>
		/// <returns>true if the aggregate is invariant to duplicates; otherwise false.</returns>
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002F1A RID: 12058 RVA: 0x000CB0CF File Offset: 0x000C92CF
		// (set) Token: 0x06002F1B RID: 12059 RVA: 0x000CB0D7 File Offset: 0x000C92D7
		public bool IsInvariantToDuplicates
		{
			get
			{
				return this.m_fInvariantToDup;
			}
			set
			{
				this.m_fInvariantToDup = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to nulls.</summary>
		/// <returns>true if the aggregate is invariant to nulls; otherwise false.</returns>
		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000CB0E0 File Offset: 0x000C92E0
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x000CB0E8 File Offset: 0x000C92E8
		public bool IsInvariantToNulls
		{
			get
			{
				return this.m_fInvariantToNulls;
			}
			set
			{
				this.m_fInvariantToNulls = value;
			}
		}

		/// <summary>Indicates whether the aggregate is invariant to order.</summary>
		/// <returns>true if the aggregate is invariant to order; otherwise false.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000CB0F1 File Offset: 0x000C92F1
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x000CB0F9 File Offset: 0x000C92F9
		public bool IsInvariantToOrder
		{
			get
			{
				return this.m_fInvariantToOrder;
			}
			set
			{
				this.m_fInvariantToOrder = value;
			}
		}

		/// <summary>Indicates whether the aggregate returns null if no values have been accumulated.</summary>
		/// <returns>true if the aggregate returns null if no values have been accumulated; otherwise false.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000CB102 File Offset: 0x000C9302
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x000CB10A File Offset: 0x000C930A
		public bool IsNullIfEmpty
		{
			get
			{
				return this.m_fNullIfEmpty;
			}
			set
			{
				this.m_fNullIfEmpty = value;
			}
		}

		/// <summary>The serialization format as a <see cref="T:Microsoft.SqlServer.Server.Format" />.</summary>
		/// <returns>A <see cref="T:Microsoft.SqlServer.Server.Format" /> representing the serialization format.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x000CB113 File Offset: 0x000C9313
		public Format Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>The name of the aggregate.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of the aggregate.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000CB11B File Offset: 0x000C931B
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000CB123 File Offset: 0x000C9323
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

		// Token: 0x04001BCE RID: 7118
		private int m_MaxByteSize;

		// Token: 0x04001BCF RID: 7119
		private bool m_fInvariantToDup;

		// Token: 0x04001BD0 RID: 7120
		private bool m_fInvariantToNulls;

		// Token: 0x04001BD1 RID: 7121
		private bool m_fInvariantToOrder = true;

		// Token: 0x04001BD2 RID: 7122
		private bool m_fNullIfEmpty;

		// Token: 0x04001BD3 RID: 7123
		private Format m_format;

		// Token: 0x04001BD4 RID: 7124
		private string m_fName;

		/// <summary>The maximum size, in bytes, required to store the state of this aggregate instance during computation.</summary>
		// Token: 0x04001BD5 RID: 7125
		public const int MaxByteSizeValue = 8000;
	}
}
