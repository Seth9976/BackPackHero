using System;

namespace UnityEngine
{
	// Token: 0x02000257 RID: 599
	[Flags]
	public enum DrivenTransformProperties
	{
		// Token: 0x04000890 RID: 2192
		None = 0,
		// Token: 0x04000891 RID: 2193
		All = -1,
		// Token: 0x04000892 RID: 2194
		AnchoredPositionX = 2,
		// Token: 0x04000893 RID: 2195
		AnchoredPositionY = 4,
		// Token: 0x04000894 RID: 2196
		AnchoredPositionZ = 8,
		// Token: 0x04000895 RID: 2197
		Rotation = 16,
		// Token: 0x04000896 RID: 2198
		ScaleX = 32,
		// Token: 0x04000897 RID: 2199
		ScaleY = 64,
		// Token: 0x04000898 RID: 2200
		ScaleZ = 128,
		// Token: 0x04000899 RID: 2201
		AnchorMinX = 256,
		// Token: 0x0400089A RID: 2202
		AnchorMinY = 512,
		// Token: 0x0400089B RID: 2203
		AnchorMaxX = 1024,
		// Token: 0x0400089C RID: 2204
		AnchorMaxY = 2048,
		// Token: 0x0400089D RID: 2205
		SizeDeltaX = 4096,
		// Token: 0x0400089E RID: 2206
		SizeDeltaY = 8192,
		// Token: 0x0400089F RID: 2207
		PivotX = 16384,
		// Token: 0x040008A0 RID: 2208
		PivotY = 32768,
		// Token: 0x040008A1 RID: 2209
		AnchoredPosition = 6,
		// Token: 0x040008A2 RID: 2210
		AnchoredPosition3D = 14,
		// Token: 0x040008A3 RID: 2211
		Scale = 224,
		// Token: 0x040008A4 RID: 2212
		AnchorMin = 768,
		// Token: 0x040008A5 RID: 2213
		AnchorMax = 3072,
		// Token: 0x040008A6 RID: 2214
		Anchors = 3840,
		// Token: 0x040008A7 RID: 2215
		SizeDelta = 12288,
		// Token: 0x040008A8 RID: 2216
		Pivot = 49152
	}
}
