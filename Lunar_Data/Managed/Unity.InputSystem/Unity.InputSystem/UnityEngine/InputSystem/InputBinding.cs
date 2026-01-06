using System;
using System.Linq;
using System.Text;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public struct InputBinding : IEquatable<InputBinding>
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000BD76 File Offset: 0x00009F76
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000BD7E File Offset: 0x00009F7E
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000BD88 File Offset: 0x00009F88
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000BDB7 File Offset: 0x00009FB7
		public Guid id
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_Id))
				{
					return default(Guid);
				}
				return new Guid(this.m_Id);
			}
			set
			{
				this.m_Id = value.ToString();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000BDCC File Offset: 0x00009FCC
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		public string path
		{
			get
			{
				return this.m_Path;
			}
			set
			{
				this.m_Path = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000BDDD File Offset: 0x00009FDD
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000BDE5 File Offset: 0x00009FE5
		public string overridePath
		{
			get
			{
				return this.m_OverridePath;
			}
			set
			{
				this.m_OverridePath = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000BDEE File Offset: 0x00009FEE
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000BDF6 File Offset: 0x00009FF6
		public string interactions
		{
			get
			{
				return this.m_Interactions;
			}
			set
			{
				this.m_Interactions = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000BDFF File Offset: 0x00009FFF
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000BE07 File Offset: 0x0000A007
		public string overrideInteractions
		{
			get
			{
				return this.m_OverrideInteractions;
			}
			set
			{
				this.m_OverrideInteractions = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000BE10 File Offset: 0x0000A010
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000BE18 File Offset: 0x0000A018
		public string processors
		{
			get
			{
				return this.m_Processors;
			}
			set
			{
				this.m_Processors = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000BE21 File Offset: 0x0000A021
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000BE29 File Offset: 0x0000A029
		public string overrideProcessors
		{
			get
			{
				return this.m_OverrideProcessors;
			}
			set
			{
				this.m_OverrideProcessors = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000BE32 File Offset: 0x0000A032
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000BE3A File Offset: 0x0000A03A
		public string groups
		{
			get
			{
				return this.m_Groups;
			}
			set
			{
				this.m_Groups = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000BE43 File Offset: 0x0000A043
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000BE4B File Offset: 0x0000A04B
		public string action
		{
			get
			{
				return this.m_Action;
			}
			set
			{
				this.m_Action = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000BE54 File Offset: 0x0000A054
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000BE61 File Offset: 0x0000A061
		public bool isComposite
		{
			get
			{
				return (this.m_Flags & InputBinding.Flags.Composite) == InputBinding.Flags.Composite;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputBinding.Flags.Composite;
					return;
				}
				this.m_Flags &= ~InputBinding.Flags.Composite;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000BE84 File Offset: 0x0000A084
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000BE91 File Offset: 0x0000A091
		public bool isPartOfComposite
		{
			get
			{
				return (this.m_Flags & InputBinding.Flags.PartOfComposite) == InputBinding.Flags.PartOfComposite;
			}
			set
			{
				if (value)
				{
					this.m_Flags |= InputBinding.Flags.PartOfComposite;
					return;
				}
				this.m_Flags &= ~InputBinding.Flags.PartOfComposite;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		public bool hasOverrides
		{
			get
			{
				return this.overridePath != null || this.overrideProcessors != null || this.overrideInteractions != null;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public InputBinding(string path, string action = null, string groups = null, string processors = null, string interactions = null, string name = null)
		{
			this.m_Path = path;
			this.m_Action = action;
			this.m_Groups = groups;
			this.m_Processors = processors;
			this.m_Interactions = interactions;
			this.m_Name = name;
			this.m_Id = null;
			this.m_Flags = InputBinding.Flags.None;
			this.m_OverridePath = null;
			this.m_OverrideInteractions = null;
			this.m_OverrideProcessors = null;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BF34 File Offset: 0x0000A134
		public string GetNameOfComposite()
		{
			if (!this.isComposite)
			{
				return null;
			}
			return NameAndParameters.Parse(this.effectivePath).name;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000BF60 File Offset: 0x0000A160
		internal void GenerateId()
		{
			this.m_Id = Guid.NewGuid().ToString();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BF86 File Offset: 0x0000A186
		internal void RemoveOverrides()
		{
			this.m_OverridePath = null;
			this.m_OverrideInteractions = null;
			this.m_OverrideProcessors = null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public static InputBinding MaskByGroup(string group)
		{
			return new InputBinding
			{
				groups = group
			};
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		public static InputBinding MaskByGroups(params string[] groups)
		{
			InputBinding inputBinding = default(InputBinding);
			inputBinding.groups = string.Join(";", groups.Where((string x) => !string.IsNullOrEmpty(x)));
			return inputBinding;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000C00C File Offset: 0x0000A20C
		public string effectivePath
		{
			get
			{
				return this.overridePath ?? this.path;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000C01E File Offset: 0x0000A21E
		public string effectiveInteractions
		{
			get
			{
				return this.overrideInteractions ?? this.interactions;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000C030 File Offset: 0x0000A230
		public string effectiveProcessors
		{
			get
			{
				return this.overrideProcessors ?? this.processors;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000C042 File Offset: 0x0000A242
		internal bool isEmpty
		{
			get
			{
				return string.IsNullOrEmpty(this.effectivePath) && string.IsNullOrEmpty(this.action) && string.IsNullOrEmpty(this.groups);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C06C File Offset: 0x0000A26C
		public bool Equals(InputBinding other)
		{
			return string.Equals(this.effectivePath, other.effectivePath, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.effectiveInteractions, other.effectiveInteractions, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.effectiveProcessors, other.effectiveProcessors, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.groups, other.groups, StringComparison.InvariantCultureIgnoreCase) && string.Equals(this.action, other.action, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputBinding)
			{
				InputBinding inputBinding = (InputBinding)obj;
				return this.Equals(inputBinding);
			}
			return false;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C10E File Offset: 0x0000A30E
		public static bool operator ==(InputBinding left, InputBinding right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C118 File Offset: 0x0000A318
		public static bool operator !=(InputBinding left, InputBinding right)
		{
			return !(left == right);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C124 File Offset: 0x0000A324
		public override int GetHashCode()
		{
			return (((((((((this.effectivePath != null) ? this.effectivePath.GetHashCode() : 0) * 397) ^ ((this.effectiveInteractions != null) ? this.effectiveInteractions.GetHashCode() : 0)) * 397) ^ ((this.effectiveProcessors != null) ? this.effectiveProcessors.GetHashCode() : 0)) * 397) ^ ((this.groups != null) ? this.groups.GetHashCode() : 0)) * 397) ^ ((this.action != null) ? this.action.GetHashCode() : 0);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.action))
			{
				stringBuilder.Append(this.action);
				stringBuilder.Append(':');
			}
			string effectivePath = this.effectivePath;
			if (!string.IsNullOrEmpty(effectivePath))
			{
				stringBuilder.Append(effectivePath);
			}
			if (!string.IsNullOrEmpty(this.groups))
			{
				stringBuilder.Append('[');
				stringBuilder.Append(this.groups);
				stringBuilder.Append(']');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C23C File Offset: 0x0000A43C
		public string ToDisplayString(InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, InputControl control = null)
		{
			string text;
			string text2;
			return this.ToDisplayString(out text, out text2, options, control);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C258 File Offset: 0x0000A458
		public string ToDisplayString(out string deviceLayoutName, out string controlPath, InputBinding.DisplayStringOptions options = (InputBinding.DisplayStringOptions)0, InputControl control = null)
		{
			if (this.isComposite)
			{
				deviceLayoutName = null;
				controlPath = null;
				return string.Empty;
			}
			InputControlPath.HumanReadableStringOptions humanReadableStringOptions = InputControlPath.HumanReadableStringOptions.None;
			if ((options & InputBinding.DisplayStringOptions.DontOmitDevice) == (InputBinding.DisplayStringOptions)0)
			{
				humanReadableStringOptions |= InputControlPath.HumanReadableStringOptions.OmitDevice;
			}
			if ((options & InputBinding.DisplayStringOptions.DontUseShortDisplayNames) == (InputBinding.DisplayStringOptions)0)
			{
				humanReadableStringOptions |= InputControlPath.HumanReadableStringOptions.UseShortNames;
			}
			string text = InputControlPath.ToHumanReadableString(((options & InputBinding.DisplayStringOptions.IgnoreBindingOverrides) != (InputBinding.DisplayStringOptions)0) ? this.path : this.effectivePath, out deviceLayoutName, out controlPath, humanReadableStringOptions, control);
			if (!string.IsNullOrEmpty(this.effectiveInteractions) && (options & InputBinding.DisplayStringOptions.DontIncludeInteractions) == (InputBinding.DisplayStringOptions)0)
			{
				string text2 = string.Empty;
				foreach (NameAndParameters nameAndParameters in NameAndParameters.ParseMultiple(this.effectiveInteractions))
				{
					string displayName = InputInteraction.GetDisplayName(nameAndParameters.name);
					if (!string.IsNullOrEmpty(displayName))
					{
						if (!string.IsNullOrEmpty(text2))
						{
							text2 = text2 + " or " + displayName;
						}
						else
						{
							text2 = displayName;
						}
					}
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text = text2 + " " + text;
				}
			}
			return text;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C350 File Offset: 0x0000A550
		internal bool TriggersAction(InputAction action)
		{
			return string.Compare(action.name, this.action, StringComparison.InvariantCultureIgnoreCase) == 0 || this.action == action.m_Id;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C379 File Offset: 0x0000A579
		public bool Matches(InputBinding binding)
		{
			return this.Matches(ref binding, (InputBinding.MatchOptions)0);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C384 File Offset: 0x0000A584
		internal bool Matches(ref InputBinding binding, InputBinding.MatchOptions options = (InputBinding.MatchOptions)0)
		{
			if (this.name != null && (binding.name == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.name, binding.name, ';')))
			{
				return false;
			}
			if (this.path != null && (binding.path == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.path, binding.path, ';')))
			{
				return false;
			}
			if (this.action != null && (binding.action == null || !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.action, binding.action, ';')))
			{
				return false;
			}
			if (this.groups != null)
			{
				bool flag = !string.IsNullOrEmpty(binding.groups);
				if (!flag && (options & InputBinding.MatchOptions.EmptyGroupMatchesAny) == (InputBinding.MatchOptions)0)
				{
					return false;
				}
				if (flag && !StringHelpers.CharacterSeparatedListsHaveAtLeastOneCommonElement(this.groups, binding.groups, ';'))
				{
					return false;
				}
			}
			return string.IsNullOrEmpty(this.m_Id) || !(binding.id != this.id);
		}

		// Token: 0x040000D7 RID: 215
		public const char Separator = ';';

		// Token: 0x040000D8 RID: 216
		internal const string kSeparatorString = ";";

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		private string m_Name;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		internal string m_Id;

		// Token: 0x040000DB RID: 219
		[Tooltip("Path of the control to bind to. Matched at runtime to controls from InputDevices present at the time.\n\nCan either be graphically from the control picker dropdown UI or edited manually in text mode by clicking the 'T' button. Internally, both methods result in control path strings that look like, for example, \"<Gamepad>/buttonSouth\".")]
		[SerializeField]
		private string m_Path;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		private string m_Interactions;

		// Token: 0x040000DD RID: 221
		[SerializeField]
		private string m_Processors;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		internal string m_Groups;

		// Token: 0x040000DF RID: 223
		[SerializeField]
		private string m_Action;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		internal InputBinding.Flags m_Flags;

		// Token: 0x040000E1 RID: 225
		[NonSerialized]
		private string m_OverridePath;

		// Token: 0x040000E2 RID: 226
		[NonSerialized]
		private string m_OverrideInteractions;

		// Token: 0x040000E3 RID: 227
		[NonSerialized]
		private string m_OverrideProcessors;

		// Token: 0x02000173 RID: 371
		[Flags]
		public enum DisplayStringOptions
		{
			// Token: 0x04000801 RID: 2049
			DontUseShortDisplayNames = 1,
			// Token: 0x04000802 RID: 2050
			DontOmitDevice = 2,
			// Token: 0x04000803 RID: 2051
			DontIncludeInteractions = 4,
			// Token: 0x04000804 RID: 2052
			IgnoreBindingOverrides = 8
		}

		// Token: 0x02000174 RID: 372
		[Flags]
		internal enum MatchOptions
		{
			// Token: 0x04000806 RID: 2054
			EmptyGroupMatchesAny = 1
		}

		// Token: 0x02000175 RID: 373
		[Flags]
		internal enum Flags
		{
			// Token: 0x04000808 RID: 2056
			None = 0,
			// Token: 0x04000809 RID: 2057
			Composite = 4,
			// Token: 0x0400080A RID: 2058
			PartOfComposite = 8
		}
	}
}
