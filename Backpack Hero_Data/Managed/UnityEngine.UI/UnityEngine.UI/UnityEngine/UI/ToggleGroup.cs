using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003B RID: 59
	[AddComponentMenu("UI/Toggle Group", 31)]
	[DisallowMultipleComponent]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x00015939 File Offset: 0x00013B39
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x00015941 File Offset: 0x00013B41
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001594A File Offset: 0x00013B4A
		protected ToggleGroup()
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001595D File Offset: 0x00013B5D
		protected override void Start()
		{
			this.EnsureValidState();
			base.Start();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001596B File Offset: 0x00013B6B
		protected override void OnEnable()
		{
			this.EnsureValidState();
			base.OnEnable();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00015979 File Offset: 0x00013B79
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000159B0 File Offset: 0x00013BB0
		public void NotifyToggleOn(Toggle toggle, bool sendCallback = true)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					if (sendCallback)
					{
						this.m_Toggles[i].isOn = false;
					}
					else
					{
						this.m_Toggles[i].SetIsOnWithoutNotify(false);
					}
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00015A17 File Offset: 0x00013C17
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00015A34 File Offset: 0x00013C34
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00015A50 File Offset: 0x00013C50
		public void EnsureValidState()
		{
			if (!this.allowSwitchOff && !this.AnyTogglesOn() && this.m_Toggles.Count != 0)
			{
				this.m_Toggles[0].isOn = true;
				this.NotifyToggleOn(this.m_Toggles[0], true);
			}
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() > 1)
			{
				Toggle firstActiveToggle = this.GetFirstActiveToggle();
				foreach (Toggle toggle in enumerable)
				{
					if (!(toggle == firstActiveToggle))
					{
						toggle.isOn = false;
					}
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00015AFC File Offset: 0x00013CFC
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00015B2E File Offset: 0x00013D2E
		public IEnumerable<Toggle> ActiveToggles()
		{
			return this.m_Toggles.Where((Toggle x) => x.isOn);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00015B5C File Offset: 0x00013D5C
		public Toggle GetFirstActiveToggle()
		{
			IEnumerable<Toggle> enumerable = this.ActiveToggles();
			if (enumerable.Count<Toggle>() <= 0)
			{
				return null;
			}
			return enumerable.First<Toggle>();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00015B84 File Offset: 0x00013D84
		public void SetAllTogglesOff(bool sendCallback = true)
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			if (sendCallback)
			{
				for (int i = 0; i < this.m_Toggles.Count; i++)
				{
					this.m_Toggles[i].isOn = false;
				}
			}
			else
			{
				for (int j = 0; j < this.m_Toggles.Count; j++)
				{
					this.m_Toggles[j].SetIsOnWithoutNotify(false);
				}
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x0400017B RID: 379
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x0400017C RID: 380
		protected List<Toggle> m_Toggles = new List<Toggle>();
	}
}
