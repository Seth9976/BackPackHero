using System;
using System.Linq;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200010F RID: 271
	public class ReflectionInvoker : IOptimizedInvoker
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00021011 File Offset: 0x0001F211
		public ReflectionInvoker(MethodInfo methodInfo)
		{
			if (OptimizedReflection.safeMode)
			{
				Ensure.That("methodInfo").IsNotNull<MethodInfo>(methodInfo);
			}
			this.methodInfo = methodInfo;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00021037 File Offset: 0x0001F237
		public void Compile()
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00021039 File Offset: 0x0001F239
		public object Invoke(object target, params object[] args)
		{
			return this.methodInfo.Invoke(target, args);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00021048 File Offset: 0x0001F248
		public object Invoke(object target)
		{
			return this.methodInfo.Invoke(target, ReflectionInvoker.EmptyObjects);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0002105B File Offset: 0x0001F25B
		public object Invoke(object target, object arg0)
		{
			return this.methodInfo.Invoke(target, new object[] { arg0 });
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00021073 File Offset: 0x0001F273
		public object Invoke(object target, object arg0, object arg1)
		{
			return this.methodInfo.Invoke(target, new object[] { arg0, arg1 });
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0002108F File Offset: 0x0001F28F
		public object Invoke(object target, object arg0, object arg1, object arg2)
		{
			return this.methodInfo.Invoke(target, new object[] { arg0, arg1, arg2 });
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x000210B0 File Offset: 0x0001F2B0
		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3)
		{
			return this.methodInfo.Invoke(target, new object[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x000210D6 File Offset: 0x0001F2D6
		public object Invoke(object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return this.methodInfo.Invoke(target, new object[] { arg0, arg1, arg2, arg3, arg4 });
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00021101 File Offset: 0x0001F301
		public Type[] GetParameterTypes()
		{
			return (from pi in this.methodInfo.GetParameters()
				select pi.ParameterType).ToArray<Type>();
		}

		// Token: 0x040001B0 RID: 432
		private readonly MethodInfo methodInfo;

		// Token: 0x040001B1 RID: 433
		private static readonly object[] EmptyObjects = new object[0];
	}
}
