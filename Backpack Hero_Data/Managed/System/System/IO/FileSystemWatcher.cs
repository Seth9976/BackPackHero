using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO
{
	/// <summary>Listens to the file system change notifications and raises events when a directory, or file in a directory, changes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000827 RID: 2087
	[DefaultEvent("Changed")]
	[IODescription("")]
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class.</summary>
		// Token: 0x0600428C RID: 17036 RVA: 0x000E7794 File Offset: 0x000E5994
		public FileSystemWatcher()
		{
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.enableRaisingEvents = false;
			this.filter = "*";
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.path = "";
			this.InitWatcher();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x0600428D RID: 17037 RVA: 0x000E77E4 File Offset: 0x000E59E4
		public FileSystemWatcher(string path)
			: this(path, "*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemWatcher" /> class, given the specified directory and type of files to monitor.</summary>
		/// <param name="path">The directory to monitor, in standard or Universal Naming Convention (UNC) notation. </param>
		/// <param name="filter">The type of files to watch. For example, "*.txt" watches for changes to all text files. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.-or- The <paramref name="filter" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is an empty string ("").-or- The path specified through the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">
		///   <paramref name="path" /> is too long.</exception>
		// Token: 0x0600428E RID: 17038 RVA: 0x000E77F4 File Offset: 0x000E59F4
		public FileSystemWatcher(string path, string filter)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (path == string.Empty)
			{
				throw new ArgumentException("Empty path", "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException("Directory does not exist", "path");
			}
			this.inited = false;
			this.start_requested = false;
			this.enableRaisingEvents = false;
			this.filter = filter;
			if (this.filter == "*.*")
			{
				this.filter = "*";
			}
			this.includeSubdirectories = false;
			this.internalBufferSize = 8192;
			this.notifyFilter = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;
			this.path = path;
			this.synchronizingObject = null;
			this.InitWatcher();
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x000E78C0 File Offset: 0x000E5AC0
		[EnvironmentPermission(SecurityAction.Assert, Read = "MONO_MANAGED_WATCHER")]
		private void InitWatcher()
		{
			object obj = FileSystemWatcher.lockobj;
			lock (obj)
			{
				if (this.watcher_handle == null)
				{
					string environmentVariable = Environment.GetEnvironmentVariable("MONO_MANAGED_WATCHER");
					int num = 0;
					bool flag2 = false;
					if (environmentVariable == null)
					{
						num = FileSystemWatcher.InternalSupportsFSW();
					}
					switch (num)
					{
					case 1:
						flag2 = DefaultWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 2:
						flag2 = FAMWatcher.GetInstance(out this.watcher, false);
						this.watcher_handle = this;
						break;
					case 3:
						flag2 = KeventWatcher.GetInstance(out this.watcher);
						this.watcher_handle = this;
						break;
					case 4:
						flag2 = FAMWatcher.GetInstance(out this.watcher, true);
						this.watcher_handle = this;
						break;
					case 6:
						flag2 = CoreFXFileSystemWatcherProxy.GetInstance(out this.watcher);
						this.watcher_handle = (this.watcher as CoreFXFileSystemWatcherProxy).NewWatcher(this);
						break;
					}
					if (num == 0 || !flag2)
					{
						if (string.Compare(environmentVariable, "disabled", true) == 0)
						{
							NullFileWatcher.GetInstance(out this.watcher);
						}
						else
						{
							DefaultWatcher.GetInstance(out this.watcher);
							this.watcher_handle = this;
						}
					}
					this.inited = true;
				}
			}
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x000E7A08 File Offset: 0x000E5C08
		[Conditional("DEBUG")]
		[Conditional("TRACE")]
		private void ShowWatcherInfo()
		{
			Console.WriteLine("Watcher implementation: {0}", (this.watcher != null) ? this.watcher.GetType().ToString() : "<none>");
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x000E7A33 File Offset: 0x000E5C33
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x000E7A3B File Offset: 0x000E5C3B
		internal bool Waiting
		{
			get
			{
				return this.waiting;
			}
			set
			{
				this.waiting = value;
			}
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06004293 RID: 17043 RVA: 0x000E7A44 File Offset: 0x000E5C44
		internal string MangledFilter
		{
			get
			{
				if (this.filter != "*.*")
				{
					return this.filter;
				}
				if (this.mangledFilter != null)
				{
					return this.mangledFilter;
				}
				return "*.*";
			}
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x000E7A74 File Offset: 0x000E5C74
		internal SearchPattern2 Pattern
		{
			get
			{
				if (this.pattern == null)
				{
					IFileWatcher fileWatcher = this.watcher;
					if (((fileWatcher != null) ? fileWatcher.GetType() : null) == typeof(KeventWatcher))
					{
						this.pattern = new SearchPattern2(this.MangledFilter, true);
					}
					else
					{
						this.pattern = new SearchPattern2(this.MangledFilter);
					}
				}
				return this.pattern;
			}
		}

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000E7AD8 File Offset: 0x000E5CD8
		internal string FullPath
		{
			get
			{
				if (this.fullpath == null)
				{
					if (this.path == null || this.path == "")
					{
						this.fullpath = Environment.CurrentDirectory;
					}
					else
					{
						this.fullpath = global::System.IO.Path.GetFullPath(this.path);
					}
				}
				return this.fullpath;
			}
		}

		/// <summary>Gets or sets a value indicating whether the component is enabled.</summary>
		/// <returns>true if the component is enabled; otherwise, false. The default is false. If you are using the component on a designer in Visual Studio 2005, the default is true.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.FileSystemWatcher" /> object has been disposed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The directory specified in <see cref="P:System.IO.FileSystemWatcher.Path" /> could not be found.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.IO.FileSystemWatcher.Path" /> has not been set or is invalid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x000E7B2B File Offset: 0x000E5D2B
		// (set) Token: 0x06004297 RID: 17047 RVA: 0x000E7B34 File Offset: 0x000E5D34
		[IODescription("Flag to indicate if this instance is active")]
		[DefaultValue(false)]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.enableRaisingEvents;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.start_requested = true;
				if (!this.inited)
				{
					return;
				}
				if (value == this.enableRaisingEvents)
				{
					return;
				}
				this.enableRaisingEvents = value;
				if (value)
				{
					this.Start();
					return;
				}
				this.Stop();
				this.start_requested = false;
			}
		}

		/// <summary>Gets or sets the filter string used to determine what files are monitored in a directory.</summary>
		/// <returns>The filter string. The default is "*.*" (Watches all files.) </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x000E7B92 File Offset: 0x000E5D92
		// (set) Token: 0x06004299 RID: 17049 RVA: 0x000E7B9C File Offset: 0x000E5D9C
		[IODescription("File name filter pattern")]
		[SettingsBindable(true)]
		[DefaultValue("*.*")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null || value == "")
				{
					value = "*";
				}
				if (!string.Equals(this.filter, value, PathInternal.StringComparison))
				{
					this.filter = ((value == "*.*") ? "*" : value);
					this.pattern = null;
					this.mangledFilter = null;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether subdirectories within the specified path should be monitored.</summary>
		/// <returns>true if you want to monitor subdirectories; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x0600429A RID: 17050 RVA: 0x000E7BFC File Offset: 0x000E5DFC
		// (set) Token: 0x0600429B RID: 17051 RVA: 0x000E7C04 File Offset: 0x000E5E04
		[IODescription("Flag to indicate we want to watch subdirectories")]
		[DefaultValue(false)]
		public bool IncludeSubdirectories
		{
			get
			{
				return this.includeSubdirectories;
			}
			set
			{
				if (this.includeSubdirectories == value)
				{
					return;
				}
				this.includeSubdirectories = value;
				if (value && this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the size (in bytes) of the internal buffer.</summary>
		/// <returns>The internal buffer size in bytes. The default is 8192 (8 KB).</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x0600429C RID: 17052 RVA: 0x000E7C2E File Offset: 0x000E5E2E
		// (set) Token: 0x0600429D RID: 17053 RVA: 0x000E7C36 File Offset: 0x000E5E36
		[DefaultValue(8192)]
		[Browsable(false)]
		public int InternalBufferSize
		{
			get
			{
				return this.internalBufferSize;
			}
			set
			{
				if (this.internalBufferSize == value)
				{
					return;
				}
				if (value < 4096)
				{
					value = 4096;
				}
				this.internalBufferSize = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the type of changes to watch for.</summary>
		/// <returns>One of the <see cref="T:System.IO.NotifyFilters" /> values. The default is the bitwise OR combination of LastWrite, FileName, and DirectoryName.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid bitwise OR combination of the <see cref="T:System.IO.NotifyFilters" /> values. </exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value that is being set is not valid.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x0600429E RID: 17054 RVA: 0x000E7C6C File Offset: 0x000E5E6C
		// (set) Token: 0x0600429F RID: 17055 RVA: 0x000E7C74 File Offset: 0x000E5E74
		[DefaultValue(NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite)]
		[IODescription("Flag to indicate which change event we want to monitor")]
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this.notifyFilter;
			}
			set
			{
				if (this.notifyFilter == value)
				{
					return;
				}
				this.notifyFilter = value;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets the path of the directory to watch.</summary>
		/// <returns>The path to monitor. The default is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The specified path does not exist or could not be found.-or- The specified path contains wildcard characters.-or- The specified path contains invalid path characters.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060042A0 RID: 17056 RVA: 0x000E7C9B File Offset: 0x000E5E9B
		// (set) Token: 0x060042A1 RID: 17057 RVA: 0x000E7CA4 File Offset: 0x000E5EA4
		[IODescription("The directory to monitor")]
		[DefaultValue("")]
		[Editor("System.Diagnostics.Design.FSWPathEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[SettingsBindable(true)]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Path
		{
			get
			{
				return this.path;
			}
			set
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				value = ((value == null) ? string.Empty : value);
				if (string.Equals(this.path, value, PathInternal.StringComparison))
				{
					return;
				}
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = Directory.Exists(value);
				}
				catch (Exception ex)
				{
				}
				if (ex != null)
				{
					throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
				}
				if (!flag)
				{
					throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
				}
				this.path = value;
				this.fullpath = null;
				if (this.enableRaisingEvents)
				{
					this.Stop();
					this.Start();
				}
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.IO.FileSystemWatcher" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060042A2 RID: 17058 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		// (set) Token: 0x060042A3 RID: 17059 RVA: 0x000E7D60 File Offset: 0x000E5F60
		[Browsable(false)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (this.Site != null && this.Site.DesignMode)
				{
					this.EnableRaisingEvents = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal the event handler calls issued as a result of a directory change.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> that represents the object used to marshal the event handler calls issued as a result of a directory change. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x060042A4 RID: 17060 RVA: 0x000E7D85 File Offset: 0x000E5F85
		// (set) Token: 0x060042A5 RID: 17061 RVA: 0x000E7D8D File Offset: 0x000E5F8D
		[Browsable(false)]
		[DefaultValue(null)]
		[IODescription("The object used to marshal the event handler calls resulting from a directory change")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060042A6 RID: 17062 RVA: 0x000E7D96 File Offset: 0x000E5F96
		public void BeginInit()
		{
			this.inited = false;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.FileSystemWatcher" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060042A7 RID: 17063 RVA: 0x000E7DA0 File Offset: 0x000E5FA0
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			try
			{
				IFileWatcher fileWatcher = this.watcher;
				if (fileWatcher != null)
				{
					fileWatcher.StopDispatching(this.watcher_handle);
				}
				IFileWatcher fileWatcher2 = this.watcher;
				if (fileWatcher2 != null)
				{
					fileWatcher2.Dispose(this.watcher_handle);
				}
			}
			catch (Exception)
			{
			}
			this.watcher_handle = null;
			this.watcher = null;
			this.disposed = true;
			base.Dispose(disposing);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060042A8 RID: 17064 RVA: 0x000E7E1C File Offset: 0x000E601C
		~FileSystemWatcher()
		{
			if (!this.disposed)
			{
				this.Dispose(false);
			}
		}

		/// <summary>Ends the initialization of a <see cref="T:System.IO.FileSystemWatcher" /> used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060042A9 RID: 17065 RVA: 0x000E7E54 File Offset: 0x000E6054
		public void EndInit()
		{
			this.inited = true;
			if (this.start_requested)
			{
				this.EnableRaisingEvents = true;
			}
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x000E7E6C File Offset: 0x000E606C
		private void RaiseEvent(Delegate ev, EventArgs arg, FileSystemWatcher.EventType evtype)
		{
			if (this.disposed)
			{
				return;
			}
			if (ev == null)
			{
				return;
			}
			if (this.synchronizingObject == null)
			{
				foreach (Delegate @delegate in ev.GetInvocationList())
				{
					switch (evtype)
					{
					case FileSystemWatcher.EventType.FileSystemEvent:
						((FileSystemEventHandler)@delegate)(this, (FileSystemEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.ErrorEvent:
						((ErrorEventHandler)@delegate)(this, (ErrorEventArgs)arg);
						break;
					case FileSystemWatcher.EventType.RenameEvent:
						((RenamedEventHandler)@delegate)(this, (RenamedEventArgs)arg);
						break;
					}
				}
				return;
			}
			this.synchronizingObject.BeginInvoke(ev, new object[] { this, arg });
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060042AB RID: 17067 RVA: 0x000E7F11 File Offset: 0x000E6111
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Changed, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Created" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060042AC RID: 17068 RVA: 0x000E7F21 File Offset: 0x000E6121
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Created, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Deleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.FileSystemEventArgs" /> that contains the event data. </param>
		// Token: 0x060042AD RID: 17069 RVA: 0x000E7F31 File Offset: 0x000E6131
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.RaiseEvent(this.Deleted, e, FileSystemWatcher.EventType.FileSystemEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
		/// <param name="e">An <see cref="T:System.IO.ErrorEventArgs" /> that contains the event data. </param>
		// Token: 0x060042AE RID: 17070 RVA: 0x000E7F41 File Offset: 0x000E6141
		protected void OnError(ErrorEventArgs e)
		{
			this.RaiseEvent(this.Error, e, FileSystemWatcher.EventType.ErrorEvent);
		}

		/// <summary>Raises the <see cref="E:System.IO.FileSystemWatcher.Renamed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.IO.RenamedEventArgs" /> that contains the event data. </param>
		// Token: 0x060042AF RID: 17071 RVA: 0x000E7F51 File Offset: 0x000E6151
		protected void OnRenamed(RenamedEventArgs e)
		{
			this.RaiseEvent(this.Renamed, e, FileSystemWatcher.EventType.RenameEvent);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060042B0 RID: 17072 RVA: 0x000E7F61 File Offset: 0x000E6161
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		/// <summary>A synchronous method that returns a structure that contains specific information on the change that occurred, given the type of change you want to monitor and the time (in milliseconds) to wait before timing out.</summary>
		/// <returns>A <see cref="T:System.IO.WaitForChangedResult" /> that contains specific information on the change that occurred.</returns>
		/// <param name="changeType">The <see cref="T:System.IO.WatcherChangeTypes" /> to watch for. </param>
		/// <param name="timeout">The time (in milliseconds) to wait before timing out. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060042B1 RID: 17073 RVA: 0x000E7F6C File Offset: 0x000E616C
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			WaitForChangedResult waitForChangedResult = default(WaitForChangedResult);
			bool flag = this.EnableRaisingEvents;
			if (!flag)
			{
				this.EnableRaisingEvents = true;
			}
			bool flag3;
			lock (this)
			{
				this.waiting = true;
				flag3 = Monitor.Wait(this, timeout);
				if (flag3)
				{
					waitForChangedResult = this.lastData;
				}
			}
			this.EnableRaisingEvents = flag;
			if (!flag3)
			{
				waitForChangedResult.TimedOut = true;
			}
			return waitForChangedResult;
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x000E7FE8 File Offset: 0x000E61E8
		internal void DispatchErrorEvents(ErrorEventArgs args)
		{
			if (this.disposed)
			{
				return;
			}
			this.OnError(args);
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x000E7FFC File Offset: 0x000E61FC
		internal void DispatchEvents(FileAction act, string filename, ref RenamedEventArgs renamed)
		{
			FileSystemWatcher.<>c__DisplayClass70_0 CS$<>8__locals1 = new FileSystemWatcher.<>c__DisplayClass70_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.filename = filename;
			if (this.disposed)
			{
				return;
			}
			if (this.waiting)
			{
				this.lastData = default(WaitForChangedResult);
			}
			switch (act)
			{
			case FileAction.Added:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Created;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnCreated(new FileSystemEventArgs(WatcherChangeTypes.Created, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Removed:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Deleted;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnDeleted(new FileSystemEventArgs(WatcherChangeTypes.Deleted, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.Modified:
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Changed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnChanged(new FileSystemEventArgs(WatcherChangeTypes.Changed, CS$<>8__locals1.<>4__this.path, CS$<>8__locals1.filename));
				});
				return;
			case FileAction.RenamedOldName:
				if (renamed != null)
				{
					this.OnRenamed(renamed);
				}
				this.lastData.OldName = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, CS$<>8__locals1.filename, "");
				return;
			case FileAction.RenamedNewName:
			{
				this.lastData.Name = CS$<>8__locals1.filename;
				this.lastData.ChangeType = WatcherChangeTypes.Renamed;
				if (renamed == null)
				{
					renamed = new RenamedEventArgs(WatcherChangeTypes.Renamed, this.path, "", CS$<>8__locals1.filename);
				}
				RenamedEventArgs renamed_ref = renamed;
				Task.Run(delegate
				{
					CS$<>8__locals1.<>4__this.OnRenamed(renamed_ref);
				});
				renamed = null;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x000E81C0 File Offset: 0x000E63C0
		private void Start()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StartDispatching(this.watcher_handle);
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x000E81EA File Offset: 0x000E63EA
		private void Stop()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.watcher_handle == null)
			{
				return;
			}
			IFileWatcher fileWatcher = this.watcher;
			if (fileWatcher == null)
			{
				return;
			}
			fileWatcher.StopDispatching(this.watcher_handle);
		}

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060042B6 RID: 17078 RVA: 0x000E8214 File Offset: 0x000E6414
		// (remove) Token: 0x060042B7 RID: 17079 RVA: 0x000E824C File Offset: 0x000E644C
		[IODescription("Occurs when a file/directory change matches the filter")]
		public event FileSystemEventHandler Changed;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is created.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060042B8 RID: 17080 RVA: 0x000E8284 File Offset: 0x000E6484
		// (remove) Token: 0x060042B9 RID: 17081 RVA: 0x000E82BC File Offset: 0x000E64BC
		[IODescription("Occurs when a file/directory creation matches the filter")]
		public event FileSystemEventHandler Created;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is deleted.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060042BA RID: 17082 RVA: 0x000E82F4 File Offset: 0x000E64F4
		// (remove) Token: 0x060042BB RID: 17083 RVA: 0x000E832C File Offset: 0x000E652C
		[IODescription("Occurs when a file/directory deletion matches the filter")]
		public event FileSystemEventHandler Deleted;

		/// <summary>Occurs when the instance of <see cref="T:System.IO.FileSystemWatcher" /> is unable to continue monitoring changes or when the internal buffer overflows.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060042BC RID: 17084 RVA: 0x000E8364 File Offset: 0x000E6564
		// (remove) Token: 0x060042BD RID: 17085 RVA: 0x000E839C File Offset: 0x000E659C
		[Browsable(false)]
		public event ErrorEventHandler Error;

		/// <summary>Occurs when a file or directory in the specified <see cref="P:System.IO.FileSystemWatcher.Path" /> is renamed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060042BE RID: 17086 RVA: 0x000E83D4 File Offset: 0x000E65D4
		// (remove) Token: 0x060042BF RID: 17087 RVA: 0x000E840C File Offset: 0x000E660C
		[IODescription("Occurs when a file/directory rename matches the filter")]
		public event RenamedEventHandler Renamed;

		// Token: 0x060042C0 RID: 17088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalSupportsFSW();

		// Token: 0x040027C6 RID: 10182
		private bool inited;

		// Token: 0x040027C7 RID: 10183
		private bool start_requested;

		// Token: 0x040027C8 RID: 10184
		private bool enableRaisingEvents;

		// Token: 0x040027C9 RID: 10185
		private string filter;

		// Token: 0x040027CA RID: 10186
		private bool includeSubdirectories;

		// Token: 0x040027CB RID: 10187
		private int internalBufferSize;

		// Token: 0x040027CC RID: 10188
		private NotifyFilters notifyFilter;

		// Token: 0x040027CD RID: 10189
		private string path;

		// Token: 0x040027CE RID: 10190
		private string fullpath;

		// Token: 0x040027CF RID: 10191
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x040027D0 RID: 10192
		private WaitForChangedResult lastData;

		// Token: 0x040027D1 RID: 10193
		private bool waiting;

		// Token: 0x040027D2 RID: 10194
		private SearchPattern2 pattern;

		// Token: 0x040027D3 RID: 10195
		private bool disposed;

		// Token: 0x040027D4 RID: 10196
		private string mangledFilter;

		// Token: 0x040027D5 RID: 10197
		private IFileWatcher watcher;

		// Token: 0x040027D6 RID: 10198
		private object watcher_handle;

		// Token: 0x040027D7 RID: 10199
		private static object lockobj = new object();

		// Token: 0x02000828 RID: 2088
		private enum EventType
		{
			// Token: 0x040027DE RID: 10206
			FileSystemEvent,
			// Token: 0x040027DF RID: 10207
			ErrorEvent,
			// Token: 0x040027E0 RID: 10208
			RenameEvent
		}
	}
}
