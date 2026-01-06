using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000095 RID: 149
	internal interface IStylePropertyAnimationSystem
	{
		// Token: 0x06000513 RID: 1299
		bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000514 RID: 1300
		bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000515 RID: 1301
		bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000516 RID: 1302
		bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000517 RID: 1303
		bool StartAnimationEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000518 RID: 1304
		bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000519 RID: 1305
		bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051A RID: 1306
		bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051B RID: 1307
		bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051C RID: 1308
		bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051D RID: 1309
		bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051E RID: 1310
		bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x0600051F RID: 1311
		bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve);

		// Token: 0x06000520 RID: 1312
		void CancelAllAnimations();

		// Token: 0x06000521 RID: 1313
		void CancelAllAnimations(VisualElement owner);

		// Token: 0x06000522 RID: 1314
		void CancelAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000523 RID: 1315
		bool HasRunningAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000524 RID: 1316
		void UpdateAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06000525 RID: 1317
		void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds);

		// Token: 0x06000526 RID: 1318
		void Update();
	}
}
