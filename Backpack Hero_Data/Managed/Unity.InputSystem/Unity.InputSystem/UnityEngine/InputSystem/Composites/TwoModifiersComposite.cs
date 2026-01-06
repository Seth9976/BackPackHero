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
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x00054414 File Offset: 0x00052614
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0005441C File Offset: 0x0005261C
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00054424 File Offset: 0x00052624
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifiersArePressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00054441 File Offset: 0x00052641
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifiersArePressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00054468 File Offset: 0x00052668
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

		// Token: 0x060011E6 RID: 4582 RVA: 0x000544DA File Offset: 0x000526DA
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00054515 File Offset: 0x00052715
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier1) && context.ReadValueAsButton(this.modifier2))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x040006FF RID: 1791
		[InputControl(layout = "Button")]
		public int modifier1;

		// Token: 0x04000700 RID: 1792
		[InputControl(layout = "Button")]
		public int modifier2;

		// Token: 0x04000701 RID: 1793
		[InputControl]
		public int binding;

		// Token: 0x04000702 RID: 1794
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x04000703 RID: 1795
		private int m_ValueSizeInBytes;

		// Token: 0x04000704 RID: 1796
		private Type m_ValueType;

		// Token: 0x04000705 RID: 1797
		private bool m_BindingIsButton;
	}
}
