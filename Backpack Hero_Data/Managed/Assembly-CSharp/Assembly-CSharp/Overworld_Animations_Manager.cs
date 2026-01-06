using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class Overworld_Animations_Manager : MonoBehaviour
{
	// Token: 0x0600029C RID: 668 RVA: 0x0000F947 File Offset: 0x0000DB47
	private void Awake()
	{
		if (Overworld_Animations_Manager.main == null)
		{
			Overworld_Animations_Manager.main = this;
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000F95C File Offset: 0x0000DB5C
	private void OnDestroy()
	{
		if (Overworld_Animations_Manager.main == this)
		{
			Overworld_Animations_Manager.main = null;
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000F971 File Offset: 0x0000DB71
	private void Start()
	{
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0000F973 File Offset: 0x0000DB73
	private void Update()
	{
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000F978 File Offset: 0x0000DB78
	public void ShowAnimation(Overworld_Animations_Manager.Animation_Type type)
	{
		foreach (Overworld_Animations_Manager.Animation animation in this.animations)
		{
			animation.gameObject.SetActive(false);
		}
		base.transform.position = Overworld_ConversationManager.main.currentNPCSpeaker.transform.position;
		foreach (Overworld_Animations_Manager.Animation animation2 in this.animations)
		{
			if (animation2.animation_Type == type)
			{
				animation2.gameObject.SetActive(true);
				break;
			}
		}
	}

	// Token: 0x040001B4 RID: 436
	public static Overworld_Animations_Manager main;

	// Token: 0x040001B5 RID: 437
	[SerializeField]
	private List<Overworld_Animations_Manager.Animation> animations = new List<Overworld_Animations_Manager.Animation>();

	// Token: 0x02000289 RID: 649
	public enum Animation_Type
	{
		// Token: 0x04000F8B RID: 3979
		love,
		// Token: 0x04000F8C RID: 3980
		hungry,
		// Token: 0x04000F8D RID: 3981
		mad,
		// Token: 0x04000F8E RID: 3982
		sad
	}

	// Token: 0x0200028A RID: 650
	[Serializable]
	private class Animation
	{
		// Token: 0x04000F8F RID: 3983
		public Overworld_Animations_Manager.Animation_Type animation_Type;

		// Token: 0x04000F90 RID: 3984
		public GameObject gameObject;
	}
}
