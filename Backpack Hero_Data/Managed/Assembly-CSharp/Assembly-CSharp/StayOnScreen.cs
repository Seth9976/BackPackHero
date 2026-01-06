using System;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class StayOnScreen : MonoBehaviour
{
	// Token: 0x0600107C RID: 4220 RVA: 0x0009D35C File Offset: 0x0009B55C
	private void Start()
	{
		this.storedLocalPosition = base.transform.localPosition;
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x0009D370 File Offset: 0x0009B570
	private void Update()
	{
		base.transform.localPosition = this.storedLocalPosition;
		Vector3 vector = Camera.main.WorldToViewportPoint(base.transform.position);
		if (vector.x < this.bounds.x)
		{
			vector.x = this.bounds.x;
		}
		if (vector.x > this.bounds.y)
		{
			vector.x = this.bounds.y;
		}
		if (vector.y < this.bounds.z)
		{
			vector.y = this.bounds.z;
		}
		if (vector.y > this.bounds.w)
		{
			vector.y = this.bounds.w;
		}
		base.transform.position = Camera.main.ViewportToWorldPoint(vector);
	}

	// Token: 0x04000D64 RID: 3428
	[SerializeField]
	private Vector4 bounds;

	// Token: 0x04000D65 RID: 3429
	private Vector3 storedLocalPosition;
}
