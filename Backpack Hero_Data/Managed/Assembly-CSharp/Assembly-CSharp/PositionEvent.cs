using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200008E RID: 142
public class PositionEvent : MonoBehaviour
{
	// Token: 0x06000318 RID: 792 RVA: 0x00012245 File Offset: 0x00010445
	private void Start()
	{
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00012248 File Offset: 0x00010448
	private void Update()
	{
		if (!this.destination)
		{
			return;
		}
		if (this.eventType == PositionEvent.EventType.ifYisGreaterThanDestination && base.transform.position.y > this.destination.position.y)
		{
			this.onPositionEvent.Invoke();
			base.enabled = false;
		}
	}

	// Token: 0x0400021B RID: 539
	[SerializeField]
	private PositionEvent.EventType eventType;

	// Token: 0x0400021C RID: 540
	[SerializeField]
	private UnityEvent onPositionEvent;

	// Token: 0x0400021D RID: 541
	[SerializeField]
	private Transform destination;

	// Token: 0x02000296 RID: 662
	public enum EventType
	{
		// Token: 0x04000FC4 RID: 4036
		ifYisGreaterThanDestination
	}
}
