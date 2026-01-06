using System;
using System.Collections;

namespace System
{
	// Token: 0x02000252 RID: 594
	internal class ByteMatcher
	{
		// Token: 0x06001B99 RID: 7065 RVA: 0x0006751A File Offset: 0x0006571A
		public void AddMapping(TermInfoStrings key, byte[] val)
		{
			if (val.Length == 0)
			{
				return;
			}
			this.map[val] = key;
			this.starts[(int)val[0]] = true;
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Sort()
		{
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0006754C File Offset: 0x0006574C
		public bool StartsWith(int c)
		{
			return this.starts[c] != null;
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x00067564 File Offset: 0x00065764
		public TermInfoStrings Match(char[] buffer, int offset, int length, out int used)
		{
			foreach (object obj in this.map.Keys)
			{
				byte[] array = (byte[])obj;
				int num = 0;
				while (num < array.Length && num < length && (char)array[num] == buffer[offset + num])
				{
					if (array.Length - 1 == num)
					{
						used = array.Length;
						return (TermInfoStrings)this.map[array];
					}
					num++;
				}
			}
			used = 0;
			return (TermInfoStrings)(-1);
		}

		// Token: 0x040017D6 RID: 6102
		private Hashtable map = new Hashtable();

		// Token: 0x040017D7 RID: 6103
		private Hashtable starts = new Hashtable();
	}
}
