using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class Grenade : MonoBehaviour
{
	// Token: 0x06000207 RID: 519 RVA: 0x0000AC97 File Offset: 0x00008E97
	private void Start()
	{
		this.startPosition = base.transform.position;
		base.StartCoroutine(this.ThrowGrenade());
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000ACBC File Offset: 0x00008EBC
	private void Update()
	{
		this.timeManagerCompo.Update(TimeManagerCompo.TimeScaleType.deltaTime);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000ACCA File Offset: 0x00008ECA
	private IEnumerator ThrowGrenade()
	{
		if (this.fadeIn)
		{
			this.grenadeSprite.color = new Color(1f, 1f, 1f, 0f);
		}
		float time = 0f;
		float duration = 0.25f;
		float yVelocityMagnitude = 3f;
		while (time < duration)
		{
			if (this.spinGrenade)
			{
				this.grenadeSprite.transform.Rotate(Vector3.forward, 360f * this.timeManagerCompo.GetTimeScale() * Time.deltaTime);
			}
			if (this.fadeIn)
			{
				this.grenadeSprite.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), time / duration);
			}
			this.shadowSprite.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), time / duration);
			float num = Mathf.Lerp(yVelocityMagnitude, 0f, time / duration);
			this.grenadeSprite.transform.localPosition += Vector3.up * num * this.timeManagerCompo.GetTimeScale() * Time.deltaTime;
			base.transform.position = Vector2.Lerp(this.startPosition, (this.startPosition + this.destination) / 2f, time / duration);
			time += Time.deltaTime * this.timeManagerCompo.GetTimeScale();
			yield return null;
		}
		time = 0f;
		while (time < duration)
		{
			if (this.spinGrenade)
			{
				this.grenadeSprite.transform.Rotate(Vector3.forward, 360f * Time.deltaTime * this.timeManagerCompo.GetTimeScale());
			}
			this.shadowSprite.color = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0f), time / duration);
			float num2 = Mathf.Lerp(0f, yVelocityMagnitude, time / duration);
			this.grenadeSprite.transform.localPosition += Vector3.down * num2 * this.timeManagerCompo.GetTimeScale() * Time.deltaTime;
			base.transform.position = Vector2.Lerp((this.startPosition + this.destination) / 2f, this.destination, time / duration);
			time += Time.deltaTime * this.timeManagerCompo.GetTimeScale();
			yield return null;
		}
		this.Explode();
		yield break;
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000ACDC File Offset: 0x00008EDC
	private void Explode()
	{
		SoundManager.instance.PlaySFX("bulletHit", double.PositiveInfinity);
		if (this.explosionPrefab)
		{
			Object.Instantiate<GameObject>(this.explosionPrefab, new Vector3(base.transform.position.x, base.transform.position.y, this.explosionPrefab.transform.position.z), Quaternion.identity, RoomManager.instance.roomContents);
		}
		if (this.destroyParticles)
		{
			this.destroyParticles.gameObject.SetActive(true);
			this.destroyParticles.transform.SetParent(null);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000AD9D File Offset: 0x00008F9D
	private void DestroyBullet(Vector2 position)
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400018C RID: 396
	[SerializeField]
	private TimeManagerCompo timeManagerCompo;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	private bool spinGrenade;

	// Token: 0x0400018E RID: 398
	[SerializeField]
	private GameObject explosionPrefab;

	// Token: 0x0400018F RID: 399
	[SerializeField]
	private SpriteRenderer grenadeSprite;

	// Token: 0x04000190 RID: 400
	[SerializeField]
	private SpriteRenderer shadowSprite;

	// Token: 0x04000191 RID: 401
	[SerializeField]
	private ParticleSystem destroyParticles;

	// Token: 0x04000192 RID: 402
	[SerializeField]
	private bool fadeIn;

	// Token: 0x04000193 RID: 403
	[NonSerialized]
	public Vector2 destination;

	// Token: 0x04000194 RID: 404
	private Vector2 startPosition;
}
