using System;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.GameObjectVariables;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	public class GameObjectOverride : IGameObjectOverride
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000024A3 File Offset: 0x000006A3
		public IKeyValueDefinition<GameObject> Definition
		{
			get
			{
				return this._variable;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024AB File Offset: 0x000006AB
		public GameObject Value
		{
			get
			{
				return this._gameObject;
			}
		}

		// Token: 0x0400000D RID: 13
		[SerializeField]
		private KeyValueDefinitionGameObject _variable;

		// Token: 0x0400000E RID: 14
		[SerializeField]
		private GameObject _gameObject;
	}
}
