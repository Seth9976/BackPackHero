using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005D RID: 93
public class HideScrollBar : MonoBehaviour
{
	// Token: 0x060001A1 RID: 417 RVA: 0x0000A8AC File Offset: 0x00008AAC
	public bool IsVisible(RectTransform rectTransform)
	{
		if (!rectTransform)
		{
			return false;
		}
		Vector3[] array = new Vector3[4];
		this.viewportTransform.GetWorldCorners(array);
		return rectTransform.transform.position.y <= array[2].y && rectTransform.transform.position.y >= array[0].y;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000A918 File Offset: 0x00008B18
	private void Setup()
	{
		foreach (object obj in this.sliderContentTransform)
		{
			Transform transform = (Transform)obj;
			this.activeChildren.Add(new HideScrollBar.ActiveChildren(transform.GetComponent<RectTransform>()));
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000A980 File Offset: 0x00008B80
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			this.Setup();
		}
		foreach (HideScrollBar.ActiveChildren activeChildren in this.activeChildren)
		{
			if (this.IsVisible(activeChildren.parentTransform))
			{
				if (!activeChildren.isCurrentlyVisible)
				{
					activeChildren.isCurrentlyVisible = true;
					activeChildren.SetChildren();
				}
			}
			else if (activeChildren.isCurrentlyVisible)
			{
				activeChildren.isCurrentlyVisible = false;
				activeChildren.DisableAndRememberChildren();
			}
		}
	}

	// Token: 0x04000112 RID: 274
	[SerializeField]
	private RectTransform sliderContentTransform;

	// Token: 0x04000113 RID: 275
	[SerializeField]
	private RectTransform viewportTransform;

	// Token: 0x04000114 RID: 276
	[SerializeField]
	private List<HideScrollBar.ActiveChildren> activeChildren = new List<HideScrollBar.ActiveChildren>();

	// Token: 0x04000115 RID: 277
	private RectTransform[] sliderContentTransforms;

	// Token: 0x04000116 RID: 278
	private ImagePosition[] sliderContentImages;

	// Token: 0x0200026E RID: 622
	[Serializable]
	private class ActiveChildren
	{
		// Token: 0x0600132B RID: 4907 RVA: 0x000AF218 File Offset: 0x000AD418
		private void GetChildren()
		{
			foreach (object obj in this.parentTransform)
			{
				Transform transform = (Transform)obj;
				this.activeChildren.Add(new HideScrollBar.ActiveChildren.ActiveChild(transform, transform.gameObject.activeSelf));
			}
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000AF288 File Offset: 0x000AD488
		public void DisableAndRememberChildren()
		{
			this.parentImage.enabled = false;
			foreach (HideScrollBar.ActiveChildren.ActiveChild activeChild in this.activeChildren)
			{
				activeChild.active = activeChild.transform.gameObject.activeSelf;
				activeChild.transform.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000AF308 File Offset: 0x000AD508
		public void SetChildren()
		{
			this.parentImage.enabled = true;
			foreach (HideScrollBar.ActiveChildren.ActiveChild activeChild in this.activeChildren)
			{
				activeChild.transform.gameObject.SetActive(activeChild.active);
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000AF378 File Offset: 0x000AD578
		public ActiveChildren(RectTransform parentTransform)
		{
			this.parentImage = parentTransform.GetComponent<Image>();
			this.parentTransform = parentTransform;
			this.GetChildren();
		}

		// Token: 0x04000F28 RID: 3880
		public RectTransform parentTransform;

		// Token: 0x04000F29 RID: 3881
		public Image parentImage;

		// Token: 0x04000F2A RID: 3882
		public bool isCurrentlyVisible;

		// Token: 0x04000F2B RID: 3883
		private List<HideScrollBar.ActiveChildren.ActiveChild> activeChildren = new List<HideScrollBar.ActiveChildren.ActiveChild>();

		// Token: 0x0200048A RID: 1162
		private class ActiveChild
		{
			// Token: 0x06001AF2 RID: 6898 RVA: 0x000D6D80 File Offset: 0x000D4F80
			public ActiveChild(Transform transform, bool active)
			{
				this.transform = transform;
				this.active = active;
			}

			// Token: 0x04001A84 RID: 6788
			public Transform transform;

			// Token: 0x04001A85 RID: 6789
			public bool active;
		}
	}
}
