using System;

namespace System.Data.OleDb
{
	/// <summary>Returns the type of schema table specified by the <see cref="M:System.Data.OleDb.OleDbConnection.GetOleDbSchemaTable(System.Guid,System.Object[])" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000122 RID: 290
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbSchemaGuid
	{
		/// <summary>Returns the assertions defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F1 RID: 2545
		public static readonly Guid Assertions;

		/// <summary>Returns the physical attributes associated with catalogs accessible from the data source. Returns the assertions defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F2 RID: 2546
		public static readonly Guid Catalogs;

		/// <summary>Returns the character sets defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F3 RID: 2547
		public static readonly Guid Character_Sets;

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F4 RID: 2548
		public static readonly Guid Check_Constraints;

		/// <summary>Returns the check constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F5 RID: 2549
		public static readonly Guid Check_Constraints_By_Table;

		/// <summary>Returns the character collations defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F6 RID: 2550
		public static readonly Guid Collations;

		/// <summary>Returns the columns defined in the catalog that are dependent on a domain defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F7 RID: 2551
		public static readonly Guid Column_Domain_Usage;

		/// <summary>Returns the privileges on columns of tables defined in the catalog that are available to or granted by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F8 RID: 2552
		public static readonly Guid Column_Privileges;

		/// <summary>Returns the columns of tables (including views) defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009F9 RID: 2553
		public static readonly Guid Columns;

		/// <summary>Returns the columns used by referential constraints, unique constraints, check constraints, and assertions, defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FA RID: 2554
		public static readonly Guid Constraint_Column_Usage;

		/// <summary>Returns the tables that are used by referential constraints, unique constraints, check constraints, and assertions defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FB RID: 2555
		public static readonly Guid Constraint_Table_Usage;

		/// <summary>Returns a list of provider-specific keywords.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FC RID: 2556
		public static readonly Guid DbInfoKeywords;

		/// <summary>Returns a list of provider-specific literals used in text commands.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FD RID: 2557
		public static readonly Guid DbInfoLiterals;

		/// <summary>Returns the foreign key columns defined in the catalog by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FE RID: 2558
		public static readonly Guid Foreign_Keys;

		/// <summary>Returns the indexes defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x040009FF RID: 2559
		public static readonly Guid Indexes;

		/// <summary>Returns the columns defined in the catalog that is constrained as keys by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A00 RID: 2560
		public static readonly Guid Key_Column_Usage;

		/// <summary>Returns the primary key columns defined in the catalog by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A01 RID: 2561
		public static readonly Guid Primary_Keys;

		/// <summary>Returns information about the columns of rowsets returned by procedures.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A02 RID: 2562
		public static readonly Guid Procedure_Columns;

		/// <summary>Returns information about the parameters and return codes of procedures.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A03 RID: 2563
		public static readonly Guid Procedure_Parameters;

		/// <summary>Returns the procedures defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A04 RID: 2564
		public static readonly Guid Procedures;

		/// <summary>Returns the base data types supported by the .NET Framework Data Provider for OLE DB.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A05 RID: 2565
		public static readonly Guid Provider_Types;

		/// <summary>Returns the referential constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A06 RID: 2566
		public static readonly Guid Referential_Constraints;

		/// <summary>Returns a list of schema rowsets, identified by their GUIDs, and a pointer to the descriptions of the restriction columns.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A07 RID: 2567
		public static readonly Guid SchemaGuids;

		/// <summary>Returns the schema objects that are owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A08 RID: 2568
		public static readonly Guid Schemata;

		/// <summary>Returns the conformance levels, options, and dialects supported by the SQL-implementation processing data defined in the catalog.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A09 RID: 2569
		public static readonly Guid Sql_Languages;

		/// <summary>Returns the statistics defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0A RID: 2570
		public static readonly Guid Statistics;

		/// <summary>Returns the table constraints defined in the catalog that is owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0B RID: 2571
		public static readonly Guid Table_Constraints;

		/// <summary>Returns the privileges on tables defined in the catalog that are available to, or granted by, a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0C RID: 2572
		public static readonly Guid Table_Privileges;

		/// <summary>Describes the available set of statistics on tables in the provider.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0D RID: 2573
		public static readonly Guid Table_Statistics;

		/// <summary>Returns the tables (including views) defined in the catalog that are accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0E RID: 2574
		public static readonly Guid Tables;

		/// <summary>Returns the tables (including views) that are accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A0F RID: 2575
		public static readonly Guid Tables_Info;

		/// <summary>Returns the character translations defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A10 RID: 2576
		public static readonly Guid Translations;

		/// <summary>Identifies the trustees defined in the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A11 RID: 2577
		public static readonly Guid Trustee;

		/// <summary>Returns the USAGE privileges on objects defined in the catalog that are available to or granted by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A12 RID: 2578
		public static readonly Guid Usage_Privileges;

		/// <summary>Returns the columns on which viewed tables depend, as defined in the catalog and owned by a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A13 RID: 2579
		public static readonly Guid View_Column_Usage;

		/// <summary>Returns the tables on which viewed tables, defined in the catalog and owned by a given user, are dependent.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A14 RID: 2580
		public static readonly Guid View_Table_Usage;

		/// <summary>Returns the views defined in the catalog that is accessible to a given user.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A15 RID: 2581
		public static readonly Guid Views;
	}
}
