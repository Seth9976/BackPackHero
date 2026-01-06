using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000130 RID: 304
	internal struct JsonParser
	{
		// Token: 0x060010C3 RID: 4291 RVA: 0x0004F891 File Offset: 0x0004DA91
		public JsonParser(string json)
		{
			this = default(JsonParser);
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			this.m_Text = json;
			this.m_Length = json.Length;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004F8BB File Offset: 0x0004DABB
		public void Reset()
		{
			this.m_Position = 0;
			this.m_MatchAnyElementInArray = false;
			this.m_DryRun = false;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004F8D4 File Offset: 0x0004DAD4
		public override string ToString()
		{
			if (this.m_Text != null)
			{
				return string.Format("{0}: {1}", this.m_Position, this.m_Text.Substring(this.m_Position));
			}
			return base.ToString();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0004F920 File Offset: 0x0004DB20
		public bool NavigateToProperty(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			int length = path.Length;
			int i = 0;
			this.m_DryRun = true;
			if (!this.ParseToken('{'))
			{
				return false;
			}
			while (this.m_Position < this.m_Length && i < length)
			{
				this.SkipWhitespace();
				if (this.m_Position == this.m_Length)
				{
					return false;
				}
				if (this.m_Text[this.m_Position] != '"')
				{
					return false;
				}
				this.m_Position++;
				int num = i;
				while (i < length)
				{
					char c = path[i];
					if (c == '/' || c == '[' || this.m_Text[this.m_Position] != c)
					{
						break;
					}
					this.m_Position++;
					i++;
				}
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '"' && (i >= length || path[i] == '/' || path[i] == '['))
				{
					this.m_Position++;
					if (!this.SkipToValue())
					{
						return false;
					}
					if (i >= length)
					{
						return true;
					}
					if (path[i] == '/')
					{
						i++;
						if (!this.ParseToken('{'))
						{
							return false;
						}
					}
					else if (path[i] == '[')
					{
						i++;
						if (i == length)
						{
							throw new ArgumentException("Malformed JSON property path: " + path, "path");
						}
						if (path[i] != ']')
						{
							throw new NotImplementedException("Navigating to specific array element");
						}
						this.m_MatchAnyElementInArray = true;
						i++;
						if (i == length)
						{
							return true;
						}
					}
				}
				else
				{
					i = num;
					while (this.m_Position < this.m_Length && this.m_Text[this.m_Position] != '"')
					{
						this.m_Position++;
					}
					if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '"')
					{
						return false;
					}
					this.m_Position++;
					if (!this.SkipToValue() || !this.ParseValue())
					{
						return false;
					}
					this.SkipWhitespace();
					if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] == '}' || this.m_Text[this.m_Position] != ',')
					{
						return false;
					}
					this.m_Position++;
				}
			}
			return false;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0004FB90 File Offset: 0x0004DD90
		public bool CurrentPropertyHasValueEqualTo(JsonParser.JsonValue expectedValue)
		{
			int position = this.m_Position;
			this.m_DryRun = false;
			JsonParser.JsonValue jsonValue;
			if (!this.ParseValue(out jsonValue))
			{
				this.m_Position = position;
				return false;
			}
			this.m_Position = position;
			bool flag = false;
			if (jsonValue.type == JsonParser.JsonValueType.Array && this.m_MatchAnyElementInArray)
			{
				List<JsonParser.JsonValue> arrayValue = jsonValue.arrayValue;
				int num = 0;
				while (!flag)
				{
					if (num >= arrayValue.Count)
					{
						break;
					}
					flag = arrayValue[num] == expectedValue;
					num++;
				}
			}
			else
			{
				flag = jsonValue == expectedValue;
			}
			return flag;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004FC14 File Offset: 0x0004DE14
		public bool ParseToken(char token)
		{
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			if (this.m_Text[this.m_Position] != token)
			{
				return false;
			}
			this.m_Position++;
			this.SkipWhitespace();
			return this.m_Position < this.m_Length;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0004FC70 File Offset: 0x0004DE70
		public bool ParseValue()
		{
			JsonParser.JsonValue jsonValue;
			return this.ParseValue(out jsonValue);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004FC88 File Offset: 0x0004DE88
		public bool ParseValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			char c = this.m_Text[this.m_Position];
			if (c <= 'f')
			{
				if (c != '"')
				{
					if (c != '[')
					{
						if (c != 'f')
						{
							goto IL_008D;
						}
					}
					else
					{
						if (this.ParseArrayValue(out result))
						{
							return true;
						}
						return false;
					}
				}
				else
				{
					if (this.ParseStringValue(out result))
					{
						return true;
					}
					return false;
				}
			}
			else if (c != 'n')
			{
				if (c != 't')
				{
					if (c != '{')
					{
						goto IL_008D;
					}
					if (this.ParseObjectValue(out result))
					{
						return true;
					}
					return false;
				}
			}
			else
			{
				if (this.ParseNullValue(out result))
				{
					return true;
				}
				return false;
			}
			if (this.ParseBooleanValue(out result))
			{
				return true;
			}
			return false;
			IL_008D:
			if (this.ParseNumber(out result))
			{
				return true;
			}
			return false;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0004FD30 File Offset: 0x0004DF30
		public bool ParseStringValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '"')
			{
				return false;
			}
			this.m_Position++;
			int position = this.m_Position;
			bool flag = false;
			while (this.m_Position < this.m_Length)
			{
				char c = this.m_Text[this.m_Position];
				if (c == '\\')
				{
					this.m_Position++;
					if (this.m_Position == this.m_Length)
					{
						break;
					}
					flag = true;
				}
				else if (c == '"')
				{
					this.m_Position++;
					result = new JsonParser.JsonString
					{
						text = new Substring(this.m_Text, position, this.m_Position - position - 1),
						hasEscapes = flag
					};
					return true;
				}
				this.m_Position++;
			}
			return false;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0004FE30 File Offset: 0x0004E030
		public bool ParseArrayValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '[')
			{
				return false;
			}
			this.m_Position++;
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			if (this.m_Text[this.m_Position] == ']')
			{
				result = new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Array
				};
				this.m_Position++;
				return true;
			}
			List<JsonParser.JsonValue> list = null;
			if (!this.m_DryRun)
			{
				list = new List<JsonParser.JsonValue>();
			}
			while (this.m_Position < this.m_Length)
			{
				JsonParser.JsonValue jsonValue;
				if (!this.ParseValue(out jsonValue))
				{
					return false;
				}
				if (!this.m_DryRun)
				{
					list.Add(jsonValue);
				}
				this.SkipWhitespace();
				if (this.m_Position == this.m_Length)
				{
					return false;
				}
				char c = this.m_Text[this.m_Position];
				if (c == ']')
				{
					this.m_Position++;
					if (!this.m_DryRun)
					{
						result = list;
					}
					return true;
				}
				if (c == ',')
				{
					this.m_Position++;
				}
			}
			return false;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0004FF70 File Offset: 0x0004E170
		public bool ParseObjectValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			if (!this.ParseToken('{'))
			{
				return false;
			}
			if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '}')
			{
				result = new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Object
				};
				this.m_Position++;
				return true;
			}
			while (this.m_Position < this.m_Length)
			{
				JsonParser.JsonValue jsonValue;
				if (!this.ParseStringValue(out jsonValue))
				{
					return false;
				}
				if (!this.SkipToValue())
				{
					return false;
				}
				JsonParser.JsonValue jsonValue2;
				if (!this.ParseValue(out jsonValue2))
				{
					return false;
				}
				if (!this.m_DryRun)
				{
					throw new NotImplementedException();
				}
				this.SkipWhitespace();
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '}')
				{
					if (!this.m_DryRun)
					{
						throw new NotImplementedException();
					}
					this.m_Position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0005006C File Offset: 0x0004E26C
		public bool ParseNumber(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			double num2 = 0.0;
			double num3 = 10.0;
			int num4 = 0;
			if (this.m_Text[this.m_Position] == '-')
			{
				flag = true;
				this.m_Position++;
			}
			if (this.m_Position == this.m_Length || !char.IsDigit(this.m_Text[this.m_Position]))
			{
				return false;
			}
			while (this.m_Position < this.m_Length)
			{
				char c = this.m_Text[this.m_Position];
				if (c == '.' || c < '0' || c > '9')
				{
					break;
				}
				num = num * 10L + (long)((ulong)c) - 48L;
				this.m_Position++;
			}
			if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '.')
			{
				flag2 = true;
				this.m_Position++;
				if (this.m_Position == this.m_Length || !char.IsDigit(this.m_Text[this.m_Position]))
				{
					return false;
				}
				while (this.m_Position < this.m_Length)
				{
					char c2 = this.m_Text[this.m_Position];
					if (c2 < '0' || c2 > '9')
					{
						break;
					}
					num2 = (double)(c2 - '0') / num3 + num2;
					num3 *= 10.0;
					this.m_Position++;
				}
			}
			if (this.m_Position < this.m_Length && (this.m_Text[this.m_Position] == 'e' || this.m_Text[this.m_Position] == 'E'))
			{
				this.m_Position++;
				bool flag3 = false;
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '-')
				{
					flag3 = true;
					this.m_Position++;
				}
				else if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '+')
				{
					this.m_Position++;
				}
				int num5 = 1;
				while (this.m_Position < this.m_Length && char.IsDigit(this.m_Text[this.m_Position]))
				{
					int num6 = (int)(this.m_Text[this.m_Position] - '0');
					num4 *= num5;
					num4 += num6;
					num5 *= 10;
					this.m_Position++;
				}
				if (flag3)
				{
					num4 *= -1;
				}
			}
			if (!this.m_DryRun)
			{
				if (!flag2 && num4 == 0)
				{
					if (flag)
					{
						result = -num;
					}
					else
					{
						result = num;
					}
				}
				else
				{
					float num7;
					if (flag)
					{
						num7 = (float)(-(float)((double)num + num2));
					}
					else
					{
						num7 = (float)((double)num + num2);
					}
					if (num4 != 0)
					{
						num7 *= Mathf.Pow(10f, (float)num4);
					}
					result = (double)num7;
				}
			}
			return true;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00050394 File Offset: 0x0004E594
		public bool ParseBooleanValue(out JsonParser.JsonValue result)
		{
			this.SkipWhitespace();
			if (this.SkipString("true"))
			{
				result = true;
				return true;
			}
			if (this.SkipString("false"))
			{
				result = false;
				return true;
			}
			result = default(JsonParser.JsonValue);
			return false;
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000503E5 File Offset: 0x0004E5E5
		public bool ParseNullValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			return this.SkipString("null");
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000503FC File Offset: 0x0004E5FC
		public bool SkipToValue()
		{
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != ':')
			{
				return false;
			}
			this.m_Position++;
			this.SkipWhitespace();
			return true;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0005044C File Offset: 0x0004E64C
		private bool SkipString(string text)
		{
			this.SkipWhitespace();
			int length = text.Length;
			if (this.m_Position + length >= this.m_Length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (this.m_Text[this.m_Position + i] != text[i])
				{
					return false;
				}
			}
			this.m_Position += length;
			return true;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000504B1 File Offset: 0x0004E6B1
		private void SkipWhitespace()
		{
			while (this.m_Position < this.m_Length && char.IsWhiteSpace(this.m_Text[this.m_Position]))
			{
				this.m_Position++;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000504E9 File Offset: 0x0004E6E9
		public bool isAtEnd
		{
			get
			{
				return this.m_Position >= this.m_Length;
			}
		}

		// Token: 0x040006BC RID: 1724
		private readonly string m_Text;

		// Token: 0x040006BD RID: 1725
		private readonly int m_Length;

		// Token: 0x040006BE RID: 1726
		private int m_Position;

		// Token: 0x040006BF RID: 1727
		private bool m_MatchAnyElementInArray;

		// Token: 0x040006C0 RID: 1728
		private bool m_DryRun;

		// Token: 0x02000239 RID: 569
		public enum JsonValueType
		{
			// Token: 0x04000BF8 RID: 3064
			None,
			// Token: 0x04000BF9 RID: 3065
			Bool,
			// Token: 0x04000BFA RID: 3066
			Real,
			// Token: 0x04000BFB RID: 3067
			Integer,
			// Token: 0x04000BFC RID: 3068
			String,
			// Token: 0x04000BFD RID: 3069
			Array,
			// Token: 0x04000BFE RID: 3070
			Object,
			// Token: 0x04000BFF RID: 3071
			Any
		}

		// Token: 0x0200023A RID: 570
		public struct JsonString : IEquatable<JsonParser.JsonString>
		{
			// Token: 0x0600158F RID: 5519 RVA: 0x00062B94 File Offset: 0x00060D94
			public override string ToString()
			{
				if (!this.hasEscapes)
				{
					return this.text.ToString();
				}
				StringBuilder stringBuilder = new StringBuilder();
				int length = this.text.length;
				for (int i = 0; i < length; i++)
				{
					char c = this.text[i];
					if (c == '\\')
					{
						i++;
						if (i == length)
						{
							break;
						}
						c = this.text[i];
					}
					stringBuilder.Append(c);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06001590 RID: 5520 RVA: 0x00062C10 File Offset: 0x00060E10
			public bool Equals(JsonParser.JsonString other)
			{
				if (this.hasEscapes == other.hasEscapes)
				{
					return Substring.Compare(this.text, other.text, StringComparison.InvariantCultureIgnoreCase) == 0;
				}
				int length = this.text.length;
				int length2 = other.text.length;
				int num = 0;
				int num2 = 0;
				while (num < length && num2 < length2)
				{
					char c = this.text[num];
					char c2 = other.text[num2];
					if (c == '\\')
					{
						num++;
						if (num == length)
						{
							return false;
						}
						c = this.text[num];
					}
					if (c2 == '\\')
					{
						num2++;
						if (num2 == length2)
						{
							return false;
						}
						c2 = other.text[num2];
					}
					if (char.ToUpperInvariant(c) != char.ToUpperInvariant(c2))
					{
						return false;
					}
					num++;
					num2++;
				}
				return num == length && num2 == length2;
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x00062CE8 File Offset: 0x00060EE8
			public override bool Equals(object obj)
			{
				if (obj is JsonParser.JsonString)
				{
					JsonParser.JsonString jsonString = (JsonParser.JsonString)obj;
					return this.Equals(jsonString);
				}
				return false;
			}

			// Token: 0x06001592 RID: 5522 RVA: 0x00062D0D File Offset: 0x00060F0D
			public override int GetHashCode()
			{
				return (this.text.GetHashCode() * 397) ^ this.hasEscapes.GetHashCode();
			}

			// Token: 0x06001593 RID: 5523 RVA: 0x00062D32 File Offset: 0x00060F32
			public static bool operator ==(JsonParser.JsonString left, JsonParser.JsonString right)
			{
				return left.Equals(right);
			}

			// Token: 0x06001594 RID: 5524 RVA: 0x00062D3C File Offset: 0x00060F3C
			public static bool operator !=(JsonParser.JsonString left, JsonParser.JsonString right)
			{
				return !left.Equals(right);
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00062D4C File Offset: 0x00060F4C
			public static implicit operator JsonParser.JsonString(string str)
			{
				return new JsonParser.JsonString
				{
					text = str
				};
			}

			// Token: 0x04000C00 RID: 3072
			public Substring text;

			// Token: 0x04000C01 RID: 3073
			public bool hasEscapes;
		}

		// Token: 0x0200023B RID: 571
		public struct JsonValue : IEquatable<JsonParser.JsonValue>
		{
			// Token: 0x06001596 RID: 5526 RVA: 0x00062D70 File Offset: 0x00060F70
			public bool ToBoolean()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return this.boolValue;
				case JsonParser.JsonValueType.Real:
					return NumberHelpers.Approximately(0.0, this.realValue);
				case JsonParser.JsonValueType.Integer:
					return this.integerValue != 0L;
				case JsonParser.JsonValueType.String:
					return Convert.ToBoolean(this.ToString());
				default:
					return false;
				}
			}

			// Token: 0x06001597 RID: 5527 RVA: 0x00062DD8 File Offset: 0x00060FD8
			public long ToInteger()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return this.boolValue ? 1L : 0L;
				case JsonParser.JsonValueType.Real:
					return (long)this.realValue;
				case JsonParser.JsonValueType.Integer:
					return this.integerValue;
				case JsonParser.JsonValueType.String:
					return Convert.ToInt64(this.ToString());
				default:
					return 0L;
				}
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x00062E38 File Offset: 0x00061038
			public double ToDouble()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return (double)(this.boolValue ? 1 : 0);
				case JsonParser.JsonValueType.Real:
					return this.realValue;
				case JsonParser.JsonValueType.Integer:
					return (double)this.integerValue;
				case JsonParser.JsonValueType.String:
					return (double)Convert.ToSingle(this.ToString());
				default:
					return 0.0;
				}
			}

			// Token: 0x06001599 RID: 5529 RVA: 0x00062EA0 File Offset: 0x000610A0
			public override string ToString()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.None:
					return "null";
				case JsonParser.JsonValueType.Bool:
					return this.boolValue.ToString();
				case JsonParser.JsonValueType.Real:
					return this.realValue.ToString(CultureInfo.InvariantCulture);
				case JsonParser.JsonValueType.Integer:
					return this.integerValue.ToString(CultureInfo.InvariantCulture);
				case JsonParser.JsonValueType.String:
					return this.stringValue.ToString();
				case JsonParser.JsonValueType.Array:
					if (this.arrayValue == null)
					{
						return "[]";
					}
					return "[" + string.Join(",", this.arrayValue.Select((JsonParser.JsonValue x) => x.ToString())) + "]";
				case JsonParser.JsonValueType.Object:
				{
					if (this.objectValue == null)
					{
						return "{}";
					}
					IEnumerable<string> enumerable = this.objectValue.Select((KeyValuePair<string, JsonParser.JsonValue> pair) => string.Format("\"{0}\" : \"{1}\"", pair.Key, pair.Value));
					return "{" + string.Join(",", enumerable) + "}";
				}
				case JsonParser.JsonValueType.Any:
					return this.anyValue.ToString();
				default:
					return base.ToString();
				}
			}

			// Token: 0x0600159A RID: 5530 RVA: 0x00062FE8 File Offset: 0x000611E8
			public static implicit operator JsonParser.JsonValue(bool val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Bool,
					boolValue = val
				};
			}

			// Token: 0x0600159B RID: 5531 RVA: 0x00063010 File Offset: 0x00061210
			public static implicit operator JsonParser.JsonValue(long val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Integer,
					integerValue = val
				};
			}

			// Token: 0x0600159C RID: 5532 RVA: 0x00063038 File Offset: 0x00061238
			public static implicit operator JsonParser.JsonValue(double val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Real,
					realValue = val
				};
			}

			// Token: 0x0600159D RID: 5533 RVA: 0x00063060 File Offset: 0x00061260
			public static implicit operator JsonParser.JsonValue(string str)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.String,
					stringValue = new JsonParser.JsonString
					{
						text = str
					}
				};
			}

			// Token: 0x0600159E RID: 5534 RVA: 0x0006309C File Offset: 0x0006129C
			public static implicit operator JsonParser.JsonValue(JsonParser.JsonString str)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.String,
					stringValue = str
				};
			}

			// Token: 0x0600159F RID: 5535 RVA: 0x000630C4 File Offset: 0x000612C4
			public static implicit operator JsonParser.JsonValue(List<JsonParser.JsonValue> array)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Array,
					arrayValue = array
				};
			}

			// Token: 0x060015A0 RID: 5536 RVA: 0x000630EC File Offset: 0x000612EC
			public static implicit operator JsonParser.JsonValue(Dictionary<string, JsonParser.JsonValue> obj)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Object,
					objectValue = obj
				};
			}

			// Token: 0x060015A1 RID: 5537 RVA: 0x00063114 File Offset: 0x00061314
			public static implicit operator JsonParser.JsonValue(Enum val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Any,
					anyValue = val
				};
			}

			// Token: 0x060015A2 RID: 5538 RVA: 0x0006313C File Offset: 0x0006133C
			public bool Equals(JsonParser.JsonValue other)
			{
				if (this.type == other.type)
				{
					switch (this.type)
					{
					case JsonParser.JsonValueType.None:
						return true;
					case JsonParser.JsonValueType.Bool:
						return this.boolValue == other.boolValue;
					case JsonParser.JsonValueType.Real:
						return NumberHelpers.Approximately(this.realValue, other.realValue);
					case JsonParser.JsonValueType.Integer:
						return this.integerValue == other.integerValue;
					case JsonParser.JsonValueType.String:
						return this.stringValue == other.stringValue;
					case JsonParser.JsonValueType.Array:
						throw new NotImplementedException();
					case JsonParser.JsonValueType.Object:
						throw new NotImplementedException();
					case JsonParser.JsonValueType.Any:
						return this.anyValue.Equals(other.anyValue);
					default:
						return false;
					}
				}
				else
				{
					if (this.anyValue != null)
					{
						return JsonParser.JsonValue.Equals(this.anyValue, other);
					}
					return other.anyValue != null && JsonParser.JsonValue.Equals(other.anyValue, this);
				}
			}

			// Token: 0x060015A3 RID: 5539 RVA: 0x00063220 File Offset: 0x00061420
			private static bool Equals(object obj, JsonParser.JsonValue value)
			{
				if (obj == null)
				{
					return false;
				}
				Regex regex = obj as Regex;
				if (regex != null)
				{
					return regex.IsMatch(value.ToString());
				}
				string text = obj as string;
				if (text != null)
				{
					switch (value.type)
					{
					case JsonParser.JsonValueType.Bool:
						if (value.boolValue)
						{
							return text == "True" || text == "true" || text == "1";
						}
						return text == "False" || text == "false" || text == "0";
					case JsonParser.JsonValueType.Real:
					{
						double num;
						return double.TryParse(text, out num) && NumberHelpers.Approximately(num, value.realValue);
					}
					case JsonParser.JsonValueType.Integer:
					{
						long num2;
						return long.TryParse(text, out num2) && num2 == value.integerValue;
					}
					case JsonParser.JsonValueType.String:
						return value.stringValue == text;
					}
				}
				if (obj is float)
				{
					float num3 = (float)obj;
					if (value.type == JsonParser.JsonValueType.Real)
					{
						return NumberHelpers.Approximately((double)num3, value.realValue);
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						float num4;
						return float.TryParse(value.ToString(), out num4) && Mathf.Approximately(num3, num4);
					}
				}
				if (obj is double)
				{
					double num5 = (double)obj;
					if (value.type == JsonParser.JsonValueType.Real)
					{
						return NumberHelpers.Approximately(num5, value.realValue);
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						double num6;
						return double.TryParse(value.ToString(), out num6) && NumberHelpers.Approximately(num5, num6);
					}
				}
				if (obj is int)
				{
					int num7 = (int)obj;
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return (long)num7 == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						int num8;
						return int.TryParse(value.ToString(), out num8) && num7 == num8;
					}
				}
				if (obj is long)
				{
					long num9 = (long)obj;
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return num9 == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						long num10;
						return long.TryParse(value.ToString(), out num10) && num9 == num10;
					}
				}
				if (obj is bool)
				{
					bool flag = (bool)obj;
					if (value.type == JsonParser.JsonValueType.Bool)
					{
						return flag == value.boolValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						if (flag)
						{
							return value.stringValue == "true" || value.stringValue == "True" || value.stringValue == "1";
						}
						return value.stringValue == "false" || value.stringValue == "False" || value.stringValue == "0";
					}
				}
				if (obj is Enum)
				{
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return Convert.ToInt64(obj) == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						return value.stringValue == Enum.GetName(obj.GetType(), obj);
					}
				}
				return false;
			}

			// Token: 0x060015A4 RID: 5540 RVA: 0x00063560 File Offset: 0x00061760
			public override bool Equals(object obj)
			{
				if (obj is JsonParser.JsonValue)
				{
					JsonParser.JsonValue jsonValue = (JsonParser.JsonValue)obj;
					return this.Equals(jsonValue);
				}
				return false;
			}

			// Token: 0x060015A5 RID: 5541 RVA: 0x00063588 File Offset: 0x00061788
			public override int GetHashCode()
			{
				return (int)((((((((((((((this.type * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.boolValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.realValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.integerValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.stringValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.arrayValue != null) ? this.arrayValue.GetHashCode() : 0)) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.objectValue != null) ? this.objectValue.GetHashCode() : 0)) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.anyValue != null) ? this.anyValue.GetHashCode() : 0));
			}

			// Token: 0x060015A6 RID: 5542 RVA: 0x00063640 File Offset: 0x00061840
			public static bool operator ==(JsonParser.JsonValue left, JsonParser.JsonValue right)
			{
				return left.Equals(right);
			}

			// Token: 0x060015A7 RID: 5543 RVA: 0x0006364A File Offset: 0x0006184A
			public static bool operator !=(JsonParser.JsonValue left, JsonParser.JsonValue right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04000C02 RID: 3074
			public JsonParser.JsonValueType type;

			// Token: 0x04000C03 RID: 3075
			public bool boolValue;

			// Token: 0x04000C04 RID: 3076
			public double realValue;

			// Token: 0x04000C05 RID: 3077
			public long integerValue;

			// Token: 0x04000C06 RID: 3078
			public JsonParser.JsonString stringValue;

			// Token: 0x04000C07 RID: 3079
			public List<JsonParser.JsonValue> arrayValue;

			// Token: 0x04000C08 RID: 3080
			public Dictionary<string, JsonParser.JsonValue> objectValue;

			// Token: 0x04000C09 RID: 3081
			public object anyValue;
		}
	}
}
