using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class MessageSpawner : MonoBehaviour
{
	// Token: 0x060002A7 RID: 679 RVA: 0x0000D99C File Offset: 0x0000BB9C
	private void Start()
	{
		this.messageInstance = Object.Instantiate<GameObject>(this.messagePrefab, base.transform.position, Quaternion.identity, CanvasManager.instance.masterContentScaler);
		ReplacementText componentInChildren = this.messageInstance.GetComponentInChildren<ReplacementText>();
		if (componentInChildren)
		{
			componentInChildren.SetKey(this.messageKey);
		}
		this.messageInstance.transform.SetAsFirstSibling();
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000DA04 File Offset: 0x0000BC04
	private void Update()
	{
		if (this.messageInstance)
		{
			if (this.messageInstancePosition)
			{
				this.messageInstance.transform.position = Camera.main.WorldToScreenPoint(this.messageInstancePosition.position);
				return;
			}
			this.messageInstance.transform.position = Camera.main.WorldToScreenPoint(base.transform.position + Vector3.up * 0.5f);
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000DA8A File Offset: 0x0000BC8A
	private void OnDestroy()
	{
		if (this.messageInstance)
		{
			Object.Destroy(this.messageInstance);
		}
	}

	// Token: 0x040001F6 RID: 502
	[SerializeField]
	private GameObject messagePrefab;

	// Token: 0x040001F7 RID: 503
	[SerializeField]
	private Transform messageInstancePosition;

	// Token: 0x040001F8 RID: 504
	[SerializeField]
	private GameObject messageInstance;

	// Token: 0x040001F9 RID: 505
	[SerializeField]
	private string messageKey;
}
