using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x020002BC RID: 700
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x0002F0DC File Offset: 0x0002D2DC
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument)
			: base(target, theFunction)
		{
			this.m_Arg1 = argument;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0002F0EF File Offset: 0x0002D2EF
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0002F0EF File Offset: 0x0002D2EF
		public override void Invoke(T arg0)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x04000996 RID: 2454
		private readonly T m_Arg1;
	}
}
