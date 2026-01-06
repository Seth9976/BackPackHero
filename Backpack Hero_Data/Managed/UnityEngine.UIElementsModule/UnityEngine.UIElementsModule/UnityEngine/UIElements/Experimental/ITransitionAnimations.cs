using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x02000381 RID: 897
	public interface ITransitionAnimations
	{
		// Token: 0x06001CB9 RID: 7353
		ValueAnimation<float> Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x06001CBA RID: 7354
		ValueAnimation<Rect> Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x06001CBB RID: 7355
		ValueAnimation<Color> Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x06001CBC RID: 7356
		ValueAnimation<Vector3> Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x06001CBD RID: 7357
		ValueAnimation<Vector2> Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x06001CBE RID: 7358
		ValueAnimation<Quaternion> Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001CBF RID: 7359
		ValueAnimation<StyleValues> Start(StyleValues from, StyleValues to, int durationMs);

		// Token: 0x06001CC0 RID: 7360
		ValueAnimation<StyleValues> Start(StyleValues to, int durationMs);

		// Token: 0x06001CC1 RID: 7361
		ValueAnimation<float> Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x06001CC2 RID: 7362
		ValueAnimation<Rect> Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x06001CC3 RID: 7363
		ValueAnimation<Color> Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x06001CC4 RID: 7364
		ValueAnimation<Vector3> Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x06001CC5 RID: 7365
		ValueAnimation<Vector2> Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x06001CC6 RID: 7366
		ValueAnimation<Quaternion> Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x06001CC7 RID: 7367
		ValueAnimation<Rect> Layout(Rect to, int durationMs);

		// Token: 0x06001CC8 RID: 7368
		ValueAnimation<Vector2> TopLeft(Vector2 to, int durationMs);

		// Token: 0x06001CC9 RID: 7369
		ValueAnimation<Vector2> Size(Vector2 to, int durationMs);

		// Token: 0x06001CCA RID: 7370
		ValueAnimation<float> Scale(float to, int duration);

		// Token: 0x06001CCB RID: 7371
		ValueAnimation<Vector3> Position(Vector3 to, int duration);

		// Token: 0x06001CCC RID: 7372
		ValueAnimation<Quaternion> Rotation(Quaternion to, int duration);
	}
}
