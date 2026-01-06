using System;

namespace TwitchLib.Api.Helix.Models.Helpers
{
	// Token: 0x02000072 RID: 114
	public class ExtensionAnalytics
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00003E8A File Offset: 0x0000208A
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00003E92 File Offset: 0x00002092
		public string Date { get; protected set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00003E9B File Offset: 0x0000209B
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00003EA3 File Offset: 0x000020A3
		public string ExtensionName { get; protected set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00003EAC File Offset: 0x000020AC
		// (set) Token: 0x06000396 RID: 918 RVA: 0x00003EB4 File Offset: 0x000020B4
		public string ExtensionClientId { get; protected set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00003EBD File Offset: 0x000020BD
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00003EC5 File Offset: 0x000020C5
		public int Installs { get; protected set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00003ECE File Offset: 0x000020CE
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00003ED6 File Offset: 0x000020D6
		public int Uninstalls { get; protected set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600039B RID: 923 RVA: 0x00003EDF File Offset: 0x000020DF
		// (set) Token: 0x0600039C RID: 924 RVA: 0x00003EE7 File Offset: 0x000020E7
		public int Activations { get; protected set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00003EF0 File Offset: 0x000020F0
		// (set) Token: 0x0600039E RID: 926 RVA: 0x00003EF8 File Offset: 0x000020F8
		public int UniqueActiveChannels { get; protected set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00003F01 File Offset: 0x00002101
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x00003F09 File Offset: 0x00002109
		public int Renders { get; protected set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00003F12 File Offset: 0x00002112
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00003F1A File Offset: 0x0000211A
		public int UniqueRenders { get; protected set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00003F23 File Offset: 0x00002123
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00003F2B File Offset: 0x0000212B
		public int Views { get; protected set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00003F34 File Offset: 0x00002134
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x00003F3C File Offset: 0x0000213C
		public int UniqueViewers { get; protected set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00003F45 File Offset: 0x00002145
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x00003F4D File Offset: 0x0000214D
		public int UniqueInteractors { get; protected set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00003F56 File Offset: 0x00002156
		// (set) Token: 0x060003AA RID: 938 RVA: 0x00003F5E File Offset: 0x0000215E
		public int Clicks { get; protected set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00003F67 File Offset: 0x00002167
		// (set) Token: 0x060003AC RID: 940 RVA: 0x00003F6F File Offset: 0x0000216F
		public double ClicksPerInteractor { get; protected set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00003F78 File Offset: 0x00002178
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00003F80 File Offset: 0x00002180
		public double InteractionRate { get; protected set; }

		// Token: 0x060003AF RID: 943 RVA: 0x00003F8C File Offset: 0x0000218C
		public ExtensionAnalytics(string row)
		{
			string[] array = row.Split(new char[] { ',' });
			this.Date = array[0];
			this.ExtensionName = array[1];
			this.ExtensionClientId = array[2];
			this.Installs = int.Parse(array[3]);
			this.Uninstalls = int.Parse(array[4]);
			this.Activations = int.Parse(array[5]);
			this.UniqueActiveChannels = int.Parse(array[6]);
			this.Renders = int.Parse(array[7]);
			this.UniqueRenders = int.Parse(array[8]);
			this.Views = int.Parse(array[9]);
			this.UniqueViewers = int.Parse(array[10]);
			this.UniqueInteractors = int.Parse(array[11]);
			this.Clicks = int.Parse(array[12]);
			this.ClicksPerInteractor = double.Parse(array[13]);
			this.InteractionRate = double.Parse(array[14]);
		}
	}
}
