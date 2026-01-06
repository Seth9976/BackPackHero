using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000026 RID: 38
	public abstract class Marker : ScriptableObject, IMarker
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000829A File Offset: 0x0000649A
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000082A2 File Offset: 0x000064A2
		public TrackAsset parent { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000082AB File Offset: 0x000064AB
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000082B3 File Offset: 0x000064B3
		public double time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = Math.Max(value, 0.0);
			}
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000082CC File Offset: 0x000064CC
		void IMarker.Initialize(TrackAsset parentTrack)
		{
			if (this.parent == null)
			{
				this.parent = parentTrack;
				try
				{
					this.OnInitialize(parentTrack);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.Message, this);
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008314 File Offset: 0x00006514
		public virtual void OnInitialize(TrackAsset aPent)
		{
		}

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		[TimeField(TimeFieldAttribute.UseEditMode.ApplyEditMode)]
		[Tooltip("Time for the marker")]
		private double m_Time;
	}
}
