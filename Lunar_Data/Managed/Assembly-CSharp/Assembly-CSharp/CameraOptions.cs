using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

// Token: 0x0200000E RID: 14
public class CameraOptions : MonoBehaviour
{
	// Token: 0x06000056 RID: 86 RVA: 0x00003660 File Offset: 0x00001860
	private void Update()
	{
		this.pixelPerfectCamera.cropFrame = (Singleton.instance.stretchToFill ? PixelPerfectCamera.CropFrame.StretchFill : PixelPerfectCamera.CropFrame.Windowbox);
	}

	// Token: 0x0400003A RID: 58
	[SerializeField]
	private PixelPerfectCamera pixelPerfectCamera;
}
