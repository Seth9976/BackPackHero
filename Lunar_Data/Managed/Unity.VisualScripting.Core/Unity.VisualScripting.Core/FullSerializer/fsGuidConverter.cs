using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017C RID: 380
	public class fsGuidConverter : fsConverter
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x0002A709 File Offset: 0x00028909
		public override bool CanProcess(Type type)
		{
			return type == typeof(Guid);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002A71B File Offset: 0x0002891B
		public override bool RequestCycleSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A71E File Offset: 0x0002891E
		public override bool RequestInheritanceSupport(Type storageType)
		{
			return false;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002A724 File Offset: 0x00028924
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = new fsData(((Guid)instance).ToString());
			return fsResult.Success;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A751 File Offset: 0x00028951
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (data.IsString)
			{
				instance = new Guid(data.AsString);
				return fsResult.Success;
			}
			return fsResult.Fail("fsGuidConverter encountered an unknown JSON data type");
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A780 File Offset: 0x00028980
		public override object CreateInstance(fsData data, Type storageType)
		{
			return default(Guid);
		}
	}
}
