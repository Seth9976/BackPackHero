using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000036 RID: 54
	[BurstCompile]
	internal static class BurstedSpriteSkinUtilities
	{
		// Token: 0x0600012E RID: 302 RVA: 0x00006C06 File Offset: 0x00004E06
		[BurstCompile]
		internal static bool ValidateBoneWeights(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount)
		{
			return BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.Invoke(in boneWeights, bindPoseCount);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006C10 File Offset: 0x00004E10
		[BurstCompile]
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

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x06000131 RID: 305
		public delegate bool ValidateBoneWeights_00000106$PostfixBurstDelegate(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount);

		// Token: 0x02000038 RID: 56
		internal static class ValidateBoneWeights_00000106$BurstDirectCall
		{
			// Token: 0x06000134 RID: 308 RVA: 0x00006C97 File Offset: 0x00004E97
			[BurstDiscard]
			private static void GetFunctionPointerDiscard(ref IntPtr A_0)
			{
				if (BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.Pointer == 0)
				{
					BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.Pointer = BurstCompiler.GetILPPMethodFunctionPointer2(BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.DeferredCompilation, methodof(BurstedSpriteSkinUtilities.ValidateBoneWeights$BurstManaged(ref NativeSlice<BoneWeight>, int)).MethodHandle, typeof(BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$PostfixBurstDelegate).TypeHandle);
				}
				A_0 = BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.Pointer;
			}

			// Token: 0x06000135 RID: 309 RVA: 0x00006CC4 File Offset: 0x00004EC4
			private static IntPtr GetFunctionPointer()
			{
				IntPtr intPtr = (IntPtr)0;
				BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.GetFunctionPointerDiscard(ref intPtr);
				return intPtr;
			}

			// Token: 0x06000136 RID: 310 RVA: 0x00006CDC File Offset: 0x00004EDC
			public static void Constructor()
			{
				BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.DeferredCompilation = BurstCompiler.CompileILPPMethod2(methodof(BurstedSpriteSkinUtilities.ValidateBoneWeights(ref NativeSlice<BoneWeight>, int)).MethodHandle);
			}

			// Token: 0x06000137 RID: 311 RVA: 0x000026F3 File Offset: 0x000008F3
			public static void Initialize()
			{
			}

			// Token: 0x06000138 RID: 312 RVA: 0x00006CED File Offset: 0x00004EED
			// Note: this type is marked as 'beforefieldinit'.
			static ValidateBoneWeights_00000106$BurstDirectCall()
			{
				BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.Constructor();
			}

			// Token: 0x06000139 RID: 313 RVA: 0x00006CF4 File Offset: 0x00004EF4
			public static bool Invoke(in NativeSlice<BoneWeight> boneWeights, int bindPoseCount)
			{
				if (BurstCompiler.IsEnabled)
				{
					IntPtr functionPointer = BurstedSpriteSkinUtilities.ValidateBoneWeights_00000106$BurstDirectCall.GetFunctionPointer();
					if (functionPointer != 0)
					{
						return calli(System.Boolean(Unity.Collections.NativeSlice`1<UnityEngine.BoneWeight>&,System.Int32), ref boneWeights, bindPoseCount, functionPointer);
					}
				}
				return BurstedSpriteSkinUtilities.ValidateBoneWeights$BurstManaged(in boneWeights, bindPoseCount);
			}

			// Token: 0x040000C6 RID: 198
			private static IntPtr Pointer;

			// Token: 0x040000C7 RID: 199
			private static IntPtr DeferredCompilation;
		}
	}
}
