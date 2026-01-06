using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Animation/Motion.h")]
	public class Motion : Object
	{
		// Token: 0x0600027E RID: 638 RVA: 0x000039FB File Offset: 0x00001BFB
		protected Motion()
		{
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600027F RID: 639
		public extern float averageDuration
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000280 RID: 640
		public extern float averageAngularSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00004344 File Offset: 0x00002544
		public Vector3 averageSpeed
		{
			get
			{
				Vector3 vector;
				this.get_averageSpeed_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000282 RID: 642
		public extern float apparentSpeed
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000283 RID: 643
		public extern bool isLooping
		{
			[NativeMethod("IsLooping")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000284 RID: 644
		public extern bool legacy
		{
			[NativeMethod("IsLegacy")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000285 RID: 645
		public extern bool isHumanMotion
		{
			[NativeMethod("IsHumanMotion")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000435C File Offset: 0x0000255C
		[EditorBrowsable(1)]
		[Obsolete("ValidateIfRetargetable is not supported anymore, please use isHumanMotion instead.", true)]
		public bool ValidateIfRetargetable(bool val)
		{
			return false;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000436F File Offset: 0x0000256F
		[Obsolete("isAnimatorMotion is not supported anymore, please use !legacy instead.", true)]
		[EditorBrowsable(1)]
		public bool isAnimatorMotion { get; }

		// Token: 0x06000288 RID: 648
		[MethodImpl(4096)]
		private extern void get_averageSpeed_Injected(out Vector3 ret);
	}
}
