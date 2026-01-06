using System;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x02000326 RID: 806
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values. </summary>
		// Token: 0x060019AD RID: 6573 RVA: 0x0005F580 File Offset: 0x0005D780
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name. </summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x060019AE RID: 6574 RVA: 0x00060B55 File Offset: 0x0005ED55
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.RegionMode = regionMode;
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00060B6B File Offset: 0x0005ED6B
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x00060B7C File Offset: 0x0005ED7C
		public string RegionText
		{
			get
			{
				return this._regionText ?? string.Empty;
			}
			set
			{
				this._regionText = value;
			}
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00060B85 File Offset: 0x0005ED85
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x00060B8D File Offset: 0x0005ED8D
		public CodeRegionMode RegionMode { get; set; }

		// Token: 0x04000DCD RID: 3533
		private string _regionText;
	}
}
