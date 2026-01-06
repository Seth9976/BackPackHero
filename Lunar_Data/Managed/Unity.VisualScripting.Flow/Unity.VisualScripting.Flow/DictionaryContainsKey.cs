using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000023 RID: 35
	[UnitCategory("Collections/Dictionaries")]
	[UnitSurtitle("Dictionary")]
	[UnitShortTitle("Contains Key")]
	[TypeIcon(typeof(IDictionary))]
	public sealed class DictionaryContainsKey : Unit
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000536A File Offset: 0x0000356A
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005372 File Offset: 0x00003572
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput dictionary { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000537B File Offset: 0x0000357B
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005383 File Offset: 0x00003583
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput key { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000538C File Offset: 0x0000358C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00005394 File Offset: 0x00003594
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput contains { get; private set; }

		// Token: 0x06000160 RID: 352 RVA: 0x000053A0 File Offset: 0x000035A0
		protected override void Definition()
		{
			this.dictionary = base.ValueInput<IDictionary>("dictionary");
			this.key = base.ValueInput<object>("key");
			this.contains = base.ValueOutput<bool>("contains", new Func<Flow, bool>(this.Contains));
			base.Requirement(this.dictionary, this.contains);
			base.Requirement(this.key, this.contains);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005410 File Offset: 0x00003610
		private bool Contains(Flow flow)
		{
			IDictionary value = flow.GetValue<IDictionary>(this.dictionary);
			object value2 = flow.GetValue<object>(this.key);
			return value.Contains(value2);
		}
	}
}
