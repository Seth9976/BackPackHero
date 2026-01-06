using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000034 RID: 52
	internal class DiskCache : IDiskCache
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000485A File Offset: 0x00002A5A
		public DiskCache(IFileSystemCalls systemCalls)
		{
			this.k_CacheFilePath = Application.persistentDataPath + "/eventcache";
			this.k_SystemCalls = systemCalls;
			this.k_CacheFileMaximumSize = 5242880L;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000488A File Offset: 0x00002A8A
		public DiskCache(string cacheFilePath, IFileSystemCalls systemCalls, long maximumFileSize)
		{
			this.k_CacheFilePath = cacheFilePath;
			this.k_SystemCalls = systemCalls;
			this.k_CacheFileMaximumSize = maximumFileSize;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000048A8 File Offset: 0x00002AA8
		public void Write(List<int> eventEndIndices, Stream payload)
		{
			if (eventEndIndices.Count > 0 && this.k_SystemCalls.CanAccessFileSystem())
			{
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < eventEndIndices.Count; i++)
				{
					if ((long)eventEndIndices[i] < this.k_CacheFileMaximumSize)
					{
						num = eventEndIndices[i];
						num2 = i + 1;
					}
				}
				using (Stream stream = this.k_SystemCalls.OpenFileForWriting(this.k_CacheFilePath))
				{
					using (BinaryWriter binaryWriter = new BinaryWriter(stream))
					{
						binaryWriter.Write("UGSEventCache");
						binaryWriter.Write(1);
						binaryWriter.Write(num2);
						for (int j = 0; j < num2; j++)
						{
							binaryWriter.Write(eventEndIndices[j]);
						}
						long position = payload.Position;
						payload.Position = 0L;
						for (int k = 0; k < num; k++)
						{
							binaryWriter.Write((byte)payload.ReadByte());
						}
						payload.Position = position;
					}
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000049C4 File Offset: 0x00002BC4
		public void Clear()
		{
			if (this.k_SystemCalls.CanAccessFileSystem() && this.k_SystemCalls.FileExists(this.k_CacheFilePath))
			{
				this.k_SystemCalls.DeleteFile(this.k_CacheFilePath);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000049F8 File Offset: 0x00002BF8
		public bool Read(List<int> eventEndIndices, Stream buffer)
		{
			if (this.k_SystemCalls.CanAccessFileSystem() && this.k_SystemCalls.FileExists(this.k_CacheFilePath))
			{
				using (Stream stream = this.k_SystemCalls.OpenFileForReading(this.k_CacheFilePath))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						try
						{
							if (binaryReader.ReadString() == "UGSEventCache")
							{
								int num = binaryReader.ReadInt32();
								if (num == 1)
								{
									this.ReadVersionOneCacheFile(in eventEndIndices, binaryReader, in buffer);
									return true;
								}
								Debug.LogWarning(string.Format("Unable to read event cache file: unknown file format version {0}", num));
								this.Clear();
							}
							else
							{
								Debug.LogWarning("Unable to read event cache file: corrupt");
								this.Clear();
							}
						}
						catch (Exception)
						{
							Debug.LogWarning("Unable to read event cache file: corrupt");
							this.Clear();
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004AF4 File Offset: 0x00002CF4
		private void ReadVersionOneCacheFile(in List<int> eventEndIndices, BinaryReader reader, in Stream buffer)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int num2 = reader.ReadInt32();
				eventEndIndices.Add(num2);
			}
			buffer.SetLength(0L);
			buffer.Position = 0L;
			reader.BaseStream.CopyTo(buffer);
		}

		// Token: 0x040000CB RID: 203
		internal const string k_FileHeaderString = "UGSEventCache";

		// Token: 0x040000CC RID: 204
		internal const int k_CacheFileVersionOne = 1;

		// Token: 0x040000CD RID: 205
		private readonly string k_CacheFilePath;

		// Token: 0x040000CE RID: 206
		private readonly IFileSystemCalls k_SystemCalls;

		// Token: 0x040000CF RID: 207
		private readonly long k_CacheFileMaximumSize;
	}
}
