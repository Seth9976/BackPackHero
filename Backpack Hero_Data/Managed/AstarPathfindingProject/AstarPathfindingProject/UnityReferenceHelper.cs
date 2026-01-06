using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009D RID: 157
	[ExecuteInEditMode]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_unity_reference_helper.php")]
	public class UnityReferenceHelper : MonoBehaviour
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x0002CDEF File Offset: 0x0002AFEF
		public string GetGUID()
		{
			return this.guid;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0002CDF7 File Offset: 0x0002AFF7
		public void Awake()
		{
			this.Reset();
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0002CE00 File Offset: 0x0002B000
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
				foreach (UnityReferenceHelper unityReferenceHelper in Object.FindObjectsOfType(typeof(UnityReferenceHelper)) as UnityReferenceHelper[])
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

		// Token: 0x0400042F RID: 1071
		[HideInInspector]
		[SerializeField]
		private string guid;
	}
}
