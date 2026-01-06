using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x0600180A RID: 6154 RVA: 0x0005EE15 File Offset: 0x0005D015
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x0600180B RID: 6155 RVA: 0x0005EE28 File Offset: 0x0005D028
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this._createType = createType;
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x0600180C RID: 6156 RVA: 0x0005EE4E File Offset: 0x0005D04E
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x0600180D RID: 6157 RVA: 0x0005EE79 File Offset: 0x0005D079
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x0600180E RID: 6158 RVA: 0x0005EEA4 File Offset: 0x0005D0A4
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this._createType = createType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005EEC5 File Offset: 0x0005D0C5
		public CodeArrayCreateExpression(string createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005EEEB File Offset: 0x0005D0EB
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0005EF11 File Offset: 0x0005D111
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this._createType = createType;
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06001812 RID: 6162 RVA: 0x0005EF32 File Offset: 0x0005D132
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x06001813 RID: 6163 RVA: 0x0005EF58 File Offset: 0x0005D158
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x0005EF80 File Offset: 0x0005D180
		// (set) Token: 0x06001815 RID: 6165 RVA: 0x0005EFAA File Offset: 0x0005D1AA
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._createType) == null)
				{
					codeTypeReference = (this._createType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._createType = value;
			}
		}

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001816 RID: 6166 RVA: 0x0005EFB3 File Offset: 0x0005D1B3
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this._initializers;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0005EFBB File Offset: 0x0005D1BB
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x0005EFC3 File Offset: 0x0005D1C3
		public int Size { get; set; }

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0005EFCC File Offset: 0x0005D1CC
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0005EFD4 File Offset: 0x0005D1D4
		public CodeExpression SizeExpression { get; set; }

		// Token: 0x04000D46 RID: 3398
		private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();

		// Token: 0x04000D47 RID: 3399
		private CodeTypeReference _createType;
	}
}
