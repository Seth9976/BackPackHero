using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010D RID: 269
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class.</summary>
		// Token: 0x06000EC9 RID: 3785 RVA: 0x0004EFE7 File Offset: 0x0004D1E7
		public OleDbCommandBuilder()
		{
			throw ADP.OleDb();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class with the associated <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		// Token: 0x06000ECA RID: 3786 RVA: 0x0004EFE7 File Offset: 0x0004D1E7
		public OleDbCommandBuilder(OleDbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object for which SQL statements are automatically generated.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x000094D4 File Offset: 0x000076D4
		public new OleDbDataAdapter DataAdapter
		{
			get
			{
				throw ADP.OleDb();
			}
			set
			{
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			throw ADP.OleDb();
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.OleDb.OleDbCommand" /> and populates the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.OleDb.OleDbCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the <see cref="T:System.Data.OleDb.OleDbCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The underlying OLE DB provider does not support returning stored procedure parameter information, the command text is not a valid stored procedure name, or the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> specified was not StoredProcedure.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000ECE RID: 3790 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public static void DeriveParameters(OleDbCommand command)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000ECF RID: 3791 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetDeleteCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetInsertCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED2 RID: 3794 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override string GetParameterName(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override string GetParameterName(string parameterName)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000ED6 RID: 3798 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetUpdateCommand()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000ED8 RID: 3800 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The unquoted identifier to be returned in quoted format.</param>
		/// <param name="connection">When a connection is passed, causes the managed wrapper to get the quote character from the OLE DB provider. When no connection is passed, the string is quoted using values from <see cref="P:System.Data.Common.DbCommandBuilder.QuotePrefix" /> and <see cref="P:System.Data.Common.DbCommandBuilder.QuoteSuffix" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000ED9 RID: 3801 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EDB RID: 3803 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			throw ADP.OleDb();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.OleDb.OleDbConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000EDC RID: 3804 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			throw ADP.OleDb();
		}
	}
}
