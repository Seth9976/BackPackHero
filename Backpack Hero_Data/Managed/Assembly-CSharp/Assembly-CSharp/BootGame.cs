using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000A4 RID: 164
public class BootGame : MonoBehaviour
{
	// Token: 0x060003A3 RID: 931 RVA: 0x00015EDB File Offset: 0x000140DB
	private void Awake()
	{
		this.dt_logo.color = new Color(1f, 1f, 1f, 0f);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00015F01 File Offset: 0x00014101
	private void Start()
	{
		base.StartCoroutine(this.BootGameLoop());
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00015F10 File Offset: 0x00014110
	private IEnumerator BootGameLoop()
	{
		bool inProgress = true;
		this.canvas.SetActive(true);
		SceneLoader.main.LoadScene("MainMenu", () => !inProgress);
		yield return new WaitForSeconds(0.5f);
		yield return this.FadeInImage(this.dt_logo, 1f);
		yield return new WaitForSeconds(1f);
		yield return this.FadeOutImage(this.dt_logo, 1f);
		this.canvas.SetActive(false);
		inProgress = false;
		yield break;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00015F1F File Offset: 0x0001411F
	private IEnumerator FadeInImage(Image image, float fadeTime)
	{
		yield return this.FadeImage(image, 0f, 1f, fadeTime);
		yield break;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00015F3C File Offset: 0x0001413C
	private IEnumerator FadeOutImage(Image image, float fadeTime)
	{
		yield return this.FadeImage(image, 1f, 0f, fadeTime);
		yield break;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00015F59 File Offset: 0x00014159
	private IEnumerator FadeImage(Image image, float from, float to, float fadeTime)
	{
		image.color = new Color(1f, 1f, 1f, from);
		float time = 0f;
		while (time < fadeTime)
		{
			time += Time.deltaTime;
			float num = Mathf.Lerp(from, to, time / fadeTime);
			image.color = new Color(1f, 1f, 1f, num);
			yield return null;
		}
		image.color = new Color(1f, 1f, 1f, to);
		yield break;
	}

	// Token: 0x04000292 RID: 658
	[SerializeField]
	private GameObject canvas;

	// Token: 0x04000293 RID: 659
	[SerializeField]
	private Image dt_logo;

	// Token: 0x04000294 RID: 660
	private const float fadeTime = 1f;

	// Token: 0x04000295 RID: 661
	private const float visibleTime = 1f;
}
