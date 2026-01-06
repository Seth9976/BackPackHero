using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FB RID: 251
	public class DebugUIHandlerPanel : MonoBehaviour
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00020CA1 File Offset: 0x0001EEA1
		private void OnEnable()
		{
			this.m_ScrollTransform = this.scrollRect.GetComponent<RectTransform>();
			this.m_ContentTransform = base.GetComponent<DebugUIHandlerContainer>().contentHolder;
			this.m_MaskTransform = base.GetComponentInChildren<Mask>(true).rectTransform;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00020CD7 File Offset: 0x0001EED7
		internal void SetPanel(DebugUI.Panel panel)
		{
			this.m_Panel = panel;
			this.nameLabel.text = panel.displayName;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x00020CF1 File Offset: 0x0001EEF1
		internal DebugUI.Panel GetPanel()
		{
			return this.m_Panel;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00020CF9 File Offset: 0x0001EEF9
		public void SelectNextItem()
		{
			this.Canvas.SelectNextPanel();
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00020D06 File Offset: 0x0001EF06
		public void SelectPreviousItem()
		{
			this.Canvas.SelectPreviousPanel();
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00020D13 File Offset: 0x0001EF13
		public void OnScrollbarClicked()
		{
			DebugManager.instance.SetScrollTarget(null);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00020D20 File Offset: 0x0001EF20
		internal void SetScrollTarget(DebugUIHandlerWidget target)
		{
			this.m_ScrollTarget = target;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00020D2C File Offset: 0x0001EF2C
		internal void UpdateScroll()
		{
			if (this.m_ScrollTarget == null)
			{
				return;
			}
			RectTransform component = this.m_ScrollTarget.GetComponent<RectTransform>();
			float yposInScroll = this.GetYPosInScroll(component);
			float num = (this.GetYPosInScroll(this.m_MaskTransform) - yposInScroll) / (this.m_ContentTransform.rect.size.y - this.m_ScrollTransform.rect.size.y);
			float num2 = this.scrollRect.verticalNormalizedPosition - num;
			num2 = Mathf.Clamp01(num2);
			this.scrollRect.verticalNormalizedPosition = Mathf.Lerp(this.scrollRect.verticalNormalizedPosition, num2, Time.deltaTime * 10f);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00020DDC File Offset: 0x0001EFDC
		private float GetYPosInScroll(RectTransform target)
		{
			Vector3 vector = new Vector3((0.5f - target.pivot.x) * target.rect.size.x, (0.5f - target.pivot.y) * target.rect.size.y, 0f);
			Vector3 vector2 = target.localPosition + vector;
			Vector3 vector3 = target.parent.TransformPoint(vector2);
			return this.m_ScrollTransform.TransformPoint(vector3).y;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00020E6A File Offset: 0x0001F06A
		internal DebugUIHandlerWidget GetFirstItem()
		{
			return base.GetComponent<DebugUIHandlerContainer>().GetFirstItem();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00020E77 File Offset: 0x0001F077
		public void ResetDebugManager()
		{
			DebugManager.instance.Reset();
		}

		// Token: 0x04000414 RID: 1044
		public Text nameLabel;

		// Token: 0x04000415 RID: 1045
		public ScrollRect scrollRect;

		// Token: 0x04000416 RID: 1046
		public RectTransform viewport;

		// Token: 0x04000417 RID: 1047
		public DebugUIHandlerCanvas Canvas;

		// Token: 0x04000418 RID: 1048
		private RectTransform m_ScrollTransform;

		// Token: 0x04000419 RID: 1049
		private RectTransform m_ContentTransform;

		// Token: 0x0400041A RID: 1050
		private RectTransform m_MaskTransform;

		// Token: 0x0400041B RID: 1051
		private DebugUIHandlerWidget m_ScrollTarget;

		// Token: 0x0400041C RID: 1052
		protected internal DebugUI.Panel m_Panel;
	}
}
