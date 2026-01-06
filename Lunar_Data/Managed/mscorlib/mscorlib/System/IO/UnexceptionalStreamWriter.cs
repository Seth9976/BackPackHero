using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000B6B RID: 2923
	internal class UnexceptionalStreamWriter : StreamWriter
	{
		// Token: 0x06006A61 RID: 27233 RVA: 0x0016C2EC File Offset: 0x0016A4EC
		public UnexceptionalStreamWriter(Stream stream, Encoding encoding)
			: base(stream, encoding, 1024, true)
		{
		}

		// Token: 0x06006A62 RID: 27234 RVA: 0x0016C2FC File Offset: 0x0016A4FC
		public override void Flush()
		{
			try
			{
				base.Flush();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A63 RID: 27235 RVA: 0x0016C324 File Offset: 0x0016A524
		public override void Write(char[] buffer, int index, int count)
		{
			try
			{
				base.Write(buffer, index, count);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A64 RID: 27236 RVA: 0x0016C350 File Offset: 0x0016A550
		public override void Write(char value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A65 RID: 27237 RVA: 0x0016C37C File Offset: 0x0016A57C
		public override void Write(char[] value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06006A66 RID: 27238 RVA: 0x0016C3A8 File Offset: 0x0016A5A8
		public override void Write(string value)
		{
			try
			{
				base.Write(value);
			}
			catch (Exception)
			{
			}
		}
	}
}
