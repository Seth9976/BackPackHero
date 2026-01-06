using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x02000073 RID: 115
	[Obsolete("Please use ProfilingScope")]
	public struct ProfilingSample : IDisposable
	{
		// Token: 0x060003AC RID: 940 RVA: 0x000119A9 File Offset: 0x0000FBA9
		public ProfilingSample(CommandBuffer cmd, string name, CustomSampler sampler = null)
		{
			this.m_Cmd = cmd;
			this.m_Name = name;
			this.m_Disposed = false;
			if (cmd != null && name != "")
			{
				cmd.BeginSample(name);
			}
			this.m_Sampler = sampler;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000119DE File Offset: 0x0000FBDE
		public ProfilingSample(CommandBuffer cmd, string format, object arg)
		{
			this = new ProfilingSample(cmd, string.Format(format, arg), null);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000119EF File Offset: 0x0000FBEF
		public ProfilingSample(CommandBuffer cmd, string format, params object[] args)
		{
			this = new ProfilingSample(cmd, string.Format(format, args), null);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011A00 File Offset: 0x0000FC00
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011A09 File Offset: 0x0000FC09
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing && this.m_Cmd != null && this.m_Name != "")
			{
				this.m_Cmd.EndSample(this.m_Name);
			}
			this.m_Disposed = true;
		}

		// Token: 0x0400024B RID: 587
		private readonly CommandBuffer m_Cmd;

		// Token: 0x0400024C RID: 588
		private readonly string m_Name;

		// Token: 0x0400024D RID: 589
		private bool m_Disposed;

		// Token: 0x0400024E RID: 590
		private CustomSampler m_Sampler;
	}
}
