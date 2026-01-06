using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D7 RID: 471
	internal class EventCallbackList
	{
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0003B6AF File Offset: 0x000398AF
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x0003B6B7 File Offset: 0x000398B7
		public int trickleDownCallbackCount { get; private set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003B6C0 File Offset: 0x000398C0
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0003B6C8 File Offset: 0x000398C8
		public int bubbleUpCallbackCount { get; private set; }

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003B6D1 File Offset: 0x000398D1
		public EventCallbackList()
		{
			this.m_List = new List<EventCallbackFunctorBase>();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0003B6F6 File Offset: 0x000398F6
		public EventCallbackList(EventCallbackList source)
		{
			this.m_List = new List<EventCallbackFunctorBase>(source.m_List);
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x0003B724 File Offset: 0x00039924
		public bool Contains(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			return this.Find(eventTypeId, callback, phase) != null;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0003B744 File Offset: 0x00039944
		public EventCallbackFunctorBase Find(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					return this.m_List[i];
				}
			}
			return null;
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0003B79C File Offset: 0x0003999C
		public bool Remove(long eventTypeId, Delegate callback, CallbackPhase phase)
		{
			for (int i = 0; i < this.m_List.Count; i++)
			{
				bool flag = this.m_List[i].IsEquivalentTo(eventTypeId, callback, phase);
				if (flag)
				{
					this.m_List.RemoveAt(i);
					bool flag2 = phase == CallbackPhase.TrickleDownAndTarget;
					if (flag2)
					{
						int num = this.trickleDownCallbackCount;
						this.trickleDownCallbackCount = num - 1;
					}
					else
					{
						bool flag3 = phase == CallbackPhase.TargetAndBubbleUp;
						if (flag3)
						{
							int num = this.bubbleUpCallbackCount;
							this.bubbleUpCallbackCount = num - 1;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003B834 File Offset: 0x00039A34
		public void Add(EventCallbackFunctorBase item)
		{
			this.m_List.Add(item);
			bool flag = item.phase == CallbackPhase.TrickleDownAndTarget;
			if (flag)
			{
				int num = this.trickleDownCallbackCount;
				this.trickleDownCallbackCount = num + 1;
			}
			else
			{
				bool flag2 = item.phase == CallbackPhase.TargetAndBubbleUp;
				if (flag2)
				{
					int num = this.bubbleUpCallbackCount;
					this.bubbleUpCallbackCount = num + 1;
				}
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003B894 File Offset: 0x00039A94
		public void AddRange(EventCallbackList list)
		{
			this.m_List.AddRange(list.m_List);
			foreach (EventCallbackFunctorBase eventCallbackFunctorBase in list.m_List)
			{
				bool flag = eventCallbackFunctorBase.phase == CallbackPhase.TrickleDownAndTarget;
				if (flag)
				{
					int num = this.trickleDownCallbackCount;
					this.trickleDownCallbackCount = num + 1;
				}
				else
				{
					bool flag2 = eventCallbackFunctorBase.phase == CallbackPhase.TargetAndBubbleUp;
					if (flag2)
					{
						int num = this.bubbleUpCallbackCount;
						this.bubbleUpCallbackCount = num + 1;
					}
				}
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x0003B93C File Offset: 0x00039B3C
		public int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x1700031F RID: 799
		public EventCallbackFunctorBase this[int i]
		{
			get
			{
				return this.m_List[i];
			}
			set
			{
				this.m_List[i] = value;
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003B98B File Offset: 0x00039B8B
		public void Clear()
		{
			this.m_List.Clear();
			this.trickleDownCallbackCount = 0;
			this.bubbleUpCallbackCount = 0;
		}

		// Token: 0x040006BC RID: 1724
		private List<EventCallbackFunctorBase> m_List;
	}
}
