using System;
using System.Runtime.InteropServices;
using AOT;

// Token: 0x02000007 RID: 7
public class SystemMemory
{
	// Token: 0x06000011 RID: 17 RVA: 0x0000217D File Offset: 0x0000037D
	[MonoPInvokeCallback(typeof(SystemMemory.EOS_GenericAlignAlloc))]
	public static IntPtr GenericAlignAlloc(UIntPtr sizeInBytes, UIntPtr alignmentInBytes)
	{
		return SystemMemory.Mem_generic_align_alloc(sizeInBytes, alignmentInBytes);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002186 File Offset: 0x00000386
	[MonoPInvokeCallback(typeof(SystemMemory.EOS_GenericAlignRealloc))]
	public static IntPtr GenericAlignRealloc(IntPtr ptr, UIntPtr sizeInBytes, UIntPtr alignmentInBytes)
	{
		return SystemMemory.Mem_generic_align_realloc(ptr, sizeInBytes, alignmentInBytes);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002190 File Offset: 0x00000390
	[MonoPInvokeCallback(typeof(SystemMemory.EOS_GenericFree))]
	public static void GenericFree(IntPtr ptr)
	{
		SystemMemory.Mem_generic_free(ptr);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002198 File Offset: 0x00000398
	public static void GetAllocatorFunctions(out IntPtr alloc, out IntPtr realloc, out IntPtr free)
	{
		alloc = IntPtr.Zero;
		realloc = IntPtr.Zero;
		free = IntPtr.Zero;
	}

	// Token: 0x06000015 RID: 21
	[DllImport("DynamicLibraryLoaderHelper")]
	public static extern IntPtr Mem_generic_align_alloc(UIntPtr size_in_bytes, UIntPtr alignment_in_bytes);

	// Token: 0x06000016 RID: 22
	[DllImport("DynamicLibraryLoaderHelper")]
	public static extern IntPtr Mem_generic_align_realloc(IntPtr ptr, UIntPtr size_in_bytes, UIntPtr alignment_in_bytes);

	// Token: 0x06000017 RID: 23
	[DllImport("DynamicLibraryLoaderHelper")]
	public static extern void Mem_generic_free(IntPtr ptr);

	// Token: 0x04000008 RID: 8
	private const string DLLHBinaryName = "DynamicLibraryLoaderHelper";

	// Token: 0x02000020 RID: 32
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MemCounters
	{
		// Token: 0x04000036 RID: 54
		public long currentMemoryAllocatedInBytes;
	}

	// Token: 0x02000021 RID: 33
	// (Invoke) Token: 0x0600005A RID: 90
	public delegate IntPtr EOS_GenericAlignAlloc(UIntPtr sizeInBytes, UIntPtr alignmentInBytes);

	// Token: 0x02000022 RID: 34
	// (Invoke) Token: 0x0600005E RID: 94
	public delegate IntPtr EOS_GenericAlignRealloc(IntPtr ptr, UIntPtr sizeInBytes, UIntPtr alignmentInBytes);

	// Token: 0x02000023 RID: 35
	// (Invoke) Token: 0x06000062 RID: 98
	public delegate void EOS_GenericFree(IntPtr ptr);
}
