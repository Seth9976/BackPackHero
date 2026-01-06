using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UnityEngine.Rendering.UI
{
	// Token: 0x02000107 RID: 263
	[ExecuteAlways]
	public class UIFoldout : Toggle
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x000222A9 File Offset: 0x000204A9
		protected override void Start()
		{
			base.Start();
			this.onValueChanged.AddListener(new UnityAction<bool>(this.SetState));
			this.SetState(base.isOn);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000222D4 File Offset: 0x000204D4
		private void OnValidate()
		{
			this.SetState(base.isOn, false);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000222E3 File Offset: 0x000204E3
		public void SetState(bool state)
		{
			this.SetState(state, true);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000222F0 File Offset: 0x000204F0
		public void SetState(bool state, bool rebuildLayout)
		{
			if (this.arrowOpened == null || this.arrowClosed == null || this.content == null)
			{
				return;
			}
			if (this.arrowOpened.activeSelf != state)
			{
				this.arrowOpened.SetActive(state);
			}
			if (this.arrowClosed.activeSelf == state)
			{
				this.arrowClosed.SetActive(!state);
			}
			if (this.content.activeSelf != state)
			{
				this.content.SetActive(state);
			}
			if (rebuildLayout)
			{
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform.parent as RectTransform);
			}
		}

		// Token: 0x0400044A RID: 1098
		public GameObject content;

		// Token: 0x0400044B RID: 1099
		public GameObject arrowOpened;

		// Token: 0x0400044C RID: 1100
		public GameObject arrowClosed;
	}
}
