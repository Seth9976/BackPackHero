using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000118 RID: 280
public class ScrollCard : MonoBehaviour
{
	// Token: 0x06000980 RID: 2432 RVA: 0x0006152C File Offset: 0x0005F72C
	private void Start()
	{
		Card componentInChildren = base.GetComponentInChildren<Card>();
		if (componentInChildren)
		{
			this.myCard = componentInChildren.gameObject;
		}
		this.myScrollbar = base.GetComponentInChildren<Scrollbar>();
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x00061560 File Offset: 0x0005F760
	private void Update()
	{
		if (this.myScrollbar && this.myScrollbar.size < 1f)
		{
			DigitalCursor.main.SelectUIElementIfNothingSelected(this.myScrollbar.gameObject);
		}
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = true;
		}
		if (!this.resizedYet && this.myCard)
		{
			this.myCard.transform.GetComponentInParent<VerticalLayoutGroup>().enabled = false;
			this.myCard.transform.GetComponentInParent<VerticalLayoutGroup>().enabled = true;
			this.resizedYet = true;
		}
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00061600 File Offset: 0x0005F800
	public void GetCard(Card card)
	{
		card.MakeStuck();
		card.transform.SetParent(this.content);
		card.transform.localScale = Vector3.one;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00061629 File Offset: 0x0005F829
	public void Close()
	{
		if (GameManager.main)
		{
			GameManager.main.viewingEvent = false;
		}
		Object.Destroy(base.gameObject);
		ContextMenuManager.main.currentState = ContextMenuManager.CurrentState.noMenu;
	}

	// Token: 0x04000794 RID: 1940
	private GameObject myCard;

	// Token: 0x04000795 RID: 1941
	private bool resizedYet;

	// Token: 0x04000796 RID: 1942
	private Scrollbar myScrollbar;

	// Token: 0x04000797 RID: 1943
	[SerializeField]
	private Transform content;
}
