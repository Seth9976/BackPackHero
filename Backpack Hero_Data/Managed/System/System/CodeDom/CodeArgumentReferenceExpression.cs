using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x06001806 RID: 6150 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference. </param>
		// Token: 0x06001807 RID: 6151 RVA: 0x0005EDEC File Offset: 0x0005CFEC
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this._parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005EDFB File Offset: 0x0005CFFB
		// (set) Token: 0x06001809 RID: 6153 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x04000D45 RID: 3397
		private string _parameterName;
	}
}
