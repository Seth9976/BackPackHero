using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000020 RID: 32
public class CreditsScroll : MonoBehaviour
{
	// Token: 0x060000D2 RID: 210 RVA: 0x00006D8F File Offset: 0x00004F8F
	private void Start()
	{
		base.StartCoroutine(this.UpdateCredits());
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00006D9E File Offset: 0x00004F9E
	private IEnumerator UpdateCredits()
	{
		for (;;)
		{
			yield return new WaitForSeconds(0.1f);
			this.c.UpdateText();
		}
		yield break;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00006DB0 File Offset: 0x00004FB0
	private void Update()
	{
		float num = this.speed;
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Return) || Input.GetMouseButton(0) || Input.GetMouseButton(1) || DigitalCursor.main.GetInputHold("confirm") || DigitalCursor.main.GetInputHold("cancel"))
		{
			this.exptendedSpeed += Time.deltaTime * 7f;
			num = this.exptendedSpeed * this.speed;
		}
		else
		{
			this.exptendedSpeed = 20f;
		}
		this.credits.position += Vector3.up * Time.deltaTime * num;
	}

	// Token: 0x04000074 RID: 116
	[SerializeField]
	private Transform credits;

	// Token: 0x04000075 RID: 117
	[SerializeField]
	private float speed = 1f;

	// Token: 0x04000076 RID: 118
	[SerializeField]
	private float exptendedSpeed = 1f;

	// Token: 0x04000077 RID: 119
	[SerializeField]
	private Credits c;
}
