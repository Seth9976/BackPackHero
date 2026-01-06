using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x02000306 RID: 774
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x060018B4 RID: 6324 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate. </param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method. </param>
		/// <param name="methodName">The name of the event-handler method. </param>
		// Token: 0x060018B5 RID: 6325 RVA: 0x0005F907 File Offset: 0x0005DB07
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this._delegateType = delegateType;
			this.TargetObject = targetObject;
			this._methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005F924 File Offset: 0x0005DB24
		// (set) Token: 0x060018B7 RID: 6327 RVA: 0x0005F94E File Offset: 0x0005DB4E
		public CodeTypeReference DelegateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._delegateType) == null)
				{
					codeTypeReference = (this._delegateType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._delegateType = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005F957 File Offset: 0x0005DB57
		// (set) Token: 0x060018B9 RID: 6329 RVA: 0x0005F95F File Offset: 0x0005DB5F
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0005F968 File Offset: 0x0005DB68
		// (set) Token: 0x060018BB RID: 6331 RVA: 0x0005F979 File Offset: 0x0005DB79
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

		// Token: 0x04000D80 RID: 3456
		private CodeTypeReference _delegateType;

		// Token: 0x04000D81 RID: 3457
		private string _methodName;
	}
}
