using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x020000AA RID: 170
	public class IApps : IDisposable
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x00016A54 File Offset: 0x00014C54
		internal IApps(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00016A70 File Offset: 0x00014C70
		internal static HandleRef getCPtr(IApps obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00016A90 File Offset: 0x00014C90
		~IApps()
		{
			this.Dispose();
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00016AC0 File Offset: 0x00014CC0
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IApps(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00016B40 File Offset: 0x00014D40
		public virtual bool IsDlcInstalled(ulong productID)
		{
			bool flag = GalaxyInstancePINVOKE.IApps_IsDlcInstalled(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00016B6B File Offset: 0x00014D6B
		public virtual void IsDlcOwned(ulong productID, IIsDlcOwnedListener listener)
		{
			GalaxyInstancePINVOKE.IApps_IsDlcOwned(this.swigCPtr, productID, IIsDlcOwnedListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00016B90 File Offset: 0x00014D90
		public virtual string GetCurrentGameLanguage(ulong productID)
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguage__SWIG_0(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00016BBC File Offset: 0x00014DBC
		public virtual string GetCurrentGameLanguage()
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguage__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00016BE8 File Offset: 0x00014DE8
		public virtual void GetCurrentGameLanguageCopy(out string buffer, uint bufferLength, ulong productID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCopy__SWIG_0(this.swigCPtr, array, bufferLength, productID);
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

		// Token: 0x06000871 RID: 2161 RVA: 0x00016C40 File Offset: 0x00014E40
		public virtual void GetCurrentGameLanguageCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCopy__SWIG_1(this.swigCPtr, array, bufferLength);
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

		// Token: 0x06000872 RID: 2162 RVA: 0x00016C98 File Offset: 0x00014E98
		public virtual string GetCurrentGameLanguageCode(ulong productID)
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCode__SWIG_0(this.swigCPtr, productID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00016CC4 File Offset: 0x00014EC4
		public virtual string GetCurrentGameLanguageCode()
		{
			string text = GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCode__SWIG_1(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public virtual void GetCurrentGameLanguageCodeCopy(out string buffer, uint bufferLength, ulong productID)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCodeCopy__SWIG_0(this.swigCPtr, array, bufferLength, productID);
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

		// Token: 0x06000875 RID: 2165 RVA: 0x00016D48 File Offset: 0x00014F48
		public virtual void GetCurrentGameLanguageCodeCopy(out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IApps_GetCurrentGameLanguageCodeCopy__SWIG_1(this.swigCPtr, array, bufferLength);
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

		// Token: 0x040000D0 RID: 208
		private HandleRef swigCPtr;

		// Token: 0x040000D1 RID: 209
		protected bool swigCMemOwn;
	}
}
