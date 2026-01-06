using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.Fluid.Dialogues
{
	// Token: 0x02000002 RID: 2
	[CreateAssetMenu(fileName = "Actor", menuName = "Fluid/Dialogue/Actor")]
	public class ActorDefinition : ScriptableObject, IActor
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public Sprite Portrait
		{
			get
			{
				return this._portrait;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public float TalkPitch
		{
			get
			{
				return this._talkPitch;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public List<string> Tasks
		{
			get
			{
				return this._tasks;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public IActor.ActorType actorType
		{
			get
			{
				return this._actorType;
			}
		}

		// Token: 0x04000001 RID: 1
		[SerializeField]
		private string _displayName;

		// Token: 0x04000002 RID: 2
		[SerializeField]
		private Sprite _portrait;

		// Token: 0x04000003 RID: 3
		[SerializeField]
		private float _talkPitch = 1f;

		// Token: 0x04000004 RID: 4
		[SerializeField]
		private IActor.ActorType _actorType;

		// Token: 0x04000005 RID: 5
		[SerializeField]
		private List<string> _tasks;
	}
}
