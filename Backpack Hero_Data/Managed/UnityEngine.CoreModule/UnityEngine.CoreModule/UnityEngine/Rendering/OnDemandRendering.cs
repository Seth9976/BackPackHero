using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020003E0 RID: 992
	[RequiredByNativeCode]
	public class OnDemandRendering
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x00033C64 File Offset: 0x00031E64
		public static bool willCurrentFrameRender
		{
			get
			{
				return Time.frameCount % OnDemandRendering.renderFrameInterval == 0;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x00033C84 File Offset: 0x00031E84
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00033C9B File Offset: 0x00031E9B
		public static int renderFrameInterval
		{
			get
			{
				return OnDemandRendering.m_RenderFrameInterval;
			}
			set
			{
				OnDemandRendering.m_RenderFrameInterval = Math.Max(1, value);
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00033CAA File Offset: 0x00031EAA
		[RequiredByNativeCode]
		internal static void GetRenderFrameInterval(out int frameInterval)
		{
			frameInterval = OnDemandRendering.renderFrameInterval;
		}

		// Token: 0x06001FCB RID: 8139
		[FreeFunction]
		[MethodImpl(4096)]
		internal static extern float GetEffectiveRenderFrameRate();

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x00033CB4 File Offset: 0x00031EB4
		public static int effectiveRenderFrameRate
		{
			get
			{
				float effectiveRenderFrameRate = OnDemandRendering.GetEffectiveRenderFrameRate();
				bool flag = (double)effectiveRenderFrameRate <= 0.0;
				int num;
				if (flag)
				{
					num = (int)effectiveRenderFrameRate;
				}
				else
				{
					num = (int)(effectiveRenderFrameRate + 0.5f);
				}
				return num;
			}
		}

		// Token: 0x04000C2A RID: 3114
		private static int m_RenderFrameInterval = 1;
	}
}
