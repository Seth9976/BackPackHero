using System;
using Febucci.UI.Core;
using UnityEngine;

namespace Febucci.UI.Effects
{
	// Token: 0x0200002E RID: 46
	public abstract class AnimationScriptableBase : ScriptableObject, ITagProvider
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004AED File Offset: 0x00002CED
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004AF5 File Offset: 0x00002CF5
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

		// Token: 0x060000AB RID: 171 RVA: 0x00004AFE File Offset: 0x00002CFE
		public void InitializeOnce()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			this.OnInitialize();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004B16 File Offset: 0x00002D16
		protected virtual void OnInitialize()
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004B18 File Offset: 0x00002D18
		private void OnEnable()
		{
			this.initialized = false;
		}

		// Token: 0x060000AE RID: 174
		public abstract void ResetContext(TAnimCore animator);

		// Token: 0x060000AF RID: 175 RVA: 0x00004B21 File Offset: 0x00002D21
		public virtual void SetModifier(ModifierInfo modifier)
		{
		}

		// Token: 0x060000B0 RID: 176
		public abstract float GetMaxDuration();

		// Token: 0x060000B1 RID: 177
		public abstract bool CanApplyEffectTo(CharacterData character, TAnimCore animator);

		// Token: 0x060000B2 RID: 178
		public abstract void ApplyEffectTo(ref CharacterData character, TAnimCore animator);

		// Token: 0x040000A2 RID: 162
		[SerializeField]
		private string tagID;

		// Token: 0x040000A3 RID: 163
		private bool initialized;
	}
}
