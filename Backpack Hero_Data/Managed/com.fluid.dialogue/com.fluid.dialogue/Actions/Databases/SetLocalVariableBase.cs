using System;
using CleverCrow.Fluid.Databases;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Actions.Databases
{
	// Token: 0x02000049 RID: 73
	public abstract class SetLocalVariableBase<T> : ActionDataBase
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000137 RID: 311
		protected abstract KeyValueDefinitionBase<T> Variable { get; }

		// Token: 0x06000138 RID: 312 RVA: 0x00004546 File Offset: 0x00002746
		public override void OnInit(IDialogueController controller)
		{
			this._setKeyValue = new SetKeyValueInternal<T>(this.GetDatabase(controller));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000455A File Offset: 0x0000275A
		public override ActionStatus OnUpdate()
		{
			this._setKeyValue.WriteValue(this.Variable.key, this._value);
			return base.OnUpdate();
		}

		// Token: 0x0600013A RID: 314
		protected abstract IKeyValueData<T> GetDatabase(IDialogueController dialogue);

		// Token: 0x0400007E RID: 126
		private SetKeyValueInternal<T> _setKeyValue;

		// Token: 0x0400007F RID: 127
		[SerializeField]
		public T _value;
	}
}
