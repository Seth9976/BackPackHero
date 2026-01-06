using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000036 RID: 54
	internal class ShadowCasterGroup2DManager
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00011128 File Offset: 0x0000F328
		public static List<ShadowCasterGroup2D> shadowCasterGroups
		{
			get
			{
				return ShadowCasterGroup2DManager.s_ShadowCasterGroups;
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00011130 File Offset: 0x0000F330
		public static void CacheValues()
		{
			if (ShadowCasterGroup2DManager.shadowCasterGroups != null)
			{
				for (int i = 0; i < ShadowCasterGroup2DManager.shadowCasterGroups.Count; i++)
				{
					if (ShadowCasterGroup2DManager.shadowCasterGroups[i] != null)
					{
						ShadowCasterGroup2DManager.shadowCasterGroups[i].CacheValues();
					}
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0001117C File Offset: 0x0000F37C
		public static void AddShadowCasterGroupToList(ShadowCasterGroup2D shadowCaster, List<ShadowCasterGroup2D> list)
		{
			int num = 0;
			while (num < list.Count && shadowCaster.GetShadowGroup() != list[num].GetShadowGroup())
			{
				num++;
			}
			list.Insert(num, shadowCaster);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public static void RemoveShadowCasterGroupFromList(ShadowCasterGroup2D shadowCaster, List<ShadowCasterGroup2D> list)
		{
			list.Remove(shadowCaster);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000111C4 File Offset: 0x0000F3C4
		private static CompositeShadowCaster2D FindTopMostCompositeShadowCaster(ShadowCaster2D shadowCaster)
		{
			CompositeShadowCaster2D compositeShadowCaster2D = null;
			Transform transform = shadowCaster.transform.parent;
			while (transform != null)
			{
				CompositeShadowCaster2D compositeShadowCaster2D2;
				if (transform.TryGetComponent<CompositeShadowCaster2D>(out compositeShadowCaster2D2))
				{
					compositeShadowCaster2D = compositeShadowCaster2D2;
				}
				transform = transform.parent;
			}
			return compositeShadowCaster2D;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00011200 File Offset: 0x0000F400
		public static bool AddToShadowCasterGroup(ShadowCaster2D shadowCaster, ref ShadowCasterGroup2D shadowCasterGroup)
		{
			ShadowCasterGroup2D shadowCasterGroup2D = ShadowCasterGroup2DManager.FindTopMostCompositeShadowCaster(shadowCaster);
			if (shadowCasterGroup2D == null)
			{
				shadowCasterGroup2D = shadowCaster.GetComponent<ShadowCaster2D>();
			}
			if (shadowCasterGroup2D != null && shadowCasterGroup != shadowCasterGroup2D)
			{
				shadowCasterGroup2D.RegisterShadowCaster2D(shadowCaster);
				shadowCasterGroup = shadowCasterGroup2D;
				return true;
			}
			return false;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00011244 File Offset: 0x0000F444
		public static void RemoveFromShadowCasterGroup(ShadowCaster2D shadowCaster, ShadowCasterGroup2D shadowCasterGroup)
		{
			if (shadowCasterGroup != null)
			{
				shadowCasterGroup.UnregisterShadowCaster2D(shadowCaster);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00011256 File Offset: 0x0000F456
		public static void AddGroup(ShadowCasterGroup2D group)
		{
			if (group == null)
			{
				return;
			}
			if (ShadowCasterGroup2DManager.s_ShadowCasterGroups == null)
			{
				ShadowCasterGroup2DManager.s_ShadowCasterGroups = new List<ShadowCasterGroup2D>();
			}
			ShadowCasterGroup2DManager.AddShadowCasterGroupToList(group, ShadowCasterGroup2DManager.s_ShadowCasterGroups);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0001127E File Offset: 0x0000F47E
		public static void RemoveGroup(ShadowCasterGroup2D group)
		{
			if (group != null && ShadowCasterGroup2DManager.s_ShadowCasterGroups != null)
			{
				ShadowCasterGroup2DManager.RemoveShadowCasterGroupFromList(group, ShadowCasterGroup2DManager.s_ShadowCasterGroups);
			}
		}

		// Token: 0x04000174 RID: 372
		private static List<ShadowCasterGroup2D> s_ShadowCasterGroups;
	}
}
