using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000018 RID: 24
	internal static class TimelineClipCapsExtensions
	{
		// Token: 0x0600019D RID: 413 RVA: 0x00006A4F File Offset: 0x00004C4F
		public static bool SupportsLooping(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Looping) > ClipCaps.None;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006A61 File Offset: 0x00004C61
		public static bool SupportsExtrapolation(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Extrapolation) > ClipCaps.None;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006A73 File Offset: 0x00004C73
		public static bool SupportsClipIn(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.ClipIn) > ClipCaps.None;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006A85 File Offset: 0x00004C85
		public static bool SupportsSpeedMultiplier(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.SpeedMultiplier) > ClipCaps.None;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006A97 File Offset: 0x00004C97
		public static bool SupportsBlending(this TimelineClip clip)
		{
			return clip != null && (clip.clipCaps & ClipCaps.Blending) > ClipCaps.None;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006AAA File Offset: 0x00004CAA
		public static bool HasAll(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) == flags;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006AB2 File Offset: 0x00004CB2
		public static bool HasAny(this ClipCaps caps, ClipCaps flags)
		{
			return (caps & flags) > ClipCaps.None;
		}
	}
}
