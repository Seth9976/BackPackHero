using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005E RID: 94
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	public static class AnimationStreamHandleUtility
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x000070E4 File Offset: 0x000052E4
		public static void WriteInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num, useMask);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00007124 File Offset: 0x00005324
		public static void WriteFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num, useMask);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00007164 File Offset: 0x00005364
		public static void ReadInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000071A4 File Offset: 0x000053A4
		public static void ReadFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num);
			}
		}

		// Token: 0x060004E0 RID: 1248
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private unsafe static extern void ReadStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count);

		// Token: 0x060004E1 RID: 1249
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private unsafe static extern void ReadStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count);

		// Token: 0x060004E2 RID: 1250
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private unsafe static extern void WriteStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count, bool useMask);

		// Token: 0x060004E3 RID: 1251
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private unsafe static extern void WriteStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count, bool useMask);
	}
}
