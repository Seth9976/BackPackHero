using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

// Token: 0x02000010 RID: 16
public class PixelPefectCanvas : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x00003A81 File Offset: 0x00001C81
	private void Start()
	{
		this.pixelPerfectCamera = Object.FindObjectOfType<PixelPerfectCamera>();
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00003A8E File Offset: 0x00001C8E
	private void LateUpdate()
	{
		base.transform.position = this.pixelPerfectCamera.RoundToPixel(this.pixelPerfectCamera.transform.position);
	}

	// Token: 0x0400002F RID: 47
	private PixelPerfectCamera pixelPerfectCamera;
}
