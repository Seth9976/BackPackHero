using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F1 RID: 241
	public class DebugUIHandlerEnumField : DebugUIHandlerWidget
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0001FE84 File Offset: 0x0001E084
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.EnumField>();
			this.nameLabel.text = this.m_Field.displayName;
			this.UpdateValueLabel();
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorSelected;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorSelected;
			}
			this.nameLabel.color = this.colorSelected;
			this.valueLabel.color = this.colorSelected;
			return true;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001FF28 File Offset: 0x0001E128
		public override void OnDeselection()
		{
			if (this.nextButtonText != null)
			{
				this.nextButtonText.color = this.colorDefault;
			}
			if (this.previousButtonText != null)
			{
				this.previousButtonText.color = this.colorDefault;
			}
			this.nameLabel.color = this.colorDefault;
			this.valueLabel.color = this.colorDefault;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001FF95 File Offset: 0x0001E195
		public override void OnAction()
		{
			this.OnIncrement(false);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public override void OnIncrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == enumValues.Length - 1)
			{
				num = 0;
			}
			else if (fast)
			{
				int[] array = this.m_Field.quickSeparators;
				if (array == null)
				{
					this.m_Field.InitQuickSeparators();
					array = this.m_Field.quickSeparators;
				}
				int num2 = 0;
				while (num2 < array.Length && num + 1 > array[num2])
				{
					num2++;
				}
				if (num2 == array.Length)
				{
					num = 0;
				}
				else
				{
					num = array[num2];
				}
			}
			else
			{
				num++;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00020054 File Offset: 0x0001E254
		public override void OnDecrement(bool fast)
		{
			if (this.m_Field.enumValues.Length == 0)
			{
				return;
			}
			int[] enumValues = this.m_Field.enumValues;
			int num = this.m_Field.currentIndex;
			if (num == 0)
			{
				if (fast)
				{
					int[] array = this.m_Field.quickSeparators;
					if (array == null)
					{
						this.m_Field.InitQuickSeparators();
						array = this.m_Field.quickSeparators;
					}
					num = array[array.Length - 1];
				}
				else
				{
					num = enumValues.Length - 1;
				}
			}
			else if (fast)
			{
				int[] array2 = this.m_Field.quickSeparators;
				if (array2 == null)
				{
					this.m_Field.InitQuickSeparators();
					array2 = this.m_Field.quickSeparators;
				}
				int num2 = array2.Length - 1;
				while (num2 > 0 && num <= array2[num2])
				{
					num2--;
				}
				num = array2[num2];
			}
			else
			{
				num--;
			}
			this.m_Field.SetValue(enumValues[num]);
			this.m_Field.currentIndex = num;
			this.UpdateValueLabel();
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00020138 File Offset: 0x0001E338
		protected virtual void UpdateValueLabel()
		{
			int num = this.m_Field.currentIndex;
			if (num < 0)
			{
				num = 0;
			}
			string text = this.m_Field.enumNames[num].text;
			if (text.Length > 26)
			{
				text = text.Substring(0, 23) + "...";
			}
			this.valueLabel.text = text;
		}

		// Token: 0x040003E9 RID: 1001
		public Text nextButtonText;

		// Token: 0x040003EA RID: 1002
		public Text previousButtonText;

		// Token: 0x040003EB RID: 1003
		public Text nameLabel;

		// Token: 0x040003EC RID: 1004
		public Text valueLabel;

		// Token: 0x040003ED RID: 1005
		protected internal DebugUI.EnumField m_Field;
	}
}
