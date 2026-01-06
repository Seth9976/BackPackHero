using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000047 RID: 71
public class GameOverManager : MonoBehaviour
{
	// Token: 0x06000200 RID: 512 RVA: 0x0000ABF7 File Offset: 0x00008DF7
	private void OnEnable()
	{
		if (GameOverManager.instance == null)
		{
			GameOverManager.instance = this;
		}
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000AC0C File Offset: 0x00008E0C
	private void OnDisable()
	{
		if (GameOverManager.instance == this)
		{
			GameOverManager.instance = null;
		}
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000AC21 File Offset: 0x00008E21
	public void ShowGameOver()
	{
		if (this.showGameOverCoroutine != null)
		{
			base.StopCoroutine(this.showGameOverCoroutine);
		}
		this.showGameOverCoroutine = base.StartCoroutine(this.ShowGameOverR());
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000AC49 File Offset: 0x00008E49
	private IEnumerator ShowGameOverR()
	{
		yield return new WaitForSeconds(1.25f);
		UnlockManager.instance.ShowUnlocks();
		yield return new WaitForSeconds(0.1f);
		while (SingleUI.IsViewingPopUp())
		{
			yield return null;
		}
		SoundManager.instance.FadeOutAllSongs();
		this.gameOverPanel.SetActive(true);
		yield break;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x0000AC58 File Offset: 0x00008E58
	public void Retry()
	{
		string name = SceneManager.GetActiveScene().name;
		LoadingManager.instance.LoadScene(name);
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000AC7E File Offset: 0x00008E7E
	public void Quit()
	{
		LoadingManager.instance.LoadScene("Main Menu");
	}

	// Token: 0x04000189 RID: 393
	public static GameOverManager instance;

	// Token: 0x0400018A RID: 394
	[SerializeField]
	private GameObject gameOverPanel;

	// Token: 0x0400018B RID: 395
	private Coroutine showGameOverCoroutine;
}
