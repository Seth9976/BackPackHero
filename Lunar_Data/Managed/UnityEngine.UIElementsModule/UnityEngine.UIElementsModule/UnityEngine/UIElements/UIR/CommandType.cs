using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000342 RID: 834
	internal enum CommandType
	{
		// Token: 0x04000CAE RID: 3246
		Draw,
		// Token: 0x04000CAF RID: 3247
		ImmediateCull,
		// Token: 0x04000CB0 RID: 3248
		Immediate,
		// Token: 0x04000CB1 RID: 3249
		PushView,
		// Token: 0x04000CB2 RID: 3250
		PopView,
		// Token: 0x04000CB3 RID: 3251
		PushScissor,
		// Token: 0x04000CB4 RID: 3252
		PopScissor,
		// Token: 0x04000CB5 RID: 3253
		PushRenderTexture,
		// Token: 0x04000CB6 RID: 3254
		PopRenderTexture,
		// Token: 0x04000CB7 RID: 3255
		BlitToPreviousRT,
		// Token: 0x04000CB8 RID: 3256
		PushDefaultMaterial,
		// Token: 0x04000CB9 RID: 3257
		PopDefaultMaterial
	}
}
