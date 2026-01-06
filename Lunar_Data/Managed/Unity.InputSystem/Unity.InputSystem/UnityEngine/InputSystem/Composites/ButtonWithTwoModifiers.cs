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
		// Token: 0x060011CC RID: 4556 RVA: 0x00053F88 File Offset: 0x00052188
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00053FA8 File Offset: 0x000521A8
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

		// Token: 0x060011CE RID: 4558 RVA: 0x00054012 File Offset: 0x00052212
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0005401B File Offset: 0x0005221B
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x040006F4 RID: 1780
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x040006F5 RID: 1781
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x040006F6 RID: 1782
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x040006F7 RID: 1783
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
