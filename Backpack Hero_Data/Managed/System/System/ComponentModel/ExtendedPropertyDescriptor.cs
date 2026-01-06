using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x020006BF RID: 1727
	internal sealed class ExtendedPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x0600373C RID: 14140 RVA: 0x000C355C File Offset: 0x000C175C
		public ExtendedPropertyDescriptor(ReflectPropertyDescriptor extenderInfo, Type receiverType, IExtenderProvider provider, Attribute[] attributes)
			: base(extenderInfo, attributes)
		{
			List<Attribute> list = new List<Attribute>(this.AttributeArray);
			list.Add(ExtenderProvidedPropertyAttribute.Create(extenderInfo, receiverType, provider));
			if (extenderInfo.IsReadOnly)
			{
				list.Add(ReadOnlyAttribute.Yes);
			}
			Attribute[] array = new Attribute[list.Count];
			list.CopyTo(array, 0);
			this.AttributeArray = array;
			this._extenderInfo = extenderInfo;
			this._provider = provider;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000C35C8 File Offset: 0x000C17C8
		public ExtendedPropertyDescriptor(PropertyDescriptor extender, Attribute[] attributes)
			: base(extender, attributes)
		{
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = extender.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			ReflectPropertyDescriptor reflectPropertyDescriptor = extenderProvidedPropertyAttribute.ExtenderProperty as ReflectPropertyDescriptor;
			this._extenderInfo = reflectPropertyDescriptor;
			this._provider = extenderProvidedPropertyAttribute.Provider;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000C3617 File Offset: 0x000C1817
		public override bool CanResetValue(object comp)
		{
			return this._extenderInfo.ExtenderCanResetValue(this._provider, comp);
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000C362B File Offset: 0x000C182B
		public override Type ComponentType
		{
			get
			{
				return this._extenderInfo.ComponentType;
			}
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003740 RID: 14144 RVA: 0x000C3638 File Offset: 0x000C1838
		public override bool IsReadOnly
		{
			get
			{
				return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000C3659 File Offset: 0x000C1859
		public override Type PropertyType
		{
			get
			{
				return this._extenderInfo.ExtenderGetType(this._provider);
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000C366C File Offset: 0x000C186C
		public override string DisplayName
		{
			get
			{
				string text = base.DisplayName;
				DisplayNameAttribute displayNameAttribute = this.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayNameAttribute == null || displayNameAttribute.IsDefaultAttribute())
				{
					ISite site = MemberDescriptor.GetSite(this._provider);
					string text2 = ((site != null) ? site.Name : null);
					if (text2 != null && text2.Length > 0)
					{
						text = string.Format("{0} on {1}", text, text2);
					}
				}
				return text;
			}
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x000C36D8 File Offset: 0x000C18D8
		public override object GetValue(object comp)
		{
			return this._extenderInfo.ExtenderGetValue(this._provider, comp);
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x000C36EC File Offset: 0x000C18EC
		public override void ResetValue(object comp)
		{
			this._extenderInfo.ExtenderResetValue(this._provider, comp, this);
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x000C3701 File Offset: 0x000C1901
		public override void SetValue(object component, object value)
		{
			this._extenderInfo.ExtenderSetValue(this._provider, component, value, this);
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x000C3717 File Offset: 0x000C1917
		public override bool ShouldSerializeValue(object comp)
		{
			return this._extenderInfo.ExtenderShouldSerializeValue(this._provider, comp);
		}

		// Token: 0x040020AF RID: 8367
		private readonly ReflectPropertyDescriptor _extenderInfo;

		// Token: 0x040020B0 RID: 8368
		private readonly IExtenderProvider _provider;
	}
}
