using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000024 RID: 36
	[UnitCategory("Collections/Dictionaries")]
	[UnitSurtitle("Dictionary")]
	[UnitShortTitle("Get Item")]
	[UnitOrder(0)]
	[TypeIcon(typeof(IDictionary))]
	public sealed class GetDictionaryItem : Unit
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00005444 File Offset: 0x00003644
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000544C File Offset: 0x0000364C
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput dictionary { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005455 File Offset: 0x00003655
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000545D File Offset: 0x0000365D
		[DoNotSerialize]
		public ValueInput key { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00005466 File Offset: 0x00003666
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000546E File Offset: 0x0000366E
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput value { get; private set; }

		// Token: 0x06000169 RID: 361 RVA: 0x00005478 File Offset: 0x00003678
		protected override void Definition()
		{
			this.dictionary = base.ValueInput<IDictionary>("dictionary");
			this.key = base.ValueInput<object>("key");
			this.value = base.ValueOutput<object>("value", new Func<Flow, object>(this.Get));
			base.Requirement(this.dictionary, this.value);
			base.Requirement(this.key, this.value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000054E8 File Offset: 0x000036E8
		private object Get(Flow flow)
		{
			IDictionary value = flow.GetValue<IDictionary>(this.dictionary);
			object value2 = flow.GetValue<object>(this.key);
			return value[value2];
		}
	}
}
