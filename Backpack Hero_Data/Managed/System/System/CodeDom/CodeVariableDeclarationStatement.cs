using System;

namespace System.CodeDom
{
	/// <summary>Represents a variable declaration.</summary>
	// Token: 0x0200033C RID: 828
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class.</summary>
		// Token: 0x06001A4D RID: 6733 RVA: 0x0005F031 File Offset: 0x0005D231
		public CodeVariableDeclarationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified type and name.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A4E RID: 6734 RVA: 0x000616AF File Offset: 0x0005F8AF
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type name and variable name.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A4F RID: 6735 RVA: 0x000616C5 File Offset: 0x0005F8C5
		public CodeVariableDeclarationStatement(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type and variable name.</summary>
		/// <param name="type">The data type for the variable. </param>
		/// <param name="name">The name of the variable. </param>
		// Token: 0x06001A50 RID: 6736 RVA: 0x000616E0 File Offset: 0x0005F8E0
		public CodeVariableDeclarationStatement(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A51 RID: 6737 RVA: 0x000616FB File Offset: 0x0005F8FB
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name, CodeExpression initExpression)
		{
			this.Type = type;
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The name of the data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A52 RID: 6738 RVA: 0x00061718 File Offset: 0x0005F918
		public CodeVariableDeclarationStatement(string type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The data type of the variable. </param>
		/// <param name="name">The name of the variable. </param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable. </param>
		// Token: 0x06001A53 RID: 6739 RVA: 0x0006173A File Offset: 0x0005F93A
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Gets or sets the initialization expression for the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x0006175C File Offset: 0x0005F95C
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x00061764 File Offset: 0x0005F964
		public CodeExpression InitExpression { get; set; }

		/// <summary>Gets or sets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0006176D File Offset: 0x0005F96D
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x0006177E File Offset: 0x0005F97E
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

		/// <summary>Gets or sets the data type of the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x00061788 File Offset: 0x0005F988
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x000617B2 File Offset: 0x0005F9B2
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

		// Token: 0x04000DFC RID: 3580
		private CodeTypeReference _type;

		// Token: 0x04000DFD RID: 3581
		private string _name;
	}
}
