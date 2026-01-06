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
		// Token: 0x060011C7 RID: 4551 RVA: 0x00053EF7 File Offset: 0x000520F7
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.ReadValue<float>(this.button);
			}
			return 0f;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00053F14 File Offset: 0x00052114
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

		// Token: 0x060011C9 RID: 4553 RVA: 0x00053F5A File Offset: 0x0005215A
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return this.ReadValue(ref context);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00053F63 File Offset: 0x00052163
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x040006F1 RID: 1777
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x040006F2 RID: 1778
		[InputControl(layout = "Button")]
		public int button;

		// Token: 0x040006F3 RID: 1779
		public bool overrideModifiersNeedToBePressedFirst;
	}
}
