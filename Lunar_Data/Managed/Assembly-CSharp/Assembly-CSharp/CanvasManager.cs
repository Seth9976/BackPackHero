using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class CanvasManager : MonoBehaviour
{
	// Token: 0x0600005E RID: 94 RVA: 0x000036E6 File Offset: 0x000018E6
	private void OnEnable()
	{
		if (CanvasManager.instance == null)
		{
			CanvasManager.instance = this;
			return;
		}
		if (CanvasManager.instance != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00003714 File Offset: 0x00001914
	private void OnDisable()
	{
		if (CanvasManager.instance == this)
		{
			CanvasManager.instance = null;
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003729 File Offset: 0x00001929
	public Vector2 GetCenterOfCanvas()
	{
		return this.canvas.transform.position;
	}

	// Token: 0x0400003E RID: 62
	[SerializeField]
	public static CanvasManager instance;

	// Token: 0x0400003F RID: 63
	[SerializeField]
	public Canvas canvas;

	// Token: 0x04000040 RID: 64
	[SerializeField]
	public Transform masterContentScaler;

	// Token: 0x04000041 RID: 65
	[SerializeField]
	public RectTransform rectTransform;
}
