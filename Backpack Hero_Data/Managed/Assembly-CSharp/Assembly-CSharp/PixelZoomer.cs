using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

// Token: 0x02000088 RID: 136
public class PixelZoomer : MonoBehaviour
{
	// Token: 0x06000305 RID: 773 RVA: 0x00011DC2 File Offset: 0x0000FFC2
	private void Awake()
	{
		if (PixelZoomer.main == null)
		{
			PixelZoomer.main = this;
			return;
		}
		Object.Destroy(this);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00011DDE File Offset: 0x0000FFDE
	private void OnDestroy()
	{
		if (PixelZoomer.main == this)
		{
			PixelZoomer.main = null;
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00011DF4 File Offset: 0x0000FFF4
	private void Start()
	{
		this.startTime = 0f;
		this.storedZoom = 0f;
		this.SetResolution(1f);
		this.pixelPerfectCamera.assetsPPU = 64;
		this.pixelPerfectCamera.refResolutionX = Mathf.RoundToInt(1280f);
		this.pixelPerfectCamera.refResolutionY = Mathf.RoundToInt(720f);
		this.currentResolution.x = 1280f;
		this.currentResolution.y = 720f;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00011E7C File Offset: 0x0001007C
	private void Update()
	{
		if (this.startTime < 1f)
		{
			this.currentResolution.x = (float)Mathf.RoundToInt(Mathf.Lerp(this.currentResolution.x, this.setResolution.x, this.startTime / 1f));
			this.currentResolution.y = (float)Mathf.RoundToInt(Mathf.Lerp(this.currentResolution.y, this.setResolution.y, this.startTime / 1f));
			this.startTime += Time.deltaTime;
		}
		else
		{
			this.startTime = 1f;
			this.currentResolution.x = (float)Mathf.RoundToInt(this.setResolution.x);
			this.currentResolution.y = (float)Mathf.RoundToInt(this.setResolution.y);
		}
		float num = (float)Singleton.Instance.resolutionX / 1280f;
		this.pixelPerfectCamera.assetsPPU = Mathf.RoundToInt(64f * num);
		this.pixelPerfectCamera.refResolutionX = Mathf.RoundToInt(this.currentResolution.x * num);
		this.pixelPerfectCamera.refResolutionY = Mathf.RoundToInt(this.currentResolution.y * num);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00011FC4 File Offset: 0x000101C4
	public void SetResolution(float percentageOfDefault)
	{
		if (this.storedZoom == percentageOfDefault)
		{
			return;
		}
		this.storedZoom = percentageOfDefault;
		this.startTime = 0f;
		this.setResolution = new Vector2((float)Mathf.RoundToInt(1280f / percentageOfDefault), (float)Mathf.RoundToInt(720f / percentageOfDefault));
	}

	// Token: 0x04000201 RID: 513
	public static PixelZoomer main;

	// Token: 0x04000202 RID: 514
	[SerializeField]
	private PixelPerfectCamera pixelPerfectCamera;

	// Token: 0x04000203 RID: 515
	[SerializeField]
	private Vector2 setResolution;

	// Token: 0x04000204 RID: 516
	[SerializeField]
	private Vector2 currentResolution;

	// Token: 0x04000205 RID: 517
	[SerializeField]
	private float startTime = 1f;

	// Token: 0x04000206 RID: 518
	[SerializeField]
	private float storedZoom = 1f;
}
