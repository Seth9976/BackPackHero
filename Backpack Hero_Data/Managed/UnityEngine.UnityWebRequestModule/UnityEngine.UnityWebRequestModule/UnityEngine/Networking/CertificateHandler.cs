using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(0)]
	public class CertificateHandler : IDisposable
	{
		// Token: 0x060000D0 RID: 208
		[MethodImpl(4096)]
		private static extern IntPtr Create(CertificateHandler obj);

		// Token: 0x060000D1 RID: 209
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x060000D2 RID: 210 RVA: 0x000049D0 File Offset: 0x00002BD0
		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000049E8 File Offset: 0x00002BE8
		~CertificateHandler()
		{
			this.Dispose();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004A18 File Offset: 0x00002C18
		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004A2C File Offset: 0x00002C2C
		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004A48 File Offset: 0x00002C48
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x04000059 RID: 89
		[NonSerialized]
		internal IntPtr m_Ptr;
	}
}
