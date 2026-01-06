using System;
using System.Linq;

namespace UnityEngine.Rendering
{
	// Token: 0x02000076 RID: 118
	public static class DocumentationUtils
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00011A7C File Offset: 0x0000FC7C
		public static string GetHelpURL<TEnum>(TEnum mask = default(TEnum)) where TEnum : struct, IConvertible
		{
			HelpURLAttribute helpURLAttribute = (HelpURLAttribute)mask.GetType().GetCustomAttributes(typeof(HelpURLAttribute), false).FirstOrDefault<object>();
			if (helpURLAttribute != null)
			{
				return string.Format("{0}#{1}", helpURLAttribute.URL, mask);
			}
			return string.Empty;
		}
	}
}
