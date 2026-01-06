using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000166 RID: 358
	[ExecuteInEditMode]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/unityreferencehelper.html")]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0003B342 File Offset: 0x00039542
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0003B34A File Offset: 0x0003954A
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0003B354 File Offset: 0x00039554
		public void Reset()
		{
			if (string.IsNullOrEmpty(this.guid))
			{
				this.guid = Guid.NewGuid().ToString();
				Debug.Log("Created new GUID - " + this.guid, this);
				return;
			}
			if (base.gameObject.scene.name != null)
			{
				foreach (UnityReferenceHelper unityReferenceHelper in UnityCompatibility.FindObjectsByTypeUnsorted<UnityReferenceHelper>())
				{
					if (unityReferenceHelper != this && this.guid == unityReferenceHelper.guid)
					{
						this.guid = Guid.NewGuid().ToString();
						Debug.Log("Created new GUID - " + this.guid, this);
						return;
					}
				}
			}
		}

		// Token: 0x040006FF RID: 1791
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}
