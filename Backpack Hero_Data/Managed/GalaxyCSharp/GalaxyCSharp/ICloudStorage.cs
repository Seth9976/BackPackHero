using System;
using System.Runtime.InteropServices;

namespace Galaxy.Api
{
	// Token: 0x020000BF RID: 191
	public class ICloudStorage : IDisposable
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x000170CD File Offset: 0x000152CD
		internal ICloudStorage(IntPtr cPtr, bool cMemoryOwn)
		{
			this.swigCMemOwn = cMemoryOwn;
			this.swigCPtr = new HandleRef(this, cPtr);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000170E9 File Offset: 0x000152E9
		internal static HandleRef getCPtr(ICloudStorage obj)
		{
			return (obj != null) ? obj.swigCPtr : new HandleRef(null, IntPtr.Zero);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00017108 File Offset: 0x00015308
		~ICloudStorage()
		{
			this.Dispose();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00017138 File Offset: 0x00015338
		public virtual void Dispose()
		{
			lock (this)
			{
				if (this.swigCPtr.Handle != IntPtr.Zero)
				{
					if (this.swigCMemOwn)
					{
						this.swigCMemOwn = false;
						GalaxyInstancePINVOKE.delete_ICloudStorage(this.swigCPtr);
					}
					this.swigCPtr = new HandleRef(null, IntPtr.Zero);
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000171B8 File Offset: 0x000153B8
		public virtual void GetFileList(string container, ICloudStorageGetFileListListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFileList(this.swigCPtr, container, ICloudStorageGetFileListListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000171DC File Offset: 0x000153DC
		public virtual string GetFileNameByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.ICloudStorage_GetFileNameByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00017208 File Offset: 0x00015408
		public virtual uint GetFileSizeByIndex(uint index)
		{
			uint num = GalaxyInstancePINVOKE.ICloudStorage_GetFileSizeByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00017234 File Offset: 0x00015434
		public virtual uint GetFileTimestampByIndex(uint index)
		{
			uint num = GalaxyInstancePINVOKE.ICloudStorage_GetFileTimestampByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return num;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00017260 File Offset: 0x00015460
		public virtual string GetFileHashByIndex(uint index)
		{
			string text = GalaxyInstancePINVOKE.ICloudStorage_GetFileHashByIndex(this.swigCPtr, index);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
			return text;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001728B File Offset: 0x0001548B
		public virtual void GetFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_q_const__char_int__int writeFunc, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFile__SWIG_0(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_q_const__char_int__int.getCPtr(writeFunc), ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000172B9 File Offset: 0x000154B9
		public virtual void GetFile(string container, string name, byte[] dataBuffer, uint bufferLength, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFile__SWIG_1(this.swigCPtr, container, name, dataBuffer, bufferLength, ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000172E2 File Offset: 0x000154E2
		public virtual void GetFileMetadata(string container, string name, ICloudStorageGetFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_GetFileMetadata(this.swigCPtr, container, name, ICloudStorageGetFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00017308 File Offset: 0x00015508
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp, string hash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_0(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp, hash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00017350 File Offset: 0x00015550
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_1(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00017394 File Offset: 0x00015594
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener, SavegameType savegameType)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_2(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000173CB File Offset: 0x000155CB
		public virtual void PutFile(string container, string name, IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ICloudStoragePutFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_3(this.swigCPtr, container, name, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), ICloudStoragePutFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00017400 File Offset: 0x00015600
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp, string hash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_4(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp, hash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0001743A File Offset: 0x0001563A
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType, uint timeStamp)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_5(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType, timeStamp);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00017467 File Offset: 0x00015667
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener, SavegameType savegameType)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_6(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener), (int)savegameType);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00017492 File Offset: 0x00015692
		public virtual void PutFile(string container, string name, byte[] buffer, uint bufferLength, ICloudStoragePutFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_PutFile__SWIG_7(this.swigCPtr, container, name, buffer, bufferLength, ICloudStoragePutFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x000174BB File Offset: 0x000156BB
		public virtual void CalculateHash(IntPtr userParam, SWIGTYPE_p_f_p_void_p_char_int__int readFunc, SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int rewindFunc, ref byte[] hashBuffer, uint hashBufferSize)
		{
			GalaxyInstancePINVOKE.ICloudStorage_CalculateHash__SWIG_0(this.swigCPtr, userParam, SWIGTYPE_p_f_p_void_p_char_int__int.getCPtr(readFunc), SWIGTYPE_p_f_p_void_enum_galaxy__api__ICloudStorage__ReadPhase__int.getCPtr(rewindFunc), hashBuffer, hashBufferSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000174EA File Offset: 0x000156EA
		public virtual void CalculateHash(byte[] buffer, uint bufferLength, ref byte[] hashBuffer, uint hashBufferSize)
		{
			GalaxyInstancePINVOKE.ICloudStorage_CalculateHash__SWIG_1(this.swigCPtr, buffer, bufferLength, hashBuffer, hashBufferSize);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x0001750D File Offset: 0x0001570D
		public virtual void DeleteFile(string container, string name, ICloudStorageDeleteFileListener listener, string expectedHash)
		{
			GalaxyInstancePINVOKE.ICloudStorage_DeleteFile__SWIG_0(this.swigCPtr, container, name, ICloudStorageDeleteFileListener.getCPtr(listener), expectedHash);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00017534 File Offset: 0x00015734
		public virtual void DeleteFile(string container, string name, ICloudStorageDeleteFileListener listener)
		{
			GalaxyInstancePINVOKE.ICloudStorage_DeleteFile__SWIG_1(this.swigCPtr, container, name, ICloudStorageDeleteFileListener.getCPtr(listener));
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00017559 File Offset: 0x00015759
		public virtual void OpenSavegame()
		{
			GalaxyInstancePINVOKE.ICloudStorage_OpenSavegame(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00017576 File Offset: 0x00015776
		public virtual void CloseSavegame()
		{
			GalaxyInstancePINVOKE.ICloudStorage_CloseSavegame(this.swigCPtr);
			if (GalaxyInstancePINVOKE.SWIGPendingException.Pending)
			{
				throw GalaxyInstancePINVOKE.SWIGPendingException.Retrieve();
			}
		}

		// Token: 0x04000107 RID: 263
		private HandleRef swigCPtr;

		// Token: 0x04000108 RID: 264
		protected bool swigCMemOwn;

		// Token: 0x04000109 RID: 265
		public static readonly uint MIN_HASH_BUFFER_SIZE = GalaxyInstancePINVOKE.ICloudStorage_MIN_HASH_BUFFER_SIZE_get();

		// Token: 0x020000C0 RID: 192
		public enum ReadPhase
		{
			// Token: 0x0400010B RID: 267
			CHECKSUM_CALCULATING,
			// Token: 0x0400010C RID: 268
			UPLOADING
		}
	}
}
