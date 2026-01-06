using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000110 RID: 272
	public sealed class ReflectionPropertyAccessor : IOptimizedAccessor
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00021144 File Offset: 0x0001F344
		public ReflectionPropertyAccessor(PropertyInfo propertyInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				Ensure.That("propertyInfo").IsNotNull<PropertyInfo>(propertyInfo);
			}
			this.propertyInfo = propertyInfo;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0002116A File Offset: 0x0001F36A
		public void Compile()
		{
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0002116C File Offset: 0x0001F36C
		public object GetValue(object target)
		{
			return this.propertyInfo.GetValue(target, null);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0002117B File Offset: 0x0001F37B
		public void SetValue(object target, object value)
		{
			this.propertyInfo.SetValue(target, value, null);
		}

		// Token: 0x040001B2 RID: 434
		private readonly PropertyInfo propertyInfo;
	}
}
