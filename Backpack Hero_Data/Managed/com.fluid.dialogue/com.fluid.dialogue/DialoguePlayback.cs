using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x0200000D RID: 13
	public class DialoguePlayback : IDialoguePlayback
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000024D1 File Offset: 0x000006D1
		public IDialogueEvents Events { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024D9 File Offset: 0x000006D9
		public IDialogueController ParentCtrl { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000024E1 File Offset: 0x000006E1
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000024E9 File Offset: 0x000006E9
		public INode Pointer { get; private set; }

		// Token: 0x0600003F RID: 63 RVA: 0x000024F2 File Offset: 0x000006F2
		public DialoguePlayback(IGraph graph, IDialogueController ctrl, IDialogueEvents events)
		{
			this._graph = graph;
			this.Events = events;
			this.ParentCtrl = ctrl;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000251C File Offset: 0x0000071C
		public void Play()
		{
			this.Stop();
			this._playing = true;
			this.Pointer = this._graph.Root;
			this.Events.Begin.Invoke();
			if (!this._graph.Root.IsValid)
			{
				this.Events.End.Invoke();
				return;
			}
			this.Next(null, this.Pointer);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002587 File Offset: 0x00000787
		private void ClearAllActions()
		{
			while (this._actionQueue.Count > 0)
			{
				this._actionQueue.Dequeue().End();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000025A9 File Offset: 0x000007A9
		private ActionStatus UpdateActionQueue()
		{
			while (this._actionQueue.Count > 0)
			{
				if (this._actionQueue.Peek().Tick() == ActionStatus.Continue)
				{
					return ActionStatus.Continue;
				}
				this._actionQueue.Dequeue();
			}
			return ActionStatus.Success;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000025E0 File Offset: 0x000007E0
		public void Next()
		{
			if (this._actionQueue.Count != 0)
			{
				return;
			}
			INode pointer = this.Pointer;
			INode node = this.Pointer.Next();
			this.Pointer = node;
			this.Next(pointer, node);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002620 File Offset: 0x00000820
		private void Next(INode current, INode next)
		{
			if (current != null)
			{
				foreach (IAction action in current.ExitActions)
				{
					this._actionQueue.Enqueue(action);
				}
			}
			if (next != null)
			{
				foreach (IAction action2 in next.EnterActions)
				{
					this._actionQueue.Enqueue(action2);
				}
			}
			if (this.UpdateActionQueue() == ActionStatus.Continue)
			{
				return;
			}
			this.UpdatePointer(next);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000026D8 File Offset: 0x000008D8
		private void UpdatePointer(INode pointer)
		{
			if (pointer == null)
			{
				this.Events.End.Invoke();
				this._playing = false;
				return;
			}
			pointer.Play(this);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000026FC File Offset: 0x000008FC
		public void Tick()
		{
			if (this._actionQueue.Count > 0 && this.UpdateActionQueue() == ActionStatus.Success)
			{
				this.UpdatePointer(this.Pointer);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002720 File Offset: 0x00000920
		public void Stop()
		{
			this.Pointer = null;
			this.ClearAllActions();
			if (this._playing)
			{
				this.Events.End.Invoke();
				this._playing = false;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002750 File Offset: 0x00000950
		public void SelectChoice(int index)
		{
			IChoice choice = this.Pointer.GetChoice(index);
			INode pointer = this.Pointer;
			this.Pointer = choice.GetValidChildNode();
			this.Next(pointer, this.Pointer);
		}

		// Token: 0x0400000F RID: 15
		private bool _playing;

		// Token: 0x04000010 RID: 16
		private readonly Queue<IAction> _actionQueue = new Queue<IAction>();

		// Token: 0x04000011 RID: 17
		private readonly IGraph _graph;
	}
}
