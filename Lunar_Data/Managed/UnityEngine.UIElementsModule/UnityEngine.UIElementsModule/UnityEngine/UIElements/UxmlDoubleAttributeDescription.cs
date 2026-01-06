using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D1 RID: 721
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		// Token: 0x060017FC RID: 6140 RVA: 0x000609BB File Offset: 0x0005EBBB
		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x000609F0 File Offset: 0x0005EBF0
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00060A1C File Offset: 0x0005EC1C
		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue);
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00060A5C File Offset: 0x0005EC5C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref double value)
		{
			return base.TryGetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue, ref value);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00060A9C File Offset: 0x0005EC9C
		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double num;
			bool flag = v == null || !double.TryParse(v, 167, CultureInfo.InvariantCulture, ref num);
			double num2;
			if (flag)
			{
				num2 = defaultValue;
			}
			else
			{
				num2 = num;
			}
			return num2;
		}
	}
}
