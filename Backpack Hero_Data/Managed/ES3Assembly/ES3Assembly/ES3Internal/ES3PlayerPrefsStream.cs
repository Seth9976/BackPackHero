using System;
using System.IO;
using UnityEngine;

namespace ES3Internal
{
	// Token: 0x020000D4 RID: 212
	internal class ES3PlayerPrefsStream : MemoryStream
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x00021A42 File Offset: 0x0001FC42
		public ES3PlayerPrefsStream(string path)
			: base(ES3PlayerPrefsStream.GetData(path, false))
		{
			this.path = path;
			this.append = false;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00021A5F File Offset: 0x0001FC5F
		public ES3PlayerPrefsStream(string path, int bufferSize, bool append = false)
			: base(bufferSize)
		{
			this.path = path;
			this.append = append;
			this.isWriteStream = true;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00021A7D File Offset: 0x0001FC7D
		private static byte[] GetData(string path, bool isWriteStream)
		{
			if (!PlayerPrefs.HasKey(path))
			{
				throw new FileNotFoundException("File \"" + path + "\" could not be found in PlayerPrefs");
			}
			return Convert.FromBase64String(PlayerPrefs.GetString(path));
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		protected override void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.isDisposed = true;
			if (this.isWriteStream && this.Length > 0L)
			{
				if (this.append)
				{
					byte[] array = Convert.FromBase64String(PlayerPrefs.GetString(this.path));
					byte[] array2 = this.ToArray();
					byte[] array3 = new byte[array.Length + array2.Length];
					Buffer.BlockCopy(array, 0, array3, 0, array.Length);
					Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
					PlayerPrefs.SetString(this.path, Convert.ToBase64String(array3));
					PlayerPrefs.Save();
				}
				else
				{
					PlayerPrefs.SetString(this.path + ".tmp", Convert.ToBase64String(this.ToArray()));
				}
				PlayerPrefs.SetString("timestamp_" + this.path, DateTime.UtcNow.Ticks.ToString());
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400012C RID: 300
		private string path;

		// Token: 0x0400012D RID: 301
		private bool append;

		// Token: 0x0400012E RID: 302
		private bool isWriteStream;

		// Token: 0x0400012F RID: 303
		private bool isDisposed;
	}
}
