using System;

namespace UnityEngine
{
	// Token: 0x0200023D RID: 573
	public static class Snapping
	{
		// Token: 0x06001896 RID: 6294 RVA: 0x00027E24 File Offset: 0x00026024
		internal static bool IsCardinalDirection(Vector3 direction)
		{
			return (Mathf.Abs(direction.x) > 0f && Mathf.Approximately(direction.y, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.y) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.z, 0f)) || (Mathf.Abs(direction.z) > 0f && Mathf.Approximately(direction.x, 0f) && Mathf.Approximately(direction.y, 0f));
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00027EDC File Offset: 0x000260DC
		public static float Snap(float val, float snap)
		{
			bool flag = snap == 0f;
			float num;
			if (flag)
			{
				num = val;
			}
			else
			{
				num = snap * Mathf.Round(val / snap);
			}
			return num;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00027F08 File Offset: 0x00026108
		public static Vector2 Snap(Vector2 val, Vector2 snap)
		{
			return new Vector3((Mathf.Abs(snap.x) < Mathf.Epsilon) ? val.x : (snap.x * Mathf.Round(val.x / snap.x)), (Mathf.Abs(snap.y) < Mathf.Epsilon) ? val.y : (snap.y * Mathf.Round(val.y / snap.y)));
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00027F8C File Offset: 0x0002618C
		public static Vector3 Snap(Vector3 val, Vector3 snap, SnapAxis axis = SnapAxis.All)
		{
			return new Vector3(((axis & SnapAxis.X) == SnapAxis.X) ? Snapping.Snap(val.x, snap.x) : val.x, ((axis & SnapAxis.Y) == SnapAxis.Y) ? Snapping.Snap(val.y, snap.y) : val.y, ((axis & SnapAxis.Z) == SnapAxis.Z) ? Snapping.Snap(val.z, snap.z) : val.z);
		}
	}
}
