using System;
using System.ComponentModel;

namespace ES3Internal
{
	// Token: 0x020000DC RID: 220
	internal class ES3CacheWriter : ES3Writer
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x000226C1 File Offset: 0x000208C1
		internal ES3CacheWriter(ES3Settings settings, bool writeHeaderAndFooter, bool mergeKeys)
			: base(settings, writeHeaderAndFooter, mergeKeys)
		{
			this.es3File = new ES3File(settings);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000226D8 File Offset: 0x000208D8
		public override void Write<T>(string key, object value)
		{
			this.es3File.Save<T>(key, (T)((object)value));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000226EC File Offset: 0x000208EC
		internal override void Write(string key, Type type, byte[] value)
		{
			ES3Debug.LogError("Not implemented", null, 0);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000226FA File Offset: 0x000208FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Write(Type type, string key, object value)
		{
			this.es3File.Save<object>(key, value);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00022709 File Offset: 0x00020909
		internal override void WritePrimitive(int value)
		{
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0002270B File Offset: 0x0002090B
		internal override void WritePrimitive(float value)
		{
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0002270D File Offset: 0x0002090D
		internal override void WritePrimitive(bool value)
		{
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0002270F File Offset: 0x0002090F
		internal override void WritePrimitive(decimal value)
		{
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00022711 File Offset: 0x00020911
		internal override void WritePrimitive(double value)
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00022713 File Offset: 0x00020913
		internal override void WritePrimitive(long value)
		{
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00022715 File Offset: 0x00020915
		internal override void WritePrimitive(ulong value)
		{
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00022717 File Offset: 0x00020917
		internal override void WritePrimitive(uint value)
		{
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00022719 File Offset: 0x00020919
		internal override void WritePrimitive(byte value)
		{
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0002271B File Offset: 0x0002091B
		internal override void WritePrimitive(sbyte value)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0002271D File Offset: 0x0002091D
		internal override void WritePrimitive(short value)
		{
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0002271F File Offset: 0x0002091F
		internal override void WritePrimitive(ushort value)
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00022721 File Offset: 0x00020921
		internal override void WritePrimitive(char value)
		{
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00022723 File Offset: 0x00020923
		internal override void WritePrimitive(byte[] value)
		{
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00022725 File Offset: 0x00020925
		internal override void WritePrimitive(string value)
		{
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00022727 File Offset: 0x00020927
		internal override void WriteNull()
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00022729 File Offset: 0x00020929
		private static bool CharacterRequiresEscaping(char c)
		{
			return false;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0002272C File Offset: 0x0002092C
		private void WriteCommaIfRequired()
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0002272E File Offset: 0x0002092E
		internal override void WriteRawProperty(string name, byte[] value)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00022730 File Offset: 0x00020930
		internal override void StartWriteFile()
		{
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00022732 File Offset: 0x00020932
		internal override void EndWriteFile()
		{
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00022734 File Offset: 0x00020934
		internal override void StartWriteProperty(string name)
		{
			base.StartWriteProperty(name);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0002273D File Offset: 0x0002093D
		internal override void EndWriteProperty(string name)
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002273F File Offset: 0x0002093F
		internal override void StartWriteObject(string name)
		{
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00022741 File Offset: 0x00020941
		internal override void EndWriteObject(string name)
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00022743 File Offset: 0x00020943
		internal override void StartWriteCollection()
		{
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00022745 File Offset: 0x00020945
		internal override void EndWriteCollection()
		{
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00022747 File Offset: 0x00020947
		internal override void StartWriteCollectionItem(int index)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00022749 File Offset: 0x00020949
		internal override void EndWriteCollectionItem(int index)
		{
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0002274B File Offset: 0x0002094B
		internal override void StartWriteDictionary()
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0002274D File Offset: 0x0002094D
		internal override void EndWriteDictionary()
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0002274F File Offset: 0x0002094F
		internal override void StartWriteDictionaryKey(int index)
		{
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00022751 File Offset: 0x00020951
		internal override void EndWriteDictionaryKey(int index)
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00022753 File Offset: 0x00020953
		internal override void StartWriteDictionaryValue(int index)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00022755 File Offset: 0x00020955
		internal override void EndWriteDictionaryValue(int index)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00022757 File Offset: 0x00020957
		public override void Dispose()
		{
		}

		// Token: 0x04000158 RID: 344
		private ES3File es3File;
	}
}
