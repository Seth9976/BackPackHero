using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class InventoryScaler : MonoBehaviour
{
	// Token: 0x060008E9 RID: 2281 RVA: 0x0005D7A3 File Offset: 0x0005B9A3
	private void Start()
	{
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0005D7A5 File Offset: 0x0005B9A5
	private void Update()
	{
		base.transform.localScale = Vector3.MoveTowards(base.transform.localScale, this.intendedScale, Time.deltaTime * this.speed);
	}

	// Token: 0x0400070E RID: 1806
	public Vector3 intendedScale = Vector3.one;

	// Token: 0x0400070F RID: 1807
	[SerializeField]
	private float speed = 0.25f;
}
