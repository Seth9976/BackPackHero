using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000078 RID: 120
public class LoadingManager : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000354 RID: 852 RVA: 0x00010C85 File Offset: 0x0000EE85
	public static LoadingManager instance
	{
		get
		{
			return LoadingManager.main;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000355 RID: 853 RVA: 0x00010C8C File Offset: 0x0000EE8C
	// (set) Token: 0x06000356 RID: 854 RVA: 0x00010C94 File Offset: 0x0000EE94
	public string sceneNameToLoad { get; private set; } = "";

	// Token: 0x06000357 RID: 855 RVA: 0x00010C9D File Offset: 0x0000EE9D
	private void Awake()
	{
		if (LoadingManager.main != this && LoadingManager.main != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		LoadingManager.main = this;
		Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00010CD1 File Offset: 0x0000EED1
	private void OnDestroy()
	{
		if (LoadingManager.main == this)
		{
			LoadingManager.main = null;
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00010CE6 File Offset: 0x0000EEE6
	public bool IsLoading()
	{
		return this.isLoading;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00010CF0 File Offset: 0x0000EEF0
	public void ReloadScene()
	{
		if (this.isLoading)
		{
			return;
		}
		this.sceneNameToLoad = SceneManager.GetActiveScene().name;
		base.StartCoroutine(this.LoadSceneAsync(this.sceneNameToLoad));
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00010D2C File Offset: 0x0000EF2C
	public void LoadScene(string sceneName)
	{
		if (this.isLoading)
		{
			return;
		}
		this.sceneNameToLoad = sceneName;
		base.StartCoroutine(this.LoadSceneAsync(sceneName));
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00010D4C File Offset: 0x0000EF4C
	private IEnumerator LoadSceneAsync(string sceneID)
	{
		this.isLoading = true;
		this.loadingScreenObject.SetActive(true);
		CanvasGroup canvasGroup = this.loadingScreenObject.GetComponentInChildren<CanvasGroup>();
		yield return this.Fade(canvasGroup, 0f, 1f, 0.25f);
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Single);
		while (!asyncLoad.isDone)
		{
			float num = Mathf.Clamp(asyncLoad.progress / 0.9f, 0f, 1f);
			this.loadingBarFill.fillAmount = num;
			yield return null;
		}
		yield return this.Fade(canvasGroup, 1f, 0f, 0.25f);
		this.loadingScreenObject.SetActive(false);
		this.isLoading = false;
		this.sceneNameToLoad = "";
		yield break;
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00010D62 File Offset: 0x0000EF62
	private IEnumerator Fade(CanvasGroup c, float start, float end, float duration)
	{
		float currentTime = 0f;
		while (currentTime < duration)
		{
			currentTime += Time.unscaledDeltaTime;
			float num = Mathf.Lerp(start, end, currentTime / duration);
			c.alpha = num;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0400028B RID: 651
	public static LoadingManager main;

	// Token: 0x0400028C RID: 652
	[SerializeField]
	private GameObject loadingScreenObject;

	// Token: 0x0400028D RID: 653
	[SerializeField]
	private Image loadingBarFill;

	// Token: 0x0400028E RID: 654
	private bool isLoading;
}
