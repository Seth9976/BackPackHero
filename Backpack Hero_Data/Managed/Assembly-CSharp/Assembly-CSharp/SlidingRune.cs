using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class SlidingRune : MonoBehaviour
{
	// Token: 0x060009A5 RID: 2469 RVA: 0x000623B2 File Offset: 0x000605B2
	private void Start()
	{
		this.itemMovement = base.GetComponent<ItemMovement>();
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x000623C0 File Offset: 0x000605C0
	private void Update()
	{
		if (this.itemMovement.inGrid)
		{
			foreach (GameObject gameObject in this.slidingButtons)
			{
				gameObject.SetActive(true);
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
			}
		}
	}

	// Token: 0x040007E1 RID: 2017
	[SerializeField]
	private List<GameObject> slidingButtons;

	// Token: 0x040007E2 RID: 2018
	private ItemMovement itemMovement;

	// Token: 0x040007E3 RID: 2019
	public bool used;
}
