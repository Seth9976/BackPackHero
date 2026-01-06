using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
public class LoadTutorial : MonoBehaviour
{
	// Token: 0x06000FB9 RID: 4025 RVA: 0x000992F8 File Offset: 0x000974F8
	private void Start()
	{
		TutorialManager tutorialManager = Object.FindObjectOfType<TutorialManager>();
		if (tutorialManager && this.tutorialName.Length > 0)
		{
			tutorialManager.ConsiderTutorial(this.tutorialName);
		}
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x0009932D File Offset: 0x0009752D
	private void Update()
	{
	}

	// Token: 0x04000CE2 RID: 3298
	[SerializeField]
	private string tutorialName;
}
