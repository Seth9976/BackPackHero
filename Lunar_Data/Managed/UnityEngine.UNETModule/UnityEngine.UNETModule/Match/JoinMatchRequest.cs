using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000027 RID: 39
	internal class JoinMatchRequest : Request
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005A06 File Offset: 0x00003C06
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00005A0E File Offset: 0x00003C0E
		public NetworkID networkId { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005A17 File Offset: 0x00003C17
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00005A1F File Offset: 0x00003C1F
		public string publicAddress { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005A28 File Offset: 0x00003C28
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00005A30 File Offset: 0x00003C30
		public string privateAddress { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005A39 File Offset: 0x00003C39
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005A41 File Offset: 0x00003C41
		public int eloScore { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005A4A File Offset: 0x00003C4A
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005A52 File Offset: 0x00003C52
		public string password { get; set; }

		// Token: 0x060001C7 RID: 455 RVA: 0x00005A5C File Offset: 0x00003C5C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-networkId:0x{1},publicAddress:{2},privateAddress:{3},eloScore:{4},HasPassword:{5}", new object[]
			{
				base.ToString(),
				this.networkId.ToString("X"),
				this.publicAddress,
				this.privateAddress,
				this.eloScore,
				string.IsNullOrEmpty(this.password) ? "NO" : "YES"
			});
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005ADC File Offset: 0x00003CDC
		public override bool IsValid()
		{
			return base.IsValid() && this.networkId != NetworkID.Invalid;
		}
	}
}
