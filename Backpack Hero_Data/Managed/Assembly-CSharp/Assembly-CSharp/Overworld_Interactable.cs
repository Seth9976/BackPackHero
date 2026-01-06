using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000145 RID: 325
public class Overworld_Interactable : Overworld_InteractiveObject
{
	// Token: 0x06000C73 RID: 3187 RVA: 0x0007FEC4 File Offset: 0x0007E0C4
	private void OnEnable()
	{
		Overworld_Interactable.interactables.Add(this);
	}

	// Token: 0x06000C74 RID: 3188 RVA: 0x0007FED1 File Offset: 0x0007E0D1
	private void OnDisable()
	{
		Overworld_Interactable.interactables.Remove(this);
	}

	// Token: 0x06000C75 RID: 3189 RVA: 0x0007FEE0 File Offset: 0x0007E0E0
	private void Start()
	{
		this.collider = base.GetComponent<Collider2D>();
		foreach (Overworld_Interactable.interactable_Action interactable_Action in this.interactable_Actions)
		{
			if (interactable_Action.keyEvent == null)
			{
				interactable_Action.keyEvent = new UnityEvent();
				Overworld_BuildingInterfaceLauncher component = base.GetComponent<Overworld_BuildingInterfaceLauncher>();
				if (component)
				{
					interactable_Action.keyEvent.AddListener(new UnityAction(component.Interact));
				}
				Overworld_ShopKeeper component2 = base.GetComponent<Overworld_ShopKeeper>();
				if (component2)
				{
					interactable_Action.keyEvent.AddListener(new UnityAction(component2.OpenShopDirect));
				}
				Overworld_Chest component3 = base.GetComponent<Overworld_Chest>();
				if (component3)
				{
					interactable_Action.keyEvent.AddListener(new UnityAction(component3.Interact));
				}
			}
		}
	}

	// Token: 0x06000C76 RID: 3190 RVA: 0x0007FFCC File Offset: 0x0007E1CC
	public static Overworld_Interactable GetClosestInteractable(Vector2 position)
	{
		return Overworld_Interactable.GetClosestInteractable(position, float.MaxValue);
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0007FFD9 File Offset: 0x0007E1D9
	private Vector2 GetClosestPointOnCollider(Vector2 position)
	{
		if (this.collider == null)
		{
			return base.transform.position;
		}
		return this.collider.ClosestPoint(position);
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x00080008 File Offset: 0x0007E208
	public static Overworld_Interactable GetClosestInteractable(Vector2 position, float maxDistance)
	{
		Overworld_Interactable overworld_Interactable = null;
		float num = float.MaxValue;
		foreach (Overworld_Interactable overworld_Interactable2 in Overworld_Interactable.interactables)
		{
			float num2 = Vector2.Distance(position, overworld_Interactable2.GetClosestPointOnCollider(position));
			if (num2 < num && num2 < maxDistance)
			{
				num = num2;
				overworld_Interactable = overworld_Interactable2;
			}
		}
		return overworld_Interactable;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0008007C File Offset: 0x0007E27C
	private static void SetAllAsNotSelected()
	{
		foreach (Overworld_Interactable overworld_Interactable in Overworld_Interactable.interactables)
		{
			overworld_Interactable.SetAsNotSelected();
		}
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x000800CC File Offset: 0x0007E2CC
	private void SetAsNotSelected()
	{
		this.isSelected = false;
		foreach (Overworld_Interactable.interactable_Action interactable_Action in this.interactable_Actions)
		{
			interactable_Action.isSelected = false;
		}
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00080124 File Offset: 0x0007E324
	public static List<Overworld_Interactable.ClosestOfType> GetClosestInteractableOfEachType(Vector2 position, float maxDistance)
	{
		Overworld_Interactable.SetAllAsNotSelected();
		List<Overworld_Interactable.ClosestOfType> list = new List<Overworld_Interactable.ClosestOfType>();
		foreach (Overworld_Interactable overworld_Interactable in Overworld_Interactable.interactables)
		{
			float num = Vector2.Distance(position, overworld_Interactable.GetClosestPointOnCollider(position));
			foreach (Overworld_Interactable.interactable_Action interactable_Action in overworld_Interactable.interactable_Actions)
			{
				bool flag = false;
				foreach (Overworld_Interactable.ClosestOfType closestOfType in list)
				{
					if (closestOfType.key == interactable_Action.key)
					{
						flag = true;
						if (num < closestOfType.closestDistance)
						{
							closestOfType.closestDistance = num;
							closestOfType.closestInteractable = overworld_Interactable;
							break;
						}
					}
				}
				if (!flag && num < maxDistance)
				{
					list.Add(new Overworld_Interactable.ClosestOfType
					{
						key = interactable_Action.key,
						closestInteractable = overworld_Interactable,
						closestDistance = num
					});
				}
			}
		}
		return list;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00080274 File Offset: 0x0007E474
	public override void Interact()
	{
		this.Interact(InputHandler.Key.A);
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00080280 File Offset: 0x0007E480
	public void Interact(InputHandler.Key key)
	{
		if (!Overworld_Manager.IsFreeToMove())
		{
			return;
		}
		foreach (Overworld_Interactable.interactable_Action interactable_Action in this.interactable_Actions)
		{
			if (interactable_Action.key == key)
			{
				interactable_Action.keyEvent.Invoke();
				break;
			}
		}
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x000802EC File Offset: 0x0007E4EC
	private void Update()
	{
	}

	// Token: 0x04000A12 RID: 2578
	private static List<Overworld_Interactable> interactables = new List<Overworld_Interactable>();

	// Token: 0x04000A13 RID: 2579
	private Collider2D collider;

	// Token: 0x04000A14 RID: 2580
	public InputHandler.Key key;

	// Token: 0x04000A15 RID: 2581
	public bool isSelected;

	// Token: 0x04000A16 RID: 2582
	public List<Overworld_Interactable.interactable_Action> interactable_Actions = new List<Overworld_Interactable.interactable_Action>();

	// Token: 0x020003F7 RID: 1015
	[ES3Serializable]
	[Serializable]
	public class interactable_Action
	{
		// Token: 0x0400176D RID: 5997
		public InputHandler.Key key;

		// Token: 0x0400176E RID: 5998
		[ES3Serializable]
		public UnityEvent keyEvent;

		// Token: 0x0400176F RID: 5999
		public bool isSelected;
	}

	// Token: 0x020003F8 RID: 1016
	public class ClosestOfType
	{
		// Token: 0x04001770 RID: 6000
		public Overworld_Interactable closestInteractable;

		// Token: 0x04001771 RID: 6001
		public float closestDistance = float.MaxValue;

		// Token: 0x04001772 RID: 6002
		public InputHandler.Key key;
	}
}
