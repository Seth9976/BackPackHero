using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000087 RID: 135
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/UISupport.html#setting-up-ui-input")]
	public class InputSystemUIInputModule : BaseInputModule
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000392D0 File Offset: 0x000374D0
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x000392D8 File Offset: 0x000374D8
		public bool deselectOnBackgroundClick
		{
			get
			{
				return this.m_DeselectOnBackgroundClick;
			}
			set
			{
				this.m_DeselectOnBackgroundClick = value;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000392E1 File Offset: 0x000374E1
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x000392E9 File Offset: 0x000374E9
		public UIPointerBehavior pointerBehavior
		{
			get
			{
				return this.m_PointerBehavior;
			}
			set
			{
				this.m_PointerBehavior = value;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000392F2 File Offset: 0x000374F2
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x000392FA File Offset: 0x000374FA
		public InputSystemUIInputModule.CursorLockBehavior cursorLockBehavior
		{
			get
			{
				return this.m_CursorLockBehavior;
			}
			set
			{
				this.m_CursorLockBehavior = value;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00039303 File Offset: 0x00037503
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x0003930B File Offset: 0x0003750B
		internal GameObject localMultiPlayerRoot
		{
			get
			{
				return this.m_LocalMultiPlayerRoot;
			}
			set
			{
				this.m_LocalMultiPlayerRoot = value;
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00039314 File Offset: 0x00037514
		public override void ActivateModule()
		{
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0003935C File Offset: 0x0003755C
		public override bool IsPointerOverGameObject(int pointerOrTouchId)
		{
			if (InputSystem.isProcessingEvents)
			{
				Debug.LogWarning("Calling IsPointerOverGameObject() from within event processing (such as from InputAction callbacks) will not work as expected; it will query UI state from the last frame");
			}
			int num = -1;
			if (pointerOrTouchId < 0)
			{
				if (this.m_CurrentPointerId != -1)
				{
					num = this.m_CurrentPointerIndex;
				}
				else if (this.m_PointerStates.length > 0)
				{
					num = 0;
				}
			}
			else
			{
				num = this.GetPointerStateIndexFor(pointerOrTouchId);
			}
			return num != -1 && this.m_PointerStates[num].eventData.pointerEnter != null;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000393D0 File Offset: 0x000375D0
		public RaycastResult GetLastRaycastResult(int pointerOrTouchId)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(pointerOrTouchId);
			if (pointerStateIndexFor == -1)
			{
				return default(RaycastResult);
			}
			return this.m_PointerStates[pointerStateIndexFor].eventData.pointerCurrentRaycast;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0003940C File Offset: 0x0003760C
		private RaycastResult PerformRaycast(ExtendedPointerEventData eventData)
		{
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			if (eventData.pointerType == UIPointerType.Tracked && TrackedDeviceRaycaster.s_Instances.length > 0)
			{
				for (int i = 0; i < TrackedDeviceRaycaster.s_Instances.length; i++)
				{
					TrackedDeviceRaycaster trackedDeviceRaycaster = TrackedDeviceRaycaster.s_Instances[i];
					this.m_RaycastResultCache.Clear();
					trackedDeviceRaycaster.PerformRaycast(eventData, this.m_RaycastResultCache);
					if (this.m_RaycastResultCache.Count > 0)
					{
						RaycastResult raycastResult = this.m_RaycastResultCache[0];
						this.m_RaycastResultCache.Clear();
						return raycastResult;
					}
				}
				return default(RaycastResult);
			}
			base.eventSystem.RaycastAll(eventData, this.m_RaycastResultCache);
			RaycastResult raycastResult2 = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			this.m_RaycastResultCache.Clear();
			return raycastResult2;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000394CC File Offset: 0x000376CC
		private void ProcessPointer(ref PointerModel state)
		{
			ExtendedPointerEventData eventData = state.eventData;
			UIPointerType pointerType = eventData.pointerType;
			if (pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked)
			{
				eventData.position = ((this.m_CursorLockBehavior == InputSystemUIInputModule.CursorLockBehavior.OutsideScreen) ? new Vector2(-1f, -1f) : new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
				eventData.delta = default(Vector2);
			}
			else if (pointerType == UIPointerType.Tracked)
			{
				Vector3 vector = state.worldPosition;
				Quaternion quaternion = state.worldOrientation;
				if (this.m_XRTrackingOrigin != null)
				{
					vector = this.m_XRTrackingOrigin.TransformPoint(vector);
					quaternion = this.m_XRTrackingOrigin.rotation * quaternion;
				}
				eventData.trackedDeviceOrientation = quaternion;
				eventData.trackedDevicePosition = vector;
			}
			else
			{
				eventData.delta = state.screenPosition - eventData.position;
				eventData.position = state.screenPosition;
			}
			eventData.Reset();
			eventData.pointerCurrentRaycast = this.PerformRaycast(eventData);
			if (pointerType == UIPointerType.Tracked && eventData.pointerCurrentRaycast.isValid)
			{
				Vector2 screenPosition = eventData.pointerCurrentRaycast.screenPosition;
				eventData.delta = screenPosition - eventData.position;
				eventData.position = eventData.pointerCurrentRaycast.screenPosition;
			}
			eventData.button = PointerEventData.InputButton.Left;
			state.leftButton.CopyPressStateTo(eventData);
			this.ProcessPointerMovement(ref state, eventData);
			if (!state.changedThisFrame && (this.xrTrackingOrigin == null || state.pointerType != UIPointerType.Tracked))
			{
				return;
			}
			this.ProcessPointerButton(ref state.leftButton, eventData);
			this.ProcessPointerButtonDrag(ref state.leftButton, eventData);
			InputSystemUIInputModule.ProcessPointerScroll(ref state, eventData);
			eventData.button = PointerEventData.InputButton.Right;
			state.rightButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.rightButton, eventData);
			this.ProcessPointerButtonDrag(ref state.rightButton, eventData);
			eventData.button = PointerEventData.InputButton.Middle;
			state.middleButton.CopyPressStateTo(eventData);
			this.ProcessPointerButton(ref state.middleButton, eventData);
			this.ProcessPointerButtonDrag(ref state.middleButton, eventData);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000396C8 File Offset: 0x000378C8
		private bool PointerShouldIgnoreTransform(Transform t)
		{
			MultiplayerEventSystem multiplayerEventSystem = base.eventSystem as MultiplayerEventSystem;
			return multiplayerEventSystem != null && multiplayerEventSystem.playerRoot != null && !t.IsChildOf(multiplayerEventSystem.playerRoot.transform);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00039708 File Offset: 0x00037908
		private void ProcessPointerMovement(ref PointerModel pointer, ExtendedPointerEventData eventData)
		{
			GameObject gameObject = ((eventData.pointerType == UIPointerType.Touch && !pointer.leftButton.isPressed && !pointer.leftButton.wasReleasedThisFrame) ? null : eventData.pointerCurrentRaycast.gameObject);
			this.ProcessPointerMovement(eventData, gameObject);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00039754 File Offset: 0x00037954
		private void ProcessPointerMovement(ExtendedPointerEventData eventData, GameObject currentPointerTarget)
		{
			bool flag = eventData.IsPointerMoving();
			if (flag)
			{
				for (int i = 0; i < eventData.hovered.Count; i++)
				{
					ExecuteEvents.Execute<IPointerMoveHandler>(eventData.hovered[i], eventData, ExecuteEvents.pointerMoveHandler);
				}
			}
			if (currentPointerTarget == null || eventData.pointerEnter == null)
			{
				for (int j = 0; j < eventData.hovered.Count; j++)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(eventData.hovered[j], eventData, ExecuteEvents.pointerExitHandler);
				}
				eventData.hovered.Clear();
				if (currentPointerTarget == null)
				{
					eventData.pointerEnter = null;
					return;
				}
			}
			if (eventData.pointerEnter == currentPointerTarget && currentPointerTarget)
			{
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(eventData.pointerEnter, currentPointerTarget);
			Transform transform = ((gameObject != null) ? gameObject.transform : null);
			if (eventData.pointerEnter != null)
			{
				Transform transform2 = eventData.pointerEnter.transform;
				while (transform2 != null && transform2 != transform)
				{
					ExecuteEvents.Execute<IPointerExitHandler>(transform2.gameObject, eventData, ExecuteEvents.pointerExitHandler);
					eventData.hovered.Remove(transform2.gameObject);
					transform2 = transform2.parent;
				}
			}
			eventData.pointerEnter = currentPointerTarget;
			if (currentPointerTarget != null)
			{
				Transform transform3 = currentPointerTarget.transform;
				while (transform3 != null && transform3 != transform && !this.PointerShouldIgnoreTransform(transform3))
				{
					ExecuteEvents.Execute<IPointerEnterHandler>(transform3.gameObject, eventData, ExecuteEvents.pointerEnterHandler);
					if (flag)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(transform3.gameObject, eventData, ExecuteEvents.pointerMoveHandler);
					}
					eventData.hovered.Add(transform3.gameObject);
					transform3 = transform3.parent;
				}
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00039908 File Offset: 0x00037B08
		private void ProcessPointerButton(ref PointerModel.ButtonState button, PointerEventData eventData)
		{
			GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
			if (gameObject != null && this.PointerShouldIgnoreTransform(gameObject.transform))
			{
				return;
			}
			if (button.wasPressedThisFrame)
			{
				button.pressTime = InputRuntime.s_Instance.unscaledGameTime;
				eventData.delta = Vector2.zero;
				eventData.dragging = false;
				eventData.pressPosition = eventData.position;
				eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;
				eventData.eligibleForClick = true;
				eventData.useDragThreshold = true;
				GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(gameObject);
				if (eventHandler != base.eventSystem.currentSelectedGameObject && (eventHandler != null || this.m_DeselectOnBackgroundClick))
				{
					base.eventSystem.SetSelectedGameObject(null, eventData);
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, eventData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				button.clickedOnSameGameObject = gameObject2 == eventData.lastPress && button.pressTime - eventData.clickTime <= 0.3f;
				if (eventData.clickCount > 0 && !button.clickedOnSameGameObject)
				{
					eventData.clickCount = 0;
					eventData.clickTime = 0f;
				}
				eventData.pointerPress = gameObject2;
				eventData.rawPointerPress = gameObject;
				eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (button.wasReleasedThisFrame)
			{
				GameObject eventHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				bool flag = eventData.pointerPress == eventHandler2 && eventData.eligibleForClick;
				if (flag)
				{
					if (button.clickedOnSameGameObject)
					{
						int num = eventData.clickCount + 1;
						eventData.clickCount = num;
					}
					else
					{
						eventData.clickCount = 1;
					}
					eventData.clickTime = InputRuntime.s_Instance.unscaledGameTime;
				}
				ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
				if (flag)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
				}
				else if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, eventData, ExecuteEvents.dropHandler);
				}
				eventData.eligibleForClick = false;
				eventData.pointerPress = null;
				eventData.rawPointerPress = null;
				if (eventData.dragging && eventData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
				}
				eventData.dragging = false;
				eventData.pointerDrag = null;
				button.ignoreNextClick = false;
			}
			button.CopyPressStateFrom(eventData);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00039B74 File Offset: 0x00037D74
		private void ProcessPointerButtonDrag(ref PointerModel.ButtonState button, ExtendedPointerEventData eventData)
		{
			if (!eventData.IsPointerMoving() || (eventData.pointerType == UIPointerType.MouseOrPen && Cursor.lockState == CursorLockMode.Locked) || eventData.pointerDrag == null)
			{
				return;
			}
			if (!eventData.dragging && (!eventData.useDragThreshold || (double)(eventData.pressPosition - eventData.position).sqrMagnitude >= (double)base.eventSystem.pixelDragThreshold * (double)base.eventSystem.pixelDragThreshold * (double)((eventData.pointerType == UIPointerType.Tracked) ? this.m_TrackedDeviceDragThresholdMultiplier : 1f)))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
				eventData.dragging = true;
			}
			if (eventData.dragging)
			{
				if (eventData.pointerPress != eventData.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
					eventData.eligibleForClick = false;
					eventData.pointerPress = null;
					eventData.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
				button.CopyPressStateFrom(eventData);
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00039C7C File Offset: 0x00037E7C
		private static void ProcessPointerScroll(ref PointerModel pointer, PointerEventData eventData)
		{
			Vector2 scrollDelta = pointer.scrollDelta;
			if (!Mathf.Approximately(scrollDelta.sqrMagnitude, 0f))
			{
				eventData.scrollDelta = scrollDelta;
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerEnter), eventData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00039CC4 File Offset: 0x00037EC4
		internal void ProcessNavigation(ref NavigationModel navigationState)
		{
			bool flag = false;
			if (base.eventSystem.currentSelectedGameObject != null)
			{
				BaseEventData baseEventData = this.GetBaseEventData();
				ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
				flag = baseEventData.used;
			}
			if (!base.eventSystem.sendNavigationEvents)
			{
				return;
			}
			Vector2 move = navigationState.move;
			if (!flag && (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f)))
			{
				float unscaledGameTime = InputRuntime.s_Instance.unscaledGameTime;
				Vector2 move2 = navigationState.move;
				MoveDirection moveDirection = MoveDirection.None;
				if (move2.sqrMagnitude > 0f)
				{
					if (Mathf.Abs(move2.x) > Mathf.Abs(move2.y))
					{
						moveDirection = ((move2.x > 0f) ? MoveDirection.Right : MoveDirection.Left);
					}
					else
					{
						moveDirection = ((move2.y > 0f) ? MoveDirection.Up : MoveDirection.Down);
					}
				}
				if (moveDirection != this.m_NavigationState.lastMoveDirection)
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
				if (moveDirection != MoveDirection.None)
				{
					bool flag2 = true;
					if (this.m_NavigationState.consecutiveMoveCount != 0)
					{
						if (this.m_NavigationState.consecutiveMoveCount > 1)
						{
							flag2 = unscaledGameTime > this.m_NavigationState.lastMoveTime + this.moveRepeatRate;
						}
						else
						{
							flag2 = unscaledGameTime > this.m_NavigationState.lastMoveTime + this.moveRepeatDelay;
						}
					}
					if (flag2)
					{
						AxisEventData axisEventData = this.m_NavigationState.eventData;
						if (axisEventData == null)
						{
							axisEventData = new ExtendedAxisEventData(base.eventSystem);
							this.m_NavigationState.eventData = axisEventData;
						}
						axisEventData.Reset();
						axisEventData.moveVector = move2;
						axisEventData.moveDir = moveDirection;
						if (this.IsMoveAllowed(axisEventData))
						{
							ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
							flag = axisEventData.used;
							this.m_NavigationState.consecutiveMoveCount = this.m_NavigationState.consecutiveMoveCount + 1;
							this.m_NavigationState.lastMoveTime = unscaledGameTime;
							this.m_NavigationState.lastMoveDirection = moveDirection;
						}
					}
				}
				else
				{
					this.m_NavigationState.consecutiveMoveCount = 0;
				}
			}
			else
			{
				this.m_NavigationState.consecutiveMoveCount = 0;
			}
			if (!flag && base.eventSystem.currentSelectedGameObject != null)
			{
				InputActionReference submitAction = this.m_SubmitAction;
				InputAction inputAction = ((submitAction != null) ? submitAction.action : null);
				InputActionReference cancelAction = this.m_CancelAction;
				InputAction inputAction2 = ((cancelAction != null) ? cancelAction.action : null);
				BaseEventData baseEventData2 = this.GetBaseEventData();
				if (inputAction2 != null && inputAction2.WasPressedThisFrame())
				{
					ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData2, ExecuteEvents.cancelHandler);
				}
				if (!baseEventData2.used && inputAction != null && inputAction.WasPressedThisFrame())
				{
					ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData2, ExecuteEvents.submitHandler);
				}
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00039F88 File Offset: 0x00038188
		private bool IsMoveAllowed(AxisEventData eventData)
		{
			if (this.m_LocalMultiPlayerRoot == null)
			{
				return true;
			}
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return true;
			}
			Selectable component = base.eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
			if (component == null)
			{
				return true;
			}
			Selectable selectable = null;
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				selectable = component.FindSelectableOnLeft();
				break;
			case MoveDirection.Up:
				selectable = component.FindSelectableOnUp();
				break;
			case MoveDirection.Right:
				selectable = component.FindSelectableOnRight();
				break;
			case MoveDirection.Down:
				selectable = component.FindSelectableOnDown();
				break;
			}
			return selectable == null || selectable.transform.IsChildOf(this.m_LocalMultiPlayerRoot.transform);
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0003A03A File Offset: 0x0003823A
		// (set) Token: 0x06000AC4 RID: 2756 RVA: 0x0003A042 File Offset: 0x00038242
		public float moveRepeatDelay
		{
			get
			{
				return this.m_MoveRepeatDelay;
			}
			set
			{
				this.m_MoveRepeatDelay = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x0003A04B File Offset: 0x0003824B
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x0003A053 File Offset: 0x00038253
		public float moveRepeatRate
		{
			get
			{
				return this.m_MoveRepeatRate;
			}
			set
			{
				this.m_MoveRepeatRate = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0003A05C File Offset: 0x0003825C
		private bool explictlyIgnoreFocus
		{
			get
			{
				return InputSystem.settings.backgroundBehavior == InputSettings.BackgroundBehavior.IgnoreFocus;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0003A06B File Offset: 0x0003826B
		private bool shouldIgnoreFocus
		{
			get
			{
				return this.explictlyIgnoreFocus || InputRuntime.s_Instance.runInBackground;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0003A081 File Offset: 0x00038281
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0003A089 File Offset: 0x00038289
		[Obsolete("'repeatRate' has been obsoleted; use 'moveRepeatRate' instead. (UnityUpgradable) -> moveRepeatRate", false)]
		public float repeatRate
		{
			get
			{
				return this.moveRepeatRate;
			}
			set
			{
				this.moveRepeatRate = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0003A092 File Offset: 0x00038292
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0003A09A File Offset: 0x0003829A
		[Obsolete("'repeatDelay' has been obsoleted; use 'moveRepeatDelay' instead. (UnityUpgradable) -> moveRepeatDelay", false)]
		public float repeatDelay
		{
			get
			{
				return this.moveRepeatDelay;
			}
			set
			{
				this.moveRepeatDelay = value;
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0003A0A3 File Offset: 0x000382A3
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0003A0AB File Offset: 0x000382AB
		public Transform xrTrackingOrigin
		{
			get
			{
				return this.m_XRTrackingOrigin;
			}
			set
			{
				this.m_XRTrackingOrigin = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0003A0B4 File Offset: 0x000382B4
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0003A0BC File Offset: 0x000382BC
		public float trackedDeviceDragThresholdMultiplier
		{
			get
			{
				return this.m_TrackedDeviceDragThresholdMultiplier;
			}
			set
			{
				this.m_TrackedDeviceDragThresholdMultiplier = value;
			}
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0003A0C8 File Offset: 0x000382C8
		private void SwapAction(ref InputActionReference property, InputActionReference newValue, bool actionsHooked, Action<InputAction.CallbackContext> actionCallback)
		{
			if (property == newValue || (property != null && newValue != null && property.action == newValue.action))
			{
				return;
			}
			if (property != null && actionCallback != null && actionsHooked)
			{
				property.action.performed -= actionCallback;
				property.action.canceled -= actionCallback;
			}
			InputActionReference inputActionReference = property;
			bool flag = ((inputActionReference != null) ? inputActionReference.action : null) == null;
			InputActionReference inputActionReference2 = property;
			bool flag2 = ((inputActionReference2 != null) ? inputActionReference2.action : null) != null && property.action.enabled;
			this.TryDisableInputAction(property, false);
			property = newValue;
			if (((newValue != null) ? newValue.action : null) != null && actionCallback != null && actionsHooked)
			{
				property.action.performed += actionCallback;
				property.action.canceled += actionCallback;
			}
			if (base.isActiveAndEnabled && ((newValue != null) ? newValue.action : null) != null && (flag2 || flag))
			{
				this.EnableInputAction(property);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0003A1CB File Offset: 0x000383CB
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0003A1D3 File Offset: 0x000383D3
		public InputActionReference point
		{
			get
			{
				return this.m_PointAction;
			}
			set
			{
				this.SwapAction(ref this.m_PointAction, value, this.m_ActionsHooked, this.m_OnPointDelegate);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0003A1EE File Offset: 0x000383EE
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0003A1F6 File Offset: 0x000383F6
		public InputActionReference scrollWheel
		{
			get
			{
				return this.m_ScrollWheelAction;
			}
			set
			{
				this.SwapAction(ref this.m_ScrollWheelAction, value, this.m_ActionsHooked, this.m_OnScrollWheelDelegate);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0003A211 File Offset: 0x00038411
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0003A219 File Offset: 0x00038419
		public InputActionReference leftClick
		{
			get
			{
				return this.m_LeftClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_LeftClickAction, value, this.m_ActionsHooked, this.m_OnLeftClickDelegate);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0003A234 File Offset: 0x00038434
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0003A23C File Offset: 0x0003843C
		public InputActionReference middleClick
		{
			get
			{
				return this.m_MiddleClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_MiddleClickAction, value, this.m_ActionsHooked, this.m_OnMiddleClickDelegate);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0003A257 File Offset: 0x00038457
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0003A25F File Offset: 0x0003845F
		public InputActionReference rightClick
		{
			get
			{
				return this.m_RightClickAction;
			}
			set
			{
				this.SwapAction(ref this.m_RightClickAction, value, this.m_ActionsHooked, this.m_OnRightClickDelegate);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0003A27A File Offset: 0x0003847A
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0003A282 File Offset: 0x00038482
		public InputActionReference move
		{
			get
			{
				return this.m_MoveAction;
			}
			set
			{
				this.SwapAction(ref this.m_MoveAction, value, this.m_ActionsHooked, this.m_OnMoveDelegate);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0003A29D File Offset: 0x0003849D
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0003A2A5 File Offset: 0x000384A5
		public InputActionReference submit
		{
			get
			{
				return this.m_SubmitAction;
			}
			set
			{
				this.SwapAction(ref this.m_SubmitAction, value, this.m_ActionsHooked, null);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0003A2BB File Offset: 0x000384BB
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0003A2C3 File Offset: 0x000384C3
		public InputActionReference cancel
		{
			get
			{
				return this.m_CancelAction;
			}
			set
			{
				this.SwapAction(ref this.m_CancelAction, value, this.m_ActionsHooked, null);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0003A2D9 File Offset: 0x000384D9
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0003A2E1 File Offset: 0x000384E1
		public InputActionReference trackedDeviceOrientation
		{
			get
			{
				return this.m_TrackedDeviceOrientationAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDeviceOrientationAction, value, this.m_ActionsHooked, this.m_OnTrackedDeviceOrientationDelegate);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0003A2FC File Offset: 0x000384FC
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0003A304 File Offset: 0x00038504
		public InputActionReference trackedDevicePosition
		{
			get
			{
				return this.m_TrackedDevicePositionAction;
			}
			set
			{
				this.SwapAction(ref this.m_TrackedDevicePositionAction, value, this.m_ActionsHooked, this.m_OnTrackedDevicePositionDelegate);
			}
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003A320 File Offset: 0x00038520
		public void AssignDefaultActions()
		{
			DefaultInputActions defaultInputActions = new DefaultInputActions();
			this.actionsAsset = defaultInputActions.asset;
			this.cancel = InputActionReference.Create(defaultInputActions.UI.Cancel);
			this.submit = InputActionReference.Create(defaultInputActions.UI.Submit);
			this.move = InputActionReference.Create(defaultInputActions.UI.Navigate);
			this.leftClick = InputActionReference.Create(defaultInputActions.UI.Click);
			this.rightClick = InputActionReference.Create(defaultInputActions.UI.RightClick);
			this.middleClick = InputActionReference.Create(defaultInputActions.UI.MiddleClick);
			this.point = InputActionReference.Create(defaultInputActions.UI.Point);
			this.scrollWheel = InputActionReference.Create(defaultInputActions.UI.ScrollWheel);
			this.trackedDeviceOrientation = InputActionReference.Create(defaultInputActions.UI.TrackedDeviceOrientation);
			this.trackedDevicePosition = InputActionReference.Create(defaultInputActions.UI.TrackedDevicePosition);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0003A43C File Offset: 0x0003863C
		public void UnassignActions()
		{
			this.actionsAsset = null;
			this.cancel = null;
			this.submit = null;
			this.move = null;
			this.leftClick = null;
			this.rightClick = null;
			this.middleClick = null;
			this.point = null;
			this.scrollWheel = null;
			this.trackedDeviceOrientation = null;
			this.trackedDevicePosition = null;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0003A496 File Offset: 0x00038696
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0003A49D File Offset: 0x0003869D
		[Obsolete("'trackedDeviceSelect' has been obsoleted; use 'leftClick' instead.", true)]
		public InputActionReference trackedDeviceSelect
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003A4A4 File Offset: 0x000386A4
		protected override void Awake()
		{
			base.Awake();
			this.m_NavigationState.Reset();
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0003A4B7 File Offset: 0x000386B7
		protected override void OnDestroy()
		{
			base.OnDestroy();
			this.UnhookActions();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003A4C8 File Offset: 0x000386C8
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_OnControlsChangedDelegate == null)
			{
				this.m_OnControlsChangedDelegate = new Action<object>(this.OnControlsChanged);
			}
			InputActionState.s_GlobalState.onActionControlsChanged.AddCallback(this.m_OnControlsChangedDelegate);
			if (this.HasNoActions())
			{
				this.AssignDefaultActions();
			}
			this.ResetPointers();
			this.HookActions();
			this.EnableAllActions();
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003A52A File Offset: 0x0003872A
		protected override void OnDisable()
		{
			this.ResetPointers();
			InputActionState.s_GlobalState.onActionControlsChanged.RemoveCallback(this.m_OnControlsChangedDelegate);
			this.DisableAllActions();
			this.UnhookActions();
			base.OnDisable();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0003A55C File Offset: 0x0003875C
		private void ResetPointers()
		{
			int length = this.m_PointerStates.length;
			for (int i = 0; i < length; i++)
			{
				this.SendPointerExitEventsAndRemovePointer(0);
			}
			this.m_CurrentPointerId = -1;
			this.m_CurrentPointerIndex = -1;
			this.m_CurrentPointerType = UIPointerType.None;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0003A5A0 File Offset: 0x000387A0
		private bool HasNoActions()
		{
			if (this.m_ActionsAsset != null)
			{
				return false;
			}
			InputActionReference pointAction = this.m_PointAction;
			if (((pointAction != null) ? pointAction.action : null) == null)
			{
				InputActionReference leftClickAction = this.m_LeftClickAction;
				if (((leftClickAction != null) ? leftClickAction.action : null) == null)
				{
					InputActionReference rightClickAction = this.m_RightClickAction;
					if (((rightClickAction != null) ? rightClickAction.action : null) == null)
					{
						InputActionReference middleClickAction = this.m_MiddleClickAction;
						if (((middleClickAction != null) ? middleClickAction.action : null) == null)
						{
							InputActionReference submitAction = this.m_SubmitAction;
							if (((submitAction != null) ? submitAction.action : null) == null)
							{
								InputActionReference cancelAction = this.m_CancelAction;
								if (((cancelAction != null) ? cancelAction.action : null) == null)
								{
									InputActionReference scrollWheelAction = this.m_ScrollWheelAction;
									if (((scrollWheelAction != null) ? scrollWheelAction.action : null) == null)
									{
										InputActionReference trackedDeviceOrientationAction = this.m_TrackedDeviceOrientationAction;
										if (((trackedDeviceOrientationAction != null) ? trackedDeviceOrientationAction.action : null) == null)
										{
											InputActionReference trackedDevicePositionAction = this.m_TrackedDevicePositionAction;
											return ((trackedDevicePositionAction != null) ? trackedDevicePositionAction.action : null) == null;
										}
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0003A67C File Offset: 0x0003887C
		private void EnableAllActions()
		{
			this.EnableInputAction(this.m_PointAction);
			this.EnableInputAction(this.m_LeftClickAction);
			this.EnableInputAction(this.m_RightClickAction);
			this.EnableInputAction(this.m_MiddleClickAction);
			this.EnableInputAction(this.m_MoveAction);
			this.EnableInputAction(this.m_SubmitAction);
			this.EnableInputAction(this.m_CancelAction);
			this.EnableInputAction(this.m_ScrollWheelAction);
			this.EnableInputAction(this.m_TrackedDeviceOrientationAction);
			this.EnableInputAction(this.m_TrackedDevicePositionAction);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0003A704 File Offset: 0x00038904
		private void DisableAllActions()
		{
			this.TryDisableInputAction(this.m_PointAction, true);
			this.TryDisableInputAction(this.m_LeftClickAction, true);
			this.TryDisableInputAction(this.m_RightClickAction, true);
			this.TryDisableInputAction(this.m_MiddleClickAction, true);
			this.TryDisableInputAction(this.m_MoveAction, true);
			this.TryDisableInputAction(this.m_SubmitAction, true);
			this.TryDisableInputAction(this.m_CancelAction, true);
			this.TryDisableInputAction(this.m_ScrollWheelAction, true);
			this.TryDisableInputAction(this.m_TrackedDeviceOrientationAction, true);
			this.TryDisableInputAction(this.m_TrackedDevicePositionAction, true);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0003A794 File Offset: 0x00038994
		private void EnableInputAction(InputActionReference inputActionReference)
		{
			InputAction inputAction = ((inputActionReference != null) ? inputActionReference.action : null);
			if (inputAction == null)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState inputActionReferenceState;
			if (InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(inputAction, out inputActionReferenceState))
			{
				inputActionReferenceState.refCount++;
				InputSystemUIInputModule.s_InputActionReferenceCounts[inputAction] = inputActionReferenceState;
			}
			else
			{
				inputActionReferenceState = new InputSystemUIInputModule.InputActionReferenceState
				{
					refCount = 1,
					enabledByInputModule = !inputAction.enabled
				};
				InputSystemUIInputModule.s_InputActionReferenceCounts.Add(inputAction, inputActionReferenceState);
			}
			inputAction.Enable();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0003A810 File Offset: 0x00038A10
		private void TryDisableInputAction(InputActionReference inputActionReference, bool isComponentDisabling = false)
		{
			InputAction inputAction = ((inputActionReference != null) ? inputActionReference.action : null);
			if (inputAction == null)
			{
				return;
			}
			if (!base.isActiveAndEnabled && !isComponentDisabling)
			{
				return;
			}
			InputSystemUIInputModule.InputActionReferenceState inputActionReferenceState;
			if (!InputSystemUIInputModule.s_InputActionReferenceCounts.TryGetValue(inputAction, out inputActionReferenceState))
			{
				return;
			}
			if (inputActionReferenceState.refCount - 1 == 0 && inputActionReferenceState.enabledByInputModule)
			{
				inputAction.Disable();
				InputSystemUIInputModule.s_InputActionReferenceCounts.Remove(inputAction);
				return;
			}
			inputActionReferenceState.refCount--;
			InputSystemUIInputModule.s_InputActionReferenceCounts[inputAction] = inputActionReferenceState;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0003A888 File Offset: 0x00038A88
		private int GetPointerStateIndexFor(int pointerOrTouchId)
		{
			if (pointerOrTouchId == this.m_CurrentPointerId)
			{
				return this.m_CurrentPointerIndex;
			}
			for (int i = 0; i < this.m_PointerIds.length; i++)
			{
				if (this.m_PointerIds[i] == pointerOrTouchId)
				{
					return i;
				}
			}
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ExtendedPointerEventData eventData = this.m_PointerStates[j].eventData;
				if (eventData.touchId == pointerOrTouchId || (eventData.touchId != 0 && eventData.device.deviceId == pointerOrTouchId))
				{
					return j;
				}
			}
			return -1;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0003A916 File Offset: 0x00038B16
		private ref PointerModel GetPointerStateForIndex(int index)
		{
			if (index == 0)
			{
				return ref this.m_PointerStates.firstValue;
			}
			return ref this.m_PointerStates.additionalValues[index - 1];
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0003A93C File Offset: 0x00038B3C
		private int GetDisplayIndexFor(InputControl control)
		{
			int num = 0;
			Pointer pointer = control.device as Pointer;
			if (pointer != null)
			{
				num = pointer.displayIndex.ReadValue();
			}
			return num;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0003A968 File Offset: 0x00038B68
		private int GetPointerStateIndexFor(ref InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context))
			{
				return -1;
			}
			InputActionPhase phase = context.phase;
			return this.GetPointerStateIndexFor(context.control, phase != InputActionPhase.Canceled);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003A99C File Offset: 0x00038B9C
		private unsafe int GetPointerStateIndexFor(InputControl control, bool createIfNotExists = true)
		{
			InputDevice device = control.device;
			InputControl parent = control.parent;
			int num = this.m_PointerTouchControls.IndexOfReference(parent);
			if (num != -1)
			{
				this.m_CurrentPointerId = this.m_PointerIds[num];
				this.m_CurrentPointerIndex = num;
				this.m_CurrentPointerType = UIPointerType.Touch;
				return num;
			}
			int num2 = device.deviceId;
			int num3 = 0;
			Vector2 vector = Vector2.zero;
			TouchControl touchControl = parent as TouchControl;
			if (touchControl != null)
			{
				num3 = *touchControl.touchId.value;
				vector = *touchControl.position.value;
			}
			else
			{
				Touchscreen touchscreen = parent as Touchscreen;
				if (touchscreen != null)
				{
					num3 = *touchscreen.primaryTouch.touchId.value;
					vector = *touchscreen.primaryTouch.position.value;
				}
			}
			int displayIndexFor = this.GetDisplayIndexFor(control);
			if (num3 != 0)
			{
				num2 = ExtendedPointerEventData.MakePointerIdForTouch(num2, num3);
			}
			if (this.m_CurrentPointerId == num2)
			{
				return this.m_CurrentPointerIndex;
			}
			if (num3 == 0)
			{
				for (int i = 0; i < this.m_PointerIds.length; i++)
				{
					if (this.m_PointerIds[i] == num2)
					{
						this.m_CurrentPointerId = num2;
						this.m_CurrentPointerIndex = i;
						this.m_CurrentPointerType = this.m_PointerStates[i].pointerType;
						return i;
					}
				}
			}
			if (!createIfNotExists)
			{
				return -1;
			}
			UIPointerType uipointerType = UIPointerType.None;
			if (num3 != 0)
			{
				uipointerType = UIPointerType.Touch;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.point))
			{
				uipointerType = UIPointerType.MouseOrPen;
			}
			else if (InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition))
			{
				uipointerType = UIPointerType.Tracked;
			}
			if (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && uipointerType != UIPointerType.None)
			{
				if (uipointerType == UIPointerType.MouseOrPen)
				{
					for (int j = 0; j < this.m_PointerStates.length; j++)
					{
						if (this.m_PointerStates[j].pointerType != UIPointerType.MouseOrPen)
						{
							this.SendPointerExitEventsAndRemovePointer(j);
							j--;
						}
					}
				}
				else
				{
					for (int k = 0; k < this.m_PointerStates.length; k++)
					{
						if (this.m_PointerStates[k].pointerType == UIPointerType.MouseOrPen)
						{
							this.SendPointerExitEventsAndRemovePointer(k);
							k--;
						}
					}
				}
			}
			if ((this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer && uipointerType != UIPointerType.None) || (this.m_PointerBehavior == UIPointerBehavior.SingleMouseOrPenButMultiTouchAndTrack && uipointerType == UIPointerType.MouseOrPen))
			{
				if (this.m_CurrentPointerIndex == -1)
				{
					this.m_CurrentPointerIndex = this.AllocatePointer(num2, displayIndexFor, num3, uipointerType, control, device, (num3 != 0) ? parent : null);
				}
				else
				{
					ExtendedPointerEventData eventData = this.GetPointerStateForIndex(this.m_CurrentPointerIndex).eventData;
					eventData.control = control;
					eventData.device = device;
					eventData.pointerType = uipointerType;
					eventData.pointerId = num2;
					eventData.touchId = num3;
					eventData.trackedDeviceOrientation = default(Quaternion);
					eventData.trackedDevicePosition = default(Vector3);
				}
				if (uipointerType == UIPointerType.Touch)
				{
					this.GetPointerStateForIndex(this.m_CurrentPointerIndex).screenPosition = vector;
				}
				this.m_CurrentPointerId = num2;
				this.m_CurrentPointerType = uipointerType;
				return this.m_CurrentPointerIndex;
			}
			int num4;
			if (uipointerType != UIPointerType.None)
			{
				num4 = this.AllocatePointer(num2, displayIndexFor, num3, uipointerType, control, device, (num3 != 0) ? parent : null);
			}
			else
			{
				if (this.m_CurrentPointerId != -1)
				{
					return this.m_CurrentPointerIndex;
				}
				InputActionReference point = this.point;
				ReadOnlyArray<InputControl>? readOnlyArray;
				if (point == null)
				{
					readOnlyArray = null;
				}
				else
				{
					InputAction action = point.action;
					readOnlyArray = ((action != null) ? new ReadOnlyArray<InputControl>?(action.controls) : null);
				}
				ReadOnlyArray<InputControl>? readOnlyArray2 = readOnlyArray;
				InputDevice inputDevice = ((readOnlyArray2 != null && readOnlyArray2.Value.Count > 0) ? readOnlyArray2.Value[0].device : null);
				if (inputDevice != null && !(inputDevice is Touchscreen))
				{
					num4 = this.AllocatePointer(inputDevice.deviceId, displayIndexFor, 0, UIPointerType.MouseOrPen, readOnlyArray2.Value[0], inputDevice, null);
				}
				else
				{
					InputActionReference trackedDevicePosition = this.trackedDevicePosition;
					ReadOnlyArray<InputControl>? readOnlyArray3;
					if (trackedDevicePosition == null)
					{
						readOnlyArray3 = null;
					}
					else
					{
						InputAction action2 = trackedDevicePosition.action;
						readOnlyArray3 = ((action2 != null) ? new ReadOnlyArray<InputControl>?(action2.controls) : null);
					}
					ReadOnlyArray<InputControl>? readOnlyArray4 = readOnlyArray3;
					InputDevice inputDevice2 = ((readOnlyArray4 != null && readOnlyArray4.Value.Count > 0) ? readOnlyArray4.Value[0].device : null);
					if (inputDevice2 != null)
					{
						num4 = this.AllocatePointer(inputDevice2.deviceId, displayIndexFor, 0, UIPointerType.Tracked, readOnlyArray4.Value[0], inputDevice2, null);
					}
					else
					{
						num4 = this.AllocatePointer(num2, displayIndexFor, 0, UIPointerType.None, control, device, null);
					}
				}
			}
			if (uipointerType == UIPointerType.Touch)
			{
				this.GetPointerStateForIndex(num4).screenPosition = vector;
			}
			this.m_CurrentPointerId = num2;
			this.m_CurrentPointerIndex = num4;
			this.m_CurrentPointerType = uipointerType;
			return num4;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0003AE34 File Offset: 0x00039034
		private int AllocatePointer(int pointerId, int displayIndex, int touchId, UIPointerType pointerType, InputControl control, InputDevice device, InputControl touchControl = null)
		{
			ExtendedPointerEventData extendedPointerEventData = null;
			if (this.m_PointerStates.Capacity > this.m_PointerStates.length)
			{
				if (this.m_PointerStates.length == 0)
				{
					extendedPointerEventData = this.m_PointerStates.firstValue.eventData;
				}
				else
				{
					extendedPointerEventData = this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData;
				}
			}
			if (extendedPointerEventData == null)
			{
				extendedPointerEventData = new ExtendedPointerEventData(base.eventSystem);
			}
			extendedPointerEventData.pointerId = pointerId;
			extendedPointerEventData.touchId = touchId;
			extendedPointerEventData.pointerType = pointerType;
			extendedPointerEventData.control = control;
			extendedPointerEventData.device = device;
			this.m_PointerIds.AppendWithCapacity(pointerId, 10);
			this.m_PointerTouchControls.AppendWithCapacity(touchControl, 10);
			return this.m_PointerStates.AppendWithCapacity(new PointerModel(extendedPointerEventData), 10);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003AF08 File Offset: 0x00039108
		private void SendPointerExitEventsAndRemovePointer(int index)
		{
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (eventData.pointerEnter != null)
			{
				this.ProcessPointerMovement(eventData, null);
			}
			this.RemovePointerAtIndex(index);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0003AF44 File Offset: 0x00039144
		private void RemovePointerAtIndex(int index)
		{
			ExtendedPointerEventData eventData = this.m_PointerStates[index].eventData;
			if (index == this.m_CurrentPointerIndex)
			{
				this.m_CurrentPointerId = -1;
				this.m_CurrentPointerIndex = -1;
				this.m_CurrentPointerType = UIPointerType.None;
			}
			else if (this.m_CurrentPointerIndex == this.m_PointerIds.length - 1)
			{
				this.m_CurrentPointerIndex = index;
			}
			this.m_PointerIds.RemoveAtByMovingTailWithCapacity(index);
			this.m_PointerTouchControls.RemoveAtByMovingTailWithCapacity(index);
			this.m_PointerStates.RemoveAtByMovingTailWithCapacity(index);
			eventData.hovered.Clear();
			eventData.device = null;
			eventData.pointerCurrentRaycast = default(RaycastResult);
			eventData.pointerPressRaycast = default(RaycastResult);
			eventData.pointerPress = null;
			eventData.pointerPress = null;
			eventData.pointerDrag = null;
			eventData.pointerEnter = null;
			eventData.rawPointerPress = null;
			if (this.m_PointerStates.length == 0)
			{
				this.m_PointerStates.firstValue.eventData = eventData;
				return;
			}
			this.m_PointerStates.additionalValues[this.m_PointerStates.length - 1].eventData = eventData;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0003B058 File Offset: 0x00039258
		private void PurgeStalePointers()
		{
			for (int i = 0; i < this.m_PointerStates.length; i++)
			{
				InputDevice device = this.GetPointerStateForIndex(i).eventData.device;
				if (!device.added || (!InputSystemUIInputModule.HaveControlForDevice(device, this.point) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDevicePosition) && !InputSystemUIInputModule.HaveControlForDevice(device, this.trackedDeviceOrientation)))
				{
					this.SendPointerExitEventsAndRemovePointer(i);
					i--;
				}
			}
			this.m_NeedToPurgeStalePointers = false;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0003B0D4 File Offset: 0x000392D4
		private static bool HaveControlForDevice(InputDevice device, InputActionReference actionReference)
		{
			InputAction inputAction = ((actionReference != null) ? actionReference.action : null);
			if (inputAction == null)
			{
				return false;
			}
			ReadOnlyArray<InputControl> controls = inputAction.controls;
			for (int i = 0; i < controls.Count; i++)
			{
				if (controls[i].device == device)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0003B120 File Offset: 0x00039320
		private void OnPointCallback(InputAction.CallbackContext context)
		{
			if (this.CheckForRemovedDevice(ref context) || context.canceled)
			{
				return;
			}
			int pointerStateIndexFor = this.GetPointerStateIndexFor(context.control, true);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			this.GetPointerStateForIndex(pointerStateIndexFor).screenPosition = context.ReadValue<Vector2>();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0003B168 File Offset: 0x00039368
		private bool IgnoreNextClick(ref InputAction.CallbackContext context, bool wasPressed)
		{
			return !this.explictlyIgnoreFocus && (context.canceled && !InputRuntime.s_Instance.isPlayerFocused && !context.control.device.canRunInBackground && wasPressed);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0003B1A0 File Offset: 0x000393A0
		private void OnLeftClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.leftButton.isPressed;
			pointerStateForIndex.leftButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.leftButton.ignoreNextClick = true;
			}
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0003B200 File Offset: 0x00039400
		private void OnRightClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.rightButton.isPressed;
			pointerStateForIndex.rightButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.rightButton.ignoreNextClick = true;
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0003B260 File Offset: 0x00039460
		private void OnMiddleClickCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(pointerStateIndexFor);
			bool isPressed = pointerStateForIndex.middleButton.isPressed;
			pointerStateForIndex.middleButton.isPressed = context.ReadValueAsButton();
			pointerStateForIndex.changedThisFrame = true;
			if (this.IgnoreNextClick(ref context, isPressed))
			{
				pointerStateForIndex.middleButton.ignoreNextClick = true;
			}
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0003B2BF File Offset: 0x000394BF
		private bool CheckForRemovedDevice(ref InputAction.CallbackContext context)
		{
			if (context.canceled && !context.control.device.added)
			{
				this.m_NeedToPurgeStalePointers = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0003B2E8 File Offset: 0x000394E8
		private void OnScrollCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			this.GetPointerStateForIndex(pointerStateIndexFor).scrollDelta = context.ReadValue<Vector2>() * 0.05f;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0003B320 File Offset: 0x00039520
		private void OnMoveCallback(InputAction.CallbackContext context)
		{
			this.m_NavigationState.move = context.ReadValue<Vector2>();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0003B334 File Offset: 0x00039534
		private void OnTrackedDeviceOrientationCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			this.GetPointerStateForIndex(pointerStateIndexFor).worldOrientation = context.ReadValue<Quaternion>();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0003B364 File Offset: 0x00039564
		private void OnTrackedDevicePositionCallback(InputAction.CallbackContext context)
		{
			int pointerStateIndexFor = this.GetPointerStateIndexFor(ref context);
			if (pointerStateIndexFor == -1)
			{
				return;
			}
			this.GetPointerStateForIndex(pointerStateIndexFor).worldPosition = context.ReadValue<Vector3>();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0003B392 File Offset: 0x00039592
		private void OnControlsChanged(object obj)
		{
			this.m_NeedToPurgeStalePointers = true;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003B39C File Offset: 0x0003959C
		public override void Process()
		{
			if (this.m_NeedToPurgeStalePointers)
			{
				this.PurgeStalePointers();
			}
			if (!base.eventSystem.isFocused && !this.shouldIgnoreFocus)
			{
				for (int i = 0; i < this.m_PointerStates.length; i++)
				{
					this.m_PointerStates[i].OnFrameFinished();
				}
				return;
			}
			this.ProcessNavigation(ref this.m_NavigationState);
			for (int j = 0; j < this.m_PointerStates.length; j++)
			{
				ref PointerModel pointerStateForIndex = ref this.GetPointerStateForIndex(j);
				pointerStateForIndex.eventData.ReadDeviceState();
				pointerStateForIndex.CopyTouchOrPenStateFrom(pointerStateForIndex.eventData);
				this.ProcessPointer(ref pointerStateForIndex);
				if (pointerStateForIndex.pointerType == UIPointerType.Touch && !pointerStateForIndex.leftButton.isPressed && !pointerStateForIndex.leftButton.wasReleasedThisFrame)
				{
					this.RemovePointerAtIndex(j);
					j--;
				}
				else
				{
					pointerStateForIndex.OnFrameFinished();
				}
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0003B478 File Offset: 0x00039678
		public override int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (this.m_PointerBehavior == UIPointerBehavior.SingleUnifiedPointer)
			{
				return PointerId.mousePointerId;
			}
			ExtendedPointerEventData extendedPointerEventData = sourcePointerData as ExtendedPointerEventData;
			if (extendedPointerEventData == null)
			{
				return base.ConvertUIToolkitPointerId(sourcePointerData);
			}
			return extendedPointerEventData.uiToolkitPointerId;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0003B4AC File Offset: 0x000396AC
		private void HookActions()
		{
			if (this.m_ActionsHooked)
			{
				return;
			}
			if (this.m_OnPointDelegate == null)
			{
				this.m_OnPointDelegate = new Action<InputAction.CallbackContext>(this.OnPointCallback);
			}
			if (this.m_OnLeftClickDelegate == null)
			{
				this.m_OnLeftClickDelegate = new Action<InputAction.CallbackContext>(this.OnLeftClickCallback);
			}
			if (this.m_OnRightClickDelegate == null)
			{
				this.m_OnRightClickDelegate = new Action<InputAction.CallbackContext>(this.OnRightClickCallback);
			}
			if (this.m_OnMiddleClickDelegate == null)
			{
				this.m_OnMiddleClickDelegate = new Action<InputAction.CallbackContext>(this.OnMiddleClickCallback);
			}
			if (this.m_OnScrollWheelDelegate == null)
			{
				this.m_OnScrollWheelDelegate = new Action<InputAction.CallbackContext>(this.OnScrollCallback);
			}
			if (this.m_OnMoveDelegate == null)
			{
				this.m_OnMoveDelegate = new Action<InputAction.CallbackContext>(this.OnMoveCallback);
			}
			if (this.m_OnTrackedDeviceOrientationDelegate == null)
			{
				this.m_OnTrackedDeviceOrientationDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDeviceOrientationCallback);
			}
			if (this.m_OnTrackedDevicePositionDelegate == null)
			{
				this.m_OnTrackedDevicePositionDelegate = new Action<InputAction.CallbackContext>(this.OnTrackedDevicePositionCallback);
			}
			this.SetActionCallbacks(true);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0003B599 File Offset: 0x00039799
		private void UnhookActions()
		{
			if (!this.m_ActionsHooked)
			{
				return;
			}
			this.SetActionCallbacks(false);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003B5AC File Offset: 0x000397AC
		private void SetActionCallbacks(bool install)
		{
			this.m_ActionsHooked = install;
			InputSystemUIInputModule.SetActionCallback(this.m_PointAction, this.m_OnPointDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MoveAction, this.m_OnMoveDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_LeftClickAction, this.m_OnLeftClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_RightClickAction, this.m_OnRightClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_MiddleClickAction, this.m_OnMiddleClickDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_ScrollWheelAction, this.m_OnScrollWheelDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDeviceOrientationAction, this.m_OnTrackedDeviceOrientationDelegate, install);
			InputSystemUIInputModule.SetActionCallback(this.m_TrackedDevicePositionAction, this.m_OnTrackedDevicePositionDelegate, install);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0003B650 File Offset: 0x00039850
		private static void SetActionCallback(InputActionReference actionReference, Action<InputAction.CallbackContext> callback, bool install)
		{
			if (!install && callback == null)
			{
				return;
			}
			if (actionReference == null)
			{
				return;
			}
			InputAction action = actionReference.action;
			if (action == null)
			{
				return;
			}
			if (install)
			{
				action.performed += callback;
				action.canceled += callback;
				return;
			}
			action.performed -= callback;
			action.canceled -= callback;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0003B69C File Offset: 0x0003989C
		private InputActionReference UpdateReferenceForNewAsset(InputActionReference actionReference)
		{
			InputAction inputAction = ((actionReference != null) ? actionReference.action : null);
			if (inputAction == null)
			{
				return null;
			}
			InputActionMap actionMap = inputAction.actionMap;
			InputActionAsset actionsAsset = this.m_ActionsAsset;
			InputActionMap inputActionMap = ((actionsAsset != null) ? actionsAsset.FindActionMap(actionMap.name, false) : null);
			if (inputActionMap == null)
			{
				return null;
			}
			InputAction inputAction2 = inputActionMap.FindAction(inputAction.name, false);
			if (inputAction2 == null)
			{
				return null;
			}
			return InputActionReference.Create(inputAction2);
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0003B6FA File Offset: 0x000398FA
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0003B704 File Offset: 0x00039904
		public InputActionAsset actionsAsset
		{
			get
			{
				return this.m_ActionsAsset;
			}
			set
			{
				if (value != this.m_ActionsAsset)
				{
					this.UnhookActions();
					this.m_ActionsAsset = value;
					this.point = this.UpdateReferenceForNewAsset(this.point);
					this.move = this.UpdateReferenceForNewAsset(this.move);
					this.leftClick = this.UpdateReferenceForNewAsset(this.leftClick);
					this.rightClick = this.UpdateReferenceForNewAsset(this.rightClick);
					this.middleClick = this.UpdateReferenceForNewAsset(this.middleClick);
					this.scrollWheel = this.UpdateReferenceForNewAsset(this.scrollWheel);
					this.submit = this.UpdateReferenceForNewAsset(this.submit);
					this.cancel = this.UpdateReferenceForNewAsset(this.cancel);
					this.trackedDeviceOrientation = this.UpdateReferenceForNewAsset(this.trackedDeviceOrientation);
					this.trackedDevicePosition = this.UpdateReferenceForNewAsset(this.trackedDevicePosition);
					this.HookActions();
				}
			}
		}

		// Token: 0x040003B5 RID: 949
		private const float kClickSpeed = 0.3f;

		// Token: 0x040003B6 RID: 950
		[FormerlySerializedAs("m_RepeatDelay")]
		[Tooltip("The Initial delay (in seconds) between an initial move action and a repeated move action.")]
		[SerializeField]
		private float m_MoveRepeatDelay = 0.5f;

		// Token: 0x040003B7 RID: 951
		[FormerlySerializedAs("m_RepeatRate")]
		[Tooltip("The speed (in seconds) that the move action repeats itself once repeating (max 1 per frame).")]
		[SerializeField]
		private float m_MoveRepeatRate = 0.1f;

		// Token: 0x040003B8 RID: 952
		[Tooltip("Scales the Eventsystem.DragThreshold, for tracked devices, to make selection easier.")]
		private float m_TrackedDeviceDragThresholdMultiplier = 2f;

		// Token: 0x040003B9 RID: 953
		[Tooltip("Transform representing the real world origin for tracking devices. When using the XR Interaction Toolkit, this should be pointing to the XR Rig's Transform.")]
		[SerializeField]
		private Transform m_XRTrackingOrigin;

		// Token: 0x040003BA RID: 954
		internal const float kPixelPerLine = 20f;

		// Token: 0x040003BB RID: 955
		[SerializeField]
		[HideInInspector]
		private InputActionAsset m_ActionsAsset;

		// Token: 0x040003BC RID: 956
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_PointAction;

		// Token: 0x040003BD RID: 957
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MoveAction;

		// Token: 0x040003BE RID: 958
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_SubmitAction;

		// Token: 0x040003BF RID: 959
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_CancelAction;

		// Token: 0x040003C0 RID: 960
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_LeftClickAction;

		// Token: 0x040003C1 RID: 961
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_MiddleClickAction;

		// Token: 0x040003C2 RID: 962
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_RightClickAction;

		// Token: 0x040003C3 RID: 963
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_ScrollWheelAction;

		// Token: 0x040003C4 RID: 964
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDevicePositionAction;

		// Token: 0x040003C5 RID: 965
		[SerializeField]
		[HideInInspector]
		private InputActionReference m_TrackedDeviceOrientationAction;

		// Token: 0x040003C6 RID: 966
		[SerializeField]
		private bool m_DeselectOnBackgroundClick = true;

		// Token: 0x040003C7 RID: 967
		[SerializeField]
		private UIPointerBehavior m_PointerBehavior;

		// Token: 0x040003C8 RID: 968
		[SerializeField]
		[HideInInspector]
		internal InputSystemUIInputModule.CursorLockBehavior m_CursorLockBehavior;

		// Token: 0x040003C9 RID: 969
		private static Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState> s_InputActionReferenceCounts = new Dictionary<InputAction, InputSystemUIInputModule.InputActionReferenceState>();

		// Token: 0x040003CA RID: 970
		[NonSerialized]
		private bool m_ActionsHooked;

		// Token: 0x040003CB RID: 971
		[NonSerialized]
		private bool m_NeedToPurgeStalePointers;

		// Token: 0x040003CC RID: 972
		private Action<InputAction.CallbackContext> m_OnPointDelegate;

		// Token: 0x040003CD RID: 973
		private Action<InputAction.CallbackContext> m_OnMoveDelegate;

		// Token: 0x040003CE RID: 974
		private Action<InputAction.CallbackContext> m_OnLeftClickDelegate;

		// Token: 0x040003CF RID: 975
		private Action<InputAction.CallbackContext> m_OnRightClickDelegate;

		// Token: 0x040003D0 RID: 976
		private Action<InputAction.CallbackContext> m_OnMiddleClickDelegate;

		// Token: 0x040003D1 RID: 977
		private Action<InputAction.CallbackContext> m_OnScrollWheelDelegate;

		// Token: 0x040003D2 RID: 978
		private Action<InputAction.CallbackContext> m_OnTrackedDevicePositionDelegate;

		// Token: 0x040003D3 RID: 979
		private Action<InputAction.CallbackContext> m_OnTrackedDeviceOrientationDelegate;

		// Token: 0x040003D4 RID: 980
		private Action<object> m_OnControlsChangedDelegate;

		// Token: 0x040003D5 RID: 981
		[NonSerialized]
		private int m_CurrentPointerId = -1;

		// Token: 0x040003D6 RID: 982
		[NonSerialized]
		private int m_CurrentPointerIndex = -1;

		// Token: 0x040003D7 RID: 983
		[NonSerialized]
		internal UIPointerType m_CurrentPointerType;

		// Token: 0x040003D8 RID: 984
		internal InlinedArray<int> m_PointerIds;

		// Token: 0x040003D9 RID: 985
		internal InlinedArray<InputControl> m_PointerTouchControls;

		// Token: 0x040003DA RID: 986
		internal InlinedArray<PointerModel> m_PointerStates;

		// Token: 0x040003DB RID: 987
		private NavigationModel m_NavigationState;

		// Token: 0x040003DC RID: 988
		[NonSerialized]
		private GameObject m_LocalMultiPlayerRoot;

		// Token: 0x020001C9 RID: 457
		private struct InputActionReferenceState
		{
			// Token: 0x04000942 RID: 2370
			public int refCount;

			// Token: 0x04000943 RID: 2371
			public bool enabledByInputModule;
		}

		// Token: 0x020001CA RID: 458
		public enum CursorLockBehavior
		{
			// Token: 0x04000945 RID: 2373
			OutsideScreen,
			// Token: 0x04000946 RID: 2374
			ScreenCenter
		}
	}
}
