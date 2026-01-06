using System;
using Unity.VisualScripting.FullSerializer.Internal;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017B RID: 379
	public class fsForwardConverter : fsConverter
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x0002A595 File Offset: 0x00028795
		public fsForwardConverter(fsForwardAttribute attribute)
		{
			this._memberName = attribute.MemberName;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002A5A9 File Offset: 0x000287A9
		public override bool CanProcess(Type type)
		{
			throw new NotSupportedException("Please use the [fsForward(...)] attribute.");
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002A5B8 File Offset: 0x000287B8
		private fsResult GetProperty(object instance, out fsMetaProperty property)
		{
			fsMetaProperty[] properties = fsMetaType.Get(this.Serializer.Config, instance.GetType()).Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].MemberName == this._memberName)
				{
					property = properties[i];
					return fsResult.Success;
				}
			}
			property = null;
			return fsResult.Fail("No property named \"" + this._memberName + "\" on " + instance.GetType().CSharpName());
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002A638 File Offset: 0x00028838
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = fsData.Null;
			fsResult fsResult = fsResult.Success;
			fsMetaProperty fsMetaProperty;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + this.GetProperty(instance, out fsMetaProperty));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			object obj = fsMetaProperty.Read(instance);
			return this.Serializer.TrySerialize(fsMetaProperty.StorageType, obj, out serialized);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002A68C File Offset: 0x0002888C
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsMetaProperty fsMetaProperty;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + this.GetProperty(instance, out fsMetaProperty));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			object obj = null;
			fsResult = (fsResult2 = fsResult + this.Serializer.TryDeserialize(data, fsMetaProperty.StorageType, ref obj));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			fsMetaProperty.Write(instance, obj);
			return fsResult;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A6F1 File Offset: 0x000288F1
		public override object CreateInstance(fsData data, Type storageType)
		{
			return fsMetaType.Get(this.Serializer.Config, storageType).CreateInstance();
		}

		// Token: 0x0400025D RID: 605
		private string _memberName;
	}
}
