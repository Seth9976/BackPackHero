using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class EventDependent : MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x00009AF1 File Offset: 0x00007CF1
	private void Start()
	{
		this.Setup();
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00009AF9 File Offset: 0x00007CF9
	private void Update()
	{
		this.Setup();
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00009B01 File Offset: 0x00007D01
	private void Setup()
	{
		if (this.setup)
		{
			return;
		}
		if (!EventManager.instance)
		{
			return;
		}
		this.setup = true;
		if (EventManager.instance.eventType != this.eventType)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040000F1 RID: 241
	[SerializeField]
	private EventManager.EventType eventType;

	// Token: 0x040000F2 RID: 242
	private bool setup;
}
