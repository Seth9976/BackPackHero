using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D7 RID: 727
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		// Token: 0x06001817 RID: 6167 RVA: 0x00060D19 File Offset: 0x0005EF19
		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x00060D44 File Offset: 0x0005EF44
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString().ToLower();
			}
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00060D6C File Offset: 0x0005EF6C
		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00060DAC File Offset: 0x0005EFAC
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref bool value)
		{
			return base.TryGetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue, ref value);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00060DEC File Offset: 0x0005EFEC
		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool flag2;
			bool flag = v == null || !bool.TryParse(v, ref flag2);
			bool flag3;
			if (flag)
			{
				flag3 = defaultValue;
			}
			else
			{
				flag3 = flag2;
			}
			return flag3;
		}
	}
}
