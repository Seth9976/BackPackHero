using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200002C RID: 44
	[BurstCompile]
	internal static class BurstedSpriteSkinUtilities
	{
		// Token: 0x06000108 RID: 264 RVA: 0x0000505E File Offset: 0x0000325E
		[BurstCompile]
		internal static bool ValidateBoneWeights(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount)
		{
			return BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.Invoke(in boneWeights, bindPoseCount);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005068 File Offset: 0x00003268
		[MethodImpl(256)]
		public static bool ValidateBoneWeights$BurstManaged(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount)
		{
			NativeSlice<BoneWeight> nativeSlice = boneWeights;
			int length = nativeSlice.Length;
			for (int i = 0; i < length; i++)
			{
				nativeSlice = boneWeights;
				BoneWeight boneWeight = nativeSlice[i];
				int boneIndex = boneWeight.boneIndex0;
				int boneIndex2 = boneWeight.boneIndex1;
				int boneIndex3 = boneWeight.boneIndex2;
				int boneIndex4 = boneWeight.boneIndex3;
				if (boneIndex < 0 || boneIndex >= bindPoseCount || boneIndex2 < 0 || boneIndex2 >= bindPoseCount || boneIndex3 < 0 || boneIndex3 >= bindPoseCount || boneIndex4 < 0 || boneIndex4 >= bindPoseCount)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x0600010B RID: 267
		public delegate bool ValidateBoneWeights_000000E1$PostfixBurstDelegate(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount);

		// Token: 0x0200002E RID: 46
		internal static class ValidateBoneWeights_000000E1$BurstDirectCall
		{
			// Token: 0x0600010E RID: 270 RVA: 0x000050EF File Offset: 0x000032EF
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.Pointer == 0)
				{
					BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.DeferredCompilation, methodof(BurstedSpriteSkinUtilities.ValidateBoneWeights$BurstManaged(ref NativeSlice<BoneWeight>, int)).MethodHandle, typeof(BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.Pointer;
			}

			// Token: 0x0600010F RID: 271 RVA: 0x0000511C File Offset: 0x0000331C
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00005134 File Offset: 0x00003334
			public static void Constructor()
			{
				BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BurstedSpriteSkinUtilities.ValidateBoneWeights(ref NativeSlice<BoneWeight>, int)).MethodHandle);
			}

			// Token: 0x06000111 RID: 273 RVA: 0x000026F3 File Offset: 0x000008F3
			public static void Initialize()
			{
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00005145 File Offset: 0x00003345
			// Note: this type is marked as 'beforefieldinit'.
			static ValidateBoneWeights_000000E1$BurstDirectCall()
			{
				BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.Constructor();
			}

			// Token: 0x06000113 RID: 275 RVA: 0x0000514C File Offset: 0x0000334C
			public static bool Invoke(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = BurstedSpriteSkinUtilities.ValidateBoneWeights_000000E1$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Boolean(Unity.Collections.NativeSlice`1<UnityEngine.BoneWeight>&,System.Int32), ref boneWeights, bindPoseCount, functionPointer);
					}
				}
				return BurstedSpriteSkinUtilities.ValidateBoneWeights$BurstManaged(in boneWeights, bindPoseCount);
			}

			// Token: 0x0400006D RID: 109
			private static IntPtr Pointer;

			// Token: 0x0400006E RID: 110
			private static IntPtr DeferredCompilation;
		}
	}
}
