using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000144 RID: 324
public class Overworld_GiftReceiver : MonoBehaviour
{
	// Token: 0x06000C6D RID: 3181 RVA: 0x0007FDC5 File Offset: 0x0007DFC5
	private void Start()
	{
		this.npc = base.GetComponent<Overworld_NPC>();
	}

	// Token: 0x06000C6E RID: 3182 RVA: 0x0007FDD3 File Offset: 0x0007DFD3
	private void Update()
	{
	}

	// Token: 0x06000C6F RID: 3183 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
	public void ReceiveGift(Item2 item)
	{
		foreach (Overworld_GiftReceiver.GiftAction giftAction in this.giftActions)
		{
			if (item.name.Trim().ToLower() == giftAction.giftName.Trim().ToLower() || giftAction.giftName.Trim().ToLower() == "any")
			{
				this.TakeAction(giftAction);
				return;
			}
		}
		this.npc.ShowShortText(this.noResponseText);
	}

	// Token: 0x06000C70 RID: 3184 RVA: 0x0007FE84 File Offset: 0x0007E084
	public void MarkMetaProgress(int m)
	{
		MetaProgressSaveManager.main.AddMetaProgressMarker((MetaProgressSaveManager.MetaProgressMarker)m);
	}

	// Token: 0x06000C71 RID: 3185 RVA: 0x0007FE9E File Offset: 0x0007E09E
	protected virtual void TakeAction(Overworld_GiftReceiver.GiftAction giftAction)
	{
		this.npc.ShowShortText(giftAction.responseText);
		giftAction.action.Invoke();
	}

	// Token: 0x04000A0F RID: 2575
	[SerializeField]
	public List<Overworld_GiftReceiver.GiftAction> giftActions;

	// Token: 0x04000A10 RID: 2576
	public string noResponseText;

	// Token: 0x04000A11 RID: 2577
	private Overworld_NPC npc;

	// Token: 0x020003F6 RID: 1014
	[Serializable]
	public class GiftAction
	{
		// Token: 0x0400176A RID: 5994
		public string giftName;

		// Token: 0x0400176B RID: 5995
		public string responseText;

		// Token: 0x0400176C RID: 5996
		public UnityEvent action;
	}
}
