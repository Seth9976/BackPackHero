using System;
using CleverCrow.Fluid.Dialogues.GameObjectVariables;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.GameObjects
{
	// Token: 0x0200003F RID: 63
	[CreateMenu("GameObject/Set Active", 0)]
	public class SetActive : ActionDataBase
	{
		// Token: 0x0600011A RID: 282 RVA: 0x000043D0 File Offset: 0x000025D0
		public override void OnInit(IDialogueController dialogue)
		{
			this._dialogue = dialogue;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000043DC File Offset: 0x000025DC
		public override void OnStart()
		{
			new SetActiveInternal(new GameObjectWrapper(this._dialogue.LocalDatabaseExtended.GameObjects.Get(this._gameObject.key, this._gameObject.defaultValue))).SetValue(this._setActive);
		}

		// Token: 0x04000072 RID: 114
		private IDialogueController _dialogue;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private KeyValueDefinitionGameObject _gameObject;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private bool _setActive;
	}
}
