using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000092 RID: 146
	[AddComponentMenu("Input/On-Screen Stick")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/OnScreen.html#on-screen-sticks")]
	public class OnScreenStick : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x0003D7DE File Offset: 0x0003B9DE
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.BeginInteraction(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003D809 File Offset: 0x0003BA09
		public void OnDrag(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			if (eventData == null)
			{
				throw new ArgumentNullException("eventData");
			}
			this.MoveStick(eventData.position, eventData.pressEventCamera);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003D834 File Offset: 0x0003BA34
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_UseIsolatedInputActions)
			{
				return;
			}
			this.EndInteraction();
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003D848 File Offset: 0x0003BA48
		private void Start()
		{
			if (this.m_UseIsolatedInputActions)
			{
				this.m_RaycastResults = new List<RaycastResult>();
				this.m_PointerEventData = new PointerEventData(EventSystem.current);
				if (this.m_PointerDownAction == null || this.m_PointerDownAction.bindings.Count == 0)
				{
					if (this.m_PointerDownAction == null)
					{
						this.m_PointerDownAction = new InputAction();
					}
					this.m_PointerDownAction.AddBinding("<Mouse>/leftButton", null, null, null);
					this.m_PointerDownAction.AddBinding("<Pen>/tip", null, null, null);
					this.m_PointerDownAction.AddBinding("<Touchscreen>/touch*/press", null, null, null);
					this.m_PointerDownAction.AddBinding("<XRController>/trigger", null, null, null);
				}
				if (this.m_PointerMoveAction == null || this.m_PointerMoveAction.bindings.Count == 0)
				{
					if (this.m_PointerMoveAction == null)
					{
						this.m_PointerMoveAction = new InputAction();
					}
					this.m_PointerMoveAction.AddBinding("<Mouse>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Pen>/position", null, null, null);
					this.m_PointerMoveAction.AddBinding("<Touchscreen>/touch*/position", null, null, null);
				}
				this.m_PointerDownAction.started += this.OnPointerDown;
				this.m_PointerDownAction.canceled += this.OnPointerUp;
				this.m_PointerDownAction.Enable();
				this.m_PointerMoveAction.Enable();
			}
			this.m_StartPos = ((RectTransform)base.transform).anchoredPosition;
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			this.m_PointerDownPos = this.m_StartPos;
			GameObject gameObject = new GameObject("DynamicOriginClickable", new Type[] { typeof(Image) });
			gameObject.transform.SetParent(base.transform);
			Image component = gameObject.GetComponent<Image>();
			component.color = new Color(1f, 1f, 1f, 0f);
			RectTransform rectTransform = (RectTransform)gameObject.transform;
			rectTransform.sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			rectTransform.localScale = new Vector3(1f, 1f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			component.sprite = SpriteUtilities.CreateCircleSprite(16, new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
			component.alphaHitTestMinimumThreshold = 0.5f;
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		private void BeginInteraction(Vector2 pointerPosition, Camera uiCamera)
		{
			Transform parent = base.transform.parent;
			RectTransform rectTransform = ((parent != null) ? parent.GetComponentInParent<RectTransform>() : null);
			if (rectTransform == null)
			{
				Debug.LogError("OnScreenStick needs to be attached as a child to a UI Canvas to function properly.");
				return;
			}
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pointerPosition, uiCamera, out this.m_PointerDownPos);
				this.MoveStick(pointerPosition, uiCamera);
				return;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pointerPosition, uiCamera, out vector);
				this.m_PointerDownPos = (((RectTransform)base.transform).anchoredPosition = vector);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003DB58 File Offset: 0x0003BD58
		private void MoveStick(Vector2 pointerPosition, Camera uiCamera)
		{
			Transform parent = base.transform.parent;
			RectTransform rectTransform = ((parent != null) ? parent.GetComponentInParent<RectTransform>() : null);
			if (rectTransform == null)
			{
				Debug.LogError("OnScreenStick needs to be attached as a child to a UI Canvas to function properly.");
				return;
			}
			Vector2 vector;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pointerPosition, uiCamera, out vector);
			Vector2 vector2 = vector - this.m_PointerDownPos;
			switch (this.m_Behaviour)
			{
			case OnScreenStick.Behaviour.RelativePositionWithStaticOrigin:
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + vector2;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithStaticOrigin:
				vector2 = vector - this.m_StartPos;
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_StartPos + vector2;
				break;
			case OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin:
				vector2 = Vector2.ClampMagnitude(vector2, this.movementRange);
				((RectTransform)base.transform).anchoredPosition = this.m_PointerDownPos + vector2;
				break;
			}
			Vector2 vector3 = new Vector2(vector2.x / this.movementRange, vector2.y / this.movementRange);
			base.SendValueToControl<Vector2>(vector3);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003DC8C File Offset: 0x0003BE8C
		private void EndInteraction()
		{
			((RectTransform)base.transform).anchoredPosition = (this.m_PointerDownPos = this.m_StartPos);
			base.SendValueToControl<Vector2>(Vector2.zero);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		private void OnPointerDown(InputAction.CallbackContext ctx)
		{
			Vector2 vector = Vector2.zero;
			InputControl control = ctx.control;
			Pointer pointer = ((control != null) ? control.device : null) as Pointer;
			if (pointer != null)
			{
				vector = pointer.position.ReadValue();
			}
			this.m_PointerEventData.position = vector;
			EventSystem.current.RaycastAll(this.m_PointerEventData, this.m_RaycastResults);
			if (this.m_RaycastResults.Count == 0)
			{
				return;
			}
			bool flag = false;
			foreach (RaycastResult raycastResult in this.m_RaycastResults)
			{
				if (!(raycastResult.gameObject != base.gameObject))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return;
			}
			this.BeginInteraction(vector, this.GetCameraFromCanvas());
			this.m_PointerMoveAction.performed += this.OnPointerMove;
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		private void OnPointerMove(InputAction.CallbackContext ctx)
		{
			Vector2 vector = ((Pointer)ctx.control.device).position.ReadValue();
			this.MoveStick(vector, this.GetCameraFromCanvas());
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0003DDEE File Offset: 0x0003BFEE
		private void OnPointerUp(InputAction.CallbackContext ctx)
		{
			this.EndInteraction();
			this.m_PointerMoveAction.performed -= this.OnPointerMove;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0003DE10 File Offset: 0x0003C010
		private Camera GetCameraFromCanvas()
		{
			Canvas componentInParent = base.GetComponentInParent<Canvas>();
			RenderMode? renderMode = ((componentInParent != null) ? new RenderMode?(componentInParent.renderMode) : null);
			RenderMode? renderMode2 = renderMode;
			RenderMode renderMode3 = RenderMode.ScreenSpaceOverlay;
			if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)))
			{
				renderMode2 = renderMode;
				renderMode3 = RenderMode.ScreenSpaceCamera;
				if (!((renderMode2.GetValueOrDefault() == renderMode3) & (renderMode2 != null)) || !(((componentInParent != null) ? componentInParent.worldCamera : null) == null))
				{
					return ((componentInParent != null) ? componentInParent.worldCamera : null) ?? Camera.main;
				}
			}
			return null;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0003DE9C File Offset: 0x0003C09C
		private void OnDrawGizmosSelected()
		{
			Gizmos.matrix = ((RectTransform)base.transform.parent).localToWorldMatrix;
			Vector2 vector = ((RectTransform)base.transform).anchoredPosition;
			if (Application.isPlaying)
			{
				vector = this.m_StartPos;
			}
			Gizmos.color = new Color32(84, 173, 219, byte.MaxValue);
			Vector2 vector2 = vector;
			if (Application.isPlaying && this.m_Behaviour == OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				vector2 = this.m_PointerDownPos;
			}
			this.DrawGizmoCircle(vector2, this.m_MovementRange);
			if (this.m_Behaviour != OnScreenStick.Behaviour.ExactPositionWithDynamicOrigin)
			{
				return;
			}
			Gizmos.color = new Color32(158, 84, 219, byte.MaxValue);
			this.DrawGizmoCircle(vector, this.m_DynamicOriginRange);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0003DF64 File Offset: 0x0003C164
		private void DrawGizmoCircle(Vector2 center, float radius)
		{
			for (int i = 0; i < 32; i++)
			{
				float num = (float)i / 32f * 3.1415927f * 2f;
				float num2 = (float)(i + 1) / 32f * 3.1415927f * 2f;
				Gizmos.DrawLine(new Vector3(center.x + Mathf.Cos(num) * radius, center.y + Mathf.Sin(num) * radius, 0f), new Vector3(center.x + Mathf.Cos(num2) * radius, center.y + Mathf.Sin(num2) * radius, 0f));
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0003E008 File Offset: 0x0003C208
		private void UpdateDynamicOriginClickableArea()
		{
			Transform transform = base.transform.Find("DynamicOriginClickable");
			if (transform)
			{
				((RectTransform)transform).sizeDelta = new Vector2(this.m_DynamicOriginRange * 2f, this.m_DynamicOriginRange * 2f);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0003E056 File Offset: 0x0003C256
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0003E05E File Offset: 0x0003C25E
		public float movementRange
		{
			get
			{
				return this.m_MovementRange;
			}
			set
			{
				this.m_MovementRange = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0003E067 File Offset: 0x0003C267
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x0003E06F File Offset: 0x0003C26F
		public float dynamicOriginRange
		{
			get
			{
				return this.m_DynamicOriginRange;
			}
			set
			{
				if (this.m_DynamicOriginRange != value)
				{
					this.m_DynamicOriginRange = value;
					this.UpdateDynamicOriginClickableArea();
				}
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0003E087 File Offset: 0x0003C287
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0003E08F File Offset: 0x0003C28F
		public bool useIsolatedInputActions
		{
			get
			{
				return this.m_UseIsolatedInputActions;
			}
			set
			{
				this.m_UseIsolatedInputActions = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0003E098 File Offset: 0x0003C298
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0003E0A0 File Offset: 0x0003C2A0
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0003E0A9 File Offset: 0x0003C2A9
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0003E0B1 File Offset: 0x0003C2B1
		public OnScreenStick.Behaviour behaviour
		{
			get
			{
				return this.m_Behaviour;
			}
			set
			{
				this.m_Behaviour = value;
			}
		}

		// Token: 0x04000420 RID: 1056
		private const string kDynamicOriginClickable = "DynamicOriginClickable";

		// Token: 0x04000421 RID: 1057
		[FormerlySerializedAs("movementRange")]
		[SerializeField]
		[Min(0f)]
		private float m_MovementRange = 50f;

		// Token: 0x04000422 RID: 1058
		[SerializeField]
		[Tooltip("Defines the circular region where the onscreen control may have it's origin placed.")]
		[Min(0f)]
		private float m_DynamicOriginRange = 100f;

		// Token: 0x04000423 RID: 1059
		[InputControl(layout = "Vector2")]
		[SerializeField]
		private string m_ControlPath;

		// Token: 0x04000424 RID: 1060
		[SerializeField]
		[Tooltip("Choose how the onscreen stick will move relative to it's origin and the press position.\n\nRelativePositionWithStaticOrigin: The control's center of origin is fixed. The control will begin un-actuated at it's centered position and then move relative to the pointer or finger motion.\n\nExactPositionWithStaticOrigin: The control's center of origin is fixed. The stick will immediately jump to the exact position of the click or touch and begin tracking motion from there.\n\nExactPositionWithDynamicOrigin: The control's center of origin is determined by the initial press position. The stick will begin un-actuated at this center position and then track the current pointer or finger position.")]
		private OnScreenStick.Behaviour m_Behaviour;

		// Token: 0x04000425 RID: 1061
		[SerializeField]
		[Tooltip("Set this to true to prevent cancellation of pointer events due to device switching. Cancellation will appear as the stick jumping back and forth between the pointer position and the stick center.")]
		private bool m_UseIsolatedInputActions;

		// Token: 0x04000426 RID: 1062
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer down events on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerDownAction;

		// Token: 0x04000427 RID: 1063
		[SerializeField]
		[Tooltip("The action that will be used to detect pointer movement on the stick control. Note that if no bindings are set, default ones will be provided.")]
		private InputAction m_PointerMoveAction;

		// Token: 0x04000428 RID: 1064
		private Vector3 m_StartPos;

		// Token: 0x04000429 RID: 1065
		private Vector2 m_PointerDownPos;

		// Token: 0x0400042A RID: 1066
		[NonSerialized]
		private List<RaycastResult> m_RaycastResults;

		// Token: 0x0400042B RID: 1067
		[NonSerialized]
		private PointerEventData m_PointerEventData;

		// Token: 0x020001D8 RID: 472
		public enum Behaviour
		{
			// Token: 0x040009A3 RID: 2467
			RelativePositionWithStaticOrigin,
			// Token: 0x040009A4 RID: 2468
			ExactPositionWithStaticOrigin,
			// Token: 0x040009A5 RID: 2469
			ExactPositionWithDynamicOrigin
		}
	}
}
