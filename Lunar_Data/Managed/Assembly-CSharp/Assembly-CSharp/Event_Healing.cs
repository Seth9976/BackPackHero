using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class Event_Healing : MonoBehaviour
{
	// Token: 0x060001BF RID: 447 RVA: 0x00009A82 File Offset: 0x00007C82
	private void Start()
	{
		HealthBarMaster.instance.Heal(25f);
	}
}
