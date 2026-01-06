using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200014C RID: 332
	[DisplayStringFormat("{modifier1}+{modifier2}+{binding}")]
	[DisplayName("Binding With Two Modifiers")]
	public class TwoModifiersComposite : InputBindingComposite
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x00054200 File Offset: 0x00052400
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00054208 File Offset: 0x00052408
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00054210 File Offset: 0x00052410
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0005422D File Offset: 0x0005242D
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifiersArePressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00054254 File Offset: 0x00052454
		private bool ModifiersArePressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2);
			if (flag && this.m_BindingIsButton && !this.overrideModifiersNeedToBePressedFirst)
			{
				double pressTime = context.GetPressTime(this.binding);
				double pressTime2 = context.GetPressTime(this.modifier1);
				double pressTime3 = context.GetPressTime(this.modifier2);
				return pressTime2 <= pressTime && pressTime3 <= pressTime;
			}
			return flag;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000542C6 File Offset: 0x000524C6
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00054301 File Offset: 0x00052501
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x040006FE RID: 1790
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x040006FF RID: 1791
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000700 RID: 1792
		[InputControl]
		public int binding;

		// Token: 0x04000701 RID: 1793
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000702 RID: 1794
		private int m_ValueSizeInBytes;

		// Token: 0x04000703 RID: 1795
		private Type m_ValueType;

		// Token: 0x04000704 RID: 1796
		private bool m_BindingIsButton;
	}
}
