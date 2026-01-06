using System;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x0200001B RID: 27
	[ExcludeFromPreset]
	[ExcludeFromObjectFactory]
	[EditorBrowsable(1)]
	[Obsolete("GUILayer has been removed.", true)]
	public sealed class GUILayer
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00007E39 File Offset: 0x00006039
		[Obsolete("GUILayer has been removed.", true)]
		public GUIElement HitTest(Vector3 screenPosition)
		{
			throw new Exception("GUILayer has been removed from Unity.");
		}
	}
}
