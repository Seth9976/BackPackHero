using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class TypeIconAttribute : Attribute
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00004E80 File Offset: 0x00003080
		public TypeIconAttribute(Type type)
		{
			Ensure.That("type").IsNotNull<Type>(type);
			this.type = type;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00004E9F File Offset: 0x0000309F
		public Type type { get; }
	}
}
