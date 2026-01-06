using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000395 RID: 917
	[AttributeUsage(18432)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00032554 File Offset: 0x00030754
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00032568 File Offset: 0x00030768
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00032580 File Offset: 0x00030780
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			bool flag = defaultValueAttribute == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.DefaultValue == null;
				if (flag3)
				{
					flag2 = defaultValueAttribute.Value == null;
				}
				else
				{
					flag2 = this.DefaultValue.Equals(defaultValueAttribute.Value);
				}
			}
			return flag2;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000325D0 File Offset: 0x000307D0
		public override int GetHashCode()
		{
			bool flag = this.DefaultValue == null;
			int num;
			if (flag)
			{
				num = base.GetHashCode();
			}
			else
			{
				num = this.DefaultValue.GetHashCode();
			}
			return num;
		}

		// Token: 0x04000A2F RID: 2607
		private object DefaultValue;
	}
}
