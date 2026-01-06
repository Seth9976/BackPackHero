using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class EnemyHitter : MonoBehaviour
{
	// Token: 0x0600018B RID: 395 RVA: 0x00008F34 File Offset: 0x00007134
	private void OnTriggerEnter2D(Collider2D other)
	{
		Destructible component = other.GetComponent<Destructible>();
		if (component && !this.destructiblesHit.Contains(component))
		{
			this.damageDealer.ApplyEffects(other.gameObject);
			this.destructiblesHit.Add(component);
		}
	}

	// Token: 0x0400013D RID: 317
	[SerializeField]
	private DamageDealer damageDealer;

	// Token: 0x0400013E RID: 318
	[SerializeField]
	private List<Destructible> destructiblesHit = new List<Destructible>();
}
