using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.VisualScripting.InputSystem
{
	// Token: 0x02000184 RID: 388
	[UnitCategory("Events/Input")]
	public abstract class OnInputSystemEvent : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00012D01 File Offset: 0x00010F01
		protected override string hookName
		{
			get
			{
				if (InputSystem.settings.updateMode != InputSettings.UpdateMode.ProcessEventsInDynamicUpdate)
				{
					return "FixedUpdate";
				}
				return "Update";
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000A5C RID: 2652
		protected abstract OutputType OutputType { get; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00012D1B File Offset: 0x00010F1B
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00012D23 File Offset: 0x00010F23
		[DoNotSerialize]
		public ValueInput InputAction { get; private set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00012D2C File Offset: 0x00010F2C
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00012D34 File Offset: 0x00010F34
		[DoNotSerialize]
		[PortLabelHidden]
		[NullMeansSelf]
		public ValueInput Target { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00012D3D File Offset: 0x00010F3D
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00012D45 File Offset: 0x00010F45
		[PortLabelHidden]
		public ValueOutput FloatValue { get; private set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00012D4E File Offset: 0x00010F4E
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00012D56 File Offset: 0x00010F56
		[PortLabelHidden]
		public ValueOutput Vector2Value { get; private set; }

		// Token: 0x06000A65 RID: 2661 RVA: 0x00012D60 File Offset: 0x00010F60
		protected override void Definition()
		{
			base.Definition();
			this.Target = base.ValueInput(typeof(PlayerInput), "Target");
			this.Target.SetDefaultValue(null);
			this.Target.NullMeansSelf();
			this.InputAction = base.ValueInput(typeof(InputAction), "InputAction");
			this.InputAction.SetDefaultValue(null);
			switch (this.OutputType)
			{
			case OutputType.Button:
				return;
			case OutputType.Float:
				this.FloatValue = base.ValueOutput<float>("FloatValue", (Flow _) => this.m_Value.x);
				return;
			case OutputType.Vector2:
				this.Vector2Value = base.ValueOutput<Vector2>("Vector2Value", (Flow _) => this.m_Value);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00012E2C File Offset: 0x0001102C
		public override void StartListening(GraphStack stack)
		{
			base.StartListening(stack);
			GraphReference graphReference = stack.ToReference();
			PlayerInput playerInput = Flow.FetchValue<PlayerInput>(this.Target, graphReference);
			InputAction inputAction = Flow.FetchValue<InputAction>(this.InputAction, graphReference);
			if (inputAction == null)
			{
				return;
			}
			this.m_Action = (playerInput ? playerInput.actions.FindAction(inputAction.id) : ((inputAction.actionMap != null) ? inputAction : null));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00012E92 File Offset: 0x00011092
		public override void StopListening(GraphStack stack)
		{
			base.StopListening(stack);
			this.m_Action = null;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00012EA4 File Offset: 0x000110A4
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			if (this.m_Action == null)
			{
				return false;
			}
			bool flag;
			switch (this.InputActionChangeType)
			{
			case InputActionChangeOption.OnPressed:
				flag = this.m_Action.WasPressedThisFrame();
				break;
			case InputActionChangeOption.OnHold:
				flag = this.m_Action.IsPressed();
				break;
			case InputActionChangeOption.OnReleased:
				flag = this.m_Action.WasReleasedThisFrame();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			this.DoAssignArguments(flow);
			return flag;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00012F10 File Offset: 0x00011110
		private void DoAssignArguments(Flow flow)
		{
			switch (this.OutputType)
			{
			case OutputType.Button:
				return;
			case OutputType.Float:
			{
				float num = this.m_Action.ReadValue<float>();
				this.m_Value.Set(num, 0f);
				flow.SetValue(this.FloatValue, num);
				return;
			}
			case OutputType.Vector2:
			{
				Vector2 vector = this.m_Action.ReadValue<Vector2>();
				this.m_Value = vector;
				flow.SetValue(this.Vector2Value, vector);
				return;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x04000230 RID: 560
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable]
		public InputActionChangeOption InputActionChangeType;

		// Token: 0x04000235 RID: 565
		private InputAction m_Action;

		// Token: 0x04000236 RID: 566
		private Vector2 m_Value;
	}
}
