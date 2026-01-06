using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F4 RID: 244
	public class DebugUIHandlerFoldout : DebugUIHandlerWidget
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00020448 File Offset: 0x0001E648
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Foldout>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			this.nameLabel.text = this.m_Field.displayName;
			string[] columnLabels = this.m_Field.columnLabels;
			int num = ((columnLabels != null) ? columnLabels.Length : 0);
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.nameLabel.gameObject, base.GetComponent<DebugUIHandlerContainer>().contentHolder);
				gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
				RectTransform rectTransform = gameObject.transform as RectTransform;
				RectTransform rectTransform2 = this.nameLabel.transform as RectTransform;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform.anchorMin = vector;
				rectTransform.anchorMax = vector;
				rectTransform.sizeDelta = new Vector2(100f, 26f);
				Vector3 vector2 = rectTransform2.anchoredPosition;
				vector2.x += (float)(i + 1) * 60f + 230f;
				rectTransform.anchoredPosition = vector2;
				rectTransform.pivot = new Vector2(0f, 0.5f);
				rectTransform.eulerAngles = new Vector3(0f, 0f, 13f);
				Text component = gameObject.GetComponent<Text>();
				component.fontSize = 15;
				component.text = this.m_Field.columnLabels[i];
			}
			this.UpdateValue();
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x000205B0 File Offset: 0x0001E7B0
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

		// Token: 0x06000736 RID: 1846 RVA: 0x00020627 File Offset: 0x0001E827
		public override void OnDeselection()
		{
			this.nameLabel.color = this.colorDefault;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0002063A File Offset: 0x0001E83A
		public override void OnIncrement(bool fast)
		{
			this.m_Field.SetValue(true);
			this.UpdateValue();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0002064E File Offset: 0x0001E84E
		public override void OnDecrement(bool fast)
		{
			this.m_Field.SetValue(false);
			this.UpdateValue();
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00020664 File Offset: 0x0001E864
		public override void OnAction()
		{
			bool flag = !this.m_Field.GetValue();
			this.m_Field.SetValue(flag);
			this.UpdateValue();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00020692 File Offset: 0x0001E892
		private void UpdateValue()
		{
			this.valueToggle.isOn = this.m_Field.GetValue();
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000206AC File Offset: 0x0001E8AC
		public override DebugUIHandlerWidget Next()
		{
			if (!this.m_Field.GetValue() || this.m_Container == null)
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

		// Token: 0x040003F3 RID: 1011
		public Text nameLabel;

		// Token: 0x040003F4 RID: 1012
		public UIFoldout valueToggle;

		// Token: 0x040003F5 RID: 1013
		private DebugUI.Foldout m_Field;

		// Token: 0x040003F6 RID: 1014
		private DebugUIHandlerContainer m_Container;

		// Token: 0x040003F7 RID: 1015
		private const float xDecal = 60f;

		// Token: 0x040003F8 RID: 1016
		private const float xDecalInit = 230f;
	}
}
