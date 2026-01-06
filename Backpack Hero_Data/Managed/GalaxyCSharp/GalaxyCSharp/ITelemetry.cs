using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x02000174 RID: 372
	public class ITelemetry : IDisposable
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0001BB17 File Offset: 0x00019D17
		internal ITelemetry(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0001BB33 File Offset: 0x00019D33
		internal static HandleRef getCPtr(ITelemetry obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001BB54 File Offset: 0x00019D54
		~ITelemetry()
		{
			this.Dispose();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001BB84 File Offset: 0x00019D84
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ITelemetry(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001BC04 File Offset: 0x00019E04
		public virtual void AddStringParam(string name, string value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddStringParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001BC23 File Offset: 0x00019E23
		public virtual void AddIntParam(string name, int value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddIntParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001BC42 File Offset: 0x00019E42
		public virtual void AddFloatParam(string name, double value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddFloatParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001BC61 File Offset: 0x00019E61
		public virtual void AddBoolParam(string name, bool value)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddBoolParam(this.swigCPtr, name, value);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001BC80 File Offset: 0x00019E80
		public virtual void AddObjectParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddObjectParam(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001BC9E File Offset: 0x00019E9E
		public virtual void AddArrayParam(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_AddArrayParam(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public virtual void CloseParam()
		{
			GalaxyInstancePINVOKE.ITelemetry_CloseParam(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001BCD9 File Offset: 0x00019ED9
		public virtual void ClearParams()
		{
			GalaxyInstancePINVOKE.ITelemetry_ClearParams(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001BCF6 File Offset: 0x00019EF6
		public virtual void SetSamplingClass(string name)
		{
			GalaxyInstancePINVOKE.ITelemetry_SetSamplingClass(this.swigCPtr, name);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001BD14 File Offset: 0x00019F14
		public virtual uint SendTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_0(this.swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001BD48 File Offset: 0x00019F48
		public virtual uint SendTelemetryEvent(string eventType)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendTelemetryEvent__SWIG_1(this.swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001BD74 File Offset: 0x00019F74
		public virtual uint SendAnonymousTelemetryEvent(string eventType, ITelemetryEventSendListener listener)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_0(this.swigCPtr, eventType, ITelemetryEventSendListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		public virtual uint SendAnonymousTelemetryEvent(string eventType)
		{
			uint num = GalaxyInstancePINVOKE.ITelemetry_SendAnonymousTelemetryEvent__SWIG_1(this.swigCPtr, eventType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		public virtual string GetVisitID()
		{
			string text = GalaxyInstancePINVOKE.ITelemetry_GetVisitID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001BE00 File Offset: 0x0001A000
		public virtual void GetVisitIDCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.ITelemetry_GetVisitIDCopy(this.swigCPtr, array, bufferLength);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
			finally
			{
				buffer = Encoding.UTF8.GetString(array);
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001BE58 File Offset: 0x0001A058
		public virtual void ResetVisitID()
		{
			GalaxyInstancePINVOKE.ITelemetry_ResetVisitID(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x040002CA RID: 714
		private HandleRef swigCPtr;

		// Token: 0x040002CB RID: 715
		protected bool swigCMemOwn;
	}
}
