using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E3 RID: 995
	[NativeHeader("Runtime/Graphics/DrawSplashScreenAndWatermarks.h")]
	public class SplashScreen
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060021AF RID: 8623
		public static extern bool isFinished
		{
			[FreeFunction("IsSplashScreenFinished")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060021B0 RID: 8624
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void CancelSplashScreen();

		// Token: 0x060021B1 RID: 8625
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void BeginSplashScreenFade();

		// Token: 0x060021B2 RID: 8626
		[FreeFunction("BeginSplashScreen_Binding")]
		[MethodImpl(4096)]
		public static extern void Begin();

		// Token: 0x060021B3 RID: 8627 RVA: 0x00036E00 File Offset: 0x00035000
		public static void Stop(SplashScreen.StopBehavior stopBehavior)
		{
			bool flag = stopBehavior == SplashScreen.StopBehavior.FadeOut;
			if (flag)
			{
				SplashScreen.BeginSplashScreenFade();
			}
			else
			{
				SplashScreen.CancelSplashScreen();
			}
		}

		// Token: 0x060021B4 RID: 8628
		[FreeFunction("DrawSplashScreen_Binding")]
		[MethodImpl(4096)]
		public static extern void Draw();

		// Token: 0x060021B5 RID: 8629
		[FreeFunction("SetSplashScreenTime")]
		[MethodImpl(4096)]
		internal static extern void SetTime(float time);

		// Token: 0x020003E4 RID: 996
		public enum StopBehavior
		{
			// Token: 0x04000C2D RID: 3117
			StopImmediate,
			// Token: 0x04000C2E RID: 3118
			FadeOut
		}
	}
}
