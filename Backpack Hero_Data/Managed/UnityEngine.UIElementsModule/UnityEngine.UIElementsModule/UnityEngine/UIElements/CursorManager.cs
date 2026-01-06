using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000016 RID: 22
	internal class CursorManager : ICursorManager
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000410D File Offset: 0x0000230D
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004115 File Offset: 0x00002315
		public bool isCursorOverriden { get; private set; }

		// Token: 0x06000097 RID: 151 RVA: 0x00004120 File Offset: 0x00002320
		public void SetCursor(Cursor cursor)
		{
			bool flag = cursor.texture != null;
			if (flag)
			{
				Cursor.SetCursor(cursor.texture, cursor.hotspot, CursorMode.Auto);
				this.isCursorOverriden = true;
			}
			else
			{
				bool flag2 = cursor.defaultCursorId != 0;
				if (flag2)
				{
					Debug.LogWarning("Runtime cursors other than the default cursor need to be defined using a texture.");
				}
				this.ResetCursor();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004184 File Offset: 0x00002384
		public void ResetCursor()
		{
			bool isCursorOverriden = this.isCursorOverriden;
			if (isCursorOverriden)
			{
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			}
			this.isCursorOverriden = false;
		}
	}
}
