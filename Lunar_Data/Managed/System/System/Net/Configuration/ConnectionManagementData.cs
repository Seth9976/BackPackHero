using System;
using System.Collections;

namespace System.Net.Configuration
{
	// Token: 0x0200056F RID: 1391
	internal class ConnectionManagementData
	{
		// Token: 0x06002C17 RID: 11287 RVA: 0x0009D970 File Offset: 0x0009BB70
		public ConnectionManagementData(object parent)
		{
			this.data = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			if (parent != null && parent is ConnectionManagementData)
			{
				ConnectionManagementData connectionManagementData = (ConnectionManagementData)parent;
				foreach (object obj in connectionManagementData.data.Keys)
				{
					string text = (string)obj;
					this.data[text] = connectionManagementData.data[text];
				}
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0009DA0C File Offset: 0x0009BC0C
		public void Add(string address, string nconns)
		{
			if (nconns == null || nconns == "")
			{
				nconns = "2";
			}
			this.data[address] = uint.Parse(nconns);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0009DA3C File Offset: 0x0009BC3C
		public void Add(string address, int nconns)
		{
			this.data[address] = (uint)nconns;
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0009DA50 File Offset: 0x0009BC50
		public void Remove(string address)
		{
			this.data.Remove(address);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0009DA5E File Offset: 0x0009BC5E
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0009DA6C File Offset: 0x0009BC6C
		public uint GetMaxConnections(string hostOrIP)
		{
			object obj = this.data[hostOrIP];
			if (obj == null)
			{
				obj = this.data["*"];
			}
			if (obj == null)
			{
				return 2U;
			}
			return (uint)obj;
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x0009DAA5 File Offset: 0x0009BCA5
		public Hashtable Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x04001A46 RID: 6726
		private Hashtable data;

		// Token: 0x04001A47 RID: 6727
		private const int defaultMaxConnections = 2;
	}
}
