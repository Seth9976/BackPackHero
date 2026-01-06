using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000137 RID: 311
[CreateAssetMenu(fileName = "npcOutfit", menuName = "ScriptableObjects/NPC Outfit")]
public class npcOutfit : ScriptableObject
{
	// Token: 0x06000BB6 RID: 2998 RVA: 0x0007B1FC File Offset: 0x000793FC
	private static bool CorrectDirection(List<npcOutfit.Direction> outfitDirection, npcOutfit.Direction direction)
	{
		return outfitDirection.Contains(direction) || outfitDirection.Contains(npcOutfit.Direction.Unspecified) || (outfitDirection.Contains(npcOutfit.Direction.Side) && (direction == npcOutfit.Direction.Left || direction == npcOutfit.Direction.Right));
	}

	// Token: 0x06000BB7 RID: 2999 RVA: 0x0007B226 File Offset: 0x00079426
	private static bool CorrectAnimation(List<npcOutfit.Animation> outfitAnimation, npcOutfit.Animation animation)
	{
		return outfitAnimation.Contains(animation) || outfitAnimation.Contains(npcOutfit.Animation.Unspecified);
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0007B23D File Offset: 0x0007943D
	public static bool MatchingDirection(npcOutfit.Outfit outfit, npcOutfit.Animation animation, npcOutfit.Direction direction)
	{
		return npcOutfit.CorrectDirection(outfit.directions, direction) && npcOutfit.CorrectAnimation(outfit.animations, animation);
	}

	// Token: 0x0400098D RID: 2445
	public npcOutfit.Type type;

	// Token: 0x0400098E RID: 2446
	public List<npcOutfit.Outfit> outfits;

	// Token: 0x020003E4 RID: 996
	public enum Type
	{
		// Token: 0x0400171E RID: 5918
		Head,
		// Token: 0x0400171F RID: 5919
		Arms,
		// Token: 0x04001720 RID: 5920
		Body,
		// Token: 0x04001721 RID: 5921
		Hat
	}

	// Token: 0x020003E5 RID: 997
	public enum Direction
	{
		// Token: 0x04001723 RID: 5923
		Unspecified,
		// Token: 0x04001724 RID: 5924
		Front,
		// Token: 0x04001725 RID: 5925
		Back,
		// Token: 0x04001726 RID: 5926
		Side,
		// Token: 0x04001727 RID: 5927
		Left,
		// Token: 0x04001728 RID: 5928
		Right
	}

	// Token: 0x020003E6 RID: 998
	public enum Animation
	{
		// Token: 0x0400172A RID: 5930
		Unspecified,
		// Token: 0x0400172B RID: 5931
		Idle,
		// Token: 0x0400172C RID: 5932
		Walk
	}

	// Token: 0x020003E7 RID: 999
	[Serializable]
	public class SpriteAndOffset
	{
		// Token: 0x0400172D RID: 5933
		public Sprite sprite;

		// Token: 0x0400172E RID: 5934
		public Vector2 offset;
	}

	// Token: 0x020003E8 RID: 1000
	[Serializable]
	public class Outfit
	{
		// Token: 0x0400172F RID: 5935
		public List<npcOutfit.Direction> directions;

		// Token: 0x04001730 RID: 5936
		public List<npcOutfit.Animation> animations;

		// Token: 0x04001731 RID: 5937
		public List<npcOutfit.SpriteAndOffset> spriteFrames;
	}
}
