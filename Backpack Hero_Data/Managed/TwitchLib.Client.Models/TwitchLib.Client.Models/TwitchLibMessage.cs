using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200001E RID: 30
	public abstract class TwitchLibMessage
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006A74 File Offset: 0x00004C74
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00006A7C File Offset: 0x00004C7C
		public List<KeyValuePair<string, string>> Badges { get; protected set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006A85 File Offset: 0x00004C85
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006A8D File Offset: 0x00004C8D
		public string BotUsername { get; protected set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006A96 File Offset: 0x00004C96
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006A9E File Offset: 0x00004C9E
		public Color Color { get; protected set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006AA7 File Offset: 0x00004CA7
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00006AAF File Offset: 0x00004CAF
		public string ColorHex { get; protected set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006AB8 File Offset: 0x00004CB8
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public string DisplayName { get; protected set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006AC9 File Offset: 0x00004CC9
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006AD1 File Offset: 0x00004CD1
		public EmoteSet EmoteSet { get; protected set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006ADA File Offset: 0x00004CDA
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006AE2 File Offset: 0x00004CE2
		public bool IsTurbo { get; protected set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006AEB File Offset: 0x00004CEB
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006AF3 File Offset: 0x00004CF3
		public string UserId { get; protected set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006AFC File Offset: 0x00004CFC
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006B04 File Offset: 0x00004D04
		public string Username { get; protected set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006B0D File Offset: 0x00004D0D
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00006B15 File Offset: 0x00004D15
		public UserType UserType { get; protected set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00006B1E File Offset: 0x00004D1E
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00006B26 File Offset: 0x00004D26
		public string RawIrcMessage { get; protected set; }
	}
}
