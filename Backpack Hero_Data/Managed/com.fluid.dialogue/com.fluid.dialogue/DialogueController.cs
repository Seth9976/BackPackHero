using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Databases;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Graphs;
using CleverCrow.Fluid.Dialogues.Nodes;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000007 RID: 7
	public class DialogueController : IDialogueController
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020D9 File Offset: 0x000002D9
		[Obsolete("Use LocalDatabaseExtended instead")]
		public IDatabaseInstance LocalDatabase { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020E1 File Offset: 0x000002E1
		public IDatabaseInstanceExtended LocalDatabaseExtended { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000020E9 File Offset: 0x000002E9
		public IDialogueEvents Events { get; } = new DialogueEvents();

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000020F1 File Offset: 0x000002F1
		public IDialoguePlayback ActiveDialogue
		{
			get
			{
				if (this._activeDialogue.Count <= 0)
				{
					return null;
				}
				return this._activeDialogue.Peek();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000210E File Offset: 0x0000030E
		[Obsolete("Use DatabaseInstanceExtended instead. Old databases do not support GameObjects")]
		public DialogueController(IDatabaseInstance localDatabase)
		{
			this.LocalDatabase = localDatabase;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002133 File Offset: 0x00000333
		public DialogueController(IDatabaseInstanceExtended localDatabase)
		{
			this.LocalDatabase = localDatabase;
			this.LocalDatabaseExtended = localDatabase;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002160 File Offset: 0x00000360
		public void Play(IDialoguePlayback playback, IGameObjectOverride[] gameObjectOverrides = null)
		{
			this.SetupDatabases(gameObjectOverrides);
			this.Stop();
			playback.Events.Speak.AddListener(new UnityAction<IActor, string>(this.TriggerSpeak));
			playback.Events.SpeakWithAudio.AddListener(new UnityAction<IActor, string, AudioClip>(this.TriggerSpeakWithAudio));
			playback.Events.Choice.AddListener(new UnityAction<IActor, string, List<IChoice>>(this.TriggerChoice));
			playback.Events.NodeEnter.AddListener(new UnityAction<INode>(this.TriggerEnterNode));
			playback.Events.Begin.AddListener(new UnityAction(this.TriggerBegin));
			playback.Events.End.AddListener(new UnityAction(this.TriggerEnd));
			this._activeDialogue.Push(playback);
			playback.Play();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002234 File Offset: 0x00000434
		public void Play(IGraphData graph, IGameObjectOverride[] gameObjectOverrides = null)
		{
			GraphRuntime graphRuntime = new GraphRuntime(this, graph);
			this.Play(new DialoguePlayback(graphRuntime, this, new DialogueEvents()), gameObjectOverrides);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000225C File Offset: 0x0000045C
		private void SetupDatabases(IGameObjectOverride[] gameObjectOverrides)
		{
			this.LocalDatabase.Clear();
			if (this.LocalDatabaseExtended == null)
			{
				return;
			}
			this.LocalDatabaseExtended.ClearGameObjects();
			if (gameObjectOverrides == null)
			{
				return;
			}
			foreach (IGameObjectOverride gameObjectOverride in gameObjectOverrides)
			{
				this.LocalDatabaseExtended.GameObjects.Set(gameObjectOverride.Definition.Key, gameObjectOverride.Value);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022C4 File Offset: 0x000004C4
		public void PlayChild(IDialoguePlayback playback)
		{
			if (this.ActiveDialogue == null)
			{
				throw new InvalidOperationException("Cannot trigger child dialogue, nothing is playing");
			}
			IDialoguePlayback parentDialogue = this.ActiveDialogue;
			playback.Events.End.AddListener(delegate
			{
				this._activeDialogue.Pop();
				parentDialogue.Next();
			});
			playback.Events.Speak.AddListener(new UnityAction<IActor, string>(this.TriggerSpeak));
			playback.Events.SpeakWithAudio.AddListener(new UnityAction<IActor, string, AudioClip>(this.TriggerSpeakWithAudio));
			playback.Events.Choice.AddListener(new UnityAction<IActor, string, List<IChoice>>(this.TriggerChoice));
			playback.Events.NodeEnter.AddListener(new UnityAction<INode>(this.TriggerEnterNode));
			this._activeDialogue.Push(playback);
			playback.Play();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000239C File Offset: 0x0000059C
		public void PlayChild(IGraphData graph)
		{
			GraphRuntime graphRuntime = new GraphRuntime(this, graph);
			this.PlayChild(new DialoguePlayback(graphRuntime, this, new DialogueEvents()));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023C3 File Offset: 0x000005C3
		private void TriggerBegin()
		{
			this.Events.Begin.Invoke();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000023D5 File Offset: 0x000005D5
		private void TriggerEnd()
		{
			this._activeDialogue.Pop();
			this.Events.End.Invoke();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023F3 File Offset: 0x000005F3
		private void TriggerSpeakWithAudio(IActor actor, string text, AudioClip audioClip)
		{
			this.Events.SpeakWithAudio.Invoke(actor, text, audioClip);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002408 File Offset: 0x00000608
		private void TriggerSpeak(IActor actor, string text)
		{
			this.Events.Speak.Invoke(actor, text);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000241C File Offset: 0x0000061C
		private void TriggerChoice(IActor actor, string text, List<IChoice> choices)
		{
			this.Events.Choice.Invoke(actor, text, choices);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002431 File Offset: 0x00000631
		private void TriggerEnterNode(INode node)
		{
			this.Events.NodeEnter.Invoke(node);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002444 File Offset: 0x00000644
		public void Next()
		{
			if (this._activeDialogue == null)
			{
				return;
			}
			IDialoguePlayback activeDialogue = this.ActiveDialogue;
			if (activeDialogue == null)
			{
				return;
			}
			activeDialogue.Next();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000245F File Offset: 0x0000065F
		public void Tick()
		{
			if (this._activeDialogue == null)
			{
				return;
			}
			IDialoguePlayback activeDialogue = this.ActiveDialogue;
			if (activeDialogue == null)
			{
				return;
			}
			activeDialogue.Tick();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247A File Offset: 0x0000067A
		public void SelectChoice(int index)
		{
			IDialoguePlayback activeDialogue = this.ActiveDialogue;
			if (activeDialogue == null)
			{
				return;
			}
			activeDialogue.SelectChoice(index);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000248D File Offset: 0x0000068D
		public void Stop()
		{
			if (this._activeDialogue == null)
			{
				return;
			}
			this._activeDialogue.Clear();
		}

		// Token: 0x04000009 RID: 9
		private readonly Stack<IDialoguePlayback> _activeDialogue = new Stack<IDialoguePlayback>();
	}
}
