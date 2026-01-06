using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Annotates the returned result of a user-defined type (UDT) with additional information that can be used in Transact-SQL.</summary>
	// Token: 0x020003C1 RID: 961
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public class SqlFacetAttribute : Attribute
	{
		/// <summary>Indicates whether the return type of the user-defined type is of a fixed length.</summary>
		/// <returns>true if the return type is of a fixed length; otherwise false.</returns>
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000CAE9E File Offset: 0x000C909E
		// (set) Token: 0x06002EED RID: 12013 RVA: 0x000CAEA6 File Offset: 0x000C90A6
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

		/// <summary>The maximum size, in logical units, of the underlying field type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the maximum size, in logical units, of the underlying field type.</returns>
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000CAEAF File Offset: 0x000C90AF
		// (set) Token: 0x06002EEF RID: 12015 RVA: 0x000CAEB7 File Offset: 0x000C90B7
		public int MaxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = value;
			}
		}

		/// <summary>The precision of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the precision of the return type.</returns>
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000CAEC0 File Offset: 0x000C90C0
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000CAEC8 File Offset: 0x000C90C8
		public int Precision
		{
			get
			{
				return this.m_Precision;
			}
			set
			{
				this.m_Precision = value;
			}
		}

		/// <summary>The scale of the return type of the user-defined type.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the scale of the return type.</returns>
		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000CAED1 File Offset: 0x000C90D1
		// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x000CAED9 File Offset: 0x000C90D9
		public int Scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		/// <summary>Indicates whether the return type of the user-defined type can be null.</summary>
		/// <returns>true if the return type of the user-defined type can be null; otherwise false.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000CAEE2 File Offset: 0x000C90E2
		// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x000CAEEA File Offset: 0x000C90EA
		public bool IsNullable
		{
			get
			{
				return this.m_IsNullable;
			}
			set
			{
				this.m_IsNullable = value;
			}
		}

		// Token: 0x04001BB5 RID: 7093
		private bool m_IsFixedLength;

		// Token: 0x04001BB6 RID: 7094
		private int m_MaxSize;

		// Token: 0x04001BB7 RID: 7095
		private int m_Scale;

		// Token: 0x04001BB8 RID: 7096
		private int m_Precision;

		// Token: 0x04001BB9 RID: 7097
		private bool m_IsNullable;
	}
}
