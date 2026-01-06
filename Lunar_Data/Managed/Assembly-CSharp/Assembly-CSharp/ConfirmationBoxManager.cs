using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class ConfirmationBoxManager : MonoBehaviour
{
	// Token: 0x06000350 RID: 848 RVA: 0x00010C49 File Offset: 0x0000EE49
	private void OnEnable()
	{
		ConfirmationBoxManager.instance = this;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00010C51 File Offset: 0x0000EE51
	private void OnDisable()
	{
		ConfirmationBoxManager.instance = null;
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00010C59 File Offset: 0x0000EE59
	public void ShowConfirmationBox(string message, ConfirmationBox.OnConfirm onConfirm, ConfirmationBox.OnCancel onCancel)
	{
		Object.Instantiate<GameObject>(this.confirmationBoxPrefab, CanvasManager.instance.masterContentScaler).GetComponent<ConfirmationBox>().Show(message, onConfirm, onCancel);
	}

	// Token: 0x04000289 RID: 649
	[SerializeField]
	private GameObject confirmationBoxPrefab;

	// Token: 0x0400028A RID: 650
	public static ConfirmationBoxManager instance;
}
