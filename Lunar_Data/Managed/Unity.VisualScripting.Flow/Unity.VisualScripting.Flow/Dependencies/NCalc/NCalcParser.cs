using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000196 RID: 406
	public class NCalcParser : Parser
	{
		// Token: 0x06000B27 RID: 2855 RVA: 0x00016110 File Offset: 0x00014310
		public NCalcParser(ITokenStream input)
			: this(input, new RecognizerSharedState())
		{
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001611E File Offset: 0x0001431E
		public NCalcParser(ITokenStream input, RecognizerSharedState state)
			: base(input, state)
		{
			this.InitializeCyclicDFAs();
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00016139 File Offset: 0x00014339
		// (set) Token: 0x06000B2A RID: 2858 RVA: 0x00016141 File Offset: 0x00014341
		public ITreeAdaptor TreeAdaptor
		{
			get
			{
				return this.adaptor;
			}
			set
			{
				this.adaptor = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x0001614A File Offset: 0x0001434A
		public override string[] TokenNames
		{
			get
			{
				return NCalcParser.tokenNames;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00016151 File Offset: 0x00014351
		public override string GrammarFileName
		{
			get
			{
				return "C:\\Users\\s.ros\\Documents\\D\ufffdveloppement\\NCalc\\Grammar\\NCalc.g";
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00016158 File Offset: 0x00014358
		// (set) Token: 0x06000B2E RID: 2862 RVA: 0x00016160 File Offset: 0x00014360
		public List<string> Errors { get; private set; }

		// Token: 0x06000B2F RID: 2863 RVA: 0x00016169 File Offset: 0x00014369
		private void InitializeCyclicDFAs()
		{
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001616C File Offset: 0x0001436C
		private string extractString(string text)
		{
			StringBuilder stringBuilder = new StringBuilder(text);
			int num = 1;
			int num2;
			while ((num2 = stringBuilder.ToString().IndexOf('\\', num)) != -1)
			{
				char c = stringBuilder[num2 + 1];
				if (c <= '\\')
				{
					if (c != '\'')
					{
						if (c != '\\')
						{
							goto IL_013E;
						}
						stringBuilder.Remove(num2, 2).Insert(num2, '\\');
					}
					else
					{
						stringBuilder.Remove(num2, 2).Insert(num2, '\'');
					}
				}
				else if (c != 'n')
				{
					switch (c)
					{
					case 'r':
						stringBuilder.Remove(num2, 2).Insert(num2, '\r');
						break;
					case 's':
						goto IL_013E;
					case 't':
						stringBuilder.Remove(num2, 2).Insert(num2, '\t');
						break;
					case 'u':
					{
						string text2 = stringBuilder[num2 + 4] + stringBuilder[num2 + 5];
						string text3 = stringBuilder[num2 + 2] + stringBuilder[num2 + 3];
						char c2 = Encoding.Unicode.GetChars(new byte[]
						{
							Convert.ToByte(text2, 16),
							Convert.ToByte(text3, 16)
						})[0];
						stringBuilder.Remove(num2, 6).Insert(num2, c2);
						break;
					}
					default:
						goto IL_013E;
					}
				}
				else
				{
					stringBuilder.Remove(num2, 2).Insert(num2, '\n');
				}
				num = num2 + 1;
				continue;
				IL_013E:
				throw new RecognitionException("Unvalid escape sequence: \\" + c.ToString());
			}
			stringBuilder.Remove(0, 1);
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00016308 File Offset: 0x00014508
		public override void DisplayRecognitionError(string[] tokenNames, RecognitionException e)
		{
			base.DisplayRecognitionError(tokenNames, e);
			if (this.Errors == null)
			{
				this.Errors = new List<string>();
			}
			string errorHeader = this.GetErrorHeader(e);
			string errorMessage = this.GetErrorMessage(e, tokenNames);
			this.Errors.Add(errorMessage + " at " + errorHeader);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00016358 File Offset: 0x00014558
		public NCalcParser.ncalcExpression_return ncalcExpression()
		{
			NCalcParser.ncalcExpression_return ncalcExpression_return = new NCalcParser.ncalcExpression_return();
			ncalcExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_logicalExpression_in_ncalcExpression56);
				NCalcParser.logicalExpression_return logicalExpression_return = this.logicalExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, logicalExpression_return.Tree);
				IToken token = (IToken)this.Match(this.input, -1, NCalcParser.FOLLOW_EOF_in_ncalcExpression58);
				ncalcExpression_return.value = ((logicalExpression_return != null) ? logicalExpression_return.value : null);
				ncalcExpression_return.Stop = this.input.LT(-1);
				ncalcExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(ncalcExpression_return.Tree, (IToken)ncalcExpression_return.Start, (IToken)ncalcExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				ncalcExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)ncalcExpression_return.Start, this.input.LT(-1), ex);
			}
			return ncalcExpression_return;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000164A4 File Offset: 0x000146A4
		public NCalcParser.logicalExpression_return logicalExpression()
		{
			NCalcParser.logicalExpression_return logicalExpression_return = new NCalcParser.logicalExpression_return();
			logicalExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_conditionalExpression_in_logicalExpression78);
				NCalcParser.conditionalExpression_return conditionalExpression_return = this.conditionalExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, conditionalExpression_return.Tree);
				logicalExpression_return.value = ((conditionalExpression_return != null) ? conditionalExpression_return.value : null);
				int num = 2;
				if (this.input.LA(1) == 19)
				{
					num = 1;
				}
				if (num == 1)
				{
					IToken token = (IToken)this.Match(this.input, 19, NCalcParser.FOLLOW_19_in_logicalExpression84);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					base.PushFollow(NCalcParser.FOLLOW_conditionalExpression_in_logicalExpression88);
					NCalcParser.conditionalExpression_return conditionalExpression_return2 = this.conditionalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, conditionalExpression_return2.Tree);
					IToken token2 = (IToken)this.Match(this.input, 20, NCalcParser.FOLLOW_20_in_logicalExpression90);
					CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
					this.adaptor.AddChild(commonTree, commonTree3);
					base.PushFollow(NCalcParser.FOLLOW_conditionalExpression_in_logicalExpression94);
					NCalcParser.conditionalExpression_return conditionalExpression_return3 = this.conditionalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, conditionalExpression_return3.Tree);
					logicalExpression_return.value = new TernaryExpression((conditionalExpression_return != null) ? conditionalExpression_return.value : null, (conditionalExpression_return2 != null) ? conditionalExpression_return2.value : null, (conditionalExpression_return3 != null) ? conditionalExpression_return3.value : null);
				}
				logicalExpression_return.Stop = this.input.LT(-1);
				logicalExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(logicalExpression_return.Tree, (IToken)logicalExpression_return.Start, (IToken)logicalExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				logicalExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)logicalExpression_return.Start, this.input.LT(-1), ex);
			}
			return logicalExpression_return;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00016734 File Offset: 0x00014934
		public NCalcParser.conditionalExpression_return conditionalExpression()
		{
			NCalcParser.conditionalExpression_return conditionalExpression_return = new NCalcParser.conditionalExpression_return();
			conditionalExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_booleanAndExpression_in_conditionalExpression121);
				NCalcParser.booleanAndExpression_return booleanAndExpression_return = this.booleanAndExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, booleanAndExpression_return.Tree);
				conditionalExpression_return.value = ((booleanAndExpression_return != null) ? booleanAndExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 21 && num2 <= 22)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_0179;
					}
					IToken token = this.input.LT(1);
					if (this.input.LA(1) < 21 || this.input.LA(1) > 22)
					{
						break;
					}
					this.input.Consume();
					this.adaptor.AddChild(commonTree, (CommonTree)this.adaptor.Create(token));
					this.state.errorRecovery = false;
					BinaryExpressionType binaryExpressionType = BinaryExpressionType.Or;
					base.PushFollow(NCalcParser.FOLLOW_conditionalExpression_in_conditionalExpression146);
					NCalcParser.conditionalExpression_return conditionalExpression_return2 = this.conditionalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, conditionalExpression_return2.Tree);
					conditionalExpression_return.value = new BinaryExpression(binaryExpressionType, conditionalExpression_return.value, (conditionalExpression_return2 != null) ? conditionalExpression_return2.value : null);
				}
				throw new MismatchedSetException(null, this.input);
				IL_0179:
				conditionalExpression_return.Stop = this.input.LT(-1);
				conditionalExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(conditionalExpression_return.Tree, (IToken)conditionalExpression_return.Start, (IToken)conditionalExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				conditionalExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)conditionalExpression_return.Start, this.input.LT(-1), ex);
			}
			return conditionalExpression_return;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00016978 File Offset: 0x00014B78
		public NCalcParser.booleanAndExpression_return booleanAndExpression()
		{
			NCalcParser.booleanAndExpression_return booleanAndExpression_return = new NCalcParser.booleanAndExpression_return();
			booleanAndExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_bitwiseOrExpression_in_booleanAndExpression180);
				NCalcParser.bitwiseOrExpression_return bitwiseOrExpression_return = this.bitwiseOrExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, bitwiseOrExpression_return.Tree);
				booleanAndExpression_return.value = ((bitwiseOrExpression_return != null) ? bitwiseOrExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 23 && num2 <= 24)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_0179;
					}
					IToken token = this.input.LT(1);
					if (this.input.LA(1) < 23 || this.input.LA(1) > 24)
					{
						break;
					}
					this.input.Consume();
					this.adaptor.AddChild(commonTree, (CommonTree)this.adaptor.Create(token));
					this.state.errorRecovery = false;
					BinaryExpressionType binaryExpressionType = BinaryExpressionType.And;
					base.PushFollow(NCalcParser.FOLLOW_bitwiseOrExpression_in_booleanAndExpression205);
					NCalcParser.bitwiseOrExpression_return bitwiseOrExpression_return2 = this.bitwiseOrExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, bitwiseOrExpression_return2.Tree);
					booleanAndExpression_return.value = new BinaryExpression(binaryExpressionType, booleanAndExpression_return.value, (bitwiseOrExpression_return2 != null) ? bitwiseOrExpression_return2.value : null);
				}
				throw new MismatchedSetException(null, this.input);
				IL_0179:
				booleanAndExpression_return.Stop = this.input.LT(-1);
				booleanAndExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(booleanAndExpression_return.Tree, (IToken)booleanAndExpression_return.Start, (IToken)booleanAndExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				booleanAndExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)booleanAndExpression_return.Start, this.input.LT(-1), ex);
			}
			return booleanAndExpression_return;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00016BBC File Offset: 0x00014DBC
		public NCalcParser.bitwiseOrExpression_return bitwiseOrExpression()
		{
			NCalcParser.bitwiseOrExpression_return bitwiseOrExpression_return = new NCalcParser.bitwiseOrExpression_return();
			bitwiseOrExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_bitwiseXOrExpression_in_bitwiseOrExpression237);
				NCalcParser.bitwiseXOrExpression_return bitwiseXOrExpression_return = this.bitwiseXOrExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, bitwiseXOrExpression_return.Tree);
				bitwiseOrExpression_return.value = ((bitwiseXOrExpression_return != null) ? bitwiseXOrExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					if (this.input.LA(1) == 25)
					{
						num = 1;
					}
					if (num != 1)
					{
						break;
					}
					IToken token = (IToken)this.Match(this.input, 25, NCalcParser.FOLLOW_25_in_bitwiseOrExpression246);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					BinaryExpressionType binaryExpressionType = BinaryExpressionType.BitwiseOr;
					base.PushFollow(NCalcParser.FOLLOW_bitwiseOrExpression_in_bitwiseOrExpression256);
					NCalcParser.bitwiseOrExpression_return bitwiseOrExpression_return2 = this.bitwiseOrExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, bitwiseOrExpression_return2.Tree);
					bitwiseOrExpression_return.value = new BinaryExpression(binaryExpressionType, bitwiseOrExpression_return.value, (bitwiseOrExpression_return2 != null) ? bitwiseOrExpression_return2.value : null);
				}
				bitwiseOrExpression_return.Stop = this.input.LT(-1);
				bitwiseOrExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(bitwiseOrExpression_return.Tree, (IToken)bitwiseOrExpression_return.Start, (IToken)bitwiseOrExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				bitwiseOrExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)bitwiseOrExpression_return.Start, this.input.LT(-1), ex);
			}
			return bitwiseOrExpression_return;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00016DC4 File Offset: 0x00014FC4
		public NCalcParser.bitwiseXOrExpression_return bitwiseXOrExpression()
		{
			NCalcParser.bitwiseXOrExpression_return bitwiseXOrExpression_return = new NCalcParser.bitwiseXOrExpression_return();
			bitwiseXOrExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_bitwiseAndExpression_in_bitwiseXOrExpression290);
				NCalcParser.bitwiseAndExpression_return bitwiseAndExpression_return = this.bitwiseAndExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, bitwiseAndExpression_return.Tree);
				bitwiseXOrExpression_return.value = ((bitwiseAndExpression_return != null) ? bitwiseAndExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					if (this.input.LA(1) == 26)
					{
						num = 1;
					}
					if (num != 1)
					{
						break;
					}
					IToken token = (IToken)this.Match(this.input, 26, NCalcParser.FOLLOW_26_in_bitwiseXOrExpression299);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					BinaryExpressionType binaryExpressionType = BinaryExpressionType.BitwiseXOr;
					base.PushFollow(NCalcParser.FOLLOW_bitwiseAndExpression_in_bitwiseXOrExpression309);
					NCalcParser.bitwiseAndExpression_return bitwiseAndExpression_return2 = this.bitwiseAndExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, bitwiseAndExpression_return2.Tree);
					bitwiseXOrExpression_return.value = new BinaryExpression(binaryExpressionType, bitwiseXOrExpression_return.value, (bitwiseAndExpression_return2 != null) ? bitwiseAndExpression_return2.value : null);
				}
				bitwiseXOrExpression_return.Stop = this.input.LT(-1);
				bitwiseXOrExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(bitwiseXOrExpression_return.Tree, (IToken)bitwiseXOrExpression_return.Start, (IToken)bitwiseXOrExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				bitwiseXOrExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)bitwiseXOrExpression_return.Start, this.input.LT(-1), ex);
			}
			return bitwiseXOrExpression_return;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00016FCC File Offset: 0x000151CC
		public NCalcParser.bitwiseAndExpression_return bitwiseAndExpression()
		{
			NCalcParser.bitwiseAndExpression_return bitwiseAndExpression_return = new NCalcParser.bitwiseAndExpression_return();
			bitwiseAndExpression_return.Start = this.input.LT(1);
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_equalityExpression_in_bitwiseAndExpression341);
				NCalcParser.equalityExpression_return equalityExpression_return = this.equalityExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, equalityExpression_return.Tree);
				bitwiseAndExpression_return.value = ((equalityExpression_return != null) ? equalityExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					if (this.input.LA(1) == 27)
					{
						num = 1;
					}
					if (num != 1)
					{
						break;
					}
					IToken token = (IToken)this.Match(this.input, 27, NCalcParser.FOLLOW_27_in_bitwiseAndExpression350);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					BinaryExpressionType binaryExpressionType = BinaryExpressionType.BitwiseAnd;
					base.PushFollow(NCalcParser.FOLLOW_equalityExpression_in_bitwiseAndExpression360);
					NCalcParser.equalityExpression_return equalityExpression_return2 = this.equalityExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, equalityExpression_return2.Tree);
					bitwiseAndExpression_return.value = new BinaryExpression(binaryExpressionType, bitwiseAndExpression_return.value, (equalityExpression_return2 != null) ? equalityExpression_return2.value : null);
				}
				bitwiseAndExpression_return.Stop = this.input.LT(-1);
				bitwiseAndExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(bitwiseAndExpression_return.Tree, (IToken)bitwiseAndExpression_return.Start, (IToken)bitwiseAndExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				bitwiseAndExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)bitwiseAndExpression_return.Start, this.input.LT(-1), ex);
			}
			return bitwiseAndExpression_return;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000171D4 File Offset: 0x000153D4
		public NCalcParser.equalityExpression_return equalityExpression()
		{
			NCalcParser.equalityExpression_return equalityExpression_return = new NCalcParser.equalityExpression_return();
			equalityExpression_return.Start = this.input.LT(1);
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Unknown;
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_relationalExpression_in_equalityExpression394);
				NCalcParser.relationalExpression_return relationalExpression_return = this.relationalExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, relationalExpression_return.Tree);
				equalityExpression_return.value = ((relationalExpression_return != null) ? relationalExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 28 && num2 <= 31)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_024A;
					}
					int num3 = this.input.LA(1);
					int num4;
					if (num3 >= 28 && num3 <= 29)
					{
						num4 = 1;
					}
					else
					{
						if (num3 < 30 || num3 > 31)
						{
							break;
						}
						num4 = 2;
					}
					if (num4 != 1)
					{
						if (num4 == 2)
						{
							IToken token = this.input.LT(1);
							if (this.input.LA(1) < 30 || this.input.LA(1) > 31)
							{
								goto IL_01DB;
							}
							this.input.Consume();
							this.adaptor.AddChild(commonTree, (CommonTree)this.adaptor.Create(token));
							this.state.errorRecovery = false;
							binaryExpressionType = BinaryExpressionType.NotEqual;
						}
					}
					else
					{
						IToken token2 = this.input.LT(1);
						if (this.input.LA(1) < 28 || this.input.LA(1) > 29)
						{
							goto IL_0166;
						}
						this.input.Consume();
						this.adaptor.AddChild(commonTree, (CommonTree)this.adaptor.Create(token2));
						this.state.errorRecovery = false;
						binaryExpressionType = BinaryExpressionType.Equal;
					}
					base.PushFollow(NCalcParser.FOLLOW_relationalExpression_in_equalityExpression441);
					NCalcParser.relationalExpression_return relationalExpression_return2 = this.relationalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, relationalExpression_return2.Tree);
					equalityExpression_return.value = new BinaryExpression(binaryExpressionType, equalityExpression_return.value, (relationalExpression_return2 != null) ? relationalExpression_return2.value : null);
				}
				throw new NoViableAltException("", 7, 0, this.input);
				IL_0166:
				throw new MismatchedSetException(null, this.input);
				IL_01DB:
				throw new MismatchedSetException(null, this.input);
				IL_024A:
				equalityExpression_return.Stop = this.input.LT(-1);
				equalityExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(equalityExpression_return.Tree, (IToken)equalityExpression_return.Start, (IToken)equalityExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				equalityExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)equalityExpression_return.Start, this.input.LT(-1), ex);
			}
			return equalityExpression_return;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000174EC File Offset: 0x000156EC
		public NCalcParser.relationalExpression_return relationalExpression()
		{
			NCalcParser.relationalExpression_return relationalExpression_return = new NCalcParser.relationalExpression_return();
			relationalExpression_return.Start = this.input.LT(1);
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Unknown;
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_shiftExpression_in_relationalExpression474);
				NCalcParser.shiftExpression_return shiftExpression_return = this.shiftExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, shiftExpression_return.Tree);
				relationalExpression_return.value = ((shiftExpression_return != null) ? shiftExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 32 && num2 <= 35)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_0296;
					}
					int num3;
					switch (this.input.LA(1))
					{
					case 32:
						num3 = 1;
						goto IL_0115;
					case 33:
						num3 = 2;
						goto IL_0115;
					case 34:
						num3 = 3;
						goto IL_0115;
					case 35:
						num3 = 4;
						goto IL_0115;
					}
					break;
					IL_0115:
					switch (num3)
					{
					case 1:
					{
						IToken token = (IToken)this.Match(this.input, 32, NCalcParser.FOLLOW_32_in_relationalExpression485);
						CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
						this.adaptor.AddChild(commonTree, commonTree2);
						binaryExpressionType = BinaryExpressionType.Lesser;
						break;
					}
					case 2:
					{
						IToken token2 = (IToken)this.Match(this.input, 33, NCalcParser.FOLLOW_33_in_relationalExpression495);
						CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
						this.adaptor.AddChild(commonTree, commonTree3);
						binaryExpressionType = BinaryExpressionType.LesserOrEqual;
						break;
					}
					case 3:
					{
						IToken token3 = (IToken)this.Match(this.input, 34, NCalcParser.FOLLOW_34_in_relationalExpression506);
						CommonTree commonTree4 = (CommonTree)this.adaptor.Create(token3);
						this.adaptor.AddChild(commonTree, commonTree4);
						binaryExpressionType = BinaryExpressionType.Greater;
						break;
					}
					case 4:
					{
						IToken token4 = (IToken)this.Match(this.input, 35, NCalcParser.FOLLOW_35_in_relationalExpression516);
						CommonTree commonTree5 = (CommonTree)this.adaptor.Create(token4);
						this.adaptor.AddChild(commonTree, commonTree5);
						binaryExpressionType = BinaryExpressionType.GreaterOrEqual;
						break;
					}
					}
					base.PushFollow(NCalcParser.FOLLOW_shiftExpression_in_relationalExpression528);
					NCalcParser.shiftExpression_return shiftExpression_return2 = this.shiftExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, shiftExpression_return2.Tree);
					relationalExpression_return.value = new BinaryExpression(binaryExpressionType, relationalExpression_return.value, (shiftExpression_return2 != null) ? shiftExpression_return2.value : null);
				}
				throw new NoViableAltException("", 9, 0, this.input);
				IL_0296:
				relationalExpression_return.Stop = this.input.LT(-1);
				relationalExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(relationalExpression_return.Tree, (IToken)relationalExpression_return.Start, (IToken)relationalExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				relationalExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)relationalExpression_return.Start, this.input.LT(-1), ex);
			}
			return relationalExpression_return;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00017850 File Offset: 0x00015A50
		public NCalcParser.shiftExpression_return shiftExpression()
		{
			NCalcParser.shiftExpression_return shiftExpression_return = new NCalcParser.shiftExpression_return();
			shiftExpression_return.Start = this.input.LT(1);
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Unknown;
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_additiveExpression_in_shiftExpression560);
				NCalcParser.additiveExpression_return additiveExpression_return = this.additiveExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, additiveExpression_return.Tree);
				shiftExpression_return.value = ((additiveExpression_return != null) ? additiveExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 36 && num2 <= 37)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_01D8;
					}
					int num3 = this.input.LA(1);
					int num4;
					if (num3 == 36)
					{
						num4 = 1;
					}
					else
					{
						if (num3 != 37)
						{
							break;
						}
						num4 = 2;
					}
					if (num4 != 1)
					{
						if (num4 == 2)
						{
							IToken token = (IToken)this.Match(this.input, 37, NCalcParser.FOLLOW_37_in_shiftExpression581);
							CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
							this.adaptor.AddChild(commonTree, commonTree2);
							binaryExpressionType = BinaryExpressionType.RightShift;
						}
					}
					else
					{
						IToken token2 = (IToken)this.Match(this.input, 36, NCalcParser.FOLLOW_36_in_shiftExpression571);
						CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
						this.adaptor.AddChild(commonTree, commonTree3);
						binaryExpressionType = BinaryExpressionType.LeftShift;
					}
					base.PushFollow(NCalcParser.FOLLOW_additiveExpression_in_shiftExpression593);
					NCalcParser.additiveExpression_return additiveExpression_return2 = this.additiveExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, additiveExpression_return2.Tree);
					shiftExpression_return.value = new BinaryExpression(binaryExpressionType, shiftExpression_return.value, (additiveExpression_return2 != null) ? additiveExpression_return2.value : null);
				}
				throw new NoViableAltException("", 11, 0, this.input);
				IL_01D8:
				shiftExpression_return.Stop = this.input.LT(-1);
				shiftExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(shiftExpression_return.Tree, (IToken)shiftExpression_return.Start, (IToken)shiftExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				shiftExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)shiftExpression_return.Start, this.input.LT(-1), ex);
			}
			return shiftExpression_return;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00017AF4 File Offset: 0x00015CF4
		public NCalcParser.additiveExpression_return additiveExpression()
		{
			NCalcParser.additiveExpression_return additiveExpression_return = new NCalcParser.additiveExpression_return();
			additiveExpression_return.Start = this.input.LT(1);
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Unknown;
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_multiplicativeExpression_in_additiveExpression625);
				NCalcParser.multiplicativeExpression_return multiplicativeExpression_return = this.multiplicativeExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, multiplicativeExpression_return.Tree);
				additiveExpression_return.value = ((multiplicativeExpression_return != null) ? multiplicativeExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 38 && num2 <= 39)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_01D7;
					}
					int num3 = this.input.LA(1);
					int num4;
					if (num3 == 38)
					{
						num4 = 1;
					}
					else
					{
						if (num3 != 39)
						{
							break;
						}
						num4 = 2;
					}
					if (num4 != 1)
					{
						if (num4 == 2)
						{
							IToken token = (IToken)this.Match(this.input, 39, NCalcParser.FOLLOW_39_in_additiveExpression646);
							CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
							this.adaptor.AddChild(commonTree, commonTree2);
							binaryExpressionType = BinaryExpressionType.Minus;
						}
					}
					else
					{
						IToken token2 = (IToken)this.Match(this.input, 38, NCalcParser.FOLLOW_38_in_additiveExpression636);
						CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
						this.adaptor.AddChild(commonTree, commonTree3);
						binaryExpressionType = BinaryExpressionType.Plus;
					}
					base.PushFollow(NCalcParser.FOLLOW_multiplicativeExpression_in_additiveExpression658);
					NCalcParser.multiplicativeExpression_return multiplicativeExpression_return2 = this.multiplicativeExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, multiplicativeExpression_return2.Tree);
					additiveExpression_return.value = new BinaryExpression(binaryExpressionType, additiveExpression_return.value, (multiplicativeExpression_return2 != null) ? multiplicativeExpression_return2.value : null);
				}
				throw new NoViableAltException("", 13, 0, this.input);
				IL_01D7:
				additiveExpression_return.Stop = this.input.LT(-1);
				additiveExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(additiveExpression_return.Tree, (IToken)additiveExpression_return.Start, (IToken)additiveExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				additiveExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)additiveExpression_return.Start, this.input.LT(-1), ex);
			}
			return additiveExpression_return;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00017D98 File Offset: 0x00015F98
		public NCalcParser.multiplicativeExpression_return multiplicativeExpression()
		{
			NCalcParser.multiplicativeExpression_return multiplicativeExpression_return = new NCalcParser.multiplicativeExpression_return();
			multiplicativeExpression_return.Start = this.input.LT(1);
			BinaryExpressionType binaryExpressionType = BinaryExpressionType.Unknown;
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_unaryExpression_in_multiplicativeExpression690);
				NCalcParser.unaryExpression_return unaryExpression_return = this.unaryExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, unaryExpression_return.Tree);
				multiplicativeExpression_return.value = ((unaryExpression_return != null) ? unaryExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					int num2 = this.input.LA(1);
					if (num2 >= 40 && num2 <= 42)
					{
						num = 1;
					}
					if (num != 1)
					{
						goto IL_0242;
					}
					int num3;
					switch (this.input.LA(1))
					{
					case 40:
						num3 = 1;
						goto IL_0106;
					case 41:
						num3 = 2;
						goto IL_0106;
					case 42:
						num3 = 3;
						goto IL_0106;
					}
					break;
					IL_0106:
					switch (num3)
					{
					case 1:
					{
						IToken token = (IToken)this.Match(this.input, 40, NCalcParser.FOLLOW_40_in_multiplicativeExpression701);
						CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
						this.adaptor.AddChild(commonTree, commonTree2);
						binaryExpressionType = BinaryExpressionType.Times;
						break;
					}
					case 2:
					{
						IToken token2 = (IToken)this.Match(this.input, 41, NCalcParser.FOLLOW_41_in_multiplicativeExpression711);
						CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
						this.adaptor.AddChild(commonTree, commonTree3);
						binaryExpressionType = BinaryExpressionType.Div;
						break;
					}
					case 3:
					{
						IToken token3 = (IToken)this.Match(this.input, 42, NCalcParser.FOLLOW_42_in_multiplicativeExpression721);
						CommonTree commonTree4 = (CommonTree)this.adaptor.Create(token3);
						this.adaptor.AddChild(commonTree, commonTree4);
						binaryExpressionType = BinaryExpressionType.Modulo;
						break;
					}
					}
					base.PushFollow(NCalcParser.FOLLOW_unaryExpression_in_multiplicativeExpression733);
					NCalcParser.unaryExpression_return unaryExpression_return2 = this.unaryExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, unaryExpression_return2.Tree);
					multiplicativeExpression_return.value = new BinaryExpression(binaryExpressionType, multiplicativeExpression_return.value, (unaryExpression_return2 != null) ? unaryExpression_return2.value : null);
				}
				throw new NoViableAltException("", 15, 0, this.input);
				IL_0242:
				multiplicativeExpression_return.Stop = this.input.LT(-1);
				multiplicativeExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(multiplicativeExpression_return.Tree, (IToken)multiplicativeExpression_return.Start, (IToken)multiplicativeExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				multiplicativeExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)multiplicativeExpression_return.Start, this.input.LT(-1), ex);
			}
			return multiplicativeExpression_return;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000180A8 File Offset: 0x000162A8
		public NCalcParser.unaryExpression_return unaryExpression()
		{
			NCalcParser.unaryExpression_return unaryExpression_return = new NCalcParser.unaryExpression_return();
			unaryExpression_return.Start = this.input.LT(1);
			CommonTree commonTree = null;
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num - 4 > 7)
				{
					switch (num)
					{
					case 39:
						num2 = 4;
						goto IL_009F;
					case 43:
					case 44:
						num2 = 2;
						goto IL_009F;
					case 45:
						num2 = 3;
						goto IL_009F;
					case 46:
						goto IL_0077;
					}
					throw new NoViableAltException("", 17, 0, this.input);
				}
				IL_0077:
				num2 = 1;
				IL_009F:
				switch (num2)
				{
				case 1:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					base.PushFollow(NCalcParser.FOLLOW_primaryExpression_in_unaryExpression760);
					NCalcParser.primaryExpression_return primaryExpression_return = this.primaryExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, primaryExpression_return.Tree);
					unaryExpression_return.value = ((primaryExpression_return != null) ? primaryExpression_return.value : null);
					break;
				}
				case 2:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token = this.input.LT(1);
					if (this.input.LA(1) < 43 || this.input.LA(1) > 44)
					{
						throw new MismatchedSetException(null, this.input);
					}
					this.input.Consume();
					this.adaptor.AddChild(commonTree, (CommonTree)this.adaptor.Create(token));
					this.state.errorRecovery = false;
					base.PushFollow(NCalcParser.FOLLOW_primaryExpression_in_unaryExpression779);
					NCalcParser.primaryExpression_return primaryExpression_return2 = this.primaryExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, primaryExpression_return2.Tree);
					unaryExpression_return.value = new UnaryExpression(UnaryExpressionType.Not, (primaryExpression_return2 != null) ? primaryExpression_return2.value : null);
					break;
				}
				case 3:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token2 = (IToken)this.Match(this.input, 45, NCalcParser.FOLLOW_45_in_unaryExpression791);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token2);
					this.adaptor.AddChild(commonTree, commonTree2);
					base.PushFollow(NCalcParser.FOLLOW_primaryExpression_in_unaryExpression794);
					NCalcParser.primaryExpression_return primaryExpression_return3 = this.primaryExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, primaryExpression_return3.Tree);
					unaryExpression_return.value = new UnaryExpression(UnaryExpressionType.BitwiseNot, (primaryExpression_return3 != null) ? primaryExpression_return3.value : null);
					break;
				}
				case 4:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token3 = (IToken)this.Match(this.input, 39, NCalcParser.FOLLOW_39_in_unaryExpression805);
					CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token3);
					this.adaptor.AddChild(commonTree, commonTree3);
					base.PushFollow(NCalcParser.FOLLOW_primaryExpression_in_unaryExpression807);
					NCalcParser.primaryExpression_return primaryExpression_return4 = this.primaryExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, primaryExpression_return4.Tree);
					unaryExpression_return.value = new UnaryExpression(UnaryExpressionType.Negate, (primaryExpression_return4 != null) ? primaryExpression_return4.value : null);
					break;
				}
				}
				unaryExpression_return.Stop = this.input.LT(-1);
				unaryExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(unaryExpression_return.Tree, (IToken)unaryExpression_return.Start, (IToken)unaryExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				unaryExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)unaryExpression_return.Start, this.input.LT(-1), ex);
			}
			return unaryExpression_return;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000184B0 File Offset: 0x000166B0
		public NCalcParser.primaryExpression_return primaryExpression()
		{
			NCalcParser.primaryExpression_return primaryExpression_return = new NCalcParser.primaryExpression_return();
			primaryExpression_return.Start = this.input.LT(1);
			CommonTree commonTree = null;
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num - 4 > 5)
				{
					if (num - 10 > 1)
					{
						if (num != 46)
						{
							throw new NoViableAltException("", 19, 0, this.input);
						}
						num2 = 1;
					}
					else
					{
						num2 = 3;
					}
				}
				else
				{
					num2 = 2;
				}
				switch (num2)
				{
				case 1:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token = (IToken)this.Match(this.input, 46, NCalcParser.FOLLOW_46_in_primaryExpression829);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					base.PushFollow(NCalcParser.FOLLOW_logicalExpression_in_primaryExpression831);
					NCalcParser.logicalExpression_return logicalExpression_return = this.logicalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, logicalExpression_return.Tree);
					IToken token2 = (IToken)this.Match(this.input, 47, NCalcParser.FOLLOW_47_in_primaryExpression833);
					CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
					this.adaptor.AddChild(commonTree, commonTree3);
					primaryExpression_return.value = ((logicalExpression_return != null) ? logicalExpression_return.value : null);
					break;
				}
				case 2:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					base.PushFollow(NCalcParser.FOLLOW_value_in_primaryExpression843);
					NCalcParser.value_return value_return = this.value();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, value_return.Tree);
					primaryExpression_return.value = ((value_return != null) ? value_return.value : null);
					break;
				}
				case 3:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					base.PushFollow(NCalcParser.FOLLOW_identifier_in_primaryExpression851);
					NCalcParser.identifier_return identifier_return = this.identifier();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, identifier_return.Tree);
					primaryExpression_return.value = ((identifier_return != null) ? identifier_return.value : null);
					int num3 = 2;
					if (this.input.LA(1) == 46)
					{
						num3 = 1;
					}
					if (num3 == 1)
					{
						base.PushFollow(NCalcParser.FOLLOW_arguments_in_primaryExpression856);
						NCalcParser.arguments_return arguments_return = this.arguments();
						this.state.followingStackPointer--;
						this.adaptor.AddChild(commonTree, arguments_return.Tree);
						primaryExpression_return.value = new FunctionExpression((identifier_return != null) ? identifier_return.value : null, ((arguments_return != null) ? arguments_return.value : null).ToArray());
					}
					break;
				}
				}
				primaryExpression_return.Stop = this.input.LT(-1);
				primaryExpression_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(primaryExpression_return.Tree, (IToken)primaryExpression_return.Start, (IToken)primaryExpression_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				primaryExpression_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)primaryExpression_return.Start, this.input.LT(-1), ex);
			}
			return primaryExpression_return;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00018828 File Offset: 0x00016A28
		public NCalcParser.value_return value()
		{
			NCalcParser.value_return value_return = new NCalcParser.value_return();
			value_return.Start = this.input.LT(1);
			CommonTree commonTree = null;
			IToken token = null;
			try
			{
				int num;
				switch (this.input.LA(1))
				{
				case 4:
					num = 1;
					break;
				case 5:
					num = 2;
					break;
				case 6:
					num = 3;
					break;
				case 7:
					num = 4;
					break;
				case 8:
					num = 5;
					break;
				case 9:
					num = 6;
					break;
				default:
					throw new NoViableAltException("", 20, 0, this.input);
				}
				switch (num)
				{
				case 1:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					token = (IToken)this.Match(this.input, 4, NCalcParser.FOLLOW_INTEGER_in_value876);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					try
					{
						value_return.value = new ValueExpression(int.Parse((token != null) ? token.Text : null));
						goto IL_037D;
					}
					catch (OverflowException)
					{
						value_return.value = new ValueExpression((float)long.Parse((token != null) ? token.Text : null));
						goto IL_037D;
					}
					break;
				}
				case 2:
					break;
				case 3:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token2 = (IToken)this.Match(this.input, 6, NCalcParser.FOLLOW_STRING_in_value892);
					CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
					this.adaptor.AddChild(commonTree, commonTree3);
					value_return.value = new ValueExpression(this.extractString((token2 != null) ? token2.Text : null));
					goto IL_037D;
				}
				case 4:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token3 = (IToken)this.Match(this.input, 7, NCalcParser.FOLLOW_DATETIME_in_value901);
					CommonTree commonTree4 = (CommonTree)this.adaptor.Create(token3);
					this.adaptor.AddChild(commonTree, commonTree4);
					value_return.value = new ValueExpression(DateTime.Parse(((token3 != null) ? token3.Text : null).Substring(1, ((token3 != null) ? token3.Text : null).Length - 2)));
					goto IL_037D;
				}
				case 5:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token4 = (IToken)this.Match(this.input, 8, NCalcParser.FOLLOW_TRUE_in_value908);
					CommonTree commonTree5 = (CommonTree)this.adaptor.Create(token4);
					this.adaptor.AddChild(commonTree, commonTree5);
					value_return.value = new ValueExpression(true);
					goto IL_037D;
				}
				case 6:
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token5 = (IToken)this.Match(this.input, 9, NCalcParser.FOLLOW_FALSE_in_value916);
					CommonTree commonTree6 = (CommonTree)this.adaptor.Create(token5);
					this.adaptor.AddChild(commonTree, commonTree6);
					value_return.value = new ValueExpression(false);
					goto IL_037D;
				}
				default:
					goto IL_037D;
				}
				commonTree = (CommonTree)this.adaptor.GetNilNode();
				IToken token6 = (IToken)this.Match(this.input, 5, NCalcParser.FOLLOW_FLOAT_in_value884);
				CommonTree commonTree7 = (CommonTree)this.adaptor.Create(token6);
				this.adaptor.AddChild(commonTree, commonTree7);
				value_return.value = new ValueExpression(double.Parse((token6 != null) ? token6.Text : null, NumberStyles.Float, NCalcParser.numberFormatInfo));
				IL_037D:
				value_return.Stop = this.input.LT(-1);
				value_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(value_return.Tree, (IToken)value_return.Start, (IToken)value_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				value_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)value_return.Start, this.input.LT(-1), ex);
			}
			return value_return;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00018C88 File Offset: 0x00016E88
		public NCalcParser.identifier_return identifier()
		{
			NCalcParser.identifier_return identifier_return = new NCalcParser.identifier_return();
			identifier_return.Start = this.input.LT(1);
			CommonTree commonTree = null;
			try
			{
				int num = this.input.LA(1);
				int num2;
				if (num == 10)
				{
					num2 = 1;
				}
				else
				{
					if (num != 11)
					{
						throw new NoViableAltException("", 21, 0, this.input);
					}
					num2 = 2;
				}
				if (num2 != 1)
				{
					if (num2 == 2)
					{
						commonTree = (CommonTree)this.adaptor.GetNilNode();
						IToken token = (IToken)this.Match(this.input, 11, NCalcParser.FOLLOW_NAME_in_identifier942);
						CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
						this.adaptor.AddChild(commonTree, commonTree2);
						identifier_return.value = new IdentifierExpression(((token != null) ? token.Text : null).Substring(1, ((token != null) ? token.Text : null).Length - 2));
					}
				}
				else
				{
					commonTree = (CommonTree)this.adaptor.GetNilNode();
					IToken token2 = (IToken)this.Match(this.input, 10, NCalcParser.FOLLOW_ID_in_identifier934);
					CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
					this.adaptor.AddChild(commonTree, commonTree3);
					identifier_return.value = new IdentifierExpression((token2 != null) ? token2.Text : null);
				}
				identifier_return.Stop = this.input.LT(-1);
				identifier_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(identifier_return.Tree, (IToken)identifier_return.Start, (IToken)identifier_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				identifier_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)identifier_return.Start, this.input.LT(-1), ex);
			}
			return identifier_return;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00018EA0 File Offset: 0x000170A0
		public NCalcParser.expressionList_return expressionList()
		{
			NCalcParser.expressionList_return expressionList_return = new NCalcParser.expressionList_return();
			expressionList_return.Start = this.input.LT(1);
			List<LogicalExpression> list = new List<LogicalExpression>();
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				base.PushFollow(NCalcParser.FOLLOW_logicalExpression_in_expressionList966);
				NCalcParser.logicalExpression_return logicalExpression_return = this.logicalExpression();
				this.state.followingStackPointer--;
				this.adaptor.AddChild(commonTree, logicalExpression_return.Tree);
				list.Add((logicalExpression_return != null) ? logicalExpression_return.value : null);
				for (;;)
				{
					int num = 2;
					if (this.input.LA(1) == 48)
					{
						num = 1;
					}
					if (num != 1)
					{
						break;
					}
					IToken token = (IToken)this.Match(this.input, 48, NCalcParser.FOLLOW_48_in_expressionList973);
					CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
					this.adaptor.AddChild(commonTree, commonTree2);
					base.PushFollow(NCalcParser.FOLLOW_logicalExpression_in_expressionList977);
					NCalcParser.logicalExpression_return logicalExpression_return2 = this.logicalExpression();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, logicalExpression_return2.Tree);
					list.Add((logicalExpression_return2 != null) ? logicalExpression_return2.value : null);
				}
				expressionList_return.value = list;
				expressionList_return.Stop = this.input.LT(-1);
				expressionList_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(expressionList_return.Tree, (IToken)expressionList_return.Start, (IToken)expressionList_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				expressionList_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)expressionList_return.Start, this.input.LT(-1), ex);
			}
			return expressionList_return;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000190A4 File Offset: 0x000172A4
		public NCalcParser.arguments_return arguments()
		{
			NCalcParser.arguments_return arguments_return = new NCalcParser.arguments_return();
			arguments_return.Start = this.input.LT(1);
			arguments_return.value = new List<LogicalExpression>();
			try
			{
				CommonTree commonTree = (CommonTree)this.adaptor.GetNilNode();
				IToken token = (IToken)this.Match(this.input, 46, NCalcParser.FOLLOW_46_in_arguments1006);
				CommonTree commonTree2 = (CommonTree)this.adaptor.Create(token);
				this.adaptor.AddChild(commonTree, commonTree2);
				int num = 2;
				int num2 = this.input.LA(1);
				if ((num2 >= 4 && num2 <= 11) || num2 == 39 || (num2 >= 43 && num2 <= 46))
				{
					num = 1;
				}
				if (num == 1)
				{
					base.PushFollow(NCalcParser.FOLLOW_expressionList_in_arguments1010);
					NCalcParser.expressionList_return expressionList_return = this.expressionList();
					this.state.followingStackPointer--;
					this.adaptor.AddChild(commonTree, expressionList_return.Tree);
					arguments_return.value = ((expressionList_return != null) ? expressionList_return.value : null);
				}
				IToken token2 = (IToken)this.Match(this.input, 47, NCalcParser.FOLLOW_47_in_arguments1017);
				CommonTree commonTree3 = (CommonTree)this.adaptor.Create(token2);
				this.adaptor.AddChild(commonTree, commonTree3);
				arguments_return.Stop = this.input.LT(-1);
				arguments_return.Tree = (CommonTree)this.adaptor.RulePostProcessing(commonTree);
				this.adaptor.SetTokenBoundaries(arguments_return.Tree, (IToken)arguments_return.Start, (IToken)arguments_return.Stop);
			}
			catch (RecognitionException ex)
			{
				this.ReportError(ex);
				this.Recover(this.input, ex);
				arguments_return.Tree = (CommonTree)this.adaptor.ErrorNode(this.input, (IToken)arguments_return.Start, this.input.LT(-1), ex);
			}
			return arguments_return;
		}

		// Token: 0x040002B8 RID: 696
		protected ITreeAdaptor adaptor = new CommonTreeAdaptor();

		// Token: 0x040002BA RID: 698
		public const int T__29 = 29;

		// Token: 0x040002BB RID: 699
		public const int T__28 = 28;

		// Token: 0x040002BC RID: 700
		public const int T__27 = 27;

		// Token: 0x040002BD RID: 701
		public const int T__26 = 26;

		// Token: 0x040002BE RID: 702
		public const int T__25 = 25;

		// Token: 0x040002BF RID: 703
		public const int T__24 = 24;

		// Token: 0x040002C0 RID: 704
		public const int T__23 = 23;

		// Token: 0x040002C1 RID: 705
		public const int LETTER = 12;

		// Token: 0x040002C2 RID: 706
		public const int T__22 = 22;

		// Token: 0x040002C3 RID: 707
		public const int T__21 = 21;

		// Token: 0x040002C4 RID: 708
		public const int T__20 = 20;

		// Token: 0x040002C5 RID: 709
		public const int FLOAT = 5;

		// Token: 0x040002C6 RID: 710
		public const int ID = 10;

		// Token: 0x040002C7 RID: 711
		public const int EOF = -1;

		// Token: 0x040002C8 RID: 712
		public const int HexDigit = 17;

		// Token: 0x040002C9 RID: 713
		public const int T__19 = 19;

		// Token: 0x040002CA RID: 714
		public const int NAME = 11;

		// Token: 0x040002CB RID: 715
		public const int DIGIT = 13;

		// Token: 0x040002CC RID: 716
		public const int T__42 = 42;

		// Token: 0x040002CD RID: 717
		public const int INTEGER = 4;

		// Token: 0x040002CE RID: 718
		public const int E = 14;

		// Token: 0x040002CF RID: 719
		public const int T__43 = 43;

		// Token: 0x040002D0 RID: 720
		public const int T__40 = 40;

		// Token: 0x040002D1 RID: 721
		public const int T__41 = 41;

		// Token: 0x040002D2 RID: 722
		public const int T__46 = 46;

		// Token: 0x040002D3 RID: 723
		public const int T__47 = 47;

		// Token: 0x040002D4 RID: 724
		public const int T__44 = 44;

		// Token: 0x040002D5 RID: 725
		public const int T__45 = 45;

		// Token: 0x040002D6 RID: 726
		public const int T__48 = 48;

		// Token: 0x040002D7 RID: 727
		public const int DATETIME = 7;

		// Token: 0x040002D8 RID: 728
		public const int TRUE = 8;

		// Token: 0x040002D9 RID: 729
		public const int T__30 = 30;

		// Token: 0x040002DA RID: 730
		public const int T__31 = 31;

		// Token: 0x040002DB RID: 731
		public const int T__32 = 32;

		// Token: 0x040002DC RID: 732
		public const int WS = 18;

		// Token: 0x040002DD RID: 733
		public const int T__33 = 33;

		// Token: 0x040002DE RID: 734
		public const int T__34 = 34;

		// Token: 0x040002DF RID: 735
		public const int T__35 = 35;

		// Token: 0x040002E0 RID: 736
		public const int T__36 = 36;

		// Token: 0x040002E1 RID: 737
		public const int T__37 = 37;

		// Token: 0x040002E2 RID: 738
		public const int T__38 = 38;

		// Token: 0x040002E3 RID: 739
		public const int T__39 = 39;

		// Token: 0x040002E4 RID: 740
		public const int UnicodeEscape = 16;

		// Token: 0x040002E5 RID: 741
		public const int FALSE = 9;

		// Token: 0x040002E6 RID: 742
		public const int EscapeSequence = 15;

		// Token: 0x040002E7 RID: 743
		public const int STRING = 6;

		// Token: 0x040002E8 RID: 744
		private const char BS = '\\';

		// Token: 0x040002E9 RID: 745
		public static readonly string[] tokenNames = new string[]
		{
			"<invalid>", "<EOR>", "<DOWN>", "<UP>", "INTEGER", "FLOAT", "STRING", "DATETIME", "TRUE", "FALSE",
			"ID", "NAME", "LETTER", "DIGIT", "E", "EscapeSequence", "UnicodeEscape", "HexDigit", "WS", "'?'",
			"':'", "'||'", "'or'", "'&&'", "'and'", "'|'", "'^'", "'&'", "'=='", "'='",
			"'!='", "'<>'", "'<'", "'<='", "'>'", "'>='", "'<<'", "'>>'", "'+'", "'-'",
			"'*'", "'/'", "'%'", "'!'", "'not'", "'~'", "'('", "')'", "','"
		};

		// Token: 0x040002EA RID: 746
		private static NumberFormatInfo numberFormatInfo = new NumberFormatInfo();

		// Token: 0x040002EB RID: 747
		public static readonly BitSet FOLLOW_logicalExpression_in_ncalcExpression56 = new BitSet(new ulong[1]);

		// Token: 0x040002EC RID: 748
		public static readonly BitSet FOLLOW_EOF_in_ncalcExpression58 = new BitSet(new ulong[] { 2UL });

		// Token: 0x040002ED RID: 749
		public static readonly BitSet FOLLOW_conditionalExpression_in_logicalExpression78 = new BitSet(new ulong[] { 524290UL });

		// Token: 0x040002EE RID: 750
		public static readonly BitSet FOLLOW_19_in_logicalExpression84 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002EF RID: 751
		public static readonly BitSet FOLLOW_conditionalExpression_in_logicalExpression88 = new BitSet(new ulong[] { 1048576UL });

		// Token: 0x040002F0 RID: 752
		public static readonly BitSet FOLLOW_20_in_logicalExpression90 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002F1 RID: 753
		public static readonly BitSet FOLLOW_conditionalExpression_in_logicalExpression94 = new BitSet(new ulong[] { 2UL });

		// Token: 0x040002F2 RID: 754
		public static readonly BitSet FOLLOW_booleanAndExpression_in_conditionalExpression121 = new BitSet(new ulong[] { 6291458UL });

		// Token: 0x040002F3 RID: 755
		public static readonly BitSet FOLLOW_set_in_conditionalExpression130 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002F4 RID: 756
		public static readonly BitSet FOLLOW_conditionalExpression_in_conditionalExpression146 = new BitSet(new ulong[] { 6291458UL });

		// Token: 0x040002F5 RID: 757
		public static readonly BitSet FOLLOW_bitwiseOrExpression_in_booleanAndExpression180 = new BitSet(new ulong[] { 25165826UL });

		// Token: 0x040002F6 RID: 758
		public static readonly BitSet FOLLOW_set_in_booleanAndExpression189 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002F7 RID: 759
		public static readonly BitSet FOLLOW_bitwiseOrExpression_in_booleanAndExpression205 = new BitSet(new ulong[] { 25165826UL });

		// Token: 0x040002F8 RID: 760
		public static readonly BitSet FOLLOW_bitwiseXOrExpression_in_bitwiseOrExpression237 = new BitSet(new ulong[] { 33554434UL });

		// Token: 0x040002F9 RID: 761
		public static readonly BitSet FOLLOW_25_in_bitwiseOrExpression246 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002FA RID: 762
		public static readonly BitSet FOLLOW_bitwiseOrExpression_in_bitwiseOrExpression256 = new BitSet(new ulong[] { 33554434UL });

		// Token: 0x040002FB RID: 763
		public static readonly BitSet FOLLOW_bitwiseAndExpression_in_bitwiseXOrExpression290 = new BitSet(new ulong[] { 67108866UL });

		// Token: 0x040002FC RID: 764
		public static readonly BitSet FOLLOW_26_in_bitwiseXOrExpression299 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x040002FD RID: 765
		public static readonly BitSet FOLLOW_bitwiseAndExpression_in_bitwiseXOrExpression309 = new BitSet(new ulong[] { 67108866UL });

		// Token: 0x040002FE RID: 766
		public static readonly BitSet FOLLOW_equalityExpression_in_bitwiseAndExpression341 = new BitSet(new ulong[] { 134217730UL });

		// Token: 0x040002FF RID: 767
		public static readonly BitSet FOLLOW_27_in_bitwiseAndExpression350 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000300 RID: 768
		public static readonly BitSet FOLLOW_equalityExpression_in_bitwiseAndExpression360 = new BitSet(new ulong[] { 134217730UL });

		// Token: 0x04000301 RID: 769
		public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression394 = new BitSet(new ulong[] { (ulong)(-268435454) });

		// Token: 0x04000302 RID: 770
		public static readonly BitSet FOLLOW_set_in_equalityExpression405 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000303 RID: 771
		public static readonly BitSet FOLLOW_set_in_equalityExpression422 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000304 RID: 772
		public static readonly BitSet FOLLOW_relationalExpression_in_equalityExpression441 = new BitSet(new ulong[] { (ulong)(-268435454) });

		// Token: 0x04000305 RID: 773
		public static readonly BitSet FOLLOW_shiftExpression_in_relationalExpression474 = new BitSet(new ulong[] { 64424509442UL });

		// Token: 0x04000306 RID: 774
		public static readonly BitSet FOLLOW_32_in_relationalExpression485 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000307 RID: 775
		public static readonly BitSet FOLLOW_33_in_relationalExpression495 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000308 RID: 776
		public static readonly BitSet FOLLOW_34_in_relationalExpression506 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000309 RID: 777
		public static readonly BitSet FOLLOW_35_in_relationalExpression516 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x0400030A RID: 778
		public static readonly BitSet FOLLOW_shiftExpression_in_relationalExpression528 = new BitSet(new ulong[] { 64424509442UL });

		// Token: 0x0400030B RID: 779
		public static readonly BitSet FOLLOW_additiveExpression_in_shiftExpression560 = new BitSet(new ulong[] { 206158430210UL });

		// Token: 0x0400030C RID: 780
		public static readonly BitSet FOLLOW_36_in_shiftExpression571 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x0400030D RID: 781
		public static readonly BitSet FOLLOW_37_in_shiftExpression581 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x0400030E RID: 782
		public static readonly BitSet FOLLOW_additiveExpression_in_shiftExpression593 = new BitSet(new ulong[] { 206158430210UL });

		// Token: 0x0400030F RID: 783
		public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression625 = new BitSet(new ulong[] { 824633720834UL });

		// Token: 0x04000310 RID: 784
		public static readonly BitSet FOLLOW_38_in_additiveExpression636 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000311 RID: 785
		public static readonly BitSet FOLLOW_39_in_additiveExpression646 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000312 RID: 786
		public static readonly BitSet FOLLOW_multiplicativeExpression_in_additiveExpression658 = new BitSet(new ulong[] { 824633720834UL });

		// Token: 0x04000313 RID: 787
		public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression690 = new BitSet(new ulong[] { 7696581394434UL });

		// Token: 0x04000314 RID: 788
		public static readonly BitSet FOLLOW_40_in_multiplicativeExpression701 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000315 RID: 789
		public static readonly BitSet FOLLOW_41_in_multiplicativeExpression711 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000316 RID: 790
		public static readonly BitSet FOLLOW_42_in_multiplicativeExpression721 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000317 RID: 791
		public static readonly BitSet FOLLOW_unaryExpression_in_multiplicativeExpression733 = new BitSet(new ulong[] { 7696581394434UL });

		// Token: 0x04000318 RID: 792
		public static readonly BitSet FOLLOW_primaryExpression_in_unaryExpression760 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000319 RID: 793
		public static readonly BitSet FOLLOW_set_in_unaryExpression771 = new BitSet(new ulong[] { 70368744181744UL });

		// Token: 0x0400031A RID: 794
		public static readonly BitSet FOLLOW_primaryExpression_in_unaryExpression779 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400031B RID: 795
		public static readonly BitSet FOLLOW_45_in_unaryExpression791 = new BitSet(new ulong[] { 70368744181744UL });

		// Token: 0x0400031C RID: 796
		public static readonly BitSet FOLLOW_primaryExpression_in_unaryExpression794 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400031D RID: 797
		public static readonly BitSet FOLLOW_39_in_unaryExpression805 = new BitSet(new ulong[] { 70368744181744UL });

		// Token: 0x0400031E RID: 798
		public static readonly BitSet FOLLOW_primaryExpression_in_unaryExpression807 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400031F RID: 799
		public static readonly BitSet FOLLOW_46_in_primaryExpression829 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x04000320 RID: 800
		public static readonly BitSet FOLLOW_logicalExpression_in_primaryExpression831 = new BitSet(new ulong[] { 140737488355328UL });

		// Token: 0x04000321 RID: 801
		public static readonly BitSet FOLLOW_47_in_primaryExpression833 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000322 RID: 802
		public static readonly BitSet FOLLOW_value_in_primaryExpression843 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000323 RID: 803
		public static readonly BitSet FOLLOW_identifier_in_primaryExpression851 = new BitSet(new ulong[] { 70368744177666UL });

		// Token: 0x04000324 RID: 804
		public static readonly BitSet FOLLOW_arguments_in_primaryExpression856 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000325 RID: 805
		public static readonly BitSet FOLLOW_INTEGER_in_value876 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000326 RID: 806
		public static readonly BitSet FOLLOW_FLOAT_in_value884 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000327 RID: 807
		public static readonly BitSet FOLLOW_STRING_in_value892 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000328 RID: 808
		public static readonly BitSet FOLLOW_DATETIME_in_value901 = new BitSet(new ulong[] { 2UL });

		// Token: 0x04000329 RID: 809
		public static readonly BitSet FOLLOW_TRUE_in_value908 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400032A RID: 810
		public static readonly BitSet FOLLOW_FALSE_in_value916 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400032B RID: 811
		public static readonly BitSet FOLLOW_ID_in_identifier934 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400032C RID: 812
		public static readonly BitSet FOLLOW_NAME_in_identifier942 = new BitSet(new ulong[] { 2UL });

		// Token: 0x0400032D RID: 813
		public static readonly BitSet FOLLOW_logicalExpression_in_expressionList966 = new BitSet(new ulong[] { 281474976710658UL });

		// Token: 0x0400032E RID: 814
		public static readonly BitSet FOLLOW_48_in_expressionList973 = new BitSet(new ulong[] { 132491151151088UL });

		// Token: 0x0400032F RID: 815
		public static readonly BitSet FOLLOW_logicalExpression_in_expressionList977 = new BitSet(new ulong[] { 281474976710658UL });

		// Token: 0x04000330 RID: 816
		public static readonly BitSet FOLLOW_46_in_arguments1006 = new BitSet(new ulong[] { 273228639506416UL });

		// Token: 0x04000331 RID: 817
		public static readonly BitSet FOLLOW_expressionList_in_arguments1010 = new BitSet(new ulong[] { 140737488355328UL });

		// Token: 0x04000332 RID: 818
		public static readonly BitSet FOLLOW_47_in_arguments1017 = new BitSet(new ulong[] { 2UL });

		// Token: 0x020001ED RID: 493
		public class ncalcExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003E9 RID: 1001
			// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0001C5CE File Offset: 0x0001A7CE
			// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000431 RID: 1073
			public LogicalExpression value;

			// Token: 0x04000432 RID: 1074
			private CommonTree tree;
		}

		// Token: 0x020001EE RID: 494
		public class logicalExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003EA RID: 1002
			// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0001C5EC File Offset: 0x0001A7EC
			// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000433 RID: 1075
			public LogicalExpression value;

			// Token: 0x04000434 RID: 1076
			private CommonTree tree;
		}

		// Token: 0x020001EF RID: 495
		public class conditionalExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003EB RID: 1003
			// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0001C60A File Offset: 0x0001A80A
			// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0001C612 File Offset: 0x0001A812
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000435 RID: 1077
			public LogicalExpression value;

			// Token: 0x04000436 RID: 1078
			private CommonTree tree;
		}

		// Token: 0x020001F0 RID: 496
		public class booleanAndExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0001C628 File Offset: 0x0001A828
			// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0001C630 File Offset: 0x0001A830
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000437 RID: 1079
			public LogicalExpression value;

			// Token: 0x04000438 RID: 1080
			private CommonTree tree;
		}

		// Token: 0x020001F1 RID: 497
		public class bitwiseOrExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0001C646 File Offset: 0x0001A846
			// (set) Token: 0x06000CA2 RID: 3234 RVA: 0x0001C64E File Offset: 0x0001A84E
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000439 RID: 1081
			public LogicalExpression value;

			// Token: 0x0400043A RID: 1082
			private CommonTree tree;
		}

		// Token: 0x020001F2 RID: 498
		public class bitwiseXOrExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0001C664 File Offset: 0x0001A864
			// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0001C66C File Offset: 0x0001A86C
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400043B RID: 1083
			public LogicalExpression value;

			// Token: 0x0400043C RID: 1084
			private CommonTree tree;
		}

		// Token: 0x020001F3 RID: 499
		public class bitwiseAndExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003EF RID: 1007
			// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0001C682 File Offset: 0x0001A882
			// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x0001C68A File Offset: 0x0001A88A
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400043D RID: 1085
			public LogicalExpression value;

			// Token: 0x0400043E RID: 1086
			private CommonTree tree;
		}

		// Token: 0x020001F4 RID: 500
		public class equalityExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F0 RID: 1008
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
			// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400043F RID: 1087
			public LogicalExpression value;

			// Token: 0x04000440 RID: 1088
			private CommonTree tree;
		}

		// Token: 0x020001F5 RID: 501
		public class relationalExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F1 RID: 1009
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0001C6BE File Offset: 0x0001A8BE
			// (set) Token: 0x06000CAE RID: 3246 RVA: 0x0001C6C6 File Offset: 0x0001A8C6
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000441 RID: 1089
			public LogicalExpression value;

			// Token: 0x04000442 RID: 1090
			private CommonTree tree;
		}

		// Token: 0x020001F6 RID: 502
		public class shiftExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F2 RID: 1010
			// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001C6DC File Offset: 0x0001A8DC
			// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000443 RID: 1091
			public LogicalExpression value;

			// Token: 0x04000444 RID: 1092
			private CommonTree tree;
		}

		// Token: 0x020001F7 RID: 503
		public class additiveExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F3 RID: 1011
			// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0001C6FA File Offset: 0x0001A8FA
			// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0001C702 File Offset: 0x0001A902
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000445 RID: 1093
			public LogicalExpression value;

			// Token: 0x04000446 RID: 1094
			private CommonTree tree;
		}

		// Token: 0x020001F8 RID: 504
		public class multiplicativeExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F4 RID: 1012
			// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0001C718 File Offset: 0x0001A918
			// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0001C720 File Offset: 0x0001A920
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000447 RID: 1095
			public LogicalExpression value;

			// Token: 0x04000448 RID: 1096
			private CommonTree tree;
		}

		// Token: 0x020001F9 RID: 505
		public class unaryExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0001C736 File Offset: 0x0001A936
			// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0001C73E File Offset: 0x0001A93E
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000449 RID: 1097
			public LogicalExpression value;

			// Token: 0x0400044A RID: 1098
			private CommonTree tree;
		}

		// Token: 0x020001FA RID: 506
		public class primaryExpression_return : ParserRuleReturnScope
		{
			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001C754 File Offset: 0x0001A954
			// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0001C75C File Offset: 0x0001A95C
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400044B RID: 1099
			public LogicalExpression value;

			// Token: 0x0400044C RID: 1100
			private CommonTree tree;
		}

		// Token: 0x020001FB RID: 507
		public class value_return : ParserRuleReturnScope
		{
			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0001C772 File Offset: 0x0001A972
			// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0001C77A File Offset: 0x0001A97A
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400044D RID: 1101
			public ValueExpression value;

			// Token: 0x0400044E RID: 1102
			private CommonTree tree;
		}

		// Token: 0x020001FC RID: 508
		public class identifier_return : ParserRuleReturnScope
		{
			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0001C790 File Offset: 0x0001A990
			// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0001C798 File Offset: 0x0001A998
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x0400044F RID: 1103
			public IdentifierExpression value;

			// Token: 0x04000450 RID: 1104
			private CommonTree tree;
		}

		// Token: 0x020001FD RID: 509
		public class expressionList_return : ParserRuleReturnScope
		{
			// Token: 0x170003F9 RID: 1017
			// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0001C7AE File Offset: 0x0001A9AE
			// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0001C7B6 File Offset: 0x0001A9B6
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000451 RID: 1105
			public List<LogicalExpression> value;

			// Token: 0x04000452 RID: 1106
			private CommonTree tree;
		}

		// Token: 0x020001FE RID: 510
		public class arguments_return : ParserRuleReturnScope
		{
			// Token: 0x170003FA RID: 1018
			// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0001C7CC File Offset: 0x0001A9CC
			// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
			public override object Tree
			{
				get
				{
					return this.tree;
				}
				set
				{
					this.tree = (CommonTree)value;
				}
			}

			// Token: 0x04000453 RID: 1107
			public List<LogicalExpression> value;

			// Token: 0x04000454 RID: 1108
			private CommonTree tree;
		}
	}
}
