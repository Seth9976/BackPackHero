using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EB RID: 491
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x0003581C File Offset: 0x00033A1C
		public void SetDelegates(Action<RegexRunner> go, Func<RegexRunner, bool> firstChar, Action<RegexRunner> trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00035833 File Offset: 0x00033A33
		protected override void Go()
		{
			this._goMethod(this);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00035841 File Offset: 0x00033A41
		protected override bool FindFirstChar()
		{
			return this._findFirstCharMethod(this);
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003584F File Offset: 0x00033A4F
		protected override void InitTrackCount()
		{
			this._initTrackCountMethod(this);
		}

		// Token: 0x040007DA RID: 2010
		private Action<RegexRunner> _goMethod;

		// Token: 0x040007DB RID: 2011
		private Func<RegexRunner, bool> _findFirstCharMethod;

		// Token: 0x040007DC RID: 2012
		private Action<RegexRunner> _initTrackCountMethod;
	}
}
