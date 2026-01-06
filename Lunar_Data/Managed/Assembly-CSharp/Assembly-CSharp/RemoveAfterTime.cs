using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class RemoveAfterTime : MonoBehaviour
{
	// Token: 0x06000373 RID: 883 RVA: 0x000113B3 File Offset: 0x0000F5B3
	private void Update()
	{
		this.time -= Time.deltaTime;
		if (this.time <= 0f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040002A3 RID: 675
	[SerializeField]
	private float time;
}
