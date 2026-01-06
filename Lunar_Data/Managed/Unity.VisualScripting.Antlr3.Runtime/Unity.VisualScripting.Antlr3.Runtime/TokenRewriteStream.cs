using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200004F RID: 79
	public class TokenRewriteStream : CommonTokenStream
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x00009057 File Offset: 0x00008057
		public TokenRewriteStream()
		{
			this.Init();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009065 File Offset: 0x00008065
		public TokenRewriteStream(ITokenSource tokenSource)
			: base(tokenSource)
		{
			this.Init();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009074 File Offset: 0x00008074
		public TokenRewriteStream(ITokenSource tokenSource, int channel)
			: base(tokenSource, channel)
		{
			this.Init();
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009084 File Offset: 0x00008084
		protected internal virtual void Init()
		{
			this.programs = new Hashtable();
			this.programs["default"] = new List<object>(100);
			this.lastRewriteTokenIndexes = new Hashtable();
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000090B3 File Offset: 0x000080B3
		public virtual void Rollback(int instructionIndex)
		{
			this.Rollback("default", instructionIndex);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000090C4 File Offset: 0x000080C4
		public virtual void Rollback(string programName, int instructionIndex)
		{
			IList list = (IList)this.programs[programName];
			if (list != null)
			{
				this.programs[programName] = ((List<object>)list).GetRange(0, instructionIndex);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000090FF File Offset: 0x000080FF
		public virtual void DeleteProgram()
		{
			this.DeleteProgram("default");
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000910C File Offset: 0x0000810C
		public virtual void DeleteProgram(string programName)
		{
			this.Rollback(programName, 0);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00009116 File Offset: 0x00008116
		public virtual void InsertAfter(IToken t, object text)
		{
			this.InsertAfter("default", t, text);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00009125 File Offset: 0x00008125
		public virtual void InsertAfter(int index, object text)
		{
			this.InsertAfter("default", index, text);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009134 File Offset: 0x00008134
		public virtual void InsertAfter(string programName, IToken t, object text)
		{
			this.InsertAfter(programName, t.TokenIndex, text);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009144 File Offset: 0x00008144
		public virtual void InsertAfter(string programName, int index, object text)
		{
			this.InsertBefore(programName, index + 1, text);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00009151 File Offset: 0x00008151
		public virtual void InsertBefore(IToken t, object text)
		{
			this.InsertBefore("default", t, text);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00009160 File Offset: 0x00008160
		public virtual void InsertBefore(int index, object text)
		{
			this.InsertBefore("default", index, text);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000916F File Offset: 0x0000816F
		public virtual void InsertBefore(string programName, IToken t, object text)
		{
			this.InsertBefore(programName, t.TokenIndex, text);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00009180 File Offset: 0x00008180
		public virtual void InsertBefore(string programName, int index, object text)
		{
			TokenRewriteStream.RewriteOperation rewriteOperation = new TokenRewriteStream.InsertBeforeOp(index, text, this);
			IList program = this.GetProgram(programName);
			program.Add(rewriteOperation);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000091A6 File Offset: 0x000081A6
		public virtual void Replace(int index, object text)
		{
			this.Replace("default", index, index, text);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000091B6 File Offset: 0x000081B6
		public virtual void Replace(int from, int to, object text)
		{
			this.Replace("default", from, to, text);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000091C6 File Offset: 0x000081C6
		public virtual void Replace(IToken indexT, object text)
		{
			this.Replace("default", indexT, indexT, text);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000091D6 File Offset: 0x000081D6
		public virtual void Replace(IToken from, IToken to, object text)
		{
			this.Replace("default", from, to, text);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000091E8 File Offset: 0x000081E8
		public virtual void Replace(string programName, int from, int to, object text)
		{
			if (from > to || from < 0 || to < 0 || to >= this.tokens.Count)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new object[]
				{
					"replace: range invalid: ",
					from,
					"..",
					to,
					"(size=",
					this.tokens.Count,
					")"
				}));
			}
			TokenRewriteStream.RewriteOperation rewriteOperation = new TokenRewriteStream.ReplaceOp(from, to, text, this);
			IList program = this.GetProgram(programName);
			rewriteOperation.instructionIndex = program.Count;
			program.Add(rewriteOperation);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000928E File Offset: 0x0000828E
		public virtual void Replace(string programName, IToken from, IToken to, object text)
		{
			this.Replace(programName, from.TokenIndex, to.TokenIndex, text);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000092A5 File Offset: 0x000082A5
		public virtual void Delete(int index)
		{
			this.Delete("default", index, index);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000092B4 File Offset: 0x000082B4
		public virtual void Delete(int from, int to)
		{
			this.Delete("default", from, to);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000092C3 File Offset: 0x000082C3
		public virtual void Delete(IToken indexT)
		{
			this.Delete("default", indexT, indexT);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000092D2 File Offset: 0x000082D2
		public virtual void Delete(IToken from, IToken to)
		{
			this.Delete("default", from, to);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000092E1 File Offset: 0x000082E1
		public virtual void Delete(string programName, int from, int to)
		{
			this.Replace(programName, from, to, null);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000092ED File Offset: 0x000082ED
		public virtual void Delete(string programName, IToken from, IToken to)
		{
			this.Replace(programName, from, to, null);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000092F9 File Offset: 0x000082F9
		public virtual int GetLastRewriteTokenIndex()
		{
			return this.GetLastRewriteTokenIndex("default");
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00009308 File Offset: 0x00008308
		protected virtual int GetLastRewriteTokenIndex(string programName)
		{
			object obj = this.lastRewriteTokenIndexes[programName];
			if (obj == null)
			{
				return -1;
			}
			return (int)obj;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000932D File Offset: 0x0000832D
		protected virtual void SetLastRewriteTokenIndex(string programName, int i)
		{
			this.lastRewriteTokenIndexes[programName] = i;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009344 File Offset: 0x00008344
		protected virtual IList GetProgram(string name)
		{
			IList list = (IList)this.programs[name];
			if (list == null)
			{
				list = this.InitializeProgram(name);
			}
			return list;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00009370 File Offset: 0x00008370
		private IList InitializeProgram(string name)
		{
			IList list = new List<object>(100);
			this.programs[name] = list;
			return list;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00009393 File Offset: 0x00008393
		public virtual string ToOriginalString()
		{
			return this.ToOriginalString(0, this.Count - 1);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000093A4 File Offset: 0x000083A4
		public virtual string ToOriginalString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this.tokens.Count)
			{
				stringBuilder.Append(this.Get(num).Text);
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000093EE File Offset: 0x000083EE
		public override string ToString()
		{
			return this.ToString(0, this.Count - 1);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000093FF File Offset: 0x000083FF
		public virtual string ToString(string programName)
		{
			return this.ToString(programName, 0, this.Count - 1);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009411 File Offset: 0x00008411
		public override string ToString(int start, int end)
		{
			return this.ToString("default", start, end);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009420 File Offset: 0x00008420
		public virtual string ToString(string programName, int start, int end)
		{
			IList list = (IList)this.programs[programName];
			if (end > this.tokens.Count - 1)
			{
				end = this.tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (list == null || list.Count == 0)
			{
				return this.ToOriginalString(start, end);
			}
			StringBuilder stringBuilder = new StringBuilder();
			IDictionary dictionary = this.ReduceToSingleOperationPerIndex(list);
			int num = start;
			while (num <= end && num < this.tokens.Count)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = (TokenRewriteStream.RewriteOperation)dictionary[num];
				dictionary.Remove(num);
				IToken token = (IToken)this.tokens[num];
				if (rewriteOperation == null)
				{
					stringBuilder.Append(token.Text);
					num++;
				}
				else
				{
					num = rewriteOperation.Execute(stringBuilder);
				}
			}
			if (end == this.tokens.Count - 1)
			{
				foreach (object obj in dictionary.Values)
				{
					TokenRewriteStream.InsertBeforeOp insertBeforeOp = (TokenRewriteStream.InsertBeforeOp)obj;
					if (insertBeforeOp.index >= this.tokens.Count - 1)
					{
						stringBuilder.Append(insertBeforeOp.text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009550 File Offset: 0x00008550
		protected IDictionary ReduceToSingleOperationPerIndex(IList rewrites)
		{
			for (int i = 0; i < rewrites.Count; i++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = (TokenRewriteStream.RewriteOperation)rewrites[i];
				if (rewriteOperation != null && rewriteOperation is TokenRewriteStream.ReplaceOp)
				{
					TokenRewriteStream.ReplaceOp replaceOp = (TokenRewriteStream.ReplaceOp)rewrites[i];
					IList kindOfOps = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.InsertBeforeOp), i);
					for (int j = 0; j < kindOfOps.Count; j++)
					{
						TokenRewriteStream.InsertBeforeOp insertBeforeOp = (TokenRewriteStream.InsertBeforeOp)kindOfOps[j];
						if (insertBeforeOp.index >= replaceOp.index && insertBeforeOp.index <= replaceOp.lastIndex)
						{
							rewrites[insertBeforeOp.instructionIndex] = null;
						}
					}
					IList kindOfOps2 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.ReplaceOp), i);
					for (int k = 0; k < kindOfOps2.Count; k++)
					{
						TokenRewriteStream.ReplaceOp replaceOp2 = (TokenRewriteStream.ReplaceOp)kindOfOps2[k];
						if (replaceOp2.index >= replaceOp.index && replaceOp2.lastIndex <= replaceOp.lastIndex)
						{
							rewrites[replaceOp2.instructionIndex] = null;
						}
						else
						{
							bool flag = replaceOp2.lastIndex < replaceOp.index || replaceOp2.index > replaceOp.lastIndex;
							bool flag2 = replaceOp2.index == replaceOp.index && replaceOp2.lastIndex == replaceOp.lastIndex;
							if (!flag && !flag2)
							{
								throw new ArgumentOutOfRangeException(string.Concat(new object[] { "replace op boundaries of ", replaceOp, " overlap with previous ", replaceOp2 }));
							}
						}
					}
				}
			}
			for (int l = 0; l < rewrites.Count; l++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation2 = (TokenRewriteStream.RewriteOperation)rewrites[l];
				if (rewriteOperation2 != null && rewriteOperation2 is TokenRewriteStream.InsertBeforeOp)
				{
					TokenRewriteStream.InsertBeforeOp insertBeforeOp2 = (TokenRewriteStream.InsertBeforeOp)rewrites[l];
					IList kindOfOps3 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.InsertBeforeOp), l);
					for (int m = 0; m < kindOfOps3.Count; m++)
					{
						TokenRewriteStream.InsertBeforeOp insertBeforeOp3 = (TokenRewriteStream.InsertBeforeOp)kindOfOps3[m];
						if (insertBeforeOp3.index == insertBeforeOp2.index)
						{
							insertBeforeOp2.text = this.CatOpText(insertBeforeOp2.text, insertBeforeOp3.text);
							rewrites[insertBeforeOp3.instructionIndex] = null;
						}
					}
					IList kindOfOps4 = this.GetKindOfOps(rewrites, typeof(TokenRewriteStream.ReplaceOp), l);
					for (int n = 0; n < kindOfOps4.Count; n++)
					{
						TokenRewriteStream.ReplaceOp replaceOp3 = (TokenRewriteStream.ReplaceOp)kindOfOps4[n];
						if (insertBeforeOp2.index == replaceOp3.index)
						{
							replaceOp3.text = this.CatOpText(insertBeforeOp2.text, replaceOp3.text);
							rewrites[l] = null;
						}
						else if (insertBeforeOp2.index >= replaceOp3.index && insertBeforeOp2.index <= replaceOp3.lastIndex)
						{
							throw new ArgumentOutOfRangeException(string.Concat(new object[] { "insert op ", insertBeforeOp2, " within boundaries of previous ", replaceOp3 }));
						}
					}
				}
			}
			IDictionary dictionary = new Hashtable();
			for (int num = 0; num < rewrites.Count; num++)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation3 = (TokenRewriteStream.RewriteOperation)rewrites[num];
				if (rewriteOperation3 != null)
				{
					if (dictionary[rewriteOperation3.index] != null)
					{
						throw new Exception("should only be one op per index");
					}
					dictionary[rewriteOperation3.index] = rewriteOperation3;
				}
			}
			return dictionary;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000098E4 File Offset: 0x000088E4
		protected string CatOpText(object a, object b)
		{
			string text = "";
			string text2 = "";
			if (a != null)
			{
				text = a.ToString();
			}
			if (b != null)
			{
				text2 = b.ToString();
			}
			return text + text2;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00009918 File Offset: 0x00008918
		protected IList GetKindOfOps(IList rewrites, Type kind)
		{
			return this.GetKindOfOps(rewrites, kind, rewrites.Count);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009928 File Offset: 0x00008928
		protected IList GetKindOfOps(IList rewrites, Type kind, int before)
		{
			IList list = new List<object>();
			int num = 0;
			while (num < before && num < rewrites.Count)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = (TokenRewriteStream.RewriteOperation)rewrites[num];
				if (rewriteOperation != null && rewriteOperation.GetType() == kind)
				{
					list.Add(rewriteOperation);
				}
				num++;
			}
			return list;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009972 File Offset: 0x00008972
		public virtual string ToDebugString()
		{
			return this.ToDebugString(0, this.Count - 1);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009984 File Offset: 0x00008984
		public virtual string ToDebugString(int start, int end)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = start;
			while (num >= 0 && num <= end && num < this.tokens.Count)
			{
				stringBuilder.Append(this.Get(num));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000E4 RID: 228
		public const string DEFAULT_PROGRAM_NAME = "default";

		// Token: 0x040000E5 RID: 229
		public const int PROGRAM_INIT_SIZE = 100;

		// Token: 0x040000E6 RID: 230
		public const int MIN_TOKEN_INDEX = 0;

		// Token: 0x040000E7 RID: 231
		protected IDictionary programs;

		// Token: 0x040000E8 RID: 232
		protected IDictionary lastRewriteTokenIndexes;

		// Token: 0x02000050 RID: 80
		private class RewriteOpComparer : IComparer
		{
			// Token: 0x06000326 RID: 806 RVA: 0x000099CC File Offset: 0x000089CC
			public virtual int Compare(object o1, object o2)
			{
				TokenRewriteStream.RewriteOperation rewriteOperation = (TokenRewriteStream.RewriteOperation)o1;
				TokenRewriteStream.RewriteOperation rewriteOperation2 = (TokenRewriteStream.RewriteOperation)o2;
				if (rewriteOperation.index < rewriteOperation2.index)
				{
					return -1;
				}
				if (rewriteOperation.index > rewriteOperation2.index)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x02000051 RID: 81
		protected internal class RewriteOperation
		{
			// Token: 0x06000328 RID: 808 RVA: 0x00009A10 File Offset: 0x00008A10
			protected internal RewriteOperation(int index, object text, TokenRewriteStream parent)
			{
				this.index = index;
				this.text = text;
				this.parent = parent;
			}

			// Token: 0x06000329 RID: 809 RVA: 0x00009A2D File Offset: 0x00008A2D
			public virtual int Execute(StringBuilder buf)
			{
				return this.index;
			}

			// Token: 0x0600032A RID: 810 RVA: 0x00009A38 File Offset: 0x00008A38
			public override string ToString()
			{
				string text = base.GetType().FullName;
				int num = text.IndexOf('$');
				text = text.Substring(num + 1, text.Length - (num + 1));
				return string.Concat(new object[] { "<", text, "@", this.index, ":\"", this.text, "\">" });
			}

			// Token: 0x040000E9 RID: 233
			protected internal int instructionIndex;

			// Token: 0x040000EA RID: 234
			protected internal int index;

			// Token: 0x040000EB RID: 235
			protected internal object text;

			// Token: 0x040000EC RID: 236
			protected internal TokenRewriteStream parent;
		}

		// Token: 0x02000052 RID: 82
		protected internal class InsertBeforeOp : TokenRewriteStream.RewriteOperation
		{
			// Token: 0x0600032B RID: 811 RVA: 0x00009AB6 File Offset: 0x00008AB6
			public InsertBeforeOp(int index, object text, TokenRewriteStream parent)
				: base(index, text, parent)
			{
			}

			// Token: 0x0600032C RID: 812 RVA: 0x00009AC1 File Offset: 0x00008AC1
			public override int Execute(StringBuilder buf)
			{
				buf.Append(this.text);
				buf.Append(this.parent.Get(this.index).Text);
				return this.index + 1;
			}
		}

		// Token: 0x02000053 RID: 83
		protected internal class ReplaceOp : TokenRewriteStream.RewriteOperation
		{
			// Token: 0x0600032D RID: 813 RVA: 0x00009AF5 File Offset: 0x00008AF5
			public ReplaceOp(int from, int to, object text, TokenRewriteStream parent)
				: base(from, text, parent)
			{
				this.lastIndex = to;
			}

			// Token: 0x0600032E RID: 814 RVA: 0x00009B08 File Offset: 0x00008B08
			public override int Execute(StringBuilder buf)
			{
				if (this.text != null)
				{
					buf.Append(this.text);
				}
				return this.lastIndex + 1;
			}

			// Token: 0x0600032F RID: 815 RVA: 0x00009B28 File Offset: 0x00008B28
			public override string ToString()
			{
				return string.Concat(new object[] { "<ReplaceOp@", this.index, "..", this.lastIndex, ":\"", this.text, "\">" });
			}

			// Token: 0x040000ED RID: 237
			protected internal int lastIndex;
		}

		// Token: 0x02000054 RID: 84
		protected internal class DeleteOp : TokenRewriteStream.ReplaceOp
		{
			// Token: 0x06000330 RID: 816 RVA: 0x00009B87 File Offset: 0x00008B87
			public DeleteOp(int from, int to, TokenRewriteStream parent)
				: base(from, to, null, parent)
			{
			}

			// Token: 0x06000331 RID: 817 RVA: 0x00009B94 File Offset: 0x00008B94
			public override string ToString()
			{
				return string.Concat(new object[] { "<DeleteOp@", this.index, "..", this.lastIndex, ">" });
			}
		}
	}
}
