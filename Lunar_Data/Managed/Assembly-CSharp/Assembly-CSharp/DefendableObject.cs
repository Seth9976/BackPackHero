using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class DefendableObject : MonoBehaviour
{
	// Token: 0x06000136 RID: 310 RVA: 0x0000745C File Offset: 0x0000565C
	private void OnEnable()
	{
		DefendableObject.defendableObjects.Add(this);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x00007469 File Offset: 0x00005669
	private void OnDisable()
	{
		DefendableObject.defendableObjects.Remove(this);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00007477 File Offset: 0x00005677
	private void Start()
	{
		this.maxhealth = this.health;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007488 File Offset: 0x00005688
	public static DefendableObject GetClosestDefendableObject(Vector2 pos, float maxDistance = 3.4028235E+38f)
	{
		DefendableObject defendableObject = null;
		float num = maxDistance;
		foreach (DefendableObject defendableObject2 in DefendableObject.defendableObjects)
		{
			float num2 = Vector2.Distance(pos, defendableObject2.transform.position);
			if (num2 < num)
			{
				defendableObject = defendableObject2;
				num = num2;
			}
		}
		return defendableObject;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000074FC File Offset: 0x000056FC
	public void TakeDamage(float damage)
	{
		if (damage > 0f && this.robotAnimatorController != null)
		{
			this.robotAnimatorController.TakeDamage();
		}
		this.health -= damage;
		if (this.health <= 0f)
		{
			HealthBarMaster.instance.TakeDamage(999f);
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040000EB RID: 235
	public static List<DefendableObject> defendableObjects = new List<DefendableObject>();

	// Token: 0x040000EC RID: 236
	[NonSerialized]
	public float maxhealth = 10f;

	// Token: 0x040000ED RID: 237
	[SerializeField]
	private RobotAnimatorController robotAnimatorController;

	// Token: 0x040000EE RID: 238
	public float health = 10f;
}
