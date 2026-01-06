using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x02000287 RID: 647
	[NativeHeader("PlatformDependent/Win/Bindings/InputBindings.h")]
	public static class Input
	{
		// Token: 0x06001C1B RID: 7195
		[NativeName("ForwardRawInput")]
		[ThreadSafe]
		[StaticAccessor("", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private unsafe static extern void ForwardRawInputImpl(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize);

		// Token: 0x06001C1C RID: 7196 RVA: 0x0002D0AD File Offset: 0x0002B2AD
		public unsafe static void ForwardRawInput(IntPtr rawInputHeaderIndices, IntPtr rawInputDataIndices, uint indicesCount, IntPtr rawInputData, uint rawInputDataSize)
		{
			Input.ForwardRawInput((uint*)(void*)rawInputHeaderIndices, (uint*)(void*)rawInputDataIndices, indicesCount, (byte*)(void*)rawInputData, rawInputDataSize);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x0002D0D0 File Offset: 0x0002B2D0
		public unsafe static void ForwardRawInput(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize)
		{
			bool flag = rawInputHeaderIndices == null;
			if (flag)
			{
				throw new ArgumentNullException("rawInputHeaderIndices");
			}
			bool flag2 = rawInputDataIndices == null;
			if (flag2)
			{
				throw new ArgumentNullException("rawInputDataIndices");
			}
			bool flag3 = rawInputData == null;
			if (flag3)
			{
				throw new ArgumentNullException("rawInputData");
			}
			Input.ForwardRawInputImpl(rawInputHeaderIndices, rawInputDataIndices, indicesCount, rawInputData, rawInputDataSize);
		}
	}
}
