using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class SerializedPropertyProviderAttribute : Attribute, IDecoratorAttribute
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x000262A4 File Offset: 0x000244A4
		public SerializedPropertyProviderAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000262B3 File Offset: 0x000244B3
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x000262BB File Offset: 0x000244BB
		public Type type { get; private set; }
	}
}
