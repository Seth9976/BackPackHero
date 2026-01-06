using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class LightningStrike : MonoBehaviour
{
	// Token: 0x06000261 RID: 609 RVA: 0x0000CA5C File Offset: 0x0000AC5C
	private void Start()
	{
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0000CA60 File Offset: 0x0000AC60
	private void Update()
	{
		this.time += Time.deltaTime;
		this.lightningSprite.color = Color.Lerp(this.lightningSprite.color, new Color(1f, 1f, 1f, 0f), this.time / 0.25f);
		if (this.time >= this.timeInterval)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x040001C1 RID: 449
	[SerializeField]
	private SpriteRenderer lightningSprite;

	// Token: 0x040001C2 RID: 450
	[SerializeField]
	private float timeInterval = 0.25f;

	// Token: 0x040001C3 RID: 451
	private float time;
}
