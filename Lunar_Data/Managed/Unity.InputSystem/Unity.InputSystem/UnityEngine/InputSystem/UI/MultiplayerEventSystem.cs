using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000088 RID: 136
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.5/manual/UISupport.html#multiplayer-uis")]
	public class MultiplayerEventSystem : EventSystem
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0003B833 File Offset: 0x00039A33
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0003B83B File Offset: 0x00039A3B
		public GameObject playerRoot
		{
			get
			{
				return this.m_PlayerRoot;
			}
			set
			{
				this.m_PlayerRoot = value;
				this.InitializePlayerRoot();
			}
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0003B84A File Offset: 0x00039A4A
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitializePlayerRoot();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0003B858 File Offset: 0x00039A58
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003B860 File Offset: 0x00039A60
		private void InitializePlayerRoot()
		{
			if (this.m_PlayerRoot == null)
			{
				return;
			}
			InputSystemUIInputModule component = base.GetComponent<InputSystemUIInputModule>();
			if (component != null)
			{
				component.localMultiPlayerRoot = this.m_PlayerRoot;
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003B898 File Offset: 0x00039A98
		protected override void Update()
		{
			EventSystem current = EventSystem.current;
			EventSystem.current = this;
			try
			{
				base.Update();
			}
			finally
			{
				EventSystem.current = current;
			}
		}

		// Token: 0x040003DD RID: 989
		[Tooltip("If set, only process mouse and navigation events for any game objects which are children of this game object.")]
		[SerializeField]
		private GameObject m_PlayerRoot;
	}
}
