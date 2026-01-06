using System;
using System.Diagnostics;

namespace Unity.VisualScripting
{
	// Token: 0x020000C6 RID: 198
	public class ProfiledSegment
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000AC5C File Offset: 0x00008E5C
		public ProfiledSegment(ProfiledSegment parent, string name)
		{
			this.parent = parent;
			this.name = name;
			this.stopwatch = new Stopwatch();
			this.children = new ProfiledSegmentCollection();
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000AC88 File Offset: 0x00008E88
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000AC90 File Offset: 0x00008E90
		public string name { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000AC99 File Offset: 0x00008E99
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		public Stopwatch stopwatch { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000ACAA File Offset: 0x00008EAA
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		public long calls { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000ACBB File Offset: 0x00008EBB
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		public ProfiledSegment parent { get; private set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000ACCC File Offset: 0x00008ECC
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public ProfiledSegmentCollection children { get; private set; }
	}
}
