using System;

namespace CleverCrow.Fluid.Dialogues.Actions.GameObjects
{
	// Token: 0x02000040 RID: 64
	public class SetActiveInternal
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00004431 File Offset: 0x00002631
		public SetActiveInternal(IGameObjectWrapper go)
		{
			this._go = go;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004440 File Offset: 0x00002640
		public void SetValue(bool value)
		{
			this._go.SetActive(value);
		}

		// Token: 0x04000075 RID: 117
		private readonly IGameObjectWrapper _go;
	}
}
