using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200010E RID: 270
	public sealed class ReflectionFieldAccessor : IOptimizedAccessor
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00020FCC File Offset: 0x0001F1CC
		public ReflectionFieldAccessor(FieldInfo fieldInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				Ensure.That("fieldInfo").IsNotNull<FieldInfo>(fieldInfo);
			}
			this.fieldInfo = fieldInfo;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00020FF2 File Offset: 0x0001F1F2
		public void Compile()
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		public object GetValue(object target)
		{
			return this.fieldInfo.GetValue(target);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00021002 File Offset: 0x0001F202
		public void SetValue(object target, object value)
		{
			this.fieldInfo.SetValue(target, value);
		}

		// Token: 0x040001AF RID: 431
		private readonly FieldInfo fieldInfo;
	}
}
