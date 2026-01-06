using System;

namespace System.Data
{
	/// <summary>Represents an SQL statement that is executed while connected to a data source, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B2 RID: 178
	public interface IDbCommand : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.Data.IDbConnection" /> used by this instance of the <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <returns>The connection to the data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B49 RID: 2889
		// (set) Token: 0x06000B4A RID: 2890
		IDbConnection Connection { get; set; }

		/// <summary>Gets or sets the transaction within which the Command object of a .NET Framework data provider executes.</summary>
		/// <returns>the Command object of a .NET Framework data provider executes. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B4B RID: 2891
		// (set) Token: 0x06000B4C RID: 2892
		IDbTransaction Transaction { get; set; }

		/// <summary>Gets or sets the text command to run against the data source.</summary>
		/// <returns>The text command to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000B4D RID: 2893
		// (set) Token: 0x06000B4E RID: 2894
		string CommandText { get; set; }

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default value is 30 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The property value assigned is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B4F RID: 2895
		// (set) Token: 0x06000B50 RID: 2896
		int CommandTimeout { get; set; }

		/// <summary>Indicates or specifies how the <see cref="P:System.Data.IDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000B51 RID: 2897
		// (set) Token: 0x06000B52 RID: 2898
		CommandType CommandType { get; set; }

		/// <summary>Gets the <see cref="T:System.Data.IDataParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000B53 RID: 2899
		IDataParameterCollection Parameters { get; }

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not <see cref="M:System.Data.OleDb.OleDbConnection.Open" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B54 RID: 2900
		void Prepare();

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> method of a <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values. The default is Both unless the command is automatically generated. Then the default is None.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000B55 RID: 2901
		// (set) Token: 0x06000B56 RID: 2902
		UpdateRowSource UpdatedRowSource { get; set; }

		/// <summary>Attempts to cancels the execution of an <see cref="T:System.Data.IDbCommand" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B57 RID: 2903
		void Cancel();

		/// <summary>Creates a new instance of an <see cref="T:System.Data.IDbDataParameter" /> object.</summary>
		/// <returns>An IDbDataParameter object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B58 RID: 2904
		IDbDataParameter CreateParameter();

		/// <summary>Executes an SQL statement against the Connection object of a .NET Framework data provider, and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B59 RID: 2905
		int ExecuteNonQuery();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" /> and builds an <see cref="T:System.Data.IDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B5A RID: 2906
		IDataReader ExecuteReader();

		/// <summary>Executes the <see cref="P:System.Data.IDbCommand.CommandText" /> against the <see cref="P:System.Data.IDbCommand.Connection" />, and builds an <see cref="T:System.Data.IDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B5B RID: 2907
		IDataReader ExecuteReader(CommandBehavior behavior);

		/// <summary>Executes the query, and returns the first column of the first row in the resultset returned by the query. Extra columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the resultset.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B5C RID: 2908
		object ExecuteScalar();
	}
}
