using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates the determinism and data access properties of a method or property on a user-defined type (UDT). The properties on the attribute reflect the physical characteristics that are used when the type is registered with SQL Server.</summary>
	// Token: 0x020003C5 RID: 965
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		/// <summary>An attribute on a user-defined type (UDT), used to indicate the determinism and data access properties of a method or a property on a UDT.</summary>
		// Token: 0x06002F06 RID: 12038 RVA: 0x000CAFA3 File Offset: 0x000C91A3
		public SqlMethodAttribute()
		{
			this.m_fCallOnNullInputs = true;
			this.m_fMutator = false;
			this.m_fInvokeIfReceiverIsNull = false;
		}

		/// <summary>Indicates whether the method on a user-defined type (UDT) is called when null input arguments are specified in the method invocation.</summary>
		/// <returns>true if the method is called when null input arguments are specified in the method invocation; false if the method returns a null value when any of its input parameters are null. If the method cannot be invoked (because of an attribute on the method), the SQL Server DbNull is returned.</returns>
		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000CAFC0 File Offset: 0x000C91C0
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000CAFC8 File Offset: 0x000C91C8
		public bool OnNullCall
		{
			get
			{
				return this.m_fCallOnNullInputs;
			}
			set
			{
				this.m_fCallOnNullInputs = value;
			}
		}

		/// <summary>Indicates whether a method on a user-defined type (UDT) is a mutator.</summary>
		/// <returns>true if the method is a mutator; otherwise false.</returns>
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002F09 RID: 12041 RVA: 0x000CAFD1 File Offset: 0x000C91D1
		// (set) Token: 0x06002F0A RID: 12042 RVA: 0x000CAFD9 File Offset: 0x000C91D9
		public bool IsMutator
		{
			get
			{
				return this.m_fMutator;
			}
			set
			{
				this.m_fMutator = value;
			}
		}

		/// <summary>Indicates whether SQL Server should invoke the method on null instances.</summary>
		/// <returns>true if SQL Server should invoke the method on null instances; otherwise false. If the method cannot be invoked (because of an attribute on the method), the SQL Server DbNull is returned.</returns>
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000CAFE2 File Offset: 0x000C91E2
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000CAFEA File Offset: 0x000C91EA
		public bool InvokeIfReceiverIsNull
		{
			get
			{
				return this.m_fInvokeIfReceiverIsNull;
			}
			set
			{
				this.m_fInvokeIfReceiverIsNull = value;
			}
		}

		// Token: 0x04001BC7 RID: 7111
		private bool m_fCallOnNullInputs;

		// Token: 0x04001BC8 RID: 7112
		private bool m_fMutator;

		// Token: 0x04001BC9 RID: 7113
		private bool m_fInvokeIfReceiverIsNull;
	}
}
