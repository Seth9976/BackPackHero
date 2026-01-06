using System;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x0200031C RID: 796
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Comments" /> collection is accessed.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001949 RID: 6473 RVA: 0x00060350 File Offset: 0x0005E550
		// (remove) Token: 0x0600194A RID: 6474 RVA: 0x00060388 File Offset: 0x0005E588
		public event EventHandler PopulateComments;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Imports" /> collection is accessed.</summary>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600194B RID: 6475 RVA: 0x000603C0 File Offset: 0x0005E5C0
		// (remove) Token: 0x0600194C RID: 6476 RVA: 0x000603F8 File Offset: 0x0005E5F8
		public event EventHandler PopulateImports;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Types" /> collection is accessed.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600194D RID: 6477 RVA: 0x00060430 File Offset: 0x0005E630
		// (remove) Token: 0x0600194E RID: 6478 RVA: 0x00060468 File Offset: 0x0005E668
		public event EventHandler PopulateTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x0600194F RID: 6479 RVA: 0x0006049D File Offset: 0x0005E69D
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared. </param>
		// Token: 0x06001950 RID: 6480 RVA: 0x000604C6 File Offset: 0x0005E6C6
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x000604F6 File Offset: 0x0005E6F6
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateTypes = this.PopulateTypes;
					if (populateTypes != null)
					{
						populateTypes(this, EventArgs.Empty);
					}
				}
				return this._classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x0006052D File Offset: 0x0005E72D
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateImports = this.PopulateImports;
					if (populateImports != null)
					{
						populateImports(this, EventArgs.Empty);
					}
				}
				return this._imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x00060564 File Offset: 0x0005E764
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x00060575 File Offset: 0x0005E775
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

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0006057E File Offset: 0x0005E77E
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateComments = this.PopulateComments;
					if (populateComments != null)
					{
						populateComments(this, EventArgs.Empty);
					}
				}
				return this._comments;
			}
		}

		// Token: 0x04000DB5 RID: 3509
		private string _name;

		// Token: 0x04000DB6 RID: 3510
		private readonly CodeNamespaceImportCollection _imports = new CodeNamespaceImportCollection();

		// Token: 0x04000DB7 RID: 3511
		private readonly CodeCommentStatementCollection _comments = new CodeCommentStatementCollection();

		// Token: 0x04000DB8 RID: 3512
		private readonly CodeTypeDeclarationCollection _classes = new CodeTypeDeclarationCollection();

		// Token: 0x04000DB9 RID: 3513
		private int _populated;

		// Token: 0x04000DBA RID: 3514
		private const int ImportsCollection = 1;

		// Token: 0x04000DBB RID: 3515
		private const int CommentsCollection = 2;

		// Token: 0x04000DBC RID: 3516
		private const int TypesCollection = 4;
	}
}
