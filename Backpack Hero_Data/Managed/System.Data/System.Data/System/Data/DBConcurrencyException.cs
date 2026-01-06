using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown by the <see cref="T:System.Data.Common.DataAdapter" /> during an insert, update, or delete operation if the number of rows affected equals zero.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000047 RID: 71
	[Serializable]
	public sealed class DBConcurrencyException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		// Token: 0x060002BF RID: 703 RVA: 0x0000E55D File Offset: 0x0000C75D
		public DBConcurrencyException()
			: this("DB concurrency violation.", null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		// Token: 0x060002C0 RID: 704 RVA: 0x0000E56B File Offset: 0x0000C76B
		public DBConcurrencyException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The text string describing the details of the exception. </param>
		/// <param name="inner">A reference to an inner exception. </param>
		// Token: 0x060002C1 RID: 705 RVA: 0x0000E575 File Offset: 0x0000C775
		public DBConcurrencyException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232011;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DBConcurrencyException" /> class.</summary>
		/// <param name="message">The error message that explains the reason for this exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
		/// <param name="dataRows">An array containing the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception.</param>
		// Token: 0x060002C2 RID: 706 RVA: 0x0000E58A File Offset: 0x0000C78A
		public DBConcurrencyException(string message, Exception inner, DataRow[] dataRows)
			: base(message, inner)
		{
			base.HResult = -2146232011;
			this._dataRows = dataRows;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E5A6 File Offset: 0x0000C7A6
		private DBConcurrencyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Populates the aprcified serialization information object with the data needed to serialize the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DBConcurrencyException" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060002C4 RID: 708 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Data.DataRow" /> that generated the <see cref="T:System.Data.DBConcurrencyException" />.</summary>
		/// <returns>The value of the <see cref="T:System.Data.DataRow" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000E5DC File Offset: 0x0000C7DC
		public DataRow Row
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null || dataRows.Length == 0)
				{
					return null;
				}
				return dataRows[0];
			}
			set
			{
				this._dataRows = new DataRow[] { value };
			}
		}

		/// <summary>Gets the number of rows whose update failed, generating this exception.</summary>
		/// <returns>An integer containing a count of the number of rows whose update failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000E5F0 File Offset: 0x0000C7F0
		public int RowCount
		{
			get
			{
				DataRow[] dataRows = this._dataRows;
				if (dataRows == null)
				{
					return 0;
				}
				return dataRows.Length;
			}
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002C8 RID: 712 RVA: 0x0000E60C File Offset: 0x0000C80C
		public void CopyToRows(DataRow[] array)
		{
			this.CopyToRows(array, 0);
		}

		/// <summary>Copies the <see cref="T:System.Data.DataRow" /> objects whose update failure generated this exception, to the specified array of <see cref="T:System.Data.DataRow" /> objects, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Data.DataRow" /> objects to copy the <see cref="T:System.Data.DataRow" /> objects into.</param>
		/// <param name="arrayIndex">The destination array index to start copying into.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060002C9 RID: 713 RVA: 0x0000E618 File Offset: 0x0000C818
		public void CopyToRows(DataRow[] array, int arrayIndex)
		{
			DataRow[] dataRows = this._dataRows;
			if (dataRows != null)
			{
				dataRows.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x040004A7 RID: 1191
		private DataRow[] _dataRows;
	}
}
