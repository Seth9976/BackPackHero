using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x02000324 RID: 804
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x060019A6 RID: 6566 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference. </param>
		/// <param name="propertyName">The name of the property to reference. </param>
		// Token: 0x060019A7 RID: 6567 RVA: 0x00060B14 File Offset: 0x0005ED14
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x00060B2A File Offset: 0x0005ED2A
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x00060B32 File Offset: 0x0005ED32
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00060B3B File Offset: 0x0005ED3B
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x00060B4C File Offset: 0x0005ED4C
		public string PropertyName
		{
			get
			{
				return this._propertyName ?? string.Empty;
			}
			set
			{
				this._propertyName = value;
			}
		}

		// Token: 0x04000DCB RID: 3531
		private string _propertyName;
	}
}
