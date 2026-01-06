using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a field.</summary>
	// Token: 0x02000310 RID: 784
	[Serializable]
	public class CodeFieldReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class.</summary>
		// Token: 0x060018EF RID: 6383 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
		public CodeFieldReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeFieldReferenceExpression" /> class using the specified target object and field name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field. </param>
		/// <param name="fieldName">The name of the field. </param>
		// Token: 0x060018F0 RID: 6384 RVA: 0x0005FBD4 File Offset: 0x0005DDD4
		public CodeFieldReferenceExpression(CodeExpression targetObject, string fieldName)
		{
			this.TargetObject = targetObject;
			this.FieldName = fieldName;
		}

		/// <summary>Gets or sets the object that contains the field to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the field to reference.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0005FBEA File Offset: 0x0005DDEA
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x0005FBF2 File Offset: 0x0005DDF2
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the field to reference.</summary>
		/// <returns>A string containing the field name.</returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0005FBFB File Offset: 0x0005DDFB
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		public string FieldName
		{
			get
			{
				return this._fieldName ?? string.Empty;
			}
			set
			{
				this._fieldName = value;
			}
		}

		// Token: 0x04000D8A RID: 3466
		private string _fieldName;
	}
}
