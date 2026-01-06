using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014D RID: 333
public class OVerworld_NPCDestination : MonoBehaviour
{
	// Token: 0x06000D2D RID: 3373 RVA: 0x00084F6D File Offset: 0x0008316D
	private void Awake()
	{
		OVerworld_NPCDestination.allDestinations.Add(this);
	}

	// Token: 0x06000D2E RID: 3374 RVA: 0x00084F7A File Offset: 0x0008317A
	private void OnDestroy()
	{
		OVerworld_NPCDestination.allDestinations.Remove(this);
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x00084F88 File Offset: 0x00083188
	private void Start()
	{
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x00084F8A File Offset: 0x0008318A
	private void Update()
	{
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x00084F8C File Offset: 0x0008318C
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x04000AAD RID: 2733
	public static List<OVerworld_NPCDestination> allDestinations = new List<OVerworld_NPCDestination>();

	// Token: 0x04000AAE RID: 2734
	public Overworld_NPC.Phase actionHere;

	// Token: 0x04000AAF RID: 2735
	[SerializeField]
	public float radius;
}
