using System;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000102 RID: 258
	public class DebugUIHandlerVBox : DebugUIHandlerWidget
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00021635 File Offset: 0x0001F835
		internal override void SetWidget(DebugUI.Widget widget)
		{
			base.SetWidget(widget);
			this.m_Container = base.GetComponent<DebugUIHandlerContainer>();
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0002164C File Offset: 0x0001F84C
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

		// Token: 0x0600078D RID: 1933 RVA: 0x00021688 File Offset: 0x0001F888
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

		// Token: 0x0400042E RID: 1070
		private DebugUIHandlerContainer m_Container;
	}
}
