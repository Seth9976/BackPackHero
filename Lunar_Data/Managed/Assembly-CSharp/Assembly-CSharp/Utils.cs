using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class Utils : MonoBehaviour
{
	// Token: 0x06000483 RID: 1155 RVA: 0x0001635C File Offset: 0x0001455C
	public static string GetPrefabName(string name)
	{
		name = name.ToLower();
		name = name.Split('(', StringSplitOptions.None)[0];
		name = name.Replace("trait", "");
		name = name.Replace("skill", "");
		name = name.Replace("equipment", "");
		name = name.Replace("variant", "");
		name = name.Replace("Clone", "");
		name = name.Replace("clone", "");
		name = name.Replace(" ", "");
		name = name.Trim();
		return name;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00016405 File Offset: 0x00014605
	public static bool CompareStrings(string a, string b)
	{
		a = Utils.GetPrefabName(a);
		b = Utils.GetPrefabName(b);
		return a == b;
	}
}
