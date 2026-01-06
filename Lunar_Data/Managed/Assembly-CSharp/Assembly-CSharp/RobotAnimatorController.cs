using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class RobotAnimatorController : MonoBehaviour
{
	// Token: 0x0600037D RID: 893 RVA: 0x000116E8 File Offset: 0x0000F8E8
	public void TakeDamage()
	{
		this.timeSinceTookDamage = 0.25f;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000116F8 File Offset: 0x0000F8F8
	private void Update()
	{
		if (Player.instance.isWinning)
		{
			this.simpleAnimator.PlayAnimation("win");
			return;
		}
		if (this.timeSinceTookDamage > 0f)
		{
			this.timeSinceTookDamage -= Time.deltaTime * TimeManager.instance.currentTimeScale;
			this.simpleAnimator.PlayAnimation("takingDamage");
			return;
		}
		if (this.defendableObject.health > this.defendableObject.maxhealth * 0.5f)
		{
			this.simpleAnimator.PlayAnimation("idle");
			return;
		}
		this.simpleAnimator.PlayAnimation("damaged");
	}

	// Token: 0x040002A6 RID: 678
	[SerializeField]
	private DefendableObject defendableObject;

	// Token: 0x040002A7 RID: 679
	[SerializeField]
	private SimpleAnimator simpleAnimator;

	// Token: 0x040002A8 RID: 680
	private float timeSinceTookDamage;
}
