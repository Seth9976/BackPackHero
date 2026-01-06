using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class ZombieBait : MonoBehaviour
{
	// Token: 0x0600049F RID: 1183 RVA: 0x000169F9 File Offset: 0x00014BF9
	private void OnEnable()
	{
		ZombieBait.zombieBaits.Add(this);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00016A06 File Offset: 0x00014C06
	private void OnDisable()
	{
		ZombieBait.zombieBaits.Remove(this);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00016A14 File Offset: 0x00014C14
	private void Start()
	{
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00016A16 File Offset: 0x00014C16
	private void Update()
	{
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00016A18 File Offset: 0x00014C18
	public static ZombieBait GetClosestZombieBaitNearBy(Vector2 position, float radius)
	{
		ZombieBait zombieBait = null;
		float num = radius;
		foreach (ZombieBait zombieBait2 in ZombieBait.zombieBaits)
		{
			float num2 = Vector2.Distance(position, zombieBait2.transform.position);
			if (num2 < radius && num2 < num)
			{
				num = num2;
				zombieBait = zombieBait2;
			}
		}
		return zombieBait;
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00016A90 File Offset: 0x00014C90
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x04000396 RID: 918
	[SerializeField]
	private float radius = 5f;

	// Token: 0x04000397 RID: 919
	private static List<ZombieBait> zombieBaits = new List<ZombieBait>();
}
