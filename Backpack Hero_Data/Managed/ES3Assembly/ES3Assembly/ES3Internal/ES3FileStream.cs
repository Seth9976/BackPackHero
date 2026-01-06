using System;
using System.IO;

namespace ES3Internal
{
	// Token: 0x020000D3 RID: 211
	public class ES3FileStream : FileStream
	{
		// Token: 0x0600048B RID: 1163 RVA: 0x000219A9 File Offset: 0x0001FBA9
		public ES3FileStream(string path, ES3FileMode fileMode, int bufferSize, bool useAsync)
			: base(ES3FileStream.GetPath(path, fileMode), ES3FileStream.GetFileMode(fileMode), ES3FileStream.GetFileAccess(fileMode), FileShare.None, bufferSize, useAsync)
		{
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000219C8 File Offset: 0x0001FBC8
		protected static string GetPath(string path, ES3FileMode fileMode)
		{
			string directoryPath = ES3IO.GetDirectoryPath(path, '/');
			if (fileMode != ES3FileMode.Read && directoryPath != ES3IO.persistentDataPath)
			{
				ES3IO.CreateDirectory(directoryPath);
			}
			if (fileMode != ES3FileMode.Write || fileMode == ES3FileMode.Append)
			{
				return path;
			}
			if (fileMode != ES3FileMode.Write)
			{
				return path;
			}
			return path + ".tmp";
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00021A0F File Offset: 0x0001FC0F
		protected static FileMode GetFileMode(ES3FileMode fileMode)
		{
			if (fileMode == ES3FileMode.Read)
			{
				return FileMode.Open;
			}
			if (fileMode == ES3FileMode.Write)
			{
				return FileMode.Create;
			}
			return FileMode.Append;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00021A1D File Offset: 0x0001FC1D
		protected static FileAccess GetFileAccess(ES3FileMode fileMode)
		{
			if (fileMode == ES3FileMode.Read)
			{
				return FileAccess.Read;
			}
			return FileAccess.Write;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00021A29 File Offset: 0x0001FC29
		protected override void Dispose(bool disposing)
		{
			if (this.isDisposed)
			{
				return;
			}
			this.isDisposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x0400012B RID: 299
		private bool isDisposed;
	}
}
