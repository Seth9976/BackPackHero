using System;
using System.Security;

namespace System.Data.SqlClient
{
	/// <summary>
	///   <see cref="T:System.Data.SqlClient.SqlCredential" /> provides a more secure way to specify the password for a login attempt using SQL Server Authentication.<see cref="T:System.Data.SqlClient.SqlCredential" /> is comprised of a user id and a password that will be used for SQL Server Authentication. The password in a <see cref="T:System.Data.SqlClient.SqlCredential" /> object is of type <see cref="T:System.Security.SecureString" />.<see cref="T:System.Data.SqlClient.SqlCredential" /> cannot be inherited.Windows Authentication (Integrated Security = true) remains the most secure way to log in to a SQL Server database.</summary>
	// Token: 0x02000231 RID: 561
	[Serializable]
	public sealed class SqlCredential
	{
		/// <summary>Creates an object of type <see cref="T:System.Data.SqlClient.SqlCredential" />.</summary>
		/// <param name="userId">The user id.</param>
		/// <param name="password">The password; a <see cref="T:System.Security.SecureString" /> value marked as read-only.  Passing a read/write <see cref="T:System.Security.SecureString" /> parameter will raise an <see cref="T:System.ArgumentException" />.</param>
		// Token: 0x06001A27 RID: 6695 RVA: 0x0008332C File Offset: 0x0008152C
		public SqlCredential(string userId, SecureString password)
		{
			if (userId == null)
			{
				throw new ArgumentNullException("userId");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.uid = userId;
			this.pwd = password;
		}

		/// <summary>Returns the user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>Returns the user ID component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object..</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x00083369 File Offset: 0x00081569
		public string UserId
		{
			get
			{
				return this.uid;
			}
		}

		/// <summary>Returns the password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</summary>
		/// <returns>Returns the password component of the <see cref="T:System.Data.SqlClient.SqlCredential" /> object.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x00083371 File Offset: 0x00081571
		public SecureString Password
		{
			get
			{
				return this.pwd;
			}
		}

		// Token: 0x0400129B RID: 4763
		private string uid = "";

		// Token: 0x0400129C RID: 4764
		private SecureString pwd;
	}
}
