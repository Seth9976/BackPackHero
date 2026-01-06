using System;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000046 RID: 70
[CreateMenu("Town Actions/ShowEmojiAnimation", 0)]
public class ShowEmojiAnimation : ActionDataBase
{
	// Token: 0x06000137 RID: 311 RVA: 0x0000843B File Offset: 0x0000663B
	public override void OnStart()
	{
		if (Overworld_Animations_Manager.main)
		{
			Overworld_Animations_Manager.main.ShowAnimation(this.animation_Type);
		}
	}

	// Token: 0x040000CA RID: 202
	[SerializeField]
	private Overworld_Animations_Manager.Animation_Type animation_Type;
}
