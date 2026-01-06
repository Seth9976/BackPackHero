using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x0200001C RID: 28
	public sealed class InvokeMember : MemberUnit
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004005 File Offset: 0x00002205
		public InvokeMember()
		{
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000400D File Offset: 0x0000220D
		public InvokeMember(Member member)
			: base(member)
		{
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004016 File Offset: 0x00002216
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000401E File Offset: 0x0000221E
		[Serialize]
		[InspectableIf("supportsChaining")]
		public bool chainable { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004027 File Offset: 0x00002227
		[DoNotSerialize]
		public bool supportsChaining
		{
			get
			{
				return base.member.requiresTarget;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004034 File Offset: 0x00002234
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000403C File Offset: 0x0000223C
		[DoNotSerialize]
		[MemberFilter(Methods = true, Constructors = true)]
		public Member invocation
		{
			get
			{
				return base.member;
			}
			set
			{
				base.member = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004045 File Offset: 0x00002245
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000404D File Offset: 0x0000224D
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004056 File Offset: 0x00002256
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000405E File Offset: 0x0000225E
		[DoNotSerialize]
		public Dictionary<int, ValueInput> inputParameters { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004067 File Offset: 0x00002267
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000406F File Offset: 0x0000226F
		[DoNotSerialize]
		[PortLabel("Target")]
		[PortLabelHidden]
		public ValueOutput targetOutput { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004078 File Offset: 0x00002278
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004080 File Offset: 0x00002280
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput result { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004089 File Offset: 0x00002289
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004091 File Offset: 0x00002291
		[DoNotSerialize]
		public Dictionary<int, ValueOutput> outputParameters { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000409A File Offset: 0x0000229A
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000040A2 File Offset: 0x000022A2
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x0600010A RID: 266 RVA: 0x000040AC File Offset: 0x000022AC
		public override bool HandleDependencies()
		{
			if (!base.HandleDependencies())
			{
				return false;
			}
			if (this.parameterNames == null && base.member.parameterTypes.Length == base.defaultValues.Count)
			{
				this.parameterNames = base.defaultValues.Select((KeyValuePair<string, object> defaultValue) => defaultValue.Key.Substring(1)).ToList<string>();
			}
			return true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000411C File Offset: 0x0000231C
		protected override void Definition()
		{
			base.Definition();
			this.inputParameters = new Dictionary<int, ValueInput>();
			this.outputParameters = new Dictionary<int, ValueOutput>();
			this.useExpandedParameters = true;
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.exit = base.ControlOutput("exit");
			base.Succession(this.enter, this.exit);
			if (base.member.requiresTarget)
			{
				base.Requirement(base.target, this.enter);
			}
			if (this.supportsChaining && this.chainable)
			{
				this.targetOutput = base.ValueOutput(base.member.targetType, "targetOutput");
				base.Assignment(this.enter, this.targetOutput);
			}
			if (base.member.isGettable)
			{
				this.result = base.ValueOutput(base.member.type, "result", new Func<Flow, object>(this.Result));
				if (base.member.requiresTarget)
				{
					base.Requirement(base.target, this.result);
				}
			}
			ParameterInfo[] array = base.member.GetParameterInfos().ToArray<ParameterInfo>();
			this.parameterCount = array.Length;
			for (int i = 0; i < this.parameterCount; i++)
			{
				ParameterInfo parameterInfo = array[i];
				Type type = parameterInfo.UnderlyingParameterType();
				if (!parameterInfo.HasOutModifier())
				{
					string text = "%" + parameterInfo.Name;
					if (this.parameterNames != null && this.parameterNames[i] != parameterInfo.Name)
					{
						text = "%" + this.parameterNames[i];
					}
					ValueInput valueInput = base.ValueInput(type, text);
					this.inputParameters.Add(i, valueInput);
					valueInput.SetDefaultValue(parameterInfo.PseudoDefaultValue());
					if (parameterInfo.AllowsNull())
					{
						valueInput.AllowsNull();
					}
					base.Requirement(valueInput, this.enter);
					if (base.member.isGettable)
					{
						base.Requirement(valueInput, this.result);
					}
				}
				if (parameterInfo.ParameterType.IsByRef || parameterInfo.IsOut)
				{
					string text2 = "&" + parameterInfo.Name;
					if (this.parameterNames != null && this.parameterNames[i] != parameterInfo.Name)
					{
						text2 = "&" + this.parameterNames[i];
					}
					ValueOutput valueOutput = base.ValueOutput(type, text2);
					this.outputParameters.Add(i, valueOutput);
					base.Assignment(this.enter, valueOutput);
					this.useExpandedParameters = false;
				}
			}
			if (this.inputParameters.Count > 5)
			{
				this.useExpandedParameters = false;
			}
			if (this.parameterNames == null)
			{
				this.parameterNames = array.Select((ParameterInfo pInfo) => pInfo.Name).ToList<string>();
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004414 File Offset: 0x00002614
		private void PostDeserializeRemapParameterNames()
		{
			ParameterInfo[] array = base.member.GetParameterInfos().ToArray<ParameterInfo>();
			List<string> list = this.parameterNames;
			int? num = ((list != null) ? new int?(list.Count) : null);
			int i = array.Length;
			if (!((num.GetValueOrDefault() == i) & (num != null)))
			{
				return;
			}
			List<ValueTuple<ValueInput, ValueOutput[]>> list2 = null;
			List<ValueTuple<ValueOutput, ValueInput[]>> list3 = null;
			List<ValueTuple<string, object>> list4 = null;
			for (int j = 0; j < array.Length; j++)
			{
				ParameterInfo parameterInfo = array[j];
				string text = this.parameterNames[j];
				if (parameterInfo.Name != text)
				{
					ValueInput valueInput;
					ValueOutput valueOutput;
					if (base.valueInputs.TryGetValue("%" + text, out valueInput))
					{
						ValueOutput[] array2 = valueInput.validConnections.Select((ValueConnection con) => con.source).ToArray<ValueOutput>();
						ValueOutput[] array3 = array2;
						for (i = 0; i < array3.Length; i++)
						{
							array3[i].DisconnectFromValid(valueInput);
						}
						base.valueInputs.Remove(valueInput);
						if (list2 == null)
						{
							list2 = new List<ValueTuple<ValueInput, ValueOutput[]>>(1);
						}
						list2.Add(new ValueTuple<ValueInput, ValueOutput[]>(new ValueInput("%" + parameterInfo.Name, parameterInfo.ParameterType), array2));
						object obj;
						if (base.defaultValues.TryGetValue(valueInput.key, out obj))
						{
							base.defaultValues.Remove(valueInput.key);
							if (list4 == null)
							{
								list4 = new List<ValueTuple<string, object>>(1);
							}
							list4.Add(new ValueTuple<string, object>("%" + parameterInfo.Name, obj));
						}
					}
					else if (base.valueOutputs.TryGetValue("&" + text, out valueOutput))
					{
						ValueInput[] array4 = valueOutput.validConnections.Select((ValueConnection con) => con.destination).ToArray<ValueInput>();
						ValueInput[] array5 = array4;
						for (i = 0; i < array5.Length; i++)
						{
							array5[i].DisconnectFromValid(valueOutput);
						}
						base.valueOutputs.Remove(valueOutput);
						if (list3 == null)
						{
							list3 = new List<ValueTuple<ValueOutput, ValueInput[]>>(1);
						}
						list3.Add(new ValueTuple<ValueOutput, ValueInput[]>(new ValueOutput("&" + parameterInfo.Name, parameterInfo.ParameterType), array4));
					}
					this.parameterNames[j] = parameterInfo.Name;
				}
			}
			if (list2 != null)
			{
				foreach (ValueTuple<ValueInput, ValueOutput[]> valueTuple in list2)
				{
					base.valueInputs.Add(valueTuple.Item1);
					ValueOutput[] array3 = valueTuple.Item2;
					for (i = 0; i < array3.Length; i++)
					{
						array3[i].ConnectToValid(valueTuple.Item1);
					}
				}
				if (list4 != null)
				{
					foreach (ValueTuple<string, object> valueTuple2 in list4)
					{
						base.defaultValues[valueTuple2.Item1] = valueTuple2.Item2;
					}
				}
			}
			if (list3 != null)
			{
				foreach (ValueTuple<ValueOutput, ValueInput[]> valueTuple3 in list3)
				{
					base.valueOutputs.Add(valueTuple3.Item1);
					ValueInput[] array5 = valueTuple3.Item2;
					for (i = 0; i < array5.Length; i++)
					{
						array5[i].ConnectToValid(valueTuple3.Item1);
					}
				}
			}
			if (list2 != null || list3 != null)
			{
				base.Define();
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000047E8 File Offset: 0x000029E8
		protected override bool IsMemberValid(Member member)
		{
			return member.isInvocable;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000047F0 File Offset: 0x000029F0
		private object Invoke(object target, Flow flow)
		{
			if (!this.useExpandedParameters)
			{
				object[] array = new object[this.parameterCount];
				for (int i = 0; i < this.parameterCount; i++)
				{
					ValueInput valueInput;
					if (this.inputParameters.TryGetValue(i, out valueInput))
					{
						array[i] = flow.GetConvertedValue(valueInput);
					}
				}
				object obj = base.member.Invoke(target, array);
				for (int j = 0; j < this.parameterCount; j++)
				{
					ValueOutput valueOutput;
					if (this.outputParameters.TryGetValue(j, out valueOutput))
					{
						flow.SetValue(valueOutput, array[j]);
					}
				}
				return obj;
			}
			switch (this.inputParameters.Count)
			{
			case 0:
				return base.member.Invoke(target);
			case 1:
				return base.member.Invoke(target, flow.GetConvertedValue(this.inputParameters[0]));
			case 2:
				return base.member.Invoke(target, flow.GetConvertedValue(this.inputParameters[0]), flow.GetConvertedValue(this.inputParameters[1]));
			case 3:
				return base.member.Invoke(target, flow.GetConvertedValue(this.inputParameters[0]), flow.GetConvertedValue(this.inputParameters[1]), flow.GetConvertedValue(this.inputParameters[2]));
			case 4:
				return base.member.Invoke(target, flow.GetConvertedValue(this.inputParameters[0]), flow.GetConvertedValue(this.inputParameters[1]), flow.GetConvertedValue(this.inputParameters[2]), flow.GetConvertedValue(this.inputParameters[3]));
			case 5:
				return base.member.Invoke(target, flow.GetConvertedValue(this.inputParameters[0]), flow.GetConvertedValue(this.inputParameters[1]), flow.GetConvertedValue(this.inputParameters[2]), flow.GetConvertedValue(this.inputParameters[3]), flow.GetConvertedValue(this.inputParameters[4]));
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004A14 File Offset: 0x00002C14
		private object GetAndChainTarget(Flow flow)
		{
			if (base.member.requiresTarget)
			{
				object value = flow.GetValue(base.target, base.member.targetType);
				if (this.supportsChaining && this.chainable)
				{
					flow.SetValue(this.targetOutput, value);
				}
				return value;
			}
			return null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004A68 File Offset: 0x00002C68
		private object Result(Flow flow)
		{
			object andChainTarget = this.GetAndChainTarget(flow);
			return this.Invoke(andChainTarget, flow);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004A88 File Offset: 0x00002C88
		private ControlOutput Enter(Flow flow)
		{
			object andChainTarget = this.GetAndChainTarget(flow);
			object obj = this.Invoke(andChainTarget, flow);
			if (this.result != null)
			{
				flow.SetValue(this.result, obj);
			}
			return this.exit;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public override AnalyticsIdentifier GetAnalyticsIdentifier()
		{
			string text = base.member.targetType.FullName + "." + base.member.name;
			if (base.member.parameterTypes != null)
			{
				text += "(";
				for (int i = 0; i < base.member.parameterTypes.Length; i++)
				{
					if (i >= 5)
					{
						text += string.Format("->{0}", i);
						break;
					}
					text += base.member.parameterTypes[i].FullName;
					if (i < base.member.parameterTypes.Length - 1)
					{
						text += ", ";
					}
				}
				text += ")";
			}
			AnalyticsIdentifier analyticsIdentifier = new AnalyticsIdentifier();
			analyticsIdentifier.Identifier = text;
			analyticsIdentifier.Namespace = base.member.targetType.Namespace;
			analyticsIdentifier.Hashcode = analyticsIdentifier.Identifier.GetHashCode();
			return analyticsIdentifier;
		}

		// Token: 0x04000040 RID: 64
		private bool useExpandedParameters;

		// Token: 0x04000048 RID: 72
		[DoNotSerialize]
		private int parameterCount;

		// Token: 0x04000049 RID: 73
		[Serialize]
		private List<string> parameterNames;
	}
}
