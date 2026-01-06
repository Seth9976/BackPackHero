using System;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x020002F6 RID: 758
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x06001841 RID: 6209 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute. </param>
		// Token: 0x06001842 RID: 6210 RVA: 0x0005F1DB File Offset: 0x0005D3DB
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" />  that contains the arguments for the attribute. </param>
		// Token: 0x06001843 RID: 6211 RVA: 0x0005F1F5 File Offset: 0x0005D3F5
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x06001844 RID: 6212 RVA: 0x0005F21B File Offset: 0x0005D41B
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
			: this(attributeType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x06001845 RID: 6213 RVA: 0x0005F225 File Offset: 0x0005D425
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this._attributeType = attributeType;
			if (attributeType != null)
			{
				this._name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0005F25D File Offset: 0x0005D45D
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x0005F26E File Offset: 0x0005D46E
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
				this._attributeType = new CodeTypeReference(this._name);
			}
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001848 RID: 6216 RVA: 0x0005F288 File Offset: 0x0005D488
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x0005F290 File Offset: 0x0005D490
		public CodeTypeReference AttributeType
		{
			get
			{
				return this._attributeType;
			}
		}

		// Token: 0x04000D52 RID: 3410
		private string _name;

		// Token: 0x04000D53 RID: 3411
		private readonly CodeAttributeArgumentCollection _arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04000D54 RID: 3412
		private CodeTypeReference _attributeType;
	}
}
