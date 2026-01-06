using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200026E RID: 622
	public interface ICustomStyle
	{
		// Token: 0x0600133A RID: 4922
		bool TryGetValue(CustomStyleProperty<float> property, out float value);

		// Token: 0x0600133B RID: 4923
		bool TryGetValue(CustomStyleProperty<int> property, out int value);

		// Token: 0x0600133C RID: 4924
		bool TryGetValue(CustomStyleProperty<bool> property, out bool value);

		// Token: 0x0600133D RID: 4925
		bool TryGetValue(CustomStyleProperty<Color> property, out Color value);

		// Token: 0x0600133E RID: 4926
		bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value);

		// Token: 0x0600133F RID: 4927
		bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value);

		// Token: 0x06001340 RID: 4928
		bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value);

		// Token: 0x06001341 RID: 4929
		bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object;

		// Token: 0x06001342 RID: 4930
		bool TryGetValue(CustomStyleProperty<string> property, out string value);
	}
}
