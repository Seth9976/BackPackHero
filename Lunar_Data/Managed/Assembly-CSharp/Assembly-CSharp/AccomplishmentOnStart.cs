using System;
using SaveSystem.States;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AccomplishmentOnStart : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Start()
	{
		Singleton.instance.AddAccomplishment(this.accomplishment, this.amount);
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	public ProgressState.Accomplishment accomplishment;

	// Token: 0x04000002 RID: 2
	[SerializeField]
	public int amount = 1;
}
