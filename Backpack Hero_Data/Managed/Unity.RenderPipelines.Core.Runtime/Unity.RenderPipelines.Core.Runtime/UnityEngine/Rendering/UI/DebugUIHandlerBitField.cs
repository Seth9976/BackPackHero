using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000EB RID: 235
	public class DebugUIHandlerBitField : DebugUIHandlerWidget
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001EC7C File Offset: 0x0001CE7C
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.BitField>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			int i = 0;
			foreach (GUIContent guicontent in this.m_Field.enumNames)
			{
				if (i < this.toggles.Count)
				{
					DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = this.toggles[i];
					debugUIHandlerIndirectToggle.getter = new Func<int, bool>(this.GetValue);
					debugUIHandlerIndirectToggle.setter = new Action<int, bool>(this.SetValue);
					debugUIHandlerIndirectToggle.nextUIHandler = ((i < this.m_Field.enumNames.Length - 1) ? this.toggles[i + 1] : null);
					debugUIHandlerIndirectToggle.previousUIHandler = ((i > 0) ? this.toggles[i - 1] : null);
					debugUIHandlerIndirectToggle.parentUIHandler = this;
					debugUIHandlerIndirectToggle.index = i;
					debugUIHandlerIndirectToggle.nameLabel.text = guicontent.text;
					debugUIHandlerIndirectToggle.Init();
					i++;
				}
			}
			while (i < this.toggles.Count)
			{
				CoreUtils.Destroy(this.toggles[i].gameObject);
				this.toggles[i] = null;
				i++;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001EDC8 File Offset: 0x0001CFC8
		private bool GetValue(int index)
		{
			if (index == 0)
			{
				return false;
			}
			index--;
			return (Convert.ToInt32(this.m_Field.GetValue()) & (1 << index)) != 0;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private void SetValue(int index, bool value)
		{
			if (index == 0)
			{
				this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, 0));
				using (List<DebugUIHandlerIndirectToggle>.Enumerator enumerator = this.toggles.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DebugUIHandlerIndirectToggle debugUIHandlerIndirectToggle = enumerator.Current;
						if (debugUIHandlerIndirectToggle != null && debugUIHandlerIndirectToggle.getter != null)
						{
							debugUIHandlerIndirectToggle.UpdateValueLabel();
						}
					}
					return;
				}
			}
			int num = Convert.ToInt32(this.m_Field.GetValue());
			if (value)
			{
				num |= this.m_Field.enumValues[index];
			}
			else
			{
				num &= ~this.m_Field.enumValues[index];
			}
			this.m_Field.SetValue(Enum.ToObject(this.m_Field.enumType, num));
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (fromNext || !this.valueToggle.isOn)
			{
				this.nameLabel.color = this.colorSelected;
			}
			else if (this.valueToggle.isOn)
			{
				if (this.m_Container.IsDirectChild(previous))
				{
					this.nameLabel.color = this.colorSelected;
				}
				else
				{
					DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
					DebugManager.instance.ChangeSelection(lastItem, false);
				}
			}
			return true;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001EF37 File Offset: 0x0001D137
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001EF4A File Offset: 0x0001D14A
		public override void OnIncrement(bool fast)
		{
			this.valueToggle.isOn = true;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001EF58 File Offset: 0x0001D158
		public override void OnDecrement(bool fast)
		{
			this.valueToggle.isOn = false;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001EF66 File Offset: 0x0001D166
		public override void OnAction()
		{
			this.valueToggle.isOn = !this.valueToggle.isOn;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001EF84 File Offset: 0x0001D184
		public override DebugUIHandlerWidget Next()
		{
			if (!this.valueToggle.isOn || this.m_Container == null)
			{
				return base.Next();
			}
			DebugUIHandlerWidget firstItem = this.m_Container.GetFirstItem();
			if (firstItem == null)
			{
				return base.Next();
			}
			return firstItem;
		}

		// Token: 0x040003CE RID: 974
		public Text nameLabel;

		// Token: 0x040003CF RID: 975
		public UIFoldout valueToggle;

		// Token: 0x040003D0 RID: 976
		public List<DebugUIHandlerIndirectToggle> toggles;

		// Token: 0x040003D1 RID: 977
		private DebugUI.BitField m_Field;

		// Token: 0x040003D2 RID: 978
		private DebugUIHandlerContainer m_Container;
	}
}
