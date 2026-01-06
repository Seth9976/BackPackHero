using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000294 RID: 660
	public interface IStyle
	{
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060015F3 RID: 5619
		// (set) Token: 0x060015F4 RID: 5620
		StyleEnum<Align> alignContent { get; set; }

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x060015F5 RID: 5621
		// (set) Token: 0x060015F6 RID: 5622
		StyleEnum<Align> alignItems { get; set; }

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x060015F7 RID: 5623
		// (set) Token: 0x060015F8 RID: 5624
		StyleEnum<Align> alignSelf { get; set; }

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060015F9 RID: 5625
		// (set) Token: 0x060015FA RID: 5626
		StyleColor backgroundColor { get; set; }

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060015FB RID: 5627
		// (set) Token: 0x060015FC RID: 5628
		StyleBackground backgroundImage { get; set; }

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060015FD RID: 5629
		// (set) Token: 0x060015FE RID: 5630
		StyleColor borderBottomColor { get; set; }

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060015FF RID: 5631
		// (set) Token: 0x06001600 RID: 5632
		StyleLength borderBottomLeftRadius { get; set; }

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001601 RID: 5633
		// (set) Token: 0x06001602 RID: 5634
		StyleLength borderBottomRightRadius { get; set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001603 RID: 5635
		// (set) Token: 0x06001604 RID: 5636
		StyleFloat borderBottomWidth { get; set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001605 RID: 5637
		// (set) Token: 0x06001606 RID: 5638
		StyleColor borderLeftColor { get; set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001607 RID: 5639
		// (set) Token: 0x06001608 RID: 5640
		StyleFloat borderLeftWidth { get; set; }

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001609 RID: 5641
		// (set) Token: 0x0600160A RID: 5642
		StyleColor borderRightColor { get; set; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600160B RID: 5643
		// (set) Token: 0x0600160C RID: 5644
		StyleFloat borderRightWidth { get; set; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600160D RID: 5645
		// (set) Token: 0x0600160E RID: 5646
		StyleColor borderTopColor { get; set; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600160F RID: 5647
		// (set) Token: 0x06001610 RID: 5648
		StyleLength borderTopLeftRadius { get; set; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001611 RID: 5649
		// (set) Token: 0x06001612 RID: 5650
		StyleLength borderTopRightRadius { get; set; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001613 RID: 5651
		// (set) Token: 0x06001614 RID: 5652
		StyleFloat borderTopWidth { get; set; }

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001615 RID: 5653
		// (set) Token: 0x06001616 RID: 5654
		StyleLength bottom { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001617 RID: 5655
		// (set) Token: 0x06001618 RID: 5656
		StyleColor color { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001619 RID: 5657
		// (set) Token: 0x0600161A RID: 5658
		StyleCursor cursor { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600161B RID: 5659
		// (set) Token: 0x0600161C RID: 5660
		StyleEnum<DisplayStyle> display { get; set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600161D RID: 5661
		// (set) Token: 0x0600161E RID: 5662
		StyleLength flexBasis { get; set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600161F RID: 5663
		// (set) Token: 0x06001620 RID: 5664
		StyleEnum<FlexDirection> flexDirection { get; set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001621 RID: 5665
		// (set) Token: 0x06001622 RID: 5666
		StyleFloat flexGrow { get; set; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001623 RID: 5667
		// (set) Token: 0x06001624 RID: 5668
		StyleFloat flexShrink { get; set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001625 RID: 5669
		// (set) Token: 0x06001626 RID: 5670
		StyleEnum<Wrap> flexWrap { get; set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001627 RID: 5671
		// (set) Token: 0x06001628 RID: 5672
		StyleLength fontSize { get; set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001629 RID: 5673
		// (set) Token: 0x0600162A RID: 5674
		StyleLength height { get; set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600162B RID: 5675
		// (set) Token: 0x0600162C RID: 5676
		StyleEnum<Justify> justifyContent { get; set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600162D RID: 5677
		// (set) Token: 0x0600162E RID: 5678
		StyleLength left { get; set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600162F RID: 5679
		// (set) Token: 0x06001630 RID: 5680
		StyleLength letterSpacing { get; set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001631 RID: 5681
		// (set) Token: 0x06001632 RID: 5682
		StyleLength marginBottom { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001633 RID: 5683
		// (set) Token: 0x06001634 RID: 5684
		StyleLength marginLeft { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001635 RID: 5685
		// (set) Token: 0x06001636 RID: 5686
		StyleLength marginRight { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001637 RID: 5687
		// (set) Token: 0x06001638 RID: 5688
		StyleLength marginTop { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001639 RID: 5689
		// (set) Token: 0x0600163A RID: 5690
		StyleLength maxHeight { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600163B RID: 5691
		// (set) Token: 0x0600163C RID: 5692
		StyleLength maxWidth { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600163D RID: 5693
		// (set) Token: 0x0600163E RID: 5694
		StyleLength minHeight { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600163F RID: 5695
		// (set) Token: 0x06001640 RID: 5696
		StyleLength minWidth { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001641 RID: 5697
		// (set) Token: 0x06001642 RID: 5698
		StyleFloat opacity { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001643 RID: 5699
		// (set) Token: 0x06001644 RID: 5700
		StyleEnum<Overflow> overflow { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001645 RID: 5701
		// (set) Token: 0x06001646 RID: 5702
		StyleLength paddingBottom { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001647 RID: 5703
		// (set) Token: 0x06001648 RID: 5704
		StyleLength paddingLeft { get; set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001649 RID: 5705
		// (set) Token: 0x0600164A RID: 5706
		StyleLength paddingRight { get; set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600164B RID: 5707
		// (set) Token: 0x0600164C RID: 5708
		StyleLength paddingTop { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600164D RID: 5709
		// (set) Token: 0x0600164E RID: 5710
		StyleEnum<Position> position { get; set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600164F RID: 5711
		// (set) Token: 0x06001650 RID: 5712
		StyleLength right { get; set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001651 RID: 5713
		// (set) Token: 0x06001652 RID: 5714
		StyleRotate rotate { get; set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001653 RID: 5715
		// (set) Token: 0x06001654 RID: 5716
		StyleScale scale { get; set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001655 RID: 5717
		// (set) Token: 0x06001656 RID: 5718
		StyleEnum<TextOverflow> textOverflow { get; set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001657 RID: 5719
		// (set) Token: 0x06001658 RID: 5720
		StyleTextShadow textShadow { get; set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001659 RID: 5721
		// (set) Token: 0x0600165A RID: 5722
		StyleLength top { get; set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600165B RID: 5723
		// (set) Token: 0x0600165C RID: 5724
		StyleTransformOrigin transformOrigin { get; set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600165D RID: 5725
		// (set) Token: 0x0600165E RID: 5726
		StyleList<TimeValue> transitionDelay { get; set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600165F RID: 5727
		// (set) Token: 0x06001660 RID: 5728
		StyleList<TimeValue> transitionDuration { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001661 RID: 5729
		// (set) Token: 0x06001662 RID: 5730
		StyleList<StylePropertyName> transitionProperty { get; set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001663 RID: 5731
		// (set) Token: 0x06001664 RID: 5732
		StyleList<EasingFunction> transitionTimingFunction { get; set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001665 RID: 5733
		// (set) Token: 0x06001666 RID: 5734
		StyleTranslate translate { get; set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001667 RID: 5735
		// (set) Token: 0x06001668 RID: 5736
		StyleColor unityBackgroundImageTintColor { get; set; }

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001669 RID: 5737
		// (set) Token: 0x0600166A RID: 5738
		StyleEnum<ScaleMode> unityBackgroundScaleMode { get; set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x0600166B RID: 5739
		// (set) Token: 0x0600166C RID: 5740
		StyleFont unityFont { get; set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600166D RID: 5741
		// (set) Token: 0x0600166E RID: 5742
		StyleFontDefinition unityFontDefinition { get; set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600166F RID: 5743
		// (set) Token: 0x06001670 RID: 5744
		StyleEnum<FontStyle> unityFontStyleAndWeight { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001671 RID: 5745
		// (set) Token: 0x06001672 RID: 5746
		StyleEnum<OverflowClipBox> unityOverflowClipBox { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001673 RID: 5747
		// (set) Token: 0x06001674 RID: 5748
		StyleLength unityParagraphSpacing { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001675 RID: 5749
		// (set) Token: 0x06001676 RID: 5750
		StyleInt unitySliceBottom { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001677 RID: 5751
		// (set) Token: 0x06001678 RID: 5752
		StyleInt unitySliceLeft { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001679 RID: 5753
		// (set) Token: 0x0600167A RID: 5754
		StyleInt unitySliceRight { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600167B RID: 5755
		// (set) Token: 0x0600167C RID: 5756
		StyleInt unitySliceTop { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x0600167D RID: 5757
		// (set) Token: 0x0600167E RID: 5758
		StyleEnum<TextAnchor> unityTextAlign { get; set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600167F RID: 5759
		// (set) Token: 0x06001680 RID: 5760
		StyleColor unityTextOutlineColor { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001681 RID: 5761
		// (set) Token: 0x06001682 RID: 5762
		StyleFloat unityTextOutlineWidth { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001683 RID: 5763
		// (set) Token: 0x06001684 RID: 5764
		StyleEnum<TextOverflowPosition> unityTextOverflowPosition { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001685 RID: 5765
		// (set) Token: 0x06001686 RID: 5766
		StyleEnum<Visibility> visibility { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001687 RID: 5767
		// (set) Token: 0x06001688 RID: 5768
		StyleEnum<WhiteSpace> whiteSpace { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001689 RID: 5769
		// (set) Token: 0x0600168A RID: 5770
		StyleLength width { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600168B RID: 5771
		// (set) Token: 0x0600168C RID: 5772
		StyleLength wordSpacing { get; set; }
	}
}
