using System;
using TMPro;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class ConfirmationManager : MonoBehaviour
{
	// Token: 0x06000086 RID: 134 RVA: 0x000054AF File Offset: 0x000036AF
	private void Awake()
	{
		if (ConfirmationManager.instance == null)
		{
			ConfirmationManager.instance = this;
			return;
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000054D0 File Offset: 0x000036D0
	private void OnDestroy()
	{
		if (ConfirmationManager.instance == this)
		{
			ConfirmationManager.instance = null;
		}
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000054E5 File Offset: 0x000036E5
	public static void CreateConfirmation(string message, string description, Func<Action> delegateFunction)
	{
		ConfirmationManager.instance.CreateConfirmationInternal(message, delegateFunction, null, description);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000054F5 File Offset: 0x000036F5
	public static void CreateConfirmation(string message, Func<Action> delegateFunction)
	{
		ConfirmationManager.instance.CreateConfirmationInternal(message, delegateFunction, null, "");
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00005509 File Offset: 0x00003709
	public static void CreateConfirmation(string message, Func<Action> delegateFunction, ItemMovement im)
	{
		ConfirmationManager.instance.CreateConfirmationInternal(message, delegateFunction, im, "");
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000551D File Offset: 0x0000371D
	public void CreateConfirmationInstance(string message, string description, Func<Action> delegateFunction, ItemMovement im)
	{
		this.CreateConfirmationInternal(message, delegateFunction, im, description);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000552A File Offset: 0x0000372A
	public void CreateConfirmationInstance(string message, Func<Action> delegateFunction, ItemMovement im)
	{
		this.CreateConfirmationInternal(message, delegateFunction, im, "");
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000553C File Offset: 0x0000373C
	private void CreateConfirmationInternal(string message, Func<Action> delegateFunction, ItemMovement im, string description = "")
	{
		GameObject gameObject = Object.Instantiate<GameObject>(this.confirmationPrefab, GameObject.FindGameObjectWithTag("UI Canvas").transform);
		Confirm component = gameObject.GetComponent<Confirm>();
		component.DelegateFunction = delegateFunction;
		gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
		if (message.Length > 0)
		{
			if (description != null && description.Length > 0)
			{
				component.UpdateText(message, description);
			}
			else
			{
				component.UpdateText(message);
			}
		}
		if (im)
		{
			GameObject gameObject2 = Object.Instantiate<GameObject>(im.cardPrefab, Vector3.zero, Quaternion.identity);
			im.ApplyCardToItem(gameObject2, im.GetComponent<Item2>(), null, false);
			Card component2 = gameObject2.GetComponent<Card>();
			component2.MakeStuck();
			component2.deleteOnDeactivate = false;
			component.AddCardToPanel(gameObject2);
		}
	}

	// Token: 0x0400004D RID: 77
	public static ConfirmationManager instance;

	// Token: 0x0400004E RID: 78
	[SerializeField]
	private GameObject confirmationPrefab;
}
