using System;

namespace Unity.VisualScripting.Dependencies.NCalc
{
	// Token: 0x02000190 RID: 400
	public class FunctionArgs : EventArgs
	{
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000146DC File Offset: 0x000128DC
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x000146E4 File Offset: 0x000128E4
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x000146F4 File Offset: 0x000128F4
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x000146FC File Offset: 0x000128FC
		public bool HasResult { get; set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x00014705 File Offset: 0x00012905
		// (set) Token: 0x06000AB7 RID: 2743 RVA: 0x0001470D File Offset: 0x0001290D
		public Expression[] Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				this._parameters = value;
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00014718 File Offset: 0x00012918
		public object[] EvaluateParameters(Flow flow)
		{
			object[] array = new object[this._parameters.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this._parameters[i].Evaluate(flow);
			}
			return array;
		}

		// Token: 0x04000265 RID: 613
		private object _result;

		// Token: 0x04000266 RID: 614
		private Expression[] _parameters = new Expression[0];
	}
}
