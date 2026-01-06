using System;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x0200001B RID: 27
	[Obsolete("Should not be public. Do not use this, it will be removed in an upcoming version.")]
	public class AnalyticsLifetime : MonoBehaviour
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003548 File Offset: 0x00001748
		// (set) Token: 0x0600005C RID: 92 RVA: 0x0000354F File Offset: 0x0000174F
		internal static AnalyticsLifetime Instance { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003557 File Offset: 0x00001757
		internal float TimeUntilHeartbeat
		{
			get
			{
				return 60f - this.m_HeartbeatTime;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003565 File Offset: 0x00001765
		private void Awake()
		{
			AnalyticsLifetime.Instance = this;
			base.hideFlags = HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild;
			base.hideFlags |= HideFlags.HideInInspector;
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003590 File Offset: 0x00001790
		private void Update()
		{
			this.m_GameRunningTime += Time.unscaledDeltaTime;
			if (this.m_GameRunningTime >= 60f)
			{
				AnalyticsService.internalInstance.RecordGameRunningIfNecessary();
				this.m_GameRunningTime = 0f;
			}
			this.m_HeartbeatTime += Time.unscaledDeltaTime;
			if (this.m_HeartbeatTime >= 60f)
			{
				AnalyticsService.internalInstance.InternalTick();
				this.m_HeartbeatTime = 0f;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003605 File Offset: 0x00001805
		private void OnDestroy()
		{
			AnalyticsService.internalInstance.GameEnded();
		}

		// Token: 0x04000091 RID: 145
		private const float k_HeartbeatPeriod = 60f;

		// Token: 0x04000092 RID: 146
		private const float k_GameRunningPeriod = 60f;

		// Token: 0x04000093 RID: 147
		private float m_HeartbeatTime;

		// Token: 0x04000094 RID: 148
		private float m_GameRunningTime;
	}
}
