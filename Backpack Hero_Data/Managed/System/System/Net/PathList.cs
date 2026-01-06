using System;
using System.Collections;

namespace System.Net
{
	// Token: 0x02000465 RID: 1125
	[Serializable]
	internal class PathList
	{
		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00083781 File Offset: 0x00081981
		public int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x06002382 RID: 9090 RVA: 0x00083790 File Offset: 0x00081990
		public int GetCookiesCount()
		{
			int num = 0;
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in this.m_list.Values)
				{
					CookieCollection cookieCollection = (CookieCollection)obj;
					num += cookieCollection.Count;
				}
			}
			return num;
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06002383 RID: 9091 RVA: 0x00083820 File Offset: 0x00081A20
		public ICollection Values
		{
			get
			{
				return this.m_list.Values;
			}
		}

		// Token: 0x17000719 RID: 1817
		public object this[string s]
		{
			get
			{
				return this.m_list[s];
			}
			set
			{
				object syncRoot = this.SyncRoot;
				lock (syncRoot)
				{
					this.m_list[s] = value;
				}
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00083884 File Offset: 0x00081A84
		public IEnumerator GetEnumerator()
		{
			return this.m_list.GetEnumerator();
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00083891 File Offset: 0x00081A91
		public object SyncRoot
		{
			get
			{
				return this.m_list.SyncRoot;
			}
		}

		// Token: 0x040014C8 RID: 5320
		private SortedList m_list = SortedList.Synchronized(new SortedList(PathList.PathListComparer.StaticInstance));

		// Token: 0x02000466 RID: 1126
		[Serializable]
		private class PathListComparer : IComparer
		{
			// Token: 0x06002388 RID: 9096 RVA: 0x000838A0 File Offset: 0x00081AA0
			int IComparer.Compare(object ol, object or)
			{
				string text = CookieParser.CheckQuoted((string)ol);
				string text2 = CookieParser.CheckQuoted((string)or);
				int length = text.Length;
				int length2 = text2.Length;
				int num = Math.Min(length, length2);
				for (int i = 0; i < num; i++)
				{
					if (text[i] != text2[i])
					{
						return (int)(text[i] - text2[i]);
					}
				}
				return length2 - length;
			}

			// Token: 0x040014C9 RID: 5321
			internal static readonly PathList.PathListComparer StaticInstance = new PathList.PathListComparer();
		}
	}
}
