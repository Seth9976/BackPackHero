using System;

namespace System.IO
{
	/// <summary>Changes that might occur to a file or directory.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200083D RID: 2109
	[Flags]
	public enum WatcherChangeTypes
	{
		/// <summary>The creation, deletion, change, or renaming of a file or folder.</summary>
		// Token: 0x0400286A RID: 10346
		All = 15,
		/// <summary>The change of a file or folder. The types of changes include: changes to size, attributes, security settings, last write, and last access time.</summary>
		// Token: 0x0400286B RID: 10347
		Changed = 4,
		/// <summary>The creation of a file or folder.</summary>
		// Token: 0x0400286C RID: 10348
		Created = 1,
		/// <summary>The deletion of a file or folder.</summary>
		// Token: 0x0400286D RID: 10349
		Deleted = 2,
		/// <summary>The renaming of a file or folder.</summary>
		// Token: 0x0400286E RID: 10350
		Renamed = 8
	}
}
