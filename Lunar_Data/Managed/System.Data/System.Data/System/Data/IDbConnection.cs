using System;

namespace System.Data
{
	/// <summary>Represents an open connection to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B3 RID: 179
	public interface IDbConnection : IDisposable
	{
		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>A string containing connection settings.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B5D RID: 2909
		// (set) Token: 0x06000B5E RID: 2910
		string ConnectionString { get; set; }

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B5F RID: 2911
		int ConnectionTimeout { get; }

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used once a connection is open. The default value is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B60 RID: 2912
		string Database { get; }

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>One of the <see cref="T:System.Data.ConnectionState" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B61 RID: 2913
		ConnectionState State { get; }

		/// <summary>Begins a database transaction.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B62 RID: 2914
		IDbTransaction BeginTransaction();

		/// <summary>Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="il">One of the <see cref="T:System.Data.IsolationLevel" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B63 RID: 2915
		IDbTransaction BeginTransaction(IsolationLevel il);

		/// <summary>Closes the connection to the database.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B64 RID: 2916
		void Close();

		/// <summary>Changes the current database for an open Connection object.</summary>
		/// <param name="databaseName">The name of the database to use in place of the current database. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B65 RID: 2917
		void ChangeDatabase(string databaseName);

		/// <summary>Creates and returns a Command object associated with the connection.</summary>
		/// <returns>A Command object associated with the connection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B66 RID: 2918
		IDbCommand CreateCommand();

		/// <summary>Opens a database connection with the settings specified by the ConnectionString property of the provider-specific Connection object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B67 RID: 2919
		void Open();
	}
}
