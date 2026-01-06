using System;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000023 RID: 35
	internal class ScheduleRuntimeClip : RuntimeClipBase
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000803E File Offset: 0x0000623E
		public override double start
		{
			get
			{
				return Math.Max(0.0, this.m_Clip.start - this.m_StartDelay);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008060 File Offset: 0x00006260
		public override double duration
		{
			get
			{
				return this.m_Clip.duration + this.m_FinishTail + this.m_Clip.start - this.start;
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00008087 File Offset: 0x00006287
		public void SetTime(double time)
		{
			this.m_Playable.SetTime(time);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008095 File Offset: 0x00006295
		public TimelineClip clip
		{
			get
			{
				return this.m_Clip;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000809D File Offset: 0x0000629D
		public Playable mixer
		{
			get
			{
				return this.m_ParentMixer;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000080A5 File Offset: 0x000062A5
		public Playable playable
		{
			get
			{
				return this.m_Playable;
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000080AD File Offset: 0x000062AD
		public ScheduleRuntimeClip(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay = 0.2, double finishTail = 0.1)
		{
			this.Create(clip, clipPlayable, parentMixer, startDelay, finishTail);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000080C2 File Offset: 0x000062C2
		private void Create(TimelineClip clip, Playable clipPlayable, Playable parentMixer, double startDelay, double finishTail)
		{
			this.m_Clip = clip;
			this.m_Playable = clipPlayable;
			this.m_ParentMixer = parentMixer;
			this.m_StartDelay = startDelay;
			this.m_FinishTail = finishTail;
			clipPlayable.Pause<Playable>();
		}

		// Token: 0x170000A4 RID: 164
		// (set) Token: 0x06000220 RID: 544 RVA: 0x000080F0 File Offset: 0x000062F0
		public override bool enable
		{
			set
			{
				if (value && this.m_Playable.GetPlayState<Playable>() != PlayState.Playing)
				{
					this.m_Playable.Play<Playable>();
				}
				else if (!value && this.m_Playable.GetPlayState<Playable>() != PlayState.Paused)
				{
					this.m_Playable.Pause<Playable>();
					if (this.m_ParentMixer.IsValid<Playable>())
					{
						this.m_ParentMixer.SetInputWeight(this.m_Playable, 0f);
					}
				}
				this.m_Started = this.m_Started && value;
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008168 File Offset: 0x00006368
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			if (frameData.timeHeld)
			{
				this.enable = false;
				return;
			}
			bool flag = frameData.seekOccurred || frameData.timeLooped || frameData.evaluationType == FrameData.EvaluationType.Evaluate;
			if (localTime > this.start + this.duration - this.m_FinishTail)
			{
				return;
			}
			float num = this.clip.EvaluateMixIn(localTime) * this.clip.EvaluateMixOut(localTime);
			if (this.mixer.IsValid<Playable>())
			{
				this.mixer.SetInputWeight(this.playable, num);
			}
			if (!this.m_Started || flag)
			{
				double num2 = this.clip.ToLocalTime(Math.Max(localTime, this.clip.start));
				double num3 = Math.Max(this.clip.start - localTime, 0.0) * this.clip.timeScale;
				double num4 = this.m_Clip.duration * this.clip.timeScale;
				if (this.m_Playable.IsPlayableOfType<AudioClipPlayable>())
				{
					((AudioClipPlayable)this.m_Playable).Seek(num2, num3, num4);
				}
				this.m_Started = true;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00008291 File Offset: 0x00006491
		public override void DisableAt(double localTime, double rootDuration, FrameData frameData)
		{
			this.enable = false;
		}

		// Token: 0x040000C0 RID: 192
		private TimelineClip m_Clip;

		// Token: 0x040000C1 RID: 193
		private Playable m_Playable;

		// Token: 0x040000C2 RID: 194
		private Playable m_ParentMixer;

		// Token: 0x040000C3 RID: 195
		private double m_StartDelay;

		// Token: 0x040000C4 RID: 196
		private double m_FinishTail;

		// Token: 0x040000C5 RID: 197
		private bool m_Started;
	}
}
