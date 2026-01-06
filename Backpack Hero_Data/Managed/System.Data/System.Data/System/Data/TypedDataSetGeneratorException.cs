using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Data
{
	/// <summary>The exception that is thrown when a name conflict occurs while generating a strongly typed <see cref="T:System.Data.DataSet" />. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class TypedDataSetGeneratorException : DataException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class using the specified serialization information and streaming context.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		// Token: 0x06000E1C RID: 3612 RVA: 0x00049A28 File Offset: 0x00047C28
		protected TypedDataSetGeneratorException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			int num = (int)info.GetValue(this.KEY_ARRAYCOUNT, typeof(int));
			if (num > 0)
			{
				this.errorList = new ArrayList();
				for (int i = 0; i < num; i++)
				{
					this.errorList.Add(info.GetValue(this.KEY_ARRAYVALUES + i.ToString(), typeof(string)));
				}
				return;
			}
			this.errorList = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		// Token: 0x06000E1D RID: 3613 RVA: 0x00049AC0 File Offset: 0x00047CC0
		public TypedDataSetGeneratorException()
		{
			this.errorList = null;
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		// Token: 0x06000E1E RID: 3614 RVA: 0x00049AF0 File Offset: 0x00047CF0
		public TypedDataSetGeneratorException(string message)
			: base(message)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class with the specified string and inner exception. </summary>
		/// <param name="message">The string to display when the exception is thrown.</param>
		/// <param name="innerException">A reference to an inner exception.</param>
		// Token: 0x06000E1F RID: 3615 RVA: 0x00049B1A File Offset: 0x00047D1A
		public TypedDataSetGeneratorException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146232021;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.TypedDataSetGeneratorException" /> class.</summary>
		/// <param name="list">
		///   <see cref="T:System.Collections.ArrayList" /> object containing a dynamic list of exceptions. </param>
		// Token: 0x06000E20 RID: 3616 RVA: 0x00049B45 File Offset: 0x00047D45
		public TypedDataSetGeneratorException(ArrayList list)
			: this()
		{
			this.errorList = list;
			base.HResult = -2146232021;
		}

		/// <summary>Gets a dynamic list of generated errors.</summary>
		/// <returns>
		///   <see cref="T:System.Collections.ArrayList" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00049B5F File Offset: 0x00047D5F
		public ArrayList ErrorList
		{
			get
			{
				return this.errorList;
			}
		}

		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the <see cref="T:System.Data.TypedDataSetGeneratorException" /> object.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object. </param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06000E22 RID: 3618 RVA: 0x00049B68 File Offset: 0x00047D68
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			if (this.errorList != null)
			{
				info.AddValue(this.KEY_ARRAYCOUNT, this.errorList.Count);
				for (int i = 0; i < this.errorList.Count; i++)
				{
					info.AddValue(this.KEY_ARRAYVALUES + i.ToString(), this.errorList[i].ToString());
				}
				return;
			}
			info.AddValue(this.KEY_ARRAYCOUNT, 0);
		}

		// Token: 0x04000988 RID: 2440
		private ArrayList errorList;

		// Token: 0x04000989 RID: 2441
		private string KEY_ARRAYCOUNT = "KEY_ARRAYCOUNT";

		// Token: 0x0400098A RID: 2442
		private string KEY_ARRAYVALUES = "KEY_ARRAYVALUES";
	}
}
