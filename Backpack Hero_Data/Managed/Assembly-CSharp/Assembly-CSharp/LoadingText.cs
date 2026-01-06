using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000186 RID: 390
public class LoadingText : MonoBehaviour
{
	// Token: 0x06000FB6 RID: 4022 RVA: 0x00099250 File Offset: 0x00097450
	private void Start()
	{
		foreach (TMP_Text tmp_Text in this.dots)
		{
			tmp_Text.color = new Color(0f, 0f, 0f, 0f);
		}
		base.StartCoroutine(this.LoadingDotsAnimation());
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x000992C8 File Offset: 0x000974C8
	private IEnumerator LoadingDotsAnimation()
	{
		yield return null;
		this.asyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
		this.asyncOperation.allowSceneActivation = false;
		while (this.asyncOperation != null && this.asyncOperation.isDone)
		{
			foreach (TMP_Text tmp_Text in this.dots)
			{
				tmp_Text.color = Color.white;
				if (this.asyncOperation != null && this.asyncOperation.isDone)
				{
					yield return new WaitForSeconds(0.5f);
				}
			}
			List<TMP_Text>.Enumerator enumerator = default(List<TMP_Text>.Enumerator);
			foreach (TMP_Text tmp_Text2 in this.dots)
			{
				tmp_Text2.color = new Color(0f, 0f, 0f, 0f);
			}
			if (this.asyncOperation != null && this.asyncOperation.isDone)
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
		Debug.Log("Main Menu Loading done 1");
		foreach (GameObject gameObject in this._gameObjectsToKill)
		{
			Object.Destroy(gameObject);
		}
		yield return null;
		Debug.Log("Main Menu Loading done 2");
		this.asyncOperation.allowSceneActivation = true;
		Debug.Log("Main Menu Loading done 3");
		while (LangaugeManager.main == null || !LangaugeManager.main.TranslationFileLoaded())
		{
			foreach (TMP_Text tmp_Text3 in this.dots)
			{
				tmp_Text3.color = Color.white;
				if (LangaugeManager.main == null || !LangaugeManager.main.TranslationFileLoaded())
				{
					yield return new WaitForSeconds(0.5f);
				}
			}
			List<TMP_Text>.Enumerator enumerator = default(List<TMP_Text>.Enumerator);
			foreach (TMP_Text tmp_Text4 in this.dots)
			{
				tmp_Text4.color = new Color(0f, 0f, 0f, 0f);
			}
			if (LangaugeManager.main == null || !LangaugeManager.main.TranslationFileLoaded())
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
		yield return null;
		yield break;
		yield break;
	}

	// Token: 0x04000CDF RID: 3295
	[SerializeField]
	private List<TMP_Text> dots = new List<TMP_Text>();

	// Token: 0x04000CE0 RID: 3296
	[SerializeField]
	private List<GameObject> _gameObjectsToKill = new List<GameObject>();

	// Token: 0x04000CE1 RID: 3297
	private AsyncOperation asyncOperation;
}
