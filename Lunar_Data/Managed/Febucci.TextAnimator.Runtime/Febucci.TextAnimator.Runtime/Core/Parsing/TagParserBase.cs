using System;
using System.Text;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000053 RID: 83
	public abstract class TagParserBase
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00007D9C File Offset: 0x00005F9C
		public TagParserBase()
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public TagParserBase(char startSymbol, char closingSymbol, char endSymbol)
		{
			this.startSymbol = startSymbol;
			this.closingSymbol = closingSymbol;
			this.endSymbol = endSymbol;
		}

		// Token: 0x060001A2 RID: 418
		public abstract bool TryProcessingTag(string textInsideBrackets, int tagLength, ref int realTextIndex, StringBuilder finalTextBuilder, int internalOrder);

		// Token: 0x060001A3 RID: 419 RVA: 0x00007DC1 File Offset: 0x00005FC1
		public void Initialize()
		{
			this.OnInitialize();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00007DC9 File Offset: 0x00005FC9
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x04000122 RID: 290
		public char startSymbol;

		// Token: 0x04000123 RID: 291
		public char endSymbol;

		// Token: 0x04000124 RID: 292
		public char closingSymbol;
	}
}
