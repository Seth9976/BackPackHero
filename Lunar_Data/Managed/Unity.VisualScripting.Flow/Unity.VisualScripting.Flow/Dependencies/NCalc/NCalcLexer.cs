using System;
using Unity.VisualScripting.Antlr3.Runtime;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000195 RID: 405
	public class NCalcLexer : Lexer
	{
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00014B87 File Offset: 0x00012D87
		public NCalcLexer()
		{
			this.InitializeCyclicDFAs();
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00014B95 File Offset: 0x00012D95
		public NCalcLexer(ICharStream input)
			: this(input, null)
		{
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00014B9F File Offset: 0x00012D9F
		public NCalcLexer(ICharStream input, RecognizerSharedState state)
			: base(input, state)
		{
			this.InitializeCyclicDFAs();
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00014BAF File Offset: 0x00012DAF
		public override string GrammarFileName
		{
			get
			{
				return "C:\\Users\\s.ros\\Documents\\D\ufffdveloppement\\NCalc\\Grammar\\NCalc.g";
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x00014BB6 File Offset: 0x00012DB6
		private void InitializeCyclicDFAs()
		{
			this.dfa7 = new NCalcLexer.DFA7(this);
			this.dfa14 = new NCalcLexer.DFA14(this);
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00014BD0 File Offset: 0x00012DD0
		public void mT__19()
		{
			int num = 19;
			int num2 = 0;
			this.Match(63);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00014C04 File Offset: 0x00012E04
		public void mT__20()
		{
			int num = 20;
			int num2 = 0;
			this.Match(58);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00014C38 File Offset: 0x00012E38
		public void mT__21()
		{
			int num = 21;
			int num2 = 0;
			this.Match("||");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00014C70 File Offset: 0x00012E70
		public void mT__22()
		{
			int num = 22;
			int num2 = 0;
			this.Match("or");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00014CA8 File Offset: 0x00012EA8
		public void mT__23()
		{
			int num = 23;
			int num2 = 0;
			this.Match("&&");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00014CE0 File Offset: 0x00012EE0
		public void mT__24()
		{
			int num = 24;
			int num2 = 0;
			this.Match("and");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00014D18 File Offset: 0x00012F18
		public void mT__25()
		{
			int num = 25;
			int num2 = 0;
			this.Match(124);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00014D4C File Offset: 0x00012F4C
		public void mT__26()
		{
			int num = 26;
			int num2 = 0;
			this.Match(94);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00014D80 File Offset: 0x00012F80
		public void mT__27()
		{
			int num = 27;
			int num2 = 0;
			this.Match(38);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x00014DB4 File Offset: 0x00012FB4
		public void mT__28()
		{
			int num = 28;
			int num2 = 0;
			this.Match("==");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00014DEC File Offset: 0x00012FEC
		public void mT__29()
		{
			int num = 29;
			int num2 = 0;
			this.Match(61);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00014E20 File Offset: 0x00013020
		public void mT__30()
		{
			int num = 30;
			int num2 = 0;
			this.Match("!=");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00014E58 File Offset: 0x00013058
		public void mT__31()
		{
			int num = 31;
			int num2 = 0;
			this.Match("<>");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00014E90 File Offset: 0x00013090
		public void mT__32()
		{
			int num = 32;
			int num2 = 0;
			this.Match(60);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00014EC4 File Offset: 0x000130C4
		public void mT__33()
		{
			int num = 33;
			int num2 = 0;
			this.Match("<=");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00014EFC File Offset: 0x000130FC
		public void mT__34()
		{
			int num = 34;
			int num2 = 0;
			this.Match(62);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00014F30 File Offset: 0x00013130
		public void mT__35()
		{
			int num = 35;
			int num2 = 0;
			this.Match(">=");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00014F68 File Offset: 0x00013168
		public void mT__36()
		{
			int num = 36;
			int num2 = 0;
			this.Match("<<");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00014FA0 File Offset: 0x000131A0
		public void mT__37()
		{
			int num = 37;
			int num2 = 0;
			this.Match(">>");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00014FD8 File Offset: 0x000131D8
		public void mT__38()
		{
			int num = 38;
			int num2 = 0;
			this.Match(43);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001500C File Offset: 0x0001320C
		public void mT__39()
		{
			int num = 39;
			int num2 = 0;
			this.Match(45);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00015040 File Offset: 0x00013240
		public void mT__40()
		{
			int num = 40;
			int num2 = 0;
			this.Match(42);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00015074 File Offset: 0x00013274
		public void mT__41()
		{
			int num = 41;
			int num2 = 0;
			this.Match(47);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000150A8 File Offset: 0x000132A8
		public void mT__42()
		{
			int num = 42;
			int num2 = 0;
			this.Match(37);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000150DC File Offset: 0x000132DC
		public void mT__43()
		{
			int num = 43;
			int num2 = 0;
			this.Match(33);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00015110 File Offset: 0x00013310
		public void mT__44()
		{
			int num = 44;
			int num2 = 0;
			this.Match("not");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00015148 File Offset: 0x00013348
		public void mT__45()
		{
			int num = 45;
			int num2 = 0;
			this.Match(126);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001517C File Offset: 0x0001337C
		public void mT__46()
		{
			int num = 46;
			int num2 = 0;
			this.Match(40);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000151B0 File Offset: 0x000133B0
		public void mT__47()
		{
			int num = 47;
			int num2 = 0;
			this.Match(41);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000151E4 File Offset: 0x000133E4
		public void mT__48()
		{
			int num = 48;
			int num2 = 0;
			this.Match(44);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00015218 File Offset: 0x00013418
		public void mTRUE()
		{
			int num = 8;
			int num2 = 0;
			this.Match("true");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0001524C File Offset: 0x0001344C
		public void mFALSE()
		{
			int num = 9;
			int num2 = 0;
			this.Match("false");
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00015284 File Offset: 0x00013484
		public void mID()
		{
			int num = 10;
			int num2 = 0;
			this.mLETTER();
			for (;;)
			{
				int num3 = 2;
				int num4 = this.input.LA(1);
				if ((num4 >= 48 && num4 <= 57) || (num4 >= 65 && num4 <= 90) || num4 == 95 || (num4 >= 97 && num4 <= 122))
				{
					num3 = 1;
				}
				if (num3 != 1)
				{
					goto IL_00DF;
				}
				if ((this.input.LA(1) < 48 || this.input.LA(1) > 57) && (this.input.LA(1) < 65 || this.input.LA(1) > 90) && this.input.LA(1) != 95 && (this.input.LA(1) < 97 || this.input.LA(1) > 122))
				{
					break;
				}
				this.input.Consume();
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
			IL_00DF:
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00015388 File Offset: 0x00013588
		public void mINTEGER()
		{
			int num = 4;
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				int num4 = 2;
				int num5 = this.input.LA(1);
				if (num5 >= 48 && num5 <= 57)
				{
					num4 = 1;
				}
				if (num4 != 1)
				{
					break;
				}
				this.mDIGIT();
				num3++;
			}
			if (num3 < 1)
			{
				throw new EarlyExitException(2, this.input);
			}
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000153F4 File Offset: 0x000135F4
		public void mFLOAT()
		{
			int num = 5;
			int num2 = 0;
			int num3 = this.dfa7.Predict(this.input);
			if (num3 != 1)
			{
				if (num3 == 2)
				{
					int num4 = 0;
					for (;;)
					{
						int num5 = 2;
						int num6 = this.input.LA(1);
						if (num6 >= 48 && num6 <= 57)
						{
							num5 = 1;
						}
						if (num5 != 1)
						{
							break;
						}
						this.mDIGIT();
						num4++;
					}
					if (num4 < 1)
					{
						throw new EarlyExitException(6, this.input);
					}
					this.mE();
				}
			}
			else
			{
				for (;;)
				{
					int num7 = 2;
					int num8 = this.input.LA(1);
					if (num8 >= 48 && num8 <= 57)
					{
						num7 = 1;
					}
					if (num7 != 1)
					{
						break;
					}
					this.mDIGIT();
				}
				this.Match(46);
				int num9 = 0;
				for (;;)
				{
					int num10 = 2;
					int num11 = this.input.LA(1);
					if (num11 >= 48 && num11 <= 57)
					{
						num10 = 1;
					}
					if (num10 != 1)
					{
						break;
					}
					this.mDIGIT();
					num9++;
				}
				if (num9 < 1)
				{
					throw new EarlyExitException(4, this.input);
				}
				int num12 = 2;
				int num13 = this.input.LA(1);
				if (num13 == 69 || num13 == 101)
				{
					num12 = 1;
				}
				if (num12 == 1)
				{
					this.mE();
				}
			}
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001553C File Offset: 0x0001373C
		public void mSTRING()
		{
			int num = 6;
			int num2 = 0;
			this.Match(39);
			for (;;)
			{
				int num3 = 3;
				int num4 = this.input.LA(1);
				if (num4 == 92)
				{
					num3 = 1;
				}
				else if ((num4 >= 32 && num4 <= 38) || (num4 >= 40 && num4 <= 91) || (num4 >= 93 && num4 <= 65535))
				{
					num3 = 2;
				}
				if (num3 != 1)
				{
					if (num3 != 2)
					{
						break;
					}
					if ((this.input.LA(1) < 32 || this.input.LA(1) > 38) && (this.input.LA(1) < 40 || this.input.LA(1) > 91) && (this.input.LA(1) < 93 || this.input.LA(1) > 65535))
					{
						goto IL_00CF;
					}
					this.input.Consume();
				}
				else
				{
					this.mEscapeSequence();
				}
			}
			this.Match(39);
			this.state.type = num;
			this.state.channel = num2;
			return;
			IL_00CF:
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00015654 File Offset: 0x00013854
		public void mDATETIME()
		{
			int num = 7;
			int num2 = 0;
			this.Match(35);
			for (;;)
			{
				int num3 = 2;
				int num4 = this.input.LA(1);
				if ((num4 >= 0 && num4 <= 34) || (num4 >= 36 && num4 <= 65535))
				{
					num3 = 1;
				}
				if (num3 != 1)
				{
					goto IL_009F;
				}
				if ((this.input.LA(1) < 0 || this.input.LA(1) > 34) && (this.input.LA(1) < 36 || this.input.LA(1) > 65535))
				{
					break;
				}
				this.input.Consume();
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
			IL_009F:
			this.Match(35);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00015720 File Offset: 0x00013920
		public void mNAME()
		{
			int num = 11;
			int num2 = 0;
			this.Match(91);
			for (;;)
			{
				int num3 = 2;
				int num4 = this.input.LA(1);
				if ((num4 >= 0 && num4 <= 92) || (num4 >= 94 && num4 <= 65535))
				{
					num3 = 1;
				}
				if (num3 != 1)
				{
					goto IL_00A0;
				}
				if ((this.input.LA(1) < 0 || this.input.LA(1) > 92) && (this.input.LA(1) < 94 || this.input.LA(1) > 65535))
				{
					break;
				}
				this.input.Consume();
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
			IL_00A0:
			this.Match(93);
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000157F0 File Offset: 0x000139F0
		public void mE()
		{
			int num = 14;
			int num2 = 0;
			if (this.input.LA(1) != 69 && this.input.LA(1) != 101)
			{
				MismatchedSetException ex = new MismatchedSetException(null, this.input);
				this.Recover(ex);
				throw ex;
			}
			this.input.Consume();
			int num3 = 2;
			int num4 = this.input.LA(1);
			if (num4 == 43 || num4 == 45)
			{
				num3 = 1;
			}
			if (num3 == 1)
			{
				if (this.input.LA(1) != 43 && this.input.LA(1) != 45)
				{
					MismatchedSetException ex2 = new MismatchedSetException(null, this.input);
					this.Recover(ex2);
					throw ex2;
				}
				this.input.Consume();
			}
			int num5 = 0;
			for (;;)
			{
				int num6 = 2;
				int num7 = this.input.LA(1);
				if (num7 >= 48 && num7 <= 57)
				{
					num6 = 1;
				}
				if (num6 != 1)
				{
					break;
				}
				this.mDIGIT();
				num5++;
			}
			if (num5 < 1)
			{
				throw new EarlyExitException(12, this.input);
			}
			this.state.type = num;
			this.state.channel = num2;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00015910 File Offset: 0x00013B10
		public void mLETTER()
		{
			if ((this.input.LA(1) >= 65 && this.input.LA(1) <= 90) || this.input.LA(1) == 95 || (this.input.LA(1) >= 97 && this.input.LA(1) <= 122))
			{
				this.input.Consume();
				return;
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001598E File Offset: 0x00013B8E
		public void mDIGIT()
		{
			this.MatchRange(48, 57);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001599C File Offset: 0x00013B9C
		public void mEscapeSequence()
		{
			this.Match(92);
			int num = this.input.LA(1);
			int num2;
			if (num <= 92)
			{
				if (num == 39)
				{
					num2 = 4;
					goto IL_0074;
				}
				if (num == 92)
				{
					num2 = 5;
					goto IL_0074;
				}
			}
			else
			{
				if (num == 110)
				{
					num2 = 1;
					goto IL_0074;
				}
				switch (num)
				{
				case 114:
					num2 = 2;
					goto IL_0074;
				case 116:
					num2 = 3;
					goto IL_0074;
				case 117:
					num2 = 6;
					goto IL_0074;
				}
			}
			throw new NoViableAltException("", 13, 0, this.input);
			IL_0074:
			switch (num2)
			{
			case 1:
				this.Match(110);
				return;
			case 2:
				this.Match(114);
				return;
			case 3:
				this.Match(116);
				return;
			case 4:
				this.Match(39);
				return;
			case 5:
				this.Match(92);
				return;
			case 6:
				this.mUnicodeEscape();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00015A74 File Offset: 0x00013C74
		public void mHexDigit()
		{
			if ((this.input.LA(1) >= 48 && this.input.LA(1) <= 57) || (this.input.LA(1) >= 65 && this.input.LA(1) <= 70) || (this.input.LA(1) >= 97 && this.input.LA(1) <= 102))
			{
				this.input.Consume();
				return;
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00015B02 File Offset: 0x00013D02
		public void mUnicodeEscape()
		{
			this.Match(117);
			this.mHexDigit();
			this.mHexDigit();
			this.mHexDigit();
			this.mHexDigit();
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00015B24 File Offset: 0x00013D24
		public void mWS()
		{
			int num = 18;
			if ((this.input.LA(1) >= 9 && this.input.LA(1) <= 10) || (this.input.LA(1) >= 12 && this.input.LA(1) <= 13) || this.input.LA(1) == 32)
			{
				this.input.Consume();
				int num2 = 99;
				this.state.type = num;
				this.state.channel = num2;
				return;
			}
			MismatchedSetException ex = new MismatchedSetException(null, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00015BC4 File Offset: 0x00013DC4
		public override void mTokens()
		{
			switch (this.dfa14.Predict(this.input))
			{
			case 1:
				this.mT__19();
				return;
			case 2:
				this.mT__20();
				return;
			case 3:
				this.mT__21();
				return;
			case 4:
				this.mT__22();
				return;
			case 5:
				this.mT__23();
				return;
			case 6:
				this.mT__24();
				return;
			case 7:
				this.mT__25();
				return;
			case 8:
				this.mT__26();
				return;
			case 9:
				this.mT__27();
				return;
			case 10:
				this.mT__28();
				return;
			case 11:
				this.mT__29();
				return;
			case 12:
				this.mT__30();
				return;
			case 13:
				this.mT__31();
				return;
			case 14:
				this.mT__32();
				return;
			case 15:
				this.mT__33();
				return;
			case 16:
				this.mT__34();
				return;
			case 17:
				this.mT__35();
				return;
			case 18:
				this.mT__36();
				return;
			case 19:
				this.mT__37();
				return;
			case 20:
				this.mT__38();
				return;
			case 21:
				this.mT__39();
				return;
			case 22:
				this.mT__40();
				return;
			case 23:
				this.mT__41();
				return;
			case 24:
				this.mT__42();
				return;
			case 25:
				this.mT__43();
				return;
			case 26:
				this.mT__44();
				return;
			case 27:
				this.mT__45();
				return;
			case 28:
				this.mT__46();
				return;
			case 29:
				this.mT__47();
				return;
			case 30:
				this.mT__48();
				return;
			case 31:
				this.mTRUE();
				return;
			case 32:
				this.mFALSE();
				return;
			case 33:
				this.mID();
				return;
			case 34:
				this.mINTEGER();
				return;
			case 35:
				this.mFLOAT();
				return;
			case 36:
				this.mSTRING();
				return;
			case 37:
				this.mDATETIME();
				return;
			case 38:
				this.mNAME();
				return;
			case 39:
				this.mE();
				return;
			case 40:
				this.mWS();
				return;
			default:
				return;
			}
		}

		// Token: 0x0400026C RID: 620
		protected NCalcLexer.DFA7 dfa7;

		// Token: 0x0400026D RID: 621
		protected NCalcLexer.DFA14 dfa14;

		// Token: 0x0400026E RID: 622
		public const int T__29 = 29;

		// Token: 0x0400026F RID: 623
		public const int T__28 = 28;

		// Token: 0x04000270 RID: 624
		public const int T__27 = 27;

		// Token: 0x04000271 RID: 625
		public const int T__26 = 26;

		// Token: 0x04000272 RID: 626
		public const int T__25 = 25;

		// Token: 0x04000273 RID: 627
		public const int T__24 = 24;

		// Token: 0x04000274 RID: 628
		public const int LETTER = 12;

		// Token: 0x04000275 RID: 629
		public const int T__23 = 23;

		// Token: 0x04000276 RID: 630
		public const int T__22 = 22;

		// Token: 0x04000277 RID: 631
		public const int T__21 = 21;

		// Token: 0x04000278 RID: 632
		public const int T__20 = 20;

		// Token: 0x04000279 RID: 633
		public const int FLOAT = 5;

		// Token: 0x0400027A RID: 634
		public const int ID = 10;

		// Token: 0x0400027B RID: 635
		public const int EOF = -1;

		// Token: 0x0400027C RID: 636
		public const int HexDigit = 17;

		// Token: 0x0400027D RID: 637
		public const int T__19 = 19;

		// Token: 0x0400027E RID: 638
		public const int NAME = 11;

		// Token: 0x0400027F RID: 639
		public const int DIGIT = 13;

		// Token: 0x04000280 RID: 640
		public const int T__42 = 42;

		// Token: 0x04000281 RID: 641
		public const int INTEGER = 4;

		// Token: 0x04000282 RID: 642
		public const int E = 14;

		// Token: 0x04000283 RID: 643
		public const int T__43 = 43;

		// Token: 0x04000284 RID: 644
		public const int T__40 = 40;

		// Token: 0x04000285 RID: 645
		public const int T__41 = 41;

		// Token: 0x04000286 RID: 646
		public const int T__46 = 46;

		// Token: 0x04000287 RID: 647
		public const int T__47 = 47;

		// Token: 0x04000288 RID: 648
		public const int T__44 = 44;

		// Token: 0x04000289 RID: 649
		public const int T__45 = 45;

		// Token: 0x0400028A RID: 650
		public const int T__48 = 48;

		// Token: 0x0400028B RID: 651
		public const int DATETIME = 7;

		// Token: 0x0400028C RID: 652
		public const int TRUE = 8;

		// Token: 0x0400028D RID: 653
		public const int T__30 = 30;

		// Token: 0x0400028E RID: 654
		public const int T__31 = 31;

		// Token: 0x0400028F RID: 655
		public const int T__32 = 32;

		// Token: 0x04000290 RID: 656
		public const int WS = 18;

		// Token: 0x04000291 RID: 657
		public const int T__33 = 33;

		// Token: 0x04000292 RID: 658
		public const int T__34 = 34;

		// Token: 0x04000293 RID: 659
		public const int T__35 = 35;

		// Token: 0x04000294 RID: 660
		public const int T__36 = 36;

		// Token: 0x04000295 RID: 661
		public const int T__37 = 37;

		// Token: 0x04000296 RID: 662
		public const int T__38 = 38;

		// Token: 0x04000297 RID: 663
		public const int T__39 = 39;

		// Token: 0x04000298 RID: 664
		public const int UnicodeEscape = 16;

		// Token: 0x04000299 RID: 665
		public const int FALSE = 9;

		// Token: 0x0400029A RID: 666
		public const int EscapeSequence = 15;

		// Token: 0x0400029B RID: 667
		public const int STRING = 6;

		// Token: 0x0400029C RID: 668
		private const string DFA7_eotS = "\u0004\uffff";

		// Token: 0x0400029D RID: 669
		private const string DFA7_eofS = "\u0004\uffff";

		// Token: 0x0400029E RID: 670
		private const string DFA7_minS = "\u0002.\u0002\uffff";

		// Token: 0x0400029F RID: 671
		private const string DFA7_maxS = "\u00019\u0001e\u0002\uffff";

		// Token: 0x040002A0 RID: 672
		private const string DFA7_acceptS = "\u0002\uffff\u0001\u0001\u0001\u0002";

		// Token: 0x040002A1 RID: 673
		private const string DFA7_specialS = "\u0004\uffff}>";

		// Token: 0x040002A2 RID: 674
		private const string DFA14_eotS = "\u0003\uffff\u0001!\u0001\u001e\u0001$\u0001\u001e\u0001\uffff\u0001'\u0001)\u0001-\u00010\u0005\uffff\u0001\u001e\u0004\uffff\u0003\u001e\u00016\b\uffff\u00017\u0002\uffff\u0001\u001e\v\uffff\u0003\u001e\u0001\uffff\u0001\u001e\u0002\uffff\u0001<\u0001=\u0002\u001e\u0002\uffff\u0001@\u0001\u001e\u0001\uffff\u0001B\u0001\uffff";

		// Token: 0x040002A3 RID: 675
		private const string DFA14_eofS = "C\uffff";

		// Token: 0x040002A4 RID: 676
		private const string DFA14_minS = "\u0001\t\u0002\uffff\u0001|\u0001r\u0001&\u0001n\u0001\uffff\u0002=\u0001<\u0001=\u0005\uffff\u0001o\u0004\uffff\u0001r\u0001a\u0001+\u0001.\b\uffff\u00010\u0002\uffff\u0001d\v\uffff\u0001t\u0001u\u0001l\u0001\uffff\u00010\u0002\uffff\u00020\u0001e\u0001s\u0002\uffff\u00010\u0001e\u0001\uffff\u00010\u0001\uffff";

		// Token: 0x040002A5 RID: 677
		private const string DFA14_maxS = "\u0001~\u0002\uffff\u0001|\u0001r\u0001&\u0001n\u0001\uffff\u0002=\u0002>\u0005\uffff\u0001o\u0004\uffff\u0001r\u0001a\u00019\u0001e\b\uffff\u0001z\u0002\uffff\u0001d\v\uffff\u0001t\u0001u\u0001l\u0001\uffff\u00019\u0002\uffff\u0002z\u0001e\u0001s\u0002\uffff\u0001z\u0001e\u0001\uffff\u0001z\u0001\uffff";

		// Token: 0x040002A6 RID: 678
		private const string DFA14_acceptS = "\u0001\uffff\u0001\u0001\u0001\u0002\u0004\uffff\u0001\b\u0004\uffff\u0001\u0014\u0001\u0015\u0001\u0016\u0001\u0017\u0001\u0018\u0001\uffff\u0001\u001b\u0001\u001c\u0001\u001d\u0001\u001e\u0004\uffff\u0001#\u0001$\u0001%\u0001&\u0001!\u0001(\u0001\u0003\u0001\a\u0001\uffff\u0001\u0005\u0001\t\u0001\uffff\u0001\n\u0001\v\u0001\f\u0001\u0019\u0001\r\u0001\u000f\u0001\u0012\u0001\u000e\u0001\u0011\u0001\u0013\u0001\u0010\u0003\uffff\u0001'\u0001\uffff\u0001\"\u0001\u0004\u0004\uffff\u0001\u0006\u0001\u001a\u0002\uffff\u0001\u001f\u0001\uffff\u0001 ";

		// Token: 0x040002A7 RID: 679
		private const string DFA14_specialS = "C\uffff}>";

		// Token: 0x040002A8 RID: 680
		private static readonly string[] DFA7_transitionS = new string[] { "\u0001\u0002\u0001\uffff\n\u0001", "\u0001\u0002\u0001\uffff\n\u0001\v\uffff\u0001\u0003\u001f\uffff\u0001\u0003", "", "" };

		// Token: 0x040002A9 RID: 681
		private static readonly short[] DFA7_eot = DFA.UnpackEncodedString("\u0004\uffff");

		// Token: 0x040002AA RID: 682
		private static readonly short[] DFA7_eof = DFA.UnpackEncodedString("\u0004\uffff");

		// Token: 0x040002AB RID: 683
		private static readonly char[] DFA7_min = DFA.UnpackEncodedStringToUnsignedChars("\u0002.\u0002\uffff");

		// Token: 0x040002AC RID: 684
		private static readonly char[] DFA7_max = DFA.UnpackEncodedStringToUnsignedChars("\u00019\u0001e\u0002\uffff");

		// Token: 0x040002AD RID: 685
		private static readonly short[] DFA7_accept = DFA.UnpackEncodedString("\u0002\uffff\u0001\u0001\u0001\u0002");

		// Token: 0x040002AE RID: 686
		private static readonly short[] DFA7_special = DFA.UnpackEncodedString("\u0004\uffff}>");

		// Token: 0x040002AF RID: 687
		private static readonly short[][] DFA7_transition = DFA.UnpackEncodedStringArray(NCalcLexer.DFA7_transitionS);

		// Token: 0x040002B0 RID: 688
		private static readonly string[] DFA14_transitionS = new string[]
		{
			"\u0002\u001f\u0001\uffff\u0002\u001f\u0012\uffff\u0001\u001f\u0001\t\u0001\uffff\u0001\u001c\u0001\uffff\u0001\u0010\u0001\u0005\u0001\u001b\u0001\u0013\u0001\u0014\u0001\u000e\u0001\f\u0001\u0015\u0001\r\u0001\u001a\u0001\u000f\n\u0019\u0001\u0002\u0001\uffff\u0001\n\u0001\b\u0001\v\u0001\u0001\u0001\uffff\u0004\u001e\u0001\u0018\u0015\u001e\u0001\u001d\u0002\uffff\u0001\a\u0001\u001e\u0001\uffff\u0001\u0006\u0003\u001e\u0001\u0018\u0001\u0017\a\u001e\u0001\u0011\u0001\u0004\u0004\u001e\u0001\u0016\u0006\u001e\u0001\uffff\u0001\u0003\u0001\uffff\u0001\u0012", "", "", "\u0001 ", "\u0001\"", "\u0001#", "\u0001%", "", "\u0001&", "\u0001(",
			"\u0001,\u0001+\u0001*", "\u0001.\u0001/", "", "", "", "", "", "\u00011", "", "",
			"", "", "\u00012", "\u00013", "\u00014\u0001\uffff\u00014\u0002\uffff\n5", "\u0001\u001a\u0001\uffff\n\u0019\v\uffff\u0001\u001a\u001f\uffff\u0001\u001a", "", "", "", "",
			"", "", "", "", "\n\u001e\a\uffff\u001a\u001e\u0004\uffff\u0001\u001e\u0001\uffff\u001a\u001e", "", "", "\u00018", "", "",
			"", "", "", "", "", "", "", "", "", "\u00019",
			"\u0001:", "\u0001;", "", "\n5", "", "", "\n\u001e\a\uffff\u001a\u001e\u0004\uffff\u0001\u001e\u0001\uffff\u001a\u001e", "\n\u001e\a\uffff\u001a\u001e\u0004\uffff\u0001\u001e\u0001\uffff\u001a\u001e", "\u0001>", "\u0001?",
			"", "", "\n\u001e\a\uffff\u001a\u001e\u0004\uffff\u0001\u001e\u0001\uffff\u001a\u001e", "\u0001A", "", "\n\u001e\a\uffff\u001a\u001e\u0004\uffff\u0001\u001e\u0001\uffff\u001a\u001e", ""
		};

		// Token: 0x040002B1 RID: 689
		private static readonly short[] DFA14_eot = DFA.UnpackEncodedString("\u0003\uffff\u0001!\u0001\u001e\u0001$\u0001\u001e\u0001\uffff\u0001'\u0001)\u0001-\u00010\u0005\uffff\u0001\u001e\u0004\uffff\u0003\u001e\u00016\b\uffff\u00017\u0002\uffff\u0001\u001e\v\uffff\u0003\u001e\u0001\uffff\u0001\u001e\u0002\uffff\u0001<\u0001=\u0002\u001e\u0002\uffff\u0001@\u0001\u001e\u0001\uffff\u0001B\u0001\uffff");

		// Token: 0x040002B2 RID: 690
		private static readonly short[] DFA14_eof = DFA.UnpackEncodedString("C\uffff");

		// Token: 0x040002B3 RID: 691
		private static readonly char[] DFA14_min = DFA.UnpackEncodedStringToUnsignedChars("\u0001\t\u0002\uffff\u0001|\u0001r\u0001&\u0001n\u0001\uffff\u0002=\u0001<\u0001=\u0005\uffff\u0001o\u0004\uffff\u0001r\u0001a\u0001+\u0001.\b\uffff\u00010\u0002\uffff\u0001d\v\uffff\u0001t\u0001u\u0001l\u0001\uffff\u00010\u0002\uffff\u00020\u0001e\u0001s\u0002\uffff\u00010\u0001e\u0001\uffff\u00010\u0001\uffff");

		// Token: 0x040002B4 RID: 692
		private static readonly char[] DFA14_max = DFA.UnpackEncodedStringToUnsignedChars("\u0001~\u0002\uffff\u0001|\u0001r\u0001&\u0001n\u0001\uffff\u0002=\u0002>\u0005\uffff\u0001o\u0004\uffff\u0001r\u0001a\u00019\u0001e\b\uffff\u0001z\u0002\uffff\u0001d\v\uffff\u0001t\u0001u\u0001l\u0001\uffff\u00019\u0002\uffff\u0002z\u0001e\u0001s\u0002\uffff\u0001z\u0001e\u0001\uffff\u0001z\u0001\uffff");

		// Token: 0x040002B5 RID: 693
		private static readonly short[] DFA14_accept = DFA.UnpackEncodedString("\u0001\uffff\u0001\u0001\u0001\u0002\u0004\uffff\u0001\b\u0004\uffff\u0001\u0014\u0001\u0015\u0001\u0016\u0001\u0017\u0001\u0018\u0001\uffff\u0001\u001b\u0001\u001c\u0001\u001d\u0001\u001e\u0004\uffff\u0001#\u0001$\u0001%\u0001&\u0001!\u0001(\u0001\u0003\u0001\a\u0001\uffff\u0001\u0005\u0001\t\u0001\uffff\u0001\n\u0001\v\u0001\f\u0001\u0019\u0001\r\u0001\u000f\u0001\u0012\u0001\u000e\u0001\u0011\u0001\u0013\u0001\u0010\u0003\uffff\u0001'\u0001\uffff\u0001\"\u0001\u0004\u0004\uffff\u0001\u0006\u0001\u001a\u0002\uffff\u0001\u001f\u0001\uffff\u0001 ");

		// Token: 0x040002B6 RID: 694
		private static readonly short[] DFA14_special = DFA.UnpackEncodedString("C\uffff}>");

		// Token: 0x040002B7 RID: 695
		private static readonly short[][] DFA14_transition = DFA.UnpackEncodedStringArray(NCalcLexer.DFA14_transitionS);

		// Token: 0x020001EB RID: 491
		protected class DFA7 : DFA
		{
			// Token: 0x06000C91 RID: 3217 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
			public DFA7(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 7;
				this.eot = NCalcLexer.DFA7_eot;
				this.eof = NCalcLexer.DFA7_eof;
				this.min = NCalcLexer.DFA7_min;
				this.max = NCalcLexer.DFA7_max;
				this.accept = NCalcLexer.DFA7_accept;
				this.special = NCalcLexer.DFA7_special;
				this.transition = NCalcLexer.DFA7_transition;
			}

			// Token: 0x170003E7 RID: 999
			// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0001C54E File Offset: 0x0001A74E
			public override string Description
			{
				get
				{
					return "252:1: FLOAT : ( ( DIGIT )* '.' ( DIGIT )+ ( E )? | ( DIGIT )+ E );";
				}
			}
		}

		// Token: 0x020001EC RID: 492
		protected class DFA14 : DFA
		{
			// Token: 0x06000C93 RID: 3219 RVA: 0x0001C558 File Offset: 0x0001A758
			public DFA14(BaseRecognizer recognizer)
			{
				this.recognizer = recognizer;
				this.decisionNumber = 14;
				this.eot = NCalcLexer.DFA14_eot;
				this.eof = NCalcLexer.DFA14_eof;
				this.min = NCalcLexer.DFA14_min;
				this.max = NCalcLexer.DFA14_max;
				this.accept = NCalcLexer.DFA14_accept;
				this.special = NCalcLexer.DFA14_special;
				this.transition = NCalcLexer.DFA14_transition;
			}

			// Token: 0x170003E8 RID: 1000
			// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0001C5C7 File Offset: 0x0001A7C7
			public override string Description
			{
				get
				{
					return "1:1: Tokens : ( T__19 | T__20 | T__21 | T__22 | T__23 | T__24 | T__25 | T__26 | T__27 | T__28 | T__29 | T__30 | T__31 | T__32 | T__33 | T__34 | T__35 | T__36 | T__37 | T__38 | T__39 | T__40 | T__41 | T__42 | T__43 | T__44 | T__45 | T__46 | T__47 | T__48 | TRUE | FALSE | ID | INTEGER | FLOAT | STRING | DATETIME | NAME | E | WS );";
				}
			}
		}
	}
}
