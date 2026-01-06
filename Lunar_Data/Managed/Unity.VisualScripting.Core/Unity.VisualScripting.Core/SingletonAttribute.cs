using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200014A RID: 330
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class SingletonAttribute : Attribute
	{
		// Token: 0x060008E2 RID: 2274 RVA: 0x00026D69 File Offset: 0x00024F69
		public SingletonAttribute()
		{
			this.HideFlags = HideFlags.None;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00026D78 File Offset: 0x00024F78
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00026D80 File Offset: 0x00024F80
		public bool Persistent { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00026D89 File Offset: 0x00024F89
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00026D91 File Offset: 0x00024F91
		public bool Automatic { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00026D9A File Offset: 0x00024F9A
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00026DA2 File Offset: 0x00024FA2
		public HideFlags HideFlags { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00026DAB File Offset: 0x00024FAB
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00026DB3 File Offset: 0x00024FB3
		public string Name { get; set; }
	}
}
