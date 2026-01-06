using System;
using Unity;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the results from a single capturing group. </summary>
	// Token: 0x020001ED RID: 493
	[Serializable]
	public class Group : Capture
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x000358E2 File Offset: 0x00033AE2
		internal Group(string text, int[] caps, int capcount, string name)
			: base(text, (capcount == 0) ? 0 : caps[(capcount - 1) * 2], (capcount == 0) ? 0 : caps[capcount * 2 - 1])
		{
			this._caps = caps;
			this._capcount = capcount;
			this.Name = name;
		}

		/// <summary>Gets a value indicating whether the match is successful.</summary>
		/// <returns>true if the match is successful; otherwise, false.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0003591B File Offset: 0x00033B1B
		public bool Success
		{
			get
			{
				return this._capcount != 0;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x00035926 File Offset: 0x00033B26
		public string Name { get; }

		/// <summary>Gets a collection of all the captures matched by the capturing group, in innermost-leftmost-first order (or innermost-rightmost-first order if the regular expression is modified with the <see cref="F:System.Text.RegularExpressions.RegexOptions.RightToLeft" /> option). The collection may have zero or more items.</summary>
		/// <returns>The collection of substrings matched by the group.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0003592E File Offset: 0x00033B2E
		public CaptureCollection Captures
		{
			get
			{
				if (this._capcoll == null)
				{
					this._capcoll = new CaptureCollection(this);
				}
				return this._capcoll;
			}
		}

		/// <summary>Returns a Group object equivalent to the one supplied that is safe to share between multiple threads.</summary>
		/// <returns>A regular expression Group object. </returns>
		/// <param name="inner">The input <see cref="T:System.Text.RegularExpressions.Group" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inner" /> is null.</exception>
		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003594C File Offset: 0x00033B4C
		public static Group Synchronized(Group inner)
		{
			if (inner == null)
			{
				throw new ArgumentNullException("inner");
			}
			CaptureCollection captures = inner.Captures;
			if (inner.Success)
			{
				captures.ForceInitialized();
			}
			return inner;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00013B26 File Offset: 0x00011D26
		internal Group()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040007E0 RID: 2016
		internal static readonly Group s_emptyGroup = new Group(string.Empty, Array.Empty<int>(), 0, string.Empty);

		// Token: 0x040007E1 RID: 2017
		internal readonly int[] _caps;

		// Token: 0x040007E2 RID: 2018
		internal int _capcount;

		// Token: 0x040007E3 RID: 2019
		internal CaptureCollection _capcoll;
	}
}
