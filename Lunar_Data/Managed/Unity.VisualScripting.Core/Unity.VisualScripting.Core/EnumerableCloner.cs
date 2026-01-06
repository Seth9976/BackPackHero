using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting
{
	// Token: 0x02000006 RID: 6
	public sealed class EnumerableCloner : Cloner<IEnumerable>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002205 File Offset: 0x00000405
		public override bool Handles(Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type) && !typeof(IList).IsAssignableFrom(type) && this.GetAddMethod(type) != null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002238 File Offset: 0x00000438
		public override void FillClone(Type type, ref IEnumerable clone, IEnumerable original, CloningContext context)
		{
			IOptimizedInvoker addMethod = this.GetAddMethod(type);
			if (addMethod == null)
			{
				throw new InvalidOperationException(string.Format("Cannot instantiate enumerable type '{0}' because it does not provide an add method.", type));
			}
			foreach (object obj in original)
			{
				addMethod.Invoke(obj, Cloning.Clone(context, obj));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022AC File Offset: 0x000004AC
		private IOptimizedInvoker GetAddMethod(Type type)
		{
			if (!this.addMethods.ContainsKey(type))
			{
				Type @interface = fsReflectionUtility.GetInterface(type, typeof(ICollection<>));
				MethodInfo methodInfo;
				if ((methodInfo = ((@interface != null) ? @interface.GetDeclaredMethod("Add") : null)) == null && (methodInfo = type.GetFlattenedMethod("Add")) == null)
				{
					methodInfo = type.GetFlattenedMethod("Push") ?? type.GetFlattenedMethod("Enqueue");
				}
				MethodInfo methodInfo2 = methodInfo;
				this.addMethods.Add(type, (methodInfo2 != null) ? methodInfo2.Prewarm() : null);
			}
			return this.addMethods[type];
		}

		// Token: 0x04000001 RID: 1
		private readonly Dictionary<Type, IOptimizedInvoker> addMethods = new Dictionary<Type, IOptimizedInvoker>();
	}
}
