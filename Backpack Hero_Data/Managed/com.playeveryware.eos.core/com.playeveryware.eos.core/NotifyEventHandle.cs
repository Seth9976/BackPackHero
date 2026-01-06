using System;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200001C RID: 28
	public class NotifyEventHandle : GenericSafeHandle<ulong>
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000026A1 File Offset: 0x000008A1
		public NotifyEventHandle(ulong aLong, NotifyEventHandle.RemoveDelegate aRemoveDelegate)
			: base(aLong)
		{
			this.removeDelegate = aRemoveDelegate;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026B1 File Offset: 0x000008B1
		protected override void ReleaseHandle()
		{
			if (!this.IsValid())
			{
				return;
			}
			this.removeDelegate(this.handleObject);
			this.handleObject = 0UL;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026D5 File Offset: 0x000008D5
		public override bool IsValid()
		{
			return this.handleObject > 0UL;
		}

		// Token: 0x0400002F RID: 47
		private NotifyEventHandle.RemoveDelegate removeDelegate;

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000067 RID: 103
		public delegate void RemoveDelegate(ulong aHandle);
	}
}
