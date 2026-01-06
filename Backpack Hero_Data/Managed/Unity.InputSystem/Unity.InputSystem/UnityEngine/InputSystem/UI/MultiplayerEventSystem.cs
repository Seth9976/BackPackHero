using System;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000088 RID: 136
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.6/manual/UISupport.html#multiplayer-uis")]
	public class MultiplayerEventSystem : EventSystem
	{
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0003B86F File Offset: 0x00039A6F
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0003B877 File Offset: 0x00039A77
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

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003B886 File Offset: 0x00039A86
		protected override void OnEnable()
		{
			base.OnEnable();
			this.InitializePlayerRoot();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003B894 File Offset: 0x00039A94
		protected override void OnDisable()
		{
			base.OnDisable();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0003B89C File Offset: 0x00039A9C
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

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003B8D4 File Offset: 0x00039AD4
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
