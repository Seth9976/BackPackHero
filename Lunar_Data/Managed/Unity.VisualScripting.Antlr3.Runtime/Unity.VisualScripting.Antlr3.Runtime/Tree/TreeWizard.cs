using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime.Tree
{
	// Token: 0x02000032 RID: 50
	public class TreeWizard
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00006582 File Offset: 0x00005582
		public TreeWizard(ITreeAdaptor adaptor)
		{
			this.adaptor = adaptor;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00006591 File Offset: 0x00005591
		public TreeWizard(ITreeAdaptor adaptor, IDictionary tokenNameToTypeMap)
		{
			this.adaptor = adaptor;
			this.tokenNameToTypeMap = tokenNameToTypeMap;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000065A7 File Offset: 0x000055A7
		public TreeWizard(ITreeAdaptor adaptor, string[] tokenNames)
		{
			this.adaptor = adaptor;
			this.tokenNameToTypeMap = this.ComputeTokenTypes(tokenNames);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000065C3 File Offset: 0x000055C3
		public TreeWizard(string[] tokenNames)
			: this(null, tokenNames)
		{
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000065D0 File Offset: 0x000055D0
		public IDictionary ComputeTokenTypes(string[] tokenNames)
		{
			IDictionary dictionary = new Hashtable();
			if (tokenNames == null)
			{
				return dictionary;
			}
			for (int i = Token.MIN_TOKEN_TYPE; i < tokenNames.Length; i++)
			{
				string text = tokenNames[i];
				dictionary.Add(text, i);
			}
			return dictionary;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000660C File Offset: 0x0000560C
		public int GetTokenType(string tokenName)
		{
			if (this.tokenNameToTypeMap == null)
			{
				return 0;
			}
			object obj = this.tokenNameToTypeMap[tokenName];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000663C File Offset: 0x0000563C
		public IDictionary Index(object t)
		{
			IDictionary dictionary = new Hashtable();
			this._Index(t, dictionary);
			return dictionary;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00006658 File Offset: 0x00005658
		protected void _Index(object t, IDictionary m)
		{
			if (t == null)
			{
				return;
			}
			int nodeType = this.adaptor.GetNodeType(t);
			IList list = m[nodeType] as IList;
			if (list == null)
			{
				list = new List<object>();
				m[nodeType] = list;
			}
			list.Add(t);
			int childCount = this.adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this._Index(child, m);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000066D8 File Offset: 0x000056D8
		public IList Find(object t, int ttype)
		{
			IList list = new List<object>();
			this.Visit(t, ttype, new TreeWizard.RecordAllElementsVisitor(list));
			return list;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000066FC File Offset: 0x000056FC
		public IList Find(object t, string pattern)
		{
			IList list = new List<object>();
			TreePatternLexer treePatternLexer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(treePatternLexer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern == null || treePattern.IsNil || treePattern.GetType() == typeof(TreeWizard.WildcardTreePattern))
			{
				return null;
			}
			int type = treePattern.Type;
			this.Visit(t, type, new TreeWizard.PatternMatchingContextVisitor(this, treePattern, list));
			return list;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00006768 File Offset: 0x00005768
		public object FindFirst(object t, int ttype)
		{
			return null;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000676B File Offset: 0x0000576B
		public object FindFirst(object t, string pattern)
		{
			return null;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000676E File Offset: 0x0000576E
		public void Visit(object t, int ttype, TreeWizard.ContextVisitor visitor)
		{
			this._Visit(t, null, 0, ttype, visitor);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000677C File Offset: 0x0000577C
		protected void _Visit(object t, object parent, int childIndex, int ttype, TreeWizard.ContextVisitor visitor)
		{
			if (t == null)
			{
				return;
			}
			if (this.adaptor.GetNodeType(t) == ttype)
			{
				visitor.Visit(t, parent, childIndex, null);
			}
			int childCount = this.adaptor.GetChildCount(t);
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t, i);
				this._Visit(child, t, i, ttype, visitor);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000067DC File Offset: 0x000057DC
		public void Visit(object t, string pattern, TreeWizard.ContextVisitor visitor)
		{
			TreePatternLexer treePatternLexer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(treePatternLexer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			if (treePattern == null || treePattern.IsNil || treePattern.GetType() == typeof(TreeWizard.WildcardTreePattern))
			{
				return;
			}
			int type = treePattern.Type;
			this.Visit(t, type, new TreeWizard.InvokeVisitorOnPatternMatchContextVisitor(this, treePattern, visitor));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00006840 File Offset: 0x00005840
		public bool Parse(object t, string pattern, IDictionary labels)
		{
			TreePatternLexer treePatternLexer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(treePatternLexer, this, new TreeWizard.TreePatternTreeAdaptor());
			TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)treePatternParser.Pattern();
			return this._Parse(t, treePattern, labels);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006878 File Offset: 0x00005878
		public bool Parse(object t, string pattern)
		{
			return this.Parse(t, pattern, null);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006884 File Offset: 0x00005884
		protected bool _Parse(object t1, TreeWizard.TreePattern t2, IDictionary labels)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if (t2.GetType() != typeof(TreeWizard.WildcardTreePattern))
			{
				if (this.adaptor.GetNodeType(t1) != t2.Type)
				{
					return false;
				}
				if (t2.hasTextArg && !this.adaptor.GetNodeText(t1).Equals(t2.Text))
				{
					return false;
				}
			}
			if (t2.label != null && labels != null)
			{
				labels[t2.label] = t1;
			}
			int childCount = this.adaptor.GetChildCount(t1);
			int childCount2 = t2.ChildCount;
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = this.adaptor.GetChild(t1, i);
				TreeWizard.TreePattern treePattern = (TreeWizard.TreePattern)t2.GetChild(i);
				if (!this._Parse(child, treePattern, labels))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006950 File Offset: 0x00005950
		public object Create(string pattern)
		{
			TreePatternLexer treePatternLexer = new TreePatternLexer(pattern);
			TreePatternParser treePatternParser = new TreePatternParser(treePatternLexer, this, this.adaptor);
			return treePatternParser.Pattern();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000697A File Offset: 0x0000597A
		public static bool Equals(object t1, object t2, ITreeAdaptor adaptor)
		{
			return TreeWizard._Equals(t1, t2, adaptor);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006984 File Offset: 0x00005984
		public bool Equals(object t1, object t2)
		{
			return TreeWizard._Equals(t1, t2, this.adaptor);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006994 File Offset: 0x00005994
		protected static bool _Equals(object t1, object t2, ITreeAdaptor adaptor)
		{
			if (t1 == null || t2 == null)
			{
				return false;
			}
			if (adaptor.GetNodeType(t1) != adaptor.GetNodeType(t2))
			{
				return false;
			}
			if (!adaptor.GetNodeText(t1).Equals(adaptor.GetNodeText(t2)))
			{
				return false;
			}
			int childCount = adaptor.GetChildCount(t1);
			int childCount2 = adaptor.GetChildCount(t2);
			if (childCount != childCount2)
			{
				return false;
			}
			for (int i = 0; i < childCount; i++)
			{
				object child = adaptor.GetChild(t1, i);
				object child2 = adaptor.GetChild(t2, i);
				if (!TreeWizard._Equals(child, child2, adaptor))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000082 RID: 130
		protected ITreeAdaptor adaptor;

		// Token: 0x04000083 RID: 131
		protected IDictionary tokenNameToTypeMap;

		// Token: 0x02000033 RID: 51
		public interface ContextVisitor
		{
			// Token: 0x06000228 RID: 552
			void Visit(object t, object parent, int childIndex, IDictionary labels);
		}

		// Token: 0x02000034 RID: 52
		public abstract class Visitor : TreeWizard.ContextVisitor
		{
			// Token: 0x06000229 RID: 553 RVA: 0x00006A15 File Offset: 0x00005A15
			public void Visit(object t, object parent, int childIndex, IDictionary labels)
			{
				this.Visit(t);
			}

			// Token: 0x0600022A RID: 554
			public abstract void Visit(object t);
		}

		// Token: 0x02000035 RID: 53
		private sealed class RecordAllElementsVisitor : TreeWizard.Visitor
		{
			// Token: 0x0600022C RID: 556 RVA: 0x00006A26 File Offset: 0x00005A26
			public RecordAllElementsVisitor(IList list)
			{
				this.list = list;
			}

			// Token: 0x0600022D RID: 557 RVA: 0x00006A35 File Offset: 0x00005A35
			public override void Visit(object t)
			{
				this.list.Add(t);
			}

			// Token: 0x04000084 RID: 132
			private IList list;
		}

		// Token: 0x02000036 RID: 54
		private sealed class PatternMatchingContextVisitor : TreeWizard.ContextVisitor
		{
			// Token: 0x0600022E RID: 558 RVA: 0x00006A44 File Offset: 0x00005A44
			public PatternMatchingContextVisitor(TreeWizard owner, TreeWizard.TreePattern pattern, IList list)
			{
				this.owner = owner;
				this.pattern = pattern;
				this.list = list;
			}

			// Token: 0x0600022F RID: 559 RVA: 0x00006A61 File Offset: 0x00005A61
			public void Visit(object t, object parent, int childIndex, IDictionary labels)
			{
				if (this.owner._Parse(t, this.pattern, null))
				{
					this.list.Add(t);
				}
			}

			// Token: 0x04000085 RID: 133
			private TreeWizard owner;

			// Token: 0x04000086 RID: 134
			private TreeWizard.TreePattern pattern;

			// Token: 0x04000087 RID: 135
			private IList list;
		}

		// Token: 0x02000037 RID: 55
		public class TreePattern : CommonTree
		{
			// Token: 0x06000230 RID: 560 RVA: 0x00006A85 File Offset: 0x00005A85
			public TreePattern(IToken payload)
				: base(payload)
			{
			}

			// Token: 0x06000231 RID: 561 RVA: 0x00006A8E File Offset: 0x00005A8E
			public override string ToString()
			{
				if (this.label != null)
				{
					return "%" + this.label + ":" + base.ToString();
				}
				return base.ToString();
			}

			// Token: 0x04000088 RID: 136
			public string label;

			// Token: 0x04000089 RID: 137
			public bool hasTextArg;
		}

		// Token: 0x02000038 RID: 56
		private sealed class InvokeVisitorOnPatternMatchContextVisitor : TreeWizard.ContextVisitor
		{
			// Token: 0x06000232 RID: 562 RVA: 0x00006ABA File Offset: 0x00005ABA
			public InvokeVisitorOnPatternMatchContextVisitor(TreeWizard owner, TreeWizard.TreePattern pattern, TreeWizard.ContextVisitor visitor)
			{
				this.owner = owner;
				this.pattern = pattern;
				this.visitor = visitor;
			}

			// Token: 0x06000233 RID: 563 RVA: 0x00006AE2 File Offset: 0x00005AE2
			public void Visit(object t, object parent, int childIndex, IDictionary unusedlabels)
			{
				this.labels.Clear();
				if (this.owner._Parse(t, this.pattern, this.labels))
				{
					this.visitor.Visit(t, parent, childIndex, this.labels);
				}
			}

			// Token: 0x0400008A RID: 138
			private TreeWizard owner;

			// Token: 0x0400008B RID: 139
			private TreeWizard.TreePattern pattern;

			// Token: 0x0400008C RID: 140
			private TreeWizard.ContextVisitor visitor;

			// Token: 0x0400008D RID: 141
			private Hashtable labels = new Hashtable();
		}

		// Token: 0x02000039 RID: 57
		public class WildcardTreePattern : TreeWizard.TreePattern
		{
			// Token: 0x06000234 RID: 564 RVA: 0x00006B1D File Offset: 0x00005B1D
			public WildcardTreePattern(IToken payload)
				: base(payload)
			{
			}
		}

		// Token: 0x0200003A RID: 58
		public class TreePatternTreeAdaptor : CommonTreeAdaptor
		{
			// Token: 0x06000235 RID: 565 RVA: 0x00006B26 File Offset: 0x00005B26
			public override object Create(IToken payload)
			{
				return new TreeWizard.TreePattern(payload);
			}
		}
	}
}
