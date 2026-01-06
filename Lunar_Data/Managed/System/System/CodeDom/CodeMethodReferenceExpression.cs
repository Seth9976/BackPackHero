using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x02000340 RID: 832
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06001A5E RID: 6750 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		// Token: 0x06001A5F RID: 6751 RVA: 0x000617E4 File Offset: 0x0005F9E4
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06001A60 RID: 6752 RVA: 0x000617FA File Offset: 0x0005F9FA
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00061823 File Offset: 0x0005FA23
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x0006182B File Offset: 0x0005FA2B
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00061834 File Offset: 0x0005FA34
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x00061845 File Offset: 0x0005FA45
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00061850 File Offset: 0x0005FA50
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._typeArguments) == null)
				{
					codeTypeReferenceCollection = (this._typeArguments = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04000E15 RID: 3605
		private string _methodName;

		// Token: 0x04000E16 RID: 3606
		private CodeTypeReferenceCollection _typeArguments;
	}
}
