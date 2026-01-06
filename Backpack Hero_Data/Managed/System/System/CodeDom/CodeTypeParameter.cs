using System;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x02000339 RID: 825
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class. </summary>
		// Token: 0x06001A31 RID: 6705 RVA: 0x0005F5E1 File Offset: 0x0005D7E1
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name. </summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x06001A32 RID: 6706 RVA: 0x0006150B File Offset: 0x0005F70B
		public CodeTypeParameter(string name)
		{
			this._name = name;
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0006151A File Offset: 0x0005F71A
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x0006152B File Offset: 0x0005F72B
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

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00061534 File Offset: 0x0005F734
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._constraints) == null)
				{
					codeTypeReferenceCollection = (this._constraints = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is null.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x0006155C File Offset: 0x0005F75C
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
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>true if the type parameter has a constructor constraint; otherwise, false. The default is false.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x00061581 File Offset: 0x0005F781
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x00061589 File Offset: 0x0005F789
		public bool HasConstructorConstraint { get; set; }

		// Token: 0x04000DF7 RID: 3575
		private string _name;

		// Token: 0x04000DF8 RID: 3576
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000DF9 RID: 3577
		private CodeTypeReferenceCollection _constraints;
	}
}
