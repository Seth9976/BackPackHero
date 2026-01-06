using System;
using System.Collections.Specialized;
using System.IO;

namespace System.Net.Cache
{
	// Token: 0x0200058F RID: 1423
	internal abstract class RequestCache
	{
		// Token: 0x06002D01 RID: 11521 RVA: 0x0009F8FF File Offset: 0x0009DAFF
		protected RequestCache(bool isPrivateCache, bool canWrite)
		{
			this._IsPrivateCache = isPrivateCache;
			this._CanWrite = canWrite;
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0009F915 File Offset: 0x0009DB15
		internal bool IsPrivateCache
		{
			get
			{
				return this._IsPrivateCache;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x0009F91D File Offset: 0x0009DB1D
		internal bool CanWrite
		{
			get
			{
				return this._CanWrite;
			}
		}

		// Token: 0x06002D04 RID: 11524
		internal abstract Stream Retrieve(string key, out RequestCacheEntry cacheEntry);

		// Token: 0x06002D05 RID: 11525
		internal abstract Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002D06 RID: 11526
		internal abstract void Remove(string key);

		// Token: 0x06002D07 RID: 11527
		internal abstract void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002D08 RID: 11528
		internal abstract bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream);

		// Token: 0x06002D09 RID: 11529
		internal abstract bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream);

		// Token: 0x06002D0A RID: 11530
		internal abstract bool TryRemove(string key);

		// Token: 0x06002D0B RID: 11531
		internal abstract bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata);

		// Token: 0x06002D0C RID: 11532
		internal abstract void UnlockEntry(Stream retrieveStream);

		// Token: 0x04001A95 RID: 6805
		internal static readonly char[] LineSplits = new char[] { '\r', '\n' };

		// Token: 0x04001A96 RID: 6806
		private bool _IsPrivateCache;

		// Token: 0x04001A97 RID: 6807
		private bool _CanWrite;
	}
}
