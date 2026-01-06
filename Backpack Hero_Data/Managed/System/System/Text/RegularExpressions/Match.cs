using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single regular expression match.</summary>
	// Token: 0x020001F0 RID: 496
	[Serializable]
	public class Match : Group
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00035CC8 File Offset: 0x00033EC8
		internal Match(Regex regex, int capcount, string text, int begpos, int len, int startpos)
			: base(text, new int[2], 0, "0")
		{
			this._regex = regex;
			this._matchcount = new int[capcount];
			this._matches = new int[capcount][];
			this._matches[0] = this._caps;
			this._textbeg = begpos;
			this._textend = begpos + len;
			this._textstart = startpos;
			this._balancing = false;
		}

		/// <summary>Gets the empty group. All failed matches return this empty match.</summary>
		/// <returns>An empty match.</returns>
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00035D37 File Offset: 0x00033F37
		public static Match Empty { get; } = new Match(null, 1, string.Empty, 0, 0, 0);

		// Token: 0x06000D23 RID: 3363 RVA: 0x00035D40 File Offset: 0x00033F40
		internal virtual void Reset(Regex regex, string text, int textbeg, int textend, int textstart)
		{
			this._regex = regex;
			base.Text = text;
			this._textbeg = textbeg;
			this._textend = textend;
			this._textstart = textstart;
			for (int i = 0; i < this._matchcount.Length; i++)
			{
				this._matchcount[i] = 0;
			}
			this._balancing = false;
		}

		/// <summary>Gets a collection of groups matched by the regular expression.</summary>
		/// <returns>The character groups matched by the pattern.</returns>
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x00035D95 File Offset: 0x00033F95
		public virtual GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, null);
				}
				return this._groupcoll;
			}
		}

		/// <summary>Returns a new <see cref="T:System.Text.RegularExpressions.Match" /> object with the results for the next match, starting at the position at which the last match ended (at the character after the last matched character).</summary>
		/// <returns>The next regular expression match.</returns>
		/// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D25 RID: 3365 RVA: 0x00035DB2 File Offset: 0x00033FB2
		public Match NextMatch()
		{
			if (this._regex == null)
			{
				return this;
			}
			return this._regex.Run(false, base.Length, base.Text, this._textbeg, this._textend - this._textbeg, this._textpos);
		}

		/// <summary>Returns the expansion of the specified replacement pattern. </summary>
		/// <returns>The expanded version of the <paramref name="replacement" /> parameter.</returns>
		/// <param name="replacement">The replacement pattern to use. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="replacement" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">Expansion is not allowed for this pattern.</exception>
		// Token: 0x06000D26 RID: 3366 RVA: 0x00035DF0 File Offset: 0x00033FF0
		public virtual string Result(string replacement)
		{
			if (replacement == null)
			{
				throw new ArgumentNullException("replacement");
			}
			if (this._regex == null)
			{
				throw new NotSupportedException("Result cannot be called on a failed Match.");
			}
			return RegexReplacement.GetOrCreate(this._regex._replref, replacement, this._regex.caps, this._regex.capsize, this._regex.capnames, this._regex.roptions).Replacement(this);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00035E64 File Offset: 0x00034064
		internal virtual ReadOnlySpan<char> GroupToStringImpl(int groupnum)
		{
			int num = this._matchcount[groupnum];
			if (num == 0)
			{
				return string.Empty;
			}
			int[] array = this._matches[groupnum];
			return base.Text.AsSpan(array[(num - 1) * 2], array[num * 2 - 1]);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00035EAA File Offset: 0x000340AA
		internal ReadOnlySpan<char> LastGroupToStringImpl()
		{
			return this.GroupToStringImpl(this._matchcount.Length - 1);
		}

		/// <summary>Returns a <see cref="T:System.Text.RegularExpressions.Match" /> instance equivalent to the one supplied that is suitable to share between multiple threads.</summary>
		/// <returns>A regular expression match that is suitable to share between multiple threads. </returns>
		/// <param name="inner">A regular expression match equivalent to the one expected. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06000D29 RID: 3369 RVA: 0x00035EBC File Offset: 0x000340BC
		public static Match Synchronized(Match inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			int num = inner._matchcount.Length;
			for (int i = 0; i < num; i++)
			{
				Group.Synchronized(inner.Groups[i]);
			}
			return inner;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00035F00 File Offset: 0x00034100
		internal virtual void AddMatch(int cap, int start, int len)
		{
			if (this._matches[cap] == null)
			{
				this._matches[cap] = new int[2];
			}
			int num = this._matchcount[cap];
			if (num * 2 + 2 > this._matches[cap].Length)
			{
				int[] array = this._matches[cap];
				int[] array2 = new int[num * 8];
				for (int i = 0; i < num * 2; i++)
				{
					array2[i] = array[i];
				}
				this._matches[cap] = array2;
			}
			this._matches[cap][num * 2] = start;
			this._matches[cap][num * 2 + 1] = len;
			this._matchcount[cap] = num + 1;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00035F98 File Offset: 0x00034198
		internal virtual void BalanceMatch(int cap)
		{
			this._balancing = true;
			int num = this._matchcount[cap] * 2 - 2;
			if (this._matches[cap][num] < 0)
			{
				num = -3 - this._matches[cap][num];
			}
			num -= 2;
			if (num >= 0 && this._matches[cap][num] < 0)
			{
				this.AddMatch(cap, this._matches[cap][num], this._matches[cap][num + 1]);
				return;
			}
			this.AddMatch(cap, -3 - num, -4 - num);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00036016 File Offset: 0x00034216
		internal virtual void RemoveMatch(int cap)
		{
			this._matchcount[cap]--;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00036029 File Offset: 0x00034229
		internal virtual bool IsMatched(int cap)
		{
			return cap < this._matchcount.Length && this._matchcount[cap] > 0 && this._matches[cap][this._matchcount[cap] * 2 - 1] != -2;
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00036060 File Offset: 0x00034260
		internal virtual int MatchIndex(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 2];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00036098 File Offset: 0x00034298
		internal virtual int MatchLength(int cap)
		{
			int num = this._matches[cap][this._matchcount[cap] * 2 - 1];
			if (num >= 0)
			{
				return num;
			}
			return this._matches[cap][-3 - num];
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x000360D0 File Offset: 0x000342D0
		internal virtual void Tidy(int textpos)
		{
			int[] array = this._matches[0];
			base.Index = array[0];
			base.Length = array[1];
			this._textpos = textpos;
			this._capcount = this._matchcount[0];
			if (this._balancing)
			{
				for (int i = 0; i < this._matchcount.Length; i++)
				{
					int num = this._matchcount[i] * 2;
					int[] array2 = this._matches[i];
					int j = 0;
					while (j < num && array2[j] >= 0)
					{
						j++;
					}
					int num2 = j;
					while (j < num)
					{
						if (array2[j] < 0)
						{
							num2--;
						}
						else
						{
							if (j != num2)
							{
								array2[num2] = array2[j];
							}
							num2++;
						}
						j++;
					}
					this._matchcount[i] = num2 / 2;
				}
				this._balancing = false;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00013B26 File Offset: 0x00011D26
		internal Match()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007EA RID: 2026
		internal GroupCollection _groupcoll;

		// Token: 0x040007EB RID: 2027
		internal Regex _regex;

		// Token: 0x040007EC RID: 2028
		internal int _textbeg;

		// Token: 0x040007ED RID: 2029
		internal int _textpos;

		// Token: 0x040007EE RID: 2030
		internal int _textend;

		// Token: 0x040007EF RID: 2031
		internal int _textstart;

		// Token: 0x040007F0 RID: 2032
		internal int[][] _matches;

		// Token: 0x040007F1 RID: 2033
		internal int[] _matchcount;

		// Token: 0x040007F2 RID: 2034
		internal bool _balancing;
	}
}
