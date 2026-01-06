using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000033 RID: 51
	public static class InputControlPath
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00011D03 File Offset: 0x0000FF03
		internal static string CleanSlashes(this string pathComponent)
		{
			return pathComponent.Replace('/', ' ');
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00011D10 File Offset: 0x0000FF10
		public static string Combine(InputControl parent, string path)
		{
			if (parent == null)
			{
				if (string.IsNullOrEmpty(path))
				{
					return string.Empty;
				}
				if (path[0] != '/')
				{
					return "/" + path;
				}
				return path;
			}
			else
			{
				if (string.IsNullOrEmpty(path))
				{
					return parent.path;
				}
				return parent.path + "/" + path;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00011D68 File Offset: 0x0000FF68
		public static string ToHumanReadableString(string path, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			string text;
			string text2;
			return InputControlPath.ToHumanReadableString(path, out text, out text2, options, control);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00011D84 File Offset: 0x0000FF84
		public static string ToHumanReadableString(string path, out string deviceLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options = InputControlPath.HumanReadableStringOptions.None, InputControl control = null)
		{
			deviceLayoutName = null;
			controlPath = null;
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			if (control != null)
			{
				InputControl inputControl = InputControlPath.TryFindControl(control, path, 0) ?? (InputControlPath.Matches(path, control) ? control : null);
				if (inputControl != null)
				{
					string text = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(inputControl.shortDisplayName)) ? inputControl.shortDisplayName : inputControl.displayName);
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None)
					{
						text = text + " [" + inputControl.device.displayName + "]";
					}
					deviceLayoutName = inputControl.device.layout;
					if (!(inputControl is InputDevice))
					{
						controlPath = inputControl.path.Substring(inputControl.device.path.Length + 1);
					}
					return text;
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			string text4;
			using (InputControlLayout.CacheRef())
			{
				if (pathParser.MoveToNextComponent())
				{
					string text3;
					string text2 = pathParser.current.ToHumanReadableString(null, null, out text3, out text4, options);
					deviceLayoutName = text3;
					bool flag = true;
					while (pathParser.MoveToNextComponent())
					{
						if (!flag)
						{
							stringBuilder.Append('/');
						}
						stringBuilder.Append(pathParser.current.ToHumanReadableString(text3, controlPath, out text3, out controlPath, options));
						flag = false;
					}
					if ((options & InputControlPath.HumanReadableStringOptions.OmitDevice) == InputControlPath.HumanReadableStringOptions.None && !string.IsNullOrEmpty(text2))
					{
						stringBuilder.Append(" [");
						stringBuilder.Append(text2);
						stringBuilder.Append(']');
					}
				}
				if (stringBuilder.Length == 0)
				{
					text4 = path;
				}
				else
				{
					text4 = stringBuilder.ToString();
				}
			}
			return text4;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011F18 File Offset: 0x00010118
		public static string[] TryGetDeviceUsages(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.m_Usages.length > 0)
			{
				return pathParser.current.m_Usages.ToArray<string>((Substring x) => x.ToString());
			}
			return null;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011F8C File Offset: 0x0001018C
		public static string TryGetDeviceLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.m_Layout.length > 0)
			{
				return pathParser.current.m_Layout.ToString().Unescape("ntr\\\"", "\n\t\r\\\"");
			}
			if (pathParser.current.isWildcard)
			{
				return "*";
			}
			return null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001200C File Offset: 0x0001020C
		public static string TryGetControlLayout(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			int length = path.Length;
			int num = path.LastIndexOf('/');
			if (num == -1 || num == 0)
			{
				return null;
			}
			if (length > num + 2 && path[num + 1] == '<' && path[length - 1] == '>')
			{
				int num2 = num + 2;
				int num3 = length - num2 - 1;
				return path.Substring(num2, num3);
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(path);
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.isWildcard)
			{
				throw new NotImplementedException();
			}
			if (pathParser.current.m_Layout.length == 0)
			{
				return null;
			}
			string text = pathParser.current.m_Layout.ToString();
			if (!pathParser.MoveToNextComponent())
			{
				return null;
			}
			if (pathParser.current.isWildcard)
			{
				return "*";
			}
			return InputControlPath.FindControlLayoutRecursive(ref pathParser, text.Unescape("ntr\\\"", "\n\t\r\\\""));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00012104 File Offset: 0x00010304
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, string layoutName)
		{
			string text;
			using (InputControlLayout.CacheRef())
			{
				InputControlLayout inputControlLayout = InputControlLayout.cache.FindOrLoadLayout(new InternedString(layoutName), false);
				if (inputControlLayout == null)
				{
					text = null;
				}
				else
				{
					text = InputControlPath.FindControlLayoutRecursive(ref parser, inputControlLayout);
				}
			}
			return text;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00012160 File Offset: 0x00010360
		private static string FindControlLayoutRecursive(ref InputControlPath.PathParser parser, InputControlLayout layout)
		{
			string text = null;
			int count = layout.controls.Count;
			for (int i = 0; i < count; i++)
			{
				if (InputControlPath.ControlLayoutMatchesPathComponent(ref layout.m_Controls[i], ref parser))
				{
					InternedString layout2 = layout.m_Controls[i].layout;
					if (!parser.isAtEnd)
					{
						InputControlPath.PathParser pathParser = parser;
						if (pathParser.MoveToNextComponent())
						{
							string text2 = InputControlPath.FindControlLayoutRecursive(ref pathParser, layout2);
							if (text2 != null)
							{
								if (text != null && text2 != text)
								{
									return null;
								}
								text = text2;
							}
						}
					}
					else
					{
						if (text != null && layout2 != text)
						{
							return null;
						}
						text = layout2.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00012218 File Offset: 0x00010418
		private static bool ControlLayoutMatchesPathComponent(ref InputControlLayout.ControlItem controlItem, ref InputControlPath.PathParser parser)
		{
			Substring layout = parser.current.m_Layout;
			if (layout.length > 0 && !InputControlPath.StringMatches(layout, controlItem.layout))
			{
				return false;
			}
			if (parser.current.m_Usages.length > 0)
			{
				for (int i = 0; i < parser.current.m_Usages.length; i++)
				{
					Substring substring = parser.current.m_Usages[i];
					if (substring.length > 0)
					{
						int count = controlItem.usages.Count;
						bool flag = false;
						for (int j = 0; j < count; j++)
						{
							if (InputControlPath.StringMatches(substring, controlItem.usages[j]))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
			}
			Substring name = parser.current.m_Name;
			return name.length <= 0 || InputControlPath.StringMatches(name, controlItem.name);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00012308 File Offset: 0x00010508
		private static bool StringMatches(Substring str, InternedString matchTo)
		{
			int length = str.length;
			int length2 = matchTo.length;
			string text = matchTo.ToLower();
			int num = 0;
			int num2 = 0;
			while (num2 < length && num < length2)
			{
				char c = str[num2];
				if (c == '\\' && num2 + 1 < length)
				{
					c = str[++num2];
				}
				if (c == '*')
				{
					if (num2 == length - 1)
					{
						return true;
					}
					num2++;
					c = char.ToLower(str[num2]);
					while (num < length2 && text[num] != c)
					{
						num++;
					}
					if (num == length2)
					{
						return false;
					}
				}
				else if (char.ToLower(c) != text[num])
				{
					return false;
				}
				num++;
				num2++;
			}
			return num == length2 && num2 == length;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000123D2 File Offset: 0x000105D2
		public static InputControl TryFindControl(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindControl<InputControl>(control, path, indexInPath);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000123DC File Offset: 0x000105DC
		public static InputControl[] TryFindControls(InputControl control, string path, int indexInPath = 0)
		{
			InputControlList<InputControl> inputControlList = new InputControlList<InputControl>(Allocator.Temp, 0);
			InputControl[] array;
			try
			{
				InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref inputControlList);
				array = inputControlList.ToArray(false);
			}
			finally
			{
				inputControlList.Dispose();
			}
			return array;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00012424 File Offset: 0x00010624
		public static int TryFindControls(InputControl control, string path, ref InputControlList<InputControl> matches, int indexInPath = 0)
		{
			return InputControlPath.TryFindControls<InputControl>(control, path, indexInPath, ref matches);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012430 File Offset: 0x00010630
		public static TControl TryFindControl<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (string.IsNullOrEmpty(path))
			{
				return default(TControl);
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			InputControlList<TControl> inputControlList = default(InputControlList<TControl>);
			return InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref inputControlList, false);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00012484 File Offset: 0x00010684
		public static int TryFindControls<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (indexInPath == 0 && path[0] == '/')
			{
				indexInPath++;
			}
			int count = matches.Count;
			InputControlPath.MatchControlsRecursive<TControl>(control, path, indexInPath, ref matches, true);
			return matches.Count - count;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000124DA File Offset: 0x000106DA
		public static InputControl TryFindChild(InputControl control, string path, int indexInPath = 0)
		{
			return InputControlPath.TryFindChild<InputControl>(control, path, indexInPath);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000124E4 File Offset: 0x000106E4
		public static TControl TryFindChild<TControl>(InputControl control, string path, int indexInPath = 0) where TControl : InputControl
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			ReadOnlyArray<InputControl> children = control.children;
			int count = children.Count;
			for (int i = 0; i < count; i++)
			{
				TControl tcontrol = InputControlPath.TryFindControl<TControl>(children[i], path, indexInPath);
				if (tcontrol != null)
				{
					return tcontrol;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001254C File Offset: 0x0001074C
		public static bool Matches(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref pathParser, control, false);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001258C File Offset: 0x0001078C
		public static bool MatchesPrefix(string expected, InputControl control)
		{
			if (string.IsNullOrEmpty(expected))
			{
				throw new ArgumentNullException("expected");
			}
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			InputControlPath.PathParser pathParser = new InputControlPath.PathParser(expected);
			return InputControlPath.MatchesRecursive(ref pathParser, control, true) && pathParser.isAtEnd;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000125DC File Offset: 0x000107DC
		private static bool MatchesRecursive(ref InputControlPath.PathParser parser, InputControl currentControl, bool prefixOnly = false)
		{
			InputControl parent = currentControl.parent;
			if (parent != null && !InputControlPath.MatchesRecursive(ref parser, parent, prefixOnly))
			{
				return false;
			}
			if (!parser.MoveToNextComponent())
			{
				return prefixOnly;
			}
			return parser.current.Matches(currentControl);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00012618 File Offset: 0x00010818
		private static TControl MatchControlsRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			int length = path.Length;
			bool flag = true;
			if (path[indexInPath] == '<')
			{
				indexInPath++;
				flag = InputControlPath.MatchPathComponent(control.layout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
				if (!flag)
				{
					InternedString layout = control.m_Layout;
					while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(layout, out layout))
					{
						flag = InputControlPath.MatchPathComponent(layout, path, ref indexInPath, InputControlPath.PathComponentType.Layout, 0);
						if (flag)
						{
							break;
						}
					}
				}
			}
			while (indexInPath < length && path[indexInPath] == '{' && flag)
			{
				indexInPath++;
				for (int i = 0; i < control.usages.Count; i++)
				{
					flag = InputControlPath.MatchPathComponent(control.usages[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0);
					if (flag)
					{
						break;
					}
				}
			}
			if (indexInPath < length - 1 && flag && path[indexInPath] == '#' && path[indexInPath + 1] == '(')
			{
				indexInPath += 2;
				flag = InputControlPath.MatchPathComponent(control.displayName, path, ref indexInPath, InputControlPath.PathComponentType.DisplayName, 0);
			}
			if (indexInPath < length && flag && path[indexInPath] != '/')
			{
				flag = InputControlPath.MatchPathComponent(control.name, path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
				if (!flag)
				{
					int num = 0;
					while (num < control.aliases.Count && !flag)
					{
						flag = InputControlPath.MatchPathComponent(control.aliases[num], path, ref indexInPath, InputControlPath.PathComponentType.Name, 0);
						num++;
					}
				}
			}
			if (flag)
			{
				if (indexInPath < length && path[indexInPath] == '*')
				{
					indexInPath++;
				}
				if (indexInPath == length)
				{
					TControl tcontrol = control as TControl;
					if (tcontrol == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(tcontrol);
					}
					return tcontrol;
				}
				else if (path[indexInPath] == '/')
				{
					indexInPath++;
					if (indexInPath != length)
					{
						TControl tcontrol2;
						if (path[indexInPath] == '{')
						{
							tcontrol2 = InputControlPath.MatchByUsageAtDeviceRootRecursive<TControl>(control.device, path, indexInPath, ref matches, matchMultiple);
						}
						else
						{
							tcontrol2 = InputControlPath.MatchChildrenRecursive<TControl>(control, path, indexInPath, ref matches, matchMultiple);
						}
						return tcontrol2;
					}
					TControl tcontrol3 = control as TControl;
					if (tcontrol3 == null)
					{
						return default(TControl);
					}
					if (matchMultiple)
					{
						matches.Add(tcontrol3);
					}
					return tcontrol3;
				}
			}
			return default(TControl);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001284C File Offset: 0x00010A4C
		private static TControl MatchByUsageAtDeviceRootRecursive<TControl>(InputDevice device, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			InternedString[] usagesForEachControl = device.m_UsagesForEachControl;
			if (usagesForEachControl == null)
			{
				return default(TControl);
			}
			int num = device.m_UsageToControl.LengthSafe<InputControl>();
			int num2 = indexInPath + 1;
			bool flag = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			int length = path.Length;
			indexInPath++;
			if (indexInPath == length)
			{
				throw new ArgumentException("Invalid path spec '" + path + "'; trailing '{'", "path");
			}
			TControl tcontrol = default(TControl);
			for (int i = 0; i < num; i++)
			{
				if (!InputControlPath.MatchPathComponent(usagesForEachControl[i], path, ref indexInPath, InputControlPath.PathComponentType.Usage, 0))
				{
					indexInPath = num2;
				}
				else
				{
					InputControl inputControl = device.m_UsageToControl[i];
					if (indexInPath < length && path[indexInPath] == '/')
					{
						tcontrol = InputControlPath.MatchChildrenRecursive<TControl>(inputControl, path, indexInPath + 1, ref matches, matchMultiple);
						if (tcontrol != null && !flag)
						{
							break;
						}
						if (tcontrol != null && !matchMultiple)
						{
							break;
						}
					}
					else
					{
						tcontrol = inputControl as TControl;
						if (tcontrol != null)
						{
							if (!matchMultiple)
							{
								break;
							}
							matches.Add(tcontrol);
						}
					}
				}
			}
			return tcontrol;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001295C File Offset: 0x00010B5C
		private static TControl MatchChildrenRecursive<TControl>(InputControl control, string path, int indexInPath, ref InputControlList<TControl> matches, bool matchMultiple) where TControl : InputControl
		{
			ReadOnlyArray<InputControl> children = control.children;
			int count = children.Count;
			TControl tcontrol = default(TControl);
			bool flag = InputControlPath.PathComponentCanYieldMultipleMatches(path, indexInPath);
			for (int i = 0; i < count; i++)
			{
				TControl tcontrol2 = InputControlPath.MatchControlsRecursive<TControl>(children[i], path, indexInPath, ref matches, matchMultiple);
				if (tcontrol2 != null)
				{
					if (!flag)
					{
						return tcontrol2;
					}
					if (!matchMultiple)
					{
						return tcontrol2;
					}
					tcontrol = tcontrol2;
				}
			}
			return tcontrol;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000129C8 File Offset: 0x00010BC8
		private static bool MatchPathComponent(string component, string path, ref int indexInPath, InputControlPath.PathComponentType componentType, int startIndexInComponent = 0)
		{
			int length = component.Length;
			int length2 = path.Length;
			int num = indexInPath;
			int num2 = startIndexInComponent;
			while (indexInPath < length2)
			{
				char c = path[indexInPath];
				if (c == '\\' && indexInPath + 1 < length2)
				{
					indexInPath++;
					c = path[indexInPath];
				}
				else
				{
					if (c == '/' && componentType == InputControlPath.PathComponentType.Name)
					{
						break;
					}
					if ((c == '>' && componentType == InputControlPath.PathComponentType.Layout) || (c == '}' && componentType == InputControlPath.PathComponentType.Usage) || (c == ')' && componentType == InputControlPath.PathComponentType.DisplayName))
					{
						indexInPath++;
						break;
					}
					if (c == '*')
					{
						int num3 = indexInPath + 1;
						if (indexInPath < length2 - 1 && num2 < length && InputControlPath.MatchPathComponent(component, path, ref num3, componentType, num2))
						{
							indexInPath = num3;
							return true;
						}
						if (num2 < length)
						{
							num2++;
							continue;
						}
						return true;
					}
				}
				if (num2 == length)
				{
					indexInPath = num;
					return false;
				}
				char c2 = component[num2];
				if (c2 != c && char.ToLower(c2) != char.ToLower(c))
				{
					indexInPath = num;
					return false;
				}
				num2++;
				indexInPath++;
			}
			if (num2 == length)
			{
				return true;
			}
			indexInPath = num;
			return false;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00012ACC File Offset: 0x00010CCC
		private static bool PathComponentCanYieldMultipleMatches(string path, int indexInPath)
		{
			int num = path.IndexOf('/', indexInPath);
			if (num == -1)
			{
				return path.IndexOf('*', indexInPath) != -1 || path.IndexOf('<', indexInPath) != -1;
			}
			int num2 = num - indexInPath;
			return path.IndexOf('*', indexInPath, num2) != -1 || path.IndexOf('<', indexInPath, num2) != -1;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00012B28 File Offset: 0x00010D28
		public static IEnumerable<InputControlPath.ParsedPathComponent> Parse(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControlPath.PathParser parser = new InputControlPath.PathParser(path);
			while (parser.MoveToNextComponent())
			{
				yield return parser.current;
			}
			yield break;
		}

		// Token: 0x0400013C RID: 316
		public const string Wildcard = "*";

		// Token: 0x0400013D RID: 317
		public const string DoubleWildcard = "**";

		// Token: 0x0400013E RID: 318
		public const char Separator = '/';

		// Token: 0x0400013F RID: 319
		internal const char SeparatorReplacement = ' ';

		// Token: 0x0200018A RID: 394
		[Flags]
		public enum HumanReadableStringOptions
		{
			// Token: 0x04000867 RID: 2151
			None = 0,
			// Token: 0x04000868 RID: 2152
			OmitDevice = 2,
			// Token: 0x04000869 RID: 2153
			UseShortNames = 4
		}

		// Token: 0x0200018B RID: 395
		private enum PathComponentType
		{
			// Token: 0x0400086B RID: 2155
			Name,
			// Token: 0x0400086C RID: 2156
			DisplayName,
			// Token: 0x0400086D RID: 2157
			Usage,
			// Token: 0x0400086E RID: 2158
			Layout
		}

		// Token: 0x0200018C RID: 396
		public struct ParsedPathComponent
		{
			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x06001380 RID: 4992 RVA: 0x00059F74 File Offset: 0x00058174
			public string layout
			{
				get
				{
					return this.m_Layout.ToString();
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x06001381 RID: 4993 RVA: 0x00059F87 File Offset: 0x00058187
			public IEnumerable<string> usages
			{
				get
				{
					return this.m_Usages.Select((Substring x) => x.ToString());
				}
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06001382 RID: 4994 RVA: 0x00059FB8 File Offset: 0x000581B8
			public string name
			{
				get
				{
					return this.m_Name.ToString();
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x06001383 RID: 4995 RVA: 0x00059FCB File Offset: 0x000581CB
			public string displayName
			{
				get
				{
					return this.m_DisplayName.ToString();
				}
			}

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x06001384 RID: 4996 RVA: 0x00059FDE File Offset: 0x000581DE
			internal bool isWildcard
			{
				get
				{
					return this.m_Name == "*";
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x06001385 RID: 4997 RVA: 0x00059FF5 File Offset: 0x000581F5
			internal bool isDoubleWildcard
			{
				get
				{
					return this.m_Name == "**";
				}
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x0005A00C File Offset: 0x0005820C
			internal string ToHumanReadableString(string parentLayoutName, string parentControlPath, out string referencedLayoutName, out string controlPath, InputControlPath.HumanReadableStringOptions options)
			{
				referencedLayoutName = null;
				controlPath = null;
				string text = string.Empty;
				if (this.isWildcard)
				{
					text += "Any";
				}
				if (this.m_Usages.length > 0)
				{
					string text2 = string.Empty;
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							if (text2 != string.Empty)
							{
								text2 = text2 + " & " + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
							else
							{
								text2 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Usages[i]);
							}
						}
					}
					if (text2 != string.Empty)
					{
						if (text != string.Empty)
						{
							text = text + " " + text2;
						}
						else
						{
							text += text2;
						}
					}
				}
				if (!this.m_Layout.isEmpty)
				{
					referencedLayoutName = this.m_Layout.ToString();
					InputControlLayout inputControlLayout = InputControlLayout.cache.FindOrLoadLayout(referencedLayoutName, false);
					string text3;
					if (inputControlLayout != null && !string.IsNullOrEmpty(inputControlLayout.m_DisplayName))
					{
						text3 = inputControlLayout.m_DisplayName;
					}
					else
					{
						text3 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Layout);
					}
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text3;
					}
					else
					{
						text += text3;
					}
				}
				if (!this.m_Name.isEmpty && !this.isWildcard)
				{
					string text4 = null;
					if (!string.IsNullOrEmpty(parentLayoutName))
					{
						InputControlLayout inputControlLayout2 = InputControlLayout.cache.FindOrLoadLayout(new InternedString(parentLayoutName), false);
						if (inputControlLayout2 != null)
						{
							InternedString internedString = new InternedString(this.m_Name.ToString());
							int num;
							InputControlLayout.ControlItem? controlItem = inputControlLayout2.FindControlIncludingArrayElements(internedString, out num);
							if (controlItem != null)
							{
								if (string.IsNullOrEmpty(parentControlPath))
								{
									if (num != -1)
									{
										controlPath = string.Format("{0}{1}", controlItem.Value.name, num);
									}
									else
									{
										controlPath = controlItem.Value.name;
									}
								}
								else if (num != -1)
								{
									controlPath = string.Format("{0}/{1}{2}", parentControlPath, controlItem.Value.name, num);
								}
								else
								{
									controlPath = string.Format("{0}/{1}", parentControlPath, controlItem.Value.name);
								}
								string text5 = (((options & InputControlPath.HumanReadableStringOptions.UseShortNames) != InputControlPath.HumanReadableStringOptions.None) ? controlItem.Value.shortDisplayName : null);
								string text6 = ((!string.IsNullOrEmpty(text5)) ? text5 : controlItem.Value.displayName);
								if (!string.IsNullOrEmpty(text6))
								{
									if (num != -1)
									{
										text4 = string.Format("{0} #{1}", text6, num);
									}
									else
									{
										text4 = text6;
									}
								}
								if (string.IsNullOrEmpty(referencedLayoutName))
								{
									referencedLayoutName = controlItem.Value.layout;
								}
							}
						}
					}
					if (text4 == null)
					{
						text4 = InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_Name);
					}
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text4;
					}
					else
					{
						text += text4;
					}
				}
				if (!this.m_DisplayName.isEmpty)
				{
					string text7 = "\"" + InputControlPath.ParsedPathComponent.ToHumanReadableString(this.m_DisplayName) + "\"";
					if (!string.IsNullOrEmpty(text))
					{
						text = text + " " + text7;
					}
					else
					{
						text += text7;
					}
				}
				return text;
			}

			// Token: 0x06001387 RID: 4999 RVA: 0x0005A38A File Offset: 0x0005858A
			private static string ToHumanReadableString(Substring substring)
			{
				return substring.ToString().Unescape("/*{<", "/*{<");
			}

			// Token: 0x06001388 RID: 5000 RVA: 0x0005A3A8 File Offset: 0x000585A8
			public bool Matches(InputControl control)
			{
				if (!this.m_Layout.isEmpty)
				{
					bool flag = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, control.layout);
					if (!flag)
					{
						InternedString layout = control.m_Layout;
						while (InputControlLayout.s_Layouts.baseLayoutTable.TryGetValue(layout, out layout) && !flag)
						{
							flag = InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Layout, layout.ToString());
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				if (this.m_Usages.length > 0)
				{
					for (int i = 0; i < this.m_Usages.length; i++)
					{
						if (!this.m_Usages[i].isEmpty)
						{
							ReadOnlyArray<InternedString> usages = control.usages;
							bool flag2 = false;
							for (int j = 0; j < usages.Count; j++)
							{
								if (InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Usages[i], usages[j]))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								return false;
							}
						}
					}
				}
				return (this.m_Name.isEmpty || this.isWildcard || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_Name, control.name)) && (this.m_DisplayName.isEmpty || InputControlPath.ParsedPathComponent.ComparePathElementToString(this.m_DisplayName, control.displayName));
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x0005A4EC File Offset: 0x000586EC
			private static bool ComparePathElementToString(Substring pathElement, string element)
			{
				int length = pathElement.length;
				int length2 = element.Length;
				int num = 0;
				int num2 = 0;
				bool flag;
				bool flag2;
				for (;;)
				{
					flag = num == length;
					flag2 = num2 == length2;
					if (flag || flag2)
					{
						break;
					}
					char c = pathElement[num];
					if (c == '\\' && num + 1 < length)
					{
						c = pathElement[++num];
					}
					if (char.ToLowerInvariant(c) != char.ToLowerInvariant(element[num2]))
					{
						return false;
					}
					num++;
					num2++;
				}
				return flag == flag2;
			}

			// Token: 0x0400086F RID: 2159
			internal Substring m_Layout;

			// Token: 0x04000870 RID: 2160
			internal InlinedArray<Substring> m_Usages;

			// Token: 0x04000871 RID: 2161
			internal Substring m_Name;

			// Token: 0x04000872 RID: 2162
			internal Substring m_DisplayName;
		}

		// Token: 0x0200018D RID: 397
		private struct PathParser
		{
			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x0600138A RID: 5002 RVA: 0x0005A56A File Offset: 0x0005876A
			public bool isAtEnd
			{
				get
				{
					return this.rightIndexInPath == this.length;
				}
			}

			// Token: 0x0600138B RID: 5003 RVA: 0x0005A57A File Offset: 0x0005877A
			public PathParser(string path)
			{
				this.path = path;
				this.length = path.Length;
				this.leftIndexInPath = 0;
				this.rightIndexInPath = 0;
				this.current = default(InputControlPath.ParsedPathComponent);
			}

			// Token: 0x0600138C RID: 5004 RVA: 0x0005A5AC File Offset: 0x000587AC
			public bool MoveToNextComponent()
			{
				if (this.rightIndexInPath == this.length)
				{
					return false;
				}
				this.leftIndexInPath = this.rightIndexInPath;
				if (this.path[this.leftIndexInPath] == '/')
				{
					this.leftIndexInPath++;
					this.rightIndexInPath = this.leftIndexInPath;
					if (this.leftIndexInPath == this.length)
					{
						return false;
					}
				}
				Substring substring = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '<')
				{
					substring = this.ParseComponentPart('>');
				}
				InlinedArray<Substring> inlinedArray = default(InlinedArray<Substring>);
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] == '{')
				{
					inlinedArray.AppendWithCapacity(this.ParseComponentPart('}'), 10);
				}
				Substring substring2 = default(Substring);
				if (this.rightIndexInPath < this.length - 1 && this.path[this.rightIndexInPath] == '#' && this.path[this.rightIndexInPath + 1] == '(')
				{
					this.rightIndexInPath++;
					substring2 = this.ParseComponentPart(')');
				}
				Substring substring3 = default(Substring);
				if (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != '/')
				{
					substring3 = this.ParseComponentPart('/');
				}
				this.current = new InputControlPath.ParsedPathComponent
				{
					m_Layout = substring,
					m_Usages = inlinedArray,
					m_Name = substring3,
					m_DisplayName = substring2
				};
				return this.leftIndexInPath != this.rightIndexInPath;
			}

			// Token: 0x0600138D RID: 5005 RVA: 0x0005A758 File Offset: 0x00058958
			private Substring ParseComponentPart(char terminator)
			{
				if (terminator != '/')
				{
					this.rightIndexInPath++;
				}
				int num = this.rightIndexInPath;
				while (this.rightIndexInPath < this.length && this.path[this.rightIndexInPath] != terminator)
				{
					if (this.path[this.rightIndexInPath] == '\\' && this.rightIndexInPath + 1 < this.length)
					{
						this.rightIndexInPath++;
					}
					this.rightIndexInPath++;
				}
				int num2 = this.rightIndexInPath - num;
				if (this.rightIndexInPath < this.length && terminator != '/')
				{
					this.rightIndexInPath++;
				}
				return new Substring(this.path, num, num2);
			}

			// Token: 0x04000873 RID: 2163
			private string path;

			// Token: 0x04000874 RID: 2164
			private int length;

			// Token: 0x04000875 RID: 2165
			private int leftIndexInPath;

			// Token: 0x04000876 RID: 2166
			private int rightIndexInPath;

			// Token: 0x04000877 RID: 2167
			public InputControlPath.ParsedPathComponent current;
		}
	}
}
