using System;
using UnityEngine;

namespace Febucci.UI.Styles
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public struct Style
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000024B3 File Offset: 0x000006B3
		public Style(string styleTag, string openingTag, string closingTag)
		{
			this.styleTag = styleTag;
			this.openingTag = openingTag;
			this.closingTag = closingTag;
		}

		// Token: 0x0400001E RID: 30
		public string styleTag;

		// Token: 0x0400001F RID: 31
		[TextArea]
		public string openingTag;

		// Token: 0x04000020 RID: 32
		[TextArea]
		public string closingTag;
	}
}
