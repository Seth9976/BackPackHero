using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000045 RID: 69
public class GameEventHandler : MonoBehaviour
{
	// Token: 0x060001EF RID: 495 RVA: 0x0000A9EA File Offset: 0x00008BEA
	private void OnEnable()
	{
		GameEventHandler.gameEventHandlers.Add(this);
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000A9F7 File Offset: 0x00008BF7
	private void OnDisable()
	{
		GameEventHandler.gameEventHandlers.Remove(this);
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000AA08 File Offset: 0x00008C08
	public static void CallEvent(GameEventHandler.GameEvent.EventType eventType)
	{
		foreach (GameEventHandler gameEventHandler in new List<GameEventHandler>(GameEventHandler.gameEventHandlers))
		{
			foreach (GameEventHandler.GameEvent gameEvent in gameEventHandler.gameEvents)
			{
				if (gameEvent.eventType == eventType && Random.value <= gameEvent.chanceItWillHappen)
				{
					gameEvent.unityEvent.Invoke();
				}
			}
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0000AAB4 File Offset: 0x00008CB4
	public void DestroyThis()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400017E RID: 382
	private static List<GameEventHandler> gameEventHandlers = new List<GameEventHandler>();

	// Token: 0x0400017F RID: 383
	public List<GameEventHandler.GameEvent> gameEvents = new List<GameEventHandler.GameEvent>();

	// Token: 0x020000DE RID: 222
	[Serializable]
	public class GameEvent
	{
		// Token: 0x0400043D RID: 1085
		public GameEventHandler.GameEvent.EventType eventType;

		// Token: 0x0400043E RID: 1086
		public float chanceItWillHappen = 1f;

		// Token: 0x0400043F RID: 1087
		public UnityEvent unityEvent;

		// Token: 0x0200012A RID: 298
		public enum EventType
		{
			// Token: 0x0400054A RID: 1354
			onCardPlayed
		}
	}
}
