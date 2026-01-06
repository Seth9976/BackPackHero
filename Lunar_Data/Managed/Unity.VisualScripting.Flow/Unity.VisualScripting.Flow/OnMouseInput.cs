using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000083 RID: 131
	[UnitCategory("Events/Input")]
	public sealed class OnMouseInput : MachineEventUnit<EmptyEventArgs>, IMouseEventUnit
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000955E File Offset: 0x0000775E
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00009565 File Offset: 0x00007765
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000956D File Offset: 0x0000776D
		[DoNotSerialize]
		public ValueInput button { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00009576 File Offset: 0x00007776
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000957E File Offset: 0x0000777E
		[DoNotSerialize]
		public ValueInput action { get; private set; }

		// Token: 0x06000423 RID: 1059 RVA: 0x00009587 File Offset: 0x00007787
		protected override void Definition()
		{
			base.Definition();
			this.button = base.ValueInput<MouseButton>("button", MouseButton.Left);
			this.action = base.ValueInput<PressState>("action", PressState.Down);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000095B4 File Offset: 0x000077B4
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			int value = (int)flow.GetValue<MouseButton>(this.button);
			PressState value2 = flow.GetValue<PressState>(this.action);
			switch (value2)
			{
			case PressState.Hold:
				return Input.GetMouseButton(value);
			case PressState.Down:
				return Input.GetMouseButtonDown(value);
			case PressState.Up:
				return Input.GetMouseButtonUp(value);
			default:
				throw new UnexpectedEnumValueException<PressState>(value2);
			}
		}
	}
}
