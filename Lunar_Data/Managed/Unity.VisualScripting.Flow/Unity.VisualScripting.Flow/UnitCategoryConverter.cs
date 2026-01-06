using System;
using Unity.VisualScripting.FullSerializer;

namespace Unity.VisualScripting
{
	// Token: 0x0200017F RID: 383
	public class UnitCategoryConverter : fsDirectConverter
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x0001273C File Offset: 0x0001093C
		public override Type ModelType
		{
			get
			{
				return typeof(UnitCategory);
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00012748 File Offset: 0x00010948
		public override object CreateInstance(fsData data, Type storageType)
		{
			return new object();
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001274F File Offset: 0x0001094F
		public override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			serialized = new fsData(((UnitCategory)instance).fullName);
			return fsResult.Success;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00012768 File Offset: 0x00010968
		public override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			if (!data.IsString)
			{
				return fsResult.Fail("Expected string in " + ((data != null) ? data.ToString() : null));
			}
			instance = new UnitCategory(data.AsString);
			return fsResult.Success;
		}
	}
}
