using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Enumeration;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.IO.CoreFX
{
	// Token: 0x02000853 RID: 2131
	public class FileSystemWatcher : Component, ISupportInitialize
	{
		// Token: 0x060043E1 RID: 17377 RVA: 0x000EB200 File Offset: 0x000E9400
		private void StartRaisingEvents()
		{
			if (this.IsSuspended())
			{
				this._enabled = true;
				return;
			}
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			this._directoryHandle = global::Interop.Kernel32.CreateFile(this._directory, 1, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, 1107296256);
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle = null;
				throw new FileNotFoundException(SR.Format("Error reading the {0} directory.", this._directory));
			}
			FileSystemWatcher.AsyncReadState asyncReadState;
			try
			{
				int num = Interlocked.Increment(ref this._currentSession);
				byte[] array = this.AllocateBuffer();
				asyncReadState = new FileSystemWatcher.AsyncReadState(num, array, this._directoryHandle, ThreadPoolBoundHandle.BindHandle(this._directoryHandle));
				asyncReadState.PreAllocatedOverlapped = new PreAllocatedOverlapped(new IOCompletionCallback(this.ReadDirectoryChangesCallback), asyncReadState, array);
			}
			catch
			{
				this._directoryHandle.Dispose();
				this._directoryHandle = null;
				throw;
			}
			this._enabled = true;
			this.Monitor(asyncReadState);
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x000EB2E8 File Offset: 0x000E94E8
		private void StopRaisingEvents()
		{
			this._enabled = false;
			if (this.IsSuspended())
			{
				return;
			}
			if (FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				return;
			}
			Interlocked.Increment(ref this._currentSession);
			this._directoryHandle.Dispose();
			this._directoryHandle = null;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x000EB326 File Offset: 0x000E9526
		private void FinalizeDispose()
		{
			if (!FileSystemWatcher.IsHandleInvalid(this._directoryHandle))
			{
				this._directoryHandle.Dispose();
			}
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x000EB340 File Offset: 0x000E9540
		private static bool IsHandleInvalid(SafeFileHandle handle)
		{
			return handle == null || handle.IsInvalid || handle.IsClosed;
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000EB358 File Offset: 0x000E9558
		private unsafe void Monitor(FileSystemWatcher.AsyncReadState state)
		{
			NativeOverlapped* ptr = null;
			bool flag = false;
			try
			{
				if (this._enabled && !FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
				{
					ptr = state.ThreadPoolBinding.AllocateNativeOverlapped(state.PreAllocatedOverlapped);
					int num;
					flag = global::Interop.Kernel32.ReadDirectoryChangesW(state.DirectoryHandle, state.Buffer, this._internalBufferSize, this._includeSubdirectories, (int)this._notifyFilters, out num, ptr, IntPtr.Zero);
				}
			}
			catch (ObjectDisposedException)
			{
			}
			catch (ArgumentNullException)
			{
			}
			finally
			{
				if (!flag)
				{
					if (ptr != null)
					{
						state.ThreadPoolBinding.FreeNativeOverlapped(ptr);
					}
					state.PreAllocatedOverlapped.Dispose();
					state.ThreadPoolBinding.Dispose();
					if (!FileSystemWatcher.IsHandleInvalid(state.DirectoryHandle))
					{
						this.OnError(new ErrorEventArgs(new Win32Exception()));
					}
				}
			}
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x000EB43C File Offset: 0x000E963C
		private unsafe void ReadDirectoryChangesCallback(uint errorCode, uint numBytes, NativeOverlapped* overlappedPointer)
		{
			FileSystemWatcher.AsyncReadState asyncReadState = (FileSystemWatcher.AsyncReadState)ThreadPoolBoundHandle.GetNativeOverlappedState(overlappedPointer);
			try
			{
				if (!FileSystemWatcher.IsHandleInvalid(asyncReadState.DirectoryHandle))
				{
					if (errorCode != 0U)
					{
						if (errorCode != 995U)
						{
							this.OnError(new ErrorEventArgs(new Win32Exception((int)errorCode)));
							this.EnableRaisingEvents = false;
						}
					}
					else if (asyncReadState.Session == Volatile.Read(ref this._currentSession))
					{
						if (numBytes == 0U)
						{
							this.NotifyInternalBufferOverflowEvent();
						}
						else
						{
							this.ParseEventBufferAndNotifyForEach(asyncReadState.Buffer);
						}
					}
				}
			}
			finally
			{
				asyncReadState.ThreadPoolBinding.FreeNativeOverlapped(overlappedPointer);
				this.Monitor(asyncReadState);
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000EB4DC File Offset: 0x000E96DC
		private unsafe void ParseEventBufferAndNotifyForEach(byte[] buffer)
		{
			int num = 0;
			string text = null;
			int num2;
			do
			{
				int num3;
				string text2;
				fixed (byte* ptr = &buffer[0])
				{
					byte* ptr2 = ptr;
					num2 = *(int*)(ptr2 + num);
					num3 = *(int*)(ptr2 + num + 4);
					int num4 = *(int*)(ptr2 + num + 8);
					text2 = new string((char*)(ptr2 + num + 12), 0, num4 / 2);
				}
				if (num3 == 4)
				{
					text = text2;
				}
				else if (num3 == 5)
				{
					this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, text2, text);
					text = null;
				}
				else
				{
					if (text != null)
					{
						this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
						text = null;
					}
					switch (num3)
					{
					case 1:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Created, text2);
						break;
					case 2:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Deleted, text2);
						break;
					case 3:
						this.NotifyFileSystemEventArgs(WatcherChangeTypes.Changed, text2);
						break;
					}
				}
				num += num2;
			}
			while (num2 != 0);
			if (text != null)
			{
				this.NotifyRenameEventArgs(WatcherChangeTypes.Renamed, null, text);
			}
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000EB5C1 File Offset: 0x000E97C1
		public FileSystemWatcher()
		{
			this._directory = string.Empty;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x000EB5F2 File Offset: 0x000E97F2
		public FileSystemWatcher(string path)
		{
			FileSystemWatcher.CheckPathValidity(path);
			this._directory = path;
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x000EB628 File Offset: 0x000E9828
		public FileSystemWatcher(string path, string filter)
		{
			FileSystemWatcher.CheckPathValidity(path);
			this._directory = path;
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			this.Filter = filter;
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x000EB67C File Offset: 0x000E987C
		// (set) Token: 0x060043EC RID: 17388 RVA: 0x000EB684 File Offset: 0x000E9884
		public NotifyFilters NotifyFilter
		{
			get
			{
				return this._notifyFilters;
			}
			set
			{
				if ((value & ~(NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size)) != (NotifyFilters)0)
				{
					throw new ArgumentException(SR.Format("The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.", "value", (int)value, "NotifyFilters"));
				}
				if (this._notifyFilters != value)
				{
					this._notifyFilters = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x000EB6D0 File Offset: 0x000E98D0
		public Collection<string> Filters
		{
			get
			{
				return this._filters;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x000EB6D8 File Offset: 0x000E98D8
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x000EB6E0 File Offset: 0x000E98E0
		public bool EnableRaisingEvents
		{
			get
			{
				return this._enabled;
			}
			set
			{
				if (this._enabled == value)
				{
					return;
				}
				if (this.IsSuspended())
				{
					this._enabled = value;
					return;
				}
				if (value)
				{
					this.StartRaisingEventsIfNotDisposed();
					return;
				}
				this.StopRaisingEvents();
			}
		}

		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x000EB70C File Offset: 0x000E990C
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x000EB72D File Offset: 0x000E992D
		public string Filter
		{
			get
			{
				if (this.Filters.Count != 0)
				{
					return this.Filters[0];
				}
				return "*";
			}
			set
			{
				this.Filters.Clear();
				this.Filters.Add(value);
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000EB746 File Offset: 0x000E9946
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000EB74E File Offset: 0x000E994E
		public bool IncludeSubdirectories
		{
			get
			{
				return this._includeSubdirectories;
			}
			set
			{
				if (this._includeSubdirectories != value)
				{
					this._includeSubdirectories = value;
					this.Restart();
				}
			}
		}

		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000EB766 File Offset: 0x000E9966
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x000EB76E File Offset: 0x000E996E
		public int InternalBufferSize
		{
			get
			{
				return (int)this._internalBufferSize;
			}
			set
			{
				if ((ulong)this._internalBufferSize != (ulong)((long)value))
				{
					if (value < 4096)
					{
						this._internalBufferSize = 4096U;
					}
					else
					{
						this._internalBufferSize = (uint)value;
					}
					this.Restart();
				}
			}
		}

		// Token: 0x060043F6 RID: 17398 RVA: 0x000EB7A0 File Offset: 0x000E99A0
		private byte[] AllocateBuffer()
		{
			byte[] array;
			try
			{
				array = new byte[this._internalBufferSize];
			}
			catch (OutOfMemoryException)
			{
				throw new OutOfMemoryException(SR.Format("The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer.", this._internalBufferSize));
			}
			return array;
		}

		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x000EB7E8 File Offset: 0x000E99E8
		// (set) Token: 0x060043F8 RID: 17400 RVA: 0x000EB7F0 File Offset: 0x000E99F0
		public string Path
		{
			get
			{
				return this._directory;
			}
			set
			{
				value = ((value == null) ? string.Empty : value);
				if (!string.Equals(this._directory, value, PathInternal.StringComparison))
				{
					if (value.Length == 0)
					{
						throw new ArgumentException(SR.Format("The directory name {0} is invalid.", value), "Path");
					}
					if (!Directory.Exists(value))
					{
						throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", value), "Path");
					}
					this._directory = value;
					this.Restart();
				}
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x060043F9 RID: 17401 RVA: 0x000EB866 File Offset: 0x000E9A66
		// (remove) Token: 0x060043FA RID: 17402 RVA: 0x000EB87F File Offset: 0x000E9A7F
		public event FileSystemEventHandler Changed
		{
			add
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Combine(this._onChangedHandler, value);
			}
			remove
			{
				this._onChangedHandler = (FileSystemEventHandler)Delegate.Remove(this._onChangedHandler, value);
			}
		}

		// Token: 0x14000074 RID: 116
		// (add) Token: 0x060043FB RID: 17403 RVA: 0x000EB898 File Offset: 0x000E9A98
		// (remove) Token: 0x060043FC RID: 17404 RVA: 0x000EB8B1 File Offset: 0x000E9AB1
		public event FileSystemEventHandler Created
		{
			add
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Combine(this._onCreatedHandler, value);
			}
			remove
			{
				this._onCreatedHandler = (FileSystemEventHandler)Delegate.Remove(this._onCreatedHandler, value);
			}
		}

		// Token: 0x14000075 RID: 117
		// (add) Token: 0x060043FD RID: 17405 RVA: 0x000EB8CA File Offset: 0x000E9ACA
		// (remove) Token: 0x060043FE RID: 17406 RVA: 0x000EB8E3 File Offset: 0x000E9AE3
		public event FileSystemEventHandler Deleted
		{
			add
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Combine(this._onDeletedHandler, value);
			}
			remove
			{
				this._onDeletedHandler = (FileSystemEventHandler)Delegate.Remove(this._onDeletedHandler, value);
			}
		}

		// Token: 0x14000076 RID: 118
		// (add) Token: 0x060043FF RID: 17407 RVA: 0x000EB8FC File Offset: 0x000E9AFC
		// (remove) Token: 0x06004400 RID: 17408 RVA: 0x000EB915 File Offset: 0x000E9B15
		public event ErrorEventHandler Error
		{
			add
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Combine(this._onErrorHandler, value);
			}
			remove
			{
				this._onErrorHandler = (ErrorEventHandler)Delegate.Remove(this._onErrorHandler, value);
			}
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06004401 RID: 17409 RVA: 0x000EB92E File Offset: 0x000E9B2E
		// (remove) Token: 0x06004402 RID: 17410 RVA: 0x000EB947 File Offset: 0x000E9B47
		public event RenamedEventHandler Renamed
		{
			add
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Combine(this._onRenamedHandler, value);
			}
			remove
			{
				this._onRenamedHandler = (RenamedEventHandler)Delegate.Remove(this._onRenamedHandler, value);
			}
		}

		// Token: 0x06004403 RID: 17411 RVA: 0x000EB960 File Offset: 0x000E9B60
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.StopRaisingEvents();
					this._onChangedHandler = null;
					this._onCreatedHandler = null;
					this._onDeletedHandler = null;
					this._onRenamedHandler = null;
					this._onErrorHandler = null;
				}
				else
				{
					this.FinalizeDispose();
				}
			}
			finally
			{
				this._disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x06004404 RID: 17412 RVA: 0x000EB9C4 File Offset: 0x000E9BC4
		private static void CheckPathValidity(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(SR.Format("The directory name {0} is invalid.", path), "path");
			}
			if (!Directory.Exists(path))
			{
				throw new ArgumentException(SR.Format("The directory name '{0}' does not exist.", path), "path");
			}
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x000EBA1C File Offset: 0x000E9C1C
		private bool MatchPattern(ReadOnlySpan<char> relativePath)
		{
			if (relativePath.IsWhiteSpace())
			{
				return false;
			}
			ReadOnlySpan<char> fileName = global::System.IO.Path.GetFileName(relativePath);
			if (fileName.Length == 0)
			{
				return false;
			}
			string[] filters = this._filters.GetFilters();
			if (filters.Length == 0)
			{
				return true;
			}
			string[] array = filters;
			for (int i = 0; i < array.Length; i++)
			{
				if (FileSystemName.MatchesSimpleExpression(array[i], fileName, !PathInternal.IsCaseSensitive))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x000EBA82 File Offset: 0x000E9C82
		private void NotifyInternalBufferOverflowEvent()
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler == null)
			{
				return;
			}
			onErrorHandler(this, new ErrorEventArgs(new InternalBufferOverflowException(SR.Format("Too many changes at once in directory:{0}.", this._directory))));
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x000EBAB0 File Offset: 0x000E9CB0
		private void NotifyRenameEventArgs(WatcherChangeTypes action, ReadOnlySpan<char> name, ReadOnlySpan<char> oldName)
		{
			RenamedEventHandler onRenamedHandler = this._onRenamedHandler;
			if (onRenamedHandler != null && (this.MatchPattern(name) || this.MatchPattern(oldName)))
			{
				onRenamedHandler(this, new RenamedEventArgs(action, this._directory, name.IsEmpty ? null : name.ToString(), oldName.IsEmpty ? null : oldName.ToString()));
			}
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000EBB1E File Offset: 0x000E9D1E
		private FileSystemEventHandler GetHandler(WatcherChangeTypes changeType)
		{
			switch (changeType)
			{
			case WatcherChangeTypes.Created:
				return this._onCreatedHandler;
			case WatcherChangeTypes.Deleted:
				return this._onDeletedHandler;
			case WatcherChangeTypes.Changed:
				return this._onChangedHandler;
			}
			return null;
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000EBB50 File Offset: 0x000E9D50
		private void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, ReadOnlySpan<char> name)
		{
			FileSystemEventHandler handler = this.GetHandler(changeType);
			if (handler != null && this.MatchPattern(name.IsEmpty ? this._directory : name))
			{
				handler(this, new FileSystemEventArgs(changeType, this._directory, name.IsEmpty ? null : name.ToString()));
			}
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000EBBB4 File Offset: 0x000E9DB4
		private void NotifyFileSystemEventArgs(WatcherChangeTypes changeType, string name)
		{
			FileSystemEventHandler handler = this.GetHandler(changeType);
			if (handler != null && this.MatchPattern(string.IsNullOrEmpty(name) ? this._directory : name))
			{
				handler(this, new FileSystemEventArgs(changeType, this._directory, name));
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000EBBFE File Offset: 0x000E9DFE
		protected void OnChanged(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onChangedHandler);
		}

		// Token: 0x0600440C RID: 17420 RVA: 0x000EBC0D File Offset: 0x000E9E0D
		protected void OnCreated(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onCreatedHandler);
		}

		// Token: 0x0600440D RID: 17421 RVA: 0x000EBC1C File Offset: 0x000E9E1C
		protected void OnDeleted(FileSystemEventArgs e)
		{
			this.InvokeOn(e, this._onDeletedHandler);
		}

		// Token: 0x0600440E RID: 17422 RVA: 0x000EBC2C File Offset: 0x000E9E2C
		private void InvokeOn(FileSystemEventArgs e, FileSystemEventHandler handler)
		{
			if (handler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(handler, new object[] { this, e });
					return;
				}
				handler(this, e);
			}
		}

		// Token: 0x0600440F RID: 17423 RVA: 0x000EBC70 File Offset: 0x000E9E70
		protected void OnError(ErrorEventArgs e)
		{
			ErrorEventHandler onErrorHandler = this._onErrorHandler;
			if (onErrorHandler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(onErrorHandler, new object[] { this, e });
					return;
				}
				onErrorHandler(this, e);
			}
		}

		// Token: 0x06004410 RID: 17424 RVA: 0x000EBCB8 File Offset: 0x000E9EB8
		protected void OnRenamed(RenamedEventArgs e)
		{
			RenamedEventHandler onRenamedHandler = this._onRenamedHandler;
			if (onRenamedHandler != null)
			{
				ISynchronizeInvoke synchronizingObject = this.SynchronizingObject;
				if (synchronizingObject != null && synchronizingObject.InvokeRequired)
				{
					synchronizingObject.BeginInvoke(onRenamedHandler, new object[] { this, e });
					return;
				}
				onRenamedHandler(this, e);
			}
		}

		// Token: 0x06004411 RID: 17425 RVA: 0x000EBD00 File Offset: 0x000E9F00
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType)
		{
			return this.WaitForChanged(changeType, -1);
		}

		// Token: 0x06004412 RID: 17426 RVA: 0x000EBD0C File Offset: 0x000E9F0C
		public WaitForChangedResult WaitForChanged(WatcherChangeTypes changeType, int timeout)
		{
			TaskCompletionSource<WaitForChangedResult> tcs = new TaskCompletionSource<WaitForChangedResult>();
			FileSystemEventHandler fileSystemEventHandler = null;
			RenamedEventHandler renamedEventHandler = null;
			if ((changeType & (WatcherChangeTypes.Changed | WatcherChangeTypes.Created | WatcherChangeTypes.Deleted)) != (WatcherChangeTypes)0)
			{
				fileSystemEventHandler = delegate(object s, FileSystemEventArgs e)
				{
					if ((e.ChangeType & changeType) != (WatcherChangeTypes)0)
					{
						tcs.TrySetResult(new WaitForChangedResult(e.ChangeType, e.Name, null, false));
					}
				};
				if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
				{
					this.Created += fileSystemEventHandler;
				}
				if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
				{
					this.Deleted += fileSystemEventHandler;
				}
				if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
				{
					this.Changed += fileSystemEventHandler;
				}
			}
			if ((changeType & WatcherChangeTypes.Renamed) != (WatcherChangeTypes)0)
			{
				renamedEventHandler = delegate(object s, RenamedEventArgs e)
				{
					if ((e.ChangeType & changeType) != (WatcherChangeTypes)0)
					{
						tcs.TrySetResult(new WaitForChangedResult(e.ChangeType, e.Name, e.OldName, false));
					}
				};
				this.Renamed += renamedEventHandler;
			}
			try
			{
				bool enableRaisingEvents = this.EnableRaisingEvents;
				if (!enableRaisingEvents)
				{
					this.EnableRaisingEvents = true;
				}
				tcs.Task.Wait(timeout);
				this.EnableRaisingEvents = enableRaisingEvents;
			}
			finally
			{
				if (renamedEventHandler != null)
				{
					this.Renamed -= renamedEventHandler;
				}
				if (fileSystemEventHandler != null)
				{
					if ((changeType & WatcherChangeTypes.Changed) != (WatcherChangeTypes)0)
					{
						this.Changed -= fileSystemEventHandler;
					}
					if ((changeType & WatcherChangeTypes.Deleted) != (WatcherChangeTypes)0)
					{
						this.Deleted -= fileSystemEventHandler;
					}
					if ((changeType & WatcherChangeTypes.Created) != (WatcherChangeTypes)0)
					{
						this.Created -= fileSystemEventHandler;
					}
				}
			}
			if (tcs.Task.Status != TaskStatus.RanToCompletion)
			{
				return WaitForChangedResult.TimedOutResult;
			}
			return tcs.Task.Result;
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x000EBE44 File Offset: 0x000EA044
		private void Restart()
		{
			if (!this.IsSuspended() && this._enabled)
			{
				this.StopRaisingEvents();
				this.StartRaisingEventsIfNotDisposed();
			}
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x000EBE62 File Offset: 0x000EA062
		private void StartRaisingEventsIfNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			this.StartRaisingEvents();
		}

		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x000EBE83 File Offset: 0x000EA083
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

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x000EBEA8 File Offset: 0x000EA0A8
		// (set) Token: 0x06004418 RID: 17432 RVA: 0x000EBEB0 File Offset: 0x000EA0B0
		public ISynchronizeInvoke SynchronizingObject { get; set; }

		// Token: 0x06004419 RID: 17433 RVA: 0x000EBEBC File Offset: 0x000EA0BC
		public void BeginInit()
		{
			bool enabled = this._enabled;
			this.StopRaisingEvents();
			this._enabled = enabled;
			this._initializing = true;
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x000EBEE4 File Offset: 0x000EA0E4
		public void EndInit()
		{
			this._initializing = false;
			if (this._directory.Length != 0 && this._enabled)
			{
				this.StartRaisingEvents();
			}
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x000EBF08 File Offset: 0x000EA108
		private bool IsSuspended()
		{
			return this._initializing || base.DesignMode;
		}

		// Token: 0x040028F4 RID: 10484
		private int _currentSession;

		// Token: 0x040028F5 RID: 10485
		private SafeFileHandle _directoryHandle;

		// Token: 0x040028F6 RID: 10486
		private readonly FileSystemWatcher.NormalizedFilterCollection _filters = new FileSystemWatcher.NormalizedFilterCollection();

		// Token: 0x040028F7 RID: 10487
		private string _directory;

		// Token: 0x040028F8 RID: 10488
		private const NotifyFilters c_defaultNotifyFilters = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

		// Token: 0x040028F9 RID: 10489
		private NotifyFilters _notifyFilters = NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastWrite;

		// Token: 0x040028FA RID: 10490
		private bool _includeSubdirectories;

		// Token: 0x040028FB RID: 10491
		private bool _enabled;

		// Token: 0x040028FC RID: 10492
		private bool _initializing;

		// Token: 0x040028FD RID: 10493
		private uint _internalBufferSize = 8192U;

		// Token: 0x040028FE RID: 10494
		private bool _disposed;

		// Token: 0x040028FF RID: 10495
		private FileSystemEventHandler _onChangedHandler;

		// Token: 0x04002900 RID: 10496
		private FileSystemEventHandler _onCreatedHandler;

		// Token: 0x04002901 RID: 10497
		private FileSystemEventHandler _onDeletedHandler;

		// Token: 0x04002902 RID: 10498
		private RenamedEventHandler _onRenamedHandler;

		// Token: 0x04002903 RID: 10499
		private ErrorEventHandler _onErrorHandler;

		// Token: 0x04002904 RID: 10500
		private static readonly char[] s_wildcards = new char[] { '?', '*' };

		// Token: 0x04002905 RID: 10501
		private const int c_notifyFiltersValidMask = 383;

		// Token: 0x02000854 RID: 2132
		private sealed class AsyncReadState
		{
			// Token: 0x0600441D RID: 17437 RVA: 0x000EBF31 File Offset: 0x000EA131
			internal AsyncReadState(int session, byte[] buffer, SafeFileHandle handle, ThreadPoolBoundHandle binding)
			{
				this.Session = session;
				this.Buffer = buffer;
				this.DirectoryHandle = handle;
				this.ThreadPoolBinding = binding;
			}

			// Token: 0x17000F68 RID: 3944
			// (get) Token: 0x0600441E RID: 17438 RVA: 0x000EBF56 File Offset: 0x000EA156
			// (set) Token: 0x0600441F RID: 17439 RVA: 0x000EBF5E File Offset: 0x000EA15E
			internal int Session { get; private set; }

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x06004420 RID: 17440 RVA: 0x000EBF67 File Offset: 0x000EA167
			// (set) Token: 0x06004421 RID: 17441 RVA: 0x000EBF6F File Offset: 0x000EA16F
			internal byte[] Buffer { get; private set; }

			// Token: 0x17000F6A RID: 3946
			// (get) Token: 0x06004422 RID: 17442 RVA: 0x000EBF78 File Offset: 0x000EA178
			// (set) Token: 0x06004423 RID: 17443 RVA: 0x000EBF80 File Offset: 0x000EA180
			internal SafeFileHandle DirectoryHandle { get; private set; }

			// Token: 0x17000F6B RID: 3947
			// (get) Token: 0x06004424 RID: 17444 RVA: 0x000EBF89 File Offset: 0x000EA189
			// (set) Token: 0x06004425 RID: 17445 RVA: 0x000EBF91 File Offset: 0x000EA191
			internal ThreadPoolBoundHandle ThreadPoolBinding { get; private set; }

			// Token: 0x17000F6C RID: 3948
			// (get) Token: 0x06004426 RID: 17446 RVA: 0x000EBF9A File Offset: 0x000EA19A
			// (set) Token: 0x06004427 RID: 17447 RVA: 0x000EBFA2 File Offset: 0x000EA1A2
			internal PreAllocatedOverlapped PreAllocatedOverlapped { get; set; }
		}

		// Token: 0x02000855 RID: 2133
		private sealed class NormalizedFilterCollection : Collection<string>
		{
			// Token: 0x06004428 RID: 17448 RVA: 0x000EBFAB File Offset: 0x000EA1AB
			internal NormalizedFilterCollection()
				: base(new FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList())
			{
			}

			// Token: 0x06004429 RID: 17449 RVA: 0x000EBFB8 File Offset: 0x000EA1B8
			protected override void InsertItem(int index, string item)
			{
				base.InsertItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x0600442A RID: 17450 RVA: 0x000EBFDE File Offset: 0x000EA1DE
			protected override void SetItem(int index, string item)
			{
				base.SetItem(index, (string.IsNullOrEmpty(item) || item == "*.*") ? "*" : item);
			}

			// Token: 0x0600442B RID: 17451 RVA: 0x000EC004 File Offset: 0x000EA204
			internal string[] GetFilters()
			{
				return ((FileSystemWatcher.NormalizedFilterCollection.ImmutableStringList)base.Items).Items;
			}

			// Token: 0x02000856 RID: 2134
			private sealed class ImmutableStringList : IList<string>, ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x17000F6D RID: 3949
				public string this[int index]
				{
					get
					{
						string[] items = this.Items;
						if (index >= items.Length)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						return items[index];
					}
					set
					{
						string[] array = (string[])this.Items.Clone();
						array[index] = value;
						this.Items = array;
					}
				}

				// Token: 0x17000F6E RID: 3950
				// (get) Token: 0x0600442E RID: 17454 RVA: 0x000EC069 File Offset: 0x000EA269
				public int Count
				{
					get
					{
						return this.Items.Length;
					}
				}

				// Token: 0x17000F6F RID: 3951
				// (get) Token: 0x0600442F RID: 17455 RVA: 0x00003062 File Offset: 0x00001262
				public bool IsReadOnly
				{
					get
					{
						return false;
					}
				}

				// Token: 0x06004430 RID: 17456 RVA: 0x000044FA File Offset: 0x000026FA
				public void Add(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06004431 RID: 17457 RVA: 0x000EC073 File Offset: 0x000EA273
				public void Clear()
				{
					this.Items = Array.Empty<string>();
				}

				// Token: 0x06004432 RID: 17458 RVA: 0x000EC080 File Offset: 0x000EA280
				public bool Contains(string item)
				{
					return Array.IndexOf<string>(this.Items, item) != -1;
				}

				// Token: 0x06004433 RID: 17459 RVA: 0x000EC094 File Offset: 0x000EA294
				public void CopyTo(string[] array, int arrayIndex)
				{
					this.Items.CopyTo(array, arrayIndex);
				}

				// Token: 0x06004434 RID: 17460 RVA: 0x000EC0A3 File Offset: 0x000EA2A3
				public IEnumerator<string> GetEnumerator()
				{
					return ((IEnumerable<string>)this.Items).GetEnumerator();
				}

				// Token: 0x06004435 RID: 17461 RVA: 0x000EC0B0 File Offset: 0x000EA2B0
				public int IndexOf(string item)
				{
					return Array.IndexOf<string>(this.Items, item);
				}

				// Token: 0x06004436 RID: 17462 RVA: 0x000EC0C0 File Offset: 0x000EA2C0
				public void Insert(int index, string item)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length + 1];
					items.AsSpan(0, index).CopyTo(array);
					items.AsSpan(index).CopyTo(array.AsSpan(index + 1));
					array[index] = item;
					this.Items = array;
				}

				// Token: 0x06004437 RID: 17463 RVA: 0x000044FA File Offset: 0x000026FA
				public bool Remove(string item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06004438 RID: 17464 RVA: 0x000EC118 File Offset: 0x000EA318
				public void RemoveAt(int index)
				{
					string[] items = this.Items;
					string[] array = new string[items.Length - 1];
					items.AsSpan(0, index).CopyTo(array);
					items.AsSpan(index + 1).CopyTo(array.AsSpan(index));
					this.Items = array;
				}

				// Token: 0x06004439 RID: 17465 RVA: 0x000EC16A File Offset: 0x000EA36A
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0400290C RID: 10508
				public string[] Items = Array.Empty<string>();
			}
		}
	}
}
