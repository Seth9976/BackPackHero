using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public static class ExtensionMethod
{
	// Token: 0x060008D4 RID: 2260 RVA: 0x0005CA00 File Offset: 0x0005AC00
	public static Texture2D DeCompress(this Texture2D source)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
		Graphics.Blit(source, temporary);
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = temporary;
		Texture2D texture2D = new Texture2D(source.width, source.height);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)temporary.width, (float)temporary.height), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		RenderTexture.ReleaseTemporary(temporary);
		return texture2D;
	}
}
