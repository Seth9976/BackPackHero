using System;
using System.Collections.Generic;
using System.Linq;
using CleverCrow.Fluid.Dialogues.Actions;
using CleverCrow.Fluid.Dialogues.Choices;
using CleverCrow.Fluid.Dialogues.Conditions;
using CleverCrow.Fluid.Dialogues.Graphs;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues.Nodes
{
	// Token: 0x02000015 RID: 21
	public class NodeDialogue : NodeBase
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002930 File Offset: 0x00000B30
		public NodeDialogue(IGraph graph, string uniqueId, IActor actor, string dialogue, string key, bool keyOverride, bool externalKey, string prefix, AudioClip audioClip, List<INodeData> children, List<IChoice> choices, List<ICondition> conditions, List<IAction> enterActions, List<IAction> exitActions)
			: base(graph, uniqueId, children, conditions, enterActions, exitActions)
		{
			this._actor = actor;
			this._dialogue = dialogue;
			this._key = key;
			this.keyOverride = keyOverride;
			this.externalKey = externalKey;
			this.prefix = prefix;
			this._choices = choices;
			this._audioClip = audioClip;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000298C File Offset: 0x00000B8C
		private List<IChoice> GetValidChoices(IDialoguePlayback playback)
		{
			INode node = base.Next();
			if (this._choices.Count == 0 && ((node != null) ? node.HubChoices : null) != null && node.HubChoices.Count > 0 && !(node is NodeDice))
			{
				playback.Events.NodeEnter.Invoke(node);
				return node.HubChoices;
			}
			return this._choices.Where((IChoice c) => c.IsValid).ToList<IChoice>();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002A18 File Offset: 0x00000C18
		protected override void OnPlay(IDialoguePlayback playback)
		{
			if (this.externalKey && this.prefix != "nokey")
			{
				this.prefix = "EXT#";
			}
			this._emittedChoices = this.GetValidChoices(playback);
			if (this._emittedChoices.Count > 0)
			{
				playback.Events.Choice.Invoke(this._actor, (this.prefix == "nokey") ? this._dialogue : (this.prefix + this._key), this._emittedChoices);
				return;
			}
			playback.Events.Speak.Invoke(this._actor, (this.prefix == "nokey") ? this._dialogue : (this.prefix + this._key));
			playback.Events.SpeakWithAudio.Invoke(this._actor, (this.prefix == "nokey") ? this._dialogue : (this.prefix + this._key), this._audioClip);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002B35 File Offset: 0x00000D35
		public override IChoice GetChoice(int index)
		{
			if (index >= this._emittedChoices.Count)
			{
				return null;
			}
			return this._emittedChoices[index];
		}

		// Token: 0x04000020 RID: 32
		private readonly IActor _actor;

		// Token: 0x04000021 RID: 33
		public readonly string _dialogue;

		// Token: 0x04000022 RID: 34
		public readonly string _key;

		// Token: 0x04000023 RID: 35
		public bool keyOverride;

		// Token: 0x04000024 RID: 36
		public bool externalKey;

		// Token: 0x04000025 RID: 37
		public string prefix;

		// Token: 0x04000026 RID: 38
		public readonly List<IChoice> _choices;

		// Token: 0x04000027 RID: 39
		private List<IChoice> _emittedChoices;

		// Token: 0x04000028 RID: 40
		private readonly AudioClip _audioClip;
	}
}
