using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x0200014A RID: 330
	[Serializable]
	internal class InvariantComparer : IComparer
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00020A21 File Offset: 0x0001EC21
		internal InvariantComparer()
		{
			this.m_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00020A3C File Offset: 0x0001EC3C
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this.m_compareInfo.Compare(text, text2);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x0400057D RID: 1405
		private CompareInfo m_compareInfo;

		// Token: 0x0400057E RID: 1406
		internal static readonly InvariantComparer Default = new InvariantComparer();
	}
}
