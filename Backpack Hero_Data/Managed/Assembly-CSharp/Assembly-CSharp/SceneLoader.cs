using System;
using System.Collections;
using DevPunksSaveGame;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x0200016A RID: 362
public class SceneLoader : MonoBehaviour
{
	// Token: 0x06000EB8 RID: 3768 RVA: 0x00092BAD File Offset: 0x00090DAD
	private void Awake()
	{
		if (SceneLoader.main != this && SceneLoader.main != null)
		{
			Debug.Log("Destroying duplicate loading scene");
			Object.Destroy(base.gameObject);
			return;
		}
		SceneLoader.main = this;
		Object.DontDestroyOnLoad(this);
	}

	// Token: 0x06000EB9 RID: 3769 RVA: 0x00092BEB File Offset: 0x00090DEB
	private void OnDestroy()
	{
		if (SceneLoader.main == this)
		{
			SceneLoader.main = null;
		}
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x00092C00 File Offset: 0x00090E00
	public bool IsLoading()
	{
		return this.isLoading;
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x00092C08 File Offset: 0x00090E08
	public string GetSceneNameToLoad()
	{
		return this.sceneNameToLoad;
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x00092C10 File Offset: 0x00090E10
	public bool IsHidden()
	{
		return !this.loadingScreenObject.activeInHierarchy;
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x00092C20 File Offset: 0x00090E20
	public void SetHidden(bool state)
	{
		this.loadingScreenObject.SetActive(!state);
	}

	// Token: 0x06000EBE RID: 3774 RVA: 0x00092C31 File Offset: 0x00090E31
	public void LoadScene(string sceneName, SceneLoader.SceneLoaderPredicate predicate)
	{
		this.LoadScene(sceneName, LoadSceneMode.Single, predicate, null);
	}

	// Token: 0x06000EBF RID: 3775 RVA: 0x00092C3D File Offset: 0x00090E3D
	public void LoadScene(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, SceneLoader.SceneLoaderPredicate predicate = null, Action callback = null)
	{
		if (this.isLoading)
		{
			return;
		}
		this.sceneNameToLoad = sceneName;
		base.StartCoroutine(this.LoadSceneAsync(sceneName, mode, predicate, callback));
	}

	// Token: 0x06000EC0 RID: 3776 RVA: 0x00092C61 File Offset: 0x00090E61
	private IEnumerator LoadSceneAsync(string sceneID, LoadSceneMode mode = LoadSceneMode.Single, SceneLoader.SceneLoaderPredicate predicate = null, Action callback = null)
	{
		this.isLoading = true;
		this.loadingScreenObject.SetActive(true);
		this.loadingScreenAnimator.enabled = false;
		this.loadingScreenObject.GetComponentInChildren<CanvasGroup>().alpha = 1f;
		this.loadingBarFill.fillAmount = 0f;
		yield return new WaitForEndOfFrame();
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneID, mode);
		asyncLoad.allowSceneActivation = false;
		while ((double)asyncLoad.progress < 0.5)
		{
			float num = Mathf.Clamp(asyncLoad.progress, 0f, 1f);
			this.loadingBarFill.fillAmount = num;
			yield return null;
		}
		while (ConsoleWrapper.Instance.SaveInProgress)
		{
			yield return null;
		}
		while (predicate != null && !predicate())
		{
			yield return null;
		}
		asyncLoad.allowSceneActivation = true;
		while (!asyncLoad.isDone)
		{
			float num2 = Mathf.Clamp(asyncLoad.progress, 0f, 1f);
			this.loadingBarFill.fillAmount = num2;
			yield return null;
		}
		this.loadingScreenAnimator.enabled = true;
		this.loadingScreenAnimator.Play("fadeLoading");
		this.isLoading = false;
		this.sceneNameToLoad = null;
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	// Token: 0x06000EC1 RID: 3777 RVA: 0x00092C8D File Offset: 0x00090E8D
	private IEnumerator CompleteLoadingBar()
	{
		while (this.loadingBarFill.fillAmount < 0.99f)
		{
			this.loadingBarFill.fillAmount = Mathf.Clamp(this.loadingBarFill.fillAmount + 0.01f, 0f, 1f);
			yield return new WaitForSeconds(0.01f);
		}
		yield break;
	}

	// Token: 0x04000BF1 RID: 3057
	public static SceneLoader main;

	// Token: 0x04000BF2 RID: 3058
	[SerializeField]
	private GameObject loadingScreenObject;

	// Token: 0x04000BF3 RID: 3059
	[SerializeField]
	private Image loadingBarFill;

	// Token: 0x04000BF4 RID: 3060
	[SerializeField]
	private Animator loadingScreenAnimator;

	// Token: 0x04000BF5 RID: 3061
	private bool isLoading;

	// Token: 0x04000BF6 RID: 3062
	private string sceneNameToLoad;

	// Token: 0x0200043B RID: 1083
	// (Invoke) Token: 0x060019FC RID: 6652
	public delegate bool SceneLoaderPredicate();
}
