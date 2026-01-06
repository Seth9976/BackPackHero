using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Collections;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200004E RID: 78
	public class CommonTokenStream : ITokenStream, IIntStream
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00008AE2 File Offset: 0x00007AE2
		public CommonTokenStream()
		{
			this.channel = 0;
			this.tokens = new List<object>(500);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008B08 File Offset: 0x00007B08
		public CommonTokenStream(ITokenSource tokenSource)
			: this()
		{
			this.tokenSource = tokenSource;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008B17 File Offset: 0x00007B17
		public CommonTokenStream(ITokenSource tokenSource, int channel)
			: this(tokenSource)
		{
			this.channel = channel;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008B28 File Offset: 0x00007B28
		public virtual IToken LT(int k)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (k < 0)
			{
				return this.LB(-k);
			}
			if (this.p + k - 1 >= this.tokens.Count)
			{
				return Token.EOF_TOKEN;
			}
			int num = this.p;
			for (int i = 1; i < k; i++)
			{
				num = this.SkipOffTokenChannels(num + 1);
			}
			if (num >= this.tokens.Count)
			{
				return Token.EOF_TOKEN;
			}
			return (IToken)this.tokens[num];
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008BB5 File Offset: 0x00007BB5
		public virtual IToken Get(int i)
		{
			return (IToken)this.tokens[i];
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00008BC8 File Offset: 0x00007BC8
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00008BD0 File Offset: 0x00007BD0
		public virtual ITokenSource TokenSource
		{
			get
			{
				return this.tokenSource;
			}
			set
			{
				this.tokenSource = value;
				this.tokens.Clear();
				this.p = -1;
				this.channel = 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00008BF2 File Offset: 0x00007BF2
		public virtual string SourceName
		{
			get
			{
				return this.TokenSource.SourceName;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00008C00 File Offset: 0x00007C00
		public virtual string ToString(int start, int stop)
		{
			if (start < 0 || stop < 0)
			{
				return null;
			}
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (stop >= this.tokens.Count)
			{
				stop = this.tokens.Count - 1;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = start; i <= stop; i++)
			{
				IToken token = (IToken)this.tokens[i];
				stringBuilder.Append(token.Text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00008C7A File Offset: 0x00007C7A
		public virtual string ToString(IToken start, IToken stop)
		{
			if (start != null && stop != null)
			{
				return this.ToString(start.TokenIndex, stop.TokenIndex);
			}
			return null;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00008C96 File Offset: 0x00007C96
		public virtual void Consume()
		{
			if (this.p < this.tokens.Count)
			{
				this.p++;
				this.p = this.SkipOffTokenChannels(this.p);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008CCB File Offset: 0x00007CCB
		public virtual int LA(int i)
		{
			return this.LT(i).Type;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00008CD9 File Offset: 0x00007CD9
		public virtual int Mark()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			this.lastMarker = this.Index();
			return this.lastMarker;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00008CFC File Offset: 0x00007CFC
		public virtual int Index()
		{
			return this.p;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00008D04 File Offset: 0x00007D04
		public virtual void Rewind(int marker)
		{
			this.Seek(marker);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00008D0D File Offset: 0x00007D0D
		public virtual void Rewind()
		{
			this.Seek(this.lastMarker);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00008D1B File Offset: 0x00007D1B
		public virtual void Reset()
		{
			this.p = 0;
			this.lastMarker = 0;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00008D2B File Offset: 0x00007D2B
		public virtual void Release(int marker)
		{
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00008D2D File Offset: 0x00007D2D
		public virtual void Seek(int index)
		{
			this.p = index;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008D36 File Offset: 0x00007D36
		[Obsolete("Please use the property Count instead.")]
		public virtual int Size()
		{
			return this.Count;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00008D3E File Offset: 0x00007D3E
		public virtual int Count
		{
			get
			{
				return this.tokens.Count;
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008D4C File Offset: 0x00007D4C
		protected virtual void FillBuffer()
		{
			int num = 0;
			IToken token = this.tokenSource.NextToken();
			while (token != null && token.Type != -1)
			{
				bool flag = false;
				if (this.channelOverrideMap != null)
				{
					object obj = this.channelOverrideMap[token.Type];
					if (obj != null)
					{
						token.Channel = (int)obj;
					}
				}
				if (this.discardSet != null && this.discardSet.Contains(token.Type.ToString()))
				{
					flag = true;
				}
				else if (this.discardOffChannelTokens && token.Channel != this.channel)
				{
					flag = true;
				}
				if (!flag)
				{
					token.TokenIndex = num;
					this.tokens.Add(token);
					num++;
				}
				token = this.tokenSource.NextToken();
			}
			this.p = 0;
			this.p = this.SkipOffTokenChannels(this.p);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008E2C File Offset: 0x00007E2C
		protected virtual int SkipOffTokenChannels(int i)
		{
			int count = this.tokens.Count;
			while (i < count && ((IToken)this.tokens[i]).Channel != this.channel)
			{
				i++;
			}
			return i;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008E6F File Offset: 0x00007E6F
		protected virtual int SkipOffTokenChannelsReverse(int i)
		{
			while (i >= 0 && ((IToken)this.tokens[i]).Channel != this.channel)
			{
				i--;
			}
			return i;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008E9B File Offset: 0x00007E9B
		public virtual void SetTokenTypeChannel(int ttype, int channel)
		{
			if (this.channelOverrideMap == null)
			{
				this.channelOverrideMap = new Hashtable();
			}
			this.channelOverrideMap[ttype] = channel;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008EC7 File Offset: 0x00007EC7
		public virtual void DiscardTokenType(int ttype)
		{
			if (this.discardSet == null)
			{
				this.discardSet = new HashList();
			}
			this.discardSet.Add(ttype.ToString(), ttype);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008EF4 File Offset: 0x00007EF4
		public virtual void DiscardOffChannelTokens(bool discardOffChannelTokens)
		{
			this.discardOffChannelTokens = discardOffChannelTokens;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00008EFD File Offset: 0x00007EFD
		public virtual IList GetTokens()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return this.tokens;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00008F14 File Offset: 0x00007F14
		public virtual IList GetTokens(int start, int stop)
		{
			return this.GetTokens(start, stop, null);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00008F20 File Offset: 0x00007F20
		public virtual IList GetTokens(int start, int stop, BitSet types)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (stop >= this.tokens.Count)
			{
				stop = this.tokens.Count - 1;
			}
			if (start < 0)
			{
				start = 0;
			}
			if (start > stop)
			{
				return null;
			}
			IList list = new List<object>();
			for (int i = start; i <= stop; i++)
			{
				IToken token = (IToken)this.tokens[i];
				if (types == null || types.Member(token.Type))
				{
					list.Add(token);
				}
			}
			if (list.Count == 0)
			{
				list = null;
			}
			return list;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00008FAE File Offset: 0x00007FAE
		public virtual IList GetTokens(int start, int stop, IList types)
		{
			return this.GetTokens(start, stop, new BitSet(types));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00008FBE File Offset: 0x00007FBE
		public virtual IList GetTokens(int start, int stop, int ttype)
		{
			return this.GetTokens(start, stop, BitSet.Of(ttype));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00008FD0 File Offset: 0x00007FD0
		protected virtual IToken LB(int k)
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			if (k == 0)
			{
				return null;
			}
			if (this.p - k < 0)
			{
				return null;
			}
			int num = this.p;
			for (int i = 1; i <= k; i++)
			{
				num = this.SkipOffTokenChannelsReverse(num - 1);
			}
			if (num < 0)
			{
				return null;
			}
			return (IToken)this.tokens[num];
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009032 File Offset: 0x00008032
		public override string ToString()
		{
			if (this.p == -1)
			{
				this.FillBuffer();
			}
			return this.ToString(0, this.tokens.Count - 1);
		}

		// Token: 0x040000DC RID: 220
		protected ITokenSource tokenSource;

		// Token: 0x040000DD RID: 221
		protected IList tokens;

		// Token: 0x040000DE RID: 222
		protected IDictionary channelOverrideMap;

		// Token: 0x040000DF RID: 223
		protected HashList discardSet;

		// Token: 0x040000E0 RID: 224
		protected int channel;

		// Token: 0x040000E1 RID: 225
		protected bool discardOffChannelTokens;

		// Token: 0x040000E2 RID: 226
		protected int lastMarker;

		// Token: 0x040000E3 RID: 227
		protected int p = -1;
	}
}
