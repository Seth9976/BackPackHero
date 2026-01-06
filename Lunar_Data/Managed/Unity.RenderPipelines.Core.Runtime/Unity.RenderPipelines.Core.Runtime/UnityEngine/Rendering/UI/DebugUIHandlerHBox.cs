using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F6 RID: 246
	public class DebugUIHandlerHBox : DebugUIHandlerWidget
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x000207EB File Offset: 0x0001E9EB
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00020800 File Offset: 0x0001EA00
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

		// Token: 0x06000743 RID: 1859 RVA: 0x0002083C File Offset: 0x0001EA3C
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

		// Token: 0x040003FD RID: 1021
		private DebugUIHandlerContainer m_Container;
	}
}
