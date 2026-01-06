using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000B RID: 11
	internal static class MatchTargetFieldConstants
	{
		// Token: 0x0600003D RID: 61 RVA: 0x0000286E File Offset: 0x00000A6E
		public static bool HasAny(this MatchTargetFields me, MatchTargetFields fields)
		{
			return (me & fields) != MatchTargetFieldConstants.None;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000287D File Offset: 0x00000A7D
		public static MatchTargetFields Toggle(this MatchTargetFields me, MatchTargetFields flag)
		{
			return me ^ flag;
		}

		// Token: 0x0400002B RID: 43
		public static MatchTargetFields All = MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ | MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ;

		// Token: 0x0400002C RID: 44
		public static MatchTargetFields None = (MatchTargetFields)0;

		// Token: 0x0400002D RID: 45
		public static MatchTargetFields Position = MatchTargetFields.PositionX | MatchTargetFields.PositionY | MatchTargetFields.PositionZ;

		// Token: 0x0400002E RID: 46
		public static MatchTargetFields Rotation = MatchTargetFields.RotationX | MatchTargetFields.RotationY | MatchTargetFields.RotationZ;
	}
}
