using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017E RID: 382
	public class fsKeyValuePairConverter : fsConverter
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x0002AAAF File Offset: 0x00028CAF
		public override bool CanProcess(Type type)
		{
			return type.Resolve().IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<, >);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002AAD5 File Offset: 0x00028CD5
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002AAD8 File Offset: 0x00028CD8
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002AADC File Offset: 0x00028CDC
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsData fsData;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckKey(data, "Key", out fsData));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			fsData fsData2;
			fsResult = (fsResult2 = fsResult + base.CheckKey(data, "Value", out fsData2));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type[] genericArguments = storageType.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			object obj = null;
			object obj2 = null;
			fsResult.AddMessages(this.Serializer.TryDeserialize(fsData, type, ref obj));
			fsResult.AddMessages(this.Serializer.TryDeserialize(fsData2, type2, ref obj2));
			instance = Activator.CreateInstance(storageType, new object[] { obj, obj2 });
			return fsResult;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002AB90 File Offset: 0x00028D90
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			PropertyInfo declaredProperty = storageType.GetDeclaredProperty("Key");
			PropertyInfo declaredProperty2 = storageType.GetDeclaredProperty("Value");
			object value = declaredProperty.GetValue(instance, null);
			object value2 = declaredProperty2.GetValue(instance, null);
			Type[] genericArguments = storageType.GetGenericArguments();
			Type type = genericArguments[0];
			Type type2 = genericArguments[1];
			fsResult success = fsResult.Success;
			fsData fsData;
			success.AddMessages(this.Serializer.TrySerialize(type, value, out fsData));
			fsData fsData2;
			success.AddMessages(this.Serializer.TrySerialize(type2, value2, out fsData2));
			serialized = fsData.CreateDictionary();
			if (fsData != null)
			{
				serialized.AsDictionary["Key"] = fsData;
			}
			if (fsData2 != null)
			{
				serialized.AsDictionary["Value"] = fsData2;
			}
			return success;
		}
	}
}
