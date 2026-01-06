using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CF RID: 719
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		// Token: 0x060017F3 RID: 6131 RVA: 0x00060894 File Offset: 0x0005EA94
		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000608C4 File Offset: 0x0005EAC4
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000608F0 File Offset: 0x0005EAF0
		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00060930 File Offset: 0x0005EB30
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref float value)
		{
			return base.TryGetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue, ref value);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00060970 File Offset: 0x0005EB70
		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float num;
			bool flag = v == null || !float.TryParse(v, 167, CultureInfo.InvariantCulture, ref num);
			float num2;
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
