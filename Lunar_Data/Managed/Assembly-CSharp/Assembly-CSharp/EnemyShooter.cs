using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class EnemyShooter : MonoBehaviour
{
	// Token: 0x0600018D RID: 397 RVA: 0x00008F8E File Offset: 0x0000718E
	private void Start()
	{
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00008F90 File Offset: 0x00007190
	private void Update()
	{
		this.ConsiderShooting();
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00008F98 File Offset: 0x00007198
	private void ConsiderShooting()
	{
		if (!this.bulletPrefab)
		{
			return;
		}
		this.timeSinceLastShot -= Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (this.timeSinceLastShot <= 0f)
		{
			this.Shoot();
			this.timeSinceLastShot = Random.Range(this.shootInterval.x, this.shootInterval.y);
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00009004 File Offset: 0x00007204
	private void Shoot()
	{
		if (Vector2.Distance(base.transform.position, Player.instance.transform.position) > 3f)
		{
			Vector2 normalized = (Player.instance.transform.position - base.transform.position).normalized;
			Object.Instantiate<GameObject>(this.bulletPrefab, base.transform.position, Quaternion.identity).GetComponent<Bullet>().SetVelocity(normalized);
		}
	}

	// Token: 0x0400013F RID: 319
	[SerializeField]
	private GameObject bulletPrefab;

	// Token: 0x04000140 RID: 320
	[SerializeField]
	private Vector2 shootInterval = new Vector2(1f, 2f);

	// Token: 0x04000141 RID: 321
	[SerializeField]
	private float timeSinceLastShot;
}
