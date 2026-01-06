using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000F RID: 15
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	[StructLayout(0)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		// Token: 0x060000F2 RID: 242
		[MethodImpl(4096)]
		private static extern IntPtr Create(DownloadHandlerBuffer obj);

		// Token: 0x060000F3 RID: 243 RVA: 0x00004E75 File Offset: 0x00003075
		private void InternalCreateBuffer()
		{
			this.m_Ptr = DownloadHandlerBuffer.Create(this);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004E84 File Offset: 0x00003084
		public DownloadHandlerBuffer()
		{
			this.InternalCreateBuffer();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004E98 File Offset: 0x00003098
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004EB6 File Offset: 0x000030B6
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004ECC File Offset: 0x000030CC
		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}

		// Token: 0x0400005B RID: 91
		private NativeArray<byte> m_NativeData;
	}
}
