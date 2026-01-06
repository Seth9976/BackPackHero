using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000076 RID: 118
public class OnCloseEvent : MonoBehaviour
{
	// Token: 0x06000265 RID: 613 RVA: 0x0000EB3F File Offset: 0x0000CD3F
	public void LoadCredits()
	{
		SceneLoader.main.LoadScene("Credits", LoadSceneMode.Single, null, null);
	}
}
