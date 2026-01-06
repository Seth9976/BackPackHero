using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DE RID: 734
	public class UxmlHash128AttributeDescription : TypedUxmlAttributeDescription<Hash128>
	{
		// Token: 0x06001839 RID: 6201 RVA: 0x000612C8 File Offset: 0x0005F4C8
		public UxmlHash128AttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(Hash128);
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x00061308 File Offset: 0x0005F508
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00061330 File Offset: 0x0005F530
		public override Hash128 GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00061370 File Offset: 0x0005F570
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Hash128 value)
		{
			return base.TryGetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue, ref value);
		}
	}
}
