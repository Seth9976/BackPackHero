using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000FC RID: 252
	internal class DebugUIHandlerPersistentCanvas : MonoBehaviour
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00020E8C File Offset: 0x0001F08C
		internal void Toggle(DebugUI.Value widget)
		{
			int num = this.m_Items.FindIndex((DebugUIHandlerValue x) => x.GetWidget() == widget);
			if (num > -1)
			{
				CoreUtils.Destroy(this.m_Items[num].gameObject);
				this.m_Items.RemoveAt(num);
				return;
			}
			GameObject gameObject = Object.Instantiate<RectTransform>(this.valuePrefab, this.panel, false).gameObject;
			gameObject.name = widget.displayName;
			DebugUIHandlerValue component = gameObject.GetComponent<DebugUIHandlerValue>();
			component.SetWidget(widget);
			this.m_Items.Add(component);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00020F2C File Offset: 0x0001F12C
		internal void Clear()
		{
			if (this.m_Items == null)
			{
				return;
			}
			foreach (DebugUIHandlerValue debugUIHandlerValue in this.m_Items)
			{
				CoreUtils.Destroy(debugUIHandlerValue.gameObject);
			}
			this.m_Items.Clear();
		}

		// Token: 0x0400041D RID: 1053
		public RectTransform panel;

		// Token: 0x0400041E RID: 1054
		public RectTransform valuePrefab;

		// Token: 0x0400041F RID: 1055
		private List<DebugUIHandlerValue> m_Items = new List<DebugUIHandlerValue>();
	}
}
