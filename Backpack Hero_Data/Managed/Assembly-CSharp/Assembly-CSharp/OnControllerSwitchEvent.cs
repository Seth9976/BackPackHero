using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000077 RID: 119
public class OnControllerSwitchEvent : MonoBehaviour
{
	// Token: 0x06000267 RID: 615 RVA: 0x0000EB5C File Offset: 0x0000CD5C
	public static void CallAllEvents()
	{
		OnControllerSwitchEvent[] array = Object.FindObjectsOfType<OnControllerSwitchEvent>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].CallEvent();
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000EB85 File Offset: 0x0000CD85
	private void CallEvent()
	{
		this.onControllerSwitchEvent.Invoke();
	}

	// Token: 0x0400019A RID: 410
	[SerializeField]
	private UnityEvent onControllerSwitchEvent;
}
