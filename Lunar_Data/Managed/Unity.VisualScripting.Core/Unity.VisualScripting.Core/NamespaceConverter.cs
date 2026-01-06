using System;
using Unity.VisualScripting.FullSerializer;

namespace Unity.VisualScripting
{
	// Token: 0x0200012D RID: 301
	public class NamespaceConverter : fsDirectConverter
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x000253DA File Offset: 0x000235DA
		public override Type ModelType
		{
			get
			{
				return typeof(Namespace);
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000253E6 File Offset: 0x000235E6
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new object();
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000253ED File Offset: 0x000235ED
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = new fsData(((Namespace)instance).FullName);
			return fsResult.Success;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00025406 File Offset: 0x00023606
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Expected string in " + ((data != null) ? data.ToString() : null));
			}
			instance = Namespace.FromFullName(data.AsString);
			return fsResult.Success;
		}
	}
}
