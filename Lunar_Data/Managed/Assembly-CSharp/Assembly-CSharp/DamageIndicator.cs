using System;
using TMPro;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class DamageIndicator : MonoBehaviour
{
	// Token: 0x06000112 RID: 274 RVA: 0x00006B1C File Offset: 0x00004D1C
	public void ShowDamage(int damage)
	{
		this.rb.velocity = new Vector2(Random.Range(-this.velocity.x, this.velocity.x), this.velocity.y);
		this.text.text = damage.ToString();
		if (this.scalable)
		{
			this.text.fontSize = Mathf.Lerp(4f, 7.5f, (float)(damage - 6) / 20f);
			this.thisColor = Color.Lerp(Color.white, Color.red, (float)(damage - 6) / 20f);
			this.text.color = this.thisColor;
		}
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00006BD0 File Offset: 0x00004DD0
	private void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > this.delay)
		{
			float num = this.time - this.delay;
			this.text.color = Color.Lerp(this.thisColor, new Color(this.thisColor.r, this.thisColor.g, this.thisColor.b, 0f), num / this.fadeOutTime);
			if (num > this.fadeOutTime)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x040000CF RID: 207
	[SerializeField]
	private TextMeshPro text;

	// Token: 0x040000D0 RID: 208
	[SerializeField]
	private Rigidbody2D rb;

	// Token: 0x040000D1 RID: 209
	[SerializeField]
	private Vector2 velocity;

	// Token: 0x040000D2 RID: 210
	[SerializeField]
	private float delay = 0.2f;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	private float fadeOutTime = 1f;

	// Token: 0x040000D4 RID: 212
	[SerializeField]
	private float time;

	// Token: 0x040000D5 RID: 213
	[SerializeField]
	private bool scalable;

	// Token: 0x040000D6 RID: 214
	private Color thisColor;
}
