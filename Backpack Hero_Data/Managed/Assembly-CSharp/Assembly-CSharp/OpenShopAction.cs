using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;

// Token: 0x02000041 RID: 65
[CreateMenu("Town Actions/Open Shop", 0)]
public class OpenShopAction : ActionDataBase
{
	// Token: 0x0600012A RID: 298 RVA: 0x00008054 File Offset: 0x00006254
	public override void OnStart()
	{
		Overworld_ConversationManager.main.StartCoroutine(Overworld_ConversationManager.main.ShrinkAndDisableOverTime(0f));
		this.atlas = Overworld_ConversationManager.main.interactiveObject.GetComponentInChildren<Overworld_ShopKeeper>().OpenShop(true);
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000808B File Offset: 0x0000628B
	public override ActionStatus OnUpdate()
	{
		if (this.atlas != null && !this.atlas.finished)
		{
			return ActionStatus.Continue;
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000BF RID: 191
	private StoreAtlas atlas;
}
