using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class EventManager : MonoBehaviour
{
	// Token: 0x060001B5 RID: 437 RVA: 0x000099C1 File Offset: 0x00007BC1
	private void OnEnable()
	{
		EventManager.instance = this;
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x000099C9 File Offset: 0x00007BC9
	private void OnDisable()
	{
		EventManager.instance = null;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x000099D1 File Offset: 0x00007BD1
	private void Start()
	{
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x000099D3 File Offset: 0x00007BD3
	private void Update()
	{
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x000099D5 File Offset: 0x00007BD5
	public void CreateEvent()
	{
		if (this.originalEventPanel)
		{
			Object.Destroy(this.originalEventPanel);
		}
		Object.Instantiate<GameObject>(this.eventResultPanel, CanvasManager.instance.masterContentScaler);
		this.eventResultPanel = null;
	}

	// Token: 0x04000154 RID: 340
	public static EventManager instance;

	// Token: 0x04000155 RID: 341
	public GameObject selectedCardFromEvent;

	// Token: 0x04000156 RID: 342
	public GameObject originalEventPanel;

	// Token: 0x04000157 RID: 343
	public GameObject eventResultPanel;
}
