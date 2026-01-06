using System;
using System.Diagnostics;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200043D RID: 1085
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007E5FC File Offset: 0x0007C7FC
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.t_ThreadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.t_ThreadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x0600224C RID: 8780 RVA: 0x0007E61F File Offset: 0x0007C81F
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = DBNull.Value;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0007E647 File Offset: 0x0007C847
		internal LazyAsyncResult(object myObject, object myState, AsyncCallback myCallBack, object result)
		{
			this.m_AsyncObject = myObject;
			this.m_AsyncState = myState;
			this.m_AsyncCallback = myCallBack;
			this.m_Result = result;
			this.m_IntCompleted = 1;
			if (this.m_AsyncCallback != null)
			{
				this.m_AsyncCallback(this);
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0007E687 File Offset: 0x0007C887
		internal object AsyncObject
		{
			get
			{
				return this.m_AsyncObject;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0007E68F File Offset: 0x0007C88F
		public object AsyncState
		{
			get
			{
				return this.m_AsyncState;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007E697 File Offset: 0x0007C897
		// (set) Token: 0x06002251 RID: 8785 RVA: 0x0007E69F File Offset: 0x0007C89F
		protected AsyncCallback AsyncCallback
		{
			get
			{
				return this.m_AsyncCallback;
			}
			set
			{
				this.m_AsyncCallback = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0007E6A8 File Offset: 0x0007C8A8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.m_UserEvent = true;
				if (this.m_IntCompleted == 0)
				{
					Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x0007E6F4 File Offset: 0x0007C8F4
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.m_Event, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.m_Event;
					flag = false;
				}
			}
			catch
			{
				this.m_Event = null;
				if (waitHandle != null)
				{
					waitHandle.Close();
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		protected void DebugProtectState(bool protect)
		{
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0007E76C File Offset: 0x0007C96C
		public bool CompletedSynchronously
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return num > 0;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0007E79C File Offset: 0x0007C99C
		public bool IsCompleted
		{
			get
			{
				int num = this.m_IntCompleted;
				if (num == 0)
				{
					num = Interlocked.CompareExchange(ref this.m_IntCompleted, int.MinValue, 0);
				}
				return (num & int.MaxValue) != 0;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0007E7CF File Offset: 0x0007C9CF
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.m_IntCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x0007E7E0 File Offset: 0x0007C9E0
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x0007E7F7 File Offset: 0x0007C9F7
		internal object Result
		{
			get
			{
				if (this.m_Result != DBNull.Value)
				{
					return this.m_Result;
				}
				return null;
			}
			set
			{
				this.m_Result = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x0007E800 File Offset: 0x0007CA00
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x0007E808 File Offset: 0x0007CA08
		internal bool EndCalled
		{
			get
			{
				return this.m_EndCalled;
			}
			set
			{
				this.m_EndCalled = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x0007E811 File Offset: 0x0007CA11
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x0007E819 File Offset: 0x0007CA19
		internal int ErrorCode
		{
			get
			{
				return this.m_ErrorCode;
			}
			set
			{
				this.m_ErrorCode = value;
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007E824 File Offset: 0x0007CA24
		protected void ProtectedInvokeCallback(object result, IntPtr userToken)
		{
			if (result == DBNull.Value)
			{
				throw new ArgumentNullException("result");
			}
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				if (this.m_Result == DBNull.Value)
				{
					this.m_Result = result;
				}
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Complete(userToken);
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0007E8AC File Offset: 0x0007CAAC
		internal void InvokeCallback(object result)
		{
			this.ProtectedInvokeCallback(result, IntPtr.Zero);
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0007E8BA File Offset: 0x0007CABA
		internal void InvokeCallback()
		{
			this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0007E8C8 File Offset: 0x0007CAC8
		protected virtual void Complete(IntPtr userToken)
		{
			bool flag = false;
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.m_NestedIOCount++;
				if (this.m_AsyncCallback != null)
				{
					if (currentThreadContext.m_NestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
						flag = true;
					}
					else
					{
						this.m_AsyncCallback(this);
					}
				}
			}
			finally
			{
				currentThreadContext.m_NestedIOCount--;
				if (!flag)
				{
					this.Cleanup();
				}
			}
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0007E94C File Offset: 0x0007CB4C
		private void WorkerThreadComplete(object state)
		{
			try
			{
				this.m_AsyncCallback(this);
			}
			finally
			{
				this.Cleanup();
			}
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Cleanup()
		{
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0007E980 File Offset: 0x0007CB80
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007E98C File Offset: 0x0007CB8C
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.m_Event;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_0073;
			}
			try
			{
				manualResetEvent.WaitOne(-1, false);
				goto IL_0073;
			}
			catch (ObjectDisposedException)
			{
				goto IL_0073;
			}
			finally
			{
				if (flag && !this.m_UserEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.m_Event;
					this.m_Event = null;
					if (!this.m_UserEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_006D:
			Thread.SpinWait(1);
			IL_0073:
			if (this.m_Result != DBNull.Value)
			{
				return this.m_Result;
			}
			goto IL_006D;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0007EA3C File Offset: 0x0007CC3C
		internal void InternalCleanup()
		{
			if ((this.m_IntCompleted & 2147483647) == 0 && (Interlocked.Increment(ref this.m_IntCompleted) & 2147483647) == 1)
			{
				this.m_Result = null;
				this.Cleanup();
			}
		}

		// Token: 0x040013FD RID: 5117
		private const int c_HighBit = -2147483648;

		// Token: 0x040013FE RID: 5118
		private const int c_ForceAsyncCount = 50;

		// Token: 0x040013FF RID: 5119
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext t_ThreadContext;

		// Token: 0x04001400 RID: 5120
		private object m_AsyncObject;

		// Token: 0x04001401 RID: 5121
		private object m_AsyncState;

		// Token: 0x04001402 RID: 5122
		private AsyncCallback m_AsyncCallback;

		// Token: 0x04001403 RID: 5123
		private object m_Result;

		// Token: 0x04001404 RID: 5124
		private int m_ErrorCode;

		// Token: 0x04001405 RID: 5125
		private int m_IntCompleted;

		// Token: 0x04001406 RID: 5126
		private bool m_EndCalled;

		// Token: 0x04001407 RID: 5127
		private bool m_UserEvent;

		// Token: 0x04001408 RID: 5128
		private object m_Event;

		// Token: 0x0200043E RID: 1086
		private class ThreadContext
		{
			// Token: 0x04001409 RID: 5129
			internal int m_NestedIOCount;
		}
	}
}
