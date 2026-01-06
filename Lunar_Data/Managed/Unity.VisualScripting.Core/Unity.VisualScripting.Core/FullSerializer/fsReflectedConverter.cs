using System;
using System.Collections;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000181 RID: 385
	public class fsReflectedConverter : fsConverter
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x0002B173 File Offset: 0x00029373
		public override bool CanProcess(Type type)
		{
			return !type.Resolve().IsArray && !typeof(ICollection).IsAssignableFrom(type);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002B198 File Offset: 0x00029398
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = fsData.CreateDictionary();
			fsResult success = fsResult.Success;
			fsMetaType fsMetaType = fsMetaType.Get(this.Serializer.Config, instance.GetType());
			fsMetaType.EmitAotData();
			for (int i = 0; i < fsMetaType.Properties.Length; i++)
			{
				fsMetaProperty fsMetaProperty = fsMetaType.Properties[i];
				if (fsMetaProperty.CanRead)
				{
					fsData fsData;
					fsResult fsResult = this.Serializer.TrySerialize(fsMetaProperty.StorageType, fsMetaProperty.OverrideConverterType, fsMetaProperty.Read(instance), out fsData);
					success.AddMessages(fsResult);
					if (!fsResult.Failed)
					{
						serialized.AsDictionary[fsMetaProperty.JsonName] = fsData;
					}
				}
			}
			return success;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0002B240 File Offset: 0x00029440
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			fsMetaType fsMetaType = fsMetaType.Get(this.Serializer.Config, storageType);
			fsMetaType.EmitAotData();
			for (int i = 0; i < fsMetaType.Properties.Length; i++)
			{
				fsMetaProperty fsMetaProperty = fsMetaType.Properties[i];
				fsData fsData;
				if (fsMetaProperty.CanWrite && data.AsDictionary.TryGetValue(fsMetaProperty.JsonName, out fsData))
				{
					object obj = null;
					if (fsMetaProperty.CanRead)
					{
						obj = fsMetaProperty.Read(instance);
					}
					fsResult fsResult3 = this.Serializer.TryDeserialize(fsData, fsMetaProperty.StorageType, fsMetaProperty.OverrideConverterType, ref obj);
					fsResult.AddMessages(fsResult3);
					if (!fsResult3.Failed)
					{
						fsMetaProperty.Write(instance, obj);
					}
				}
			}
			return fsResult;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0002B31E File Offset: 0x0002951E
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}
	}
}
