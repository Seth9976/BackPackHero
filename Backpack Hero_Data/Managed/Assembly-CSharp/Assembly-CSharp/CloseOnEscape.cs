using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200000E RID: 14
public class CloseOnEscape : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x000032C8 File Offset: 0x000014C8
	private void Start()
	{
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000032CA File Offset: 0x000014CA
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.OnClose.Invoke();
		}
	}

	// Token: 0x0400001D RID: 29
	[SerializeField]
	private UnityEvent OnClose;
}
