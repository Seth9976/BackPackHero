using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class ConfirmationBox : MonoBehaviour
{
	// Token: 0x0600034C RID: 844 RVA: 0x00010BE9 File Offset: 0x0000EDE9
	public void Show(string message, ConfirmationBox.OnConfirm onConfirm, ConfirmationBox.OnCancel onCancel)
	{
		this.messageText.SetKey(message);
		this.onConfirm = onConfirm;
		this.onCancel = onCancel;
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00010C05 File Offset: 0x0000EE05
	public void Confirm()
	{
		ConfirmationBox.OnConfirm onConfirm = this.onConfirm;
		if (onConfirm != null)
		{
			onConfirm();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00010C23 File Offset: 0x0000EE23
	public void Cancel()
	{
		ConfirmationBox.OnCancel onCancel = this.onCancel;
		if (onCancel != null)
		{
			onCancel();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000286 RID: 646
	public ConfirmationBox.OnConfirm onConfirm;

	// Token: 0x04000287 RID: 647
	public ConfirmationBox.OnCancel onCancel;

	// Token: 0x04000288 RID: 648
	[SerializeField]
	private ReplacementText messageText;

	// Token: 0x020000FE RID: 254
	// (Invoke) Token: 0x06000586 RID: 1414
	public delegate void OnConfirm();

	// Token: 0x020000FF RID: 255
	// (Invoke) Token: 0x0600058A RID: 1418
	public delegate void OnCancel();
}
