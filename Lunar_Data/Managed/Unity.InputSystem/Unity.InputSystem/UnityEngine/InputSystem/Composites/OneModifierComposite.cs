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
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x00054040 File Offset: 0x00052240
		public override Type valueType
		{
			get
			{
				return this.m_ValueType;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00054048 File Offset: 0x00052248
		public override int valueSizeInBytes
		{
			get
			{
				return this.m_ValueSizeInBytes;
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00054050 File Offset: 0x00052250
		public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			if (this.ModifierIsPressed(ref context))
			{
				return context.EvaluateMagnitude(this.binding);
			}
			return 0f;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0005406D File Offset: 0x0005226D
		public unsafe override void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize)
		{
			if (this.ModifierIsPressed(ref context))
			{
				context.ReadValue(this.binding, buffer, bufferSize);
				return;
			}
			UnsafeUtility.MemClear(buffer, (long)this.m_ValueSizeInBytes);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00054094 File Offset: 0x00052294
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

		// Token: 0x060011D6 RID: 4566 RVA: 0x000540E2 File Offset: 0x000522E2
		protected override void FinishSetup(ref InputBindingCompositeContext context)
		{
			OneModifierComposite.DetermineValueTypeAndSize(ref context, this.binding, out this.m_ValueType, out this.m_ValueSizeInBytes, out this.m_BindingIsButton);
			if (!this.overrideModifiersNeedToBePressedFirst)
			{
				this.overrideModifiersNeedToBePressedFirst = !InputSystem.settings.shortcutKeysConsumeInput;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0005411D File Offset: 0x0005231D
		public override object ReadValueAsObject(ref InputBindingCompositeContext context)
		{
			if (context.ReadValueAsButton(this.modifier))
			{
				return context.ReadValueAsObject(this.binding);
			}
			return null;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0005413C File Offset: 0x0005233C
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

		// Token: 0x040006F8 RID: 1784
		[InputControl(layout = "Button")]
		public int modifier;

		// Token: 0x040006F9 RID: 1785
		[InputControl]
		public int binding;

		// Token: 0x040006FA RID: 1786
		public bool overrideModifiersNeedToBePressedFirst;

		// Token: 0x040006FB RID: 1787
		private int m_ValueSizeInBytes;

		// Token: 0x040006FC RID: 1788
		private Type m_ValueType;

		// Token: 0x040006FD RID: 1789
		private bool m_BindingIsButton;
	}
}
