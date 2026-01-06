using System;
using System.Collections.Generic;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x02000198 RID: 408
	public abstract class fsDirectConverter<TModel> : fsDirectConverter
	{
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002D3AF File Offset: 0x0002B5AF
		public override Type ModelType
		{
			get
			{
				return typeof(TModel);
			}
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		public sealed override fsResult TrySerialize(object instance, out fsData serialized, Type storageType)
		{
			Dictionary<string, fsData> dictionary = new Dictionary<string, fsData>();
			fsResult fsResult = this.DoSerialize((TModel)((object)instance), dictionary);
			serialized = new fsData(dictionary);
			return fsResult;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0002D3E4 File Offset: 0x0002B5E4
		public sealed override fsResult TryDeserialize(fsData data, ref object instance, Type storageType)
		{
			fsResult fsResult = fsResult.Success;
			fsResult fsResult2;
			fsResult = (fsResult2 = fsResult + base.CheckType(data, fsDataType.Object));
			if (fsResult2.Failed)
			{
				return fsResult;
			}
			TModel tmodel = (TModel)((object)instance);
			fsResult += this.DoDeserialize(data.AsDictionary, ref tmodel);
			instance = tmodel;
			return fsResult;
		}

		// Token: 0x06000ACA RID: 2762
		protected abstract fsResult DoSerialize(TModel model, Dictionary<string, fsData> serialized);

		// Token: 0x06000ACB RID: 2763
		protected abstract fsResult DoDeserialize(Dictionary<string, fsData> data, ref TModel model);
	}
}
