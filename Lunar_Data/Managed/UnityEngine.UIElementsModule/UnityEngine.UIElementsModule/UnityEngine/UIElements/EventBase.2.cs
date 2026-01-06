using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001CD RID: 461
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x0003B22D File Offset: 0x0003942D
		protected EventBase()
		{
			this.m_RefCount = 0;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003B240 File Offset: 0x00039440
		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003B258 File Offset: 0x00039458
		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003B290 File Offset: 0x00039490
		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003B2D4 File Offset: 0x000394D4
		internal static T GetPooled(EventBase e)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				pooled.SetTriggerEventId(e.eventId);
			}
			return pooled;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003B30C File Offset: 0x0003950C
		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003B350 File Offset: 0x00039550
		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003B364 File Offset: 0x00039564
		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0003B398 File Offset: 0x00039598
		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		// Token: 0x040006A8 RID: 1704
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		// Token: 0x040006A9 RID: 1705
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(100);

		// Token: 0x040006AA RID: 1706
		private int m_RefCount;
	}
}
