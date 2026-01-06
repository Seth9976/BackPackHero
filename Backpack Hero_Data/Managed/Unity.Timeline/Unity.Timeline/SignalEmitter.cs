using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002B RID: 43
	[CustomStyle("SignalEmitter")]
	[ExcludeFromPreset]
	[Serializable]
	public class SignalEmitter : Marker, INotification, INotificationOptionProvider
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000864B File Offset: 0x0000684B
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00008653 File Offset: 0x00006853
		public bool retroactive
		{
			get
			{
				return this.m_Retroactive;
			}
			set
			{
				this.m_Retroactive = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000865C File Offset: 0x0000685C
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00008664 File Offset: 0x00006864
		public bool emitOnce
		{
			get
			{
				return this.m_EmitOnce;
			}
			set
			{
				this.m_EmitOnce = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000866D File Offset: 0x0000686D
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00008675 File Offset: 0x00006875
		public SignalAsset asset
		{
			get
			{
				return this.m_Asset;
			}
			set
			{
				this.m_Asset = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000867E File Offset: 0x0000687E
		PropertyName INotification.id
		{
			get
			{
				if (this.m_Asset != null)
				{
					return new PropertyName(this.m_Asset.name);
				}
				return new PropertyName(string.Empty);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600024D RID: 589 RVA: 0x000086A9 File Offset: 0x000068A9
		NotificationFlags INotificationOptionProvider.flags
		{
			get
			{
				return (this.retroactive ? NotificationFlags.Retroactive : ((NotificationFlags)0)) | (this.emitOnce ? NotificationFlags.TriggerOnce : ((NotificationFlags)0)) | NotificationFlags.TriggerInEditMode;
			}
		}

		// Token: 0x040000CD RID: 205
		[SerializeField]
		private bool m_Retroactive;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		private bool m_EmitOnce;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		private SignalAsset m_Asset;
	}
}
