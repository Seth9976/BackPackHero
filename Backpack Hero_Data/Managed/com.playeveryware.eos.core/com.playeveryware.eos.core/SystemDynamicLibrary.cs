using System;

// Token: 0x02000006 RID: 6
public class SystemDynamicLibrary
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002145 File Offset: 0x00000345
	private SystemDynamicLibrary()
	{
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600000C RID: 12 RVA: 0x0000214D File Offset: 0x0000034D
	public static SystemDynamicLibrary Instance
	{
		get
		{
			if (SystemDynamicLibrary.s_instance == null)
			{
				SystemDynamicLibrary.s_instance = new SystemDynamicLibrary();
			}
			return SystemDynamicLibrary.s_instance;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002165 File Offset: 0x00000365
	public static IntPtr GetHandleForModule(string moduleName)
	{
		return IntPtr.Zero;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000216C File Offset: 0x0000036C
	public IntPtr LoadLibraryAtPath(string libraryPath)
	{
		return IntPtr.Zero;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002173 File Offset: 0x00000373
	public bool UnloadLibrary(IntPtr libraryHandle)
	{
		return true;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002176 File Offset: 0x00000376
	public IntPtr LoadFunctionWithName(IntPtr libraryHandle, string functionName)
	{
		return IntPtr.Zero;
	}

	// Token: 0x04000006 RID: 6
	private static SystemDynamicLibrary s_instance;

	// Token: 0x04000007 RID: 7
	private IntPtr DLLHContex;
}
