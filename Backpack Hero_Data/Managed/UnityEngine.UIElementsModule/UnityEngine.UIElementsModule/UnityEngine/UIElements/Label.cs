using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014B RID: 331
	public class Label : TextElement
	{
		// Token: 0x06000A9A RID: 2714 RVA: 0x0002A662 File Offset: 0x00028862
		public Label()
			: this(string.Empty)
		{
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002A671 File Offset: 0x00028871
		public Label(string text)
		{
			base.AddToClassList(Label.ussClassName);
			this.text = text;
		}

		// Token: 0x0400049C RID: 1180
		public new static readonly string ussClassName = "unity-label";

		// Token: 0x0200014C RID: 332
		public new class UxmlFactory : UxmlFactory<Label, Label.UxmlTraits>
		{
		}

		// Token: 0x0200014D RID: 333
		public new class UxmlTraits : TextElement.UxmlTraits
		{
		}
	}
}
