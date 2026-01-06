using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class Overworld_RunManager : MonoBehaviour
{
	// Token: 0x06000D8B RID: 3467 RVA: 0x00087A6B File Offset: 0x00085C6B
	private void Start()
	{
		this.UICanvas = GameObject.FindGameObjectWithTag("UI Canvas").GetComponent<Canvas>();
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00087A82 File Offset: 0x00085C82
	private void Update()
	{
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00087A84 File Offset: 0x00085C84
	public void RunButton()
	{
		if (!Overworld_Purse.main.IsFreeToMove())
		{
			return;
		}
		PulseImage component = this.runButton.GetComponent<PulseImage>();
		if (component)
		{
			Object.Destroy(component);
		}
		SoundManager.main.PlaySFX("menuBlip");
		DigitalCursor.main.Show();
		Object.Instantiate<GameObject>(this.runMenuPrefab, this.UICanvas.transform);
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x00087AE8 File Offset: 0x00085CE8
	public void StartGame()
	{
	}

	// Token: 0x04000AFE RID: 2814
	[SerializeField]
	private GameObject runButton;

	// Token: 0x04000AFF RID: 2815
	[SerializeField]
	private GameObject runMenuPrefab;

	// Token: 0x04000B00 RID: 2816
	private Canvas UICanvas;
}
