using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200036F RID: 879
	internal class CallbackClosure
	{
		// Token: 0x06001D08 RID: 7432 RVA: 0x000694CA File Offset: 0x000676CA
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this._savedCallback = callback;
				this._savedContext = context;
			}
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000694E3 File Offset: 0x000676E3
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this._savedCallback != null && object.Equals(this._savedCallback, callback);
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00069503 File Offset: 0x00067703
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this._savedCallback;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0006950B File Offset: 0x0006770B
		internal ExecutionContext Context
		{
			get
			{
				return this._savedContext;
			}
		}

		// Token: 0x04000EC6 RID: 3782
		private AsyncCallback _savedCallback;

		// Token: 0x04000EC7 RID: 3783
		private ExecutionContext _savedContext;
	}
}
