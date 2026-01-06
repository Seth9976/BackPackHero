using System;

namespace System.Data
{
	// Token: 0x020000A7 RID: 167
	internal sealed class Operators
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00003D55 File Offset: 0x00001F55
		private Operators()
		{
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0003185C File Offset: 0x0002FA5C
		internal static bool IsArithmetical(int op)
		{
			return op == 15 || op == 16 || op == 17 || op == 18 || op == 20;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00031879 File Offset: 0x0002FA79
		internal static bool IsLogical(int op)
		{
			return op == 26 || op == 27 || op == 3 || op == 13 || op == 39;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00031895 File Offset: 0x0002FA95
		internal static bool IsRelational(int op)
		{
			return 7 <= op && op <= 12;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000318A5 File Offset: 0x0002FAA5
		internal static int Priority(int op)
		{
			if (op > Operators.s_priority.Length)
			{
				return 24;
			}
			return Operators.s_priority[op];
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000318BC File Offset: 0x0002FABC
		internal static string ToString(int op)
		{
			string text;
			if (op <= Operators.s_looks.Length)
			{
				text = Operators.s_looks[op];
			}
			else
			{
				text = "Unknown op";
			}
			return text;
		}

		// Token: 0x04000708 RID: 1800
		internal const int Noop = 0;

		// Token: 0x04000709 RID: 1801
		internal const int Negative = 1;

		// Token: 0x0400070A RID: 1802
		internal const int UnaryPlus = 2;

		// Token: 0x0400070B RID: 1803
		internal const int Not = 3;

		// Token: 0x0400070C RID: 1804
		internal const int BetweenAnd = 4;

		// Token: 0x0400070D RID: 1805
		internal const int In = 5;

		// Token: 0x0400070E RID: 1806
		internal const int Between = 6;

		// Token: 0x0400070F RID: 1807
		internal const int EqualTo = 7;

		// Token: 0x04000710 RID: 1808
		internal const int GreaterThen = 8;

		// Token: 0x04000711 RID: 1809
		internal const int LessThen = 9;

		// Token: 0x04000712 RID: 1810
		internal const int GreaterOrEqual = 10;

		// Token: 0x04000713 RID: 1811
		internal const int LessOrEqual = 11;

		// Token: 0x04000714 RID: 1812
		internal const int NotEqual = 12;

		// Token: 0x04000715 RID: 1813
		internal const int Is = 13;

		// Token: 0x04000716 RID: 1814
		internal const int Like = 14;

		// Token: 0x04000717 RID: 1815
		internal const int Plus = 15;

		// Token: 0x04000718 RID: 1816
		internal const int Minus = 16;

		// Token: 0x04000719 RID: 1817
		internal const int Multiply = 17;

		// Token: 0x0400071A RID: 1818
		internal const int Divide = 18;

		// Token: 0x0400071B RID: 1819
		internal const int Modulo = 20;

		// Token: 0x0400071C RID: 1820
		internal const int BitwiseAnd = 22;

		// Token: 0x0400071D RID: 1821
		internal const int BitwiseOr = 23;

		// Token: 0x0400071E RID: 1822
		internal const int BitwiseXor = 24;

		// Token: 0x0400071F RID: 1823
		internal const int BitwiseNot = 25;

		// Token: 0x04000720 RID: 1824
		internal const int And = 26;

		// Token: 0x04000721 RID: 1825
		internal const int Or = 27;

		// Token: 0x04000722 RID: 1826
		internal const int Proc = 28;

		// Token: 0x04000723 RID: 1827
		internal const int Iff = 29;

		// Token: 0x04000724 RID: 1828
		internal const int Qual = 30;

		// Token: 0x04000725 RID: 1829
		internal const int Dot = 31;

		// Token: 0x04000726 RID: 1830
		internal const int Null = 32;

		// Token: 0x04000727 RID: 1831
		internal const int True = 33;

		// Token: 0x04000728 RID: 1832
		internal const int False = 34;

		// Token: 0x04000729 RID: 1833
		internal const int Date = 35;

		// Token: 0x0400072A RID: 1834
		internal const int GenUniqueId = 36;

		// Token: 0x0400072B RID: 1835
		internal const int GenGUID = 37;

		// Token: 0x0400072C RID: 1836
		internal const int GUID = 38;

		// Token: 0x0400072D RID: 1837
		internal const int IsNot = 39;

		// Token: 0x0400072E RID: 1838
		internal const int priStart = 0;

		// Token: 0x0400072F RID: 1839
		internal const int priSubstr = 1;

		// Token: 0x04000730 RID: 1840
		internal const int priParen = 2;

		// Token: 0x04000731 RID: 1841
		internal const int priLow = 3;

		// Token: 0x04000732 RID: 1842
		internal const int priImp = 4;

		// Token: 0x04000733 RID: 1843
		internal const int priEqv = 5;

		// Token: 0x04000734 RID: 1844
		internal const int priXor = 6;

		// Token: 0x04000735 RID: 1845
		internal const int priOr = 7;

		// Token: 0x04000736 RID: 1846
		internal const int priAnd = 8;

		// Token: 0x04000737 RID: 1847
		internal const int priNot = 9;

		// Token: 0x04000738 RID: 1848
		internal const int priIs = 10;

		// Token: 0x04000739 RID: 1849
		internal const int priBetweenInLike = 11;

		// Token: 0x0400073A RID: 1850
		internal const int priBetweenAnd = 12;

		// Token: 0x0400073B RID: 1851
		internal const int priRelOp = 13;

		// Token: 0x0400073C RID: 1852
		internal const int priConcat = 14;

		// Token: 0x0400073D RID: 1853
		internal const int priContains = 15;

		// Token: 0x0400073E RID: 1854
		internal const int priPlusMinus = 16;

		// Token: 0x0400073F RID: 1855
		internal const int priMod = 17;

		// Token: 0x04000740 RID: 1856
		internal const int priIDiv = 18;

		// Token: 0x04000741 RID: 1857
		internal const int priMulDiv = 19;

		// Token: 0x04000742 RID: 1858
		internal const int priNeg = 20;

		// Token: 0x04000743 RID: 1859
		internal const int priExp = 21;

		// Token: 0x04000744 RID: 1860
		internal const int priProc = 22;

		// Token: 0x04000745 RID: 1861
		internal const int priDot = 23;

		// Token: 0x04000746 RID: 1862
		internal const int priMax = 24;

		// Token: 0x04000747 RID: 1863
		private static readonly int[] s_priority = new int[]
		{
			0, 20, 20, 9, 12, 11, 11, 13, 13, 13,
			13, 13, 13, 10, 11, 16, 16, 19, 19, 18,
			17, 21, 8, 7, 6, 9, 8, 7, 2, 22,
			23, 23, 24, 24, 24, 24, 24, 24, 24, 24,
			24, 24, 24, 24
		};

		// Token: 0x04000748 RID: 1864
		private static readonly string[] s_looks = new string[]
		{
			"", "-", "+", "Not", "BetweenAnd", "In", "Between", "=", ">", "<",
			">=", "<=", "<>", "Is", "Like", "+", "-", "*", "/", "\\",
			"Mod", "**", "&", "|", "^", "~", "And", "Or", "Proc", "Iff",
			".", ".", "Null", "True", "False", "Date", "GenUniqueId()", "GenGuid()", "Guid {..}", "Is Not"
		};
	}
}
