using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class RunTypeButton : MonoBehaviour
{
	// Token: 0x0600039B RID: 923 RVA: 0x00011EEC File Offset: 0x000100EC
	private void Update()
	{
		if (Singleton.instance.selectedRun == this.runType)
		{
			this.selectionIndicator.SetActive(true);
			return;
		}
		this.selectionIndicator.SetActive(false);
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00011F20 File Offset: 0x00010120
	public void SetRunType(RunType runType)
	{
		this.runType = runType;
		this.replacementText.SetKey(runType.runName);
		if (Singleton.instance.CheckCompletedRun(runType) || runType.forceTutorial)
		{
			this.completedImage.SetActive(true);
			return;
		}
		this.completedImage.SetActive(false);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00011F73 File Offset: 0x00010173
	public void OnClick()
	{
		RunTypeWindow.instance.GetComponentInChildren<RunTypeDescriptionWindow>().SetDescription(this.runType);
		Singleton.instance.selectedRun = this.runType;
	}

	// Token: 0x040002C4 RID: 708
	[SerializeField]
	private RunType runType;

	// Token: 0x040002C5 RID: 709
	[SerializeField]
	private ReplacementText replacementText;

	// Token: 0x040002C6 RID: 710
	[SerializeField]
	private GameObject selectionIndicator;

	// Token: 0x040002C7 RID: 711
	[SerializeField]
	private GameObject completedImage;
}
