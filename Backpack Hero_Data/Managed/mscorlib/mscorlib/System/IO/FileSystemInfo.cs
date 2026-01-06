using System;
using System.IO.Enumeration;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.IO
{
	/// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000B3A RID: 2874
	[Serializable]
	public abstract class FileSystemInfo : MarshalByRefObject, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class.</summary>
		// Token: 0x060067D5 RID: 26581 RVA: 0x001622D6 File Offset: 0x001604D6
		protected FileSystemInfo()
		{
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x001622E5 File Offset: 0x001604E5
		internal static FileSystemInfo Create(string fullPath, ref FileSystemEntry findData)
		{
			DirectoryInfo directoryInfo = (findData.IsDirectory ? new DirectoryInfo(fullPath, null, new string(findData.FileName), true) : new FileInfo(fullPath, null, new string(findData.FileName), true));
			directoryInfo.Init(findData._info);
			return directoryInfo;
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x00162323 File Offset: 0x00160523
		internal void Invalidate()
		{
			this._dataInitialized = -1;
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x0016232C File Offset: 0x0016052C
		internal unsafe void Init(Interop.NtDll.FILE_FULL_DIR_INFORMATION* info)
		{
			this._data.dwFileAttributes = (int)info->FileAttributes;
			this._data.ftCreationTime = *(Interop.Kernel32.FILE_TIME*)(&info->CreationTime);
			this._data.ftLastAccessTime = *(Interop.Kernel32.FILE_TIME*)(&info->LastAccessTime);
			this._data.ftLastWriteTime = *(Interop.Kernel32.FILE_TIME*)(&info->LastWriteTime);
			this._data.nFileSizeHigh = (uint)(info->EndOfFile >> 32);
			this._data.nFileSizeLow = (uint)info->EndOfFile;
			this._dataInitialized = 0;
		}

		/// <summary>Gets or sets the attributes for the current file or directory.</summary>
		/// <returns>
		///   <see cref="T:System.IO.FileAttributes" /> of the current <see cref="T:System.IO.FileSystemInfo" />.</returns>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The caller attempts to set an invalid file attribute. -or-The user attempts to set an attribute value but does not have write permission.</exception>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x001623BD File Offset: 0x001605BD
		// (set) Token: 0x060067DA RID: 26586 RVA: 0x001623D0 File Offset: 0x001605D0
		public FileAttributes Attributes
		{
			get
			{
				this.EnsureDataInitialized();
				return (FileAttributes)this._data.dwFileAttributes;
			}
			set
			{
				FileSystem.SetAttributes(this.FullPath, value);
				this._dataInitialized = -1;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060067DB RID: 26587 RVA: 0x001623E8 File Offset: 0x001605E8
		internal bool ExistsCore
		{
			get
			{
				if (this._dataInitialized == -1)
				{
					this.Refresh();
				}
				return this._dataInitialized == 0 && this._data.dwFileAttributes != -1 && this is DirectoryInfo == ((this._data.dwFileAttributes & 16) == 16);
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060067DC RID: 26588 RVA: 0x0016243B File Offset: 0x0016063B
		// (set) Token: 0x060067DD RID: 26589 RVA: 0x00162453 File Offset: 0x00160653
		internal DateTimeOffset CreationTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftCreationTime.ToDateTimeOffset();
			}
			set
			{
				FileSystem.SetCreationTime(this.FullPath, value, this is DirectoryInfo);
				this._dataInitialized = -1;
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x060067DE RID: 26590 RVA: 0x00162471 File Offset: 0x00160671
		// (set) Token: 0x060067DF RID: 26591 RVA: 0x00162489 File Offset: 0x00160689
		internal DateTimeOffset LastAccessTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftLastAccessTime.ToDateTimeOffset();
			}
			set
			{
				FileSystem.SetLastAccessTime(this.FullPath, value, this is DirectoryInfo);
				this._dataInitialized = -1;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060067E0 RID: 26592 RVA: 0x001624A7 File Offset: 0x001606A7
		// (set) Token: 0x060067E1 RID: 26593 RVA: 0x001624BF File Offset: 0x001606BF
		internal DateTimeOffset LastWriteTimeCore
		{
			get
			{
				this.EnsureDataInitialized();
				return this._data.ftLastWriteTime.ToDateTimeOffset();
			}
			set
			{
				FileSystem.SetLastWriteTime(this.FullPath, value, this is DirectoryInfo);
				this._dataInitialized = -1;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060067E2 RID: 26594 RVA: 0x001624DD File Offset: 0x001606DD
		internal long LengthCore
		{
			get
			{
				this.EnsureDataInitialized();
				return (long)(((ulong)this._data.nFileSizeHigh << 32) | ((ulong)this._data.nFileSizeLow & (ulong)(-1)));
			}
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x00162504 File Offset: 0x00160704
		private void EnsureDataInitialized()
		{
			if (this._dataInitialized == -1)
			{
				this._data = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
				this.Refresh();
			}
			if (this._dataInitialized != 0)
			{
				throw Win32Marshal.GetExceptionForWin32Error(this._dataInitialized, this.FullPath);
			}
		}

		/// <summary>Refreshes the state of the object.</summary>
		/// <exception cref="T:System.IO.IOException">A device such as a disk drive is not ready. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060067E4 RID: 26596 RVA: 0x0016253B File Offset: 0x0016073B
		public void Refresh()
		{
			this._dataInitialized = FileSystem.FillAttributeInfo(this.FullPath, ref this._data, false);
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060067E5 RID: 26597 RVA: 0x00162555 File Offset: 0x00160755
		internal string NormalizedPath
		{
			get
			{
				if (!PathInternal.EndsWithPeriodOrSpace(this.FullPath))
				{
					return this.FullPath;
				}
				return PathInternal.EnsureExtendedPrefix(this.FullPath);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.IO.FileSystemInfo" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> is null.</exception>
		// Token: 0x060067E6 RID: 26598 RVA: 0x00162578 File Offset: 0x00160778
		protected FileSystemInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.FullPath = Path.GetFullPathInternal(info.GetString("FullPath"));
			this.OriginalPath = info.GetString("OriginalPath");
			this._name = info.GetString("Name");
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the file name and additional exception information.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060067E7 RID: 26599 RVA: 0x001625D8 File Offset: 0x001607D8
		[ComVisible(false)]
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("OriginalPath", this.OriginalPath, typeof(string));
			info.AddValue("FullPath", this.FullPath, typeof(string));
			info.AddValue("Name", this.Name, typeof(string));
		}

		/// <summary>Gets the full path of the directory or file.</summary>
		/// <returns>A string containing the full path.</returns>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path and file name is 260 or more characters.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060067E8 RID: 26600 RVA: 0x00162636 File Offset: 0x00160836
		public virtual string FullName
		{
			get
			{
				return this.FullPath;
			}
		}

		/// <summary>Gets the string representing the extension part of the file.</summary>
		/// <returns>A string containing the <see cref="T:System.IO.FileSystemInfo" /> extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x060067E9 RID: 26601 RVA: 0x00162640 File Offset: 0x00160840
		public string Extension
		{
			get
			{
				int length = this.FullPath.Length;
				int num = length;
				while (--num >= 0)
				{
					char c = this.FullPath[num];
					if (c == '.')
					{
						return this.FullPath.Substring(num, length - num);
					}
					if (PathInternal.IsDirectorySeparator(c) || c == Path.VolumeSeparatorChar)
					{
						break;
					}
				}
				return string.Empty;
			}
		}

		/// <summary>For files, gets the name of the file. For directories, gets the name of the last directory in the hierarchy if a hierarchy exists. Otherwise, the Name property gets the name of the directory.</summary>
		/// <returns>A string that is the name of the parent directory, the name of the last directory in the hierarchy, or the name of a file, including the file name extension.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060067EA RID: 26602 RVA: 0x001622CE File Offset: 0x001604CE
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets a value indicating whether the file or directory exists.</summary>
		/// <returns>true if the file or directory exists; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x0016269C File Offset: 0x0016089C
		public virtual bool Exists
		{
			get
			{
				bool flag;
				try
				{
					flag = this.ExistsCore;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		/// <summary>Deletes a file or directory.</summary>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.IO.IOException">There is an open handle on the file or directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060067EC RID: 26604
		public abstract void Delete();

		/// <summary>Gets or sets the creation time of the current file or directory.</summary>
		/// <returns>The creation date and time of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x060067ED RID: 26605 RVA: 0x001626C8 File Offset: 0x001608C8
		// (set) Token: 0x060067EE RID: 26606 RVA: 0x001626E3 File Offset: 0x001608E3
		public DateTime CreationTime
		{
			get
			{
				return this.CreationTimeUtc.ToLocalTime();
			}
			set
			{
				this.CreationTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
		/// <returns>The creation date and time in UTC format of the current <see cref="T:System.IO.FileSystemInfo" /> object.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x060067EF RID: 26607 RVA: 0x001626F4 File Offset: 0x001608F4
		// (set) Token: 0x060067F0 RID: 26608 RVA: 0x0016270F File Offset: 0x0016090F
		public DateTime CreationTimeUtc
		{
			get
			{
				return this.CreationTimeCore.UtcDateTime;
			}
			set
			{
				this.CreationTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		/// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
		/// <returns>The time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x060067F1 RID: 26609 RVA: 0x00162720 File Offset: 0x00160920
		// (set) Token: 0x060067F2 RID: 26610 RVA: 0x0016273B File Offset: 0x0016093B
		public DateTime LastAccessTime
		{
			get
			{
				return this.LastAccessTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastAccessTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
		/// <returns>The UTC time that the current file or directory was last accessed.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x060067F3 RID: 26611 RVA: 0x0016274C File Offset: 0x0016094C
		// (set) Token: 0x060067F4 RID: 26612 RVA: 0x00162767 File Offset: 0x00160967
		public DateTime LastAccessTimeUtc
		{
			get
			{
				return this.LastAccessTimeCore.UtcDateTime;
			}
			set
			{
				this.LastAccessTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		/// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
		/// <returns>The time the current file was last written.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x060067F5 RID: 26613 RVA: 0x00162778 File Offset: 0x00160978
		// (set) Token: 0x060067F6 RID: 26614 RVA: 0x00162793 File Offset: 0x00160993
		public DateTime LastWriteTime
		{
			get
			{
				return this.LastWriteTimeUtc.ToLocalTime();
			}
			set
			{
				this.LastWriteTimeUtc = value.ToUniversalTime();
			}
		}

		/// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
		/// <returns>The UTC time when the current file was last written to.</returns>
		/// <exception cref="T:System.IO.IOException">
		///   <see cref="M:System.IO.FileSystemInfo.Refresh" /> cannot initialize the data. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x060067F7 RID: 26615 RVA: 0x001627A4 File Offset: 0x001609A4
		// (set) Token: 0x060067F8 RID: 26616 RVA: 0x001627BF File Offset: 0x001609BF
		public DateTime LastWriteTimeUtc
		{
			get
			{
				return this.LastWriteTimeCore.UtcDateTime;
			}
			set
			{
				this.LastWriteTimeCore = File.GetUtcDateTimeOffset(value);
			}
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x001627CD File Offset: 0x001609CD
		public override string ToString()
		{
			return this.OriginalPath ?? string.Empty;
		}

		// Token: 0x04003C6E RID: 15470
		private Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA _data;

		// Token: 0x04003C6F RID: 15471
		private int _dataInitialized = -1;

		/// <summary>Represents the fully qualified path of the directory or file.</summary>
		/// <exception cref="T:System.IO.PathTooLongException">The fully qualified path is 260 or more characters.</exception>
		// Token: 0x04003C70 RID: 15472
		protected string FullPath;

		/// <summary>The path originally specified by the user, whether relative or absolute.</summary>
		// Token: 0x04003C71 RID: 15473
		protected string OriginalPath;

		// Token: 0x04003C72 RID: 15474
		internal string _name;
	}
}
