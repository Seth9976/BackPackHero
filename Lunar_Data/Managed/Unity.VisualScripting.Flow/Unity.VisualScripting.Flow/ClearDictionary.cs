using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000021 RID: 33
	[UnitCategory("Collections/Dictionaries")]
	[UnitSurtitle("Dictionary")]
	[UnitShortTitle("Clear")]
	[UnitOrder(4)]
	[TypeIcon(typeof(RemoveDictionaryItem))]
	public sealed class ClearDictionary : Unit
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005217 File Offset: 0x00003417
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000521F File Offset: 0x0000341F
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005228 File Offset: 0x00003428
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005230 File Offset: 0x00003430
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueInput dictionaryInput { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005239 File Offset: 0x00003439
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005241 File Offset: 0x00003441
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueOutput dictionaryOutput { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000524A File Offset: 0x0000344A
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005252 File Offset: 0x00003452
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x06000152 RID: 338 RVA: 0x0000525C File Offset: 0x0000345C
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Clear));
			this.dictionaryInput = base.ValueInput<IDictionary>("dictionaryInput");
			this.dictionaryOutput = base.ValueOutput<IDictionary>("dictionaryOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.dictionaryInput, this.enter);
			base.Assignment(this.enter, this.dictionaryOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000052F0 File Offset: 0x000034F0
		private ControlOutput Clear(Flow flow)
		{
			IDictionary value = flow.GetValue<IDictionary>(this.dictionaryInput);
			flow.SetValue(this.dictionaryOutput, value);
			value.Clear();
			return this.exit;
		}
	}
}
