using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Audio;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Video
{
	// Token: 0x02000014 RID: 20
	[UsedByNativeCode]
	[NativeHeader("Modules/Video/Public/Base/MediaComponent.h")]
	internal class VideoPlayback
	{
		// Token: 0x060000AE RID: 174
		[MethodImpl(4096)]
		public extern void StartPlayback();

		// Token: 0x060000AF RID: 175
		[MethodImpl(4096)]
		public extern void PausePlayback();

		// Token: 0x060000B0 RID: 176
		[MethodImpl(4096)]
		public extern void StopPlayback();

		// Token: 0x060000B1 RID: 177
		[MethodImpl(4096)]
		public extern VideoError GetStatus();

		// Token: 0x060000B2 RID: 178
		[MethodImpl(4096)]
		public extern bool IsReady();

		// Token: 0x060000B3 RID: 179
		[MethodImpl(4096)]
		public extern bool IsPlaying();

		// Token: 0x060000B4 RID: 180
		[MethodImpl(4096)]
		public extern void Step();

		// Token: 0x060000B5 RID: 181
		[MethodImpl(4096)]
		public extern bool CanStep();

		// Token: 0x060000B6 RID: 182
		[MethodImpl(4096)]
		public extern uint GetWidth();

		// Token: 0x060000B7 RID: 183
		[MethodImpl(4096)]
		public extern uint GetHeight();

		// Token: 0x060000B8 RID: 184
		[MethodImpl(4096)]
		public extern float GetFrameRate();

		// Token: 0x060000B9 RID: 185
		[MethodImpl(4096)]
		public extern float GetDuration();

		// Token: 0x060000BA RID: 186
		[MethodImpl(4096)]
		public extern ulong GetFrameCount();

		// Token: 0x060000BB RID: 187
		[MethodImpl(4096)]
		public extern uint GetPixelAspectRatioNumerator();

		// Token: 0x060000BC RID: 188
		[MethodImpl(4096)]
		public extern uint GetPixelAspectRatioDenominator();

		// Token: 0x060000BD RID: 189
		[MethodImpl(4096)]
		public extern VideoPixelFormat GetPixelFormat();

		// Token: 0x060000BE RID: 190
		[MethodImpl(4096)]
		public extern bool CanNotSkipOnDrop();

		// Token: 0x060000BF RID: 191
		[MethodImpl(4096)]
		public extern void SetSkipOnDrop(bool skipOnDrop);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(4096)]
		public extern bool GetTexture(Texture texture, out long outputFrameNum);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(4096)]
		public extern void SeekToFrame(long frameIndex, VideoPlayback.Callback seekCompletedCallback);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(4096)]
		public extern void SeekToTime(double secs, VideoPlayback.Callback seekCompletedCallback);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(4096)]
		public extern float GetPlaybackSpeed();

		// Token: 0x060000C4 RID: 196
		[MethodImpl(4096)]
		public extern void SetPlaybackSpeed(float value);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(4096)]
		public extern bool GetLoop();

		// Token: 0x060000C6 RID: 198
		[MethodImpl(4096)]
		public extern void SetLoop(bool value);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(4096)]
		public extern void SetAdjustToLinearSpace(bool enable);

		// Token: 0x060000C8 RID: 200
		[NativeHeader("Modules/Audio/Public/AudioSource.h")]
		[MethodImpl(4096)]
		public extern ushort GetAudioTrackCount();

		// Token: 0x060000C9 RID: 201
		[MethodImpl(4096)]
		public extern ushort GetAudioChannelCount(ushort trackIdx);

		// Token: 0x060000CA RID: 202
		[MethodImpl(4096)]
		public extern uint GetAudioSampleRate(ushort trackIdx);

		// Token: 0x060000CB RID: 203
		[MethodImpl(4096)]
		public extern void SetAudioTarget(ushort trackIdx, bool enabled, bool softwareOutput, AudioSource audioSource);

		// Token: 0x060000CC RID: 204
		[MethodImpl(4096)]
		private extern uint GetAudioSampleProviderId(ushort trackIndex);

		// Token: 0x060000CD RID: 205 RVA: 0x00002A2C File Offset: 0x00000C2C
		public AudioSampleProvider GetAudioSampleProvider(ushort trackIndex)
		{
			bool flag = trackIndex >= this.GetAudioTrackCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("trackIndex", trackIndex, "VideoPlayback has " + this.GetAudioTrackCount().ToString() + " tracks.");
			}
			AudioSampleProvider audioSampleProvider = AudioSampleProvider.Lookup(this.GetAudioSampleProviderId(trackIndex), null, trackIndex);
			bool flag2 = audioSampleProvider == null;
			if (flag2)
			{
				throw new InvalidOperationException("VideoPlayback.GetAudioSampleProvider got null provider.");
			}
			bool flag3 = audioSampleProvider.owner != null;
			if (flag3)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got unexpected non-null provider owner.");
			}
			bool flag4 = audioSampleProvider.trackIndex != trackIndex;
			if (flag4)
			{
				throw new InvalidOperationException("Internal error: VideoPlayback.GetAudioSampleProvider got provider for track " + audioSampleProvider.trackIndex.ToString() + " instead of " + trackIndex.ToString());
			}
			return audioSampleProvider;
		}

		// Token: 0x060000CE RID: 206
		[MethodImpl(4096)]
		internal static extern bool PlatformSupportsH265();

		// Token: 0x0400003D RID: 61
		internal IntPtr m_Ptr;

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x060000D1 RID: 209
		public delegate void Callback();
	}
}
