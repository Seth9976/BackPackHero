using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000B3E RID: 2878
	internal class ReadLinesIterator : Iterator<string>
	{
		// Token: 0x06006804 RID: 26628 RVA: 0x0016285F File Offset: 0x00160A5F
		private ReadLinesIterator(string path, Encoding encoding, StreamReader reader)
		{
			this._path = path;
			this._encoding = encoding;
			this._reader = reader;
		}

		// Token: 0x06006805 RID: 26629 RVA: 0x0016287C File Offset: 0x00160A7C
		public override bool MoveNext()
		{
			if (this._reader != null)
			{
				this.current = this._reader.ReadLine();
				if (this.current != null)
				{
					return true;
				}
				base.Dispose();
			}
			return false;
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x001628A8 File Offset: 0x00160AA8
		protected override Iterator<string> Clone()
		{
			return ReadLinesIterator.CreateIterator(this._path, this._encoding, this._reader);
		}

		// Token: 0x06006807 RID: 26631 RVA: 0x001628C4 File Offset: 0x00160AC4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this._reader != null)
				{
					this._reader.Dispose();
				}
			}
			finally
			{
				this._reader = null;
				base.Dispose(disposing);
			}
		}

		// Token: 0x06006808 RID: 26632 RVA: 0x00162908 File Offset: 0x00160B08
		internal static ReadLinesIterator CreateIterator(string path, Encoding encoding)
		{
			return ReadLinesIterator.CreateIterator(path, encoding, null);
		}

		// Token: 0x06006809 RID: 26633 RVA: 0x00162912 File Offset: 0x00160B12
		private static ReadLinesIterator CreateIterator(string path, Encoding encoding, StreamReader reader)
		{
			return new ReadLinesIterator(path, encoding, reader ?? new StreamReader(path, encoding));
		}

		// Token: 0x04003C7D RID: 15485
		private readonly string _path;

		// Token: 0x04003C7E RID: 15486
		private readonly Encoding _encoding;

		// Token: 0x04003C7F RID: 15487
		private StreamReader _reader;
	}
}
