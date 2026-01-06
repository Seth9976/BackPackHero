using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D5 RID: 213
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	public struct TouchState : IInputStateTypeInfo
	{
		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00041FF7 File Offset: 0x000401F7
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'O', 'U', 'C');
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x00042006 File Offset: 0x00040206
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0004200E File Offset: 0x0004020E
		public TouchPhase phase
		{
			get
			{
				return (TouchPhase)this.phaseId;
			}
			set
			{
				this.phaseId = (byte)value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00042018 File Offset: 0x00040218
		public bool isNoneEndedOrCanceled
		{
			get
			{
				return this.phase == TouchPhase.None || this.phase == TouchPhase.Ended || this.phase == TouchPhase.Canceled;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00042036 File Offset: 0x00040236
		public bool isInProgress
		{
			get
			{
				return this.phase == TouchPhase.Began || this.phase == TouchPhase.Moved || this.phase == TouchPhase.Stationary;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00042055 File Offset: 0x00040255
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00042062 File Offset: 0x00040262
		public bool isPrimaryTouch
		{
			get
			{
				return (this.flags & 8) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 8;
					return;
				}
				this.flags &= 247;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0004208A File Offset: 0x0004028A
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x00042098 File Offset: 0x00040298
		internal bool isOrphanedPrimaryTouch
		{
			get
			{
				return (this.flags & 64) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 64;
					return;
				}
				this.flags &= 191;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x000420C1 File Offset: 0x000402C1
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x000420CE File Offset: 0x000402CE
		public bool isIndirectTouch
		{
			get
			{
				return (this.flags & 1) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 1;
					return;
				}
				this.flags &= 254;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x000420F6 File Offset: 0x000402F6
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x000420FE File Offset: 0x000402FE
		public bool isTap
		{
			get
			{
				return this.isTapPress;
			}
			set
			{
				this.isTapPress = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x00042107 File Offset: 0x00040307
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x00042115 File Offset: 0x00040315
		internal bool isTapPress
		{
			get
			{
				return (this.flags & 16) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 16;
					return;
				}
				this.flags &= 239;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0004213E File Offset: 0x0004033E
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0004214C File Offset: 0x0004034C
		internal bool isTapRelease
		{
			get
			{
				return (this.flags & 32) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 32;
					return;
				}
				this.flags &= 223;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x00042175 File Offset: 0x00040375
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x00042186 File Offset: 0x00040386
		internal bool beganInSameFrame
		{
			get
			{
				return (this.flags & 128) > 0;
			}
			set
			{
				if (value)
				{
					this.flags |= 128;
					return;
				}
				this.flags &= 127;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000421AF File Offset: 0x000403AF
		public FourCC format
		{
			get
			{
				return TouchState.Format;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000421B8 File Offset: 0x000403B8
		public override string ToString()
		{
			return string.Format("{{ id={0} phase={1} pos={2} delta={3} pressure={4} radius={5} primary={6} }}", new object[] { this.touchId, this.phase, this.position, this.delta, this.pressure, this.radius, this.isPrimaryTouch });
		}

		// Token: 0x04000537 RID: 1335
		internal const int kSizeInBytes = 56;

		// Token: 0x04000538 RID: 1336
		[InputControl(displayName = "Touch ID", layout = "Integer", synthetic = true, dontReset = true)]
		[FieldOffset(0)]
		public int touchId;

		// Token: 0x04000539 RID: 1337
		[InputControl(displayName = "Position", dontReset = true)]
		[FieldOffset(4)]
		public Vector2 position;

		// Token: 0x0400053A RID: 1338
		[InputControl(displayName = "Delta", layout = "Delta")]
		[FieldOffset(12)]
		public Vector2 delta;

		// Token: 0x0400053B RID: 1339
		[InputControl(displayName = "Pressure", layout = "Axis")]
		[FieldOffset(20)]
		public float pressure;

		// Token: 0x0400053C RID: 1340
		[InputControl(displayName = "Radius")]
		[FieldOffset(24)]
		public Vector2 radius;

		// Token: 0x0400053D RID: 1341
		[InputControl(name = "phase", displayName = "Touch Phase", layout = "TouchPhase", synthetic = true)]
		[InputControl(name = "press", displayName = "Touch Contact?", layout = "TouchPress", useStateFrom = "phase")]
		[FieldOffset(32)]
		public byte phaseId;

		// Token: 0x0400053E RID: 1342
		[InputControl(name = "tapCount", displayName = "Tap Count", layout = "Integer")]
		[FieldOffset(33)]
		public byte tapCount;

		// Token: 0x0400053F RID: 1343
		[InputControl(name = "displayIndex", displayName = "Display Index", layout = "Integer")]
		[FieldOffset(34)]
		public byte displayIndex;

		// Token: 0x04000540 RID: 1344
		[InputControl(name = "indirectTouch", displayName = "Indirect Touch?", layout = "Button", bit = 0U, synthetic = true)]
		[InputControl(name = "tap", displayName = "Tap", layout = "Button", bit = 4U)]
		[FieldOffset(35)]
		public byte flags;

		// Token: 0x04000541 RID: 1345
		[FieldOffset(36)]
		internal uint updateStepCount;

		// Token: 0x04000542 RID: 1346
		[InputControl(displayName = "Start Time", layout = "Double", synthetic = true)]
		[FieldOffset(40)]
		public double startTime;

		// Token: 0x04000543 RID: 1347
		[InputControl(displayName = "Start Position", synthetic = true)]
		[FieldOffset(48)]
		public Vector2 startPosition;
	}
}
