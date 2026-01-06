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
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00047C30 File Offset: 0x00045E30
		public static InternedString DefaultVariant
		{
			get
			{
				return InputControlLayout.s_DefaultVariant;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00047C37 File Offset: 0x00045E37
		public InternedString name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00047C3F File Offset: 0x00045E3F
		public string displayName
		{
			get
			{
				return this.m_DisplayName ?? this.m_Name;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00047C56 File Offset: 0x00045E56
		public Type type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00047C5E File Offset: 0x00045E5E
		public InternedString variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00047C66 File Offset: 0x00045E66
		public FourCC stateFormat
		{
			get
			{
				return this.m_StateFormat;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00047C6E File Offset: 0x00045E6E
		public int stateSizeInBytes
		{
			get
			{
				return this.m_StateSizeInBytes;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00047C76 File Offset: 0x00045E76
		public IEnumerable<InternedString> baseLayouts
		{
			get
			{
				return this.m_BaseLayouts;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00047C83 File Offset: 0x00045E83
		public IEnumerable<InternedString> appliedOverrides
		{
			get
			{
				return this.m_AppliedOverrides;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00047C90 File Offset: 0x00045E90
		public ReadOnlyArray<InternedString> commonUsages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_CommonUsages);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00047C9D File Offset: 0x00045E9D
		public ReadOnlyArray<InputControlLayout.ControlItem> controls
		{
			get
			{
				return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00047CAA File Offset: 0x00045EAA
		public bool updateBeforeRender
		{
			get
			{
				return this.m_UpdateBeforeRender.GetValueOrDefault();
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00047CB7 File Offset: 0x00045EB7
		public bool isDeviceLayout
		{
			get
			{
				return typeof(InputDevice).IsAssignableFrom(this.m_Type);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00047CCE File Offset: 0x00045ECE
		public bool isControlLayout
		{
			get
			{
				return !this.isDeviceLayout;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00047CD9 File Offset: 0x00045ED9
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00047CE6 File Offset: 0x00045EE6
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

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00047D09 File Offset: 0x00045F09
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00047D16 File Offset: 0x00045F16
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

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00047D39 File Offset: 0x00045F39
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00047D46 File Offset: 0x00045F46
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

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00047D69 File Offset: 0x00045F69
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00047D77 File Offset: 0x00045F77
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

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00047D9C File Offset: 0x00045F9C
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00047DD0 File Offset: 0x00045FD0
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

		// Token: 0x1700045F RID: 1119
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

		// Token: 0x06000F0F RID: 3855 RVA: 0x00047EA8 File Offset: 0x000460A8
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

		// Token: 0x06000F10 RID: 3856 RVA: 0x00047F28 File Offset: 0x00046128
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

		// Token: 0x06000F11 RID: 3857 RVA: 0x0004804C File Offset: 0x0004624C
		public Type GetValueType()
		{
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(this.type, typeof(InputControl<>), 0);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00048064 File Offset: 0x00046264
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

		// Token: 0x06000F13 RID: 3859 RVA: 0x00048205 File Offset: 0x00046405
		public string ToJson()
		{
			return JsonUtility.ToJson(InputControlLayout.LayoutJson.FromLayout(this), true);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00048218 File Offset: 0x00046418
		public static InputControlLayout FromJson(string json)
		{
			return JsonUtility.FromJson<InputControlLayout.LayoutJson>(json).ToLayout();
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00048233 File Offset: 0x00046433
		private InputControlLayout(string name, Type type)
		{
			this.m_Name = new InternedString(name);
			this.m_Type = type;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0004824E File Offset: 0x0004644E
		private static void AddControlItems(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			InputControlLayout.AddControlItemsFromFields(type, controlLayouts, layoutName);
			InputControlLayout.AddControlItemsFromProperties(type, controlLayouts, layoutName);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00048260 File Offset: 0x00046460
		private static void AddControlItemsFromFields(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(fields, controlLayouts, layoutName);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00048280 File Offset: 0x00046480
		private static void AddControlItemsFromProperties(Type type, List<InputControlLayout.ControlItem> controlLayouts, string layoutName)
		{
			MemberInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
			InputControlLayout.AddControlItemsFromMembers(properties, controlLayouts, layoutName);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000482A0 File Offset: 0x000464A0
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

		// Token: 0x06000F1A RID: 3866 RVA: 0x000483E0 File Offset: 0x000465E0
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

		// Token: 0x06000F1B RID: 3867 RVA: 0x00048428 File Offset: 0x00046628
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

		// Token: 0x06000F1C RID: 3868 RVA: 0x000487D0 File Offset: 0x000469D0
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

		// Token: 0x06000F1D RID: 3869 RVA: 0x00048860 File Offset: 0x00046A60
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

		// Token: 0x06000F1E RID: 3870 RVA: 0x00048CE0 File Offset: 0x00046EE0
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

		// Token: 0x06000F1F RID: 3871 RVA: 0x00048DF5 File Offset: 0x00046FF5
		internal static bool VariantsMatch(InternedString expected, InternedString actual)
		{
			return InputControlLayout.VariantsMatch(expected.ToLower(), actual.ToLower());
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00048E0A File Offset: 0x0004700A
		internal static bool VariantsMatch(string expected, string actual)
		{
			return (actual != null && StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(InputControlLayout.DefaultVariant, actual, ";"[0])) || expected == null || actual == null || StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(expected, actual, ";"[0]);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00048E4C File Offset: 0x0004704C
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

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00048ED7 File Offset: 0x000470D7
		internal static ref InputControlLayout.Cache cache
		{
			get
			{
				return ref InputControlLayout.s_CacheInstance;
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00048EE0 File Offset: 0x000470E0
		internal static InputControlLayout.CacheRefInstance CacheRef()
		{
			InputControlLayout.s_CacheInstanceRef++;
			return new InputControlLayout.CacheRefInstance
			{
				valid = true
			};
		}

		// Token: 0x04000627 RID: 1575
		private static InternedString s_DefaultVariant = new InternedString("Default");

		// Token: 0x04000628 RID: 1576
		public const string VariantSeparator = ";";

		// Token: 0x04000629 RID: 1577
		private InternedString m_Name;

		// Token: 0x0400062A RID: 1578
		private Type m_Type;

		// Token: 0x0400062B RID: 1579
		private InternedString m_Variants;

		// Token: 0x0400062C RID: 1580
		private FourCC m_StateFormat;

		// Token: 0x0400062D RID: 1581
		internal int m_StateSizeInBytes;

		// Token: 0x0400062E RID: 1582
		internal bool? m_UpdateBeforeRender;

		// Token: 0x0400062F RID: 1583
		internal InlinedArray<InternedString> m_BaseLayouts;

		// Token: 0x04000630 RID: 1584
		private InlinedArray<InternedString> m_AppliedOverrides;

		// Token: 0x04000631 RID: 1585
		private InternedString[] m_CommonUsages;

		// Token: 0x04000632 RID: 1586
		internal InputControlLayout.ControlItem[] m_Controls;

		// Token: 0x04000633 RID: 1587
		internal string m_DisplayName;

		// Token: 0x04000634 RID: 1588
		private string m_Description;

		// Token: 0x04000635 RID: 1589
		private InputControlLayout.Flags m_Flags;

		// Token: 0x04000636 RID: 1590
		internal static InputControlLayout.Collection s_Layouts;

		// Token: 0x04000637 RID: 1591
		internal static InputControlLayout.Cache s_CacheInstance;

		// Token: 0x04000638 RID: 1592
		internal static int s_CacheInstanceRef;

		// Token: 0x0200021E RID: 542
		public struct ControlItem
		{
			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00060A77 File Offset: 0x0005EC77
			// (set) Token: 0x060014FA RID: 5370 RVA: 0x00060A7F File Offset: 0x0005EC7F
			public InternedString name { readonly get; internal set; }

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x060014FB RID: 5371 RVA: 0x00060A88 File Offset: 0x0005EC88
			// (set) Token: 0x060014FC RID: 5372 RVA: 0x00060A90 File Offset: 0x0005EC90
			public InternedString layout { readonly get; internal set; }

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x060014FD RID: 5373 RVA: 0x00060A99 File Offset: 0x0005EC99
			// (set) Token: 0x060014FE RID: 5374 RVA: 0x00060AA1 File Offset: 0x0005ECA1
			public InternedString variants { readonly get; internal set; }

			// Token: 0x170005AD RID: 1453
			// (get) Token: 0x060014FF RID: 5375 RVA: 0x00060AAA File Offset: 0x0005ECAA
			// (set) Token: 0x06001500 RID: 5376 RVA: 0x00060AB2 File Offset: 0x0005ECB2
			public string useStateFrom { readonly get; internal set; }

			// Token: 0x170005AE RID: 1454
			// (get) Token: 0x06001501 RID: 5377 RVA: 0x00060ABB File Offset: 0x0005ECBB
			// (set) Token: 0x06001502 RID: 5378 RVA: 0x00060AC3 File Offset: 0x0005ECC3
			public string displayName { readonly get; internal set; }

			// Token: 0x170005AF RID: 1455
			// (get) Token: 0x06001503 RID: 5379 RVA: 0x00060ACC File Offset: 0x0005ECCC
			// (set) Token: 0x06001504 RID: 5380 RVA: 0x00060AD4 File Offset: 0x0005ECD4
			public string shortDisplayName { readonly get; internal set; }

			// Token: 0x170005B0 RID: 1456
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x00060ADD File Offset: 0x0005ECDD
			// (set) Token: 0x06001506 RID: 5382 RVA: 0x00060AE5 File Offset: 0x0005ECE5
			public ReadOnlyArray<InternedString> usages { readonly get; internal set; }

			// Token: 0x170005B1 RID: 1457
			// (get) Token: 0x06001507 RID: 5383 RVA: 0x00060AEE File Offset: 0x0005ECEE
			// (set) Token: 0x06001508 RID: 5384 RVA: 0x00060AF6 File Offset: 0x0005ECF6
			public ReadOnlyArray<InternedString> aliases { readonly get; internal set; }

			// Token: 0x170005B2 RID: 1458
			// (get) Token: 0x06001509 RID: 5385 RVA: 0x00060AFF File Offset: 0x0005ECFF
			// (set) Token: 0x0600150A RID: 5386 RVA: 0x00060B07 File Offset: 0x0005ED07
			public ReadOnlyArray<NamedValue> parameters { readonly get; internal set; }

			// Token: 0x170005B3 RID: 1459
			// (get) Token: 0x0600150B RID: 5387 RVA: 0x00060B10 File Offset: 0x0005ED10
			// (set) Token: 0x0600150C RID: 5388 RVA: 0x00060B18 File Offset: 0x0005ED18
			public ReadOnlyArray<NameAndParameters> processors { readonly get; internal set; }

			// Token: 0x170005B4 RID: 1460
			// (get) Token: 0x0600150D RID: 5389 RVA: 0x00060B21 File Offset: 0x0005ED21
			// (set) Token: 0x0600150E RID: 5390 RVA: 0x00060B29 File Offset: 0x0005ED29
			public uint offset { readonly get; internal set; }

			// Token: 0x170005B5 RID: 1461
			// (get) Token: 0x0600150F RID: 5391 RVA: 0x00060B32 File Offset: 0x0005ED32
			// (set) Token: 0x06001510 RID: 5392 RVA: 0x00060B3A File Offset: 0x0005ED3A
			public uint bit { readonly get; internal set; }

			// Token: 0x170005B6 RID: 1462
			// (get) Token: 0x06001511 RID: 5393 RVA: 0x00060B43 File Offset: 0x0005ED43
			// (set) Token: 0x06001512 RID: 5394 RVA: 0x00060B4B File Offset: 0x0005ED4B
			public uint sizeInBits { readonly get; internal set; }

			// Token: 0x170005B7 RID: 1463
			// (get) Token: 0x06001513 RID: 5395 RVA: 0x00060B54 File Offset: 0x0005ED54
			// (set) Token: 0x06001514 RID: 5396 RVA: 0x00060B5C File Offset: 0x0005ED5C
			public FourCC format { readonly get; internal set; }

			// Token: 0x170005B8 RID: 1464
			// (get) Token: 0x06001515 RID: 5397 RVA: 0x00060B65 File Offset: 0x0005ED65
			// (set) Token: 0x06001516 RID: 5398 RVA: 0x00060B6D File Offset: 0x0005ED6D
			private InputControlLayout.ControlItem.Flags flags { readonly get; set; }

			// Token: 0x170005B9 RID: 1465
			// (get) Token: 0x06001517 RID: 5399 RVA: 0x00060B76 File Offset: 0x0005ED76
			// (set) Token: 0x06001518 RID: 5400 RVA: 0x00060B7E File Offset: 0x0005ED7E
			public int arraySize { readonly get; internal set; }

			// Token: 0x170005BA RID: 1466
			// (get) Token: 0x06001519 RID: 5401 RVA: 0x00060B87 File Offset: 0x0005ED87
			// (set) Token: 0x0600151A RID: 5402 RVA: 0x00060B8F File Offset: 0x0005ED8F
			public PrimitiveValue defaultState { readonly get; internal set; }

			// Token: 0x170005BB RID: 1467
			// (get) Token: 0x0600151B RID: 5403 RVA: 0x00060B98 File Offset: 0x0005ED98
			// (set) Token: 0x0600151C RID: 5404 RVA: 0x00060BA0 File Offset: 0x0005EDA0
			public PrimitiveValue minValue { readonly get; internal set; }

			// Token: 0x170005BC RID: 1468
			// (get) Token: 0x0600151D RID: 5405 RVA: 0x00060BA9 File Offset: 0x0005EDA9
			// (set) Token: 0x0600151E RID: 5406 RVA: 0x00060BB1 File Offset: 0x0005EDB1
			public PrimitiveValue maxValue { readonly get; internal set; }

			// Token: 0x170005BD RID: 1469
			// (get) Token: 0x0600151F RID: 5407 RVA: 0x00060BBA File Offset: 0x0005EDBA
			// (set) Token: 0x06001520 RID: 5408 RVA: 0x00060BC7 File Offset: 0x0005EDC7
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

			// Token: 0x170005BE RID: 1470
			// (get) Token: 0x06001521 RID: 5409 RVA: 0x00060BEA File Offset: 0x0005EDEA
			// (set) Token: 0x06001522 RID: 5410 RVA: 0x00060BF7 File Offset: 0x0005EDF7
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

			// Token: 0x170005BF RID: 1471
			// (get) Token: 0x06001523 RID: 5411 RVA: 0x00060C1A File Offset: 0x0005EE1A
			// (set) Token: 0x06001524 RID: 5412 RVA: 0x00060C27 File Offset: 0x0005EE27
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

			// Token: 0x170005C0 RID: 1472
			// (get) Token: 0x06001525 RID: 5413 RVA: 0x00060C4A File Offset: 0x0005EE4A
			// (set) Token: 0x06001526 RID: 5414 RVA: 0x00060C59 File Offset: 0x0005EE59
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

			// Token: 0x170005C1 RID: 1473
			// (get) Token: 0x06001527 RID: 5415 RVA: 0x00060C7D File Offset: 0x0005EE7D
			// (set) Token: 0x06001528 RID: 5416 RVA: 0x00060C8A File Offset: 0x0005EE8A
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

			// Token: 0x170005C2 RID: 1474
			// (get) Token: 0x06001529 RID: 5417 RVA: 0x00060CAD File Offset: 0x0005EEAD
			public bool isArray
			{
				get
				{
					return this.arraySize != 0;
				}
			}

			// Token: 0x0600152A RID: 5418 RVA: 0x00060CB8 File Offset: 0x0005EEB8
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
				// Token: 0x04000C98 RID: 3224
				isModifyingExistingControl = 1,
				// Token: 0x04000C99 RID: 3225
				IsNoisy = 2,
				// Token: 0x04000C9A RID: 3226
				IsSynthetic = 4,
				// Token: 0x04000C9B RID: 3227
				IsFirstDefinedInThisLayout = 8,
				// Token: 0x04000C9C RID: 3228
				DontReset = 16
			}
		}

		// Token: 0x0200021F RID: 543
		public class Builder
		{
			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x0600152B RID: 5419 RVA: 0x0006100F File Offset: 0x0005F20F
			// (set) Token: 0x0600152C RID: 5420 RVA: 0x00061017 File Offset: 0x0005F217
			public string name { get; set; }

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x0600152D RID: 5421 RVA: 0x00061020 File Offset: 0x0005F220
			// (set) Token: 0x0600152E RID: 5422 RVA: 0x00061028 File Offset: 0x0005F228
			public string displayName { get; set; }

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x0600152F RID: 5423 RVA: 0x00061031 File Offset: 0x0005F231
			// (set) Token: 0x06001530 RID: 5424 RVA: 0x00061039 File Offset: 0x0005F239
			public Type type { get; set; }

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001531 RID: 5425 RVA: 0x00061042 File Offset: 0x0005F242
			// (set) Token: 0x06001532 RID: 5426 RVA: 0x0006104A File Offset: 0x0005F24A
			public FourCC stateFormat { get; set; }

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001533 RID: 5427 RVA: 0x00061053 File Offset: 0x0005F253
			// (set) Token: 0x06001534 RID: 5428 RVA: 0x0006105B File Offset: 0x0005F25B
			public int stateSizeInBytes { get; set; }

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001535 RID: 5429 RVA: 0x00061064 File Offset: 0x0005F264
			// (set) Token: 0x06001536 RID: 5430 RVA: 0x0006106C File Offset: 0x0005F26C
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

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001537 RID: 5431 RVA: 0x00061085 File Offset: 0x0005F285
			// (set) Token: 0x06001538 RID: 5432 RVA: 0x0006108D File Offset: 0x0005F28D
			public bool? updateBeforeRender { get; set; }

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001539 RID: 5433 RVA: 0x00061096 File Offset: 0x0005F296
			public ReadOnlyArray<InputControlLayout.ControlItem> controls
			{
				get
				{
					return new ReadOnlyArray<InputControlLayout.ControlItem>(this.m_Controls, 0, this.m_ControlCount);
				}
			}

			// Token: 0x0600153A RID: 5434 RVA: 0x000610AC File Offset: 0x0005F2AC
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

			// Token: 0x0600153B RID: 5435 RVA: 0x00061130 File Offset: 0x0005F330
			public InputControlLayout.Builder WithName(string name)
			{
				this.name = name;
				return this;
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0006113A File Offset: 0x0005F33A
			public InputControlLayout.Builder WithDisplayName(string displayName)
			{
				this.displayName = displayName;
				return this;
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x00061144 File Offset: 0x0005F344
			public InputControlLayout.Builder WithType<T>() where T : InputControl
			{
				this.type = typeof(T);
				return this;
			}

			// Token: 0x0600153E RID: 5438 RVA: 0x00061157 File Offset: 0x0005F357
			public InputControlLayout.Builder WithFormat(FourCC format)
			{
				this.stateFormat = format;
				return this;
			}

			// Token: 0x0600153F RID: 5439 RVA: 0x00061161 File Offset: 0x0005F361
			public InputControlLayout.Builder WithFormat(string format)
			{
				return this.WithFormat(new FourCC(format));
			}

			// Token: 0x06001540 RID: 5440 RVA: 0x0006116F File Offset: 0x0005F36F
			public InputControlLayout.Builder WithSizeInBytes(int sizeInBytes)
			{
				this.stateSizeInBytes = sizeInBytes;
				return this;
			}

			// Token: 0x06001541 RID: 5441 RVA: 0x00061179 File Offset: 0x0005F379
			public InputControlLayout.Builder Extend(string baseLayoutName)
			{
				this.extendsLayout = baseLayoutName;
				return this;
			}

			// Token: 0x06001542 RID: 5442 RVA: 0x00061184 File Offset: 0x0005F384
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

			// Token: 0x04000B7A RID: 2938
			private string m_ExtendsLayout;

			// Token: 0x04000B7C RID: 2940
			private int m_ControlCount;

			// Token: 0x04000B7D RID: 2941
			private InputControlLayout.ControlItem[] m_Controls;

			// Token: 0x02000272 RID: 626
			public struct ControlBuilder
			{
				// Token: 0x06001615 RID: 5653 RVA: 0x00064490 File Offset: 0x00062690
				public InputControlLayout.Builder.ControlBuilder WithDisplayName(string displayName)
				{
					this.builder.m_Controls[this.index].displayName = displayName;
					return this;
				}

				// Token: 0x06001616 RID: 5654 RVA: 0x000644B4 File Offset: 0x000626B4
				public InputControlLayout.Builder.ControlBuilder WithLayout(string layout)
				{
					if (string.IsNullOrEmpty(layout))
					{
						throw new ArgumentException("Layout name cannot be null or empty", "layout");
					}
					this.builder.m_Controls[this.index].layout = new InternedString(layout);
					return this;
				}

				// Token: 0x06001617 RID: 5655 RVA: 0x00064500 File Offset: 0x00062700
				public InputControlLayout.Builder.ControlBuilder WithFormat(FourCC format)
				{
					this.builder.m_Controls[this.index].format = format;
					return this;
				}

				// Token: 0x06001618 RID: 5656 RVA: 0x00064524 File Offset: 0x00062724
				public InputControlLayout.Builder.ControlBuilder WithFormat(string format)
				{
					return this.WithFormat(new FourCC(format));
				}

				// Token: 0x06001619 RID: 5657 RVA: 0x00064532 File Offset: 0x00062732
				public InputControlLayout.Builder.ControlBuilder WithByteOffset(uint offset)
				{
					this.builder.m_Controls[this.index].offset = offset;
					return this;
				}

				// Token: 0x0600161A RID: 5658 RVA: 0x00064556 File Offset: 0x00062756
				public InputControlLayout.Builder.ControlBuilder WithBitOffset(uint bit)
				{
					this.builder.m_Controls[this.index].bit = bit;
					return this;
				}

				// Token: 0x0600161B RID: 5659 RVA: 0x0006457A File Offset: 0x0006277A
				public InputControlLayout.Builder.ControlBuilder IsSynthetic(bool value)
				{
					this.builder.m_Controls[this.index].isSynthetic = value;
					return this;
				}

				// Token: 0x0600161C RID: 5660 RVA: 0x0006459E File Offset: 0x0006279E
				public InputControlLayout.Builder.ControlBuilder IsNoisy(bool value)
				{
					this.builder.m_Controls[this.index].isNoisy = value;
					return this;
				}

				// Token: 0x0600161D RID: 5661 RVA: 0x000645C2 File Offset: 0x000627C2
				public InputControlLayout.Builder.ControlBuilder DontReset(bool value)
				{
					this.builder.m_Controls[this.index].dontReset = value;
					return this;
				}

				// Token: 0x0600161E RID: 5662 RVA: 0x000645E6 File Offset: 0x000627E6
				public InputControlLayout.Builder.ControlBuilder WithSizeInBits(uint sizeInBits)
				{
					this.builder.m_Controls[this.index].sizeInBits = sizeInBits;
					return this;
				}

				// Token: 0x0600161F RID: 5663 RVA: 0x0006460C File Offset: 0x0006280C
				public InputControlLayout.Builder.ControlBuilder WithRange(float minValue, float maxValue)
				{
					this.builder.m_Controls[this.index].minValue = minValue;
					this.builder.m_Controls[this.index].maxValue = maxValue;
					return this;
				}

				// Token: 0x06001620 RID: 5664 RVA: 0x00064664 File Offset: 0x00062864
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

				// Token: 0x06001621 RID: 5665 RVA: 0x00064708 File Offset: 0x00062908
				public InputControlLayout.Builder.ControlBuilder WithUsages(IEnumerable<string> usages)
				{
					InternedString[] array = usages.Select((string x) => new InternedString(x)).ToArray<InternedString>();
					return this.WithUsages(array);
				}

				// Token: 0x06001622 RID: 5666 RVA: 0x00064747 File Offset: 0x00062947
				public InputControlLayout.Builder.ControlBuilder WithUsages(params string[] usages)
				{
					return this.WithUsages(usages);
				}

				// Token: 0x06001623 RID: 5667 RVA: 0x00064750 File Offset: 0x00062950
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

				// Token: 0x06001624 RID: 5668 RVA: 0x0006479C File Offset: 0x0006299C
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

				// Token: 0x06001625 RID: 5669 RVA: 0x000647EB File Offset: 0x000629EB
				public InputControlLayout.Builder.ControlBuilder WithDefaultState(PrimitiveValue value)
				{
					this.builder.m_Controls[this.index].defaultState = value;
					return this;
				}

				// Token: 0x06001626 RID: 5670 RVA: 0x0006480F File Offset: 0x00062A0F
				public InputControlLayout.Builder.ControlBuilder UsingStateFrom(string path)
				{
					if (string.IsNullOrEmpty(path))
					{
						return this;
					}
					this.builder.m_Controls[this.index].useStateFrom = path;
					return this;
				}

				// Token: 0x06001627 RID: 5671 RVA: 0x00064842 File Offset: 0x00062A42
				public InputControlLayout.Builder.ControlBuilder AsArrayOfControlsWithSize(int arraySize)
				{
					this.builder.m_Controls[this.index].arraySize = arraySize;
					return this;
				}

				// Token: 0x04000C9D RID: 3229
				internal InputControlLayout.Builder builder;

				// Token: 0x04000C9E RID: 3230
				internal int index;
			}
		}

		// Token: 0x02000220 RID: 544
		[Flags]
		private enum Flags
		{
			// Token: 0x04000B7F RID: 2943
			IsGenericTypeOfDevice = 1,
			// Token: 0x04000B80 RID: 2944
			HideInUI = 2,
			// Token: 0x04000B81 RID: 2945
			IsOverride = 4,
			// Token: 0x04000B82 RID: 2946
			CanRunInBackground = 8,
			// Token: 0x04000B83 RID: 2947
			CanRunInBackgroundIsSet = 16,
			// Token: 0x04000B84 RID: 2948
			IsNoisy = 32
		}

		// Token: 0x02000221 RID: 545
		[Serializable]
		internal struct LayoutJsonNameAndDescriptorOnly
		{
			// Token: 0x04000B85 RID: 2949
			public string name;

			// Token: 0x04000B86 RID: 2950
			public string extend;

			// Token: 0x04000B87 RID: 2951
			public string[] extendMultiple;

			// Token: 0x04000B88 RID: 2952
			public InputDeviceMatcher.MatcherJson device;
		}

		// Token: 0x02000222 RID: 546
		[Serializable]
		private struct LayoutJson
		{
			// Token: 0x06001544 RID: 5444 RVA: 0x0006126C File Offset: 0x0005F46C
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

			// Token: 0x06001545 RID: 5445 RVA: 0x00061588 File Offset: 0x0005F788
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

			// Token: 0x04000B89 RID: 2953
			public string name;

			// Token: 0x04000B8A RID: 2954
			public string extend;

			// Token: 0x04000B8B RID: 2955
			public string[] extendMultiple;

			// Token: 0x04000B8C RID: 2956
			public string format;

			// Token: 0x04000B8D RID: 2957
			public string beforeRender;

			// Token: 0x04000B8E RID: 2958
			public string runInBackground;

			// Token: 0x04000B8F RID: 2959
			public string[] commonUsages;

			// Token: 0x04000B90 RID: 2960
			public string displayName;

			// Token: 0x04000B91 RID: 2961
			public string description;

			// Token: 0x04000B92 RID: 2962
			public string type;

			// Token: 0x04000B93 RID: 2963
			public string variant;

			// Token: 0x04000B94 RID: 2964
			public bool isGenericTypeOfDevice;

			// Token: 0x04000B95 RID: 2965
			public bool hideInUI;

			// Token: 0x04000B96 RID: 2966
			public InputControlLayout.ControlItemJson[] controls;
		}

		// Token: 0x02000223 RID: 547
		[Serializable]
		private class ControlItemJson
		{
			// Token: 0x06001546 RID: 5446 RVA: 0x00061711 File Offset: 0x0005F911
			public ControlItemJson()
			{
				this.offset = uint.MaxValue;
				this.bit = uint.MaxValue;
			}

			// Token: 0x06001547 RID: 5447 RVA: 0x00061728 File Offset: 0x0005F928
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

			// Token: 0x06001548 RID: 5448 RVA: 0x000619D0 File Offset: 0x0005FBD0
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

			// Token: 0x04000B97 RID: 2967
			public string name;

			// Token: 0x04000B98 RID: 2968
			public string layout;

			// Token: 0x04000B99 RID: 2969
			public string variants;

			// Token: 0x04000B9A RID: 2970
			public string usage;

			// Token: 0x04000B9B RID: 2971
			public string alias;

			// Token: 0x04000B9C RID: 2972
			public string useStateFrom;

			// Token: 0x04000B9D RID: 2973
			public uint offset;

			// Token: 0x04000B9E RID: 2974
			public uint bit;

			// Token: 0x04000B9F RID: 2975
			public uint sizeInBits;

			// Token: 0x04000BA0 RID: 2976
			public string format;

			// Token: 0x04000BA1 RID: 2977
			public int arraySize;

			// Token: 0x04000BA2 RID: 2978
			public string[] usages;

			// Token: 0x04000BA3 RID: 2979
			public string[] aliases;

			// Token: 0x04000BA4 RID: 2980
			public string parameters;

			// Token: 0x04000BA5 RID: 2981
			public string processors;

			// Token: 0x04000BA6 RID: 2982
			public string displayName;

			// Token: 0x04000BA7 RID: 2983
			public string shortDisplayName;

			// Token: 0x04000BA8 RID: 2984
			public bool noisy;

			// Token: 0x04000BA9 RID: 2985
			public bool dontReset;

			// Token: 0x04000BAA RID: 2986
			public bool synthetic;

			// Token: 0x04000BAB RID: 2987
			public string defaultState;

			// Token: 0x04000BAC RID: 2988
			public string minValue;

			// Token: 0x04000BAD RID: 2989
			public string maxValue;
		}

		// Token: 0x02000224 RID: 548
		internal struct Collection
		{
			// Token: 0x06001549 RID: 5449 RVA: 0x00061C2C File Offset: 0x0005FE2C
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

			// Token: 0x0600154A RID: 5450 RVA: 0x00061C94 File Offset: 0x0005FE94
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

			// Token: 0x0600154B RID: 5451 RVA: 0x00061D04 File Offset: 0x0005FF04
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

			// Token: 0x0600154C RID: 5452 RVA: 0x00061DA0 File Offset: 0x0005FFA0
			public bool HasLayout(InternedString name)
			{
				return this.layoutTypes.ContainsKey(name) || this.layoutStrings.ContainsKey(name) || this.layoutBuilders.ContainsKey(name);
			}

			// Token: 0x0600154D RID: 5453 RVA: 0x00061DCC File Offset: 0x0005FFCC
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

			// Token: 0x0600154E RID: 5454 RVA: 0x00061E40 File Offset: 0x00060040
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

			// Token: 0x0600154F RID: 5455 RVA: 0x00061F80 File Offset: 0x00060180
			public InternedString GetBaseLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				if (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					return internedString;
				}
				return default(InternedString);
			}

			// Token: 0x06001550 RID: 5456 RVA: 0x00061FA8 File Offset: 0x000601A8
			public InternedString GetRootLayoutName(InternedString layoutName)
			{
				InternedString internedString;
				while (this.baseLayoutTable.TryGetValue(layoutName, out internedString))
				{
					layoutName = internedString;
				}
				return layoutName;
			}

			// Token: 0x06001551 RID: 5457 RVA: 0x00061FCC File Offset: 0x000601CC
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

			// Token: 0x06001552 RID: 5458 RVA: 0x00062044 File Offset: 0x00060244
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

			// Token: 0x06001553 RID: 5459 RVA: 0x000620B0 File Offset: 0x000602B0
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

			// Token: 0x06001554 RID: 5460 RVA: 0x00062100 File Offset: 0x00060300
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

			// Token: 0x06001555 RID: 5461 RVA: 0x00062144 File Offset: 0x00060344
			public bool IsGeneratedLayout(InternedString layout)
			{
				return this.layoutBuilders.ContainsKey(layout);
			}

			// Token: 0x06001556 RID: 5462 RVA: 0x00062152 File Offset: 0x00060352
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

			// Token: 0x06001557 RID: 5463 RVA: 0x00062178 File Offset: 0x00060378
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

			// Token: 0x06001558 RID: 5464 RVA: 0x000621A8 File Offset: 0x000603A8
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

			// Token: 0x04000BAE RID: 2990
			public const float kBaseScoreForNonGeneratedLayouts = 1f;

			// Token: 0x04000BAF RID: 2991
			public Dictionary<InternedString, Type> layoutTypes;

			// Token: 0x04000BB0 RID: 2992
			public Dictionary<InternedString, string> layoutStrings;

			// Token: 0x04000BB1 RID: 2993
			public Dictionary<InternedString, Func<InputControlLayout>> layoutBuilders;

			// Token: 0x04000BB2 RID: 2994
			public Dictionary<InternedString, InternedString> baseLayoutTable;

			// Token: 0x04000BB3 RID: 2995
			public Dictionary<InternedString, InternedString[]> layoutOverrides;

			// Token: 0x04000BB4 RID: 2996
			public HashSet<InternedString> layoutOverrideNames;

			// Token: 0x04000BB5 RID: 2997
			public Dictionary<InternedString, InputControlLayout.Collection.PrecompiledLayout> precompiledLayouts;

			// Token: 0x04000BB6 RID: 2998
			public List<InputControlLayout.Collection.LayoutMatcher> layoutMatchers;

			// Token: 0x02000275 RID: 629
			public struct LayoutMatcher
			{
				// Token: 0x04000CAA RID: 3242
				public InternedString layoutName;

				// Token: 0x04000CAB RID: 3243
				public InputDeviceMatcher deviceMatcher;
			}

			// Token: 0x02000276 RID: 630
			public struct PrecompiledLayout
			{
				// Token: 0x04000CAC RID: 3244
				public Func<InputDevice> factoryMethod;

				// Token: 0x04000CAD RID: 3245
				public string metadata;
			}
		}

		// Token: 0x02000225 RID: 549
		public class LayoutNotFoundException : Exception
		{
			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x06001559 RID: 5465 RVA: 0x0006220B File Offset: 0x0006040B
			public string layout { get; }

			// Token: 0x0600155A RID: 5466 RVA: 0x00062213 File Offset: 0x00060413
			public LayoutNotFoundException()
			{
			}

			// Token: 0x0600155B RID: 5467 RVA: 0x0006221B File Offset: 0x0006041B
			public LayoutNotFoundException(string name, string message)
				: base(message)
			{
				this.layout = name;
			}

			// Token: 0x0600155C RID: 5468 RVA: 0x0006222B File Offset: 0x0006042B
			public LayoutNotFoundException(string name)
				: base("Cannot find control layout '" + name + "'")
			{
				this.layout = name;
			}

			// Token: 0x0600155D RID: 5469 RVA: 0x0006224A File Offset: 0x0006044A
			public LayoutNotFoundException(string message, Exception innerException)
				: base(message, innerException)
			{
			}

			// Token: 0x0600155E RID: 5470 RVA: 0x00062254 File Offset: 0x00060454
			protected LayoutNotFoundException(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
			}
		}

		// Token: 0x02000226 RID: 550
		internal struct Cache
		{
			// Token: 0x0600155F RID: 5471 RVA: 0x0006225E File Offset: 0x0006045E
			public void Clear()
			{
				this.table = null;
			}

			// Token: 0x06001560 RID: 5472 RVA: 0x00062268 File Offset: 0x00060468
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

			// Token: 0x04000BB8 RID: 3000
			public Dictionary<InternedString, InputControlLayout> table;
		}

		// Token: 0x02000227 RID: 551
		internal struct CacheRefInstance : IDisposable
		{
			// Token: 0x06001561 RID: 5473 RVA: 0x000622B2 File Offset: 0x000604B2
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

			// Token: 0x04000BB9 RID: 3001
			public bool valid;
		}
	}
}
