using System;
using Unity.VisualScripting.FullSerializer;

namespace Unity.VisualScripting
{
	// Token: 0x0200012C RID: 300
	public class LooseAssemblyNameConverter : fsDirectConverter
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00025368 File Offset: 0x00023568
		public override Type ModelType
		{
			get
			{
				return typeof(LooseAssemblyName);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00025374 File Offset: 0x00023574
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new object();
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002537B File Offset: 0x0002357B
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = new fsData(((LooseAssemblyName)instance).name);
			return fsResult.Success;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00025394 File Offset: 0x00023594
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Expected string in " + ((data != null) ? data.ToString() : null));
			}
			instance = new LooseAssemblyName(data.AsString);
			return fsResult.Success;
		}
	}
}
