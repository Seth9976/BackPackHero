using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

// Token: 0x0200009E RID: 158
public class Screenshotter : MonoBehaviour
{
	// Token: 0x0600037B RID: 891 RVA: 0x000145B2 File Offset: 0x000127B2
	private void Start()
	{
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000145B4 File Offset: 0x000127B4
	private void Update()
	{
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000145B6 File Offset: 0x000127B6
	public void TakeScreenshot()
	{
		base.StartCoroutine(this.SaveImage());
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000145C5 File Offset: 0x000127C5
	public IEnumerator SaveImage()
	{
		yield return null;
		Camera camera = this.screenshotCamera;
		bool activeStart = camera.gameObject.activeSelf;
		camera.gameObject.SetActive(true);
		PixelPerfectCamera ppc = camera.gameObject.GetComponent<PixelPerfectCamera>();
		Vector2 startingResolution = new Vector2((float)ppc.refResolutionX, (float)ppc.refResolutionY);
		int startingPPU = ppc.assetsPPU;
		int resWidth = this.resolution.x;
		int resHeight = this.resolution.y;
		ppc.refResolutionX = resWidth;
		ppc.refResolutionY = resHeight;
		ppc.assetsPPU = this.ppu;
		yield return null;
		RenderTexture renderTexture = new RenderTexture(resWidth, resHeight, 24);
		camera.targetTexture = renderTexture;
		Texture2D texture2D = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
		camera.Render();
		RenderTexture.active = renderTexture;
		Debug.unityLogger.Log("read pixels");
		texture2D.ReadPixels(new Rect(0f, 0f, (float)resWidth, (float)resHeight), 0, 0);
		camera.targetTexture = null;
		RenderTexture.active = null;
		Object.Destroy(renderTexture);
		Debug.Log("Encoding");
		byte[] array = texture2D.EncodeToPNG();
		Debug.Log("Writing to persistent Data Path");
		if (!Directory.Exists(Application.persistentDataPath + "/Screenshots/"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/Screenshots/");
		}
		int num = 0;
		string text = Application.persistentDataPath + "/Screenshots/myScreenshot" + num.ToString() + ".png";
		while (File.Exists(text))
		{
			num++;
			text = Application.persistentDataPath + "/Screenshots/myScreenshot" + num.ToString() + ".png";
		}
		Debug.Log("write all bytese for PC");
		File.WriteAllBytes(text, array);
		Debug.Log("Took screenshot to at " + text);
		Application.OpenURL(Application.persistentDataPath + "/Screenshots/");
		yield return null;
		ppc.refResolutionX = (int)startingResolution.x;
		ppc.refResolutionY = (int)startingResolution.y;
		ppc.assetsPPU = startingPPU;
		camera.gameObject.SetActive(activeStart);
		yield break;
	}

	// Token: 0x0400027C RID: 636
	[SerializeField]
	private Camera screenshotCamera;

	// Token: 0x0400027D RID: 637
	[SerializeField]
	private Vector2Int resolution = new Vector2Int(2560, 1440);

	// Token: 0x0400027E RID: 638
	[SerializeField]
	private int ppu = 128;
}
