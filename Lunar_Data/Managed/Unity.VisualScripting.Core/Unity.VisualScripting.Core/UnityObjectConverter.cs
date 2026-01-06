using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000130 RID: 304
	public class UnityObjectConverter : fsConverter
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x000255AF File Offset: 0x000237AF
		private List<Object> objectReferences
		{
			get
			{
				return this.Serializer.Context.Get<List<Object>>();
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000255C1 File Offset: 0x000237C1
		public override bool CanProcess(Type type)
		{
			return typeof(Object).IsAssignableFrom(type);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000255D3 File Offset: 0x000237D3
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000255D6 File Offset: 0x000237D6
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000255DC File Offset: 0x000237DC
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Object @object = (Object)instance;
			int count = this.objectReferences.Count;
			serialized = new fsData((long)count);
			this.objectReferences.Add(@object);
			return fsResult.Success;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00025618 File Offset: 0x00023818
		public override fsResult TryDeserialize(fsData storage, ref object instance, Type storageType)
		{
			int num = (int)storage.AsInt64;
			fsResult success = fsResult.Success;
			if (num >= 0 && num < this.objectReferences.Count)
			{
				Object @object = this.objectReferences[num];
				instance = @object;
				if (instance != null && !storageType.IsInstanceOfType(instance))
				{
					if (@object.GetHashCode() != 0)
					{
						success.AddMessage(string.Format("Object reference at index #{0} does not match target type ({1} != {2}). Defaulting to null.", num, instance.GetType(), storageType));
					}
					instance = null;
				}
			}
			else
			{
				success.AddMessage(string.Format("No object reference provided at index #{0}. Defaulting to null.", num));
				instance = null;
			}
			return success;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000256AA File Offset: 0x000238AA
		public override object CreateInstance(fsData data, Type storageType)
		{
			return storageType;
		}
	}
}
