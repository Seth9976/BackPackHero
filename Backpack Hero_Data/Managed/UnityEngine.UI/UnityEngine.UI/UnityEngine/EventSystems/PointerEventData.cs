using System;
using System.Collections.Generic;
using System.Text;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004F RID: 79
	public class PointerEventData : BaseEventData
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00017D52 File Offset: 0x00015F52
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00017D5A File Offset: 0x00015F5A
		public GameObject pointerEnter { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00017D63 File Offset: 0x00015F63
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00017D6B File Offset: 0x00015F6B
		public GameObject lastPress { get; private set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00017D74 File Offset: 0x00015F74
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00017D7C File Offset: 0x00015F7C
		public GameObject rawPointerPress { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00017D85 File Offset: 0x00015F85
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00017D8D File Offset: 0x00015F8D
		public GameObject pointerDrag { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00017D96 File Offset: 0x00015F96
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00017D9E File Offset: 0x00015F9E
		public GameObject pointerClick { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00017DA7 File Offset: 0x00015FA7
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x00017DAF File Offset: 0x00015FAF
		public RaycastResult pointerCurrentRaycast { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00017DB8 File Offset: 0x00015FB8
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00017DC0 File Offset: 0x00015FC0
		public RaycastResult pointerPressRaycast { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x00017DC9 File Offset: 0x00015FC9
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public bool eligibleForClick { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x00017DDA File Offset: 0x00015FDA
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00017DE2 File Offset: 0x00015FE2
		public int pointerId { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x00017DEB File Offset: 0x00015FEB
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00017DF3 File Offset: 0x00015FF3
		public Vector2 position { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x00017DFC File Offset: 0x00015FFC
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00017E04 File Offset: 0x00016004
		public Vector2 delta { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00017E0D File Offset: 0x0001600D
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00017E15 File Offset: 0x00016015
		public Vector2 pressPosition { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00017E1E File Offset: 0x0001601E
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x00017E26 File Offset: 0x00016026
		[Obsolete("Use either pointerCurrentRaycast.worldPosition or pointerPressRaycast.worldPosition")]
		public Vector3 worldPosition { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00017E2F File Offset: 0x0001602F
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00017E37 File Offset: 0x00016037
		[Obsolete("Use either pointerCurrentRaycast.worldNormal or pointerPressRaycast.worldNormal")]
		public Vector3 worldNormal { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00017E40 File Offset: 0x00016040
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x00017E48 File Offset: 0x00016048
		public float clickTime { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x00017E51 File Offset: 0x00016051
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x00017E59 File Offset: 0x00016059
		public int clickCount { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00017E62 File Offset: 0x00016062
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x00017E6A File Offset: 0x0001606A
		public Vector2 scrollDelta { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00017E73 File Offset: 0x00016073
		// (set) Token: 0x06000555 RID: 1365 RVA: 0x00017E7B File Offset: 0x0001607B
		public bool useDragThreshold { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00017E84 File Offset: 0x00016084
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x00017E8C File Offset: 0x0001608C
		public bool dragging { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00017E95 File Offset: 0x00016095
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x00017E9D File Offset: 0x0001609D
		public PointerEventData.InputButton button { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00017EA6 File Offset: 0x000160A6
		// (set) Token: 0x0600055B RID: 1371 RVA: 0x00017EAE File Offset: 0x000160AE
		public float pressure { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x00017EB7 File Offset: 0x000160B7
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x00017EBF File Offset: 0x000160BF
		public float tangentialPressure { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00017EC8 File Offset: 0x000160C8
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00017ED0 File Offset: 0x000160D0
		public float altitudeAngle { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00017ED9 File Offset: 0x000160D9
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00017EE1 File Offset: 0x000160E1
		public float azimuthAngle { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00017EEA File Offset: 0x000160EA
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00017EF2 File Offset: 0x000160F2
		public float twist { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00017EFB File Offset: 0x000160FB
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00017F03 File Offset: 0x00016103
		public Vector2 radius { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00017F0C File Offset: 0x0001610C
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00017F14 File Offset: 0x00016114
		public Vector2 radiusVariance { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00017F1D File Offset: 0x0001611D
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00017F25 File Offset: 0x00016125
		public bool fullyExited { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00017F2E File Offset: 0x0001612E
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00017F36 File Offset: 0x00016136
		public bool reentered { get; set; }

		// Token: 0x0600056C RID: 1388 RVA: 0x00017F40 File Offset: 0x00016140
		public PointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
			this.eligibleForClick = false;
			this.pointerId = -1;
			this.position = Vector2.zero;
			this.delta = Vector2.zero;
			this.pressPosition = Vector2.zero;
			this.clickTime = 0f;
			this.clickCount = 0;
			this.scrollDelta = Vector2.zero;
			this.useDragThreshold = true;
			this.dragging = false;
			this.button = PointerEventData.InputButton.Left;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00018010 File Offset: 0x00016210
		public bool IsPointerMoving()
		{
			return this.delta.sqrMagnitude > 0f;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00018034 File Offset: 0x00016234
		public bool IsScrolling()
		{
			return this.scrollDelta.sqrMagnitude > 0f;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00018056 File Offset: 0x00016256
		public Camera enterEventCamera
		{
			get
			{
				if (!(this.pointerCurrentRaycast.module == null))
				{
					return this.pointerCurrentRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001807D File Offset: 0x0001627D
		public Camera pressEventCamera
		{
			get
			{
				if (!(this.pointerPressRaycast.module == null))
				{
					return this.pointerPressRaycast.module.eventCamera;
				}
				return null;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x000180A4 File Offset: 0x000162A4
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x000180AC File Offset: 0x000162AC
		public GameObject pointerPress
		{
			get
			{
				return this.m_PointerPress;
			}
			set
			{
				if (this.m_PointerPress == value)
				{
					return;
				}
				this.lastPress = this.m_PointerPress;
				this.m_PointerPress = value;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000180D0 File Offset: 0x000162D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("<b>Position</b>: " + this.position.ToString());
			stringBuilder.AppendLine("<b>delta</b>: " + this.delta.ToString());
			stringBuilder.AppendLine("<b>eligibleForClick</b>: " + this.eligibleForClick.ToString());
			string text = "<b>pointerEnter</b>: ";
			GameObject pointerEnter = this.pointerEnter;
			stringBuilder.AppendLine(text + ((pointerEnter != null) ? pointerEnter.ToString() : null));
			string text2 = "<b>pointerPress</b>: ";
			GameObject pointerPress = this.pointerPress;
			stringBuilder.AppendLine(text2 + ((pointerPress != null) ? pointerPress.ToString() : null));
			string text3 = "<b>lastPointerPress</b>: ";
			GameObject lastPress = this.lastPress;
			stringBuilder.AppendLine(text3 + ((lastPress != null) ? lastPress.ToString() : null));
			string text4 = "<b>pointerDrag</b>: ";
			GameObject pointerDrag = this.pointerDrag;
			stringBuilder.AppendLine(text4 + ((pointerDrag != null) ? pointerDrag.ToString() : null));
			stringBuilder.AppendLine("<b>Use Drag Threshold</b>: " + this.useDragThreshold.ToString());
			stringBuilder.AppendLine("<b>Current Raycast:</b>");
			stringBuilder.AppendLine(this.pointerCurrentRaycast.ToString());
			stringBuilder.AppendLine("<b>Press Raycast:</b>");
			stringBuilder.AppendLine(this.pointerPressRaycast.ToString());
			stringBuilder.AppendLine("<b>pressure</b>: " + this.pressure.ToString());
			stringBuilder.AppendLine("<b>tangentialPressure</b>: " + this.tangentialPressure.ToString());
			stringBuilder.AppendLine("<b>altitudeAngle</b>: " + this.altitudeAngle.ToString());
			stringBuilder.AppendLine("<b>azimuthAngle</b>: " + this.azimuthAngle.ToString());
			stringBuilder.AppendLine("<b>twist</b>: " + this.twist.ToString());
			stringBuilder.AppendLine("<b>radius</b>: " + this.radius.ToString());
			stringBuilder.AppendLine("<b>radiusVariance</b>: " + this.radiusVariance.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x040001B0 RID: 432
		private GameObject m_PointerPress;

		// Token: 0x040001B7 RID: 439
		public List<GameObject> hovered = new List<GameObject>();

		// Token: 0x020000C0 RID: 192
		public enum InputButton
		{
			// Token: 0x04000339 RID: 825
			Left,
			// Token: 0x0400033A RID: 826
			Right,
			// Token: 0x0400033B RID: 827
			Middle
		}

		// Token: 0x020000C1 RID: 193
		public enum FramePressState
		{
			// Token: 0x0400033D RID: 829
			Pressed,
			// Token: 0x0400033E RID: 830
			Released,
			// Token: 0x0400033F RID: 831
			PressedAndReleased,
			// Token: 0x04000340 RID: 832
			NotChanged
		}
	}
}
