using System;
using UnityEngine.InputSystem;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006A RID: 106
	internal class DebugActionState
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00010200 File Offset: 0x0000E400
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00010208 File Offset: 0x0000E408
		internal bool runningAction { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00010211 File Offset: 0x0000E411
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00010219 File Offset: 0x0000E419
		internal float actionState { get; private set; }

		// Token: 0x0600036D RID: 877 RVA: 0x00010224 File Offset: 0x0000E424
		private void Trigger(int triggerCount, float state)
		{
			this.actionState = state;
			this.runningAction = true;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = new bool[triggerCount];
			for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
			{
				this.m_TriggerPressedUp[i] = false;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00010274 File Offset: 0x0000E474
		public void TriggerWithButton(InputAction action, float state)
		{
			this.inputAction = action;
			this.Trigger(action.bindings.Count, state);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001029D File Offset: 0x0000E49D
		private void Reset()
		{
			this.runningAction = false;
			this.m_Timer = 0f;
			this.m_TriggerPressedUp = null;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public void Update(DebugActionDesc desc)
		{
			this.actionState = 0f;
			if (this.m_TriggerPressedUp != null)
			{
				this.m_Timer += Time.deltaTime;
				for (int i = 0; i < this.m_TriggerPressedUp.Length; i++)
				{
					if (this.inputAction != null)
					{
						this.m_TriggerPressedUp[i] |= Mathf.Approximately(this.inputAction.ReadValue<float>(), 0f);
					}
				}
				bool flag = true;
				foreach (bool flag2 in this.m_TriggerPressedUp)
				{
					flag = flag && flag2;
				}
				if (flag || (this.m_Timer > desc.repeatDelay && desc.repeatMode == DebugActionRepeatMode.Delay))
				{
					this.Reset();
				}
			}
		}

		// Token: 0x04000236 RID: 566
		private DebugActionState.DebugActionKeyType m_Type;

		// Token: 0x04000237 RID: 567
		private InputAction inputAction;

		// Token: 0x04000238 RID: 568
		private bool[] m_TriggerPressedUp;

		// Token: 0x04000239 RID: 569
		private float m_Timer;

		// Token: 0x02000145 RID: 325
		private enum DebugActionKeyType
		{
			// Token: 0x0400050E RID: 1294
			Button,
			// Token: 0x0400050F RID: 1295
			Axis,
			// Token: 0x04000510 RID: 1296
			Key
		}
	}
}
