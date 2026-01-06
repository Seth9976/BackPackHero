using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000293 RID: 659
	public interface IResolvedStyle
	{
		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x060015AA RID: 5546
		Align alignContent { get; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015AB RID: 5547
		Align alignItems { get; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015AC RID: 5548
		Align alignSelf { get; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015AD RID: 5549
		Color backgroundColor { get; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015AE RID: 5550
		Background backgroundImage { get; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015AF RID: 5551
		Color borderBottomColor { get; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015B0 RID: 5552
		float borderBottomLeftRadius { get; }

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060015B1 RID: 5553
		float borderBottomRightRadius { get; }

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060015B2 RID: 5554
		float borderBottomWidth { get; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060015B3 RID: 5555
		Color borderLeftColor { get; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060015B4 RID: 5556
		float borderLeftWidth { get; }

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060015B5 RID: 5557
		Color borderRightColor { get; }

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060015B6 RID: 5558
		float borderRightWidth { get; }

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060015B7 RID: 5559
		Color borderTopColor { get; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060015B8 RID: 5560
		float borderTopLeftRadius { get; }

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060015B9 RID: 5561
		float borderTopRightRadius { get; }

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060015BA RID: 5562
		float borderTopWidth { get; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060015BB RID: 5563
		float bottom { get; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060015BC RID: 5564
		Color color { get; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060015BD RID: 5565
		DisplayStyle display { get; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060015BE RID: 5566
		StyleFloat flexBasis { get; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060015BF RID: 5567
		FlexDirection flexDirection { get; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060015C0 RID: 5568
		float flexGrow { get; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060015C1 RID: 5569
		float flexShrink { get; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060015C2 RID: 5570
		Wrap flexWrap { get; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060015C3 RID: 5571
		float fontSize { get; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060015C4 RID: 5572
		float height { get; }

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060015C5 RID: 5573
		Justify justifyContent { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060015C6 RID: 5574
		float left { get; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060015C7 RID: 5575
		float letterSpacing { get; }

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060015C8 RID: 5576
		float marginBottom { get; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060015C9 RID: 5577
		float marginLeft { get; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060015CA RID: 5578
		float marginRight { get; }

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060015CB RID: 5579
		float marginTop { get; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060015CC RID: 5580
		StyleFloat maxHeight { get; }

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060015CD RID: 5581
		StyleFloat maxWidth { get; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060015CE RID: 5582
		StyleFloat minHeight { get; }

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060015CF RID: 5583
		StyleFloat minWidth { get; }

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060015D0 RID: 5584
		float opacity { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060015D1 RID: 5585
		float paddingBottom { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060015D2 RID: 5586
		float paddingLeft { get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060015D3 RID: 5587
		float paddingRight { get; }

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060015D4 RID: 5588
		float paddingTop { get; }

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015D5 RID: 5589
		Position position { get; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060015D6 RID: 5590
		float right { get; }

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060015D7 RID: 5591
		Rotate rotate { get; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060015D8 RID: 5592
		Scale scale { get; }

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060015D9 RID: 5593
		TextOverflow textOverflow { get; }

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060015DA RID: 5594
		float top { get; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060015DB RID: 5595
		Vector3 transformOrigin { get; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060015DC RID: 5596
		IEnumerable<TimeValue> transitionDelay { get; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060015DD RID: 5597
		IEnumerable<TimeValue> transitionDuration { get; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060015DE RID: 5598
		IEnumerable<StylePropertyName> transitionProperty { get; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060015DF RID: 5599
		IEnumerable<EasingFunction> transitionTimingFunction { get; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060015E0 RID: 5600
		Vector3 translate { get; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060015E1 RID: 5601
		Color unityBackgroundImageTintColor { get; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015E2 RID: 5602
		ScaleMode unityBackgroundScaleMode { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015E3 RID: 5603
		Font unityFont { get; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060015E4 RID: 5604
		FontDefinition unityFontDefinition { get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060015E5 RID: 5605
		FontStyle unityFontStyleAndWeight { get; }

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060015E6 RID: 5606
		float unityParagraphSpacing { get; }

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060015E7 RID: 5607
		int unitySliceBottom { get; }

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060015E8 RID: 5608
		int unitySliceLeft { get; }

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060015E9 RID: 5609
		int unitySliceRight { get; }

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060015EA RID: 5610
		int unitySliceTop { get; }

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060015EB RID: 5611
		TextAnchor unityTextAlign { get; }

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060015EC RID: 5612
		Color unityTextOutlineColor { get; }

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060015ED RID: 5613
		float unityTextOutlineWidth { get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060015EE RID: 5614
		TextOverflowPosition unityTextOverflowPosition { get; }

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x060015EF RID: 5615
		Visibility visibility { get; }

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x060015F0 RID: 5616
		WhiteSpace whiteSpace { get; }

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060015F1 RID: 5617
		float width { get; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060015F2 RID: 5618
		float wordSpacing { get; }
	}
}
