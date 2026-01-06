using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class PauseMenu : MonoBehaviour
{
	// Token: 0x060002E5 RID: 741 RVA: 0x0000EE10 File Offset: 0x0000D010
	private void Start()
	{
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0000EE12 File Offset: 0x0000D012
	private void Update()
	{
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x0000EE14 File Offset: 0x0000D014
	public void ShowOptions()
	{
		Object.Instantiate<GameObject>(this.optionsPanel, CanvasManager.instance.canvas.transform);
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000EE31 File Offset: 0x0000D031
	public void QuitToMenu()
	{
		LoadingManager.instance.LoadScene("Main Menu");
	}

	// Token: 0x04000231 RID: 561
	[SerializeField]
	private GameObject optionsPanel;
}
