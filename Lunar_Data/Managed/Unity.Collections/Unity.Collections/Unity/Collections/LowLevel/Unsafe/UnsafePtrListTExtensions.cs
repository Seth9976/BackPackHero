using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200012C RID: 300
	[BurstCompatible]
	internal static class UnsafePtrListTExtensions
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x00020A58 File Offset: 0x0001EC58
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public static ref UnsafeList<IntPtr> ListData<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this UnsafePtrList<T> from) where T : struct, ValueType
		{
			return UnsafeUtility.As<UnsafePtrList<T>, UnsafeList<IntPtr>>(ref from);
		}
	}
}
