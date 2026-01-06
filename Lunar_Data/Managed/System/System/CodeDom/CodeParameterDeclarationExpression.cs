using System;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x02000321 RID: 801
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x06001989 RID: 6537 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x0600198A RID: 6538 RVA: 0x00060977 File Offset: 0x0005EB77
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x0600198B RID: 6539 RVA: 0x0006098D File Offset: 0x0005EB8D
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x0600198C RID: 6540 RVA: 0x000609A8 File Offset: 0x0005EBA8
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600198D RID: 6541 RVA: 0x000609C4 File Offset: 0x0005EBC4
		// (set) Token: 0x0600198E RID: 6542 RVA: 0x000609E9 File Offset: 0x0005EBE9
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

		/// <summary>Gets or sets the direction of the field.</summary>
		/// <returns>An object that indicates the direction of the field.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x000609F2 File Offset: 0x0005EBF2
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x000609FA File Offset: 0x0005EBFA
		public FieldDirection Direction { get; set; }

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00060A04 File Offset: 0x0005EC04
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x00060A2E File Offset: 0x0005EC2E
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

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00060A37 File Offset: 0x0005EC37
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x00060A48 File Offset: 0x0005EC48
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

		// Token: 0x04000DC6 RID: 3526
		private CodeTypeReference _type;

		// Token: 0x04000DC7 RID: 3527
		private string _name;

		// Token: 0x04000DC8 RID: 3528
		private CodeAttributeDeclarationCollection _customAttributes;
	}
}
