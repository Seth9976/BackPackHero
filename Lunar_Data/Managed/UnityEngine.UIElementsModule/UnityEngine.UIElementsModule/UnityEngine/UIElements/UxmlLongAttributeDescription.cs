using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D5 RID: 725
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		// Token: 0x0600180E RID: 6158 RVA: 0x00060C01 File Offset: 0x0005EE01
		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x00060C2C File Offset: 0x0005EE2C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00060C58 File Offset: 0x0005EE58
		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00060C98 File Offset: 0x0005EE98
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref long value)
		{
			return base.TryGetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue, ref value);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00060CD8 File Offset: 0x0005EED8
		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long num;
			bool flag = v == null || !long.TryParse(v, ref num);
			long num2;
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
