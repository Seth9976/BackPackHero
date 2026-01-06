using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x02000319 RID: 793
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x00060183 File Offset: 0x0005E383
		// (set) Token: 0x06001933 RID: 6451 RVA: 0x0006018B File Offset: 0x0005E38B
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00060194 File Offset: 0x0005E394
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

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x000601BC File Offset: 0x0005E3BC
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x000601E6 File Offset: 0x0005E3E6
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

		/// <summary>Gets or sets a value indicating whether the property has a get method accessor.</summary>
		/// <returns>true if the Count property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to true; otherwise, false.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000601EF File Offset: 0x0005E3EF
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x00060209 File Offset: 0x0005E409
		public bool HasGet
		{
			get
			{
				return this._hasGet || this.GetStatements.Count > 0;
			}
			set
			{
				this._hasGet = value;
				if (!value)
				{
					this.GetStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a set method accessor.</summary>
		/// <returns>true if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, false.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00060220 File Offset: 0x0005E420
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x0006023A File Offset: 0x0005E43A
		public bool HasSet
		{
			get
			{
				return this._hasSet || this.SetStatements.Count > 0;
			}
			set
			{
				this._hasSet = value;
				if (!value)
				{
					this.SetStatements.Clear();
				}
			}
		}

		/// <summary>Gets the collection of get statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the get statements for the member property.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x00060251 File Offset: 0x0005E451
		public CodeStatementCollection GetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of set statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the set statements for the member property.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x00060259 File Offset: 0x0005E459
		public CodeStatementCollection SetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x00060261 File Offset: 0x0005E461
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04000DAA RID: 3498
		private CodeTypeReference _type;

		// Token: 0x04000DAB RID: 3499
		private bool _hasGet;

		// Token: 0x04000DAC RID: 3500
		private bool _hasSet;

		// Token: 0x04000DAD RID: 3501
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
