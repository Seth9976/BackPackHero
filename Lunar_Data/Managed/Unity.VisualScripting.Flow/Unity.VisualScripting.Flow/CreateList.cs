using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200002C RID: 44
	[UnitCategory("Collections/Lists")]
	[UnitOrder(-1)]
	[TypeIcon(typeof(IList))]
	public sealed class CreateList : MultiInputUnit<object>
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00005CFE File Offset: 0x00003EFE
		[DoNotSerialize]
		protected override int minInputCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005D01 File Offset: 0x00003F01
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00005D09 File Offset: 0x00003F09
		[InspectorLabel("Elements")]
		[UnitHeaderInspectable("Elements")]
		[Inspectable]
		public override int inputCount
		{
			get
			{
				return base.inputCount;
			}
			set
			{
				base.inputCount = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00005D12 File Offset: 0x00003F12
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00005D1A File Offset: 0x00003F1A
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput list { get; private set; }

		// Token: 0x060001B6 RID: 438 RVA: 0x00005D24 File Offset: 0x00003F24
		protected override void Definition()
		{
			this.list = base.ValueOutput<IList>("list", new Func<Flow, IList>(this.Create));
			base.Definition();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.list);
			}
			base.InputsAllowNull();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public IList Create(Flow flow)
		{
			AotList aotList = new AotList();
			for (int i = 0; i < this.inputCount; i++)
			{
				aotList.Add(flow.GetValue<object>(base.multiInputs[i]));
			}
			return aotList;
		}
	}
}
