using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000183 RID: 387
	public class fsWeakReferenceConverter : fsConverter
	{
		// Token: 0x06000A54 RID: 2644 RVA: 0x0002B3E8 File Offset: 0x000295E8
		public override bool CanProcess(Type type)
		{
			return type == typeof(WeakReference);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002B3FA File Offset: 0x000295FA
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0002B3FD File Offset: 0x000295FD
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0002B400 File Offset: 0x00029600
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			WeakReference weakReference = (WeakReference)instance;
			fsResult fsResult = fsResult.Success;
			serialized = fsData.CreateDictionary();
			if (weakReference.IsAlive)
			{
				fsData fsData;
				fsResult fsResult2;
				fsResult = (fsResult2 = fsResult + this.Serializer.TrySerialize<object>(weakReference.Target, out fsData));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				serialized.AsDictionary["Target"] = fsData;
				serialized.AsDictionary["TrackResurrection"] = new fsData(weakReference.TrackResurrection);
			}
			return fsResult;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0002B480 File Offset: 0x00029680
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			if (data.AsDictionary.ContainsKey("Target"))
			{
				fsData fsData = data.AsDictionary["Target"];
				object obj = null;
				fsResult = (fsResult2 = fsResult + this.Serializer.TryDeserialize(fsData, typeof(object), ref obj));
				if (fsResult2.Failed)
				{
					return fsResult;
				}
				bool flag = false;
				if (data.AsDictionary.ContainsKey("TrackResurrection") && data.AsDictionary["TrackResurrection"].IsBool)
				{
					flag = data.AsDictionary["TrackResurrection"].AsBool;
				}
				instance = new WeakReference(obj, flag);
			}
			return fsResult;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0002B551 File Offset: 0x00029751
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new WeakReference(null);
		}
	}
}
