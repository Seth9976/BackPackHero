using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200018E RID: 398
	public class EvaluationVisitor : LogicalExpressionVisitor
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0001303E File Offset: 0x0001123E
		public EvaluationVisitor(Flow flow, EvaluateOptions options)
		{
			this.flow = flow;
			this.options = options;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000A86 RID: 2694 RVA: 0x00013054 File Offset: 0x00011254
		// (remove) Token: 0x06000A87 RID: 2695 RVA: 0x0001308C File Offset: 0x0001128C
		public event EvaluateFunctionHandler EvaluateFunction;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000A88 RID: 2696 RVA: 0x000130C4 File Offset: 0x000112C4
		// (remove) Token: 0x06000A89 RID: 2697 RVA: 0x000130FC File Offset: 0x000112FC
		public event EvaluateParameterHandler EvaluateParameter;

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x00013131 File Offset: 0x00011331
		private bool IgnoreCase
		{
			get
			{
				return this.options.HasFlag(EvaluateOptions.IgnoreCase);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00013149 File Offset: 0x00011349
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00013151 File Offset: 0x00011351
		public object Result { get; private set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001315A File Offset: 0x0001135A
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00013162 File Offset: 0x00011362
		public Dictionary<string, object> Parameters { get; set; }

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001316B File Offset: 0x0001136B
		private object Evaluate(LogicalExpression expression)
		{
			expression.Accept(this);
			return this.Result;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001317A File Offset: 0x0001137A
		public override void Visit(TernaryExpression ternary)
		{
			ternary.LeftExpression.Accept(this);
			if (ConversionUtility.Convert<bool>(this.Result))
			{
				ternary.MiddleExpression.Accept(this);
				return;
			}
			ternary.RightExpression.Accept(this);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000131B0 File Offset: 0x000113B0
		public override void Visit(BinaryExpression binary)
		{
			object leftValue = null;
			EvaluationVisitor.Func<object> func = delegate
			{
				if (leftValue == null)
				{
					binary.LeftExpression.Accept(this);
					leftValue = this.Result;
				}
				return leftValue;
			};
			object rightValue = null;
			EvaluationVisitor.Func<object> func2 = delegate
			{
				if (rightValue == null)
				{
					binary.RightExpression.Accept(this);
					rightValue = this.Result;
				}
				return rightValue;
			};
			switch (binary.Type)
			{
			case BinaryExpressionType.And:
				this.Result = ConversionUtility.Convert<bool>(func()) && ConversionUtility.Convert<bool>(func2());
				return;
			case BinaryExpressionType.Or:
				this.Result = ConversionUtility.Convert<bool>(func()) || ConversionUtility.Convert<bool>(func2());
				return;
			case BinaryExpressionType.NotEqual:
				this.Result = OperatorUtility.NotEqual(func(), func2());
				return;
			case BinaryExpressionType.LesserOrEqual:
				this.Result = OperatorUtility.LessThanOrEqual(func(), func2());
				return;
			case BinaryExpressionType.GreaterOrEqual:
				this.Result = OperatorUtility.GreaterThanOrEqual(func(), func2());
				return;
			case BinaryExpressionType.Lesser:
				this.Result = OperatorUtility.LessThan(func(), func2());
				return;
			case BinaryExpressionType.Greater:
				this.Result = OperatorUtility.GreaterThan(func(), func2());
				return;
			case BinaryExpressionType.Equal:
				this.Result = OperatorUtility.Equal(func(), func2());
				return;
			case BinaryExpressionType.Minus:
				this.Result = OperatorUtility.Subtract(func(), func2());
				return;
			case BinaryExpressionType.Plus:
				this.Result = OperatorUtility.Add(func(), func2());
				return;
			case BinaryExpressionType.Modulo:
				this.Result = OperatorUtility.Modulo(func(), func2());
				return;
			case BinaryExpressionType.Div:
				this.Result = OperatorUtility.Divide(func(), func2());
				return;
			case BinaryExpressionType.Times:
				this.Result = OperatorUtility.Multiply(func(), func2());
				return;
			case BinaryExpressionType.BitwiseOr:
				this.Result = OperatorUtility.Or(func(), func2());
				return;
			case BinaryExpressionType.BitwiseAnd:
				this.Result = OperatorUtility.And(func(), func2());
				return;
			case BinaryExpressionType.BitwiseXOr:
				this.Result = OperatorUtility.ExclusiveOr(func(), func2());
				return;
			case BinaryExpressionType.LeftShift:
				this.Result = OperatorUtility.LeftShift(func(), func2());
				return;
			case BinaryExpressionType.RightShift:
				this.Result = OperatorUtility.RightShift(func(), func2());
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00013440 File Offset: 0x00011640
		public override void Visit(UnaryExpression unary)
		{
			unary.Expression.Accept(this);
			switch (unary.Type)
			{
			case UnaryExpressionType.Not:
				this.Result = !ConversionUtility.Convert<bool>(this.Result);
				return;
			case UnaryExpressionType.Negate:
				this.Result = OperatorUtility.Negate(this.Result);
				return;
			case UnaryExpressionType.BitwiseNot:
				this.Result = OperatorUtility.Not(this.Result);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000134B0 File Offset: 0x000116B0
		public override void Visit(ValueExpression value)
		{
			this.Result = value.Value;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x000134C0 File Offset: 0x000116C0
		public override void Visit(FunctionExpression function)
		{
			FunctionArgs functionArgs = new FunctionArgs
			{
				Parameters = new Expression[function.Expressions.Length]
			};
			for (int i = 0; i < function.Expressions.Length; i++)
			{
				functionArgs.Parameters[i] = new Expression(function.Expressions[i], this.options);
				functionArgs.Parameters[i].EvaluateFunction += this.EvaluateFunction;
				functionArgs.Parameters[i].EvaluateParameter += this.EvaluateParameter;
				functionArgs.Parameters[i].Parameters = this.Parameters;
			}
			this.OnEvaluateFunction(this.IgnoreCase ? function.Identifier.Name.ToLower() : function.Identifier.Name, functionArgs);
			if (functionArgs.HasResult)
			{
				this.Result = functionArgs.Result;
				return;
			}
			string text = function.Identifier.Name.ToLower(CultureInfo.InvariantCulture);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1659167240U)
			{
				if (num <= 1006755615U)
				{
					if (num <= 213683108U)
					{
						if (num != 108579519U)
						{
							if (num == 213683108U)
							{
								if (text == "sign")
								{
									this.CheckCase(function, "Sign");
									EvaluationVisitor.CheckExactArgumentCount(function, 1);
									this.Result = Mathf.Sign(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
									return;
								}
							}
						}
						else if (text == "atan")
						{
							this.CheckCase(function, "Atan");
							EvaluationVisitor.CheckExactArgumentCount(function, 1);
							this.Result = Mathf.Atan(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
							return;
						}
					}
					else if (num != 709362235U)
					{
						if (num == 1006755615U)
						{
							if (text == "acos")
							{
								this.CheckCase(function, "Acos");
								EvaluationVisitor.CheckExactArgumentCount(function, 1);
								this.Result = Mathf.Acos(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
								return;
							}
						}
					}
					else if (text == "abs")
					{
						this.CheckCase(function, "Abs");
						EvaluationVisitor.CheckExactArgumentCount(function, 1);
						this.Result = Mathf.Abs(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
						return;
					}
				}
				else if (num <= 1094220446U)
				{
					if (num != 1062293841U)
					{
						if (num == 1094220446U)
						{
							if (text == "in")
							{
								this.CheckCase(function, "In");
								EvaluationVisitor.CheckExactArgumentCount(function, 2);
								object obj = this.Evaluate(function.Expressions[0]);
								bool flag = false;
								for (int j = 1; j < function.Expressions.Length; j++)
								{
									object obj2 = this.Evaluate(function.Expressions[j]);
									if (object.Equals(obj, obj2))
									{
										flag = true;
										break;
									}
								}
								this.Result = flag;
								return;
							}
						}
					}
					else if (text == "log")
					{
						this.CheckCase(function, "Log");
						EvaluationVisitor.CheckExactArgumentCount(function, 2);
						this.Result = Mathf.Log(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])), ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[1])));
						return;
					}
				}
				else if (num != 1326178875U)
				{
					if (num != 1479764693U)
					{
						if (num == 1659167240U)
						{
							if (text == "ceil")
							{
								this.CheckCase(function, "Ceil");
								EvaluationVisitor.CheckExactArgumentCount(function, 1);
								this.Result = Mathf.Ceil(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
								return;
							}
						}
					}
					else if (text == "pow")
					{
						this.CheckCase(function, "Pow");
						EvaluationVisitor.CheckExactArgumentCount(function, 2);
						this.Result = Mathf.Pow(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])), ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[1])));
						return;
					}
				}
				else if (text == "round")
				{
					this.CheckCase(function, "Round");
					EvaluationVisitor.CheckExactArgumentCount(function, 1);
					this.Result = Mathf.Round(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
					return;
				}
			}
			else if (num <= 3102149661U)
			{
				if (num <= 2112764879U)
				{
					if (num != 1923516200U)
					{
						if (num == 2112764879U)
						{
							if (text == "sqrt")
							{
								this.CheckCase(function, "Sqrt");
								EvaluationVisitor.CheckExactArgumentCount(function, 1);
								this.Result = Mathf.Sqrt(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
								return;
							}
						}
					}
					else if (text == "exp")
					{
						this.CheckCase(function, "Exp");
						EvaluationVisitor.CheckExactArgumentCount(function, 1);
						this.Result = Mathf.Exp(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
						return;
					}
				}
				else if (num != 2346846000U)
				{
					if (num != 2633446552U)
					{
						if (num == 3102149661U)
						{
							if (text == "floor")
							{
								this.CheckCase(function, "Floor");
								EvaluationVisitor.CheckExactArgumentCount(function, 1);
								this.Result = Mathf.Floor(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
								return;
							}
						}
					}
					else if (text == "tan")
					{
						this.CheckCase(function, "Tan");
						EvaluationVisitor.CheckExactArgumentCount(function, 1);
						this.Result = Mathf.Tan(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
						return;
					}
				}
				else if (text == "log10")
				{
					this.CheckCase(function, "Log10");
					EvaluationVisitor.CheckExactArgumentCount(function, 1);
					this.Result = Mathf.Log10(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
					return;
				}
			}
			else if (num <= 3617776409U)
			{
				if (num != 3381609815U)
				{
					if (num == 3617776409U)
					{
						if (text == "max")
						{
							this.CheckCase(function, "Max");
							EvaluationVisitor.CheckExactArgumentCount(function, 2);
							this.Result = Mathf.Max(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])), ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[1])));
							return;
						}
					}
				}
				else if (text == "min")
				{
					this.CheckCase(function, "Min");
					EvaluationVisitor.CheckExactArgumentCount(function, 2);
					this.Result = Mathf.Min(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])), ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[1])));
					return;
				}
			}
			else if (num != 3761252941U)
			{
				if (num != 4220379804U)
				{
					if (num == 4272848550U)
					{
						if (text == "asin")
						{
							this.CheckCase(function, "Asin");
							EvaluationVisitor.CheckExactArgumentCount(function, 1);
							this.Result = Mathf.Asin(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
							return;
						}
					}
				}
				else if (text == "cos")
				{
					this.CheckCase(function, "Cos");
					EvaluationVisitor.CheckExactArgumentCount(function, 1);
					this.Result = Mathf.Cos(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
					return;
				}
			}
			else if (text == "sin")
			{
				this.CheckCase(function, "Sin");
				EvaluationVisitor.CheckExactArgumentCount(function, 1);
				this.Result = Mathf.Sin(ConversionUtility.Convert<float>(this.Evaluate(function.Expressions[0])));
				return;
			}
			throw new ArgumentException("Function not found", function.Identifier.Name);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00013D40 File Offset: 0x00011F40
		private void CheckCase(FunctionExpression function, string reference)
		{
			string name = function.Identifier.Name;
			if (this.IgnoreCase)
			{
				if (string.Equals(name, reference, StringComparison.InvariantCultureIgnoreCase))
				{
					return;
				}
				throw new ArgumentException("Function not found.", name);
			}
			else
			{
				if (name != reference)
				{
					throw new ArgumentException(string.Concat(new string[] { "Function not found: '", name, "'. Try '", reference, "' instead." }));
				}
				return;
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00013DB2 File Offset: 0x00011FB2
		private void OnEvaluateFunction(string name, FunctionArgs args)
		{
			EvaluateFunctionHandler evaluateFunction = this.EvaluateFunction;
			if (evaluateFunction == null)
			{
				return;
			}
			evaluateFunction(this.flow, name, args);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00013DCC File Offset: 0x00011FCC
		public override void Visit(IdentifierExpression identifier)
		{
			if (this.Parameters.ContainsKey(identifier.Name))
			{
				if (this.Parameters[identifier.Name] is Expression)
				{
					Expression expression = (Expression)this.Parameters[identifier.Name];
					foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
					{
						expression.Parameters[keyValuePair.Key] = keyValuePair.Value;
					}
					expression.EvaluateFunction += this.EvaluateFunction;
					expression.EvaluateParameter += this.EvaluateParameter;
					this.Result = ((Expression)this.Parameters[identifier.Name]).Evaluate(this.flow);
					return;
				}
				this.Result = this.Parameters[identifier.Name];
				return;
			}
			else
			{
				ParameterArgs parameterArgs = new ParameterArgs();
				this.OnEvaluateParameter(identifier.Name, parameterArgs);
				if (!parameterArgs.HasResult)
				{
					throw new ArgumentException("Parameter was not defined", identifier.Name);
				}
				this.Result = parameterArgs.Result;
				return;
			}
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00013F0C File Offset: 0x0001210C
		private void OnEvaluateParameter(string name, ParameterArgs args)
		{
			EvaluateParameterHandler evaluateParameter = this.EvaluateParameter;
			if (evaluateParameter == null)
			{
				return;
			}
			evaluateParameter(this.flow, name, args);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00013F26 File Offset: 0x00012126
		public static void CheckExactArgumentCount(FunctionExpression function, int count)
		{
			if (function.Expressions.Length != count)
			{
				throw new ArgumentException(string.Format("{0}() takes at exactly {1} arguments. {2} provided.", function.Identifier.Name, count, function.Expressions.Length));
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00013F61 File Offset: 0x00012161
		public static void CheckMinArgumentCount(FunctionExpression function, int count)
		{
			if (function.Expressions.Length < count)
			{
				throw new ArgumentException(string.Format("{0}() takes at at least {1} arguments. {2} provided.", function.Identifier.Name, count, function.Expressions.Length));
			}
		}

		// Token: 0x04000256 RID: 598
		private readonly Flow flow;

		// Token: 0x04000257 RID: 599
		private readonly EvaluateOptions options;

		// Token: 0x020001E9 RID: 489
		// (Invoke) Token: 0x06000C8B RID: 3211
		private delegate T Func<T>();
	}
}
