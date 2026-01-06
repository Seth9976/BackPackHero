using System;

namespace CleverCrow.Fluid.Dialogues.Actions
{
	// Token: 0x0200003A RID: 58
	public class ActionRuntime : IAction, IUniqueId
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000042C8 File Offset: 0x000024C8
		public string UniqueId { get; }

		// Token: 0x0600010D RID: 269 RVA: 0x000042D0 File Offset: 0x000024D0
		public ActionRuntime(IDialogueController dialogue, string uniqueId, IActionData data)
		{
			this._data = data;
			this._dialogueController = dialogue;
			this.UniqueId = uniqueId;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000042ED File Offset: 0x000024ED
		public ActionStatus Tick()
		{
			this.Reset();
			this.Init();
			this.Start();
			ActionStatus actionStatus = this.Update();
			if (actionStatus == ActionStatus.Success)
			{
				this.Exit();
			}
			return actionStatus;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004310 File Offset: 0x00002510
		public void End()
		{
			if (this._active)
			{
				this.Exit();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004320 File Offset: 0x00002520
		private void Init()
		{
			if (this._initUsed)
			{
				return;
			}
			this._initUsed = true;
			this._data.OnInit(this._dialogueController);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004343 File Offset: 0x00002543
		private void Start()
		{
			if (this._startUsed)
			{
				return;
			}
			this._startUsed = true;
			this._active = true;
			this._data.OnStart();
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004367 File Offset: 0x00002567
		private void Exit()
		{
			this._startUsed = false;
			this._resetReady = true;
			this._active = false;
			this._data.OnExit();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004389 File Offset: 0x00002589
		private ActionStatus Update()
		{
			return this._data.OnUpdate();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004396 File Offset: 0x00002596
		private void Reset()
		{
			if (!this._resetReady)
			{
				return;
			}
			this._resetReady = false;
			this._data.OnReset();
		}

		// Token: 0x04000067 RID: 103
		private readonly IDialogueController _dialogueController;

		// Token: 0x04000068 RID: 104
		private readonly IActionData _data;

		// Token: 0x04000069 RID: 105
		private bool _initUsed;

		// Token: 0x0400006A RID: 106
		private bool _startUsed;

		// Token: 0x0400006B RID: 107
		private bool _resetReady;

		// Token: 0x0400006C RID: 108
		private bool _active;
	}
}
