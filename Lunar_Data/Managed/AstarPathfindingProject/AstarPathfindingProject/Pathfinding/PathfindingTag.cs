using System;

namespace Pathfinding
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	public struct PathfindingTag
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x000167B5 File Offset: 0x000149B5
		public PathfindingTag(uint value)
		{
			this.value = value;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000167BE File Offset: 0x000149BE
		public static implicit operator uint(PathfindingTag tag)
		{
			return tag.value;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000167C6 File Offset: 0x000149C6
		public static implicit operator PathfindingTag(uint tag)
		{
			return new PathfindingTag(tag);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000167D0 File Offset: 0x000149D0
		public static PathfindingTag FromName(string tagName)
		{
			AstarPath.FindAstarPath();
			if (AstarPath.active == null)
			{
				throw new InvalidOperationException("There's no AstarPath component in the scene. Cannot get tag names.");
			}
			int num = Array.IndexOf<string>(AstarPath.active.GetTagNames(), tagName);
			if (num == -1)
			{
				throw new ArgumentException("There's no pathfinding tag with the name '" + tagName + "'");
			}
			return new PathfindingTag((uint)num);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00016829 File Offset: 0x00014A29
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x040002E7 RID: 743
		public uint value;
	}
}
