using System;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class SetYPosition : MonoBehaviour
{
	// Token: 0x0600103E RID: 4158 RVA: 0x0009C5E8 File Offset: 0x0009A7E8
	private void Start()
	{
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x0009C5EC File Offset: 0x0009A7EC
	private void Update()
	{
		if (!GameManager.main || GameManager.main == null || !GameManager.main.player || !GameManager.main.player.chosenCharacter)
		{
			return;
		}
		float num = 0f;
		if (GameManager.main.player.chosenCharacter.yAdjustment.Count > Singleton.Instance.costumeNumber)
		{
			num = GameManager.main.player.chosenCharacter.yAdjustment[Singleton.Instance.costumeNumber];
		}
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, num, base.transform.localPosition.z);
	}
}
