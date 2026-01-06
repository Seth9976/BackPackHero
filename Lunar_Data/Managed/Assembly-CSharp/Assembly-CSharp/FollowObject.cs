using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class FollowObject : MonoBehaviour
{
	// Token: 0x060001DD RID: 477 RVA: 0x0000A1AC File Offset: 0x000083AC
	private void Update()
	{
		if (!this.objectToFollow)
		{
			return;
		}
		Vector3 vector = new Vector3(this.objectToFollow.position.x, this.objectToFollow.position.y, base.transform.position.z);
		base.transform.position = Vector3.Lerp(base.transform.position, vector, this.followSpeed * Time.deltaTime);
	}

	// Token: 0x04000170 RID: 368
	[SerializeField]
	public Transform objectToFollow;

	// Token: 0x04000171 RID: 369
	[SerializeField]
	private float followSpeed = 5f;
}
