using System;

namespace System.IO
{
	/// <summary>Provides data for the directory events: <see cref="E:System.IO.FileSystemWatcher.Changed" />, <see cref="E:System.IO.FileSystemWatcher.Created" />, <see cref="E:System.IO.FileSystemWatcher.Deleted" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000807 RID: 2055
	public class FileSystemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemEventArgs" /> class.</summary>
		/// <param name="changeType">One of the <see cref="T:System.IO.WatcherChangeTypes" /> values, which represents the kind of change detected in the file system. </param>
		/// <param name="directory">The root directory of the affected file or directory. </param>
		/// <param name="name">The name of the affected file or directory. </param>
		// Token: 0x060041E7 RID: 16871 RVA: 0x000E54EC File Offset: 0x000E36EC
		public FileSystemEventArgs(WatcherChangeTypes changeType, string directory, string name)
		{
			this._changeType = changeType;
			this._name = name;
			this._fullPath = Path.GetFullPath(FileSystemEventArgs.Combine(directory, name));
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000E5514 File Offset: 0x000E3714
		internal static string Combine(string directoryPath, string name)
		{
			bool flag = false;
			if (directoryPath.Length > 0)
			{
				char c = directoryPath[directoryPath.Length - 1];
				flag = c == Path.DirectorySeparatorChar || c == Path.AltDirectorySeparatorChar;
			}
			if (!flag)
			{
				return directoryPath + Path.DirectorySeparatorChar.ToString() + name;
			}
			return directoryPath + name;
		}

		/// <summary>Gets the type of directory event that occurred.</summary>
		/// <returns>One of the <see cref="T:System.IO.WatcherChangeTypes" /> values that represents the kind of change detected in the file system.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060041E9 RID: 16873 RVA: 0x000E556B File Offset: 0x000E376B
		public WatcherChangeTypes ChangeType
		{
			get
			{
				return this._changeType;
			}
		}

		/// <summary>Gets the fully qualifed path of the affected file or directory.</summary>
		/// <returns>The path of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060041EA RID: 16874 RVA: 0x000E5573 File Offset: 0x000E3773
		public string FullPath
		{
			get
			{
				return this._fullPath;
			}
		}

		/// <summary>Gets the name of the affected file or directory.</summary>
		/// <returns>The name of the affected file or directory.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060041EB RID: 16875 RVA: 0x000E557B File Offset: 0x000E377B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0400274E RID: 10062
		private readonly WatcherChangeTypes _changeType;

		// Token: 0x0400274F RID: 10063
		private readonly string _name;

		// Token: 0x04002750 RID: 10064
		private readonly string _fullPath;
	}
}
