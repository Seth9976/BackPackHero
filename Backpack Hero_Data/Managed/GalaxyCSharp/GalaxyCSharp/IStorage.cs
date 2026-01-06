using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Galaxy.Api
{
	// Token: 0x02000173 RID: 371
	public class IStorage : IDisposable
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x0001B63E File Offset: 0x0001983E
		internal IStorage(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0001B65A File Offset: 0x0001985A
		internal static HandleRef getCPtr(IStorage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0001B678 File Offset: 0x00019878
		~IStorage()
		{
			this.Dispose();
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001B6A8 File Offset: 0x000198A8
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_IStorage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0001B728 File Offset: 0x00019928
		public virtual void FileWrite(string fileName, byte[] data, uint dataSize)
		{
			GalaxyInstancePINVOKE.IStorage_FileWrite(this.swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001B748 File Offset: 0x00019948
		public virtual uint FileRead(string fileName, byte[] data, uint dataSize)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_FileRead(this.swigCPtr, fileName, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001B775 File Offset: 0x00019975
		public virtual void FileDelete(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileDelete(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001B794 File Offset: 0x00019994
		public virtual bool FileExists(string fileName)
		{
			bool flag = GalaxyInstancePINVOKE.IStorage_FileExists(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return flag;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001B7C0 File Offset: 0x000199C0
		public virtual uint GetFileSize(string fileName)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileSize(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0001B7EC File Offset: 0x000199EC
		public virtual uint GetFileTimestamp(string fileName)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileTimestamp(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001B818 File Offset: 0x00019A18
		public virtual uint GetFileCount()
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetFileCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001B844 File Offset: 0x00019A44
		public virtual string GetFileNameByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.IStorage_GetFileNameByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001B870 File Offset: 0x00019A70
		public virtual void GetFileNameCopyByIndex(uint index, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetFileNameCopyByIndex(this.swigCPtr, index, array, bufferLength);
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

		// Token: 0x06000D9D RID: 3485 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		public virtual void FileShare(string fileName, IFileShareListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_0(this.swigCPtr, fileName, IFileShareListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0001B8EC File Offset: 0x00019AEC
		public virtual void FileShare(string fileName)
		{
			GalaxyInstancePINVOKE.IStorage_FileShare__SWIG_1(this.swigCPtr, fileName);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001B90A File Offset: 0x00019B0A
		public virtual void DownloadSharedFile(ulong sharedFileID, ISharedFileDownloadListener listener)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_0(this.swigCPtr, sharedFileID, ISharedFileDownloadListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0001B92E File Offset: 0x00019B2E
		public virtual void DownloadSharedFile(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_DownloadSharedFile__SWIG_1(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0001B94C File Offset: 0x00019B4C
		public virtual string GetSharedFileName(ulong sharedFileID)
		{
			string text = GalaxyInstancePINVOKE.IStorage_GetSharedFileName(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001B978 File Offset: 0x00019B78
		public virtual void GetSharedFileNameCopy(ulong sharedFileID, out string buffer, uint bufferLength)
		{
			byte[] array = new byte[bufferLength];
			try
			{
				GalaxyInstancePINVOKE.IStorage_GetSharedFileNameCopy(this.swigCPtr, sharedFileID, array, bufferLength);
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

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		public virtual uint GetSharedFileSize(ulong sharedFileID)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetSharedFileSize(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public virtual GalaxyID GetSharedFileOwner(ulong sharedFileID)
		{
			IntPtr intPtr = GalaxyInstancePINVOKE.IStorage_GetSharedFileOwner(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			GalaxyID galaxyID = null;
			if (intPtr != IntPtr.Zero)
			{
				galaxyID = new GalaxyID(intPtr, true);
			}
			return galaxyID;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001BA44 File Offset: 0x00019C44
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize, uint offset)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_0(this.swigCPtr, sharedFileID, data, dataSize, offset);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001BA74 File Offset: 0x00019C74
		public virtual uint SharedFileRead(ulong sharedFileID, byte[] data, uint dataSize)
		{
			uint num = GalaxyInstancePINVOKE.IStorage_SharedFileRead__SWIG_1(this.swigCPtr, sharedFileID, data, dataSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001BAA1 File Offset: 0x00019CA1
		public virtual void SharedFileClose(ulong sharedFileID)
		{
			GalaxyInstancePINVOKE.IStorage_SharedFileClose(this.swigCPtr, sharedFileID);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0001BAC0 File Offset: 0x00019CC0
		public virtual uint GetDownloadedSharedFileCount()
		{
			uint num = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileCount(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0001BAEC File Offset: 0x00019CEC
		public virtual ulong GetDownloadedSharedFileByIndex(uint index)
		{
			ulong num = GalaxyInstancePINVOKE.IStorage_GetDownloadedSharedFileByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x040002C8 RID: 712
		private HandleRef swigCPtr;

		// Token: 0x040002C9 RID: 713
		protected bool swigCMemOwn;
	}
}
