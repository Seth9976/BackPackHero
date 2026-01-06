using System;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F5 RID: 245
	public class DebugUIHandlerGroup : DebugUIHandlerWidget
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x00020700 File Offset: 0x0001E900
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Field = base.CastWidget<DebugUI.Container>();
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
			if (string.IsNullOrEmpty(this.m_Field.displayName))
			{
				this.header.gameObject.SetActive(false);
				return;
			}
			this.nameLabel.text = this.m_Field.displayName;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00020768 File Offset: 0x0001E968
		public override bool OnSelection(bool fromNext, DebugUIHandlerWidget previous)
		{
			if (!fromNext && !this.m_Container.IsDirectChild(previous))
			{
				DebugUIHandlerWidget lastItem = this.m_Container.GetLastItem();
				DebugManager.instance.ChangeSelection(lastItem, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public override DebugUIHandlerWidget Next()
		{
			if (this.m_Container == null)
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

		// Token: 0x040003F9 RID: 1017
		public Text nameLabel;

		// Token: 0x040003FA RID: 1018
		public Transform header;

		// Token: 0x040003FB RID: 1019
		private DebugUI.Container m_Field;

		// Token: 0x040003FC RID: 1020
		private DebugUIHandlerContainer m_Container;
	}
}
