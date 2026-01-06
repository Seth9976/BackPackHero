using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000029 RID: 41
	internal enum SpriteSkinValidationResult
	{
		// Token: 0x04000062 RID: 98
		SpriteNotFound,
		// Token: 0x04000063 RID: 99
		SpriteHasNoSkinningInformation,
		// Token: 0x04000064 RID: 100
		SpriteHasNoWeights,
		// Token: 0x04000065 RID: 101
		RootTransformNotFound,
		// Token: 0x04000066 RID: 102
		InvalidTransformArray,
		// Token: 0x04000067 RID: 103
		InvalidTransformArrayLength,
		// Token: 0x04000068 RID: 104
		TransformArrayContainsNull,
		// Token: 0x04000069 RID: 105
		RootNotFoundInTransformArray,
		// Token: 0x0400006A RID: 106
		InvalidBoneWeights,
		// Token: 0x0400006B RID: 107
		Ready
	}
}
