using System;
using UnityEngine.InputSystem;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006F RID: 111
	public class MousePositionDebug
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000116AA File Offset: 0x0000F8AA
		public static MousePositionDebug instance
		{
			get
			{
				if (MousePositionDebug.s_Instance == null)
				{
					MousePositionDebug.s_Instance = new MousePositionDebug();
				}
				return MousePositionDebug.s_Instance;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000116C2 File Offset: 0x0000F8C2
		public void Build()
		{
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public void Cleanup()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000116C6 File Offset: 0x0000F8C6
		public Vector2 GetMousePosition(float ScreenHeight, bool sceneView)
		{
			return this.GetInputMousePosition();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000116CE File Offset: 0x0000F8CE
		private Vector2 GetInputMousePosition()
		{
			if (Pointer.current == null)
			{
				return new Vector2(-1f, -1f);
			}
			return Pointer.current.position.ReadValue();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000116F6 File Offset: 0x0000F8F6
		public Vector2 GetMouseClickPosition(float ScreenHeight)
		{
			return Vector2.zero;
		}

		// Token: 0x04000244 RID: 580
		private static MousePositionDebug s_Instance;
	}
}
