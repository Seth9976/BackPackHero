using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerVFS.h")]
	[StructLayout(0)]
	public sealed class DownloadHandlerFile : DownloadHandler
	{
		// Token: 0x060000FE RID: 254
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern IntPtr Create(DownloadHandlerFile obj, string path, bool append);

		// Token: 0x060000FF RID: 255 RVA: 0x00004F58 File Offset: 0x00003158
		private void InternalCreateVFS(string path, bool append)
		{
			string directoryName = Path.GetDirectoryName(path);
			bool flag = !Directory.Exists(directoryName);
			if (flag)
			{
				Directory.CreateDirectory(directoryName);
			}
			this.m_Ptr = DownloadHandlerFile.Create(this, path, append);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004F8F File Offset: 0x0000318F
		public DownloadHandlerFile(string path)
		{
			this.InternalCreateVFS(path, false);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004FA2 File Offset: 0x000031A2
		public DownloadHandlerFile(string path, bool append)
		{
			this.InternalCreateVFS(path, append);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004FB5 File Offset: 0x000031B5
		protected override NativeArray<byte> GetNativeData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004FB5 File Offset: 0x000031B5
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004FC2 File Offset: 0x000031C2
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported");
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000105 RID: 261
		// (set) Token: 0x06000106 RID: 262
		public extern bool removeFileOnAbort
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
