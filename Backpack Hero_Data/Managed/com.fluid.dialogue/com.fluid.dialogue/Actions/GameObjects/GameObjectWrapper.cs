using System;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.GameObjects
{
	// Token: 0x0200003E RID: 62
	public class GameObjectWrapper : IGameObjectWrapper
	{
		// Token: 0x06000118 RID: 280 RVA: 0x000043B3 File Offset: 0x000025B3
		public GameObjectWrapper(GameObject go)
		{
			this._go = go;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000043C2 File Offset: 0x000025C2
		public void SetActive(bool value)
		{
			this._go.SetActive(value);
		}

		// Token: 0x04000071 RID: 113
		private readonly GameObject _go;
	}
}
