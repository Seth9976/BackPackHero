using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity.VisualScripting
{
	// Token: 0x020000B4 RID: 180
	[AddComponentMenu("")]
	[Obsolete("UnityMessageListener is deprecated and has been replaced by separate message listeners for each event, eg. UnityOnCollisionEnterMessageListener or UnityOnButtonClickMessageListener.")]
	public sealed class UnityMessageListener : MessageListener, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, ISelectHandler, IDeselectHandler, ISubmitHandler, ICancelHandler, IMoveHandler
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00009B76 File Offset: 0x00007D76
		private void Start()
		{
			this.AddGUIListeners();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00009B80 File Offset: 0x00007D80
		public void AddGUIListeners()
		{
			Button component = base.GetComponent<Button>();
			if (component != null)
			{
				Button.ButtonClickedEvent onClick = component.onClick;
				if (onClick != null)
				{
					onClick.AddListener(delegate
					{
						EventBus.Trigger("OnButtonClick", base.gameObject);
					});
				}
			}
			Toggle component2 = base.GetComponent<Toggle>();
			if (component2 != null)
			{
				Toggle.ToggleEvent onValueChanged = component2.onValueChanged;
				if (onValueChanged != null)
				{
					onValueChanged.AddListener(delegate(bool value)
					{
						EventBus.Trigger<bool>("OnToggleValueChanged", base.gameObject, value);
					});
				}
			}
			Slider component3 = base.GetComponent<Slider>();
			if (component3 != null)
			{
				Slider.SliderEvent onValueChanged2 = component3.onValueChanged;
				if (onValueChanged2 != null)
				{
					onValueChanged2.AddListener(delegate(float value)
					{
						EventBus.Trigger<float>("OnSliderValueChanged", base.gameObject, value);
					});
				}
			}
			Scrollbar component4 = base.GetComponent<Scrollbar>();
			if (component4 != null)
			{
				Scrollbar.ScrollEvent onValueChanged3 = component4.onValueChanged;
				if (onValueChanged3 != null)
				{
					onValueChanged3.AddListener(delegate(float value)
					{
						EventBus.Trigger<float>("OnScrollbarValueChanged", base.gameObject, value);
					});
				}
			}
			Dropdown component5 = base.GetComponent<Dropdown>();
			if (component5 != null)
			{
				Dropdown.DropdownEvent onValueChanged4 = component5.onValueChanged;
				if (onValueChanged4 != null)
				{
					onValueChanged4.AddListener(delegate(int value)
					{
						EventBus.Trigger<int>("OnDropdownValueChanged", base.gameObject, value);
					});
				}
			}
			InputField component6 = base.GetComponent<InputField>();
			if (component6 != null)
			{
				InputField.OnChangeEvent onValueChanged5 = component6.onValueChanged;
				if (onValueChanged5 != null)
				{
					onValueChanged5.AddListener(delegate(string value)
					{
						EventBus.Trigger<string>("OnInputFieldValueChanged", base.gameObject, value);
					});
				}
			}
			InputField component7 = base.GetComponent<InputField>();
			if (component7 != null)
			{
				InputField.EndEditEvent onEndEdit = component7.onEndEdit;
				if (onEndEdit != null)
				{
					onEndEdit.AddListener(delegate(string value)
					{
						EventBus.Trigger<string>("OnInputFieldEndEdit", base.gameObject, value);
					});
				}
			}
			ScrollRect component8 = base.GetComponent<ScrollRect>();
			if (component8 == null)
			{
				return;
			}
			ScrollRect.ScrollRectEvent onValueChanged6 = component8.onValueChanged;
			if (onValueChanged6 == null)
			{
				return;
			}
			onValueChanged6.AddListener(delegate(Vector2 value)
			{
				EventBus.Trigger<Vector2>("OnScrollRectValueChanged", base.gameObject, value);
			});
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00009CCB File Offset: 0x00007ECB
		public void OnPointerEnter(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerEnter", base.gameObject, eventData);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00009CDE File Offset: 0x00007EDE
		public void OnPointerExit(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerExit", base.gameObject, eventData);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00009CF1 File Offset: 0x00007EF1
		public void OnPointerDown(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerDown", base.gameObject, eventData);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00009D04 File Offset: 0x00007F04
		public void OnPointerUp(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerUp", base.gameObject, eventData);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00009D17 File Offset: 0x00007F17
		public void OnPointerClick(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnPointerClick", base.gameObject, eventData);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00009D2A File Offset: 0x00007F2A
		public void OnBeginDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnBeginDrag", base.gameObject, eventData);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00009D3D File Offset: 0x00007F3D
		public void OnDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnDrag", base.gameObject, eventData);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00009D50 File Offset: 0x00007F50
		public void OnEndDrag(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnEndDrag", base.gameObject, eventData);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00009D63 File Offset: 0x00007F63
		public void OnDrop(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnDrop", base.gameObject, eventData);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00009D76 File Offset: 0x00007F76
		public void OnScroll(PointerEventData eventData)
		{
			EventBus.Trigger<PointerEventData>("OnScroll", base.gameObject, eventData);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00009D89 File Offset: 0x00007F89
		public void OnSelect(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnSelect", base.gameObject, eventData);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00009D9C File Offset: 0x00007F9C
		public void OnDeselect(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnDeselect", base.gameObject, eventData);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00009DAF File Offset: 0x00007FAF
		public void OnSubmit(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnSubmit", base.gameObject, eventData);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009DC2 File Offset: 0x00007FC2
		public void OnCancel(BaseEventData eventData)
		{
			EventBus.Trigger<BaseEventData>("OnCancel", base.gameObject, eventData);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00009DD5 File Offset: 0x00007FD5
		public void OnMove(AxisEventData eventData)
		{
			EventBus.Trigger<AxisEventData>("OnMove", base.gameObject, eventData);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00009DE8 File Offset: 0x00007FE8
		private void OnBecameInvisible()
		{
			EventBus.Trigger("OnBecameInvisible", base.gameObject);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00009DFA File Offset: 0x00007FFA
		private void OnBecameVisible()
		{
			EventBus.Trigger("OnBecameVisible", base.gameObject);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00009E0C File Offset: 0x0000800C
		private void OnCollisionEnter(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionEnter", base.gameObject, collision);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00009E1F File Offset: 0x0000801F
		private void OnCollisionExit(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionExit", base.gameObject, collision);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00009E32 File Offset: 0x00008032
		private void OnCollisionStay(Collision collision)
		{
			EventBus.Trigger<Collision>("OnCollisionStay", base.gameObject, collision);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00009E45 File Offset: 0x00008045
		private void OnCollisionEnter2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionEnter2D", base.gameObject, collision);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00009E58 File Offset: 0x00008058
		private void OnCollisionExit2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionExit2D", base.gameObject, collision);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00009E6B File Offset: 0x0000806B
		private void OnCollisionStay2D(Collision2D collision)
		{
			EventBus.Trigger<Collision2D>("OnCollisionStay2D", base.gameObject, collision);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00009E7E File Offset: 0x0000807E
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			EventBus.Trigger<ControllerColliderHit>("OnControllerColliderHit", base.gameObject, hit);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00009E91 File Offset: 0x00008091
		private void OnJointBreak(float breakForce)
		{
			EventBus.Trigger<float>("OnJointBreak", base.gameObject, breakForce);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00009EA4 File Offset: 0x000080A4
		private void OnJointBreak2D(Joint2D brokenJoint)
		{
			EventBus.Trigger<Joint2D>("OnJointBreak2D", base.gameObject, brokenJoint);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00009EB7 File Offset: 0x000080B7
		private void OnMouseDown()
		{
			EventBus.Trigger("OnMouseDown", base.gameObject);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00009EC9 File Offset: 0x000080C9
		private void OnMouseDrag()
		{
			EventBus.Trigger("OnMouseDrag", base.gameObject);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00009EDB File Offset: 0x000080DB
		private void OnMouseEnter()
		{
			EventBus.Trigger("OnMouseEnter", base.gameObject);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00009EED File Offset: 0x000080ED
		private void OnMouseExit()
		{
			EventBus.Trigger("OnMouseExit", base.gameObject);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00009EFF File Offset: 0x000080FF
		private void OnMouseOver()
		{
			EventBus.Trigger("OnMouseOver", base.gameObject);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00009F11 File Offset: 0x00008111
		private void OnMouseUp()
		{
			EventBus.Trigger("OnMouseUp", base.gameObject);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00009F23 File Offset: 0x00008123
		private void OnMouseUpAsButton()
		{
			EventBus.Trigger("OnMouseUpAsButton", base.gameObject);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00009F35 File Offset: 0x00008135
		private void OnParticleCollision(GameObject other)
		{
			EventBus.Trigger<GameObject>("OnParticleCollision", base.gameObject, other);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00009F48 File Offset: 0x00008148
		private void OnTransformChildrenChanged()
		{
			EventBus.Trigger("OnTransformChildrenChanged", base.gameObject);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00009F5A File Offset: 0x0000815A
		private void OnTransformParentChanged()
		{
			EventBus.Trigger("OnTransformParentChanged", base.gameObject);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00009F6C File Offset: 0x0000816C
		private void OnTriggerEnter(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerEnter", base.gameObject, other);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00009F7F File Offset: 0x0000817F
		private void OnTriggerExit(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerExit", base.gameObject, other);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00009F92 File Offset: 0x00008192
		private void OnTriggerStay(Collider other)
		{
			EventBus.Trigger<Collider>("OnTriggerStay", base.gameObject, other);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00009FA5 File Offset: 0x000081A5
		private void OnTriggerEnter2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerEnter2D", base.gameObject, other);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00009FB8 File Offset: 0x000081B8
		private void OnTriggerExit2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerExit2D", base.gameObject, other);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00009FCB File Offset: 0x000081CB
		private void OnTriggerStay2D(Collider2D other)
		{
			EventBus.Trigger<Collider2D>("OnTriggerStay2D", base.gameObject, other);
		}
	}
}
