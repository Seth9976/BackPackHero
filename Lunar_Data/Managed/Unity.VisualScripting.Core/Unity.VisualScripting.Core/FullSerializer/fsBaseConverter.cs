using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000190 RID: 400
	public abstract class fsBaseConverter
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0002CA70 File Offset: 0x0002AC70
		public virtual object CreateInstance(fsData data, Type storageType)
		{
			if (this.RequestCycleSupport(storageType))
			{
				string[] array = new string[5];
				array[0] = "Please override CreateInstance for ";
				int num = 1;
				Type type = base.GetType();
				array[num] = ((type != null) ? type.ToString() : null);
				array[2] = "; the object graph for ";
				array[3] = ((storageType != null) ? storageType.ToString() : null);
				array[4] = " can contain potentially contain cycles, so separated instance creation is needed";
				throw new InvalidOperationException(string.Concat(array));
			}
			return storageType;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002CAD5 File Offset: 0x0002ACD5
		public virtual bool RequestCycleSupport(Type storageType)
		{
			return !(storageType == typeof(string)) && (storageType.Resolve().IsClass || storageType.Resolve().IsInterface);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002CB05 File Offset: 0x0002AD05
		public virtual bool RequestInheritanceSupport(Type storageType)
		{
			return !storageType.Resolve().IsSealed;
		}

		// Token: 0x06000A91 RID: 2705
		public abstract fsResult TrySerialize(object instance, out fsData serialized, Type storageType);

		// Token: 0x06000A92 RID: 2706
		public abstract fsResult TryDeserialize(fsData data, ref object instance, Type storageType);

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002CB18 File Offset: 0x0002AD18
		protected fsResult FailExpectedType(fsData data, params fsDataType[] types)
		{
			string[] array = new string[7];
			array[0] = base.GetType().Name;
			array[1] = " expected one of ";
			array[2] = string.Join(", ", types.Select((fsDataType t) => t.ToString()).ToArray<string>());
			array[3] = " but got ";
			array[4] = data.Type.ToString();
			array[5] = " in ";
			array[6] = ((data != null) ? data.ToString() : null);
			return fsResult.Fail(string.Concat(array));
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002CBBC File Offset: 0x0002ADBC
		protected fsResult CheckType(fsData data, fsDataType type)
		{
			if (data.Type != type)
			{
				return fsResult.Fail(string.Concat(new string[]
				{
					base.GetType().Name,
					" expected ",
					type.ToString(),
					" but got ",
					data.Type.ToString(),
					" in ",
					(data != null) ? data.ToString() : null
				}));
			}
			return fsResult.Success;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002CC45 File Offset: 0x0002AE45
		protected fsResult CheckKey(fsData data, string key, out fsData subitem)
		{
			return this.CheckKey(data.AsDictionary, key, out subitem);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002CC58 File Offset: 0x0002AE58
		protected fsResult CheckKey(Dictionary<string, fsData> data, string key, out fsData subitem)
		{
			if (!data.TryGetValue(key, out subitem))
			{
				return fsResult.Fail(string.Concat(new string[]
				{
					base.GetType().Name,
					" requires a <",
					key,
					"> key in the data ",
					(data != null) ? data.ToString() : null
				}));
			}
			return fsResult.Success;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002CCB8 File Offset: 0x0002AEB8
		protected fsResult SerializeMember<T>(Dictionary<string, fsData> data, Type overrideConverterType, string name, T value)
		{
			fsData fsData;
			fsResult fsResult = this.Serializer.TrySerialize(typeof(T), overrideConverterType, value, out fsData);
			if (fsResult.Succeeded)
			{
				data[name] = fsData;
			}
			return fsResult;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		protected fsResult DeserializeMember<T>(Dictionary<string, fsData> data, Type overrideConverterType, string name, out T value)
		{
			fsData fsData;
			if (!data.TryGetValue(name, out fsData))
			{
				value = default(T);
				return fsResult.Fail("Unable to find member \"" + name + "\"");
			}
			object obj = null;
			fsResult fsResult = this.Serializer.TryDeserialize(fsData, typeof(T), overrideConverterType, ref obj);
			value = (T)((object)obj);
			return fsResult;
		}

		// Token: 0x0400026B RID: 619
		public fsSerializer Serializer;
	}
}
