using System;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class Turret : MonoBehaviour
{
	// Token: 0x0600043B RID: 1083 RVA: 0x00015187 File Offset: 0x00013387
	private void Start()
	{
		PassiveManager.instance.CreatePassiveEffectFromPrefab(this.passiveEffect, null, "", this.lengthOfEffect, null, new PassiveEffect.PassiveEffectDeactivateDelegate(this.DestroyThis));
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x000151B2 File Offset: 0x000133B2
	private void Update()
	{
		this.AimTurretHead();
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x000151BC File Offset: 0x000133BC
	private void AimTurretHead()
	{
		if (!this.turretHead)
		{
			return;
		}
		Enemy closest = Enemy.GetClosest(base.transform.position, this.playerShooter.maxDistance);
		if (!closest)
		{
			return;
		}
		Vector2 vector = closest.transform.position - this.turretHead.position;
		float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
		this.turretHead.rotation = Quaternion.Euler(new Vector3(0f, 0f, num + 90f));
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00015265 File Offset: 0x00013465
	private void DestroyThis()
	{
		this.destroyEffect.gameObject.SetActive(true);
		this.destroyEffect.transform.SetParent(null);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000340 RID: 832
	[SerializeField]
	private Transform turretHead;

	// Token: 0x04000341 RID: 833
	[SerializeField]
	private PlayerShooter playerShooter;

	// Token: 0x04000342 RID: 834
	[SerializeField]
	private GameObject passiveEffect;

	// Token: 0x04000343 RID: 835
	[SerializeField]
	private GameObject destroyEffect;

	// Token: 0x04000344 RID: 836
	[SerializeField]
	private float lengthOfEffect = 5f;
}
