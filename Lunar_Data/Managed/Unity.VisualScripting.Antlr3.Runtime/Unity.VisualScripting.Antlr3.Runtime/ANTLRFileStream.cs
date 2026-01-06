using System;
using System.IO;
using System.Text;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x02000040 RID: 64
	public class ANTLRFileStream : ANTLRStringStream
	{
		// Token: 0x06000251 RID: 593 RVA: 0x000071C9 File Offset: 0x000061C9
		protected ANTLRFileStream()
		{
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000071D1 File Offset: 0x000061D1
		public ANTLRFileStream(string fileName)
			: this(fileName, Encoding.Default)
		{
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000071DF File Offset: 0x000061DF
		public ANTLRFileStream(string fileName, Encoding encoding)
		{
			this.fileName = fileName;
			this.Load(fileName, encoding);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000071F6 File Offset: 0x000061F6
		public override string SourceName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00007200 File Offset: 0x00006200
		public virtual void Load(string fileName, Encoding encoding)
		{
			if (fileName == null)
			{
				return;
			}
			StreamReader streamReader = null;
			try
			{
				FileInfo fileInfo = new FileInfo(fileName);
				int num = (int)this.GetFileLength(fileInfo);
				this.data = new char[num];
				if (encoding != null)
				{
					streamReader = new StreamReader(fileName, encoding);
				}
				else
				{
					streamReader = new StreamReader(fileName, Encoding.Default);
				}
				this.n = streamReader.Read(this.data, 0, this.data.Length);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00007280 File Offset: 0x00006280
		private long GetFileLength(FileInfo file)
		{
			if (file.Exists)
			{
				return file.Length;
			}
			return 0L;
		}

		// Token: 0x040000A3 RID: 163
		protected string fileName;
	}
}
