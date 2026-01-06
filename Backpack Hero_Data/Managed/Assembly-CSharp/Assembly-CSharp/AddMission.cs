using System;
using System.Collections.Generic;
using CleverCrow.Fluid.Dialogues;
using CleverCrow.Fluid.Dialogues.Actions;
using UnityEngine;

// Token: 0x02000038 RID: 56
[CreateMenu("AddMission", 0)]
public class AddMission : ActionDataBase
{
	// Token: 0x06000116 RID: 278 RVA: 0x00007D10 File Offset: 0x00005F10
	public override void OnStart()
	{
		if (this.mission)
		{
			if (MetaProgressSaveManager.main.HasMission(this.mission))
			{
				return;
			}
			MetaProgressSaveManager.main.AddMission(this.mission);
			this.window = Overworld_Manager.main.OpenNewMissionWindow(this.mission);
		}
		foreach (Missions missions in this.missions)
		{
			if (!MetaProgressSaveManager.main.HasMission(missions))
			{
				MetaProgressSaveManager.main.AddMission(missions);
				this.windows.Add(Overworld_Manager.main.OpenNewMissionWindow(missions));
			}
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00007DD0 File Offset: 0x00005FD0
	public override ActionStatus OnUpdate()
	{
		if (this.skip)
		{
			return ActionStatus.Success;
		}
		if (this.window != null)
		{
			return ActionStatus.Continue;
		}
		using (List<GameObject>.Enumerator enumerator = this.windows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current != null)
				{
					return ActionStatus.Continue;
				}
			}
		}
		return ActionStatus.Success;
	}

	// Token: 0x040000B0 RID: 176
	[SerializeField]
	private Missions mission;

	// Token: 0x040000B1 RID: 177
	[SerializeField]
	private List<Missions> missions;

	// Token: 0x040000B2 RID: 178
	private GameObject window;

	// Token: 0x040000B3 RID: 179
	private List<GameObject> windows = new List<GameObject>();

	// Token: 0x040000B4 RID: 180
	private bool skip;
}
