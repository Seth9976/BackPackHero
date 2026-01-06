using System;
using System.Collections.Generic;
using System.Globalization;

namespace Febucci.UI.Core
{
	// Token: 0x02000045 RID: 69
	public static class FormatUtils
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00006ED8 File Offset: 0x000050D8
		public static bool TryGetFloat(List<string> attributes, int index, float defValue, out float result)
		{
			if (index >= attributes.Count || index < 0)
			{
				result = defValue;
				return false;
			}
			return FormatUtils.TryGetFloat(attributes[index], defValue, out result);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006EFA File Offset: 0x000050FA
		public static bool TryGetFloat(string attribute, float defValue, out float result)
		{
			if (FormatUtils.ParseFloat(attribute, out result))
			{
				return true;
			}
			result = defValue;
			return false;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006F0B File Offset: 0x0000510B
		public static bool ParseFloat(string value, out float result)
		{
			return float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
		}
	}
}
