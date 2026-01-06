using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EF RID: 751
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000624C4 File Offset: 0x000606C4
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x000624DC File Offset: 0x000606DC
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000624E8 File Offset: 0x000606E8
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x00062500 File Offset: 0x00060700
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0006250C File Offset: 0x0006070C
		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> visualElementAssets = this.m_VisualElementAssets;
			int num = ((visualElementAssets != null) ? visualElementAssets.Count : 0);
			int num2 = num;
			List<TemplateAsset> templateAssets = this.m_TemplateAssets;
			return num2 + ((templateAssets != null) ? templateAssets.Count : 0);
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00062548 File Offset: 0x00060748
		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
				if (flag)
				{
					yield break;
				}
				HashSet<VisualTreeAsset> sent = new HashSet<VisualTreeAsset>();
				foreach (VisualTreeAsset.UsingEntry entry in this.m_Usings)
				{
					bool flag2 = entry.asset != null && !sent.Contains(entry.asset);
					if (flag2)
					{
						sent.Add(entry.asset);
						yield return entry.asset;
					}
					else
					{
						bool flag3 = !string.IsNullOrEmpty(entry.path);
						if (flag3)
						{
							VisualTreeAsset vta = Panel.LoadResource(entry.path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset;
							bool flag4 = vta != null && !sent.Contains(entry.asset);
							if (flag4)
							{
								sent.Add(entry.asset);
								yield return vta;
							}
							vta = null;
						}
					}
					entry = default(VisualTreeAsset.UsingEntry);
				}
				List<VisualTreeAsset.UsingEntry>.Enumerator enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x00062568 File Offset: 0x00060768
		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				HashSet<StyleSheet> sent = new HashSet<StyleSheet>();
				foreach (VisualElementAsset vea in this.m_VisualElementAssets)
				{
					bool hasStylesheets = vea.hasStylesheets;
					if (hasStylesheets)
					{
						foreach (StyleSheet stylesheet in vea.stylesheets)
						{
							bool flag = !sent.Contains(stylesheet);
							if (flag)
							{
								sent.Add(stylesheet);
								yield return stylesheet;
							}
							stylesheet = null;
						}
						List<StyleSheet>.Enumerator enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						foreach (string stylesheetPath in vea.stylesheetPaths)
						{
							StyleSheet stylesheet2 = Panel.LoadResource(stylesheetPath, typeof(StyleSheet), GUIUtility.pixelsPerPoint) as StyleSheet;
							bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
							if (flag2)
							{
								sent.Add(stylesheet2);
								yield return stylesheet2;
							}
							stylesheet2 = null;
							stylesheetPath = null;
						}
						List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
					}
					vea = null;
				}
				List<VisualElementAsset>.Enumerator enumerator = default(List<VisualElementAsset>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00062588 File Offset: 0x00060788
		// (set) Token: 0x060018BD RID: 6333 RVA: 0x000625A0 File Offset: 0x000607A0
		internal List<VisualElementAsset> visualElementAssets
		{
			get
			{
				return this.m_VisualElementAssets;
			}
			set
			{
				this.m_VisualElementAssets = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060018BE RID: 6334 RVA: 0x000625AC File Offset: 0x000607AC
		// (set) Token: 0x060018BF RID: 6335 RVA: 0x000625C4 File Offset: 0x000607C4
		internal List<TemplateAsset> templateAssets
		{
			get
			{
				return this.m_TemplateAssets;
			}
			set
			{
				this.m_TemplateAssets = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000625D0 File Offset: 0x000607D0
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x000625E8 File Offset: 0x000607E8
		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
			set
			{
				this.m_Slots = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000625F4 File Offset: 0x000607F4
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0006260C File Offset: 0x0006080C
		internal int contentContainerId
		{
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00062618 File Offset: 0x00060818
		public TemplateContainer Instantiate()
		{
			TemplateContainer templateContainer = new TemplateContainer(base.name);
			try
			{
				this.CloneTree(templateContainer, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
			return templateContainer;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00062668 File Offset: 0x00060868
		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer templateContainer = this.Instantiate();
			templateContainer.bindingPath = bindingPath;
			return templateContainer;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0006268C File Offset: 0x0006088C
		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000626A4 File Offset: 0x000608A4
		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000626C0 File Offset: 0x000608C0
		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000626DC File Offset: 0x000608DC
		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				this.CloneTree(target, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00062740 File Offset: 0x00060940
		internal void CloneTree(VisualElement target, Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				TemplateContainer templateContainer = target as TemplateContainer;
				bool flag3 = templateContainer != null;
				if (flag3)
				{
					templateContainer.templateSource = this;
				}
				Dictionary<int, List<VisualElementAsset>> dictionary = new Dictionary<int, List<VisualElementAsset>>();
				int num = ((this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count);
				int num2 = ((this.templateAssets == null) ? 0 : this.templateAssets.Count);
				for (int i = 0; i < num + num2; i++)
				{
					VisualElementAsset visualElementAsset = ((i < num) ? this.visualElementAssets[i] : this.templateAssets[i - num]);
					List<VisualElementAsset> list;
					bool flag4 = !dictionary.TryGetValue(visualElementAsset.parentId, ref list);
					if (flag4)
					{
						list = new List<VisualElementAsset>();
						dictionary.Add(visualElementAsset.parentId, list);
					}
					list.Add(visualElementAsset);
				}
				List<VisualElementAsset> list2;
				dictionary.TryGetValue(0, ref list2);
				bool flag5 = list2 == null || list2.Count == 0;
				if (!flag5)
				{
					Debug.Assert(list2.Count == 1);
					VisualElementAsset visualElementAsset2 = list2[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(visualElementAsset2, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(visualElementAsset2, target);
					list2.Clear();
					dictionary.TryGetValue(visualElementAsset2.id, ref list2);
					bool flag6 = list2 == null || list2.Count == 0;
					if (!flag6)
					{
						list2.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset visualElementAsset3 in list2)
						{
							Assert.IsNotNull<VisualElementAsset>(visualElementAsset3);
							VisualElement visualElement = this.CloneSetupRecursively(visualElementAsset3, dictionary, new CreationContext(slotInsertionPoints, attributeOverrides, this, target));
							bool flag7 = visualElement != null;
							if (flag7)
							{
								visualElement.visualTreeAssetSource = this;
								target.hierarchy.Add(visualElement);
							}
							else
							{
								Debug.LogWarning("VisualTreeAsset instantiated an empty UI. Check the syntax of your UXML document.");
							}
						}
					}
				}
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0006299C File Offset: 0x00060B9C
		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			VisualElement visualElement = VisualTreeAsset.Create(root, context);
			bool flag = visualElement == null;
			VisualElement visualElement2;
			if (flag)
			{
				visualElement2 = null;
			}
			else
			{
				bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
				if (flag2)
				{
					bool flag3 = context.target is TemplateContainer;
					if (flag3)
					{
						((TemplateContainer)context.target).SetContentContainer(visualElement);
					}
					else
					{
						Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
					}
				}
				string text;
				bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out text);
				if (flag4)
				{
					context.slotInsertionPoints.Add(text, visualElement);
				}
				bool flag5 = root.ruleIndex != -1;
				if (flag5)
				{
					bool flag6 = this.inlineSheet == null;
					if (flag6)
					{
						Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
					}
					else
					{
						StyleRule styleRule = this.inlineSheet.rules[root.ruleIndex];
						visualElement.SetInlineRule(this.inlineSheet, styleRule);
					}
				}
				TemplateAsset templateAsset = root as TemplateAsset;
				List<VisualElementAsset> list;
				bool flag7 = idToChildren.TryGetValue(root.id, ref list);
				if (flag7)
				{
					list.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
					using (List<VisualElementAsset>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							VisualElementAsset childVea = enumerator.Current;
							VisualElement visualElement3 = this.CloneSetupRecursively(childVea, idToChildren, context);
							bool flag8 = visualElement3 == null;
							if (!flag8)
							{
								bool flag9 = templateAsset == null;
								if (flag9)
								{
									visualElement.Add(visualElement3);
								}
								else
								{
									int num = ((templateAsset.slotUsages == null) ? (-1) : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id));
									bool flag10 = num != -1;
									if (flag10)
									{
										string slotName = templateAsset.slotUsages[num].slotName;
										Assert.IsFalse(string.IsNullOrEmpty(slotName), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
										VisualElement visualElement4;
										bool flag11 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(slotName, ref visualElement4);
										if (flag11)
										{
											Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
											{
												slotName,
												(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", Enumerable.ToArray<string>(context.slotInsertionPoints.Keys))
											});
											visualElement.Add(visualElement3);
										}
										else
										{
											visualElement4.Add(visualElement3);
										}
									}
									else
									{
										visualElement.Add(visualElement3);
									}
								}
							}
						}
					}
				}
				bool flag12 = templateAsset != null && context.slotInsertionPoints != null;
				if (flag12)
				{
					context.slotInsertionPoints.Clear();
				}
				visualElement2 = visualElement;
			}
			return visualElement2;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00062C80 File Offset: 0x00060E80
		private static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00062CA4 File Offset: 0x00060EA4
		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			bool flag = this.m_Slots == null;
			bool flag2;
			if (flag)
			{
				slotName = null;
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Slots.Count; i++)
				{
					VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[i];
					bool flag3 = slotDefinition.insertionPointId == insertionPointId;
					if (flag3)
					{
						slotName = slotDefinition.name;
						return true;
					}
				}
				slotName = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00062D18 File Offset: 0x00060F18
		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
			VisualTreeAsset visualTreeAsset;
			if (flag)
			{
				visualTreeAsset = null;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag2 = num < 0;
				if (flag2)
				{
					visualTreeAsset = null;
				}
				else
				{
					bool flag3 = this.m_Usings[num].asset;
					if (flag3)
					{
						visualTreeAsset = this.m_Usings[num].asset;
					}
					else
					{
						string path = this.m_Usings[num].path;
						visualTreeAsset = Panel.LoadResource(path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset;
					}
				}
			}
			return visualTreeAsset;
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00062DD4 File Offset: 0x00060FD4
		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			VisualTreeAsset.<>c__DisplayClass49_0 CS$<>8__locals1;
			CS$<>8__locals1.asset = asset;
			List<IUxmlFactory> list;
			bool flag = !VisualElementFactoryRegistry.TryGetValue(CS$<>8__locals1.asset.fullTypeName, out list);
			if (flag)
			{
				bool flag2 = CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
				if (flag2)
				{
					string text = CS$<>8__locals1.asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
					bool flag3 = !VisualElementFactoryRegistry.TryGetValue(text, out list);
					if (flag3)
					{
						return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
					}
				}
				else
				{
					bool flag4 = CS$<>8__locals1.asset.fullTypeName == "UnityEditor.UIElements.ProgressBar";
					if (flag4)
					{
						string text2 = CS$<>8__locals1.asset.fullTypeName.Replace("UnityEditor", "UnityEngine");
						bool flag5 = !VisualElementFactoryRegistry.TryGetValue(text2, out list);
						if (flag5)
						{
							return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
						}
					}
					else
					{
						bool flag6 = CS$<>8__locals1.asset.fullTypeName == "UXML";
						if (!flag6)
						{
							return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
						}
						VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + CS$<>8__locals1.asset.fullTypeName, out list);
					}
				}
			}
			IUxmlFactory uxmlFactory = null;
			foreach (IUxmlFactory uxmlFactory2 in list)
			{
				bool flag7 = uxmlFactory2.AcceptsAttributeBag(CS$<>8__locals1.asset, ctx);
				if (flag7)
				{
					uxmlFactory = uxmlFactory2;
					break;
				}
			}
			bool flag8 = uxmlFactory == null;
			VisualElement visualElement;
			if (flag8)
			{
				Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { CS$<>8__locals1.asset.fullTypeName });
				visualElement = new Label(string.Format("Type with no factory: '{0}'", CS$<>8__locals1.asset.fullTypeName));
			}
			else
			{
				VisualElement visualElement2 = uxmlFactory.Create(CS$<>8__locals1.asset, ctx);
				bool flag9 = visualElement2 != null;
				if (flag9)
				{
					VisualTreeAsset.AssignClassListFromAssetToElement(CS$<>8__locals1.asset, visualElement2);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(CS$<>8__locals1.asset, visualElement2);
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006301C File Offset: 0x0006121C
		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00063064 File Offset: 0x00061264
		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00063114 File Offset: 0x00061314
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x0006312C File Offset: 0x0006132C
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006314C File Offset: 0x0006134C
		[CompilerGenerated]
		internal static VisualElement <Create>g__CreateError|49_0(ref VisualTreeAsset.<>c__DisplayClass49_0 A_0)
		{
			Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[] { A_0.asset.fullTypeName });
			return new Label(string.Format("Unknown type: '{0}'", A_0.asset.fullTypeName));
		}

		// Token: 0x04000A80 RID: 2688
		internal static string LinkedVEAInTemplatePropertyName = "--unity-linked-vea-in-template";

		// Token: 0x04000A81 RID: 2689
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x04000A82 RID: 2690
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x04000A83 RID: 2691
		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		// Token: 0x04000A84 RID: 2692
		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings;

		// Token: 0x04000A85 RID: 2693
		[SerializeField]
		internal StyleSheet inlineSheet;

		// Token: 0x04000A86 RID: 2694
		[SerializeField]
		private List<VisualElementAsset> m_VisualElementAssets;

		// Token: 0x04000A87 RID: 2695
		[SerializeField]
		private List<TemplateAsset> m_TemplateAssets;

		// Token: 0x04000A88 RID: 2696
		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots;

		// Token: 0x04000A89 RID: 2697
		[SerializeField]
		private int m_ContentContainerId;

		// Token: 0x04000A8A RID: 2698
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x020002F0 RID: 752
		[Serializable]
		internal struct UsingEntry
		{
			// Token: 0x060018D7 RID: 6359 RVA: 0x00063197 File Offset: 0x00061397
			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			// Token: 0x060018D8 RID: 6360 RVA: 0x000631AF File Offset: 0x000613AF
			public UsingEntry(string alias, VisualTreeAsset asset)
			{
				this.alias = alias;
				this.path = null;
				this.asset = asset;
			}

			// Token: 0x04000A8B RID: 2699
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			// Token: 0x04000A8C RID: 2700
			[SerializeField]
			public string alias;

			// Token: 0x04000A8D RID: 2701
			[SerializeField]
			public string path;

			// Token: 0x04000A8E RID: 2702
			[SerializeField]
			public VisualTreeAsset asset;
		}

		// Token: 0x020002F1 RID: 753
		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			// Token: 0x060018DA RID: 6362 RVA: 0x000631D4 File Offset: 0x000613D4
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}
		}

		// Token: 0x020002F2 RID: 754
		[Serializable]
		internal struct SlotDefinition
		{
			// Token: 0x04000A8F RID: 2703
			[SerializeField]
			public string name;

			// Token: 0x04000A90 RID: 2704
			[SerializeField]
			public int insertionPointId;
		}

		// Token: 0x020002F3 RID: 755
		[Serializable]
		internal struct SlotUsageEntry
		{
			// Token: 0x060018DC RID: 6364 RVA: 0x000631F7 File Offset: 0x000613F7
			public SlotUsageEntry(string slotName, int assetId)
			{
				this.slotName = slotName;
				this.assetId = assetId;
			}

			// Token: 0x04000A91 RID: 2705
			[SerializeField]
			public string slotName;

			// Token: 0x04000A92 RID: 2706
			[SerializeField]
			public int assetId;
		}
	}
}
