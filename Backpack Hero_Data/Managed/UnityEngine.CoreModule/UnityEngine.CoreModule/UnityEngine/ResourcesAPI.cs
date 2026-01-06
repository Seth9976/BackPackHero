using System;

namespace UnityEngine
{
	// Token: 0x020001E9 RID: 489
	public class ResourcesAPI
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00023509 File Offset: 0x00021709
		internal static ResourcesAPI ActiveAPI
		{
			get
			{
				return ResourcesAPI.overrideAPI ?? ResourcesAPI.s_DefaultAPI;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00023519 File Offset: 0x00021719
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x00023520 File Offset: 0x00021720
		public static ResourcesAPI overrideAPI { get; set; }

		// Token: 0x06001618 RID: 5656 RVA: 0x00008C2F File Offset: 0x00006E2F
		protected internal ResourcesAPI()
		{
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00023528 File Offset: 0x00021728
		protected internal virtual Object[] FindObjectsOfTypeAll(Type systemTypeInstance)
		{
			return ResourcesAPIInternal.FindObjectsOfTypeAll(systemTypeInstance);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00023530 File Offset: 0x00021730
		protected internal virtual Shader FindShaderByName(string name)
		{
			return ResourcesAPIInternal.FindShaderByName(name);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00023538 File Offset: 0x00021738
		protected internal virtual Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.Load(path, systemTypeInstance);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00023541 File Offset: 0x00021741
		protected internal virtual Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPIInternal.LoadAll(path, systemTypeInstance);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0002354C File Offset: 0x0002174C
		protected internal virtual ResourceRequest LoadAsync(string path, Type systemTypeInstance)
		{
			ResourceRequest resourceRequest = ResourcesAPIInternal.LoadAsyncInternal(path, systemTypeInstance);
			resourceRequest.m_Path = path;
			resourceRequest.m_Type = systemTypeInstance;
			return resourceRequest;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00023575 File Offset: 0x00021775
		protected internal virtual void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPIInternal.UnloadAsset(assetToUnload);
		}

		// Token: 0x040007C4 RID: 1988
		private static ResourcesAPI s_DefaultAPI = new ResourcesAPI();
	}
}
