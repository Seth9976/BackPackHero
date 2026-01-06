using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class PassiveManager : MonoBehaviour
{
	// Token: 0x060002DA RID: 730 RVA: 0x0000EC4C File Offset: 0x0000CE4C
	private void OnEnable()
	{
		PassiveManager.instance = this;
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000EC54 File Offset: 0x0000CE54
	private void OnDisable()
	{
		if (PassiveManager.instance == this)
		{
			PassiveManager.instance = null;
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000EC6C File Offset: 0x0000CE6C
	public void CreatePassiveEffectFromPrefab(GameObject passiveEffectPrefab, Sprite sprite, string description, float duration, List<GameObject> objectsEffected = null, PassiveEffect.PassiveEffectDeactivateDelegate passiveEffectDeactivateDelegate = null)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(passiveEffectPrefab, base.transform);
		PassiveEffect component = gameObject.GetComponent<PassiveEffect>();
		component.ActivatePassiveEffect(sprite, duration, null, passiveEffectDeactivateDelegate);
		component.objectsEffected = objectsEffected;
		this.storedEffects.Add(gameObject);
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000ECAB File Offset: 0x0000CEAB
	public void CreatePassiveEffect(Sprite sprite, string description, float duration, List<GameObject> objectsEffected = null, PassiveEffect.PassiveEffectDeactivateDelegate passiveEffectDeactivateDelegate = null)
	{
		PassiveEffect component = Object.Instantiate<GameObject>(this.passiveEffectPrefab, base.transform).GetComponent<PassiveEffect>();
		component.ActivatePassiveEffect(sprite, duration, null, passiveEffectDeactivateDelegate);
		component.objectsEffected = objectsEffected;
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000ECD8 File Offset: 0x0000CED8
	public void ClearAllPassiveEffects()
	{
		foreach (object obj in base.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
	}

	// Token: 0x0400022B RID: 555
	public static PassiveManager instance;

	// Token: 0x0400022C RID: 556
	[SerializeField]
	public GameObject passiveEffectPrefab;

	// Token: 0x0400022D RID: 557
	[SerializeField]
	public GameObject timerlessPassiveEffectPrefab;

	// Token: 0x0400022E RID: 558
	[SerializeField]
	private List<GameObject> storedEffects = new List<GameObject>();
}
