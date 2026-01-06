using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class AnalyticsManager : MonoBehaviour
{
	// Token: 0x0600038B RID: 907 RVA: 0x00014AA8 File Offset: 0x00012CA8
	private async void Start()
	{
		if (!Singleton.Instance.analyticsActive)
		{
			this.analyticsActive = false;
		}
		if (this.analyticsActive)
		{
			try
			{
				await UnityServices.InitializeAsync();
				await AnalyticsService.Instance.CheckForRequiredConsents();
				Debug.Log("Analytics active!");
				this.RefreshList();
			}
			catch (ConsentCheckException)
			{
				Debug.Log("no consent");
			}
		}
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00014ADF File Offset: 0x00012CDF
	public void RefreshList()
	{
		if (!this.analyticsActive)
		{
			return;
		}
		this.parameterObject = new AnalyticsManager.ParameterObject();
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00014AF8 File Offset: 0x00012CF8
	public void AddItem(string key, string name)
	{
		if (!this.analyticsActive)
		{
			return;
		}
		if (key == "runOutcome")
		{
			this.parameterObject.runOutcome = name;
		}
		if (key == "enemiesEncountered")
		{
			this.parameterObject.enemiesEncountered.Add(name);
		}
		if (key == "diedWithEnemies")
		{
			this.parameterObject.diedWithEnemies.Add(name);
			return;
		}
		if (key == "itemsRejected")
		{
			this.parameterObject.itemsLeftBehind.Add(name);
			return;
		}
		if (key == "itemsHeld")
		{
			this.parameterObject.itemsHeld.Add(name);
			return;
		}
		if (key == "itemsSold")
		{
			this.parameterObject.itemsSold.Add(name);
			return;
		}
		if (key == "zoneName")
		{
			this.parameterObject.finalZoneName = name;
			return;
		}
		if (key == "zoneFloor")
		{
			this.parameterObject.finalZoneFloor = name;
			return;
		}
		if (key == "zonesCompleted")
		{
			this.parameterObject.zonesCompleted.Add(name);
			return;
		}
		if (key == "level")
		{
			this.parameterObject.level = name;
			return;
		}
		if (key == "character")
		{
			this.parameterObject.character = name;
			return;
		}
		if (key == "runType")
		{
			this.parameterObject.runType = name;
			return;
		}
		if (key == "itemsOffered")
		{
			this.parameterObject.itemsOffered.Add(name);
			return;
		}
		if (key == "versionNumber")
		{
			this.parameterObject.versionNumber = name;
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00014C9C File Offset: 0x00012E9C
	public void FlushGameData()
	{
		if (!this.analyticsActive)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("Character", this.parameterObject.character);
		dictionary.Add("runType", this.parameterObject.runType);
		dictionary.Add("level", this.parameterObject.level);
		dictionary.Add("runOutcome", this.parameterObject.runOutcome);
		dictionary.Add("versionNumber", this.parameterObject.versionNumber);
		dictionary.Add("finalZoneName", this.parameterObject.finalZoneName);
		dictionary.Add("finalZoneFloor", this.parameterObject.finalZoneFloor);
		if (this.parameterObject.zonesCompleted.Count > 0)
		{
			dictionary.Add("zonesCompleted", this.parameterObject.zonesCompleted.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("zonesCompleted", "");
		}
		if (this.parameterObject.itemsLeftBehind.Count > 0)
		{
			dictionary.Add("itemsLeftBehind", this.parameterObject.itemsLeftBehind.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("itemsLeftBehind", "");
		}
		if (this.parameterObject.itemsHeld.Count > 0)
		{
			dictionary.Add("itemsHeld", this.parameterObject.itemsHeld.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("itemsHeld", "");
		}
		if (this.parameterObject.diedWithEnemies.Count > 0)
		{
			dictionary.Add("diedWithEnemies", this.parameterObject.diedWithEnemies.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("diedWithEnemies", "");
		}
		if (this.parameterObject.enemiesEncountered.Count > 0)
		{
			dictionary.Add("enemiesEncountered", this.parameterObject.enemiesEncountered.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("enemiesEncountered", "");
		}
		if (this.parameterObject.itemsSold.Count > 0)
		{
			dictionary.Add("itemsSold", this.parameterObject.itemsSold.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("itemsSold", "");
		}
		if (this.parameterObject.itemsOffered.Count > 0)
		{
			dictionary.Add("itemsOffered", this.parameterObject.itemsOffered.Aggregate((string x, string y) => x + "," + y));
		}
		else
		{
			dictionary.Add("itemsOffered", "");
		}
		AnalyticsService.Instance.CustomData("NewGameDataPackage", dictionary);
		AnalyticsService.Instance.Flush();
		Debug.Log("Data Sent!");
	}

	// Token: 0x0400028D RID: 653
	[SerializeField]
	private bool analyticsActive = true;

	// Token: 0x0400028E RID: 654
	[SerializeField]
	public AnalyticsManager.ParameterObject parameterObject = new AnalyticsManager.ParameterObject();

	// Token: 0x020002AB RID: 683
	[Serializable]
	public class ParameterObject
	{
		// Token: 0x04001026 RID: 4134
		public string runOutcome = "";

		// Token: 0x04001027 RID: 4135
		public string character;

		// Token: 0x04001028 RID: 4136
		public string runType;

		// Token: 0x04001029 RID: 4137
		public string level;

		// Token: 0x0400102A RID: 4138
		public string versionNumber;

		// Token: 0x0400102B RID: 4139
		public string finalZoneName;

		// Token: 0x0400102C RID: 4140
		public string finalZoneFloor;

		// Token: 0x0400102D RID: 4141
		public List<string> zonesCompleted = new List<string>();

		// Token: 0x0400102E RID: 4142
		public List<string> itemsLeftBehind = new List<string>();

		// Token: 0x0400102F RID: 4143
		public List<string> itemsHeld = new List<string>();

		// Token: 0x04001030 RID: 4144
		public List<string> diedWithEnemies = new List<string>();

		// Token: 0x04001031 RID: 4145
		public List<string> enemiesEncountered = new List<string>();

		// Token: 0x04001032 RID: 4146
		public List<string> itemsSold = new List<string>();

		// Token: 0x04001033 RID: 4147
		public List<string> itemsOffered = new List<string>();
	}
}
