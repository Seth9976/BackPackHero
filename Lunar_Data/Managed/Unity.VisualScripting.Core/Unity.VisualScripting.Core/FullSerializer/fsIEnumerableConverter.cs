using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017D RID: 381
	public class fsIEnumerableConverter : fsConverter
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x0002A7A3 File Offset: 0x000289A3
		public override bool CanProcess(Type type)
		{
			return typeof(IEnumerable).IsAssignableFrom(type) && fsIEnumerableConverter.GetAddMethod(type) != null;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002A7C5 File Offset: 0x000289C5
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002A7E0 File Offset: 0x000289E0
		public override fsResult TrySerialize(object instance_, out fsData serialized, Type storageType)
		{
			IEnumerable enumerable = (IEnumerable)instance_;
			fsResult success = fsResult.Success;
			Type elementType = fsIEnumerableConverter.GetElementType(storageType);
			serialized = fsData.CreateList(fsIEnumerableConverter.HintSize(enumerable));
			List<fsData> asList = serialized.AsList;
			foreach (object obj in enumerable)
			{
				fsData fsData;
				fsResult fsResult = this.Serializer.TrySerialize(elementType, obj, out fsData);
				success.AddMessages(fsResult);
				if (!fsResult.Failed)
				{
					asList.Add(fsData);
				}
			}
			if (this.IsStack(enumerable.GetType()))
			{
				asList.Reverse();
			}
			return success;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002A89C File Offset: 0x00028A9C
		private bool IsStack(Type type)
		{
			return type.Resolve().IsGenericType && type.Resolve().GetGenericTypeDefinition() == typeof(Stack<>);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002A8C8 File Offset: 0x00028AC8
		public override fsResult TryDeserialize(fsData data, ref object instance_, Type storageType)
		{
			IEnumerable enumerable = (IEnumerable)instance_;
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Array));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type elementType = fsIEnumerableConverter.GetElementType(storageType);
			MethodInfo addMethod = fsIEnumerableConverter.GetAddMethod(storageType);
			fsIEnumerableConverter.TryClear(storageType, enumerable);
			List<fsData> asList = data.AsList;
			for (int i = 0; i < asList.Count; i++)
			{
				fsData fsData = asList[i];
				object obj = null;
				fsResult fsResult3 = this.Serializer.TryDeserialize(fsData, elementType, ref obj);
				fsResult.AddMessages(fsResult3);
				if (fsResult3.Succeeded)
				{
					addMethod.Invoke(enumerable, new object[] { obj });
				}
			}
			return fsResult;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002A979 File Offset: 0x00028B79
		private static int HintSize(IEnumerable collection)
		{
			if (collection is ICollection)
			{
				return ((ICollection)collection).Count;
			}
			return 0;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002A990 File Offset: 0x00028B90
		private static Type GetElementType(Type objectType)
		{
			if (objectType.HasElementType)
			{
				return objectType.GetElementType();
			}
			Type @interface = fsReflectionUtility.GetInterface(objectType, typeof(IEnumerable<>));
			if (@interface != null)
			{
				return @interface.GetGenericArguments()[0];
			}
			return typeof(object);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002A9DC File Offset: 0x00028BDC
		private static void TryClear(Type type, object instance)
		{
			MethodInfo flattenedMethod = type.GetFlattenedMethod("Clear");
			if (flattenedMethod != null)
			{
				flattenedMethod.Invoke(instance, null);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002AA08 File Offset: 0x00028C08
		private static int TryGetExistingSize(Type type, object instance)
		{
			PropertyInfo flattenedProperty = type.GetFlattenedProperty("Count");
			if (flattenedProperty != null)
			{
				return (int)flattenedProperty.GetGetMethod().Invoke(instance, null);
			}
			return 0;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002AA40 File Offset: 0x00028C40
		private static MethodInfo GetAddMethod(Type type)
		{
			Type @interface = fsReflectionUtility.GetInterface(type, typeof(ICollection<>));
			if (@interface != null)
			{
				MethodInfo declaredMethod = @interface.GetDeclaredMethod("Add");
				if (declaredMethod != null)
				{
					return declaredMethod;
				}
			}
			MethodInfo methodInfo;
			if ((methodInfo = type.GetFlattenedMethod("Add")) == null)
			{
				methodInfo = type.GetFlattenedMethod("Push") ?? type.GetFlattenedMethod("Enqueue");
			}
			return methodInfo;
		}
	}
}
