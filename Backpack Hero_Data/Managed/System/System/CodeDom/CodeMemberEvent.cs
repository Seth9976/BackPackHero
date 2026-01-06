using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an event of a type.</summary>
	// Token: 0x02000316 RID: 790
	[Serializable]
	public class CodeMemberEvent : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the delegate type that handles the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the delegate type that handles the event.</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x0005FDB8 File Offset: 0x0005DFB8
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x0005FDE2 File Offset: 0x0005DFE2
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the privately implemented data type, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type that the event privately implements.</returns>
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x0005FDEB File Offset: 0x0005DFEB
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x0005FDF3 File Offset: 0x0005DFF3
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets or sets the data type that the member event implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data type or types that the member event implements.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x0005FDFC File Offset: 0x0005DFFC
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._implementationTypes) == null)
				{
					codeTypeReferenceCollection = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04000D97 RID: 3479
		private CodeTypeReference _type;

		// Token: 0x04000D98 RID: 3480
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
