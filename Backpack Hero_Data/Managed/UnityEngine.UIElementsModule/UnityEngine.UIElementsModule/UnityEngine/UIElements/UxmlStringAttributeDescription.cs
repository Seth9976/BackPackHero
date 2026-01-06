using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002CD RID: 717
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		// Token: 0x060017EB RID: 6123 RVA: 0x000607BF File Offset: 0x0005E9BF
		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x000607F0 File Offset: 0x0005E9F0
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue;
			}
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00060808 File Offset: 0x0005EA08
		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00060848 File Offset: 0x0005EA48
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			return base.TryGetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue, ref value);
		}
	}
}
