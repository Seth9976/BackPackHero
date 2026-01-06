using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200003D RID: 61
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
	public sealed class InspectorTextAreaAttribute : Attribute
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004DE0 File Offset: 0x00002FE0
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00004DED File Offset: 0x00002FED
		public float minLines
		{
			get
			{
				return this._minLines.GetValueOrDefault();
			}
			set
			{
				this._minLines = new float?(value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004DFB File Offset: 0x00002FFB
		public bool hasMinLines
		{
			get
			{
				return this._minLines != null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00004E08 File Offset: 0x00003008
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00004E15 File Offset: 0x00003015
		public float maxLines
		{
			get
			{
				return this._maxLines.GetValueOrDefault();
			}
			set
			{
				this._maxLines = new float?(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00004E23 File Offset: 0x00003023
		public bool hasMaxLines
		{
			get
			{
				return this._maxLines != null;
			}
		}

		// Token: 0x0400003A RID: 58
		private float? _minLines;

		// Token: 0x0400003B RID: 59
		private float? _maxLines;
	}
}
