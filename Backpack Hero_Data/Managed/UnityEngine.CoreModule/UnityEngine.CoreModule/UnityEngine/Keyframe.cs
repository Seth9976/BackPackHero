using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000E1 RID: 225
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x06000386 RID: 902 RVA: 0x00005EB0 File Offset: 0x000040B0
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00005EFF File Offset: 0x000040FF
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 0;
			this.m_InWeight = 0f;
			this.m_OutWeight = 0f;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00005F3C File Offset: 0x0000413C
		public Keyframe(float time, float value, float inTangent, float outTangent, float inWeight, float outWeight)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
			this.m_WeightedMode = 3;
			this.m_InWeight = inWeight;
			this.m_OutWeight = outWeight;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00005F74 File Offset: 0x00004174
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00005F8C File Offset: 0x0000418C
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00005F98 File Offset: 0x00004198
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00005FB0 File Offset: 0x000041B0
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00005FBC File Offset: 0x000041BC
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00005FD4 File Offset: 0x000041D4
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00005FE0 File Offset: 0x000041E0
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00005FF8 File Offset: 0x000041F8
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00006004 File Offset: 0x00004204
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000601C File Offset: 0x0000421C
		public float inWeight
		{
			get
			{
				return this.m_InWeight;
			}
			set
			{
				this.m_InWeight = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00006028 File Offset: 0x00004228
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00006040 File Offset: 0x00004240
		public float outWeight
		{
			get
			{
				return this.m_OutWeight;
			}
			set
			{
				this.m_OutWeight = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000604C File Offset: 0x0000424C
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00006064 File Offset: 0x00004264
		public WeightedMode weightedMode
		{
			get
			{
				return (WeightedMode)this.m_WeightedMode;
			}
			set
			{
				this.m_WeightedMode = (int)value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00006070 File Offset: 0x00004270
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00006088 File Offset: 0x00004288
		[Obsolete("Use AnimationUtility.SetKeyLeftTangentMode, AnimationUtility.SetKeyRightTangentMode, AnimationUtility.GetKeyLeftTangentMode or AnimationUtility.GetKeyRightTangentMode instead.")]
		public int tangentMode
		{
			get
			{
				return this.tangentModeInternal;
			}
			set
			{
				this.tangentModeInternal = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00006094 File Offset: 0x00004294
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00004557 File Offset: 0x00002757
		internal int tangentModeInternal
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x040002E5 RID: 741
		private float m_Time;

		// Token: 0x040002E6 RID: 742
		private float m_Value;

		// Token: 0x040002E7 RID: 743
		private float m_InTangent;

		// Token: 0x040002E8 RID: 744
		private float m_OutTangent;

		// Token: 0x040002E9 RID: 745
		private int m_WeightedMode;

		// Token: 0x040002EA RID: 746
		private float m_InWeight;

		// Token: 0x040002EB RID: 747
		private float m_OutWeight;
	}
}
