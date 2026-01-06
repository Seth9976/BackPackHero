using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class VisualEffectOnItemUse : MonoBehaviour
{
	// Token: 0x06001100 RID: 4352 RVA: 0x000A0F44 File Offset: 0x0009F144
	public void PlayEffect()
	{
		if (this.effectType == VisualEffectOnItemUse.EffectType.text)
		{
			string text = this.textKeys[Random.Range(0, this.textKeys.Count)];
			PopUpManager.main.CreatePopUp(LangaugeManager.main.GetTextByKey(text), Player.main.transform.position + new Vector3(1.2f, 0.7f, 0f));
		}
	}

	// Token: 0x04000DD8 RID: 3544
	public VisualEffectOnItemUse.EffectType effectType;

	// Token: 0x04000DD9 RID: 3545
	public List<string> textKeys;

	// Token: 0x04000DDA RID: 3546
	[SerializeField]
	private GameObject damageIndicatorForText;

	// Token: 0x02000482 RID: 1154
	public enum EffectType
	{
		// Token: 0x04001A73 RID: 6771
		text
	}
}
