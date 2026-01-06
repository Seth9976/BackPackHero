using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000099 RID: 153
	internal sealed class ExpressionParser
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0002E45C File Offset: 0x0002C65C
		internal ExpressionParser(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002E4B8 File Offset: 0x0002C6B8
		internal void LoadExpression(string data)
		{
			int num;
			if (data == null)
			{
				num = 0;
				this._text = new char[num + 1];
			}
			else
			{
				num = data.Length;
				this._text = new char[num + 1];
				data.CopyTo(0, this._text, 0, num);
			}
			this._text[num] = '\0';
			if (this._expression != null)
			{
				this._expression = null;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002E518 File Offset: 0x0002C718
		internal void StartScan()
		{
			this._op = 0;
			this._pos = 0;
			this._start = 0;
			this._topOperator = 0;
			OperatorInfo[] ops = this._ops;
			int topOperator = this._topOperator;
			this._topOperator = topOperator + 1;
			ops[topOperator] = new OperatorInfo(Nodes.Noop, 0, 0);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002E564 File Offset: 0x0002C764
		internal ExpressionNode Parse()
		{
			this._expression = null;
			this.StartScan();
			int num = 0;
			while (this._token != Tokens.EOS)
			{
				OperatorInfo operatorInfo;
				for (;;)
				{
					this.Scan();
					int num2;
					switch (this._token)
					{
					case Tokens.Name:
					case Tokens.Numeric:
					case Tokens.Decimal:
					case Tokens.Float:
					case Tokens.StringConst:
					case Tokens.Date:
					case Tokens.Parent:
					{
						ExpressionNode expressionNode = null;
						if (this._prevOperand != 0)
						{
							goto Block_5;
						}
						if (this._topOperator > 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5 && this._token != Tokens.Parent)
							{
								goto Block_9;
							}
						}
						this._prevOperand = 1;
						Tokens token = this._token;
						switch (token)
						{
						case Tokens.Name:
							operatorInfo = this._ops[this._topOperator - 1];
							expressionNode = new NameNode(this._table, this._text, this._start, this._pos);
							break;
						case Tokens.Numeric:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Numeric, text);
							break;
						}
						case Tokens.Decimal:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Decimal, text);
							break;
						}
						case Tokens.Float:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Float, text);
							break;
						}
						case Tokens.BinaryConst:
							break;
						case Tokens.StringConst:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Str, text);
							break;
						}
						case Tokens.Date:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Date, text);
							break;
						}
						default:
							if (token == Tokens.Parent)
							{
								string text2;
								try
								{
									this.Scan();
									if (this._token == Tokens.LeftParen)
									{
										this.ScanToken(Tokens.Name);
										text2 = NameNode.ParseName(this._text, this._start, this._pos);
										this.ScanToken(Tokens.RightParen);
										this.ScanToken(Tokens.Dot);
									}
									else
									{
										text2 = null;
										this.CheckToken(Tokens.Dot);
									}
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									throw ExprException.LookupArgument();
								}
								this.ScanToken(Tokens.Name);
								string text3 = NameNode.ParseName(this._text, this._start, this._pos);
								operatorInfo = this._ops[this._topOperator - 1];
								expressionNode = new LookupNode(this._table, text3, text2);
							}
							break;
						}
						this.NodePush(expressionNode);
						continue;
					}
					case Tokens.ListSeparator:
					{
						if (this._prevOperand == 0)
						{
							goto Block_23;
						}
						this.BuildExpression(3);
						operatorInfo = this._ops[this._topOperator - 1];
						if (operatorInfo._type != Nodes.Call)
						{
							goto Block_24;
						}
						ExpressionNode expressionNode2 = this.NodePop();
						FunctionNode functionNode = (FunctionNode)this.NodePop();
						functionNode.AddArgument(expressionNode2);
						this.NodePush(functionNode);
						this._prevOperand = 0;
						continue;
					}
					case Tokens.LeftParen:
						num++;
						if (this._prevOperand == 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5)
							{
								ExpressionNode expressionNode = new FunctionNode(this._table, "In");
								this.NodePush(expressionNode);
								OperatorInfo[] ops = this._ops;
								num2 = this._topOperator;
								this._topOperator = num2 + 1;
								ops[num2] = new OperatorInfo(Nodes.Call, 0, 2);
								continue;
							}
							OperatorInfo[] ops2 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops2[num2] = new OperatorInfo(Nodes.Paren, 0, 2);
							continue;
						}
						else
						{
							this.BuildExpression(22);
							this._prevOperand = 0;
							ExpressionNode expressionNode3 = this.NodePeek();
							if (expressionNode3 == null || expressionNode3.GetType() != typeof(NameNode))
							{
								goto IL_041A;
							}
							NameNode nameNode = (NameNode)this.NodePop();
							ExpressionNode expressionNode = new FunctionNode(this._table, nameNode._name);
							Aggregate aggregate = (Aggregate)((FunctionNode)expressionNode).Aggregate;
							if (aggregate != Aggregate.None)
							{
								expressionNode = this.ParseAggregateArgument((FunctionId)aggregate);
								this.NodePush(expressionNode);
								this._prevOperand = 2;
								continue;
							}
							this.NodePush(expressionNode);
							OperatorInfo[] ops3 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops3[num2] = new OperatorInfo(Nodes.Call, 0, 2);
							continue;
						}
						break;
					case Tokens.RightParen:
						if (this._prevOperand != 0)
						{
							this.BuildExpression(3);
						}
						if (this._topOperator <= 1)
						{
							goto Block_18;
						}
						this._topOperator--;
						operatorInfo = this._ops[this._topOperator];
						if (this._prevOperand == 0 && operatorInfo._type != Nodes.Call)
						{
							goto Block_20;
						}
						if (operatorInfo._type == Nodes.Call)
						{
							if (this._prevOperand != 0)
							{
								ExpressionNode expressionNode4 = this.NodePop();
								FunctionNode functionNode2 = (FunctionNode)this.NodePop();
								functionNode2.AddArgument(expressionNode4);
								functionNode2.Check();
								this.NodePush(functionNode2);
							}
						}
						else
						{
							ExpressionNode expressionNode = this.NodePop();
							expressionNode = new UnaryNode(this._table, 0, expressionNode);
							this.NodePush(expressionNode);
						}
						this._prevOperand = 2;
						num--;
						continue;
					case Tokens.ZeroOp:
					{
						if (this._prevOperand != 0)
						{
							goto Block_28;
						}
						OperatorInfo[] ops4 = this._ops;
						num2 = this._topOperator;
						this._topOperator = num2 + 1;
						ops4[num2] = new OperatorInfo(Nodes.Zop, this._op, 24);
						this._prevOperand = 2;
						continue;
					}
					case Tokens.UnaryOp:
						goto IL_0654;
					case Tokens.BinaryOp:
						if (this._prevOperand != 0)
						{
							this._prevOperand = 0;
							this.BuildExpression(Operators.Priority(this._op));
							OperatorInfo[] ops5 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops5[num2] = new OperatorInfo(Nodes.Binop, this._op, Operators.Priority(this._op));
							continue;
						}
						if (this._op == 15)
						{
							this._op = 2;
							goto IL_0654;
						}
						if (this._op == 16)
						{
							this._op = 1;
							goto IL_0654;
						}
						goto IL_05F4;
					case Tokens.Dot:
					{
						ExpressionNode expressionNode5 = this.NodePeek();
						if (expressionNode5 != null && expressionNode5.GetType() == typeof(NameNode))
						{
							this.Scan();
							if (this._token == Tokens.Name)
							{
								string text4 = ((NameNode)this.NodePop())._name + "." + NameNode.ParseName(this._text, this._start, this._pos);
								this.NodePush(new NameNode(this._table, text4));
								continue;
							}
						}
						break;
					}
					case Tokens.EOS:
						goto IL_0079;
					}
					goto Block_1;
					IL_0654:
					OperatorInfo[] ops6 = this._ops;
					num2 = this._topOperator;
					this._topOperator = num2 + 1;
					ops6[num2] = new OperatorInfo(Nodes.Unop, this._op, Operators.Priority(this._op));
				}
				IL_0079:
				if (this._prevOperand == 0)
				{
					if (this._topNode != 0)
					{
						operatorInfo = this._ops[this._topOperator - 1];
						throw ExprException.MissingOperand(operatorInfo);
					}
					continue;
				}
				else
				{
					this.BuildExpression(3);
					if (this._topOperator != 1)
					{
						throw ExprException.MissingRightParen();
					}
					continue;
				}
				Block_1:
				goto IL_076B;
				Block_5:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				Block_9:
				throw ExprException.InWithoutParentheses();
				IL_041A:
				throw ExprException.SyntaxError();
				Block_18:
				throw ExprException.TooManyRightParentheses();
				Block_20:
				throw ExprException.MissingOperand(operatorInfo);
				Block_23:
				throw ExprException.MissingOperandBefore(",");
				Block_24:
				throw ExprException.SyntaxError();
				IL_05F4:
				throw ExprException.MissingOperandBefore(Operators.ToString(this._op));
				Block_28:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				IL_076B:
				throw ExprException.UnknownToken(new string(this._text, this._start, this._pos - this._start), this._start + 1);
			}
			this._expression = this._nodeStack[0];
			return this._expression;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		private ExpressionNode ParseAggregateArgument(FunctionId aggregate)
		{
			this.Scan();
			string text;
			bool flag;
			string text2;
			try
			{
				if (this._token != Tokens.Child)
				{
					if (this._token != Tokens.Name)
					{
						throw ExprException.AggregateArgument();
					}
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
					return new AggregateNode(this._table, aggregate, text);
				}
				else
				{
					flag = this._token == Tokens.Child;
					this._prevOperand = 1;
					this.Scan();
					if (this._token == Tokens.LeftParen)
					{
						this.ScanToken(Tokens.Name);
						text2 = NameNode.ParseName(this._text, this._start, this._pos);
						this.ScanToken(Tokens.RightParen);
						this.ScanToken(Tokens.Dot);
					}
					else
					{
						text2 = null;
						this.CheckToken(Tokens.Dot);
					}
					this.ScanToken(Tokens.Name);
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExprException.AggregateArgument();
			}
			return new AggregateNode(this._table, aggregate, text, !flag, text2);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002EE64 File Offset: 0x0002D064
		private ExpressionNode NodePop()
		{
			ExpressionNode[] nodeStack = this._nodeStack;
			int num = this._topNode - 1;
			this._topNode = num;
			return nodeStack[num];
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002EE89 File Offset: 0x0002D089
		private ExpressionNode NodePeek()
		{
			if (this._topNode <= 0)
			{
				return null;
			}
			return this._nodeStack[this._topNode - 1];
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002EEA8 File Offset: 0x0002D0A8
		private void NodePush(ExpressionNode node)
		{
			if (this._topNode >= 98)
			{
				throw ExprException.ExpressionTooComplex();
			}
			ExpressionNode[] nodeStack = this._nodeStack;
			int topNode = this._topNode;
			this._topNode = topNode + 1;
			nodeStack[topNode] = node;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		private void BuildExpression(int pri)
		{
			OperatorInfo operatorInfo;
			for (;;)
			{
				operatorInfo = this._ops[this._topOperator - 1];
				if (operatorInfo._priority < pri)
				{
					return;
				}
				this._topOperator--;
				ExpressionNode expressionNode2;
				switch (operatorInfo._type)
				{
				case Nodes.Unop:
				{
					ExpressionNode expressionNode = this.NodePop();
					int op = operatorInfo._op;
					if (op != 1 && op != 3 && op == 25)
					{
						goto Block_6;
					}
					expressionNode2 = new UnaryNode(this._table, operatorInfo._op, expressionNode);
					goto IL_0163;
				}
				case Nodes.UnopSpec:
				case Nodes.BinopSpec:
					return;
				case Nodes.Binop:
				{
					ExpressionNode expressionNode = this.NodePop();
					ExpressionNode expressionNode3 = this.NodePop();
					switch (operatorInfo._op)
					{
					case 4:
					case 6:
					case 22:
					case 23:
					case 24:
					case 25:
						goto IL_00D3;
					}
					if (operatorInfo._op == 14)
					{
						expressionNode2 = new LikeNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
						goto IL_0163;
					}
					expressionNode2 = new BinaryNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
					goto IL_0163;
				}
				case Nodes.Zop:
					expressionNode2 = new ZeroOpNode(operatorInfo._op);
					goto IL_0163;
				}
				break;
				IL_0163:
				this.NodePush(expressionNode2);
			}
			return;
			IL_00D3:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
			Block_6:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002F05C File Offset: 0x0002D25C
		internal void CheckToken(Tokens token)
		{
			if (this._token != token)
			{
				throw ExprException.UnknownToken(token, this._token, this._pos);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002F07C File Offset: 0x0002D27C
		internal Tokens Scan()
		{
			char[] text = this._text;
			this._token = Tokens.None;
			char c;
			for (;;)
			{
				this._start = this._pos;
				this._op = 0;
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				c = array[pos];
				if (c > '>')
				{
					goto IL_00CD;
				}
				if (c > '\r')
				{
					switch (c)
					{
					case ' ':
						goto IL_0111;
					case '!':
					case '"':
					case '$':
					case ',':
					case '.':
						goto IL_0311;
					case '#':
						goto IL_0136;
					case '%':
						goto IL_026E;
					case '&':
						goto IL_0283;
					case '\'':
						goto IL_0148;
					case '(':
						goto IL_011C;
					case ')':
						goto IL_0129;
					case '*':
						goto IL_0244;
					case '+':
						goto IL_021A;
					case '-':
						goto IL_022F;
					case '/':
						goto IL_0259;
					}
					goto Block_5;
				}
				if (c != '\0')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_0111;
					}
					break;
				}
				goto IL_0104;
				IL_0111:
				this.ScanWhite();
			}
			goto IL_0311;
			Block_5:
			switch (c)
			{
			case '<':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 11;
					goto IL_03E5;
				}
				if (text[this._pos] == '>')
				{
					this._pos++;
					this._op = 12;
					goto IL_03E5;
				}
				this._op = 9;
				goto IL_03E5;
			case '=':
				this._token = Tokens.BinaryOp;
				this._op = 7;
				goto IL_03E5;
			case '>':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 10;
					goto IL_03E5;
				}
				this._op = 8;
				goto IL_03E5;
			default:
				goto IL_0311;
			}
			IL_00CD:
			if (c <= '^')
			{
				if (c == '[')
				{
					this.ScanName(']', this._escape, "]\\");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c != '^')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 24;
				goto IL_03E5;
			}
			else
			{
				if (c == '`')
				{
					this.ScanName('`', '`', "`");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c == '|')
				{
					this._token = Tokens.BinaryOp;
					this._op = 23;
					goto IL_03E5;
				}
				if (c != '~')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 25;
				goto IL_03E5;
			}
			IL_0104:
			this._token = Tokens.EOS;
			goto IL_03E5;
			IL_011C:
			this._token = Tokens.LeftParen;
			goto IL_03E5;
			IL_0129:
			this._token = Tokens.RightParen;
			goto IL_03E5;
			IL_0136:
			this.ScanDate();
			this.CheckToken(Tokens.Date);
			goto IL_03E5;
			IL_0148:
			this.ScanString('\'');
			this.CheckToken(Tokens.StringConst);
			goto IL_03E5;
			IL_021A:
			this._token = Tokens.BinaryOp;
			this._op = 15;
			goto IL_03E5;
			IL_022F:
			this._token = Tokens.BinaryOp;
			this._op = 16;
			goto IL_03E5;
			IL_0244:
			this._token = Tokens.BinaryOp;
			this._op = 17;
			goto IL_03E5;
			IL_0259:
			this._token = Tokens.BinaryOp;
			this._op = 18;
			goto IL_03E5;
			IL_026E:
			this._token = Tokens.BinaryOp;
			this._op = 20;
			goto IL_03E5;
			IL_0283:
			this._token = Tokens.BinaryOp;
			this._op = 22;
			goto IL_03E5;
			IL_0311:
			if (c == this._listSeparator)
			{
				this._token = Tokens.ListSeparator;
			}
			else if (c == '.')
			{
				if (this._prevOperand == 0)
				{
					this.ScanNumeric();
				}
				else
				{
					this._token = Tokens.Dot;
				}
			}
			else if (c == '0' && (text[this._pos] == 'x' || text[this._pos] == 'X'))
			{
				this.ScanBinaryConstant();
				this._token = Tokens.BinaryConst;
			}
			else if (this.IsDigit(c))
			{
				this.ScanNumeric();
			}
			else
			{
				this.ScanReserved();
				if (this._token == Tokens.None)
				{
					if (this.IsAlphaNumeric(c))
					{
						this.ScanName();
						if (this._token != Tokens.None)
						{
							this.CheckToken(Tokens.Name);
							goto IL_03E5;
						}
					}
					this._token = Tokens.Unknown;
					throw ExprException.UnknownToken(new string(text, this._start, this._pos - this._start), this._start + 1);
				}
			}
			IL_03E5:
			return this._token;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002F474 File Offset: 0x0002D674
		private void ScanNumeric()
		{
			char[] text = this._text;
			bool flag = false;
			bool flag2 = false;
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._decimalSeparator)
			{
				flag = true;
				this._pos++;
			}
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._exponentL || text[this._pos] == this._exponentU)
			{
				flag2 = true;
				this._pos++;
				if (text[this._pos] == '-' || text[this._pos] == '+')
				{
					this._pos++;
				}
				while (this.IsDigit(text[this._pos]))
				{
					this._pos++;
				}
			}
			if (flag2)
			{
				this._token = Tokens.Float;
				return;
			}
			if (flag)
			{
				this._token = Tokens.Decimal;
				return;
			}
			this._token = Tokens.Numeric;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002F580 File Offset: 0x0002D780
		private void ScanName()
		{
			char[] text = this._text;
			while (this.IsAlphaNumeric(text[this._pos]))
			{
				this._pos++;
			}
			this._token = Tokens.Name;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		private void ScanName(char chEnd, char esc, string charsToEscape)
		{
			char[] text = this._text;
			do
			{
				if (text[this._pos] == esc && this._pos + 1 < text.Length && charsToEscape.IndexOf(text[this._pos + 1]) >= 0)
				{
					this._pos++;
				}
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != chEnd);
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidNameBracketing(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._pos++;
			this._token = Tokens.Name;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002F670 File Offset: 0x0002D870
		private void ScanDate()
		{
			char[] text = this._text;
			do
			{
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != '#');
			if (this._pos < text.Length && text[this._pos] == '#')
			{
				this._token = Tokens.Date;
				this._pos++;
				return;
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidDate(new string(text, this._start, this._pos - 1 - this._start));
			}
			throw ExprException.InvalidDate(new string(text, this._start, this._pos - this._start));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0002F720 File Offset: 0x0002D920
		private void ScanBinaryConstant()
		{
			char[] text = this._text;
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002F72C File Offset: 0x0002D92C
		private void ScanReserved()
		{
			char[] text = this._text;
			if (this.IsAlpha(text[this._pos]))
			{
				this.ScanName();
				string text2 = new string(text, this._start, this._pos - this._start);
				CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
				int num = 0;
				int num2 = ExpressionParser.s_reservedwords.Length - 1;
				int num3;
				for (;;)
				{
					num3 = (num + num2) / 2;
					int num4 = compareInfo.Compare(ExpressionParser.s_reservedwords[num3]._word, text2, CompareOptions.IgnoreCase);
					if (num4 == 0)
					{
						break;
					}
					if (num4 < 0)
					{
						num = num3 + 1;
					}
					else
					{
						num2 = num3 - 1;
					}
					if (num > num2)
					{
						return;
					}
				}
				this._token = ExpressionParser.s_reservedwords[num3]._token;
				this._op = ExpressionParser.s_reservedwords[num3]._op;
				return;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002F7F8 File Offset: 0x0002D9F8
		private void ScanString(char escape)
		{
			char[] text = this._text;
			while (this._pos < text.Length)
			{
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				char c = array[pos];
				if (c == escape && this._pos < text.Length && text[this._pos] == escape)
				{
					this._pos++;
				}
				else if (c == escape)
				{
					break;
				}
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidString(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._token = Tokens.StringConst;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002F88E File Offset: 0x0002DA8E
		internal void ScanToken(Tokens token)
		{
			this.Scan();
			this.CheckToken(token);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
		private void ScanWhite()
		{
			char[] text = this._text;
			while (this._pos < text.Length && this.IsWhiteSpace(text[this._pos]))
			{
				this._pos++;
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002F8DF File Offset: 0x0002DADF
		private bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && ch > '\0';
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002F8EC File Offset: 0x0002DAEC
		private bool IsAlphaNumeric(char ch)
		{
			switch (ch)
			{
			case '$':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return ch > '\u007f';
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002FA6A File Offset: 0x0002DC6A
		private bool IsDigit(char ch)
		{
			switch (ch)
			{
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
		private bool IsAlpha(char ch)
		{
			switch (ch)
			{
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return false;
		}

		// Token: 0x04000698 RID: 1688
		private const int Empty = 0;

		// Token: 0x04000699 RID: 1689
		private const int Scalar = 1;

		// Token: 0x0400069A RID: 1690
		private const int Expr = 2;

		// Token: 0x0400069B RID: 1691
		private static readonly ExpressionParser.ReservedWords[] s_reservedwords = new ExpressionParser.ReservedWords[]
		{
			new ExpressionParser.ReservedWords("And", Tokens.BinaryOp, 26),
			new ExpressionParser.ReservedWords("Between", Tokens.BinaryOp, 6),
			new ExpressionParser.ReservedWords("Child", Tokens.Child, 0),
			new ExpressionParser.ReservedWords("False", Tokens.ZeroOp, 34),
			new ExpressionParser.ReservedWords("In", Tokens.BinaryOp, 5),
			new ExpressionParser.ReservedWords("Is", Tokens.BinaryOp, 13),
			new ExpressionParser.ReservedWords("Like", Tokens.BinaryOp, 14),
			new ExpressionParser.ReservedWords("Not", Tokens.UnaryOp, 3),
			new ExpressionParser.ReservedWords("Null", Tokens.ZeroOp, 32),
			new ExpressionParser.ReservedWords("Or", Tokens.BinaryOp, 27),
			new ExpressionParser.ReservedWords("Parent", Tokens.Parent, 0),
			new ExpressionParser.ReservedWords("True", Tokens.ZeroOp, 33)
		};

		// Token: 0x0400069C RID: 1692
		private char _escape = '\\';

		// Token: 0x0400069D RID: 1693
		private char _decimalSeparator = '.';

		// Token: 0x0400069E RID: 1694
		private char _listSeparator = ',';

		// Token: 0x0400069F RID: 1695
		private char _exponentL = 'e';

		// Token: 0x040006A0 RID: 1696
		private char _exponentU = 'E';

		// Token: 0x040006A1 RID: 1697
		internal char[] _text;

		// Token: 0x040006A2 RID: 1698
		internal int _pos;

		// Token: 0x040006A3 RID: 1699
		internal int _start;

		// Token: 0x040006A4 RID: 1700
		internal Tokens _token;

		// Token: 0x040006A5 RID: 1701
		internal int _op;

		// Token: 0x040006A6 RID: 1702
		internal OperatorInfo[] _ops = new OperatorInfo[100];

		// Token: 0x040006A7 RID: 1703
		internal int _topOperator;

		// Token: 0x040006A8 RID: 1704
		internal int _topNode;

		// Token: 0x040006A9 RID: 1705
		private readonly DataTable _table;

		// Token: 0x040006AA RID: 1706
		private const int MaxPredicates = 100;

		// Token: 0x040006AB RID: 1707
		internal ExpressionNode[] _nodeStack = new ExpressionNode[100];

		// Token: 0x040006AC RID: 1708
		internal int _prevOperand;

		// Token: 0x040006AD RID: 1709
		internal ExpressionNode _expression;

		// Token: 0x0200009A RID: 154
		private readonly struct ReservedWords
		{
			// Token: 0x06000A47 RID: 2631 RVA: 0x0002FCBB File Offset: 0x0002DEBB
			internal ReservedWords(string word, Tokens token, int op)
			{
				this._word = word;
				this._token = token;
				this._op = op;
			}

			// Token: 0x040006AE RID: 1710
			internal readonly string _word;

			// Token: 0x040006AF RID: 1711
			internal readonly Tokens _token;

			// Token: 0x040006B0 RID: 1712
			internal readonly int _op;
		}
	}
}
