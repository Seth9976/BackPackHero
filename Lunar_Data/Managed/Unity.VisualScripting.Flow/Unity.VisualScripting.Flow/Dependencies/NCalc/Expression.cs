using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x0200018F RID: 399
	public class Expression
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00013F9C File Offset: 0x0001219C
		private Expression()
		{
			this.Parameters["null"] = (this.Parameters["NULL"] = null);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00013FD3 File Offset: 0x000121D3
		public Expression(string expression, EvaluateOptions options = EvaluateOptions.None)
			: this()
		{
			if (string.IsNullOrEmpty(expression))
			{
				throw new ArgumentException("Expression can't be empty", "expression");
			}
			expression = expression.Replace('"', '\'');
			this.OriginalExpression = expression;
			this.Options = options;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001400D File Offset: 0x0001220D
		public Expression(LogicalExpression expression, EvaluateOptions options = EvaluateOptions.None)
			: this()
		{
			if (expression == null)
			{
				throw new ArgumentException("Expression can't be null", "expression");
			}
			this.ParsedExpression = expression;
			this.Options = options;
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000A9E RID: 2718 RVA: 0x00014038 File Offset: 0x00012238
		// (remove) Token: 0x06000A9F RID: 2719 RVA: 0x00014070 File Offset: 0x00012270
		public event EvaluateFunctionHandler EvaluateFunction;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000AA0 RID: 2720 RVA: 0x000140A8 File Offset: 0x000122A8
		// (remove) Token: 0x06000AA1 RID: 2721 RVA: 0x000140E0 File Offset: 0x000122E0
		public event EvaluateParameterHandler EvaluateParameter;

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00014115 File Offset: 0x00012315
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0001411D File Offset: 0x0001231D
		public EvaluateOptions Options { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00014126 File Offset: 0x00012326
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001412E File Offset: 0x0001232E
		public string Error { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00014137 File Offset: 0x00012337
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0001413F File Offset: 0x0001233F
		public LogicalExpression ParsedExpression { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00014148 File Offset: 0x00012348
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0001416D File Offset: 0x0001236D
		public Dictionary<string, object> Parameters
		{
			get
			{
				Dictionary<string, object> dictionary;
				if ((dictionary = this._parameters) == null)
				{
					dictionary = (this._parameters = new Dictionary<string, object>());
				}
				return dictionary;
			}
			set
			{
				this._parameters = value;
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00014178 File Offset: 0x00012378
		public void UpdateUnityTimeParameters()
		{
			this.Parameters["dt"] = (this.Parameters["DT"] = Time.deltaTime);
			this.Parameters["second"] = (this.Parameters["Second"] = 1f / Time.deltaTime);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000141E8 File Offset: 0x000123E8
		public bool HasErrors()
		{
			bool flag;
			try
			{
				if (this.ParsedExpression == null)
				{
					this.ParsedExpression = Expression.Compile(this.OriginalExpression, (this.Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);
				}
				flag = this.ParsedExpression != null && this.Error != null;
			}
			catch (Exception ex)
			{
				this.Error = ex.Message;
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00014254 File Offset: 0x00012454
		public object Evaluate(Flow flow)
		{
			if (this.HasErrors())
			{
				throw new EvaluationException(this.Error);
			}
			if (this.ParsedExpression == null)
			{
				this.ParsedExpression = Expression.Compile(this.OriginalExpression, (this.Options & EvaluateOptions.NoCache) == EvaluateOptions.NoCache);
			}
			EvaluationVisitor evaluationVisitor = new EvaluationVisitor(flow, this.Options);
			evaluationVisitor.EvaluateFunction += this.EvaluateFunction;
			evaluationVisitor.EvaluateParameter += this.EvaluateParameter;
			evaluationVisitor.Parameters = this.Parameters;
			if ((this.Options & EvaluateOptions.IterateParameters) == EvaluateOptions.IterateParameters)
			{
				int num = -1;
				this.ParameterEnumerators = new Dictionary<string, IEnumerator>();
				foreach (object obj in this.Parameters.Values)
				{
					IEnumerable enumerable = obj as IEnumerable;
					if (enumerable != null)
					{
						int num2 = 0;
						foreach (object obj2 in enumerable)
						{
							num2++;
						}
						if (num == -1)
						{
							num = num2;
						}
						else if (num2 != num)
						{
							throw new EvaluationException("When IterateParameters option is used, IEnumerable parameters must have the same number of items.");
						}
					}
				}
				foreach (string text in this.Parameters.Keys)
				{
					IEnumerable enumerable2 = this.Parameters[text] as IEnumerable;
					if (enumerable2 != null)
					{
						this.ParameterEnumerators.Add(text, enumerable2.GetEnumerator());
					}
				}
				List<object> list = new List<object>();
				for (int i = 0; i < num; i++)
				{
					foreach (string text2 in this.ParameterEnumerators.Keys)
					{
						IEnumerator enumerator5 = this.ParameterEnumerators[text2];
						enumerator5.MoveNext();
						this.Parameters[text2] = enumerator5.Current;
					}
					this.ParsedExpression.Accept(evaluationVisitor);
					list.Add(evaluationVisitor.Result);
				}
				return list;
			}
			this.ParsedExpression.Accept(evaluationVisitor);
			return evaluationVisitor.Result;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000144B4 File Offset: 0x000126B4
		public static LogicalExpression Compile(string expression, bool noCache)
		{
			LogicalExpression logicalExpression = null;
			if (Expression._cacheEnabled && !noCache)
			{
				try
				{
					Expression.Rwl.AcquireReaderLock(-1);
					if (Expression._compiledExpressions.ContainsKey(expression))
					{
						WeakReference weakReference = Expression._compiledExpressions[expression];
						logicalExpression = weakReference.Target as LogicalExpression;
						if (weakReference.IsAlive && logicalExpression != null)
						{
							return logicalExpression;
						}
					}
				}
				finally
				{
					Expression.Rwl.ReleaseReaderLock();
				}
			}
			if (logicalExpression == null)
			{
				NCalcParser ncalcParser = new NCalcParser(new CommonTokenStream(new NCalcLexer(new ANTLRStringStream(expression))));
				logicalExpression = ncalcParser.ncalcExpression().value;
				if (ncalcParser.Errors != null && ncalcParser.Errors.Count > 0)
				{
					throw new EvaluationException(string.Join(Environment.NewLine, ncalcParser.Errors.ToArray()));
				}
				if (Expression._cacheEnabled && !noCache)
				{
					try
					{
						Expression.Rwl.AcquireWriterLock(-1);
						Expression._compiledExpressions[expression] = new WeakReference(logicalExpression);
					}
					finally
					{
						Expression.Rwl.ReleaseWriterLock();
					}
					Expression.CleanCache();
				}
			}
			return logicalExpression;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000145CC File Offset: 0x000127CC
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x000145D3 File Offset: 0x000127D3
		public static bool CacheEnabled
		{
			get
			{
				return Expression._cacheEnabled;
			}
			set
			{
				Expression._cacheEnabled = value;
				if (!Expression.CacheEnabled)
				{
					Expression._compiledExpressions = new Dictionary<string, WeakReference>();
				}
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000145EC File Offset: 0x000127EC
		private static void CleanCache()
		{
			List<string> list = new List<string>();
			try
			{
				Expression.Rwl.AcquireWriterLock(-1);
				foreach (KeyValuePair<string, WeakReference> keyValuePair in Expression._compiledExpressions)
				{
					if (!keyValuePair.Value.IsAlive)
					{
						list.Add(keyValuePair.Key);
					}
				}
				foreach (string text in list)
				{
					Expression._compiledExpressions.Remove(text);
				}
			}
			finally
			{
				Expression.Rwl.ReleaseReaderLock();
			}
		}

		// Token: 0x0400025C RID: 604
		protected readonly string OriginalExpression;

		// Token: 0x0400025D RID: 605
		protected Dictionary<string, IEnumerator> ParameterEnumerators;

		// Token: 0x0400025E RID: 606
		private Dictionary<string, object> _parameters;

		// Token: 0x04000262 RID: 610
		private static bool _cacheEnabled = true;

		// Token: 0x04000263 RID: 611
		private static Dictionary<string, WeakReference> _compiledExpressions = new Dictionary<string, WeakReference>();

		// Token: 0x04000264 RID: 612
		private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();
	}
}
