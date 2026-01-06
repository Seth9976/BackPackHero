using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000026 RID: 38
	[UnitCategory("Collections/Dictionaries")]
	[UnitSurtitle("Dictionary")]
	[UnitShortTitle("Remove Item")]
	[UnitOrder(3)]
	public sealed class RemoveDictionaryItem : Unit
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00005617 File Offset: 0x00003817
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000561F File Offset: 0x0000381F
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00005628 File Offset: 0x00003828
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00005630 File Offset: 0x00003830
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueInput dictionaryInput { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00005639 File Offset: 0x00003839
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00005641 File Offset: 0x00003841
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueOutput dictionaryOutput { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000564A File Offset: 0x0000384A
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005652 File Offset: 0x00003852
		[DoNotSerialize]
		public ValueInput key { get; private set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000565B File Offset: 0x0000385B
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005663 File Offset: 0x00003863
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x0600017B RID: 379 RVA: 0x0000566C File Offset: 0x0000386C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Remove));
			this.dictionaryInput = base.ValueInput<IDictionary>("dictionaryInput");
			this.dictionaryOutput = base.ValueOutput<IDictionary>("dictionaryOutput");
			this.key = base.ValueInput<object>("key");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.dictionaryInput, this.enter);
			base.Requirement(this.key, this.enter);
			base.Assignment(this.enter, this.dictionaryOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005724 File Offset: 0x00003924
		public ControlOutput Remove(Flow flow)
		{
			IDictionary value = flow.GetValue<IDictionary>(this.dictionaryInput);
			object value2 = flow.GetValue<object>(this.key);
			flow.SetValue(this.dictionaryOutput, value);
			value.Remove(value2);
			return this.exit;
		}
	}
}
