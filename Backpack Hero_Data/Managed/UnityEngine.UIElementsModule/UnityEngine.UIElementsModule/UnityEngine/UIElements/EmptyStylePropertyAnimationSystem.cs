using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000B0 RID: 176
	internal class EmptyStylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00016118 File Offset: 0x00014318
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001612C File Offset: 0x0001432C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00016140 File Offset: 0x00014340
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00016154 File Offset: 0x00014354
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00016168 File Offset: 0x00014368
		public bool StartAnimationEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001617C File Offset: 0x0001437C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00016190 File Offset: 0x00014390
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000161A4 File Offset: 0x000143A4
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000161B8 File Offset: 0x000143B8
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000161CC File Offset: 0x000143CC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000161E0 File Offset: 0x000143E0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000161F4 File Offset: 0x000143F4
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00016208 File Offset: 0x00014408
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000020E6 File Offset: 0x000002E6
		public void CancelAllAnimations()
		{
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000020E6 File Offset: 0x000002E6
		public void CancelAllAnimations(VisualElement owner)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000020E6 File Offset: 0x000002E6
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001621C File Offset: 0x0001441C
		public bool HasRunningAnimation(VisualElement owner, StylePropertyId id)
		{
			return false;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000020E6 File Offset: 0x000002E6
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000020E6 File Offset: 0x000002E6
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000020E6 File Offset: 0x000002E6
		public void Update()
		{
		}
	}
}
