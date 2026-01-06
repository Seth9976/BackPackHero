using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200010C RID: 268
	public struct InputDeviceMatcher : IEquatable<InputDeviceMatcher>
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0004B050 File Offset: 0x00049250
		public bool empty
		{
			get
			{
				return this.m_Patterns == null;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0004B05B File Offset: 0x0004925B
		public IEnumerable<KeyValuePair<string, object>> patterns
		{
			get
			{
				if (this.m_Patterns == null)
				{
					yield break;
				}
				int count = this.m_Patterns.Length;
				int num;
				for (int i = 0; i < count; i = num)
				{
					yield return new KeyValuePair<string, object>(this.m_Patterns[i].Key.ToString(), this.m_Patterns[i].Value);
					num = i + 1;
				}
				yield break;
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0004B070 File Offset: 0x00049270
		public InputDeviceMatcher WithInterface(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kInterfaceKey, pattern, supportRegex);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0004B07F File Offset: 0x0004927F
		public InputDeviceMatcher WithDeviceClass(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kDeviceClassKey, pattern, supportRegex);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0004B08E File Offset: 0x0004928E
		public InputDeviceMatcher WithManufacturer(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kManufacturerKey, pattern, supportRegex);
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0004B09D File Offset: 0x0004929D
		public InputDeviceMatcher WithProduct(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kProductKey, pattern, supportRegex);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0004B0AC File Offset: 0x000492AC
		public InputDeviceMatcher WithVersion(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kVersionKey, pattern, supportRegex);
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0004B0BB File Offset: 0x000492BB
		public InputDeviceMatcher WithCapability<TValue>(string path, TValue value)
		{
			return this.With(new InternedString(path), value, true);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0004B0D0 File Offset: 0x000492D0
		private InputDeviceMatcher With(InternedString key, object value, bool supportRegex = true)
		{
			if (supportRegex)
			{
				string text = value as string;
				if (text != null)
				{
					double num;
					if (!text.All((char ch) => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)) && !double.TryParse(text, out num))
					{
						value = new Regex(text, RegexOptions.IgnoreCase);
					}
				}
			}
			InputDeviceMatcher inputDeviceMatcher = this;
			ArrayHelpers.Append<KeyValuePair<InternedString, object>>(ref inputDeviceMatcher.m_Patterns, new KeyValuePair<InternedString, object>(key, value));
			return inputDeviceMatcher;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0004B148 File Offset: 0x00049348
		public float MatchPercentage(InputDeviceDescription deviceDescription)
		{
			if (this.empty)
			{
				return 0f;
			}
			int num = this.m_Patterns.Length;
			for (int i = 0; i < num; i++)
			{
				InternedString key = this.m_Patterns[i].Key;
				object value = this.m_Patterns[i].Value;
				if (key == InputDeviceMatcher.kInterfaceKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.interfaceName) || !InputDeviceMatcher.MatchSingleProperty(value, deviceDescription.interfaceName))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kDeviceClassKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.deviceClass) || !InputDeviceMatcher.MatchSingleProperty(value, deviceDescription.deviceClass))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kManufacturerKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.manufacturer) || !InputDeviceMatcher.MatchSingleProperty(value, deviceDescription.manufacturer))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kProductKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.product) || !InputDeviceMatcher.MatchSingleProperty(value, deviceDescription.product))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kVersionKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.version) || !InputDeviceMatcher.MatchSingleProperty(value, deviceDescription.version))
					{
						return 0f;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(deviceDescription.capabilities))
					{
						return 0f;
					}
					JsonParser jsonParser = new JsonParser(deviceDescription.capabilities);
					if (!jsonParser.NavigateToProperty(key.ToString()) || !jsonParser.CurrentPropertyHasValueEqualTo(new JsonParser.JsonValue
					{
						type = JsonParser.JsonValueType.Any,
						anyValue = value
					}))
					{
						return 0f;
					}
				}
			}
			int numPropertiesIn = InputDeviceMatcher.GetNumPropertiesIn(deviceDescription);
			float num2 = 1f / (float)numPropertiesIn;
			return (float)num * num2;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0004B320 File Offset: 0x00049520
		private static bool MatchSingleProperty(object pattern, string value)
		{
			string text = pattern as string;
			if (text != null)
			{
				return string.Compare(text, value, StringComparison.OrdinalIgnoreCase) == 0;
			}
			Regex regex = pattern as Regex;
			return regex != null && regex.IsMatch(value);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0004B358 File Offset: 0x00049558
		private static int GetNumPropertiesIn(InputDeviceDescription description)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(description.interfaceName))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.deviceClass))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.manufacturer))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.product))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.version))
			{
				num++;
			}
			if (!string.IsNullOrEmpty(description.capabilities))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0004B3D4 File Offset: 0x000495D4
		public static InputDeviceMatcher FromDeviceDescription(InputDeviceDescription deviceDescription)
		{
			InputDeviceMatcher inputDeviceMatcher = default(InputDeviceMatcher);
			if (!string.IsNullOrEmpty(deviceDescription.interfaceName))
			{
				inputDeviceMatcher = inputDeviceMatcher.WithInterface(deviceDescription.interfaceName, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.deviceClass))
			{
				inputDeviceMatcher = inputDeviceMatcher.WithDeviceClass(deviceDescription.deviceClass, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.manufacturer))
			{
				inputDeviceMatcher = inputDeviceMatcher.WithManufacturer(deviceDescription.manufacturer, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				inputDeviceMatcher = inputDeviceMatcher.WithProduct(deviceDescription.product, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.version))
			{
				inputDeviceMatcher = inputDeviceMatcher.WithVersion(deviceDescription.version, false);
			}
			return inputDeviceMatcher;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0004B480 File Offset: 0x00049680
		public override string ToString()
		{
			if (this.empty)
			{
				return "<empty>";
			}
			string text = string.Empty;
			foreach (KeyValuePair<InternedString, object> keyValuePair in this.m_Patterns)
			{
				if (text.Length > 0)
				{
					text += string.Format(",{0}={1}", keyValuePair.Key, keyValuePair.Value);
				}
				else
				{
					text += string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value);
				}
			}
			return text;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0004B514 File Offset: 0x00049714
		public bool Equals(InputDeviceMatcher other)
		{
			if (this.m_Patterns == other.m_Patterns)
			{
				return true;
			}
			if (this.m_Patterns == null || other.m_Patterns == null)
			{
				return false;
			}
			if (this.m_Patterns.Length != other.m_Patterns.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_Patterns.Length; i++)
			{
				KeyValuePair<InternedString, object> keyValuePair = this.m_Patterns[i];
				bool flag = false;
				int j = 0;
				while (j < this.m_Patterns.Length)
				{
					KeyValuePair<InternedString, object> keyValuePair2 = other.m_Patterns[j];
					if (!(keyValuePair.Key != keyValuePair2.Key))
					{
						if (!keyValuePair.Value.Equals(keyValuePair2.Value))
						{
							return false;
						}
						flag = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0004B5D0 File Offset: 0x000497D0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputDeviceMatcher)
			{
				InputDeviceMatcher inputDeviceMatcher = (InputDeviceMatcher)obj;
				return this.Equals(inputDeviceMatcher);
			}
			return false;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0004B5FA File Offset: 0x000497FA
		public static bool operator ==(InputDeviceMatcher left, InputDeviceMatcher right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0004B604 File Offset: 0x00049804
		public static bool operator !=(InputDeviceMatcher left, InputDeviceMatcher right)
		{
			return !(left == right);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0004B610 File Offset: 0x00049810
		public override int GetHashCode()
		{
			if (this.m_Patterns == null)
			{
				return 0;
			}
			return this.m_Patterns.GetHashCode();
		}

		// Token: 0x04000653 RID: 1619
		private KeyValuePair<InternedString, object>[] m_Patterns;

		// Token: 0x04000654 RID: 1620
		private static readonly InternedString kInterfaceKey = new InternedString("interface");

		// Token: 0x04000655 RID: 1621
		private static readonly InternedString kDeviceClassKey = new InternedString("deviceClass");

		// Token: 0x04000656 RID: 1622
		private static readonly InternedString kManufacturerKey = new InternedString("manufacturer");

		// Token: 0x04000657 RID: 1623
		private static readonly InternedString kProductKey = new InternedString("product");

		// Token: 0x04000658 RID: 1624
		private static readonly InternedString kVersionKey = new InternedString("version");

		// Token: 0x0200022B RID: 555
		[Serializable]
		internal struct MatcherJson
		{
			// Token: 0x06001568 RID: 5480 RVA: 0x00062354 File Offset: 0x00060554
			public static InputDeviceMatcher.MatcherJson FromMatcher(InputDeviceMatcher matcher)
			{
				if (matcher.empty)
				{
					return default(InputDeviceMatcher.MatcherJson);
				}
				InputDeviceMatcher.MatcherJson matcherJson = default(InputDeviceMatcher.MatcherJson);
				foreach (KeyValuePair<InternedString, object> keyValuePair in matcher.m_Patterns)
				{
					InternedString key = keyValuePair.Key;
					string text = keyValuePair.Value.ToString();
					if (key == InputDeviceMatcher.kInterfaceKey)
					{
						if (matcherJson.@interface == null)
						{
							matcherJson.@interface = text;
						}
						else
						{
							ArrayHelpers.Append<string>(ref matcherJson.interfaces, text);
						}
					}
					else if (key == InputDeviceMatcher.kDeviceClassKey)
					{
						if (matcherJson.deviceClass == null)
						{
							matcherJson.deviceClass = text;
						}
						else
						{
							ArrayHelpers.Append<string>(ref matcherJson.deviceClasses, text);
						}
					}
					else if (key == InputDeviceMatcher.kManufacturerKey)
					{
						if (matcherJson.manufacturer == null)
						{
							matcherJson.manufacturer = text;
						}
						else
						{
							ArrayHelpers.Append<string>(ref matcherJson.manufacturers, text);
						}
					}
					else if (key == InputDeviceMatcher.kProductKey)
					{
						if (matcherJson.product == null)
						{
							matcherJson.product = text;
						}
						else
						{
							ArrayHelpers.Append<string>(ref matcherJson.products, text);
						}
					}
					else if (key == InputDeviceMatcher.kVersionKey)
					{
						if (matcherJson.version == null)
						{
							matcherJson.version = text;
						}
						else
						{
							ArrayHelpers.Append<string>(ref matcherJson.versions, text);
						}
					}
					else
					{
						ArrayHelpers.Append<InputDeviceMatcher.MatcherJson.Capability>(ref matcherJson.capabilities, new InputDeviceMatcher.MatcherJson.Capability
						{
							path = key,
							value = text
						});
					}
				}
				return matcherJson;
			}

			// Token: 0x06001569 RID: 5481 RVA: 0x000624F4 File Offset: 0x000606F4
			public InputDeviceMatcher ToMatcher()
			{
				InputDeviceMatcher inputDeviceMatcher = default(InputDeviceMatcher);
				if (!string.IsNullOrEmpty(this.@interface))
				{
					inputDeviceMatcher = inputDeviceMatcher.WithInterface(this.@interface, true);
				}
				if (this.interfaces != null)
				{
					foreach (string text in this.interfaces)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithInterface(text, true);
					}
				}
				if (!string.IsNullOrEmpty(this.deviceClass))
				{
					inputDeviceMatcher = inputDeviceMatcher.WithDeviceClass(this.deviceClass, true);
				}
				if (this.deviceClasses != null)
				{
					foreach (string text2 in this.deviceClasses)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithDeviceClass(text2, true);
					}
				}
				if (!string.IsNullOrEmpty(this.manufacturer))
				{
					inputDeviceMatcher = inputDeviceMatcher.WithManufacturer(this.manufacturer, true);
				}
				if (this.manufacturers != null)
				{
					foreach (string text3 in this.manufacturers)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithManufacturer(text3, true);
					}
				}
				if (!string.IsNullOrEmpty(this.product))
				{
					inputDeviceMatcher = inputDeviceMatcher.WithProduct(this.product, true);
				}
				if (this.products != null)
				{
					foreach (string text4 in this.products)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithProduct(text4, true);
					}
				}
				if (!string.IsNullOrEmpty(this.version))
				{
					inputDeviceMatcher = inputDeviceMatcher.WithVersion(this.version, true);
				}
				if (this.versions != null)
				{
					foreach (string text5 in this.versions)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithVersion(text5, true);
					}
				}
				if (this.capabilities != null)
				{
					foreach (InputDeviceMatcher.MatcherJson.Capability capability in this.capabilities)
					{
						inputDeviceMatcher = inputDeviceMatcher.WithCapability<string>(capability.path, capability.value);
					}
				}
				return inputDeviceMatcher;
			}

			// Token: 0x04000BC5 RID: 3013
			public string @interface;

			// Token: 0x04000BC6 RID: 3014
			public string[] interfaces;

			// Token: 0x04000BC7 RID: 3015
			public string deviceClass;

			// Token: 0x04000BC8 RID: 3016
			public string[] deviceClasses;

			// Token: 0x04000BC9 RID: 3017
			public string manufacturer;

			// Token: 0x04000BCA RID: 3018
			public string[] manufacturers;

			// Token: 0x04000BCB RID: 3019
			public string product;

			// Token: 0x04000BCC RID: 3020
			public string[] products;

			// Token: 0x04000BCD RID: 3021
			public string version;

			// Token: 0x04000BCE RID: 3022
			public string[] versions;

			// Token: 0x04000BCF RID: 3023
			public InputDeviceMatcher.MatcherJson.Capability[] capabilities;

			// Token: 0x02000278 RID: 632
			public struct Capability
			{
				// Token: 0x04000CB7 RID: 3255
				public string path;

				// Token: 0x04000CB8 RID: 3256
				public string value;
			}
		}
	}
}
