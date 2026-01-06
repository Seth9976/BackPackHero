using System;
using System.Globalization;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000DD RID: 221
	internal class ES3JSONWriter : ES3Writer
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00022759 File Offset: 0x00020959
		public ES3JSONWriter(Stream stream, ES3Settings settings)
			: this(stream, settings, true, true)
		{
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00022765 File Offset: 0x00020965
		internal ES3JSONWriter(Stream stream, ES3Settings settings, bool writeHeaderAndFooter, bool mergeKeys)
			: base(settings, writeHeaderAndFooter, mergeKeys)
		{
			this.baseWriter = new StreamWriter(stream);
			this.StartWriteFile();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0002278A File Offset: 0x0002098A
		internal override void WritePrimitive(int value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00022798 File Offset: 0x00020998
		internal override void WritePrimitive(float value)
		{
			this.baseWriter.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000227B6 File Offset: 0x000209B6
		internal override void WritePrimitive(bool value)
		{
			this.baseWriter.Write(value ? "true" : "false");
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000227D2 File Offset: 0x000209D2
		internal override void WritePrimitive(decimal value)
		{
			this.baseWriter.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000227EB File Offset: 0x000209EB
		internal override void WritePrimitive(double value)
		{
			this.baseWriter.Write(value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00022809 File Offset: 0x00020A09
		internal override void WritePrimitive(long value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00022817 File Offset: 0x00020A17
		internal override void WritePrimitive(ulong value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00022825 File Offset: 0x00020A25
		internal override void WritePrimitive(uint value)
		{
			this.baseWriter.Write(value);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00022833 File Offset: 0x00020A33
		internal override void WritePrimitive(byte value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00022846 File Offset: 0x00020A46
		internal override void WritePrimitive(sbyte value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00022859 File Offset: 0x00020A59
		internal override void WritePrimitive(short value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002286C File Offset: 0x00020A6C
		internal override void WritePrimitive(ushort value)
		{
			this.baseWriter.Write(Convert.ToInt32(value));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002287F File Offset: 0x00020A7F
		internal override void WritePrimitive(char value)
		{
			this.WritePrimitive(value.ToString());
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002288E File Offset: 0x00020A8E
		internal override void WritePrimitive(byte[] value)
		{
			this.WritePrimitive(Convert.ToBase64String(value));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002289C File Offset: 0x00020A9C
		internal override void WritePrimitive(string value)
		{
			this.baseWriter.Write("\"");
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '/')
				{
					switch (c)
					{
					case '\b':
						this.baseWriter.Write("\\b");
						break;
					case '\t':
						this.baseWriter.Write("\\t");
						break;
					case '\n':
						this.baseWriter.Write("\\n");
						break;
					case '\v':
						goto IL_00DD;
					case '\f':
						this.baseWriter.Write("\\f");
						break;
					case '\r':
						this.baseWriter.Write("\\r");
						break;
					default:
						if (c != '"' && c != '/')
						{
							goto IL_00DD;
						}
						goto IL_0068;
					}
				}
				else
				{
					if (c == '\\' || c == '“' || c == '”')
					{
						goto IL_0068;
					}
					goto IL_00DD;
				}
				IL_00E9:
				i++;
				continue;
				IL_0068:
				this.baseWriter.Write('\\');
				this.baseWriter.Write(c);
				goto IL_00E9;
				IL_00DD:
				this.baseWriter.Write(c);
				goto IL_00E9;
			}
			this.baseWriter.Write("\"");
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000229B2 File Offset: 0x00020BB2
		internal override void WriteNull()
		{
			this.baseWriter.Write("null");
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000229C4 File Offset: 0x00020BC4
		private static bool CharacterRequiresEscaping(char c)
		{
			return c == '"' || c == '\\' || c == '“' || c == '”';
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000229E2 File Offset: 0x00020BE2
		private void WriteCommaIfRequired()
		{
			if (!this.isFirstProperty)
			{
				this.baseWriter.Write(',');
			}
			else
			{
				this.isFirstProperty = false;
			}
			this.WriteNewlineAndTabs();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00022A08 File Offset: 0x00020C08
		internal override void WriteRawProperty(string name, byte[] value)
		{
			this.StartWriteProperty(name);
			this.baseWriter.Write(this.settings.encoding.GetString(value, 0, value.Length));
			this.EndWriteProperty(name);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00022A38 File Offset: 0x00020C38
		internal override void StartWriteFile()
		{
			if (this.writeHeaderAndFooter)
			{
				this.baseWriter.Write('{');
			}
			base.StartWriteFile();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00022A55 File Offset: 0x00020C55
		internal override void EndWriteFile()
		{
			base.EndWriteFile();
			this.WriteNewlineAndTabs();
			if (this.writeHeaderAndFooter)
			{
				this.baseWriter.Write('}');
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00022A78 File Offset: 0x00020C78
		internal override void StartWriteProperty(string name)
		{
			base.StartWriteProperty(name);
			this.WriteCommaIfRequired();
			this.Write(name, ES3.ReferenceMode.ByRef);
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(' ');
			}
			this.baseWriter.Write(':');
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(' ');
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00022ADB File Offset: 0x00020CDB
		internal override void EndWriteProperty(string name)
		{
			base.EndWriteProperty(name);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00022AE4 File Offset: 0x00020CE4
		internal override void StartWriteObject(string name)
		{
			base.StartWriteObject(name);
			this.isFirstProperty = true;
			this.baseWriter.Write('{');
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00022B01 File Offset: 0x00020D01
		internal override void EndWriteObject(string name)
		{
			base.EndWriteObject(name);
			this.isFirstProperty = false;
			this.WriteNewlineAndTabs();
			this.baseWriter.Write('}');
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00022B24 File Offset: 0x00020D24
		internal override void StartWriteCollection()
		{
			base.StartWriteCollection();
			this.baseWriter.Write('[');
			this.WriteNewlineAndTabs();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00022B3F File Offset: 0x00020D3F
		internal override void EndWriteCollection()
		{
			base.EndWriteCollection();
			this.WriteNewlineAndTabs();
			this.baseWriter.Write(']');
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00022B5A File Offset: 0x00020D5A
		internal override void StartWriteCollectionItem(int index)
		{
			if (index != 0)
			{
				this.baseWriter.Write(',');
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00022B6C File Offset: 0x00020D6C
		internal override void EndWriteCollectionItem(int index)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00022B6E File Offset: 0x00020D6E
		internal override void StartWriteDictionary()
		{
			this.StartWriteObject(null);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00022B77 File Offset: 0x00020D77
		internal override void EndWriteDictionary()
		{
			this.EndWriteObject(null);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00022B80 File Offset: 0x00020D80
		internal override void StartWriteDictionaryKey(int index)
		{
			if (index != 0)
			{
				this.baseWriter.Write(',');
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00022B92 File Offset: 0x00020D92
		internal override void EndWriteDictionaryKey(int index)
		{
			this.baseWriter.Write(':');
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00022BA1 File Offset: 0x00020DA1
		internal override void StartWriteDictionaryValue(int index)
		{
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00022BA3 File Offset: 0x00020DA3
		internal override void EndWriteDictionaryValue(int index)
		{
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00022BA5 File Offset: 0x00020DA5
		public override void Dispose()
		{
			this.baseWriter.Dispose();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public void WriteNewlineAndTabs()
		{
			if (this.settings.prettyPrint)
			{
				this.baseWriter.Write(Environment.NewLine);
				for (int i = 0; i < this.serializationDepth; i++)
				{
					this.baseWriter.Write('\t');
				}
			}
		}

		// Token: 0x04000159 RID: 345
		internal StreamWriter baseWriter;

		// Token: 0x0400015A RID: 346
		private bool isFirstProperty = true;
	}
}
