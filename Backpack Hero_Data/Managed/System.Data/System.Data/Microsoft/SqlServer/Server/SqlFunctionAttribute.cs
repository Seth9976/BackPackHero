using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Used to mark a method definition of a user-defined aggregate as a function in SQL Server. The properties on the attribute reflect the physical characteristics used when the type is registered with SQL Server.</summary>
	// Token: 0x020003C4 RID: 964
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public class SqlFunctionAttribute : Attribute
	{
		/// <summary>An optional attribute on a user-defined aggregate, used to indicate that the method should be registered in SQL Server as a function. Also used to set the <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.DataAccess" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.FillRowMethodName" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsDeterministic" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.IsPrecise" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.Name" />, <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.SystemDataAccess" />, and <see cref="P:Microsoft.SqlServer.Server.SqlFunctionAttribute.TableDefinition" /> properties of the function attribute.</summary>
		// Token: 0x06002EF7 RID: 12023 RVA: 0x000CAEF3 File Offset: 0x000C90F3
		public SqlFunctionAttribute()
		{
			this.m_fDeterministic = false;
			this.m_eDataAccess = DataAccessKind.None;
			this.m_eSystemDataAccess = SystemDataAccessKind.None;
			this.m_fPrecise = false;
			this.m_fName = null;
			this.m_fTableDefinition = null;
			this.m_FillRowMethodName = null;
		}

		/// <summary>Indicates whether the user-defined function is deterministic.</summary>
		/// <returns>true if the function is deterministic; otherwise false.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000CAF2C File Offset: 0x000C912C
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000CAF34 File Offset: 0x000C9134
		public bool IsDeterministic
		{
			get
			{
				return this.m_fDeterministic;
			}
			set
			{
				this.m_fDeterministic = value;
			}
		}

		/// <summary>Indicates whether the function involves access to user data stored in the local instance of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads user data.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000CAF3D File Offset: 0x000C913D
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000CAF45 File Offset: 0x000C9145
		public DataAccessKind DataAccess
		{
			get
			{
				return this.m_eDataAccess;
			}
			set
			{
				this.m_eDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function requires access to data stored in the system catalogs or virtual system tables of SQL Server.</summary>
		/// <returns>
		///   <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.None: Does not access system data. <see cref="T:Microsoft.SqlServer.Server.DataAccessKind" />.Read: Only reads system data.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000CAF4E File Offset: 0x000C914E
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000CAF56 File Offset: 0x000C9156
		public SystemDataAccessKind SystemDataAccess
		{
			get
			{
				return this.m_eSystemDataAccess;
			}
			set
			{
				this.m_eSystemDataAccess = value;
			}
		}

		/// <summary>Indicates whether the function involves imprecise computations, such as floating point operations.</summary>
		/// <returns>true if the function involves precise computations; otherwise false.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000CAF5F File Offset: 0x000C915F
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000CAF67 File Offset: 0x000C9167
		public bool IsPrecise
		{
			get
			{
				return this.m_fPrecise;
			}
			set
			{
				this.m_fPrecise = value;
			}
		}

		/// <summary>The name under which the function should be registered in SQL Server.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name under which the function should be registered.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000CAF70 File Offset: 0x000C9170
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000CAF78 File Offset: 0x000C9178
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

		/// <summary>A string that represents the table definition of the results, if the method is used as a table-valued function (TVF).</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the table definition of the results.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000CAF81 File Offset: 0x000C9181
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000CAF89 File Offset: 0x000C9189
		public string TableDefinition
		{
			get
			{
				return this.m_fTableDefinition;
			}
			set
			{
				this.m_fTableDefinition = value;
			}
		}

		/// <summary>The name of a method in the same class as the table-valued function (TVF) that is used by the TVF contract.</summary>
		/// <returns>A <see cref="T:System.String" /> value representing the name of a method used by the TVF contract.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000CAF92 File Offset: 0x000C9192
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000CAF9A File Offset: 0x000C919A
		public string FillRowMethodName
		{
			get
			{
				return this.m_FillRowMethodName;
			}
			set
			{
				this.m_FillRowMethodName = value;
			}
		}

		// Token: 0x04001BC0 RID: 7104
		private bool m_fDeterministic;

		// Token: 0x04001BC1 RID: 7105
		private DataAccessKind m_eDataAccess;

		// Token: 0x04001BC2 RID: 7106
		private SystemDataAccessKind m_eSystemDataAccess;

		// Token: 0x04001BC3 RID: 7107
		private bool m_fPrecise;

		// Token: 0x04001BC4 RID: 7108
		private string m_fName;

		// Token: 0x04001BC5 RID: 7109
		private string m_fTableDefinition;

		// Token: 0x04001BC6 RID: 7110
		private string m_FillRowMethodName;
	}
}
