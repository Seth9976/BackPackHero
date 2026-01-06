using System;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class RewriteCardinalityException : Exception
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00004FB2 File Offset: 0x00003FB2
		public RewriteCardinalityException(string elementDescription)
		{
			this.elementDescription = elementDescription;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004FC1 File Offset: 0x00003FC1
		public override string Message
		{
			get
			{
				if (this.elementDescription != null)
				{
					return this.elementDescription;
				}
				return null;
			}
		}

		// Token: 0x04000067 RID: 103
		public string elementDescription;
	}
}
