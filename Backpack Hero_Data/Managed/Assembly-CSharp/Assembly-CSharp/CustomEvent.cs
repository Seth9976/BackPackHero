using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000021 RID: 33
public class CustomEvent : CustomInputHandler
{
	// Token: 0x060000D6 RID: 214 RVA: 0x00006E84 File Offset: 0x00005084
	private void Start()
	{
		if (this.dungeonEvent)
		{
			this.dungeonEvent.cannotWalkAwayFrom = true;
		}
		this.player = Player.main;
		this.gameManager = GameManager.main;
		this.gameFlowManager = GameFlowManager.main;
		if (base.transform.parent.CompareTag("InteractiveVisual"))
		{
			base.transform.parent.localPosition = new Vector3(base.transform.parent.localPosition.x, base.transform.parent.localPosition.y, 2f);
		}
		this.digitalInputSelectOnButton = base.GetComponentInParent<InputHandler>();
		if (this.objectToCreateOnView)
		{
			Object.Instantiate<GameObject>(this.objectToCreateOnView, base.transform.position, Quaternion.identity);
		}
		UnityEvent unityEvent = this.onSpawn;
		if (unityEvent == null)
		{
			return;
		}
		unityEvent.Invoke();
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00006F6B File Offset: 0x0000516B
	private void OnMouseUpAsButton()
	{
		if (DigitalCursor.main.controlStyle != DigitalCursor.ControlStyle.cursor)
		{
			return;
		}
		this.Click();
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00006F80 File Offset: 0x00005180
	public override void OnPressStart(string keyName, bool overrideKeyName)
	{
		if (keyName == "confirm" || overrideKeyName)
		{
			this.Click();
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00006F98 File Offset: 0x00005198
	public void Click()
	{
		if (this.gameManager.travelling || this.isOpen)
		{
			return;
		}
		if (SingleUI.IsViewingPopUp())
		{
			return;
		}
		this.isOpen = true;
		if (this.dungeonEvent)
		{
			this.dungeonEvent.FinishEvent();
		}
		base.StartCoroutine(this.Interact());
		Object.Destroy(this.digitalInputSelectOnButton);
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00006FFA File Offset: 0x000051FA
	public virtual IEnumerator Interact()
	{
		this.dungeonEvent.FinishEvent();
		yield break;
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00007009 File Offset: 0x00005209
	public virtual void InteractFromDialogue(int num)
	{
	}

	// Token: 0x04000078 RID: 120
	[SerializeField]
	protected UnityEvent onSpawn;

	// Token: 0x04000079 RID: 121
	[SerializeField]
	protected UnityEvent onInteract;

	// Token: 0x0400007A RID: 122
	[SerializeField]
	protected UnityEvent onCombatStart;

	// Token: 0x0400007B RID: 123
	public DungeonEvent dungeonEvent;

	// Token: 0x0400007C RID: 124
	[SerializeField]
	protected Sprite locketSprite;

	// Token: 0x0400007D RID: 125
	[SerializeField]
	protected GameObject objectToCreateOnView;

	// Token: 0x0400007E RID: 126
	[SerializeField]
	protected GameObject enemyToCreate;

	// Token: 0x0400007F RID: 127
	[SerializeField]
	protected Animator animator;

	// Token: 0x04000080 RID: 128
	[SerializeField]
	protected InputHandler digitalInputSelectOnButton;

	// Token: 0x04000081 RID: 129
	[SerializeField]
	protected Animator parentAnimator;

	// Token: 0x04000082 RID: 130
	protected GameManager gameManager;

	// Token: 0x04000083 RID: 131
	protected GameFlowManager gameFlowManager;

	// Token: 0x04000084 RID: 132
	protected Player player;

	// Token: 0x04000085 RID: 133
	public bool isOpen;
}
