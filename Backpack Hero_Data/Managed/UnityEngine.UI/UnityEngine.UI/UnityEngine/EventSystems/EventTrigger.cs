using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000065 RID: 101
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00018C1E File Offset: 0x00016E1E
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x00018C26 File Offset: 0x00016E26
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates
		{
			get
			{
				return this.triggers;
			}
			set
			{
				this.triggers = value;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00018C2F File Offset: 0x00016E2F
		protected EventTrigger()
		{
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00018C37 File Offset: 0x00016E37
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00018C52 File Offset: 0x00016E52
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018C5C File Offset: 0x00016E5C
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int count = this.triggers.Count;
			int i = 0;
			int count2 = this.triggers.Count;
			while (i < count2)
			{
				EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
				i++;
			}
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018CB7 File Offset: 0x00016EB7
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018CC1 File Offset: 0x00016EC1
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018CCB File Offset: 0x00016ECB
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00018CD5 File Offset: 0x00016ED5
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00018CDF File Offset: 0x00016EDF
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00018CE9 File Offset: 0x00016EE9
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00018CF3 File Offset: 0x00016EF3
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00018CFD File Offset: 0x00016EFD
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00018D08 File Offset: 0x00016F08
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00018D13 File Offset: 0x00016F13
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00018D1D File Offset: 0x00016F1D
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00018D28 File Offset: 0x00016F28
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00018D32 File Offset: 0x00016F32
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018D3D File Offset: 0x00016F3D
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00018D48 File Offset: 0x00016F48
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018D53 File Offset: 0x00016F53
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018D5E File Offset: 0x00016F5E
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x040001DD RID: 477
		[FormerlySerializedAs("delegates")]
		[SerializeField]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x020000C4 RID: 196
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x020000C5 RID: 197
		[Serializable]
		public class Entry
		{
			// Token: 0x04000345 RID: 837
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000346 RID: 838
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
