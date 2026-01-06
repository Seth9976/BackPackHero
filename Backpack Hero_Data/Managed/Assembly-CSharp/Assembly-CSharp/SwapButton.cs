using System;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class SwapButton : MonoBehaviour
{
	// Token: 0x0600108A RID: 4234 RVA: 0x0009D9F0 File Offset: 0x0009BBF0
	private void Start()
	{
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0009D9F2 File Offset: 0x0009BBF2
	private void Update()
	{
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0009D9F4 File Offset: 0x0009BBF4
	public void SwapItems()
	{
		OverworldInventory[] array = Object.FindObjectsOfType<OverworldInventory>();
		if (array.Length != 2)
		{
			Debug.LogError("There should be exactly 2 inventories in the scene");
		}
		OverworldInventory overworldInventory = array[0];
		OverworldInventory overworldInventory2 = array[0];
		foreach (OverworldInventory overworldInventory3 in array)
		{
			if (Vector2.Dot(overworldInventory3.transform.position - base.transform.position, this.direction) > Vector2.Dot(overworldInventory.transform.position - base.transform.position, this.direction))
			{
				overworldInventory = overworldInventory3;
			}
			else
			{
				overworldInventory2 = overworldInventory3;
			}
		}
		if (this.movingToPlayer)
		{
			overworldInventory.TakeAll();
			return;
		}
		if (overworldInventory2.HasItemsThatAreValidAndDontHaveResarch(overworldInventory))
		{
			overworldInventory.TakeAllResearch(true);
			return;
		}
		overworldInventory.TakeAll();
	}

	// Token: 0x04000D7A RID: 3450
	[SerializeField]
	private Vector2 direction;

	// Token: 0x04000D7B RID: 3451
	public bool movingToPlayer;
}
