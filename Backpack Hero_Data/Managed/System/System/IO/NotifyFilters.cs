using System;

namespace System.IO
{
	/// <summary>Specifies changes to watch for in a file or folder.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000838 RID: 2104
	[Flags]
	public enum NotifyFilters
	{
		/// <summary>The attributes of the file or folder.</summary>
		// Token: 0x04002851 RID: 10321
		Attributes = 4,
		/// <summary>The time the file or folder was created.</summary>
		// Token: 0x04002852 RID: 10322
		CreationTime = 64,
		/// <summary>The name of the directory.</summary>
		// Token: 0x04002853 RID: 10323
		DirectoryName = 2,
		/// <summary>The name of the file.</summary>
		// Token: 0x04002854 RID: 10324
		FileName = 1,
		/// <summary>The date the file or folder was last opened.</summary>
		// Token: 0x04002855 RID: 10325
		LastAccess = 32,
		/// <summary>The date the file or folder last had anything written to it.</summary>
		// Token: 0x04002856 RID: 10326
		LastWrite = 16,
		/// <summary>The security settings of the file or folder.</summary>
		// Token: 0x04002857 RID: 10327
		Security = 256,
		/// <summary>The size of the file or folder.</summary>
		// Token: 0x04002858 RID: 10328
		Size = 8
	}
}
