using System;
using System.ComponentModel;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Composites
{
	// Token: 0x0200014B RID: 331
	[DisplayStringFormat("{modifier}+{binding}")]
	[DisplayName("Binding With One Modifier")]
	public class OneModifierComposite : InputBindingComposite
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060011D8 RID: 4568 RVA: 0x00054254 File Offset: 0x00052454
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0005425C File Offset: 0x0005245C
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00054264 File Offset: 0x00052464
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00054281 File Offset: 0x00052481
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifierIsPressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000542A8 File Offset: 0x000524A8
		private bool ModifierIsPressed(ref InputBindingCompositeContext context)
		{
			bool flag = context.ReadValueAsButton(this.modifier);
			if (flag && this.m_BindingIsButton && !this.overrideModifiersNeedToBePressedFirst)
			{
				double pressTime = context.GetPressTime(this.binding);
				return context.GetPressTime(this.modifier) <= pressTime;
			}
			return flag;
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000542F6 File Offset: 0x000524F6
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00054331 File Offset: 0x00052531
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00054350 File Offset: 0x00052550
		internal static void DetermineValueTypeAndSize(ref InputBindingCompositeContext context, int part, out Type valueType, out int valueSizeInBytes, out bool isButton)
		{
			valueSizeInBytes = 0;
			isButton = true;
			Type type = null;
			foreach (InputBindingCompositeContext.PartBinding partBinding in context.controls)
			{
				if (partBinding.part == part)
				{
					Type valueType2 = partBinding.control.valueType;
					if (type == null || valueType2.IsAssignableFrom(type))
					{
						type = valueType2;
					}
					else if (!type.IsAssignableFrom(valueType2))
					{
						type = typeof(Object);
					}
					valueSizeInBytes = Math.Max(partBinding.control.valueSizeInBytes, valueSizeInBytes);
					isButton &= partBinding.control.isButton;
				}
			}
			valueType = type;
		}

		// Token: 0x040006F9 RID: 1785
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x040006FA RID: 1786
		[InputControl]
		public int binding;

		// Token: 0x040006FB RID: 1787
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x040006FC RID: 1788
		private int m_ValueSizeInBytes;

		// Token: 0x040006FD RID: 1789
		private Type m_ValueType;

		// Token: 0x040006FE RID: 1790
		private bool m_BindingIsButton;
	}
}
