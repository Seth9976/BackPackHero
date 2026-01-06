using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000184 RID: 388
public class EarlyAccessManager : MonoBehaviour
{
	// Token: 0x06000F94 RID: 3988 RVA: 0x00097A83 File Offset: 0x00095C83
	private void Start()
	{
		this.animator.Play("move_up");
		this.nextUpdateCharacter.Play("Player_Win");
		if (this.nextUpdateCharacter2 != null)
		{
			this.nextUpdateCharacter2.Play("Player_Win");
		}
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x00097AC3 File Offset: 0x00095CC3
	private void Update()
	{
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x00097AC5 File Offset: 0x00095CC5
	public void StartGame()
	{
		SoundManager.main.PlaySFX("menuBlip");
		SceneLoader.main.LoadScene("MainMenu", LoadSceneMode.Single, null, null);
	}

	// Token: 0x04000CB6 RID: 3254
	[SerializeField]
	private GameObject buttonGameObject;

	// Token: 0x04000CB7 RID: 3255
	[SerializeField]
	private Animator animator;

	// Token: 0x04000CB8 RID: 3256
	[SerializeField]
	private Animator nextUpdateCharacter;

	// Token: 0x04000CB9 RID: 3257
	[SerializeField]
	private Animator nextUpdateCharacter2;
}
