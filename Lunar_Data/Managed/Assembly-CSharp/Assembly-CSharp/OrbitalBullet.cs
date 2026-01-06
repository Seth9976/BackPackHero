using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class OrbitalBullet : MonoBehaviour
{
	// Token: 0x060002CC RID: 716 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
	private void Start()
	{
		this.currentAngle = Mathf.Atan2(base.transform.position.y - Player.instance.transform.position.y, base.transform.position.x - Player.instance.transform.position.x);
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000E838 File Offset: 0x0000CA38
	private void Update()
	{
		this.currentAngle += Time.deltaTime * TimeManager.instance.currentTimeScale * this.speed;
		if (this.currentAngle > 6.2831855f)
		{
			this.currentAngle -= 6.2831855f;
		}
		base.transform.position = Player.instance.transform.position + new Vector3(Mathf.Cos(this.currentAngle), Mathf.Sin(this.currentAngle), 0f) * this.distance;
		base.transform.rotation = Quaternion.Euler(0f, 0f, (this.currentAngle + 90f) * 57.29578f);
	}

	// Token: 0x04000215 RID: 533
	private float currentAngle;

	// Token: 0x04000216 RID: 534
	[SerializeField]
	private float speed = 5f;

	// Token: 0x04000217 RID: 535
	[SerializeField]
	private float distance = 1.5f;

	// Token: 0x04000218 RID: 536
	[SerializeField]
	private float damage = 1f;
}
