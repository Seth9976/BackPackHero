using System;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000250 RID: 592
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x0004E9B0 File Offset: 0x0004CBB0
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0004E9BF File Offset: 0x0004CBBF
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00013B26 File Offset: 0x00011D26
		internal DataReceivedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A97 RID: 2711
		private string data;
	}
}
