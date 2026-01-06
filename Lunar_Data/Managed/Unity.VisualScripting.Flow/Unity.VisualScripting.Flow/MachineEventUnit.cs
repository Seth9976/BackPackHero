using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200008E RID: 142
	public abstract class MachineEventUnit<TArgs> : EventUnit<TArgs>
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000096CC File Offset: 0x000078CC
		protected sealed override bool register
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000096CF File Offset: 0x000078CF
		public override EventHook GetHook(GraphReference reference)
		{
			return new EventHook(this.hookName, reference.machine, null);
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x000096E3 File Offset: 0x000078E3
		protected virtual string hookName
		{
			get
			{
				throw new InvalidImplementationException(string.Format("Missing event hook for '{0}'.", this));
			}
		}
	}
}
