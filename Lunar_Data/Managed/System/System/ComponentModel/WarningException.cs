using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Specifies an exception that is handled as a warning instead of an error.</summary>
	// Token: 0x0200070F RID: 1807
	[Serializable]
	public class WarningException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class. </summary>
		// Token: 0x060039AD RID: 14765 RVA: 0x000C8CA1 File Offset: 0x000C6EA1
		public WarningException()
			: this(null, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message and no Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		// Token: 0x060039AE RID: 14766 RVA: 0x000C8CAC File Offset: 0x000C6EAC
		public WarningException(string message)
			: this(message, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		// Token: 0x060039AF RID: 14767 RVA: 0x000C8CB7 File Offset: 0x000C6EB7
		public WarningException(string message, string helpUrl)
			: this(message, helpUrl, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified detailed description and the specified exception. </summary>
		/// <param name="message">A detailed description of the error.</param>
		/// <param name="innerException">A reference to the inner exception that is the cause of this exception.</param>
		// Token: 0x060039B0 RID: 14768 RVA: 0x0002F0ED File Offset: 0x0002D2ED
		public WarningException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class with the specified message, and with access to the specified Help file and topic.</summary>
		/// <param name="message">The message to display to the end user. </param>
		/// <param name="helpUrl">The Help file to display if the user requests help. </param>
		/// <param name="helpTopic">The Help topic to display if the user requests help. </param>
		// Token: 0x060039B1 RID: 14769 RVA: 0x000C8CC2 File Offset: 0x000C6EC2
		public WarningException(string message, string helpUrl, string helpTopic)
			: base(message)
		{
			this.HelpUrl = helpUrl;
			this.HelpTopic = helpTopic;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.WarningException" /> class using the specified serialization data and context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		// Token: 0x060039B2 RID: 14770 RVA: 0x000C8CDC File Offset: 0x000C6EDC
		protected WarningException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.HelpUrl = (string)info.GetValue("helpUrl", typeof(string));
			this.HelpTopic = (string)info.GetValue("helpTopic", typeof(string));
		}

		/// <summary>Gets the Help file associated with the warning.</summary>
		/// <returns>The Help file associated with the warning.</returns>
		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000C8D31 File Offset: 0x000C6F31
		public string HelpUrl { get; }

		/// <summary>Gets the Help topic associated with the warning.</summary>
		/// <returns>The Help topic associated with the warning.</returns>
		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x000C8D39 File Offset: 0x000C6F39
		public string HelpTopic { get; }

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the parameter name and additional exception information.</summary>
		/// <param name="info">Stores the data that was being used to serialize or deserialize the object that the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> was serializing or deserializing. </param>
		/// <param name="context">Describes the source and destination of the stream that generated the exception, as well as a means for serialization to retain that context and an additional caller-defined context. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x060039B5 RID: 14773 RVA: 0x000C8D41 File Offset: 0x000C6F41
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("helpUrl", this.HelpUrl);
			info.AddValue("helpTopic", this.HelpTopic);
		}
	}
}
