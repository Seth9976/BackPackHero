using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class SceneLoader : MonoBehaviour
{
	// Token: 0x060003AE RID: 942 RVA: 0x0001256F File Offset: 0x0001076F
	public void LoadMainMenu()
	{
		LoadingManager.instance.LoadScene("Main Menu");
	}
}
