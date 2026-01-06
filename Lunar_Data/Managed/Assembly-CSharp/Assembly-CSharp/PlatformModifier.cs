using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class PlatformModifier : MonoBehaviour
{
	// Token: 0x06000304 RID: 772 RVA: 0x0000F5BC File Offset: 0x0000D7BC
	private void OnEnable()
	{
		PlatformWrapper.Platform currentPlatform = PlatformWrapper.GetCurrentPlatform();
		foreach (PlatformModifier.ModificationList modificationList in this.modifications)
		{
			if (modificationList.platform == currentPlatform)
			{
				foreach (GameObject gameObject in modificationList.enable)
				{
					try
					{
						gameObject.SetActive(true);
					}
					catch
					{
						string text = "PlatformModifier: Could not enable object ";
						GameObject gameObject2 = gameObject;
						Debug.Log(text + ((gameObject2 != null) ? gameObject2.ToString() : null));
					}
				}
				foreach (GameObject gameObject3 in modificationList.add)
				{
					try
					{
						Object.Instantiate<GameObject>(gameObject3);
					}
					catch
					{
						string text2 = "PlatformModifier: Could not add object ";
						GameObject gameObject4 = gameObject3;
						Debug.Log(text2 + ((gameObject4 != null) ? gameObject4.ToString() : null));
					}
				}
				foreach (GameObject gameObject5 in modificationList.remove)
				{
					try
					{
						Object.Destroy(gameObject5);
					}
					catch
					{
						string text3 = "PlatformModifier: Could not destroy object ";
						GameObject gameObject6 = gameObject5;
						Debug.Log(text3 + ((gameObject6 != null) ? gameObject6.ToString() : null));
					}
				}
				foreach (GameObject gameObject7 in modificationList.disable)
				{
					try
					{
						gameObject7.SetActive(false);
					}
					catch
					{
						string text4 = "PlatformModifier: Could not disable object ";
						GameObject gameObject8 = gameObject7;
						Debug.Log(text4 + ((gameObject8 != null) ? gameObject8.ToString() : null));
					}
				}
			}
		}
	}

	// Token: 0x0400024F RID: 591
	public List<PlatformModifier.ModificationList> modifications = new List<PlatformModifier.ModificationList>();

	// Token: 0x020000F6 RID: 246
	[Serializable]
	public class ModificationList
	{
		// Token: 0x0400048D RID: 1165
		public PlatformWrapper.Platform platform = PlatformWrapper.Platform.Unknown;

		// Token: 0x0400048E RID: 1166
		public bool skipInEditor;

		// Token: 0x0400048F RID: 1167
		[SerializeField]
		public List<GameObject> remove = new List<GameObject>();

		// Token: 0x04000490 RID: 1168
		[SerializeField]
		public List<GameObject> disable = new List<GameObject>();

		// Token: 0x04000491 RID: 1169
		[SerializeField]
		public List<GameObject> enable = new List<GameObject>();

		// Token: 0x04000492 RID: 1170
		[SerializeField]
		public List<GameObject> add = new List<GameObject>();
	}
}
