using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Steamworks
{
	// Token: 0x0200017D RID: 381
	public class InteropHelp
	{
		// Token: 0x060008B1 RID: 2225 RVA: 0x0000CC8F File Offset: 0x0000AE8F
		public static void TestIfPlatformSupported()
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0000CC91 File Offset: 0x0000AE91
		public static void TestIfAvailableClient()
		{
			InteropHelp.TestIfPlatformSupported();
			if (CSteamAPIContext.GetSteamClient() == IntPtr.Zero && !CSteamAPIContext.Init())
			{
				throw new InvalidOperationException("Steamworks is not initialized.");
			}
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0000CCBB File Offset: 0x0000AEBB
		public static void TestIfAvailableGameServer()
		{
			InteropHelp.TestIfPlatformSupported();
			if (CSteamGameServerAPIContext.GetSteamClient() == IntPtr.Zero && !CSteamGameServerAPIContext.Init())
			{
				throw new InvalidOperationException("Steamworks GameServer is not initialized.");
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public static string PtrToStringUTF8(IntPtr nativeUtf8)
		{
			if (nativeUtf8 == IntPtr.Zero)
			{
				return null;
			}
			int num = 0;
			while (Marshal.ReadByte(nativeUtf8, num) != 0)
			{
				num++;
			}
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(nativeUtf8, array, 0, array.Length);
			return Encoding.UTF8.GetString(array);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public static string ByteArrayToStringUTF8(byte[] buffer)
		{
			int num = 0;
			while (num < buffer.Length && buffer[num] != 0)
			{
				num++;
			}
			return Encoding.UTF8.GetString(buffer, 0, num);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0000CD6C File Offset: 0x0000AF6C
		public static void StringToByteArrayUTF8(string str, byte[] outArrayBuffer, int outArrayBufferSize)
		{
			outArrayBuffer = new byte[outArrayBufferSize];
			int bytes = Encoding.UTF8.GetBytes(str, 0, str.Length, outArrayBuffer, 0);
			outArrayBuffer[bytes] = 0;
		}

		// Token: 0x020001C8 RID: 456
		public class UTF8StringHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06000B65 RID: 2917 RVA: 0x000105D0 File Offset: 0x0000E7D0
			public UTF8StringHandle(string str)
				: base(true)
			{
				if (str == null)
				{
					base.SetHandle(IntPtr.Zero);
					return;
				}
				byte[] array = new byte[Encoding.UTF8.GetByteCount(str) + 1];
				Encoding.UTF8.GetBytes(str, 0, str.Length, array, 0);
				IntPtr intPtr = Marshal.AllocHGlobal(array.Length);
				Marshal.Copy(array, 0, intPtr, array.Length);
				base.SetHandle(intPtr);
			}

			// Token: 0x06000B66 RID: 2918 RVA: 0x00010636 File Offset: 0x0000E836
			protected override bool ReleaseHandle()
			{
				if (!this.IsInvalid)
				{
					Marshal.FreeHGlobal(this.handle);
				}
				return true;
			}
		}

		// Token: 0x020001C9 RID: 457
		public class SteamParamStringArray
		{
			// Token: 0x06000B67 RID: 2919 RVA: 0x0001064C File Offset: 0x0000E84C
			public SteamParamStringArray(IList<string> strings)
			{
				if (strings == null)
				{
					this.m_pSteamParamStringArray = IntPtr.Zero;
					return;
				}
				this.m_Strings = new IntPtr[strings.Count];
				for (int i = 0; i < strings.Count; i++)
				{
					byte[] array = new byte[Encoding.UTF8.GetByteCount(strings[i]) + 1];
					Encoding.UTF8.GetBytes(strings[i], 0, strings[i].Length, array, 0);
					this.m_Strings[i] = Marshal.AllocHGlobal(array.Length);
					Marshal.Copy(array, 0, this.m_Strings[i], array.Length);
				}
				this.m_ptrStrings = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)) * this.m_Strings.Length);
				SteamParamStringArray_t steamParamStringArray_t = new SteamParamStringArray_t
				{
					m_ppStrings = this.m_ptrStrings,
					m_nNumStrings = this.m_Strings.Length
				};
				Marshal.Copy(this.m_Strings, 0, steamParamStringArray_t.m_ppStrings, this.m_Strings.Length);
				this.m_pSteamParamStringArray = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SteamParamStringArray_t)));
				Marshal.StructureToPtr<SteamParamStringArray_t>(steamParamStringArray_t, this.m_pSteamParamStringArray, false);
			}

			// Token: 0x06000B68 RID: 2920 RVA: 0x00010778 File Offset: 0x0000E978
			protected override void Finalize()
			{
				try
				{
					if (this.m_Strings != null)
					{
						IntPtr[] strings = this.m_Strings;
						for (int i = 0; i < strings.Length; i++)
						{
							Marshal.FreeHGlobal(strings[i]);
						}
					}
					if (this.m_ptrStrings != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_ptrStrings);
					}
					if (this.m_pSteamParamStringArray != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(this.m_pSteamParamStringArray);
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x06000B69 RID: 2921 RVA: 0x00010800 File Offset: 0x0000EA00
			public static implicit operator IntPtr(InteropHelp.SteamParamStringArray that)
			{
				return that.m_pSteamParamStringArray;
			}

			// Token: 0x04000AC0 RID: 2752
			private IntPtr[] m_Strings;

			// Token: 0x04000AC1 RID: 2753
			private IntPtr m_ptrStrings;

			// Token: 0x04000AC2 RID: 2754
			private IntPtr m_pSteamParamStringArray;
		}
	}
}
