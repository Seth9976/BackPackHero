using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class Wiggle : MonoBehaviour
{
	// Token: 0x06000495 RID: 1173 RVA: 0x0001683D File Offset: 0x00014A3D
	private void Start()
	{
		this.originalPosition = base.transform.localPosition;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00016855 File Offset: 0x00014A55
	private void Update()
	{
		base.transform.localPosition = this.originalPosition + Random.insideUnitCircle * this.wiggleAmount;
	}

	// Token: 0x0400038C RID: 908
	[SerializeField]
	private float wiggleAmount = 10f;

	// Token: 0x0400038D RID: 909
	private Vector2 originalPosition;
}
