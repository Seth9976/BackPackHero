using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Rendering.HybridV2
{
	// Token: 0x02000071 RID: 113
	public class HybridV2ShaderReflection
	{
		// Token: 0x060001B4 RID: 436
		[FreeFunction("ShaderScripting::GetDOTSInstancingCbuffersPointer")]
		[MethodImpl(4096)]
		private static extern IntPtr GetDOTSInstancingCbuffersPointer([NotNull("ArgumentNullException")] Shader shader, ref int cbufferCount);

		// Token: 0x060001B5 RID: 437
		[FreeFunction("ShaderScripting::GetDOTSInstancingPropertiesPointer")]
		[MethodImpl(4096)]
		private static extern IntPtr GetDOTSInstancingPropertiesPointer([NotNull("ArgumentNullException")] Shader shader, ref int propertyCount);

		// Token: 0x060001B6 RID: 438
		[FreeFunction("Shader::GetDOTSReflectionVersionNumber")]
		[MethodImpl(4096)]
		public static extern uint GetDOTSReflectionVersionNumber();

		// Token: 0x060001B7 RID: 439 RVA: 0x00003944 File Offset: 0x00001B44
		public unsafe static NativeArray<DOTSInstancingCbuffer> GetDOTSInstancingCbuffers(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingCbuffer> nativeArray;
			if (flag)
			{
				nativeArray = default(NativeArray<DOTSInstancingCbuffer>);
			}
			else
			{
				int num = 0;
				IntPtr dotsinstancingCbuffersPointer = HybridV2ShaderReflection.GetDOTSInstancingCbuffersPointer(shader, ref num);
				bool flag2 = dotsinstancingCbuffersPointer == IntPtr.Zero;
				if (flag2)
				{
					nativeArray = default(NativeArray<DOTSInstancingCbuffer>);
				}
				else
				{
					NativeArray<DOTSInstancingCbuffer> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingCbuffer>((void*)dotsinstancingCbuffersPointer, num, Allocator.Temp);
					nativeArray = nativeArray2;
				}
			}
			return nativeArray;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000039AC File Offset: 0x00001BAC
		public unsafe static NativeArray<DOTSInstancingProperty> GetDOTSInstancingProperties(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingProperty> nativeArray;
			if (flag)
			{
				nativeArray = default(NativeArray<DOTSInstancingProperty>);
			}
			else
			{
				int num = 0;
				IntPtr dotsinstancingPropertiesPointer = HybridV2ShaderReflection.GetDOTSInstancingPropertiesPointer(shader, ref num);
				bool flag2 = dotsinstancingPropertiesPointer == IntPtr.Zero;
				if (flag2)
				{
					nativeArray = default(NativeArray<DOTSInstancingProperty>);
				}
				else
				{
					NativeArray<DOTSInstancingProperty> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingProperty>((void*)dotsinstancingPropertiesPointer, num, Allocator.Temp);
					nativeArray = nativeArray2;
				}
			}
			return nativeArray;
		}
	}
}
