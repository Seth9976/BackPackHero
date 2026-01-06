using System;
using System.Diagnostics;

namespace Unity.Burst.Intrinsics
{
	// Token: 0x02000019 RID: 25
	public static class Arm
	{
		// Token: 0x0200003D RID: 61
		public class Neon
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00007D6C File Offset: 0x00005F6C
			public static bool IsNeonSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000166 RID: 358 RVA: 0x00007D6F File Offset: 0x00005F6F
			[DebuggerStepThrough]
			public static v64 vadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00007D76 File Offset: 0x00005F76
			[DebuggerStepThrough]
			public static v128 vaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000168 RID: 360 RVA: 0x00007D7D File Offset: 0x00005F7D
			[DebuggerStepThrough]
			public static v64 vadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000169 RID: 361 RVA: 0x00007D84 File Offset: 0x00005F84
			[DebuggerStepThrough]
			public static v128 vaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016A RID: 362 RVA: 0x00007D8B File Offset: 0x00005F8B
			[DebuggerStepThrough]
			public static v64 vadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016B RID: 363 RVA: 0x00007D92 File Offset: 0x00005F92
			[DebuggerStepThrough]
			public static v128 vaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016C RID: 364 RVA: 0x00007D99 File Offset: 0x00005F99
			[DebuggerStepThrough]
			public static v64 vadd_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016D RID: 365 RVA: 0x00007DA0 File Offset: 0x00005FA0
			[DebuggerStepThrough]
			public static v128 vaddq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600016E RID: 366 RVA: 0x00007DA7 File Offset: 0x00005FA7
			[DebuggerStepThrough]
			public static v64 vadd_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vadd_s8(a0, a1);
			}

			// Token: 0x0600016F RID: 367 RVA: 0x00007DB0 File Offset: 0x00005FB0
			[DebuggerStepThrough]
			public static v128 vaddq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddq_s8(a0, a1);
			}

			// Token: 0x06000170 RID: 368 RVA: 0x00007DB9 File Offset: 0x00005FB9
			[DebuggerStepThrough]
			public static v64 vadd_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vadd_s16(a0, a1);
			}

			// Token: 0x06000171 RID: 369 RVA: 0x00007DC2 File Offset: 0x00005FC2
			[DebuggerStepThrough]
			public static v128 vaddq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddq_s16(a0, a1);
			}

			// Token: 0x06000172 RID: 370 RVA: 0x00007DCB File Offset: 0x00005FCB
			[DebuggerStepThrough]
			public static v64 vadd_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vadd_s32(a0, a1);
			}

			// Token: 0x06000173 RID: 371 RVA: 0x00007DD4 File Offset: 0x00005FD4
			[DebuggerStepThrough]
			public static v128 vaddq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddq_s32(a0, a1);
			}

			// Token: 0x06000174 RID: 372 RVA: 0x00007DDD File Offset: 0x00005FDD
			[DebuggerStepThrough]
			public static v64 vadd_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vadd_s64(a0, a1);
			}

			// Token: 0x06000175 RID: 373 RVA: 0x00007DE6 File Offset: 0x00005FE6
			[DebuggerStepThrough]
			public static v128 vaddq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddq_s64(a0, a1);
			}

			// Token: 0x06000176 RID: 374 RVA: 0x00007DEF File Offset: 0x00005FEF
			[DebuggerStepThrough]
			public static v64 vadd_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000177 RID: 375 RVA: 0x00007DF6 File Offset: 0x00005FF6
			[DebuggerStepThrough]
			public static v128 vaddq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000178 RID: 376 RVA: 0x00007DFD File Offset: 0x00005FFD
			[DebuggerStepThrough]
			public static v128 vaddl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000179 RID: 377 RVA: 0x00007E04 File Offset: 0x00006004
			[DebuggerStepThrough]
			public static v128 vaddl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017A RID: 378 RVA: 0x00007E0B File Offset: 0x0000600B
			[DebuggerStepThrough]
			public static v128 vaddl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017B RID: 379 RVA: 0x00007E12 File Offset: 0x00006012
			[DebuggerStepThrough]
			public static v128 vaddl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017C RID: 380 RVA: 0x00007E19 File Offset: 0x00006019
			[DebuggerStepThrough]
			public static v128 vaddl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017D RID: 381 RVA: 0x00007E20 File Offset: 0x00006020
			[DebuggerStepThrough]
			public static v128 vaddl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017E RID: 382 RVA: 0x00007E27 File Offset: 0x00006027
			[DebuggerStepThrough]
			public static v128 vaddw_s8(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600017F RID: 383 RVA: 0x00007E2E File Offset: 0x0000602E
			[DebuggerStepThrough]
			public static v128 vaddw_s16(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00007E35 File Offset: 0x00006035
			[DebuggerStepThrough]
			public static v128 vaddw_s32(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000181 RID: 385 RVA: 0x00007E3C File Offset: 0x0000603C
			[DebuggerStepThrough]
			public static v128 vaddw_u8(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000182 RID: 386 RVA: 0x00007E43 File Offset: 0x00006043
			[DebuggerStepThrough]
			public static v128 vaddw_u16(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000183 RID: 387 RVA: 0x00007E4A File Offset: 0x0000604A
			[DebuggerStepThrough]
			public static v128 vaddw_u32(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000184 RID: 388 RVA: 0x00007E51 File Offset: 0x00006051
			[DebuggerStepThrough]
			public static v64 vhadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000185 RID: 389 RVA: 0x00007E58 File Offset: 0x00006058
			[DebuggerStepThrough]
			public static v128 vhaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000186 RID: 390 RVA: 0x00007E5F File Offset: 0x0000605F
			[DebuggerStepThrough]
			public static v64 vhadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000187 RID: 391 RVA: 0x00007E66 File Offset: 0x00006066
			[DebuggerStepThrough]
			public static v128 vhaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000188 RID: 392 RVA: 0x00007E6D File Offset: 0x0000606D
			[DebuggerStepThrough]
			public static v64 vhadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00007E74 File Offset: 0x00006074
			[DebuggerStepThrough]
			public static v128 vhaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018A RID: 394 RVA: 0x00007E7B File Offset: 0x0000607B
			[DebuggerStepThrough]
			public static v64 vhadd_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00007E82 File Offset: 0x00006082
			[DebuggerStepThrough]
			public static v128 vhaddq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018C RID: 396 RVA: 0x00007E89 File Offset: 0x00006089
			[DebuggerStepThrough]
			public static v64 vhadd_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018D RID: 397 RVA: 0x00007E90 File Offset: 0x00006090
			[DebuggerStepThrough]
			public static v128 vhaddq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00007E97 File Offset: 0x00006097
			[DebuggerStepThrough]
			public static v64 vhadd_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00007E9E File Offset: 0x0000609E
			[DebuggerStepThrough]
			public static v128 vhaddq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000190 RID: 400 RVA: 0x00007EA5 File Offset: 0x000060A5
			[DebuggerStepThrough]
			public static v64 vrhadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000191 RID: 401 RVA: 0x00007EAC File Offset: 0x000060AC
			[DebuggerStepThrough]
			public static v128 vrhaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000192 RID: 402 RVA: 0x00007EB3 File Offset: 0x000060B3
			[DebuggerStepThrough]
			public static v64 vrhadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000193 RID: 403 RVA: 0x00007EBA File Offset: 0x000060BA
			[DebuggerStepThrough]
			public static v128 vrhaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000194 RID: 404 RVA: 0x00007EC1 File Offset: 0x000060C1
			[DebuggerStepThrough]
			public static v64 vrhadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000195 RID: 405 RVA: 0x00007EC8 File Offset: 0x000060C8
			[DebuggerStepThrough]
			public static v128 vrhaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00007ECF File Offset: 0x000060CF
			[DebuggerStepThrough]
			public static v64 vrhadd_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000197 RID: 407 RVA: 0x00007ED6 File Offset: 0x000060D6
			[DebuggerStepThrough]
			public static v128 vrhaddq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000198 RID: 408 RVA: 0x00007EDD File Offset: 0x000060DD
			[DebuggerStepThrough]
			public static v64 vrhadd_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000199 RID: 409 RVA: 0x00007EE4 File Offset: 0x000060E4
			[DebuggerStepThrough]
			public static v128 vrhaddq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019A RID: 410 RVA: 0x00007EEB File Offset: 0x000060EB
			[DebuggerStepThrough]
			public static v64 vrhadd_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00007EF2 File Offset: 0x000060F2
			[DebuggerStepThrough]
			public static v128 vrhaddq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019C RID: 412 RVA: 0x00007EF9 File Offset: 0x000060F9
			[DebuggerStepThrough]
			public static v64 vqadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019D RID: 413 RVA: 0x00007F00 File Offset: 0x00006100
			[DebuggerStepThrough]
			public static v128 vqaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019E RID: 414 RVA: 0x00007F07 File Offset: 0x00006107
			[DebuggerStepThrough]
			public static v64 vqadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600019F RID: 415 RVA: 0x00007F0E File Offset: 0x0000610E
			[DebuggerStepThrough]
			public static v128 vqaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00007F15 File Offset: 0x00006115
			[DebuggerStepThrough]
			public static v64 vqadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A1 RID: 417 RVA: 0x00007F1C File Offset: 0x0000611C
			[DebuggerStepThrough]
			public static v128 vqaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00007F23 File Offset: 0x00006123
			[DebuggerStepThrough]
			public static v64 vqadd_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A3 RID: 419 RVA: 0x00007F2A File Offset: 0x0000612A
			[DebuggerStepThrough]
			public static v128 vqaddq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A4 RID: 420 RVA: 0x00007F31 File Offset: 0x00006131
			[DebuggerStepThrough]
			public static v64 vqadd_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A5 RID: 421 RVA: 0x00007F38 File Offset: 0x00006138
			[DebuggerStepThrough]
			public static v128 vqaddq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A6 RID: 422 RVA: 0x00007F3F File Offset: 0x0000613F
			[DebuggerStepThrough]
			public static v64 vqadd_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A7 RID: 423 RVA: 0x00007F46 File Offset: 0x00006146
			[DebuggerStepThrough]
			public static v128 vqaddq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A8 RID: 424 RVA: 0x00007F4D File Offset: 0x0000614D
			[DebuggerStepThrough]
			public static v64 vqadd_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001A9 RID: 425 RVA: 0x00007F54 File Offset: 0x00006154
			[DebuggerStepThrough]
			public static v128 vqaddq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AA RID: 426 RVA: 0x00007F5B File Offset: 0x0000615B
			[DebuggerStepThrough]
			public static v64 vqadd_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AB RID: 427 RVA: 0x00007F62 File Offset: 0x00006162
			[DebuggerStepThrough]
			public static v128 vqaddq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AC RID: 428 RVA: 0x00007F69 File Offset: 0x00006169
			[DebuggerStepThrough]
			public static v64 vaddhn_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AD RID: 429 RVA: 0x00007F70 File Offset: 0x00006170
			[DebuggerStepThrough]
			public static v64 vaddhn_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AE RID: 430 RVA: 0x00007F77 File Offset: 0x00006177
			[DebuggerStepThrough]
			public static v64 vaddhn_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001AF RID: 431 RVA: 0x00007F7E File Offset: 0x0000617E
			[DebuggerStepThrough]
			public static v64 vaddhn_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddhn_s16(a0, a1);
			}

			// Token: 0x060001B0 RID: 432 RVA: 0x00007F87 File Offset: 0x00006187
			[DebuggerStepThrough]
			public static v64 vaddhn_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddhn_s32(a0, a1);
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x00007F90 File Offset: 0x00006190
			[DebuggerStepThrough]
			public static v64 vaddhn_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vaddhn_s64(a0, a1);
			}

			// Token: 0x060001B2 RID: 434 RVA: 0x00007F99 File Offset: 0x00006199
			[DebuggerStepThrough]
			public static v64 vraddhn_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001B3 RID: 435 RVA: 0x00007FA0 File Offset: 0x000061A0
			[DebuggerStepThrough]
			public static v64 vraddhn_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001B4 RID: 436 RVA: 0x00007FA7 File Offset: 0x000061A7
			[DebuggerStepThrough]
			public static v64 vraddhn_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00007FAE File Offset: 0x000061AE
			[DebuggerStepThrough]
			public static v64 vraddhn_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vraddhn_s16(a0, a1);
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x00007FB7 File Offset: 0x000061B7
			[DebuggerStepThrough]
			public static v64 vraddhn_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vraddhn_s32(a0, a1);
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00007FC0 File Offset: 0x000061C0
			[DebuggerStepThrough]
			public static v64 vraddhn_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vraddhn_s64(a0, a1);
			}

			// Token: 0x060001B8 RID: 440 RVA: 0x00007FC9 File Offset: 0x000061C9
			[DebuggerStepThrough]
			public static v64 vmul_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001B9 RID: 441 RVA: 0x00007FD0 File Offset: 0x000061D0
			[DebuggerStepThrough]
			public static v128 vmulq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BA RID: 442 RVA: 0x00007FD7 File Offset: 0x000061D7
			[DebuggerStepThrough]
			public static v64 vmul_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BB RID: 443 RVA: 0x00007FDE File Offset: 0x000061DE
			[DebuggerStepThrough]
			public static v128 vmulq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BC RID: 444 RVA: 0x00007FE5 File Offset: 0x000061E5
			[DebuggerStepThrough]
			public static v64 vmul_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BD RID: 445 RVA: 0x00007FEC File Offset: 0x000061EC
			[DebuggerStepThrough]
			public static v128 vmulq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001BE RID: 446 RVA: 0x00007FF3 File Offset: 0x000061F3
			[DebuggerStepThrough]
			public static v64 vmul_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vmul_s8(a0, a1);
			}

			// Token: 0x060001BF RID: 447 RVA: 0x00007FFC File Offset: 0x000061FC
			[DebuggerStepThrough]
			public static v128 vmulq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vmulq_s8(a0, a1);
			}

			// Token: 0x060001C0 RID: 448 RVA: 0x00008005 File Offset: 0x00006205
			[DebuggerStepThrough]
			public static v64 vmul_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vmul_s16(a0, a1);
			}

			// Token: 0x060001C1 RID: 449 RVA: 0x0000800E File Offset: 0x0000620E
			[DebuggerStepThrough]
			public static v128 vmulq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vmulq_s16(a0, a1);
			}

			// Token: 0x060001C2 RID: 450 RVA: 0x00008017 File Offset: 0x00006217
			[DebuggerStepThrough]
			public static v64 vmul_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vmul_s32(a0, a1);
			}

			// Token: 0x060001C3 RID: 451 RVA: 0x00008020 File Offset: 0x00006220
			[DebuggerStepThrough]
			public static v128 vmulq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vmulq_s32(a0, a1);
			}

			// Token: 0x060001C4 RID: 452 RVA: 0x00008029 File Offset: 0x00006229
			[DebuggerStepThrough]
			public static v64 vmul_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C5 RID: 453 RVA: 0x00008030 File Offset: 0x00006230
			[DebuggerStepThrough]
			public static v128 vmulq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C6 RID: 454 RVA: 0x00008037 File Offset: 0x00006237
			[DebuggerStepThrough]
			public static v64 vmla_s8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x0000803E File Offset: 0x0000623E
			[DebuggerStepThrough]
			public static v128 vmlaq_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00008045 File Offset: 0x00006245
			[DebuggerStepThrough]
			public static v64 vmla_s16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0000804C File Offset: 0x0000624C
			[DebuggerStepThrough]
			public static v128 vmlaq_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001CA RID: 458 RVA: 0x00008053 File Offset: 0x00006253
			[DebuggerStepThrough]
			public static v64 vmla_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001CB RID: 459 RVA: 0x0000805A File Offset: 0x0000625A
			[DebuggerStepThrough]
			public static v128 vmlaq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001CC RID: 460 RVA: 0x00008061 File Offset: 0x00006261
			[DebuggerStepThrough]
			public static v64 vmla_u8(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmla_s8(a0, a1, a2);
			}

			// Token: 0x060001CD RID: 461 RVA: 0x0000806B File Offset: 0x0000626B
			[DebuggerStepThrough]
			public static v128 vmlaq_u8(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlaq_s8(a0, a1, a2);
			}

			// Token: 0x060001CE RID: 462 RVA: 0x00008075 File Offset: 0x00006275
			[DebuggerStepThrough]
			public static v64 vmla_u16(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmla_s16(a0, a1, a2);
			}

			// Token: 0x060001CF RID: 463 RVA: 0x0000807F File Offset: 0x0000627F
			[DebuggerStepThrough]
			public static v128 vmlaq_u16(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlaq_s16(a0, a1, a2);
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x00008089 File Offset: 0x00006289
			[DebuggerStepThrough]
			public static v64 vmla_u32(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmla_s32(a0, a1, a2);
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x00008093 File Offset: 0x00006293
			[DebuggerStepThrough]
			public static v128 vmlaq_u32(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlaq_s32(a0, a1, a2);
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x0000809D File Offset: 0x0000629D
			[DebuggerStepThrough]
			public static v64 vmla_f32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x000080A4 File Offset: 0x000062A4
			[DebuggerStepThrough]
			public static v128 vmlaq_f32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x000080AB File Offset: 0x000062AB
			[DebuggerStepThrough]
			public static v128 vmlal_s8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x000080B2 File Offset: 0x000062B2
			[DebuggerStepThrough]
			public static v128 vmlal_s16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x000080B9 File Offset: 0x000062B9
			[DebuggerStepThrough]
			public static v128 vmlal_s32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x000080C0 File Offset: 0x000062C0
			[DebuggerStepThrough]
			public static v128 vmlal_u8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x000080C7 File Offset: 0x000062C7
			[DebuggerStepThrough]
			public static v128 vmlal_u16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x000080CE File Offset: 0x000062CE
			[DebuggerStepThrough]
			public static v128 vmlal_u32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DA RID: 474 RVA: 0x000080D5 File Offset: 0x000062D5
			[DebuggerStepThrough]
			public static v64 vmls_s8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DB RID: 475 RVA: 0x000080DC File Offset: 0x000062DC
			[DebuggerStepThrough]
			public static v128 vmlsq_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DC RID: 476 RVA: 0x000080E3 File Offset: 0x000062E3
			[DebuggerStepThrough]
			public static v64 vmls_s16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DD RID: 477 RVA: 0x000080EA File Offset: 0x000062EA
			[DebuggerStepThrough]
			public static v128 vmlsq_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DE RID: 478 RVA: 0x000080F1 File Offset: 0x000062F1
			[DebuggerStepThrough]
			public static v64 vmls_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001DF RID: 479 RVA: 0x000080F8 File Offset: 0x000062F8
			[DebuggerStepThrough]
			public static v128 vmlsq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E0 RID: 480 RVA: 0x000080FF File Offset: 0x000062FF
			[DebuggerStepThrough]
			public static v64 vmls_u8(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmls_s8(a0, a1, a2);
			}

			// Token: 0x060001E1 RID: 481 RVA: 0x00008109 File Offset: 0x00006309
			[DebuggerStepThrough]
			public static v128 vmlsq_u8(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlsq_s8(a0, a1, a2);
			}

			// Token: 0x060001E2 RID: 482 RVA: 0x00008113 File Offset: 0x00006313
			[DebuggerStepThrough]
			public static v64 vmls_u16(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmls_s16(a0, a1, a2);
			}

			// Token: 0x060001E3 RID: 483 RVA: 0x0000811D File Offset: 0x0000631D
			[DebuggerStepThrough]
			public static v128 vmlsq_u16(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlsq_s16(a0, a1, a2);
			}

			// Token: 0x060001E4 RID: 484 RVA: 0x00008127 File Offset: 0x00006327
			[DebuggerStepThrough]
			public static v64 vmls_u32(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vmls_s32(a0, a1, a2);
			}

			// Token: 0x060001E5 RID: 485 RVA: 0x00008131 File Offset: 0x00006331
			[DebuggerStepThrough]
			public static v128 vmlsq_u32(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vmlsq_s32(a0, a1, a2);
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000813B File Offset: 0x0000633B
			[DebuggerStepThrough]
			public static v64 vmls_f32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x00008142 File Offset: 0x00006342
			[DebuggerStepThrough]
			public static v128 vmlsq_f32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x00008149 File Offset: 0x00006349
			[DebuggerStepThrough]
			public static v128 vmlsl_s8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x00008150 File Offset: 0x00006350
			[DebuggerStepThrough]
			public static v128 vmlsl_s16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EA RID: 490 RVA: 0x00008157 File Offset: 0x00006357
			[DebuggerStepThrough]
			public static v128 vmlsl_s32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EB RID: 491 RVA: 0x0000815E File Offset: 0x0000635E
			[DebuggerStepThrough]
			public static v128 vmlsl_u8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00008165 File Offset: 0x00006365
			[DebuggerStepThrough]
			public static v128 vmlsl_u16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001ED RID: 493 RVA: 0x0000816C File Offset: 0x0000636C
			[DebuggerStepThrough]
			public static v128 vmlsl_u32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00008173 File Offset: 0x00006373
			[DebuggerStepThrough]
			public static v64 vfma_f32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000817A File Offset: 0x0000637A
			[DebuggerStepThrough]
			public static v128 vfmaq_f32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x00008181 File Offset: 0x00006381
			[DebuggerStepThrough]
			public static v64 vfms_f32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00008188 File Offset: 0x00006388
			[DebuggerStepThrough]
			public static v128 vfmsq_f32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000818F File Offset: 0x0000638F
			[DebuggerStepThrough]
			public static v64 vqdmulh_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00008196 File Offset: 0x00006396
			[DebuggerStepThrough]
			public static v128 vqdmulhq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x0000819D File Offset: 0x0000639D
			[DebuggerStepThrough]
			public static v64 vqdmulh_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x000081A4 File Offset: 0x000063A4
			[DebuggerStepThrough]
			public static v128 vqdmulhq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x000081AB File Offset: 0x000063AB
			[DebuggerStepThrough]
			public static v64 vqrdmulh_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x000081B2 File Offset: 0x000063B2
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x000081B9 File Offset: 0x000063B9
			[DebuggerStepThrough]
			public static v64 vqrdmulh_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x000081C0 File Offset: 0x000063C0
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FA RID: 506 RVA: 0x000081C7 File Offset: 0x000063C7
			[DebuggerStepThrough]
			public static v128 vqdmlal_s16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FB RID: 507 RVA: 0x000081CE File Offset: 0x000063CE
			[DebuggerStepThrough]
			public static v128 vqdmlal_s32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FC RID: 508 RVA: 0x000081D5 File Offset: 0x000063D5
			[DebuggerStepThrough]
			public static v128 vqdmlsl_s16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FD RID: 509 RVA: 0x000081DC File Offset: 0x000063DC
			[DebuggerStepThrough]
			public static v128 vqdmlsl_s32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FE RID: 510 RVA: 0x000081E3 File Offset: 0x000063E3
			[DebuggerStepThrough]
			public static v128 vmull_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060001FF RID: 511 RVA: 0x000081EA File Offset: 0x000063EA
			[DebuggerStepThrough]
			public static v128 vmull_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000200 RID: 512 RVA: 0x000081F1 File Offset: 0x000063F1
			[DebuggerStepThrough]
			public static v128 vmull_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000201 RID: 513 RVA: 0x000081F8 File Offset: 0x000063F8
			[DebuggerStepThrough]
			public static v128 vmull_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000202 RID: 514 RVA: 0x000081FF File Offset: 0x000063FF
			[DebuggerStepThrough]
			public static v128 vmull_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000203 RID: 515 RVA: 0x00008206 File Offset: 0x00006406
			[DebuggerStepThrough]
			public static v128 vmull_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000204 RID: 516 RVA: 0x0000820D File Offset: 0x0000640D
			[DebuggerStepThrough]
			public static v128 vqdmull_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00008214 File Offset: 0x00006414
			[DebuggerStepThrough]
			public static v128 vqdmull_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000821B File Offset: 0x0000641B
			[DebuggerStepThrough]
			public static v64 vsub_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00008222 File Offset: 0x00006422
			[DebuggerStepThrough]
			public static v128 vsubq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00008229 File Offset: 0x00006429
			[DebuggerStepThrough]
			public static v64 vsub_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000209 RID: 521 RVA: 0x00008230 File Offset: 0x00006430
			[DebuggerStepThrough]
			public static v128 vsubq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600020A RID: 522 RVA: 0x00008237 File Offset: 0x00006437
			[DebuggerStepThrough]
			public static v64 vsub_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600020B RID: 523 RVA: 0x0000823E File Offset: 0x0000643E
			[DebuggerStepThrough]
			public static v128 vsubq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600020C RID: 524 RVA: 0x00008245 File Offset: 0x00006445
			[DebuggerStepThrough]
			public static v64 vsub_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600020D RID: 525 RVA: 0x0000824C File Offset: 0x0000644C
			[DebuggerStepThrough]
			public static v128 vsubq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00008253 File Offset: 0x00006453
			[DebuggerStepThrough]
			public static v64 vsub_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vsub_s8(a0, a1);
			}

			// Token: 0x0600020F RID: 527 RVA: 0x0000825C File Offset: 0x0000645C
			[DebuggerStepThrough]
			public static v128 vsubq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubq_s8(a0, a1);
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00008265 File Offset: 0x00006465
			[DebuggerStepThrough]
			public static v64 vsub_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vsub_s16(a0, a1);
			}

			// Token: 0x06000211 RID: 529 RVA: 0x0000826E File Offset: 0x0000646E
			[DebuggerStepThrough]
			public static v128 vsubq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubq_s16(a0, a1);
			}

			// Token: 0x06000212 RID: 530 RVA: 0x00008277 File Offset: 0x00006477
			[DebuggerStepThrough]
			public static v64 vsub_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vsub_s32(a0, a1);
			}

			// Token: 0x06000213 RID: 531 RVA: 0x00008280 File Offset: 0x00006480
			[DebuggerStepThrough]
			public static v128 vsubq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubq_s32(a0, a1);
			}

			// Token: 0x06000214 RID: 532 RVA: 0x00008289 File Offset: 0x00006489
			[DebuggerStepThrough]
			public static v64 vsub_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vsub_s64(a0, a1);
			}

			// Token: 0x06000215 RID: 533 RVA: 0x00008292 File Offset: 0x00006492
			[DebuggerStepThrough]
			public static v128 vsubq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubq_s64(a0, a1);
			}

			// Token: 0x06000216 RID: 534 RVA: 0x0000829B File Offset: 0x0000649B
			[DebuggerStepThrough]
			public static v64 vsub_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000217 RID: 535 RVA: 0x000082A2 File Offset: 0x000064A2
			[DebuggerStepThrough]
			public static v128 vsubq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000218 RID: 536 RVA: 0x000082A9 File Offset: 0x000064A9
			[DebuggerStepThrough]
			public static v128 vsubl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000219 RID: 537 RVA: 0x000082B0 File Offset: 0x000064B0
			[DebuggerStepThrough]
			public static v128 vsubl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021A RID: 538 RVA: 0x000082B7 File Offset: 0x000064B7
			[DebuggerStepThrough]
			public static v128 vsubl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021B RID: 539 RVA: 0x000082BE File Offset: 0x000064BE
			[DebuggerStepThrough]
			public static v128 vsubl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021C RID: 540 RVA: 0x000082C5 File Offset: 0x000064C5
			[DebuggerStepThrough]
			public static v128 vsubl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021D RID: 541 RVA: 0x000082CC File Offset: 0x000064CC
			[DebuggerStepThrough]
			public static v128 vsubl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021E RID: 542 RVA: 0x000082D3 File Offset: 0x000064D3
			[DebuggerStepThrough]
			public static v128 vsubw_s8(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600021F RID: 543 RVA: 0x000082DA File Offset: 0x000064DA
			[DebuggerStepThrough]
			public static v128 vsubw_s16(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000220 RID: 544 RVA: 0x000082E1 File Offset: 0x000064E1
			[DebuggerStepThrough]
			public static v128 vsubw_s32(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000221 RID: 545 RVA: 0x000082E8 File Offset: 0x000064E8
			[DebuggerStepThrough]
			public static v128 vsubw_u8(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000222 RID: 546 RVA: 0x000082EF File Offset: 0x000064EF
			[DebuggerStepThrough]
			public static v128 vsubw_u16(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000223 RID: 547 RVA: 0x000082F6 File Offset: 0x000064F6
			[DebuggerStepThrough]
			public static v128 vsubw_u32(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000224 RID: 548 RVA: 0x000082FD File Offset: 0x000064FD
			[DebuggerStepThrough]
			public static v64 vhsub_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000225 RID: 549 RVA: 0x00008304 File Offset: 0x00006504
			[DebuggerStepThrough]
			public static v128 vhsubq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000830B File Offset: 0x0000650B
			[DebuggerStepThrough]
			public static v64 vhsub_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000227 RID: 551 RVA: 0x00008312 File Offset: 0x00006512
			[DebuggerStepThrough]
			public static v128 vhsubq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000228 RID: 552 RVA: 0x00008319 File Offset: 0x00006519
			[DebuggerStepThrough]
			public static v64 vhsub_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000229 RID: 553 RVA: 0x00008320 File Offset: 0x00006520
			[DebuggerStepThrough]
			public static v128 vhsubq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022A RID: 554 RVA: 0x00008327 File Offset: 0x00006527
			[DebuggerStepThrough]
			public static v64 vhsub_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022B RID: 555 RVA: 0x0000832E File Offset: 0x0000652E
			[DebuggerStepThrough]
			public static v128 vhsubq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022C RID: 556 RVA: 0x00008335 File Offset: 0x00006535
			[DebuggerStepThrough]
			public static v64 vhsub_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022D RID: 557 RVA: 0x0000833C File Offset: 0x0000653C
			[DebuggerStepThrough]
			public static v128 vhsubq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022E RID: 558 RVA: 0x00008343 File Offset: 0x00006543
			[DebuggerStepThrough]
			public static v64 vhsub_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600022F RID: 559 RVA: 0x0000834A File Offset: 0x0000654A
			[DebuggerStepThrough]
			public static v128 vhsubq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000230 RID: 560 RVA: 0x00008351 File Offset: 0x00006551
			[DebuggerStepThrough]
			public static v64 vqsub_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000231 RID: 561 RVA: 0x00008358 File Offset: 0x00006558
			[DebuggerStepThrough]
			public static v128 vqsubq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000232 RID: 562 RVA: 0x0000835F File Offset: 0x0000655F
			[DebuggerStepThrough]
			public static v64 vqsub_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00008366 File Offset: 0x00006566
			[DebuggerStepThrough]
			public static v128 vqsubq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000234 RID: 564 RVA: 0x0000836D File Offset: 0x0000656D
			[DebuggerStepThrough]
			public static v64 vqsub_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000235 RID: 565 RVA: 0x00008374 File Offset: 0x00006574
			[DebuggerStepThrough]
			public static v128 vqsubq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000837B File Offset: 0x0000657B
			[DebuggerStepThrough]
			public static v64 vqsub_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000237 RID: 567 RVA: 0x00008382 File Offset: 0x00006582
			[DebuggerStepThrough]
			public static v128 vqsubq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000238 RID: 568 RVA: 0x00008389 File Offset: 0x00006589
			[DebuggerStepThrough]
			public static v64 vqsub_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000239 RID: 569 RVA: 0x00008390 File Offset: 0x00006590
			[DebuggerStepThrough]
			public static v128 vqsubq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023A RID: 570 RVA: 0x00008397 File Offset: 0x00006597
			[DebuggerStepThrough]
			public static v64 vqsub_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000839E File Offset: 0x0000659E
			[DebuggerStepThrough]
			public static v128 vqsubq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023C RID: 572 RVA: 0x000083A5 File Offset: 0x000065A5
			[DebuggerStepThrough]
			public static v64 vqsub_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023D RID: 573 RVA: 0x000083AC File Offset: 0x000065AC
			[DebuggerStepThrough]
			public static v128 vqsubq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023E RID: 574 RVA: 0x000083B3 File Offset: 0x000065B3
			[DebuggerStepThrough]
			public static v64 vqsub_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600023F RID: 575 RVA: 0x000083BA File Offset: 0x000065BA
			[DebuggerStepThrough]
			public static v128 vqsubq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000240 RID: 576 RVA: 0x000083C1 File Offset: 0x000065C1
			[DebuggerStepThrough]
			public static v64 vsubhn_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000241 RID: 577 RVA: 0x000083C8 File Offset: 0x000065C8
			[DebuggerStepThrough]
			public static v64 vsubhn_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000242 RID: 578 RVA: 0x000083CF File Offset: 0x000065CF
			[DebuggerStepThrough]
			public static v64 vsubhn_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000243 RID: 579 RVA: 0x000083D6 File Offset: 0x000065D6
			[DebuggerStepThrough]
			public static v64 vsubhn_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubhn_s16(a0, a1);
			}

			// Token: 0x06000244 RID: 580 RVA: 0x000083DF File Offset: 0x000065DF
			[DebuggerStepThrough]
			public static v64 vsubhn_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubhn_s32(a0, a1);
			}

			// Token: 0x06000245 RID: 581 RVA: 0x000083E8 File Offset: 0x000065E8
			[DebuggerStepThrough]
			public static v64 vsubhn_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vsubhn_s64(a0, a1);
			}

			// Token: 0x06000246 RID: 582 RVA: 0x000083F1 File Offset: 0x000065F1
			[DebuggerStepThrough]
			public static v64 vrsubhn_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000247 RID: 583 RVA: 0x000083F8 File Offset: 0x000065F8
			[DebuggerStepThrough]
			public static v64 vrsubhn_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000248 RID: 584 RVA: 0x000083FF File Offset: 0x000065FF
			[DebuggerStepThrough]
			public static v64 vrsubhn_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000249 RID: 585 RVA: 0x00008406 File Offset: 0x00006606
			[DebuggerStepThrough]
			public static v64 vrsubhn_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vrsubhn_s16(a0, a1);
			}

			// Token: 0x0600024A RID: 586 RVA: 0x0000840F File Offset: 0x0000660F
			[DebuggerStepThrough]
			public static v64 vrsubhn_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vrsubhn_s32(a0, a1);
			}

			// Token: 0x0600024B RID: 587 RVA: 0x00008418 File Offset: 0x00006618
			[DebuggerStepThrough]
			public static v64 vrsubhn_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vrsubhn_s64(a0, a1);
			}

			// Token: 0x0600024C RID: 588 RVA: 0x00008421 File Offset: 0x00006621
			[DebuggerStepThrough]
			public static v64 vceq_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600024D RID: 589 RVA: 0x00008428 File Offset: 0x00006628
			[DebuggerStepThrough]
			public static v128 vceqq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600024E RID: 590 RVA: 0x0000842F File Offset: 0x0000662F
			[DebuggerStepThrough]
			public static v64 vceq_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600024F RID: 591 RVA: 0x00008436 File Offset: 0x00006636
			[DebuggerStepThrough]
			public static v128 vceqq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000250 RID: 592 RVA: 0x0000843D File Offset: 0x0000663D
			[DebuggerStepThrough]
			public static v64 vceq_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000251 RID: 593 RVA: 0x00008444 File Offset: 0x00006644
			[DebuggerStepThrough]
			public static v128 vceqq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000252 RID: 594 RVA: 0x0000844B File Offset: 0x0000664B
			[DebuggerStepThrough]
			public static v64 vceq_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vceq_s8(a0, a1);
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00008454 File Offset: 0x00006654
			[DebuggerStepThrough]
			public static v128 vceqq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vceqq_s8(a0, a1);
			}

			// Token: 0x06000254 RID: 596 RVA: 0x0000845D File Offset: 0x0000665D
			[DebuggerStepThrough]
			public static v64 vceq_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vceq_s16(a0, a1);
			}

			// Token: 0x06000255 RID: 597 RVA: 0x00008466 File Offset: 0x00006666
			[DebuggerStepThrough]
			public static v128 vceqq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vceqq_s16(a0, a1);
			}

			// Token: 0x06000256 RID: 598 RVA: 0x0000846F File Offset: 0x0000666F
			[DebuggerStepThrough]
			public static v64 vceq_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vceq_s32(a0, a1);
			}

			// Token: 0x06000257 RID: 599 RVA: 0x00008478 File Offset: 0x00006678
			[DebuggerStepThrough]
			public static v128 vceqq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vceqq_s32(a0, a1);
			}

			// Token: 0x06000258 RID: 600 RVA: 0x00008481 File Offset: 0x00006681
			[DebuggerStepThrough]
			public static v64 vceq_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000259 RID: 601 RVA: 0x00008488 File Offset: 0x00006688
			[DebuggerStepThrough]
			public static v128 vceqq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025A RID: 602 RVA: 0x0000848F File Offset: 0x0000668F
			[DebuggerStepThrough]
			public static v64 vcge_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025B RID: 603 RVA: 0x00008496 File Offset: 0x00006696
			[DebuggerStepThrough]
			public static v128 vcgeq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025C RID: 604 RVA: 0x0000849D File Offset: 0x0000669D
			[DebuggerStepThrough]
			public static v64 vcge_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025D RID: 605 RVA: 0x000084A4 File Offset: 0x000066A4
			[DebuggerStepThrough]
			public static v128 vcgeq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025E RID: 606 RVA: 0x000084AB File Offset: 0x000066AB
			[DebuggerStepThrough]
			public static v64 vcge_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600025F RID: 607 RVA: 0x000084B2 File Offset: 0x000066B2
			[DebuggerStepThrough]
			public static v128 vcgeq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000260 RID: 608 RVA: 0x000084B9 File Offset: 0x000066B9
			[DebuggerStepThrough]
			public static v64 vcge_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000261 RID: 609 RVA: 0x000084C0 File Offset: 0x000066C0
			[DebuggerStepThrough]
			public static v128 vcgeq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000262 RID: 610 RVA: 0x000084C7 File Offset: 0x000066C7
			[DebuggerStepThrough]
			public static v64 vcge_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000263 RID: 611 RVA: 0x000084CE File Offset: 0x000066CE
			[DebuggerStepThrough]
			public static v128 vcgeq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000264 RID: 612 RVA: 0x000084D5 File Offset: 0x000066D5
			[DebuggerStepThrough]
			public static v64 vcge_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000265 RID: 613 RVA: 0x000084DC File Offset: 0x000066DC
			[DebuggerStepThrough]
			public static v128 vcgeq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000266 RID: 614 RVA: 0x000084E3 File Offset: 0x000066E3
			[DebuggerStepThrough]
			public static v64 vcge_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000267 RID: 615 RVA: 0x000084EA File Offset: 0x000066EA
			[DebuggerStepThrough]
			public static v128 vcgeq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000268 RID: 616 RVA: 0x000084F1 File Offset: 0x000066F1
			[DebuggerStepThrough]
			public static v64 vcle_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000269 RID: 617 RVA: 0x000084F8 File Offset: 0x000066F8
			[DebuggerStepThrough]
			public static v128 vcleq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026A RID: 618 RVA: 0x000084FF File Offset: 0x000066FF
			[DebuggerStepThrough]
			public static v64 vcle_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026B RID: 619 RVA: 0x00008506 File Offset: 0x00006706
			[DebuggerStepThrough]
			public static v128 vcleq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026C RID: 620 RVA: 0x0000850D File Offset: 0x0000670D
			[DebuggerStepThrough]
			public static v64 vcle_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026D RID: 621 RVA: 0x00008514 File Offset: 0x00006714
			[DebuggerStepThrough]
			public static v128 vcleq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000851B File Offset: 0x0000671B
			[DebuggerStepThrough]
			public static v64 vcle_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600026F RID: 623 RVA: 0x00008522 File Offset: 0x00006722
			[DebuggerStepThrough]
			public static v128 vcleq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000270 RID: 624 RVA: 0x00008529 File Offset: 0x00006729
			[DebuggerStepThrough]
			public static v64 vcle_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000271 RID: 625 RVA: 0x00008530 File Offset: 0x00006730
			[DebuggerStepThrough]
			public static v128 vcleq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000272 RID: 626 RVA: 0x00008537 File Offset: 0x00006737
			[DebuggerStepThrough]
			public static v64 vcle_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000853E File Offset: 0x0000673E
			[DebuggerStepThrough]
			public static v128 vcleq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000274 RID: 628 RVA: 0x00008545 File Offset: 0x00006745
			[DebuggerStepThrough]
			public static v64 vcle_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000275 RID: 629 RVA: 0x0000854C File Offset: 0x0000674C
			[DebuggerStepThrough]
			public static v128 vcleq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000276 RID: 630 RVA: 0x00008553 File Offset: 0x00006753
			[DebuggerStepThrough]
			public static v64 vcgt_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000277 RID: 631 RVA: 0x0000855A File Offset: 0x0000675A
			[DebuggerStepThrough]
			public static v128 vcgtq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000278 RID: 632 RVA: 0x00008561 File Offset: 0x00006761
			[DebuggerStepThrough]
			public static v64 vcgt_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000279 RID: 633 RVA: 0x00008568 File Offset: 0x00006768
			[DebuggerStepThrough]
			public static v128 vcgtq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027A RID: 634 RVA: 0x0000856F File Offset: 0x0000676F
			[DebuggerStepThrough]
			public static v64 vcgt_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027B RID: 635 RVA: 0x00008576 File Offset: 0x00006776
			[DebuggerStepThrough]
			public static v128 vcgtq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027C RID: 636 RVA: 0x0000857D File Offset: 0x0000677D
			[DebuggerStepThrough]
			public static v64 vcgt_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027D RID: 637 RVA: 0x00008584 File Offset: 0x00006784
			[DebuggerStepThrough]
			public static v128 vcgtq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027E RID: 638 RVA: 0x0000858B File Offset: 0x0000678B
			[DebuggerStepThrough]
			public static v64 vcgt_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600027F RID: 639 RVA: 0x00008592 File Offset: 0x00006792
			[DebuggerStepThrough]
			public static v128 vcgtq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000280 RID: 640 RVA: 0x00008599 File Offset: 0x00006799
			[DebuggerStepThrough]
			public static v64 vcgt_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000281 RID: 641 RVA: 0x000085A0 File Offset: 0x000067A0
			[DebuggerStepThrough]
			public static v128 vcgtq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000282 RID: 642 RVA: 0x000085A7 File Offset: 0x000067A7
			[DebuggerStepThrough]
			public static v64 vcgt_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000283 RID: 643 RVA: 0x000085AE File Offset: 0x000067AE
			[DebuggerStepThrough]
			public static v128 vcgtq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000284 RID: 644 RVA: 0x000085B5 File Offset: 0x000067B5
			[DebuggerStepThrough]
			public static v64 vclt_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000285 RID: 645 RVA: 0x000085BC File Offset: 0x000067BC
			[DebuggerStepThrough]
			public static v128 vcltq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000286 RID: 646 RVA: 0x000085C3 File Offset: 0x000067C3
			[DebuggerStepThrough]
			public static v64 vclt_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000287 RID: 647 RVA: 0x000085CA File Offset: 0x000067CA
			[DebuggerStepThrough]
			public static v128 vcltq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000288 RID: 648 RVA: 0x000085D1 File Offset: 0x000067D1
			[DebuggerStepThrough]
			public static v64 vclt_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000289 RID: 649 RVA: 0x000085D8 File Offset: 0x000067D8
			[DebuggerStepThrough]
			public static v128 vcltq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000085DF File Offset: 0x000067DF
			[DebuggerStepThrough]
			public static v64 vclt_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028B RID: 651 RVA: 0x000085E6 File Offset: 0x000067E6
			[DebuggerStepThrough]
			public static v128 vcltq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028C RID: 652 RVA: 0x000085ED File Offset: 0x000067ED
			[DebuggerStepThrough]
			public static v64 vclt_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028D RID: 653 RVA: 0x000085F4 File Offset: 0x000067F4
			[DebuggerStepThrough]
			public static v128 vcltq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028E RID: 654 RVA: 0x000085FB File Offset: 0x000067FB
			[DebuggerStepThrough]
			public static v64 vclt_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600028F RID: 655 RVA: 0x00008602 File Offset: 0x00006802
			[DebuggerStepThrough]
			public static v128 vcltq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00008609 File Offset: 0x00006809
			[DebuggerStepThrough]
			public static v64 vclt_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00008610 File Offset: 0x00006810
			[DebuggerStepThrough]
			public static v128 vcltq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000292 RID: 658 RVA: 0x00008617 File Offset: 0x00006817
			[DebuggerStepThrough]
			public static v64 vcage_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000293 RID: 659 RVA: 0x0000861E File Offset: 0x0000681E
			[DebuggerStepThrough]
			public static v128 vcageq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000294 RID: 660 RVA: 0x00008625 File Offset: 0x00006825
			[DebuggerStepThrough]
			public static v64 vcale_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000295 RID: 661 RVA: 0x0000862C File Offset: 0x0000682C
			[DebuggerStepThrough]
			public static v128 vcaleq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000296 RID: 662 RVA: 0x00008633 File Offset: 0x00006833
			[DebuggerStepThrough]
			public static v64 vcagt_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000863A File Offset: 0x0000683A
			[DebuggerStepThrough]
			public static v128 vcagtq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000298 RID: 664 RVA: 0x00008641 File Offset: 0x00006841
			[DebuggerStepThrough]
			public static v64 vcalt_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000299 RID: 665 RVA: 0x00008648 File Offset: 0x00006848
			[DebuggerStepThrough]
			public static v128 vcaltq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000864F File Offset: 0x0000684F
			[DebuggerStepThrough]
			public static v64 vtst_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029B RID: 667 RVA: 0x00008656 File Offset: 0x00006856
			[DebuggerStepThrough]
			public static v128 vtstq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029C RID: 668 RVA: 0x0000865D File Offset: 0x0000685D
			[DebuggerStepThrough]
			public static v64 vtst_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029D RID: 669 RVA: 0x00008664 File Offset: 0x00006864
			[DebuggerStepThrough]
			public static v128 vtstq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000866B File Offset: 0x0000686B
			[DebuggerStepThrough]
			public static v64 vtst_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600029F RID: 671 RVA: 0x00008672 File Offset: 0x00006872
			[DebuggerStepThrough]
			public static v128 vtstq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x00008679 File Offset: 0x00006879
			[DebuggerStepThrough]
			public static v64 vtst_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vtst_s8(a0, a1);
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x00008682 File Offset: 0x00006882
			[DebuggerStepThrough]
			public static v128 vtstq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vtstq_s8(a0, a1);
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x0000868B File Offset: 0x0000688B
			[DebuggerStepThrough]
			public static v64 vtst_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vtst_s16(a0, a1);
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x00008694 File Offset: 0x00006894
			[DebuggerStepThrough]
			public static v128 vtstq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vtstq_s16(a0, a1);
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x0000869D File Offset: 0x0000689D
			[DebuggerStepThrough]
			public static v64 vtst_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vtst_s32(a0, a1);
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x000086A6 File Offset: 0x000068A6
			[DebuggerStepThrough]
			public static v128 vtstq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vtstq_s32(a0, a1);
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x000086AF File Offset: 0x000068AF
			[DebuggerStepThrough]
			public static v64 vabd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x000086B6 File Offset: 0x000068B6
			[DebuggerStepThrough]
			public static v128 vabdq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002A8 RID: 680 RVA: 0x000086BD File Offset: 0x000068BD
			[DebuggerStepThrough]
			public static v64 vabd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x000086C4 File Offset: 0x000068C4
			[DebuggerStepThrough]
			public static v128 vabdq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AA RID: 682 RVA: 0x000086CB File Offset: 0x000068CB
			[DebuggerStepThrough]
			public static v64 vabd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AB RID: 683 RVA: 0x000086D2 File Offset: 0x000068D2
			[DebuggerStepThrough]
			public static v128 vabdq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AC RID: 684 RVA: 0x000086D9 File Offset: 0x000068D9
			[DebuggerStepThrough]
			public static v64 vabd_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AD RID: 685 RVA: 0x000086E0 File Offset: 0x000068E0
			[DebuggerStepThrough]
			public static v128 vabdq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AE RID: 686 RVA: 0x000086E7 File Offset: 0x000068E7
			[DebuggerStepThrough]
			public static v64 vabd_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002AF RID: 687 RVA: 0x000086EE File Offset: 0x000068EE
			[DebuggerStepThrough]
			public static v128 vabdq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x000086F5 File Offset: 0x000068F5
			[DebuggerStepThrough]
			public static v64 vabd_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x000086FC File Offset: 0x000068FC
			[DebuggerStepThrough]
			public static v128 vabdq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x00008703 File Offset: 0x00006903
			[DebuggerStepThrough]
			public static v64 vabd_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B3 RID: 691 RVA: 0x0000870A File Offset: 0x0000690A
			[DebuggerStepThrough]
			public static v128 vabdq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x00008711 File Offset: 0x00006911
			[DebuggerStepThrough]
			public static v128 vabdl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x00008718 File Offset: 0x00006918
			[DebuggerStepThrough]
			public static v128 vabdl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x0000871F File Offset: 0x0000691F
			[DebuggerStepThrough]
			public static v128 vabdl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x00008726 File Offset: 0x00006926
			[DebuggerStepThrough]
			public static v128 vabdl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000872D File Offset: 0x0000692D
			[DebuggerStepThrough]
			public static v128 vabdl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x00008734 File Offset: 0x00006934
			[DebuggerStepThrough]
			public static v128 vabdl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000873B File Offset: 0x0000693B
			[DebuggerStepThrough]
			public static v64 vaba_s8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BB RID: 699 RVA: 0x00008742 File Offset: 0x00006942
			[DebuggerStepThrough]
			public static v128 vabaq_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BC RID: 700 RVA: 0x00008749 File Offset: 0x00006949
			[DebuggerStepThrough]
			public static v64 vaba_s16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00008750 File Offset: 0x00006950
			[DebuggerStepThrough]
			public static v128 vabaq_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BE RID: 702 RVA: 0x00008757 File Offset: 0x00006957
			[DebuggerStepThrough]
			public static v64 vaba_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000875E File Offset: 0x0000695E
			[DebuggerStepThrough]
			public static v128 vabaq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x00008765 File Offset: 0x00006965
			[DebuggerStepThrough]
			public static v64 vaba_u8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000876C File Offset: 0x0000696C
			[DebuggerStepThrough]
			public static v128 vabaq_u8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x00008773 File Offset: 0x00006973
			[DebuggerStepThrough]
			public static v64 vaba_u16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000877A File Offset: 0x0000697A
			[DebuggerStepThrough]
			public static v128 vabaq_u16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x00008781 File Offset: 0x00006981
			[DebuggerStepThrough]
			public static v64 vaba_u32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x00008788 File Offset: 0x00006988
			[DebuggerStepThrough]
			public static v128 vabaq_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000878F File Offset: 0x0000698F
			[DebuggerStepThrough]
			public static v128 vabal_s8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x00008796 File Offset: 0x00006996
			[DebuggerStepThrough]
			public static v128 vabal_s16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x0000879D File Offset: 0x0000699D
			[DebuggerStepThrough]
			public static v128 vabal_s32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x000087A4 File Offset: 0x000069A4
			[DebuggerStepThrough]
			public static v128 vabal_u8(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CA RID: 714 RVA: 0x000087AB File Offset: 0x000069AB
			[DebuggerStepThrough]
			public static v128 vabal_u16(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CB RID: 715 RVA: 0x000087B2 File Offset: 0x000069B2
			[DebuggerStepThrough]
			public static v128 vabal_u32(v128 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CC RID: 716 RVA: 0x000087B9 File Offset: 0x000069B9
			[DebuggerStepThrough]
			public static v64 vmax_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CD RID: 717 RVA: 0x000087C0 File Offset: 0x000069C0
			[DebuggerStepThrough]
			public static v128 vmaxq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CE RID: 718 RVA: 0x000087C7 File Offset: 0x000069C7
			[DebuggerStepThrough]
			public static v64 vmax_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002CF RID: 719 RVA: 0x000087CE File Offset: 0x000069CE
			[DebuggerStepThrough]
			public static v128 vmaxq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x000087D5 File Offset: 0x000069D5
			[DebuggerStepThrough]
			public static v64 vmax_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x000087DC File Offset: 0x000069DC
			[DebuggerStepThrough]
			public static v128 vmaxq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x000087E3 File Offset: 0x000069E3
			[DebuggerStepThrough]
			public static v64 vmax_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x000087EA File Offset: 0x000069EA
			[DebuggerStepThrough]
			public static v128 vmaxq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x000087F1 File Offset: 0x000069F1
			[DebuggerStepThrough]
			public static v64 vmax_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x000087F8 File Offset: 0x000069F8
			[DebuggerStepThrough]
			public static v128 vmaxq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x000087FF File Offset: 0x000069FF
			[DebuggerStepThrough]
			public static v64 vmax_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00008806 File Offset: 0x00006A06
			[DebuggerStepThrough]
			public static v128 vmaxq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000880D File Offset: 0x00006A0D
			[DebuggerStepThrough]
			public static v64 vmax_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x00008814 File Offset: 0x00006A14
			[DebuggerStepThrough]
			public static v128 vmaxq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000881B File Offset: 0x00006A1B
			[DebuggerStepThrough]
			public static v64 vmin_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DB RID: 731 RVA: 0x00008822 File Offset: 0x00006A22
			[DebuggerStepThrough]
			public static v128 vminq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DC RID: 732 RVA: 0x00008829 File Offset: 0x00006A29
			[DebuggerStepThrough]
			public static v64 vmin_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DD RID: 733 RVA: 0x00008830 File Offset: 0x00006A30
			[DebuggerStepThrough]
			public static v128 vminq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DE RID: 734 RVA: 0x00008837 File Offset: 0x00006A37
			[DebuggerStepThrough]
			public static v64 vmin_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000883E File Offset: 0x00006A3E
			[DebuggerStepThrough]
			public static v128 vminq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x00008845 File Offset: 0x00006A45
			[DebuggerStepThrough]
			public static v64 vmin_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000884C File Offset: 0x00006A4C
			[DebuggerStepThrough]
			public static v128 vminq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x00008853 File Offset: 0x00006A53
			[DebuggerStepThrough]
			public static v64 vmin_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000885A File Offset: 0x00006A5A
			[DebuggerStepThrough]
			public static v128 vminq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x00008861 File Offset: 0x00006A61
			[DebuggerStepThrough]
			public static v64 vmin_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x00008868 File Offset: 0x00006A68
			[DebuggerStepThrough]
			public static v128 vminq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000886F File Offset: 0x00006A6F
			[DebuggerStepThrough]
			public static v64 vmin_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x00008876 File Offset: 0x00006A76
			[DebuggerStepThrough]
			public static v128 vminq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0000887D File Offset: 0x00006A7D
			[DebuggerStepThrough]
			public static v64 vshl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x00008884 File Offset: 0x00006A84
			[DebuggerStepThrough]
			public static v128 vshlq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000888B File Offset: 0x00006A8B
			[DebuggerStepThrough]
			public static v64 vshl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002EB RID: 747 RVA: 0x00008892 File Offset: 0x00006A92
			[DebuggerStepThrough]
			public static v128 vshlq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002EC RID: 748 RVA: 0x00008899 File Offset: 0x00006A99
			[DebuggerStepThrough]
			public static v64 vshl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002ED RID: 749 RVA: 0x000088A0 File Offset: 0x00006AA0
			[DebuggerStepThrough]
			public static v128 vshlq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002EE RID: 750 RVA: 0x000088A7 File Offset: 0x00006AA7
			[DebuggerStepThrough]
			public static v64 vshl_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002EF RID: 751 RVA: 0x000088AE File Offset: 0x00006AAE
			[DebuggerStepThrough]
			public static v128 vshlq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x000088B5 File Offset: 0x00006AB5
			[DebuggerStepThrough]
			public static v64 vshl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x000088BC File Offset: 0x00006ABC
			[DebuggerStepThrough]
			public static v128 vshlq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x000088C3 File Offset: 0x00006AC3
			[DebuggerStepThrough]
			public static v64 vshl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F3 RID: 755 RVA: 0x000088CA File Offset: 0x00006ACA
			[DebuggerStepThrough]
			public static v128 vshlq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x000088D1 File Offset: 0x00006AD1
			[DebuggerStepThrough]
			public static v64 vshl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x000088D8 File Offset: 0x00006AD8
			[DebuggerStepThrough]
			public static v128 vshlq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x000088DF File Offset: 0x00006ADF
			[DebuggerStepThrough]
			public static v64 vshl_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x000088E6 File Offset: 0x00006AE6
			[DebuggerStepThrough]
			public static v128 vshlq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x000088ED File Offset: 0x00006AED
			[DebuggerStepThrough]
			public static v64 vqshl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x000088F4 File Offset: 0x00006AF4
			[DebuggerStepThrough]
			public static v128 vqshlq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FA RID: 762 RVA: 0x000088FB File Offset: 0x00006AFB
			[DebuggerStepThrough]
			public static v64 vqshl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FB RID: 763 RVA: 0x00008902 File Offset: 0x00006B02
			[DebuggerStepThrough]
			public static v128 vqshlq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FC RID: 764 RVA: 0x00008909 File Offset: 0x00006B09
			[DebuggerStepThrough]
			public static v64 vqshl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FD RID: 765 RVA: 0x00008910 File Offset: 0x00006B10
			[DebuggerStepThrough]
			public static v128 vqshlq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FE RID: 766 RVA: 0x00008917 File Offset: 0x00006B17
			[DebuggerStepThrough]
			public static v64 vqshl_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060002FF RID: 767 RVA: 0x0000891E File Offset: 0x00006B1E
			[DebuggerStepThrough]
			public static v128 vqshlq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000300 RID: 768 RVA: 0x00008925 File Offset: 0x00006B25
			[DebuggerStepThrough]
			public static v64 vqshl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000892C File Offset: 0x00006B2C
			[DebuggerStepThrough]
			public static v128 vqshlq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000302 RID: 770 RVA: 0x00008933 File Offset: 0x00006B33
			[DebuggerStepThrough]
			public static v64 vqshl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000893A File Offset: 0x00006B3A
			[DebuggerStepThrough]
			public static v128 vqshlq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000304 RID: 772 RVA: 0x00008941 File Offset: 0x00006B41
			[DebuggerStepThrough]
			public static v64 vqshl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000305 RID: 773 RVA: 0x00008948 File Offset: 0x00006B48
			[DebuggerStepThrough]
			public static v128 vqshlq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000894F File Offset: 0x00006B4F
			[DebuggerStepThrough]
			public static v64 vqshl_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000307 RID: 775 RVA: 0x00008956 File Offset: 0x00006B56
			[DebuggerStepThrough]
			public static v128 vqshlq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000308 RID: 776 RVA: 0x0000895D File Offset: 0x00006B5D
			[DebuggerStepThrough]
			public static v64 vrshl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000309 RID: 777 RVA: 0x00008964 File Offset: 0x00006B64
			[DebuggerStepThrough]
			public static v128 vrshlq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000896B File Offset: 0x00006B6B
			[DebuggerStepThrough]
			public static v64 vrshl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030B RID: 779 RVA: 0x00008972 File Offset: 0x00006B72
			[DebuggerStepThrough]
			public static v128 vrshlq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030C RID: 780 RVA: 0x00008979 File Offset: 0x00006B79
			[DebuggerStepThrough]
			public static v64 vrshl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030D RID: 781 RVA: 0x00008980 File Offset: 0x00006B80
			[DebuggerStepThrough]
			public static v128 vrshlq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030E RID: 782 RVA: 0x00008987 File Offset: 0x00006B87
			[DebuggerStepThrough]
			public static v64 vrshl_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000898E File Offset: 0x00006B8E
			[DebuggerStepThrough]
			public static v128 vrshlq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000310 RID: 784 RVA: 0x00008995 File Offset: 0x00006B95
			[DebuggerStepThrough]
			public static v64 vrshl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000899C File Offset: 0x00006B9C
			[DebuggerStepThrough]
			public static v128 vrshlq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000312 RID: 786 RVA: 0x000089A3 File Offset: 0x00006BA3
			[DebuggerStepThrough]
			public static v64 vrshl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000313 RID: 787 RVA: 0x000089AA File Offset: 0x00006BAA
			[DebuggerStepThrough]
			public static v128 vrshlq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000314 RID: 788 RVA: 0x000089B1 File Offset: 0x00006BB1
			[DebuggerStepThrough]
			public static v64 vrshl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000315 RID: 789 RVA: 0x000089B8 File Offset: 0x00006BB8
			[DebuggerStepThrough]
			public static v128 vrshlq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000316 RID: 790 RVA: 0x000089BF File Offset: 0x00006BBF
			[DebuggerStepThrough]
			public static v64 vrshl_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000317 RID: 791 RVA: 0x000089C6 File Offset: 0x00006BC6
			[DebuggerStepThrough]
			public static v128 vrshlq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000318 RID: 792 RVA: 0x000089CD File Offset: 0x00006BCD
			[DebuggerStepThrough]
			public static v64 vqrshl_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000319 RID: 793 RVA: 0x000089D4 File Offset: 0x00006BD4
			[DebuggerStepThrough]
			public static v128 vqrshlq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031A RID: 794 RVA: 0x000089DB File Offset: 0x00006BDB
			[DebuggerStepThrough]
			public static v64 vqrshl_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031B RID: 795 RVA: 0x000089E2 File Offset: 0x00006BE2
			[DebuggerStepThrough]
			public static v128 vqrshlq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031C RID: 796 RVA: 0x000089E9 File Offset: 0x00006BE9
			[DebuggerStepThrough]
			public static v64 vqrshl_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031D RID: 797 RVA: 0x000089F0 File Offset: 0x00006BF0
			[DebuggerStepThrough]
			public static v128 vqrshlq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031E RID: 798 RVA: 0x000089F7 File Offset: 0x00006BF7
			[DebuggerStepThrough]
			public static v64 vqrshl_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600031F RID: 799 RVA: 0x000089FE File Offset: 0x00006BFE
			[DebuggerStepThrough]
			public static v128 vqrshlq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000320 RID: 800 RVA: 0x00008A05 File Offset: 0x00006C05
			[DebuggerStepThrough]
			public static v64 vqrshl_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000321 RID: 801 RVA: 0x00008A0C File Offset: 0x00006C0C
			[DebuggerStepThrough]
			public static v128 vqrshlq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000322 RID: 802 RVA: 0x00008A13 File Offset: 0x00006C13
			[DebuggerStepThrough]
			public static v64 vqrshl_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000323 RID: 803 RVA: 0x00008A1A File Offset: 0x00006C1A
			[DebuggerStepThrough]
			public static v128 vqrshlq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000324 RID: 804 RVA: 0x00008A21 File Offset: 0x00006C21
			[DebuggerStepThrough]
			public static v64 vqrshl_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000325 RID: 805 RVA: 0x00008A28 File Offset: 0x00006C28
			[DebuggerStepThrough]
			public static v128 vqrshlq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000326 RID: 806 RVA: 0x00008A2F File Offset: 0x00006C2F
			[DebuggerStepThrough]
			public static v64 vqrshl_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000327 RID: 807 RVA: 0x00008A36 File Offset: 0x00006C36
			[DebuggerStepThrough]
			public static v128 vqrshlq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000328 RID: 808 RVA: 0x00008A3D File Offset: 0x00006C3D
			[DebuggerStepThrough]
			public static v64 vshr_n_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000329 RID: 809 RVA: 0x00008A44 File Offset: 0x00006C44
			[DebuggerStepThrough]
			public static v128 vshrq_n_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032A RID: 810 RVA: 0x00008A4B File Offset: 0x00006C4B
			[DebuggerStepThrough]
			public static v64 vshr_n_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032B RID: 811 RVA: 0x00008A52 File Offset: 0x00006C52
			[DebuggerStepThrough]
			public static v128 vshrq_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032C RID: 812 RVA: 0x00008A59 File Offset: 0x00006C59
			[DebuggerStepThrough]
			public static v64 vshr_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032D RID: 813 RVA: 0x00008A60 File Offset: 0x00006C60
			[DebuggerStepThrough]
			public static v128 vshrq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032E RID: 814 RVA: 0x00008A67 File Offset: 0x00006C67
			[DebuggerStepThrough]
			public static v64 vshr_n_s64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600032F RID: 815 RVA: 0x00008A6E File Offset: 0x00006C6E
			[DebuggerStepThrough]
			public static v128 vshrq_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000330 RID: 816 RVA: 0x00008A75 File Offset: 0x00006C75
			[DebuggerStepThrough]
			public static v64 vshr_n_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000331 RID: 817 RVA: 0x00008A7C File Offset: 0x00006C7C
			[DebuggerStepThrough]
			public static v128 vshrq_n_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000332 RID: 818 RVA: 0x00008A83 File Offset: 0x00006C83
			[DebuggerStepThrough]
			public static v64 vshr_n_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000333 RID: 819 RVA: 0x00008A8A File Offset: 0x00006C8A
			[DebuggerStepThrough]
			public static v128 vshrq_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000334 RID: 820 RVA: 0x00008A91 File Offset: 0x00006C91
			[DebuggerStepThrough]
			public static v64 vshr_n_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000335 RID: 821 RVA: 0x00008A98 File Offset: 0x00006C98
			[DebuggerStepThrough]
			public static v128 vshrq_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000336 RID: 822 RVA: 0x00008A9F File Offset: 0x00006C9F
			[DebuggerStepThrough]
			public static v64 vshr_n_u64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000337 RID: 823 RVA: 0x00008AA6 File Offset: 0x00006CA6
			[DebuggerStepThrough]
			public static v128 vshrq_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000338 RID: 824 RVA: 0x00008AAD File Offset: 0x00006CAD
			[DebuggerStepThrough]
			public static v64 vshl_n_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000339 RID: 825 RVA: 0x00008AB4 File Offset: 0x00006CB4
			[DebuggerStepThrough]
			public static v128 vshlq_n_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033A RID: 826 RVA: 0x00008ABB File Offset: 0x00006CBB
			[DebuggerStepThrough]
			public static v64 vshl_n_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033B RID: 827 RVA: 0x00008AC2 File Offset: 0x00006CC2
			[DebuggerStepThrough]
			public static v128 vshlq_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033C RID: 828 RVA: 0x00008AC9 File Offset: 0x00006CC9
			[DebuggerStepThrough]
			public static v64 vshl_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033D RID: 829 RVA: 0x00008AD0 File Offset: 0x00006CD0
			[DebuggerStepThrough]
			public static v128 vshlq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033E RID: 830 RVA: 0x00008AD7 File Offset: 0x00006CD7
			[DebuggerStepThrough]
			public static v64 vshl_n_s64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600033F RID: 831 RVA: 0x00008ADE File Offset: 0x00006CDE
			[DebuggerStepThrough]
			public static v128 vshlq_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000340 RID: 832 RVA: 0x00008AE5 File Offset: 0x00006CE5
			[DebuggerStepThrough]
			public static v64 vshl_n_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000341 RID: 833 RVA: 0x00008AEC File Offset: 0x00006CEC
			[DebuggerStepThrough]
			public static v128 vshlq_n_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000342 RID: 834 RVA: 0x00008AF3 File Offset: 0x00006CF3
			[DebuggerStepThrough]
			public static v64 vshl_n_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000343 RID: 835 RVA: 0x00008AFA File Offset: 0x00006CFA
			[DebuggerStepThrough]
			public static v128 vshlq_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000344 RID: 836 RVA: 0x00008B01 File Offset: 0x00006D01
			[DebuggerStepThrough]
			public static v64 vshl_n_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000345 RID: 837 RVA: 0x00008B08 File Offset: 0x00006D08
			[DebuggerStepThrough]
			public static v128 vshlq_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000346 RID: 838 RVA: 0x00008B0F File Offset: 0x00006D0F
			[DebuggerStepThrough]
			public static v64 vshl_n_u64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000347 RID: 839 RVA: 0x00008B16 File Offset: 0x00006D16
			[DebuggerStepThrough]
			public static v128 vshlq_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000348 RID: 840 RVA: 0x00008B1D File Offset: 0x00006D1D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_s8(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_s8(a0, new v64((sbyte)(-(sbyte)a1)));
			}

			// Token: 0x06000349 RID: 841 RVA: 0x00008B2D File Offset: 0x00006D2D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_s8(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_s8(a0, new v128((sbyte)(-(sbyte)a1)));
			}

			// Token: 0x0600034A RID: 842 RVA: 0x00008B3D File Offset: 0x00006D3D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_s16(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_s16(a0, new v64((short)(-(short)a1)));
			}

			// Token: 0x0600034B RID: 843 RVA: 0x00008B4D File Offset: 0x00006D4D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_s16(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_s16(a0, new v128((short)(-(short)a1)));
			}

			// Token: 0x0600034C RID: 844 RVA: 0x00008B5D File Offset: 0x00006D5D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_s32(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_s32(a0, new v64(-a1));
			}

			// Token: 0x0600034D RID: 845 RVA: 0x00008B6C File Offset: 0x00006D6C
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_s32(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_s32(a0, new v128(-a1));
			}

			// Token: 0x0600034E RID: 846 RVA: 0x00008B7B File Offset: 0x00006D7B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_s64(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_s64(a0, new v64((long)(-(long)a1)));
			}

			// Token: 0x0600034F RID: 847 RVA: 0x00008B8B File Offset: 0x00006D8B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_s64(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_s64(a0, new v128((long)(-(long)a1)));
			}

			// Token: 0x06000350 RID: 848 RVA: 0x00008B9B File Offset: 0x00006D9B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_u8(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_u8(a0, new v64((byte)(-(byte)a1)));
			}

			// Token: 0x06000351 RID: 849 RVA: 0x00008BAB File Offset: 0x00006DAB
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_u8(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_u8(a0, new v128((byte)(-(byte)a1)));
			}

			// Token: 0x06000352 RID: 850 RVA: 0x00008BBB File Offset: 0x00006DBB
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_u16(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_u16(a0, new v64((ushort)(-(ushort)a1)));
			}

			// Token: 0x06000353 RID: 851 RVA: 0x00008BCB File Offset: 0x00006DCB
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_u16(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_u16(a0, new v128((ushort)(-(ushort)a1)));
			}

			// Token: 0x06000354 RID: 852 RVA: 0x00008BDB File Offset: 0x00006DDB
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_u32(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_u32(a0, new v64(-a1));
			}

			// Token: 0x06000355 RID: 853 RVA: 0x00008BEA File Offset: 0x00006DEA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_u32(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_u32(a0, new v128(-a1));
			}

			// Token: 0x06000356 RID: 854 RVA: 0x00008BF9 File Offset: 0x00006DF9
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrshr_n_u64(v64 a0, int a1)
			{
				return Arm.Neon.vrshl_u64(a0, new v64((ulong)((long)(-(long)a1))));
			}

			// Token: 0x06000357 RID: 855 RVA: 0x00008C09 File Offset: 0x00006E09
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrshrq_n_u64(v128 a0, int a1)
			{
				return Arm.Neon.vrshlq_u64(a0, new v128((ulong)((long)(-(long)a1))));
			}

			// Token: 0x06000358 RID: 856 RVA: 0x00008C19 File Offset: 0x00006E19
			[DebuggerStepThrough]
			public static v64 vsra_n_s8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000359 RID: 857 RVA: 0x00008C20 File Offset: 0x00006E20
			[DebuggerStepThrough]
			public static v128 vsraq_n_s8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035A RID: 858 RVA: 0x00008C27 File Offset: 0x00006E27
			[DebuggerStepThrough]
			public static v64 vsra_n_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035B RID: 859 RVA: 0x00008C2E File Offset: 0x00006E2E
			[DebuggerStepThrough]
			public static v128 vsraq_n_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035C RID: 860 RVA: 0x00008C35 File Offset: 0x00006E35
			[DebuggerStepThrough]
			public static v64 vsra_n_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035D RID: 861 RVA: 0x00008C3C File Offset: 0x00006E3C
			[DebuggerStepThrough]
			public static v128 vsraq_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035E RID: 862 RVA: 0x00008C43 File Offset: 0x00006E43
			[DebuggerStepThrough]
			public static v64 vsra_n_s64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600035F RID: 863 RVA: 0x00008C4A File Offset: 0x00006E4A
			[DebuggerStepThrough]
			public static v128 vsraq_n_s64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000360 RID: 864 RVA: 0x00008C51 File Offset: 0x00006E51
			[DebuggerStepThrough]
			public static v64 vsra_n_u8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000361 RID: 865 RVA: 0x00008C58 File Offset: 0x00006E58
			[DebuggerStepThrough]
			public static v128 vsraq_n_u8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000362 RID: 866 RVA: 0x00008C5F File Offset: 0x00006E5F
			[DebuggerStepThrough]
			public static v64 vsra_n_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000363 RID: 867 RVA: 0x00008C66 File Offset: 0x00006E66
			[DebuggerStepThrough]
			public static v128 vsraq_n_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00008C6D File Offset: 0x00006E6D
			[DebuggerStepThrough]
			public static v64 vsra_n_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000365 RID: 869 RVA: 0x00008C74 File Offset: 0x00006E74
			[DebuggerStepThrough]
			public static v128 vsraq_n_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000366 RID: 870 RVA: 0x00008C7B File Offset: 0x00006E7B
			[DebuggerStepThrough]
			public static v64 vsra_n_u64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000367 RID: 871 RVA: 0x00008C82 File Offset: 0x00006E82
			[DebuggerStepThrough]
			public static v128 vsraq_n_u64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000368 RID: 872 RVA: 0x00008C89 File Offset: 0x00006E89
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_s8(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_s8(a0, Arm.Neon.vrshr_n_s8(a1, a2));
			}

			// Token: 0x06000369 RID: 873 RVA: 0x00008C98 File Offset: 0x00006E98
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_s8(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_s8(a0, Arm.Neon.vrshrq_n_s8(a1, a2));
			}

			// Token: 0x0600036A RID: 874 RVA: 0x00008CA7 File Offset: 0x00006EA7
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_s16(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_s16(a0, Arm.Neon.vrshr_n_s16(a1, a2));
			}

			// Token: 0x0600036B RID: 875 RVA: 0x00008CB6 File Offset: 0x00006EB6
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_s16(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_s16(a0, Arm.Neon.vrshrq_n_s16(a1, a2));
			}

			// Token: 0x0600036C RID: 876 RVA: 0x00008CC5 File Offset: 0x00006EC5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_s32(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_s32(a0, Arm.Neon.vrshr_n_s32(a1, a2));
			}

			// Token: 0x0600036D RID: 877 RVA: 0x00008CD4 File Offset: 0x00006ED4
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_s32(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_s32(a0, Arm.Neon.vrshrq_n_s32(a1, a2));
			}

			// Token: 0x0600036E RID: 878 RVA: 0x00008CE3 File Offset: 0x00006EE3
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_s64(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_s64(a0, Arm.Neon.vrshr_n_s64(a1, a2));
			}

			// Token: 0x0600036F RID: 879 RVA: 0x00008CF2 File Offset: 0x00006EF2
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_s64(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_s64(a0, Arm.Neon.vrshrq_n_s64(a1, a2));
			}

			// Token: 0x06000370 RID: 880 RVA: 0x00008D01 File Offset: 0x00006F01
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_u8(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_u8(a0, Arm.Neon.vrshr_n_u8(a1, a2));
			}

			// Token: 0x06000371 RID: 881 RVA: 0x00008D10 File Offset: 0x00006F10
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_u8(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_u8(a0, Arm.Neon.vrshrq_n_u8(a1, a2));
			}

			// Token: 0x06000372 RID: 882 RVA: 0x00008D1F File Offset: 0x00006F1F
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_u16(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_u16(a0, Arm.Neon.vrshr_n_u16(a1, a2));
			}

			// Token: 0x06000373 RID: 883 RVA: 0x00008D2E File Offset: 0x00006F2E
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_u16(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_u16(a0, Arm.Neon.vrshrq_n_u16(a1, a2));
			}

			// Token: 0x06000374 RID: 884 RVA: 0x00008D3D File Offset: 0x00006F3D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_u32(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_u32(a0, Arm.Neon.vrshr_n_u32(a1, a2));
			}

			// Token: 0x06000375 RID: 885 RVA: 0x00008D4C File Offset: 0x00006F4C
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_u32(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_u32(a0, Arm.Neon.vrshrq_n_u32(a1, a2));
			}

			// Token: 0x06000376 RID: 886 RVA: 0x00008D5B File Offset: 0x00006F5B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vrsra_n_u64(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vadd_u64(a0, Arm.Neon.vrshr_n_u64(a1, a2));
			}

			// Token: 0x06000377 RID: 887 RVA: 0x00008D6A File Offset: 0x00006F6A
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vrsraq_n_u64(v128 a0, v128 a1, int a2)
			{
				return Arm.Neon.vaddq_u64(a0, Arm.Neon.vrshrq_n_u64(a1, a2));
			}

			// Token: 0x06000378 RID: 888 RVA: 0x00008D79 File Offset: 0x00006F79
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_s8(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_s8(a0, new v64((sbyte)a1));
			}

			// Token: 0x06000379 RID: 889 RVA: 0x00008D88 File Offset: 0x00006F88
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_s8(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_s8(a0, new v128((sbyte)a1));
			}

			// Token: 0x0600037A RID: 890 RVA: 0x00008D97 File Offset: 0x00006F97
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_s16(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_s16(a0, new v64((short)a1));
			}

			// Token: 0x0600037B RID: 891 RVA: 0x00008DA6 File Offset: 0x00006FA6
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_s16(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_s16(a0, new v128((short)a1));
			}

			// Token: 0x0600037C RID: 892 RVA: 0x00008DB5 File Offset: 0x00006FB5
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_s32(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_s32(a0, new v64(a1));
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00008DC3 File Offset: 0x00006FC3
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_s32(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_s32(a0, new v128(a1));
			}

			// Token: 0x0600037E RID: 894 RVA: 0x00008DD1 File Offset: 0x00006FD1
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_s64(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_s64(a0, new v64((long)a1));
			}

			// Token: 0x0600037F RID: 895 RVA: 0x00008DE0 File Offset: 0x00006FE0
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_s64(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_s64(a0, new v128((long)a1));
			}

			// Token: 0x06000380 RID: 896 RVA: 0x00008DEF File Offset: 0x00006FEF
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_u8(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_u8(a0, new v64((byte)a1));
			}

			// Token: 0x06000381 RID: 897 RVA: 0x00008DFE File Offset: 0x00006FFE
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_u8(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_u8(a0, new v128((byte)a1));
			}

			// Token: 0x06000382 RID: 898 RVA: 0x00008E0D File Offset: 0x0000700D
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_u16(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_u16(a0, new v64((ushort)a1));
			}

			// Token: 0x06000383 RID: 899 RVA: 0x00008E1C File Offset: 0x0000701C
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_u16(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_u16(a0, new v128((ushort)a1));
			}

			// Token: 0x06000384 RID: 900 RVA: 0x00008E2B File Offset: 0x0000702B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_u32(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_u32(a0, new v64((uint)a1));
			}

			// Token: 0x06000385 RID: 901 RVA: 0x00008E39 File Offset: 0x00007039
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_u32(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_u32(a0, new v128((uint)a1));
			}

			// Token: 0x06000386 RID: 902 RVA: 0x00008E47 File Offset: 0x00007047
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v64 vqshl_n_u64(v64 a0, int a1)
			{
				return Arm.Neon.vqshl_u64(a0, new v64((ulong)((long)a1)));
			}

			// Token: 0x06000387 RID: 903 RVA: 0x00008E56 File Offset: 0x00007056
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV7A_NEON32)]
			public static v128 vqshlq_n_u64(v128 a0, int a1)
			{
				return Arm.Neon.vqshlq_u64(a0, new v128((ulong)((long)a1)));
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00008E65 File Offset: 0x00007065
			[DebuggerStepThrough]
			public static v64 vqshlu_n_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000389 RID: 905 RVA: 0x00008E6C File Offset: 0x0000706C
			[DebuggerStepThrough]
			public static v128 vqshluq_n_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038A RID: 906 RVA: 0x00008E73 File Offset: 0x00007073
			[DebuggerStepThrough]
			public static v64 vqshlu_n_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038B RID: 907 RVA: 0x00008E7A File Offset: 0x0000707A
			[DebuggerStepThrough]
			public static v128 vqshluq_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00008E81 File Offset: 0x00007081
			[DebuggerStepThrough]
			public static v64 vqshlu_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038D RID: 909 RVA: 0x00008E88 File Offset: 0x00007088
			[DebuggerStepThrough]
			public static v128 vqshluq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038E RID: 910 RVA: 0x00008E8F File Offset: 0x0000708F
			[DebuggerStepThrough]
			public static v64 vqshlu_n_s64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600038F RID: 911 RVA: 0x00008E96 File Offset: 0x00007096
			[DebuggerStepThrough]
			public static v128 vqshluq_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000390 RID: 912 RVA: 0x00008E9D File Offset: 0x0000709D
			[DebuggerStepThrough]
			public static v64 vshrn_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000391 RID: 913 RVA: 0x00008EA4 File Offset: 0x000070A4
			[DebuggerStepThrough]
			public static v64 vshrn_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000392 RID: 914 RVA: 0x00008EAB File Offset: 0x000070AB
			[DebuggerStepThrough]
			public static v64 vshrn_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000393 RID: 915 RVA: 0x00008EB2 File Offset: 0x000070B2
			[DebuggerStepThrough]
			public static v64 vshrn_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000394 RID: 916 RVA: 0x00008EB9 File Offset: 0x000070B9
			[DebuggerStepThrough]
			public static v64 vshrn_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000395 RID: 917 RVA: 0x00008EC0 File Offset: 0x000070C0
			[DebuggerStepThrough]
			public static v64 vshrn_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00008EC7 File Offset: 0x000070C7
			[DebuggerStepThrough]
			public static v64 vqshrun_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00008ECE File Offset: 0x000070CE
			[DebuggerStepThrough]
			public static v64 vqshrun_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000398 RID: 920 RVA: 0x00008ED5 File Offset: 0x000070D5
			[DebuggerStepThrough]
			public static v64 vqshrun_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00008EDC File Offset: 0x000070DC
			[DebuggerStepThrough]
			public static v64 vqrshrun_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039A RID: 922 RVA: 0x00008EE3 File Offset: 0x000070E3
			[DebuggerStepThrough]
			public static v64 vqrshrun_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039B RID: 923 RVA: 0x00008EEA File Offset: 0x000070EA
			[DebuggerStepThrough]
			public static v64 vqrshrun_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039C RID: 924 RVA: 0x00008EF1 File Offset: 0x000070F1
			[DebuggerStepThrough]
			public static v64 vqshrn_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039D RID: 925 RVA: 0x00008EF8 File Offset: 0x000070F8
			[DebuggerStepThrough]
			public static v64 vqshrn_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039E RID: 926 RVA: 0x00008EFF File Offset: 0x000070FF
			[DebuggerStepThrough]
			public static v64 vqshrn_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600039F RID: 927 RVA: 0x00008F06 File Offset: 0x00007106
			[DebuggerStepThrough]
			public static v64 vqshrn_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A0 RID: 928 RVA: 0x00008F0D File Offset: 0x0000710D
			[DebuggerStepThrough]
			public static v64 vqshrn_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00008F14 File Offset: 0x00007114
			[DebuggerStepThrough]
			public static v64 vqshrn_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00008F1B File Offset: 0x0000711B
			[DebuggerStepThrough]
			public static v64 vrshrn_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x00008F22 File Offset: 0x00007122
			[DebuggerStepThrough]
			public static v64 vrshrn_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00008F29 File Offset: 0x00007129
			[DebuggerStepThrough]
			public static v64 vrshrn_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x00008F30 File Offset: 0x00007130
			[DebuggerStepThrough]
			public static v64 vrshrn_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A6 RID: 934 RVA: 0x00008F37 File Offset: 0x00007137
			[DebuggerStepThrough]
			public static v64 vrshrn_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A7 RID: 935 RVA: 0x00008F3E File Offset: 0x0000713E
			[DebuggerStepThrough]
			public static v64 vrshrn_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00008F45 File Offset: 0x00007145
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003A9 RID: 937 RVA: 0x00008F4C File Offset: 0x0000714C
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AA RID: 938 RVA: 0x00008F53 File Offset: 0x00007153
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AB RID: 939 RVA: 0x00008F5A File Offset: 0x0000715A
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AC RID: 940 RVA: 0x00008F61 File Offset: 0x00007161
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AD RID: 941 RVA: 0x00008F68 File Offset: 0x00007168
			[DebuggerStepThrough]
			public static v64 vqrshrn_n_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AE RID: 942 RVA: 0x00008F6F File Offset: 0x0000716F
			[DebuggerStepThrough]
			public static v128 vshll_n_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003AF RID: 943 RVA: 0x00008F76 File Offset: 0x00007176
			[DebuggerStepThrough]
			public static v128 vshll_n_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B0 RID: 944 RVA: 0x00008F7D File Offset: 0x0000717D
			[DebuggerStepThrough]
			public static v128 vshll_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x00008F84 File Offset: 0x00007184
			[DebuggerStepThrough]
			public static v128 vshll_n_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x00008F8B File Offset: 0x0000718B
			[DebuggerStepThrough]
			public static v128 vshll_n_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x00008F92 File Offset: 0x00007192
			[DebuggerStepThrough]
			public static v128 vshll_n_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x00008F99 File Offset: 0x00007199
			[DebuggerStepThrough]
			public static v64 vsri_n_s8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x00008FA0 File Offset: 0x000071A0
			[DebuggerStepThrough]
			public static v128 vsriq_n_s8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x00008FA7 File Offset: 0x000071A7
			[DebuggerStepThrough]
			public static v64 vsri_n_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B7 RID: 951 RVA: 0x00008FAE File Offset: 0x000071AE
			[DebuggerStepThrough]
			public static v128 vsriq_n_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B8 RID: 952 RVA: 0x00008FB5 File Offset: 0x000071B5
			[DebuggerStepThrough]
			public static v64 vsri_n_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003B9 RID: 953 RVA: 0x00008FBC File Offset: 0x000071BC
			[DebuggerStepThrough]
			public static v128 vsriq_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BA RID: 954 RVA: 0x00008FC3 File Offset: 0x000071C3
			[DebuggerStepThrough]
			public static v64 vsri_n_s64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BB RID: 955 RVA: 0x00008FCA File Offset: 0x000071CA
			[DebuggerStepThrough]
			public static v128 vsriq_n_s64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BC RID: 956 RVA: 0x00008FD1 File Offset: 0x000071D1
			[DebuggerStepThrough]
			public static v64 vsri_n_u8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BD RID: 957 RVA: 0x00008FD8 File Offset: 0x000071D8
			[DebuggerStepThrough]
			public static v128 vsriq_n_u8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BE RID: 958 RVA: 0x00008FDF File Offset: 0x000071DF
			[DebuggerStepThrough]
			public static v64 vsri_n_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003BF RID: 959 RVA: 0x00008FE6 File Offset: 0x000071E6
			[DebuggerStepThrough]
			public static v128 vsriq_n_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C0 RID: 960 RVA: 0x00008FED File Offset: 0x000071ED
			[DebuggerStepThrough]
			public static v64 vsri_n_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C1 RID: 961 RVA: 0x00008FF4 File Offset: 0x000071F4
			[DebuggerStepThrough]
			public static v128 vsriq_n_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C2 RID: 962 RVA: 0x00008FFB File Offset: 0x000071FB
			[DebuggerStepThrough]
			public static v64 vsri_n_u64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C3 RID: 963 RVA: 0x00009002 File Offset: 0x00007202
			[DebuggerStepThrough]
			public static v128 vsriq_n_u64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C4 RID: 964 RVA: 0x00009009 File Offset: 0x00007209
			[DebuggerStepThrough]
			public static v64 vsli_n_s8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C5 RID: 965 RVA: 0x00009010 File Offset: 0x00007210
			[DebuggerStepThrough]
			public static v128 vsliq_n_s8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x00009017 File Offset: 0x00007217
			[DebuggerStepThrough]
			public static v64 vsli_n_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C7 RID: 967 RVA: 0x0000901E File Offset: 0x0000721E
			[DebuggerStepThrough]
			public static v128 vsliq_n_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C8 RID: 968 RVA: 0x00009025 File Offset: 0x00007225
			[DebuggerStepThrough]
			public static v64 vsli_n_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x0000902C File Offset: 0x0000722C
			[DebuggerStepThrough]
			public static v128 vsliq_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CA RID: 970 RVA: 0x00009033 File Offset: 0x00007233
			[DebuggerStepThrough]
			public static v64 vsli_n_s64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CB RID: 971 RVA: 0x0000903A File Offset: 0x0000723A
			[DebuggerStepThrough]
			public static v128 vsliq_n_s64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CC RID: 972 RVA: 0x00009041 File Offset: 0x00007241
			[DebuggerStepThrough]
			public static v64 vsli_n_u8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CD RID: 973 RVA: 0x00009048 File Offset: 0x00007248
			[DebuggerStepThrough]
			public static v128 vsliq_n_u8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CE RID: 974 RVA: 0x0000904F File Offset: 0x0000724F
			[DebuggerStepThrough]
			public static v64 vsli_n_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003CF RID: 975 RVA: 0x00009056 File Offset: 0x00007256
			[DebuggerStepThrough]
			public static v128 vsliq_n_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D0 RID: 976 RVA: 0x0000905D File Offset: 0x0000725D
			[DebuggerStepThrough]
			public static v64 vsli_n_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D1 RID: 977 RVA: 0x00009064 File Offset: 0x00007264
			[DebuggerStepThrough]
			public static v128 vsliq_n_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x0000906B File Offset: 0x0000726B
			[DebuggerStepThrough]
			public static v64 vsli_n_u64(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x00009072 File Offset: 0x00007272
			[DebuggerStepThrough]
			public static v128 vsliq_n_u64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x00009079 File Offset: 0x00007279
			[DebuggerStepThrough]
			public static v64 vcvt_s32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D5 RID: 981 RVA: 0x00009080 File Offset: 0x00007280
			[DebuggerStepThrough]
			public static v128 vcvtq_s32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x00009087 File Offset: 0x00007287
			[DebuggerStepThrough]
			public static v64 vcvt_u32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0000908E File Offset: 0x0000728E
			[DebuggerStepThrough]
			public static v128 vcvtq_u32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x00009095 File Offset: 0x00007295
			[DebuggerStepThrough]
			public static v64 vcvt_n_s32_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x0000909C File Offset: 0x0000729C
			[DebuggerStepThrough]
			public static v128 vcvtq_n_s32_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DA RID: 986 RVA: 0x000090A3 File Offset: 0x000072A3
			[DebuggerStepThrough]
			public static v64 vcvt_n_u32_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DB RID: 987 RVA: 0x000090AA File Offset: 0x000072AA
			[DebuggerStepThrough]
			public static v128 vcvtq_n_u32_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DC RID: 988 RVA: 0x000090B1 File Offset: 0x000072B1
			[DebuggerStepThrough]
			public static v64 vcvt_f32_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DD RID: 989 RVA: 0x000090B8 File Offset: 0x000072B8
			[DebuggerStepThrough]
			public static v128 vcvtq_f32_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DE RID: 990 RVA: 0x000090BF File Offset: 0x000072BF
			[DebuggerStepThrough]
			public static v64 vcvt_f32_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003DF RID: 991 RVA: 0x000090C6 File Offset: 0x000072C6
			[DebuggerStepThrough]
			public static v128 vcvtq_f32_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x000090CD File Offset: 0x000072CD
			[DebuggerStepThrough]
			public static v64 vcvt_n_f32_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E1 RID: 993 RVA: 0x000090D4 File Offset: 0x000072D4
			[DebuggerStepThrough]
			public static v128 vcvtq_n_f32_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x000090DB File Offset: 0x000072DB
			[DebuggerStepThrough]
			public static v64 vcvt_n_f32_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x000090E2 File Offset: 0x000072E2
			[DebuggerStepThrough]
			public static v128 vcvtq_n_f32_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E4 RID: 996 RVA: 0x000090E9 File Offset: 0x000072E9
			[DebuggerStepThrough]
			public static v64 vmovn_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E5 RID: 997 RVA: 0x000090F0 File Offset: 0x000072F0
			[DebuggerStepThrough]
			public static v64 vmovn_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E6 RID: 998 RVA: 0x000090F7 File Offset: 0x000072F7
			[DebuggerStepThrough]
			public static v64 vmovn_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x000090FE File Offset: 0x000072FE
			[DebuggerStepThrough]
			public static v64 vmovn_u16(v128 a0)
			{
				return Arm.Neon.vmovn_s16(a0);
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x00009106 File Offset: 0x00007306
			[DebuggerStepThrough]
			public static v64 vmovn_u32(v128 a0)
			{
				return Arm.Neon.vmovn_s32(a0);
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000910E File Offset: 0x0000730E
			[DebuggerStepThrough]
			public static v64 vmovn_u64(v128 a0)
			{
				return Arm.Neon.vmovn_s64(a0);
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x00009116 File Offset: 0x00007316
			[DebuggerStepThrough]
			public static v128 vmovn_high_s16(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000911D File Offset: 0x0000731D
			[DebuggerStepThrough]
			public static v128 vmovn_high_s32(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003EC RID: 1004 RVA: 0x00009124 File Offset: 0x00007324
			[DebuggerStepThrough]
			public static v128 vmovn_high_s64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003ED RID: 1005 RVA: 0x0000912B File Offset: 0x0000732B
			[DebuggerStepThrough]
			public static v128 vmovn_high_u16(v64 a0, v128 a1)
			{
				return Arm.Neon.vmovn_high_s16(a0, a1);
			}

			// Token: 0x060003EE RID: 1006 RVA: 0x00009134 File Offset: 0x00007334
			[DebuggerStepThrough]
			public static v128 vmovn_high_u32(v64 a0, v128 a1)
			{
				return Arm.Neon.vmovn_high_s32(a0, a1);
			}

			// Token: 0x060003EF RID: 1007 RVA: 0x0000913D File Offset: 0x0000733D
			[DebuggerStepThrough]
			public static v128 vmovn_high_u64(v64 a0, v128 a1)
			{
				return Arm.Neon.vmovn_high_s64(a0, a1);
			}

			// Token: 0x060003F0 RID: 1008 RVA: 0x00009146 File Offset: 0x00007346
			[DebuggerStepThrough]
			public static v128 vmovl_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000914D File Offset: 0x0000734D
			[DebuggerStepThrough]
			public static v128 vmovl_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x00009154 File Offset: 0x00007354
			[DebuggerStepThrough]
			public static v128 vmovl_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0000915B File Offset: 0x0000735B
			[DebuggerStepThrough]
			public static v128 vmovl_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F4 RID: 1012 RVA: 0x00009162 File Offset: 0x00007362
			[DebuggerStepThrough]
			public static v128 vmovl_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F5 RID: 1013 RVA: 0x00009169 File Offset: 0x00007369
			[DebuggerStepThrough]
			public static v128 vmovl_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F6 RID: 1014 RVA: 0x00009170 File Offset: 0x00007370
			[DebuggerStepThrough]
			public static v64 vqmovn_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F7 RID: 1015 RVA: 0x00009177 File Offset: 0x00007377
			[DebuggerStepThrough]
			public static v64 vqmovn_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F8 RID: 1016 RVA: 0x0000917E File Offset: 0x0000737E
			[DebuggerStepThrough]
			public static v64 vqmovn_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003F9 RID: 1017 RVA: 0x00009185 File Offset: 0x00007385
			[DebuggerStepThrough]
			public static v64 vqmovn_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FA RID: 1018 RVA: 0x0000918C File Offset: 0x0000738C
			[DebuggerStepThrough]
			public static v64 vqmovn_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FB RID: 1019 RVA: 0x00009193 File Offset: 0x00007393
			[DebuggerStepThrough]
			public static v64 vqmovn_u64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FC RID: 1020 RVA: 0x0000919A File Offset: 0x0000739A
			[DebuggerStepThrough]
			public static v64 vqmovun_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FD RID: 1021 RVA: 0x000091A1 File Offset: 0x000073A1
			[DebuggerStepThrough]
			public static v64 vqmovun_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FE RID: 1022 RVA: 0x000091A8 File Offset: 0x000073A8
			[DebuggerStepThrough]
			public static v64 vqmovun_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060003FF RID: 1023 RVA: 0x000091AF File Offset: 0x000073AF
			[DebuggerStepThrough]
			public static v64 vmla_lane_s16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000400 RID: 1024 RVA: 0x000091B6 File Offset: 0x000073B6
			[DebuggerStepThrough]
			public static v128 vmlaq_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000401 RID: 1025 RVA: 0x000091BD File Offset: 0x000073BD
			[DebuggerStepThrough]
			public static v64 vmla_lane_s32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000402 RID: 1026 RVA: 0x000091C4 File Offset: 0x000073C4
			[DebuggerStepThrough]
			public static v128 vmlaq_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000403 RID: 1027 RVA: 0x000091CB File Offset: 0x000073CB
			[DebuggerStepThrough]
			public static v64 vmla_lane_u16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000404 RID: 1028 RVA: 0x000091D2 File Offset: 0x000073D2
			[DebuggerStepThrough]
			public static v128 vmlaq_lane_u16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000405 RID: 1029 RVA: 0x000091D9 File Offset: 0x000073D9
			[DebuggerStepThrough]
			public static v64 vmla_lane_u32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000406 RID: 1030 RVA: 0x000091E0 File Offset: 0x000073E0
			[DebuggerStepThrough]
			public static v128 vmlaq_lane_u32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000407 RID: 1031 RVA: 0x000091E7 File Offset: 0x000073E7
			[DebuggerStepThrough]
			public static v64 vmla_lane_f32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000408 RID: 1032 RVA: 0x000091EE File Offset: 0x000073EE
			[DebuggerStepThrough]
			public static v128 vmlaq_lane_f32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000409 RID: 1033 RVA: 0x000091F5 File Offset: 0x000073F5
			[DebuggerStepThrough]
			public static v128 vmlal_lane_s16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x000091FC File Offset: 0x000073FC
			[DebuggerStepThrough]
			public static v128 vmlal_lane_s32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040B RID: 1035 RVA: 0x00009203 File Offset: 0x00007403
			[DebuggerStepThrough]
			public static v128 vmlal_lane_u16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040C RID: 1036 RVA: 0x0000920A File Offset: 0x0000740A
			[DebuggerStepThrough]
			public static v128 vmlal_lane_u32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x00009211 File Offset: 0x00007411
			[DebuggerStepThrough]
			public static v128 vqdmlal_lane_s16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x00009218 File Offset: 0x00007418
			[DebuggerStepThrough]
			public static v128 vqdmlal_lane_s32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000921F File Offset: 0x0000741F
			[DebuggerStepThrough]
			public static v64 vmls_lane_s16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000410 RID: 1040 RVA: 0x00009226 File Offset: 0x00007426
			[DebuggerStepThrough]
			public static v128 vmlsq_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000922D File Offset: 0x0000742D
			[DebuggerStepThrough]
			public static v64 vmls_lane_s32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x00009234 File Offset: 0x00007434
			[DebuggerStepThrough]
			public static v128 vmlsq_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x0000923B File Offset: 0x0000743B
			[DebuggerStepThrough]
			public static v64 vmls_lane_u16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x00009242 File Offset: 0x00007442
			[DebuggerStepThrough]
			public static v128 vmlsq_lane_u16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x00009249 File Offset: 0x00007449
			[DebuggerStepThrough]
			public static v64 vmls_lane_u32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x00009250 File Offset: 0x00007450
			[DebuggerStepThrough]
			public static v128 vmlsq_lane_u32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x00009257 File Offset: 0x00007457
			[DebuggerStepThrough]
			public static v64 vmls_lane_f32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x0000925E File Offset: 0x0000745E
			[DebuggerStepThrough]
			public static v128 vmlsq_lane_f32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x00009265 File Offset: 0x00007465
			[DebuggerStepThrough]
			public static v128 vmlsl_lane_s16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x0000926C File Offset: 0x0000746C
			[DebuggerStepThrough]
			public static v128 vmlsl_lane_s32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x00009273 File Offset: 0x00007473
			[DebuggerStepThrough]
			public static v128 vmlsl_lane_u16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041C RID: 1052 RVA: 0x0000927A File Offset: 0x0000747A
			[DebuggerStepThrough]
			public static v128 vmlsl_lane_u32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x00009281 File Offset: 0x00007481
			[DebuggerStepThrough]
			public static v128 vqdmlsl_lane_s16(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00009288 File Offset: 0x00007488
			[DebuggerStepThrough]
			public static v128 vqdmlsl_lane_s32(v128 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000928F File Offset: 0x0000748F
			[DebuggerStepThrough]
			public static v64 vmul_n_s16(v64 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x00009296 File Offset: 0x00007496
			[DebuggerStepThrough]
			public static v128 vmulq_n_s16(v128 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000929D File Offset: 0x0000749D
			[DebuggerStepThrough]
			public static v64 vmul_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x000092A4 File Offset: 0x000074A4
			[DebuggerStepThrough]
			public static v128 vmulq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x000092AB File Offset: 0x000074AB
			[DebuggerStepThrough]
			public static v64 vmul_n_u16(v64 a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x000092B2 File Offset: 0x000074B2
			[DebuggerStepThrough]
			public static v128 vmulq_n_u16(v128 a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x000092B9 File Offset: 0x000074B9
			[DebuggerStepThrough]
			public static v64 vmul_n_u32(v64 a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x000092C0 File Offset: 0x000074C0
			[DebuggerStepThrough]
			public static v128 vmulq_n_u32(v128 a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x000092C7 File Offset: 0x000074C7
			[DebuggerStepThrough]
			public static v64 vmul_n_f32(v64 a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x000092CE File Offset: 0x000074CE
			[DebuggerStepThrough]
			public static v128 vmulq_n_f32(v128 a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000429 RID: 1065 RVA: 0x000092D5 File Offset: 0x000074D5
			[DebuggerStepThrough]
			public static v64 vmul_lane_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x000092DC File Offset: 0x000074DC
			[DebuggerStepThrough]
			public static v128 vmulq_lane_s16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x000092E3 File Offset: 0x000074E3
			[DebuggerStepThrough]
			public static v64 vmul_lane_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x000092EA File Offset: 0x000074EA
			[DebuggerStepThrough]
			public static v128 vmulq_lane_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x000092F1 File Offset: 0x000074F1
			[DebuggerStepThrough]
			public static v64 vmul_lane_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x000092F8 File Offset: 0x000074F8
			[DebuggerStepThrough]
			public static v128 vmulq_lane_u16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x000092FF File Offset: 0x000074FF
			[DebuggerStepThrough]
			public static v64 vmul_lane_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x00009306 File Offset: 0x00007506
			[DebuggerStepThrough]
			public static v128 vmulq_lane_u32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0000930D File Offset: 0x0000750D
			[DebuggerStepThrough]
			public static v64 vmul_lane_f32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x00009314 File Offset: 0x00007514
			[DebuggerStepThrough]
			public static v128 vmulq_lane_f32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0000931B File Offset: 0x0000751B
			[DebuggerStepThrough]
			public static v128 vmull_n_s16(v64 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00009322 File Offset: 0x00007522
			[DebuggerStepThrough]
			public static v128 vmull_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00009329 File Offset: 0x00007529
			[DebuggerStepThrough]
			public static v128 vmull_n_u16(v64 a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x00009330 File Offset: 0x00007530
			[DebuggerStepThrough]
			public static v128 vmull_n_u32(v64 a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x00009337 File Offset: 0x00007537
			[DebuggerStepThrough]
			public static v128 vmull_lane_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000933E File Offset: 0x0000753E
			[DebuggerStepThrough]
			public static v128 vmull_lane_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x00009345 File Offset: 0x00007545
			[DebuggerStepThrough]
			public static v128 vmull_lane_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0000934C File Offset: 0x0000754C
			[DebuggerStepThrough]
			public static v128 vmull_lane_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x00009353 File Offset: 0x00007553
			[DebuggerStepThrough]
			public static v128 vqdmull_n_s16(v64 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0000935A File Offset: 0x0000755A
			[DebuggerStepThrough]
			public static v128 vqdmull_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x00009361 File Offset: 0x00007561
			[DebuggerStepThrough]
			public static v128 vqdmull_lane_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x00009368 File Offset: 0x00007568
			[DebuggerStepThrough]
			public static v128 vqdmull_lane_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x0000936F File Offset: 0x0000756F
			[DebuggerStepThrough]
			public static v64 vqdmulh_n_s16(v64 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x00009376 File Offset: 0x00007576
			[DebuggerStepThrough]
			public static v128 vqdmulhq_n_s16(v128 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000441 RID: 1089 RVA: 0x0000937D File Offset: 0x0000757D
			[DebuggerStepThrough]
			public static v64 vqdmulh_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000442 RID: 1090 RVA: 0x00009384 File Offset: 0x00007584
			[DebuggerStepThrough]
			public static v128 vqdmulhq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x0000938B File Offset: 0x0000758B
			[DebuggerStepThrough]
			public static v64 vqdmulh_lane_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x00009392 File Offset: 0x00007592
			[DebuggerStepThrough]
			public static v128 vqdmulhq_lane_s16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x00009399 File Offset: 0x00007599
			[DebuggerStepThrough]
			public static v64 vqdmulh_lane_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000446 RID: 1094 RVA: 0x000093A0 File Offset: 0x000075A0
			[DebuggerStepThrough]
			public static v128 vqdmulhq_lane_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x000093A7 File Offset: 0x000075A7
			[DebuggerStepThrough]
			public static v64 vqrdmulh_n_s16(v64 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x000093AE File Offset: 0x000075AE
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_n_s16(v128 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x000093B5 File Offset: 0x000075B5
			[DebuggerStepThrough]
			public static v64 vqrdmulh_n_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x000093BC File Offset: 0x000075BC
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x000093C3 File Offset: 0x000075C3
			[DebuggerStepThrough]
			public static v64 vqrdmulh_lane_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x000093CA File Offset: 0x000075CA
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_lane_s16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044D RID: 1101 RVA: 0x000093D1 File Offset: 0x000075D1
			[DebuggerStepThrough]
			public static v64 vqrdmulh_lane_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044E RID: 1102 RVA: 0x000093D8 File Offset: 0x000075D8
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_lane_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600044F RID: 1103 RVA: 0x000093DF File Offset: 0x000075DF
			[DebuggerStepThrough]
			public static v64 vmla_n_s16(v64 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x000093E6 File Offset: 0x000075E6
			[DebuggerStepThrough]
			public static v128 vmlaq_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x000093ED File Offset: 0x000075ED
			[DebuggerStepThrough]
			public static v64 vmla_n_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x000093F4 File Offset: 0x000075F4
			[DebuggerStepThrough]
			public static v128 vmlaq_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x000093FB File Offset: 0x000075FB
			[DebuggerStepThrough]
			public static v64 vmla_n_u16(v64 a0, v64 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000454 RID: 1108 RVA: 0x00009402 File Offset: 0x00007602
			[DebuggerStepThrough]
			public static v128 vmlaq_n_u16(v128 a0, v128 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000455 RID: 1109 RVA: 0x00009409 File Offset: 0x00007609
			[DebuggerStepThrough]
			public static v64 vmla_n_u32(v64 a0, v64 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000456 RID: 1110 RVA: 0x00009410 File Offset: 0x00007610
			[DebuggerStepThrough]
			public static v128 vmlaq_n_u32(v128 a0, v128 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000457 RID: 1111 RVA: 0x00009417 File Offset: 0x00007617
			[DebuggerStepThrough]
			public static v64 vmla_n_f32(v64 a0, v64 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000458 RID: 1112 RVA: 0x0000941E File Offset: 0x0000761E
			[DebuggerStepThrough]
			public static v128 vmlaq_n_f32(v128 a0, v128 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000459 RID: 1113 RVA: 0x00009425 File Offset: 0x00007625
			[DebuggerStepThrough]
			public static v128 vmlal_n_s16(v128 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045A RID: 1114 RVA: 0x0000942C File Offset: 0x0000762C
			[DebuggerStepThrough]
			public static v128 vmlal_n_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045B RID: 1115 RVA: 0x00009433 File Offset: 0x00007633
			[DebuggerStepThrough]
			public static v128 vmlal_n_u16(v128 a0, v64 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045C RID: 1116 RVA: 0x0000943A File Offset: 0x0000763A
			[DebuggerStepThrough]
			public static v128 vmlal_n_u32(v128 a0, v64 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x00009441 File Offset: 0x00007641
			[DebuggerStepThrough]
			public static v128 vqdmlal_n_s16(v128 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x00009448 File Offset: 0x00007648
			[DebuggerStepThrough]
			public static v128 vqdmlal_n_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x0000944F File Offset: 0x0000764F
			[DebuggerStepThrough]
			public static v64 vmls_n_s16(v64 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x00009456 File Offset: 0x00007656
			[DebuggerStepThrough]
			public static v128 vmlsq_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000461 RID: 1121 RVA: 0x0000945D File Offset: 0x0000765D
			[DebuggerStepThrough]
			public static v64 vmls_n_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x00009464 File Offset: 0x00007664
			[DebuggerStepThrough]
			public static v128 vmlsq_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x0000946B File Offset: 0x0000766B
			[DebuggerStepThrough]
			public static v64 vmls_n_u16(v64 a0, v64 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000464 RID: 1124 RVA: 0x00009472 File Offset: 0x00007672
			[DebuggerStepThrough]
			public static v128 vmlsq_n_u16(v128 a0, v128 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x00009479 File Offset: 0x00007679
			[DebuggerStepThrough]
			public static v64 vmls_n_u32(v64 a0, v64 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x00009480 File Offset: 0x00007680
			[DebuggerStepThrough]
			public static v128 vmlsq_n_u32(v128 a0, v128 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00009487 File Offset: 0x00007687
			[DebuggerStepThrough]
			public static v64 vmls_n_f32(v64 a0, v64 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x0000948E File Offset: 0x0000768E
			[DebuggerStepThrough]
			public static v128 vmlsq_n_f32(v128 a0, v128 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x00009495 File Offset: 0x00007695
			[DebuggerStepThrough]
			public static v128 vmlsl_n_s16(v128 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x0000949C File Offset: 0x0000769C
			[DebuggerStepThrough]
			public static v128 vmlsl_n_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x000094A3 File Offset: 0x000076A3
			[DebuggerStepThrough]
			public static v128 vmlsl_n_u16(v128 a0, v64 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x000094AA File Offset: 0x000076AA
			[DebuggerStepThrough]
			public static v128 vmlsl_n_u32(v128 a0, v64 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x000094B1 File Offset: 0x000076B1
			[DebuggerStepThrough]
			public static v128 vqdmlsl_n_s16(v128 a0, v64 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x000094B8 File Offset: 0x000076B8
			[DebuggerStepThrough]
			public static v128 vqdmlsl_n_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x000094BF File Offset: 0x000076BF
			[DebuggerStepThrough]
			public static v64 vabs_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x000094C6 File Offset: 0x000076C6
			[DebuggerStepThrough]
			public static v128 vabsq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x000094CD File Offset: 0x000076CD
			[DebuggerStepThrough]
			public static v64 vabs_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x000094D4 File Offset: 0x000076D4
			[DebuggerStepThrough]
			public static v128 vabsq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x000094DB File Offset: 0x000076DB
			[DebuggerStepThrough]
			public static v64 vabs_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x000094E2 File Offset: 0x000076E2
			[DebuggerStepThrough]
			public static v128 vabsq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x000094E9 File Offset: 0x000076E9
			[DebuggerStepThrough]
			public static v64 vabs_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000476 RID: 1142 RVA: 0x000094F0 File Offset: 0x000076F0
			[DebuggerStepThrough]
			public static v128 vabsq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x000094F7 File Offset: 0x000076F7
			[DebuggerStepThrough]
			public static v64 vqabs_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x000094FE File Offset: 0x000076FE
			[DebuggerStepThrough]
			public static v128 vqabsq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x00009505 File Offset: 0x00007705
			[DebuggerStepThrough]
			public static v64 vqabs_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x0000950C File Offset: 0x0000770C
			[DebuggerStepThrough]
			public static v128 vqabsq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x00009513 File Offset: 0x00007713
			[DebuggerStepThrough]
			public static v64 vqabs_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x0000951A File Offset: 0x0000771A
			[DebuggerStepThrough]
			public static v128 vqabsq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x00009521 File Offset: 0x00007721
			[DebuggerStepThrough]
			public static v64 vneg_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x00009528 File Offset: 0x00007728
			[DebuggerStepThrough]
			public static v128 vnegq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600047F RID: 1151 RVA: 0x0000952F File Offset: 0x0000772F
			[DebuggerStepThrough]
			public static v64 vneg_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x00009536 File Offset: 0x00007736
			[DebuggerStepThrough]
			public static v128 vnegq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000481 RID: 1153 RVA: 0x0000953D File Offset: 0x0000773D
			[DebuggerStepThrough]
			public static v64 vneg_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x00009544 File Offset: 0x00007744
			[DebuggerStepThrough]
			public static v128 vnegq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x0000954B File Offset: 0x0000774B
			[DebuggerStepThrough]
			public static v64 vneg_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x00009552 File Offset: 0x00007752
			[DebuggerStepThrough]
			public static v128 vnegq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x00009559 File Offset: 0x00007759
			[DebuggerStepThrough]
			public static v64 vqneg_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x00009560 File Offset: 0x00007760
			[DebuggerStepThrough]
			public static v128 vqnegq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x00009567 File Offset: 0x00007767
			[DebuggerStepThrough]
			public static v64 vqneg_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x0000956E File Offset: 0x0000776E
			[DebuggerStepThrough]
			public static v128 vqnegq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x00009575 File Offset: 0x00007775
			[DebuggerStepThrough]
			public static v64 vqneg_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0000957C File Offset: 0x0000777C
			[DebuggerStepThrough]
			public static v128 vqnegq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x00009583 File Offset: 0x00007783
			[DebuggerStepThrough]
			public static v64 vcls_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0000958A File Offset: 0x0000778A
			[DebuggerStepThrough]
			public static v128 vclsq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00009591 File Offset: 0x00007791
			[DebuggerStepThrough]
			public static v64 vcls_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x00009598 File Offset: 0x00007798
			[DebuggerStepThrough]
			public static v128 vclsq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x0000959F File Offset: 0x0000779F
			[DebuggerStepThrough]
			public static v64 vcls_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x000095A6 File Offset: 0x000077A6
			[DebuggerStepThrough]
			public static v128 vclsq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x000095AD File Offset: 0x000077AD
			[DebuggerStepThrough]
			public static v64 vclz_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x000095B4 File Offset: 0x000077B4
			[DebuggerStepThrough]
			public static v128 vclzq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x000095BB File Offset: 0x000077BB
			[DebuggerStepThrough]
			public static v64 vclz_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x000095C2 File Offset: 0x000077C2
			[DebuggerStepThrough]
			public static v128 vclzq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x000095C9 File Offset: 0x000077C9
			[DebuggerStepThrough]
			public static v64 vclz_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000496 RID: 1174 RVA: 0x000095D0 File Offset: 0x000077D0
			[DebuggerStepThrough]
			public static v128 vclzq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x000095D7 File Offset: 0x000077D7
			[DebuggerStepThrough]
			public static v64 vclz_u8(v64 a0)
			{
				return Arm.Neon.vclz_s8(a0);
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x000095DF File Offset: 0x000077DF
			[DebuggerStepThrough]
			public static v128 vclzq_u8(v128 a0)
			{
				return Arm.Neon.vclzq_s8(a0);
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x000095E7 File Offset: 0x000077E7
			[DebuggerStepThrough]
			public static v64 vclz_u16(v64 a0)
			{
				return Arm.Neon.vclz_s16(a0);
			}

			// Token: 0x0600049A RID: 1178 RVA: 0x000095EF File Offset: 0x000077EF
			[DebuggerStepThrough]
			public static v128 vclzq_u16(v128 a0)
			{
				return Arm.Neon.vclzq_s16(a0);
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x000095F7 File Offset: 0x000077F7
			[DebuggerStepThrough]
			public static v64 vclz_u32(v64 a0)
			{
				return Arm.Neon.vclz_s32(a0);
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x000095FF File Offset: 0x000077FF
			[DebuggerStepThrough]
			public static v128 vclzq_u32(v128 a0)
			{
				return Arm.Neon.vclzq_s32(a0);
			}

			// Token: 0x0600049D RID: 1181 RVA: 0x00009607 File Offset: 0x00007807
			[DebuggerStepThrough]
			public static v64 vcnt_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600049E RID: 1182 RVA: 0x0000960E File Offset: 0x0000780E
			[DebuggerStepThrough]
			public static v128 vcntq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x00009615 File Offset: 0x00007815
			[DebuggerStepThrough]
			public static v64 vcnt_u8(v64 a0)
			{
				return Arm.Neon.vcnt_s8(a0);
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x0000961D File Offset: 0x0000781D
			[DebuggerStepThrough]
			public static v128 vcntq_u8(v128 a0)
			{
				return Arm.Neon.vcntq_s8(a0);
			}

			// Token: 0x060004A1 RID: 1185 RVA: 0x00009625 File Offset: 0x00007825
			[DebuggerStepThrough]
			public static v64 vrecpe_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A2 RID: 1186 RVA: 0x0000962C File Offset: 0x0000782C
			[DebuggerStepThrough]
			public static v128 vrecpeq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A3 RID: 1187 RVA: 0x00009633 File Offset: 0x00007833
			[DebuggerStepThrough]
			public static v64 vrecpe_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A4 RID: 1188 RVA: 0x0000963A File Offset: 0x0000783A
			[DebuggerStepThrough]
			public static v128 vrecpeq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A5 RID: 1189 RVA: 0x00009641 File Offset: 0x00007841
			[DebuggerStepThrough]
			public static v64 vrecps_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A6 RID: 1190 RVA: 0x00009648 File Offset: 0x00007848
			[DebuggerStepThrough]
			public static v128 vrecpsq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A7 RID: 1191 RVA: 0x0000964F File Offset: 0x0000784F
			[DebuggerStepThrough]
			public static v64 vrsqrte_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A8 RID: 1192 RVA: 0x00009656 File Offset: 0x00007856
			[DebuggerStepThrough]
			public static v128 vrsqrteq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0000965D File Offset: 0x0000785D
			[DebuggerStepThrough]
			public static v64 vrsqrte_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x00009664 File Offset: 0x00007864
			[DebuggerStepThrough]
			public static v128 vrsqrteq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0000966B File Offset: 0x0000786B
			[DebuggerStepThrough]
			public static v64 vrsqrts_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x00009672 File Offset: 0x00007872
			[DebuggerStepThrough]
			public static v128 vrsqrtsq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x00009679 File Offset: 0x00007879
			[DebuggerStepThrough]
			public static v64 vmvn_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AE RID: 1198 RVA: 0x00009680 File Offset: 0x00007880
			[DebuggerStepThrough]
			public static v128 vmvnq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x00009687 File Offset: 0x00007887
			[DebuggerStepThrough]
			public static v64 vmvn_s16(v64 a0)
			{
				return Arm.Neon.vmvn_s8(a0);
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0000968F File Offset: 0x0000788F
			[DebuggerStepThrough]
			public static v128 vmvnq_s16(v128 a0)
			{
				return Arm.Neon.vmvnq_s8(a0);
			}

			// Token: 0x060004B1 RID: 1201 RVA: 0x00009697 File Offset: 0x00007897
			[DebuggerStepThrough]
			public static v64 vmvn_s32(v64 a0)
			{
				return Arm.Neon.vmvn_s8(a0);
			}

			// Token: 0x060004B2 RID: 1202 RVA: 0x0000969F File Offset: 0x0000789F
			[DebuggerStepThrough]
			public static v128 vmvnq_s32(v128 a0)
			{
				return Arm.Neon.vmvnq_s8(a0);
			}

			// Token: 0x060004B3 RID: 1203 RVA: 0x000096A7 File Offset: 0x000078A7
			[DebuggerStepThrough]
			public static v64 vmvn_u8(v64 a0)
			{
				return Arm.Neon.vmvn_s8(a0);
			}

			// Token: 0x060004B4 RID: 1204 RVA: 0x000096AF File Offset: 0x000078AF
			[DebuggerStepThrough]
			public static v128 vmvnq_u8(v128 a0)
			{
				return Arm.Neon.vmvnq_s8(a0);
			}

			// Token: 0x060004B5 RID: 1205 RVA: 0x000096B7 File Offset: 0x000078B7
			[DebuggerStepThrough]
			public static v64 vmvn_u16(v64 a0)
			{
				return Arm.Neon.vmvn_s8(a0);
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x000096BF File Offset: 0x000078BF
			[DebuggerStepThrough]
			public static v128 vmvnq_u16(v128 a0)
			{
				return Arm.Neon.vmvnq_s8(a0);
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x000096C7 File Offset: 0x000078C7
			[DebuggerStepThrough]
			public static v64 vmvn_u32(v64 a0)
			{
				return Arm.Neon.vmvn_s8(a0);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x000096CF File Offset: 0x000078CF
			[DebuggerStepThrough]
			public static v128 vmvnq_u32(v128 a0)
			{
				return Arm.Neon.vmvnq_s8(a0);
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x000096D7 File Offset: 0x000078D7
			[DebuggerStepThrough]
			public static v64 vand_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004BA RID: 1210 RVA: 0x000096DE File Offset: 0x000078DE
			[DebuggerStepThrough]
			public static v128 vandq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x000096E5 File Offset: 0x000078E5
			[DebuggerStepThrough]
			public static v64 vand_s16(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004BC RID: 1212 RVA: 0x000096EE File Offset: 0x000078EE
			[DebuggerStepThrough]
			public static v128 vandq_s16(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004BD RID: 1213 RVA: 0x000096F7 File Offset: 0x000078F7
			[DebuggerStepThrough]
			public static v64 vand_s32(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004BE RID: 1214 RVA: 0x00009700 File Offset: 0x00007900
			[DebuggerStepThrough]
			public static v128 vandq_s32(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004BF RID: 1215 RVA: 0x00009709 File Offset: 0x00007909
			[DebuggerStepThrough]
			public static v64 vand_s64(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004C0 RID: 1216 RVA: 0x00009712 File Offset: 0x00007912
			[DebuggerStepThrough]
			public static v128 vandq_s64(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004C1 RID: 1217 RVA: 0x0000971B File Offset: 0x0000791B
			[DebuggerStepThrough]
			public static v64 vand_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x00009724 File Offset: 0x00007924
			[DebuggerStepThrough]
			public static v128 vandq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004C3 RID: 1219 RVA: 0x0000972D File Offset: 0x0000792D
			[DebuggerStepThrough]
			public static v64 vand_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x00009736 File Offset: 0x00007936
			[DebuggerStepThrough]
			public static v128 vandq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004C5 RID: 1221 RVA: 0x0000973F File Offset: 0x0000793F
			[DebuggerStepThrough]
			public static v64 vand_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x00009748 File Offset: 0x00007948
			[DebuggerStepThrough]
			public static v128 vandq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x00009751 File Offset: 0x00007951
			[DebuggerStepThrough]
			public static v64 vand_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vand_s8(a0, a1);
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0000975A File Offset: 0x0000795A
			[DebuggerStepThrough]
			public static v128 vandq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vandq_s8(a0, a1);
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x00009763 File Offset: 0x00007963
			[DebuggerStepThrough]
			public static v64 vorr_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0000976A File Offset: 0x0000796A
			[DebuggerStepThrough]
			public static v128 vorrq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x00009771 File Offset: 0x00007971
			[DebuggerStepThrough]
			public static v64 vorr_s16(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004CC RID: 1228 RVA: 0x0000977A File Offset: 0x0000797A
			[DebuggerStepThrough]
			public static v128 vorrq_s16(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x00009783 File Offset: 0x00007983
			[DebuggerStepThrough]
			public static v64 vorr_s32(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x0000978C File Offset: 0x0000798C
			[DebuggerStepThrough]
			public static v128 vorrq_s32(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00009795 File Offset: 0x00007995
			[DebuggerStepThrough]
			public static v64 vorr_s64(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0000979E File Offset: 0x0000799E
			[DebuggerStepThrough]
			public static v128 vorrq_s64(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004D1 RID: 1233 RVA: 0x000097A7 File Offset: 0x000079A7
			[DebuggerStepThrough]
			public static v64 vorr_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x000097B0 File Offset: 0x000079B0
			[DebuggerStepThrough]
			public static v128 vorrq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004D3 RID: 1235 RVA: 0x000097B9 File Offset: 0x000079B9
			[DebuggerStepThrough]
			public static v64 vorr_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x000097C2 File Offset: 0x000079C2
			[DebuggerStepThrough]
			public static v128 vorrq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x000097CB File Offset: 0x000079CB
			[DebuggerStepThrough]
			public static v64 vorr_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x000097D4 File Offset: 0x000079D4
			[DebuggerStepThrough]
			public static v128 vorrq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x000097DD File Offset: 0x000079DD
			[DebuggerStepThrough]
			public static v64 vorr_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vorr_s8(a0, a1);
			}

			// Token: 0x060004D8 RID: 1240 RVA: 0x000097E6 File Offset: 0x000079E6
			[DebuggerStepThrough]
			public static v128 vorrq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vorrq_s8(a0, a1);
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x000097EF File Offset: 0x000079EF
			[DebuggerStepThrough]
			public static v64 veor_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004DA RID: 1242 RVA: 0x000097F6 File Offset: 0x000079F6
			[DebuggerStepThrough]
			public static v128 veorq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x000097FD File Offset: 0x000079FD
			[DebuggerStepThrough]
			public static v64 veor_s16(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x00009806 File Offset: 0x00007A06
			[DebuggerStepThrough]
			public static v128 veorq_s16(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0000980F File Offset: 0x00007A0F
			[DebuggerStepThrough]
			public static v64 veor_s32(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00009818 File Offset: 0x00007A18
			[DebuggerStepThrough]
			public static v128 veorq_s32(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00009821 File Offset: 0x00007A21
			[DebuggerStepThrough]
			public static v64 veor_s64(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x0000982A File Offset: 0x00007A2A
			[DebuggerStepThrough]
			public static v128 veorq_s64(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00009833 File Offset: 0x00007A33
			[DebuggerStepThrough]
			public static v64 veor_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x0000983C File Offset: 0x00007A3C
			[DebuggerStepThrough]
			public static v128 veorq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00009845 File Offset: 0x00007A45
			[DebuggerStepThrough]
			public static v64 veor_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x0000984E File Offset: 0x00007A4E
			[DebuggerStepThrough]
			public static v128 veorq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004E5 RID: 1253 RVA: 0x00009857 File Offset: 0x00007A57
			[DebuggerStepThrough]
			public static v64 veor_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x00009860 File Offset: 0x00007A60
			[DebuggerStepThrough]
			public static v128 veorq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00009869 File Offset: 0x00007A69
			[DebuggerStepThrough]
			public static v64 veor_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.veor_s8(a0, a1);
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00009872 File Offset: 0x00007A72
			[DebuggerStepThrough]
			public static v128 veorq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.veorq_s8(a0, a1);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0000987B File Offset: 0x00007A7B
			[DebuggerStepThrough]
			public static v64 vbic_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00009882 File Offset: 0x00007A82
			[DebuggerStepThrough]
			public static v128 vbicq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004EB RID: 1259 RVA: 0x00009889 File Offset: 0x00007A89
			[DebuggerStepThrough]
			public static v64 vbic_s16(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004EC RID: 1260 RVA: 0x00009892 File Offset: 0x00007A92
			[DebuggerStepThrough]
			public static v128 vbicq_s16(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004ED RID: 1261 RVA: 0x0000989B File Offset: 0x00007A9B
			[DebuggerStepThrough]
			public static v64 vbic_s32(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004EE RID: 1262 RVA: 0x000098A4 File Offset: 0x00007AA4
			[DebuggerStepThrough]
			public static v128 vbicq_s32(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004EF RID: 1263 RVA: 0x000098AD File Offset: 0x00007AAD
			[DebuggerStepThrough]
			public static v64 vbic_s64(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004F0 RID: 1264 RVA: 0x000098B6 File Offset: 0x00007AB6
			[DebuggerStepThrough]
			public static v128 vbicq_s64(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004F1 RID: 1265 RVA: 0x000098BF File Offset: 0x00007ABF
			[DebuggerStepThrough]
			public static v64 vbic_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004F2 RID: 1266 RVA: 0x000098C8 File Offset: 0x00007AC8
			[DebuggerStepThrough]
			public static v128 vbicq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004F3 RID: 1267 RVA: 0x000098D1 File Offset: 0x00007AD1
			[DebuggerStepThrough]
			public static v64 vbic_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004F4 RID: 1268 RVA: 0x000098DA File Offset: 0x00007ADA
			[DebuggerStepThrough]
			public static v128 vbicq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004F5 RID: 1269 RVA: 0x000098E3 File Offset: 0x00007AE3
			[DebuggerStepThrough]
			public static v64 vbic_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004F6 RID: 1270 RVA: 0x000098EC File Offset: 0x00007AEC
			[DebuggerStepThrough]
			public static v128 vbicq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004F7 RID: 1271 RVA: 0x000098F5 File Offset: 0x00007AF5
			[DebuggerStepThrough]
			public static v64 vbic_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vbic_s8(a0, a1);
			}

			// Token: 0x060004F8 RID: 1272 RVA: 0x000098FE File Offset: 0x00007AFE
			[DebuggerStepThrough]
			public static v128 vbicq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vbicq_s8(a0, a1);
			}

			// Token: 0x060004F9 RID: 1273 RVA: 0x00009907 File Offset: 0x00007B07
			[DebuggerStepThrough]
			public static v64 vorn_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004FA RID: 1274 RVA: 0x0000990E File Offset: 0x00007B0E
			[DebuggerStepThrough]
			public static v128 vornq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060004FB RID: 1275 RVA: 0x00009915 File Offset: 0x00007B15
			[DebuggerStepThrough]
			public static v64 vorn_s16(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x060004FC RID: 1276 RVA: 0x0000991E File Offset: 0x00007B1E
			[DebuggerStepThrough]
			public static v128 vornq_s16(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x060004FD RID: 1277 RVA: 0x00009927 File Offset: 0x00007B27
			[DebuggerStepThrough]
			public static v64 vorn_s32(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x060004FE RID: 1278 RVA: 0x00009930 File Offset: 0x00007B30
			[DebuggerStepThrough]
			public static v128 vornq_s32(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00009939 File Offset: 0x00007B39
			[DebuggerStepThrough]
			public static v64 vorn_s64(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x06000500 RID: 1280 RVA: 0x00009942 File Offset: 0x00007B42
			[DebuggerStepThrough]
			public static v128 vornq_s64(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x06000501 RID: 1281 RVA: 0x0000994B File Offset: 0x00007B4B
			[DebuggerStepThrough]
			public static v64 vorn_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x06000502 RID: 1282 RVA: 0x00009954 File Offset: 0x00007B54
			[DebuggerStepThrough]
			public static v128 vornq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x06000503 RID: 1283 RVA: 0x0000995D File Offset: 0x00007B5D
			[DebuggerStepThrough]
			public static v64 vorn_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x06000504 RID: 1284 RVA: 0x00009966 File Offset: 0x00007B66
			[DebuggerStepThrough]
			public static v128 vornq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x06000505 RID: 1285 RVA: 0x0000996F File Offset: 0x00007B6F
			[DebuggerStepThrough]
			public static v64 vorn_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x06000506 RID: 1286 RVA: 0x00009978 File Offset: 0x00007B78
			[DebuggerStepThrough]
			public static v128 vornq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x06000507 RID: 1287 RVA: 0x00009981 File Offset: 0x00007B81
			[DebuggerStepThrough]
			public static v64 vorn_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vorn_s8(a0, a1);
			}

			// Token: 0x06000508 RID: 1288 RVA: 0x0000998A File Offset: 0x00007B8A
			[DebuggerStepThrough]
			public static v128 vornq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vornq_s8(a0, a1);
			}

			// Token: 0x06000509 RID: 1289 RVA: 0x00009993 File Offset: 0x00007B93
			[DebuggerStepThrough]
			public static v64 vbsl_s8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600050A RID: 1290 RVA: 0x0000999A File Offset: 0x00007B9A
			[DebuggerStepThrough]
			public static v128 vbslq_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600050B RID: 1291 RVA: 0x000099A1 File Offset: 0x00007BA1
			[DebuggerStepThrough]
			public static v64 vbsl_s16(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x0600050C RID: 1292 RVA: 0x000099AB File Offset: 0x00007BAB
			[DebuggerStepThrough]
			public static v128 vbslq_s16(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x000099B5 File Offset: 0x00007BB5
			[DebuggerStepThrough]
			public static v64 vbsl_s32(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x000099BF File Offset: 0x00007BBF
			[DebuggerStepThrough]
			public static v128 vbslq_s32(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x000099C9 File Offset: 0x00007BC9
			[DebuggerStepThrough]
			public static v64 vbsl_s64(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x000099D3 File Offset: 0x00007BD3
			[DebuggerStepThrough]
			public static v128 vbslq_s64(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x000099DD File Offset: 0x00007BDD
			[DebuggerStepThrough]
			public static v64 vbsl_u8(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x000099E7 File Offset: 0x00007BE7
			[DebuggerStepThrough]
			public static v128 vbslq_u8(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x000099F1 File Offset: 0x00007BF1
			[DebuggerStepThrough]
			public static v64 vbsl_u16(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x000099FB File Offset: 0x00007BFB
			[DebuggerStepThrough]
			public static v128 vbslq_u16(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x00009A05 File Offset: 0x00007C05
			[DebuggerStepThrough]
			public static v64 vbsl_u32(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x00009A0F File Offset: 0x00007C0F
			[DebuggerStepThrough]
			public static v128 vbslq_u32(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x06000517 RID: 1303 RVA: 0x00009A19 File Offset: 0x00007C19
			[DebuggerStepThrough]
			public static v64 vbsl_u64(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x06000518 RID: 1304 RVA: 0x00009A23 File Offset: 0x00007C23
			[DebuggerStepThrough]
			public static v128 vbslq_u64(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x00009A2D File Offset: 0x00007C2D
			[DebuggerStepThrough]
			public static v64 vbsl_f32(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x0600051A RID: 1306 RVA: 0x00009A37 File Offset: 0x00007C37
			[DebuggerStepThrough]
			public static v128 vbslq_f32(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x00009A41 File Offset: 0x00007C41
			[DebuggerStepThrough]
			public static v64 vdup_lane_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x00009A48 File Offset: 0x00007C48
			[DebuggerStepThrough]
			public static v128 vdupq_lane_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x00009A4F File Offset: 0x00007C4F
			[DebuggerStepThrough]
			public static v64 vdup_lane_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x00009A56 File Offset: 0x00007C56
			[DebuggerStepThrough]
			public static v128 vdupq_lane_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x00009A5D File Offset: 0x00007C5D
			[DebuggerStepThrough]
			public static v64 vdup_lane_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000520 RID: 1312 RVA: 0x00009A64 File Offset: 0x00007C64
			[DebuggerStepThrough]
			public static v128 vdupq_lane_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x00009A6B File Offset: 0x00007C6B
			[DebuggerStepThrough]
			public static v64 vdup_lane_s64(v64 a0, int a1)
			{
				return a0;
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x00009A6E File Offset: 0x00007C6E
			[DebuggerStepThrough]
			public static v128 vdupq_lane_s64(v64 a0, int a1)
			{
				return new v128(a0, a0);
			}

			// Token: 0x06000523 RID: 1315 RVA: 0x00009A77 File Offset: 0x00007C77
			[DebuggerStepThrough]
			public static v64 vdup_lane_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000524 RID: 1316 RVA: 0x00009A7E File Offset: 0x00007C7E
			[DebuggerStepThrough]
			public static v128 vdupq_lane_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000525 RID: 1317 RVA: 0x00009A85 File Offset: 0x00007C85
			[DebuggerStepThrough]
			public static v64 vdup_lane_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000526 RID: 1318 RVA: 0x00009A8C File Offset: 0x00007C8C
			[DebuggerStepThrough]
			public static v128 vdupq_lane_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000527 RID: 1319 RVA: 0x00009A93 File Offset: 0x00007C93
			[DebuggerStepThrough]
			public static v64 vdup_lane_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000528 RID: 1320 RVA: 0x00009A9A File Offset: 0x00007C9A
			[DebuggerStepThrough]
			public static v128 vdupq_lane_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x00009AA1 File Offset: 0x00007CA1
			[DebuggerStepThrough]
			public static v64 vdup_lane_u64(v64 a0, int a1)
			{
				return a0;
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x00009AA4 File Offset: 0x00007CA4
			[DebuggerStepThrough]
			public static v128 vdupq_lane_u64(v64 a0, int a1)
			{
				return new v128(a0, a0);
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x00009AAD File Offset: 0x00007CAD
			[DebuggerStepThrough]
			public static v64 vdup_lane_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600052C RID: 1324 RVA: 0x00009AB4 File Offset: 0x00007CB4
			[DebuggerStepThrough]
			public static v128 vdupq_lane_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600052D RID: 1325 RVA: 0x00009ABB File Offset: 0x00007CBB
			[DebuggerStepThrough]
			public static v64 vpadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600052E RID: 1326 RVA: 0x00009AC2 File Offset: 0x00007CC2
			[DebuggerStepThrough]
			public static v64 vpadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x00009AC9 File Offset: 0x00007CC9
			[DebuggerStepThrough]
			public static v64 vpadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x00009AD0 File Offset: 0x00007CD0
			[DebuggerStepThrough]
			public static v64 vpadd_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vpadd_s8(a0, a1);
			}

			// Token: 0x06000531 RID: 1329 RVA: 0x00009AD9 File Offset: 0x00007CD9
			[DebuggerStepThrough]
			public static v64 vpadd_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vpadd_s16(a0, a1);
			}

			// Token: 0x06000532 RID: 1330 RVA: 0x00009AE2 File Offset: 0x00007CE2
			[DebuggerStepThrough]
			public static v64 vpadd_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vpadd_s32(a0, a1);
			}

			// Token: 0x06000533 RID: 1331 RVA: 0x00009AEB File Offset: 0x00007CEB
			[DebuggerStepThrough]
			public static v64 vpadd_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000534 RID: 1332 RVA: 0x00009AF2 File Offset: 0x00007CF2
			[DebuggerStepThrough]
			public static v64 vpaddl_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x00009AF9 File Offset: 0x00007CF9
			[DebuggerStepThrough]
			public static v128 vpaddlq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x00009B00 File Offset: 0x00007D00
			[DebuggerStepThrough]
			public static v64 vpaddl_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x00009B07 File Offset: 0x00007D07
			[DebuggerStepThrough]
			public static v128 vpaddlq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x00009B0E File Offset: 0x00007D0E
			[DebuggerStepThrough]
			public static v64 vpaddl_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000539 RID: 1337 RVA: 0x00009B15 File Offset: 0x00007D15
			[DebuggerStepThrough]
			public static v128 vpaddlq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053A RID: 1338 RVA: 0x00009B1C File Offset: 0x00007D1C
			[DebuggerStepThrough]
			public static v64 vpaddl_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053B RID: 1339 RVA: 0x00009B23 File Offset: 0x00007D23
			[DebuggerStepThrough]
			public static v128 vpaddlq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053C RID: 1340 RVA: 0x00009B2A File Offset: 0x00007D2A
			[DebuggerStepThrough]
			public static v64 vpaddl_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053D RID: 1341 RVA: 0x00009B31 File Offset: 0x00007D31
			[DebuggerStepThrough]
			public static v128 vpaddlq_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053E RID: 1342 RVA: 0x00009B38 File Offset: 0x00007D38
			[DebuggerStepThrough]
			public static v64 vpaddl_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600053F RID: 1343 RVA: 0x00009B3F File Offset: 0x00007D3F
			[DebuggerStepThrough]
			public static v128 vpaddlq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000540 RID: 1344 RVA: 0x00009B46 File Offset: 0x00007D46
			[DebuggerStepThrough]
			public static v64 vpadal_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000541 RID: 1345 RVA: 0x00009B4D File Offset: 0x00007D4D
			[DebuggerStepThrough]
			public static v128 vpadalq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000542 RID: 1346 RVA: 0x00009B54 File Offset: 0x00007D54
			[DebuggerStepThrough]
			public static v64 vpadal_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000543 RID: 1347 RVA: 0x00009B5B File Offset: 0x00007D5B
			[DebuggerStepThrough]
			public static v128 vpadalq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000544 RID: 1348 RVA: 0x00009B62 File Offset: 0x00007D62
			[DebuggerStepThrough]
			public static v64 vpadal_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x00009B69 File Offset: 0x00007D69
			[DebuggerStepThrough]
			public static v128 vpadalq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000546 RID: 1350 RVA: 0x00009B70 File Offset: 0x00007D70
			[DebuggerStepThrough]
			public static v64 vpadal_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000547 RID: 1351 RVA: 0x00009B77 File Offset: 0x00007D77
			[DebuggerStepThrough]
			public static v128 vpadalq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000548 RID: 1352 RVA: 0x00009B7E File Offset: 0x00007D7E
			[DebuggerStepThrough]
			public static v64 vpadal_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000549 RID: 1353 RVA: 0x00009B85 File Offset: 0x00007D85
			[DebuggerStepThrough]
			public static v128 vpadalq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x00009B8C File Offset: 0x00007D8C
			[DebuggerStepThrough]
			public static v64 vpadal_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054B RID: 1355 RVA: 0x00009B93 File Offset: 0x00007D93
			[DebuggerStepThrough]
			public static v128 vpadalq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054C RID: 1356 RVA: 0x00009B9A File Offset: 0x00007D9A
			[DebuggerStepThrough]
			public static v64 vpmax_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054D RID: 1357 RVA: 0x00009BA1 File Offset: 0x00007DA1
			[DebuggerStepThrough]
			public static v64 vpmax_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x00009BA8 File Offset: 0x00007DA8
			[DebuggerStepThrough]
			public static v64 vpmax_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x00009BAF File Offset: 0x00007DAF
			[DebuggerStepThrough]
			public static v64 vpmax_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x00009BB6 File Offset: 0x00007DB6
			[DebuggerStepThrough]
			public static v64 vpmax_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x00009BBD File Offset: 0x00007DBD
			[DebuggerStepThrough]
			public static v64 vpmax_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000552 RID: 1362 RVA: 0x00009BC4 File Offset: 0x00007DC4
			[DebuggerStepThrough]
			public static v64 vpmax_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000553 RID: 1363 RVA: 0x00009BCB File Offset: 0x00007DCB
			[DebuggerStepThrough]
			public static v64 vpmin_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x00009BD2 File Offset: 0x00007DD2
			[DebuggerStepThrough]
			public static v64 vpmin_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x00009BD9 File Offset: 0x00007DD9
			[DebuggerStepThrough]
			public static v64 vpmin_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000556 RID: 1366 RVA: 0x00009BE0 File Offset: 0x00007DE0
			[DebuggerStepThrough]
			public static v64 vpmin_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000557 RID: 1367 RVA: 0x00009BE7 File Offset: 0x00007DE7
			[DebuggerStepThrough]
			public static v64 vpmin_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000558 RID: 1368 RVA: 0x00009BEE File Offset: 0x00007DEE
			[DebuggerStepThrough]
			public static v64 vpmin_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000559 RID: 1369 RVA: 0x00009BF5 File Offset: 0x00007DF5
			[DebuggerStepThrough]
			public static v64 vpmin_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055A RID: 1370 RVA: 0x00009BFC File Offset: 0x00007DFC
			[DebuggerStepThrough]
			public static v64 vext_s8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055B RID: 1371 RVA: 0x00009C03 File Offset: 0x00007E03
			[DebuggerStepThrough]
			public static v128 vextq_s8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055C RID: 1372 RVA: 0x00009C0A File Offset: 0x00007E0A
			[DebuggerStepThrough]
			public static v64 vext_s16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055D RID: 1373 RVA: 0x00009C11 File Offset: 0x00007E11
			[DebuggerStepThrough]
			public static v128 vextq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055E RID: 1374 RVA: 0x00009C18 File Offset: 0x00007E18
			[DebuggerStepThrough]
			public static v64 vext_s32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600055F RID: 1375 RVA: 0x00009C1F File Offset: 0x00007E1F
			[DebuggerStepThrough]
			public static v128 vextq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x00009C26 File Offset: 0x00007E26
			[DebuggerStepThrough]
			public static v64 vext_s64(v64 a0, v64 a1, int a2)
			{
				return a0;
			}

			// Token: 0x06000561 RID: 1377 RVA: 0x00009C29 File Offset: 0x00007E29
			[DebuggerStepThrough]
			public static v128 vextq_s64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x00009C30 File Offset: 0x00007E30
			[DebuggerStepThrough]
			public static v64 vext_u8(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000563 RID: 1379 RVA: 0x00009C37 File Offset: 0x00007E37
			[DebuggerStepThrough]
			public static v128 vextq_u8(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x00009C3E File Offset: 0x00007E3E
			[DebuggerStepThrough]
			public static v64 vext_u16(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000565 RID: 1381 RVA: 0x00009C45 File Offset: 0x00007E45
			[DebuggerStepThrough]
			public static v128 vextq_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x00009C4C File Offset: 0x00007E4C
			[DebuggerStepThrough]
			public static v64 vext_u32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000567 RID: 1383 RVA: 0x00009C53 File Offset: 0x00007E53
			[DebuggerStepThrough]
			public static v128 vextq_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00009C5A File Offset: 0x00007E5A
			[DebuggerStepThrough]
			public static v64 vext_u64(v64 a0, v64 a1, int a2)
			{
				return a0;
			}

			// Token: 0x06000569 RID: 1385 RVA: 0x00009C5D File Offset: 0x00007E5D
			[DebuggerStepThrough]
			public static v128 vextq_u64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x00009C64 File Offset: 0x00007E64
			[DebuggerStepThrough]
			public static v64 vext_f32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00009C6B File Offset: 0x00007E6B
			[DebuggerStepThrough]
			public static v128 vextq_f32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x00009C72 File Offset: 0x00007E72
			[DebuggerStepThrough]
			public static v64 vrev64_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00009C79 File Offset: 0x00007E79
			[DebuggerStepThrough]
			public static v128 vrev64q_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x00009C80 File Offset: 0x00007E80
			[DebuggerStepThrough]
			public static v64 vrev64_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x00009C87 File Offset: 0x00007E87
			[DebuggerStepThrough]
			public static v128 vrev64q_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x00009C8E File Offset: 0x00007E8E
			[DebuggerStepThrough]
			public static v64 vrev64_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000571 RID: 1393 RVA: 0x00009C95 File Offset: 0x00007E95
			[DebuggerStepThrough]
			public static v128 vrev64q_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x00009C9C File Offset: 0x00007E9C
			[DebuggerStepThrough]
			public static v64 vrev64_u8(v64 a0)
			{
				return Arm.Neon.vrev64_s8(a0);
			}

			// Token: 0x06000573 RID: 1395 RVA: 0x00009CA4 File Offset: 0x00007EA4
			[DebuggerStepThrough]
			public static v128 vrev64q_u8(v128 a0)
			{
				return Arm.Neon.vrev64q_s8(a0);
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00009CAC File Offset: 0x00007EAC
			[DebuggerStepThrough]
			public static v64 vrev64_u16(v64 a0)
			{
				return Arm.Neon.vrev64_s16(a0);
			}

			// Token: 0x06000575 RID: 1397 RVA: 0x00009CB4 File Offset: 0x00007EB4
			[DebuggerStepThrough]
			public static v128 vrev64q_u16(v128 a0)
			{
				return Arm.Neon.vrev64q_s16(a0);
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x00009CBC File Offset: 0x00007EBC
			[DebuggerStepThrough]
			public static v64 vrev64_u32(v64 a0)
			{
				return Arm.Neon.vrev64_s32(a0);
			}

			// Token: 0x06000577 RID: 1399 RVA: 0x00009CC4 File Offset: 0x00007EC4
			[DebuggerStepThrough]
			public static v128 vrev64q_u32(v128 a0)
			{
				return Arm.Neon.vrev64q_s32(a0);
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x00009CCC File Offset: 0x00007ECC
			[DebuggerStepThrough]
			public static v64 vrev64_f32(v64 a0)
			{
				return Arm.Neon.vrev64_s32(a0);
			}

			// Token: 0x06000579 RID: 1401 RVA: 0x00009CD4 File Offset: 0x00007ED4
			[DebuggerStepThrough]
			public static v128 vrev64q_f32(v128 a0)
			{
				return Arm.Neon.vrev64q_s32(a0);
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x00009CDC File Offset: 0x00007EDC
			[DebuggerStepThrough]
			public static v64 vrev32_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600057B RID: 1403 RVA: 0x00009CE3 File Offset: 0x00007EE3
			[DebuggerStepThrough]
			public static v128 vrev32q_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x00009CEA File Offset: 0x00007EEA
			[DebuggerStepThrough]
			public static v64 vrev32_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600057D RID: 1405 RVA: 0x00009CF1 File Offset: 0x00007EF1
			[DebuggerStepThrough]
			public static v128 vrev32q_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x00009CF8 File Offset: 0x00007EF8
			[DebuggerStepThrough]
			public static v64 vrev32_u8(v64 a0)
			{
				return Arm.Neon.vrev32_s8(a0);
			}

			// Token: 0x0600057F RID: 1407 RVA: 0x00009D00 File Offset: 0x00007F00
			[DebuggerStepThrough]
			public static v128 vrev32q_u8(v128 a0)
			{
				return Arm.Neon.vrev32q_s8(a0);
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x00009D08 File Offset: 0x00007F08
			[DebuggerStepThrough]
			public static v64 vrev32_u16(v64 a0)
			{
				return Arm.Neon.vrev32_s16(a0);
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00009D10 File Offset: 0x00007F10
			[DebuggerStepThrough]
			public static v128 vrev32q_u16(v128 a0)
			{
				return Arm.Neon.vrev32q_s16(a0);
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00009D18 File Offset: 0x00007F18
			[DebuggerStepThrough]
			public static v64 vrev16_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x00009D1F File Offset: 0x00007F1F
			[DebuggerStepThrough]
			public static v128 vrev16q_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x00009D26 File Offset: 0x00007F26
			[DebuggerStepThrough]
			public static v64 vrev16_u8(v64 a0)
			{
				return Arm.Neon.vrev16_s8(a0);
			}

			// Token: 0x06000585 RID: 1413 RVA: 0x00009D2E File Offset: 0x00007F2E
			[DebuggerStepThrough]
			public static v128 vrev16q_u8(v128 a0)
			{
				return Arm.Neon.vrev16q_s8(a0);
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x00009D36 File Offset: 0x00007F36
			[DebuggerStepThrough]
			public static v64 vtbl1_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000587 RID: 1415 RVA: 0x00009D3D File Offset: 0x00007F3D
			[DebuggerStepThrough]
			public static v64 vtbl1_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vtbl1_s8(a0, a1);
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x00009D46 File Offset: 0x00007F46
			[DebuggerStepThrough]
			public static v64 vtbx1_s8(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000589 RID: 1417 RVA: 0x00009D4D File Offset: 0x00007F4D
			[DebuggerStepThrough]
			public static v64 vtbx1_u8(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vtbx1_s8(a0, a1, a2);
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x00009D57 File Offset: 0x00007F57
			[DebuggerStepThrough]
			public static byte vget_lane_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600058B RID: 1419 RVA: 0x00009D5E File Offset: 0x00007F5E
			[DebuggerStepThrough]
			public static ushort vget_lane_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x00009D65 File Offset: 0x00007F65
			[DebuggerStepThrough]
			public static uint vget_lane_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600058D RID: 1421 RVA: 0x00009D6C File Offset: 0x00007F6C
			[DebuggerStepThrough]
			public static ulong vget_lane_u64(v64 a0, int a1)
			{
				return a0.ULong0;
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x00009D74 File Offset: 0x00007F74
			[DebuggerStepThrough]
			public static sbyte vget_lane_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600058F RID: 1423 RVA: 0x00009D7B File Offset: 0x00007F7B
			[DebuggerStepThrough]
			public static short vget_lane_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x00009D82 File Offset: 0x00007F82
			[DebuggerStepThrough]
			public static int vget_lane_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000591 RID: 1425 RVA: 0x00009D89 File Offset: 0x00007F89
			[DebuggerStepThrough]
			public static long vget_lane_s64(v64 a0, int a1)
			{
				return a0.SLong0;
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x00009D91 File Offset: 0x00007F91
			[DebuggerStepThrough]
			public static float vget_lane_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000593 RID: 1427 RVA: 0x00009D98 File Offset: 0x00007F98
			[DebuggerStepThrough]
			public static byte vgetq_lane_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00009D9F File Offset: 0x00007F9F
			[DebuggerStepThrough]
			public static ushort vgetq_lane_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x00009DA6 File Offset: 0x00007FA6
			[DebuggerStepThrough]
			public static uint vgetq_lane_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x00009DAD File Offset: 0x00007FAD
			[DebuggerStepThrough]
			public static ulong vgetq_lane_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000597 RID: 1431 RVA: 0x00009DB4 File Offset: 0x00007FB4
			[DebuggerStepThrough]
			public static sbyte vgetq_lane_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x00009DBB File Offset: 0x00007FBB
			[DebuggerStepThrough]
			public static short vgetq_lane_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000599 RID: 1433 RVA: 0x00009DC2 File Offset: 0x00007FC2
			[DebuggerStepThrough]
			public static int vgetq_lane_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x00009DC9 File Offset: 0x00007FC9
			[DebuggerStepThrough]
			public static long vgetq_lane_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059B RID: 1435 RVA: 0x00009DD0 File Offset: 0x00007FD0
			[DebuggerStepThrough]
			public static float vgetq_lane_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x00009DD7 File Offset: 0x00007FD7
			[DebuggerStepThrough]
			public static v64 vset_lane_u8(byte a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059D RID: 1437 RVA: 0x00009DDE File Offset: 0x00007FDE
			[DebuggerStepThrough]
			public static v64 vset_lane_u16(ushort a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x00009DE5 File Offset: 0x00007FE5
			[DebuggerStepThrough]
			public static v64 vset_lane_u32(uint a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600059F RID: 1439 RVA: 0x00009DEC File Offset: 0x00007FEC
			[DebuggerStepThrough]
			public static v64 vset_lane_u64(ulong a0, v64 a1, int a2)
			{
				return new v64(a0);
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00009DF4 File Offset: 0x00007FF4
			[DebuggerStepThrough]
			public static v64 vset_lane_s8(sbyte a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00009DFB File Offset: 0x00007FFB
			[DebuggerStepThrough]
			public static v64 vset_lane_s16(short a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00009E02 File Offset: 0x00008002
			[DebuggerStepThrough]
			public static v64 vset_lane_s32(int a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A3 RID: 1443 RVA: 0x00009E09 File Offset: 0x00008009
			[DebuggerStepThrough]
			public static v64 vset_lane_s64(long a0, v64 a1, int a2)
			{
				return new v64(a0);
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x00009E11 File Offset: 0x00008011
			[DebuggerStepThrough]
			public static v64 vset_lane_f32(float a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00009E18 File Offset: 0x00008018
			[DebuggerStepThrough]
			public static v128 vsetq_lane_u8(byte a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x00009E1F File Offset: 0x0000801F
			[DebuggerStepThrough]
			public static v128 vsetq_lane_u16(ushort a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00009E26 File Offset: 0x00008026
			[DebuggerStepThrough]
			public static v128 vsetq_lane_u32(uint a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00009E2D File Offset: 0x0000802D
			[DebuggerStepThrough]
			public static v128 vsetq_lane_u64(ulong a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00009E34 File Offset: 0x00008034
			[DebuggerStepThrough]
			public static v128 vsetq_lane_s8(sbyte a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00009E3B File Offset: 0x0000803B
			[DebuggerStepThrough]
			public static v128 vsetq_lane_s16(short a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AB RID: 1451 RVA: 0x00009E42 File Offset: 0x00008042
			[DebuggerStepThrough]
			public static v128 vsetq_lane_s32(int a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00009E49 File Offset: 0x00008049
			[DebuggerStepThrough]
			public static v128 vsetq_lane_s64(long a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AD RID: 1453 RVA: 0x00009E50 File Offset: 0x00008050
			[DebuggerStepThrough]
			public static v128 vsetq_lane_f32(float a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x00009E57 File Offset: 0x00008057
			[DebuggerStepThrough]
			public static v64 vfma_n_f32(v64 a0, v64 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005AF RID: 1455 RVA: 0x00009E5E File Offset: 0x0000805E
			[DebuggerStepThrough]
			public static v128 vfmaq_n_f32(v128 a0, v128 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00009E65 File Offset: 0x00008065
			public static bool IsNeonArmv82FeaturesSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x00009E68 File Offset: 0x00008068
			[DebuggerStepThrough]
			public static v64 vadd_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x00009E6F File Offset: 0x0000806F
			[DebuggerStepThrough]
			public static v128 vaddq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x00009E76 File Offset: 0x00008076
			[DebuggerStepThrough]
			public static long vaddd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x00009E7D File Offset: 0x0000807D
			[DebuggerStepThrough]
			public static ulong vaddd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x00009E84 File Offset: 0x00008084
			[DebuggerStepThrough]
			public static v128 vaddl_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x00009E8B File Offset: 0x0000808B
			[DebuggerStepThrough]
			public static v128 vaddl_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B7 RID: 1463 RVA: 0x00009E92 File Offset: 0x00008092
			[DebuggerStepThrough]
			public static v128 vaddl_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x00009E99 File Offset: 0x00008099
			[DebuggerStepThrough]
			public static v128 vaddl_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005B9 RID: 1465 RVA: 0x00009EA0 File Offset: 0x000080A0
			[DebuggerStepThrough]
			public static v128 vaddl_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x00009EA7 File Offset: 0x000080A7
			[DebuggerStepThrough]
			public static v128 vaddl_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x00009EAE File Offset: 0x000080AE
			[DebuggerStepThrough]
			public static v128 vaddw_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x00009EB5 File Offset: 0x000080B5
			[DebuggerStepThrough]
			public static v128 vaddw_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x00009EBC File Offset: 0x000080BC
			[DebuggerStepThrough]
			public static v128 vaddw_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x00009EC3 File Offset: 0x000080C3
			[DebuggerStepThrough]
			public static v128 vaddw_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x00009ECA File Offset: 0x000080CA
			[DebuggerStepThrough]
			public static v128 vaddw_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x00009ED1 File Offset: 0x000080D1
			[DebuggerStepThrough]
			public static v128 vaddw_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x00009ED8 File Offset: 0x000080D8
			[DebuggerStepThrough]
			public static sbyte vqaddb_s8(sbyte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x00009EDF File Offset: 0x000080DF
			[DebuggerStepThrough]
			public static short vqaddh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C3 RID: 1475 RVA: 0x00009EE6 File Offset: 0x000080E6
			[DebuggerStepThrough]
			public static int vqadds_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x00009EED File Offset: 0x000080ED
			[DebuggerStepThrough]
			public static long vqaddd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C5 RID: 1477 RVA: 0x00009EF4 File Offset: 0x000080F4
			[DebuggerStepThrough]
			public static byte vqaddb_u8(byte a0, byte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x00009EFB File Offset: 0x000080FB
			[DebuggerStepThrough]
			public static ushort vqaddh_u16(ushort a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C7 RID: 1479 RVA: 0x00009F02 File Offset: 0x00008102
			[DebuggerStepThrough]
			public static uint vqadds_u32(uint a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00009F09 File Offset: 0x00008109
			[DebuggerStepThrough]
			public static ulong vqaddd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005C9 RID: 1481 RVA: 0x00009F10 File Offset: 0x00008110
			[DebuggerStepThrough]
			public static v64 vuqadd_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x00009F17 File Offset: 0x00008117
			[DebuggerStepThrough]
			public static v128 vuqaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CB RID: 1483 RVA: 0x00009F1E File Offset: 0x0000811E
			[DebuggerStepThrough]
			public static v64 vuqadd_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x00009F25 File Offset: 0x00008125
			[DebuggerStepThrough]
			public static v128 vuqaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CD RID: 1485 RVA: 0x00009F2C File Offset: 0x0000812C
			[DebuggerStepThrough]
			public static v64 vuqadd_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x00009F33 File Offset: 0x00008133
			[DebuggerStepThrough]
			public static v128 vuqaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005CF RID: 1487 RVA: 0x00009F3A File Offset: 0x0000813A
			[DebuggerStepThrough]
			public static v64 vuqadd_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00009F41 File Offset: 0x00008141
			[DebuggerStepThrough]
			public static v128 vuqaddq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D1 RID: 1489 RVA: 0x00009F48 File Offset: 0x00008148
			[DebuggerStepThrough]
			public static sbyte vuqaddb_s8(sbyte a0, byte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x00009F4F File Offset: 0x0000814F
			[DebuggerStepThrough]
			public static short vuqaddh_s16(short a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x00009F56 File Offset: 0x00008156
			[DebuggerStepThrough]
			public static int vuqadds_s32(int a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x00009F5D File Offset: 0x0000815D
			[DebuggerStepThrough]
			public static long vuqaddd_s64(long a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D5 RID: 1493 RVA: 0x00009F64 File Offset: 0x00008164
			[DebuggerStepThrough]
			public static v64 vsqadd_u8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x00009F6B File Offset: 0x0000816B
			[DebuggerStepThrough]
			public static v128 vsqaddq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D7 RID: 1495 RVA: 0x00009F72 File Offset: 0x00008172
			[DebuggerStepThrough]
			public static v64 vsqadd_u16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x00009F79 File Offset: 0x00008179
			[DebuggerStepThrough]
			public static v128 vsqaddq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x00009F80 File Offset: 0x00008180
			[DebuggerStepThrough]
			public static v64 vsqadd_u32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00009F87 File Offset: 0x00008187
			[DebuggerStepThrough]
			public static v128 vsqaddq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DB RID: 1499 RVA: 0x00009F8E File Offset: 0x0000818E
			[DebuggerStepThrough]
			public static v64 vsqadd_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x00009F95 File Offset: 0x00008195
			[DebuggerStepThrough]
			public static v128 vsqaddq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DD RID: 1501 RVA: 0x00009F9C File Offset: 0x0000819C
			[DebuggerStepThrough]
			public static byte vsqaddb_u8(byte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x00009FA3 File Offset: 0x000081A3
			[DebuggerStepThrough]
			public static ushort vsqaddh_u16(ushort a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x00009FAA File Offset: 0x000081AA
			[DebuggerStepThrough]
			public static uint vsqadds_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x00009FB1 File Offset: 0x000081B1
			[DebuggerStepThrough]
			public static ulong vsqaddd_u64(ulong a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E1 RID: 1505 RVA: 0x00009FB8 File Offset: 0x000081B8
			[DebuggerStepThrough]
			public static v128 vaddhn_high_s16(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x00009FBF File Offset: 0x000081BF
			[DebuggerStepThrough]
			public static v128 vaddhn_high_s32(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E3 RID: 1507 RVA: 0x00009FC6 File Offset: 0x000081C6
			[DebuggerStepThrough]
			public static v128 vaddhn_high_s64(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x00009FCD File Offset: 0x000081CD
			[DebuggerStepThrough]
			public static v128 vaddhn_high_u16(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vaddhn_high_s16(a0, a1, a2);
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x00009FD7 File Offset: 0x000081D7
			[DebuggerStepThrough]
			public static v128 vaddhn_high_u32(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vaddhn_high_s32(a0, a1, a2);
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x00009FE1 File Offset: 0x000081E1
			[DebuggerStepThrough]
			public static v128 vaddhn_high_u64(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vaddhn_high_s64(a0, a1, a2);
			}

			// Token: 0x060005E7 RID: 1511 RVA: 0x00009FEB File Offset: 0x000081EB
			[DebuggerStepThrough]
			public static v128 vraddhn_high_s16(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x00009FF2 File Offset: 0x000081F2
			[DebuggerStepThrough]
			public static v128 vraddhn_high_s32(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005E9 RID: 1513 RVA: 0x00009FF9 File Offset: 0x000081F9
			[DebuggerStepThrough]
			public static v128 vraddhn_high_s64(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x0000A000 File Offset: 0x00008200
			[DebuggerStepThrough]
			public static v128 vraddhn_high_u16(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vraddhn_high_s16(a0, a1, a2);
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x0000A00A File Offset: 0x0000820A
			[DebuggerStepThrough]
			public static v128 vraddhn_high_u32(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vraddhn_high_s32(a0, a1, a2);
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x0000A014 File Offset: 0x00008214
			[DebuggerStepThrough]
			public static v128 vraddhn_high_u64(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vraddhn_high_s64(a0, a1, a2);
			}

			// Token: 0x060005ED RID: 1517 RVA: 0x0000A01E File Offset: 0x0000821E
			[DebuggerStepThrough]
			public static v64 vmul_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x0000A025 File Offset: 0x00008225
			[DebuggerStepThrough]
			public static v128 vmulq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005EF RID: 1519 RVA: 0x0000A02C File Offset: 0x0000822C
			[DebuggerStepThrough]
			public static v64 vmulx_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0000A033 File Offset: 0x00008233
			[DebuggerStepThrough]
			public static v128 vmulxq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F1 RID: 1521 RVA: 0x0000A03A File Offset: 0x0000823A
			[DebuggerStepThrough]
			public static v64 vmulx_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x0000A041 File Offset: 0x00008241
			[DebuggerStepThrough]
			public static v128 vmulxq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x0000A048 File Offset: 0x00008248
			[DebuggerStepThrough]
			public static float vmulxs_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0000A04F File Offset: 0x0000824F
			[DebuggerStepThrough]
			public static double vmulxd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x0000A056 File Offset: 0x00008256
			[DebuggerStepThrough]
			public static v64 vmulx_lane_f32(v64 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x0000A05D File Offset: 0x0000825D
			[DebuggerStepThrough]
			public static v128 vmulxq_lane_f32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x0000A064 File Offset: 0x00008264
			[DebuggerStepThrough]
			public static v64 vmulx_lane_f64(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vmulx_f64(a0, a1);
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x0000A06D File Offset: 0x0000826D
			[DebuggerStepThrough]
			public static v128 vmulxq_lane_f64(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005F9 RID: 1529 RVA: 0x0000A074 File Offset: 0x00008274
			[DebuggerStepThrough]
			public static float vmulxs_lane_f32(float a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x0000A07B File Offset: 0x0000827B
			[DebuggerStepThrough]
			public static double vmulxd_lane_f64(double a0, v64 a1, int a2)
			{
				return Arm.Neon.vmulxd_f64(a0, a1.Double0);
			}

			// Token: 0x060005FB RID: 1531 RVA: 0x0000A089 File Offset: 0x00008289
			[DebuggerStepThrough]
			public static v64 vmulx_laneq_f32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x0000A090 File Offset: 0x00008290
			[DebuggerStepThrough]
			public static v128 vmulxq_laneq_f32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005FD RID: 1533 RVA: 0x0000A097 File Offset: 0x00008297
			[DebuggerStepThrough]
			public static v64 vmulx_laneq_f64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x0000A09E File Offset: 0x0000829E
			[DebuggerStepThrough]
			public static v128 vmulxq_laneq_f64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060005FF RID: 1535 RVA: 0x0000A0A5 File Offset: 0x000082A5
			[DebuggerStepThrough]
			public static float vmulxs_laneq_f32(float a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x0000A0AC File Offset: 0x000082AC
			[DebuggerStepThrough]
			public static double vmulxd_laneq_f64(double a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000601 RID: 1537 RVA: 0x0000A0B3 File Offset: 0x000082B3
			[DebuggerStepThrough]
			public static v64 vdiv_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x0000A0BA File Offset: 0x000082BA
			[DebuggerStepThrough]
			public static v128 vdivq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000603 RID: 1539 RVA: 0x0000A0C1 File Offset: 0x000082C1
			[DebuggerStepThrough]
			public static v64 vdiv_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x0000A0C8 File Offset: 0x000082C8
			[DebuggerStepThrough]
			public static v128 vdivq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x0000A0CF File Offset: 0x000082CF
			[DebuggerStepThrough]
			public static v64 vmla_f64(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x0000A0D6 File Offset: 0x000082D6
			[DebuggerStepThrough]
			public static v128 vmlaq_f64(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x0000A0DD File Offset: 0x000082DD
			[DebuggerStepThrough]
			public static v128 vmlal_high_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x0000A0E4 File Offset: 0x000082E4
			[DebuggerStepThrough]
			public static v128 vmlal_high_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x0000A0EB File Offset: 0x000082EB
			[DebuggerStepThrough]
			public static v128 vmlal_high_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x0000A0F2 File Offset: 0x000082F2
			[DebuggerStepThrough]
			public static v128 vmlal_high_u8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x0000A0F9 File Offset: 0x000082F9
			[DebuggerStepThrough]
			public static v128 vmlal_high_u16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0000A100 File Offset: 0x00008300
			[DebuggerStepThrough]
			public static v128 vmlal_high_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0000A107 File Offset: 0x00008307
			[DebuggerStepThrough]
			public static v64 vmls_f64(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x0000A10E File Offset: 0x0000830E
			[DebuggerStepThrough]
			public static v128 vmlsq_f64(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0000A115 File Offset: 0x00008315
			[DebuggerStepThrough]
			public static v128 vmlsl_high_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0000A11C File Offset: 0x0000831C
			[DebuggerStepThrough]
			public static v128 vmlsl_high_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x0000A123 File Offset: 0x00008323
			[DebuggerStepThrough]
			public static v128 vmlsl_high_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x0000A12A File Offset: 0x0000832A
			[DebuggerStepThrough]
			public static v128 vmlsl_high_u8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000613 RID: 1555 RVA: 0x0000A131 File Offset: 0x00008331
			[DebuggerStepThrough]
			public static v128 vmlsl_high_u16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x0000A138 File Offset: 0x00008338
			[DebuggerStepThrough]
			public static v128 vmlsl_high_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0000A13F File Offset: 0x0000833F
			[DebuggerStepThrough]
			public static v64 vfma_f64(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x0000A146 File Offset: 0x00008346
			[DebuggerStepThrough]
			public static v128 vfmaq_f64(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x0000A14D File Offset: 0x0000834D
			[DebuggerStepThrough]
			public static v64 vfma_lane_f32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x0000A154 File Offset: 0x00008354
			[DebuggerStepThrough]
			public static v128 vfmaq_lane_f32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x0000A15B File Offset: 0x0000835B
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV8A_AARCH64)]
			public static v64 vfma_lane_f64(v64 a0, v64 a1, v64 a2, int a3)
			{
				return Arm.Neon.vfma_f64(a0, a1, a2);
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0000A165 File Offset: 0x00008365
			[DebuggerStepThrough]
			public static v128 vfmaq_lane_f64(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x0000A16C File Offset: 0x0000836C
			[DebuggerStepThrough]
			public static float vfmas_lane_f32(float a0, float a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0000A173 File Offset: 0x00008373
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV8A_AARCH64)]
			public static double vfmad_lane_f64(double a0, double a1, v64 a2, int a3)
			{
				return Arm.Neon.vfma_f64(new v64(a0), new v64(a1), a2).Double0;
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0000A18C File Offset: 0x0000838C
			[DebuggerStepThrough]
			public static v64 vfma_laneq_f32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x0000A193 File Offset: 0x00008393
			[DebuggerStepThrough]
			public static v128 vfmaq_laneq_f32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x0000A19A File Offset: 0x0000839A
			[DebuggerStepThrough]
			public static v64 vfma_laneq_f64(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x0000A1A1 File Offset: 0x000083A1
			[DebuggerStepThrough]
			public static v128 vfmaq_laneq_f64(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x0000A1A8 File Offset: 0x000083A8
			[DebuggerStepThrough]
			public static float vfmas_laneq_f32(float a0, float a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x0000A1AF File Offset: 0x000083AF
			[DebuggerStepThrough]
			public static double vfmad_laneq_f64(double a0, double a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0000A1B6 File Offset: 0x000083B6
			[DebuggerStepThrough]
			public static v64 vfms_f64(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0000A1BD File Offset: 0x000083BD
			[DebuggerStepThrough]
			public static v128 vfmsq_f64(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x0000A1C4 File Offset: 0x000083C4
			[DebuggerStepThrough]
			public static v64 vfms_lane_f32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0000A1CB File Offset: 0x000083CB
			[DebuggerStepThrough]
			public static v128 vfmsq_lane_f32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0000A1D2 File Offset: 0x000083D2
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV8A_AARCH64)]
			public static v64 vfms_lane_f64(v64 a0, v64 a1, v64 a2, int a3)
			{
				return Arm.Neon.vfms_f64(a0, a1, a2);
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x0000A1DC File Offset: 0x000083DC
			[DebuggerStepThrough]
			public static v128 vfmsq_lane_f64(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000629 RID: 1577 RVA: 0x0000A1E3 File Offset: 0x000083E3
			[DebuggerStepThrough]
			public static float vfmss_lane_f32(float a0, float a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x0000A1EA File Offset: 0x000083EA
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV8A_AARCH64)]
			public static double vfmsd_lane_f64(double a0, double a1, v64 a2, int a3)
			{
				return Arm.Neon.vfms_f64(new v64(a0), new v64(a1), a2).Double0;
			}

			// Token: 0x0600062B RID: 1579 RVA: 0x0000A203 File Offset: 0x00008403
			[DebuggerStepThrough]
			public static v64 vfms_laneq_f32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x0000A20A File Offset: 0x0000840A
			[DebuggerStepThrough]
			public static v128 vfmsq_laneq_f32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x0000A211 File Offset: 0x00008411
			[DebuggerStepThrough]
			public static v64 vfms_laneq_f64(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x0000A218 File Offset: 0x00008418
			[DebuggerStepThrough]
			public static v128 vfmsq_laneq_f64(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x0000A21F File Offset: 0x0000841F
			[DebuggerStepThrough]
			public static float vfmss_laneq_f32(float a0, float a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x0000A226 File Offset: 0x00008426
			[DebuggerStepThrough]
			public static double vfmsd_laneq_f64(double a0, double a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x0000A22D File Offset: 0x0000842D
			[DebuggerStepThrough]
			public static short vqdmulhh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x0000A234 File Offset: 0x00008434
			[DebuggerStepThrough]
			public static int vqdmulhs_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x0000A23B File Offset: 0x0000843B
			[DebuggerStepThrough]
			public static short vqrdmulhh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x0000A242 File Offset: 0x00008442
			[DebuggerStepThrough]
			public static int vqrdmulhs_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000635 RID: 1589 RVA: 0x0000A249 File Offset: 0x00008449
			[DebuggerStepThrough]
			public static int vqdmlalh_s16(int a0, short a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000636 RID: 1590 RVA: 0x0000A250 File Offset: 0x00008450
			[DebuggerStepThrough]
			public static long vqdmlals_s32(long a0, int a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x0000A257 File Offset: 0x00008457
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x0000A25E File Offset: 0x0000845E
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0000A265 File Offset: 0x00008465
			[DebuggerStepThrough]
			public static int vqdmlslh_s16(int a0, short a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x0000A26C File Offset: 0x0000846C
			[DebuggerStepThrough]
			public static long vqdmlsls_s32(long a0, int a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x0000A273 File Offset: 0x00008473
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x0000A27A File Offset: 0x0000847A
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063D RID: 1597 RVA: 0x0000A281 File Offset: 0x00008481
			[DebuggerStepThrough]
			public static v128 vmull_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x0000A288 File Offset: 0x00008488
			[DebuggerStepThrough]
			public static v128 vmull_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600063F RID: 1599 RVA: 0x0000A28F File Offset: 0x0000848F
			[DebuggerStepThrough]
			public static v128 vmull_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x0000A296 File Offset: 0x00008496
			[DebuggerStepThrough]
			public static v128 vmull_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x0000A29D File Offset: 0x0000849D
			[DebuggerStepThrough]
			public static v128 vmull_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000642 RID: 1602 RVA: 0x0000A2A4 File Offset: 0x000084A4
			[DebuggerStepThrough]
			public static v128 vmull_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000643 RID: 1603 RVA: 0x0000A2AB File Offset: 0x000084AB
			[DebuggerStepThrough]
			public static int vqdmullh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000644 RID: 1604 RVA: 0x0000A2B2 File Offset: 0x000084B2
			[DebuggerStepThrough]
			public static long vqdmulls_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x0000A2B9 File Offset: 0x000084B9
			[DebuggerStepThrough]
			public static v128 vqdmull_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x0000A2C0 File Offset: 0x000084C0
			[DebuggerStepThrough]
			public static v128 vqdmull_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000647 RID: 1607 RVA: 0x0000A2C7 File Offset: 0x000084C7
			[DebuggerStepThrough]
			public static v64 vsub_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x0000A2CE File Offset: 0x000084CE
			[DebuggerStepThrough]
			public static v128 vsubq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x0000A2D5 File Offset: 0x000084D5
			[DebuggerStepThrough]
			public static long vsubd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x0000A2DC File Offset: 0x000084DC
			[DebuggerStepThrough]
			public static ulong vsubd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064B RID: 1611 RVA: 0x0000A2E3 File Offset: 0x000084E3
			[DebuggerStepThrough]
			public static v128 vsubl_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x0000A2EA File Offset: 0x000084EA
			[DebuggerStepThrough]
			public static v128 vsubl_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x0000A2F1 File Offset: 0x000084F1
			[DebuggerStepThrough]
			public static v128 vsubl_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x0000A2F8 File Offset: 0x000084F8
			[DebuggerStepThrough]
			public static v128 vsubl_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x0000A2FF File Offset: 0x000084FF
			[DebuggerStepThrough]
			public static v128 vsubl_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0000A306 File Offset: 0x00008506
			[DebuggerStepThrough]
			public static v128 vsubl_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0000A30D File Offset: 0x0000850D
			[DebuggerStepThrough]
			public static v128 vsubw_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0000A314 File Offset: 0x00008514
			[DebuggerStepThrough]
			public static v128 vsubw_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x0000A31B File Offset: 0x0000851B
			[DebuggerStepThrough]
			public static v128 vsubw_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x0000A322 File Offset: 0x00008522
			[DebuggerStepThrough]
			public static v128 vsubw_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x0000A329 File Offset: 0x00008529
			[DebuggerStepThrough]
			public static v128 vsubw_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x0000A330 File Offset: 0x00008530
			[DebuggerStepThrough]
			public static v128 vsubw_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x0000A337 File Offset: 0x00008537
			[DebuggerStepThrough]
			public static sbyte vqsubb_s8(sbyte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x0000A33E File Offset: 0x0000853E
			[DebuggerStepThrough]
			public static short vqsubh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x0000A345 File Offset: 0x00008545
			[DebuggerStepThrough]
			public static int vqsubs_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x0000A34C File Offset: 0x0000854C
			[DebuggerStepThrough]
			public static long vqsubd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x0000A353 File Offset: 0x00008553
			[DebuggerStepThrough]
			public static byte vqsubb_u8(byte a0, byte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x0000A35A File Offset: 0x0000855A
			[DebuggerStepThrough]
			public static ushort vqsubh_u16(ushort a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x0000A361 File Offset: 0x00008561
			[DebuggerStepThrough]
			public static uint vqsubs_u32(uint a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x0000A368 File Offset: 0x00008568
			[DebuggerStepThrough]
			public static ulong vqsubd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x0000A36F File Offset: 0x0000856F
			[DebuggerStepThrough]
			public static v128 vsubhn_high_s16(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x0000A376 File Offset: 0x00008576
			[DebuggerStepThrough]
			public static v128 vsubhn_high_s32(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x0000A37D File Offset: 0x0000857D
			[DebuggerStepThrough]
			public static v128 vsubhn_high_s64(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x0000A384 File Offset: 0x00008584
			[DebuggerStepThrough]
			public static v128 vsubhn_high_u16(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vsubhn_high_s16(a0, a1, a2);
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x0000A38E File Offset: 0x0000858E
			[DebuggerStepThrough]
			public static v128 vsubhn_high_u32(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vsubhn_high_s32(a0, a1, a2);
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x0000A398 File Offset: 0x00008598
			[DebuggerStepThrough]
			public static v128 vsubhn_high_u64(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vsubhn_high_s64(a0, a1, a2);
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0000A3A2 File Offset: 0x000085A2
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_s16(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x0000A3A9 File Offset: 0x000085A9
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_s32(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0000A3B0 File Offset: 0x000085B0
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_s64(v64 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0000A3B7 File Offset: 0x000085B7
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_u16(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vrsubhn_high_s16(a0, a1, a2);
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x0000A3C1 File Offset: 0x000085C1
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_u32(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vrsubhn_high_s32(a0, a1, a2);
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0000A3CB File Offset: 0x000085CB
			[DebuggerStepThrough]
			public static v128 vrsubhn_high_u64(v64 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vrsubhn_high_s64(a0, a1, a2);
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x0000A3D5 File Offset: 0x000085D5
			[DebuggerStepThrough]
			public static v64 vceq_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x0000A3DC File Offset: 0x000085DC
			[DebuggerStepThrough]
			public static v128 vceqq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x0000A3E3 File Offset: 0x000085E3
			[DebuggerStepThrough]
			public static v64 vceq_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vceq_s64(a0, a1);
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x0000A3EC File Offset: 0x000085EC
			[DebuggerStepThrough]
			public static v128 vceqq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vceqq_s64(a0, a1);
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x0000A3F5 File Offset: 0x000085F5
			[DebuggerStepThrough]
			public static v64 vceq_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x0000A3FC File Offset: 0x000085FC
			[DebuggerStepThrough]
			public static v128 vceqq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x0000A403 File Offset: 0x00008603
			[DebuggerStepThrough]
			public static ulong vceqd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x0000A40A File Offset: 0x0000860A
			[DebuggerStepThrough]
			public static ulong vceqd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0000A411 File Offset: 0x00008611
			[DebuggerStepThrough]
			public static uint vceqs_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0000A418 File Offset: 0x00008618
			[DebuggerStepThrough]
			public static ulong vceqd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x0000A41F File Offset: 0x0000861F
			[DebuggerStepThrough]
			public static v64 vceqz_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0000A426 File Offset: 0x00008626
			[DebuggerStepThrough]
			public static v128 vceqzq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0000A42D File Offset: 0x0000862D
			[DebuggerStepThrough]
			public static v64 vceqz_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0000A434 File Offset: 0x00008634
			[DebuggerStepThrough]
			public static v128 vceqzq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x0000A43B File Offset: 0x0000863B
			[DebuggerStepThrough]
			public static v64 vceqz_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x0000A442 File Offset: 0x00008642
			[DebuggerStepThrough]
			public static v128 vceqzq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x0000A449 File Offset: 0x00008649
			[DebuggerStepThrough]
			public static v64 vceqz_u8(v64 a0)
			{
				return Arm.Neon.vceqz_s8(a0);
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x0000A451 File Offset: 0x00008651
			[DebuggerStepThrough]
			public static v128 vceqzq_u8(v128 a0)
			{
				return Arm.Neon.vceqzq_s8(a0);
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x0000A459 File Offset: 0x00008659
			[DebuggerStepThrough]
			public static v64 vceqz_u16(v64 a0)
			{
				return Arm.Neon.vceqz_s16(a0);
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x0000A461 File Offset: 0x00008661
			[DebuggerStepThrough]
			public static v128 vceqzq_u16(v128 a0)
			{
				return Arm.Neon.vceqzq_s16(a0);
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0000A469 File Offset: 0x00008669
			[DebuggerStepThrough]
			public static v64 vceqz_u32(v64 a0)
			{
				return Arm.Neon.vceqz_s32(a0);
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x0000A471 File Offset: 0x00008671
			[DebuggerStepThrough]
			public static v128 vceqzq_u32(v128 a0)
			{
				return Arm.Neon.vceqzq_s32(a0);
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0000A479 File Offset: 0x00008679
			[DebuggerStepThrough]
			public static v64 vceqz_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0000A480 File Offset: 0x00008680
			[DebuggerStepThrough]
			public static v128 vceqzq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0000A487 File Offset: 0x00008687
			[DebuggerStepThrough]
			public static v64 vceqz_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0000A48E File Offset: 0x0000868E
			[DebuggerStepThrough]
			public static v128 vceqzq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0000A495 File Offset: 0x00008695
			[DebuggerStepThrough]
			public static v64 vceqz_u64(v64 a0)
			{
				return Arm.Neon.vceqz_s64(a0);
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x0000A49D File Offset: 0x0000869D
			[DebuggerStepThrough]
			public static v128 vceqzq_u64(v128 a0)
			{
				return Arm.Neon.vceqzq_s64(a0);
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x0000A4A5 File Offset: 0x000086A5
			[DebuggerStepThrough]
			public static v64 vceqz_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x0000A4AC File Offset: 0x000086AC
			[DebuggerStepThrough]
			public static v128 vceqzq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x0000A4B3 File Offset: 0x000086B3
			[DebuggerStepThrough]
			public static ulong vceqzd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0000A4BA File Offset: 0x000086BA
			[DebuggerStepThrough]
			public static ulong vceqzd_u64(ulong a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x0000A4C1 File Offset: 0x000086C1
			[DebuggerStepThrough]
			public static uint vceqzs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0000A4C8 File Offset: 0x000086C8
			[DebuggerStepThrough]
			public static ulong vceqzd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x0000A4CF File Offset: 0x000086CF
			[DebuggerStepThrough]
			public static v64 vcge_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0000A4D6 File Offset: 0x000086D6
			[DebuggerStepThrough]
			public static v128 vcgeq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600068F RID: 1679 RVA: 0x0000A4DD File Offset: 0x000086DD
			[DebuggerStepThrough]
			public static v64 vcge_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x0000A4E4 File Offset: 0x000086E4
			[DebuggerStepThrough]
			public static v128 vcgeq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x0000A4EB File Offset: 0x000086EB
			[DebuggerStepThrough]
			public static v64 vcge_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x0000A4F2 File Offset: 0x000086F2
			[DebuggerStepThrough]
			public static v128 vcgeq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0000A4F9 File Offset: 0x000086F9
			[DebuggerStepThrough]
			public static ulong vcged_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x0000A500 File Offset: 0x00008700
			[DebuggerStepThrough]
			public static ulong vcged_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x0000A507 File Offset: 0x00008707
			[DebuggerStepThrough]
			public static uint vcges_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x0000A50E File Offset: 0x0000870E
			[DebuggerStepThrough]
			public static ulong vcged_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0000A515 File Offset: 0x00008715
			[DebuggerStepThrough]
			public static v64 vcgez_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0000A51C File Offset: 0x0000871C
			[DebuggerStepThrough]
			public static v128 vcgezq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x0000A523 File Offset: 0x00008723
			[DebuggerStepThrough]
			public static v64 vcgez_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0000A52A File Offset: 0x0000872A
			[DebuggerStepThrough]
			public static v128 vcgezq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0000A531 File Offset: 0x00008731
			[DebuggerStepThrough]
			public static v64 vcgez_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x0000A538 File Offset: 0x00008738
			[DebuggerStepThrough]
			public static v128 vcgezq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x0000A53F File Offset: 0x0000873F
			[DebuggerStepThrough]
			public static v64 vcgez_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x0000A546 File Offset: 0x00008746
			[DebuggerStepThrough]
			public static v128 vcgezq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600069F RID: 1695 RVA: 0x0000A54D File Offset: 0x0000874D
			[DebuggerStepThrough]
			public static v64 vcgez_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x0000A554 File Offset: 0x00008754
			[DebuggerStepThrough]
			public static v128 vcgezq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A1 RID: 1697 RVA: 0x0000A55B File Offset: 0x0000875B
			[DebuggerStepThrough]
			public static v64 vcgez_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x0000A562 File Offset: 0x00008762
			[DebuggerStepThrough]
			public static v128 vcgezq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A3 RID: 1699 RVA: 0x0000A569 File Offset: 0x00008769
			[DebuggerStepThrough]
			public static ulong vcgezd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0000A570 File Offset: 0x00008770
			[DebuggerStepThrough]
			public static uint vcgezs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x0000A577 File Offset: 0x00008777
			[DebuggerStepThrough]
			public static ulong vcgezd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x0000A57E File Offset: 0x0000877E
			[DebuggerStepThrough]
			public static v64 vcle_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0000A585 File Offset: 0x00008785
			[DebuggerStepThrough]
			public static v128 vcleq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0000A58C File Offset: 0x0000878C
			[DebuggerStepThrough]
			public static v64 vcle_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0000A593 File Offset: 0x00008793
			[DebuggerStepThrough]
			public static v128 vcleq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x0000A59A File Offset: 0x0000879A
			[DebuggerStepThrough]
			public static v64 vcle_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0000A5A1 File Offset: 0x000087A1
			[DebuggerStepThrough]
			public static v128 vcleq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0000A5A8 File Offset: 0x000087A8
			[DebuggerStepThrough]
			public static ulong vcled_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x0000A5AF File Offset: 0x000087AF
			[DebuggerStepThrough]
			public static ulong vcled_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x0000A5B6 File Offset: 0x000087B6
			[DebuggerStepThrough]
			public static uint vcles_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0000A5BD File Offset: 0x000087BD
			[DebuggerStepThrough]
			public static ulong vcled_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x0000A5C4 File Offset: 0x000087C4
			[DebuggerStepThrough]
			public static v64 vclez_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B1 RID: 1713 RVA: 0x0000A5CB File Offset: 0x000087CB
			[DebuggerStepThrough]
			public static v128 vclezq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x0000A5D2 File Offset: 0x000087D2
			[DebuggerStepThrough]
			public static v64 vclez_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0000A5D9 File Offset: 0x000087D9
			[DebuggerStepThrough]
			public static v128 vclezq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x0000A5E0 File Offset: 0x000087E0
			[DebuggerStepThrough]
			public static v64 vclez_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x0000A5E7 File Offset: 0x000087E7
			[DebuggerStepThrough]
			public static v128 vclezq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x0000A5EE File Offset: 0x000087EE
			[DebuggerStepThrough]
			public static v64 vclez_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x0000A5F5 File Offset: 0x000087F5
			[DebuggerStepThrough]
			public static v128 vclezq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x0000A5FC File Offset: 0x000087FC
			[DebuggerStepThrough]
			public static v64 vclez_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x0000A603 File Offset: 0x00008803
			[DebuggerStepThrough]
			public static v128 vclezq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x0000A60A File Offset: 0x0000880A
			[DebuggerStepThrough]
			public static v64 vclez_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BB RID: 1723 RVA: 0x0000A611 File Offset: 0x00008811
			[DebuggerStepThrough]
			public static v128 vclezq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x0000A618 File Offset: 0x00008818
			[DebuggerStepThrough]
			public static ulong vclezd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BD RID: 1725 RVA: 0x0000A61F File Offset: 0x0000881F
			[DebuggerStepThrough]
			public static uint vclezs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x0000A626 File Offset: 0x00008826
			[DebuggerStepThrough]
			public static ulong vclezd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006BF RID: 1727 RVA: 0x0000A62D File Offset: 0x0000882D
			[DebuggerStepThrough]
			public static v64 vcgt_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x0000A634 File Offset: 0x00008834
			[DebuggerStepThrough]
			public static v128 vcgtq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C1 RID: 1729 RVA: 0x0000A63B File Offset: 0x0000883B
			[DebuggerStepThrough]
			public static v64 vcgt_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x0000A642 File Offset: 0x00008842
			[DebuggerStepThrough]
			public static v128 vcgtq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C3 RID: 1731 RVA: 0x0000A649 File Offset: 0x00008849
			[DebuggerStepThrough]
			public static v64 vcgt_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x0000A650 File Offset: 0x00008850
			[DebuggerStepThrough]
			public static v128 vcgtq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C5 RID: 1733 RVA: 0x0000A657 File Offset: 0x00008857
			[DebuggerStepThrough]
			public static ulong vcgtd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x0000A65E File Offset: 0x0000885E
			[DebuggerStepThrough]
			public static ulong vcgtd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C7 RID: 1735 RVA: 0x0000A665 File Offset: 0x00008865
			[DebuggerStepThrough]
			public static uint vcgts_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x0000A66C File Offset: 0x0000886C
			[DebuggerStepThrough]
			public static ulong vcgtd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006C9 RID: 1737 RVA: 0x0000A673 File Offset: 0x00008873
			[DebuggerStepThrough]
			public static v64 vcgtz_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x0000A67A File Offset: 0x0000887A
			[DebuggerStepThrough]
			public static v128 vcgtzq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CB RID: 1739 RVA: 0x0000A681 File Offset: 0x00008881
			[DebuggerStepThrough]
			public static v64 vcgtz_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x0000A688 File Offset: 0x00008888
			[DebuggerStepThrough]
			public static v128 vcgtzq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CD RID: 1741 RVA: 0x0000A68F File Offset: 0x0000888F
			[DebuggerStepThrough]
			public static v64 vcgtz_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x0000A696 File Offset: 0x00008896
			[DebuggerStepThrough]
			public static v128 vcgtzq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006CF RID: 1743 RVA: 0x0000A69D File Offset: 0x0000889D
			[DebuggerStepThrough]
			public static v64 vcgtz_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x0000A6A4 File Offset: 0x000088A4
			[DebuggerStepThrough]
			public static v128 vcgtzq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D1 RID: 1745 RVA: 0x0000A6AB File Offset: 0x000088AB
			[DebuggerStepThrough]
			public static v64 vcgtz_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0000A6B2 File Offset: 0x000088B2
			[DebuggerStepThrough]
			public static v128 vcgtzq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D3 RID: 1747 RVA: 0x0000A6B9 File Offset: 0x000088B9
			[DebuggerStepThrough]
			public static v64 vcgtz_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x0000A6C0 File Offset: 0x000088C0
			[DebuggerStepThrough]
			public static v128 vcgtzq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D5 RID: 1749 RVA: 0x0000A6C7 File Offset: 0x000088C7
			[DebuggerStepThrough]
			public static ulong vcgtzd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x0000A6CE File Offset: 0x000088CE
			[DebuggerStepThrough]
			public static uint vcgtzs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x0000A6D5 File Offset: 0x000088D5
			[DebuggerStepThrough]
			public static ulong vcgtzd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0000A6DC File Offset: 0x000088DC
			[DebuggerStepThrough]
			public static v64 vclt_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x0000A6E3 File Offset: 0x000088E3
			[DebuggerStepThrough]
			public static v128 vcltq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0000A6EA File Offset: 0x000088EA
			[DebuggerStepThrough]
			public static v64 vclt_u64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DB RID: 1755 RVA: 0x0000A6F1 File Offset: 0x000088F1
			[DebuggerStepThrough]
			public static v128 vcltq_u64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x0000A6F8 File Offset: 0x000088F8
			[DebuggerStepThrough]
			public static v64 vclt_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DD RID: 1757 RVA: 0x0000A6FF File Offset: 0x000088FF
			[DebuggerStepThrough]
			public static v128 vcltq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x0000A706 File Offset: 0x00008906
			[DebuggerStepThrough]
			public static ulong vcltd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006DF RID: 1759 RVA: 0x0000A70D File Offset: 0x0000890D
			[DebuggerStepThrough]
			public static ulong vcltd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x0000A714 File Offset: 0x00008914
			[DebuggerStepThrough]
			public static uint vclts_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x0000A71B File Offset: 0x0000891B
			[DebuggerStepThrough]
			public static ulong vcltd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x0000A722 File Offset: 0x00008922
			[DebuggerStepThrough]
			public static v64 vcltz_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x0000A729 File Offset: 0x00008929
			[DebuggerStepThrough]
			public static v128 vcltzq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x0000A730 File Offset: 0x00008930
			[DebuggerStepThrough]
			public static v64 vcltz_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E5 RID: 1765 RVA: 0x0000A737 File Offset: 0x00008937
			[DebuggerStepThrough]
			public static v128 vcltzq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x0000A73E File Offset: 0x0000893E
			[DebuggerStepThrough]
			public static v64 vcltz_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x0000A745 File Offset: 0x00008945
			[DebuggerStepThrough]
			public static v128 vcltzq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x0000A74C File Offset: 0x0000894C
			[DebuggerStepThrough]
			public static v64 vcltz_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006E9 RID: 1769 RVA: 0x0000A753 File Offset: 0x00008953
			[DebuggerStepThrough]
			public static v128 vcltzq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0000A75A File Offset: 0x0000895A
			[DebuggerStepThrough]
			public static v64 vcltz_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x0000A761 File Offset: 0x00008961
			[DebuggerStepThrough]
			public static v128 vcltzq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x0000A768 File Offset: 0x00008968
			[DebuggerStepThrough]
			public static v64 vcltz_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0000A76F File Offset: 0x0000896F
			[DebuggerStepThrough]
			public static v128 vcltzq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x0000A776 File Offset: 0x00008976
			[DebuggerStepThrough]
			public static ulong vcltzd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006EF RID: 1775 RVA: 0x0000A77D File Offset: 0x0000897D
			[DebuggerStepThrough]
			public static uint vcltzs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x0000A784 File Offset: 0x00008984
			[DebuggerStepThrough]
			public static ulong vcltzd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x0000A78B File Offset: 0x0000898B
			[DebuggerStepThrough]
			public static v64 vcage_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x0000A792 File Offset: 0x00008992
			[DebuggerStepThrough]
			public static v128 vcageq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x0000A799 File Offset: 0x00008999
			[DebuggerStepThrough]
			public static uint vcages_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x0000A7A0 File Offset: 0x000089A0
			[DebuggerStepThrough]
			public static ulong vcaged_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x0000A7A7 File Offset: 0x000089A7
			[DebuggerStepThrough]
			public static v64 vcale_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x0000A7AE File Offset: 0x000089AE
			[DebuggerStepThrough]
			public static v128 vcaleq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x0000A7B5 File Offset: 0x000089B5
			[DebuggerStepThrough]
			public static uint vcales_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x0000A7BC File Offset: 0x000089BC
			[DebuggerStepThrough]
			public static ulong vcaled_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x0000A7C3 File Offset: 0x000089C3
			[DebuggerStepThrough]
			public static v64 vcagt_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x0000A7CA File Offset: 0x000089CA
			[DebuggerStepThrough]
			public static v128 vcagtq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FB RID: 1787 RVA: 0x0000A7D1 File Offset: 0x000089D1
			[DebuggerStepThrough]
			public static uint vcagts_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x0000A7D8 File Offset: 0x000089D8
			[DebuggerStepThrough]
			public static ulong vcagtd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FD RID: 1789 RVA: 0x0000A7DF File Offset: 0x000089DF
			[DebuggerStepThrough]
			public static v64 vcalt_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x0000A7E6 File Offset: 0x000089E6
			[DebuggerStepThrough]
			public static v128 vcaltq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060006FF RID: 1791 RVA: 0x0000A7ED File Offset: 0x000089ED
			[DebuggerStepThrough]
			public static uint vcalts_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x0000A7F4 File Offset: 0x000089F4
			[DebuggerStepThrough]
			public static ulong vcaltd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x0000A7FB File Offset: 0x000089FB
			[DebuggerStepThrough]
			public static v64 vtst_s64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x0000A802 File Offset: 0x00008A02
			[DebuggerStepThrough]
			public static v128 vtstq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x0000A809 File Offset: 0x00008A09
			[DebuggerStepThrough]
			public static v64 vtst_u64(v64 a0, v64 a1)
			{
				return Arm.Neon.vtst_s64(a0, a1);
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x0000A812 File Offset: 0x00008A12
			[DebuggerStepThrough]
			public static v128 vtstq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vtstq_s64(a0, a1);
			}

			// Token: 0x06000705 RID: 1797 RVA: 0x0000A81B File Offset: 0x00008A1B
			[DebuggerStepThrough]
			public static ulong vtstd_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x0000A822 File Offset: 0x00008A22
			[DebuggerStepThrough]
			public static ulong vtstd_u64(ulong a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x0000A829 File Offset: 0x00008A29
			[DebuggerStepThrough]
			public static v64 vabd_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0000A830 File Offset: 0x00008A30
			[DebuggerStepThrough]
			public static v128 vabdq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x0000A837 File Offset: 0x00008A37
			[DebuggerStepThrough]
			public static float vabds_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x0000A83E File Offset: 0x00008A3E
			[DebuggerStepThrough]
			public static double vabdd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x0000A845 File Offset: 0x00008A45
			[DebuggerStepThrough]
			public static v128 vabdl_high_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070C RID: 1804 RVA: 0x0000A84C File Offset: 0x00008A4C
			[DebuggerStepThrough]
			public static v128 vabdl_high_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x0000A853 File Offset: 0x00008A53
			[DebuggerStepThrough]
			public static v128 vabdl_high_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x0000A85A File Offset: 0x00008A5A
			[DebuggerStepThrough]
			public static v128 vabdl_high_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600070F RID: 1807 RVA: 0x0000A861 File Offset: 0x00008A61
			[DebuggerStepThrough]
			public static v128 vabdl_high_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x0000A868 File Offset: 0x00008A68
			[DebuggerStepThrough]
			public static v128 vabdl_high_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000711 RID: 1809 RVA: 0x0000A86F File Offset: 0x00008A6F
			[DebuggerStepThrough]
			public static v128 vabal_high_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x0000A876 File Offset: 0x00008A76
			[DebuggerStepThrough]
			public static v128 vabal_high_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x0000A87D File Offset: 0x00008A7D
			[DebuggerStepThrough]
			public static v128 vabal_high_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x0000A884 File Offset: 0x00008A84
			[DebuggerStepThrough]
			public static v128 vabal_high_u8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x0000A88B File Offset: 0x00008A8B
			[DebuggerStepThrough]
			public static v128 vabal_high_u16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0000A892 File Offset: 0x00008A92
			[DebuggerStepThrough]
			public static v128 vabal_high_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x0000A899 File Offset: 0x00008A99
			[DebuggerStepThrough]
			public static v64 vmax_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0000A8A0 File Offset: 0x00008AA0
			[DebuggerStepThrough]
			public static v128 vmaxq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000719 RID: 1817 RVA: 0x0000A8A7 File Offset: 0x00008AA7
			[DebuggerStepThrough]
			public static v64 vmin_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0000A8AE File Offset: 0x00008AAE
			[DebuggerStepThrough]
			public static v128 vminq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x0000A8B5 File Offset: 0x00008AB5
			[DebuggerStepThrough]
			public static v64 vmaxnm_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x0000A8BC File Offset: 0x00008ABC
			[DebuggerStepThrough]
			public static v128 vmaxnmq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x0000A8C3 File Offset: 0x00008AC3
			[DebuggerStepThrough]
			public static v64 vmaxnm_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x0000A8CA File Offset: 0x00008ACA
			[DebuggerStepThrough]
			public static v128 vmaxnmq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x0000A8D1 File Offset: 0x00008AD1
			[DebuggerStepThrough]
			public static v64 vminnm_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0000A8D8 File Offset: 0x00008AD8
			[DebuggerStepThrough]
			public static v128 vminnmq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000721 RID: 1825 RVA: 0x0000A8DF File Offset: 0x00008ADF
			[DebuggerStepThrough]
			public static v64 vminnm_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x0000A8E6 File Offset: 0x00008AE6
			[DebuggerStepThrough]
			public static v128 vminnmq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x0000A8ED File Offset: 0x00008AED
			[DebuggerStepThrough]
			public static long vshld_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x0000A8F4 File Offset: 0x00008AF4
			[DebuggerStepThrough]
			public static ulong vshld_u64(ulong a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x0000A8FB File Offset: 0x00008AFB
			[DebuggerStepThrough]
			public static sbyte vqshlb_s8(sbyte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000726 RID: 1830 RVA: 0x0000A902 File Offset: 0x00008B02
			[DebuggerStepThrough]
			public static short vqshlh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x0000A909 File Offset: 0x00008B09
			[DebuggerStepThrough]
			public static int vqshls_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000728 RID: 1832 RVA: 0x0000A910 File Offset: 0x00008B10
			[DebuggerStepThrough]
			public static long vqshld_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000729 RID: 1833 RVA: 0x0000A917 File Offset: 0x00008B17
			[DebuggerStepThrough]
			public static byte vqshlb_u8(byte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x0000A91E File Offset: 0x00008B1E
			[DebuggerStepThrough]
			public static ushort vqshlh_u16(ushort a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x0000A925 File Offset: 0x00008B25
			[DebuggerStepThrough]
			public static uint vqshls_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0000A92C File Offset: 0x00008B2C
			[DebuggerStepThrough]
			public static ulong vqshld_u64(ulong a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x0000A933 File Offset: 0x00008B33
			[DebuggerStepThrough]
			public static long vrshld_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x0000A93A File Offset: 0x00008B3A
			[DebuggerStepThrough]
			public static ulong vrshld_u64(ulong a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x0000A941 File Offset: 0x00008B41
			[DebuggerStepThrough]
			public static sbyte vqrshlb_s8(sbyte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000730 RID: 1840 RVA: 0x0000A948 File Offset: 0x00008B48
			[DebuggerStepThrough]
			public static short vqrshlh_s16(short a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x0000A94F File Offset: 0x00008B4F
			[DebuggerStepThrough]
			public static int vqrshls_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000732 RID: 1842 RVA: 0x0000A956 File Offset: 0x00008B56
			[DebuggerStepThrough]
			public static long vqrshld_s64(long a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x0000A95D File Offset: 0x00008B5D
			[DebuggerStepThrough]
			public static byte vqrshlb_u8(byte a0, sbyte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000734 RID: 1844 RVA: 0x0000A964 File Offset: 0x00008B64
			[DebuggerStepThrough]
			public static ushort vqrshlh_u16(ushort a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000735 RID: 1845 RVA: 0x0000A96B File Offset: 0x00008B6B
			[DebuggerStepThrough]
			public static uint vqrshls_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000736 RID: 1846 RVA: 0x0000A972 File Offset: 0x00008B72
			[DebuggerStepThrough]
			public static ulong vqrshld_u64(ulong a0, long a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000737 RID: 1847 RVA: 0x0000A979 File Offset: 0x00008B79
			[DebuggerStepThrough]
			public static long vshrd_n_s64(long a0, int a1)
			{
				return a0 >> a1;
			}

			// Token: 0x06000738 RID: 1848 RVA: 0x0000A981 File Offset: 0x00008B81
			[DebuggerStepThrough]
			public static ulong vshrd_n_u64(ulong a0, int a1)
			{
				return a0 >> a1;
			}

			// Token: 0x06000739 RID: 1849 RVA: 0x0000A989 File Offset: 0x00008B89
			[DebuggerStepThrough]
			public static long vshld_n_s64(long a0, int a1)
			{
				return a0 << a1;
			}

			// Token: 0x0600073A RID: 1850 RVA: 0x0000A991 File Offset: 0x00008B91
			[DebuggerStepThrough]
			public static ulong vshld_n_u64(ulong a0, int a1)
			{
				return a0 << a1;
			}

			// Token: 0x0600073B RID: 1851 RVA: 0x0000A999 File Offset: 0x00008B99
			[DebuggerStepThrough]
			public static long vrshrd_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600073C RID: 1852 RVA: 0x0000A9A0 File Offset: 0x00008BA0
			[DebuggerStepThrough]
			public static ulong vrshrd_n_u64(ulong a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x0000A9A7 File Offset: 0x00008BA7
			[DebuggerStepThrough]
			public static long vsrad_n_s64(long a0, long a1, int a2)
			{
				return a0 + (a1 >> a2);
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0000A9B1 File Offset: 0x00008BB1
			[DebuggerStepThrough]
			public static ulong vsrad_n_u64(ulong a0, ulong a1, int a2)
			{
				return a0 + (a1 >> a2);
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0000A9BB File Offset: 0x00008BBB
			[DebuggerStepThrough]
			public static long vrsrad_n_s64(long a0, long a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x0000A9C2 File Offset: 0x00008BC2
			[DebuggerStepThrough]
			public static ulong vrsrad_n_u64(ulong a0, ulong a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000741 RID: 1857 RVA: 0x0000A9C9 File Offset: 0x00008BC9
			[DebuggerStepThrough]
			public static sbyte vqshlb_n_s8(sbyte a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000742 RID: 1858 RVA: 0x0000A9D0 File Offset: 0x00008BD0
			[DebuggerStepThrough]
			public static short vqshlh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000743 RID: 1859 RVA: 0x0000A9D7 File Offset: 0x00008BD7
			[DebuggerStepThrough]
			public static int vqshls_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000744 RID: 1860 RVA: 0x0000A9DE File Offset: 0x00008BDE
			[DebuggerStepThrough]
			public static long vqshld_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000745 RID: 1861 RVA: 0x0000A9E5 File Offset: 0x00008BE5
			[DebuggerStepThrough]
			public static byte vqshlb_n_u8(byte a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000746 RID: 1862 RVA: 0x0000A9EC File Offset: 0x00008BEC
			[DebuggerStepThrough]
			public static ushort vqshlh_n_u16(ushort a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000747 RID: 1863 RVA: 0x0000A9F3 File Offset: 0x00008BF3
			[DebuggerStepThrough]
			public static uint vqshls_n_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000748 RID: 1864 RVA: 0x0000A9FA File Offset: 0x00008BFA
			[DebuggerStepThrough]
			public static ulong vqshld_n_u64(ulong a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000749 RID: 1865 RVA: 0x0000AA01 File Offset: 0x00008C01
			[DebuggerStepThrough]
			public static byte vqshlub_n_s8(sbyte a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074A RID: 1866 RVA: 0x0000AA08 File Offset: 0x00008C08
			[DebuggerStepThrough]
			public static ushort vqshluh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x0000AA0F File Offset: 0x00008C0F
			[DebuggerStepThrough]
			public static uint vqshlus_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x0000AA16 File Offset: 0x00008C16
			[DebuggerStepThrough]
			public static ulong vqshlud_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x0000AA1D File Offset: 0x00008C1D
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074E RID: 1870 RVA: 0x0000AA24 File Offset: 0x00008C24
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x0000AA2B File Offset: 0x00008C2B
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000750 RID: 1872 RVA: 0x0000AA32 File Offset: 0x00008C32
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x0000AA39 File Offset: 0x00008C39
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000752 RID: 1874 RVA: 0x0000AA40 File Offset: 0x00008C40
			[DebuggerStepThrough]
			public static v128 vshrn_high_n_u64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x0000AA47 File Offset: 0x00008C47
			[DebuggerStepThrough]
			public static byte vqshrunh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0000AA4E File Offset: 0x00008C4E
			[DebuggerStepThrough]
			public static ushort vqshruns_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000755 RID: 1877 RVA: 0x0000AA55 File Offset: 0x00008C55
			[DebuggerStepThrough]
			public static uint vqshrund_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x0000AA5C File Offset: 0x00008C5C
			[DebuggerStepThrough]
			public static v128 vqshrun_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000757 RID: 1879 RVA: 0x0000AA63 File Offset: 0x00008C63
			[DebuggerStepThrough]
			public static v128 vqshrun_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000758 RID: 1880 RVA: 0x0000AA6A File Offset: 0x00008C6A
			[DebuggerStepThrough]
			public static v128 vqshrun_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000759 RID: 1881 RVA: 0x0000AA71 File Offset: 0x00008C71
			[DebuggerStepThrough]
			public static byte vqrshrunh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075A RID: 1882 RVA: 0x0000AA78 File Offset: 0x00008C78
			[DebuggerStepThrough]
			public static ushort vqrshruns_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075B RID: 1883 RVA: 0x0000AA7F File Offset: 0x00008C7F
			[DebuggerStepThrough]
			public static uint vqrshrund_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075C RID: 1884 RVA: 0x0000AA86 File Offset: 0x00008C86
			[DebuggerStepThrough]
			public static v128 vqrshrun_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x0000AA8D File Offset: 0x00008C8D
			[DebuggerStepThrough]
			public static v128 vqrshrun_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075E RID: 1886 RVA: 0x0000AA94 File Offset: 0x00008C94
			[DebuggerStepThrough]
			public static v128 vqrshrun_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600075F RID: 1887 RVA: 0x0000AA9B File Offset: 0x00008C9B
			[DebuggerStepThrough]
			public static sbyte vqshrnh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000760 RID: 1888 RVA: 0x0000AAA2 File Offset: 0x00008CA2
			[DebuggerStepThrough]
			public static short vqshrns_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000761 RID: 1889 RVA: 0x0000AAA9 File Offset: 0x00008CA9
			[DebuggerStepThrough]
			public static int vqshrnd_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000762 RID: 1890 RVA: 0x0000AAB0 File Offset: 0x00008CB0
			[DebuggerStepThrough]
			public static byte vqshrnh_n_u16(ushort a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000763 RID: 1891 RVA: 0x0000AAB7 File Offset: 0x00008CB7
			[DebuggerStepThrough]
			public static ushort vqshrns_n_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000764 RID: 1892 RVA: 0x0000AABE File Offset: 0x00008CBE
			[DebuggerStepThrough]
			public static uint vqshrnd_n_u64(ulong a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000765 RID: 1893 RVA: 0x0000AAC5 File Offset: 0x00008CC5
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000766 RID: 1894 RVA: 0x0000AACC File Offset: 0x00008CCC
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000767 RID: 1895 RVA: 0x0000AAD3 File Offset: 0x00008CD3
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000768 RID: 1896 RVA: 0x0000AADA File Offset: 0x00008CDA
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000769 RID: 1897 RVA: 0x0000AAE1 File Offset: 0x00008CE1
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076A RID: 1898 RVA: 0x0000AAE8 File Offset: 0x00008CE8
			[DebuggerStepThrough]
			public static v128 vqshrn_high_n_u64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076B RID: 1899 RVA: 0x0000AAEF File Offset: 0x00008CEF
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x0000AAF6 File Offset: 0x00008CF6
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076D RID: 1901 RVA: 0x0000AAFD File Offset: 0x00008CFD
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076E RID: 1902 RVA: 0x0000AB04 File Offset: 0x00008D04
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600076F RID: 1903 RVA: 0x0000AB0B File Offset: 0x00008D0B
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x0000AB12 File Offset: 0x00008D12
			[DebuggerStepThrough]
			public static v128 vrshrn_high_n_u64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x0000AB19 File Offset: 0x00008D19
			[DebuggerStepThrough]
			public static sbyte vqrshrnh_n_s16(short a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x0000AB20 File Offset: 0x00008D20
			[DebuggerStepThrough]
			public static short vqrshrns_n_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x0000AB27 File Offset: 0x00008D27
			[DebuggerStepThrough]
			public static int vqrshrnd_n_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x0000AB2E File Offset: 0x00008D2E
			[DebuggerStepThrough]
			public static byte vqrshrnh_n_u16(ushort a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x0000AB35 File Offset: 0x00008D35
			[DebuggerStepThrough]
			public static ushort vqrshrns_n_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x0000AB3C File Offset: 0x00008D3C
			[DebuggerStepThrough]
			public static uint vqrshrnd_n_u64(ulong a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000777 RID: 1911 RVA: 0x0000AB43 File Offset: 0x00008D43
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000778 RID: 1912 RVA: 0x0000AB4A File Offset: 0x00008D4A
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000779 RID: 1913 RVA: 0x0000AB51 File Offset: 0x00008D51
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_s64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077A RID: 1914 RVA: 0x0000AB58 File Offset: 0x00008D58
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077B RID: 1915 RVA: 0x0000AB5F File Offset: 0x00008D5F
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077C RID: 1916 RVA: 0x0000AB66 File Offset: 0x00008D66
			[DebuggerStepThrough]
			public static v128 vqrshrn_high_n_u64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077D RID: 1917 RVA: 0x0000AB6D File Offset: 0x00008D6D
			[DebuggerStepThrough]
			public static v128 vshll_high_n_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077E RID: 1918 RVA: 0x0000AB74 File Offset: 0x00008D74
			[DebuggerStepThrough]
			public static v128 vshll_high_n_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600077F RID: 1919 RVA: 0x0000AB7B File Offset: 0x00008D7B
			[DebuggerStepThrough]
			public static v128 vshll_high_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000780 RID: 1920 RVA: 0x0000AB82 File Offset: 0x00008D82
			[DebuggerStepThrough]
			public static v128 vshll_high_n_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000781 RID: 1921 RVA: 0x0000AB89 File Offset: 0x00008D89
			[DebuggerStepThrough]
			public static v128 vshll_high_n_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000782 RID: 1922 RVA: 0x0000AB90 File Offset: 0x00008D90
			[DebuggerStepThrough]
			public static v128 vshll_high_n_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000783 RID: 1923 RVA: 0x0000AB97 File Offset: 0x00008D97
			[DebuggerStepThrough]
			public static long vsrid_n_s64(long a0, long a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x0000AB9E File Offset: 0x00008D9E
			[DebuggerStepThrough]
			public static ulong vsrid_n_u64(ulong a0, ulong a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000785 RID: 1925 RVA: 0x0000ABA5 File Offset: 0x00008DA5
			[DebuggerStepThrough]
			public static long vslid_n_s64(long a0, long a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x0000ABAC File Offset: 0x00008DAC
			[DebuggerStepThrough]
			public static ulong vslid_n_u64(ulong a0, ulong a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x0000ABB3 File Offset: 0x00008DB3
			[DebuggerStepThrough]
			public static v64 vcvtn_s32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x0000ABBA File Offset: 0x00008DBA
			[DebuggerStepThrough]
			public static v128 vcvtnq_s32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000789 RID: 1929 RVA: 0x0000ABC1 File Offset: 0x00008DC1
			[DebuggerStepThrough]
			public static v64 vcvtn_u32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078A RID: 1930 RVA: 0x0000ABC8 File Offset: 0x00008DC8
			[DebuggerStepThrough]
			public static v128 vcvtnq_u32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078B RID: 1931 RVA: 0x0000ABCF File Offset: 0x00008DCF
			[DebuggerStepThrough]
			public static v64 vcvtm_s32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078C RID: 1932 RVA: 0x0000ABD6 File Offset: 0x00008DD6
			[DebuggerStepThrough]
			public static v128 vcvtmq_s32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078D RID: 1933 RVA: 0x0000ABDD File Offset: 0x00008DDD
			[DebuggerStepThrough]
			public static v64 vcvtm_u32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x0000ABE4 File Offset: 0x00008DE4
			[DebuggerStepThrough]
			public static v128 vcvtmq_u32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x0000ABEB File Offset: 0x00008DEB
			[DebuggerStepThrough]
			public static v64 vcvtp_s32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x0000ABF2 File Offset: 0x00008DF2
			[DebuggerStepThrough]
			public static v128 vcvtpq_s32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000791 RID: 1937 RVA: 0x0000ABF9 File Offset: 0x00008DF9
			[DebuggerStepThrough]
			public static v64 vcvtp_u32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000792 RID: 1938 RVA: 0x0000AC00 File Offset: 0x00008E00
			[DebuggerStepThrough]
			public static v128 vcvtpq_u32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x0000AC07 File Offset: 0x00008E07
			[DebuggerStepThrough]
			public static v64 vcvta_s32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x0000AC0E File Offset: 0x00008E0E
			[DebuggerStepThrough]
			public static v128 vcvtaq_s32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000795 RID: 1941 RVA: 0x0000AC15 File Offset: 0x00008E15
			[DebuggerStepThrough]
			public static v64 vcvta_u32_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000796 RID: 1942 RVA: 0x0000AC1C File Offset: 0x00008E1C
			[DebuggerStepThrough]
			public static v128 vcvtaq_u32_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000797 RID: 1943 RVA: 0x0000AC23 File Offset: 0x00008E23
			[DebuggerStepThrough]
			public static int vcvts_s32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x0000AC2A File Offset: 0x00008E2A
			[DebuggerStepThrough]
			public static uint vcvts_u32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000799 RID: 1945 RVA: 0x0000AC31 File Offset: 0x00008E31
			[DebuggerStepThrough]
			public static int vcvtns_s32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x0000AC38 File Offset: 0x00008E38
			[DebuggerStepThrough]
			public static uint vcvtns_u32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079B RID: 1947 RVA: 0x0000AC3F File Offset: 0x00008E3F
			[DebuggerStepThrough]
			public static int vcvtms_s32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x0000AC46 File Offset: 0x00008E46
			[DebuggerStepThrough]
			public static uint vcvtms_u32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x0000AC4D File Offset: 0x00008E4D
			[DebuggerStepThrough]
			public static int vcvtps_s32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079E RID: 1950 RVA: 0x0000AC54 File Offset: 0x00008E54
			[DebuggerStepThrough]
			public static uint vcvtps_u32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600079F RID: 1951 RVA: 0x0000AC5B File Offset: 0x00008E5B
			[DebuggerStepThrough]
			public static int vcvtas_s32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A0 RID: 1952 RVA: 0x0000AC62 File Offset: 0x00008E62
			[DebuggerStepThrough]
			public static uint vcvtas_u32_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A1 RID: 1953 RVA: 0x0000AC69 File Offset: 0x00008E69
			[DebuggerStepThrough]
			public static v64 vcvt_s64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A2 RID: 1954 RVA: 0x0000AC70 File Offset: 0x00008E70
			[DebuggerStepThrough]
			public static v128 vcvtq_s64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A3 RID: 1955 RVA: 0x0000AC77 File Offset: 0x00008E77
			[DebuggerStepThrough]
			public static v64 vcvt_u64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0000AC7E File Offset: 0x00008E7E
			[DebuggerStepThrough]
			public static v128 vcvtq_u64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0000AC85 File Offset: 0x00008E85
			[DebuggerStepThrough]
			public static v64 vcvtn_s64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0000AC8C File Offset: 0x00008E8C
			[DebuggerStepThrough]
			public static v128 vcvtnq_s64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A7 RID: 1959 RVA: 0x0000AC93 File Offset: 0x00008E93
			[DebuggerStepThrough]
			public static v64 vcvtn_u64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A8 RID: 1960 RVA: 0x0000AC9A File Offset: 0x00008E9A
			[DebuggerStepThrough]
			public static v128 vcvtnq_u64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007A9 RID: 1961 RVA: 0x0000ACA1 File Offset: 0x00008EA1
			[DebuggerStepThrough]
			public static v64 vcvtm_s64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AA RID: 1962 RVA: 0x0000ACA8 File Offset: 0x00008EA8
			[DebuggerStepThrough]
			public static v128 vcvtmq_s64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AB RID: 1963 RVA: 0x0000ACAF File Offset: 0x00008EAF
			[DebuggerStepThrough]
			public static v64 vcvtm_u64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AC RID: 1964 RVA: 0x0000ACB6 File Offset: 0x00008EB6
			[DebuggerStepThrough]
			public static v128 vcvtmq_u64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AD RID: 1965 RVA: 0x0000ACBD File Offset: 0x00008EBD
			[DebuggerStepThrough]
			public static v64 vcvtp_s64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AE RID: 1966 RVA: 0x0000ACC4 File Offset: 0x00008EC4
			[DebuggerStepThrough]
			public static v128 vcvtpq_s64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007AF RID: 1967 RVA: 0x0000ACCB File Offset: 0x00008ECB
			[DebuggerStepThrough]
			public static v64 vcvtp_u64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B0 RID: 1968 RVA: 0x0000ACD2 File Offset: 0x00008ED2
			[DebuggerStepThrough]
			public static v128 vcvtpq_u64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B1 RID: 1969 RVA: 0x0000ACD9 File Offset: 0x00008ED9
			[DebuggerStepThrough]
			public static v64 vcvta_s64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B2 RID: 1970 RVA: 0x0000ACE0 File Offset: 0x00008EE0
			[DebuggerStepThrough]
			public static v128 vcvtaq_s64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B3 RID: 1971 RVA: 0x0000ACE7 File Offset: 0x00008EE7
			[DebuggerStepThrough]
			public static v64 vcvta_u64_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0000ACEE File Offset: 0x00008EEE
			[DebuggerStepThrough]
			public static v128 vcvtaq_u64_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x0000ACF5 File Offset: 0x00008EF5
			[DebuggerStepThrough]
			public static long vcvtd_s64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x0000ACFC File Offset: 0x00008EFC
			[DebuggerStepThrough]
			public static ulong vcvtd_u64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x0000AD03 File Offset: 0x00008F03
			[DebuggerStepThrough]
			public static long vcvtnd_s64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x0000AD0A File Offset: 0x00008F0A
			[DebuggerStepThrough]
			public static ulong vcvtnd_u64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007B9 RID: 1977 RVA: 0x0000AD11 File Offset: 0x00008F11
			[DebuggerStepThrough]
			public static long vcvtmd_s64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x0000AD18 File Offset: 0x00008F18
			[DebuggerStepThrough]
			public static ulong vcvtmd_u64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x0000AD1F File Offset: 0x00008F1F
			[DebuggerStepThrough]
			public static long vcvtpd_s64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x0000AD26 File Offset: 0x00008F26
			[DebuggerStepThrough]
			public static ulong vcvtpd_u64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x0000AD2D File Offset: 0x00008F2D
			[DebuggerStepThrough]
			public static long vcvtad_s64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x0000AD34 File Offset: 0x00008F34
			[DebuggerStepThrough]
			public static ulong vcvtad_u64_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x0000AD3B File Offset: 0x00008F3B
			[DebuggerStepThrough]
			public static int vcvts_n_s32_f32(float a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x0000AD42 File Offset: 0x00008F42
			[DebuggerStepThrough]
			public static uint vcvts_n_u32_f32(float a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x0000AD49 File Offset: 0x00008F49
			[DebuggerStepThrough]
			public static v64 vcvt_n_s64_f64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x0000AD50 File Offset: 0x00008F50
			[DebuggerStepThrough]
			public static v128 vcvtq_n_s64_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x0000AD57 File Offset: 0x00008F57
			[DebuggerStepThrough]
			public static v64 vcvt_n_u64_f64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x0000AD5E File Offset: 0x00008F5E
			[DebuggerStepThrough]
			public static v128 vcvtq_n_u64_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x0000AD65 File Offset: 0x00008F65
			[DebuggerStepThrough]
			public static long vcvtd_n_s64_f64(double a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x0000AD6C File Offset: 0x00008F6C
			[DebuggerStepThrough]
			public static ulong vcvtd_n_u64_f64(double a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x0000AD73 File Offset: 0x00008F73
			[DebuggerStepThrough]
			public static float vcvts_f32_s32(int a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C8 RID: 1992 RVA: 0x0000AD7A File Offset: 0x00008F7A
			[DebuggerStepThrough]
			public static float vcvts_f32_u32(uint a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x0000AD81 File Offset: 0x00008F81
			[DebuggerStepThrough]
			public static v64 vcvt_f64_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x0000AD88 File Offset: 0x00008F88
			[DebuggerStepThrough]
			public static v128 vcvtq_f64_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x0000AD8F File Offset: 0x00008F8F
			[DebuggerStepThrough]
			public static v64 vcvt_f64_u64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x0000AD96 File Offset: 0x00008F96
			[DebuggerStepThrough]
			public static v128 vcvtq_f64_u64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x0000AD9D File Offset: 0x00008F9D
			[DebuggerStepThrough]
			public static double vcvtd_f64_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x0000ADA4 File Offset: 0x00008FA4
			[DebuggerStepThrough]
			public static double vcvtd_f64_u64(ulong a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007CF RID: 1999 RVA: 0x0000ADAB File Offset: 0x00008FAB
			[DebuggerStepThrough]
			public static float vcvts_n_f32_s32(int a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D0 RID: 2000 RVA: 0x0000ADB2 File Offset: 0x00008FB2
			[DebuggerStepThrough]
			public static float vcvts_n_f32_u32(uint a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D1 RID: 2001 RVA: 0x0000ADB9 File Offset: 0x00008FB9
			[DebuggerStepThrough]
			public static v64 vcvt_n_f64_s64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x0000ADC0 File Offset: 0x00008FC0
			[DebuggerStepThrough]
			public static v128 vcvtq_n_f64_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x0000ADC7 File Offset: 0x00008FC7
			[DebuggerStepThrough]
			public static v64 vcvt_n_f64_u64(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D4 RID: 2004 RVA: 0x0000ADCE File Offset: 0x00008FCE
			[DebuggerStepThrough]
			public static v128 vcvtq_n_f64_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D5 RID: 2005 RVA: 0x0000ADD5 File Offset: 0x00008FD5
			[DebuggerStepThrough]
			public static double vcvtd_n_f64_s64(long a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D6 RID: 2006 RVA: 0x0000ADDC File Offset: 0x00008FDC
			[DebuggerStepThrough]
			public static double vcvtd_n_f64_u64(ulong a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D7 RID: 2007 RVA: 0x0000ADE3 File Offset: 0x00008FE3
			[DebuggerStepThrough]
			public static v64 vcvt_f32_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D8 RID: 2008 RVA: 0x0000ADEA File Offset: 0x00008FEA
			[DebuggerStepThrough]
			public static v128 vcvt_high_f32_f64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007D9 RID: 2009 RVA: 0x0000ADF1 File Offset: 0x00008FF1
			[DebuggerStepThrough]
			public static v128 vcvt_f64_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DA RID: 2010 RVA: 0x0000ADF8 File Offset: 0x00008FF8
			[DebuggerStepThrough]
			public static v128 vcvt_high_f64_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DB RID: 2011 RVA: 0x0000ADFF File Offset: 0x00008FFF
			[DebuggerStepThrough]
			public static v64 vcvtx_f32_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DC RID: 2012 RVA: 0x0000AE06 File Offset: 0x00009006
			[DebuggerStepThrough]
			public static float vcvtxd_f32_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DD RID: 2013 RVA: 0x0000AE0D File Offset: 0x0000900D
			[DebuggerStepThrough]
			public static v128 vcvtx_high_f32_f64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DE RID: 2014 RVA: 0x0000AE14 File Offset: 0x00009014
			[DebuggerStepThrough]
			public static v64 vrnd_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007DF RID: 2015 RVA: 0x0000AE1B File Offset: 0x0000901B
			[DebuggerStepThrough]
			public static v128 vrndq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E0 RID: 2016 RVA: 0x0000AE22 File Offset: 0x00009022
			[DebuggerStepThrough]
			public static v64 vrnd_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E1 RID: 2017 RVA: 0x0000AE29 File Offset: 0x00009029
			[DebuggerStepThrough]
			public static v128 vrndq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E2 RID: 2018 RVA: 0x0000AE30 File Offset: 0x00009030
			[DebuggerStepThrough]
			public static v64 vrndn_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E3 RID: 2019 RVA: 0x0000AE37 File Offset: 0x00009037
			[DebuggerStepThrough]
			public static v128 vrndnq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E4 RID: 2020 RVA: 0x0000AE3E File Offset: 0x0000903E
			[DebuggerStepThrough]
			public static v64 vrndn_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E5 RID: 2021 RVA: 0x0000AE45 File Offset: 0x00009045
			[DebuggerStepThrough]
			public static v128 vrndnq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E6 RID: 2022 RVA: 0x0000AE4C File Offset: 0x0000904C
			[DebuggerStepThrough]
			public static float vrndns_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E7 RID: 2023 RVA: 0x0000AE53 File Offset: 0x00009053
			[DebuggerStepThrough]
			public static v64 vrndm_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E8 RID: 2024 RVA: 0x0000AE5A File Offset: 0x0000905A
			[DebuggerStepThrough]
			public static v128 vrndmq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x0000AE61 File Offset: 0x00009061
			[DebuggerStepThrough]
			public static v64 vrndm_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007EA RID: 2026 RVA: 0x0000AE68 File Offset: 0x00009068
			[DebuggerStepThrough]
			public static v128 vrndmq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007EB RID: 2027 RVA: 0x0000AE6F File Offset: 0x0000906F
			[DebuggerStepThrough]
			public static v64 vrndp_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007EC RID: 2028 RVA: 0x0000AE76 File Offset: 0x00009076
			[DebuggerStepThrough]
			public static v128 vrndpq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007ED RID: 2029 RVA: 0x0000AE7D File Offset: 0x0000907D
			[DebuggerStepThrough]
			public static v64 vrndp_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x0000AE84 File Offset: 0x00009084
			[DebuggerStepThrough]
			public static v128 vrndpq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0000AE8B File Offset: 0x0000908B
			[DebuggerStepThrough]
			public static v64 vrnda_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x0000AE92 File Offset: 0x00009092
			[DebuggerStepThrough]
			public static v128 vrndaq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x0000AE99 File Offset: 0x00009099
			[DebuggerStepThrough]
			public static v64 vrnda_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F2 RID: 2034 RVA: 0x0000AEA0 File Offset: 0x000090A0
			[DebuggerStepThrough]
			public static v128 vrndaq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x0000AEA7 File Offset: 0x000090A7
			[DebuggerStepThrough]
			public static v64 vrndi_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x0000AEAE File Offset: 0x000090AE
			[DebuggerStepThrough]
			public static v128 vrndiq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x0000AEB5 File Offset: 0x000090B5
			[DebuggerStepThrough]
			public static v64 vrndi_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x0000AEBC File Offset: 0x000090BC
			[DebuggerStepThrough]
			public static v128 vrndiq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x0000AEC3 File Offset: 0x000090C3
			[DebuggerStepThrough]
			public static v64 vrndx_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x0000AECA File Offset: 0x000090CA
			[DebuggerStepThrough]
			public static v128 vrndxq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0000AED1 File Offset: 0x000090D1
			[DebuggerStepThrough]
			public static v64 vrndx_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FA RID: 2042 RVA: 0x0000AED8 File Offset: 0x000090D8
			[DebuggerStepThrough]
			public static v128 vrndxq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FB RID: 2043 RVA: 0x0000AEDF File Offset: 0x000090DF
			[DebuggerStepThrough]
			public static v128 vmovl_high_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FC RID: 2044 RVA: 0x0000AEE6 File Offset: 0x000090E6
			[DebuggerStepThrough]
			public static v128 vmovl_high_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FD RID: 2045 RVA: 0x0000AEED File Offset: 0x000090ED
			[DebuggerStepThrough]
			public static v128 vmovl_high_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x0000AEF4 File Offset: 0x000090F4
			[DebuggerStepThrough]
			public static v128 vmovl_high_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060007FF RID: 2047 RVA: 0x0000AEFB File Offset: 0x000090FB
			[DebuggerStepThrough]
			public static v128 vmovl_high_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000800 RID: 2048 RVA: 0x0000AF02 File Offset: 0x00009102
			[DebuggerStepThrough]
			public static v128 vmovl_high_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000801 RID: 2049 RVA: 0x0000AF09 File Offset: 0x00009109
			[DebuggerStepThrough]
			public static sbyte vqmovnh_s16(short a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000802 RID: 2050 RVA: 0x0000AF10 File Offset: 0x00009110
			[DebuggerStepThrough]
			public static short vqmovns_s32(int a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000803 RID: 2051 RVA: 0x0000AF17 File Offset: 0x00009117
			[DebuggerStepThrough]
			public static int vqmovnd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000804 RID: 2052 RVA: 0x0000AF1E File Offset: 0x0000911E
			[DebuggerStepThrough]
			public static byte vqmovnh_u16(ushort a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000805 RID: 2053 RVA: 0x0000AF25 File Offset: 0x00009125
			[DebuggerStepThrough]
			public static ushort vqmovns_u32(uint a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000806 RID: 2054 RVA: 0x0000AF2C File Offset: 0x0000912C
			[DebuggerStepThrough]
			public static uint vqmovnd_u64(ulong a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000807 RID: 2055 RVA: 0x0000AF33 File Offset: 0x00009133
			[DebuggerStepThrough]
			public static v128 vqmovn_high_s16(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x0000AF3A File Offset: 0x0000913A
			[DebuggerStepThrough]
			public static v128 vqmovn_high_s32(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000809 RID: 2057 RVA: 0x0000AF41 File Offset: 0x00009141
			[DebuggerStepThrough]
			public static v128 vqmovn_high_s64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x0000AF48 File Offset: 0x00009148
			[DebuggerStepThrough]
			public static v128 vqmovn_high_u16(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x0000AF4F File Offset: 0x0000914F
			[DebuggerStepThrough]
			public static v128 vqmovn_high_u32(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x0000AF56 File Offset: 0x00009156
			[DebuggerStepThrough]
			public static v128 vqmovn_high_u64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x0000AF5D File Offset: 0x0000915D
			[DebuggerStepThrough]
			public static byte vqmovunh_s16(short a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x0000AF64 File Offset: 0x00009164
			[DebuggerStepThrough]
			public static ushort vqmovuns_s32(int a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x0000AF6B File Offset: 0x0000916B
			[DebuggerStepThrough]
			public static uint vqmovund_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x0000AF72 File Offset: 0x00009172
			[DebuggerStepThrough]
			public static v128 vqmovun_high_s16(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x0000AF79 File Offset: 0x00009179
			[DebuggerStepThrough]
			public static v128 vqmovun_high_s32(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x0000AF80 File Offset: 0x00009180
			[DebuggerStepThrough]
			public static v128 vqmovun_high_s64(v64 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x0000AF87 File Offset: 0x00009187
			[DebuggerStepThrough]
			public static v64 vmla_laneq_s16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x0000AF8E File Offset: 0x0000918E
			[DebuggerStepThrough]
			public static v128 vmlaq_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x0000AF95 File Offset: 0x00009195
			[DebuggerStepThrough]
			public static v64 vmla_laneq_s32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x0000AF9C File Offset: 0x0000919C
			[DebuggerStepThrough]
			public static v128 vmlaq_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x0000AFA3 File Offset: 0x000091A3
			[DebuggerStepThrough]
			public static v64 vmla_laneq_u16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x0000AFAA File Offset: 0x000091AA
			[DebuggerStepThrough]
			public static v128 vmlaq_laneq_u16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x0000AFB1 File Offset: 0x000091B1
			[DebuggerStepThrough]
			public static v64 vmla_laneq_u32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x0000AFB8 File Offset: 0x000091B8
			[DebuggerStepThrough]
			public static v128 vmlaq_laneq_u32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x0000AFBF File Offset: 0x000091BF
			[DebuggerStepThrough]
			public static v64 vmla_laneq_f32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0000AFC6 File Offset: 0x000091C6
			[DebuggerStepThrough]
			public static v128 vmlaq_laneq_f32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x0000AFCD File Offset: 0x000091CD
			[DebuggerStepThrough]
			public static v128 vmlal_high_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x0000AFD4 File Offset: 0x000091D4
			[DebuggerStepThrough]
			public static v128 vmlal_high_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x0000AFDB File Offset: 0x000091DB
			[DebuggerStepThrough]
			public static v128 vmlal_high_lane_u16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x0000AFE2 File Offset: 0x000091E2
			[DebuggerStepThrough]
			public static v128 vmlal_high_lane_u32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x0000AFE9 File Offset: 0x000091E9
			[DebuggerStepThrough]
			public static v128 vmlal_laneq_s16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x0000AFF0 File Offset: 0x000091F0
			[DebuggerStepThrough]
			public static v128 vmlal_laneq_s32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000823 RID: 2083 RVA: 0x0000AFF7 File Offset: 0x000091F7
			[DebuggerStepThrough]
			public static v128 vmlal_laneq_u16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x0000AFFE File Offset: 0x000091FE
			[DebuggerStepThrough]
			public static v128 vmlal_laneq_u32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x0000B005 File Offset: 0x00009205
			[DebuggerStepThrough]
			public static v128 vmlal_high_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x0000B00C File Offset: 0x0000920C
			[DebuggerStepThrough]
			public static v128 vmlal_high_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x0000B013 File Offset: 0x00009213
			[DebuggerStepThrough]
			public static v128 vmlal_high_laneq_u16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x0000B01A File Offset: 0x0000921A
			[DebuggerStepThrough]
			public static v128 vmlal_high_laneq_u32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000829 RID: 2089 RVA: 0x0000B021 File Offset: 0x00009221
			[DebuggerStepThrough]
			public static int vqdmlalh_lane_s16(int a0, short a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082A RID: 2090 RVA: 0x0000B028 File Offset: 0x00009228
			[DebuggerStepThrough]
			public static long vqdmlals_lane_s32(long a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x0000B02F File Offset: 0x0000922F
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x0000B036 File Offset: 0x00009236
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x0000B03D File Offset: 0x0000923D
			[DebuggerStepThrough]
			public static v128 vqdmlal_laneq_s16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x0000B044 File Offset: 0x00009244
			[DebuggerStepThrough]
			public static v128 vqdmlal_laneq_s32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x0000B04B File Offset: 0x0000924B
			[DebuggerStepThrough]
			public static int vqdmlalh_laneq_s16(int a0, short a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x0000B052 File Offset: 0x00009252
			[DebuggerStepThrough]
			public static long vqdmlals_laneq_s32(long a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x0000B059 File Offset: 0x00009259
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000832 RID: 2098 RVA: 0x0000B060 File Offset: 0x00009260
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000833 RID: 2099 RVA: 0x0000B067 File Offset: 0x00009267
			[DebuggerStepThrough]
			public static v64 vmls_laneq_s16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000834 RID: 2100 RVA: 0x0000B06E File Offset: 0x0000926E
			[DebuggerStepThrough]
			public static v128 vmlsq_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000835 RID: 2101 RVA: 0x0000B075 File Offset: 0x00009275
			[DebuggerStepThrough]
			public static v64 vmls_laneq_s32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000836 RID: 2102 RVA: 0x0000B07C File Offset: 0x0000927C
			[DebuggerStepThrough]
			public static v128 vmlsq_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x0000B083 File Offset: 0x00009283
			[DebuggerStepThrough]
			public static v64 vmls_laneq_u16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000838 RID: 2104 RVA: 0x0000B08A File Offset: 0x0000928A
			[DebuggerStepThrough]
			public static v128 vmlsq_laneq_u16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000839 RID: 2105 RVA: 0x0000B091 File Offset: 0x00009291
			[DebuggerStepThrough]
			public static v64 vmls_laneq_u32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083A RID: 2106 RVA: 0x0000B098 File Offset: 0x00009298
			[DebuggerStepThrough]
			public static v128 vmlsq_laneq_u32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083B RID: 2107 RVA: 0x0000B09F File Offset: 0x0000929F
			[DebuggerStepThrough]
			public static v64 vmls_laneq_f32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083C RID: 2108 RVA: 0x0000B0A6 File Offset: 0x000092A6
			[DebuggerStepThrough]
			public static v128 vmlsq_laneq_f32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x0000B0AD File Offset: 0x000092AD
			[DebuggerStepThrough]
			public static v128 vmlsl_high_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083E RID: 2110 RVA: 0x0000B0B4 File Offset: 0x000092B4
			[DebuggerStepThrough]
			public static v128 vmlsl_high_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600083F RID: 2111 RVA: 0x0000B0BB File Offset: 0x000092BB
			[DebuggerStepThrough]
			public static v128 vmlsl_high_lane_u16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000840 RID: 2112 RVA: 0x0000B0C2 File Offset: 0x000092C2
			[DebuggerStepThrough]
			public static v128 vmlsl_high_lane_u32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x0000B0C9 File Offset: 0x000092C9
			[DebuggerStepThrough]
			public static v128 vmlsl_laneq_s16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x0000B0D0 File Offset: 0x000092D0
			[DebuggerStepThrough]
			public static v128 vmlsl_laneq_s32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0000B0D7 File Offset: 0x000092D7
			[DebuggerStepThrough]
			public static v128 vmlsl_laneq_u16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x0000B0DE File Offset: 0x000092DE
			[DebuggerStepThrough]
			public static v128 vmlsl_laneq_u32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000845 RID: 2117 RVA: 0x0000B0E5 File Offset: 0x000092E5
			[DebuggerStepThrough]
			public static v128 vmlsl_high_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x0000B0EC File Offset: 0x000092EC
			[DebuggerStepThrough]
			public static v128 vmlsl_high_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000847 RID: 2119 RVA: 0x0000B0F3 File Offset: 0x000092F3
			[DebuggerStepThrough]
			public static v128 vmlsl_high_laneq_u16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x0000B0FA File Offset: 0x000092FA
			[DebuggerStepThrough]
			public static v128 vmlsl_high_laneq_u32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x0000B101 File Offset: 0x00009301
			[DebuggerStepThrough]
			public static int vqdmlslh_lane_s16(int a0, short a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x0000B108 File Offset: 0x00009308
			[DebuggerStepThrough]
			public static long vqdmlsls_lane_s32(long a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x0000B10F File Offset: 0x0000930F
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x0000B116 File Offset: 0x00009316
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084D RID: 2125 RVA: 0x0000B11D File Offset: 0x0000931D
			[DebuggerStepThrough]
			public static v128 vqdmlsl_laneq_s16(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x0000B124 File Offset: 0x00009324
			[DebuggerStepThrough]
			public static v128 vqdmlsl_laneq_s32(v128 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x0000B12B File Offset: 0x0000932B
			[DebuggerStepThrough]
			public static int vqdmlslh_laneq_s16(int a0, short a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000850 RID: 2128 RVA: 0x0000B132 File Offset: 0x00009332
			[DebuggerStepThrough]
			public static long vqdmlsls_laneq_s32(long a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000851 RID: 2129 RVA: 0x0000B139 File Offset: 0x00009339
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000852 RID: 2130 RVA: 0x0000B140 File Offset: 0x00009340
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000853 RID: 2131 RVA: 0x0000B147 File Offset: 0x00009347
			[DebuggerStepThrough]
			public static v64 vmul_n_f64(v64 a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000854 RID: 2132 RVA: 0x0000B14E File Offset: 0x0000934E
			[DebuggerStepThrough]
			public static v128 vmulq_n_f64(v128 a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000855 RID: 2133 RVA: 0x0000B155 File Offset: 0x00009355
			[DebuggerStepThrough]
			[BurstTargetCpu(BurstTargetCpu.ARMV8A_AARCH64)]
			public static v64 vmul_lane_f64(v64 a0, v64 a1, int a2)
			{
				return Arm.Neon.vmul_f64(a0, a1);
			}

			// Token: 0x06000856 RID: 2134 RVA: 0x0000B15E File Offset: 0x0000935E
			[DebuggerStepThrough]
			public static v128 vmulq_lane_f64(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000857 RID: 2135 RVA: 0x0000B165 File Offset: 0x00009365
			[DebuggerStepThrough]
			public static float vmuls_lane_f32(float a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000858 RID: 2136 RVA: 0x0000B16C File Offset: 0x0000936C
			[DebuggerStepThrough]
			public static double vmuld_lane_f64(double a0, v64 a1, int a2)
			{
				return a0 * a1.Double0;
			}

			// Token: 0x06000859 RID: 2137 RVA: 0x0000B176 File Offset: 0x00009376
			[DebuggerStepThrough]
			public static v64 vmul_laneq_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085A RID: 2138 RVA: 0x0000B17D File Offset: 0x0000937D
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x0000B184 File Offset: 0x00009384
			[DebuggerStepThrough]
			public static v64 vmul_laneq_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x0000B18B File Offset: 0x0000938B
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x0000B192 File Offset: 0x00009392
			[DebuggerStepThrough]
			public static v64 vmul_laneq_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x0000B199 File Offset: 0x00009399
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x0000B1A0 File Offset: 0x000093A0
			[DebuggerStepThrough]
			public static v64 vmul_laneq_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x0000B1A7 File Offset: 0x000093A7
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000861 RID: 2145 RVA: 0x0000B1AE File Offset: 0x000093AE
			[DebuggerStepThrough]
			public static v64 vmul_laneq_f32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000862 RID: 2146 RVA: 0x0000B1B5 File Offset: 0x000093B5
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_f32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000863 RID: 2147 RVA: 0x0000B1BC File Offset: 0x000093BC
			[DebuggerStepThrough]
			public static v64 vmul_laneq_f64(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000864 RID: 2148 RVA: 0x0000B1C3 File Offset: 0x000093C3
			[DebuggerStepThrough]
			public static v128 vmulq_laneq_f64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000865 RID: 2149 RVA: 0x0000B1CA File Offset: 0x000093CA
			[DebuggerStepThrough]
			public static float vmuls_laneq_f32(float a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000866 RID: 2150 RVA: 0x0000B1D1 File Offset: 0x000093D1
			[DebuggerStepThrough]
			public static double vmuld_laneq_f64(double a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x0000B1D8 File Offset: 0x000093D8
			[DebuggerStepThrough]
			public static v128 vmull_high_n_s16(v128 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x0000B1DF File Offset: 0x000093DF
			[DebuggerStepThrough]
			public static v128 vmull_high_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x0000B1E6 File Offset: 0x000093E6
			[DebuggerStepThrough]
			public static v128 vmull_high_n_u16(v128 a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0000B1ED File Offset: 0x000093ED
			[DebuggerStepThrough]
			public static v128 vmull_high_n_u32(v128 a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x0000B1F4 File Offset: 0x000093F4
			[DebuggerStepThrough]
			public static v128 vmull_high_lane_s16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x0000B1FB File Offset: 0x000093FB
			[DebuggerStepThrough]
			public static v128 vmull_high_lane_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086D RID: 2157 RVA: 0x0000B202 File Offset: 0x00009402
			[DebuggerStepThrough]
			public static v128 vmull_high_lane_u16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086E RID: 2158 RVA: 0x0000B209 File Offset: 0x00009409
			[DebuggerStepThrough]
			public static v128 vmull_high_lane_u32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600086F RID: 2159 RVA: 0x0000B210 File Offset: 0x00009410
			[DebuggerStepThrough]
			public static v128 vmull_laneq_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x0000B217 File Offset: 0x00009417
			[DebuggerStepThrough]
			public static v128 vmull_laneq_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x0000B21E File Offset: 0x0000941E
			[DebuggerStepThrough]
			public static v128 vmull_laneq_u16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000872 RID: 2162 RVA: 0x0000B225 File Offset: 0x00009425
			[DebuggerStepThrough]
			public static v128 vmull_laneq_u32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x0000B22C File Offset: 0x0000942C
			[DebuggerStepThrough]
			public static v128 vmull_high_laneq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x0000B233 File Offset: 0x00009433
			[DebuggerStepThrough]
			public static v128 vmull_high_laneq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000875 RID: 2165 RVA: 0x0000B23A File Offset: 0x0000943A
			[DebuggerStepThrough]
			public static v128 vmull_high_laneq_u16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000876 RID: 2166 RVA: 0x0000B241 File Offset: 0x00009441
			[DebuggerStepThrough]
			public static v128 vmull_high_laneq_u32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000877 RID: 2167 RVA: 0x0000B248 File Offset: 0x00009448
			[DebuggerStepThrough]
			public static v128 vqdmull_high_n_s16(v128 a0, short a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000878 RID: 2168 RVA: 0x0000B24F File Offset: 0x0000944F
			[DebuggerStepThrough]
			public static v128 vqdmull_high_n_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000879 RID: 2169 RVA: 0x0000B256 File Offset: 0x00009456
			[DebuggerStepThrough]
			public static int vqdmullh_lane_s16(short a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087A RID: 2170 RVA: 0x0000B25D File Offset: 0x0000945D
			[DebuggerStepThrough]
			public static long vqdmulls_lane_s32(int a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087B RID: 2171 RVA: 0x0000B264 File Offset: 0x00009464
			[DebuggerStepThrough]
			public static v128 vqdmull_high_lane_s16(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087C RID: 2172 RVA: 0x0000B26B File Offset: 0x0000946B
			[DebuggerStepThrough]
			public static v128 vqdmull_high_lane_s32(v128 a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087D RID: 2173 RVA: 0x0000B272 File Offset: 0x00009472
			[DebuggerStepThrough]
			public static v128 vqdmull_laneq_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x0000B279 File Offset: 0x00009479
			[DebuggerStepThrough]
			public static v128 vqdmull_laneq_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600087F RID: 2175 RVA: 0x0000B280 File Offset: 0x00009480
			[DebuggerStepThrough]
			public static int vqdmullh_laneq_s16(short a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x0000B287 File Offset: 0x00009487
			[DebuggerStepThrough]
			public static long vqdmulls_laneq_s32(int a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x0000B28E File Offset: 0x0000948E
			[DebuggerStepThrough]
			public static v128 vqdmull_high_laneq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000882 RID: 2178 RVA: 0x0000B295 File Offset: 0x00009495
			[DebuggerStepThrough]
			public static v128 vqdmull_high_laneq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x0000B29C File Offset: 0x0000949C
			[DebuggerStepThrough]
			public static short vqdmulhh_lane_s16(short a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x0000B2A3 File Offset: 0x000094A3
			[DebuggerStepThrough]
			public static int vqdmulhs_lane_s32(int a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x0000B2AA File Offset: 0x000094AA
			[DebuggerStepThrough]
			public static v64 vqdmulh_laneq_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x0000B2B1 File Offset: 0x000094B1
			[DebuggerStepThrough]
			public static v128 vqdmulhq_laneq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x0000B2B8 File Offset: 0x000094B8
			[DebuggerStepThrough]
			public static v64 vqdmulh_laneq_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x0000B2BF File Offset: 0x000094BF
			[DebuggerStepThrough]
			public static v128 vqdmulhq_laneq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0000B2C6 File Offset: 0x000094C6
			[DebuggerStepThrough]
			public static short vqdmulhh_laneq_s16(short a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x0000B2CD File Offset: 0x000094CD
			[DebuggerStepThrough]
			public static int vqdmulhs_laneq_s32(int a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x0000B2D4 File Offset: 0x000094D4
			[DebuggerStepThrough]
			public static short vqrdmulhh_lane_s16(short a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x0000B2DB File Offset: 0x000094DB
			[DebuggerStepThrough]
			public static int vqrdmulhs_lane_s32(int a0, v64 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x0000B2E2 File Offset: 0x000094E2
			[DebuggerStepThrough]
			public static v64 vqrdmulh_laneq_s16(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x0000B2E9 File Offset: 0x000094E9
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_laneq_s16(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x0000B2F0 File Offset: 0x000094F0
			[DebuggerStepThrough]
			public static v64 vqrdmulh_laneq_s32(v64 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x0000B2F7 File Offset: 0x000094F7
			[DebuggerStepThrough]
			public static v128 vqrdmulhq_laneq_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x0000B2FE File Offset: 0x000094FE
			[DebuggerStepThrough]
			public static short vqrdmulhh_laneq_s16(short a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x0000B305 File Offset: 0x00009505
			[DebuggerStepThrough]
			public static int vqrdmulhs_laneq_s32(int a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x0000B30C File Offset: 0x0000950C
			[DebuggerStepThrough]
			public static v128 vmlal_high_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000894 RID: 2196 RVA: 0x0000B313 File Offset: 0x00009513
			[DebuggerStepThrough]
			public static v128 vmlal_high_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x0000B31A File Offset: 0x0000951A
			[DebuggerStepThrough]
			public static v128 vmlal_high_n_u16(v128 a0, v128 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x0000B321 File Offset: 0x00009521
			[DebuggerStepThrough]
			public static v128 vmlal_high_n_u32(v128 a0, v128 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000897 RID: 2199 RVA: 0x0000B328 File Offset: 0x00009528
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x0000B32F File Offset: 0x0000952F
			[DebuggerStepThrough]
			public static v128 vqdmlal_high_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000899 RID: 2201 RVA: 0x0000B336 File Offset: 0x00009536
			[DebuggerStepThrough]
			public static v128 vmlsl_high_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x0000B33D File Offset: 0x0000953D
			[DebuggerStepThrough]
			public static v128 vmlsl_high_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x0000B344 File Offset: 0x00009544
			[DebuggerStepThrough]
			public static v128 vmlsl_high_n_u16(v128 a0, v128 a1, ushort a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x0000B34B File Offset: 0x0000954B
			[DebuggerStepThrough]
			public static v128 vmlsl_high_n_u32(v128 a0, v128 a1, uint a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x0000B352 File Offset: 0x00009552
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_n_s16(v128 a0, v128 a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0000B359 File Offset: 0x00009559
			[DebuggerStepThrough]
			public static v128 vqdmlsl_high_n_s32(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0000B360 File Offset: 0x00009560
			[DebuggerStepThrough]
			public static v64 vabs_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0000B367 File Offset: 0x00009567
			[DebuggerStepThrough]
			public static long vabsd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x0000B36E File Offset: 0x0000956E
			[DebuggerStepThrough]
			public static v128 vabsq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x0000B375 File Offset: 0x00009575
			[DebuggerStepThrough]
			public static v64 vabs_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x0000B37C File Offset: 0x0000957C
			[DebuggerStepThrough]
			public static v128 vabsq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x0000B383 File Offset: 0x00009583
			[DebuggerStepThrough]
			public static v64 vqabs_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A5 RID: 2213 RVA: 0x0000B38A File Offset: 0x0000958A
			[DebuggerStepThrough]
			public static v128 vqabsq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x0000B391 File Offset: 0x00009591
			[DebuggerStepThrough]
			public static sbyte vqabsb_s8(sbyte a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x0000B398 File Offset: 0x00009598
			[DebuggerStepThrough]
			public static short vqabsh_s16(short a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A8 RID: 2216 RVA: 0x0000B39F File Offset: 0x0000959F
			[DebuggerStepThrough]
			public static int vqabss_s32(int a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x0000B3A6 File Offset: 0x000095A6
			[DebuggerStepThrough]
			public static long vqabsd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x0000B3AD File Offset: 0x000095AD
			[DebuggerStepThrough]
			public static v64 vneg_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x0000B3B4 File Offset: 0x000095B4
			[DebuggerStepThrough]
			public static long vnegd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AC RID: 2220 RVA: 0x0000B3BB File Offset: 0x000095BB
			[DebuggerStepThrough]
			public static v128 vnegq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x0000B3C2 File Offset: 0x000095C2
			[DebuggerStepThrough]
			public static v64 vneg_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AE RID: 2222 RVA: 0x0000B3C9 File Offset: 0x000095C9
			[DebuggerStepThrough]
			public static v128 vnegq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x0000B3D0 File Offset: 0x000095D0
			[DebuggerStepThrough]
			public static v64 vqneg_s64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x0000B3D7 File Offset: 0x000095D7
			[DebuggerStepThrough]
			public static v128 vqnegq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B1 RID: 2225 RVA: 0x0000B3DE File Offset: 0x000095DE
			[DebuggerStepThrough]
			public static sbyte vqnegb_s8(sbyte a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x0000B3E5 File Offset: 0x000095E5
			[DebuggerStepThrough]
			public static short vqnegh_s16(short a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x0000B3EC File Offset: 0x000095EC
			[DebuggerStepThrough]
			public static int vqnegs_s32(int a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x0000B3F3 File Offset: 0x000095F3
			[DebuggerStepThrough]
			public static long vqnegd_s64(long a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x0000B3FA File Offset: 0x000095FA
			[DebuggerStepThrough]
			public static v64 vrecpe_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x0000B401 File Offset: 0x00009601
			[DebuggerStepThrough]
			public static v128 vrecpeq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x0000B408 File Offset: 0x00009608
			[DebuggerStepThrough]
			public static float vrecpes_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x0000B40F File Offset: 0x0000960F
			[DebuggerStepThrough]
			public static double vrecped_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008B9 RID: 2233 RVA: 0x0000B416 File Offset: 0x00009616
			[DebuggerStepThrough]
			public static v64 vrecps_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BA RID: 2234 RVA: 0x0000B41D File Offset: 0x0000961D
			[DebuggerStepThrough]
			public static v128 vrecpsq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BB RID: 2235 RVA: 0x0000B424 File Offset: 0x00009624
			[DebuggerStepThrough]
			public static float vrecpss_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BC RID: 2236 RVA: 0x0000B42B File Offset: 0x0000962B
			[DebuggerStepThrough]
			public static double vrecpsd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x0000B432 File Offset: 0x00009632
			[DebuggerStepThrough]
			public static v64 vsqrt_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x0000B439 File Offset: 0x00009639
			[DebuggerStepThrough]
			public static v128 vsqrtq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008BF RID: 2239 RVA: 0x0000B440 File Offset: 0x00009640
			[DebuggerStepThrough]
			public static v64 vsqrt_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C0 RID: 2240 RVA: 0x0000B447 File Offset: 0x00009647
			[DebuggerStepThrough]
			public static v128 vsqrtq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C1 RID: 2241 RVA: 0x0000B44E File Offset: 0x0000964E
			[DebuggerStepThrough]
			public static v64 vrsqrte_f64(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C2 RID: 2242 RVA: 0x0000B455 File Offset: 0x00009655
			[DebuggerStepThrough]
			public static v128 vrsqrteq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C3 RID: 2243 RVA: 0x0000B45C File Offset: 0x0000965C
			[DebuggerStepThrough]
			public static float vrsqrtes_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C4 RID: 2244 RVA: 0x0000B463 File Offset: 0x00009663
			[DebuggerStepThrough]
			public static double vrsqrted_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x0000B46A File Offset: 0x0000966A
			[DebuggerStepThrough]
			public static v64 vrsqrts_f64(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C6 RID: 2246 RVA: 0x0000B471 File Offset: 0x00009671
			[DebuggerStepThrough]
			public static v128 vrsqrtsq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x0000B478 File Offset: 0x00009678
			[DebuggerStepThrough]
			public static float vrsqrtss_f32(float a0, float a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C8 RID: 2248 RVA: 0x0000B47F File Offset: 0x0000967F
			[DebuggerStepThrough]
			public static double vrsqrtsd_f64(double a0, double a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008C9 RID: 2249 RVA: 0x0000B486 File Offset: 0x00009686
			[DebuggerStepThrough]
			public static v64 vbsl_f64(v64 a0, v64 a1, v64 a2)
			{
				return Arm.Neon.vbsl_s8(a0, a1, a2);
			}

			// Token: 0x060008CA RID: 2250 RVA: 0x0000B490 File Offset: 0x00009690
			[DebuggerStepThrough]
			public static v128 vbslq_f64(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vbslq_s8(a0, a1, a2);
			}

			// Token: 0x060008CB RID: 2251 RVA: 0x0000B49A File Offset: 0x0000969A
			[DebuggerStepThrough]
			public static v64 vcopy_lane_s8(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008CC RID: 2252 RVA: 0x0000B4A1 File Offset: 0x000096A1
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_s8(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008CD RID: 2253 RVA: 0x0000B4A8 File Offset: 0x000096A8
			[DebuggerStepThrough]
			public static v64 vcopy_lane_s16(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008CE RID: 2254 RVA: 0x0000B4AF File Offset: 0x000096AF
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_s16(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008CF RID: 2255 RVA: 0x0000B4B6 File Offset: 0x000096B6
			[DebuggerStepThrough]
			public static v64 vcopy_lane_s32(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D0 RID: 2256 RVA: 0x0000B4BD File Offset: 0x000096BD
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_s32(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D1 RID: 2257 RVA: 0x0000B4C4 File Offset: 0x000096C4
			[DebuggerStepThrough]
			public static v64 vcopy_lane_s64(v64 a0, int a1, v64 a2, int a3)
			{
				return a2;
			}

			// Token: 0x060008D2 RID: 2258 RVA: 0x0000B4C7 File Offset: 0x000096C7
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_s64(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D3 RID: 2259 RVA: 0x0000B4CE File Offset: 0x000096CE
			[DebuggerStepThrough]
			public static v64 vcopy_lane_u8(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D4 RID: 2260 RVA: 0x0000B4D5 File Offset: 0x000096D5
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_u8(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D5 RID: 2261 RVA: 0x0000B4DC File Offset: 0x000096DC
			[DebuggerStepThrough]
			public static v64 vcopy_lane_u16(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D6 RID: 2262 RVA: 0x0000B4E3 File Offset: 0x000096E3
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_u16(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D7 RID: 2263 RVA: 0x0000B4EA File Offset: 0x000096EA
			[DebuggerStepThrough]
			public static v64 vcopy_lane_u32(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D8 RID: 2264 RVA: 0x0000B4F1 File Offset: 0x000096F1
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_u32(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008D9 RID: 2265 RVA: 0x0000B4F8 File Offset: 0x000096F8
			[DebuggerStepThrough]
			public static v64 vcopy_lane_u64(v64 a0, int a1, v64 a2, int a3)
			{
				return a2;
			}

			// Token: 0x060008DA RID: 2266 RVA: 0x0000B4FB File Offset: 0x000096FB
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_u64(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x0000B502 File Offset: 0x00009702
			[DebuggerStepThrough]
			public static v64 vcopy_lane_f32(v64 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x0000B509 File Offset: 0x00009709
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_f32(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x0000B510 File Offset: 0x00009710
			[DebuggerStepThrough]
			public static v64 vcopy_lane_f64(v64 a0, int a1, v64 a2, int a3)
			{
				return a2;
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x0000B513 File Offset: 0x00009713
			[DebuggerStepThrough]
			public static v128 vcopyq_lane_f64(v128 a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x0000B51A File Offset: 0x0000971A
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_s8(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E0 RID: 2272 RVA: 0x0000B521 File Offset: 0x00009721
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_s8(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E1 RID: 2273 RVA: 0x0000B528 File Offset: 0x00009728
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_s16(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x0000B52F File Offset: 0x0000972F
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_s16(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x0000B536 File Offset: 0x00009736
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_s32(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x0000B53D File Offset: 0x0000973D
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_s32(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x0000B544 File Offset: 0x00009744
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_s64(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E6 RID: 2278 RVA: 0x0000B54B File Offset: 0x0000974B
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_s64(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E7 RID: 2279 RVA: 0x0000B552 File Offset: 0x00009752
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_u8(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x0000B559 File Offset: 0x00009759
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_u8(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008E9 RID: 2281 RVA: 0x0000B560 File Offset: 0x00009760
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_u16(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x0000B567 File Offset: 0x00009767
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_u16(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008EB RID: 2283 RVA: 0x0000B56E File Offset: 0x0000976E
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_u32(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008EC RID: 2284 RVA: 0x0000B575 File Offset: 0x00009775
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_u32(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008ED RID: 2285 RVA: 0x0000B57C File Offset: 0x0000977C
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_u64(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008EE RID: 2286 RVA: 0x0000B583 File Offset: 0x00009783
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_u64(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008EF RID: 2287 RVA: 0x0000B58A File Offset: 0x0000978A
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_f32(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F0 RID: 2288 RVA: 0x0000B591 File Offset: 0x00009791
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_f32(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F1 RID: 2289 RVA: 0x0000B598 File Offset: 0x00009798
			[DebuggerStepThrough]
			public static v64 vcopy_laneq_f64(v64 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x0000B59F File Offset: 0x0000979F
			[DebuggerStepThrough]
			public static v128 vcopyq_laneq_f64(v128 a0, int a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F3 RID: 2291 RVA: 0x0000B5A6 File Offset: 0x000097A6
			[DebuggerStepThrough]
			public static v64 vrbit_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F4 RID: 2292 RVA: 0x0000B5AD File Offset: 0x000097AD
			[DebuggerStepThrough]
			public static v128 vrbitq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008F5 RID: 2293 RVA: 0x0000B5B4 File Offset: 0x000097B4
			[DebuggerStepThrough]
			public static v64 vrbit_u8(v64 a0)
			{
				return Arm.Neon.vrbit_s8(a0);
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x0000B5BC File Offset: 0x000097BC
			[DebuggerStepThrough]
			public static v128 vrbitq_u8(v128 a0)
			{
				return Arm.Neon.vrbitq_s8(a0);
			}

			// Token: 0x060008F7 RID: 2295 RVA: 0x0000B5C4 File Offset: 0x000097C4
			[DebuggerStepThrough]
			public static v64 vdup_lane_f64(v64 a0, int a1)
			{
				return a0;
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x0000B5C7 File Offset: 0x000097C7
			[DebuggerStepThrough]
			public static v128 vdupq_lane_f64(v64 a0, int a1)
			{
				return new v128(a0, a0);
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x0000B5D0 File Offset: 0x000097D0
			[DebuggerStepThrough]
			public static v64 vdup_laneq_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x0000B5D7 File Offset: 0x000097D7
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x0000B5DE File Offset: 0x000097DE
			[DebuggerStepThrough]
			public static v64 vdup_laneq_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x0000B5E5 File Offset: 0x000097E5
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FD RID: 2301 RVA: 0x0000B5EC File Offset: 0x000097EC
			[DebuggerStepThrough]
			public static v64 vdup_laneq_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FE RID: 2302 RVA: 0x0000B5F3 File Offset: 0x000097F3
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060008FF RID: 2303 RVA: 0x0000B5FA File Offset: 0x000097FA
			[DebuggerStepThrough]
			public static v64 vdup_laneq_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000900 RID: 2304 RVA: 0x0000B601 File Offset: 0x00009801
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000901 RID: 2305 RVA: 0x0000B608 File Offset: 0x00009808
			[DebuggerStepThrough]
			public static v64 vdup_laneq_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000902 RID: 2306 RVA: 0x0000B60F File Offset: 0x0000980F
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000903 RID: 2307 RVA: 0x0000B616 File Offset: 0x00009816
			[DebuggerStepThrough]
			public static v64 vdup_laneq_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000904 RID: 2308 RVA: 0x0000B61D File Offset: 0x0000981D
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000905 RID: 2309 RVA: 0x0000B624 File Offset: 0x00009824
			[DebuggerStepThrough]
			public static v64 vdup_laneq_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000906 RID: 2310 RVA: 0x0000B62B File Offset: 0x0000982B
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000907 RID: 2311 RVA: 0x0000B632 File Offset: 0x00009832
			[DebuggerStepThrough]
			public static v64 vdup_laneq_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000908 RID: 2312 RVA: 0x0000B639 File Offset: 0x00009839
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000909 RID: 2313 RVA: 0x0000B640 File Offset: 0x00009840
			[DebuggerStepThrough]
			public static v64 vdup_laneq_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090A RID: 2314 RVA: 0x0000B647 File Offset: 0x00009847
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090B RID: 2315 RVA: 0x0000B64E File Offset: 0x0000984E
			[DebuggerStepThrough]
			public static v64 vdup_laneq_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090C RID: 2316 RVA: 0x0000B655 File Offset: 0x00009855
			[DebuggerStepThrough]
			public static v128 vdupq_laneq_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090D RID: 2317 RVA: 0x0000B65C File Offset: 0x0000985C
			[DebuggerStepThrough]
			public static sbyte vdupb_lane_s8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090E RID: 2318 RVA: 0x0000B663 File Offset: 0x00009863
			[DebuggerStepThrough]
			public static short vduph_lane_s16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600090F RID: 2319 RVA: 0x0000B66A File Offset: 0x0000986A
			[DebuggerStepThrough]
			public static int vdups_lane_s32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000910 RID: 2320 RVA: 0x0000B671 File Offset: 0x00009871
			[DebuggerStepThrough]
			public static long vdupd_lane_s64(v64 a0, int a1)
			{
				return a0.SLong0;
			}

			// Token: 0x06000911 RID: 2321 RVA: 0x0000B679 File Offset: 0x00009879
			[DebuggerStepThrough]
			public static byte vdupb_lane_u8(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000912 RID: 2322 RVA: 0x0000B680 File Offset: 0x00009880
			[DebuggerStepThrough]
			public static ushort vduph_lane_u16(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000913 RID: 2323 RVA: 0x0000B687 File Offset: 0x00009887
			[DebuggerStepThrough]
			public static uint vdups_lane_u32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000914 RID: 2324 RVA: 0x0000B68E File Offset: 0x0000988E
			[DebuggerStepThrough]
			public static ulong vdupd_lane_u64(v64 a0, int a1)
			{
				return a0.ULong0;
			}

			// Token: 0x06000915 RID: 2325 RVA: 0x0000B696 File Offset: 0x00009896
			[DebuggerStepThrough]
			public static float vdups_lane_f32(v64 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000916 RID: 2326 RVA: 0x0000B69D File Offset: 0x0000989D
			[DebuggerStepThrough]
			public static double vdupd_lane_f64(v64 a0, int a1)
			{
				return a0.Double0;
			}

			// Token: 0x06000917 RID: 2327 RVA: 0x0000B6A5 File Offset: 0x000098A5
			[DebuggerStepThrough]
			public static sbyte vdupb_laneq_s8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000918 RID: 2328 RVA: 0x0000B6AC File Offset: 0x000098AC
			[DebuggerStepThrough]
			public static short vduph_laneq_s16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000919 RID: 2329 RVA: 0x0000B6B3 File Offset: 0x000098B3
			[DebuggerStepThrough]
			public static int vdups_laneq_s32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091A RID: 2330 RVA: 0x0000B6BA File Offset: 0x000098BA
			[DebuggerStepThrough]
			public static long vdupd_laneq_s64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091B RID: 2331 RVA: 0x0000B6C1 File Offset: 0x000098C1
			[DebuggerStepThrough]
			public static byte vdupb_laneq_u8(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091C RID: 2332 RVA: 0x0000B6C8 File Offset: 0x000098C8
			[DebuggerStepThrough]
			public static ushort vduph_laneq_u16(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091D RID: 2333 RVA: 0x0000B6CF File Offset: 0x000098CF
			[DebuggerStepThrough]
			public static uint vdups_laneq_u32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091E RID: 2334 RVA: 0x0000B6D6 File Offset: 0x000098D6
			[DebuggerStepThrough]
			public static ulong vdupd_laneq_u64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x0000B6DD File Offset: 0x000098DD
			[DebuggerStepThrough]
			public static float vdups_laneq_f32(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x0000B6E4 File Offset: 0x000098E4
			[DebuggerStepThrough]
			public static double vdupd_laneq_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x0000B6EB File Offset: 0x000098EB
			[DebuggerStepThrough]
			public static v128 vpaddq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x0000B6F2 File Offset: 0x000098F2
			[DebuggerStepThrough]
			public static v128 vpaddq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000923 RID: 2339 RVA: 0x0000B6F9 File Offset: 0x000098F9
			[DebuggerStepThrough]
			public static v128 vpaddq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000924 RID: 2340 RVA: 0x0000B700 File Offset: 0x00009900
			[DebuggerStepThrough]
			public static v128 vpaddq_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000925 RID: 2341 RVA: 0x0000B707 File Offset: 0x00009907
			[DebuggerStepThrough]
			public static v128 vpaddq_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vpaddq_s8(a0, a1);
			}

			// Token: 0x06000926 RID: 2342 RVA: 0x0000B710 File Offset: 0x00009910
			[DebuggerStepThrough]
			public static v128 vpaddq_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vpaddq_s16(a0, a1);
			}

			// Token: 0x06000927 RID: 2343 RVA: 0x0000B719 File Offset: 0x00009919
			[DebuggerStepThrough]
			public static v128 vpaddq_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vpaddq_s32(a0, a1);
			}

			// Token: 0x06000928 RID: 2344 RVA: 0x0000B722 File Offset: 0x00009922
			[DebuggerStepThrough]
			public static v128 vpaddq_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vpaddq_s64(a0, a1);
			}

			// Token: 0x06000929 RID: 2345 RVA: 0x0000B72B File Offset: 0x0000992B
			[DebuggerStepThrough]
			public static v128 vpaddq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092A RID: 2346 RVA: 0x0000B732 File Offset: 0x00009932
			[DebuggerStepThrough]
			public static v128 vpaddq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092B RID: 2347 RVA: 0x0000B739 File Offset: 0x00009939
			[DebuggerStepThrough]
			public static v128 vpmaxq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092C RID: 2348 RVA: 0x0000B740 File Offset: 0x00009940
			[DebuggerStepThrough]
			public static v128 vpmaxq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092D RID: 2349 RVA: 0x0000B747 File Offset: 0x00009947
			[DebuggerStepThrough]
			public static v128 vpmaxq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092E RID: 2350 RVA: 0x0000B74E File Offset: 0x0000994E
			[DebuggerStepThrough]
			public static v128 vpmaxq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600092F RID: 2351 RVA: 0x0000B755 File Offset: 0x00009955
			[DebuggerStepThrough]
			public static v128 vpmaxq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000930 RID: 2352 RVA: 0x0000B75C File Offset: 0x0000995C
			[DebuggerStepThrough]
			public static v128 vpmaxq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000931 RID: 2353 RVA: 0x0000B763 File Offset: 0x00009963
			[DebuggerStepThrough]
			public static v128 vpmaxq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x0000B76A File Offset: 0x0000996A
			[DebuggerStepThrough]
			public static v128 vpmaxq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x0000B771 File Offset: 0x00009971
			[DebuggerStepThrough]
			public static v128 vpminq_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000934 RID: 2356 RVA: 0x0000B778 File Offset: 0x00009978
			[DebuggerStepThrough]
			public static v128 vpminq_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000935 RID: 2357 RVA: 0x0000B77F File Offset: 0x0000997F
			[DebuggerStepThrough]
			public static v128 vpminq_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000936 RID: 2358 RVA: 0x0000B786 File Offset: 0x00009986
			[DebuggerStepThrough]
			public static v128 vpminq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000937 RID: 2359 RVA: 0x0000B78D File Offset: 0x0000998D
			[DebuggerStepThrough]
			public static v128 vpminq_u16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000938 RID: 2360 RVA: 0x0000B794 File Offset: 0x00009994
			[DebuggerStepThrough]
			public static v128 vpminq_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000939 RID: 2361 RVA: 0x0000B79B File Offset: 0x0000999B
			[DebuggerStepThrough]
			public static v128 vpminq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093A RID: 2362 RVA: 0x0000B7A2 File Offset: 0x000099A2
			[DebuggerStepThrough]
			public static v128 vpminq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093B RID: 2363 RVA: 0x0000B7A9 File Offset: 0x000099A9
			[DebuggerStepThrough]
			public static v64 vpmaxnm_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093C RID: 2364 RVA: 0x0000B7B0 File Offset: 0x000099B0
			[DebuggerStepThrough]
			public static v128 vpmaxnmq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093D RID: 2365 RVA: 0x0000B7B7 File Offset: 0x000099B7
			[DebuggerStepThrough]
			public static v128 vpmaxnmq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093E RID: 2366 RVA: 0x0000B7BE File Offset: 0x000099BE
			[DebuggerStepThrough]
			public static v64 vpminnm_f32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600093F RID: 2367 RVA: 0x0000B7C5 File Offset: 0x000099C5
			[DebuggerStepThrough]
			public static v128 vpminnmq_f32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000940 RID: 2368 RVA: 0x0000B7CC File Offset: 0x000099CC
			[DebuggerStepThrough]
			public static v128 vpminnmq_f64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000941 RID: 2369 RVA: 0x0000B7D3 File Offset: 0x000099D3
			[DebuggerStepThrough]
			public static long vpaddd_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x0000B7DA File Offset: 0x000099DA
			[DebuggerStepThrough]
			public static ulong vpaddd_u64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000943 RID: 2371 RVA: 0x0000B7E1 File Offset: 0x000099E1
			[DebuggerStepThrough]
			public static float vpadds_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000944 RID: 2372 RVA: 0x0000B7E8 File Offset: 0x000099E8
			[DebuggerStepThrough]
			public static double vpaddd_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000945 RID: 2373 RVA: 0x0000B7EF File Offset: 0x000099EF
			[DebuggerStepThrough]
			public static float vpmaxs_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x0000B7F6 File Offset: 0x000099F6
			[DebuggerStepThrough]
			public static double vpmaxqd_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000947 RID: 2375 RVA: 0x0000B7FD File Offset: 0x000099FD
			[DebuggerStepThrough]
			public static float vpmins_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000948 RID: 2376 RVA: 0x0000B804 File Offset: 0x00009A04
			[DebuggerStepThrough]
			public static double vpminqd_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0000B80B File Offset: 0x00009A0B
			[DebuggerStepThrough]
			public static float vpmaxnms_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0000B812 File Offset: 0x00009A12
			[DebuggerStepThrough]
			public static double vpmaxnmqd_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0000B819 File Offset: 0x00009A19
			[DebuggerStepThrough]
			public static float vpminnms_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0000B820 File Offset: 0x00009A20
			[DebuggerStepThrough]
			public static double vpminnmqd_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094D RID: 2381 RVA: 0x0000B827 File Offset: 0x00009A27
			[DebuggerStepThrough]
			public static sbyte vaddv_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094E RID: 2382 RVA: 0x0000B82E File Offset: 0x00009A2E
			[DebuggerStepThrough]
			public static sbyte vaddvq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0000B835 File Offset: 0x00009A35
			[DebuggerStepThrough]
			public static short vaddv_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0000B83C File Offset: 0x00009A3C
			[DebuggerStepThrough]
			public static short vaddvq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x0000B843 File Offset: 0x00009A43
			[DebuggerStepThrough]
			public static int vaddv_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000952 RID: 2386 RVA: 0x0000B84A File Offset: 0x00009A4A
			[DebuggerStepThrough]
			public static int vaddvq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000953 RID: 2387 RVA: 0x0000B851 File Offset: 0x00009A51
			[DebuggerStepThrough]
			public static long vaddvq_s64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000954 RID: 2388 RVA: 0x0000B858 File Offset: 0x00009A58
			[DebuggerStepThrough]
			public static byte vaddv_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000955 RID: 2389 RVA: 0x0000B85F File Offset: 0x00009A5F
			[DebuggerStepThrough]
			public static byte vaddvq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000956 RID: 2390 RVA: 0x0000B866 File Offset: 0x00009A66
			[DebuggerStepThrough]
			public static ushort vaddv_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000957 RID: 2391 RVA: 0x0000B86D File Offset: 0x00009A6D
			[DebuggerStepThrough]
			public static ushort vaddvq_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000958 RID: 2392 RVA: 0x0000B874 File Offset: 0x00009A74
			[DebuggerStepThrough]
			public static uint vaddv_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000959 RID: 2393 RVA: 0x0000B87B File Offset: 0x00009A7B
			[DebuggerStepThrough]
			public static uint vaddvq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095A RID: 2394 RVA: 0x0000B882 File Offset: 0x00009A82
			[DebuggerStepThrough]
			public static ulong vaddvq_u64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095B RID: 2395 RVA: 0x0000B889 File Offset: 0x00009A89
			[DebuggerStepThrough]
			public static float vaddv_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095C RID: 2396 RVA: 0x0000B890 File Offset: 0x00009A90
			[DebuggerStepThrough]
			public static float vaddvq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x0000B897 File Offset: 0x00009A97
			[DebuggerStepThrough]
			public static double vaddvq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095E RID: 2398 RVA: 0x0000B89E File Offset: 0x00009A9E
			[DebuggerStepThrough]
			public static short vaddlv_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600095F RID: 2399 RVA: 0x0000B8A5 File Offset: 0x00009AA5
			[DebuggerStepThrough]
			public static short vaddlvq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x0000B8AC File Offset: 0x00009AAC
			[DebuggerStepThrough]
			public static int vaddlv_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000961 RID: 2401 RVA: 0x0000B8B3 File Offset: 0x00009AB3
			[DebuggerStepThrough]
			public static int vaddlvq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000962 RID: 2402 RVA: 0x0000B8BA File Offset: 0x00009ABA
			[DebuggerStepThrough]
			public static long vaddlv_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000963 RID: 2403 RVA: 0x0000B8C1 File Offset: 0x00009AC1
			[DebuggerStepThrough]
			public static long vaddlvq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000964 RID: 2404 RVA: 0x0000B8C8 File Offset: 0x00009AC8
			[DebuggerStepThrough]
			public static ushort vaddlv_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x0000B8CF File Offset: 0x00009ACF
			[DebuggerStepThrough]
			public static ushort vaddlvq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x0000B8D6 File Offset: 0x00009AD6
			[DebuggerStepThrough]
			public static uint vaddlv_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000967 RID: 2407 RVA: 0x0000B8DD File Offset: 0x00009ADD
			[DebuggerStepThrough]
			public static uint vaddlvq_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x0000B8E4 File Offset: 0x00009AE4
			[DebuggerStepThrough]
			public static ulong vaddlv_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000969 RID: 2409 RVA: 0x0000B8EB File Offset: 0x00009AEB
			[DebuggerStepThrough]
			public static ulong vaddlvq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x0000B8F2 File Offset: 0x00009AF2
			[DebuggerStepThrough]
			public static sbyte vmaxv_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x0000B8F9 File Offset: 0x00009AF9
			[DebuggerStepThrough]
			public static sbyte vmaxvq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x0000B900 File Offset: 0x00009B00
			[DebuggerStepThrough]
			public static short vmaxv_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x0000B907 File Offset: 0x00009B07
			[DebuggerStepThrough]
			public static short vmaxvq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096E RID: 2414 RVA: 0x0000B90E File Offset: 0x00009B0E
			[DebuggerStepThrough]
			public static int vmaxv_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600096F RID: 2415 RVA: 0x0000B915 File Offset: 0x00009B15
			[DebuggerStepThrough]
			public static int vmaxvq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000970 RID: 2416 RVA: 0x0000B91C File Offset: 0x00009B1C
			[DebuggerStepThrough]
			public static byte vmaxv_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000971 RID: 2417 RVA: 0x0000B923 File Offset: 0x00009B23
			[DebuggerStepThrough]
			public static byte vmaxvq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000972 RID: 2418 RVA: 0x0000B92A File Offset: 0x00009B2A
			[DebuggerStepThrough]
			public static ushort vmaxv_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000973 RID: 2419 RVA: 0x0000B931 File Offset: 0x00009B31
			[DebuggerStepThrough]
			public static ushort vmaxvq_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000974 RID: 2420 RVA: 0x0000B938 File Offset: 0x00009B38
			[DebuggerStepThrough]
			public static uint vmaxv_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000975 RID: 2421 RVA: 0x0000B93F File Offset: 0x00009B3F
			[DebuggerStepThrough]
			public static uint vmaxvq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x0000B946 File Offset: 0x00009B46
			[DebuggerStepThrough]
			public static float vmaxv_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x0000B94D File Offset: 0x00009B4D
			[DebuggerStepThrough]
			public static float vmaxvq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x0000B954 File Offset: 0x00009B54
			[DebuggerStepThrough]
			public static double vmaxvq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x0000B95B File Offset: 0x00009B5B
			[DebuggerStepThrough]
			public static sbyte vminv_s8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x0000B962 File Offset: 0x00009B62
			[DebuggerStepThrough]
			public static sbyte vminvq_s8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x0000B969 File Offset: 0x00009B69
			[DebuggerStepThrough]
			public static short vminv_s16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x0000B970 File Offset: 0x00009B70
			[DebuggerStepThrough]
			public static short vminvq_s16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x0000B977 File Offset: 0x00009B77
			[DebuggerStepThrough]
			public static int vminv_s32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0000B97E File Offset: 0x00009B7E
			[DebuggerStepThrough]
			public static int vminvq_s32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0000B985 File Offset: 0x00009B85
			[DebuggerStepThrough]
			public static byte vminv_u8(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0000B98C File Offset: 0x00009B8C
			[DebuggerStepThrough]
			public static byte vminvq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x0000B993 File Offset: 0x00009B93
			[DebuggerStepThrough]
			public static ushort vminv_u16(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x0000B99A File Offset: 0x00009B9A
			[DebuggerStepThrough]
			public static ushort vminvq_u16(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x0000B9A1 File Offset: 0x00009BA1
			[DebuggerStepThrough]
			public static uint vminv_u32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000984 RID: 2436 RVA: 0x0000B9A8 File Offset: 0x00009BA8
			[DebuggerStepThrough]
			public static uint vminvq_u32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x0000B9AF File Offset: 0x00009BAF
			[DebuggerStepThrough]
			public static float vminv_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x0000B9B6 File Offset: 0x00009BB6
			[DebuggerStepThrough]
			public static float vminvq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x0000B9BD File Offset: 0x00009BBD
			[DebuggerStepThrough]
			public static double vminvq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x0000B9C4 File Offset: 0x00009BC4
			[DebuggerStepThrough]
			public static float vmaxnmv_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x0000B9CB File Offset: 0x00009BCB
			[DebuggerStepThrough]
			public static float vmaxnmvq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x0000B9D2 File Offset: 0x00009BD2
			[DebuggerStepThrough]
			public static double vmaxnmvq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x0000B9D9 File Offset: 0x00009BD9
			[DebuggerStepThrough]
			public static float vminnmv_f32(v64 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x0000B9E0 File Offset: 0x00009BE0
			[DebuggerStepThrough]
			public static float vminnmvq_f32(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x0000B9E7 File Offset: 0x00009BE7
			[DebuggerStepThrough]
			public static double vminnmvq_f64(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x0000B9EE File Offset: 0x00009BEE
			[DebuggerStepThrough]
			public static v64 vext_f64(v64 a0, v64 a1, int a2)
			{
				return a0;
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x0000B9F1 File Offset: 0x00009BF1
			[DebuggerStepThrough]
			public static v128 vextq_f64(v128 a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x0000B9F8 File Offset: 0x00009BF8
			[DebuggerStepThrough]
			public static v64 vzip1_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x0000B9FF File Offset: 0x00009BFF
			[DebuggerStepThrough]
			public static v128 vzip1q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x0000BA06 File Offset: 0x00009C06
			[DebuggerStepThrough]
			public static v64 vzip1_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x0000BA0D File Offset: 0x00009C0D
			[DebuggerStepThrough]
			public static v128 vzip1q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x0000BA14 File Offset: 0x00009C14
			[DebuggerStepThrough]
			public static v64 vzip1_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000995 RID: 2453 RVA: 0x0000BA1B File Offset: 0x00009C1B
			[DebuggerStepThrough]
			public static v128 vzip1q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x0000BA22 File Offset: 0x00009C22
			[DebuggerStepThrough]
			public static v128 vzip1q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0000BA29 File Offset: 0x00009C29
			[DebuggerStepThrough]
			public static v64 vzip1_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip1_s8(a0, a1);
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x0000BA32 File Offset: 0x00009C32
			[DebuggerStepThrough]
			public static v128 vzip1q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s8(a0, a1);
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x0000BA3B File Offset: 0x00009C3B
			[DebuggerStepThrough]
			public static v64 vzip1_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip1_s16(a0, a1);
			}

			// Token: 0x0600099A RID: 2458 RVA: 0x0000BA44 File Offset: 0x00009C44
			[DebuggerStepThrough]
			public static v128 vzip1q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s16(a0, a1);
			}

			// Token: 0x0600099B RID: 2459 RVA: 0x0000BA4D File Offset: 0x00009C4D
			[DebuggerStepThrough]
			public static v64 vzip1_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip1_s32(a0, a1);
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x0000BA56 File Offset: 0x00009C56
			[DebuggerStepThrough]
			public static v128 vzip1q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s32(a0, a1);
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x0000BA5F File Offset: 0x00009C5F
			[DebuggerStepThrough]
			public static v128 vzip1q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s64(a0, a1);
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x0000BA68 File Offset: 0x00009C68
			[DebuggerStepThrough]
			public static v64 vzip1_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip1_s32(a0, a1);
			}

			// Token: 0x0600099F RID: 2463 RVA: 0x0000BA71 File Offset: 0x00009C71
			[DebuggerStepThrough]
			public static v128 vzip1q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s32(a0, a1);
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x0000BA7A File Offset: 0x00009C7A
			[DebuggerStepThrough]
			public static v128 vzip1q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip1q_s64(a0, a1);
			}

			// Token: 0x060009A1 RID: 2465 RVA: 0x0000BA83 File Offset: 0x00009C83
			[DebuggerStepThrough]
			public static v64 vzip2_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A2 RID: 2466 RVA: 0x0000BA8A File Offset: 0x00009C8A
			[DebuggerStepThrough]
			public static v128 vzip2q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A3 RID: 2467 RVA: 0x0000BA91 File Offset: 0x00009C91
			[DebuggerStepThrough]
			public static v64 vzip2_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A4 RID: 2468 RVA: 0x0000BA98 File Offset: 0x00009C98
			[DebuggerStepThrough]
			public static v128 vzip2q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A5 RID: 2469 RVA: 0x0000BA9F File Offset: 0x00009C9F
			[DebuggerStepThrough]
			public static v64 vzip2_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A6 RID: 2470 RVA: 0x0000BAA6 File Offset: 0x00009CA6
			[DebuggerStepThrough]
			public static v128 vzip2q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A7 RID: 2471 RVA: 0x0000BAAD File Offset: 0x00009CAD
			[DebuggerStepThrough]
			public static v128 vzip2q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x0000BAB4 File Offset: 0x00009CB4
			[DebuggerStepThrough]
			public static v64 vzip2_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip2_s8(a0, a1);
			}

			// Token: 0x060009A9 RID: 2473 RVA: 0x0000BABD File Offset: 0x00009CBD
			[DebuggerStepThrough]
			public static v128 vzip2q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s8(a0, a1);
			}

			// Token: 0x060009AA RID: 2474 RVA: 0x0000BAC6 File Offset: 0x00009CC6
			[DebuggerStepThrough]
			public static v64 vzip2_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip2_s16(a0, a1);
			}

			// Token: 0x060009AB RID: 2475 RVA: 0x0000BACF File Offset: 0x00009CCF
			[DebuggerStepThrough]
			public static v128 vzip2q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s16(a0, a1);
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x0000BAD8 File Offset: 0x00009CD8
			[DebuggerStepThrough]
			public static v64 vzip2_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip2_s32(a0, a1);
			}

			// Token: 0x060009AD RID: 2477 RVA: 0x0000BAE1 File Offset: 0x00009CE1
			[DebuggerStepThrough]
			public static v128 vzip2q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s32(a0, a1);
			}

			// Token: 0x060009AE RID: 2478 RVA: 0x0000BAEA File Offset: 0x00009CEA
			[DebuggerStepThrough]
			public static v128 vzip2q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s64(a0, a1);
			}

			// Token: 0x060009AF RID: 2479 RVA: 0x0000BAF3 File Offset: 0x00009CF3
			[DebuggerStepThrough]
			public static v64 vzip2_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vzip2_s32(a0, a1);
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x0000BAFC File Offset: 0x00009CFC
			[DebuggerStepThrough]
			public static v128 vzip2q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s32(a0, a1);
			}

			// Token: 0x060009B1 RID: 2481 RVA: 0x0000BB05 File Offset: 0x00009D05
			[DebuggerStepThrough]
			public static v128 vzip2q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vzip2q_s64(a0, a1);
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x0000BB0E File Offset: 0x00009D0E
			[DebuggerStepThrough]
			public static v64 vuzp1_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B3 RID: 2483 RVA: 0x0000BB15 File Offset: 0x00009D15
			[DebuggerStepThrough]
			public static v128 vuzp1q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x0000BB1C File Offset: 0x00009D1C
			[DebuggerStepThrough]
			public static v64 vuzp1_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B5 RID: 2485 RVA: 0x0000BB23 File Offset: 0x00009D23
			[DebuggerStepThrough]
			public static v128 vuzp1q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B6 RID: 2486 RVA: 0x0000BB2A File Offset: 0x00009D2A
			[DebuggerStepThrough]
			public static v64 vuzp1_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B7 RID: 2487 RVA: 0x0000BB31 File Offset: 0x00009D31
			[DebuggerStepThrough]
			public static v128 vuzp1q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B8 RID: 2488 RVA: 0x0000BB38 File Offset: 0x00009D38
			[DebuggerStepThrough]
			public static v128 vuzp1q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009B9 RID: 2489 RVA: 0x0000BB3F File Offset: 0x00009D3F
			[DebuggerStepThrough]
			public static v64 vuzp1_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp1_s8(a0, a1);
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x0000BB48 File Offset: 0x00009D48
			[DebuggerStepThrough]
			public static v128 vuzp1q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s8(a0, a1);
			}

			// Token: 0x060009BB RID: 2491 RVA: 0x0000BB51 File Offset: 0x00009D51
			[DebuggerStepThrough]
			public static v64 vuzp1_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp1_s16(a0, a1);
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x0000BB5A File Offset: 0x00009D5A
			[DebuggerStepThrough]
			public static v128 vuzp1q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s16(a0, a1);
			}

			// Token: 0x060009BD RID: 2493 RVA: 0x0000BB63 File Offset: 0x00009D63
			[DebuggerStepThrough]
			public static v64 vuzp1_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp1_s32(a0, a1);
			}

			// Token: 0x060009BE RID: 2494 RVA: 0x0000BB6C File Offset: 0x00009D6C
			[DebuggerStepThrough]
			public static v128 vuzp1q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s32(a0, a1);
			}

			// Token: 0x060009BF RID: 2495 RVA: 0x0000BB75 File Offset: 0x00009D75
			[DebuggerStepThrough]
			public static v128 vuzp1q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s64(a0, a1);
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x0000BB7E File Offset: 0x00009D7E
			[DebuggerStepThrough]
			public static v64 vuzp1_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp1_s32(a0, a1);
			}

			// Token: 0x060009C1 RID: 2497 RVA: 0x0000BB87 File Offset: 0x00009D87
			[DebuggerStepThrough]
			public static v128 vuzp1q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s32(a0, a1);
			}

			// Token: 0x060009C2 RID: 2498 RVA: 0x0000BB90 File Offset: 0x00009D90
			[DebuggerStepThrough]
			public static v128 vuzp1q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp1q_s64(a0, a1);
			}

			// Token: 0x060009C3 RID: 2499 RVA: 0x0000BB99 File Offset: 0x00009D99
			[DebuggerStepThrough]
			public static v64 vuzp2_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x0000BBA0 File Offset: 0x00009DA0
			[DebuggerStepThrough]
			public static v128 vuzp2q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C5 RID: 2501 RVA: 0x0000BBA7 File Offset: 0x00009DA7
			[DebuggerStepThrough]
			public static v64 vuzp2_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x0000BBAE File Offset: 0x00009DAE
			[DebuggerStepThrough]
			public static v128 vuzp2q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C7 RID: 2503 RVA: 0x0000BBB5 File Offset: 0x00009DB5
			[DebuggerStepThrough]
			public static v64 vuzp2_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0000BBBC File Offset: 0x00009DBC
			[DebuggerStepThrough]
			public static v128 vuzp2q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009C9 RID: 2505 RVA: 0x0000BBC3 File Offset: 0x00009DC3
			[DebuggerStepThrough]
			public static v128 vuzp2q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x0000BBCA File Offset: 0x00009DCA
			[DebuggerStepThrough]
			public static v64 vuzp2_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp2_s8(a0, a1);
			}

			// Token: 0x060009CB RID: 2507 RVA: 0x0000BBD3 File Offset: 0x00009DD3
			[DebuggerStepThrough]
			public static v128 vuzp2q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s8(a0, a1);
			}

			// Token: 0x060009CC RID: 2508 RVA: 0x0000BBDC File Offset: 0x00009DDC
			[DebuggerStepThrough]
			public static v64 vuzp2_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp2_s16(a0, a1);
			}

			// Token: 0x060009CD RID: 2509 RVA: 0x0000BBE5 File Offset: 0x00009DE5
			[DebuggerStepThrough]
			public static v128 vuzp2q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s16(a0, a1);
			}

			// Token: 0x060009CE RID: 2510 RVA: 0x0000BBEE File Offset: 0x00009DEE
			[DebuggerStepThrough]
			public static v64 vuzp2_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp2_s32(a0, a1);
			}

			// Token: 0x060009CF RID: 2511 RVA: 0x0000BBF7 File Offset: 0x00009DF7
			[DebuggerStepThrough]
			public static v128 vuzp2q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s32(a0, a1);
			}

			// Token: 0x060009D0 RID: 2512 RVA: 0x0000BC00 File Offset: 0x00009E00
			[DebuggerStepThrough]
			public static v128 vuzp2q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s64(a0, a1);
			}

			// Token: 0x060009D1 RID: 2513 RVA: 0x0000BC09 File Offset: 0x00009E09
			[DebuggerStepThrough]
			public static v64 vuzp2_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vuzp2_s32(a0, a1);
			}

			// Token: 0x060009D2 RID: 2514 RVA: 0x0000BC12 File Offset: 0x00009E12
			[DebuggerStepThrough]
			public static v128 vuzp2q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s32(a0, a1);
			}

			// Token: 0x060009D3 RID: 2515 RVA: 0x0000BC1B File Offset: 0x00009E1B
			[DebuggerStepThrough]
			public static v128 vuzp2q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vuzp2q_s64(a0, a1);
			}

			// Token: 0x060009D4 RID: 2516 RVA: 0x0000BC24 File Offset: 0x00009E24
			[DebuggerStepThrough]
			public static v64 vtrn1_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009D5 RID: 2517 RVA: 0x0000BC2B File Offset: 0x00009E2B
			[DebuggerStepThrough]
			public static v128 vtrn1q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009D6 RID: 2518 RVA: 0x0000BC32 File Offset: 0x00009E32
			[DebuggerStepThrough]
			public static v64 vtrn1_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009D7 RID: 2519 RVA: 0x0000BC39 File Offset: 0x00009E39
			[DebuggerStepThrough]
			public static v128 vtrn1q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009D8 RID: 2520 RVA: 0x0000BC40 File Offset: 0x00009E40
			[DebuggerStepThrough]
			public static v64 vtrn1_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009D9 RID: 2521 RVA: 0x0000BC47 File Offset: 0x00009E47
			[DebuggerStepThrough]
			public static v128 vtrn1q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x0000BC4E File Offset: 0x00009E4E
			[DebuggerStepThrough]
			public static v128 vtrn1q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009DB RID: 2523 RVA: 0x0000BC55 File Offset: 0x00009E55
			[DebuggerStepThrough]
			public static v64 vtrn1_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn1_s8(a0, a1);
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0000BC5E File Offset: 0x00009E5E
			[DebuggerStepThrough]
			public static v128 vtrn1q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s8(a0, a1);
			}

			// Token: 0x060009DD RID: 2525 RVA: 0x0000BC67 File Offset: 0x00009E67
			[DebuggerStepThrough]
			public static v64 vtrn1_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn1_s16(a0, a1);
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0000BC70 File Offset: 0x00009E70
			[DebuggerStepThrough]
			public static v128 vtrn1q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s16(a0, a1);
			}

			// Token: 0x060009DF RID: 2527 RVA: 0x0000BC79 File Offset: 0x00009E79
			[DebuggerStepThrough]
			public static v64 vtrn1_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn1_s32(a0, a1);
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0000BC82 File Offset: 0x00009E82
			[DebuggerStepThrough]
			public static v128 vtrn1q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s32(a0, a1);
			}

			// Token: 0x060009E1 RID: 2529 RVA: 0x0000BC8B File Offset: 0x00009E8B
			[DebuggerStepThrough]
			public static v128 vtrn1q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s64(a0, a1);
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x0000BC94 File Offset: 0x00009E94
			[DebuggerStepThrough]
			public static v64 vtrn1_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn1_s32(a0, a1);
			}

			// Token: 0x060009E3 RID: 2531 RVA: 0x0000BC9D File Offset: 0x00009E9D
			[DebuggerStepThrough]
			public static v128 vtrn1q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s32(a0, a1);
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x0000BCA6 File Offset: 0x00009EA6
			[DebuggerStepThrough]
			public static v128 vtrn1q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn1q_s64(a0, a1);
			}

			// Token: 0x060009E5 RID: 2533 RVA: 0x0000BCAF File Offset: 0x00009EAF
			[DebuggerStepThrough]
			public static v64 vtrn2_s8(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x0000BCB6 File Offset: 0x00009EB6
			[DebuggerStepThrough]
			public static v128 vtrn2q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009E7 RID: 2535 RVA: 0x0000BCBD File Offset: 0x00009EBD
			[DebuggerStepThrough]
			public static v64 vtrn2_s16(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009E8 RID: 2536 RVA: 0x0000BCC4 File Offset: 0x00009EC4
			[DebuggerStepThrough]
			public static v128 vtrn2q_s16(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009E9 RID: 2537 RVA: 0x0000BCCB File Offset: 0x00009ECB
			[DebuggerStepThrough]
			public static v64 vtrn2_s32(v64 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009EA RID: 2538 RVA: 0x0000BCD2 File Offset: 0x00009ED2
			[DebuggerStepThrough]
			public static v128 vtrn2q_s32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009EB RID: 2539 RVA: 0x0000BCD9 File Offset: 0x00009ED9
			[DebuggerStepThrough]
			public static v128 vtrn2q_s64(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009EC RID: 2540 RVA: 0x0000BCE0 File Offset: 0x00009EE0
			[DebuggerStepThrough]
			public static v64 vtrn2_u8(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn2_s8(a0, a1);
			}

			// Token: 0x060009ED RID: 2541 RVA: 0x0000BCE9 File Offset: 0x00009EE9
			[DebuggerStepThrough]
			public static v128 vtrn2q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s8(a0, a1);
			}

			// Token: 0x060009EE RID: 2542 RVA: 0x0000BCF2 File Offset: 0x00009EF2
			[DebuggerStepThrough]
			public static v64 vtrn2_u16(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn2_s16(a0, a1);
			}

			// Token: 0x060009EF RID: 2543 RVA: 0x0000BCFB File Offset: 0x00009EFB
			[DebuggerStepThrough]
			public static v128 vtrn2q_u16(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s16(a0, a1);
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x0000BD04 File Offset: 0x00009F04
			[DebuggerStepThrough]
			public static v64 vtrn2_u32(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn2_s32(a0, a1);
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x0000BD0D File Offset: 0x00009F0D
			[DebuggerStepThrough]
			public static v128 vtrn2q_u32(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s32(a0, a1);
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x0000BD16 File Offset: 0x00009F16
			[DebuggerStepThrough]
			public static v128 vtrn2q_u64(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s64(a0, a1);
			}

			// Token: 0x060009F3 RID: 2547 RVA: 0x0000BD1F File Offset: 0x00009F1F
			[DebuggerStepThrough]
			public static v64 vtrn2_f32(v64 a0, v64 a1)
			{
				return Arm.Neon.vtrn2_s32(a0, a1);
			}

			// Token: 0x060009F4 RID: 2548 RVA: 0x0000BD28 File Offset: 0x00009F28
			[DebuggerStepThrough]
			public static v128 vtrn2q_f32(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s32(a0, a1);
			}

			// Token: 0x060009F5 RID: 2549 RVA: 0x0000BD31 File Offset: 0x00009F31
			[DebuggerStepThrough]
			public static v128 vtrn2q_f64(v128 a0, v128 a1)
			{
				return Arm.Neon.vtrn2q_s64(a0, a1);
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x0000BD3A File Offset: 0x00009F3A
			[DebuggerStepThrough]
			public static v64 vqtbl1_s8(v128 a0, v64 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009F7 RID: 2551 RVA: 0x0000BD41 File Offset: 0x00009F41
			[DebuggerStepThrough]
			public static v128 vqtbl1q_s8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x0000BD48 File Offset: 0x00009F48
			[DebuggerStepThrough]
			public static v64 vqtbl1_u8(v128 a0, v64 a1)
			{
				return Arm.Neon.vqtbl1_s8(a0, a1);
			}

			// Token: 0x060009F9 RID: 2553 RVA: 0x0000BD51 File Offset: 0x00009F51
			[DebuggerStepThrough]
			public static v128 vqtbl1q_u8(v128 a0, v128 a1)
			{
				return Arm.Neon.vqtbl1q_s8(a0, a1);
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x0000BD5A File Offset: 0x00009F5A
			[DebuggerStepThrough]
			public static v64 vqtbx1_s8(v64 a0, v128 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009FB RID: 2555 RVA: 0x0000BD61 File Offset: 0x00009F61
			[DebuggerStepThrough]
			public static v128 vqtbx1q_s8(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x0000BD68 File Offset: 0x00009F68
			[DebuggerStepThrough]
			public static v64 vqtbx1_u8(v64 a0, v128 a1, v64 a2)
			{
				return Arm.Neon.vqtbx1_s8(a0, a1, a2);
			}

			// Token: 0x060009FD RID: 2557 RVA: 0x0000BD72 File Offset: 0x00009F72
			[DebuggerStepThrough]
			public static v128 vqtbx1q_u8(v128 a0, v128 a1, v128 a2)
			{
				return Arm.Neon.vqtbx1q_s8(a0, a1, a2);
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x0000BD7C File Offset: 0x00009F7C
			[DebuggerStepThrough]
			public static double vget_lane_f64(v64 a0, int a1)
			{
				return a0.Double0;
			}

			// Token: 0x060009FF RID: 2559 RVA: 0x0000BD84 File Offset: 0x00009F84
			[DebuggerStepThrough]
			public static double vgetq_lane_f64(v128 a0, int a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A00 RID: 2560 RVA: 0x0000BD8B File Offset: 0x00009F8B
			[DebuggerStepThrough]
			public static v64 vset_lane_f64(double a0, v64 a1, int a2)
			{
				return new v64(a0);
			}

			// Token: 0x06000A01 RID: 2561 RVA: 0x0000BD93 File Offset: 0x00009F93
			[DebuggerStepThrough]
			public static v128 vsetq_lane_f64(double a0, v128 a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0000BD9A File Offset: 0x00009F9A
			[DebuggerStepThrough]
			public static float vrecpxs_f32(float a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A03 RID: 2563 RVA: 0x0000BDA1 File Offset: 0x00009FA1
			[DebuggerStepThrough]
			public static double vrecpxd_f64(double a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x0000BDA8 File Offset: 0x00009FA8
			[DebuggerStepThrough]
			public static v64 vfms_n_f32(v64 a0, v64 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A05 RID: 2565 RVA: 0x0000BDAF File Offset: 0x00009FAF
			[DebuggerStepThrough]
			public static v128 vfmsq_n_f32(v128 a0, v128 a1, float a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A06 RID: 2566 RVA: 0x0000BDB6 File Offset: 0x00009FB6
			[DebuggerStepThrough]
			public static v64 vfma_n_f64(v64 a0, v64 a1, double a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A07 RID: 2567 RVA: 0x0000BDBD File Offset: 0x00009FBD
			[DebuggerStepThrough]
			public static v128 vfmaq_n_f64(v128 a0, v128 a1, double a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A08 RID: 2568 RVA: 0x0000BDC4 File Offset: 0x00009FC4
			[DebuggerStepThrough]
			public static v64 vfms_n_f64(v64 a0, v64 a1, double a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A09 RID: 2569 RVA: 0x0000BDCB File Offset: 0x00009FCB
			[DebuggerStepThrough]
			public static v128 vfmsq_n_f64(v128 a0, v128 a1, double a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0000BDD2 File Offset: 0x00009FD2
			public static bool IsNeonCryptoSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A0B RID: 2571 RVA: 0x0000BDD5 File Offset: 0x00009FD5
			[DebuggerStepThrough]
			public static v128 vsha1cq_u32(v128 a0, uint a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x0000BDDC File Offset: 0x00009FDC
			[DebuggerStepThrough]
			public static v128 vsha1pq_u32(v128 a0, uint a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A0D RID: 2573 RVA: 0x0000BDE3 File Offset: 0x00009FE3
			[DebuggerStepThrough]
			public static v128 vsha1mq_u32(v128 a0, uint a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x0000BDEA File Offset: 0x00009FEA
			[DebuggerStepThrough]
			public static uint vsha1h_u32(uint a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x0000BDF1 File Offset: 0x00009FF1
			[DebuggerStepThrough]
			public static v128 vsha1su0q_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x0000BDF8 File Offset: 0x00009FF8
			[DebuggerStepThrough]
			public static v128 vsha1su1q_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x0000BDFF File Offset: 0x00009FFF
			[DebuggerStepThrough]
			public static v128 vsha256hq_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x0000BE06 File Offset: 0x0000A006
			[DebuggerStepThrough]
			public static v128 vsha256h2q_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x0000BE0D File Offset: 0x0000A00D
			[DebuggerStepThrough]
			public static v128 vsha256su0q_u32(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x0000BE14 File Offset: 0x0000A014
			[DebuggerStepThrough]
			public static v128 vsha256su1q_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x0000BE1B File Offset: 0x0000A01B
			[DebuggerStepThrough]
			public static uint __crc32b(uint a0, byte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x0000BE22 File Offset: 0x0000A022
			[DebuggerStepThrough]
			public static uint __crc32h(uint a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A17 RID: 2583 RVA: 0x0000BE29 File Offset: 0x0000A029
			[DebuggerStepThrough]
			public static uint __crc32w(uint a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x0000BE30 File Offset: 0x0000A030
			[DebuggerStepThrough]
			public static uint __crc32d(uint a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x0000BE37 File Offset: 0x0000A037
			[DebuggerStepThrough]
			public static uint __crc32cb(uint a0, byte a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x0000BE3E File Offset: 0x0000A03E
			[DebuggerStepThrough]
			public static uint __crc32ch(uint a0, ushort a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1B RID: 2587 RVA: 0x0000BE45 File Offset: 0x0000A045
			[DebuggerStepThrough]
			public static uint __crc32cw(uint a0, uint a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x0000BE4C File Offset: 0x0000A04C
			[DebuggerStepThrough]
			public static uint __crc32cd(uint a0, ulong a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1D RID: 2589 RVA: 0x0000BE53 File Offset: 0x0000A053
			[DebuggerStepThrough]
			public static v128 vaeseq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x0000BE5A File Offset: 0x0000A05A
			[DebuggerStepThrough]
			public static v128 vaesdq_u8(v128 a0, v128 a1)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x0000BE61 File Offset: 0x0000A061
			[DebuggerStepThrough]
			public static v128 vaesmcq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x0000BE68 File Offset: 0x0000A068
			[DebuggerStepThrough]
			public static v128 vaesimcq_u8(v128 a0)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0000BE6F File Offset: 0x0000A06F
			public static bool IsNeonDotProdSupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A22 RID: 2594 RVA: 0x0000BE72 File Offset: 0x0000A072
			[DebuggerStepThrough]
			public static v64 vdot_u32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x0000BE79 File Offset: 0x0000A079
			[DebuggerStepThrough]
			public static v64 vdot_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x0000BE80 File Offset: 0x0000A080
			[DebuggerStepThrough]
			public static v128 vdotq_u32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A25 RID: 2597 RVA: 0x0000BE87 File Offset: 0x0000A087
			[DebuggerStepThrough]
			public static v128 vdotq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x0000BE8E File Offset: 0x0000A08E
			[DebuggerStepThrough]
			public static v64 vdot_lane_u32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A27 RID: 2599 RVA: 0x0000BE95 File Offset: 0x0000A095
			[DebuggerStepThrough]
			public static v64 vdot_lane_s32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0000BE9C File Offset: 0x0000A09C
			[DebuggerStepThrough]
			public static v128 vdotq_laneq_u32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
			[DebuggerStepThrough]
			public static v128 vdotq_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x0000BEAA File Offset: 0x0000A0AA
			[DebuggerStepThrough]
			public static v64 vdot_laneq_u32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x0000BEB1 File Offset: 0x0000A0B1
			[DebuggerStepThrough]
			public static v64 vdot_laneq_s32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
			[DebuggerStepThrough]
			public static v128 vdotq_lane_u32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0000BEBF File Offset: 0x0000A0BF
			[DebuggerStepThrough]
			public static v128 vdotq_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
			public static bool IsNeonRDMASupported
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0000BEC9 File Offset: 0x0000A0C9
			[DebuggerStepThrough]
			public static v64 vqrdmlah_s16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0000BED0 File Offset: 0x0000A0D0
			[DebuggerStepThrough]
			public static v64 vqrdmlah_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x0000BED7 File Offset: 0x0000A0D7
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x0000BEDE File Offset: 0x0000A0DE
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_s16(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x0000BEEC File Offset: 0x0000A0EC
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_s32(v64 a0, v64 a1, v64 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_s16(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0000BEFA File Offset: 0x0000A0FA
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_s32(v128 a0, v128 a1, v128 a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A37 RID: 2615 RVA: 0x0000BF01 File Offset: 0x0000A101
			[DebuggerStepThrough]
			public static v64 vqrdmlah_lane_s16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x0000BF08 File Offset: 0x0000A108
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x0000BF0F File Offset: 0x0000A10F
			[DebuggerStepThrough]
			public static v64 vqrdmlah_laneq_s16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3A RID: 2618 RVA: 0x0000BF16 File Offset: 0x0000A116
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3B RID: 2619 RVA: 0x0000BF1D File Offset: 0x0000A11D
			[DebuggerStepThrough]
			public static v64 vqrdmlah_lane_s32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3C RID: 2620 RVA: 0x0000BF24 File Offset: 0x0000A124
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3D RID: 2621 RVA: 0x0000BF2B File Offset: 0x0000A12B
			[DebuggerStepThrough]
			public static v64 vqrdmlah_laneq_s32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x0000BF32 File Offset: 0x0000A132
			[DebuggerStepThrough]
			public static v128 vqrdmlahq_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A3F RID: 2623 RVA: 0x0000BF39 File Offset: 0x0000A139
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_lane_s16(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A40 RID: 2624 RVA: 0x0000BF40 File Offset: 0x0000A140
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_lane_s16(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A41 RID: 2625 RVA: 0x0000BF47 File Offset: 0x0000A147
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_laneq_s16(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A42 RID: 2626 RVA: 0x0000BF4E File Offset: 0x0000A14E
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_laneq_s16(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A43 RID: 2627 RVA: 0x0000BF55 File Offset: 0x0000A155
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_lane_s32(v64 a0, v64 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A44 RID: 2628 RVA: 0x0000BF5C File Offset: 0x0000A15C
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_lane_s32(v128 a0, v128 a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A45 RID: 2629 RVA: 0x0000BF63 File Offset: 0x0000A163
			[DebuggerStepThrough]
			public static v64 vqrdmlsh_laneq_s32(v64 a0, v64 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A46 RID: 2630 RVA: 0x0000BF6A File Offset: 0x0000A16A
			[DebuggerStepThrough]
			public static v128 vqrdmlshq_laneq_s32(v128 a0, v128 a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A47 RID: 2631 RVA: 0x0000BF71 File Offset: 0x0000A171
			[DebuggerStepThrough]
			public static short vqrdmlahh_s16(short a0, short a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A48 RID: 2632 RVA: 0x0000BF78 File Offset: 0x0000A178
			[DebuggerStepThrough]
			public static int vqrdmlahs_s32(int a0, int a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A49 RID: 2633 RVA: 0x0000BF7F File Offset: 0x0000A17F
			[DebuggerStepThrough]
			public static short vqrdmlshh_s16(short a0, short a1, short a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4A RID: 2634 RVA: 0x0000BF86 File Offset: 0x0000A186
			[DebuggerStepThrough]
			public static int vqrdmlshs_s32(int a0, int a1, int a2)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x0000BF8D File Offset: 0x0000A18D
			[DebuggerStepThrough]
			public static short vqrdmlahh_lane_s16(short a0, short a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4C RID: 2636 RVA: 0x0000BF94 File Offset: 0x0000A194
			[DebuggerStepThrough]
			public static short vqrdmlahh_laneq_s16(short a0, short a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x0000BF9B File Offset: 0x0000A19B
			[DebuggerStepThrough]
			public static int vqrdmlahs_lane_s32(int a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x0000BFA2 File Offset: 0x0000A1A2
			[DebuggerStepThrough]
			public static short vqrdmlshh_lane_s16(short a0, short a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A4F RID: 2639 RVA: 0x0000BFA9 File Offset: 0x0000A1A9
			[DebuggerStepThrough]
			public static short vqrdmlshh_laneq_s16(short a0, short a1, v128 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A50 RID: 2640 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
			[DebuggerStepThrough]
			public static int vqrdmlshs_lane_s32(int a0, int a1, v64 a2, int a3)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000A51 RID: 2641 RVA: 0x0000BFB7 File Offset: 0x0000A1B7
			[DebuggerStepThrough]
			public static v64 vcreate_s8(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A52 RID: 2642 RVA: 0x0000BFBF File Offset: 0x0000A1BF
			[DebuggerStepThrough]
			public static v64 vcreate_s16(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A53 RID: 2643 RVA: 0x0000BFC7 File Offset: 0x0000A1C7
			[DebuggerStepThrough]
			public static v64 vcreate_s32(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A54 RID: 2644 RVA: 0x0000BFCF File Offset: 0x0000A1CF
			[DebuggerStepThrough]
			public static v64 vcreate_s64(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A55 RID: 2645 RVA: 0x0000BFD7 File Offset: 0x0000A1D7
			[DebuggerStepThrough]
			public static v64 vcreate_u8(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A56 RID: 2646 RVA: 0x0000BFDF File Offset: 0x0000A1DF
			[DebuggerStepThrough]
			public static v64 vcreate_u16(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x0000BFE7 File Offset: 0x0000A1E7
			[DebuggerStepThrough]
			public static v64 vcreate_u32(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x0000BFEF File Offset: 0x0000A1EF
			[DebuggerStepThrough]
			public static v64 vcreate_u64(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x0000BFF7 File Offset: 0x0000A1F7
			[DebuggerStepThrough]
			public static v64 vcreate_f16(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x0000BFFF File Offset: 0x0000A1FF
			[DebuggerStepThrough]
			public static v64 vcreate_f32(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A5B RID: 2651 RVA: 0x0000C007 File Offset: 0x0000A207
			[DebuggerStepThrough]
			public static v64 vcreate_f64(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x0000C00F File Offset: 0x0000A20F
			[DebuggerStepThrough]
			public static v64 vdup_n_s8(sbyte a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A5D RID: 2653 RVA: 0x0000C017 File Offset: 0x0000A217
			[DebuggerStepThrough]
			public static v128 vdupq_n_s8(sbyte a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A5E RID: 2654 RVA: 0x0000C01F File Offset: 0x0000A21F
			[DebuggerStepThrough]
			public static v64 vdup_n_s16(short a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A5F RID: 2655 RVA: 0x0000C027 File Offset: 0x0000A227
			[DebuggerStepThrough]
			public static v128 vdupq_n_s16(short a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A60 RID: 2656 RVA: 0x0000C02F File Offset: 0x0000A22F
			[DebuggerStepThrough]
			public static v64 vdup_n_s32(int a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A61 RID: 2657 RVA: 0x0000C037 File Offset: 0x0000A237
			[DebuggerStepThrough]
			public static v128 vdupq_n_s32(int a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A62 RID: 2658 RVA: 0x0000C03F File Offset: 0x0000A23F
			[DebuggerStepThrough]
			public static v64 vdup_n_s64(long a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A63 RID: 2659 RVA: 0x0000C047 File Offset: 0x0000A247
			[DebuggerStepThrough]
			public static v128 vdupq_n_s64(long a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A64 RID: 2660 RVA: 0x0000C04F File Offset: 0x0000A24F
			[DebuggerStepThrough]
			public static v64 vdup_n_u8(byte a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x0000C057 File Offset: 0x0000A257
			[DebuggerStepThrough]
			public static v128 vdupq_n_u8(byte a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A66 RID: 2662 RVA: 0x0000C05F File Offset: 0x0000A25F
			[DebuggerStepThrough]
			public static v64 vdup_n_u16(ushort a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A67 RID: 2663 RVA: 0x0000C067 File Offset: 0x0000A267
			[DebuggerStepThrough]
			public static v128 vdupq_n_u16(ushort a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A68 RID: 2664 RVA: 0x0000C06F File Offset: 0x0000A26F
			[DebuggerStepThrough]
			public static v64 vdup_n_u32(uint a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A69 RID: 2665 RVA: 0x0000C077 File Offset: 0x0000A277
			[DebuggerStepThrough]
			public static v128 vdupq_n_u32(uint a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A6A RID: 2666 RVA: 0x0000C07F File Offset: 0x0000A27F
			[DebuggerStepThrough]
			public static v64 vdup_n_u64(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A6B RID: 2667 RVA: 0x0000C087 File Offset: 0x0000A287
			[DebuggerStepThrough]
			public static v128 vdupq_n_u64(ulong a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x0000C08F File Offset: 0x0000A28F
			[DebuggerStepThrough]
			public static v64 vdup_n_f32(float a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A6D RID: 2669 RVA: 0x0000C097 File Offset: 0x0000A297
			[DebuggerStepThrough]
			public static v128 vdupq_n_f32(float a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x0000C09F File Offset: 0x0000A29F
			[DebuggerStepThrough]
			public static v64 vdup_n_f64(double a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A6F RID: 2671 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
			[DebuggerStepThrough]
			public static v128 vdupq_n_f64(double a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A70 RID: 2672 RVA: 0x0000C0AF File Offset: 0x0000A2AF
			[DebuggerStepThrough]
			public static v64 vmov_n_s8(sbyte a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x0000C0B7 File Offset: 0x0000A2B7
			[DebuggerStepThrough]
			public static v128 vmovq_n_s8(sbyte a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A72 RID: 2674 RVA: 0x0000C0BF File Offset: 0x0000A2BF
			[DebuggerStepThrough]
			public static v64 vmov_n_s16(short a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A73 RID: 2675 RVA: 0x0000C0C7 File Offset: 0x0000A2C7
			[DebuggerStepThrough]
			public static v128 vmovq_n_s16(short a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A74 RID: 2676 RVA: 0x0000C0CF File Offset: 0x0000A2CF
			[DebuggerStepThrough]
			public static v64 vmov_n_s32(int a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x0000C0D7 File Offset: 0x0000A2D7
			[DebuggerStepThrough]
			public static v128 vmovq_n_s32(int a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x0000C0DF File Offset: 0x0000A2DF
			[DebuggerStepThrough]
			public static v64 vmov_n_s64(long a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x0000C0E7 File Offset: 0x0000A2E7
			[DebuggerStepThrough]
			public static v128 vmovq_n_s64(long a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x0000C0EF File Offset: 0x0000A2EF
			[DebuggerStepThrough]
			public static v64 vmov_n_u8(byte a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
			[DebuggerStepThrough]
			public static v128 vmovq_n_u8(byte a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x0000C0FF File Offset: 0x0000A2FF
			[DebuggerStepThrough]
			public static v64 vmov_n_u16(ushort a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x0000C107 File Offset: 0x0000A307
			[DebuggerStepThrough]
			public static v128 vmovq_n_u16(ushort a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x0000C10F File Offset: 0x0000A30F
			[DebuggerStepThrough]
			public static v64 vmov_n_u32(uint a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x0000C117 File Offset: 0x0000A317
			[DebuggerStepThrough]
			public static v128 vmovq_n_u32(uint a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A7E RID: 2686 RVA: 0x0000C11F File Offset: 0x0000A31F
			[DebuggerStepThrough]
			public static v64 vmov_n_u64(ulong a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A7F RID: 2687 RVA: 0x0000C127 File Offset: 0x0000A327
			[DebuggerStepThrough]
			public static v128 vmovq_n_u64(ulong a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x0000C12F File Offset: 0x0000A32F
			[DebuggerStepThrough]
			public static v64 vmov_n_f32(float a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x0000C137 File Offset: 0x0000A337
			[DebuggerStepThrough]
			public static v128 vmovq_n_f32(float a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x0000C13F File Offset: 0x0000A33F
			[DebuggerStepThrough]
			public static v64 vmov_n_f64(double a0)
			{
				return new v64(a0);
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x0000C147 File Offset: 0x0000A347
			[DebuggerStepThrough]
			public static v128 vmovq_n_f64(double a0)
			{
				return new v128(a0);
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x0000C14F File Offset: 0x0000A34F
			[DebuggerStepThrough]
			public static v128 vcombine_s8(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A85 RID: 2693 RVA: 0x0000C158 File Offset: 0x0000A358
			[DebuggerStepThrough]
			public static v128 vcombine_s16(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x0000C161 File Offset: 0x0000A361
			[DebuggerStepThrough]
			public static v128 vcombine_s32(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x0000C16A File Offset: 0x0000A36A
			[DebuggerStepThrough]
			public static v128 vcombine_s64(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A88 RID: 2696 RVA: 0x0000C173 File Offset: 0x0000A373
			[DebuggerStepThrough]
			public static v128 vcombine_u8(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A89 RID: 2697 RVA: 0x0000C17C File Offset: 0x0000A37C
			[DebuggerStepThrough]
			public static v128 vcombine_u16(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8A RID: 2698 RVA: 0x0000C185 File Offset: 0x0000A385
			[DebuggerStepThrough]
			public static v128 vcombine_u32(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8B RID: 2699 RVA: 0x0000C18E File Offset: 0x0000A38E
			[DebuggerStepThrough]
			public static v128 vcombine_u64(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8C RID: 2700 RVA: 0x0000C197 File Offset: 0x0000A397
			[DebuggerStepThrough]
			public static v128 vcombine_f16(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
			[DebuggerStepThrough]
			public static v128 vcombine_f32(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
			[DebuggerStepThrough]
			public static v128 vcombine_f64(v64 a0, v64 a1)
			{
				return new v128(a0, a1);
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x0000C1B2 File Offset: 0x0000A3B2
			[DebuggerStepThrough]
			public static v64 vget_high_s8(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x0000C1BA File Offset: 0x0000A3BA
			[DebuggerStepThrough]
			public static v64 vget_high_s16(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x0000C1C2 File Offset: 0x0000A3C2
			[DebuggerStepThrough]
			public static v64 vget_high_s32(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A92 RID: 2706 RVA: 0x0000C1CA File Offset: 0x0000A3CA
			[DebuggerStepThrough]
			public static v64 vget_high_s64(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A93 RID: 2707 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
			[DebuggerStepThrough]
			public static v64 vget_high_u8(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A94 RID: 2708 RVA: 0x0000C1DA File Offset: 0x0000A3DA
			[DebuggerStepThrough]
			public static v64 vget_high_u16(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A95 RID: 2709 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
			[DebuggerStepThrough]
			public static v64 vget_high_u32(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x0000C1EA File Offset: 0x0000A3EA
			[DebuggerStepThrough]
			public static v64 vget_high_u64(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
			[DebuggerStepThrough]
			public static v64 vget_high_f32(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x0000C1FA File Offset: 0x0000A3FA
			[DebuggerStepThrough]
			public static v64 vget_high_f64(v128 a0)
			{
				return a0.Hi64;
			}

			// Token: 0x06000A99 RID: 2713 RVA: 0x0000C202 File Offset: 0x0000A402
			[DebuggerStepThrough]
			public static v64 vget_low_s8(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x0000C20A File Offset: 0x0000A40A
			[DebuggerStepThrough]
			public static v64 vget_low_s16(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9B RID: 2715 RVA: 0x0000C212 File Offset: 0x0000A412
			[DebuggerStepThrough]
			public static v64 vget_low_s32(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9C RID: 2716 RVA: 0x0000C21A File Offset: 0x0000A41A
			[DebuggerStepThrough]
			public static v64 vget_low_s64(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9D RID: 2717 RVA: 0x0000C222 File Offset: 0x0000A422
			[DebuggerStepThrough]
			public static v64 vget_low_u8(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9E RID: 2718 RVA: 0x0000C22A File Offset: 0x0000A42A
			[DebuggerStepThrough]
			public static v64 vget_low_u16(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000A9F RID: 2719 RVA: 0x0000C232 File Offset: 0x0000A432
			[DebuggerStepThrough]
			public static v64 vget_low_u32(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000AA0 RID: 2720 RVA: 0x0000C23A File Offset: 0x0000A43A
			[DebuggerStepThrough]
			public static v64 vget_low_u64(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000AA1 RID: 2721 RVA: 0x0000C242 File Offset: 0x0000A442
			[DebuggerStepThrough]
			public static v64 vget_low_f32(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000AA2 RID: 2722 RVA: 0x0000C24A File Offset: 0x0000A44A
			[DebuggerStepThrough]
			public static v64 vget_low_f64(v128 a0)
			{
				return a0.Lo64;
			}

			// Token: 0x06000AA3 RID: 2723 RVA: 0x0000C252 File Offset: 0x0000A452
			[DebuggerStepThrough]
			public unsafe static v64 vld1_s8(sbyte* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AA4 RID: 2724 RVA: 0x0000C25A File Offset: 0x0000A45A
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_s8(sbyte* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AA5 RID: 2725 RVA: 0x0000C262 File Offset: 0x0000A462
			[DebuggerStepThrough]
			public unsafe static v64 vld1_s16(short* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AA6 RID: 2726 RVA: 0x0000C26A File Offset: 0x0000A46A
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_s16(short* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AA7 RID: 2727 RVA: 0x0000C272 File Offset: 0x0000A472
			[DebuggerStepThrough]
			public unsafe static v64 vld1_s32(int* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AA8 RID: 2728 RVA: 0x0000C27A File Offset: 0x0000A47A
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_s32(int* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AA9 RID: 2729 RVA: 0x0000C282 File Offset: 0x0000A482
			[DebuggerStepThrough]
			public unsafe static v64 vld1_s64(long* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AAA RID: 2730 RVA: 0x0000C28A File Offset: 0x0000A48A
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_s64(long* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AAB RID: 2731 RVA: 0x0000C292 File Offset: 0x0000A492
			[DebuggerStepThrough]
			public unsafe static v64 vld1_u8(byte* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AAC RID: 2732 RVA: 0x0000C29A File Offset: 0x0000A49A
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_u8(byte* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AAD RID: 2733 RVA: 0x0000C2A2 File Offset: 0x0000A4A2
			[DebuggerStepThrough]
			public unsafe static v64 vld1_u16(ushort* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AAE RID: 2734 RVA: 0x0000C2AA File Offset: 0x0000A4AA
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_u16(ushort* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AAF RID: 2735 RVA: 0x0000C2B2 File Offset: 0x0000A4B2
			[DebuggerStepThrough]
			public unsafe static v64 vld1_u32(uint* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AB0 RID: 2736 RVA: 0x0000C2BA File Offset: 0x0000A4BA
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_u32(uint* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AB1 RID: 2737 RVA: 0x0000C2C2 File Offset: 0x0000A4C2
			[DebuggerStepThrough]
			public unsafe static v64 vld1_u64(ulong* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AB2 RID: 2738 RVA: 0x0000C2CA File Offset: 0x0000A4CA
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_u64(ulong* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AB3 RID: 2739 RVA: 0x0000C2D2 File Offset: 0x0000A4D2
			[DebuggerStepThrough]
			public unsafe static v64 vld1_f32(float* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AB4 RID: 2740 RVA: 0x0000C2DA File Offset: 0x0000A4DA
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_f32(float* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AB5 RID: 2741 RVA: 0x0000C2E2 File Offset: 0x0000A4E2
			[DebuggerStepThrough]
			public unsafe static v64 vld1_f64(double* a0)
			{
				return *(v64*)a0;
			}

			// Token: 0x06000AB6 RID: 2742 RVA: 0x0000C2EA File Offset: 0x0000A4EA
			[DebuggerStepThrough]
			public unsafe static v128 vld1q_f64(double* a0)
			{
				return *(v128*)a0;
			}

			// Token: 0x06000AB7 RID: 2743 RVA: 0x0000C2F2 File Offset: 0x0000A4F2
			[DebuggerStepThrough]
			public unsafe static void vst1_s8(sbyte* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AB8 RID: 2744 RVA: 0x0000C2FB File Offset: 0x0000A4FB
			[DebuggerStepThrough]
			public unsafe static void vst1q_s8(sbyte* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AB9 RID: 2745 RVA: 0x0000C304 File Offset: 0x0000A504
			[DebuggerStepThrough]
			public unsafe static void vst1_s16(short* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000ABA RID: 2746 RVA: 0x0000C30D File Offset: 0x0000A50D
			[DebuggerStepThrough]
			public unsafe static void vst1q_s16(short* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000ABB RID: 2747 RVA: 0x0000C316 File Offset: 0x0000A516
			[DebuggerStepThrough]
			public unsafe static void vst1_s32(int* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000ABC RID: 2748 RVA: 0x0000C31F File Offset: 0x0000A51F
			[DebuggerStepThrough]
			public unsafe static void vst1q_s32(int* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000ABD RID: 2749 RVA: 0x0000C328 File Offset: 0x0000A528
			[DebuggerStepThrough]
			public unsafe static void vst1_s64(long* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000ABE RID: 2750 RVA: 0x0000C331 File Offset: 0x0000A531
			[DebuggerStepThrough]
			public unsafe static void vst1q_s64(long* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000ABF RID: 2751 RVA: 0x0000C33A File Offset: 0x0000A53A
			[DebuggerStepThrough]
			public unsafe static void vst1_u8(byte* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AC0 RID: 2752 RVA: 0x0000C343 File Offset: 0x0000A543
			[DebuggerStepThrough]
			public unsafe static void vst1q_u8(byte* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AC1 RID: 2753 RVA: 0x0000C34C File Offset: 0x0000A54C
			[DebuggerStepThrough]
			public unsafe static void vst1_u16(ushort* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AC2 RID: 2754 RVA: 0x0000C355 File Offset: 0x0000A555
			[DebuggerStepThrough]
			public unsafe static void vst1q_u16(ushort* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AC3 RID: 2755 RVA: 0x0000C35E File Offset: 0x0000A55E
			[DebuggerStepThrough]
			public unsafe static void vst1_u32(uint* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AC4 RID: 2756 RVA: 0x0000C367 File Offset: 0x0000A567
			[DebuggerStepThrough]
			public unsafe static void vst1q_u32(uint* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AC5 RID: 2757 RVA: 0x0000C370 File Offset: 0x0000A570
			[DebuggerStepThrough]
			public unsafe static void vst1_u64(ulong* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AC6 RID: 2758 RVA: 0x0000C379 File Offset: 0x0000A579
			[DebuggerStepThrough]
			public unsafe static void vst1q_u64(ulong* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AC7 RID: 2759 RVA: 0x0000C382 File Offset: 0x0000A582
			[DebuggerStepThrough]
			public unsafe static void vst1_f32(float* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000AC8 RID: 2760 RVA: 0x0000C38B File Offset: 0x0000A58B
			[DebuggerStepThrough]
			public unsafe static void vst1q_f32(float* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}

			// Token: 0x06000AC9 RID: 2761 RVA: 0x0000C394 File Offset: 0x0000A594
			[DebuggerStepThrough]
			public unsafe static void vst1_f64(double* a0, v64 a1)
			{
				*(v64*)a0 = a1;
			}

			// Token: 0x06000ACA RID: 2762 RVA: 0x0000C39D File Offset: 0x0000A59D
			[DebuggerStepThrough]
			public unsafe static void vst1q_f64(double* a0, v128 a1)
			{
				*(v128*)a0 = a1;
			}
		}
	}
}
