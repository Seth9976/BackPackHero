using System;
using System.Collections;
using Febucci.UI.Core;
using Febucci.UI.Core.Parsing;
using UnityEngine;

namespace Febucci.UI.Actions
{
	// Token: 0x02000035 RID: 53
	[Serializable]
	public abstract class ActionScriptableBase : ScriptableObject, ITagProvider
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C5D File Offset: 0x00002E5D
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004C65 File Offset: 0x00002E65
		public string TagID
		{
			get
			{
				return this.tagID;
			}
			set
			{
				this.tagID = value;
			}
		}

		// Token: 0x060000C4 RID: 196
		public abstract IEnumerator DoAction(ActionMarker action, TypewriterCore typewriter, TypingInfo typingInfo);

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private string tagID;
	}
}
