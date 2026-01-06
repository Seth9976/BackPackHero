using System;
using System.Text;

namespace System
{
	// Token: 0x02000147 RID: 327
	public static class StringNormalizationExtensions
	{
		// Token: 0x060008C5 RID: 2245 RVA: 0x00020737 File Offset: 0x0001E937
		public static bool IsNormalized(this string strInput)
		{
			return strInput.IsNormalized(NormalizationForm.FormC);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00020740 File Offset: 0x0001E940
		public static bool IsNormalized(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.IsNormalized(normalizationForm);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00020757 File Offset: 0x0001E957
		public static string Normalize(this string strInput)
		{
			return strInput.Normalize(NormalizationForm.FormC);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00020760 File Offset: 0x0001E960
		public static string Normalize(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.Normalize(normalizationForm);
		}
	}
}
