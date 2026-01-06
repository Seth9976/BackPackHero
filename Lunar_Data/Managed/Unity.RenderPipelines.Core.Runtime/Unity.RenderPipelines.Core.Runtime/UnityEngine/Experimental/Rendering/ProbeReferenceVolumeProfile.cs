using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000010 RID: 16
	public sealed class ProbeReferenceVolumeProfile : ScriptableObject
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000061A5 File Offset: 0x000043A5
		public int cellSizeInBricks
		{
			get
			{
				return (int)Mathf.Pow(3f, (float)this.simplificationLevels);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000061B9 File Offset: 0x000043B9
		public int maxSubdivision
		{
			get
			{
				return this.simplificationLevels + 1;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000061C3 File Offset: 0x000043C3
		public float minBrickSize
		{
			get
			{
				return Mathf.Max(0.01f, this.minDistanceBetweenProbes * 3f);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000061DB File Offset: 0x000043DB
		public float cellSizeInMeters
		{
			get
			{
				return (float)this.cellSizeInBricks * this.minBrickSize;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000061EB File Offset: 0x000043EB
		private void OnEnable()
		{
			ProbeReferenceVolumeProfile.Version version = this.version;
			CoreUtils.GetLastEnumValue<ProbeReferenceVolumeProfile.Version>();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000061FA File Offset: 0x000043FA
		public bool IsEquivalent(ProbeReferenceVolumeProfile otherProfile)
		{
			return this.minDistanceBetweenProbes == otherProfile.minDistanceBetweenProbes && this.cellSizeInMeters == otherProfile.cellSizeInMeters && this.simplificationLevels == otherProfile.simplificationLevels;
		}

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private ProbeReferenceVolumeProfile.Version version = CoreUtils.GetLastEnumValue<ProbeReferenceVolumeProfile.Version>();

		// Token: 0x0400007B RID: 123
		[Range(2f, 5f)]
		public int simplificationLevels = 3;

		// Token: 0x0400007C RID: 124
		[Min(0.1f)]
		public float minDistanceBetweenProbes = 1f;

		// Token: 0x02000120 RID: 288
		internal enum Version
		{
			// Token: 0x040004AC RID: 1196
			Initial
		}
	}
}
