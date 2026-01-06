using System;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000117 RID: 279
	[MonoTODO("OleDb is not implemented.")]
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06000F73 RID: 3955 RVA: 0x0004F1DA File Offset: 0x0004D3DA
		internal OleDbInfoMessageEventArgs()
		{
			throw ADP.OleDb();
		}

		/// <summary>Gets the HRESULT following the ANSI SQL standard for the database.</summary>
		/// <returns>The HRESULT, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public int ErrorCode
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public OleDbErrorCollection Errors
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the full text of the error sent from the data source.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string Message
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public string Source
		{
			get
			{
				throw ADP.OleDb();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F78 RID: 3960 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		public override string ToString()
		{
			throw ADP.OleDb();
		}
	}
}
