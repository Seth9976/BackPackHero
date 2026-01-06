using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DB RID: 731
	public class UxmlTypeAttributeDescription<TBase> : TypedUxmlAttributeDescription<Type>
	{
		// Token: 0x06001829 RID: 6185 RVA: 0x00060F69 File Offset: 0x0005F169
		public UxmlTypeAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = null;
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x00060F94 File Offset: 0x0005F194
		public override string defaultValueAsString
		{
			get
			{
				return (base.defaultValue == null) ? "null" : base.defaultValue.FullName;
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00060FC0 File Offset: 0x0005F1C0
		public override Type GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00060FEC File Offset: 0x0005F1EC
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Type value)
		{
			return base.TryGetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue, ref value);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0006101C File Offset: 0x0005F21C
		private Type ConvertValueToType(string v, Type defaultValue)
		{
			bool flag = string.IsNullOrEmpty(v);
			Type type;
			if (flag)
			{
				type = defaultValue;
			}
			else
			{
				try
				{
					Type type2 = Type.GetType(v, true);
					bool flag2 = !typeof(TBase).IsAssignableFrom(type2);
					if (!flag2)
					{
						return type2;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"Type: Invalid type \"",
						v,
						"\". Type must derive from ",
						typeof(TBase).FullName,
						"."
					}));
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				type = defaultValue;
			}
			return type;
		}
	}
}
