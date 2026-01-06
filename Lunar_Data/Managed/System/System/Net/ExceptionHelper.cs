using System;

namespace System.Net
{
	// Token: 0x020003F2 RID: 1010
	internal static class ExceptionHelper
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x00077EE4 File Offset: 0x000760E4
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This method is not implemented by this class."));
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x00077EF5 File Offset: 0x000760F5
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This property is not implemented by this class."));
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00077F06 File Offset: 0x00076106
		internal static WebException TimeoutException
		{
			get
			{
				return new WebException("The operation has timed out.");
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00077F12 File Offset: 0x00076112
		internal static NotSupportedException MethodNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This method is not supported by this class."));
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00077F23 File Offset: 0x00076123
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This property is not supported by this class."));
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00077F34 File Offset: 0x00076134
		internal static WebException IsolatedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.KeepAliveFailure), WebExceptionStatus.KeepAliveFailure, WebExceptionInternalStatus.Isolated, null);
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00077F4B File Offset: 0x0007614B
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x00077F5E File Offset: 0x0007615E
		internal static WebException CacheEntryNotFoundException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.CacheEntryNotFound), WebExceptionStatus.CacheEntryNotFound);
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00077F73 File Offset: 0x00076173
		internal static WebException RequestProhibitedByCachePolicyException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestProhibitedByCachePolicy), WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
		}
	}
}
