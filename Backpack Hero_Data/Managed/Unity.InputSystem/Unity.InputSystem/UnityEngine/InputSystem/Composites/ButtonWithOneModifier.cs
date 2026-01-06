using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x02000149 RID: 329
	[DesignTimeVisible(false)]
	[DisplayStringFormat("{modifier}+{button}")]
	public class ButtonWithOneModifier : InputBindingComposite<float>
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x0005410B File Offset: 0x0005230B
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00054128 File Offset: 0x00052328
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier);
			if (flag && !this.overrideModifiersNeedToBePressedFirst)
			{
				double pressTime = context.GetPressTime(this.button);
				return context.GetPressTime(this.modifier) <= pressTime;
			}
			return flag;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0005416E File Offset: 0x0005236E
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00054177 File Offset: 0x00052377
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x040006F2 RID: 1778
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x040006F3 RID: 1779
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x040006F4 RID: 1780
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
