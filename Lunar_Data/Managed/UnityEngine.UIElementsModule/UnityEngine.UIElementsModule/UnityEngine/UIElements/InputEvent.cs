using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E5 RID: 485
	public class InputEvent : EventBase<InputEvent>
	{
		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0003C7C1 File Offset: 0x0003A9C1
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0003C7C9 File Offset: 0x0003A9C9
		public string previousData { get; protected set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0003C7D2 File Offset: 0x0003A9D2
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0003C7DA File Offset: 0x0003A9DA
		public string newData { get; protected set; }

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003C7E3 File Offset: 0x0003A9E3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003C7F4 File Offset: 0x0003A9F4
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			this.previousData = null;
			this.newData = null;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0003C810 File Offset: 0x0003AA10
		public static InputEvent GetPooled(string previousData, string newData)
		{
			InputEvent pooled = EventBase<InputEvent>.GetPooled();
			pooled.previousData = previousData;
			pooled.newData = newData;
			return pooled;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0003C839 File Offset: 0x0003AA39
		public InputEvent()
		{
			this.LocalInit();
		}
	}
}
