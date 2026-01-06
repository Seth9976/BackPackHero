using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class VectorImage : ScriptableObject
	{
		// Token: 0x040002D1 RID: 721
		[SerializeField]
		internal Texture2D atlas = null;

		// Token: 0x040002D2 RID: 722
		[SerializeField]
		internal VectorImageVertex[] vertices = null;

		// Token: 0x040002D3 RID: 723
		[SerializeField]
		internal ushort[] indices = null;

		// Token: 0x040002D4 RID: 724
		[SerializeField]
		internal GradientSettings[] settings = null;

		// Token: 0x040002D5 RID: 725
		[SerializeField]
		internal Vector2 size = Vector2.zero;
	}
}
