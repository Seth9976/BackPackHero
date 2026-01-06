using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000031 RID: 49
	public static class EditorTimeBinding
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004C10 File Offset: 0x00002E10
		public static int frame
		{
			get
			{
				if (EditorTimeBinding.frameBinding == null || !UnityThread.allowsAPI)
				{
					return 0;
				}
				return EditorTimeBinding.frameBinding();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004C2C File Offset: 0x00002E2C
		public static float time
		{
			get
			{
				if (EditorTimeBinding.timeBinding == null || !UnityThread.allowsAPI)
				{
					return 0f;
				}
				return EditorTimeBinding.timeBinding();
			}
		}

		// Token: 0x0400002C RID: 44
		public static Func<int> frameBinding = () => Time.frameCount;

		// Token: 0x0400002D RID: 45
		public static Func<float> timeBinding = () => Time.time;
	}
}
