using System;
using System.Collections.Generic;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200001A RID: 26
	[Obsolete("The matchmaker and relay feature will be removed in the future, minimal support will continue until this can be safely done.")]
	public class MatchInfoSnapshot
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004743 File Offset: 0x00002943
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000474B File Offset: 0x0000294B
		public NetworkID networkId { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000475C File Offset: 0x0000295C
		public NodeID hostNodeId { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00004765 File Offset: 0x00002965
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000476D File Offset: 0x0000296D
		public string name { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00004776 File Offset: 0x00002976
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000477E File Offset: 0x0000297E
		public int averageEloScore { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004787 File Offset: 0x00002987
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000478F File Offset: 0x0000298F
		public int maxSize { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00004798 File Offset: 0x00002998
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000047A0 File Offset: 0x000029A0
		public int currentSize { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000047A9 File Offset: 0x000029A9
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000047B1 File Offset: 0x000029B1
		public bool isPrivate { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000047BA File Offset: 0x000029BA
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000047C2 File Offset: 0x000029C2
		public Dictionary<string, long> matchAttributes { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000047CB File Offset: 0x000029CB
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000047D3 File Offset: 0x000029D3
		public List<MatchInfoSnapshot.MatchInfoDirectConnectSnapshot> directConnectInfos { get; private set; }

		// Token: 0x0600015C RID: 348 RVA: 0x00002371 File Offset: 0x00000571
		public MatchInfoSnapshot()
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000047DC File Offset: 0x000029DC
		internal MatchInfoSnapshot(MatchDesc matchDesc)
		{
			this.networkId = (NetworkID)matchDesc.networkId;
			this.hostNodeId = matchDesc.hostNodeId;
			this.name = matchDesc.name;
			this.averageEloScore = matchDesc.averageEloScore;
			this.maxSize = matchDesc.maxSize;
			this.currentSize = matchDesc.currentSize;
			this.isPrivate = matchDesc.isPrivate;
			this.matchAttributes = matchDesc.matchAttributes;
			this.directConnectInfos = new List<MatchInfoSnapshot.MatchInfoDirectConnectSnapshot>();
			foreach (MatchDirectConnectInfo matchDirectConnectInfo in matchDesc.directConnectInfos)
			{
				this.directConnectInfos.Add(new MatchInfoSnapshot.MatchInfoDirectConnectSnapshot(matchDirectConnectInfo));
			}
		}

		// Token: 0x0200001B RID: 27
		public class MatchInfoDirectConnectSnapshot
		{
			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600015E RID: 350 RVA: 0x000048BC File Offset: 0x00002ABC
			// (set) Token: 0x0600015F RID: 351 RVA: 0x000048C4 File Offset: 0x00002AC4
			public NodeID nodeId { get; private set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000160 RID: 352 RVA: 0x000048CD File Offset: 0x00002ACD
			// (set) Token: 0x06000161 RID: 353 RVA: 0x000048D5 File Offset: 0x00002AD5
			public string publicAddress { get; private set; }

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000162 RID: 354 RVA: 0x000048DE File Offset: 0x00002ADE
			// (set) Token: 0x06000163 RID: 355 RVA: 0x000048E6 File Offset: 0x00002AE6
			public string privateAddress { get; private set; }

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000164 RID: 356 RVA: 0x000048EF File Offset: 0x00002AEF
			// (set) Token: 0x06000165 RID: 357 RVA: 0x000048F7 File Offset: 0x00002AF7
			public HostPriority hostPriority { get; private set; }

			// Token: 0x06000166 RID: 358 RVA: 0x00002371 File Offset: 0x00000571
			public MatchInfoDirectConnectSnapshot()
			{
			}

			// Token: 0x06000167 RID: 359 RVA: 0x00004900 File Offset: 0x00002B00
			internal MatchInfoDirectConnectSnapshot(MatchDirectConnectInfo matchDirectConnectInfo)
			{
				this.nodeId = matchDirectConnectInfo.nodeId;
				this.publicAddress = matchDirectConnectInfo.publicAddress;
				this.privateAddress = matchDirectConnectInfo.privateAddress;
				this.hostPriority = matchDirectConnectInfo.hostPriority;
			}
		}
	}
}
