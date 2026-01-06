using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Boss : MonoBehaviour
{
	// Token: 0x0600001C RID: 28 RVA: 0x0000265F File Offset: 0x0000085F
	private void OnEnable()
	{
		Boss.instance = this;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002667 File Offset: 0x00000867
	public void OnDisable()
	{
		Boss.instance = null;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000266F File Offset: 0x0000086F
	private void Start()
	{
		this.timeToAttack = 2f;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000267C File Offset: 0x0000087C
	private void Update()
	{
		if (SpecialLevelProgressComputer.fullyChargedComputers.Count >= 4)
		{
			this.Die();
		}
		this.timeToAttack -= Time.deltaTime * TimeManager.instance.currentTimeScale;
		if (this.timeToAttack > 0f)
		{
			return;
		}
		switch (Random.Range(0, 3))
		{
		case 0:
			this.animator.PlayAnimation("attack1");
			this.timeToAttack = Random.Range(1f, 1.6f);
			this.CreateBulletsInCircle();
			break;
		case 1:
			this.animator.PlayAnimation("attack2");
			this.timeToAttack = Random.Range(2f, 2.6f);
			this.CreateBulletsInSpiral();
			break;
		case 2:
			this.animator.PlayAnimation("attack3");
			this.timeToAttack = Random.Range(2f, 2.6f);
			this.CreateBulletAtPlayer();
			break;
		}
		base.StartCoroutine(this.ReturnToIdle());
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002778 File Offset: 0x00000978
	private void Die()
	{
		if (this.isDying)
		{
			return;
		}
		this.isDying = true;
		base.StartCoroutine(this.DyingRoutine());
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002797 File Offset: 0x00000997
	private IEnumerator DyingRoutine()
	{
		GameCamera.instance.SetTarget(base.transform);
		Player.instance.Win();
		GameManager.instance.WinLevel();
		CameraShaker.instance.Shake(0.5f, 0.4f);
		yield return new WaitForSeconds(0.7f);
		CameraShaker.instance.Shake(0.5f, 0.4f);
		yield return new WaitForSeconds(0.7f);
		CameraShaker.instance.Shake(0.7f, 0.75f);
		yield return new WaitForSeconds(75f);
		base.GetComponent<Destructible>().Explode();
		yield break;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000027A6 File Offset: 0x000009A6
	private IEnumerator ReturnToIdle()
	{
		float time = 0.64f;
		while (time > 0f)
		{
			time -= Time.deltaTime * TimeManager.instance.currentTimeScale;
			yield return null;
		}
		this.animator.PlayAnimation("idle");
		yield break;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000027B8 File Offset: 0x000009B8
	private void CreateBulletsInCircle()
	{
		float num = (float)Random.Range(0, 360);
		for (int i = 0; i < 8; i++)
		{
			float num2 = num + (float)(i * 45);
			Vector3 vector = new Vector3(Mathf.Cos(num2 * 0.017453292f), Mathf.Sin(num2 * 0.017453292f), 0f);
			Object.Instantiate<GameObject>(this.laserPrefab, base.transform.position, Quaternion.identity).GetComponent<Bullet>().SetVelocity(vector);
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002838 File Offset: 0x00000A38
	private void CreateBulletAtPlayer()
	{
		for (int i = 0; i < 4; i++)
		{
			Vector3 normalized = (Player.instance.transform.position - base.transform.position).normalized;
			GameObject gameObject = Object.Instantiate<GameObject>(this.laserPrefab, base.transform.position, Quaternion.identity);
			Vector3 vector = normalized + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f);
			gameObject.GetComponent<Bullet>().SetVelocity(vector);
		}
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000028D9 File Offset: 0x00000AD9
	private void CreateBulletsInSpiral()
	{
		if (this.attackCoroutine != null)
		{
			base.StopCoroutine(this.attackCoroutine);
		}
		this.attackCoroutine = base.StartCoroutine(this.CreateBulletsInSpiralR());
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002901 File Offset: 0x00000B01
	private IEnumerator CreateBulletsInSpiralR()
	{
		float angle = (float)Random.Range(0, 360);
		int num;
		for (int i = 0; i < 12; i = num + 1)
		{
			Vector3 vector = new Vector3(Mathf.Cos(angle * 0.017453292f), Mathf.Sin(angle * 0.017453292f), 0f);
			Object.Instantiate<GameObject>(this.laserPrefab, base.transform.position, Quaternion.identity).GetComponent<Bullet>().SetVelocity(vector);
			angle += 30f;
			float time = 0.125f;
			while (time > 0f)
			{
				time -= Time.deltaTime * TimeManager.instance.currentTimeScale;
				yield return null;
			}
			num = i;
		}
		yield break;
	}

	// Token: 0x04000010 RID: 16
	public static Boss instance;

	// Token: 0x04000011 RID: 17
	[SerializeField]
	private GameObject laserPrefab;

	// Token: 0x04000012 RID: 18
	[SerializeField]
	private SimpleAnimator animator;

	// Token: 0x04000013 RID: 19
	private Coroutine attackCoroutine;

	// Token: 0x04000014 RID: 20
	private float timeToAttack = 1f;

	// Token: 0x04000015 RID: 21
	private bool isDying;
}
