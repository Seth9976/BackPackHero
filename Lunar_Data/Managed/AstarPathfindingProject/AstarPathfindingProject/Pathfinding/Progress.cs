using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000029 RID: 41
	public readonly struct Progress
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00009BCA File Offset: 0x00007DCA
		public Progress(float progress, ScanningStage stage, int graphIndex = 0, int graphCount = 0)
		{
			this.progress = progress;
			this.stage = stage;
			this.graphIndex = graphIndex;
			this.graphCount = graphCount;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009BE9 File Offset: 0x00007DE9
		public Progress MapTo(float min, float max)
		{
			return new Progress(Mathf.Lerp(min, max, this.progress), this.stage, this.graphIndex, this.graphCount);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009C10 File Offset: 0x00007E10
		public override string ToString()
		{
			string text = this.progress.ToString("0%") + " ";
			switch (this.stage)
			{
			case ScanningStage.PreProcessingGraphs:
				text += "Pre-processing graphs";
				break;
			case ScanningStage.PreProcessingGraph:
				text = string.Concat(new string[]
				{
					text,
					"Pre-processing graph ",
					(this.graphIndex + 1).ToString(),
					" of ",
					this.graphCount.ToString()
				});
				break;
			case ScanningStage.ScanningGraph:
				text = string.Concat(new string[]
				{
					text,
					"Scanning graph ",
					(this.graphIndex + 1).ToString(),
					" of ",
					this.graphCount.ToString()
				});
				break;
			case ScanningStage.PostProcessingGraph:
				text = string.Concat(new string[]
				{
					text,
					"Post-processing graph ",
					(this.graphIndex + 1).ToString(),
					" of ",
					this.graphCount.ToString()
				});
				break;
			case ScanningStage.FinishingScans:
				text += "Finalizing graph scans";
				break;
			}
			return text;
		}

		// Token: 0x04000145 RID: 325
		public readonly float progress;

		// Token: 0x04000146 RID: 326
		internal readonly ScanningStage stage;

		// Token: 0x04000147 RID: 327
		internal readonly int graphIndex;

		// Token: 0x04000148 RID: 328
		internal readonly int graphCount;
	}
}
