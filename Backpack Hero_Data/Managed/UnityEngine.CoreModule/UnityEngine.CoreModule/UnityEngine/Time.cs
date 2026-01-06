using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200024E RID: 590
	[NativeHeader("Runtime/Input/TimeManager.h")]
	[StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	public class Time
	{
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600198C RID: 6540
		[NativeProperty("CurTime")]
		public static extern float time
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600198D RID: 6541
		[NativeProperty("CurTime")]
		public static extern double timeAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600198E RID: 6542
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern float timeSinceLevelLoad
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600198F RID: 6543
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern double timeSinceLevelLoadAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001990 RID: 6544
		public static extern float deltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001991 RID: 6545
		public static extern float fixedTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001992 RID: 6546
		[NativeProperty("FixedTime")]
		public static extern double fixedTimeAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001993 RID: 6547
		public static extern float unscaledTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001994 RID: 6548
		[NativeProperty("UnscaledTime")]
		public static extern double unscaledTimeAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001995 RID: 6549
		public static extern float fixedUnscaledTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001996 RID: 6550
		[NativeProperty("FixedUnscaledTime")]
		public static extern double fixedUnscaledTimeAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001997 RID: 6551
		public static extern float unscaledDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001998 RID: 6552
		public static extern float fixedUnscaledDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001999 RID: 6553
		// (set) Token: 0x0600199A RID: 6554
		public static extern float fixedDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600199B RID: 6555
		// (set) Token: 0x0600199C RID: 6556
		public static extern float maximumDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600199D RID: 6557
		public static extern float smoothDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600199E RID: 6558
		// (set) Token: 0x0600199F RID: 6559
		public static extern float maximumParticleDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060019A0 RID: 6560
		// (set) Token: 0x060019A1 RID: 6561
		public static extern float timeScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060019A2 RID: 6562
		public static extern int frameCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060019A3 RID: 6563
		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060019A4 RID: 6564
		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060019A5 RID: 6565
		[NativeProperty("Realtime")]
		public static extern double realtimeSinceStartupAsDouble
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060019A6 RID: 6566
		// (set) Token: 0x060019A7 RID: 6567
		public static extern float captureDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x000296A4 File Offset: 0x000278A4
		// (set) Token: 0x060019A9 RID: 6569 RVA: 0x000296D6 File Offset: 0x000278D6
		public static int captureFramerate
		{
			get
			{
				return (Time.captureDeltaTime == 0f) ? 0 : ((int)Mathf.Round(1f / Time.captureDeltaTime));
			}
			set
			{
				Time.captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060019AA RID: 6570
		public static extern bool inFixedTimeStep
		{
			[NativeName("IsUsingFixedTimeStep")]
			[MethodImpl(4096)]
			get;
		}
	}
}
