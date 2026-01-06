using System;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x0200001A RID: 26
	internal class AnalyticsContainer : MonoBehaviour
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003414 File Offset: 0x00001614
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000341B File Offset: 0x0000161B
		internal static AnalyticsContainer Instance { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003423 File Offset: 0x00001623
		internal float TimeUntilHeartbeat
		{
			get
			{
				return 60f - this.m_HeartbeatTime;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003434 File Offset: 0x00001634
		internal static void Initialize()
		{
			if (!AnalyticsContainer.s_Created)
			{
				AnalyticsContainer.s_Container = new GameObject("AnalyticsContainer");
				AnalyticsContainer.Instance = AnalyticsContainer.s_Container.AddComponent<AnalyticsContainer>();
				AnalyticsContainer.s_Container.hideFlags = HideFlags.NotEditable | HideFlags.DontSaveInBuild;
				AnalyticsContainer.s_Container.hideFlags |= HideFlags.HideInInspector;
				Object.DontDestroyOnLoad(AnalyticsContainer.s_Container);
				AnalyticsContainer.s_Created = true;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003494 File Offset: 0x00001694
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

		// Token: 0x06000057 RID: 87 RVA: 0x00003509 File Offset: 0x00001709
		private void OnApplicationPause(bool paused)
		{
			AnalyticsService.internalInstance.ApplicationPaused(paused);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003516 File Offset: 0x00001716
		internal static void DestroyContainer()
		{
			Object.Destroy(AnalyticsContainer.s_Container);
			AnalyticsContainer.s_Created = false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003528 File Offset: 0x00001728
		private void OnDestroy()
		{
			AnalyticsService.internalInstance.GameEnded();
			AnalyticsContainer.s_Container = null;
			AnalyticsContainer.s_Created = false;
		}

		// Token: 0x0400008A RID: 138
		private const float k_HeartbeatPeriod = 60f;

		// Token: 0x0400008B RID: 139
		private const float k_GameRunningPeriod = 60f;

		// Token: 0x0400008C RID: 140
		private static bool s_Created;

		// Token: 0x0400008D RID: 141
		private static GameObject s_Container;

		// Token: 0x0400008E RID: 142
		private float m_HeartbeatTime;

		// Token: 0x0400008F RID: 143
		private float m_GameRunningTime;
	}
}
