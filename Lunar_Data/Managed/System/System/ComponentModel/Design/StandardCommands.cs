using System;

namespace System.ComponentModel.Design
{
	/// <summary>Defines identifiers for the standard set of commands that are available to most applications.</summary>
	// Token: 0x0200078A RID: 1930
	public class StandardCommands
	{
		// Token: 0x04002256 RID: 8790
		private static readonly Guid s_standardCommandSet = StandardCommands.ShellGuids.VSStandardCommandSet97;

		// Token: 0x04002257 RID: 8791
		private static readonly Guid s_ndpCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");

		// Token: 0x04002258 RID: 8792
		private const int cmdidDesignerVerbFirst = 8192;

		// Token: 0x04002259 RID: 8793
		private const int cmdidDesignerVerbLast = 8448;

		// Token: 0x0400225A RID: 8794
		private const int cmdidArrangeIcons = 12298;

		// Token: 0x0400225B RID: 8795
		private const int cmdidLineupIcons = 12299;

		// Token: 0x0400225C RID: 8796
		private const int cmdidShowLargeIcons = 12300;

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignBottom command. This field is read-only.</summary>
		// Token: 0x0400225D RID: 8797
		public static readonly CommandID AlignBottom = new CommandID(StandardCommands.s_standardCommandSet, 1);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignHorizontalCenters command. This field is read-only.</summary>
		// Token: 0x0400225E RID: 8798
		public static readonly CommandID AlignHorizontalCenters = new CommandID(StandardCommands.s_standardCommandSet, 2);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignLeft command. This field is read-only.</summary>
		// Token: 0x0400225F RID: 8799
		public static readonly CommandID AlignLeft = new CommandID(StandardCommands.s_standardCommandSet, 3);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignRight command. This field is read-only.</summary>
		// Token: 0x04002260 RID: 8800
		public static readonly CommandID AlignRight = new CommandID(StandardCommands.s_standardCommandSet, 4);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignToGrid command. This field is read-only.</summary>
		// Token: 0x04002261 RID: 8801
		public static readonly CommandID AlignToGrid = new CommandID(StandardCommands.s_standardCommandSet, 5);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignTop command. This field is read-only.</summary>
		// Token: 0x04002262 RID: 8802
		public static readonly CommandID AlignTop = new CommandID(StandardCommands.s_standardCommandSet, 6);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the AlignVerticalCenters command. This field is read-only.</summary>
		// Token: 0x04002263 RID: 8803
		public static readonly CommandID AlignVerticalCenters = new CommandID(StandardCommands.s_standardCommandSet, 7);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeBottom command. This field is read-only.</summary>
		// Token: 0x04002264 RID: 8804
		public static readonly CommandID ArrangeBottom = new CommandID(StandardCommands.s_standardCommandSet, 8);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeRight command. This field is read-only.</summary>
		// Token: 0x04002265 RID: 8805
		public static readonly CommandID ArrangeRight = new CommandID(StandardCommands.s_standardCommandSet, 9);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringForward command. This field is read-only.</summary>
		// Token: 0x04002266 RID: 8806
		public static readonly CommandID BringForward = new CommandID(StandardCommands.s_standardCommandSet, 10);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the BringToFront command. This field is read-only.</summary>
		// Token: 0x04002267 RID: 8807
		public static readonly CommandID BringToFront = new CommandID(StandardCommands.s_standardCommandSet, 11);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterHorizontally command. This field is read-only.</summary>
		// Token: 0x04002268 RID: 8808
		public static readonly CommandID CenterHorizontally = new CommandID(StandardCommands.s_standardCommandSet, 12);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the CenterVertically command. This field is read-only.</summary>
		// Token: 0x04002269 RID: 8809
		public static readonly CommandID CenterVertically = new CommandID(StandardCommands.s_standardCommandSet, 13);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewCode command. This field is read-only.</summary>
		// Token: 0x0400226A RID: 8810
		public static readonly CommandID ViewCode = new CommandID(StandardCommands.s_standardCommandSet, 333);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Document Outline command. This field is read-only.</summary>
		// Token: 0x0400226B RID: 8811
		public static readonly CommandID DocumentOutline = new CommandID(StandardCommands.s_standardCommandSet, 239);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Copy command. This field is read-only.</summary>
		// Token: 0x0400226C RID: 8812
		public static readonly CommandID Copy = new CommandID(StandardCommands.s_standardCommandSet, 15);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Cut command. This field is read-only.</summary>
		// Token: 0x0400226D RID: 8813
		public static readonly CommandID Cut = new CommandID(StandardCommands.s_standardCommandSet, 16);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Delete command. This field is read-only.</summary>
		// Token: 0x0400226E RID: 8814
		public static readonly CommandID Delete = new CommandID(StandardCommands.s_standardCommandSet, 17);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Group command. This field is read-only.</summary>
		// Token: 0x0400226F RID: 8815
		public static readonly CommandID Group = new CommandID(StandardCommands.s_standardCommandSet, 20);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04002270 RID: 8816
		public static readonly CommandID HorizSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 21);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04002271 RID: 8817
		public static readonly CommandID HorizSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 22);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04002272 RID: 8818
		public static readonly CommandID HorizSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 23);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the HorizSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04002273 RID: 8819
		public static readonly CommandID HorizSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 24);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Paste command. This field is read-only.</summary>
		// Token: 0x04002274 RID: 8820
		public static readonly CommandID Paste = new CommandID(StandardCommands.s_standardCommandSet, 26);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Properties command. This field is read-only.</summary>
		// Token: 0x04002275 RID: 8821
		public static readonly CommandID Properties = new CommandID(StandardCommands.s_standardCommandSet, 28);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Redo command. This field is read-only.</summary>
		// Token: 0x04002276 RID: 8822
		public static readonly CommandID Redo = new CommandID(StandardCommands.s_standardCommandSet, 29);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelRedo command. This field is read-only.</summary>
		// Token: 0x04002277 RID: 8823
		public static readonly CommandID MultiLevelRedo = new CommandID(StandardCommands.s_standardCommandSet, 30);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SelectAll command. This field is read-only.</summary>
		// Token: 0x04002278 RID: 8824
		public static readonly CommandID SelectAll = new CommandID(StandardCommands.s_standardCommandSet, 31);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendBackward command. This field is read-only.</summary>
		// Token: 0x04002279 RID: 8825
		public static readonly CommandID SendBackward = new CommandID(StandardCommands.s_standardCommandSet, 32);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SendToBack command. This field is read-only.</summary>
		// Token: 0x0400227A RID: 8826
		public static readonly CommandID SendToBack = new CommandID(StandardCommands.s_standardCommandSet, 33);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControl command. This field is read-only.</summary>
		// Token: 0x0400227B RID: 8827
		public static readonly CommandID SizeToControl = new CommandID(StandardCommands.s_standardCommandSet, 35);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlHeight command. This field is read-only.</summary>
		// Token: 0x0400227C RID: 8828
		public static readonly CommandID SizeToControlHeight = new CommandID(StandardCommands.s_standardCommandSet, 36);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToControlWidth command. This field is read-only.</summary>
		// Token: 0x0400227D RID: 8829
		public static readonly CommandID SizeToControlWidth = new CommandID(StandardCommands.s_standardCommandSet, 37);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToFit command. This field is read-only.</summary>
		// Token: 0x0400227E RID: 8830
		public static readonly CommandID SizeToFit = new CommandID(StandardCommands.s_standardCommandSet, 38);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SizeToGrid command. This field is read-only.</summary>
		// Token: 0x0400227F RID: 8831
		public static readonly CommandID SizeToGrid = new CommandID(StandardCommands.s_standardCommandSet, 39);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the SnapToGrid command. This field is read-only.</summary>
		// Token: 0x04002280 RID: 8832
		public static readonly CommandID SnapToGrid = new CommandID(StandardCommands.s_standardCommandSet, 40);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the TabOrder command. This field is read-only.</summary>
		// Token: 0x04002281 RID: 8833
		public static readonly CommandID TabOrder = new CommandID(StandardCommands.s_standardCommandSet, 41);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Undo command. This field is read-only.</summary>
		// Token: 0x04002282 RID: 8834
		public static readonly CommandID Undo = new CommandID(StandardCommands.s_standardCommandSet, 43);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the MultiLevelUndo command. This field is read-only.</summary>
		// Token: 0x04002283 RID: 8835
		public static readonly CommandID MultiLevelUndo = new CommandID(StandardCommands.s_standardCommandSet, 44);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Ungroup command. This field is read-only.</summary>
		// Token: 0x04002284 RID: 8836
		public static readonly CommandID Ungroup = new CommandID(StandardCommands.s_standardCommandSet, 45);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceConcatenate command. This field is read-only.</summary>
		// Token: 0x04002285 RID: 8837
		public static readonly CommandID VertSpaceConcatenate = new CommandID(StandardCommands.s_standardCommandSet, 46);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceDecrease command. This field is read-only.</summary>
		// Token: 0x04002286 RID: 8838
		public static readonly CommandID VertSpaceDecrease = new CommandID(StandardCommands.s_standardCommandSet, 47);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceIncrease command. This field is read-only.</summary>
		// Token: 0x04002287 RID: 8839
		public static readonly CommandID VertSpaceIncrease = new CommandID(StandardCommands.s_standardCommandSet, 48);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the VertSpaceMakeEqual command. This field is read-only.</summary>
		// Token: 0x04002288 RID: 8840
		public static readonly CommandID VertSpaceMakeEqual = new CommandID(StandardCommands.s_standardCommandSet, 49);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowGrid command. This field is read-only.</summary>
		// Token: 0x04002289 RID: 8841
		public static readonly CommandID ShowGrid = new CommandID(StandardCommands.s_standardCommandSet, 103);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ViewGrid command. This field is read-only.</summary>
		// Token: 0x0400228A RID: 8842
		public static readonly CommandID ViewGrid = new CommandID(StandardCommands.s_standardCommandSet, 125);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the Replace command. This field is read-only.</summary>
		// Token: 0x0400228B RID: 8843
		public static readonly CommandID Replace = new CommandID(StandardCommands.s_standardCommandSet, 230);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the PropertiesWindow command. This field is read-only.</summary>
		// Token: 0x0400228C RID: 8844
		public static readonly CommandID PropertiesWindow = new CommandID(StandardCommands.s_standardCommandSet, 235);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LockControls command. This field is read-only.</summary>
		// Token: 0x0400228D RID: 8845
		public static readonly CommandID LockControls = new CommandID(StandardCommands.s_standardCommandSet, 369);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the F1Help command. This field is read-only.</summary>
		// Token: 0x0400228E RID: 8846
		public static readonly CommandID F1Help = new CommandID(StandardCommands.s_standardCommandSet, 377);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ArrangeIcons command. This field is read-only.</summary>
		// Token: 0x0400228F RID: 8847
		public static readonly CommandID ArrangeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12298);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the LineupIcons command. This field is read-only.</summary>
		// Token: 0x04002290 RID: 8848
		public static readonly CommandID LineupIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12299);

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> for the ShowLargeIcons command. This field is read-only.</summary>
		// Token: 0x04002291 RID: 8849
		public static readonly CommandID ShowLargeIcons = new CommandID(StandardCommands.s_ndpCommandSet, 12300);

		/// <summary>Gets the first of a set of verbs. This field is read-only.</summary>
		// Token: 0x04002292 RID: 8850
		public static readonly CommandID VerbFirst = new CommandID(StandardCommands.s_ndpCommandSet, 8192);

		/// <summary>Gets the last of a set of verbs. This field is read-only.</summary>
		// Token: 0x04002293 RID: 8851
		public static readonly CommandID VerbLast = new CommandID(StandardCommands.s_ndpCommandSet, 8448);

		// Token: 0x0200078B RID: 1931
		private static class VSStandardCommands
		{
			// Token: 0x04002294 RID: 8852
			internal const int cmdidAlignBottom = 1;

			// Token: 0x04002295 RID: 8853
			internal const int cmdidAlignHorizontalCenters = 2;

			// Token: 0x04002296 RID: 8854
			internal const int cmdidAlignLeft = 3;

			// Token: 0x04002297 RID: 8855
			internal const int cmdidAlignRight = 4;

			// Token: 0x04002298 RID: 8856
			internal const int cmdidAlignToGrid = 5;

			// Token: 0x04002299 RID: 8857
			internal const int cmdidAlignTop = 6;

			// Token: 0x0400229A RID: 8858
			internal const int cmdidAlignVerticalCenters = 7;

			// Token: 0x0400229B RID: 8859
			internal const int cmdidArrangeBottom = 8;

			// Token: 0x0400229C RID: 8860
			internal const int cmdidArrangeRight = 9;

			// Token: 0x0400229D RID: 8861
			internal const int cmdidBringForward = 10;

			// Token: 0x0400229E RID: 8862
			internal const int cmdidBringToFront = 11;

			// Token: 0x0400229F RID: 8863
			internal const int cmdidCenterHorizontally = 12;

			// Token: 0x040022A0 RID: 8864
			internal const int cmdidCenterVertically = 13;

			// Token: 0x040022A1 RID: 8865
			internal const int cmdidCode = 14;

			// Token: 0x040022A2 RID: 8866
			internal const int cmdidCopy = 15;

			// Token: 0x040022A3 RID: 8867
			internal const int cmdidCut = 16;

			// Token: 0x040022A4 RID: 8868
			internal const int cmdidDelete = 17;

			// Token: 0x040022A5 RID: 8869
			internal const int cmdidFontName = 18;

			// Token: 0x040022A6 RID: 8870
			internal const int cmdidFontSize = 19;

			// Token: 0x040022A7 RID: 8871
			internal const int cmdidGroup = 20;

			// Token: 0x040022A8 RID: 8872
			internal const int cmdidHorizSpaceConcatenate = 21;

			// Token: 0x040022A9 RID: 8873
			internal const int cmdidHorizSpaceDecrease = 22;

			// Token: 0x040022AA RID: 8874
			internal const int cmdidHorizSpaceIncrease = 23;

			// Token: 0x040022AB RID: 8875
			internal const int cmdidHorizSpaceMakeEqual = 24;

			// Token: 0x040022AC RID: 8876
			internal const int cmdidLockControls = 369;

			// Token: 0x040022AD RID: 8877
			internal const int cmdidInsertObject = 25;

			// Token: 0x040022AE RID: 8878
			internal const int cmdidPaste = 26;

			// Token: 0x040022AF RID: 8879
			internal const int cmdidPrint = 27;

			// Token: 0x040022B0 RID: 8880
			internal const int cmdidProperties = 28;

			// Token: 0x040022B1 RID: 8881
			internal const int cmdidRedo = 29;

			// Token: 0x040022B2 RID: 8882
			internal const int cmdidMultiLevelRedo = 30;

			// Token: 0x040022B3 RID: 8883
			internal const int cmdidSelectAll = 31;

			// Token: 0x040022B4 RID: 8884
			internal const int cmdidSendBackward = 32;

			// Token: 0x040022B5 RID: 8885
			internal const int cmdidSendToBack = 33;

			// Token: 0x040022B6 RID: 8886
			internal const int cmdidShowTable = 34;

			// Token: 0x040022B7 RID: 8887
			internal const int cmdidSizeToControl = 35;

			// Token: 0x040022B8 RID: 8888
			internal const int cmdidSizeToControlHeight = 36;

			// Token: 0x040022B9 RID: 8889
			internal const int cmdidSizeToControlWidth = 37;

			// Token: 0x040022BA RID: 8890
			internal const int cmdidSizeToFit = 38;

			// Token: 0x040022BB RID: 8891
			internal const int cmdidSizeToGrid = 39;

			// Token: 0x040022BC RID: 8892
			internal const int cmdidSnapToGrid = 40;

			// Token: 0x040022BD RID: 8893
			internal const int cmdidTabOrder = 41;

			// Token: 0x040022BE RID: 8894
			internal const int cmdidToolbox = 42;

			// Token: 0x040022BF RID: 8895
			internal const int cmdidUndo = 43;

			// Token: 0x040022C0 RID: 8896
			internal const int cmdidMultiLevelUndo = 44;

			// Token: 0x040022C1 RID: 8897
			internal const int cmdidUngroup = 45;

			// Token: 0x040022C2 RID: 8898
			internal const int cmdidVertSpaceConcatenate = 46;

			// Token: 0x040022C3 RID: 8899
			internal const int cmdidVertSpaceDecrease = 47;

			// Token: 0x040022C4 RID: 8900
			internal const int cmdidVertSpaceIncrease = 48;

			// Token: 0x040022C5 RID: 8901
			internal const int cmdidVertSpaceMakeEqual = 49;

			// Token: 0x040022C6 RID: 8902
			internal const int cmdidZoomPercent = 50;

			// Token: 0x040022C7 RID: 8903
			internal const int cmdidBackColor = 51;

			// Token: 0x040022C8 RID: 8904
			internal const int cmdidBold = 52;

			// Token: 0x040022C9 RID: 8905
			internal const int cmdidBorderColor = 53;

			// Token: 0x040022CA RID: 8906
			internal const int cmdidBorderDashDot = 54;

			// Token: 0x040022CB RID: 8907
			internal const int cmdidBorderDashDotDot = 55;

			// Token: 0x040022CC RID: 8908
			internal const int cmdidBorderDashes = 56;

			// Token: 0x040022CD RID: 8909
			internal const int cmdidBorderDots = 57;

			// Token: 0x040022CE RID: 8910
			internal const int cmdidBorderShortDashes = 58;

			// Token: 0x040022CF RID: 8911
			internal const int cmdidBorderSolid = 59;

			// Token: 0x040022D0 RID: 8912
			internal const int cmdidBorderSparseDots = 60;

			// Token: 0x040022D1 RID: 8913
			internal const int cmdidBorderWidth1 = 61;

			// Token: 0x040022D2 RID: 8914
			internal const int cmdidBorderWidth2 = 62;

			// Token: 0x040022D3 RID: 8915
			internal const int cmdidBorderWidth3 = 63;

			// Token: 0x040022D4 RID: 8916
			internal const int cmdidBorderWidth4 = 64;

			// Token: 0x040022D5 RID: 8917
			internal const int cmdidBorderWidth5 = 65;

			// Token: 0x040022D6 RID: 8918
			internal const int cmdidBorderWidth6 = 66;

			// Token: 0x040022D7 RID: 8919
			internal const int cmdidBorderWidthHairline = 67;

			// Token: 0x040022D8 RID: 8920
			internal const int cmdidFlat = 68;

			// Token: 0x040022D9 RID: 8921
			internal const int cmdidForeColor = 69;

			// Token: 0x040022DA RID: 8922
			internal const int cmdidItalic = 70;

			// Token: 0x040022DB RID: 8923
			internal const int cmdidJustifyCenter = 71;

			// Token: 0x040022DC RID: 8924
			internal const int cmdidJustifyGeneral = 72;

			// Token: 0x040022DD RID: 8925
			internal const int cmdidJustifyLeft = 73;

			// Token: 0x040022DE RID: 8926
			internal const int cmdidJustifyRight = 74;

			// Token: 0x040022DF RID: 8927
			internal const int cmdidRaised = 75;

			// Token: 0x040022E0 RID: 8928
			internal const int cmdidSunken = 76;

			// Token: 0x040022E1 RID: 8929
			internal const int cmdidUnderline = 77;

			// Token: 0x040022E2 RID: 8930
			internal const int cmdidChiseled = 78;

			// Token: 0x040022E3 RID: 8931
			internal const int cmdidEtched = 79;

			// Token: 0x040022E4 RID: 8932
			internal const int cmdidShadowed = 80;

			// Token: 0x040022E5 RID: 8933
			internal const int cmdidCompDebug1 = 81;

			// Token: 0x040022E6 RID: 8934
			internal const int cmdidCompDebug2 = 82;

			// Token: 0x040022E7 RID: 8935
			internal const int cmdidCompDebug3 = 83;

			// Token: 0x040022E8 RID: 8936
			internal const int cmdidCompDebug4 = 84;

			// Token: 0x040022E9 RID: 8937
			internal const int cmdidCompDebug5 = 85;

			// Token: 0x040022EA RID: 8938
			internal const int cmdidCompDebug6 = 86;

			// Token: 0x040022EB RID: 8939
			internal const int cmdidCompDebug7 = 87;

			// Token: 0x040022EC RID: 8940
			internal const int cmdidCompDebug8 = 88;

			// Token: 0x040022ED RID: 8941
			internal const int cmdidCompDebug9 = 89;

			// Token: 0x040022EE RID: 8942
			internal const int cmdidCompDebug10 = 90;

			// Token: 0x040022EF RID: 8943
			internal const int cmdidCompDebug11 = 91;

			// Token: 0x040022F0 RID: 8944
			internal const int cmdidCompDebug12 = 92;

			// Token: 0x040022F1 RID: 8945
			internal const int cmdidCompDebug13 = 93;

			// Token: 0x040022F2 RID: 8946
			internal const int cmdidCompDebug14 = 94;

			// Token: 0x040022F3 RID: 8947
			internal const int cmdidCompDebug15 = 95;

			// Token: 0x040022F4 RID: 8948
			internal const int cmdidExistingSchemaEdit = 96;

			// Token: 0x040022F5 RID: 8949
			internal const int cmdidFind = 97;

			// Token: 0x040022F6 RID: 8950
			internal const int cmdidGetZoom = 98;

			// Token: 0x040022F7 RID: 8951
			internal const int cmdidQueryOpenDesign = 99;

			// Token: 0x040022F8 RID: 8952
			internal const int cmdidQueryOpenNew = 100;

			// Token: 0x040022F9 RID: 8953
			internal const int cmdidSingleTableDesign = 101;

			// Token: 0x040022FA RID: 8954
			internal const int cmdidSingleTableNew = 102;

			// Token: 0x040022FB RID: 8955
			internal const int cmdidShowGrid = 103;

			// Token: 0x040022FC RID: 8956
			internal const int cmdidNewTable = 104;

			// Token: 0x040022FD RID: 8957
			internal const int cmdidCollapsedView = 105;

			// Token: 0x040022FE RID: 8958
			internal const int cmdidFieldView = 106;

			// Token: 0x040022FF RID: 8959
			internal const int cmdidVerifySQL = 107;

			// Token: 0x04002300 RID: 8960
			internal const int cmdidHideTable = 108;

			// Token: 0x04002301 RID: 8961
			internal const int cmdidPrimaryKey = 109;

			// Token: 0x04002302 RID: 8962
			internal const int cmdidSave = 110;

			// Token: 0x04002303 RID: 8963
			internal const int cmdidSaveAs = 111;

			// Token: 0x04002304 RID: 8964
			internal const int cmdidSortAscending = 112;

			// Token: 0x04002305 RID: 8965
			internal const int cmdidSortDescending = 113;

			// Token: 0x04002306 RID: 8966
			internal const int cmdidAppendQuery = 114;

			// Token: 0x04002307 RID: 8967
			internal const int cmdidCrosstabQuery = 115;

			// Token: 0x04002308 RID: 8968
			internal const int cmdidDeleteQuery = 116;

			// Token: 0x04002309 RID: 8969
			internal const int cmdidMakeTableQuery = 117;

			// Token: 0x0400230A RID: 8970
			internal const int cmdidSelectQuery = 118;

			// Token: 0x0400230B RID: 8971
			internal const int cmdidUpdateQuery = 119;

			// Token: 0x0400230C RID: 8972
			internal const int cmdidParameters = 120;

			// Token: 0x0400230D RID: 8973
			internal const int cmdidTotals = 121;

			// Token: 0x0400230E RID: 8974
			internal const int cmdidViewCollapsed = 122;

			// Token: 0x0400230F RID: 8975
			internal const int cmdidViewFieldList = 123;

			// Token: 0x04002310 RID: 8976
			internal const int cmdidViewKeys = 124;

			// Token: 0x04002311 RID: 8977
			internal const int cmdidViewGrid = 125;

			// Token: 0x04002312 RID: 8978
			internal const int cmdidInnerJoin = 126;

			// Token: 0x04002313 RID: 8979
			internal const int cmdidRightOuterJoin = 127;

			// Token: 0x04002314 RID: 8980
			internal const int cmdidLeftOuterJoin = 128;

			// Token: 0x04002315 RID: 8981
			internal const int cmdidFullOuterJoin = 129;

			// Token: 0x04002316 RID: 8982
			internal const int cmdidUnionJoin = 130;

			// Token: 0x04002317 RID: 8983
			internal const int cmdidShowSQLPane = 131;

			// Token: 0x04002318 RID: 8984
			internal const int cmdidShowGraphicalPane = 132;

			// Token: 0x04002319 RID: 8985
			internal const int cmdidShowDataPane = 133;

			// Token: 0x0400231A RID: 8986
			internal const int cmdidShowQBEPane = 134;

			// Token: 0x0400231B RID: 8987
			internal const int cmdidSelectAllFields = 135;

			// Token: 0x0400231C RID: 8988
			internal const int cmdidOLEObjectMenuButton = 136;

			// Token: 0x0400231D RID: 8989
			internal const int cmdidObjectVerbList0 = 137;

			// Token: 0x0400231E RID: 8990
			internal const int cmdidObjectVerbList1 = 138;

			// Token: 0x0400231F RID: 8991
			internal const int cmdidObjectVerbList2 = 139;

			// Token: 0x04002320 RID: 8992
			internal const int cmdidObjectVerbList3 = 140;

			// Token: 0x04002321 RID: 8993
			internal const int cmdidObjectVerbList4 = 141;

			// Token: 0x04002322 RID: 8994
			internal const int cmdidObjectVerbList5 = 142;

			// Token: 0x04002323 RID: 8995
			internal const int cmdidObjectVerbList6 = 143;

			// Token: 0x04002324 RID: 8996
			internal const int cmdidObjectVerbList7 = 144;

			// Token: 0x04002325 RID: 8997
			internal const int cmdidObjectVerbList8 = 145;

			// Token: 0x04002326 RID: 8998
			internal const int cmdidObjectVerbList9 = 146;

			// Token: 0x04002327 RID: 8999
			internal const int cmdidConvertObject = 147;

			// Token: 0x04002328 RID: 9000
			internal const int cmdidCustomControl = 148;

			// Token: 0x04002329 RID: 9001
			internal const int cmdidCustomizeItem = 149;

			// Token: 0x0400232A RID: 9002
			internal const int cmdidRename = 150;

			// Token: 0x0400232B RID: 9003
			internal const int cmdidImport = 151;

			// Token: 0x0400232C RID: 9004
			internal const int cmdidNewPage = 152;

			// Token: 0x0400232D RID: 9005
			internal const int cmdidMove = 153;

			// Token: 0x0400232E RID: 9006
			internal const int cmdidCancel = 154;

			// Token: 0x0400232F RID: 9007
			internal const int cmdidFont = 155;

			// Token: 0x04002330 RID: 9008
			internal const int cmdidExpandLinks = 156;

			// Token: 0x04002331 RID: 9009
			internal const int cmdidExpandImages = 157;

			// Token: 0x04002332 RID: 9010
			internal const int cmdidExpandPages = 158;

			// Token: 0x04002333 RID: 9011
			internal const int cmdidRefocusDiagram = 159;

			// Token: 0x04002334 RID: 9012
			internal const int cmdidTransitiveClosure = 160;

			// Token: 0x04002335 RID: 9013
			internal const int cmdidCenterDiagram = 161;

			// Token: 0x04002336 RID: 9014
			internal const int cmdidZoomIn = 162;

			// Token: 0x04002337 RID: 9015
			internal const int cmdidZoomOut = 163;

			// Token: 0x04002338 RID: 9016
			internal const int cmdidRemoveFilter = 164;

			// Token: 0x04002339 RID: 9017
			internal const int cmdidHidePane = 165;

			// Token: 0x0400233A RID: 9018
			internal const int cmdidDeleteTable = 166;

			// Token: 0x0400233B RID: 9019
			internal const int cmdidDeleteRelationship = 167;

			// Token: 0x0400233C RID: 9020
			internal const int cmdidRemove = 168;

			// Token: 0x0400233D RID: 9021
			internal const int cmdidJoinLeftAll = 169;

			// Token: 0x0400233E RID: 9022
			internal const int cmdidJoinRightAll = 170;

			// Token: 0x0400233F RID: 9023
			internal const int cmdidAddToOutput = 171;

			// Token: 0x04002340 RID: 9024
			internal const int cmdidOtherQuery = 172;

			// Token: 0x04002341 RID: 9025
			internal const int cmdidGenerateChangeScript = 173;

			// Token: 0x04002342 RID: 9026
			internal const int cmdidSaveSelection = 174;

			// Token: 0x04002343 RID: 9027
			internal const int cmdidAutojoinCurrent = 175;

			// Token: 0x04002344 RID: 9028
			internal const int cmdidAutojoinAlways = 176;

			// Token: 0x04002345 RID: 9029
			internal const int cmdidEditPage = 177;

			// Token: 0x04002346 RID: 9030
			internal const int cmdidViewLinks = 178;

			// Token: 0x04002347 RID: 9031
			internal const int cmdidStop = 179;

			// Token: 0x04002348 RID: 9032
			internal const int cmdidPause = 180;

			// Token: 0x04002349 RID: 9033
			internal const int cmdidResume = 181;

			// Token: 0x0400234A RID: 9034
			internal const int cmdidFilterDiagram = 182;

			// Token: 0x0400234B RID: 9035
			internal const int cmdidShowAllObjects = 183;

			// Token: 0x0400234C RID: 9036
			internal const int cmdidShowApplications = 184;

			// Token: 0x0400234D RID: 9037
			internal const int cmdidShowOtherObjects = 185;

			// Token: 0x0400234E RID: 9038
			internal const int cmdidShowPrimRelationships = 186;

			// Token: 0x0400234F RID: 9039
			internal const int cmdidExpand = 187;

			// Token: 0x04002350 RID: 9040
			internal const int cmdidCollapse = 188;

			// Token: 0x04002351 RID: 9041
			internal const int cmdidRefresh = 189;

			// Token: 0x04002352 RID: 9042
			internal const int cmdidLayout = 190;

			// Token: 0x04002353 RID: 9043
			internal const int cmdidShowResources = 191;

			// Token: 0x04002354 RID: 9044
			internal const int cmdidInsertHTMLWizard = 192;

			// Token: 0x04002355 RID: 9045
			internal const int cmdidShowDownloads = 193;

			// Token: 0x04002356 RID: 9046
			internal const int cmdidShowExternals = 194;

			// Token: 0x04002357 RID: 9047
			internal const int cmdidShowInBoundLinks = 195;

			// Token: 0x04002358 RID: 9048
			internal const int cmdidShowOutBoundLinks = 196;

			// Token: 0x04002359 RID: 9049
			internal const int cmdidShowInAndOutBoundLinks = 197;

			// Token: 0x0400235A RID: 9050
			internal const int cmdidPreview = 198;

			// Token: 0x0400235B RID: 9051
			internal const int cmdidOpen = 261;

			// Token: 0x0400235C RID: 9052
			internal const int cmdidOpenWith = 199;

			// Token: 0x0400235D RID: 9053
			internal const int cmdidShowPages = 200;

			// Token: 0x0400235E RID: 9054
			internal const int cmdidRunQuery = 201;

			// Token: 0x0400235F RID: 9055
			internal const int cmdidClearQuery = 202;

			// Token: 0x04002360 RID: 9056
			internal const int cmdidRecordFirst = 203;

			// Token: 0x04002361 RID: 9057
			internal const int cmdidRecordLast = 204;

			// Token: 0x04002362 RID: 9058
			internal const int cmdidRecordNext = 205;

			// Token: 0x04002363 RID: 9059
			internal const int cmdidRecordPrevious = 206;

			// Token: 0x04002364 RID: 9060
			internal const int cmdidRecordGoto = 207;

			// Token: 0x04002365 RID: 9061
			internal const int cmdidRecordNew = 208;

			// Token: 0x04002366 RID: 9062
			internal const int cmdidInsertNewMenu = 209;

			// Token: 0x04002367 RID: 9063
			internal const int cmdidInsertSeparator = 210;

			// Token: 0x04002368 RID: 9064
			internal const int cmdidEditMenuNames = 211;

			// Token: 0x04002369 RID: 9065
			internal const int cmdidDebugExplorer = 212;

			// Token: 0x0400236A RID: 9066
			internal const int cmdidDebugProcesses = 213;

			// Token: 0x0400236B RID: 9067
			internal const int cmdidViewThreadsWindow = 214;

			// Token: 0x0400236C RID: 9068
			internal const int cmdidWindowUIList = 215;

			// Token: 0x0400236D RID: 9069
			internal const int cmdidNewProject = 216;

			// Token: 0x0400236E RID: 9070
			internal const int cmdidOpenProject = 217;

			// Token: 0x0400236F RID: 9071
			internal const int cmdidOpenSolution = 218;

			// Token: 0x04002370 RID: 9072
			internal const int cmdidCloseSolution = 219;

			// Token: 0x04002371 RID: 9073
			internal const int cmdidFileNew = 221;

			// Token: 0x04002372 RID: 9074
			internal const int cmdidFileOpen = 222;

			// Token: 0x04002373 RID: 9075
			internal const int cmdidFileClose = 223;

			// Token: 0x04002374 RID: 9076
			internal const int cmdidSaveSolution = 224;

			// Token: 0x04002375 RID: 9077
			internal const int cmdidSaveSolutionAs = 225;

			// Token: 0x04002376 RID: 9078
			internal const int cmdidSaveProjectItemAs = 226;

			// Token: 0x04002377 RID: 9079
			internal const int cmdidPageSetup = 227;

			// Token: 0x04002378 RID: 9080
			internal const int cmdidPrintPreview = 228;

			// Token: 0x04002379 RID: 9081
			internal const int cmdidExit = 229;

			// Token: 0x0400237A RID: 9082
			internal const int cmdidReplace = 230;

			// Token: 0x0400237B RID: 9083
			internal const int cmdidGoto = 231;

			// Token: 0x0400237C RID: 9084
			internal const int cmdidPropertyPages = 232;

			// Token: 0x0400237D RID: 9085
			internal const int cmdidFullScreen = 233;

			// Token: 0x0400237E RID: 9086
			internal const int cmdidProjectExplorer = 234;

			// Token: 0x0400237F RID: 9087
			internal const int cmdidPropertiesWindow = 235;

			// Token: 0x04002380 RID: 9088
			internal const int cmdidTaskListWindow = 236;

			// Token: 0x04002381 RID: 9089
			internal const int cmdidOutputWindow = 237;

			// Token: 0x04002382 RID: 9090
			internal const int cmdidObjectBrowser = 238;

			// Token: 0x04002383 RID: 9091
			internal const int cmdidDocOutlineWindow = 239;

			// Token: 0x04002384 RID: 9092
			internal const int cmdidImmediateWindow = 240;

			// Token: 0x04002385 RID: 9093
			internal const int cmdidWatchWindow = 241;

			// Token: 0x04002386 RID: 9094
			internal const int cmdidLocalsWindow = 242;

			// Token: 0x04002387 RID: 9095
			internal const int cmdidCallStack = 243;

			// Token: 0x04002388 RID: 9096
			internal const int cmdidAutosWindow = 747;

			// Token: 0x04002389 RID: 9097
			internal const int cmdidThisWindow = 748;

			// Token: 0x0400238A RID: 9098
			internal const int cmdidAddNewItem = 220;

			// Token: 0x0400238B RID: 9099
			internal const int cmdidAddExistingItem = 244;

			// Token: 0x0400238C RID: 9100
			internal const int cmdidNewFolder = 245;

			// Token: 0x0400238D RID: 9101
			internal const int cmdidSetStartupProject = 246;

			// Token: 0x0400238E RID: 9102
			internal const int cmdidProjectSettings = 247;

			// Token: 0x0400238F RID: 9103
			internal const int cmdidProjectReferences = 367;

			// Token: 0x04002390 RID: 9104
			internal const int cmdidStepInto = 248;

			// Token: 0x04002391 RID: 9105
			internal const int cmdidStepOver = 249;

			// Token: 0x04002392 RID: 9106
			internal const int cmdidStepOut = 250;

			// Token: 0x04002393 RID: 9107
			internal const int cmdidRunToCursor = 251;

			// Token: 0x04002394 RID: 9108
			internal const int cmdidAddWatch = 252;

			// Token: 0x04002395 RID: 9109
			internal const int cmdidEditWatch = 253;

			// Token: 0x04002396 RID: 9110
			internal const int cmdidQuickWatch = 254;

			// Token: 0x04002397 RID: 9111
			internal const int cmdidToggleBreakpoint = 255;

			// Token: 0x04002398 RID: 9112
			internal const int cmdidClearBreakpoints = 256;

			// Token: 0x04002399 RID: 9113
			internal const int cmdidShowBreakpoints = 257;

			// Token: 0x0400239A RID: 9114
			internal const int cmdidSetNextStatement = 258;

			// Token: 0x0400239B RID: 9115
			internal const int cmdidShowNextStatement = 259;

			// Token: 0x0400239C RID: 9116
			internal const int cmdidEditBreakpoint = 260;

			// Token: 0x0400239D RID: 9117
			internal const int cmdidDetachDebugger = 262;

			// Token: 0x0400239E RID: 9118
			internal const int cmdidCustomizeKeyboard = 263;

			// Token: 0x0400239F RID: 9119
			internal const int cmdidToolsOptions = 264;

			// Token: 0x040023A0 RID: 9120
			internal const int cmdidNewWindow = 265;

			// Token: 0x040023A1 RID: 9121
			internal const int cmdidSplit = 266;

			// Token: 0x040023A2 RID: 9122
			internal const int cmdidCascade = 267;

			// Token: 0x040023A3 RID: 9123
			internal const int cmdidTileHorz = 268;

			// Token: 0x040023A4 RID: 9124
			internal const int cmdidTileVert = 269;

			// Token: 0x040023A5 RID: 9125
			internal const int cmdidTechSupport = 270;

			// Token: 0x040023A6 RID: 9126
			internal const int cmdidAbout = 271;

			// Token: 0x040023A7 RID: 9127
			internal const int cmdidDebugOptions = 272;

			// Token: 0x040023A8 RID: 9128
			internal const int cmdidDeleteWatch = 274;

			// Token: 0x040023A9 RID: 9129
			internal const int cmdidCollapseWatch = 275;

			// Token: 0x040023AA RID: 9130
			internal const int cmdidPbrsToggleStatus = 282;

			// Token: 0x040023AB RID: 9131
			internal const int cmdidPropbrsHide = 283;

			// Token: 0x040023AC RID: 9132
			internal const int cmdidDockingView = 284;

			// Token: 0x040023AD RID: 9133
			internal const int cmdidHideActivePane = 285;

			// Token: 0x040023AE RID: 9134
			internal const int cmdidPaneNextTab = 286;

			// Token: 0x040023AF RID: 9135
			internal const int cmdidPanePrevTab = 287;

			// Token: 0x040023B0 RID: 9136
			internal const int cmdidPaneCloseToolWindow = 288;

			// Token: 0x040023B1 RID: 9137
			internal const int cmdidPaneActivateDocWindow = 289;

			// Token: 0x040023B2 RID: 9138
			internal const int cmdidDockingViewFloater = 291;

			// Token: 0x040023B3 RID: 9139
			internal const int cmdidAutoHideWindow = 292;

			// Token: 0x040023B4 RID: 9140
			internal const int cmdidMoveToDropdownBar = 293;

			// Token: 0x040023B5 RID: 9141
			internal const int cmdidFindCmd = 294;

			// Token: 0x040023B6 RID: 9142
			internal const int cmdidStart = 295;

			// Token: 0x040023B7 RID: 9143
			internal const int cmdidRestart = 296;

			// Token: 0x040023B8 RID: 9144
			internal const int cmdidAddinManager = 297;

			// Token: 0x040023B9 RID: 9145
			internal const int cmdidMultiLevelUndoList = 298;

			// Token: 0x040023BA RID: 9146
			internal const int cmdidMultiLevelRedoList = 299;

			// Token: 0x040023BB RID: 9147
			internal const int cmdidToolboxAddTab = 300;

			// Token: 0x040023BC RID: 9148
			internal const int cmdidToolboxDeleteTab = 301;

			// Token: 0x040023BD RID: 9149
			internal const int cmdidToolboxRenameTab = 302;

			// Token: 0x040023BE RID: 9150
			internal const int cmdidToolboxTabMoveUp = 303;

			// Token: 0x040023BF RID: 9151
			internal const int cmdidToolboxTabMoveDown = 304;

			// Token: 0x040023C0 RID: 9152
			internal const int cmdidToolboxRenameItem = 305;

			// Token: 0x040023C1 RID: 9153
			internal const int cmdidToolboxListView = 306;

			// Token: 0x040023C2 RID: 9154
			internal const int cmdidWindowUIGetList = 308;

			// Token: 0x040023C3 RID: 9155
			internal const int cmdidInsertValuesQuery = 309;

			// Token: 0x040023C4 RID: 9156
			internal const int cmdidShowProperties = 310;

			// Token: 0x040023C5 RID: 9157
			internal const int cmdidThreadSuspend = 311;

			// Token: 0x040023C6 RID: 9158
			internal const int cmdidThreadResume = 312;

			// Token: 0x040023C7 RID: 9159
			internal const int cmdidThreadSetFocus = 313;

			// Token: 0x040023C8 RID: 9160
			internal const int cmdidDisplayRadix = 314;

			// Token: 0x040023C9 RID: 9161
			internal const int cmdidOpenProjectItem = 315;

			// Token: 0x040023CA RID: 9162
			internal const int cmdidPaneNextPane = 316;

			// Token: 0x040023CB RID: 9163
			internal const int cmdidPanePrevPane = 317;

			// Token: 0x040023CC RID: 9164
			internal const int cmdidClearPane = 318;

			// Token: 0x040023CD RID: 9165
			internal const int cmdidGotoErrorTag = 319;

			// Token: 0x040023CE RID: 9166
			internal const int cmdidTaskListSortByCategory = 320;

			// Token: 0x040023CF RID: 9167
			internal const int cmdidTaskListSortByFileLine = 321;

			// Token: 0x040023D0 RID: 9168
			internal const int cmdidTaskListSortByPriority = 322;

			// Token: 0x040023D1 RID: 9169
			internal const int cmdidTaskListSortByDefaultSort = 323;

			// Token: 0x040023D2 RID: 9170
			internal const int cmdidTaskListFilterByNothing = 325;

			// Token: 0x040023D3 RID: 9171
			internal const int cmdidTaskListFilterByCategoryCodeSense = 326;

			// Token: 0x040023D4 RID: 9172
			internal const int cmdidTaskListFilterByCategoryCompiler = 327;

			// Token: 0x040023D5 RID: 9173
			internal const int cmdidTaskListFilterByCategoryComment = 328;

			// Token: 0x040023D6 RID: 9174
			internal const int cmdidToolboxAddItem = 329;

			// Token: 0x040023D7 RID: 9175
			internal const int cmdidToolboxReset = 330;

			// Token: 0x040023D8 RID: 9176
			internal const int cmdidSaveProjectItem = 331;

			// Token: 0x040023D9 RID: 9177
			internal const int cmdidViewForm = 332;

			// Token: 0x040023DA RID: 9178
			internal const int cmdidViewCode = 333;

			// Token: 0x040023DB RID: 9179
			internal const int cmdidPreviewInBrowser = 334;

			// Token: 0x040023DC RID: 9180
			internal const int cmdidBrowseWith = 336;

			// Token: 0x040023DD RID: 9181
			internal const int cmdidSearchSetCombo = 307;

			// Token: 0x040023DE RID: 9182
			internal const int cmdidSearchCombo = 337;

			// Token: 0x040023DF RID: 9183
			internal const int cmdidEditLabel = 338;

			// Token: 0x040023E0 RID: 9184
			internal const int cmdidExceptions = 339;

			// Token: 0x040023E1 RID: 9185
			internal const int cmdidToggleSelMode = 341;

			// Token: 0x040023E2 RID: 9186
			internal const int cmdidToggleInsMode = 342;

			// Token: 0x040023E3 RID: 9187
			internal const int cmdidLoadUnloadedProject = 343;

			// Token: 0x040023E4 RID: 9188
			internal const int cmdidUnloadLoadedProject = 344;

			// Token: 0x040023E5 RID: 9189
			internal const int cmdidElasticColumn = 345;

			// Token: 0x040023E6 RID: 9190
			internal const int cmdidHideColumn = 346;

			// Token: 0x040023E7 RID: 9191
			internal const int cmdidTaskListPreviousView = 347;

			// Token: 0x040023E8 RID: 9192
			internal const int cmdidZoomDialog = 348;

			// Token: 0x040023E9 RID: 9193
			internal const int cmdidFindNew = 349;

			// Token: 0x040023EA RID: 9194
			internal const int cmdidFindMatchCase = 350;

			// Token: 0x040023EB RID: 9195
			internal const int cmdidFindWholeWord = 351;

			// Token: 0x040023EC RID: 9196
			internal const int cmdidFindSimplePattern = 276;

			// Token: 0x040023ED RID: 9197
			internal const int cmdidFindRegularExpression = 352;

			// Token: 0x040023EE RID: 9198
			internal const int cmdidFindBackwards = 353;

			// Token: 0x040023EF RID: 9199
			internal const int cmdidFindInSelection = 354;

			// Token: 0x040023F0 RID: 9200
			internal const int cmdidFindStop = 355;

			// Token: 0x040023F1 RID: 9201
			internal const int cmdidFindHelp = 356;

			// Token: 0x040023F2 RID: 9202
			internal const int cmdidFindInFiles = 277;

			// Token: 0x040023F3 RID: 9203
			internal const int cmdidReplaceInFiles = 278;

			// Token: 0x040023F4 RID: 9204
			internal const int cmdidNextLocation = 279;

			// Token: 0x040023F5 RID: 9205
			internal const int cmdidPreviousLocation = 280;

			// Token: 0x040023F6 RID: 9206
			internal const int cmdidTaskListNextError = 357;

			// Token: 0x040023F7 RID: 9207
			internal const int cmdidTaskListPrevError = 358;

			// Token: 0x040023F8 RID: 9208
			internal const int cmdidTaskListFilterByCategoryUser = 359;

			// Token: 0x040023F9 RID: 9209
			internal const int cmdidTaskListFilterByCategoryShortcut = 360;

			// Token: 0x040023FA RID: 9210
			internal const int cmdidTaskListFilterByCategoryHTML = 361;

			// Token: 0x040023FB RID: 9211
			internal const int cmdidTaskListFilterByCurrentFile = 362;

			// Token: 0x040023FC RID: 9212
			internal const int cmdidTaskListFilterByChecked = 363;

			// Token: 0x040023FD RID: 9213
			internal const int cmdidTaskListFilterByUnchecked = 364;

			// Token: 0x040023FE RID: 9214
			internal const int cmdidTaskListSortByDescription = 365;

			// Token: 0x040023FF RID: 9215
			internal const int cmdidTaskListSortByChecked = 366;

			// Token: 0x04002400 RID: 9216
			internal const int cmdidStartNoDebug = 368;

			// Token: 0x04002401 RID: 9217
			internal const int cmdidFindNext = 370;

			// Token: 0x04002402 RID: 9218
			internal const int cmdidFindPrev = 371;

			// Token: 0x04002403 RID: 9219
			internal const int cmdidFindSelectedNext = 372;

			// Token: 0x04002404 RID: 9220
			internal const int cmdidFindSelectedPrev = 373;

			// Token: 0x04002405 RID: 9221
			internal const int cmdidSearchGetList = 374;

			// Token: 0x04002406 RID: 9222
			internal const int cmdidInsertBreakpoint = 375;

			// Token: 0x04002407 RID: 9223
			internal const int cmdidEnableBreakpoint = 376;

			// Token: 0x04002408 RID: 9224
			internal const int cmdidF1Help = 377;

			// Token: 0x04002409 RID: 9225
			internal const int cmdidPropSheetOrProperties = 397;

			// Token: 0x0400240A RID: 9226
			internal const int cmdidTshellStep = 398;

			// Token: 0x0400240B RID: 9227
			internal const int cmdidTshellRun = 399;

			// Token: 0x0400240C RID: 9228
			internal const int cmdidMarkerCmd0 = 400;

			// Token: 0x0400240D RID: 9229
			internal const int cmdidMarkerCmd1 = 401;

			// Token: 0x0400240E RID: 9230
			internal const int cmdidMarkerCmd2 = 402;

			// Token: 0x0400240F RID: 9231
			internal const int cmdidMarkerCmd3 = 403;

			// Token: 0x04002410 RID: 9232
			internal const int cmdidMarkerCmd4 = 404;

			// Token: 0x04002411 RID: 9233
			internal const int cmdidMarkerCmd5 = 405;

			// Token: 0x04002412 RID: 9234
			internal const int cmdidMarkerCmd6 = 406;

			// Token: 0x04002413 RID: 9235
			internal const int cmdidMarkerCmd7 = 407;

			// Token: 0x04002414 RID: 9236
			internal const int cmdidMarkerCmd8 = 408;

			// Token: 0x04002415 RID: 9237
			internal const int cmdidMarkerCmd9 = 409;

			// Token: 0x04002416 RID: 9238
			internal const int cmdidMarkerLast = 409;

			// Token: 0x04002417 RID: 9239
			internal const int cmdidMarkerEnd = 410;

			// Token: 0x04002418 RID: 9240
			internal const int cmdidReloadProject = 412;

			// Token: 0x04002419 RID: 9241
			internal const int cmdidUnloadProject = 413;

			// Token: 0x0400241A RID: 9242
			internal const int cmdidDetachAttachOutline = 420;

			// Token: 0x0400241B RID: 9243
			internal const int cmdidShowHideOutline = 421;

			// Token: 0x0400241C RID: 9244
			internal const int cmdidSyncOutline = 422;

			// Token: 0x0400241D RID: 9245
			internal const int cmdidRunToCallstCursor = 423;

			// Token: 0x0400241E RID: 9246
			internal const int cmdidNoCmdsAvailable = 424;

			// Token: 0x0400241F RID: 9247
			internal const int cmdidContextWindow = 427;

			// Token: 0x04002420 RID: 9248
			internal const int cmdidAlias = 428;

			// Token: 0x04002421 RID: 9249
			internal const int cmdidGotoCommandLine = 429;

			// Token: 0x04002422 RID: 9250
			internal const int cmdidEvaluateExpression = 430;

			// Token: 0x04002423 RID: 9251
			internal const int cmdidImmediateMode = 431;

			// Token: 0x04002424 RID: 9252
			internal const int cmdidEvaluateStatement = 432;

			// Token: 0x04002425 RID: 9253
			internal const int cmdidFindResultWindow1 = 433;

			// Token: 0x04002426 RID: 9254
			internal const int cmdidFindResultWindow2 = 434;

			// Token: 0x04002427 RID: 9255
			internal const int cmdidWindow1 = 570;

			// Token: 0x04002428 RID: 9256
			internal const int cmdidWindow2 = 571;

			// Token: 0x04002429 RID: 9257
			internal const int cmdidWindow3 = 572;

			// Token: 0x0400242A RID: 9258
			internal const int cmdidWindow4 = 573;

			// Token: 0x0400242B RID: 9259
			internal const int cmdidWindow5 = 574;

			// Token: 0x0400242C RID: 9260
			internal const int cmdidWindow6 = 575;

			// Token: 0x0400242D RID: 9261
			internal const int cmdidWindow7 = 576;

			// Token: 0x0400242E RID: 9262
			internal const int cmdidWindow8 = 577;

			// Token: 0x0400242F RID: 9263
			internal const int cmdidWindow9 = 578;

			// Token: 0x04002430 RID: 9264
			internal const int cmdidWindow10 = 579;

			// Token: 0x04002431 RID: 9265
			internal const int cmdidWindow11 = 580;

			// Token: 0x04002432 RID: 9266
			internal const int cmdidWindow12 = 581;

			// Token: 0x04002433 RID: 9267
			internal const int cmdidWindow13 = 582;

			// Token: 0x04002434 RID: 9268
			internal const int cmdidWindow14 = 583;

			// Token: 0x04002435 RID: 9269
			internal const int cmdidWindow15 = 584;

			// Token: 0x04002436 RID: 9270
			internal const int cmdidWindow16 = 585;

			// Token: 0x04002437 RID: 9271
			internal const int cmdidWindow17 = 586;

			// Token: 0x04002438 RID: 9272
			internal const int cmdidWindow18 = 587;

			// Token: 0x04002439 RID: 9273
			internal const int cmdidWindow19 = 588;

			// Token: 0x0400243A RID: 9274
			internal const int cmdidWindow20 = 589;

			// Token: 0x0400243B RID: 9275
			internal const int cmdidWindow21 = 590;

			// Token: 0x0400243C RID: 9276
			internal const int cmdidWindow22 = 591;

			// Token: 0x0400243D RID: 9277
			internal const int cmdidWindow23 = 592;

			// Token: 0x0400243E RID: 9278
			internal const int cmdidWindow24 = 593;

			// Token: 0x0400243F RID: 9279
			internal const int cmdidWindow25 = 594;

			// Token: 0x04002440 RID: 9280
			internal const int cmdidMoreWindows = 595;

			// Token: 0x04002441 RID: 9281
			internal const int cmdidTaskListTaskHelp = 598;

			// Token: 0x04002442 RID: 9282
			internal const int cmdidClassView = 599;

			// Token: 0x04002443 RID: 9283
			internal const int cmdidMRUProj1 = 600;

			// Token: 0x04002444 RID: 9284
			internal const int cmdidMRUProj2 = 601;

			// Token: 0x04002445 RID: 9285
			internal const int cmdidMRUProj3 = 602;

			// Token: 0x04002446 RID: 9286
			internal const int cmdidMRUProj4 = 603;

			// Token: 0x04002447 RID: 9287
			internal const int cmdidMRUProj5 = 604;

			// Token: 0x04002448 RID: 9288
			internal const int cmdidMRUProj6 = 605;

			// Token: 0x04002449 RID: 9289
			internal const int cmdidMRUProj7 = 606;

			// Token: 0x0400244A RID: 9290
			internal const int cmdidMRUProj8 = 607;

			// Token: 0x0400244B RID: 9291
			internal const int cmdidMRUProj9 = 608;

			// Token: 0x0400244C RID: 9292
			internal const int cmdidMRUProj10 = 609;

			// Token: 0x0400244D RID: 9293
			internal const int cmdidMRUProj11 = 610;

			// Token: 0x0400244E RID: 9294
			internal const int cmdidMRUProj12 = 611;

			// Token: 0x0400244F RID: 9295
			internal const int cmdidMRUProj13 = 612;

			// Token: 0x04002450 RID: 9296
			internal const int cmdidMRUProj14 = 613;

			// Token: 0x04002451 RID: 9297
			internal const int cmdidMRUProj15 = 614;

			// Token: 0x04002452 RID: 9298
			internal const int cmdidMRUProj16 = 615;

			// Token: 0x04002453 RID: 9299
			internal const int cmdidMRUProj17 = 616;

			// Token: 0x04002454 RID: 9300
			internal const int cmdidMRUProj18 = 617;

			// Token: 0x04002455 RID: 9301
			internal const int cmdidMRUProj19 = 618;

			// Token: 0x04002456 RID: 9302
			internal const int cmdidMRUProj20 = 619;

			// Token: 0x04002457 RID: 9303
			internal const int cmdidMRUProj21 = 620;

			// Token: 0x04002458 RID: 9304
			internal const int cmdidMRUProj22 = 621;

			// Token: 0x04002459 RID: 9305
			internal const int cmdidMRUProj23 = 622;

			// Token: 0x0400245A RID: 9306
			internal const int cmdidMRUProj24 = 623;

			// Token: 0x0400245B RID: 9307
			internal const int cmdidMRUProj25 = 624;

			// Token: 0x0400245C RID: 9308
			internal const int cmdidSplitNext = 625;

			// Token: 0x0400245D RID: 9309
			internal const int cmdidSplitPrev = 626;

			// Token: 0x0400245E RID: 9310
			internal const int cmdidCloseAllDocuments = 627;

			// Token: 0x0400245F RID: 9311
			internal const int cmdidNextDocument = 628;

			// Token: 0x04002460 RID: 9312
			internal const int cmdidPrevDocument = 629;

			// Token: 0x04002461 RID: 9313
			internal const int cmdidTool1 = 630;

			// Token: 0x04002462 RID: 9314
			internal const int cmdidTool2 = 631;

			// Token: 0x04002463 RID: 9315
			internal const int cmdidTool3 = 632;

			// Token: 0x04002464 RID: 9316
			internal const int cmdidTool4 = 633;

			// Token: 0x04002465 RID: 9317
			internal const int cmdidTool5 = 634;

			// Token: 0x04002466 RID: 9318
			internal const int cmdidTool6 = 635;

			// Token: 0x04002467 RID: 9319
			internal const int cmdidTool7 = 636;

			// Token: 0x04002468 RID: 9320
			internal const int cmdidTool8 = 637;

			// Token: 0x04002469 RID: 9321
			internal const int cmdidTool9 = 638;

			// Token: 0x0400246A RID: 9322
			internal const int cmdidTool10 = 639;

			// Token: 0x0400246B RID: 9323
			internal const int cmdidTool11 = 640;

			// Token: 0x0400246C RID: 9324
			internal const int cmdidTool12 = 641;

			// Token: 0x0400246D RID: 9325
			internal const int cmdidTool13 = 642;

			// Token: 0x0400246E RID: 9326
			internal const int cmdidTool14 = 643;

			// Token: 0x0400246F RID: 9327
			internal const int cmdidTool15 = 644;

			// Token: 0x04002470 RID: 9328
			internal const int cmdidTool16 = 645;

			// Token: 0x04002471 RID: 9329
			internal const int cmdidTool17 = 646;

			// Token: 0x04002472 RID: 9330
			internal const int cmdidTool18 = 647;

			// Token: 0x04002473 RID: 9331
			internal const int cmdidTool19 = 648;

			// Token: 0x04002474 RID: 9332
			internal const int cmdidTool20 = 649;

			// Token: 0x04002475 RID: 9333
			internal const int cmdidTool21 = 650;

			// Token: 0x04002476 RID: 9334
			internal const int cmdidTool22 = 651;

			// Token: 0x04002477 RID: 9335
			internal const int cmdidTool23 = 652;

			// Token: 0x04002478 RID: 9336
			internal const int cmdidTool24 = 653;

			// Token: 0x04002479 RID: 9337
			internal const int cmdidExternalCommands = 654;

			// Token: 0x0400247A RID: 9338
			internal const int cmdidPasteNextTBXCBItem = 655;

			// Token: 0x0400247B RID: 9339
			internal const int cmdidToolboxShowAllTabs = 656;

			// Token: 0x0400247C RID: 9340
			internal const int cmdidProjectDependencies = 657;

			// Token: 0x0400247D RID: 9341
			internal const int cmdidCloseDocument = 658;

			// Token: 0x0400247E RID: 9342
			internal const int cmdidToolboxSortItems = 659;

			// Token: 0x0400247F RID: 9343
			internal const int cmdidViewBarView1 = 660;

			// Token: 0x04002480 RID: 9344
			internal const int cmdidViewBarView2 = 661;

			// Token: 0x04002481 RID: 9345
			internal const int cmdidViewBarView3 = 662;

			// Token: 0x04002482 RID: 9346
			internal const int cmdidViewBarView4 = 663;

			// Token: 0x04002483 RID: 9347
			internal const int cmdidViewBarView5 = 664;

			// Token: 0x04002484 RID: 9348
			internal const int cmdidViewBarView6 = 665;

			// Token: 0x04002485 RID: 9349
			internal const int cmdidViewBarView7 = 666;

			// Token: 0x04002486 RID: 9350
			internal const int cmdidViewBarView8 = 667;

			// Token: 0x04002487 RID: 9351
			internal const int cmdidViewBarView9 = 668;

			// Token: 0x04002488 RID: 9352
			internal const int cmdidViewBarView10 = 669;

			// Token: 0x04002489 RID: 9353
			internal const int cmdidViewBarView11 = 670;

			// Token: 0x0400248A RID: 9354
			internal const int cmdidViewBarView12 = 671;

			// Token: 0x0400248B RID: 9355
			internal const int cmdidViewBarView13 = 672;

			// Token: 0x0400248C RID: 9356
			internal const int cmdidViewBarView14 = 673;

			// Token: 0x0400248D RID: 9357
			internal const int cmdidViewBarView15 = 674;

			// Token: 0x0400248E RID: 9358
			internal const int cmdidViewBarView16 = 675;

			// Token: 0x0400248F RID: 9359
			internal const int cmdidViewBarView17 = 676;

			// Token: 0x04002490 RID: 9360
			internal const int cmdidViewBarView18 = 677;

			// Token: 0x04002491 RID: 9361
			internal const int cmdidViewBarView19 = 678;

			// Token: 0x04002492 RID: 9362
			internal const int cmdidViewBarView20 = 679;

			// Token: 0x04002493 RID: 9363
			internal const int cmdidViewBarView21 = 680;

			// Token: 0x04002494 RID: 9364
			internal const int cmdidViewBarView22 = 681;

			// Token: 0x04002495 RID: 9365
			internal const int cmdidViewBarView23 = 682;

			// Token: 0x04002496 RID: 9366
			internal const int cmdidViewBarView24 = 683;

			// Token: 0x04002497 RID: 9367
			internal const int cmdidSolutionCfg = 684;

			// Token: 0x04002498 RID: 9368
			internal const int cmdidSolutionCfgGetList = 685;

			// Token: 0x04002499 RID: 9369
			internal const int cmdidManageIndexes = 675;

			// Token: 0x0400249A RID: 9370
			internal const int cmdidManageRelationships = 676;

			// Token: 0x0400249B RID: 9371
			internal const int cmdidManageConstraints = 677;

			// Token: 0x0400249C RID: 9372
			internal const int cmdidTaskListCustomView1 = 678;

			// Token: 0x0400249D RID: 9373
			internal const int cmdidTaskListCustomView2 = 679;

			// Token: 0x0400249E RID: 9374
			internal const int cmdidTaskListCustomView3 = 680;

			// Token: 0x0400249F RID: 9375
			internal const int cmdidTaskListCustomView4 = 681;

			// Token: 0x040024A0 RID: 9376
			internal const int cmdidTaskListCustomView5 = 682;

			// Token: 0x040024A1 RID: 9377
			internal const int cmdidTaskListCustomView6 = 683;

			// Token: 0x040024A2 RID: 9378
			internal const int cmdidTaskListCustomView7 = 684;

			// Token: 0x040024A3 RID: 9379
			internal const int cmdidTaskListCustomView8 = 685;

			// Token: 0x040024A4 RID: 9380
			internal const int cmdidTaskListCustomView9 = 686;

			// Token: 0x040024A5 RID: 9381
			internal const int cmdidTaskListCustomView10 = 687;

			// Token: 0x040024A6 RID: 9382
			internal const int cmdidTaskListCustomView11 = 688;

			// Token: 0x040024A7 RID: 9383
			internal const int cmdidTaskListCustomView12 = 689;

			// Token: 0x040024A8 RID: 9384
			internal const int cmdidTaskListCustomView13 = 690;

			// Token: 0x040024A9 RID: 9385
			internal const int cmdidTaskListCustomView14 = 691;

			// Token: 0x040024AA RID: 9386
			internal const int cmdidTaskListCustomView15 = 692;

			// Token: 0x040024AB RID: 9387
			internal const int cmdidTaskListCustomView16 = 693;

			// Token: 0x040024AC RID: 9388
			internal const int cmdidTaskListCustomView17 = 694;

			// Token: 0x040024AD RID: 9389
			internal const int cmdidTaskListCustomView18 = 695;

			// Token: 0x040024AE RID: 9390
			internal const int cmdidTaskListCustomView19 = 696;

			// Token: 0x040024AF RID: 9391
			internal const int cmdidTaskListCustomView20 = 697;

			// Token: 0x040024B0 RID: 9392
			internal const int cmdidTaskListCustomView21 = 698;

			// Token: 0x040024B1 RID: 9393
			internal const int cmdidTaskListCustomView22 = 699;

			// Token: 0x040024B2 RID: 9394
			internal const int cmdidTaskListCustomView23 = 700;

			// Token: 0x040024B3 RID: 9395
			internal const int cmdidTaskListCustomView24 = 701;

			// Token: 0x040024B4 RID: 9396
			internal const int cmdidTaskListCustomView25 = 702;

			// Token: 0x040024B5 RID: 9397
			internal const int cmdidTaskListCustomView26 = 703;

			// Token: 0x040024B6 RID: 9398
			internal const int cmdidTaskListCustomView27 = 704;

			// Token: 0x040024B7 RID: 9399
			internal const int cmdidTaskListCustomView28 = 705;

			// Token: 0x040024B8 RID: 9400
			internal const int cmdidTaskListCustomView29 = 706;

			// Token: 0x040024B9 RID: 9401
			internal const int cmdidTaskListCustomView30 = 707;

			// Token: 0x040024BA RID: 9402
			internal const int cmdidTaskListCustomView31 = 708;

			// Token: 0x040024BB RID: 9403
			internal const int cmdidTaskListCustomView32 = 709;

			// Token: 0x040024BC RID: 9404
			internal const int cmdidTaskListCustomView33 = 710;

			// Token: 0x040024BD RID: 9405
			internal const int cmdidTaskListCustomView34 = 711;

			// Token: 0x040024BE RID: 9406
			internal const int cmdidTaskListCustomView35 = 712;

			// Token: 0x040024BF RID: 9407
			internal const int cmdidTaskListCustomView36 = 713;

			// Token: 0x040024C0 RID: 9408
			internal const int cmdidTaskListCustomView37 = 714;

			// Token: 0x040024C1 RID: 9409
			internal const int cmdidTaskListCustomView38 = 715;

			// Token: 0x040024C2 RID: 9410
			internal const int cmdidTaskListCustomView39 = 716;

			// Token: 0x040024C3 RID: 9411
			internal const int cmdidTaskListCustomView40 = 717;

			// Token: 0x040024C4 RID: 9412
			internal const int cmdidTaskListCustomView41 = 718;

			// Token: 0x040024C5 RID: 9413
			internal const int cmdidTaskListCustomView42 = 719;

			// Token: 0x040024C6 RID: 9414
			internal const int cmdidTaskListCustomView43 = 720;

			// Token: 0x040024C7 RID: 9415
			internal const int cmdidTaskListCustomView44 = 721;

			// Token: 0x040024C8 RID: 9416
			internal const int cmdidTaskListCustomView45 = 722;

			// Token: 0x040024C9 RID: 9417
			internal const int cmdidTaskListCustomView46 = 723;

			// Token: 0x040024CA RID: 9418
			internal const int cmdidTaskListCustomView47 = 724;

			// Token: 0x040024CB RID: 9419
			internal const int cmdidTaskListCustomView48 = 725;

			// Token: 0x040024CC RID: 9420
			internal const int cmdidTaskListCustomView49 = 726;

			// Token: 0x040024CD RID: 9421
			internal const int cmdidTaskListCustomView50 = 727;

			// Token: 0x040024CE RID: 9422
			internal const int cmdidObjectSearch = 728;

			// Token: 0x040024CF RID: 9423
			internal const int cmdidCommandWindow = 729;

			// Token: 0x040024D0 RID: 9424
			internal const int cmdidCommandWindowMarkMode = 730;

			// Token: 0x040024D1 RID: 9425
			internal const int cmdidLogCommandWindow = 731;

			// Token: 0x040024D2 RID: 9426
			internal const int cmdidShell = 732;

			// Token: 0x040024D3 RID: 9427
			internal const int cmdidSingleChar = 733;

			// Token: 0x040024D4 RID: 9428
			internal const int cmdidZeroOrMore = 734;

			// Token: 0x040024D5 RID: 9429
			internal const int cmdidOneOrMore = 735;

			// Token: 0x040024D6 RID: 9430
			internal const int cmdidBeginLine = 736;

			// Token: 0x040024D7 RID: 9431
			internal const int cmdidEndLine = 737;

			// Token: 0x040024D8 RID: 9432
			internal const int cmdidBeginWord = 738;

			// Token: 0x040024D9 RID: 9433
			internal const int cmdidEndWord = 739;

			// Token: 0x040024DA RID: 9434
			internal const int cmdidCharInSet = 740;

			// Token: 0x040024DB RID: 9435
			internal const int cmdidCharNotInSet = 741;

			// Token: 0x040024DC RID: 9436
			internal const int cmdidOr = 742;

			// Token: 0x040024DD RID: 9437
			internal const int cmdidEscape = 743;

			// Token: 0x040024DE RID: 9438
			internal const int cmdidTagExp = 744;

			// Token: 0x040024DF RID: 9439
			internal const int cmdidPatternMatchHelp = 745;

			// Token: 0x040024E0 RID: 9440
			internal const int cmdidRegExList = 746;

			// Token: 0x040024E1 RID: 9441
			internal const int cmdidDebugReserved1 = 747;

			// Token: 0x040024E2 RID: 9442
			internal const int cmdidDebugReserved2 = 748;

			// Token: 0x040024E3 RID: 9443
			internal const int cmdidDebugReserved3 = 749;

			// Token: 0x040024E4 RID: 9444
			internal const int cmdidWildZeroOrMore = 754;

			// Token: 0x040024E5 RID: 9445
			internal const int cmdidWildSingleChar = 755;

			// Token: 0x040024E6 RID: 9446
			internal const int cmdidWildSingleDigit = 756;

			// Token: 0x040024E7 RID: 9447
			internal const int cmdidWildCharInSet = 757;

			// Token: 0x040024E8 RID: 9448
			internal const int cmdidWildCharNotInSet = 758;

			// Token: 0x040024E9 RID: 9449
			internal const int cmdidFindWhatText = 759;

			// Token: 0x040024EA RID: 9450
			internal const int cmdidTaggedExp1 = 760;

			// Token: 0x040024EB RID: 9451
			internal const int cmdidTaggedExp2 = 761;

			// Token: 0x040024EC RID: 9452
			internal const int cmdidTaggedExp3 = 762;

			// Token: 0x040024ED RID: 9453
			internal const int cmdidTaggedExp4 = 763;

			// Token: 0x040024EE RID: 9454
			internal const int cmdidTaggedExp5 = 764;

			// Token: 0x040024EF RID: 9455
			internal const int cmdidTaggedExp6 = 765;

			// Token: 0x040024F0 RID: 9456
			internal const int cmdidTaggedExp7 = 766;

			// Token: 0x040024F1 RID: 9457
			internal const int cmdidTaggedExp8 = 767;

			// Token: 0x040024F2 RID: 9458
			internal const int cmdidTaggedExp9 = 768;

			// Token: 0x040024F3 RID: 9459
			internal const int cmdidEditorWidgetClick = 769;

			// Token: 0x040024F4 RID: 9460
			internal const int cmdidCmdWinUpdateAC = 770;

			// Token: 0x040024F5 RID: 9461
			internal const int cmdidSlnCfgMgr = 771;

			// Token: 0x040024F6 RID: 9462
			internal const int cmdidAddNewProject = 772;

			// Token: 0x040024F7 RID: 9463
			internal const int cmdidAddExistingProject = 773;

			// Token: 0x040024F8 RID: 9464
			internal const int cmdidAddNewSolutionItem = 774;

			// Token: 0x040024F9 RID: 9465
			internal const int cmdidAddExistingSolutionItem = 775;

			// Token: 0x040024FA RID: 9466
			internal const int cmdidAutoHideContext1 = 776;

			// Token: 0x040024FB RID: 9467
			internal const int cmdidAutoHideContext2 = 777;

			// Token: 0x040024FC RID: 9468
			internal const int cmdidAutoHideContext3 = 778;

			// Token: 0x040024FD RID: 9469
			internal const int cmdidAutoHideContext4 = 779;

			// Token: 0x040024FE RID: 9470
			internal const int cmdidAutoHideContext5 = 780;

			// Token: 0x040024FF RID: 9471
			internal const int cmdidAutoHideContext6 = 781;

			// Token: 0x04002500 RID: 9472
			internal const int cmdidAutoHideContext7 = 782;

			// Token: 0x04002501 RID: 9473
			internal const int cmdidAutoHideContext8 = 783;

			// Token: 0x04002502 RID: 9474
			internal const int cmdidAutoHideContext9 = 784;

			// Token: 0x04002503 RID: 9475
			internal const int cmdidAutoHideContext10 = 785;

			// Token: 0x04002504 RID: 9476
			internal const int cmdidAutoHideContext11 = 786;

			// Token: 0x04002505 RID: 9477
			internal const int cmdidAutoHideContext12 = 787;

			// Token: 0x04002506 RID: 9478
			internal const int cmdidAutoHideContext13 = 788;

			// Token: 0x04002507 RID: 9479
			internal const int cmdidAutoHideContext14 = 789;

			// Token: 0x04002508 RID: 9480
			internal const int cmdidAutoHideContext15 = 790;

			// Token: 0x04002509 RID: 9481
			internal const int cmdidAutoHideContext16 = 791;

			// Token: 0x0400250A RID: 9482
			internal const int cmdidAutoHideContext17 = 792;

			// Token: 0x0400250B RID: 9483
			internal const int cmdidAutoHideContext18 = 793;

			// Token: 0x0400250C RID: 9484
			internal const int cmdidAutoHideContext19 = 794;

			// Token: 0x0400250D RID: 9485
			internal const int cmdidAutoHideContext20 = 795;

			// Token: 0x0400250E RID: 9486
			internal const int cmdidAutoHideContext21 = 796;

			// Token: 0x0400250F RID: 9487
			internal const int cmdidAutoHideContext22 = 797;

			// Token: 0x04002510 RID: 9488
			internal const int cmdidAutoHideContext23 = 798;

			// Token: 0x04002511 RID: 9489
			internal const int cmdidAutoHideContext24 = 799;

			// Token: 0x04002512 RID: 9490
			internal const int cmdidAutoHideContext25 = 800;

			// Token: 0x04002513 RID: 9491
			internal const int cmdidAutoHideContext26 = 801;

			// Token: 0x04002514 RID: 9492
			internal const int cmdidAutoHideContext27 = 802;

			// Token: 0x04002515 RID: 9493
			internal const int cmdidAutoHideContext28 = 803;

			// Token: 0x04002516 RID: 9494
			internal const int cmdidAutoHideContext29 = 804;

			// Token: 0x04002517 RID: 9495
			internal const int cmdidAutoHideContext30 = 805;

			// Token: 0x04002518 RID: 9496
			internal const int cmdidAutoHideContext31 = 806;

			// Token: 0x04002519 RID: 9497
			internal const int cmdidAutoHideContext32 = 807;

			// Token: 0x0400251A RID: 9498
			internal const int cmdidAutoHideContext33 = 808;

			// Token: 0x0400251B RID: 9499
			internal const int cmdidShellNavBackward = 809;

			// Token: 0x0400251C RID: 9500
			internal const int cmdidShellNavForward = 810;

			// Token: 0x0400251D RID: 9501
			internal const int cmdidShellNavigate1 = 811;

			// Token: 0x0400251E RID: 9502
			internal const int cmdidShellNavigate2 = 812;

			// Token: 0x0400251F RID: 9503
			internal const int cmdidShellNavigate3 = 813;

			// Token: 0x04002520 RID: 9504
			internal const int cmdidShellNavigate4 = 814;

			// Token: 0x04002521 RID: 9505
			internal const int cmdidShellNavigate5 = 815;

			// Token: 0x04002522 RID: 9506
			internal const int cmdidShellNavigate6 = 816;

			// Token: 0x04002523 RID: 9507
			internal const int cmdidShellNavigate7 = 817;

			// Token: 0x04002524 RID: 9508
			internal const int cmdidShellNavigate8 = 818;

			// Token: 0x04002525 RID: 9509
			internal const int cmdidShellNavigate9 = 819;

			// Token: 0x04002526 RID: 9510
			internal const int cmdidShellNavigate10 = 820;

			// Token: 0x04002527 RID: 9511
			internal const int cmdidShellNavigate11 = 821;

			// Token: 0x04002528 RID: 9512
			internal const int cmdidShellNavigate12 = 822;

			// Token: 0x04002529 RID: 9513
			internal const int cmdidShellNavigate13 = 823;

			// Token: 0x0400252A RID: 9514
			internal const int cmdidShellNavigate14 = 824;

			// Token: 0x0400252B RID: 9515
			internal const int cmdidShellNavigate15 = 825;

			// Token: 0x0400252C RID: 9516
			internal const int cmdidShellNavigate16 = 826;

			// Token: 0x0400252D RID: 9517
			internal const int cmdidShellNavigate17 = 827;

			// Token: 0x0400252E RID: 9518
			internal const int cmdidShellNavigate18 = 828;

			// Token: 0x0400252F RID: 9519
			internal const int cmdidShellNavigate19 = 829;

			// Token: 0x04002530 RID: 9520
			internal const int cmdidShellNavigate20 = 830;

			// Token: 0x04002531 RID: 9521
			internal const int cmdidShellNavigate21 = 831;

			// Token: 0x04002532 RID: 9522
			internal const int cmdidShellNavigate22 = 832;

			// Token: 0x04002533 RID: 9523
			internal const int cmdidShellNavigate23 = 833;

			// Token: 0x04002534 RID: 9524
			internal const int cmdidShellNavigate24 = 834;

			// Token: 0x04002535 RID: 9525
			internal const int cmdidShellNavigate25 = 835;

			// Token: 0x04002536 RID: 9526
			internal const int cmdidShellNavigate26 = 836;

			// Token: 0x04002537 RID: 9527
			internal const int cmdidShellNavigate27 = 837;

			// Token: 0x04002538 RID: 9528
			internal const int cmdidShellNavigate28 = 838;

			// Token: 0x04002539 RID: 9529
			internal const int cmdidShellNavigate29 = 839;

			// Token: 0x0400253A RID: 9530
			internal const int cmdidShellNavigate30 = 840;

			// Token: 0x0400253B RID: 9531
			internal const int cmdidShellNavigate31 = 841;

			// Token: 0x0400253C RID: 9532
			internal const int cmdidShellNavigate32 = 842;

			// Token: 0x0400253D RID: 9533
			internal const int cmdidShellNavigate33 = 843;

			// Token: 0x0400253E RID: 9534
			internal const int cmdidShellWindowNavigate1 = 844;

			// Token: 0x0400253F RID: 9535
			internal const int cmdidShellWindowNavigate2 = 845;

			// Token: 0x04002540 RID: 9536
			internal const int cmdidShellWindowNavigate3 = 846;

			// Token: 0x04002541 RID: 9537
			internal const int cmdidShellWindowNavigate4 = 847;

			// Token: 0x04002542 RID: 9538
			internal const int cmdidShellWindowNavigate5 = 848;

			// Token: 0x04002543 RID: 9539
			internal const int cmdidShellWindowNavigate6 = 849;

			// Token: 0x04002544 RID: 9540
			internal const int cmdidShellWindowNavigate7 = 850;

			// Token: 0x04002545 RID: 9541
			internal const int cmdidShellWindowNavigate8 = 851;

			// Token: 0x04002546 RID: 9542
			internal const int cmdidShellWindowNavigate9 = 852;

			// Token: 0x04002547 RID: 9543
			internal const int cmdidShellWindowNavigate10 = 853;

			// Token: 0x04002548 RID: 9544
			internal const int cmdidShellWindowNavigate11 = 854;

			// Token: 0x04002549 RID: 9545
			internal const int cmdidShellWindowNavigate12 = 855;

			// Token: 0x0400254A RID: 9546
			internal const int cmdidShellWindowNavigate13 = 856;

			// Token: 0x0400254B RID: 9547
			internal const int cmdidShellWindowNavigate14 = 857;

			// Token: 0x0400254C RID: 9548
			internal const int cmdidShellWindowNavigate15 = 858;

			// Token: 0x0400254D RID: 9549
			internal const int cmdidShellWindowNavigate16 = 859;

			// Token: 0x0400254E RID: 9550
			internal const int cmdidShellWindowNavigate17 = 860;

			// Token: 0x0400254F RID: 9551
			internal const int cmdidShellWindowNavigate18 = 861;

			// Token: 0x04002550 RID: 9552
			internal const int cmdidShellWindowNavigate19 = 862;

			// Token: 0x04002551 RID: 9553
			internal const int cmdidShellWindowNavigate20 = 863;

			// Token: 0x04002552 RID: 9554
			internal const int cmdidShellWindowNavigate21 = 864;

			// Token: 0x04002553 RID: 9555
			internal const int cmdidShellWindowNavigate22 = 865;

			// Token: 0x04002554 RID: 9556
			internal const int cmdidShellWindowNavigate23 = 866;

			// Token: 0x04002555 RID: 9557
			internal const int cmdidShellWindowNavigate24 = 867;

			// Token: 0x04002556 RID: 9558
			internal const int cmdidShellWindowNavigate25 = 868;

			// Token: 0x04002557 RID: 9559
			internal const int cmdidShellWindowNavigate26 = 869;

			// Token: 0x04002558 RID: 9560
			internal const int cmdidShellWindowNavigate27 = 870;

			// Token: 0x04002559 RID: 9561
			internal const int cmdidShellWindowNavigate28 = 871;

			// Token: 0x0400255A RID: 9562
			internal const int cmdidShellWindowNavigate29 = 872;

			// Token: 0x0400255B RID: 9563
			internal const int cmdidShellWindowNavigate30 = 873;

			// Token: 0x0400255C RID: 9564
			internal const int cmdidShellWindowNavigate31 = 874;

			// Token: 0x0400255D RID: 9565
			internal const int cmdidShellWindowNavigate32 = 875;

			// Token: 0x0400255E RID: 9566
			internal const int cmdidShellWindowNavigate33 = 876;

			// Token: 0x0400255F RID: 9567
			internal const int cmdidOBSDoFind = 877;

			// Token: 0x04002560 RID: 9568
			internal const int cmdidOBSMatchCase = 878;

			// Token: 0x04002561 RID: 9569
			internal const int cmdidOBSMatchSubString = 879;

			// Token: 0x04002562 RID: 9570
			internal const int cmdidOBSMatchWholeWord = 880;

			// Token: 0x04002563 RID: 9571
			internal const int cmdidOBSMatchPrefix = 881;

			// Token: 0x04002564 RID: 9572
			internal const int cmdidBuildSln = 882;

			// Token: 0x04002565 RID: 9573
			internal const int cmdidRebuildSln = 883;

			// Token: 0x04002566 RID: 9574
			internal const int cmdidDeploySln = 884;

			// Token: 0x04002567 RID: 9575
			internal const int cmdidCleanSln = 885;

			// Token: 0x04002568 RID: 9576
			internal const int cmdidBuildSel = 886;

			// Token: 0x04002569 RID: 9577
			internal const int cmdidRebuildSel = 887;

			// Token: 0x0400256A RID: 9578
			internal const int cmdidDeploySel = 888;

			// Token: 0x0400256B RID: 9579
			internal const int cmdidCleanSel = 889;

			// Token: 0x0400256C RID: 9580
			internal const int cmdidCancelBuild = 890;

			// Token: 0x0400256D RID: 9581
			internal const int cmdidBatchBuildDlg = 891;

			// Token: 0x0400256E RID: 9582
			internal const int cmdidBuildCtx = 892;

			// Token: 0x0400256F RID: 9583
			internal const int cmdidRebuildCtx = 893;

			// Token: 0x04002570 RID: 9584
			internal const int cmdidDeployCtx = 894;

			// Token: 0x04002571 RID: 9585
			internal const int cmdidCleanCtx = 895;

			// Token: 0x04002572 RID: 9586
			internal const int cmdidMRUFile1 = 900;

			// Token: 0x04002573 RID: 9587
			internal const int cmdidMRUFile2 = 901;

			// Token: 0x04002574 RID: 9588
			internal const int cmdidMRUFile3 = 902;

			// Token: 0x04002575 RID: 9589
			internal const int cmdidMRUFile4 = 903;

			// Token: 0x04002576 RID: 9590
			internal const int cmdidMRUFile5 = 904;

			// Token: 0x04002577 RID: 9591
			internal const int cmdidMRUFile6 = 905;

			// Token: 0x04002578 RID: 9592
			internal const int cmdidMRUFile7 = 906;

			// Token: 0x04002579 RID: 9593
			internal const int cmdidMRUFile8 = 907;

			// Token: 0x0400257A RID: 9594
			internal const int cmdidMRUFile9 = 908;

			// Token: 0x0400257B RID: 9595
			internal const int cmdidMRUFile10 = 909;

			// Token: 0x0400257C RID: 9596
			internal const int cmdidMRUFile11 = 910;

			// Token: 0x0400257D RID: 9597
			internal const int cmdidMRUFile12 = 911;

			// Token: 0x0400257E RID: 9598
			internal const int cmdidMRUFile13 = 912;

			// Token: 0x0400257F RID: 9599
			internal const int cmdidMRUFile14 = 913;

			// Token: 0x04002580 RID: 9600
			internal const int cmdidMRUFile15 = 914;

			// Token: 0x04002581 RID: 9601
			internal const int cmdidMRUFile16 = 915;

			// Token: 0x04002582 RID: 9602
			internal const int cmdidMRUFile17 = 916;

			// Token: 0x04002583 RID: 9603
			internal const int cmdidMRUFile18 = 917;

			// Token: 0x04002584 RID: 9604
			internal const int cmdidMRUFile19 = 918;

			// Token: 0x04002585 RID: 9605
			internal const int cmdidMRUFile20 = 919;

			// Token: 0x04002586 RID: 9606
			internal const int cmdidMRUFile21 = 920;

			// Token: 0x04002587 RID: 9607
			internal const int cmdidMRUFile22 = 921;

			// Token: 0x04002588 RID: 9608
			internal const int cmdidMRUFile23 = 922;

			// Token: 0x04002589 RID: 9609
			internal const int cmdidMRUFile24 = 923;

			// Token: 0x0400258A RID: 9610
			internal const int cmdidMRUFile25 = 924;

			// Token: 0x0400258B RID: 9611
			internal const int cmdidGotoDefn = 925;

			// Token: 0x0400258C RID: 9612
			internal const int cmdidGotoDecl = 926;

			// Token: 0x0400258D RID: 9613
			internal const int cmdidBrowseDefn = 927;

			// Token: 0x0400258E RID: 9614
			internal const int cmdidShowMembers = 928;

			// Token: 0x0400258F RID: 9615
			internal const int cmdidShowBases = 929;

			// Token: 0x04002590 RID: 9616
			internal const int cmdidShowDerived = 930;

			// Token: 0x04002591 RID: 9617
			internal const int cmdidShowDefns = 931;

			// Token: 0x04002592 RID: 9618
			internal const int cmdidShowRefs = 932;

			// Token: 0x04002593 RID: 9619
			internal const int cmdidShowCallers = 933;

			// Token: 0x04002594 RID: 9620
			internal const int cmdidShowCallees = 934;

			// Token: 0x04002595 RID: 9621
			internal const int cmdidDefineSubset = 935;

			// Token: 0x04002596 RID: 9622
			internal const int cmdidSetSubset = 936;

			// Token: 0x04002597 RID: 9623
			internal const int cmdidCVGroupingNone = 950;

			// Token: 0x04002598 RID: 9624
			internal const int cmdidCVGroupingSortOnly = 951;

			// Token: 0x04002599 RID: 9625
			internal const int cmdidCVGroupingGrouped = 952;

			// Token: 0x0400259A RID: 9626
			internal const int cmdidCVShowPackages = 953;

			// Token: 0x0400259B RID: 9627
			internal const int cmdidQryManageIndexes = 954;

			// Token: 0x0400259C RID: 9628
			internal const int cmdidBrowseComponent = 955;

			// Token: 0x0400259D RID: 9629
			internal const int cmdidPrintDefault = 956;

			// Token: 0x0400259E RID: 9630
			internal const int cmdidBrowseDoc = 957;

			// Token: 0x0400259F RID: 9631
			internal const int cmdidStandardMax = 1000;

			// Token: 0x040025A0 RID: 9632
			internal const int cmdidFormsFirst = 24576;

			// Token: 0x040025A1 RID: 9633
			internal const int cmdidFormsLast = 28671;

			// Token: 0x040025A2 RID: 9634
			internal const int cmdidVBEFirst = 32768;

			// Token: 0x040025A3 RID: 9635
			internal const int msotcidBookmarkWellMenu = 32769;

			// Token: 0x040025A4 RID: 9636
			internal const int cmdidZoom200 = 32770;

			// Token: 0x040025A5 RID: 9637
			internal const int cmdidZoom150 = 32771;

			// Token: 0x040025A6 RID: 9638
			internal const int cmdidZoom100 = 32772;

			// Token: 0x040025A7 RID: 9639
			internal const int cmdidZoom75 = 32773;

			// Token: 0x040025A8 RID: 9640
			internal const int cmdidZoom50 = 32774;

			// Token: 0x040025A9 RID: 9641
			internal const int cmdidZoom25 = 32775;

			// Token: 0x040025AA RID: 9642
			internal const int cmdidZoom10 = 32784;

			// Token: 0x040025AB RID: 9643
			internal const int msotcidZoomWellMenu = 32785;

			// Token: 0x040025AC RID: 9644
			internal const int msotcidDebugPopWellMenu = 32786;

			// Token: 0x040025AD RID: 9645
			internal const int msotcidAlignWellMenu = 32787;

			// Token: 0x040025AE RID: 9646
			internal const int msotcidArrangeWellMenu = 32788;

			// Token: 0x040025AF RID: 9647
			internal const int msotcidCenterWellMenu = 32789;

			// Token: 0x040025B0 RID: 9648
			internal const int msotcidSizeWellMenu = 32790;

			// Token: 0x040025B1 RID: 9649
			internal const int msotcidHorizontalSpaceWellMenu = 32791;

			// Token: 0x040025B2 RID: 9650
			internal const int msotcidVerticalSpaceWellMenu = 32800;

			// Token: 0x040025B3 RID: 9651
			internal const int msotcidDebugWellMenu = 32801;

			// Token: 0x040025B4 RID: 9652
			internal const int msotcidDebugMenuVB = 32802;

			// Token: 0x040025B5 RID: 9653
			internal const int msotcidStatementBuilderWellMenu = 32803;

			// Token: 0x040025B6 RID: 9654
			internal const int msotcidProjWinInsertMenu = 32804;

			// Token: 0x040025B7 RID: 9655
			internal const int msotcidToggleMenu = 32805;

			// Token: 0x040025B8 RID: 9656
			internal const int msotcidNewObjInsertWellMenu = 32806;

			// Token: 0x040025B9 RID: 9657
			internal const int msotcidSizeToWellMenu = 32807;

			// Token: 0x040025BA RID: 9658
			internal const int msotcidCommandBars = 32808;

			// Token: 0x040025BB RID: 9659
			internal const int msotcidVBOrderMenu = 32809;

			// Token: 0x040025BC RID: 9660
			internal const int msotcidMSOnTheWeb = 32810;

			// Token: 0x040025BD RID: 9661
			internal const int msotcidVBDesignerMenu = 32816;

			// Token: 0x040025BE RID: 9662
			internal const int msotcidNewProjectWellMenu = 32817;

			// Token: 0x040025BF RID: 9663
			internal const int msotcidProjectWellMenu = 32818;

			// Token: 0x040025C0 RID: 9664
			internal const int msotcidVBCode1ContextMenu = 32819;

			// Token: 0x040025C1 RID: 9665
			internal const int msotcidVBCode2ContextMenu = 32820;

			// Token: 0x040025C2 RID: 9666
			internal const int msotcidVBWatchContextMenu = 32821;

			// Token: 0x040025C3 RID: 9667
			internal const int msotcidVBImmediateContextMenu = 32822;

			// Token: 0x040025C4 RID: 9668
			internal const int msotcidVBLocalsContextMenu = 32823;

			// Token: 0x040025C5 RID: 9669
			internal const int msotcidVBFormContextMenu = 32824;

			// Token: 0x040025C6 RID: 9670
			internal const int msotcidVBControlContextMenu = 32825;

			// Token: 0x040025C7 RID: 9671
			internal const int msotcidVBProjWinContextMenu = 32826;

			// Token: 0x040025C8 RID: 9672
			internal const int msotcidVBProjWinContextBreakMenu = 32827;

			// Token: 0x040025C9 RID: 9673
			internal const int msotcidVBPreviewWinContextMenu = 32828;

			// Token: 0x040025CA RID: 9674
			internal const int msotcidVBOBContextMenu = 32829;

			// Token: 0x040025CB RID: 9675
			internal const int msotcidVBForms3ContextMenu = 32830;

			// Token: 0x040025CC RID: 9676
			internal const int msotcidVBForms3ControlCMenu = 32831;

			// Token: 0x040025CD RID: 9677
			internal const int msotcidVBForms3ControlCMenuGroup = 32832;

			// Token: 0x040025CE RID: 9678
			internal const int msotcidVBForms3ControlPalette = 32833;

			// Token: 0x040025CF RID: 9679
			internal const int msotcidVBForms3ToolboxCMenu = 32834;

			// Token: 0x040025D0 RID: 9680
			internal const int msotcidVBForms3MPCCMenu = 32835;

			// Token: 0x040025D1 RID: 9681
			internal const int msotcidVBForms3DragDropCMenu = 32836;

			// Token: 0x040025D2 RID: 9682
			internal const int msotcidVBToolBoxContextMenu = 32837;

			// Token: 0x040025D3 RID: 9683
			internal const int msotcidVBToolBoxGroupContextMenu = 32838;

			// Token: 0x040025D4 RID: 9684
			internal const int msotcidVBPropBrsHostContextMenu = 32839;

			// Token: 0x040025D5 RID: 9685
			internal const int msotcidVBPropBrsContextMenu = 32840;

			// Token: 0x040025D6 RID: 9686
			internal const int msotcidVBPalContextMenu = 32841;

			// Token: 0x040025D7 RID: 9687
			internal const int msotcidVBProjWinProjectContextMenu = 32842;

			// Token: 0x040025D8 RID: 9688
			internal const int msotcidVBProjWinFormContextMenu = 32843;

			// Token: 0x040025D9 RID: 9689
			internal const int msotcidVBProjWinModClassContextMenu = 32844;

			// Token: 0x040025DA RID: 9690
			internal const int msotcidVBProjWinRelDocContextMenu = 32845;

			// Token: 0x040025DB RID: 9691
			internal const int msotcidVBDockedWindowContextMenu = 32846;

			// Token: 0x040025DC RID: 9692
			internal const int msotcidVBShortCutForms = 32847;

			// Token: 0x040025DD RID: 9693
			internal const int msotcidVBShortCutCodeWindows = 32848;

			// Token: 0x040025DE RID: 9694
			internal const int msotcidVBShortCutMisc = 32849;

			// Token: 0x040025DF RID: 9695
			internal const int msotcidVBBuiltInMenus = 32850;

			// Token: 0x040025E0 RID: 9696
			internal const int msotcidPreviewWinFormPos = 32851;

			// Token: 0x040025E1 RID: 9697
			internal const int msotcidVBAddinFirst = 33280;
		}

		// Token: 0x0200078C RID: 1932
		private static class ShellGuids
		{
			// Token: 0x040025E2 RID: 9698
			internal static readonly Guid VSStandardCommandSet97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x040025E3 RID: 9699
			internal static readonly Guid guidDsdCmdId = new Guid("{1F0FD094-8e53-11d2-8f9c-0060089fc486}");

			// Token: 0x040025E4 RID: 9700
			internal static readonly Guid SID_SOleComponentUIManager = new Guid("{5efc7974-14bc-11cf-9b2b-00aa00573819}");

			// Token: 0x040025E5 RID: 9701
			internal static readonly Guid GUID_VSTASKCATEGORY_DATADESIGNER = new Guid("{6B32EAED-13BB-11d3-A64F-00C04F683820}");

			// Token: 0x040025E6 RID: 9702
			internal static readonly Guid GUID_PropertyBrowserToolWindow = new Guid(-285584864, -7528, 4560, new byte[] { 143, 120, 0, 160, 201, 17, 0, 87 });
		}
	}
}
