using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000052 RID: 82
public class Map : MonoBehaviour
{
	// Token: 0x06000264 RID: 612 RVA: 0x0000CAEC File Offset: 0x0000ACEC
	private void OnEnable()
	{
		if (Map.instance == null)
		{
			Map.instance = this;
		}
		this.inputActions = new InputActions();
		this.inputActions.Enable();
		Singleton.ControlType controlType = Singleton.instance.controlType;
		if (controlType != Singleton.ControlType.Xbox)
		{
			return;
		}
		this.inputActions.Default.Confirm.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.AcceptRoom();
		};
		this.inputActions.Default.Movement.performed += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = ctx.ReadValue<Vector2>();
		};
		this.inputActions.Default.Movement.canceled += delegate(InputAction.CallbackContext ctx)
		{
			this.movementVector = Vector2.zero;
		};
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
	private void OnDisable()
	{
		if (Map.instance == this)
		{
			Map.instance = null;
		}
		this.inputActions.Disable();
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
	private void Start()
	{
		this.mapSelector.transform.position = this.mapPlayer.currentEvent.transform.position;
		this.mapSelector.SetSelectedObject(this.mapPlayer.currentEvent.gameObject);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0000CC14 File Offset: 0x0000AE14
	private void Update()
	{
		if (this.movingRoutine != null)
		{
			return;
		}
		if (this.movementVector.magnitude > 0.5f && !this.hasAcceptedInput)
		{
			MapEvent nextEventInDirection = MapEvent.GetNextEventInDirection(MapSelector.instance.GetCurrentEvent(), this.movementVector);
			if (nextEventInDirection)
			{
				MapSelector.instance.SetSelectedObject(nextEventInDirection.gameObject);
				this.hasAcceptedInput = true;
			}
		}
		if (this.movementVector.magnitude < 0.25f)
		{
			this.hasAcceptedInput = false;
		}
	}

	// Token: 0x06000268 RID: 616 RVA: 0x0000CC94 File Offset: 0x0000AE94
	public void AcceptRoom()
	{
		bool flag = false;
		if (!this.mapPlayer.currentEvent || this.mapPlayer.currentEvent.connectedEvents.Contains(MapSelector.instance.GetCurrentEvent()) || flag)
		{
			this.mapPlayer.currentEvent = MapSelector.instance.GetCurrentEvent();
			base.StartCoroutine(this.MovingRoutine());
			return;
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0000CCFE File Offset: 0x0000AEFE
	private IEnumerator MovingRoutine()
	{
		yield return new WaitForSeconds(0.5f);
		if (this.mapPlayer.currentEvent)
		{
			this.mapPlayer.currentEvent.CreateEvent();
			if (this.mapPlayer.currentEvent.eventPrefab.GetComponent<Room>())
			{
				base.gameObject.SetActive(false);
			}
		}
		this.movingRoutine = null;
		yield break;
	}

	// Token: 0x040001C4 RID: 452
	public static Map instance;

	// Token: 0x040001C5 RID: 453
	[SerializeField]
	private SingleUI singleUI;

	// Token: 0x040001C6 RID: 454
	[SerializeField]
	private MapSelector mapSelector;

	// Token: 0x040001C7 RID: 455
	[SerializeField]
	private MapPlayer mapPlayer;

	// Token: 0x040001C8 RID: 456
	[SerializeField]
	private InputActions inputActions;

	// Token: 0x040001C9 RID: 457
	[SerializeField]
	private Transform eventsParent;

	// Token: 0x040001CA RID: 458
	private Vector2 movementVector;

	// Token: 0x040001CB RID: 459
	private bool hasAcceptedInput;

	// Token: 0x040001CC RID: 460
	private Coroutine movingRoutine;
}
