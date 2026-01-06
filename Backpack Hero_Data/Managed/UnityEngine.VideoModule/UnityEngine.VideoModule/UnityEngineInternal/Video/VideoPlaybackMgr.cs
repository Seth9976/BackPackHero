using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Modules/Video/Public/Base/VideoMediaPlayback.h")]
	[UsedByNativeCode]
	internal class VideoPlaybackMgr : IDisposable
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00002B03 File Offset: 0x00000D03
		public VideoPlaybackMgr()
		{
			this.m_Ptr = VideoPlaybackMgr.Internal_Create();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002B18 File Offset: 0x00000D18
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VideoPlaybackMgr.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D6 RID: 214
		[MethodImpl(4096)]
		private static extern IntPtr Internal_Create();

		// Token: 0x060000D7 RID: 215
		[MethodImpl(4096)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(4096)]
		public extern VideoPlayback CreateVideoPlayback(string fileName, VideoPlaybackMgr.MessageCallback errorCallback, VideoPlaybackMgr.Callback readyCallback, VideoPlaybackMgr.Callback reachedEndCallback, bool splitAlpha = false);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(4096)]
		public extern void ReleaseVideoPlayback(VideoPlayback playback);

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DA RID: 218
		public extern ulong videoPlaybackCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060000DB RID: 219
		[MethodImpl(4096)]
		public extern void Update();

		// Token: 0x060000DC RID: 220
		[MethodImpl(4096)]
		internal static extern void ProcessOSMainLoopMessagesForTesting();

		// Token: 0x0400003E RID: 62
		internal IntPtr m_Ptr;

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x060000DE RID: 222
		public delegate void Callback();

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x060000E2 RID: 226
		public delegate void MessageCallback(string message);
	}
}
