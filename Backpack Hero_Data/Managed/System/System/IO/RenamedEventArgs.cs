using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200080D RID: 2061
	public class RenamedEventArgs : FileSystemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.RenamedEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values. </param>
		/// <param name="directory">The name of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		/// <param name="oldName">The old name of the affected file or directory. </param>
		// Token: 0x06004224 RID: 16932 RVA: 0x000E5DBC File Offset: 0x000E3FBC
		public RenamedEventArgs(WatcherChangeTypes changeType, string directory, string name, string oldName)
			: base(changeType, directory, name)
		{
			this._oldName = oldName;
			this._oldFullPath = FileSystemEventArgs.Combine(directory, oldName);
		}

		/// <summary>Gets the previous fully qualified path of the affected file or directory.</summary>
		/// <returns>The previous fully qualified path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000E5DDD File Offset: 0x000E3FDD
		public string OldFullPath
		{
			get
			{
				return this._oldFullPath;
			}
		}

		/// <summary>Gets the old name of the affected file or directory.</summary>
		/// <returns>The previous name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06004226 RID: 16934 RVA: 0x000E5DE5 File Offset: 0x000E3FE5
		public string OldName
		{
			get
			{
				return this._oldName;
			}
		}

		// Token: 0x04002763 RID: 10083
		private readonly string _oldName;

		// Token: 0x04002764 RID: 10084
		private readonly string _oldFullPath;
	}
}
