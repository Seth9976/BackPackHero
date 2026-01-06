using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200017A RID: 378
	public sealed class CallResult<T> : CallResult, IDisposable
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600089F RID: 2207 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		// (remove) Token: 0x060008A0 RID: 2208 RVA: 0x0000CA64 File Offset: 0x0000AC64
		private event CallResult<T>.APIDispatchDelegate m_Func;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0000CA99 File Offset: 0x0000AC99
		public SteamAPICall_t Handle
		{
			get
			{
				return this.m_hAPICall;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000CAA1 File Offset: 0x0000ACA1
		public static CallResult<T> Create(CallResult<T>.APIDispatchDelegate func = null)
		{
			return new CallResult<T>(func);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000CAA9 File Offset: 0x0000ACA9
		public CallResult(CallResult<T>.APIDispatchDelegate func = null)
		{
			this.m_Func = func;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		~CallResult()
		{
			this.Dispose();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public void Dispose()
		{
			if (this.m_bDisposed)
			{
				return;
			}
			GC.SuppressFinalize(this);
			this.Cancel();
			this.m_bDisposed = true;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public void Set(SteamAPICall_t hAPICall, CallResult<T>.APIDispatchDelegate func = null)
		{
			if (func != null)
			{
				this.m_Func = func;
			}
			if (this.m_Func == null)
			{
				throw new Exception("CallResult function was null, you must either set it in the CallResult Constructor or via Set()");
			}
			if (this.m_hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
			this.m_hAPICall = hAPICall;
			if (hAPICall != SteamAPICall_t.Invalid)
			{
				CallbackDispatcher.Register(hAPICall, this);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0000CB73 File Offset: 0x0000AD73
		public bool IsActive()
		{
			return this.m_hAPICall != SteamAPICall_t.Invalid;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000CB85 File Offset: 0x0000AD85
		public void Cancel()
		{
			if (this.IsActive())
			{
				CallbackDispatcher.Unregister(this.m_hAPICall, this);
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0000CB9B File Offset: 0x0000AD9B
		internal override Type GetCallbackType()
		{
			return typeof(T);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0000CBA8 File Offset: 0x0000ADA8
		internal override void OnRunCallResult(IntPtr pvParam, bool bFailed, ulong hSteamAPICall_)
		{
			if ((SteamAPICall_t)hSteamAPICall_ == this.m_hAPICall)
			{
				try
				{
					this.m_Func((T)((object)Marshal.PtrToStructure(pvParam, typeof(T))), bFailed);
				}
				catch (Exception ex)
				{
					CallbackDispatcher.ExceptionHandler(ex);
				}
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0000CC04 File Offset: 0x0000AE04
		internal override void SetUnregistered()
		{
			this.m_hAPICall = SteamAPICall_t.Invalid;
		}

		// Token: 0x040009EE RID: 2542
		private SteamAPICall_t m_hAPICall = SteamAPICall_t.Invalid;

		// Token: 0x040009EF RID: 2543
		private bool m_bDisposed;

		// Token: 0x020001C7 RID: 455
		// (Invoke) Token: 0x06000B62 RID: 2914
		public delegate void APIDispatchDelegate(T param, bool bIOFailure);
	}
}
