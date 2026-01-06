using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class EventManager : MonoBehaviour
{
	// Token: 0x0600017D RID: 381 RVA: 0x00009BDC File Offset: 0x00007DDC
	public void Awake()
	{
		if (EventManager.instance == null)
		{
			EventManager.instance = this;
			this.SetEvent();
			return;
		}
		EventManager.instance.SetEvent();
		Object.Destroy(this);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00009C08 File Offset: 0x00007E08
	private void OnDestroy()
	{
		if (EventManager.instance == this)
		{
			EventManager.instance = null;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00009C1D File Offset: 0x00007E1D
	private void Start()
	{
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00009C20 File Offset: 0x00007E20
	private void SetEvent()
	{
		if (!Singleton.Instance.allowHolidayEvents)
		{
			Debug.Log("Holiday events disabled");
			this.eventType = EventManager.EventType.None;
			return;
		}
		Debug.Log("Holiday events enabled");
		try
		{
			this.currentTime = DateTime.Now;
			if ((this.currentTime.Month == 12 && this.currentTime.Day >= 23) || (this.currentTime.Month == 1 && this.currentTime.Day <= 4))
			{
				this.eventType = EventManager.EventType.Winter;
			}
			else if (this.currentTime.Month == 11 && this.currentTime.Day >= 23 && this.currentTime.Month == 11 && this.currentTime.Day <= 28)
			{
				this.eventType = EventManager.EventType.Thanksgiving;
			}
			else if (this.currentTime.Month == 10 && this.currentTime.Day >= 23 && this.currentTime.Month == 10 && this.currentTime.Day <= 31)
			{
				this.eventType = EventManager.EventType.Halloween;
			}
			else if ((this.currentTime.Month == 6 && this.currentTime.Day >= 15) || this.currentTime.Month == 7)
			{
				this.eventType = EventManager.EventType.Summer;
			}
			else if (this.currentTime.Month == 2 && this.currentTime.Day >= 10 && this.currentTime.Month == 2 && this.currentTime.Day <= 17)
			{
				this.eventType = EventManager.EventType.ValentinesDay;
			}
			else
			{
				this.eventType = EventManager.EventType.None;
			}
		}
		catch (Exception ex)
		{
			string text = "Failed to set event from error ";
			Exception ex2 = ex;
			Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
		}
	}

	// Token: 0x040000F4 RID: 244
	public bool overrideTime;

	// Token: 0x040000F5 RID: 245
	public static EventManager instance;

	// Token: 0x040000F6 RID: 246
	public EventManager.EventType eventType;

	// Token: 0x040000F7 RID: 247
	private DateTime currentTime = DateTime.Now;

	// Token: 0x0200026C RID: 620
	public enum EventType
	{
		// Token: 0x04000F1F RID: 3871
		None,
		// Token: 0x04000F20 RID: 3872
		Winter,
		// Token: 0x04000F21 RID: 3873
		Thanksgiving,
		// Token: 0x04000F22 RID: 3874
		Halloween,
		// Token: 0x04000F23 RID: 3875
		Summer,
		// Token: 0x04000F24 RID: 3876
		ValentinesDay
	}
}
