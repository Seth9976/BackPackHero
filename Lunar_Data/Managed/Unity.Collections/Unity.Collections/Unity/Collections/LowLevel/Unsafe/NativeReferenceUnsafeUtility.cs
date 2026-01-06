using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200011A RID: 282
	[BurstCompatible]
	public static class NativeReferenceUnsafeUtility
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E97A File Offset: 0x0001CB7A
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetUnsafePtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E97A File Offset: 0x0001CB7A
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetUnsafeReadOnlyPtr<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E97A File Offset: 0x0001CB7A
		[BurstCompatible(GenericTypeArguments = new Type[] { typeof(int) })]
		public unsafe static void* GetUnsafePtrWithoutChecks<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(this NativeReference<T> reference) where T : struct, ValueType
		{
			return reference.m_Data;
		}
	}
}
