using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000214 RID: 532
	public interface IPointerEvent
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FF7 RID: 4087
		int pointerId { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000FF8 RID: 4088
		string pointerType { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FF9 RID: 4089
		bool isPrimary { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FFA RID: 4090
		int button { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000FFB RID: 4091
		int pressedButtons { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000FFC RID: 4092
		Vector3 position { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000FFD RID: 4093
		Vector3 localPosition { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000FFE RID: 4094
		Vector3 deltaPosition { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000FFF RID: 4095
		float deltaTime { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001000 RID: 4096
		int clickCount { get; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001001 RID: 4097
		float pressure { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001002 RID: 4098
		float tangentialPressure { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001003 RID: 4099
		float altitudeAngle { get; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001004 RID: 4100
		float azimuthAngle { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001005 RID: 4101
		float twist { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001006 RID: 4102
		Vector2 radius { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001007 RID: 4103
		Vector2 radiusVariance { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001008 RID: 4104
		EventModifiers modifiers { get; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001009 RID: 4105
		bool shiftKey { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600100A RID: 4106
		bool ctrlKey { get; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600100B RID: 4107
		bool commandKey { get; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600100C RID: 4108
		bool altKey { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x0600100D RID: 4109
		bool actionKey { get; }
	}
}
