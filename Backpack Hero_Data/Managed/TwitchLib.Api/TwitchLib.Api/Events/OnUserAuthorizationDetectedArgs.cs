using System;
using System.Collections.Generic;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Events
{
	// Token: 0x02000024 RID: 36
	public class OnUserAuthorizationDetectedArgs
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003AC1 File Offset: 0x00001CC1
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003AC9 File Offset: 0x00001CC9
		public string Id { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003AD2 File Offset: 0x00001CD2
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003ADA File Offset: 0x00001CDA
		public List<AuthScopes> Scopes { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003AE3 File Offset: 0x00001CE3
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003AEB File Offset: 0x00001CEB
		public string Username { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003AF4 File Offset: 0x00001CF4
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003AFC File Offset: 0x00001CFC
		public string Token { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003B05 File Offset: 0x00001D05
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00003B0D File Offset: 0x00001D0D
		public string Refresh { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003B16 File Offset: 0x00001D16
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00003B1E File Offset: 0x00001D1E
		public string ClientId { get; set; }
	}
}
