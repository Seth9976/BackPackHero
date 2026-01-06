using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class PopUpManager : MonoBehaviour
{
	// Token: 0x06000348 RID: 840 RVA: 0x00010BA4 File Offset: 0x0000EDA4
	private void OnEnable()
	{
		PopUpManager.instance = this;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00010BAC File Offset: 0x0000EDAC
	private void OnDisable()
	{
		PopUpManager.instance = null;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00010BB4 File Offset: 0x0000EDB4
	public void CreatePopUp(Vector2 position, string text)
	{
		Object.Instantiate<GameObject>(this.popUpPrefab, position, Quaternion.identity, CanvasManager.instance.transform).GetComponent<PopUp>().ShowText(text);
	}

	// Token: 0x04000284 RID: 644
	public static PopUpManager instance;

	// Token: 0x04000285 RID: 645
	[SerializeField]
	private GameObject popUpPrefab;
}
