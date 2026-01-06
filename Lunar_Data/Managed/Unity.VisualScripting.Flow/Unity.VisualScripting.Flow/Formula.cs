using System;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unity.VisualScripting
{
	// Token: 0x020000A9 RID: 169
	public sealed class Formula : MultiInputUnit<object>
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000A328 File Offset: 0x00008528
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000A330 File Offset: 0x00008530
		[DoNotSerialize]
		[Inspectable]
		[UnitHeaderInspectable]
		[InspectorTextArea]
		public string formula
		{
			get
			{
				return this._formula;
			}
			set
			{
				this._formula = value;
				this.InitializeNCalc();
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0000A33F File Offset: 0x0000853F
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0000A347 File Offset: 0x00008547
		[Serialize]
		[Inspectable(order = 2147483647)]
		[InspectorExpandTooltip]
		public bool cacheArguments { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0000A350 File Offset: 0x00008550
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x0000A358 File Offset: 0x00008558
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput result { get; private set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0000A361 File Offset: 0x00008561
		protected override int minInputCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000A364 File Offset: 0x00008564
		protected override void Definition()
		{
			base.Definition();
			this.result = base.ValueOutput<object>("result", new Func<Flow, object>(this.Evaluate));
			base.InputsAllowNull();
			foreach (ValueInput valueInput in base.multiInputs)
			{
				base.Requirement(valueInput, this.result);
			}
			this.InitializeNCalc();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000A3E8 File Offset: 0x000085E8
		private void InitializeNCalc()
		{
			if (string.IsNullOrEmpty(this.formula))
			{
				this.ncalc = null;
				return;
			}
			this.ncalc = new Expression(this.formula, EvaluateOptions.None);
			this.ncalc.Options = EvaluateOptions.IgnoreCase;
			this.ncalc.EvaluateParameter += this.EvaluateTreeParameter;
			this.ncalc.EvaluateFunction += this.EvaluateTreeFunction;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000A456 File Offset: 0x00008656
		private object Evaluate(Flow flow)
		{
			if (this.ncalc == null)
			{
				throw new InvalidOperationException("No formula provided.");
			}
			this.ncalc.UpdateUnityTimeParameters();
			return this.ncalc.Evaluate(flow);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000A484 File Offset: 0x00008684
		private void EvaluateTreeFunction(Flow flow, string name, FunctionArgs args)
		{
			if (name == "v2" || name == "V2")
			{
				if (args.Parameters.Length != 2)
				{
					throw new ArgumentException(string.Format("v2() takes at exactly 2 arguments. {0} provided.", args.Parameters.Length));
				}
				args.Result = new Vector2(ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)));
				return;
			}
			else
			{
				if (!(name == "v3") && !(name == "V3"))
				{
					if (name == "v4" || name == "V4")
					{
						if (args.Parameters.Length != 4)
						{
							throw new ArgumentException(string.Format("v4() takes at exactly 4 arguments. {0} provided.", args.Parameters.Length));
						}
						args.Result = new Vector4(ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[2].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[3].Evaluate(flow)));
					}
					return;
				}
				if (args.Parameters.Length != 3)
				{
					throw new ArgumentException(string.Format("v3() takes at exactly 3 arguments. {0} provided.", args.Parameters.Length));
				}
				args.Result = new Vector3(ConversionUtility.Convert<float>(args.Parameters[0].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[1].Evaluate(flow)), ConversionUtility.Convert<float>(args.Parameters[2].Evaluate(flow)));
				return;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000A638 File Offset: 0x00008838
		public object GetParameterValue(Flow flow, string name)
		{
			if (name.Length == 1)
			{
				char c = name[0];
				if (char.IsLetter(c))
				{
					c = char.ToLower(c);
					int argumentIndex = Formula.GetArgumentIndex(c);
					if (argumentIndex < base.multiInputs.Count)
					{
						ValueInput valueInput = base.multiInputs[argumentIndex];
						if (this.cacheArguments && !flow.IsLocal(valueInput))
						{
							flow.SetValue(valueInput, flow.GetValue<object>(valueInput));
						}
						return flow.GetValue<object>(valueInput);
					}
				}
			}
			else
			{
				if (Variables.Graph(flow.stack).IsDefined(name))
				{
					return Variables.Graph(flow.stack).Get(name);
				}
				GameObject self = flow.stack.self;
				if (self != null && Variables.Object(self).IsDefined(name))
				{
					return Variables.Object(self).Get(name);
				}
				Scene? scene = flow.stack.scene;
				if (scene != null && Variables.Scene(scene).IsDefined(name))
				{
					return Variables.Scene(scene).Get(name);
				}
				if (Variables.Application.IsDefined(name))
				{
					return Variables.Application.Get(name);
				}
				if (Variables.Saved.IsDefined(name))
				{
					return Variables.Saved.Get(name);
				}
			}
			throw new InvalidOperationException("Unknown expression tree parameter: '" + name + "'.\nSupported parameter names are alphabetical indices and variable names.");
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000A784 File Offset: 0x00008984
		private void EvaluateTreeParameter(Flow flow, string name, ParameterArgs args)
		{
			if (!name.Contains("."))
			{
				args.Result = this.GetParameterValue(flow, name);
				return;
			}
			string[] array = name.Split('.', StringSplitOptions.None);
			if (array.Length != 2)
			{
				throw new InvalidOperationException("Cannot parse expression tree parameter: [" + name + "]");
			}
			string text = array[0];
			string text2 = array[1].TrimEnd("()");
			object parameterValue = this.GetParameterValue(flow, text);
			Member member = new Member(parameterValue.GetType(), text2, Type.EmptyTypes);
			object obj = parameterValue;
			if (member.isInvocable)
			{
				args.Result = member.Invoke(obj);
				return;
			}
			if (member.isGettable)
			{
				args.Result = member.Get(obj);
				return;
			}
			throw new InvalidOperationException(string.Concat(new string[] { "Cannot get or invoke expression tree parameter: [", text, ".", text2, "]" }));
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000A868 File Offset: 0x00008A68
		public static string GetArgumentName(int index)
		{
			if (index > 25)
			{
				throw new NotImplementedException("Argument indices above 26 are not yet supported.");
			}
			return ((char)(97 + index)).ToString();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000A892 File Offset: 0x00008A92
		public static int GetArgumentIndex(char name)
		{
			if (name < 'a' || name > 'z')
			{
				throw new NotImplementedException("Unalphabetical argument names are not yet supported.");
			}
			return (int)(name - 'a');
		}

		// Token: 0x0400013A RID: 314
		[SerializeAs("Formula")]
		private string _formula;

		// Token: 0x0400013B RID: 315
		private Expression ncalc;
	}
}
