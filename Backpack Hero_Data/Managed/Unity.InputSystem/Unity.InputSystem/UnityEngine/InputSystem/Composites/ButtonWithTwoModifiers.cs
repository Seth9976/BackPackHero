using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200014A RID: 330
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier1}+{modifier2}+{button}")]
	public class ButtonWithTwoModifiers : InputBindingComposite<float>
	{
		// Token: 0x060011D3 RID: 4563 RVA: 0x0005419C File Offset: 0x0005239C
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000541BC File Offset: 0x000523BC
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (flag && !this.overrideModifiersNeedToBePressedFirst)
			{
				double pressTime = context.GetPressTime(this.button);
				double pressTime2 = context.GetPressTime(this.modifier1);
				double pressTime3 = context.GetPressTime(this.modifier2);
				return pressTime2 <= pressTime && pressTime3 <= pressTime;
			}
			return flag;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00054226 File Offset: 0x00052426
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0005422F File Offset: 0x0005242F
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x040006F5 RID: 1781
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x040006F6 RID: 1782
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x040006F7 RID: 1783
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x040006F8 RID: 1784
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
