using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007E RID: 126
	internal interface IStylePropertyAnimations
	{
		// Token: 0x0600031F RID: 799
		bool Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000320 RID: 800
		bool Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000321 RID: 801
		bool Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000322 RID: 802
		bool Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000323 RID: 803
		bool StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000324 RID: 804
		bool Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000325 RID: 805
		bool Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000326 RID: 806
		bool Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000327 RID: 807
		bool Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000328 RID: 808
		bool Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06000329 RID: 809
		bool Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032A RID: 810
		bool Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032B RID: 811
		bool Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600032C RID: 812
		bool HasRunningAnimation(StylePropertyId id);

		// Token: 0x0600032D RID: 813
		void UpdateAnimation(StylePropertyId id);

		// Token: 0x0600032E RID: 814
		void GetAllAnimations(List<StylePropertyId> outPropertyIds);

		// Token: 0x0600032F RID: 815
		void CancelAnimation(StylePropertyId id);

		// Token: 0x06000330 RID: 816
		void CancelAllAnimations();

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000331 RID: 817
		// (set) Token: 0x06000332 RID: 818
		int runningAnimationCount { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000333 RID: 819
		// (set) Token: 0x06000334 RID: 820
		int completedAnimationCount { get; set; }
	}
}
