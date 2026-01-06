using System;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x02000336 RID: 822
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0006131F File Offset: 0x0005F51F
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x00061330 File Offset: 0x0005F530
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />. </returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00061339 File Offset: 0x0005F539
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x00061341 File Offset: 0x0005F541
		public MemberAttributes Attributes { get; set; } = (MemberAttributes)20482;

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0006134C File Offset: 0x0005F54C
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x00061371 File Offset: 0x0005F571
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._customAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
			set
			{
				this._customAttributes = value;
			}
		}

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0006137A File Offset: 0x0005F57A
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00061382 File Offset: 0x0005F582
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0006138B File Offset: 0x0005F58B
		public CodeCommentStatementCollection Comments { get; } = new CodeCommentStatementCollection();

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00061394 File Offset: 0x0005F594
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000613BC File Offset: 0x0005F5BC
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04000DEF RID: 3567
		private string _name;

		// Token: 0x04000DF0 RID: 3568
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000DF1 RID: 3569
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000DF2 RID: 3570
		private CodeDirectiveCollection _endDirectives;
	}
}
