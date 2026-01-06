using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000078 RID: 120
public class OnStartAction : MonoBehaviour
{
	// Token: 0x0600026A RID: 618 RVA: 0x0000EB9A File Offset: 0x0000CD9A
	private void Start()
	{
		UnityEvent unityEvent = this.onStartAction;
		if (unityEvent == null)
		{
			return;
		}
		unityEvent.Invoke();
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000EBAC File Offset: 0x0000CDAC
	private void OnEnable()
	{
		UnityEvent unityEvent = this.onEnableAction;
		if (unityEvent == null)
		{
			return;
		}
		unityEvent.Invoke();
	}

	// Token: 0x0400019B RID: 411
	[SerializeField]
	private UnityEvent onStartAction;

	// Token: 0x0400019C RID: 412
	[SerializeField]
	private UnityEvent onEnableAction;
}
