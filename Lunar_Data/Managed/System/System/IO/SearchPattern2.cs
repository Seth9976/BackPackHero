using System;

namespace System.IO
{
	// Token: 0x0200083A RID: 2106
	internal class SearchPattern2
	{
		// Token: 0x060042FF RID: 17151 RVA: 0x000E95E5 File Offset: 0x000E77E5
		public SearchPattern2(string pattern)
			: this(pattern, false)
		{
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000E95EF File Offset: 0x000E77EF
		public SearchPattern2(string pattern, bool ignore)
		{
			this.ignore = ignore;
			this.pattern = pattern;
			this.Compile(pattern);
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000E960C File Offset: 0x000E780C
		public bool IsMatch(string text, bool ignorecase)
		{
			if (!this.hasWildcard && string.Compare(this.pattern, text, ignorecase) == 0)
			{
				return true;
			}
			string fileName = Path.GetFileName(text);
			if (!this.hasWildcard)
			{
				return string.Compare(this.pattern, fileName, ignorecase) == 0;
			}
			return this.Match(this.ops, fileName, 0);
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000E9663 File Offset: 0x000E7863
		public bool IsMatch(string text)
		{
			return this.IsMatch(text, this.ignore);
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x000E9672 File Offset: 0x000E7872
		public bool HasWildcard
		{
			get
			{
				return this.hasWildcard;
			}
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000E967C File Offset: 0x000E787C
		private void Compile(string pattern)
		{
			if (pattern == null || pattern.IndexOfAny(SearchPattern2.InvalidChars) >= 0)
			{
				throw new ArgumentException("Invalid search pattern: '" + pattern + "'");
			}
			if (pattern == "*")
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.True);
				this.hasWildcard = true;
				return;
			}
			this.ops = null;
			int i = 0;
			SearchPattern2.Op op = null;
			while (i < pattern.Length)
			{
				char c = pattern[i];
				SearchPattern2.Op op2;
				if (c != '*')
				{
					if (c == '?')
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyChar);
						i++;
						this.hasWildcard = true;
					}
					else
					{
						op2 = new SearchPattern2.Op(SearchPattern2.OpCode.ExactString);
						int num = pattern.IndexOfAny(SearchPattern2.WildcardChars, i);
						if (num < 0)
						{
							num = pattern.Length;
						}
						op2.Argument = pattern.Substring(i, num - i);
						if (this.ignore)
						{
							op2.Argument = op2.Argument.ToLower();
						}
						i = num;
					}
				}
				else
				{
					op2 = new SearchPattern2.Op(SearchPattern2.OpCode.AnyString);
					i++;
					this.hasWildcard = true;
				}
				if (op == null)
				{
					this.ops = op2;
				}
				else
				{
					op.Next = op2;
				}
				op = op2;
			}
			if (op == null)
			{
				this.ops = new SearchPattern2.Op(SearchPattern2.OpCode.End);
				return;
			}
			op.Next = new SearchPattern2.Op(SearchPattern2.OpCode.End);
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000E97A8 File Offset: 0x000E79A8
		private bool Match(SearchPattern2.Op op, string text, int ptr)
		{
			while (op != null)
			{
				switch (op.Code)
				{
				case SearchPattern2.OpCode.ExactString:
				{
					int length = op.Argument.Length;
					if (ptr + length > text.Length)
					{
						return false;
					}
					string text2 = text.Substring(ptr, length);
					if (this.ignore)
					{
						text2 = text2.ToLower();
					}
					if (text2 != op.Argument)
					{
						return false;
					}
					ptr += length;
					break;
				}
				case SearchPattern2.OpCode.AnyChar:
					if (++ptr > text.Length)
					{
						return false;
					}
					break;
				case SearchPattern2.OpCode.AnyString:
					while (ptr <= text.Length)
					{
						if (this.Match(op.Next, text, ptr))
						{
							return true;
						}
						ptr++;
					}
					return false;
				case SearchPattern2.OpCode.End:
					return ptr == text.Length;
				case SearchPattern2.OpCode.True:
					return true;
				}
				op = op.Next;
			}
			return true;
		}

		// Token: 0x0400285A RID: 10330
		private SearchPattern2.Op ops;

		// Token: 0x0400285B RID: 10331
		private bool ignore;

		// Token: 0x0400285C RID: 10332
		private bool hasWildcard;

		// Token: 0x0400285D RID: 10333
		private string pattern;

		// Token: 0x0400285E RID: 10334
		internal static readonly char[] WildcardChars = new char[] { '*', '?' };

		// Token: 0x0400285F RID: 10335
		internal static readonly char[] InvalidChars = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x0200083B RID: 2107
		private class Op
		{
			// Token: 0x06004307 RID: 17159 RVA: 0x000E98AA File Offset: 0x000E7AAA
			public Op(SearchPattern2.OpCode code)
			{
				this.Code = code;
				this.Argument = null;
				this.Next = null;
			}

			// Token: 0x04002860 RID: 10336
			public SearchPattern2.OpCode Code;

			// Token: 0x04002861 RID: 10337
			public string Argument;

			// Token: 0x04002862 RID: 10338
			public SearchPattern2.Op Next;
		}

		// Token: 0x0200083C RID: 2108
		private enum OpCode
		{
			// Token: 0x04002864 RID: 10340
			ExactString,
			// Token: 0x04002865 RID: 10341
			AnyChar,
			// Token: 0x04002866 RID: 10342
			AnyString,
			// Token: 0x04002867 RID: 10343
			End,
			// Token: 0x04002868 RID: 10344
			True
		}
	}
}
