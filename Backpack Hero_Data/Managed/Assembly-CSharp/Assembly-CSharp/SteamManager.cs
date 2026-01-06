using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x0200016F RID: 367
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00093E85 File Offset: 0x00092085
	protected static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00093EA9 File Offset: 0x000920A9
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x00093EB5 File Offset: 0x000920B5
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x00093EBD File Offset: 0x000920BD
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x00093ECC File Offset: 0x000920CC
	protected virtual void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary((AppId_t)1970580U))
			{
				Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			string text = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
			DllNotFoundException ex2 = ex;
			Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null), this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			SteamManager.Failed = true;
			return;
		}
		SteamManager.s_EverInitialized = true;
	}

	// Token: 0x06000EEB RID: 3819 RVA: 0x00093FC0 File Offset: 0x000921C0
	protected virtual void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x06000EEC RID: 3820 RVA: 0x0009400E File Offset: 0x0009220E
	protected virtual void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x06000EED RID: 3821 RVA: 0x00094034 File Offset: 0x00092234
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		try
		{
			SteamAPI.RunCallbacks();
		}
		catch (Exception ex)
		{
			string text = "[Steamworks.NET] SteamAPI_RunCallbacks() failed. Refer to Valve's documentation or the comment above this line for more information.\n";
			Exception ex2 = ex;
			Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null), this);
			this.m_bInitialized = false;
		}
	}

	// Token: 0x04000C1E RID: 3102
	protected static bool s_EverInitialized = false;

	// Token: 0x04000C1F RID: 3103
	protected static SteamManager s_instance;

	// Token: 0x04000C20 RID: 3104
	protected bool m_bInitialized;

	// Token: 0x04000C21 RID: 3105
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;

	// Token: 0x04000C22 RID: 3106
	public static bool Failed = false;

	// Token: 0x04000C23 RID: 3107
	public new static bool enabled = true;
}
