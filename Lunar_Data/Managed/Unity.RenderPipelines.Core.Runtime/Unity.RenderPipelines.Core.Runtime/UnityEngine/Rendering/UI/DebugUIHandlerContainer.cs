using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x020000F0 RID: 240
	public class DebugUIHandlerContainer : MonoBehaviour
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x0001FD48 File Offset: 0x0001DF48
		internal DebugUIHandlerWidget GetFirstItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[0];
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		internal DebugUIHandlerWidget GetLastItem()
		{
			if (this.contentHolder.childCount == 0)
			{
				return null;
			}
			List<DebugUIHandlerWidget> activeChildren = this.GetActiveChildren();
			if (activeChildren.Count == 0)
			{
				return null;
			}
			return activeChildren[activeChildren.Count - 1];
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001FDB8 File Offset: 0x0001DFB8
		internal bool IsDirectChild(DebugUIHandlerWidget widget)
		{
			return this.contentHolder.childCount != 0 && this.GetActiveChildren().Count((DebugUIHandlerWidget x) => x == widget) > 0;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001FDFC File Offset: 0x0001DFFC
		private List<DebugUIHandlerWidget> GetActiveChildren()
		{
			List<DebugUIHandlerWidget> list = new List<DebugUIHandlerWidget>();
			foreach (object obj in this.contentHolder)
			{
				Transform transform = (Transform)obj;
				if (transform.gameObject.activeInHierarchy)
				{
					DebugUIHandlerWidget component = transform.GetComponent<DebugUIHandlerWidget>();
					if (component != null)
					{
						list.Add(component);
					}
				}
			}
			return list;
		}

		// Token: 0x040003E8 RID: 1000
		[SerializeField]
		public RectTransform contentHolder;
	}
}
