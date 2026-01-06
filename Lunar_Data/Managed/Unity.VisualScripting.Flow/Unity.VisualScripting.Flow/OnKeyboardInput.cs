using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200007E RID: 126
	[UnitCategory("Events/Input")]
	public sealed class OnKeyboardInput : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000943E File Offset: 0x0000763E
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00009445 File Offset: 0x00007645
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000944D File Offset: 0x0000764D
		[DoNotSerialize]
		public ValueInput key { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00009456 File Offset: 0x00007656
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000945E File Offset: 0x0000765E
		[DoNotSerialize]
		public ValueInput action { get; private set; }

		// Token: 0x0600040F RID: 1039 RVA: 0x00009467 File Offset: 0x00007667
		protected override void Definition()
		{
			base.Definition();
			this.key = base.ValueInput<KeyCode>("key", KeyCode.Space);
			this.action = base.ValueInput<PressState>("action", PressState.Down);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00009494 File Offset: 0x00007694
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			KeyCode value = flow.GetValue<KeyCode>(this.key);
			PressState value2 = flow.GetValue<PressState>(this.action);
			switch (value2)
			{
			case PressState.Hold:
				return Input.GetKey(value);
			case PressState.Down:
				return Input.GetKeyDown(value);
			case PressState.Up:
				return Input.GetKeyUp(value);
			default:
				throw new UnexpectedEnumValueException<PressState>(value2);
			}
		}
	}
}
