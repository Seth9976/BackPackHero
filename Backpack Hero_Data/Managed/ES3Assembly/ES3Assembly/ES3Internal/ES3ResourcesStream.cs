using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D5 RID: 213
	internal class ES3ResourcesStream : MemoryStream
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x00021B8E File Offset: 0x0001FD8E
		public bool Exists
		{
			get
			{
				return this.Length > 0L;
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00021B9A File Offset: 0x0001FD9A
		public ES3ResourcesStream(string path)
			: base(ES3ResourcesStream.GetData(path))
		{
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00021BA8 File Offset: 0x0001FDA8
		private static byte[] GetData(string path)
		{
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			if (textAsset == null)
			{
				return new byte[0];
			}
			return textAsset.bytes;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00021BD7 File Offset: 0x0001FDD7
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
