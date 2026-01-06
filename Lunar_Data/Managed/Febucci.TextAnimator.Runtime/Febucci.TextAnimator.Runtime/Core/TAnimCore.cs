using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Febucci.UI.Actions;
using Febucci.UI.Core.Parsing;
using Febucci.UI.Effects;
using Febucci.UI.Styles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Febucci.UI.Core
{
	// Token: 0x02000036 RID: 54
	[DisallowMultipleComponent]
	[HelpURL("https://www.febucci.com/text-animator-unity/docs/how-to-add-effects-to-your-texts/")]
	public abstract class TAnimCore : MonoBehaviour
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004C78 File Offset: 0x00002E78
		private TypewriterCore typewriter
		{
			get
			{
				if (this._typewriterCache != null)
				{
					return this._typewriterCache;
				}
				if (!base.TryGetComponent<TypewriterCore>(out this._typewriterCache))
				{
					Debug.LogError("Typewriter component is null on GameObject " + base.gameObject.name + ". Please add a typewriter on the same GameObject or set 'Typewriter Starts Automatically' to false.", base.gameObject);
				}
				return this._typewriterCache;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004CD3 File Offset: 0x00002ED3
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004CDB File Offset: 0x00002EDB
		public string textFull
		{
			get
			{
				return this._text;
			}
			set
			{
				if (this.typewriterStartsAutomatically && this.typewriter)
				{
					this.SetTypewriterText(value);
					return;
				}
				this.SetText(value);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004D01 File Offset: 0x00002F01
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004D09 File Offset: 0x00002F09
		public string textWithoutTextAnimTags { get; private set; } = string.Empty;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004D12 File Offset: 0x00002F12
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004D1A File Offset: 0x00002F1A
		public string textWithoutAnyTag { get; private set; } = string.Empty;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004D23 File Offset: 0x00002F23
		private bool hasText
		{
			get
			{
				return this.charactersCount > 0;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004D2E File Offset: 0x00002F2E
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004D36 File Offset: 0x00002F36
		public CharacterData latestCharacterShown { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004D40 File Offset: 0x00002F40
		public bool allLettersShown
		{
			get
			{
				if (this._maxVisibleCharacters < this.charactersCount)
				{
					return false;
				}
				if (this._firstVisibleCharacter == this._maxVisibleCharacters)
				{
					return false;
				}
				for (int i = 0; i < this.charactersCount; i++)
				{
					if (!this.characters[i].isVisible)
					{
						if (this.characters[i].passedTime <= 0f)
						{
							return false;
						}
					}
					else if (this.characters[i].info.isRendered && this.characters[i].passedTime < this.characters[i].info.appearancesMaxDuration)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public bool anyLetterVisible
		{
			get
			{
				if (this.characters.Length == 0)
				{
					return true;
				}
				if (this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(0) || this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(this.charactersCount - 1))
				{
					return true;
				}
				for (int i = 1; i < this.charactersCount - 1; i++)
				{
					if (this.<get_anyLetterVisible>g__IsCharacterVisible|30_0(i))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004E42 File Offset: 0x00003042
		public int CharactersCount
		{
			get
			{
				return this.charactersCount;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004E4A File Offset: 0x0000304A
		public CharacterData[] Characters
		{
			get
			{
				return this.characters;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004E52 File Offset: 0x00003052
		public int WordsCount
		{
			get
			{
				return this.wordsCount;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004E5A File Offset: 0x0000305A
		public WordInfo[] Words
		{
			get
			{
				return this.words;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004E62 File Offset: 0x00003062
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004E82 File Offset: 0x00003082
		public AnimationsDatabase DatabaseBehaviors
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseBehaviors;
				}
				return TextAnimatorSettings.Instance.behaviors.defaultDatabase;
			}
			set
			{
				this.useDefaultDatabases = false;
				this.databaseBehaviors = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004E99 File Offset: 0x00003099
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004EB9 File Offset: 0x000030B9
		public AnimationsDatabase DatabaseAppearances
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseAppearances;
				}
				return TextAnimatorSettings.Instance.appearances.defaultDatabase;
			}
			set
			{
				this.useDefaultDatabases = false;
				this.databaseAppearances = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004ED0 File Offset: 0x000030D0
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004EEB File Offset: 0x000030EB
		public StyleSheetScriptable StyleSheet
		{
			get
			{
				if (!this.useDefaultStyleSheet)
				{
					return this.styleSheet;
				}
				return TextAnimatorSettings.Instance.defaultStyleSheet;
			}
			set
			{
				this.useDefaultStyleSheet = false;
				this.requiresTagRefresh = true;
				this.styleSheet = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004F02 File Offset: 0x00003102
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004F0A File Offset: 0x0000310A
		public AnimationRegion[] Behaviors
		{
			get
			{
				return this.behaviors;
			}
			set
			{
				this.behaviors = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004F13 File Offset: 0x00003113
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004F1B File Offset: 0x0000311B
		public AnimationRegion[] Appearances
		{
			get
			{
				return this.appearances;
			}
			set
			{
				this.appearances = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004F24 File Offset: 0x00003124
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004F2C File Offset: 0x0000312C
		public AnimationRegion[] Disappearances
		{
			get
			{
				return this.disappearances;
			}
			set
			{
				this.disappearances = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004F35 File Offset: 0x00003135
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004F3D File Offset: 0x0000313D
		public ActionMarker[] Actions
		{
			get
			{
				return this.actions;
			}
			set
			{
				this.actions = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004F46 File Offset: 0x00003146
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004F66 File Offset: 0x00003166
		public ActionDatabase DatabaseActions
		{
			get
			{
				if (!this.useDefaultDatabases)
				{
					return this.databaseActions;
				}
				return TextAnimatorSettings.Instance.actions.defaultDatabase;
			}
			set
			{
				this.databaseActions = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004F76 File Offset: 0x00003176
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004F7E File Offset: 0x0000317E
		public EventMarker[] Events
		{
			get
			{
				return this.events;
			}
			set
			{
				this.events = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004F87 File Offset: 0x00003187
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004F8F File Offset: 0x0000318F
		public string[] DefaultAppearancesTags
		{
			get
			{
				return this.defaultAppearancesTags;
			}
			set
			{
				this.defaultAppearancesTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004F9F File Offset: 0x0000319F
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004FA7 File Offset: 0x000031A7
		public string[] DefaultDisappearancesTags
		{
			get
			{
				return this.defaultDisappearancesTags;
			}
			set
			{
				this.defaultDisappearancesTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004FB7 File Offset: 0x000031B7
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004FBF File Offset: 0x000031BF
		public string[] DefaultBehaviorsTags
		{
			get
			{
				return this.defaultBehaviorsTags;
			}
			set
			{
				this.defaultBehaviorsTags = value;
				this.requiresTagRefresh = true;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004FCF File Offset: 0x000031CF
		protected virtual void OnInitialized()
		{
		}

		// Token: 0x060000EF RID: 239
		public abstract string GetOriginalTextFromSource();

		// Token: 0x060000F0 RID: 240
		public abstract string GetStrippedTextFromSource();

		// Token: 0x060000F1 RID: 241
		public abstract void SetTextToSource(string text);

		// Token: 0x060000F2 RID: 242
		protected abstract bool HasChangedText(string strippedText);

		// Token: 0x060000F3 RID: 243
		protected abstract bool HasChangedRenderingSettings();

		// Token: 0x060000F4 RID: 244
		protected abstract int GetCharactersCount();

		// Token: 0x060000F5 RID: 245
		protected abstract void OnForceMeshUpdate();

		// Token: 0x060000F6 RID: 246
		protected abstract void CopyMeshFromSource(ref CharacterData[] characters);

		// Token: 0x060000F7 RID: 247
		protected abstract void PasteMeshToSource(CharacterData[] characters);

		// Token: 0x060000F8 RID: 248 RVA: 0x00004FD1 File Offset: 0x000031D1
		private void ForceMeshUpdate()
		{
			this.requiresMeshUpdate = false;
			this.OnForceMeshUpdate();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004FE0 File Offset: 0x000031E0
		private void Awake()
		{
			this.requiresTagRefresh = true;
			this.TryInitializing();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004FF0 File Offset: 0x000031F0
		private void TryInitializing()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			TextUtilities.Initialize();
			this.charactersCount = 0;
			this.characters = new CharacterData[0];
			this.wordsCount = 0;
			this.words = new WordInfo[0];
			this.behaviors = new AnimationRegion[0];
			this.appearances = new AnimationRegion[0];
			this.disappearances = new AnimationRegion[0];
			this.actions = new ActionMarker[0];
			this.events = new EventMarker[0];
			if (this.DatabaseActions)
			{
				this.DatabaseActions.ForceBuildRefresh();
			}
			if (this.DatabaseAppearances)
			{
				this.DatabaseAppearances.ForceBuildRefresh();
			}
			if (this.DatabaseBehaviors)
			{
				this.DatabaseBehaviors.ForceBuildRefresh();
			}
			if (this.StyleSheet)
			{
				this.StyleSheet.ForceBuildRefresh();
			}
			this.OnInitialized();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000050DC File Offset: 0x000032DC
		private void UpdateUniformIntensity()
		{
			if (this.useDynamicScaling)
			{
				for (int i = 0; i < this.characters.Length; i++)
				{
					this.characters[i].UpdateIntensity(this.referenceFontSize);
				}
				return;
			}
			for (int j = 0; j < this.characters.Length; j++)
			{
				this.characters[j].uniformIntensity = 1f;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005145 File Offset: 0x00003345
		protected virtual TagParserBase[] GetExtraParsers()
		{
			return Array.Empty<TagParserBase>();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000514C File Offset: 0x0000334C
		private void ConvertText(string textToParse, TAnimCore.ShowTextMode showTextMode)
		{
			this.TryInitializing();
			this.requiresTagRefresh = false;
			this._text = textToParse;
			this.settings = TextAnimatorSettings.Instance;
			if (!this.settings)
			{
				this.charactersCount = 0;
				Debug.LogError("Text Animator Settings not found. Skipping setting the text to Text Animator.");
				return;
			}
			if (this.useDefaultDatabases)
			{
				this.databaseBehaviors = this.settings.behaviors.defaultDatabase;
				this.databaseAppearances = this.settings.appearances.defaultDatabase;
				this.databaseActions = this.settings.actions.defaultDatabase;
			}
			AnimationParser<AnimationScriptableBase> animationParser = new AnimationParser<AnimationScriptableBase>(this.settings.behaviors.openingSymbol, '/', this.settings.behaviors.closingSymbol, VisibilityMode.Persistent, this.databaseBehaviors);
			AnimationParser<AnimationScriptableBase> animationParser2 = new AnimationParser<AnimationScriptableBase>(this.settings.appearances.openingSymbol, '/', this.settings.appearances.closingSymbol, VisibilityMode.OnVisible, this.databaseAppearances);
			AnimationParser<AnimationScriptableBase> animationParser3 = new AnimationParser<AnimationScriptableBase>(this.settings.appearances.openingSymbol, '/', '#', this.settings.appearances.closingSymbol, VisibilityMode.OnHiding, this.databaseAppearances);
			ActionParser actionParser = new ActionParser(this.settings.actions.openingSymbol, '/', this.settings.actions.closingSymbol, this.databaseActions);
			EventParser eventParser = new EventParser('<', '/', '>');
			List<TagParserBase> list = new List<TagParserBase> { animationParser, animationParser2, animationParser3, actionParser, eventParser };
			foreach (TagParserBase tagParserBase in this.GetExtraParsers())
			{
				list.Add(tagParserBase);
			}
			this.textWithoutTextAnimTags = (this.StyleSheet ? TextParser.ParseText(this._text, new TagParserBase[]
			{
				new StylesParser('<', '/', '>', this.StyleSheet)
			}) : this._text);
			this.textWithoutTextAnimTags = TextParser.ParseText(this.textWithoutTextAnimTags, list.ToArray());
			this.SetTextToSource(this.textWithoutTextAnimTags);
			this.textWithoutAnyTag = this.GetStrippedTextFromSource();
			this.charactersCount = this.GetCharactersCount();
			this.behaviors = animationParser.results;
			this.appearances = animationParser2.results;
			this.disappearances = animationParser3.results;
			this.actions = actionParser.results;
			this.events = eventParser.results;
			this.<ConvertText>g__AddFallbackEffectsFor|119_6<AnimationScriptableBase>(ref this.behaviors, VisibilityMode.Persistent, this.databaseBehaviors, this.defaultBehaviorsTags);
			this.<ConvertText>g__AddFallbackEffectsFor|119_6<AnimationScriptableBase>(ref this.appearances, VisibilityMode.OnVisible, this.databaseAppearances, this.defaultAppearancesTags);
			this.<ConvertText>g__AddFallbackEffectsFor|119_6<AnimationScriptableBase>(ref this.disappearances, VisibilityMode.OnHiding, this.databaseAppearances, this.defaultDisappearancesTags);
			AnimationRegion[] array = this.behaviors;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			array = this.appearances;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			array = this.disappearances;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].animation.InitializeOnce();
			}
			this.<ConvertText>g__PopulateCharacters|119_0(showTextMode != TAnimCore.ShowTextMode.Refresh);
			this.CopyMeshFromSource(ref this.characters);
			this.<ConvertText>g__CalculateWords|119_1();
			switch (showTextMode)
			{
			case TAnimCore.ShowTextMode.Hidden:
				this.<ConvertText>g__HideAllCharactersTime|119_3();
				break;
			case TAnimCore.ShowTextMode.Shown:
				this.<ConvertText>g__ShowCharacterTimes|119_4();
				break;
			case TAnimCore.ShowTextMode.UserTyping:
				this.<ConvertText>g__ShowCharacterTimes|119_4();
				if (this.charactersCount > 1)
				{
					this.<ConvertText>g__HideCharacterTime|119_2(this.charactersCount - 1);
					this.characters[this.charactersCount - 1].isVisible = true;
				}
				break;
			}
			this._maxVisibleCharacters = this.charactersCount;
			this.time.UpdateDeltaTime((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
			if (this.isResettingTimeOnNewText && showTextMode != TAnimCore.ShowTextMode.Refresh)
			{
				this.time.RestartTime();
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005543 File Offset: 0x00003743
		public void SetText(string text)
		{
			this.ConvertText(text, TAnimCore.ShowTextMode.Shown);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005550 File Offset: 0x00003750
		public void SwapText(string text)
		{
			int maxVisibleCharacters = this.maxVisibleCharacters;
			this.ConvertText(text, TAnimCore.ShowTextMode.Refresh);
			this.maxVisibleCharacters = maxVisibleCharacters;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005573 File Offset: 0x00003773
		public void SetText(string text, bool hideText)
		{
			this.ConvertText(text, hideText ? TAnimCore.ShowTextMode.Hidden : TAnimCore.ShowTextMode.Shown);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005584 File Offset: 0x00003784
		public void AppendText(string appendedText, bool hideText = false)
		{
			if (string.IsNullOrEmpty(appendedText))
			{
				return;
			}
			if (!this.hasText)
			{
				this.SetText(appendedText, hideText);
				return;
			}
			bool flag = this.isResettingTimeOnNewText;
			this.isResettingTimeOnNewText = false;
			int maxVisibleCharacters = this.maxVisibleCharacters;
			int firstVisibleCharacter = this.firstVisibleCharacter;
			this.SetText(this.textFull + appendedText, hideText);
			this.isResettingTimeOnNewText = flag;
			this.maxVisibleCharacters = maxVisibleCharacters;
			this.firstVisibleCharacter = firstVisibleCharacter;
			for (int i = this.firstVisibleCharacter; i < this.maxVisibleCharacters; i++)
			{
				this.characters[i].isVisible = true;
				this.characters[i].passedTime = this.characters[i].info.appearancesMaxDuration;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000563E File Offset: 0x0000383E
		private void SetTypewriterText(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.typewriter.ShowText(string.Empty);
				return;
			}
			this.typewriter.ShowText("<noparse></noparse>" + text);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000566F File Offset: 0x0000386F
		public void SetVisibilityChar(int index, bool isVisible)
		{
			if (index < 0 || index >= this.charactersCount)
			{
				return;
			}
			this.characters[index].isVisible = isVisible;
			if (isVisible)
			{
				this.latestCharacterShown = this.characters[index];
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000056A8 File Offset: 0x000038A8
		public void SetVisibilityWord(int index, bool isVisible)
		{
			if (index < 0 || index >= this.wordsCount)
			{
				return;
			}
			WordInfo wordInfo = this.words[index];
			int num = Mathf.Max(wordInfo.firstCharacterIndex, 0);
			while (num <= wordInfo.lastCharacterIndex && num < this.charactersCount)
			{
				this.SetVisibilityChar(num, isVisible);
				num++;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005700 File Offset: 0x00003900
		public void SetVisibilityEntireText(bool isVisible, bool canPlayEffects = true)
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				this.SetVisibilityChar(i, isVisible);
			}
			if (!canPlayEffects)
			{
				if (isVisible)
				{
					for (int j = 0; j < this.charactersCount; j++)
					{
						this.characters[j].passedTime = this.characters[j].info.appearancesMaxDuration;
					}
					return;
				}
				for (int k = 0; k < this.charactersCount; k++)
				{
					this.characters[k].passedTime = 0f;
				}
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000578C File Offset: 0x0000398C
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005794 File Offset: 0x00003994
		public int firstVisibleCharacter
		{
			get
			{
				return this._firstVisibleCharacter;
			}
			set
			{
				this._firstVisibleCharacter = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000579D File Offset: 0x0000399D
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000057A5 File Offset: 0x000039A5
		public int maxVisibleCharacters
		{
			get
			{
				return this._maxVisibleCharacters;
			}
			set
			{
				if (this._maxVisibleCharacters == value)
				{
					return;
				}
				this._maxVisibleCharacters = value;
				if (this._maxVisibleCharacters < 0)
				{
					this._maxVisibleCharacters = 0;
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000057C8 File Offset: 0x000039C8
		private void Update()
		{
			if (!this.IsReady())
			{
				return;
			}
			if (!this.HasChangedText(this.textWithoutTextAnimTags))
			{
				if (this.animationLoop == AnimationLoop.Update)
				{
					this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
				}
				return;
			}
			if (this.typewriterStartsAutomatically && this.typewriter)
			{
				this.SetTypewriterText(this.GetOriginalTextFromSource());
				return;
			}
			this.ConvertText(this.GetOriginalTextFromSource(), TAnimCore.ShowTextMode.UserTyping);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000583F File Offset: 0x00003A3F
		private void LateUpdate()
		{
			if (this.animationLoop == AnimationLoop.LateUpdate)
			{
				this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
			}
		}

		// Token: 0x0600010C RID: 268
		protected abstract bool IsReady();

		// Token: 0x0600010D RID: 269 RVA: 0x00005865 File Offset: 0x00003A65
		public void Animate(float deltaTime)
		{
			if (!this.IsReady())
			{
				return;
			}
			this.time.UpdateDeltaTime(deltaTime);
			this.time.IncreaseTime();
			this.AnimateText();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000588D File Offset: 0x00003A8D
		private bool IsCharacterAppearing(int i)
		{
			return i >= this._firstVisibleCharacter && i < this._maxVisibleCharacters && this.characters[i].isVisible;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000058B4 File Offset: 0x00003AB4
		private void ProcessAnimationRegions(AnimationRegion[] regions)
		{
			foreach (AnimationRegion animationRegion in regions)
			{
				foreach (TagRange tagRange in animationRegion.ranges)
				{
					animationRegion.SetupContextFor(this, tagRange.modifiers);
					Vector2Int vector2Int = tagRange.indexes;
					int num = vector2Int.x;
					for (;;)
					{
						int num2 = num;
						vector2Int = tagRange.indexes;
						if (num2 >= vector2Int.y || num >= this.charactersCount)
						{
							break;
						}
						if (this.characters[num].passedTime > 0f && animationRegion.IsVisibilityPolicySatisfied(this.IsCharacterAppearing(num)) && animationRegion.animation.CanApplyEffectTo(this.characters[num], this))
						{
							animationRegion.animation.ApplyEffectTo(ref this.characters[num], this);
						}
						num++;
					}
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000059AC File Offset: 0x00003BAC
		private void AnimateText()
		{
			if (!this.hasText)
			{
				return;
			}
			this.TryInitializing();
			int num = 0;
			while (num < this.charactersCount && num < this.characters.Length)
			{
				if (!this.characters[num].info.isRendered)
				{
					this.characters[num].passedTime = 0f;
					this.characters[num].Hide();
				}
				else
				{
					this.characters[num].ResetAnimation();
					if (this.IsCharacterAppearing(num))
					{
						CharacterData[] array = this.characters;
						int num2 = num;
						array[num2].passedTime = array[num2].passedTime + this.time.deltaTime;
					}
					else
					{
						if (this.characters[num].passedTime > this.characters[num].info.disappearancesMaxDuration)
						{
							this.characters[num].passedTime = this.characters[num].info.disappearancesMaxDuration;
						}
						else
						{
							CharacterData[] array2 = this.characters;
							int num3 = num;
							array2[num3].passedTime = array2[num3].passedTime - this.time.deltaTime;
						}
						if (this.characters[num].passedTime <= 0f)
						{
							this.characters[num].passedTime = 0f;
							this.characters[num].Hide();
						}
					}
				}
				num++;
			}
			this.UpdateUniformIntensity();
			if (this.isAnimatingBehaviors && this.settings.behaviors.enabled)
			{
				this.ProcessAnimationRegions(this.behaviors);
			}
			if (this.isAnimatingAppearances && this.settings.appearances.enabled)
			{
				this.ProcessAnimationRegions(this.appearances);
				this.ProcessAnimationRegions(this.disappearances);
			}
			this.PasteMeshToSource(this.characters);
			if (this.requiresMeshUpdate || this.HasChangedRenderingSettings())
			{
				this.ForceMeshUpdate();
				this.CopyMeshFromSource(ref this.characters);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005BAA File Offset: 0x00003DAA
		public void ScheduleMeshRefresh()
		{
			this.requiresMeshUpdate = true;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005BB4 File Offset: 0x00003DB4
		public void ForceDatabaseRefresh()
		{
			if (this.DatabaseActions)
			{
				this.DatabaseActions.ForceBuildRefresh();
			}
			if (this.DatabaseAppearances)
			{
				this.DatabaseAppearances.ForceBuildRefresh();
			}
			if (this.DatabaseBehaviors)
			{
				this.DatabaseBehaviors.ForceBuildRefresh();
			}
			if (this.StyleSheet)
			{
				this.StyleSheet.ForceBuildRefresh();
			}
			this.ConvertText(this.GetOriginalTextFromSource(), TAnimCore.ShowTextMode.Refresh);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005C2E File Offset: 0x00003E2E
		public void SetBehaviorsActive(bool isCategoryEnabled)
		{
			this.isAnimatingBehaviors = isCategoryEnabled;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005C37 File Offset: 0x00003E37
		public void SetAppearancesActive(bool isCategoryEnabled)
		{
			this.isAnimatingAppearances = isCategoryEnabled;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005C40 File Offset: 0x00003E40
		protected virtual void OnEnable()
		{
			this.requiresMeshUpdate = true;
			this.AnimateText();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005C4F File Offset: 0x00003E4F
		public void ResetState()
		{
			this._text = string.Empty;
			this.textWithoutTextAnimTags = string.Empty;
			this.textWithoutAnyTag = string.Empty;
			this.charactersCount = 0;
			this.wordsCount = 0;
			this.initialized = false;
			this.TryInitializing();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005C8D File Offset: 0x00003E8D
		[Obsolete("Use TextAnimatorSettings.SetAllEffectsActive instead")]
		public static void EnableAllEffects(bool enabled)
		{
			TextAnimatorSettings.SetAllEffectsActive(enabled);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005C95 File Offset: 0x00003E95
		[Obsolete("Use TextAnimatorSettings.SetAppearancesActive instead")]
		public static void EnableAppearances(bool enabled)
		{
			TextAnimatorSettings.SetAppearancesActive(enabled);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005C9D File Offset: 0x00003E9D
		[Obsolete("Use TextAnimatorSettings.SetBehaviorsActive instead")]
		public static void EnableBehaviors(bool enabled)
		{
			TextAnimatorSettings.SetBehaviorsActive(enabled);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005CA5 File Offset: 0x00003EA5
		[Obsolete("Use SetAppearancesActive instead")]
		public void EnableAppearancesLocally(bool value)
		{
			this.SetAppearancesActive(value);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005CAE File Offset: 0x00003EAE
		[Obsolete("Use SetBehaviorsActive instead")]
		public void EnableBehaviorsLocally(bool value)
		{
			this.SetBehaviorsActive(value);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005CB7 File Offset: 0x00003EB7
		[Obsolete("Use SetVisibilityEntireText instead")]
		public void ShowAllCharacters(bool skipAppearanceEffects)
		{
			this.SetVisibilityEntireText(true, skipAppearanceEffects);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005CC1 File Offset: 0x00003EC1
		[Obsolete("Use 'Animate' instead.")]
		public void UpdateEffects()
		{
			this.Animate((this.timeScale == TimeScale.Unscaled) ? Time.unscaledDeltaTime : Time.deltaTime);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005CDE File Offset: 0x00003EDE
		[Obsolete("Events are not tied to TextAnimators anymore, but to their Typewriters. Please invoke 'TriggerRemainingEvents' on the Typewriter component instead.")]
		public void TriggerRemainingEvents()
		{
			if (this.typewriter)
			{
				this.typewriter.TriggerRemainingEvents();
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005CF8 File Offset: 0x00003EF8
		[Obsolete("Events are not tied to TextAnimators anymore, but to their related typewriters. Please invoke 'TriggerVisibleEvents' on the Typewriter component instead.")]
		public void TriggerVisibleEvents()
		{
			if (this.typewriter)
			{
				this.typewriter.TriggerVisibleEvents();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005D12 File Offset: 0x00003F12
		[Obsolete("Use 'ScheduleMeshRefresh' instead")]
		public void ForceMeshRefresh()
		{
			this.ScheduleMeshRefresh();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005D1A File Offset: 0x00003F1A
		[Obsolete("To restart TextAnimator's time, please use 'time.RestartTime()'. To skip appearances effects please set 'SetVisibilityEntireText(true, false)' instead")]
		public void ResetEffectsTime(bool skipAppearances)
		{
			this.time.RestartTime();
			if (skipAppearances)
			{
				this.SetVisibilityEntireText(true, false);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005D32 File Offset: 0x00003F32
		[Obsolete("Please use 'isResettingTimeOnNewText' instead")]
		public bool isResettingEffectsOnNewText
		{
			get
			{
				return this.isResettingTimeOnNewText;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00005D3A File Offset: 0x00003F3A
		[Obsolete("Please use 'animationLoop' instead")]
		public AnimationLoop updateMode
		{
			get
			{
				return this.animationLoop;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005D42 File Offset: 0x00003F42
		[Obsolete("Events are now handled/stored by Typewriters instead.")]
		public MessageEvent onEvent
		{
			get
			{
				return this.typewriter.onMessage;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005D4F File Offset: 0x00003F4F
		[Obsolete("Please use TextAnimatorSettings.Instance.appearances.enabled instead")]
		public static bool effectsAppearancesEnabled
		{
			get
			{
				return TextAnimatorSettings.Instance.appearances.enabled;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005D60 File Offset: 0x00003F60
		[Obsolete("Please use TextAnimatorSettings.Instance.behaviors.enabled instead")]
		public static bool effectsBehaviorsEnabled
		{
			get
			{
				return TextAnimatorSettings.Instance.behaviors.enabled;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005D71 File Offset: 0x00003F71
		[Obsolete("Please use 'textFull' instead")]
		public string text
		{
			get
			{
				return this.textFull;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005D79 File Offset: 0x00003F79
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00005D81 File Offset: 0x00003F81
		[Obsolete("Please change 'referenceFontSize' instead")]
		public float effectIntensityMultiplier
		{
			get
			{
				return this.referenceFontSize;
			}
			set
			{
				this.referenceFontSize = value;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005E1D File Offset: 0x0000401D
		[CompilerGenerated]
		private bool <get_anyLetterVisible>g__IsCharacterVisible|30_0(int index)
		{
			return this.characters[index].passedTime > 0f;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005E38 File Offset: 0x00004038
		[CompilerGenerated]
		private void <ConvertText>g__PopulateCharacters|119_0(bool resetVisibility)
		{
			if (this.characters.Length < this.charactersCount)
			{
				Array.Resize<CharacterData>(ref this.characters, this.charactersCount);
			}
			TAnimCore.<>c__DisplayClass119_0 CS$<>8__locals1;
			CS$<>8__locals1.i = 0;
			while (CS$<>8__locals1.i < this.charactersCount)
			{
				this.characters[CS$<>8__locals1.i].ResetInfo(CS$<>8__locals1.i, resetVisibility);
				this.characters[CS$<>8__locals1.i].info.disappearancesMaxDuration = this.<ConvertText>g__CalculateRegionMaxDuration|119_7(this.disappearances, ref CS$<>8__locals1);
				this.characters[CS$<>8__locals1.i].info.appearancesMaxDuration = this.<ConvertText>g__CalculateRegionMaxDuration|119_7(this.appearances, ref CS$<>8__locals1);
				int i = CS$<>8__locals1.i;
				CS$<>8__locals1.i = i + 1;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005F04 File Offset: 0x00004104
		[CompilerGenerated]
		private float <ConvertText>g__CalculateRegionMaxDuration|119_7(AnimationRegion[] tags, ref TAnimCore.<>c__DisplayClass119_0 A_2)
		{
			float num = 0f;
			foreach (AnimationRegion animationRegion in tags)
			{
				foreach (TagRange tagRange in animationRegion.ranges)
				{
					int i2 = A_2.i;
					Vector2Int vector2Int = tagRange.indexes;
					if (i2 >= vector2Int.x)
					{
						int i3 = A_2.i;
						vector2Int = tagRange.indexes;
						if (i3 < vector2Int.y)
						{
							animationRegion.SetupContextFor(this, tagRange.modifiers);
							float maxDuration = animationRegion.animation.GetMaxDuration();
							if (maxDuration > num)
							{
								num = maxDuration;
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005FAC File Offset: 0x000041AC
		[CompilerGenerated]
		private void <ConvertText>g__CalculateWords|119_1()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.wordsCount = this.charactersCount;
			if (this.words.Length < this.wordsCount)
			{
				Array.Resize<WordInfo>(ref this.words, this.wordsCount);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < this.charactersCount; i++)
			{
				if (!char.IsWhiteSpace(this.characters[i].info.character))
				{
					this.characters[i].wordIndex = num2;
					stringBuilder.Append(this.characters[i].info.character);
					num++;
				}
				else
				{
					this.characters[i].wordIndex = -1;
					if (num > 0)
					{
						this.words[num2] = new WordInfo(num3, num3 + num - 1, stringBuilder.ToString());
						num3 += num + 1;
						num2++;
					}
					else
					{
						num3++;
					}
					stringBuilder.Clear();
					num = 0;
				}
			}
			if (num > 0)
			{
				this.words[num2] = new WordInfo(num3, num3 + num - 1, stringBuilder.ToString());
				num2++;
			}
			this.wordsCount = num2;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000060DC File Offset: 0x000042DC
		[CompilerGenerated]
		private void <ConvertText>g__HideCharacterTime|119_2(int charIndex)
		{
			CharacterData characterData = this.characters[charIndex];
			characterData.isVisible = false;
			characterData.passedTime = 0f;
			characterData.Hide();
			this.characters[charIndex] = characterData;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006120 File Offset: 0x00004320
		[CompilerGenerated]
		private void <ConvertText>g__HideAllCharactersTime|119_3()
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				this.<ConvertText>g__HideCharacterTime|119_2(i);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006148 File Offset: 0x00004348
		[CompilerGenerated]
		private void <ConvertText>g__ShowCharacterTimes|119_4()
		{
			for (int i = 0; i < this.charactersCount; i++)
			{
				CharacterData characterData = this.characters[i];
				characterData.isVisible = true;
				characterData.passedTime = characterData.info.appearancesMaxDuration;
				this.characters[i] = characterData;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000619C File Offset: 0x0000439C
		[CompilerGenerated]
		internal static bool <ConvertText>g__IsCharacterInsideAnyEffect|119_5(int charIndex, AnimationRegion[] regions)
		{
			for (int i = 0; i < regions.Length; i++)
			{
				foreach (TagRange tagRange in regions[i].ranges)
				{
					Vector2Int vector2Int = tagRange.indexes;
					if (charIndex >= vector2Int.x)
					{
						vector2Int = tagRange.indexes;
						if (vector2Int.y != 2147483647)
						{
							vector2Int = tagRange.indexes;
							if (charIndex >= vector2Int.y)
							{
								goto IL_005B;
							}
						}
						return true;
					}
					IL_005B:;
				}
			}
			return false;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000621C File Offset: 0x0000441C
		[CompilerGenerated]
		private void <ConvertText>g__AddFallbackEffectsFor|119_6<T>(ref AnimationRegion[] currentEffects, VisibilityMode visibilityMode, Database<T> database, string[] defaultEffectsTags) where T : AnimationScriptableBase
		{
			if (!database)
			{
				return;
			}
			if (defaultEffectsTags == null || defaultEffectsTags.Length == 0)
			{
				return;
			}
			List<TAnimCore.DefaultRegion> list = new List<TAnimCore.DefaultRegion>();
			foreach (string text in defaultEffectsTags)
			{
				if (string.IsNullOrEmpty(text))
				{
					if (Application.isPlaying)
					{
						Debug.LogError("Empty tag as default effect in database " + database.name + ". Skipping.", base.gameObject);
					}
				}
				else
				{
					string[] array = text.Split(' ', StringSplitOptions.None);
					string text2 = array[0];
					if (!database.ContainsKey(text2))
					{
						if (Application.isPlaying)
						{
							Debug.LogError(string.Concat(new string[] { "Fallback effect with tag '", text2, "' not found in database ", database.name, ". Skipping." }), base.gameObject);
						}
					}
					else
					{
						list.Add(new TAnimCore.DefaultRegion(text2, visibilityMode, database[text2], array));
					}
				}
			}
			if (currentEffects.Length == 0 || this.defaultTagsMode == TAnimCore.DefaultTagsMode.Constant)
			{
				using (List<TAnimCore.DefaultRegion>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TAnimCore.DefaultRegion defaultRegion = enumerator.Current;
						defaultRegion.region.OpenNewRange(0, defaultRegion.tagWords);
					}
					goto IL_0200;
				}
			}
			for (int j = 0; j < this.charactersCount; j++)
			{
				if (!TAnimCore.<ConvertText>g__IsCharacterInsideAnyEffect|119_5(j, currentEffects))
				{
					foreach (TAnimCore.DefaultRegion defaultRegion2 in list)
					{
						defaultRegion2.region.OpenNewRange(j, defaultRegion2.tagWords);
					}
					int num = j + 1;
					while (num < this.charactersCount && !TAnimCore.<ConvertText>g__IsCharacterInsideAnyEffect|119_5(num, currentEffects))
					{
						num++;
					}
					foreach (TAnimCore.DefaultRegion defaultRegion3 in list)
					{
						defaultRegion3.region.TryClosingRange(num);
					}
					j = num;
				}
			}
			IL_0200:
			int num2 = currentEffects.Length;
			Array.Resize<AnimationRegion>(ref currentEffects, currentEffects.Length + list.Count);
			for (int k = 0; k < list.Count; k++)
			{
				currentEffects[num2 + k] = list[k].region;
			}
		}

		// Token: 0x040000A7 RID: 167
		private bool initialized;

		// Token: 0x040000A8 RID: 168
		private bool requiresTagRefresh;

		// Token: 0x040000A9 RID: 169
		[Tooltip("If the source text changes, should the typewriter start automatically? Requires a Typewriter component if true.\nP.s. Previously, this option was called 'Use Easy Integration'.")]
		public bool typewriterStartsAutomatically;

		// Token: 0x040000AA RID: 170
		private TypewriterCore _typewriterCache;

		// Token: 0x040000AB RID: 171
		[Tooltip("Controls when this TextAnimator component should update its effects. Defaults in the 'Update' Loop.\nSet it to 'Manual' if you want to control the animations from your own loop instead.")]
		public AnimationLoop animationLoop;

		// Token: 0x040000AC RID: 172
		[Tooltip("Chooses which Time Scale to use when animating effects.\nSet it to 'Unscaled' if you want to animate effects even when the game is paused.")]
		public TimeScale timeScale;

		// Token: 0x040000AD RID: 173
		[SerializeField]
		[TextArea(4, 10)]
		[HideInInspector]
		private string _text = string.Empty;

		// Token: 0x040000B1 RID: 177
		private int charactersCount;

		// Token: 0x040000B2 RID: 178
		private CharacterData[] characters;

		// Token: 0x040000B3 RID: 179
		private int wordsCount;

		// Token: 0x040000B4 RID: 180
		private WordInfo[] words;

		// Token: 0x040000B5 RID: 181
		[Tooltip("True if you want the animations to be uniform/consistent across different font sizes. Default/Suggested to leave this as true, and change the 'Reference Font Size'.\nOtherwise, effects will move more when the text is smaller (requires less space on screen)")]
		public bool useDynamicScaling = true;

		// Token: 0x040000B6 RID: 182
		[Tooltip("Font size that will be used as reference to keep animations consistent/uniform at different scales.")]
		public float referenceFontSize = 10f;

		// Token: 0x040000B7 RID: 183
		[Tooltip("True if you want the animator's time to be reset on new text.")]
		[FormerlySerializedAs("isResettingEffectsOnNewText")]
		public bool isResettingTimeOnNewText = true;

		// Token: 0x040000B8 RID: 184
		private bool isAnimatingBehaviors = true;

		// Token: 0x040000B9 RID: 185
		private bool isAnimatingAppearances = true;

		// Token: 0x040000BA RID: 186
		[Tooltip("Lets you use the databases referenced in the 'TextAnimatorSettings' asset.\nSet to false if you'd like to specify which databases to use in this component.")]
		public bool useDefaultDatabases = true;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private AnimationsDatabase databaseBehaviors;

		// Token: 0x040000BC RID: 188
		[SerializeField]
		private AnimationsDatabase databaseAppearances;

		// Token: 0x040000BD RID: 189
		public bool useDefaultStyleSheet = true;

		// Token: 0x040000BE RID: 190
		[SerializeField]
		private StyleSheetScriptable styleSheet;

		// Token: 0x040000BF RID: 191
		private AnimationRegion[] behaviors;

		// Token: 0x040000C0 RID: 192
		private AnimationRegion[] appearances;

		// Token: 0x040000C1 RID: 193
		private AnimationRegion[] disappearances;

		// Token: 0x040000C2 RID: 194
		private ActionMarker[] actions;

		// Token: 0x040000C3 RID: 195
		[SerializeField]
		private ActionDatabase databaseActions;

		// Token: 0x040000C4 RID: 196
		private EventMarker[] events;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private string[] defaultAppearancesTags = new string[] { "size" };

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		private string[] defaultDisappearancesTags = new string[] { "fade" };

		// Token: 0x040000C7 RID: 199
		[SerializeField]
		private string[] defaultBehaviorsTags;

		// Token: 0x040000C8 RID: 200
		private bool requiresMeshUpdate;

		// Token: 0x040000C9 RID: 201
		[HideInInspector]
		public TimeData time;

		// Token: 0x040000CA RID: 202
		[Tooltip("Controls how default tags should be applied.\n\"Fallback\" will apply the effects only to characters that don't have any.\n\"Constant\" will apply the default effects to all the characters, even if they already have other tags via text.")]
		public TAnimCore.DefaultTagsMode defaultTagsMode;

		// Token: 0x040000CB RID: 203
		private TextAnimatorSettings settings;

		// Token: 0x040000CC RID: 204
		private int _firstVisibleCharacter;

		// Token: 0x040000CD RID: 205
		private int _maxVisibleCharacters;

		// Token: 0x02000059 RID: 89
		private enum ShowTextMode : byte
		{
			// Token: 0x0400013C RID: 316
			Hidden,
			// Token: 0x0400013D RID: 317
			Shown,
			// Token: 0x0400013E RID: 318
			UserTyping,
			// Token: 0x0400013F RID: 319
			Refresh
		}

		// Token: 0x0200005A RID: 90
		private struct DefaultRegion
		{
			// Token: 0x060001B8 RID: 440 RVA: 0x00007FCF File Offset: 0x000061CF
			public DefaultRegion(string tagID, VisibilityMode visibilityMode, AnimationScriptableBase scriptable, string[] tagWords)
			{
				this.tagWords = tagWords;
				this.region = new AnimationRegion(tagID, visibilityMode, scriptable);
			}

			// Token: 0x04000140 RID: 320
			public string[] tagWords;

			// Token: 0x04000141 RID: 321
			public AnimationRegion region;
		}

		// Token: 0x0200005B RID: 91
		public enum DefaultTagsMode
		{
			// Token: 0x04000143 RID: 323
			Fallback,
			// Token: 0x04000144 RID: 324
			Constant
		}
	}
}
