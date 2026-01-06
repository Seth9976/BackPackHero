using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200007D RID: 125
	[UnitCategory("Events/Input")]
	public sealed class OnButtonInput : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00009387 File Offset: 0x00007587
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000938E File Offset: 0x0000758E
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00009396 File Offset: 0x00007596
		[DoNotSerialize]
		[PortLabel("Name")]
		public ValueInput buttonName { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000939F File Offset: 0x0000759F
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x000093A7 File Offset: 0x000075A7
		[DoNotSerialize]
		public ValueInput action { get; private set; }

		// Token: 0x06000407 RID: 1031 RVA: 0x000093B0 File Offset: 0x000075B0
		protected override void Definition()
		{
			base.Definition();
			this.buttonName = base.ValueInput<string>("buttonName", string.Empty);
			this.action = base.ValueInput<PressState>("action", PressState.Down);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000093E0 File Offset: 0x000075E0
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			string value = flow.GetValue<string>(this.buttonName);
			PressState value2 = flow.GetValue<PressState>(this.action);
			switch (value2)
			{
			case PressState.Hold:
				return Input.GetButton(value);
			case PressState.Down:
				return Input.GetButtonDown(value);
			case PressState.Up:
				return Input.GetButtonUp(value);
			default:
				throw new UnexpectedEnumValueException<PressState>(value2);
			}
		}
	}
}
