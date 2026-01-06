using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000108 RID: 264
	public class InputControlLayout
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00047BE4 File Offset: 0x00045DE4
		public static InternedString DefaultVariant
		{
			get
			{
				return InputControlLayout.s_DefaultVariant;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00047BEB File Offset: 0x00045DEB
		public InternedString name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00047BF3 File Offset: 0x00045DF3
		public string displayName
		{
			get
			{
				return this.m_DisplayName ?? this.m_Name;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00047C0A File Offset: 0x00045E0A
		public Type type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00047C12 File Offset: 0x00045E12
		public InternedString variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00047C1A File Offset: 0x00045E1A
		public FourCC stateFormat
		{
			get
			{
				return this.m_StateFormat;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00047C22 File Offset: 0x00045E22
		public int stateSizeInBytes
		{
			get
			{
				return this.m_StateSizeInBytes;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00047C2A File Offset: 0x00045E2A
		public IEnumerable<InternedString> baseLayouts
		{
			get
			{
				return this.m_BaseLayouts;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00047C37 File Offset: 0x00045E37
		public IEnumerable<InternedString> appliedOverrides
		{
			get
			{
				return this.m_AppliedOverrides;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00047C44 File Offset: 0x00045E44
		public ReadOnlyArray<InternedString> commonUsages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_CommonUsages);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00047C51 File Offset: 0x00045E51
		public ReadOnlyArray<InputControlLayout.ControlItem> controls
		{
			get
			{
				return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00047C5E File Offset: 0x00045E5E
		public bool updateBeforeRender
		{
			get
			{
				return this.m_UpdateBeforeRender.GetValueOrDefault();
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00047C6B File Offset: 0x00045E6B
		public bool isDeviceLayout
		{
			get
			{
				return typeof(InputDevice).IsAssignableFrom(this.m_Type);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00047C82 File Offset: 0x00045E82
		public bool isControlLayout
		{
			get
			{
				return !this.isDeviceLayout;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00047C8D File Offset: 0x00045E8D
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x00047C9A File Offset: 0x00045E9A
		public bool isOverride
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsOverride) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsOverride;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsOverride;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00047CBD File Offset: 0x00045EBD
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00047CCA File Offset: 0x00045ECA
		public bool isGenericTypeOfDevice
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsGenericTypeOfDevice) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsGenericTypeOfDevice;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsGenericTypeOfDevice;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00047CED File Offset: 0x00045EED
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x00047CFA File Offset: 0x00045EFA
		public bool hideInUI
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.HideInUI) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.HideInUI;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.HideInUI;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00047D1D File Offset: 0x00045F1D
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00047D2B File Offset: 0x00045F2B
		public bool isNoisy
		{
			get
			{
				return (this.m_Flags & InputControlLayout.Flags.IsNoisy) > (InputControlLayout.Flags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_Flags |= InputControlLayout.Flags.IsNoisy;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.IsNoisy;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00047D50 File Offset: 0x00045F50
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x00047D84 File Offset: 0x00045F84
		public bool? canRunInBackground
		{
			get
			{
				if ((this.m_Flags & InputControlLayout.Flags.CanRunInBackgroundIsSet) == (InputControlLayout.Flags)0)
				{
					return null;
				}
				return new bool?((this.m_Flags & InputControlLayout.Flags.CanRunInBackground) > (InputControlLayout.Flags)0);
			}
			internal set
			{
				if (value == null)
				{
					this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackgroundIsSet;
					return;
				}
				this.m_Flags |= InputControlLayout.Flags.CanRunInBackgroundIsSet;
				if (value.Value)
				{
					this.m_Flags |= InputControlLayout.Flags.CanRunInBackground;
					return;
				}
				this.m_Flags &= ~InputControlLayout.Flags.CanRunInBackground;
			}
		}

		// Token: 0x1700045D RID: 1117
		public InputControlLayout.ControlItem this[string path]
		{
			get
			{
				if (string.IsNullOrEmpty(path))
				{
					throw new ArgumentNullException("path");
				}
				if (this.m_Controls != null)
				{
					for (int i = 0; i < this.m_Controls.Length; i++)
					{
						if (this.m_Controls[i].name == path)
						{
							return this.m_Controls[i];
						}
					}
				}
				throw new KeyNotFoundException(string.Format("Cannot find control '{0}' in layout '{1}'", path, this.name));
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00047E5C File Offset: 0x0004605C
		public InputControlLayout.ControlItem? FindControl(InternedString path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			if (this.m_Controls == null)
			{
				return null;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				if (this.m_Controls[i].name == path)
				{
					return new InputControlLayout.ControlItem?(this.m_Controls[i]);
				}
			}
			return null;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00047EDC File Offset: 0x000460DC
		public InputControlLayout.ControlItem? FindControlIncludingArrayElements(string path, out int arrayIndex)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			arrayIndex = -1;
			if (this.m_Controls == null)
			{
				return null;
			}
			int num = 0;
			int num2 = path.Length;
			while (num2 > 0 && char.IsDigit(path[num2 - 1]))
			{
				num2--;
				num *= 10;
				num += (int)(path[num2] - '0');
			}
			int num3 = 0;
			if (num2 < path.Length && num2 > 0)
			{
				num3 = num2;
			}
			for (int i = 0; i < this.m_Controls.Length; i++)
			{
				ref InputControlLayout.ControlItem ptr = ref this.m_Controls[i];
				if (string.Compare(ptr.name, path, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return new InputControlLayout.ControlItem?(ptr);
				}
				if (ptr.isArray && num3 > 0 && num3 == ptr.name.length && string.Compare(ptr.name.ToString(), 0, path, 0, num3, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					arrayIndex = num;
					return new InputControlLayout.ControlItem?(ptr);
				}
			}
			return null;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00048000 File Offset: 0x00046200
		public Type GetValueType()
		{
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(this.type, typeof(InputControl<>), 0);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00048018 File Offset: 0x00046218
		public static InputControlLayout FromType(string name, Type type)
		{
			List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
			InputControlLayoutAttribute customAttribute = type.GetCustomAttribute(true);
			FourCC fourCC = default(FourCC);
			if (customAttribute != null && customAttribute.stateType != null)
			{
				InputControlLayout.AddControlItems(customAttribute.stateType, list, name);
				if (typeof(IInputStateTypeInfo).IsAssignableFrom(customAttribute.stateType))
				{
					fourCC = ((IInputStateTypeInfo)Activator.CreateInstance(customAttribute.stateType)).format;
				}
			}
			else
			{
				InputControlLayout.AddControlItems(type, list, name);
			}
			if (customAttribute != null && !string.IsNullOrEmpty(customAttribute.stateFormat))
			{
				fourCC = new FourCC(customAttribute.stateFormat);
			}
			InternedString internedString = default(InternedString);
			if (customAttribute != null)
			{
				internedString = new InternedString(customAttribute.variants);
			}
			InputControlLayout inputControlLayout = new InputControlLayout(name, type)
			{
				m_Controls = list.ToArray(),
				m_StateFormat = fourCC,
				m_Variants = internedString,
				m_UpdateBeforeRender = ((customAttribute != null) ? customAttribute.updateBeforeRenderInternal : null),
				isGenericTypeOfDevice = (customAttribute != null && customAttribute.isGenericTypeOfDevice),
				hideInUI = (customAttribute != null && customAttribute.hideInUI),
				m_Description = ((customAttribute != null) ? customAttribute.description : null),
				m_DisplayName = ((customAttribute != null) ? customAttribute.displayName : null),
				canRunInBackground = ((customAttribute != null) ? customAttribute.canRunInBackgroundInternal : null),
				isNoisy = (customAttribute != null && customAttribute.isNoisy)
			};
			if (((customAttribute != null) ? customAttribute.commonUsages : null) != null)
			{
				inputControlLayout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(customAttribute.commonUsages, (string x) => new InternedString(x));
			}
			return inputControlLayout;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000481B9 File Offset: 0x000463B9
		public string ToJson()
		{
			return JsonUtility.ToJson(InputControlLayout.LayoutJson.FromLayout(this), true);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000481CC File Offset: 0x000463CC
		public static InputControlLayout FromJson(string json)
		{
			return JsonUtility.FromJson<InputControlLayout.LayoutJson>(json).ToLayout();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000481E7 File Offset: 0x000463E7
		private InputControlLayout(string name, Type type)
		{
			this.m_Name = new InternedString(name);
			this.m_Type = type;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00048202 File Offset: 0x00046402
		private static void AddControlItems(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			InputControlLayout.AddControlItemsFromFields(type, controlLayouts, layoutName);
			InputControlLayout.AddControlItemsFromProperties(type, controlLayouts, layoutName);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00048214 File Offset: 0x00046414
		private static void AddControlItemsFromFields(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(fields, controlLayouts, layoutName);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00048234 File Offset: 0x00046434
		private static void AddControlItemsFromProperties(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(properties, controlLayouts, layoutName);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00048254 File Offset: 0x00046454
		private static void AddControlItemsFromMembers(MemberInfo[] members, List<InputControlLayout.ControlItem> controlItems, string layoutName)
		{
			foreach (MemberInfo memberInfo in members)
			{
				if (!(memberInfo.DeclaringType == typeof(InputControl)))
				{
					Type valueType = TypeHelpers.GetValueType(memberInfo);
					if (valueType != null && valueType.IsValueType && typeof(IInputStateTypeInfo).IsAssignableFrom(valueType))
					{
						int count = controlItems.Count;
						InputControlLayout.AddControlItems(valueType, controlItems, layoutName);
						if (memberInfo as FieldInfo != null)
						{
							int num = Marshal.OffsetOf(memberInfo.DeclaringType, memberInfo.Name).ToInt32();
							int count2 = controlItems.Count;
							for (int j = count; j < count2; j++)
							{
								InputControlLayout.ControlItem controlItem = controlItems[j];
								if (controlItems[j].offset != 4294967295U)
								{
									controlItem.offset += (uint)num;
									controlItems[j] = controlItem;
								}
							}
						}
					}
					InputControlAttribute[] array = memberInfo.GetCustomAttributes(false).ToArray<InputControlAttribute>();
					if (array.Length != 0 || (!(valueType == null) && typeof(InputControl).IsAssignableFrom(valueType) && !(memberInfo is PropertyInfo)))
					{
						InputControlLayout.AddControlItemsFromMember(memberInfo, array, controlItems);
					}
				}
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00048394 File Offset: 0x00046594
		private static void AddControlItemsFromMember(MemberInfo member, InputControlAttribute[] attributes, List<InputControlLayout.ControlItem> controlItems)
		{
			if (attributes.Length == 0)
			{
				InputControlLayout.ControlItem controlItem = InputControlLayout.CreateControlItemFromMember(member, null);
				controlItems.Add(controlItem);
				return;
			}
			foreach (InputControlAttribute inputControlAttribute in attributes)
			{
				InputControlLayout.ControlItem controlItem2 = InputControlLayout.CreateControlItemFromMember(member, inputControlAttribute);
				controlItems.Add(controlItem2);
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000483DC File Offset: 0x000465DC
		private static InputControlLayout.ControlItem CreateControlItemFromMember(MemberInfo member, InputControlAttribute attribute)
		{
			string text = ((attribute != null) ? attribute.name : null);
			if (string.IsNullOrEmpty(text))
			{
				text = member.Name;
			}
			bool flag = text.IndexOf('/') != -1;
			string text2 = ((attribute != null) ? attribute.displayName : null);
			string text3 = ((attribute != null) ? attribute.shortDisplayName : null);
			string text4 = ((attribute != null) ? attribute.layout : null);
			if (string.IsNullOrEmpty(text4) && !flag && (!(member is FieldInfo) || member.GetCustomAttribute(false) == null))
			{
				text4 = InputControlLayout.InferLayoutFromValueType(TypeHelpers.GetValueType(member));
			}
			string text5 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.variants))
			{
				text5 = attribute.variants;
			}
			uint num = uint.MaxValue;
			if (attribute != null && attribute.offset != 4294967295U)
			{
				num = attribute.offset;
			}
			else if (member is FieldInfo && !flag)
			{
				num = (uint)Marshal.OffsetOf(member.DeclaringType, member.Name).ToInt32();
			}
			uint num2 = uint.MaxValue;
			if (attribute != null)
			{
				num2 = attribute.bit;
			}
			uint num3 = 0U;
			if (attribute != null)
			{
				num3 = attribute.sizeInBits;
			}
			FourCC fourCC = default(FourCC);
			if (attribute != null && !string.IsNullOrEmpty(attribute.format))
			{
				fourCC = new FourCC(attribute.format);
			}
			else if (!flag && num2 == 4294967295U)
			{
				fourCC = InputStateBlock.GetPrimitiveFormatFromType(TypeHelpers.GetValueType(member));
			}
			InternedString[] array = null;
			if (attribute != null)
			{
				string[] array2 = ArrayHelpers.Join<string>(attribute.alias, attribute.aliases);
				if (array2 != null)
				{
					array = array2.Select((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			InternedString[] array3 = null;
			if (attribute != null)
			{
				string[] array4 = ArrayHelpers.Join<string>(attribute.usage, attribute.usages);
				if (array4 != null)
				{
					array3 = array4.Select((string x) => new InternedString(x)).ToArray<InternedString>();
				}
			}
			NamedValue[] array5 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.parameters))
			{
				array5 = NamedValue.ParseMultiple(attribute.parameters);
			}
			NameAndParameters[] array6 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.processors))
			{
				array6 = NameAndParameters.ParseMultiple(attribute.processors).ToArray<NameAndParameters>();
			}
			string text6 = null;
			if (attribute != null && !string.IsNullOrEmpty(attribute.useStateFrom))
			{
				text6 = attribute.useStateFrom;
			}
			bool flag2 = false;
			if (attribute != null)
			{
				flag2 = attribute.noisy;
			}
			bool flag3 = false;
			if (attribute != null)
			{
				flag3 = attribute.dontReset;
			}
			bool flag4 = false;
			if (attribute != null)
			{
				flag4 = attribute.synthetic;
			}
			int num4 = 0;
			if (attribute != null)
			{
				num4 = attribute.arraySize;
			}
			PrimitiveValue primitiveValue = default(PrimitiveValue);
			if (attribute != null)
			{
				primitiveValue = PrimitiveValue.FromObject(attribute.defaultState);
			}
			PrimitiveValue primitiveValue2 = default(PrimitiveValue);
			PrimitiveValue primitiveValue3 = default(PrimitiveValue);
			if (attribute != null)
			{
				primitiveValue2 = PrimitiveValue.FromObject(attribute.minValue);
				primitiveValue3 = PrimitiveValue.FromObject(attribute.maxValue);
			}
			return new InputControlLayout.ControlItem
			{
				name = new InternedString(text),
				displayName = text2,
				shortDisplayName = text3,
				layout = new InternedString(text4),
				variants = new InternedString(text5),
				useStateFrom = text6,
				format = fourCC,
				offset = num,
				bit = num2,
				sizeInBits = num3,
				parameters = new ReadOnlyArray<NamedValue>(array5),
				processors = new ReadOnlyArray<NameAndParameters>(array6),
				usages = new ReadOnlyArray<InternedString>(array3),
				aliases = new ReadOnlyArray<InternedString>(array),
				isModifyingExistingControl = flag,
				isFirstDefinedInThisLayout = true,
				isNoisy = flag2,
				dontReset = flag3,
				isSynthetic = flag4,
				arraySize = num4,
				defaultState = primitiveValue,
				minValue = primitiveValue2,
				maxValue = primitiveValue3
			};
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00048784 File Offset: 0x00046984
		private static string InferLayoutFromValueType(Type type)
		{
			InternedString internedString = InputControlLayout.s_Layouts.TryFindLayoutForType(type);
			if (internedString.IsEmpty())
			{
				InternedString internedString2 = new InternedString(type.Name);
				if (InputControlLayout.s_Layouts.HasLayout(internedString2))
				{
					internedString = internedString2;
				}
				else if (type.Name.EndsWith("Control"))
				{
					internedString2 = new InternedString(type.Name.Substring(0, type.Name.Length - "Control".Length));
					if (InputControlLayout.s_Layouts.HasLayout(internedString2))
					{
						internedString = internedString2;
					}
				}
			}
			return internedString;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00048814 File Offset: 0x00046A14
		public void MergeLayout(InputControlLayout other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			bool? updateBeforeRender = this.m_UpdateBeforeRender;
			this.m_UpdateBeforeRender = ((updateBeforeRender != null) ? updateBeforeRender : other.m_UpdateBeforeRender);
			if (this.m_Variants.IsEmpty())
			{
				this.m_Variants = other.m_Variants;
			}
			if (this.m_Type == null)
			{
				this.m_Type = other.m_Type;
			}
			else if (this.m_Type.IsAssignableFrom(other.m_Type))
			{
				this.m_Type = other.m_Type;
			}
			bool flag = !this.m_Variants.IsEmpty();
			if (this.m_StateFormat == default(FourCC))
			{
				this.m_StateFormat = other.m_StateFormat;
			}
			this.m_CommonUsages = ArrayHelpers.Merge<InternedString>(other.m_CommonUsages, this.m_CommonUsages);
			this.m_AppliedOverrides.Merge(other.m_AppliedOverrides);
			if (string.IsNullOrEmpty(this.m_DisplayName))
			{
				this.m_DisplayName = other.m_DisplayName;
			}
			if (this.m_Controls == null)
			{
				this.m_Controls = other.m_Controls;
				return;
			}
			if (other.m_Controls != null)
			{
				InputControlLayout.ControlItem[] controls = other.m_Controls;
				List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
				List<string> list2 = new List<string>();
				Dictionary<string, InputControlLayout.ControlItem> dictionary = InputControlLayout.CreateLookupTableForControls(controls, list2);
				foreach (KeyValuePair<string, InputControlLayout.ControlItem> keyValuePair in InputControlLayout.CreateLookupTableForControls(this.m_Controls, null))
				{
					InputControlLayout.ControlItem controlItem;
					if (dictionary.TryGetValue(keyValuePair.Key, out controlItem))
					{
						InputControlLayout.ControlItem controlItem2 = keyValuePair.Value.Merge(controlItem);
						list.Add(controlItem2);
						dictionary.Remove(keyValuePair.Key);
					}
					else if (keyValuePair.Value.variants.IsEmpty() || keyValuePair.Value.variants == InputControlLayout.DefaultVariant)
					{
						bool flag2 = false;
						if (flag)
						{
							for (int i = 0; i < list2.Count; i++)
							{
								if (InputControlLayout.VariantsMatch(this.m_Variants.ToLower(), list2[i]))
								{
									string text = keyValuePair.Key + "@" + list2[i];
									if (dictionary.TryGetValue(text, out controlItem))
									{
										InputControlLayout.ControlItem controlItem3 = keyValuePair.Value.Merge(controlItem);
										list.Add(controlItem3);
										dictionary.Remove(text);
										flag2 = true;
									}
								}
							}
						}
						else
						{
							foreach (string text2 in list2)
							{
								string text3 = keyValuePair.Key + "@" + text2;
								if (dictionary.TryGetValue(text3, out controlItem))
								{
									InputControlLayout.ControlItem controlItem4 = keyValuePair.Value.Merge(controlItem);
									list.Add(controlItem4);
									dictionary.Remove(text3);
									flag2 = true;
								}
							}
						}
						if (!flag2)
						{
							list.Add(keyValuePair.Value);
						}
					}
					else if (dictionary.TryGetValue(keyValuePair.Value.name.ToLower(), out controlItem))
					{
						InputControlLayout.ControlItem controlItem5 = keyValuePair.Value.Merge(controlItem);
						list.Add(controlItem5);
						dictionary.Remove(keyValuePair.Value.name.ToLower());
					}
					else if (InputControlLayout.VariantsMatch(this.m_Variants, keyValuePair.Value.variants))
					{
						list.Add(keyValuePair.Value);
					}
				}
				if (!flag)
				{
					int count = list.Count;
					list.AddRange(dictionary.Values);
					for (int j = count; j < list.Count; j++)
					{
						InputControlLayout.ControlItem controlItem6 = list[j];
						controlItem6.isFirstDefinedInThisLayout = false;
						list[j] = controlItem6;
					}
				}
				else
				{
					int count2 = list.Count;
					list.AddRange(dictionary.Values.Where((InputControlLayout.ControlItem x) => InputControlLayout.VariantsMatch(this.m_Variants, x.variants)));
					for (int k = count2; k < list.Count; k++)
					{
						InputControlLayout.ControlItem controlItem7 = list[k];
						controlItem7.isFirstDefinedInThisLayout = false;
						list[k] = controlItem7;
					}
				}
				this.m_Controls = list.ToArray();
			}
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00048C94 File Offset: 0x00046E94
		private static Dictionary<string, InputControlLayout.ControlItem> CreateLookupTableForControls(InputControlLayout.ControlItem[] controlItems, List<string> variants = null)
		{
			Dictionary<string, InputControlLayout.ControlItem> dictionary = new Dictionary<string, InputControlLayout.ControlItem>();
			int i = 0;
			while (i < controlItems.Length)
			{
				string text = controlItems[i].name.ToLower();
				InternedString variants2 = controlItems[i].variants;
				if (variants2.IsEmpty() || !(variants2 != InputControlLayout.DefaultVariant))
				{
					goto IL_00EC;
				}
				if (variants2.ToString().IndexOf(";"[0]) != -1)
				{
					foreach (string text2 in variants2.ToLower().Split(";"[0], StringSplitOptions.None))
					{
						if (variants != null)
						{
							variants.Add(text2);
						}
						text = text + "@" + text2;
						dictionary[text] = controlItems[i];
					}
				}
				else
				{
					text = text + "@" + variants2.ToLower();
					if (variants != null)
					{
						variants.Add(variants2.ToLower());
						goto IL_00EC;
					}
					goto IL_00EC;
				}
				IL_00FA:
				i++;
				continue;
				IL_00EC:
				dictionary[text] = controlItems[i];
				goto IL_00FA;
			}
			return dictionary;
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00048DA9 File Offset: 0x00046FA9
		internal static bool VariantsMatch(InternedString expected, InternedString actual)
		{
			return InputControlLayout.VariantsMatch(expected.ToLower(), actual.ToLower());
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x00048DBE File Offset: 0x00046FBE
		internal static bool VariantsMatch(string expected, string actual)
		{
			return (actual != null && StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(InputControlLayout.DefaultVariant, actual, ";"[0])) || expected == null || actual == null || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(expected, actual, ";"[0]);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00048E00 File Offset: 0x00047000
		internal static void ParseHeaderFieldsFromJson(string json, out InternedString name, out InlinedArray<InternedString> baseLayouts, out InputDeviceMatcher deviceMatcher)
		{
			InputControlLayout.LayoutJsonNameAndDescriptorOnly layoutJsonNameAndDescriptorOnly = JsonUtility.FromJson<InputControlLayout.LayoutJsonNameAndDescriptorOnly>(json);
			name = new InternedString(layoutJsonNameAndDescriptorOnly.name);
			baseLayouts = default(InlinedArray<InternedString>);
			if (!string.IsNullOrEmpty(layoutJsonNameAndDescriptorOnly.extend))
			{
				baseLayouts.Append(new InternedString(layoutJsonNameAndDescriptorOnly.extend));
			}
			if (layoutJsonNameAndDescriptorOnly.extendMultiple != null)
			{
				foreach (string text in layoutJsonNameAndDescriptorOnly.extendMultiple)
				{
					baseLayouts.Append(new InternedString(text));
				}
			}
			deviceMatcher = layoutJsonNameAndDescriptorOnly.device.ToMatcher();
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x00048E8B File Offset: 0x0004708B
		internal static ref InputControlLayout.Cache cache
		{
			get
			{
				return ref InputControlLayout.s_CacheInstance;
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00048E94 File Offset: 0x00047094
		internal static InputControlLayout.CacheRefInstance CacheRef()
		{
			InputControlLayout.s_CacheInstanceRef++;
			return new InputControlLayout.CacheRefInstance
			{
				valid = true
			};
		}

		// Token: 0x04000626 RID: 1574
		private static InternedString s_DefaultVariant = new InternedString("Default");

		// Token: 0x04000627 RID: 1575
		public const string VariantSeparator = ";";

		// Token: 0x04000628 RID: 1576
		private InternedString m_Name;

		// Token: 0x04000629 RID: 1577
		private Type m_Type;

		// Token: 0x0400062A RID: 1578
		private InternedString m_Variants;

		// Token: 0x0400062B RID: 1579
		private FourCC m_StateFormat;

		// Token: 0x0400062C RID: 1580
		internal int m_StateSizeInBytes;

		// Token: 0x0400062D RID: 1581
		internal bool? m_UpdateBeforeRender;

		// Token: 0x0400062E RID: 1582
		internal InlinedArray<InternedString> m_BaseLayouts;

		// Token: 0x0400062F RID: 1583
		private InlinedArray<InternedString> m_AppliedOverrides;

		// Token: 0x04000630 RID: 1584
		private InternedString[] m_CommonUsages;

		// Token: 0x04000631 RID: 1585
		internal InputControlLayout.ControlItem[] m_Controls;

		// Token: 0x04000632 RID: 1586
		internal string m_DisplayName;

		// Token: 0x04000633 RID: 1587
		private string m_Description;

		// Token: 0x04000634 RID: 1588
		private InputControlLayout.Flags m_Flags;

		// Token: 0x04000635 RID: 1589
		internal static InputControlLayout.Collection s_Layouts;

		// Token: 0x04000636 RID: 1590
		internal static InputControlLayout.Cache s_CacheInstance;

		// Token: 0x04000637 RID: 1591
		internal static int s_CacheInstanceRef;

		// Token: 0x0200021E RID: 542
		public struct ControlItem
		{
			// Token: 0x170005A8 RID: 1448
			// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00060863 File Offset: 0x0005EA63
			// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0006086B File Offset: 0x0005EA6B
			public InternedString name { readonly get; internal set; }

			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00060874 File Offset: 0x0005EA74
			// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0006087C File Offset: 0x0005EA7C
			public InternedString layout { readonly get; internal set; }

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00060885 File Offset: 0x0005EA85
			// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0006088D File Offset: 0x0005EA8D
			public InternedString variants { readonly get; internal set; }

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00060896 File Offset: 0x0005EA96
			// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0006089E File Offset: 0x0005EA9E
			public string useStateFrom { readonly get; internal set; }

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x060014FA RID: 5370 RVA: 0x000608A7 File Offset: 0x0005EAA7
			// (set) Token: 0x060014FB RID: 5371 RVA: 0x000608AF File Offset: 0x0005EAAF
			public string displayName { readonly get; internal set; }

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x060014FC RID: 5372 RVA: 0x000608B8 File Offset: 0x0005EAB8
			// (set) Token: 0x060014FD RID: 5373 RVA: 0x000608C0 File Offset: 0x0005EAC0
			public string shortDisplayName { readonly get; internal set; }

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x060014FE RID: 5374 RVA: 0x000608C9 File Offset: 0x0005EAC9
			// (set) Token: 0x060014FF RID: 5375 RVA: 0x000608D1 File Offset: 0x0005EAD1
			public ReadOnlyArray<InternedString> usages { readonly get; internal set; }

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001500 RID: 5376 RVA: 0x000608DA File Offset: 0x0005EADA
			// (set) Token: 0x06001501 RID: 5377 RVA: 0x000608E2 File Offset: 0x0005EAE2
			public ReadOnlyArray<InternedString> aliases { readonly get; internal set; }

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001502 RID: 5378 RVA: 0x000608EB File Offset: 0x0005EAEB
			// (set) Token: 0x06001503 RID: 5379 RVA: 0x000608F3 File Offset: 0x0005EAF3
			public ReadOnlyArray<NamedValue> parameters { readonly get; internal set; }

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001504 RID: 5380 RVA: 0x000608FC File Offset: 0x0005EAFC
			// (set) Token: 0x06001505 RID: 5381 RVA: 0x00060904 File Offset: 0x0005EB04
			public ReadOnlyArray<NameAndParameters> processors { readonly get; internal set; }

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001506 RID: 5382 RVA: 0x0006090D File Offset: 0x0005EB0D
			// (set) Token: 0x06001507 RID: 5383 RVA: 0x00060915 File Offset: 0x0005EB15
			public uint offset { readonly get; internal set; }

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x06001508 RID: 5384 RVA: 0x0006091E File Offset: 0x0005EB1E
			// (set) Token: 0x06001509 RID: 5385 RVA: 0x00060926 File Offset: 0x0005EB26
			public uint bit { readonly get; internal set; }

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x0600150A RID: 5386 RVA: 0x0006092F File Offset: 0x0005EB2F
			// (set) Token: 0x0600150B RID: 5387 RVA: 0x00060937 File Offset: 0x0005EB37
			public uint sizeInBits { readonly get; internal set; }

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x0600150C RID: 5388 RVA: 0x00060940 File Offset: 0x0005EB40
			// (set) Token: 0x0600150D RID: 5389 RVA: 0x00060948 File Offset: 0x0005EB48
			public FourCC format { readonly get; internal set; }

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x0600150E RID: 5390 RVA: 0x00060951 File Offset: 0x0005EB51
			// (set) Token: 0x0600150F RID: 5391 RVA: 0x00060959 File Offset: 0x0005EB59
			private InputControlLayout.ControlItem.Flags flags { readonly get; set; }

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001510 RID: 5392 RVA: 0x00060962 File Offset: 0x0005EB62
			// (set) Token: 0x06001511 RID: 5393 RVA: 0x0006096A File Offset: 0x0005EB6A
			public int arraySize { readonly get; internal set; }

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001512 RID: 5394 RVA: 0x00060973 File Offset: 0x0005EB73
			// (set) Token: 0x06001513 RID: 5395 RVA: 0x0006097B File Offset: 0x0005EB7B
			public PrimitiveValue defaultState { readonly get; internal set; }

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001514 RID: 5396 RVA: 0x00060984 File Offset: 0x0005EB84
			// (set) Token: 0x06001515 RID: 5397 RVA: 0x0006098C File Offset: 0x0005EB8C
			public PrimitiveValue minValue { readonly get; internal set; }

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001516 RID: 5398 RVA: 0x00060995 File Offset: 0x0005EB95
			// (set) Token: 0x06001517 RID: 5399 RVA: 0x0006099D File Offset: 0x0005EB9D
			public PrimitiveValue maxValue { readonly get; internal set; }

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x06001518 RID: 5400 RVA: 0x000609A6 File Offset: 0x0005EBA6
			// (set) Token: 0x06001519 RID: 5401 RVA: 0x000609B3 File Offset: 0x0005EBB3
			public bool isModifyingExistingControl
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.isModifyingExistingControl) == InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.isModifyingExistingControl;
				}
			}

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x0600151A RID: 5402 RVA: 0x000609D6 File Offset: 0x0005EBD6
			// (set) Token: 0x0600151B RID: 5403 RVA: 0x000609E3 File Offset: 0x0005EBE3
			public bool isNoisy
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsNoisy) == InputControlLayout.ControlItem.Flags.IsNoisy;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsNoisy;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsNoisy;
				}
			}

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x0600151C RID: 5404 RVA: 0x00060A06 File Offset: 0x0005EC06
			// (set) Token: 0x0600151D RID: 5405 RVA: 0x00060A13 File Offset: 0x0005EC13
			public bool isSynthetic
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsSynthetic) == InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsSynthetic;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsSynthetic;
				}
			}

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x0600151E RID: 5406 RVA: 0x00060A36 File Offset: 0x0005EC36
			// (set) Token: 0x0600151F RID: 5407 RVA: 0x00060A45 File Offset: 0x0005EC45
			public bool dontReset
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.DontReset) == InputControlLayout.ControlItem.Flags.DontReset;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.DontReset;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.DontReset;
				}
			}

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001520 RID: 5408 RVA: 0x00060A69 File Offset: 0x0005EC69
			// (set) Token: 0x06001521 RID: 5409 RVA: 0x00060A76 File Offset: 0x0005EC76
			public bool isFirstDefinedInThisLayout
			{
				get
				{
					return (this.flags & InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout) > (InputControlLayout.ControlItem.Flags)0;
				}
				internal set
				{
					if (value)
					{
						this.flags |= InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
						return;
					}
					this.flags &= ~InputControlLayout.ControlItem.Flags.IsFirstDefinedInThisLayout;
				}
			}

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001522 RID: 5410 RVA: 0x00060A99 File Offset: 0x0005EC99
			public bool isArray
			{
				get
				{
					return this.arraySize != 0;
				}
			}

			// Token: 0x06001523 RID: 5411 RVA: 0x00060AA4 File Offset: 0x0005ECA4
			public InputControlLayout.ControlItem Merge(InputControlLayout.ControlItem other)
			{
				InputControlLayout.ControlItem controlItem = default(InputControlLayout.ControlItem);
				controlItem.name = this.name;
				controlItem.isModifyingExistingControl = this.isModifyingExistingControl;
				controlItem.displayName = (string.IsNullOrEmpty(this.displayName) ? other.displayName : this.displayName);
				controlItem.shortDisplayName = (string.IsNullOrEmpty(this.shortDisplayName) ? other.shortDisplayName : this.shortDisplayName);
				controlItem.layout = (this.layout.IsEmpty() ? other.layout : this.layout);
				controlItem.variants = (this.variants.IsEmpty() ? other.variants : this.variants);
				controlItem.useStateFrom = this.useStateFrom ?? other.useStateFrom;
				controlItem.arraySize = ((!this.isArray) ? other.arraySize : this.arraySize);
				controlItem.isNoisy = this.isNoisy || other.isNoisy;
				controlItem.dontReset = this.dontReset || other.dontReset;
				controlItem.isSynthetic = this.isSynthetic || other.isSynthetic;
				controlItem.isFirstDefinedInThisLayout = false;
				if (this.offset != 4294967295U)
				{
					controlItem.offset = this.offset;
				}
				else
				{
					controlItem.offset = other.offset;
				}
				if (this.bit != 4294967295U)
				{
					controlItem.bit = this.bit;
				}
				else
				{
					controlItem.bit = other.bit;
				}
				if (this.format != 0)
				{
					controlItem.format = this.format;
				}
				else
				{
					controlItem.format = other.format;
				}
				if (this.sizeInBits != 0U)
				{
					controlItem.sizeInBits = this.sizeInBits;
				}
				else
				{
					controlItem.sizeInBits = other.sizeInBits;
				}
				if (this.aliases.Count > 0)
				{
					controlItem.aliases = this.aliases;
				}
				else
				{
					controlItem.aliases = other.aliases;
				}
				if (this.usages.Count > 0)
				{
					controlItem.usages = this.usages;
				}
				else
				{
					controlItem.usages = other.usages;
				}
				if (this.parameters.Count == 0)
				{
					controlItem.parameters = other.parameters;
				}
				else
				{
					controlItem.parameters = this.parameters;
				}
				if (this.processors.Count == 0)
				{
					controlItem.processors = other.processors;
				}
				else
				{
					controlItem.processors = this.processors;
				}
				if (!string.IsNullOrEmpty(this.displayName))
				{
					controlItem.displayName = this.displayName;
				}
				else
				{
					controlItem.displayName = other.displayName;
				}
				if (!this.defaultState.isEmpty)
				{
					controlItem.defaultState = this.defaultState;
				}
				else
				{
					controlItem.defaultState = other.defaultState;
				}
				if (!this.minValue.isEmpty)
				{
					controlItem.minValue = this.minValue;
				}
				else
				{
					controlItem.minValue = other.minValue;
				}
				if (!this.maxValue.isEmpty)
				{
					controlItem.maxValue = this.maxValue;
				}
				else
				{
					controlItem.maxValue = other.maxValue;
				}
				return controlItem;
			}

			// Token: 0x02000271 RID: 625
			[Flags]
			private enum Flags
			{
				// Token: 0x04000C97 RID: 3223
				isModifyingExistingControl = 1,
				// Token: 0x04000C98 RID: 3224
				IsNoisy = 2,
				// Token: 0x04000C99 RID: 3225
				IsSynthetic = 4,
				// Token: 0x04000C9A RID: 3226
				IsFirstDefinedInThisLayout = 8,
				// Token: 0x04000C9B RID: 3227
				DontReset = 16
			}
		}

		// Token: 0x0200021F RID: 543
		public class Builder
		{
			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001524 RID: 5412 RVA: 0x00060DFB File Offset: 0x0005EFFB
			// (set) Token: 0x06001525 RID: 5413 RVA: 0x00060E03 File Offset: 0x0005F003
			public string name { get; set; }

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001526 RID: 5414 RVA: 0x00060E0C File Offset: 0x0005F00C
			// (set) Token: 0x06001527 RID: 5415 RVA: 0x00060E14 File Offset: 0x0005F014
			public string displayName { get; set; }

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x06001528 RID: 5416 RVA: 0x00060E1D File Offset: 0x0005F01D
			// (set) Token: 0x06001529 RID: 5417 RVA: 0x00060E25 File Offset: 0x0005F025
			public Type type { get; set; }

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x0600152A RID: 5418 RVA: 0x00060E2E File Offset: 0x0005F02E
			// (set) Token: 0x0600152B RID: 5419 RVA: 0x00060E36 File Offset: 0x0005F036
			public FourCC stateFormat { get; set; }

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x0600152C RID: 5420 RVA: 0x00060E3F File Offset: 0x0005F03F
			// (set) Token: 0x0600152D RID: 5421 RVA: 0x00060E47 File Offset: 0x0005F047
			public int stateSizeInBytes { get; set; }

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x0600152E RID: 5422 RVA: 0x00060E50 File Offset: 0x0005F050
			// (set) Token: 0x0600152F RID: 5423 RVA: 0x00060E58 File Offset: 0x0005F058
			public string extendsLayout
			{
				get
				{
					return this.m_ExtendsLayout;
				}
				set
				{
					if (!string.IsNullOrEmpty(value))
					{
						this.m_ExtendsLayout = value;
						return;
					}
					this.m_ExtendsLayout = null;
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001530 RID: 5424 RVA: 0x00060E71 File Offset: 0x0005F071
			// (set) Token: 0x06001531 RID: 5425 RVA: 0x00060E79 File Offset: 0x0005F079
			public bool? updateBeforeRender { get; set; }

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001532 RID: 5426 RVA: 0x00060E82 File Offset: 0x0005F082
			public ReadOnlyArray<InputControlLayout.ControlItem> controls
			{
				get
				{
					return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls, 0, this.m_ControlCount);
				}
			}

			// Token: 0x06001533 RID: 5427 RVA: 0x00060E98 File Offset: 0x0005F098
			public InputControlLayout.Builder.ControlBuilder AddControl(string name)
			{
				if (string.IsNullOrEmpty(name))
				{
					throw new ArgumentException(name);
				}
				int num = ArrayHelpers.AppendWithCapacity<InputControlLayout.ControlItem>(ref this.m_Controls, ref this.m_ControlCount, new InputControlLayout.ControlItem
				{
					name = new InternedString(name),
					isModifyingExistingControl = (name.IndexOf('/') != -1),
					offset = uint.MaxValue,
					bit = uint.MaxValue
				}, 10);
				return new InputControlLayout.Builder.ControlBuilder
				{
					builder = this,
					index = num
				};
			}

			// Token: 0x06001534 RID: 5428 RVA: 0x00060F1C File Offset: 0x0005F11C
			public InputControlLayout.Builder WithName(string name)
			{
				this.name = name;
				return this;
			}

			// Token: 0x06001535 RID: 5429 RVA: 0x00060F26 File Offset: 0x0005F126
			public InputControlLayout.Builder WithDisplayName(string displayName)
			{
				this.displayName = displayName;
				return this;
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x00060F30 File Offset: 0x0005F130
			public InputControlLayout.Builder WithType<T>() where T : InputControl
			{
				this.type = typeof(T);
				return this;
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x00060F43 File Offset: 0x0005F143
			public InputControlLayout.Builder WithFormat(FourCC format)
			{
				this.stateFormat = format;
				return this;
			}

			// Token: 0x06001538 RID: 5432 RVA: 0x00060F4D File Offset: 0x0005F14D
			public InputControlLayout.Builder WithFormat(string format)
			{
				return this.WithFormat(new FourCC(format));
			}

			// Token: 0x06001539 RID: 5433 RVA: 0x00060F5B File Offset: 0x0005F15B
			public InputControlLayout.Builder WithSizeInBytes(int sizeInBytes)
			{
				this.stateSizeInBytes = sizeInBytes;
				return this;
			}

			// Token: 0x0600153A RID: 5434 RVA: 0x00060F65 File Offset: 0x0005F165
			public InputControlLayout.Builder Extend(string baseLayoutName)
			{
				this.extendsLayout = baseLayoutName;
				return this;
			}

			// Token: 0x0600153B RID: 5435 RVA: 0x00060F70 File Offset: 0x0005F170
			public InputControlLayout Build()
			{
				InputControlLayout.ControlItem[] array = null;
				if (this.m_ControlCount > 0)
				{
					array = new InputControlLayout.ControlItem[this.m_ControlCount];
					Array.Copy(this.m_Controls, array, this.m_ControlCount);
				}
				return new InputControlLayout(new InternedString(this.name), (this.type == null && string.IsNullOrEmpty(this.extendsLayout)) ? typeof(InputDevice) : this.type)
				{
					m_DisplayName = this.displayName,
					m_StateFormat = this.stateFormat,
					m_StateSizeInBytes = this.stateSizeInBytes,
					m_BaseLayouts = ((!string.IsNullOrEmpty(this.extendsLayout)) ? new InlinedArray<InternedString>(new InternedString(this.extendsLayout)) : default(InlinedArray<InternedString>)),
					m_Controls = array,
					m_UpdateBeforeRender = this.updateBeforeRender
				};
			}

			// Token: 0x04000B79 RID: 2937
			private string m_ExtendsLayout;

			// Token: 0x04000B7B RID: 2939
			private int m_ControlCount;

			// Token: 0x04000B7C RID: 2940
			private InputControlLayout.ControlItem[] m_Controls;

			// Token: 0x02000272 RID: 626
			public struct ControlBuilder
			{
				// Token: 0x0600160E RID: 5646 RVA: 0x0006427C File Offset: 0x0006247C
				public InputControlLayout.Builder.ControlBuilder WithDisplayName(string displayName)
				{
					this.builder.m_Controls[this.index].displayName = displayName;
					return this;
				}

				// Token: 0x0600160F RID: 5647 RVA: 0x000642A0 File Offset: 0x000624A0
				public InputControlLayout.Builder.ControlBuilder WithLayout(string layout)
				{
					if (string.IsNullOrEmpty(layout))
					{
						throw new ArgumentException("Layout name cannot be null or empty", "layout");
					}
					this.builder.m_Controls[this.index].layout = new InternedString(layout);
					return this;
				}

				// Token: 0x06001610 RID: 5648 RVA: 0x000642EC File Offset: 0x000624EC
				public InputControlLayout.Builder.ControlBuilder WithFormat(FourCC format)
				{
					this.builder.m_Controls[this.index].format = format;
					return this;
				}

				// Token: 0x06001611 RID: 5649 RVA: 0x00064310 File Offset: 0x00062510
				public InputControlLayout.Builder.ControlBuilder WithFormat(string format)
				{
					return this.WithFormat(new FourCC(format));
				}

				// Token: 0x06001612 RID: 5650 RVA: 0x0006431E File Offset: 0x0006251E
				public InputControlLayout.Builder.ControlBuilder WithByteOffset(uint offset)
				{
					this.builder.m_Controls[this.index].offset = offset;
					return this;
				}

				// Token: 0x06001613 RID: 5651 RVA: 0x00064342 File Offset: 0x00062542
				public InputControlLayout.Builder.ControlBuilder WithBitOffset(uint bit)
				{
					this.builder.m_Controls[this.index].bit = bit;
					return this;
				}

				// Token: 0x06001614 RID: 5652 RVA: 0x00064366 File Offset: 0x00062566
				public InputControlLayout.Builder.ControlBuilder IsSynthetic(bool value)
				{
					this.builder.m_Controls[this.index].isSynthetic = value;
					return this;
				}

				// Token: 0x06001615 RID: 5653 RVA: 0x0006438A File Offset: 0x0006258A
				public InputControlLayout.Builder.ControlBuilder IsNoisy(bool value)
				{
					this.builder.m_Controls[this.index].isNoisy = value;
					return this;
				}

				// Token: 0x06001616 RID: 5654 RVA: 0x000643AE File Offset: 0x000625AE
				public InputControlLayout.Builder.ControlBuilder DontReset(bool value)
				{
					this.builder.m_Controls[this.index].dontReset = value;
					return this;
				}

				// Token: 0x06001617 RID: 5655 RVA: 0x000643D2 File Offset: 0x000625D2
				public InputControlLayout.Builder.ControlBuilder WithSizeInBits(uint sizeInBits)
				{
					this.builder.m_Controls[this.index].sizeInBits = sizeInBits;
					return this;
				}

				// Token: 0x06001618 RID: 5656 RVA: 0x000643F8 File Offset: 0x000625F8
				public InputControlLayout.Builder.ControlBuilder WithRange(float minValue, float maxValue)
				{
					this.builder.m_Controls[this.index].minValue = minValue;
					this.builder.m_Controls[this.index].maxValue = maxValue;
					return this;
				}

				// Token: 0x06001619 RID: 5657 RVA: 0x00064450 File Offset: 0x00062650
				public InputControlLayout.Builder.ControlBuilder WithUsages(params InternedString[] usages)
				{
					if (usages == null || usages.Length == 0)
					{
						return this;
					}
					for (int i = 0; i < usages.Length; i++)
					{
						if (usages[i].IsEmpty())
						{
							throw new ArgumentException(string.Format("Empty usage entry at index {0} for control '{1}' in layout '{2}'", i, this.builder.m_Controls[this.index].name, this.builder.name), "usages");
						}
					}
					this.builder.m_Controls[this.index].usages = new ReadOnlyArray<InternedString>(usages);
					return this;
				}

				// Token: 0x0600161A RID: 5658 RVA: 0x000644F4 File Offset: 0x000626F4
				public InputControlLayout.Builder.ControlBuilder WithUsages(IEnumerable<string> usages)
				{
					InternedString[] array = usages.Select((string x) => new InternedString(x)).ToArray<InternedString>();
					return this.WithUsages(array);
				}

				// Token: 0x0600161B RID: 5659 RVA: 0x00064533 File Offset: 0x00062733
				public InputControlLayout.Builder.ControlBuilder WithUsages(params string[] usages)
				{
					return this.WithUsages(usages);
				}

				// Token: 0x0600161C RID: 5660 RVA: 0x0006453C File Offset: 0x0006273C
				public InputControlLayout.Builder.ControlBuilder WithParameters(string parameters)
				{
					if (string.IsNullOrEmpty(parameters))
					{
						return this;
					}
					NamedValue[] array = NamedValue.ParseMultiple(parameters);
					this.builder.m_Controls[this.index].parameters = new ReadOnlyArray<NamedValue>(array);
					return this;
				}

				// Token: 0x0600161D RID: 5661 RVA: 0x00064588 File Offset: 0x00062788
				public InputControlLayout.Builder.ControlBuilder WithProcessors(string processors)
				{
					if (string.IsNullOrEmpty(processors))
					{
						return this;
					}
					NameAndParameters[] array = NameAndParameters.ParseMultiple(processors).ToArray<NameAndParameters>();
					this.builder.m_Controls[this.index].processors = new ReadOnlyArray<NameAndParameters>(array);
					return this;
				}

				// Token: 0x0600161E RID: 5662 RVA: 0x000645D7 File Offset: 0x000627D7
				public InputControlLayout.Builder.ControlBuilder WithDefaultState(PrimitiveValue value)
				{
					this.builder.m_Controls[this.index].defaultState = value;
					return this;
				}

				// Token: 0x0600161F RID: 5663 RVA: 0x000645FB File Offset: 0x000627FB
				public InputControlLayout.Builder.ControlBuilder UsingStateFrom(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return this;
					}
					this.builder.m_Controls[this.index].useStateFrom = path;
					return this;
				}

				// Token: 0x06001620 RID: 5664 RVA: 0x0006462E File Offset: 0x0006282E
				public InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(int arraySize)
				{
					this.builder.m_Controls[this.index].arraySize = arraySize;
					return this;
				}

				// Token: 0x04000C9C RID: 3228
				internal InputControlLayout.Builder builder;

				// Token: 0x04000C9D RID: 3229
				internal int index;
			}
		}

		// Token: 0x02000220 RID: 544
		[Flags]
		private enum Flags
		{
			// Token: 0x04000B7E RID: 2942
			IsGenericTypeOfDevice = 1,
			// Token: 0x04000B7F RID: 2943
			HideInUI = 2,
			// Token: 0x04000B80 RID: 2944
			IsOverride = 4,
			// Token: 0x04000B81 RID: 2945
			CanRunInBackground = 8,
			// Token: 0x04000B82 RID: 2946
			CanRunInBackgroundIsSet = 16,
			// Token: 0x04000B83 RID: 2947
			IsNoisy = 32
		}

		// Token: 0x02000221 RID: 545
		[Serializable]
		internal struct LayoutJsonNameAndDescriptorOnly
		{
			// Token: 0x04000B84 RID: 2948
			public string name;

			// Token: 0x04000B85 RID: 2949
			public string extend;

			// Token: 0x04000B86 RID: 2950
			public string[] extendMultiple;

			// Token: 0x04000B87 RID: 2951
			public InputDeviceMatcher.MatcherJson device;
		}

		// Token: 0x02000222 RID: 546
		[Serializable]
		private struct LayoutJson
		{
			// Token: 0x0600153D RID: 5437 RVA: 0x00061058 File Offset: 0x0005F258
			public InputControlLayout ToLayout()
			{
				Type type = null;
				if (!string.IsNullOrEmpty(this.type))
				{
					type = Type.GetType(this.type, false);
					if (type == null)
					{
						Debug.Log(string.Concat(new string[] { "Cannot find type '", this.type, "' used by layout '", this.name, "'; falling back to using InputDevice" }));
						type = typeof(InputDevice);
					}
					else if (!typeof(InputControl).IsAssignableFrom(type))
					{
						throw new InvalidOperationException(string.Concat(new string[] { "'", this.type, "' used by layout '", this.name, "' is not an InputControl" }));
					}
				}
				else if (string.IsNullOrEmpty(this.extend))
				{
					type = typeof(InputDevice);
				}
				InputControlLayout inputControlLayout = new InputControlLayout(this.name, type);
				inputControlLayout.m_DisplayName = this.displayName;
				inputControlLayout.m_Description = this.description;
				inputControlLayout.isGenericTypeOfDevice = this.isGenericTypeOfDevice;
				inputControlLayout.hideInUI = this.hideInUI;
				inputControlLayout.m_Variants = new InternedString(this.variant);
				inputControlLayout.m_CommonUsages = ArrayHelpers.Select<string, InternedString>(this.commonUsages, (string x) => new InternedString(x));
				InputControlLayout inputControlLayout2 = inputControlLayout;
				if (!string.IsNullOrEmpty(this.format))
				{
					inputControlLayout2.m_StateFormat = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.extend))
				{
					inputControlLayout2.m_BaseLayouts.Append(new InternedString(this.extend));
				}
				if (this.extendMultiple != null)
				{
					foreach (string text in this.extendMultiple)
					{
						inputControlLayout2.m_BaseLayouts.Append(new InternedString(text));
					}
				}
				if (!string.IsNullOrEmpty(this.beforeRender))
				{
					string text2 = this.beforeRender.ToLower();
					if (text2 == "ignore")
					{
						inputControlLayout2.m_UpdateBeforeRender = new bool?(false);
					}
					else
					{
						if (!(text2 == "update"))
						{
							throw new InvalidOperationException("Invalid beforeRender setting '" + this.beforeRender + "' (should be 'ignore' or 'update')");
						}
						inputControlLayout2.m_UpdateBeforeRender = new bool?(true);
					}
				}
				if (!string.IsNullOrEmpty(this.runInBackground))
				{
					string text3 = this.runInBackground.ToLower();
					if (text3 == "enabled")
					{
						inputControlLayout2.canRunInBackground = new bool?(true);
					}
					else
					{
						if (!(text3 == "disabled"))
						{
							throw new InvalidOperationException("Invalid runInBackground setting '" + this.beforeRender + "' (should be 'enabled' or 'disabled')");
						}
						inputControlLayout2.canRunInBackground = new bool?(false);
					}
				}
				if (this.controls != null)
				{
					List<InputControlLayout.ControlItem> list = new List<InputControlLayout.ControlItem>();
					foreach (InputControlLayout.ControlItemJson controlItemJson in this.controls)
					{
						if (string.IsNullOrEmpty(controlItemJson.name))
						{
							throw new InvalidOperationException("Control with no name in layout '" + this.name);
						}
						InputControlLayout.ControlItem controlItem = controlItemJson.ToLayout();
						list.Add(controlItem);
					}
					inputControlLayout2.m_Controls = list.ToArray();
				}
				return inputControlLayout2;
			}

			// Token: 0x0600153E RID: 5438 RVA: 0x00061374 File Offset: 0x0005F574
			public static InputControlLayout.LayoutJson FromLayout(InputControlLayout layout)
			{
				InputControlLayout.LayoutJson layoutJson = default(InputControlLayout.LayoutJson);
				layoutJson.name = layout.m_Name;
				Type type = layout.type;
				layoutJson.type = ((type != null) ? type.AssemblyQualifiedName : null);
				layoutJson.variant = layout.m_Variants;
				layoutJson.displayName = layout.m_DisplayName;
				layoutJson.description = layout.m_Description;
				layoutJson.isGenericTypeOfDevice = layout.isGenericTypeOfDevice;
				layoutJson.hideInUI = layout.hideInUI;
				layoutJson.extend = ((layout.m_BaseLayouts.length == 1) ? layout.m_BaseLayouts[0].ToString() : null);
				string[] array;
				if (layout.m_BaseLayouts.length <= 1)
				{
					array = null;
				}
				else
				{
					array = layout.m_BaseLayouts.ToArray<string>((InternedString x) => x.ToString());
				}
				layoutJson.extendMultiple = array;
				layoutJson.format = layout.stateFormat.ToString();
				layoutJson.commonUsages = ArrayHelpers.Select<InternedString, string>(layout.m_CommonUsages, (InternedString x) => x.ToString());
				layoutJson.controls = InputControlLayout.ControlItemJson.FromControlItems(layout.m_Controls);
				layoutJson.beforeRender = ((layout.m_UpdateBeforeRender != null) ? (layout.m_UpdateBeforeRender.Value ? "Update" : "Ignore") : null);
				return layoutJson;
			}

			// Token: 0x04000B88 RID: 2952
			public string name;

			// Token: 0x04000B89 RID: 2953
			public string extend;

			// Token: 0x04000B8A RID: 2954
			public string[] extendMultiple;

			// Token: 0x04000B8B RID: 2955
			public string format;

			// Token: 0x04000B8C RID: 2956
			public string beforeRender;

			// Token: 0x04000B8D RID: 2957
			public string runInBackground;

			// Token: 0x04000B8E RID: 2958
			public string[] commonUsages;

			// Token: 0x04000B8F RID: 2959
			public string displayName;

			// Token: 0x04000B90 RID: 2960
			public string description;

			// Token: 0x04000B91 RID: 2961
			public string type;

			// Token: 0x04000B92 RID: 2962
			public string variant;

			// Token: 0x04000B93 RID: 2963
			public bool isGenericTypeOfDevice;

			// Token: 0x04000B94 RID: 2964
			public bool hideInUI;

			// Token: 0x04000B95 RID: 2965
			public InputControlLayout.ControlItemJson[] controls;
		}

		// Token: 0x02000223 RID: 547
		[Serializable]
		private class ControlItemJson
		{
			// Token: 0x0600153F RID: 5439 RVA: 0x000614FD File Offset: 0x0005F6FD
			public ControlItemJson()
			{
				this.offset = uint.MaxValue;
				this.bit = uint.MaxValue;
			}

			// Token: 0x06001540 RID: 5440 RVA: 0x00061514 File Offset: 0x0005F714
			public InputControlLayout.ControlItem ToLayout()
			{
				InputControlLayout.ControlItem controlItem = new InputControlLayout.ControlItem
				{
					name = new InternedString(this.name),
					layout = new InternedString(this.layout),
					variants = new InternedString(this.variants),
					displayName = this.displayName,
					shortDisplayName = this.shortDisplayName,
					offset = this.offset,
					useStateFrom = this.useStateFrom,
					bit = this.bit,
					sizeInBits = this.sizeInBits,
					isModifyingExistingControl = (this.name.IndexOf('/') != -1),
					isNoisy = this.noisy,
					dontReset = this.dontReset,
					isSynthetic = this.synthetic,
					isFirstDefinedInThisLayout = true,
					arraySize = this.arraySize
				};
				if (!string.IsNullOrEmpty(this.format))
				{
					controlItem.format = new FourCC(this.format);
				}
				if (!string.IsNullOrEmpty(this.usage) || this.usages != null)
				{
					List<string> list = new List<string>();
					if (!string.IsNullOrEmpty(this.usage))
					{
						list.Add(this.usage);
					}
					if (this.usages != null)
					{
						list.AddRange(this.usages);
					}
					controlItem.usages = new ReadOnlyArray<InternedString>(list.Select((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.alias) || this.aliases != null)
				{
					List<string> list2 = new List<string>();
					if (!string.IsNullOrEmpty(this.alias))
					{
						list2.Add(this.alias);
					}
					if (this.aliases != null)
					{
						list2.AddRange(this.aliases);
					}
					controlItem.aliases = new ReadOnlyArray<InternedString>(list2.Select((string x) => new InternedString(x)).ToArray<InternedString>());
				}
				if (!string.IsNullOrEmpty(this.parameters))
				{
					controlItem.parameters = new ReadOnlyArray<NamedValue>(NamedValue.ParseMultiple(this.parameters));
				}
				if (!string.IsNullOrEmpty(this.processors))
				{
					controlItem.processors = new ReadOnlyArray<NameAndParameters>(NameAndParameters.ParseMultiple(this.processors).ToArray<NameAndParameters>());
				}
				if (this.defaultState != null)
				{
					controlItem.defaultState = PrimitiveValue.FromObject(this.defaultState);
				}
				if (this.minValue != null)
				{
					controlItem.minValue = PrimitiveValue.FromObject(this.minValue);
				}
				if (this.maxValue != null)
				{
					controlItem.maxValue = PrimitiveValue.FromObject(this.maxValue);
				}
				return controlItem;
			}

			// Token: 0x06001541 RID: 5441 RVA: 0x000617BC File Offset: 0x0005F9BC
			public static InputControlLayout.ControlItemJson[] FromControlItems(InputControlLayout.ControlItem[] items)
			{
				if (items == null)
				{
					return null;
				}
				int num = items.Length;
				InputControlLayout.ControlItemJson[] array = new InputControlLayout.ControlItemJson[num];
				for (int i = 0; i < num; i++)
				{
					InputControlLayout.ControlItem controlItem = items[i];
					InputControlLayout.ControlItemJson[] array2 = array;
					int num2 = i;
					InputControlLayout.ControlItemJson controlItemJson = new InputControlLayout.ControlItemJson();
					controlItemJson.name = controlItem.name;
					controlItemJson.layout = controlItem.layout;
					controlItemJson.variants = controlItem.variants;
					controlItemJson.displayName = controlItem.displayName;
					controlItemJson.shortDisplayName = controlItem.shortDisplayName;
					controlItemJson.bit = controlItem.bit;
					controlItemJson.offset = controlItem.offset;
					controlItemJson.sizeInBits = controlItem.sizeInBits;
					controlItemJson.format = controlItem.format.ToString();
					controlItemJson.parameters = string.Join(",", controlItem.parameters.Select((NamedValue x) => x.ToString()).ToArray<string>());
					controlItemJson.processors = string.Join(",", controlItem.processors.Select((NameAndParameters x) => x.ToString()).ToArray<string>());
					controlItemJson.usages = controlItem.usages.Select((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.aliases = controlItem.aliases.Select((InternedString x) => x.ToString()).ToArray<string>();
					controlItemJson.noisy = controlItem.isNoisy;
					controlItemJson.dontReset = controlItem.dontReset;
					controlItemJson.synthetic = controlItem.isSynthetic;
					controlItemJson.arraySize = controlItem.arraySize;
					controlItemJson.defaultState = controlItem.defaultState.ToString();
					controlItemJson.minValue = controlItem.minValue.ToString();
					controlItemJson.maxValue = controlItem.maxValue.ToString();
					array2[num2] = controlItemJson;
				}
				return array;
			}

			// Token: 0x04000B96 RID: 2966
			public string name;

			// Token: 0x04000B97 RID: 2967
			public string layout;

			// Token: 0x04000B98 RID: 2968
			public string variants;

			// Token: 0x04000B99 RID: 2969
			public string usage;

			// Token: 0x04000B9A RID: 2970
			public string alias;

			// Token: 0x04000B9B RID: 2971
			public string useStateFrom;

			// Token: 0x04000B9C RID: 2972
			public uint offset;

			// Token: 0x04000B9D RID: 2973
			public uint bit;

			// Token: 0x04000B9E RID: 2974
			public uint sizeInBits;

			// Token: 0x04000B9F RID: 2975
			public string format;

			// Token: 0x04000BA0 RID: 2976
			public int arraySize;

			// Token: 0x04000BA1 RID: 2977
			public string[] usages;

			// Token: 0x04000BA2 RID: 2978
			public string[] aliases;

			// Token: 0x04000BA3 RID: 2979
			public string parameters;

			// Token: 0x04000BA4 RID: 2980
			public string processors;

			// Token: 0x04000BA5 RID: 2981
			public string displayName;

			// Token: 0x04000BA6 RID: 2982
			public string shortDisplayName;

			// Token: 0x04000BA7 RID: 2983
			public bool noisy;

			// Token: 0x04000BA8 RID: 2984
			public bool dontReset;

			// Token: 0x04000BA9 RID: 2985
			public bool synthetic;

			// Token: 0x04000BAA RID: 2986
			public string defaultState;

			// Token: 0x04000BAB RID: 2987
			public string minValue;

			// Token: 0x04000BAC RID: 2988
			public string maxValue;
		}

		// Token: 0x02000224 RID: 548
		internal struct Collection
		{
			// Token: 0x06001542 RID: 5442 RVA: 0x00061A18 File Offset: 0x0005FC18
			public void Allocate()
			{
				this.layoutTypes = new Dictionary<InternedString, Type>();
				this.layoutStrings = new Dictionary<InternedString, string>();
				this.layoutBuilders = new Dictionary<InternedString, Func<InputControlLayout>>();
				this.baseLayoutTable = new Dictionary<InternedString, InternedString>();
				this.layoutOverrides = new Dictionary<InternedString, InternedString[]>();
				this.layoutOverrideNames = new HashSet<InternedString>();
				this.layoutMatchers = new List<InputControlLayout.Collection.LayoutMatcher>();
				this.precompiledLayouts = new Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout>();
			}

			// Token: 0x06001543 RID: 5443 RVA: 0x00061A80 File Offset: 0x0005FC80
			public InternedString TryFindLayoutForType(Type layoutType)
			{
				foreach (KeyValuePair<InternedString, Type> keyValuePair in this.layoutTypes)
				{
					if (keyValuePair.Value == layoutType)
					{
						return keyValuePair.Key;
					}
				}
				return default(InternedString);
			}

			// Token: 0x06001544 RID: 5444 RVA: 0x00061AF0 File Offset: 0x0005FCF0
			public InternedString TryFindMatchingLayout(InputDeviceDescription deviceDescription)
			{
				float num = 0f;
				InternedString internedString = default(InternedString);
				int count = this.layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					InputDeviceMatcher deviceMatcher = this.layoutMatchers[i].deviceMatcher;
					float num2 = deviceMatcher.MatchPercentage(deviceDescription);
					if (num2 > 0f && !this.layoutBuilders.ContainsKey(this.layoutMatchers[i].layoutName))
					{
						num2 += 1f;
					}
					if (num2 > num)
					{
						num = num2;
						internedString = this.layoutMatchers[i].layoutName;
					}
				}
				return internedString;
			}

			// Token: 0x06001545 RID: 5445 RVA: 0x00061B8C File Offset: 0x0005FD8C
			public bool HasLayout(InternedString name)
			{
				return this.layoutTypes.ContainsKey(name) || this.layoutStrings.ContainsKey(name) || this.layoutBuilders.ContainsKey(name);
			}

			// Token: 0x06001546 RID: 5446 RVA: 0x00061BB8 File Offset: 0x0005FDB8
			private InputControlLayout TryLoadLayoutInternal(InternedString name)
			{
				string text;
				if (this.layoutStrings.TryGetValue(name, out text))
				{
					return InputControlLayout.FromJson(text);
				}
				Type type;
				if (this.layoutTypes.TryGetValue(name, out type))
				{
					return InputControlLayout.FromType(name, type);
				}
				Func<InputControlLayout> func;
				if (!this.layoutBuilders.TryGetValue(name, out func))
				{
					return null;
				}
				InputControlLayout inputControlLayout = func();
				if (inputControlLayout == null)
				{
					throw new InvalidOperationException(string.Format("Layout builder '{0}' returned null when invoked", name));
				}
				return inputControlLayout;
			}

			// Token: 0x06001547 RID: 5447 RVA: 0x00061C2C File Offset: 0x0005FE2C
			public InputControlLayout TryLoadLayout(InternedString name, Dictionary<InternedString, InputControlLayout> table = null)
			{
				InputControlLayout inputControlLayout;
				if (table != null && table.TryGetValue(name, out inputControlLayout))
				{
					return inputControlLayout;
				}
				inputControlLayout = this.TryLoadLayoutInternal(name);
				if (inputControlLayout != null)
				{
					inputControlLayout.m_Name = name;
					if (this.layoutOverrideNames.Contains(name))
					{
						inputControlLayout.isOverride = true;
					}
					InternedString internedString = default(InternedString);
					if (!inputControlLayout.isOverride && this.baseLayoutTable.TryGetValue(name, out internedString))
					{
						InputControlLayout inputControlLayout2 = this.TryLoadLayout(internedString, table);
						if (inputControlLayout2 == null)
						{
							throw new InputControlLayout.LayoutNotFoundException(string.Format("Cannot find base layout '{0}' of layout '{1}'", internedString, name));
						}
						inputControlLayout.MergeLayout(inputControlLayout2);
						if (inputControlLayout.m_BaseLayouts.length == 0)
						{
							inputControlLayout.m_BaseLayouts.Append(internedString);
						}
					}
					InternedString[] array;
					if (this.layoutOverrides.TryGetValue(name, out array))
					{
						foreach (InternedString internedString2 in array)
						{
							InputControlLayout inputControlLayout3 = this.TryLoadLayout(internedString2, null);
							inputControlLayout3.MergeLayout(inputControlLayout);
							inputControlLayout3.m_BaseLayouts.Clear();
							inputControlLayout3.isOverride = false;
							inputControlLayout3.isGenericTypeOfDevice = inputControlLayout.isGenericTypeOfDevice;
							inputControlLayout3.m_Name = inputControlLayout.name;
							inputControlLayout3.m_BaseLayouts = inputControlLayout.m_BaseLayouts;
							inputControlLayout = inputControlLayout3;
							inputControlLayout.m_AppliedOverrides.Append(internedString2);
						}
					}
					if (table != null)
					{
						table[name] = inputControlLayout;
					}
				}
				return inputControlLayout;
			}

			// Token: 0x06001548 RID: 5448 RVA: 0x00061D6C File Offset: 0x0005FF6C
			public InternedString GetBaseLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				if (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					return internedString;
				}
				return default(InternedString);
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x00061D94 File Offset: 0x0005FF94
			public InternedString GetRootLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				while (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					layoutName = internedString;
				}
				return layoutName;
			}

			// Token: 0x0600154A RID: 5450 RVA: 0x00061DB8 File Offset: 0x0005FFB8
			public bool ComputeDistanceInInheritanceHierarchy(InternedString firstLayout, InternedString secondLayout, out int distance)
			{
				distance = 0;
				int num = 0;
				InternedString internedString = secondLayout;
				while (!internedString.IsEmpty() && internedString != firstLayout)
				{
					internedString = this.GetBaseLayoutName(internedString);
					num++;
				}
				if (internedString == firstLayout)
				{
					distance = num;
					return true;
				}
				int num2 = 0;
				internedString = firstLayout;
				while (!internedString.IsEmpty() && internedString != secondLayout)
				{
					internedString = this.GetBaseLayoutName(internedString);
					num2++;
				}
				if (internedString == secondLayout)
				{
					distance = num2;
					return true;
				}
				return false;
			}

			// Token: 0x0600154B RID: 5451 RVA: 0x00061E30 File Offset: 0x00060030
			public InternedString FindLayoutThatIntroducesControl(InputControl control, InputControlLayout.Cache cache)
			{
				InputControl inputControl = control;
				while (inputControl.parent != control.device)
				{
					inputControl = inputControl.parent;
				}
				InternedString internedString = control.device.m_Layout;
				InternedString internedString2 = internedString;
				while (this.baseLayoutTable.TryGetValue(internedString2, out internedString2))
				{
					if (cache.FindOrLoadLayout(internedString2, true).FindControl(inputControl.m_Name) != null)
					{
						internedString = internedString2;
					}
				}
				return internedString;
			}

			// Token: 0x0600154C RID: 5452 RVA: 0x00061E9C File Offset: 0x0006009C
			public Type GetControlTypeForLayout(InternedString layoutName)
			{
				while (this.layoutStrings.ContainsKey(layoutName))
				{
					InternedString internedString;
					if (!this.baseLayoutTable.TryGetValue(layoutName, out internedString))
					{
						return typeof(InputDevice);
					}
					layoutName = internedString;
				}
				Type type;
				this.layoutTypes.TryGetValue(layoutName, out type);
				return type;
			}

			// Token: 0x0600154D RID: 5453 RVA: 0x00061EEC File Offset: 0x000600EC
			public bool ValueTypeIsAssignableFrom(InternedString layoutName, Type valueType)
			{
				Type controlTypeForLayout = this.GetControlTypeForLayout(layoutName);
				if (controlTypeForLayout == null)
				{
					return false;
				}
				Type genericTypeArgumentFromHierarchy = TypeHelpers.GetGenericTypeArgumentFromHierarchy(controlTypeForLayout, typeof(InputControl<>), 0);
				return !(genericTypeArgumentFromHierarchy == null) && valueType.IsAssignableFrom(genericTypeArgumentFromHierarchy);
			}

			// Token: 0x0600154E RID: 5454 RVA: 0x00061F30 File Offset: 0x00060130
			public bool IsGeneratedLayout(InternedString layout)
			{
				return this.layoutBuilders.ContainsKey(layout);
			}

			// Token: 0x0600154F RID: 5455 RVA: 0x00061F3E File Offset: 0x0006013E
			public IEnumerable<InternedString> GetBaseLayouts(InternedString layout, bool includeSelf = true)
			{
				if (includeSelf)
				{
					yield return layout;
				}
				while (this.baseLayoutTable.TryGetValue(layout, out layout))
				{
					yield return layout;
				}
				yield break;
			}

			// Token: 0x06001550 RID: 5456 RVA: 0x00061F64 File Offset: 0x00060164
			public bool IsBasedOn(InternedString parentLayout, InternedString childLayout)
			{
				InternedString internedString = childLayout;
				while (this.baseLayoutTable.TryGetValue(internedString, out internedString))
				{
					if (internedString == parentLayout)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001551 RID: 5457 RVA: 0x00061F94 File Offset: 0x00060194
			public void AddMatcher(InternedString layout, InputDeviceMatcher matcher)
			{
				int count = this.layoutMatchers.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.layoutMatchers[i].deviceMatcher == matcher)
					{
						return;
					}
				}
				this.layoutMatchers.Add(new InputControlLayout.Collection.LayoutMatcher
				{
					layoutName = layout,
					deviceMatcher = matcher
				});
			}

			// Token: 0x04000BAD RID: 2989
			public const float kBaseScoreForNonGeneratedLayouts = 1f;

			// Token: 0x04000BAE RID: 2990
			public Dictionary<InternedString, Type> layoutTypes;

			// Token: 0x04000BAF RID: 2991
			public Dictionary<InternedString, string> layoutStrings;

			// Token: 0x04000BB0 RID: 2992
			public Dictionary<InternedString, Func<InputControlLayout>> layoutBuilders;

			// Token: 0x04000BB1 RID: 2993
			public Dictionary<InternedString, InternedString> baseLayoutTable;

			// Token: 0x04000BB2 RID: 2994
			public Dictionary<InternedString, InternedString[]> layoutOverrides;

			// Token: 0x04000BB3 RID: 2995
			public HashSet<InternedString> layoutOverrideNames;

			// Token: 0x04000BB4 RID: 2996
			public Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts;

			// Token: 0x04000BB5 RID: 2997
			public List<InputControlLayout.Collection.LayoutMatcher> layoutMatchers;

			// Token: 0x02000275 RID: 629
			public struct LayoutMatcher
			{
				// Token: 0x04000CA9 RID: 3241
				public InternedString layoutName;

				// Token: 0x04000CAA RID: 3242
				public InputDeviceMatcher deviceMatcher;
			}

			// Token: 0x02000276 RID: 630
			public struct PrecompiledLayout
			{
				// Token: 0x04000CAB RID: 3243
				public Func<InputDevice> factoryMethod;

				// Token: 0x04000CAC RID: 3244
				public string metadata;
			}
		}

		// Token: 0x02000225 RID: 549
		public class LayoutNotFoundException : Exception
		{
			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001552 RID: 5458 RVA: 0x00061FF7 File Offset: 0x000601F7
			public string layout { get; }

			// Token: 0x06001553 RID: 5459 RVA: 0x00061FFF File Offset: 0x000601FF
			public LayoutNotFoundException()
			{
			}

			// Token: 0x06001554 RID: 5460 RVA: 0x00062007 File Offset: 0x00060207
			public LayoutNotFoundException(string name, string message)
				: base(message)
			{
				this.layout = name;
			}

			// Token: 0x06001555 RID: 5461 RVA: 0x00062017 File Offset: 0x00060217
			public LayoutNotFoundException(string name)
				: base("Cannot find control layout '" + name + "'")
			{
				this.layout = name;
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x00062036 File Offset: 0x00060236
			public LayoutNotFoundException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// Token: 0x06001557 RID: 5463 RVA: 0x00062040 File Offset: 0x00060240
			protected LayoutNotFoundException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		// Token: 0x02000226 RID: 550
		internal struct Cache
		{
			// Token: 0x06001558 RID: 5464 RVA: 0x0006204A File Offset: 0x0006024A
			public void Clear()
			{
				this.table = null;
			}

			// Token: 0x06001559 RID: 5465 RVA: 0x00062054 File Offset: 0x00060254
			public InputControlLayout FindOrLoadLayout(string name, bool throwIfNotFound = true)
			{
				InternedString internedString = new InternedString(name);
				if (this.table == null)
				{
					this.table = new Dictionary<InternedString, InputControlLayout>();
				}
				InputControlLayout inputControlLayout = InputControlLayout.s_Layouts.TryLoadLayout(internedString, this.table);
				if (inputControlLayout != null)
				{
					return inputControlLayout;
				}
				if (throwIfNotFound)
				{
					throw new InputControlLayout.LayoutNotFoundException(name);
				}
				return null;
			}

			// Token: 0x04000BB7 RID: 2999
			public Dictionary<InternedString, InputControlLayout> table;
		}

		// Token: 0x02000227 RID: 551
		internal struct CacheRefInstance : IDisposable
		{
			// Token: 0x0600155A RID: 5466 RVA: 0x0006209E File Offset: 0x0006029E
			public void Dispose()
			{
				if (!this.valid)
				{
					return;
				}
				InputControlLayout.s_CacheInstanceRef--;
				if (InputControlLayout.s_CacheInstanceRef <= 0)
				{
					InputControlLayout.s_CacheInstance = default(InputControlLayout.Cache);
					InputControlLayout.s_CacheInstanceRef = 0;
				}
				this.valid = false;
			}

			// Token: 0x04000BB8 RID: 3000
			public bool valid;
		}
	}
}
