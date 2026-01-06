using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace System.Net.Cache
{
	// Token: 0x02000590 RID: 1424
	internal class RequestCacheEntry
	{
		// Token: 0x06002D0E RID: 11534 RVA: 0x0009F93C File Offset: 0x0009DB3C
		internal RequestCacheEntry()
		{
			this.m_ExpiresUtc = (this.m_LastAccessedUtc = (this.m_LastModifiedUtc = (this.m_LastSynchronizedUtc = DateTime.MinValue)));
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x0009F975 File Offset: 0x0009DB75
		// (set) Token: 0x06002D10 RID: 11536 RVA: 0x0009F97D File Offset: 0x0009DB7D
		internal bool IsPrivateEntry
		{
			get
			{
				return this.m_IsPrivateEntry;
			}
			set
			{
				this.m_IsPrivateEntry = value;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x0009F986 File Offset: 0x0009DB86
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x0009F98E File Offset: 0x0009DB8E
		internal long StreamSize
		{
			get
			{
				return this.m_StreamSize;
			}
			set
			{
				this.m_StreamSize = value;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x0009F997 File Offset: 0x0009DB97
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0009F99F File Offset: 0x0009DB9F
		internal DateTime ExpiresUtc
		{
			get
			{
				return this.m_ExpiresUtc;
			}
			set
			{
				this.m_ExpiresUtc = value;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x0009F9A8 File Offset: 0x0009DBA8
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x0009F9B0 File Offset: 0x0009DBB0
		internal DateTime LastAccessedUtc
		{
			get
			{
				return this.m_LastAccessedUtc;
			}
			set
			{
				this.m_LastAccessedUtc = value;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0009F9B9 File Offset: 0x0009DBB9
		// (set) Token: 0x06002D18 RID: 11544 RVA: 0x0009F9C1 File Offset: 0x0009DBC1
		internal DateTime LastModifiedUtc
		{
			get
			{
				return this.m_LastModifiedUtc;
			}
			set
			{
				this.m_LastModifiedUtc = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x0009F9CA File Offset: 0x0009DBCA
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0009F9D2 File Offset: 0x0009DBD2
		internal DateTime LastSynchronizedUtc
		{
			get
			{
				return this.m_LastSynchronizedUtc;
			}
			set
			{
				this.m_LastSynchronizedUtc = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002D1B RID: 11547 RVA: 0x0009F9DB File Offset: 0x0009DBDB
		// (set) Token: 0x06002D1C RID: 11548 RVA: 0x0009F9E3 File Offset: 0x0009DBE3
		internal TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
			set
			{
				this.m_MaxStale = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002D1D RID: 11549 RVA: 0x0009F9EC File Offset: 0x0009DBEC
		// (set) Token: 0x06002D1E RID: 11550 RVA: 0x0009F9F4 File Offset: 0x0009DBF4
		internal int HitCount
		{
			get
			{
				return this.m_HitCount;
			}
			set
			{
				this.m_HitCount = value;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0009F9FD File Offset: 0x0009DBFD
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x0009FA05 File Offset: 0x0009DC05
		internal int UsageCount
		{
			get
			{
				return this.m_UsageCount;
			}
			set
			{
				this.m_UsageCount = value;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x0009FA0E File Offset: 0x0009DC0E
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x0009FA16 File Offset: 0x0009DC16
		internal bool IsPartialEntry
		{
			get
			{
				return this.m_IsPartialEntry;
			}
			set
			{
				this.m_IsPartialEntry = value;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x0009FA1F File Offset: 0x0009DC1F
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x0009FA27 File Offset: 0x0009DC27
		internal StringCollection EntryMetadata
		{
			get
			{
				return this.m_EntryMetadata;
			}
			set
			{
				this.m_EntryMetadata = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x0009FA30 File Offset: 0x0009DC30
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x0009FA38 File Offset: 0x0009DC38
		internal StringCollection SystemMetadata
		{
			get
			{
				return this.m_SystemMetadata;
			}
			set
			{
				this.m_SystemMetadata = value;
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0009FA44 File Offset: 0x0009DC44
		internal virtual string ToString(bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append("\r\nIsPrivateEntry   = ").Append(this.IsPrivateEntry);
			stringBuilder.Append("\r\nIsPartialEntry   = ").Append(this.IsPartialEntry);
			stringBuilder.Append("\r\nStreamSize       = ").Append(this.StreamSize);
			stringBuilder.Append("\r\nExpires          = ").Append((this.ExpiresUtc == DateTime.MinValue) ? "" : this.ExpiresUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastAccessed     = ").Append((this.LastAccessedUtc == DateTime.MinValue) ? "" : this.LastAccessedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastModified     = ").Append((this.LastModifiedUtc == DateTime.MinValue) ? "" : this.LastModifiedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nLastSynchronized = ").Append((this.LastSynchronizedUtc == DateTime.MinValue) ? "" : this.LastSynchronizedUtc.ToString("r", CultureInfo.CurrentCulture));
			stringBuilder.Append("\r\nMaxStale(sec)    = ").Append((this.MaxStale == TimeSpan.MinValue) ? "" : ((int)this.MaxStale.TotalSeconds).ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nHitCount         = ").Append(this.HitCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\nUsageCount       = ").Append(this.UsageCount.ToString(NumberFormatInfo.CurrentInfo));
			stringBuilder.Append("\r\n");
			if (verbose)
			{
				stringBuilder.Append("EntryMetadata:\r\n");
				if (this.m_EntryMetadata != null)
				{
					foreach (string text in this.m_EntryMetadata)
					{
						stringBuilder.Append(text).Append("\r\n");
					}
				}
				stringBuilder.Append("---\r\nSystemMetadata:\r\n");
				if (this.m_SystemMetadata != null)
				{
					foreach (string text2 in this.m_SystemMetadata)
					{
						stringBuilder.Append(text2).Append("\r\n");
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001A98 RID: 6808
		private bool m_IsPrivateEntry;

		// Token: 0x04001A99 RID: 6809
		private long m_StreamSize;

		// Token: 0x04001A9A RID: 6810
		private DateTime m_ExpiresUtc;

		// Token: 0x04001A9B RID: 6811
		private int m_HitCount;

		// Token: 0x04001A9C RID: 6812
		private DateTime m_LastAccessedUtc;

		// Token: 0x04001A9D RID: 6813
		private DateTime m_LastModifiedUtc;

		// Token: 0x04001A9E RID: 6814
		private DateTime m_LastSynchronizedUtc;

		// Token: 0x04001A9F RID: 6815
		private TimeSpan m_MaxStale;

		// Token: 0x04001AA0 RID: 6816
		private int m_UsageCount;

		// Token: 0x04001AA1 RID: 6817
		private bool m_IsPartialEntry;

		// Token: 0x04001AA2 RID: 6818
		private StringCollection m_EntryMetadata;

		// Token: 0x04001AA3 RID: 6819
		private StringCollection m_SystemMetadata;
	}
}
