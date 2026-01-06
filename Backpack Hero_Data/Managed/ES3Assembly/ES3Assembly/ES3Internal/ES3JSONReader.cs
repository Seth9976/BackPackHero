using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace ES3Internal
{
	// Token: 0x020000CF RID: 207
	public class ES3JSONReader : ES3Reader
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00021018 File Offset: 0x0001F218
		internal ES3JSONReader(Stream stream, ES3Settings settings, bool readHeaderAndFooter = true)
			: base(settings, readHeaderAndFooter)
		{
			this.baseReader = new StreamReader(stream);
			if (readHeaderAndFooter)
			{
				try
				{
					this.SkipOpeningBraceOfFile();
				}
				catch
				{
					this.Dispose();
					throw new FormatException("Cannot load from file because the data in it is not JSON data, or the data is encrypted.\nIf the save data is encrypted, please ensure that encryption is enabled when you load, and that you are using the same password used to encrypt the data.");
				}
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00021068 File Offset: 0x0001F268
		public override string ReadPropertyName()
		{
			char c = this.PeekCharIgnoreWhitespace();
			if (ES3JSONReader.IsTerminator(c))
			{
				return null;
			}
			if (c == ',')
			{
				this.ReadCharIgnoreWhitespace(true);
			}
			else if (!ES3JSONReader.IsQuotationMark(c))
			{
				throw new FormatException("Expected ',' separating properties or '\"' before property name, found '" + c.ToString() + "'.");
			}
			string text = this.Read_string();
			if (text == null)
			{
				throw new FormatException("Stream isn't positioned before a property.");
			}
			ES3Debug.Log("<b>" + text + "</b> (reading property)", null, this.serializationDepth);
			this.ReadCharIgnoreWhitespace(':');
			return text;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000210F4 File Offset: 0x0001F2F4
		protected override Type ReadKeyPrefix(bool ignoreType = false)
		{
			this.StartReadObject();
			Type type = null;
			string text = this.ReadPropertyName();
			if (text == "__type")
			{
				string text2 = this.Read_string();
				type = (ignoreType ? null : ES3Reflection.GetType(text2));
				text = this.ReadPropertyName();
			}
			if (text != "value")
			{
				throw new FormatException("This data is not Easy Save Key Value data. Expected property name \"value\", found \"" + text + "\".");
			}
			return type;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0002115D File Offset: 0x0001F35D
		protected override void ReadKeySuffix()
		{
			this.EndReadObject();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00021165 File Offset: 0x0001F365
		internal override bool StartReadObject()
		{
			base.StartReadObject();
			return this.ReadNullOrCharIgnoreWhitespace('{');
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00021176 File Offset: 0x0001F376
		internal override void EndReadObject()
		{
			this.ReadCharIgnoreWhitespace('}');
			base.EndReadObject();
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00021187 File Offset: 0x0001F387
		internal override bool StartReadDictionary()
		{
			return this.StartReadObject();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0002118F File Offset: 0x0001F38F
		internal override void EndReadDictionary()
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00021191 File Offset: 0x0001F391
		internal override bool StartReadDictionaryKey()
		{
			if (this.PeekCharIgnoreWhitespace() == '}')
			{
				this.ReadCharIgnoreWhitespace(true);
				return false;
			}
			return true;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000211A8 File Offset: 0x0001F3A8
		internal override void EndReadDictionaryKey()
		{
			this.ReadCharIgnoreWhitespace(':');
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000211B3 File Offset: 0x0001F3B3
		internal override void StartReadDictionaryValue()
		{
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000211B8 File Offset: 0x0001F3B8
		internal override bool EndReadDictionaryValue()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == '}')
			{
				return true;
			}
			if (c != ',')
			{
				throw new FormatException("Expected ',' seperating Dictionary items or '}' terminating Dictionary, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000211F6 File Offset: 0x0001F3F6
		internal override bool StartReadCollection()
		{
			return this.ReadNullOrCharIgnoreWhitespace('[');
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00021200 File Offset: 0x0001F400
		internal override void EndReadCollection()
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00021202 File Offset: 0x0001F402
		internal override bool StartReadCollectionItem()
		{
			if (this.PeekCharIgnoreWhitespace() == ']')
			{
				this.ReadCharIgnoreWhitespace(true);
				return false;
			}
			return true;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0002121C File Offset: 0x0001F41C
		internal override bool EndReadCollectionItem()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == ']')
			{
				return true;
			}
			if (c != ',')
			{
				throw new FormatException("Expected ',' seperating collection items or ']' terminating collection, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0002125C File Offset: 0x0001F45C
		private void ReadString(StreamWriter writer, bool skip = false)
		{
			bool flag = false;
			while (!flag)
			{
				char c = this.ReadOrSkipChar(writer, skip);
				if (c != '\\')
				{
					if (c == '\uffff')
					{
						throw new FormatException("String without closing quotation mark detected.");
					}
					if (ES3JSONReader.IsQuotationMark(c))
					{
						flag = true;
					}
				}
				else
				{
					this.ReadOrSkipChar(writer, skip);
				}
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000212A8 File Offset: 0x0001F4A8
		internal override byte[] ReadElement(bool skip = false)
		{
			StreamWriter streamWriter = (skip ? null : new StreamWriter(new MemoryStream(this.settings.bufferSize)));
			byte[] array;
			using (streamWriter)
			{
				int num = 0;
				char c = (char)this.baseReader.Peek();
				if (!ES3JSONReader.IsOpeningBrace(c))
				{
					if (c == '"')
					{
						this.ReadOrSkipChar(streamWriter, skip);
						this.ReadString(streamWriter, skip);
					}
					else
					{
						while (!ES3JSONReader.IsEndOfValue((char)this.baseReader.Peek()))
						{
							this.ReadOrSkipChar(streamWriter, skip);
						}
					}
					if (skip)
					{
						array = null;
					}
					else
					{
						streamWriter.Flush();
						array = ((MemoryStream)streamWriter.BaseStream).ToArray();
					}
				}
				else
				{
					for (;;)
					{
						c = this.ReadOrSkipChar(streamWriter, skip);
						if (c == '\uffff')
						{
							break;
						}
						if (ES3JSONReader.IsQuotationMark(c))
						{
							this.ReadString(streamWriter, skip);
						}
						else
						{
							if (c <= ']')
							{
								if (c != '[')
								{
									if (c != ']')
									{
										continue;
									}
									goto IL_00E2;
								}
							}
							else if (c != '{')
							{
								if (c != '}')
								{
									continue;
								}
								goto IL_00E2;
							}
							num++;
							continue;
							IL_00E2:
							num--;
							if (num < 1)
							{
								goto Block_14;
							}
						}
					}
					throw new FormatException("Missing closing brace detected, as end of stream was reached before finding it.");
					Block_14:
					if (skip)
					{
						array = null;
					}
					else
					{
						streamWriter.Flush();
						array = ((MemoryStream)streamWriter.BaseStream).ToArray();
					}
				}
			}
			return array;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000213E0 File Offset: 0x0001F5E0
		private char ReadOrSkipChar(StreamWriter writer, bool skip)
		{
			char c = (char)this.baseReader.Read();
			if (!skip)
			{
				writer.Write(c);
			}
			return c;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00021408 File Offset: 0x0001F608
		private char ReadCharIgnoreWhitespace(bool ignoreTrailingWhitespace = true)
		{
			char c;
			while (ES3JSONReader.IsWhiteSpace(c = (char)this.baseReader.Read()))
			{
			}
			if (ignoreTrailingWhitespace)
			{
				while (ES3JSONReader.IsWhiteSpace((char)this.baseReader.Peek()))
				{
					this.baseReader.Read();
				}
			}
			return c;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00021450 File Offset: 0x0001F650
		private bool ReadNullOrCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == 'n')
			{
				char[] array = new char[3];
				this.baseReader.ReadBlock(array, 0, 3);
				if (array[0] == 'u' && array[1] == 'l' && array[2] == 'l')
				{
					return true;
				}
			}
			if (c == expectedChar)
			{
				return false;
			}
			if (c == '\uffff')
			{
				throw new FormatException("End of stream reached when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"' or \"null\", found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00021500 File Offset: 0x0001F700
		private char ReadCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c == expectedChar)
			{
				return c;
			}
			if (c == '\uffff')
			{
				throw new FormatException("End of stream reached when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"', found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0002157C File Offset: 0x0001F77C
		private bool ReadQuotationMarkOrNullIgnoreWhitespace()
		{
			char c = this.ReadCharIgnoreWhitespace(false);
			if (c == 'n')
			{
				char[] array = new char[3];
				this.baseReader.ReadBlock(array, 0, 3);
				if (array[0] == 'u' && array[1] == 'l' && array[2] == 'l')
				{
					return true;
				}
			}
			else if (!ES3JSONReader.IsQuotationMark(c))
			{
				if (c == '\uffff')
				{
					throw new FormatException("End of stream reached when expecting quotation mark.");
				}
				throw new FormatException("Expected quotation mark, found '" + c.ToString() + "'.");
			}
			return false;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000215FC File Offset: 0x0001F7FC
		private char PeekCharIgnoreWhitespace(char expectedChar)
		{
			char c = this.PeekCharIgnoreWhitespace();
			if (c == expectedChar)
			{
				return c;
			}
			if (c == '\uffff')
			{
				throw new FormatException("End of stream reached while peeking, when expecting '" + expectedChar.ToString() + "'.");
			}
			throw new FormatException(string.Concat(new string[]
			{
				"Expected '",
				expectedChar.ToString(),
				"', found '",
				c.ToString(),
				"'."
			}));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00021678 File Offset: 0x0001F878
		private char PeekCharIgnoreWhitespace()
		{
			char c;
			while (ES3JSONReader.IsWhiteSpace(c = (char)this.baseReader.Peek()))
			{
				this.baseReader.Read();
			}
			return c;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000216A9 File Offset: 0x0001F8A9
		private void SkipWhiteSpace()
		{
			while (ES3JSONReader.IsWhiteSpace((char)this.baseReader.Peek()))
			{
				this.baseReader.Read();
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000216CC File Offset: 0x0001F8CC
		private void SkipOpeningBraceOfFile()
		{
			char c = this.ReadCharIgnoreWhitespace(true);
			if (c != '{')
			{
				throw new FormatException("File is not valid JSON. Expected '{' at beginning of file, but found '" + c.ToString() + "'.");
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00021702 File Offset: 0x0001F902
		private static bool IsWhiteSpace(char c)
		{
			return c == ' ' || c == '\t' || c == '\n' || c == '\r';
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0002171A File Offset: 0x0001F91A
		private static bool IsOpeningBrace(char c)
		{
			return c == '{' || c == '[';
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00021728 File Offset: 0x0001F928
		private static bool IsEndOfValue(char c)
		{
			return c == '}' || c == ' ' || c == '\t' || c == ']' || c == ',' || c == ':' || c == char.MaxValue || c == '\n' || c == '\r';
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0002175C File Offset: 0x0001F95C
		private static bool IsTerminator(char c)
		{
			return c == '}' || c == ']';
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0002176A File Offset: 0x0001F96A
		private static bool IsQuotationMark(char c)
		{
			return c == '"' || c == '“' || c == '”';
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00021783 File Offset: 0x0001F983
		private static bool IsEndOfStream(char c)
		{
			return c == char.MaxValue;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00021790 File Offset: 0x0001F990
		private string GetValueString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (!ES3JSONReader.IsEndOfValue(this.PeekCharIgnoreWhitespace()))
			{
				stringBuilder.Append((char)this.baseReader.Read());
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000217D8 File Offset: 0x0001F9D8
		internal override string Read_string()
		{
			if (this.ReadQuotationMarkOrNullIgnoreWhitespace())
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			char c;
			while (!ES3JSONReader.IsQuotationMark(c = (char)this.baseReader.Read()))
			{
				if (c == '\\')
				{
					c = (char)this.baseReader.Read();
					if (ES3JSONReader.IsEndOfStream(c))
					{
						throw new FormatException("Reached end of stream while trying to read string literal.");
					}
					if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c == 'f')
							{
								c = '\f';
							}
						}
						else
						{
							c = '\b';
						}
					}
					else if (c != 'n')
					{
						if (c != 'r')
						{
							if (c == 't')
							{
								c = '\t';
							}
						}
						else
						{
							c = '\r';
						}
					}
					else
					{
						c = '\n';
					}
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00021877 File Offset: 0x0001FA77
		internal override long Read_ref()
		{
			if (ES3ReferenceMgrBase.Current == null)
			{
				throw new InvalidOperationException("An Easy Save 3 Manager is required to load references. To add one to your scene, exit playmode and go to Assets > Easy Save 3 > Add Manager to Scene");
			}
			if (ES3JSONReader.IsQuotationMark(this.PeekCharIgnoreWhitespace()))
			{
				return long.Parse(this.Read_string());
			}
			return this.Read_long();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000218B0 File Offset: 0x0001FAB0
		internal override char Read_char()
		{
			return char.Parse(this.Read_string());
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000218BD File Offset: 0x0001FABD
		internal override float Read_float()
		{
			return float.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000218CF File Offset: 0x0001FACF
		internal override int Read_int()
		{
			return int.Parse(this.GetValueString());
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000218DC File Offset: 0x0001FADC
		internal override bool Read_bool()
		{
			return bool.Parse(this.GetValueString());
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x000218E9 File Offset: 0x0001FAE9
		internal override decimal Read_decimal()
		{
			return decimal.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000218FB File Offset: 0x0001FAFB
		internal override double Read_double()
		{
			return double.Parse(this.GetValueString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002190D File Offset: 0x0001FB0D
		internal override long Read_long()
		{
			return long.Parse(this.GetValueString());
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0002191A File Offset: 0x0001FB1A
		internal override ulong Read_ulong()
		{
			return ulong.Parse(this.GetValueString());
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00021927 File Offset: 0x0001FB27
		internal override uint Read_uint()
		{
			return uint.Parse(this.GetValueString());
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00021934 File Offset: 0x0001FB34
		internal override byte Read_byte()
		{
			return (byte)int.Parse(this.GetValueString());
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00021942 File Offset: 0x0001FB42
		internal override sbyte Read_sbyte()
		{
			return (sbyte)int.Parse(this.GetValueString());
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00021950 File Offset: 0x0001FB50
		internal override short Read_short()
		{
			return (short)int.Parse(this.GetValueString());
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0002195E File Offset: 0x0001FB5E
		internal override ushort Read_ushort()
		{
			return (ushort)int.Parse(this.GetValueString());
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0002196C File Offset: 0x0001FB6C
		internal override byte[] Read_byteArray()
		{
			return Convert.FromBase64String(this.Read_string());
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00021979 File Offset: 0x0001FB79
		public override void Dispose()
		{
			this.baseReader.Dispose();
		}

		// Token: 0x04000123 RID: 291
		private const char endOfStreamChar = '\uffff';

		// Token: 0x04000124 RID: 292
		public StreamReader baseReader;
	}
}
