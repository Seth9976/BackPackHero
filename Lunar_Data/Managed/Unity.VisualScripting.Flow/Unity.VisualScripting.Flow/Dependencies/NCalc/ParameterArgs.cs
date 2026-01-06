using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000197 RID: 407
	public class ParameterArgs : EventArgs
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00019BAB File Offset: 0x00017DAB
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00019BB3 File Offset: 0x00017DB3
		public object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
				this.HasResult = true;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00019BC3 File Offset: 0x00017DC3
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x00019BCB File Offset: 0x00017DCB
		public bool HasResult { get; set; }

		// Token: 0x04000333 RID: 819
		private object _result;
	}
}
