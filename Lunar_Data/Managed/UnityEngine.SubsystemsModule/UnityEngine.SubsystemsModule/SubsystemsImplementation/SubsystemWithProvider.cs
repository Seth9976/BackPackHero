using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000019 RID: 25
	public abstract class SubsystemWithProvider : ISubsystem
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00002D14 File Offset: 0x00000F14
		public void Start()
		{
			bool running = this.running;
			if (!running)
			{
				this.OnStart();
				this.providerBase.m_Running = true;
				this.running = true;
			}
		}

		// Token: 0x0600007E RID: 126
		protected abstract void OnStart();

		// Token: 0x0600007F RID: 127 RVA: 0x00002D4C File Offset: 0x00000F4C
		public void Stop()
		{
			bool flag = !this.running;
			if (!flag)
			{
				this.OnStop();
				this.providerBase.m_Running = false;
				this.running = false;
			}
		}

		// Token: 0x06000080 RID: 128
		protected abstract void OnStop();

		// Token: 0x06000081 RID: 129 RVA: 0x00002D84 File Offset: 0x00000F84
		public void Destroy()
		{
			this.Stop();
			bool flag = SubsystemManager.RemoveStandaloneSubsystem(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		// Token: 0x06000082 RID: 130
		protected abstract void OnDestroy();

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002DAA File Offset: 0x00000FAA
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public bool running { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002DBB File Offset: 0x00000FBB
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002DC3 File Offset: 0x00000FC3
		internal SubsystemProvider providerBase { get; set; }

		// Token: 0x06000087 RID: 135
		internal abstract void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider subsystemProvider);

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000088 RID: 136
		internal abstract SubsystemDescriptorWithProvider descriptor { get; }
	}
}
