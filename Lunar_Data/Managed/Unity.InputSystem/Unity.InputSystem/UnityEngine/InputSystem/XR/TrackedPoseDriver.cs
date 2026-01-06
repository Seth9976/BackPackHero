using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000065 RID: 101
	[AddComponentMenu("XR/Tracked Pose Driver (Input System)")]
	[Serializable]
	public class TrackedPoseDriver : MonoBehaviour, ISerializationCallbackReceiver
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00034F6B File Offset: 0x0003316B
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x00034F73 File Offset: 0x00033173
		public TrackedPoseDriver.TrackingType trackingType
		{
			get
			{
				return this.m_TrackingType;
			}
			set
			{
				this.m_TrackingType = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00034F7C File Offset: 0x0003317C
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x00034F84 File Offset: 0x00033184
		public TrackedPoseDriver.UpdateType updateType
		{
			get
			{
				return this.m_UpdateType;
			}
			set
			{
				this.m_UpdateType = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00034F8D File Offset: 0x0003318D
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x00034F95 File Offset: 0x00033195
		public bool ignoreTrackingState
		{
			get
			{
				return this.m_IgnoreTrackingState;
			}
			set
			{
				this.m_IgnoreTrackingState = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00034F9E File Offset: 0x0003319E
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x00034FA6 File Offset: 0x000331A6
		public InputActionProperty positionInput
		{
			get
			{
				return this.m_PositionInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindPosition();
				}
				this.m_PositionInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindPosition();
				}
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00034FD1 File Offset: 0x000331D1
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x00034FD9 File Offset: 0x000331D9
		public InputActionProperty rotationInput
		{
			get
			{
				return this.m_RotationInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindRotation();
				}
				this.m_RotationInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindRotation();
				}
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00035004 File Offset: 0x00033204
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0003500C File Offset: 0x0003320C
		public InputActionProperty trackingStateInput
		{
			get
			{
				return this.m_TrackingStateInput;
			}
			set
			{
				if (Application.isPlaying)
				{
					this.UnbindTrackingState();
				}
				this.m_TrackingStateInput = value;
				if (Application.isPlaying && base.isActiveAndEnabled)
				{
					this.BindTrackingState();
				}
			}
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00035037 File Offset: 0x00033237
		private void BindActions()
		{
			this.BindPosition();
			this.BindRotation();
			this.BindTrackingState();
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0003504B File Offset: 0x0003324B
		private void UnbindActions()
		{
			this.UnbindPosition();
			this.UnbindRotation();
			this.UnbindTrackingState();
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00035060 File Offset: 0x00033260
		private void BindPosition()
		{
			if (this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnPositionPerformed;
			action.canceled += this.OnPositionCanceled;
			this.m_PositionBound = true;
			if (this.m_PositionInput.reference == null)
			{
				action.Rename(base.gameObject.name + " - TPD - Position");
				action.Enable();
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000350E8 File Offset: 0x000332E8
		private void BindRotation()
		{
			if (this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnRotationPerformed;
			action.canceled += this.OnRotationCanceled;
			this.m_RotationBound = true;
			if (this.m_RotationInput.reference == null)
			{
				action.Rename(base.gameObject.name + " - TPD - Rotation");
				action.Enable();
			}
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00035170 File Offset: 0x00033370
		private void BindTrackingState()
		{
			if (this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			action.performed += this.OnTrackingStatePerformed;
			action.canceled += this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = true;
			if (this.m_TrackingStateInput.reference == null)
			{
				action.Rename(base.gameObject.name + " - TPD - Tracking State");
				action.Enable();
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000351F8 File Offset: 0x000333F8
		private void UnbindPosition()
		{
			if (!this.m_PositionBound)
			{
				return;
			}
			InputAction action = this.m_PositionInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_PositionInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnPositionPerformed;
			action.canceled -= this.OnPositionCanceled;
			this.m_PositionBound = false;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00035264 File Offset: 0x00033464
		private void UnbindRotation()
		{
			if (!this.m_RotationBound)
			{
				return;
			}
			InputAction action = this.m_RotationInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_RotationInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnRotationPerformed;
			action.canceled -= this.OnRotationCanceled;
			this.m_RotationBound = false;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000352D0 File Offset: 0x000334D0
		private void UnbindTrackingState()
		{
			if (!this.m_TrackingStateBound)
			{
				return;
			}
			InputAction action = this.m_TrackingStateInput.action;
			if (action == null)
			{
				return;
			}
			if (this.m_TrackingStateInput.reference == null)
			{
				action.Disable();
			}
			action.performed -= this.OnTrackingStatePerformed;
			action.canceled -= this.OnTrackingStateCanceled;
			this.m_TrackingStateBound = false;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0003533A File Offset: 0x0003353A
		private void OnPositionPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = context.ReadValue<Vector3>();
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00035349 File Offset: 0x00033549
		private void OnPositionCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentPosition = Vector3.zero;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00035356 File Offset: 0x00033556
		private void OnRotationPerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = context.ReadValue<Quaternion>();
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00035365 File Offset: 0x00033565
		private void OnRotationCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentRotation = Quaternion.identity;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00035372 File Offset: 0x00033572
		private void OnTrackingStatePerformed(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)context.ReadValue<int>();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00035381 File Offset: 0x00033581
		private void OnTrackingStateCanceled(InputAction.CallbackContext context)
		{
			this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0003538C File Offset: 0x0003358C
		protected void Reset()
		{
			this.m_HasMigratedActions = true;
			this.m_PositionInput = new InputActionProperty(new InputAction("Position", InputActionType.Value, null, null, null, "Vector3"));
			this.m_RotationInput = new InputActionProperty(new InputAction("Rotation", InputActionType.Value, null, null, null, "Quaternion"));
			this.m_TrackingStateInput = new InputActionProperty(new InputAction("Tracking State", InputActionType.Value, null, null, null, "Integer"));
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000353FC File Offset: 0x000335FC
		protected virtual void Awake()
		{
			Camera camera;
			if (this.HasStereoCamera(out camera))
			{
				XRDevice.DisableAutoXRCameraTracking(camera, true);
			}
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0003541A File Offset: 0x0003361A
		protected void OnEnable()
		{
			InputSystem.onAfterUpdate += this.UpdateCallback;
			this.BindActions();
			this.m_IsFirstUpdate = true;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003543A File Offset: 0x0003363A
		protected void OnDisable()
		{
			this.UnbindActions();
			InputSystem.onAfterUpdate -= this.UpdateCallback;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00035454 File Offset: 0x00033654
		protected virtual void OnDestroy()
		{
			Camera camera;
			if (this.HasStereoCamera(out camera))
			{
				XRDevice.DisableAutoXRCameraTracking(camera, false);
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00035474 File Offset: 0x00033674
		protected void UpdateCallback()
		{
			if (this.m_IsFirstUpdate)
			{
				if (this.m_PositionInput.action != null)
				{
					this.m_CurrentPosition = this.m_PositionInput.action.ReadValue<Vector3>();
				}
				if (this.m_RotationInput.action != null)
				{
					this.m_CurrentRotation = this.m_RotationInput.action.ReadValue<Quaternion>();
				}
				this.ReadTrackingState();
				this.m_IsFirstUpdate = false;
			}
			if (InputState.currentUpdateType == InputUpdateType.BeforeRender)
			{
				this.OnBeforeRender();
				return;
			}
			this.OnUpdate();
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x000354F4 File Offset: 0x000336F4
		private unsafe void ReadTrackingState()
		{
			InputAction action = this.m_TrackingStateInput.action;
			if (action != null && !action.enabled)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.None;
				return;
			}
			if (action == null || action.m_BindingsCount == 0)
			{
				this.m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;
				return;
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			bool flag = false;
			if (state != null)
			{
				int actionIndexInState = action.m_ActionIndexInState;
				int totalBindingCount = state.totalBindingCount;
				for (int i = 0; i < totalBindingCount; i++)
				{
					ref InputActionState.BindingState ptr = ref state.bindingStates[i];
					if (ptr.actionIndex == actionIndexInState && !ptr.isComposite && ptr.controlCount > 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.m_CurrentTrackingState = (TrackedPoseDriver.TrackingStates)action.ReadValue<int>();
			}
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000355AE File Offset: 0x000337AE
		protected virtual void OnUpdate()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.Update || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x000355C7 File Offset: 0x000337C7
		protected virtual void OnBeforeRender()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.BeforeRender || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000355E0 File Offset: 0x000337E0
		protected virtual void PerformUpdate()
		{
			this.SetLocalTransform(this.m_CurrentPosition, this.m_CurrentRotation);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000355F4 File Offset: 0x000337F4
		protected virtual void SetLocalTransform(Vector3 newPosition, Quaternion newRotation)
		{
			bool flag = this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Position) > TrackedPoseDriver.TrackingStates.None;
			if ((this.m_IgnoreTrackingState || (this.m_CurrentTrackingState & TrackedPoseDriver.TrackingStates.Rotation) > TrackedPoseDriver.TrackingStates.None) && (this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationOnly))
			{
				base.transform.localRotation = newRotation;
			}
			if (flag && (this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.PositionOnly))
			{
				base.transform.localPosition = newPosition;
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0003566B File Offset: 0x0003386B
		private bool HasStereoCamera(out Camera cameraComponent)
		{
			return base.TryGetComponent<Camera>(out cameraComponent) && cameraComponent.stereoEnabled;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0003567F File Offset: 0x0003387F
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0003568C File Offset: 0x0003388C
		public InputAction positionAction
		{
			get
			{
				return this.m_PositionInput.action;
			}
			set
			{
				this.positionInput = new InputActionProperty(value);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x0003569A File Offset: 0x0003389A
		// (set) Token: 0x060009BD RID: 2493 RVA: 0x000356A7 File Offset: 0x000338A7
		public InputAction rotationAction
		{
			get
			{
				return this.m_RotationInput.action;
			}
			set
			{
				this.rotationInput = new InputActionProperty(value);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x000356B5 File Offset: 0x000338B5
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000356B7 File Offset: 0x000338B7
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_HasMigratedActions)
			{
				return;
			}
			this.m_PositionInput = new InputActionProperty(this.m_PositionAction);
			this.m_RotationInput = new InputActionProperty(this.m_RotationAction);
			this.m_HasMigratedActions = true;
		}

		// Token: 0x04000316 RID: 790
		[SerializeField]
		[Tooltip("Which Transform properties to update.")]
		private TrackedPoseDriver.TrackingType m_TrackingType;

		// Token: 0x04000317 RID: 791
		[SerializeField]
		[Tooltip("Updates the Transform properties after these phases of Input System event processing.")]
		private TrackedPoseDriver.UpdateType m_UpdateType;

		// Token: 0x04000318 RID: 792
		[SerializeField]
		[Tooltip("Ignore Tracking State and always treat the input pose as valid.")]
		private bool m_IgnoreTrackingState;

		// Token: 0x04000319 RID: 793
		[SerializeField]
		[Tooltip("The input action to read the position value of a tracked device. Must be a Vector 3 control type.")]
		private InputActionProperty m_PositionInput;

		// Token: 0x0400031A RID: 794
		[SerializeField]
		[Tooltip("The input action to read the rotation value of a tracked device. Must be a Quaternion control type.")]
		private InputActionProperty m_RotationInput;

		// Token: 0x0400031B RID: 795
		[SerializeField]
		[Tooltip("The input action to read the tracking state value of a tracked device. Identifies if position and rotation have valid data. Must be an Integer control type.")]
		private InputActionProperty m_TrackingStateInput;

		// Token: 0x0400031C RID: 796
		private Vector3 m_CurrentPosition = Vector3.zero;

		// Token: 0x0400031D RID: 797
		private Quaternion m_CurrentRotation = Quaternion.identity;

		// Token: 0x0400031E RID: 798
		private TrackedPoseDriver.TrackingStates m_CurrentTrackingState = TrackedPoseDriver.TrackingStates.Position | TrackedPoseDriver.TrackingStates.Rotation;

		// Token: 0x0400031F RID: 799
		private bool m_RotationBound;

		// Token: 0x04000320 RID: 800
		private bool m_PositionBound;

		// Token: 0x04000321 RID: 801
		private bool m_TrackingStateBound;

		// Token: 0x04000322 RID: 802
		private bool m_IsFirstUpdate = true;

		// Token: 0x04000323 RID: 803
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_PositionAction;

		// Token: 0x04000324 RID: 804
		[Obsolete]
		[SerializeField]
		[HideInInspector]
		private InputAction m_RotationAction;

		// Token: 0x04000325 RID: 805
		[SerializeField]
		[HideInInspector]
		private bool m_HasMigratedActions;

		// Token: 0x020001B8 RID: 440
		public enum TrackingType
		{
			// Token: 0x040008E6 RID: 2278
			RotationAndPosition,
			// Token: 0x040008E7 RID: 2279
			RotationOnly,
			// Token: 0x040008E8 RID: 2280
			PositionOnly
		}

		// Token: 0x020001B9 RID: 441
		[Flags]
		private enum TrackingStates
		{
			// Token: 0x040008EA RID: 2282
			None = 0,
			// Token: 0x040008EB RID: 2283
			Position = 1,
			// Token: 0x040008EC RID: 2284
			Rotation = 2
		}

		// Token: 0x020001BA RID: 442
		public enum UpdateType
		{
			// Token: 0x040008EE RID: 2286
			UpdateAndBeforeRender,
			// Token: 0x040008EF RID: 2287
			Update,
			// Token: 0x040008F0 RID: 2288
			BeforeRender
		}
	}
}
