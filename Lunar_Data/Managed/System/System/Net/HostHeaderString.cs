using System;
using System.Text;

namespace System.Net
{
	// Token: 0x0200041F RID: 1055
	internal class HostHeaderString
	{
		// Token: 0x06002184 RID: 8580 RVA: 0x0007AAFE File Offset: 0x00078CFE
		internal HostHeaderString()
		{
			this.Init(null);
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0007AB0D File Offset: 0x00078D0D
		internal HostHeaderString(string s)
		{
			this.Init(s);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0007AB1C File Offset: 0x00078D1C
		private void Init(string s)
		{
			this.m_String = s;
			this.m_Converted = false;
			this.m_Bytes = null;
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0007AB34 File Offset: 0x00078D34
		private void Convert()
		{
			if (this.m_String != null && !this.m_Converted)
			{
				this.m_Bytes = Encoding.Default.GetBytes(this.m_String);
				string @string = Encoding.Default.GetString(this.m_Bytes);
				if (string.Compare(this.m_String, @string, StringComparison.Ordinal) != 0)
				{
					this.m_Bytes = Encoding.UTF8.GetBytes(this.m_String);
				}
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007AB9D File Offset: 0x00078D9D
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0007ABA5 File Offset: 0x00078DA5
		internal string String
		{
			get
			{
				return this.m_String;
			}
			set
			{
				this.Init(value);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0007ABAE File Offset: 0x00078DAE
		internal int ByteCount
		{
			get
			{
				this.Convert();
				return this.m_Bytes.Length;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x0007ABBE File Offset: 0x00078DBE
		internal byte[] Bytes
		{
			get
			{
				this.Convert();
				return this.m_Bytes;
			}
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0007ABCC File Offset: 0x00078DCC
		internal void Copy(byte[] destBytes, int destByteIndex)
		{
			this.Convert();
			Array.Copy(this.m_Bytes, 0, destBytes, destByteIndex, this.m_Bytes.Length);
		}

		// Token: 0x04001362 RID: 4962
		private bool m_Converted;

		// Token: 0x04001363 RID: 4963
		private string m_String;

		// Token: 0x04001364 RID: 4964
		private byte[] m_Bytes;
	}
}
