using System;
using System.IO;
using System.Linq;
using UnityEngine;

// Token: 0x0200012C RID: 300
internal class SpriteExporter
{
	// Token: 0x06000B4C RID: 2892 RVA: 0x00073BD4 File Offset: 0x00071DD4
	public static void ExportSprite(Sprite sprite, string path)
	{
		Color[] pixels = SpriteExporter.duplicateTexture(sprite.texture).GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
		Texture2D texture2D = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
		texture2D.SetPixels(pixels.ToArray<Color>());
		byte[] array = texture2D.EncodeToPNG();
		File.WriteAllBytes(path, array);
	}

	// Token: 0x06000B4D RID: 2893 RVA: 0x00073C6C File Offset: 0x00071E6C
	public static Texture2D duplicateTexture(Texture2D source)
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
