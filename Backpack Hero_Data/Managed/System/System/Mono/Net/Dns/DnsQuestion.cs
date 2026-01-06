using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000BB RID: 187
	internal class DnsQuestion
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000219B File Offset: 0x0000039B
		internal DnsQuestion()
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B25D File Offset: 0x0000945D
		internal int Init(DnsPacket packet, int offset)
		{
			this.name = packet.ReadName(ref offset);
			this.type = (DnsQType)packet.ReadUInt16(ref offset);
			this._class = (DnsQClass)packet.ReadUInt16(ref offset);
			return offset;
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000B28A File Offset: 0x0000948A
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000B292 File Offset: 0x00009492
		public DnsQType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000B29A File Offset: 0x0000949A
		public DnsQClass Class
		{
			get
			{
				return this._class;
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B2A2 File Offset: 0x000094A2
		public override string ToString()
		{
			return string.Format("Name: {0} Type: {1} Class: {2}", this.Name, this.Type, this.Class);
		}

		// Token: 0x04000301 RID: 769
		private string name;

		// Token: 0x04000302 RID: 770
		private DnsQType type;

		// Token: 0x04000303 RID: 771
		private DnsQClass _class;
	}
}
