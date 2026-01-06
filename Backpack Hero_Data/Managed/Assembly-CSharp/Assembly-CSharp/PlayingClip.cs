using System;

// Token: 0x0200016D RID: 365
public struct PlayingClip
{
	// Token: 0x06000ECB RID: 3787 RVA: 0x0009355A File Offset: 0x0009175A
	public PlayingClip(string name, double timeStarted, double length)
	{
		this.name = name;
		this.timeStarted = timeStarted;
		this.length = length;
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00093571 File Offset: 0x00091771
	public readonly string name { get; }

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00093579 File Offset: 0x00091779
	public readonly double timeStarted { get; }

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00093581 File Offset: 0x00091781
	public readonly double length { get; }
}
