using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000020 RID: 32
	[UnitCategory("Collections/Dictionaries")]
	[UnitSurtitle("Dictionary")]
	[UnitShortTitle("Add Item")]
	[UnitOrder(2)]
	public sealed class AddDictionaryItem : Unit
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000507D File Offset: 0x0000327D
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005085 File Offset: 0x00003285
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000508E File Offset: 0x0000328E
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005096 File Offset: 0x00003296
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueInput dictionaryInput { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000509F File Offset: 0x0000329F
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000050A7 File Offset: 0x000032A7
		[DoNotSerialize]
		[PortLabel("Dictionary")]
		[PortLabelHidden]
		public ValueOutput dictionaryOutput { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000050B0 File Offset: 0x000032B0
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000050B8 File Offset: 0x000032B8
		[DoNotSerialize]
		public ValueInput key { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000050C1 File Offset: 0x000032C1
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000050C9 File Offset: 0x000032C9
		[DoNotSerialize]
		public ValueInput value { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000050D2 File Offset: 0x000032D2
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000050DA File Offset: 0x000032DA
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x06000147 RID: 327 RVA: 0x000050E4 File Offset: 0x000032E4
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Add));
			this.dictionaryInput = base.ValueInput<IDictionary>("dictionaryInput");
			this.key = base.ValueInput<object>("key");
			this.value = base.ValueInput<object>("value");
			this.dictionaryOutput = base.ValueOutput<IDictionary>("dictionaryOutput");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.dictionaryInput, this.enter);
			base.Requirement(this.key, this.enter);
			base.Requirement(this.value, this.enter);
			base.Assignment(this.enter, this.dictionaryOutput);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000051C0 File Offset: 0x000033C0
		private ControlOutput Add(Flow flow)
		{
			IDictionary value = flow.GetValue<IDictionary>(this.dictionaryInput);
			object value2 = flow.GetValue<object>(this.key);
			object value3 = flow.GetValue<object>(this.value);
			flow.SetValue(this.dictionaryOutput, value);
			value.Add(value2, value3);
			return this.exit;
		}
	}
}
