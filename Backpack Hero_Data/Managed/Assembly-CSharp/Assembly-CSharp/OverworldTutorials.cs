using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class OverworldTutorials : MonoBehaviour
{
	// Token: 0x06000296 RID: 662 RVA: 0x0000F907 File Offset: 0x0000DB07
	private void Start()
	{
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000F909 File Offset: 0x0000DB09
	private void Update()
	{
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000F90B File Offset: 0x0000DB0B
	public void ShowDestroyStoreTutorial(bool show)
	{
		this.destroyStoreTutorial.SetActive(show);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000F919 File Offset: 0x0000DB19
	public void ShowTellZaar1Tutorial(bool show)
	{
		ArrowTutorialManager.instance.HideArrow();
		this.tellZaar1Tutorial.SetActive(show);
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000F931 File Offset: 0x0000DB31
	public void ShowTellZaar2Tutorial(bool show)
	{
		this.tellZaar2Tutorial.SetActive(show);
	}

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	private GameObject destroyStoreTutorial;

	// Token: 0x040001B2 RID: 434
	[SerializeField]
	private GameObject tellZaar1Tutorial;

	// Token: 0x040001B3 RID: 435
	[SerializeField]
	private GameObject tellZaar2Tutorial;
}
