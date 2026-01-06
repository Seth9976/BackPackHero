using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000033 RID: 51
	internal enum SpriteSkinValidationResult
	{
		// Token: 0x040000BB RID: 187
		SpriteNotFound,
		// Token: 0x040000BC RID: 188
		SpriteHasNoSkinningInformation,
		// Token: 0x040000BD RID: 189
		SpriteHasNoWeights,
		// Token: 0x040000BE RID: 190
		RootTransformNotFound,
		// Token: 0x040000BF RID: 191
		InvalidTransformArray,
		// Token: 0x040000C0 RID: 192
		InvalidTransformArrayLength,
		// Token: 0x040000C1 RID: 193
		TransformArrayContainsNull,
		// Token: 0x040000C2 RID: 194
		RootNotFoundInTransformArray,
		// Token: 0x040000C3 RID: 195
		InvalidBoneWeights,
		// Token: 0x040000C4 RID: 196
		Ready
	}
}
