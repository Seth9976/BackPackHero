using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000182 RID: 386
	public class fsTypeConverter : fsConverter
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x0002B33E File Offset: 0x0002953E
		public override bool CanProcess(Type type)
		{
			return typeof(Type).IsAssignableFrom(type);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0002B350 File Offset: 0x00029550
		public override bool RequestCycleSupport(Type type)
		{
			return false;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0002B353 File Offset: 0x00029553
		public override bool RequestInheritanceSupport(Type type)
		{
			return false;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0002B358 File Offset: 0x00029558
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Type type = (Type)instance;
			serialized = new fsData(RuntimeCodebase.SerializeType(type));
			return fsResult.Success;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002B380 File Offset: 0x00029580
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Type converter requires a string");
			}
			Type type;
			if (RuntimeCodebase.TryDeserializeType(data.AsString, out type))
			{
				instance = type;
				return fsResult.Success;
			}
			return fsResult.Fail("Unable to find type: '" + (data.AsString ?? "(null)") + "'.");
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0002B3DD File Offset: 0x000295DD
		public override object CreateInstance(fsData data, Type storageType)
		{
			return storageType;
		}
	}
}
