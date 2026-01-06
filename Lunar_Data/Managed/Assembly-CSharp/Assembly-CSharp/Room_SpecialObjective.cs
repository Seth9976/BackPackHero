using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class Room_SpecialObjective : MonoBehaviour
{
	// Token: 0x06000391 RID: 913 RVA: 0x00011D5B File Offset: 0x0000FF5B
	private void Start()
	{
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00011D60 File Offset: 0x0000FF60
	private void Update()
	{
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		if (!this.specialObjectivePrefab)
		{
			return;
		}
		this.timeToWait -= Time.deltaTime;
		if (this.timeToWait > 0f)
		{
			return;
		}
		Object.Instantiate<GameObject>(this.specialObjectivePrefab, CanvasManager.instance.masterContentScaler).GetComponentInChildren<ReplacementText>().SetKey(this.specialObjectiveKey);
		Object.Destroy(this);
	}

	// Token: 0x040002B9 RID: 697
	[SerializeField]
	private GameObject specialObjectivePrefab;

	// Token: 0x040002BA RID: 698
	[SerializeField]
	private string specialObjectiveKey = "SpecialObjective";

	// Token: 0x040002BB RID: 699
	private float timeToWait = 0.5f;
}
