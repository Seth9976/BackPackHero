using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x02000317 RID: 791
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x0600191A RID: 6426 RVA: 0x0005FDAD File Offset: 0x0005DFAD
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">An object that indicates the type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x0600191B RID: 6427 RVA: 0x0005FE21 File Offset: 0x0005E021
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x0600191C RID: 6428 RVA: 0x0005FE37 File Offset: 0x0005E037
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x0600191D RID: 6429 RVA: 0x0005FE52 File Offset: 0x0005E052
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600191E RID: 6430 RVA: 0x0005FE70 File Offset: 0x0005E070
		// (set) Token: 0x0600191F RID: 6431 RVA: 0x0005FE9A File Offset: 0x0005E09A
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

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001920 RID: 6432 RVA: 0x0005FEA3 File Offset: 0x0005E0A3
		// (set) Token: 0x06001921 RID: 6433 RVA: 0x0005FEAB File Offset: 0x0005E0AB
		public CodeExpression InitExpression { get; set; }

		// Token: 0x04000D9A RID: 3482
		private CodeTypeReference _type;
	}
}
