using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x020006B2 RID: 1714
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x060036C3 RID: 14019 RVA: 0x000C26AF File Offset: 0x000C08AF
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x000C26BE File Offset: 0x000C08BE
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000C26CB File Offset: 0x000C08CB
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000C26DD File Offset: 0x000C08DD
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000C26EB File Offset: 0x000C08EB
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000C26F9 File Offset: 0x000C08F9
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000C2707 File Offset: 0x000C0907
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000C2715 File Offset: 0x000C0915
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000C2724 File Offset: 0x000C0924
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000C2732 File Offset: 0x000C0932
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000C2741 File Offset: 0x000C0941
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x04002087 RID: 8327
		private readonly Type _type;
	}
}
