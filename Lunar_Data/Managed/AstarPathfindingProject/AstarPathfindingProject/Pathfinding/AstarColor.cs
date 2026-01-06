using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class AstarColor : ISerializationCallbackReceiver
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000095DC File Offset: 0x000077DC
		public static int ColorHash()
		{
			int num = AstarColor.SolidColor.GetHashCode() ^ AstarColor.UnwalkableNode.GetHashCode() ^ AstarColor.BoundsHandles.GetHashCode() ^ AstarColor.ConnectionLowLerp.GetHashCode() ^ AstarColor.ConnectionHighLerp.GetHashCode() ^ AstarColor.MeshEdgeColor.GetHashCode();
			for (int i = 0; i < AstarColor.AreaColors.Length; i++)
			{
				num = (7 * num) ^ AstarColor.AreaColors[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000967D File Offset: 0x0000787D
		public static Color GetAreaColor(uint area)
		{
			if ((ulong)area >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)area, 1f);
			}
			return AstarColor.AreaColors[(int)area];
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000967D File Offset: 0x0000787D
		public static Color GetTagColor(uint tag)
		{
			if ((ulong)tag >= (ulong)((long)AstarColor.AreaColors.Length))
			{
				return AstarMath.IntToColor((int)tag, 1f);
			}
			return AstarColor.AreaColors[(int)tag];
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000096A4 File Offset: 0x000078A4
		public void PushToStatic(AstarPath astar)
		{
			this._AreaColors = this._AreaColors ?? new Color[0];
			AstarColor.SolidColor = this._SolidColor;
			AstarColor.UnwalkableNode = this._UnwalkableNode;
			AstarColor.BoundsHandles = this._BoundsHandles;
			AstarColor.ConnectionLowLerp = this._ConnectionLowLerp;
			AstarColor.ConnectionHighLerp = this._ConnectionHighLerp;
			AstarColor.MeshEdgeColor = this._MeshEdgeColor;
			AstarColor.AreaColors = this._AreaColors;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000033F6 File Offset: 0x000015F6
		public void OnBeforeSerialize()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009714 File Offset: 0x00007914
		public void OnAfterDeserialize()
		{
			if (this._AreaColors != null && this._AreaColors.Length == 1 && this._AreaColors[0] == default(Color))
			{
				this._AreaColors = new Color[0];
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000975C File Offset: 0x0000795C
		public AstarColor()
		{
			this._SolidColor = new Color(0.11764706f, 0.4f, 0.7882353f, 0.9f);
			this._UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);
			this._BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);
			this._ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);
			this._ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);
			this._MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);
		}

		// Token: 0x0400011C RID: 284
		public Color _SolidColor;

		// Token: 0x0400011D RID: 285
		public Color _UnwalkableNode;

		// Token: 0x0400011E RID: 286
		public Color _BoundsHandles;

		// Token: 0x0400011F RID: 287
		public Color _ConnectionLowLerp;

		// Token: 0x04000120 RID: 288
		public Color _ConnectionHighLerp;

		// Token: 0x04000121 RID: 289
		public Color _MeshEdgeColor;

		// Token: 0x04000122 RID: 290
		public Color[] _AreaColors;

		// Token: 0x04000123 RID: 291
		public static Color SolidColor = new Color(0.11764706f, 0.4f, 0.7882353f, 0.9f);

		// Token: 0x04000124 RID: 292
		public static Color UnwalkableNode = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000125 RID: 293
		public static Color BoundsHandles = new Color(0.29f, 0.454f, 0.741f, 0.9f);

		// Token: 0x04000126 RID: 294
		public static Color ConnectionLowLerp = new Color(0f, 1f, 0f, 0.5f);

		// Token: 0x04000127 RID: 295
		public static Color ConnectionHighLerp = new Color(1f, 0f, 0f, 0.5f);

		// Token: 0x04000128 RID: 296
		public static Color MeshEdgeColor = new Color(0f, 0f, 0f, 0.5f);

		// Token: 0x04000129 RID: 297
		private static Color[] AreaColors = new Color[1];
	}
}
