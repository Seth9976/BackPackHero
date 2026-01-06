using System;
using System.Linq;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000BC RID: 188
	public static class ShaderUtils
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001F7BC File Offset: 0x0001D9BC
		public static string GetShaderPath(ShaderPathID id)
		{
			int num = (int)id;
			int num2 = ShaderUtils.s_ShaderPaths.Length;
			if (num2 > 0 && num >= 0 && num < num2)
			{
				return ShaderUtils.s_ShaderPaths[num];
			}
			Debug.LogError(string.Concat(new string[]
			{
				"Trying to access universal shader path out of bounds: (",
				id.ToString(),
				": ",
				num.ToString(),
				")"
			}));
			return "";
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001F830 File Offset: 0x0001DA30
		public static ShaderPathID GetEnumFromPath(string path)
		{
			return (ShaderPathID)Array.FindIndex<string>(ShaderUtils.s_ShaderPaths, (string m) => m == path);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001F860 File Offset: 0x0001DA60
		public static bool IsLWShader(Shader shader)
		{
			return ShaderUtils.s_ShaderPaths.Contains(shader.name);
		}

		// Token: 0x0400047A RID: 1146
		private static readonly string[] s_ShaderPaths = new string[]
		{
			"Universal Render Pipeline/Lit", "Universal Render Pipeline/Simple Lit", "Universal Render Pipeline/Unlit", "Universal Render Pipeline/Terrain/Lit", "Universal Render Pipeline/Particles/Lit", "Universal Render Pipeline/Particles/Simple Lit", "Universal Render Pipeline/Particles/Unlit", "Universal Render Pipeline/Baked Lit", "Universal Render Pipeline/Nature/SpeedTree7", "Universal Render Pipeline/Nature/SpeedTree7 Billboard",
			"Universal Render Pipeline/Nature/SpeedTree8"
		};
	}
}
