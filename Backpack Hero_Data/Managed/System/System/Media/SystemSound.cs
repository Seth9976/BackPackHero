using System;
using System.IO;
using Unity;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	/// <filterpriority>2</filterpriority>
	/// <completionlist cref="T:System.Media.SystemSounds" />
	// Token: 0x02000178 RID: 376
	public class SystemSound
	{
		// Token: 0x06000A0E RID: 2574 RVA: 0x0002C269 File Offset: 0x0002A469
		internal SystemSound(string tag)
		{
			this.resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x06000A0F RID: 2575 RVA: 0x0002C296 File Offset: 0x0002A496
		public void Play()
		{
			new SoundPlayer(this.resource).Play();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00013B26 File Offset: 0x00011D26
		internal SystemSound()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040006C4 RID: 1732
		private Stream resource;
	}
}
