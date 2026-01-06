using System;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class FakeGold : MonoBehaviour
{
	// Token: 0x060008DB RID: 2267 RVA: 0x0005CB5F File Offset: 0x0005AD5F
	private void Start()
	{
	}

	// Token: 0x060008DC RID: 2268 RVA: 0x0005CB64 File Offset: 0x0005AD64
	private void Update()
	{
		if (!this.dest)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.dest.position, 14f * Time.deltaTime);
		if (Vector3.Distance(base.transform.position, this.dest.position) < 0.1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040006FD RID: 1789
	public Transform dest;
}
