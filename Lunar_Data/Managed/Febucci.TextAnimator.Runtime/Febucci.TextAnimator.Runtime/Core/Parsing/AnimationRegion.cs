using System;
using System.Text;
using Febucci.UI.Effects;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x0200004E RID: 78
	public class AnimationRegion : RegionBase
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00007680 File Offset: 0x00005880
		public AnimationRegion(string tagId, VisibilityMode visibilityMode, AnimationScriptableBase animation)
			: base(tagId)
		{
			this.visibilityMode = visibilityMode;
			this.animation = animation;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00007697 File Offset: 0x00005897
		public bool IsVisibilityPolicySatisfied(bool visible)
		{
			return this.visibilityMode == VisibilityMode.Persistent || this.visibilityMode.HasFlag(VisibilityMode.OnVisible) == visible;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000076BD File Offset: 0x000058BD
		public void OpenNewRange(int startIndex)
		{
			this.OpenNewRange(startIndex, Array.Empty<string>());
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000076CC File Offset: 0x000058CC
		public void OpenNewRange(int startIndex, string[] tagWords)
		{
			Array.Resize<TagRange>(ref this.ranges, this.ranges.Length + 1);
			TagRange tagRange = new TagRange(new Vector2Int(startIndex, int.MaxValue), Array.Empty<ModifierInfo>());
			for (int i = 1; i < tagWords.Length; i++)
			{
				string text = tagWords[i];
				int num = text.IndexOf('=');
				float num2;
				if (num > 0 && FormatUtils.TryGetFloat(text.Substring(num + 1), 0f, out num2))
				{
					Array.Resize<ModifierInfo>(ref tagRange.modifiers, tagRange.modifiers.Length + 1);
					tagRange.modifiers[tagRange.modifiers.Length - 1] = new ModifierInfo(text.Substring(0, num), num2);
				}
			}
			this.ranges[this.ranges.Length - 1] = tagRange;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000778C File Offset: 0x0000598C
		public void TryClosingRange(int endIndex)
		{
			if (this.ranges.Length == 0)
			{
				return;
			}
			for (int i = this.ranges.Length - 1; i >= 0; i--)
			{
				if (this.ranges[i].indexes.y == 2147483647)
				{
					TagRange tagRange = this.ranges[i];
					tagRange.indexes.y = endIndex;
					this.ranges[i] = tagRange;
					return;
				}
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00007800 File Offset: 0x00005A00
		public void CloseAllOpenedRanges(int endIndex)
		{
			if (this.ranges.Length == 0)
			{
				return;
			}
			for (int i = this.ranges.Length - 1; i >= 0; i--)
			{
				if (this.ranges[i].indexes.y == 2147483647)
				{
					TagRange tagRange = this.ranges[i];
					tagRange.indexes.y = endIndex;
					this.ranges[i] = tagRange;
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00007870 File Offset: 0x00005A70
		public virtual void SetupContextFor(TAnimCore animator, ModifierInfo[] modifiers)
		{
			this.animation.ResetContext(animator);
			foreach (ModifierInfo modifierInfo in modifiers)
			{
				this.animation.SetModifier(modifierInfo);
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000078B0 File Offset: 0x00005AB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("tag: ");
			stringBuilder.Append(this.tagId);
			if (this.ranges.Length == 0)
			{
				stringBuilder.Append("\nNo ranges");
			}
			else
			{
				for (int i = 0; i < this.ranges.Length; i++)
				{
					stringBuilder.Append('\n');
					stringBuilder.Append('-');
					stringBuilder.Append('-');
					stringBuilder.Append(this.ranges[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400011A RID: 282
		private readonly VisibilityMode visibilityMode;

		// Token: 0x0400011B RID: 283
		public readonly AnimationScriptableBase animation;
	}
}
