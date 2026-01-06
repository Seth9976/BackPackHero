using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200001E RID: 30
	public static class InputActionRebindingExtensions
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x000056AC File Offset: 0x000038AC
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			return action.GetParameterValue(new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, default(PrimitiveValue)));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000056F0 File Offset: 0x000038F0
		private static PrimitiveValue? GetParameterValue(this InputAction action, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			parameterOverride.bindingMask.action = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			using (InputActionRebindingExtensions.ParameterEnumerator enumerator = new InputActionRebindingExtensions.ParameterEnumerable(orCreateActionMap.m_State, parameterOverride, orCreateActionMap.m_MapIndexInState).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					InputActionRebindingExtensions.Parameter parameter = enumerator.Current;
					return new PrimitiveValue?(PrimitiveValue.FromObject(parameter.field.GetValue(parameter.instance)));
				}
			}
			return null;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005798 File Offset: 0x00003998
		public static PrimitiveValue? GetParameterValue(this InputAction action, string name, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding inputBinding = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[num].id
			};
			return action.GetParameterValue(name, inputBinding);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005814 File Offset: 0x00003A14
		public unsafe static TValue? GetParameterValue<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, default(PrimitiveValue));
			PrimitiveValue? parameterValue = action.GetParameterValue(parameterOverride);
			if (parameterValue == null)
			{
				return null;
			}
			if (Type.GetTypeCode(typeof(TValue)) == parameterValue.Value.type)
			{
				PrimitiveValue value = parameterValue.Value;
				TValue tvalue = default(TValue);
				UnsafeUtility.MemCpy(UnsafeUtility.AddressOf<TValue>(ref tvalue), (void*)value.valuePtr, (long)UnsafeUtility.SizeOf<TValue>());
				return new TValue?(tvalue);
			}
			return new TValue?((TValue)((object)Convert.ChangeType(parameterValue.Value.ToObject(), typeof(TValue))));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000058E4 File Offset: 0x00003AE4
		public static void ApplyParameterOverride<TObject, TValue>(this InputAction action, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005954 File Offset: 0x00003B54
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionMap actionMap, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000059B0 File Offset: 0x00003BB0
		public static void ApplyParameterOverride<TObject, TValue>(this InputActionAsset asset, Expression<Func<TObject, TValue>> expr, TValue value, InputBinding bindingMask = default(InputBinding)) where TValue : struct
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (expr == null)
			{
				throw new ArgumentNullException("expr");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ParameterOverride parameterOverride = InputActionRebindingExtensions.ExtractParameterOverride<TObject, TValue>(expr, bindingMask, PrimitiveValue.From<TValue>(value));
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, parameterOverride);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005A0C File Offset: 0x00003C0C
		private static InputActionRebindingExtensions.ParameterOverride ExtractParameterOverride<TObject, TValue>(Expression<Func<TObject, TValue>> expr, InputBinding bindingMask = default(InputBinding), PrimitiveValue value = default(PrimitiveValue))
		{
			if (expr == null)
			{
				throw new ArgumentException("Expression must be a LambdaExpression but was a " + expr.GetType().Name + " instead", "expr");
			}
			MemberExpression memberExpression = expr.Body as MemberExpression;
			if (memberExpression == null)
			{
				UnaryExpression unaryExpression = expr.Body as UnaryExpression;
				if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
				{
					MemberExpression memberExpression2 = unaryExpression.Operand as MemberExpression;
					if (memberExpression2 != null)
					{
						memberExpression = memberExpression2;
						goto IL_008D;
					}
				}
				throw new ArgumentException("Body in LambdaExpression must be a MemberExpression (x.name) but was a " + expr.GetType().Name + " instead", "expr");
			}
			IL_008D:
			string text;
			if (typeof(InputProcessor).IsAssignableFrom(typeof(TObject)))
			{
				text = InputProcessor.s_Processors.FindNameForType(typeof(TObject));
			}
			else if (typeof(IInputInteraction).IsAssignableFrom(typeof(TObject)))
			{
				text = InputInteraction.s_Interactions.FindNameForType(typeof(TObject));
			}
			else
			{
				if (!typeof(InputBindingComposite).IsAssignableFrom(typeof(TObject)))
				{
					throw new ArgumentException("Given type must be an InputProcessor, IInputInteraction, or InputBindingComposite (was " + typeof(TObject).Name + ")", "TObject");
				}
				text = InputBindingComposite.s_Composites.FindNameForType(typeof(TObject));
			}
			return new InputActionRebindingExtensions.ParameterOverride(text, memberExpression.Member.Name, bindingMask, value);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005B8C File Offset: 0x00003D8C
		public static void ApplyParameterOverride(this InputActionMap actionMap, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			actionMap.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(actionMap.m_State, actionMap.m_MapIndexInState, ref actionMap.m_ParameterOverrides, ref actionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005BE8 File Offset: 0x00003DE8
		public static void ApplyParameterOverride(this InputActionAsset asset, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (asset == null)
			{
				throw new ArgumentNullException("asset");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			asset.ResolveBindingsIfNecessary();
			InputActionRebindingExtensions.ApplyParameterOverride(asset.m_SharedStateForAllMaps, -1, ref asset.m_ParameterOverrides, ref asset.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005C44 File Offset: 0x00003E44
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, InputBinding bindingMask = default(InputBinding))
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			bindingMask.action = action.name;
			InputActionRebindingExtensions.ApplyParameterOverride(orCreateActionMap.m_State, orCreateActionMap.m_MapIndexInState, ref orCreateActionMap.m_ParameterOverrides, ref orCreateActionMap.m_ParameterOverridesCount, new InputActionRebindingExtensions.ParameterOverride(name, bindingMask, value));
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005CB0 File Offset: 0x00003EB0
		public static void ApplyParameterOverride(this InputAction action, string name, PrimitiveValue value, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			if (bindingIndex < 0)
			{
				throw new ArgumentOutOfRangeException("bindingIndex");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			InputBinding inputBinding = new InputBinding
			{
				id = action.GetOrCreateActionMap().bindings[num].id
			};
			action.ApplyParameterOverride(name, value, inputBinding);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005D2C File Offset: 0x00003F2C
		private static void ApplyParameterOverride(InputActionState state, int mapIndex, ref InputActionRebindingExtensions.ParameterOverride[] parameterOverrides, ref int parameterOverridesCount, InputActionRebindingExtensions.ParameterOverride parameterOverride)
		{
			bool flag = false;
			if (parameterOverrides != null)
			{
				for (int i = 0; i < parameterOverridesCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride ptr = ref parameterOverrides[i];
					if (string.Equals(ptr.objectRegistrationName, parameterOverride.objectRegistrationName, StringComparison.OrdinalIgnoreCase) && string.Equals(ptr.parameter, parameterOverride.parameter, StringComparison.OrdinalIgnoreCase) && ptr.bindingMask == parameterOverride.bindingMask)
					{
						flag = true;
						ptr = parameterOverride;
						break;
					}
				}
			}
			if (!flag)
			{
				ArrayHelpers.AppendWithCapacity<InputActionRebindingExtensions.ParameterOverride>(ref parameterOverrides, ref parameterOverridesCount, parameterOverride, 10);
			}
			foreach (InputActionRebindingExtensions.Parameter parameter in new InputActionRebindingExtensions.ParameterEnumerable(state, parameterOverride, mapIndex))
			{
				InputActionMap actionMap = state.GetActionMap(parameter.bindingIndex);
				ref InputBinding binding = ref state.GetBinding(parameter.bindingIndex);
				InputActionRebindingExtensions.ParameterOverride? parameterOverride2 = InputActionRebindingExtensions.ParameterOverride.Find(actionMap, ref binding, parameterOverride.parameter, parameterOverride.objectRegistrationName);
				if (parameterOverride2 != null)
				{
					TypeCode typeCode = Type.GetTypeCode(parameter.field.FieldType);
					parameter.field.SetValue(parameter.instance, parameterOverride2.Value.value.ConvertTo(typeCode).ToObject());
				}
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005E80 File Offset: 0x00004080
		public static int GetBindingIndex(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005EC8 File Offset: 0x000040C8
		public static int GetBindingIndex(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			ReadOnlyArray<InputBinding> bindings = actionMap.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (bindingMask.Matches(bindings[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005F10 File Offset: 0x00004110
		public static int GetBindingIndex(this InputAction action, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			return action.GetBindingIndex(new InputBinding(path, null, group, null, null, null));
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005F40 File Offset: 0x00004140
		public static InputBinding? GetBindingForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			int bindingIndexForControl = action.GetBindingIndexForControl(control);
			if (bindingIndexForControl == -1)
			{
				return null;
			}
			return new InputBinding?(action.bindings[bindingIndexForControl]);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005F94 File Offset: 0x00004194
		public unsafe static int GetBindingIndexForControl(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			orCreateActionMap.ResolveBindingsIfNecessary();
			InputActionState state = orCreateActionMap.m_State;
			InputControl[] controls = state.controls;
			int totalControlCount = state.totalControlCount;
			InputActionState.BindingState* bindingStates = state.bindingStates;
			int* controlIndexToBindingIndex = state.controlIndexToBindingIndex;
			int actionIndexInState = action.m_ActionIndexInState;
			for (int i = 0; i < totalControlCount; i++)
			{
				if (controls[i] == control)
				{
					int num = controlIndexToBindingIndex[i];
					if (bindingStates[num].actionIndex == actionIndexInState)
					{
						int bindingIndexInMap = state.GetBindingIndexInMap(num);
						return action.BindingIndexOnMapToBindingIndexOnAction(bindingIndexInMap);
					}
				}
			}
			return -1;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00006040 File Offset: 0x00004240
		public static string GetBindingDisplayString(this InputAction action, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, string group = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputBinding inputBinding;
			if (!string.IsNullOrEmpty(group))
			{
				inputBinding = InputBinding.MaskByGroup(group);
			}
			else
			{
				InputBinding? inputBinding2 = action.FindEffectiveBindingMask();
				if (inputBinding2 != null)
				{
					inputBinding = inputBinding2.Value;
				}
				else
				{
					inputBinding = default(InputBinding);
				}
			}
			return action.GetBindingDisplayString(inputBinding, options);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00006098 File Offset: 0x00004298
		public static string GetBindingDisplayString(this InputAction action, InputBinding bindingMask, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string text = string.Empty;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			for (int i = 0; i < bindings.Count; i++)
			{
				if (!bindings[i].isPartOfComposite && bindingMask.Matches(bindings[i]))
				{
					string bindingDisplayString = action.GetBindingDisplayString(i, options);
					if (text != "")
					{
						text = text + " | " + bindingDisplayString;
					}
					else
					{
						text = bindingDisplayString;
					}
				}
			}
			return text;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00006120 File Offset: 0x00004320
		public static string GetBindingDisplayString(this InputAction action, int bindingIndex, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string text;
			string text2;
			return action.GetBindingDisplayString(bindingIndex, out text, out text2, options);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00006148 File Offset: 0x00004348
		public unsafe static string GetBindingDisplayString(this InputAction action, int bindingIndex, out string deviceLayoutName, out string controlPath, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			deviceLayoutName = null;
			controlPath = null;
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			if (bindingIndex < 0 || bindingIndex >= count)
			{
				throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range on action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindingIndex");
			}
			if (!bindings[bindingIndex].isComposite)
			{
				InputControl inputControl = null;
				InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
				orCreateActionMap.ResolveBindingsIfNecessary();
				InputActionState state = orCreateActionMap.m_State;
				int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
				int bindingIndexInState = state.GetBindingIndexInState(orCreateActionMap.m_MapIndexInState, num);
				InputActionState.BindingState* ptr = state.bindingStates + bindingIndexInState;
				if (ptr->controlCount > 0)
				{
					inputControl = state.controls[ptr->controlStartIndex];
				}
				InputBinding inputBinding = bindings[bindingIndex];
				if (string.IsNullOrEmpty(inputBinding.effectiveInteractions))
				{
					inputBinding.overrideInteractions = action.interactions;
				}
				else if (!string.IsNullOrEmpty(action.interactions))
				{
					inputBinding.overrideInteractions = inputBinding.effectiveInteractions + ";action.interactions";
				}
				return inputBinding.ToDisplayString(out deviceLayoutName, out controlPath, options, inputControl);
			}
			string name = NameAndParameters.Parse(bindings[bindingIndex].effectivePath).name;
			int firstPartIndex = bindingIndex + 1;
			int num2 = firstPartIndex;
			while (num2 < count && bindings[num2].isPartOfComposite)
			{
				num2++;
			}
			int partCount = num2 - firstPartIndex;
			string[] partStrings = new string[partCount];
			for (int i = 0; i < partCount; i++)
			{
				string text = action.GetBindingDisplayString(firstPartIndex + i, options);
				if (string.IsNullOrEmpty(text))
				{
					text = " ";
				}
				partStrings[i] = text;
			}
			string displayFormatString = InputBindingComposite.GetDisplayFormatString(name);
			if (string.IsNullOrEmpty(displayFormatString))
			{
				return StringHelpers.Join<string>("/", partStrings);
			}
			return StringHelpers.ExpandTemplateString(displayFormatString, delegate(string fragment)
			{
				string text2 = string.Empty;
				for (int j = 0; j < partCount; j++)
				{
					if (string.Equals(bindings[firstPartIndex + j].name, fragment, StringComparison.InvariantCultureIgnoreCase))
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text2 = text2 + "|" + partStrings[j];
						}
						else
						{
							text2 = partStrings[j];
						}
					}
				}
				if (string.IsNullOrEmpty(text2))
				{
					text2 = " ";
				}
				return text2;
			});
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00006388 File Offset: 0x00004588
		public static void ApplyBindingOverride(this InputAction action, string newPath, string group = null, string path = null)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(new InputBinding
			{
				overridePath = newPath,
				groups = group,
				path = path
			});
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000063CC File Offset: 0x000045CC
		public static void ApplyBindingOverride(this InputAction action, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool enabled = action.enabled;
			if (enabled)
			{
				action.Disable();
			}
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(bindingOverride);
			if (enabled)
			{
				action.Enable();
				action.RequestInitialStateCheckOnEnabledAction();
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006420 File Offset: 0x00004620
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, InputBinding bindingOverride)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			int num = action.BindingIndexOnActionToBindingIndexOnMap(bindingIndex);
			bindingOverride.action = action.name;
			action.GetOrCreateActionMap().ApplyBindingOverride(num, bindingOverride);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00006460 File Offset: 0x00004660
		public static void ApplyBindingOverride(this InputAction action, int bindingIndex, string path)
		{
			if (path == null)
			{
				throw new ArgumentException("Binding path cannot be null", "path");
			}
			action.ApplyBindingOverride(bindingIndex, new InputBinding
			{
				overridePath = path
			});
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006498 File Offset: 0x00004698
		public static int ApplyBindingOverride(this InputActionMap actionMap, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			if (bindings == null)
			{
				return 0;
			}
			int num = bindings.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if (bindingOverride.Matches(ref bindings[i], (InputBinding.MatchOptions)0))
				{
					bindings[i].overridePath = bindingOverride.overridePath;
					bindings[i].overrideInteractions = bindingOverride.overrideInteractions;
					bindings[i].overrideProcessors = bindingOverride.overrideProcessors;
					num2++;
				}
			}
			if (num2 > 0)
			{
				actionMap.OnBindingModified();
			}
			return num2;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000652C File Offset: 0x0000472C
		public static void ApplyBindingOverride(this InputActionMap actionMap, int bindingIndex, InputBinding bindingOverride)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			InputBinding[] bindings = actionMap.m_Bindings;
			int num = ((bindings != null) ? bindings.Length : 0);
			if (bindingIndex < 0 || bindingIndex >= num)
			{
				throw new ArgumentOutOfRangeException("bindingIndex", string.Format("Cannot apply override to binding at index {0} in map '{1}' with only {2} bindings", bindingIndex, actionMap, num));
			}
			actionMap.m_Bindings[bindingIndex].overridePath = bindingOverride.overridePath;
			actionMap.m_Bindings[bindingIndex].overrideInteractions = bindingOverride.overrideInteractions;
			actionMap.m_Bindings[bindingIndex].overrideProcessors = bindingOverride.overrideProcessors;
			actionMap.OnBindingModified();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000065D0 File Offset: 0x000047D0
		public static void RemoveBindingOverride(this InputAction action, int bindingIndex)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			action.ApplyBindingOverride(bindingIndex, default(InputBinding));
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000065FB File Offset: 0x000047FB
		public static void RemoveBindingOverride(this InputAction action, InputBinding bindingMask)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			action.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000662A File Offset: 0x0000482A
		private static void RemoveBindingOverride(this InputActionMap actionMap, InputBinding bindingMask)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			bindingMask.overridePath = null;
			bindingMask.overrideInteractions = null;
			bindingMask.overrideProcessors = null;
			actionMap.ApplyBindingOverride(bindingMask);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000665C File Offset: 0x0000485C
		public static void RemoveAllBindingOverrides(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				foreach (InputAction inputAction in actions)
				{
					InputActionMap orCreateActionMap = inputAction.GetOrCreateActionMap();
					InputBinding[] bindings = orCreateActionMap.m_Bindings;
					int num = bindings.LengthSafe<InputBinding>();
					for (int i = 0; i < num; i++)
					{
						ref InputBinding ptr = ref bindings[i];
						if (ptr.TriggersAction(inputAction))
						{
							ptr.RemoveOverrides();
						}
					}
					orCreateActionMap.OnBindingModified();
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006714 File Offset: 0x00004914
		public static void RemoveAllBindingOverrides(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			string name = action.name;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			InputBinding[] bindings = orCreateActionMap.m_Bindings;
			if (bindings == null)
			{
				return;
			}
			int num = bindings.Length;
			for (int i = 0; i < num; i++)
			{
				if (string.Compare(bindings[i].action, name, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					bindings[i].overridePath = null;
					bindings[i].overrideInteractions = null;
					bindings[i].overrideProcessors = null;
				}
			}
			orCreateActionMap.OnBindingModified();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000067A4 File Offset: 0x000049A4
		public static void ApplyBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding inputBinding in overrides)
			{
				actionMap.ApplyBindingOverride(inputBinding);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000680C File Offset: 0x00004A0C
		public static void RemoveBindingOverrides(this InputActionMap actionMap, IEnumerable<InputBinding> overrides)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (overrides == null)
			{
				throw new ArgumentNullException("overrides");
			}
			foreach (InputBinding inputBinding in overrides)
			{
				actionMap.RemoveBindingOverride(inputBinding);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006870 File Offset: 0x00004A70
		public static int ApplyBindingOverridesOnMatchingControls(this InputAction action, InputControl control)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputBinding> bindings = action.bindings;
			int count = bindings.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				InputControl inputControl = InputControlPath.TryFindControl(control, bindings[i].path, 0);
				if (inputControl != null)
				{
					action.ApplyBindingOverride(i, inputControl.path);
					num++;
				}
			}
			return num;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static int ApplyBindingOverridesOnMatchingControls(this InputActionMap actionMap, InputControl control)
		{
			if (actionMap == null)
			{
				throw new ArgumentNullException("actionMap");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			ReadOnlyArray<InputAction> actions = actionMap.actions;
			int count = actions.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				num = actions[i].ApplyBindingOverridesOnMatchingControls(control);
			}
			return num;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006940 File Offset: 0x00004B40
		public static string SaveBindingOverridesAsJson(this IInputActionCollection2 actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			List<InputActionMap.BindingOverrideJson> list = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding inputBinding in actions.bindings)
			{
				actions.AddBindingOverrideJsonTo(inputBinding, list, null);
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000069CC File Offset: 0x00004BCC
		public static string SaveBindingOverridesAsJson(this InputAction action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool isSingletonAction = action.isSingletonAction;
			InputActionMap orCreateActionMap = action.GetOrCreateActionMap();
			List<InputActionMap.BindingOverrideJson> list = new List<InputActionMap.BindingOverrideJson>();
			foreach (InputBinding inputBinding in action.bindings)
			{
				if (isSingletonAction || inputBinding.TriggersAction(action))
				{
					orCreateActionMap.AddBindingOverrideJsonTo(inputBinding, list, isSingletonAction ? action : null);
				}
			}
			if (list.Count == 0)
			{
				return string.Empty;
			}
			return JsonUtility.ToJson(new InputActionMap.BindingOverrideListJson
			{
				bindings = list
			});
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006A88 File Offset: 0x00004C88
		private static void AddBindingOverrideJsonTo(this IInputActionCollection2 actions, InputBinding binding, List<InputActionMap.BindingOverrideJson> list, InputAction action = null)
		{
			if (!binding.hasOverrides)
			{
				return;
			}
			if (action == null)
			{
				action = actions.FindAction(binding.action, false);
			}
			string text = ((action != null && !action.isSingletonAction) ? (action.actionMap.name + "/" + action.name) : "");
			InputActionMap.BindingOverrideJson bindingOverrideJson = InputActionMap.BindingOverrideJson.FromBinding(binding, text);
			list.Add(bindingOverrideJson);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public static void LoadBindingOverridesFromJson(this IInputActionCollection2 actions, string json, bool removeExisting = true)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					actions.RemoveAllBindingOverrides();
				}
				actions.LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006B40 File Offset: 0x00004D40
		public static void LoadBindingOverridesFromJson(this InputAction action, string json, bool removeExisting = true)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			using (InputActionRebindingExtensions.DeferBindingResolution())
			{
				if (removeExisting)
				{
					action.RemoveAllBindingOverrides();
				}
				action.GetOrCreateActionMap().LoadBindingOverridesFromJsonInternal(json);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006B94 File Offset: 0x00004D94
		private static void LoadBindingOverridesFromJsonInternal(this IInputActionCollection2 actions, string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			foreach (InputActionMap.BindingOverrideJson bindingOverrideJson in JsonUtility.FromJson<InputActionMap.BindingOverrideListJson>(json).bindings)
			{
				if (!string.IsNullOrEmpty(bindingOverrideJson.id))
				{
					InputAction inputAction;
					int num = actions.FindBinding(new InputBinding
					{
						m_Id = bindingOverrideJson.id
					}, out inputAction);
					if (num != -1)
					{
						inputAction.ApplyBindingOverride(num, InputActionMap.BindingOverrideJson.ToBinding(bindingOverrideJson));
						continue;
					}
				}
				Debug.LogWarning("Could not override binding as no existing binding was found with the id: " + bindingOverrideJson.id);
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006C44 File Offset: 0x00004E44
		public static InputActionRebindingExtensions.RebindingOperation PerformInteractiveRebinding(this InputAction action, int bindingIndex = -1)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			InputActionRebindingExtensions.RebindingOperation rebindingOperation = new InputActionRebindingExtensions.RebindingOperation().WithAction(action).OnMatchWaitForAnother(0.05f).WithControlsExcluding("<Pointer>/delta")
				.WithControlsExcluding("<Pointer>/position")
				.WithControlsExcluding("<Touchscreen>/touch*/position")
				.WithControlsExcluding("<Touchscreen>/touch*/delta")
				.WithControlsExcluding("<Mouse>/clickCount")
				.WithMatchingEventsBeingSuppressed(true);
			if (rebindingOperation.expectedControlType != "Button")
			{
				rebindingOperation.WithCancelingThrough("<Keyboard>/escape");
			}
			if (bindingIndex >= 0)
			{
				ReadOnlyArray<InputBinding> bindings = action.bindings;
				if (bindingIndex >= bindings.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Binding index {0} is out of range for action '{1}' with {2} bindings", bindingIndex, action, bindings.Count), "bindings");
				}
				if (bindings[bindingIndex].isComposite)
				{
					throw new InvalidOperationException(string.Format("Cannot perform rebinding on composite binding '{0}' of '{1}'", bindings[bindingIndex], action));
				}
				rebindingOperation.WithTargetBinding(bindingIndex);
			}
			return rebindingOperation;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006D41 File Offset: 0x00004F41
		internal static InputActionRebindingExtensions.DeferBindingResolutionWrapper DeferBindingResolution()
		{
			if (InputActionRebindingExtensions.s_DeferBindingResolutionWrapper == null)
			{
				InputActionRebindingExtensions.s_DeferBindingResolutionWrapper = new InputActionRebindingExtensions.DeferBindingResolutionWrapper();
			}
			InputActionRebindingExtensions.s_DeferBindingResolutionWrapper.Acquire();
			return InputActionRebindingExtensions.s_DeferBindingResolutionWrapper;
		}

		// Token: 0x040000B7 RID: 183
		private static InputActionRebindingExtensions.DeferBindingResolutionWrapper s_DeferBindingResolutionWrapper;

		// Token: 0x02000161 RID: 353
		internal struct Parameter
		{
			// Token: 0x0400076B RID: 1899
			public object instance;

			// Token: 0x0400076C RID: 1900
			public FieldInfo field;

			// Token: 0x0400076D RID: 1901
			public int bindingIndex;
		}

		// Token: 0x02000162 RID: 354
		private struct ParameterEnumerable : IEnumerable<InputActionRebindingExtensions.Parameter>, IEnumerable
		{
			// Token: 0x06001221 RID: 4641 RVA: 0x000558FF File Offset: 0x00053AFF
			public ParameterEnumerable(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this.m_State = state;
				this.m_Parameter = parameter;
				this.m_MapIndex = mapIndex;
			}

			// Token: 0x06001222 RID: 4642 RVA: 0x00055916 File Offset: 0x00053B16
			public InputActionRebindingExtensions.ParameterEnumerator GetEnumerator()
			{
				return new InputActionRebindingExtensions.ParameterEnumerator(this.m_State, this.m_Parameter, this.m_MapIndex);
			}

			// Token: 0x06001223 RID: 4643 RVA: 0x0005592F File Offset: 0x00053B2F
			IEnumerator<InputActionRebindingExtensions.Parameter> IEnumerable<InputActionRebindingExtensions.Parameter>.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06001224 RID: 4644 RVA: 0x0005593C File Offset: 0x00053B3C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400076E RID: 1902
			private InputActionState m_State;

			// Token: 0x0400076F RID: 1903
			private InputActionRebindingExtensions.ParameterOverride m_Parameter;

			// Token: 0x04000770 RID: 1904
			private int m_MapIndex;
		}

		// Token: 0x02000163 RID: 355
		private struct ParameterEnumerator : IEnumerator<InputActionRebindingExtensions.Parameter>, IEnumerator, IDisposable
		{
			// Token: 0x06001225 RID: 4645 RVA: 0x0005594C File Offset: 0x00053B4C
			public ParameterEnumerator(InputActionState state, InputActionRebindingExtensions.ParameterOverride parameter, int mapIndex = -1)
			{
				this = default(InputActionRebindingExtensions.ParameterEnumerator);
				this.m_State = state;
				this.m_ParameterName = parameter.parameter;
				this.m_MapIndex = mapIndex;
				this.m_ObjectType = parameter.objectType;
				this.m_MayBeComposite = this.m_ObjectType == null || typeof(InputBindingComposite).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeProcessor = this.m_ObjectType == null || typeof(InputProcessor).IsAssignableFrom(this.m_ObjectType);
				this.m_MayBeInteraction = this.m_ObjectType == null || typeof(IInputInteraction).IsAssignableFrom(this.m_ObjectType);
				this.m_BindingMask = parameter.bindingMask;
				this.Reset();
			}

			// Token: 0x06001226 RID: 4646 RVA: 0x00055A20 File Offset: 0x00053C20
			private bool MoveToNextBinding()
			{
				ref InputActionState.BindingState bindingState;
				for (;;)
				{
					this.m_BindingCurrentIndex++;
					if (this.m_BindingCurrentIndex >= this.m_BindingEndIndex)
					{
						break;
					}
					ref InputBinding binding = ref this.m_State.GetBinding(this.m_BindingCurrentIndex);
					bindingState = this.m_State.GetBindingState(this.m_BindingCurrentIndex);
					if ((bindingState.processorCount != 0 || bindingState.interactionCount != 0 || binding.isComposite) && (!this.m_MayBeComposite || this.m_MayBeProcessor || this.m_MayBeInteraction || binding.isComposite) && (!this.m_MayBeProcessor || this.m_MayBeComposite || this.m_MayBeInteraction || bindingState.processorCount != 0) && (!this.m_MayBeInteraction || this.m_MayBeComposite || this.m_MayBeProcessor || bindingState.interactionCount != 0) && this.m_BindingMask.Matches(ref binding, (InputBinding.MatchOptions)0))
					{
						goto Block_12;
					}
				}
				return false;
				Block_12:
				if (this.m_MayBeComposite)
				{
					ref InputBinding binding;
					this.m_CurrentBindingIsComposite = binding.isComposite;
				}
				this.m_ProcessorCurrentIndex = bindingState.processorStartIndex - 1;
				this.m_ProcessorEndIndex = bindingState.processorStartIndex + bindingState.processorCount;
				this.m_InteractionCurrentIndex = bindingState.interactionStartIndex - 1;
				this.m_InteractionEndIndex = bindingState.interactionStartIndex + bindingState.interactionCount;
				return true;
			}

			// Token: 0x06001227 RID: 4647 RVA: 0x00055B58 File Offset: 0x00053D58
			private bool MoveToNextInteraction()
			{
				while (this.m_InteractionCurrentIndex < this.m_InteractionEndIndex)
				{
					this.m_InteractionCurrentIndex++;
					if (this.m_InteractionCurrentIndex == this.m_InteractionEndIndex)
					{
						break;
					}
					IInputInteraction inputInteraction = this.m_State.interactions[this.m_InteractionCurrentIndex];
					if (this.FindParameter(inputInteraction))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001228 RID: 4648 RVA: 0x00055BB0 File Offset: 0x00053DB0
			private bool MoveToNextProcessor()
			{
				while (this.m_ProcessorCurrentIndex < this.m_ProcessorEndIndex)
				{
					this.m_ProcessorCurrentIndex++;
					if (this.m_ProcessorCurrentIndex == this.m_ProcessorEndIndex)
					{
						break;
					}
					InputProcessor inputProcessor = this.m_State.processors[this.m_ProcessorCurrentIndex];
					if (this.FindParameter(inputProcessor))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001229 RID: 4649 RVA: 0x00055C08 File Offset: 0x00053E08
			private bool FindParameter(object instance)
			{
				if (this.m_ObjectType != null && !this.m_ObjectType.IsInstanceOfType(instance))
				{
					return false;
				}
				FieldInfo field = instance.GetType().GetField(this.m_ParameterName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (field == null)
				{
					return false;
				}
				this.m_CurrentParameter = field;
				this.m_CurrentObject = instance;
				return true;
			}

			// Token: 0x0600122A RID: 4650 RVA: 0x00055C64 File Offset: 0x00053E64
			public bool MoveNext()
			{
				while (!this.m_MayBeInteraction || !this.MoveToNextInteraction())
				{
					if (this.m_MayBeProcessor && this.MoveToNextProcessor())
					{
						return true;
					}
					if (!this.MoveToNextBinding())
					{
						return false;
					}
					if (this.m_MayBeComposite && this.m_CurrentBindingIsComposite)
					{
						int compositeOrCompositeBindingIndex = this.m_State.GetBindingState(this.m_BindingCurrentIndex).compositeOrCompositeBindingIndex;
						InputBindingComposite inputBindingComposite = this.m_State.composites[compositeOrCompositeBindingIndex];
						if (this.FindParameter(inputBindingComposite))
						{
							return true;
						}
					}
				}
				return true;
			}

			// Token: 0x0600122B RID: 4651 RVA: 0x00055CE0 File Offset: 0x00053EE0
			public unsafe void Reset()
			{
				this.m_CurrentObject = null;
				this.m_CurrentParameter = null;
				this.m_InteractionCurrentIndex = 0;
				this.m_InteractionEndIndex = 0;
				this.m_ProcessorCurrentIndex = 0;
				this.m_ProcessorEndIndex = 0;
				this.m_CurrentBindingIsComposite = false;
				if (this.m_MapIndex < 0)
				{
					this.m_BindingCurrentIndex = -1;
					this.m_BindingEndIndex = this.m_State.totalBindingCount;
					return;
				}
				this.m_BindingCurrentIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex - 1;
				this.m_BindingEndIndex = this.m_State.mapIndices[this.m_MapIndex].bindingStartIndex + this.m_State.mapIndices[this.m_MapIndex].bindingCount;
			}

			// Token: 0x170004E1 RID: 1249
			// (get) Token: 0x0600122C RID: 4652 RVA: 0x00055DAC File Offset: 0x00053FAC
			public InputActionRebindingExtensions.Parameter Current
			{
				get
				{
					return new InputActionRebindingExtensions.Parameter
					{
						instance = this.m_CurrentObject,
						field = this.m_CurrentParameter,
						bindingIndex = this.m_BindingCurrentIndex
					};
				}
			}

			// Token: 0x170004E2 RID: 1250
			// (get) Token: 0x0600122D RID: 4653 RVA: 0x00055DE9 File Offset: 0x00053FE9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600122E RID: 4654 RVA: 0x00055DF6 File Offset: 0x00053FF6
			public void Dispose()
			{
			}

			// Token: 0x04000771 RID: 1905
			private InputActionState m_State;

			// Token: 0x04000772 RID: 1906
			private int m_MapIndex;

			// Token: 0x04000773 RID: 1907
			private int m_BindingCurrentIndex;

			// Token: 0x04000774 RID: 1908
			private int m_BindingEndIndex;

			// Token: 0x04000775 RID: 1909
			private int m_InteractionCurrentIndex;

			// Token: 0x04000776 RID: 1910
			private int m_InteractionEndIndex;

			// Token: 0x04000777 RID: 1911
			private int m_ProcessorCurrentIndex;

			// Token: 0x04000778 RID: 1912
			private int m_ProcessorEndIndex;

			// Token: 0x04000779 RID: 1913
			private InputBinding m_BindingMask;

			// Token: 0x0400077A RID: 1914
			private Type m_ObjectType;

			// Token: 0x0400077B RID: 1915
			private string m_ParameterName;

			// Token: 0x0400077C RID: 1916
			private bool m_MayBeInteraction;

			// Token: 0x0400077D RID: 1917
			private bool m_MayBeProcessor;

			// Token: 0x0400077E RID: 1918
			private bool m_MayBeComposite;

			// Token: 0x0400077F RID: 1919
			private bool m_CurrentBindingIsComposite;

			// Token: 0x04000780 RID: 1920
			private object m_CurrentObject;

			// Token: 0x04000781 RID: 1921
			private FieldInfo m_CurrentParameter;
		}

		// Token: 0x02000164 RID: 356
		internal struct ParameterOverride
		{
			// Token: 0x170004E3 RID: 1251
			// (get) Token: 0x0600122F RID: 4655 RVA: 0x00055DF8 File Offset: 0x00053FF8
			public Type objectType
			{
				get
				{
					Type type;
					if ((type = InputProcessor.s_Processors.LookupTypeRegistration(this.objectRegistrationName)) == null)
					{
						type = InputInteraction.s_Interactions.LookupTypeRegistration(this.objectRegistrationName) ?? InputBindingComposite.s_Composites.LookupTypeRegistration(this.objectRegistrationName);
					}
					return type;
				}
			}

			// Token: 0x06001230 RID: 4656 RVA: 0x00055E34 File Offset: 0x00054034
			public ParameterOverride(string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				int num = parameterName.IndexOf(':');
				if (num < 0)
				{
					this.objectRegistrationName = null;
					this.parameter = parameterName;
				}
				else
				{
					this.objectRegistrationName = parameterName.Substring(0, num);
					this.parameter = parameterName.Substring(num + 1);
				}
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x06001231 RID: 4657 RVA: 0x00055E89 File Offset: 0x00054089
			public ParameterOverride(string objectRegistrationName, string parameterName, InputBinding bindingMask, PrimitiveValue value = default(PrimitiveValue))
			{
				this.objectRegistrationName = objectRegistrationName;
				this.parameter = parameterName;
				this.bindingMask = bindingMask;
				this.value = value;
			}

			// Token: 0x06001232 RID: 4658 RVA: 0x00055EA8 File Offset: 0x000540A8
			public static InputActionRebindingExtensions.ParameterOverride? Find(InputActionMap actionMap, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? parameterOverride = InputActionRebindingExtensions.ParameterOverride.Find(actionMap.m_ParameterOverrides, actionMap.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName);
				InputActionAsset asset = actionMap.asset;
				InputActionRebindingExtensions.ParameterOverride? parameterOverride2 = ((asset != null) ? InputActionRebindingExtensions.ParameterOverride.Find(asset.m_ParameterOverrides, asset.m_ParameterOverridesCount, ref binding, parameterName, objectRegistrationName) : null);
				return InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(parameterOverride, parameterOverride2);
			}

			// Token: 0x06001233 RID: 4659 RVA: 0x00055F00 File Offset: 0x00054100
			private static InputActionRebindingExtensions.ParameterOverride? Find(InputActionRebindingExtensions.ParameterOverride[] overrides, int overrideCount, ref InputBinding binding, string parameterName, string objectRegistrationName)
			{
				InputActionRebindingExtensions.ParameterOverride? parameterOverride = null;
				for (int i = 0; i < overrideCount; i++)
				{
					ref InputActionRebindingExtensions.ParameterOverride ptr = ref overrides[i];
					if (string.Equals(parameterName, ptr.parameter, StringComparison.OrdinalIgnoreCase) && ptr.bindingMask.Matches(binding) && (ptr.objectRegistrationName == null || string.Equals(ptr.objectRegistrationName, objectRegistrationName, StringComparison.OrdinalIgnoreCase)))
					{
						if (parameterOverride == null)
						{
							parameterOverride = new InputActionRebindingExtensions.ParameterOverride?(ptr);
						}
						else
						{
							parameterOverride = InputActionRebindingExtensions.ParameterOverride.PickMoreSpecificOne(parameterOverride, new InputActionRebindingExtensions.ParameterOverride?(ptr));
						}
					}
				}
				return parameterOverride;
			}

			// Token: 0x06001234 RID: 4660 RVA: 0x00055F90 File Offset: 0x00054190
			private static InputActionRebindingExtensions.ParameterOverride? PickMoreSpecificOne(InputActionRebindingExtensions.ParameterOverride? first, InputActionRebindingExtensions.ParameterOverride? second)
			{
				if (first == null)
				{
					return second;
				}
				if (second == null)
				{
					return first;
				}
				if (first.Value.objectRegistrationName != null && second.Value.objectRegistrationName == null)
				{
					return first;
				}
				if (second.Value.objectRegistrationName != null && first.Value.objectRegistrationName == null)
				{
					return second;
				}
				InputActionRebindingExtensions.ParameterOverride parameterOverride = first.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.effectivePath != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.effectivePath == null)
					{
						return second;
					}
				}
				parameterOverride = first.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = second.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return first;
					}
				}
				parameterOverride = second.Value;
				if (parameterOverride.bindingMask.action != null)
				{
					parameterOverride = first.Value;
					if (parameterOverride.bindingMask.action == null)
					{
						return second;
					}
				}
				return first;
			}

			// Token: 0x04000782 RID: 1922
			public string objectRegistrationName;

			// Token: 0x04000783 RID: 1923
			public string parameter;

			// Token: 0x04000784 RID: 1924
			public InputBinding bindingMask;

			// Token: 0x04000785 RID: 1925
			public PrimitiveValue value;
		}

		// Token: 0x02000165 RID: 357
		public sealed class RebindingOperation : IDisposable
		{
			// Token: 0x170004E4 RID: 1252
			// (get) Token: 0x06001235 RID: 4661 RVA: 0x000560A8 File Offset: 0x000542A8
			public InputAction action
			{
				get
				{
					return this.m_ActionToRebind;
				}
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x06001236 RID: 4662 RVA: 0x000560B0 File Offset: 0x000542B0
			public InputBinding? bindingMask
			{
				get
				{
					return this.m_BindingMask;
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x06001237 RID: 4663 RVA: 0x000560B8 File Offset: 0x000542B8
			public InputControlList<InputControl> candidates
			{
				get
				{
					return this.m_Candidates;
				}
			}

			// Token: 0x170004E7 RID: 1255
			// (get) Token: 0x06001238 RID: 4664 RVA: 0x000560C0 File Offset: 0x000542C0
			public ReadOnlyArray<float> scores
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Scores, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06001239 RID: 4665 RVA: 0x000560D9 File Offset: 0x000542D9
			public ReadOnlyArray<float> magnitudes
			{
				get
				{
					return new ReadOnlyArray<float>(this.m_Magnitudes, 0, this.m_Candidates.Count);
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x0600123A RID: 4666 RVA: 0x000560F2 File Offset: 0x000542F2
			public InputControl selectedControl
			{
				get
				{
					if (this.m_Candidates.Count == 0)
					{
						return null;
					}
					return this.m_Candidates[0];
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x0600123B RID: 4667 RVA: 0x0005610F File Offset: 0x0005430F
			public bool started
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Started) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x0600123C RID: 4668 RVA: 0x0005611C File Offset: 0x0005431C
			public bool completed
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Completed) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x0600123D RID: 4669 RVA: 0x00056129 File Offset: 0x00054329
			public bool canceled
			{
				get
				{
					return (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.Canceled) > (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x0600123E RID: 4670 RVA: 0x00056136 File Offset: 0x00054336
			public double startTime
			{
				get
				{
					return this.m_StartTime;
				}
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x0600123F RID: 4671 RVA: 0x0005613E File Offset: 0x0005433E
			public float timeout
			{
				get
				{
					return this.m_Timeout;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06001240 RID: 4672 RVA: 0x00056146 File Offset: 0x00054346
			public string expectedControlType
			{
				get
				{
					return this.m_ExpectedLayout;
				}
			}

			// Token: 0x06001241 RID: 4673 RVA: 0x00056154 File Offset: 0x00054354
			public InputActionRebindingExtensions.RebindingOperation WithAction(InputAction action)
			{
				this.ThrowIfRebindInProgress();
				if (action == null)
				{
					throw new ArgumentNullException("action");
				}
				if (action.enabled)
				{
					throw new InvalidOperationException(string.Format("Cannot rebind action '{0}' while it is enabled", action));
				}
				this.m_ActionToRebind = action;
				if (!string.IsNullOrEmpty(action.expectedControlType))
				{
					this.WithExpectedControlType(action.expectedControlType);
				}
				else if (action.type == InputActionType.Button)
				{
					this.WithExpectedControlType("Button");
				}
				return this;
			}

			// Token: 0x06001242 RID: 4674 RVA: 0x000561C7 File Offset: 0x000543C7
			public InputActionRebindingExtensions.RebindingOperation WithMatchingEventsBeingSuppressed(bool value = true)
			{
				this.ThrowIfRebindInProgress();
				if (value)
				{
					this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				else
				{
					this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents;
				}
				return this;
			}

			// Token: 0x06001243 RID: 4675 RVA: 0x000561F9 File Offset: 0x000543F9
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(string binding)
			{
				this.ThrowIfRebindInProgress();
				this.m_CancelBinding = binding;
				return this;
			}

			// Token: 0x06001244 RID: 4676 RVA: 0x00056209 File Offset: 0x00054409
			public InputActionRebindingExtensions.RebindingOperation WithCancelingThrough(InputControl control)
			{
				this.ThrowIfRebindInProgress();
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				return this.WithCancelingThrough(control.path);
			}

			// Token: 0x06001245 RID: 4677 RVA: 0x0005622B File Offset: 0x0005442B
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(string layoutName)
			{
				this.ThrowIfRebindInProgress();
				this.m_ExpectedLayout = new InternedString(layoutName);
				return this;
			}

			// Token: 0x06001246 RID: 4678 RVA: 0x00056240 File Offset: 0x00054440
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType(Type type)
			{
				this.ThrowIfRebindInProgress();
				if (type != null && !typeof(InputControl).IsAssignableFrom(type))
				{
					throw new ArgumentException("Type '" + type.Name + "' is not an InputControl", "type");
				}
				this.m_ControlType = type;
				return this;
			}

			// Token: 0x06001247 RID: 4679 RVA: 0x00056296 File Offset: 0x00054496
			public InputActionRebindingExtensions.RebindingOperation WithExpectedControlType<TControl>() where TControl : InputControl
			{
				this.ThrowIfRebindInProgress();
				return this.WithExpectedControlType(typeof(TControl));
			}

			// Token: 0x06001248 RID: 4680 RVA: 0x000562B0 File Offset: 0x000544B0
			public InputActionRebindingExtensions.RebindingOperation WithTargetBinding(int bindingIndex)
			{
				if (bindingIndex < 0)
				{
					throw new ArgumentOutOfRangeException("bindingIndex");
				}
				this.m_TargetBindingIndex = bindingIndex;
				if (this.m_ActionToRebind != null && bindingIndex < this.m_ActionToRebind.bindings.Count)
				{
					InputBinding inputBinding = this.m_ActionToRebind.bindings[bindingIndex];
					if (inputBinding.isPartOfComposite)
					{
						string nameOfComposite = this.m_ActionToRebind.ChangeBinding(bindingIndex).PreviousCompositeBinding(null).binding.GetNameOfComposite();
						string name = inputBinding.name;
						string expectedControlLayoutName = InputBindingComposite.GetExpectedControlLayoutName(nameOfComposite, name);
						if (!string.IsNullOrEmpty(expectedControlLayoutName))
						{
							this.WithExpectedControlType(expectedControlLayoutName);
						}
					}
					InputActionMap actionMap = this.action.actionMap;
					InputActionAsset inputActionAsset = ((actionMap != null) ? actionMap.asset : null);
					if (inputActionAsset != null && !string.IsNullOrEmpty(inputBinding.groups))
					{
						string[] array = inputBinding.groups.Split(';', StringSplitOptions.None);
						for (int i = 0; i < array.Length; i++)
						{
							string group = array[i];
							int num = inputActionAsset.controlSchemes.IndexOf((InputControlScheme x) => group.Equals(x.bindingGroup, StringComparison.InvariantCultureIgnoreCase));
							if (num != -1)
							{
								foreach (InputControlScheme.DeviceRequirement deviceRequirement in inputActionAsset.controlSchemes[num].deviceRequirements)
								{
									this.WithControlsHavingToMatchPath(deviceRequirement.controlPath);
								}
							}
						}
					}
				}
				return this;
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x0005645C File Offset: 0x0005465C
			public InputActionRebindingExtensions.RebindingOperation WithBindingMask(InputBinding? bindingMask)
			{
				this.m_BindingMask = bindingMask;
				return this;
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x00056468 File Offset: 0x00054668
			public InputActionRebindingExtensions.RebindingOperation WithBindingGroup(string group)
			{
				return this.WithBindingMask(new InputBinding?(new InputBinding
				{
					groups = group
				}));
			}

			// Token: 0x0600124B RID: 4683 RVA: 0x00056491 File Offset: 0x00054691
			public InputActionRebindingExtensions.RebindingOperation WithoutGeneralizingPathOfSelectedControl()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl;
				return this;
			}

			// Token: 0x0600124C RID: 4684 RVA: 0x000564A6 File Offset: 0x000546A6
			public InputActionRebindingExtensions.RebindingOperation WithRebindAddingNewBinding(string group = null)
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding;
				this.m_BindingGroupForNewBinding = group;
				return this;
			}

			// Token: 0x0600124D RID: 4685 RVA: 0x000564C2 File Offset: 0x000546C2
			public InputActionRebindingExtensions.RebindingOperation WithMagnitudeHavingToBeGreaterThan(float magnitude)
			{
				this.ThrowIfRebindInProgress();
				if (magnitude < 0f)
				{
					throw new ArgumentException(string.Format("Magnitude has to be positive but was {0}", magnitude), "magnitude");
				}
				this.m_MagnitudeThreshold = magnitude;
				return this;
			}

			// Token: 0x0600124E RID: 4686 RVA: 0x000564F5 File Offset: 0x000546F5
			public InputActionRebindingExtensions.RebindingOperation WithoutIgnoringNoisyControls()
			{
				this.ThrowIfRebindInProgress();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls;
				return this;
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x00056510 File Offset: 0x00054710
			public InputActionRebindingExtensions.RebindingOperation WithControlsHavingToMatchPath(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_IncludePathCount; i++)
				{
					if (string.Compare(this.m_IncludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_IncludePaths, ref this.m_IncludePathCount, path, 10);
				return this;
			}

			// Token: 0x06001250 RID: 4688 RVA: 0x00056570 File Offset: 0x00054770
			public InputActionRebindingExtensions.RebindingOperation WithControlsExcluding(string path)
			{
				this.ThrowIfRebindInProgress();
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				for (int i = 0; i < this.m_ExcludePathCount; i++)
				{
					if (string.Compare(this.m_ExcludePaths[i], path, StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						return this;
					}
				}
				ArrayHelpers.AppendWithCapacity<string>(ref this.m_ExcludePaths, ref this.m_ExcludePathCount, path, 10);
				return this;
			}

			// Token: 0x06001251 RID: 4689 RVA: 0x000565D0 File Offset: 0x000547D0
			public InputActionRebindingExtensions.RebindingOperation WithTimeout(float timeInSeconds)
			{
				this.m_Timeout = timeInSeconds;
				return this;
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x000565DA File Offset: 0x000547DA
			public InputActionRebindingExtensions.RebindingOperation OnComplete(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnComplete = callback;
				return this;
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x000565E4 File Offset: 0x000547E4
			public InputActionRebindingExtensions.RebindingOperation OnCancel(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnCancel = callback;
				return this;
			}

			// Token: 0x06001254 RID: 4692 RVA: 0x000565EE File Offset: 0x000547EE
			public InputActionRebindingExtensions.RebindingOperation OnPotentialMatch(Action<InputActionRebindingExtensions.RebindingOperation> callback)
			{
				this.m_OnPotentialMatch = callback;
				return this;
			}

			// Token: 0x06001255 RID: 4693 RVA: 0x000565F8 File Offset: 0x000547F8
			public InputActionRebindingExtensions.RebindingOperation OnGeneratePath(Func<InputControl, string> callback)
			{
				this.m_OnGeneratePath = callback;
				return this;
			}

			// Token: 0x06001256 RID: 4694 RVA: 0x00056602 File Offset: 0x00054802
			public InputActionRebindingExtensions.RebindingOperation OnComputeScore(Func<InputControl, InputEventPtr, float> callback)
			{
				this.m_OnComputeScore = callback;
				return this;
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x0005660C File Offset: 0x0005480C
			public InputActionRebindingExtensions.RebindingOperation OnApplyBinding(Action<InputActionRebindingExtensions.RebindingOperation, string> callback)
			{
				this.m_OnApplyBinding = callback;
				return this;
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x00056616 File Offset: 0x00054816
			public InputActionRebindingExtensions.RebindingOperation OnMatchWaitForAnother(float seconds)
			{
				this.m_WaitSecondsAfterMatch = seconds;
				return this;
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x00056620 File Offset: 0x00054820
			public InputActionRebindingExtensions.RebindingOperation Start()
			{
				if (this.started)
				{
					return this;
				}
				if (this.m_ActionToRebind != null && this.m_ActionToRebind.bindings.Count == 0 && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					throw new InvalidOperationException(string.Format("Action '{0}' must have at least one existing binding or must be used with WithRebindingAddNewBinding()", this.action));
				}
				if (this.m_ActionToRebind == null && this.m_OnApplyBinding == null)
				{
					throw new InvalidOperationException("Must either have an action (call WithAction()) to apply binding to or have a custom callback to apply the binding (call OnApplyBinding())");
				}
				this.m_StartTime = InputState.currentTime;
				if (this.m_WaitSecondsAfterMatch > 0f || this.m_Timeout > 0f)
				{
					this.HookOnAfterUpdate();
					this.m_LastMatchTime = -1.0;
				}
				this.HookOnEvent();
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				return this;
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00056700 File Offset: 0x00054900
			public void Cancel()
			{
				if (!this.started)
				{
					return;
				}
				this.OnCancel();
			}

			// Token: 0x0600125B RID: 4699 RVA: 0x00056711 File Offset: 0x00054911
			public void Complete()
			{
				if (!this.started)
				{
					return;
				}
				this.OnComplete();
			}

			// Token: 0x0600125C RID: 4700 RVA: 0x00056724 File Offset: 0x00054924
			public void AddCandidate(InputControl control, float score, float magnitude = -1f)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int num = this.m_Candidates.IndexOf(control);
				if (num != -1)
				{
					this.m_Scores[num] = score;
				}
				else
				{
					int count = this.m_Candidates.Count;
					int count2 = this.m_Candidates.Count;
					this.m_Candidates.Add(control);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref count, score, 10);
					ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref count2, magnitude, 10);
				}
				this.SortCandidatesByScore();
			}

			// Token: 0x0600125D RID: 4701 RVA: 0x000567A8 File Offset: 0x000549A8
			public void RemoveCandidate(InputControl control)
			{
				if (control == null)
				{
					throw new ArgumentNullException("control");
				}
				int num = this.m_Candidates.IndexOf(control);
				if (num == -1)
				{
					return;
				}
				int count = this.m_Candidates.Count;
				this.m_Candidates.RemoveAt(num);
				this.m_Scores.EraseAtWithCapacity(ref count, num);
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x000567FB File Offset: 0x000549FB
			public void Dispose()
			{
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
				this.m_Candidates.Dispose();
				this.m_LayoutCache.Clear();
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x00056820 File Offset: 0x00054A20
			~RebindingOperation()
			{
				this.Dispose();
			}

			// Token: 0x06001260 RID: 4704 RVA: 0x0005684C File Offset: 0x00054A4C
			public InputActionRebindingExtensions.RebindingOperation Reset()
			{
				this.Cancel();
				this.m_ActionToRebind = null;
				this.m_BindingMask = null;
				this.m_ControlType = null;
				this.m_ExpectedLayout = default(InternedString);
				this.m_IncludePathCount = 0;
				this.m_ExcludePathCount = 0;
				this.m_TargetBindingIndex = -1;
				this.m_BindingGroupForNewBinding = null;
				this.m_CancelBinding = null;
				this.m_MagnitudeThreshold = 0.2f;
				this.m_Timeout = 0f;
				this.m_WaitSecondsAfterMatch = 0f;
				this.m_Flags = (InputActionRebindingExtensions.RebindingOperation.Flags)0;
				Dictionary<InputControl, float> startingActuations = this.m_StartingActuations;
				if (startingActuations != null)
				{
					startingActuations.Clear();
				}
				return this;
			}

			// Token: 0x06001261 RID: 4705 RVA: 0x000568E4 File Offset: 0x00054AE4
			private void HookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnEventDelegate == null)
				{
					this.m_OnEventDelegate = new Action<InputEventPtr, InputDevice>(this.OnEvent);
				}
				InputSystem.onEvent += this.m_OnEventDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x06001262 RID: 4706 RVA: 0x00056939 File Offset: 0x00054B39
			private void UnhookOnEvent()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onEvent -= this.m_OnEventDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnEventHooked;
			}

			// Token: 0x06001263 RID: 4707 RVA: 0x0005696C File Offset: 0x00054B6C
			private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
			{
				FourCC type = eventPtr.type;
				if (type != 1398030676 && type != 1145852993)
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				InputControlExtensions.Enumerate enumerate = InputControlExtensions.Enumerate.IncludeSyntheticControls | InputControlExtensions.Enumerate.IncludeNonLeafControls;
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontIgnoreNoisyControls) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					enumerate |= InputControlExtensions.Enumerate.IncludeNoisyControls;
				}
				foreach (InputControl inputControl in eventPtr.EnumerateControls(enumerate, device, 0f))
				{
					void* statePtrFromStateEventUnchecked = inputControl.GetStatePtrFromStateEventUnchecked(eventPtr, type);
					if (!string.IsNullOrEmpty(this.m_CancelBinding) && InputControlPath.Matches(this.m_CancelBinding, inputControl) && inputControl.HasValueChangeInState(statePtrFromStateEventUnchecked))
					{
						this.OnCancel();
						break;
					}
					if ((this.m_ExcludePathCount <= 0 || !InputActionRebindingExtensions.RebindingOperation.HavePathMatch(inputControl, this.m_ExcludePaths, this.m_ExcludePathCount)) && (this.m_IncludePathCount <= 0 || InputActionRebindingExtensions.RebindingOperation.HavePathMatch(inputControl, this.m_IncludePaths, this.m_IncludePathCount)) && (!(this.m_ControlType != null) || this.m_ControlType.IsInstanceOfType(inputControl)) && (this.m_ExpectedLayout.IsEmpty() || !(this.m_ExpectedLayout != inputControl.m_Layout) || InputControlLayout.s_Layouts.IsBasedOn(this.m_ExpectedLayout, inputControl.m_Layout)))
					{
						if (inputControl.CheckStateIsAtDefault(statePtrFromStateEventUnchecked, null))
						{
							if (!this.m_StartingActuations.ContainsKey(inputControl))
							{
								this.m_StartingActuations.Add(inputControl, 0f);
							}
							this.m_StartingActuations[inputControl] = 0f;
						}
						else
						{
							flag2 = true;
							float num = inputControl.EvaluateMagnitude(statePtrFromStateEventUnchecked);
							if (num >= 0f)
							{
								float magnitude;
								if (!this.m_StartingActuations.TryGetValue(inputControl, out magnitude))
								{
									magnitude = inputControl.magnitude;
									this.m_StartingActuations.Add(inputControl, magnitude);
								}
								if (Mathf.Abs(magnitude - num) < this.m_MagnitudeThreshold)
								{
									continue;
								}
							}
							float num2;
							if (this.m_OnComputeScore != null)
							{
								num2 = this.m_OnComputeScore(inputControl, eventPtr);
							}
							else
							{
								num2 = num;
								if (!inputControl.synthetic)
								{
									num2 += 1f;
								}
							}
							int num3 = this.m_Candidates.IndexOf(inputControl);
							if (num3 != -1)
							{
								if (this.m_Scores[num3] < num2)
								{
									flag = true;
									this.m_Scores[num3] = num2;
									if (this.m_WaitSecondsAfterMatch > 0f)
									{
										this.m_LastMatchTime = InputState.currentTime;
									}
								}
							}
							else
							{
								int count = this.m_Candidates.Count;
								int count2 = this.m_Candidates.Count;
								this.m_Candidates.Add(inputControl);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Scores, ref count, num2, 10);
								ArrayHelpers.AppendWithCapacity<float>(ref this.m_Magnitudes, ref count2, num, 10);
								flag = true;
								if (this.m_WaitSecondsAfterMatch > 0f)
								{
									this.m_LastMatchTime = InputState.currentTime;
								}
							}
						}
					}
				}
				if (flag2 && (this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.SuppressMatchingEvents) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					eventPtr.handled = true;
				}
				if (flag && !this.canceled)
				{
					if (this.m_OnPotentialMatch != null)
					{
						this.SortCandidatesByScore();
						this.m_OnPotentialMatch(this);
						return;
					}
					if (this.m_WaitSecondsAfterMatch <= 0f)
					{
						this.OnComplete();
						return;
					}
					this.SortCandidatesByScore();
				}
			}

			// Token: 0x06001264 RID: 4708 RVA: 0x00056CCC File Offset: 0x00054ECC
			private void SortCandidatesByScore()
			{
				int count = this.m_Candidates.Count;
				if (count <= 1)
				{
					return;
				}
				for (int i = 1; i < count; i++)
				{
					int num = i;
					while (num > 0 && this.m_Scores[num - 1] < this.m_Scores[num])
					{
						int num2 = num - 1;
						this.m_Scores.SwapElements(num, num2);
						this.m_Candidates.SwapElements(num, num2);
						this.m_Magnitudes.SwapElements(num, num2);
						num--;
					}
				}
			}

			// Token: 0x06001265 RID: 4709 RVA: 0x00056D44 File Offset: 0x00054F44
			private static bool HavePathMatch(InputControl control, string[] paths, int pathCount)
			{
				for (int i = 0; i < pathCount; i++)
				{
					if (InputControlPath.MatchesPrefix(paths[i], control))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001266 RID: 4710 RVA: 0x00056D6C File Offset: 0x00054F6C
			private void HookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				if (this.m_OnAfterUpdateDelegate == null)
				{
					this.m_OnAfterUpdateDelegate = new Action(this.OnAfterUpdate);
				}
				InputSystem.onAfterUpdate += this.m_OnAfterUpdateDelegate;
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x06001267 RID: 4711 RVA: 0x00056DB9 File Offset: 0x00054FB9
			private void UnhookOnAfterUpdate()
			{
				if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
				{
					return;
				}
				InputSystem.onAfterUpdate -= this.m_OnAfterUpdateDelegate;
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.OnAfterUpdateHooked;
			}

			// Token: 0x06001268 RID: 4712 RVA: 0x00056DE4 File Offset: 0x00054FE4
			private void OnAfterUpdate()
			{
				if (this.m_LastMatchTime < 0.0 && this.m_Timeout > 0f && InputState.currentTime - this.m_StartTime > (double)this.m_Timeout)
				{
					this.Cancel();
					return;
				}
				if (this.m_WaitSecondsAfterMatch <= 0f)
				{
					return;
				}
				if (this.m_LastMatchTime < 0.0)
				{
					return;
				}
				if (InputState.currentTime >= this.m_LastMatchTime + (double)this.m_WaitSecondsAfterMatch)
				{
					this.Complete();
				}
			}

			// Token: 0x06001269 RID: 4713 RVA: 0x00056E68 File Offset: 0x00055068
			private void OnComplete()
			{
				this.SortCandidatesByScore();
				if (this.m_Candidates.Count > 0)
				{
					InputControl inputControl = this.m_Candidates[0];
					string text = inputControl.path;
					if (this.m_OnGeneratePath != null)
					{
						string text2 = this.m_OnGeneratePath(inputControl);
						if (!string.IsNullOrEmpty(text2))
						{
							text = text2;
						}
						else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
						{
							text = this.GeneratePathForControl(inputControl);
						}
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.DontGeneralizePathOfSelectedControl) == (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						text = this.GeneratePathForControl(inputControl);
					}
					if (this.m_OnApplyBinding != null)
					{
						this.m_OnApplyBinding(this, text);
					}
					else if ((this.m_Flags & InputActionRebindingExtensions.RebindingOperation.Flags.AddNewBinding) != (InputActionRebindingExtensions.RebindingOperation.Flags)0)
					{
						this.m_ActionToRebind.AddBinding(text, null, null, this.m_BindingGroupForNewBinding);
					}
					else if (this.m_TargetBindingIndex >= 0)
					{
						if (this.m_TargetBindingIndex >= this.m_ActionToRebind.bindings.Count)
						{
							throw new InvalidOperationException(string.Format("Target binding index {0} out of range for action '{1}' with {2} bindings", this.m_TargetBindingIndex, this.m_ActionToRebind, this.m_ActionToRebind.bindings.Count));
						}
						this.m_ActionToRebind.ApplyBindingOverride(this.m_TargetBindingIndex, text);
					}
					else if (this.m_BindingMask != null)
					{
						InputBinding value = this.m_BindingMask.Value;
						value.overridePath = text;
						this.m_ActionToRebind.ApplyBindingOverride(value);
					}
					else
					{
						this.m_ActionToRebind.ApplyBindingOverride(text, null, null);
					}
				}
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Completed;
				Action<InputActionRebindingExtensions.RebindingOperation> onComplete = this.m_OnComplete;
				if (onComplete != null)
				{
					onComplete(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x0600126A RID: 4714 RVA: 0x00057007 File Offset: 0x00055207
			private void OnCancel()
			{
				this.m_Flags |= InputActionRebindingExtensions.RebindingOperation.Flags.Canceled;
				Action<InputActionRebindingExtensions.RebindingOperation> onCancel = this.m_OnCancel;
				if (onCancel != null)
				{
					onCancel(this);
				}
				this.ResetAfterMatchCompleted();
			}

			// Token: 0x0600126B RID: 4715 RVA: 0x00057030 File Offset: 0x00055230
			private void ResetAfterMatchCompleted()
			{
				this.m_Flags &= ~InputActionRebindingExtensions.RebindingOperation.Flags.Started;
				this.m_Candidates.Clear();
				this.m_Candidates.Capacity = 0;
				this.m_StartTime = -1.0;
				this.m_StartingActuations.Clear();
				this.UnhookOnEvent();
				this.UnhookOnAfterUpdate();
			}

			// Token: 0x0600126C RID: 4716 RVA: 0x00057089 File Offset: 0x00055289
			private void ThrowIfRebindInProgress()
			{
				if (this.started)
				{
					throw new InvalidOperationException("Cannot reconfigure rebinding while operation is in progress");
				}
			}

			// Token: 0x0600126D RID: 4717 RVA: 0x000570A0 File Offset: 0x000552A0
			private string GeneratePathForControl(InputControl control)
			{
				InputDevice device = control.device;
				InternedString internedString = InputControlLayout.s_Layouts.FindLayoutThatIntroducesControl(control, this.m_LayoutCache);
				if (this.m_PathBuilder == null)
				{
					this.m_PathBuilder = new StringBuilder();
				}
				else
				{
					this.m_PathBuilder.Length = 0;
				}
				control.BuildPath(internedString, this.m_PathBuilder);
				return this.m_PathBuilder.ToString();
			}

			// Token: 0x04000786 RID: 1926
			public const float kDefaultMagnitudeThreshold = 0.2f;

			// Token: 0x04000787 RID: 1927
			private InputAction m_ActionToRebind;

			// Token: 0x04000788 RID: 1928
			private InputBinding? m_BindingMask;

			// Token: 0x04000789 RID: 1929
			private Type m_ControlType;

			// Token: 0x0400078A RID: 1930
			private InternedString m_ExpectedLayout;

			// Token: 0x0400078B RID: 1931
			private int m_IncludePathCount;

			// Token: 0x0400078C RID: 1932
			private string[] m_IncludePaths;

			// Token: 0x0400078D RID: 1933
			private int m_ExcludePathCount;

			// Token: 0x0400078E RID: 1934
			private string[] m_ExcludePaths;

			// Token: 0x0400078F RID: 1935
			private int m_TargetBindingIndex = -1;

			// Token: 0x04000790 RID: 1936
			private string m_BindingGroupForNewBinding;

			// Token: 0x04000791 RID: 1937
			private string m_CancelBinding;

			// Token: 0x04000792 RID: 1938
			private float m_MagnitudeThreshold = 0.2f;

			// Token: 0x04000793 RID: 1939
			private float[] m_Scores;

			// Token: 0x04000794 RID: 1940
			private float[] m_Magnitudes;

			// Token: 0x04000795 RID: 1941
			private double m_LastMatchTime;

			// Token: 0x04000796 RID: 1942
			private double m_StartTime;

			// Token: 0x04000797 RID: 1943
			private float m_Timeout;

			// Token: 0x04000798 RID: 1944
			private float m_WaitSecondsAfterMatch;

			// Token: 0x04000799 RID: 1945
			private InputControlList<InputControl> m_Candidates;

			// Token: 0x0400079A RID: 1946
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnComplete;

			// Token: 0x0400079B RID: 1947
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnCancel;

			// Token: 0x0400079C RID: 1948
			private Action<InputActionRebindingExtensions.RebindingOperation> m_OnPotentialMatch;

			// Token: 0x0400079D RID: 1949
			private Func<InputControl, string> m_OnGeneratePath;

			// Token: 0x0400079E RID: 1950
			private Func<InputControl, InputEventPtr, float> m_OnComputeScore;

			// Token: 0x0400079F RID: 1951
			private Action<InputActionRebindingExtensions.RebindingOperation, string> m_OnApplyBinding;

			// Token: 0x040007A0 RID: 1952
			private Action<InputEventPtr, InputDevice> m_OnEventDelegate;

			// Token: 0x040007A1 RID: 1953
			private Action m_OnAfterUpdateDelegate;

			// Token: 0x040007A2 RID: 1954
			private InputControlLayout.Cache m_LayoutCache;

			// Token: 0x040007A3 RID: 1955
			private StringBuilder m_PathBuilder;

			// Token: 0x040007A4 RID: 1956
			private InputActionRebindingExtensions.RebindingOperation.Flags m_Flags;

			// Token: 0x040007A5 RID: 1957
			private Dictionary<InputControl, float> m_StartingActuations = new Dictionary<InputControl, float>();

			// Token: 0x02000258 RID: 600
			[Flags]
			private enum Flags
			{
				// Token: 0x04000C49 RID: 3145
				Started = 1,
				// Token: 0x04000C4A RID: 3146
				Completed = 2,
				// Token: 0x04000C4B RID: 3147
				Canceled = 4,
				// Token: 0x04000C4C RID: 3148
				OnEventHooked = 8,
				// Token: 0x04000C4D RID: 3149
				OnAfterUpdateHooked = 16,
				// Token: 0x04000C4E RID: 3150
				DontIgnoreNoisyControls = 64,
				// Token: 0x04000C4F RID: 3151
				DontGeneralizePathOfSelectedControl = 128,
				// Token: 0x04000C50 RID: 3152
				AddNewBinding = 256,
				// Token: 0x04000C51 RID: 3153
				SuppressMatchingEvents = 512
			}
		}

		// Token: 0x02000166 RID: 358
		internal class DeferBindingResolutionWrapper : IDisposable
		{
			// Token: 0x0600126F RID: 4719 RVA: 0x0005712A File Offset: 0x0005532A
			public void Acquire()
			{
				InputActionMap.s_DeferBindingResolution++;
			}

			// Token: 0x06001270 RID: 4720 RVA: 0x00057138 File Offset: 0x00055338
			public void Dispose()
			{
				if (InputActionMap.s_DeferBindingResolution > 0)
				{
					InputActionMap.s_DeferBindingResolution--;
				}
				if (InputActionMap.s_DeferBindingResolution == 0)
				{
					InputActionState.DeferredResolutionOfBindings();
				}
			}
		}
	}
}
