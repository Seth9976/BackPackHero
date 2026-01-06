using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class CreateMenu : MonoBehaviour
{
	// Token: 0x060000CA RID: 202 RVA: 0x00006BAF File Offset: 0x00004DAF
	private void Update()
	{
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00006BB4 File Offset: 0x00004DB4
	public void CreateMenuCommand()
	{
		SoundManager.main.PlaySFX("menuBlip");
		Canvas componentInParent = base.GetComponentInParent<Canvas>();
		Object.Instantiate<GameObject>(this.menuPrefab, componentInParent.transform);
	}

	// Token: 0x0400006E RID: 110
	[SerializeField]
	private GameObject menuPrefab;
}
