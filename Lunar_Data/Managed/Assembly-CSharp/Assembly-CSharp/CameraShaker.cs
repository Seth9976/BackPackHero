using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class CameraShaker : MonoBehaviour
{
	// Token: 0x06000058 RID: 88 RVA: 0x00003685 File Offset: 0x00001885
	private void OnEnable()
	{
		CameraShaker.instance = this;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000368D File Offset: 0x0000188D
	private void OnDisable()
	{
		CameraShaker.instance = null;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00003695 File Offset: 0x00001895
	private void Update()
	{
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00003697 File Offset: 0x00001897
	public void Shake(float duration, float magnitude)
	{
		if (this.shakeCoroutine != null)
		{
			base.StopCoroutine(this.shakeCoroutine);
		}
		this.shakeCoroutine = base.StartCoroutine(this.ShakeCoroutine(duration, magnitude));
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000036C1 File Offset: 0x000018C1
	private IEnumerator ShakeCoroutine(float duration, float magnitude)
	{
		Vector3 originalPos = this.cameraTransform.localPosition;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			float num = Random.Range(-1f, 1f) * magnitude;
			float num2 = Random.Range(-1f, 1f) * magnitude;
			this.cameraTransform.localPosition = new Vector3(originalPos.x + num, originalPos.y + num2, originalPos.z);
			elapsed += Time.deltaTime;
			yield return null;
		}
		this.cameraTransform.localPosition = originalPos;
		yield break;
	}

	// Token: 0x0400003B RID: 59
	public static CameraShaker instance;

	// Token: 0x0400003C RID: 60
	[SerializeField]
	private Transform cameraTransform;

	// Token: 0x0400003D RID: 61
	private Coroutine shakeCoroutine;
}
