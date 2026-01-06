using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000034 RID: 52
	[AddComponentMenu("UI/Selectable", 35)]
	[ExecuteAlways]
	[SelectionBase]
	[DisallowMultipleComponent]
	public class Selectable : UIBehaviour, IMoveHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00012FA4 File Offset: 0x000111A4
		public static Selectable[] allSelectablesArray
		{
			get
			{
				Selectable[] array = new Selectable[Selectable.s_SelectableCount];
				Array.Copy(Selectable.s_Selectables, array, Selectable.s_SelectableCount);
				return array;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00012FCD File Offset: 0x000111CD
		public static int allSelectableCount
		{
			get
			{
				return Selectable.s_SelectableCount;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00012FD4 File Offset: 0x000111D4
		[Obsolete("Replaced with allSelectablesArray to have better performance when disabling a element", false)]
		public static List<Selectable> allSelectables
		{
			get
			{
				return new List<Selectable>(Selectable.allSelectablesArray);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012FE0 File Offset: 0x000111E0
		public static int AllSelectablesNoAlloc(Selectable[] selectables)
		{
			int num = ((selectables.Length < Selectable.s_SelectableCount) ? selectables.Length : Selectable.s_SelectableCount);
			Array.Copy(Selectable.s_Selectables, selectables, num);
			return num;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001300F File Offset: 0x0001120F
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00013017 File Offset: 0x00011217
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001302D File Offset: 0x0001122D
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00013035 File Offset: 0x00011235
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001304B File Offset: 0x0001124B
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00013053 File Offset: 0x00011253
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00013069 File Offset: 0x00011269
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00013071 File Offset: 0x00011271
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00013087 File Offset: 0x00011287
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0001308F File Offset: 0x0001128F
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x000130A5 File Offset: 0x000112A5
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x000130AD File Offset: 0x000112AD
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000130C3 File Offset: 0x000112C3
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000130CC File Offset: 0x000112CC
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					if (!this.m_Interactable && EventSystem.current != null && EventSystem.current.currentSelectedGameObject == base.gameObject)
					{
						EventSystem.current.SetSelectedGameObject(null);
					}
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00013124 File Offset: 0x00011324
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001312C File Offset: 0x0001132C
		private bool isPointerInside { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00013135 File Offset: 0x00011335
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001313D File Offset: 0x0001133D
		private bool isPointerDown { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00013146 File Offset: 0x00011346
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0001314E File Offset: 0x0001134E
		private bool hasSelection { get; set; }

		// Token: 0x060003CE RID: 974 RVA: 0x00013158 File Offset: 0x00011358
		protected Selectable()
		{
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000131B3 File Offset: 0x000113B3
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x000131C0 File Offset: 0x000113C0
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x000131C9 File Offset: 0x000113C9
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000131D1 File Offset: 0x000113D1
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000131F0 File Offset: 0x000113F0
		protected override void OnCanvasGroupChanged()
		{
			bool flag = this.ParentGroupAllowsInteraction();
			if (flag != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = flag;
				this.OnSetProperty();
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001321C File Offset: 0x0001141C
		private bool ParentGroupAllowsInteraction()
		{
			Transform transform = base.transform;
			while (transform != null)
			{
				transform.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (this.m_CanvasGroupCache[i].enabled && !this.m_CanvasGroupCache[i].interactable)
					{
						return false;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						return true;
					}
				}
				transform = transform.parent;
			}
			return true;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000132A2 File Offset: 0x000114A2
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000132B4 File Offset: 0x000114B4
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000132BC File Offset: 0x000114BC
		protected override void OnEnable()
		{
			if (this.m_EnableCalled)
			{
				return;
			}
			base.OnEnable();
			if (Selectable.s_SelectableCount == Selectable.s_Selectables.Length)
			{
				Selectable[] array = new Selectable[Selectable.s_Selectables.Length * 2];
				Array.Copy(Selectable.s_Selectables, array, Selectable.s_Selectables.Length);
				Selectable.s_Selectables = array;
			}
			if (EventSystem.current && EventSystem.current.currentSelectedGameObject == base.gameObject)
			{
				this.hasSelection = true;
			}
			this.m_CurrentIndex = Selectable.s_SelectableCount;
			Selectable.s_Selectables[this.m_CurrentIndex] = this;
			Selectable.s_SelectableCount++;
			this.isPointerDown = false;
			this.m_GroupsAllowInteraction = this.ParentGroupAllowsInteraction();
			this.DoStateTransition(this.currentSelectionState, true);
			this.m_EnableCalled = true;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013382 File Offset: 0x00011582
		protected override void OnTransformParentChanged()
		{
			base.OnTransformParentChanged();
			this.OnCanvasGroupChanged();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013390 File Offset: 0x00011590
		private void OnSetProperty()
		{
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000133A0 File Offset: 0x000115A0
		protected override void OnDisable()
		{
			if (!this.m_EnableCalled)
			{
				return;
			}
			Selectable.s_SelectableCount--;
			Selectable.s_Selectables[Selectable.s_SelectableCount].m_CurrentIndex = this.m_CurrentIndex;
			Selectable.s_Selectables[this.m_CurrentIndex] = Selectable.s_Selectables[Selectable.s_SelectableCount];
			Selectable.s_Selectables[Selectable.s_SelectableCount] = null;
			this.InstantClearState();
			base.OnDisable();
			this.m_EnableCalled = false;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001340E File Offset: 0x0001160E
		private void OnApplicationFocus(bool hasFocus)
		{
			if (!hasFocus && this.IsPressed())
			{
				this.InstantClearState();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00013421 File Offset: 0x00011621
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				if (!this.IsInteractable())
				{
					return Selectable.SelectionState.Disabled;
				}
				if (this.isPointerDown)
				{
					return Selectable.SelectionState.Pressed;
				}
				if (this.hasSelection)
				{
					return Selectable.SelectionState.Selected;
				}
				if (this.isPointerInside)
				{
					return Selectable.SelectionState.Highlighted;
				}
				return Selectable.SelectionState.Normal;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001344C File Offset: 0x0001164C
		protected virtual void InstantClearState()
		{
			string normalTrigger = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(normalTrigger);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000134B4 File Offset: 0x000116B4
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			Color color;
			Sprite sprite;
			string text;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				color = this.m_Colors.normalColor;
				sprite = null;
				text = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				color = this.m_Colors.highlightedColor;
				sprite = this.m_SpriteState.highlightedSprite;
				text = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				color = this.m_Colors.pressedColor;
				sprite = this.m_SpriteState.pressedSprite;
				text = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Selected:
				color = this.m_Colors.selectedColor;
				sprite = this.m_SpriteState.selectedSprite;
				text = this.m_AnimationTriggers.selectedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				color = this.m_Colors.disabledColor;
				sprite = this.m_SpriteState.disabledSprite;
				text = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				color = Color.black;
				sprite = null;
				text = string.Empty;
				break;
			}
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(color * this.m_Colors.colorMultiplier, instant);
				return;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(sprite);
				return;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(text);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000135FC File Offset: 0x000117FC
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 vector = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 vector2 = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, vector));
			float num = float.NegativeInfinity;
			float num2 = float.NegativeInfinity;
			bool flag = this.navigation.wrapAround && (this.m_Navigation.mode == Navigation.Mode.Vertical || this.m_Navigation.mode == Navigation.Mode.Horizontal);
			Selectable selectable = null;
			Selectable selectable2 = null;
			for (int i = 0; i < Selectable.s_SelectableCount; i++)
			{
				Selectable selectable3 = Selectable.s_Selectables[i];
				if (!(selectable3 == this) && selectable3.IsInteractable() && selectable3.navigation.mode != Navigation.Mode.None)
				{
					RectTransform rectTransform = selectable3.transform as RectTransform;
					Vector3 vector3 = ((rectTransform != null) ? rectTransform.rect.center : Vector3.zero);
					Vector3 vector4 = selectable3.transform.TransformPoint(vector3) - vector2;
					float num3 = Vector3.Dot(dir, vector4);
					if (flag && num3 < 0f)
					{
						float num4 = -num3 * vector4.sqrMagnitude;
						if (num4 > num2)
						{
							num2 = num4;
							selectable2 = selectable3;
						}
					}
					else if (num3 > 0f)
					{
						float num4 = num3 / vector4.sqrMagnitude;
						if (num4 > num)
						{
							num = num4;
							selectable = selectable3;
						}
					}
				}
			}
			if (flag && null == selectable)
			{
				return selectable2;
			}
			return selectable;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000137A0 File Offset: 0x000119A0
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013825 File Offset: 0x00011A25
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013844 File Offset: 0x00011A44
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013898 File Offset: 0x00011A98
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000138EC File Offset: 0x00011AEC
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013940 File Offset: 0x00011B40
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013994 File Offset: 0x00011B94
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				return;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				return;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				return;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				return;
			default:
				return;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000139F6 File Offset: 0x00011BF6
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, instant ? 0f : this.m_Colors.fadeDuration, true, true);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00013A2A File Offset: 0x00011C2A
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00013A48 File Offset: 0x00011C48
		private void TriggerAnimation(string triggername)
		{
			if (this.transition != Selectable.Transition.Animation || this.animator == null || !this.animator.isActiveAndEnabled || !this.animator.hasBoundPlayables || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.selectedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00013B09 File Offset: 0x00011D09
		protected bool IsHighlighted()
		{
			return this.IsActive() && this.IsInteractable() && (this.isPointerInside && !this.isPointerDown) && !this.hasSelection;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00013B38 File Offset: 0x00011D38
		protected bool IsPressed()
		{
			return this.IsActive() && this.IsInteractable() && this.isPointerDown;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013B52 File Offset: 0x00011D52
		private void EvaluateAndTransitionToSelectionState()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(this.currentSelectionState, false);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00013B74 File Offset: 0x00011D74
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None && EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00013BCD File Offset: 0x00011DCD
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00013BE5 File Offset: 0x00011DE5
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013BF4 File Offset: 0x00011DF4
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00013C03 File Offset: 0x00011E03
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013C12 File Offset: 0x00011E12
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00013C21 File Offset: 0x00011E21
		public virtual void Select()
		{
			if (EventSystem.current == null || EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x0400014A RID: 330
		protected static Selectable[] s_Selectables = new Selectable[10];

		// Token: 0x0400014B RID: 331
		protected static int s_SelectableCount = 0;

		// Token: 0x0400014C RID: 332
		private bool m_EnableCalled;

		// Token: 0x0400014D RID: 333
		[FormerlySerializedAs("navigation")]
		[SerializeField]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x0400014E RID: 334
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x0400014F RID: 335
		[FormerlySerializedAs("colors")]
		[SerializeField]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x04000150 RID: 336
		[FormerlySerializedAs("spriteState")]
		[SerializeField]
		private SpriteState m_SpriteState;

		// Token: 0x04000151 RID: 337
		[FormerlySerializedAs("animationTriggers")]
		[SerializeField]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x04000152 RID: 338
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x04000153 RID: 339
		[FormerlySerializedAs("highlightGraphic")]
		[FormerlySerializedAs("m_HighlightGraphic")]
		[SerializeField]
		private Graphic m_TargetGraphic;

		// Token: 0x04000154 RID: 340
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x04000155 RID: 341
		protected int m_CurrentIndex = -1;

		// Token: 0x04000159 RID: 345
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x020000AA RID: 170
		public enum Transition
		{
			// Token: 0x040002F6 RID: 758
			None,
			// Token: 0x040002F7 RID: 759
			ColorTint,
			// Token: 0x040002F8 RID: 760
			SpriteSwap,
			// Token: 0x040002F9 RID: 761
			Animation
		}

		// Token: 0x020000AB RID: 171
		protected enum SelectionState
		{
			// Token: 0x040002FB RID: 763
			Normal,
			// Token: 0x040002FC RID: 764
			Highlighted,
			// Token: 0x040002FD RID: 765
			Pressed,
			// Token: 0x040002FE RID: 766
			Selected,
			// Token: 0x040002FF RID: 767
			Disabled
		}
	}
}
