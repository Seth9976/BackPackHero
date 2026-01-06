using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000024 RID: 36
	public static class TextEventManager
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00007D32 File Offset: 0x00005F32
		public static void ON_PRE_RENDER_OBJECT_CHANGED()
		{
			TextEventManager.OnPreRenderObject_Event.Call();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00007D40 File Offset: 0x00005F40
		public static void ON_MATERIAL_PROPERTY_CHANGED(bool isChanged, Material mat)
		{
			TextEventManager.MATERIAL_PROPERTY_EVENT.Call(isChanged, mat);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007D50 File Offset: 0x00005F50
		public static void ON_FONT_PROPERTY_CHANGED(bool isChanged, Object font)
		{
			TextEventManager.FONT_PROPERTY_EVENT.Call(isChanged, font);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007D60 File Offset: 0x00005F60
		public static void ON_SPRITE_ASSET_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.SPRITE_ASSET_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007D70 File Offset: 0x00005F70
		public static void ON_TEXTMESHPRO_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.TEXTMESHPRO_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00007D80 File Offset: 0x00005F80
		public static void ON_DRAG_AND_DROP_MATERIAL_CHANGED(GameObject sender, Material currentMaterial, Material newMaterial)
		{
			TextEventManager.DRAG_AND_DROP_MATERIAL_EVENT.Call(sender, currentMaterial, newMaterial);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007D91 File Offset: 0x00005F91
		public static void ON_TEXT_STYLE_PROPERTY_CHANGED(bool isChanged)
		{
			TextEventManager.TEXT_STYLE_PROPERTY_EVENT.Call(isChanged);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public static void ON_COLOR_GRADIENT_PROPERTY_CHANGED(Object gradient)
		{
			TextEventManager.COLOR_GRADIENT_PROPERTY_EVENT.Call(gradient);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00007DAF File Offset: 0x00005FAF
		public static void ON_TEXT_CHANGED(Object obj)
		{
			TextEventManager.TEXT_CHANGED_EVENT.Call(obj);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007DBE File Offset: 0x00005FBE
		public static void ON_TMP_SETTINGS_CHANGED()
		{
			TextEventManager.TMP_SETTINGS_PROPERTY_EVENT.Call();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007DCC File Offset: 0x00005FCC
		public static void ON_RESOURCES_LOADED()
		{
			TextEventManager.RESOURCE_LOAD_EVENT.Call();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007DDA File Offset: 0x00005FDA
		public static void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(bool isChanged, Object obj)
		{
			TextEventManager.TEXTMESHPRO_UGUI_PROPERTY_EVENT.Call(isChanged, obj);
		}

		// Token: 0x040000F5 RID: 245
		public static readonly FastAction<bool, Material> MATERIAL_PROPERTY_EVENT = new FastAction<bool, Material>();

		// Token: 0x040000F6 RID: 246
		public static readonly FastAction<bool, Object> FONT_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040000F7 RID: 247
		public static readonly FastAction<bool, Object> SPRITE_ASSET_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040000F8 RID: 248
		public static readonly FastAction<bool, Object> TEXTMESHPRO_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040000F9 RID: 249
		public static readonly FastAction<GameObject, Material, Material> DRAG_AND_DROP_MATERIAL_EVENT = new FastAction<GameObject, Material, Material>();

		// Token: 0x040000FA RID: 250
		public static readonly FastAction<bool> TEXT_STYLE_PROPERTY_EVENT = new FastAction<bool>();

		// Token: 0x040000FB RID: 251
		public static readonly FastAction<Object> COLOR_GRADIENT_PROPERTY_EVENT = new FastAction<Object>();

		// Token: 0x040000FC RID: 252
		public static readonly FastAction TMP_SETTINGS_PROPERTY_EVENT = new FastAction();

		// Token: 0x040000FD RID: 253
		public static readonly FastAction RESOURCE_LOAD_EVENT = new FastAction();

		// Token: 0x040000FE RID: 254
		public static readonly FastAction<bool, Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT = new FastAction<bool, Object>();

		// Token: 0x040000FF RID: 255
		public static readonly FastAction OnPreRenderObject_Event = new FastAction();

		// Token: 0x04000100 RID: 256
		public static readonly FastAction<Object> TEXT_CHANGED_EVENT = new FastAction<Object>();
	}
}
