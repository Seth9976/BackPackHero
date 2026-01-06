using System;

namespace System.IO
{
	// Token: 0x02000B30 RID: 2864
	public class EnumerationOptions
	{
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x0015FEF8 File Offset: 0x0015E0F8
		internal static EnumerationOptions Compatible { get; } = new EnumerationOptions
		{
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x0015FEFF File Offset: 0x0015E0FF
		private static EnumerationOptions CompatibleRecursive { get; } = new EnumerationOptions
		{
			RecurseSubdirectories = true,
			MatchType = MatchType.Win32,
			AttributesToSkip = (FileAttributes)0,
			IgnoreInaccessible = false
		};

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x0015FF06 File Offset: 0x0015E106
		internal static EnumerationOptions Default { get; } = new EnumerationOptions();

		// Token: 0x06006741 RID: 26433 RVA: 0x0015FF0D File Offset: 0x0015E10D
		public EnumerationOptions()
		{
			this.IgnoreInaccessible = true;
			this.AttributesToSkip = FileAttributes.Hidden | FileAttributes.System;
		}

		// Token: 0x06006742 RID: 26434 RVA: 0x0015FF23 File Offset: 0x0015E123
		internal static EnumerationOptions FromSearchOption(SearchOption searchOption)
		{
			if (searchOption != SearchOption.TopDirectoryOnly && searchOption != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("searchOption", "Enum value was out of legal range.");
			}
			if (searchOption != SearchOption.AllDirectories)
			{
				return EnumerationOptions.Compatible;
			}
			return EnumerationOptions.CompatibleRecursive;
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x06006743 RID: 26435 RVA: 0x0015FF4B File Offset: 0x0015E14B
		// (set) Token: 0x06006744 RID: 26436 RVA: 0x0015FF53 File Offset: 0x0015E153
		public bool RecurseSubdirectories { get; set; }

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x0015FF5C File Offset: 0x0015E15C
		// (set) Token: 0x06006746 RID: 26438 RVA: 0x0015FF64 File Offset: 0x0015E164
		public bool IgnoreInaccessible { get; set; }

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x0015FF6D File Offset: 0x0015E16D
		// (set) Token: 0x06006748 RID: 26440 RVA: 0x0015FF75 File Offset: 0x0015E175
		public int BufferSize { get; set; }

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x0015FF7E File Offset: 0x0015E17E
		// (set) Token: 0x0600674A RID: 26442 RVA: 0x0015FF86 File Offset: 0x0015E186
		public FileAttributes AttributesToSkip { get; set; }

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600674B RID: 26443 RVA: 0x0015FF8F File Offset: 0x0015E18F
		// (set) Token: 0x0600674C RID: 26444 RVA: 0x0015FF97 File Offset: 0x0015E197
		public MatchType MatchType { get; set; }

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x0600674D RID: 26445 RVA: 0x0015FFA0 File Offset: 0x0015E1A0
		// (set) Token: 0x0600674E RID: 26446 RVA: 0x0015FFA8 File Offset: 0x0015E1A8
		public MatchCasing MatchCasing { get; set; }

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x0015FFB1 File Offset: 0x0015E1B1
		// (set) Token: 0x06006750 RID: 26448 RVA: 0x0015FFB9 File Offset: 0x0015E1B9
		public bool ReturnSpecialDirectories { get; set; }
	}
}
