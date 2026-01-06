using System;
using UnityEngine;

// Token: 0x0200011D RID: 285
public class SlidingRuneButton : MonoBehaviour
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x00062434 File Offset: 0x00060634
	private void Start()
	{
		this.satchel = Object.FindObjectOfType<Satchel>();
		this.myItem = base.GetComponentInParent<Item2>();
		this.gameFlowManager = GameFlowManager.main;
		this.slidingRune = base.GetComponentInParent<SlidingRune>();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00062464 File Offset: 0x00060664
	private void Update()
	{
	}

	// Token: 0x040007E4 RID: 2020
	private Satchel satchel;

	// Token: 0x040007E5 RID: 2021
	[SerializeField]
	private Vector2 direction;

	// Token: 0x040007E6 RID: 2022
	private GameFlowManager gameFlowManager;

	// Token: 0x040007E7 RID: 2023
	private SlidingRune slidingRune;

	// Token: 0x040007E8 RID: 2024
	private Item2 myItem;
}
