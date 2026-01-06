using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class EnemyBullet : MonoBehaviour
{
	// Token: 0x06000181 RID: 385 RVA: 0x00008D98 File Offset: 0x00006F98
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHitBox component = other.GetComponent<PlayerHitBox>();
			if (component)
			{
				component.AddEnemy(base.gameObject);
			}
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00008DD0 File Offset: 0x00006FD0
	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			PlayerHitBox component = other.GetComponent<PlayerHitBox>();
			if (component)
			{
				component.RemoveEnemy(base.gameObject);
			}
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00008E05 File Offset: 0x00007005
	private void OnEnable()
	{
		EnemyBullet.enemyBullets.Add(this);
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00008E12 File Offset: 0x00007012
	private void OnDisable()
	{
		EnemyBullet.enemyBullets.Remove(this);
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00008E20 File Offset: 0x00007020
	public static void DestroyAllBullets()
	{
		foreach (EnemyBullet enemyBullet in new List<EnemyBullet>(EnemyBullet.enemyBullets))
		{
			Object.Destroy(enemyBullet.gameObject);
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00008E7C File Offset: 0x0000707C
	private void DestroyBullet(Vector2 position)
	{
		this.bulletParticles.gameObject.transform.position = position;
		this.bulletParticles.gameObject.SetActive(true);
		this.bulletParticles.transform.SetParent(null);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000138 RID: 312
	private static List<EnemyBullet> enemyBullets = new List<EnemyBullet>();

	// Token: 0x04000139 RID: 313
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x0400013A RID: 314
	[SerializeField]
	private GameObject bulletParticles;
}
