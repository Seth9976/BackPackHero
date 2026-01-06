using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000089 RID: 137
[DefaultExecutionOrder(-5000)]
public class PlatformExcluder : MonoBehaviour
{
	// Token: 0x0600030B RID: 779 RVA: 0x00012030 File Offset: 0x00010230
	private void Awake()
	{
		foreach (ToggleRule toggleRule in this._toggleRules)
		{
			bool flag = this.ShouldBeEnabled(toggleRule);
			foreach (GameObject gameObject in toggleRule.TargetGameObjects)
			{
				if (gameObject.activeSelf != base.enabled)
				{
					Debug.Log("ToggleGOByDefine: " + gameObject.name + " was set to " + (flag ? "enabled" : "disabled"));
				}
				gameObject.SetActive(flag);
				if (this._DestroyIfNotEnabled && !flag)
				{
					Object.Destroy(gameObject);
				}
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x000120FC File Offset: 0x000102FC
	private bool ShouldBeEnabled(ToggleRule toggleRule)
	{
		foreach (Platform platform in toggleRule.AllowedOn)
		{
			if (this.IsPlatform(platform))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0001212E File Offset: 0x0001032E
	private bool IsPlatform(Platform platform)
	{
		return platform == Platform.PC;
	}

	// Token: 0x04000207 RID: 519
	[FormerlySerializedAs("toggleRules")]
	[SerializeField]
	private List<ToggleRule> _toggleRules = new List<ToggleRule>();

	// Token: 0x04000208 RID: 520
	[SerializeField]
	private bool _DestroyIfNotEnabled;
}
