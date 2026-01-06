using System;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class RunPanel : MonoBehaviour
{
	// Token: 0x06000394 RID: 916 RVA: 0x00011DEC File Offset: 0x0000FFEC
	public void LoadGame()
	{
		MenuManager.instance.LoadGame();
		base.GetComponentInParent<SingleUI>().CloseAndDestroyViaFade();
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00011E03 File Offset: 0x00010003
	public void ChooseCharacter()
	{
		this.runTypeWindow.SetupRunTypes();
	}

	// Token: 0x040002BC RID: 700
	[SerializeField]
	private RunTypeWindow runTypeWindow;
}
