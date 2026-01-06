using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200010D RID: 269
	public static class OptimizedReflection
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x000203D8 File Offset: 0x0001E5D8
		internal static bool useJit
		{
			get
			{
				return OptimizedReflection.useJitIfAvailable && OptimizedReflection.jitAvailable;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x000203E8 File Offset: 0x0001E5E8
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x000203EF File Offset: 0x0001E5EF
		public static bool useJitIfAvailable
		{
			get
			{
				return OptimizedReflection._useJitIfAvailable;
			}
			set
			{
				OptimizedReflection._useJitIfAvailable = value;
				OptimizedReflection.ClearCache();
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x000203FC File Offset: 0x0001E5FC
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00020403 File Offset: 0x0001E603
		public static bool safeMode { get; set; }

		// Token: 0x06000701 RID: 1793 RVA: 0x0002040B File Offset: 0x0001E60B
		internal static void OnRuntimeMethodLoad()
		{
			OptimizedReflection.safeMode = Application.isEditor || Debug.isDebugBuild;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x00020421 File Offset: 0x0001E621
		public static void ClearCache()
		{
			OptimizedReflection.fieldAccessors.Clear();
			OptimizedReflection.propertyAccessors.Clear();
			OptimizedReflection.methodInvokers.Clear();
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00020441 File Offset: 0x0001E641
		internal static void VerifyStaticTarget(Type targetType, object target)
		{
			OptimizedReflection.VerifyTarget(targetType, target, true);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002044B File Offset: 0x0001E64B
		internal static void VerifyInstanceTarget<TTArget>(object target)
		{
			OptimizedReflection.VerifyTarget(typeof(TTArget), target, false);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x00020460 File Offset: 0x0001E660
		private static void VerifyTarget(Type targetType, object target, bool @static)
		{
			Ensure.That("targetType").IsNotNull<Type>(targetType);
			if (@static)
			{
				if (target != null)
				{
					throw new TargetException(string.Format("Superfluous target object for '{0}'.", targetType));
				}
			}
			else
			{
				if (target == null)
				{
					throw new TargetException(string.Format("Missing target object for '{0}'.", targetType));
				}
				if (!targetType.IsAssignableFrom(targetType))
				{
					throw new TargetException(string.Format("The target object does not match the target type.\nProvided: {0}\nExpected: {1}", target.GetType(), targetType));
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000204C8 File Offset: 0x0001E6C8
		private static bool SupportsOptimization(MemberInfo memberInfo)
		{
			return !memberInfo.DeclaringType.IsValueType || memberInfo.IsStatic();
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000204E2 File Offset: 0x0001E6E2
		public static IOptimizedAccessor Prewarm(this FieldInfo fieldInfo)
		{
			return OptimizedReflection.GetFieldAccessor(fieldInfo);
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000204EA File Offset: 0x0001E6EA
		public static object GetValueOptimized(this FieldInfo fieldInfo, object target)
		{
			return OptimizedReflection.GetFieldAccessor(fieldInfo).GetValue(target);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000204F8 File Offset: 0x0001E6F8
		public static void SetValueOptimized(this FieldInfo fieldInfo, object target, object value)
		{
			OptimizedReflection.GetFieldAccessor(fieldInfo).SetValue(target, value);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00020507 File Offset: 0x0001E707
		public static bool SupportsOptimization(this FieldInfo fieldInfo)
		{
			return OptimizedReflection.SupportsOptimization(fieldInfo);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00020514 File Offset: 0x0001E714
		private static IOptimizedAccessor GetFieldAccessor(FieldInfo fieldInfo)
		{
			Ensure.That("fieldInfo").IsNotNull<FieldInfo>(fieldInfo);
			Dictionary<FieldInfo, IOptimizedAccessor> dictionary = OptimizedReflection.fieldAccessors;
			IOptimizedAccessor optimizedAccessor2;
			lock (dictionary)
			{
				IOptimizedAccessor optimizedAccessor;
				if (!OptimizedReflection.fieldAccessors.TryGetValue(fieldInfo, out optimizedAccessor))
				{
					if (fieldInfo.SupportsOptimization())
					{
						Type type;
						if (fieldInfo.IsStatic)
						{
							type = typeof(StaticFieldAccessor<>).MakeGenericType(new Type[] { fieldInfo.FieldType });
						}
						else
						{
							type = typeof(InstanceFieldAccessor<, >).MakeGenericType(new Type[] { fieldInfo.DeclaringType, fieldInfo.FieldType });
						}
						optimizedAccessor = (IOptimizedAccessor)Activator.CreateInstance(type, new object[] { fieldInfo });
					}
					else
					{
						optimizedAccessor = new ReflectionFieldAccessor(fieldInfo);
					}
					optimizedAccessor.Compile();
					OptimizedReflection.fieldAccessors.Add(fieldInfo, optimizedAccessor);
				}
				optimizedAccessor2 = optimizedAccessor;
			}
			return optimizedAccessor2;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00020600 File Offset: 0x0001E800
		public static IOptimizedAccessor Prewarm(this PropertyInfo propertyInfo)
		{
			return OptimizedReflection.GetPropertyAccessor(propertyInfo);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00020608 File Offset: 0x0001E808
		public static object GetValueOptimized(this PropertyInfo propertyInfo, object target)
		{
			return OptimizedReflection.GetPropertyAccessor(propertyInfo).GetValue(target);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00020616 File Offset: 0x0001E816
		public static void SetValueOptimized(this PropertyInfo propertyInfo, object target, object value)
		{
			OptimizedReflection.GetPropertyAccessor(propertyInfo).SetValue(target, value);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00020625 File Offset: 0x0001E825
		public static bool SupportsOptimization(this PropertyInfo propertyInfo)
		{
			return OptimizedReflection.SupportsOptimization(propertyInfo);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00020634 File Offset: 0x0001E834
		private static IOptimizedAccessor GetPropertyAccessor(PropertyInfo propertyInfo)
		{
			Ensure.That("propertyInfo").IsNotNull<PropertyInfo>(propertyInfo);
			Dictionary<PropertyInfo, IOptimizedAccessor> dictionary = OptimizedReflection.propertyAccessors;
			IOptimizedAccessor optimizedAccessor2;
			lock (dictionary)
			{
				IOptimizedAccessor optimizedAccessor;
				if (!OptimizedReflection.propertyAccessors.TryGetValue(propertyInfo, out optimizedAccessor))
				{
					if (propertyInfo.SupportsOptimization())
					{
						Type type;
						if (propertyInfo.IsStatic())
						{
							type = typeof(StaticPropertyAccessor<>).MakeGenericType(new Type[] { propertyInfo.PropertyType });
						}
						else
						{
							type = typeof(InstancePropertyAccessor<, >).MakeGenericType(new Type[] { propertyInfo.DeclaringType, propertyInfo.PropertyType });
						}
						optimizedAccessor = (IOptimizedAccessor)Activator.CreateInstance(type, new object[] { propertyInfo });
					}
					else
					{
						optimizedAccessor = new ReflectionPropertyAccessor(propertyInfo);
					}
					optimizedAccessor.Compile();
					OptimizedReflection.propertyAccessors.Add(propertyInfo, optimizedAccessor);
				}
				optimizedAccessor2 = optimizedAccessor;
			}
			return optimizedAccessor2;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00020720 File Offset: 0x0001E920
		public static IOptimizedInvoker Prewarm(this MethodInfo methodInfo)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00020728 File Offset: 0x0001E928
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, params object[] args)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, args);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00020737 File Offset: 0x0001E937
		public static object InvokeOptimized(this MethodInfo methodInfo, object target)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00020745 File Offset: 0x0001E945
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, object arg0)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, arg0);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x00020754 File Offset: 0x0001E954
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, object arg0, object arg1)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00020764 File Offset: 0x0001E964
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, object arg0, object arg1, object arg2)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00020776 File Offset: 0x0001E976
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, object arg0, object arg1, object arg2, object arg3)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2, arg3);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0002078A File Offset: 0x0001E98A
		public static object InvokeOptimized(this MethodInfo methodInfo, object target, object arg0, object arg1, object arg2, object arg3, object arg4)
		{
			return OptimizedReflection.GetMethodInvoker(methodInfo).Invoke(target, arg0, arg1, arg2, arg3, arg4);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000207A0 File Offset: 0x0001E9A0
		public static bool SupportsOptimization(this MethodInfo methodInfo)
		{
			if (!OptimizedReflection.SupportsOptimization(methodInfo))
			{
				return false;
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (parameters.Length > 5)
			{
				return false;
			}
			return !parameters.Any((ParameterInfo parameter) => parameter.ParameterType.IsByRef) && (OptimizedReflection.jitAvailable || !methodInfo.IsVirtual || methodInfo.IsFinal) && methodInfo.CallingConvention != CallingConventions.VarArgs;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00020814 File Offset: 0x0001EA14
		private static IOptimizedInvoker GetMethodInvoker(MethodInfo methodInfo)
		{
			Ensure.That("methodInfo").IsNotNull<MethodInfo>(methodInfo);
			Dictionary<MethodInfo, IOptimizedInvoker> dictionary = OptimizedReflection.methodInvokers;
			IOptimizedInvoker optimizedInvoker2;
			lock (dictionary)
			{
				IOptimizedInvoker optimizedInvoker;
				if (!OptimizedReflection.methodInvokers.TryGetValue(methodInfo, out optimizedInvoker))
				{
					if (methodInfo.SupportsOptimization())
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						Type type;
						if (methodInfo.ReturnType == typeof(void))
						{
							if (methodInfo.IsStatic)
							{
								if (parameters.Length == 0)
								{
									type = typeof(StaticActionInvoker);
								}
								else if (parameters.Length == 1)
								{
									type = typeof(StaticActionInvoker<>).MakeGenericType(new Type[] { parameters[0].ParameterType });
								}
								else if (parameters.Length == 2)
								{
									type = typeof(StaticActionInvoker<, >).MakeGenericType(new Type[]
									{
										parameters[0].ParameterType,
										parameters[1].ParameterType
									});
								}
								else if (parameters.Length == 3)
								{
									type = typeof(StaticActionInvoker<, , >).MakeGenericType(new Type[]
									{
										parameters[0].ParameterType,
										parameters[1].ParameterType,
										parameters[2].ParameterType
									});
								}
								else if (parameters.Length == 4)
								{
									type = typeof(StaticActionInvoker<, , , >).MakeGenericType(new Type[]
									{
										parameters[0].ParameterType,
										parameters[1].ParameterType,
										parameters[2].ParameterType,
										parameters[3].ParameterType
									});
								}
								else
								{
									if (parameters.Length != 5)
									{
										throw new NotSupportedException();
									}
									type = typeof(StaticActionInvoker<, , , , >).MakeGenericType(new Type[]
									{
										parameters[0].ParameterType,
										parameters[1].ParameterType,
										parameters[2].ParameterType,
										parameters[3].ParameterType,
										parameters[4].ParameterType
									});
								}
							}
							else if (parameters.Length == 0)
							{
								type = typeof(InstanceActionInvoker<>).MakeGenericType(new Type[] { methodInfo.DeclaringType });
							}
							else if (parameters.Length == 1)
							{
								type = typeof(InstanceActionInvoker<, >).MakeGenericType(new Type[]
								{
									methodInfo.DeclaringType,
									parameters[0].ParameterType
								});
							}
							else if (parameters.Length == 2)
							{
								type = typeof(InstanceActionInvoker<, , >).MakeGenericType(new Type[]
								{
									methodInfo.DeclaringType,
									parameters[0].ParameterType,
									parameters[1].ParameterType
								});
							}
							else if (parameters.Length == 3)
							{
								type = typeof(InstanceActionInvoker<, , , >).MakeGenericType(new Type[]
								{
									methodInfo.DeclaringType,
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType
								});
							}
							else if (parameters.Length == 4)
							{
								type = typeof(InstanceActionInvoker<, , , , >).MakeGenericType(new Type[]
								{
									methodInfo.DeclaringType,
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType,
									parameters[3].ParameterType
								});
							}
							else
							{
								if (parameters.Length != 5)
								{
									throw new NotSupportedException();
								}
								type = typeof(InstanceActionInvoker<, , , , , >).MakeGenericType(new Type[]
								{
									methodInfo.DeclaringType,
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType,
									parameters[3].ParameterType,
									parameters[4].ParameterType
								});
							}
						}
						else if (methodInfo.IsStatic)
						{
							if (parameters.Length == 0)
							{
								type = typeof(StaticFunctionInvoker<>).MakeGenericType(new Type[] { methodInfo.ReturnType });
							}
							else if (parameters.Length == 1)
							{
								type = typeof(StaticFunctionInvoker<, >).MakeGenericType(new Type[]
								{
									parameters[0].ParameterType,
									methodInfo.ReturnType
								});
							}
							else if (parameters.Length == 2)
							{
								type = typeof(StaticFunctionInvoker<, , >).MakeGenericType(new Type[]
								{
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									methodInfo.ReturnType
								});
							}
							else if (parameters.Length == 3)
							{
								type = typeof(StaticFunctionInvoker<, , , >).MakeGenericType(new Type[]
								{
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType,
									methodInfo.ReturnType
								});
							}
							else if (parameters.Length == 4)
							{
								type = typeof(StaticFunctionInvoker<, , , , >).MakeGenericType(new Type[]
								{
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType,
									parameters[3].ParameterType,
									methodInfo.ReturnType
								});
							}
							else
							{
								if (parameters.Length != 5)
								{
									throw new NotSupportedException();
								}
								type = typeof(StaticFunctionInvoker<, , , , , >).MakeGenericType(new Type[]
								{
									parameters[0].ParameterType,
									parameters[1].ParameterType,
									parameters[2].ParameterType,
									parameters[3].ParameterType,
									parameters[4].ParameterType,
									methodInfo.ReturnType
								});
							}
						}
						else if (parameters.Length == 0)
						{
							type = typeof(InstanceFunctionInvoker<, >).MakeGenericType(new Type[] { methodInfo.DeclaringType, methodInfo.ReturnType });
						}
						else if (parameters.Length == 1)
						{
							type = typeof(InstanceFunctionInvoker<, , >).MakeGenericType(new Type[]
							{
								methodInfo.DeclaringType,
								parameters[0].ParameterType,
								methodInfo.ReturnType
							});
						}
						else if (parameters.Length == 2)
						{
							type = typeof(InstanceFunctionInvoker<, , , >).MakeGenericType(new Type[]
							{
								methodInfo.DeclaringType,
								parameters[0].ParameterType,
								parameters[1].ParameterType,
								methodInfo.ReturnType
							});
						}
						else if (parameters.Length == 3)
						{
							type = typeof(InstanceFunctionInvoker<, , , , >).MakeGenericType(new Type[]
							{
								methodInfo.DeclaringType,
								parameters[0].ParameterType,
								parameters[1].ParameterType,
								parameters[2].ParameterType,
								methodInfo.ReturnType
							});
						}
						else if (parameters.Length == 4)
						{
							type = typeof(InstanceFunctionInvoker<, , , , , >).MakeGenericType(new Type[]
							{
								methodInfo.DeclaringType,
								parameters[0].ParameterType,
								parameters[1].ParameterType,
								parameters[2].ParameterType,
								parameters[3].ParameterType,
								methodInfo.ReturnType
							});
						}
						else
						{
							if (parameters.Length != 5)
							{
								throw new NotSupportedException();
							}
							type = typeof(InstanceFunctionInvoker<, , , , , , >).MakeGenericType(new Type[]
							{
								methodInfo.DeclaringType,
								parameters[0].ParameterType,
								parameters[1].ParameterType,
								parameters[2].ParameterType,
								parameters[3].ParameterType,
								parameters[4].ParameterType,
								methodInfo.ReturnType
							});
						}
						optimizedInvoker = (IOptimizedInvoker)Activator.CreateInstance(type, new object[] { methodInfo });
					}
					else
					{
						optimizedInvoker = new ReflectionInvoker(methodInfo);
					}
					optimizedInvoker.Compile();
					OptimizedReflection.methodInvokers.Add(methodInfo, optimizedInvoker);
				}
				optimizedInvoker2 = optimizedInvoker;
			}
			return optimizedInvoker2;
		}

		// Token: 0x040001A9 RID: 425
		private static readonly Dictionary<FieldInfo, IOptimizedAccessor> fieldAccessors = new Dictionary<FieldInfo, IOptimizedAccessor>();

		// Token: 0x040001AA RID: 426
		private static readonly Dictionary<PropertyInfo, IOptimizedAccessor> propertyAccessors = new Dictionary<PropertyInfo, IOptimizedAccessor>();

		// Token: 0x040001AB RID: 427
		private static readonly Dictionary<MethodInfo, IOptimizedInvoker> methodInvokers = new Dictionary<MethodInfo, IOptimizedInvoker>();

		// Token: 0x040001AC RID: 428
		public static readonly bool jitAvailable = PlatformUtility.supportsJit;

		// Token: 0x040001AD RID: 429
		private static bool _useJitIfAvailable = true;
	}
}
