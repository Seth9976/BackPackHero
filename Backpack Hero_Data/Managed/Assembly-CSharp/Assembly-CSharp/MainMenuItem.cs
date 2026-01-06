using System;
using UnityEngine;

// Token: 0x0200010A RID: 266
public class MainMenuItem : MonoBehaviour
{
	// Token: 0x0600091C RID: 2332 RVA: 0x0005EC94 File Offset: 0x0005CE94
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.ChooseSprite();
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0005ECA8 File Offset: 0x0005CEA8
	public void ChooseSprite()
	{
		this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0005ECCC File Offset: 0x0005CECC
	private void Update()
	{
		base.transform.position += this.direction * Time.deltaTime * this.speed;
		if (base.transform.position.x <= -8f && this.direction.x < 0f)
		{
			this.ChooseSprite();
			base.transform.position += Vector3.right * 16f;
			base.transform.position += Vector3.up * 16f;
		}
		if (base.transform.position.x >= 8f && this.direction.x > 0f)
		{
			this.ChooseSprite();
			base.transform.position -= Vector3.right * 16f;
			base.transform.position -= Vector3.up * 16f;
		}
	}

	// Token: 0x04000733 RID: 1843
	[SerializeField]
	private Sprite[] sprites;

	// Token: 0x04000734 RID: 1844
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000735 RID: 1845
	[SerializeField]
	private Vector3 direction;

	// Token: 0x04000736 RID: 1846
	[SerializeField]
	private float speed = 3f;
}
