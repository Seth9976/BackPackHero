using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000176 RID: 374
	public class fsArrayConverter : fsConverter
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x00029960 File Offset: 0x00027B60
		public override bool CanProcess(Type type)
		{
			return type.IsArray;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00029968 File Offset: 0x00027B68
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002996B File Offset: 0x00027B6B
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00029970 File Offset: 0x00027B70
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			IList list = (Array)instance;
			Type elementType = storageType.GetElementType();
			fsResult success = fsResult.Success;
			serialized = fsData.CreateList(list.Count);
			List<fsData> asList = serialized.AsList;
			for (int i = 0; i < list.Count; i++)
			{
				object obj = list[i];
				fsData fsData;
				fsResult fsResult = this.Serializer.TrySerialize(elementType, obj, out fsData);
				success.AddMessages(fsResult);
				if (!fsResult.Failed)
				{
					asList.Add(fsData);
				}
			}
			return success;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000299F4 File Offset: 0x00027BF4
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Array));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			Type elementType = storageType.GetElementType();
			List<fsData> asList = data.AsList;
			ArrayList arrayList = new ArrayList(asList.Count);
			int count = arrayList.Count;
			for (int i = 0; i < asList.Count; i++)
			{
				fsData fsData = asList[i];
				object obj = null;
				if (i < count)
				{
					obj = arrayList[i];
				}
				fsResult fsResult3 = this.Serializer.TryDeserialize(fsData, elementType, ref obj);
				fsResult.AddMessages(fsResult3);
				if (!fsResult3.Failed)
				{
					if (i < count)
					{
						arrayList[i] = obj;
					}
					else
					{
						arrayList.Add(obj);
					}
				}
			}
			instance = arrayList.ToArray(elementType);
			return fsResult;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00029AC1 File Offset: 0x00027CC1
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}
	}
}
