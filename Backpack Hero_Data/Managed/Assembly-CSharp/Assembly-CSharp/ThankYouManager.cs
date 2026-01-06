using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001A0 RID: 416
public class ThankYouManager : MonoBehaviour
{
	// Token: 0x06001096 RID: 4246 RVA: 0x0009DDD4 File Offset: 0x0009BFD4
	private void Start()
	{
		base.StartCoroutine(this.StartAnimations());
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0009DDE3 File Offset: 0x0009BFE3
	private IEnumerator StartAnimations()
	{
		Animator[] componentsInChildren = this.players.GetComponentsInChildren<Animator>();
		Animator[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play("Player_Win");
			yield return new WaitForSeconds(Random.Range(0.01f, 0.03f));
		}
		array = null;
		yield break;
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0009DDF2 File Offset: 0x0009BFF2
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 60f)
		{
			this.LoadScene();
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0009DE19 File Offset: 0x0009C019
	public void LoadScene()
	{
		SceneLoader.main.LoadScene("MainMenu", LoadSceneMode.Single, null, null);
	}

	// Token: 0x04000D85 RID: 3461
	[SerializeField]
	private Transform players;

	// Token: 0x04000D86 RID: 3462
	private float time;
}
