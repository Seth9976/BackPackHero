using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001C1 RID: 449
	public class ChangeEvent<T> : EventBase<ChangeEvent<T>>, IChangeEvent
	{
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00039EF9 File Offset: 0x000380F9
		// (set) Token: 0x06000E22 RID: 3618 RVA: 0x00039F01 File Offset: 0x00038101
		public T previousValue { get; protected set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x00039F0A File Offset: 0x0003810A
		// (set) Token: 0x06000E24 RID: 3620 RVA: 0x00039F12 File Offset: 0x00038112
		public T newValue { get; protected set; }

		// Token: 0x06000E25 RID: 3621 RVA: 0x00039F1B File Offset: 0x0003811B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00039F2C File Offset: 0x0003812C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.previousValue = default(T);
			this.newValue = default(T);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00039F64 File Offset: 0x00038164
		public static ChangeEvent<T> GetPooled(T previousValue, T newValue)
		{
			ChangeEvent<T> pooled = EventBase<ChangeEvent<T>>.GetPooled();
			pooled.previousValue = previousValue;
			pooled.newValue = newValue;
			return pooled;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00039F8D File Offset: 0x0003818D
		public ChangeEvent()
		{
			this.LocalInit();
		}
	}
}
