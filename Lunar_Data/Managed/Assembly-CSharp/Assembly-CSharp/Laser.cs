using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class Laser : MonoBehaviour
{
	// Token: 0x0600025C RID: 604 RVA: 0x0000C928 File Offset: 0x0000AB28
	private void Update()
	{
		this.lineRenderer.SetPositions(new Vector3[]
		{
			this.origin.position,
			this.target.position
		});
		this.particleSystem.transform.position = this.target.position;
		this.particleSystem2.transform.position = new Vector3(this.origin.position.x, this.origin.position.y, this.origin.position.z - 1f);
		Vector3 vector = this.origin.position - this.target.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.particleSystem.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, num));
	}

	// Token: 0x040001B8 RID: 440
	[SerializeField]
	private LineRenderer lineRenderer;

	// Token: 0x040001B9 RID: 441
	[SerializeField]
	private ParticleSystem particleSystem;

	// Token: 0x040001BA RID: 442
	[SerializeField]
	private ParticleSystem particleSystem2;

	// Token: 0x040001BB RID: 443
	[SerializeField]
	private Transform laserOriginSprite;

	// Token: 0x040001BC RID: 444
	[SerializeField]
	public Transform origin;

	// Token: 0x040001BD RID: 445
	[SerializeField]
	public Transform target;
}
