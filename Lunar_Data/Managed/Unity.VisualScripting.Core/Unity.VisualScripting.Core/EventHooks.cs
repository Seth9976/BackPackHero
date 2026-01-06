using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000052 RID: 82
	public static class EventHooks
	{
		// Token: 0x04000072 RID: 114
		public const string Custom = "Custom";

		// Token: 0x04000073 RID: 115
		public const string OnGUI = "OnGUI";

		// Token: 0x04000074 RID: 116
		public const string OnApplicationFocus = "OnApplicationFocus";

		// Token: 0x04000075 RID: 117
		public const string OnApplicationLostFocus = "OnApplicationLostFocus";

		// Token: 0x04000076 RID: 118
		public const string OnApplicationPause = "OnApplicationPause";

		// Token: 0x04000077 RID: 119
		public const string OnApplicationResume = "OnApplicationResume";

		// Token: 0x04000078 RID: 120
		public const string OnApplicationQuit = "OnApplicationQuit";

		// Token: 0x04000079 RID: 121
		public const string OnEnable = "OnEnable";

		// Token: 0x0400007A RID: 122
		public const string Start = "Start";

		// Token: 0x0400007B RID: 123
		public const string Update = "Update";

		// Token: 0x0400007C RID: 124
		public const string FixedUpdate = "FixedUpdate";

		// Token: 0x0400007D RID: 125
		public const string LateUpdate = "LateUpdate";

		// Token: 0x0400007E RID: 126
		public const string OnDisable = "OnDisable";

		// Token: 0x0400007F RID: 127
		public const string OnDestroy = "OnDestroy";

		// Token: 0x04000080 RID: 128
		public const string AnimationEvent = "AnimationEvent";

		// Token: 0x04000081 RID: 129
		public const string UnityEvent = "UnityEvent";

		// Token: 0x04000082 RID: 130
		public const string OnDrawGizmos = "OnDrawGizmos";

		// Token: 0x04000083 RID: 131
		public const string OnDrawGizmosSelected = "OnDrawGizmosSelected";

		// Token: 0x04000084 RID: 132
		public const string OnPointerEnter = "OnPointerEnter";

		// Token: 0x04000085 RID: 133
		public const string OnPointerExit = "OnPointerExit";

		// Token: 0x04000086 RID: 134
		public const string OnPointerDown = "OnPointerDown";

		// Token: 0x04000087 RID: 135
		public const string OnPointerUp = "OnPointerUp";

		// Token: 0x04000088 RID: 136
		public const string OnPointerClick = "OnPointerClick";

		// Token: 0x04000089 RID: 137
		public const string OnBeginDrag = "OnBeginDrag";

		// Token: 0x0400008A RID: 138
		public const string OnDrag = "OnDrag";

		// Token: 0x0400008B RID: 139
		public const string OnEndDrag = "OnEndDrag";

		// Token: 0x0400008C RID: 140
		public const string OnDrop = "OnDrop";

		// Token: 0x0400008D RID: 141
		public const string OnScroll = "OnScroll";

		// Token: 0x0400008E RID: 142
		public const string OnSelect = "OnSelect";

		// Token: 0x0400008F RID: 143
		public const string OnDeselect = "OnDeselect";

		// Token: 0x04000090 RID: 144
		public const string OnSubmit = "OnSubmit";

		// Token: 0x04000091 RID: 145
		public const string OnCancel = "OnCancel";

		// Token: 0x04000092 RID: 146
		public const string OnMove = "OnMove";

		// Token: 0x04000093 RID: 147
		public const string OnBecameInvisible = "OnBecameInvisible";

		// Token: 0x04000094 RID: 148
		public const string OnBecameVisible = "OnBecameVisible";

		// Token: 0x04000095 RID: 149
		public const string OnCollisionEnter = "OnCollisionEnter";

		// Token: 0x04000096 RID: 150
		public const string OnCollisionExit = "OnCollisionExit";

		// Token: 0x04000097 RID: 151
		public const string OnCollisionStay = "OnCollisionStay";

		// Token: 0x04000098 RID: 152
		public const string OnCollisionEnter2D = "OnCollisionEnter2D";

		// Token: 0x04000099 RID: 153
		public const string OnCollisionExit2D = "OnCollisionExit2D";

		// Token: 0x0400009A RID: 154
		public const string OnCollisionStay2D = "OnCollisionStay2D";

		// Token: 0x0400009B RID: 155
		public const string OnControllerColliderHit = "OnControllerColliderHit";

		// Token: 0x0400009C RID: 156
		public const string OnJointBreak = "OnJointBreak";

		// Token: 0x0400009D RID: 157
		public const string OnJointBreak2D = "OnJointBreak2D";

		// Token: 0x0400009E RID: 158
		public const string OnMouseDown = "OnMouseDown";

		// Token: 0x0400009F RID: 159
		public const string OnMouseDrag = "OnMouseDrag";

		// Token: 0x040000A0 RID: 160
		public const string OnMouseEnter = "OnMouseEnter";

		// Token: 0x040000A1 RID: 161
		public const string OnMouseExit = "OnMouseExit";

		// Token: 0x040000A2 RID: 162
		public const string OnMouseOver = "OnMouseOver";

		// Token: 0x040000A3 RID: 163
		public const string OnMouseUp = "OnMouseUp";

		// Token: 0x040000A4 RID: 164
		public const string OnMouseUpAsButton = "OnMouseUpAsButton";

		// Token: 0x040000A5 RID: 165
		public const string OnParticleCollision = "OnParticleCollision";

		// Token: 0x040000A6 RID: 166
		public const string OnTransformChildrenChanged = "OnTransformChildrenChanged";

		// Token: 0x040000A7 RID: 167
		public const string OnTransformParentChanged = "OnTransformParentChanged";

		// Token: 0x040000A8 RID: 168
		public const string OnTriggerEnter = "OnTriggerEnter";

		// Token: 0x040000A9 RID: 169
		public const string OnTriggerExit = "OnTriggerExit";

		// Token: 0x040000AA RID: 170
		public const string OnTriggerStay = "OnTriggerStay";

		// Token: 0x040000AB RID: 171
		public const string OnTriggerEnter2D = "OnTriggerEnter2D";

		// Token: 0x040000AC RID: 172
		public const string OnTriggerExit2D = "OnTriggerExit2D";

		// Token: 0x040000AD RID: 173
		public const string OnTriggerStay2D = "OnTriggerStay2D";

		// Token: 0x040000AE RID: 174
		public const string OnAnimatorMove = "OnAnimatorMove";

		// Token: 0x040000AF RID: 175
		public const string OnAnimatorIK = "OnAnimatorIK";

		// Token: 0x040000B0 RID: 176
		public const string OnButtonClick = "OnButtonClick";

		// Token: 0x040000B1 RID: 177
		public const string OnToggleValueChanged = "OnToggleValueChanged";

		// Token: 0x040000B2 RID: 178
		public const string OnSliderValueChanged = "OnSliderValueChanged";

		// Token: 0x040000B3 RID: 179
		public const string OnScrollbarValueChanged = "OnScrollbarValueChanged";

		// Token: 0x040000B4 RID: 180
		public const string OnDropdownValueChanged = "OnDropdownValueChanged";

		// Token: 0x040000B5 RID: 181
		public const string OnInputFieldValueChanged = "OnInputFieldValueChanged";

		// Token: 0x040000B6 RID: 182
		public const string OnInputFieldEndEdit = "OnInputFieldEndEdit";

		// Token: 0x040000B7 RID: 183
		public const string OnScrollRectValueChanged = "OnScrollRectValueChanged";
	}
}
