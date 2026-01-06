using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x02000142 RID: 322
	public class InitParams : IDisposable
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x0001A510 File Offset: 0x00018710
		internal InitParams(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001A52C File Offset: 0x0001872C
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule, string _storagePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_0(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule, _storagePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0001A553 File Offset: 0x00018753
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath, bool _loadModule)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_1(_clientID, _clientSecret, _configFilePath, _galaxyModulePath, _loadModule), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0001A578 File Offset: 0x00018778
		public InitParams(string _clientID, string _clientSecret, string _configFilePath, string _galaxyModulePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_2(_clientID, _clientSecret, _configFilePath, _galaxyModulePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0001A59B File Offset: 0x0001879B
		public InitParams(string _clientID, string _clientSecret, string _configFilePath)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_3(_clientID, _clientSecret, _configFilePath), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0001A5BC File Offset: 0x000187BC
		public InitParams(string _clientID, string _clientSecret)
			: this(GalaxyInstancePINVOKE.new_InitParams__SWIG_4(_clientID, _clientSecret), true)
		{
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0001A5DC File Offset: 0x000187DC
		internal static HandleRef getCPtr(InitParams obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0001A5FC File Offset: 0x000187FC
		~InitParams()
		{
			this.Dispose();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0001A62C File Offset: 0x0001882C
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_InitParams(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001A6CC File Offset: 0x000188CC
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0001A6AC File Offset: 0x000188AC
		public string clientID
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_clientID_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientID_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001A714 File Offset: 0x00018914
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0001A6F6 File Offset: 0x000188F6
		public string clientSecret
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_clientSecret_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_clientSecret_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001A75C File Offset: 0x0001895C
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0001A73E File Offset: 0x0001893E
		public string configFilePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_configFilePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_configFilePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001A7A4 File Offset: 0x000189A4
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0001A786 File Offset: 0x00018986
		public string storagePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_storagePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_storagePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001A7EC File Offset: 0x000189EC
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x0001A7CE File Offset: 0x000189CE
		public string galaxyModulePath
		{
			get
			{
				string text = GalaxyInstancePINVOKE.InitParams_galaxyModulePath_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return text;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_galaxyModulePath_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0001A834 File Offset: 0x00018A34
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x0001A816 File Offset: 0x00018A16
		public bool loadModule
		{
			get
			{
				bool flag = GalaxyInstancePINVOKE.InitParams_loadModule_get(this.swigCPtr);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
				return flag;
			}
			set
			{
				GalaxyInstancePINVOKE.InitParams_loadModule_set(this.swigCPtr, value);
				if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
				{
					throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
				}
			}
		}

		// Token: 0x0400024F RID: 591
		private HandleRef swigCPtr;

		// Token: 0x04000250 RID: 592
		protected bool swigCMemOwn;
	}
}
