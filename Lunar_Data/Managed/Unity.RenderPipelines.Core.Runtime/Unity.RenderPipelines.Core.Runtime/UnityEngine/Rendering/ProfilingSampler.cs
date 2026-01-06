using System;
using UnityEngine.Profiling;

namespace UnityEngine.Rendering
{
	// Token: 0x02000071 RID: 113
	public class ProfilingSampler
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00011778 File Offset: 0x0000F978
		public static ProfilingSampler Get<TEnum>(TEnum marker) where TEnum : Enum
		{
			TProfilingSampler<TEnum> tprofilingSampler;
			TProfilingSampler<TEnum>.samples.TryGetValue(marker, out tprofilingSampler);
			return tprofilingSampler;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011794 File Offset: 0x0000F994
		public ProfilingSampler(string name)
		{
			this.sampler = CustomSampler.Create(name, true);
			this.inlineSampler = CustomSampler.Create("Inl_" + name, false);
			this.name = name;
			this.m_Recorder = this.sampler.GetRecorder();
			this.m_Recorder.enabled = false;
			this.m_InlineRecorder = this.inlineSampler.GetRecorder();
			this.m_InlineRecorder.enabled = false;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001180C File Offset: 0x0000FA0C
		public void Begin(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.BeginSample(this.sampler);
					return;
				}
				cmd.BeginSample(this.name);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001183F File Offset: 0x0000FA3F
		public void End(CommandBuffer cmd)
		{
			if (cmd != null)
			{
				if (this.sampler != null && this.sampler.isValid)
				{
					cmd.EndSample(this.sampler);
					return;
				}
				cmd.EndSample(this.name);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00011872 File Offset: 0x0000FA72
		internal bool IsValid()
		{
			return this.sampler != null && this.inlineSampler != null;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00011887 File Offset: 0x0000FA87
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001188F File Offset: 0x0000FA8F
		internal CustomSampler sampler { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00011898 File Offset: 0x0000FA98
		// (set) Token: 0x0600039F RID: 927 RVA: 0x000118A0 File Offset: 0x0000FAA0
		internal CustomSampler inlineSampler { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000118A9 File Offset: 0x0000FAA9
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000118B1 File Offset: 0x0000FAB1
		public string name { get; private set; }

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000118BA File Offset: 0x0000FABA
		public bool enableRecording
		{
			set
			{
				this.m_Recorder.enabled = value;
				this.m_InlineRecorder.enabled = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000118D4 File Offset: 0x0000FAD4
		public float gpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.gpuElapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x000118FB File Offset: 0x0000FAFB
		public int gpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.gpuSampleBlockCount;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00011917 File Offset: 0x0000FB17
		public float cpuElapsedTime
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_Recorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0001193E File Offset: 0x0000FB3E
		public int cpuSampleCount
		{
			get
			{
				if (!this.m_Recorder.enabled)
				{
					return 0;
				}
				return this.m_Recorder.sampleBlockCount;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0001195A File Offset: 0x0000FB5A
		public float inlineCpuElapsedTime
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0f;
				}
				return (float)this.m_InlineRecorder.elapsedNanoseconds / 1000000f;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00011981 File Offset: 0x0000FB81
		public int inlineCpuSampleCount
		{
			get
			{
				if (!this.m_InlineRecorder.enabled)
				{
					return 0;
				}
				return this.m_InlineRecorder.sampleBlockCount;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001199D File Offset: 0x0000FB9D
		private ProfilingSampler()
		{
		}

		// Token: 0x04000249 RID: 585
		private Recorder m_Recorder;

		// Token: 0x0400024A RID: 586
		private Recorder m_InlineRecorder;
	}
}
