using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;

namespace System.ComponentModel
{
	/// <summary>Represents a mask-parsing service that can be used by any number of controls that support masking, such as the <see cref="T:System.Windows.Forms.MaskedTextBox" /> control.</summary>
	// Token: 0x020006EA RID: 1770
	public class MaskedTextProvider : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		// Token: 0x06003830 RID: 14384 RVA: 0x000C456B File Offset: 0x000C276B
		public MaskedTextProvider(string mask)
			: this(mask, null, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06003831 RID: 14385 RVA: 0x000C457A File Offset: 0x000C277A
		public MaskedTextProvider(string mask, bool restrictToAscii)
			: this(mask, null, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask and culture.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		// Token: 0x06003832 RID: 14386 RVA: 0x000C4589 File Offset: 0x000C2789
		public MaskedTextProvider(string mask, CultureInfo culture)
			: this(mask, culture, true, '_', '\0', false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		// Token: 0x06003833 RID: 14387 RVA: 0x000C4598 File Offset: 0x000C2798
		public MaskedTextProvider(string mask, CultureInfo culture, bool restrictToAscii)
			: this(mask, culture, true, '_', '\0', restrictToAscii)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06003834 RID: 14388 RVA: 0x000C45A7 File Offset: 0x000C27A7
		public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput)
			: this(mask, null, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, password character, and prompt usage value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="allowPromptAsInput">true to allow the prompt character as input; otherwise false. </param>
		// Token: 0x06003835 RID: 14389 RVA: 0x000C45B6 File Offset: 0x000C27B6
		public MaskedTextProvider(string mask, CultureInfo culture, char passwordChar, bool allowPromptAsInput)
			: this(mask, culture, allowPromptAsInput, '_', passwordChar, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MaskedTextProvider" /> class using the specified mask, culture, prompt usage value, prompt character, password character, and ASCII restriction value.</summary>
		/// <param name="mask">A <see cref="T:System.String" /> that represents the input mask. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that is used to set region-sensitive separator characters.</param>
		/// <param name="allowPromptAsInput">A <see cref="T:System.Boolean" /> value that specifies whether the prompt character should be allowed as a valid input character. </param>
		/// <param name="promptChar">A <see cref="T:System.Char" /> that will be displayed as a placeholder for user input.</param>
		/// <param name="passwordChar">A <see cref="T:System.Char" /> that will be displayed for characters entered into a password string.</param>
		/// <param name="restrictToAscii">true to restrict input to ASCII-compatible characters; otherwise false to allow the entire Unicode set. </param>
		/// <exception cref="T:System.ArgumentException">The mask parameter is null or <see cref="F:System.String.Empty" />.-or-The mask contains one or more non-printable characters. </exception>
		// Token: 0x06003836 RID: 14390 RVA: 0x000C45C8 File Offset: 0x000C27C8
		public MaskedTextProvider(string mask, CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
		{
			if (string.IsNullOrEmpty(mask))
			{
				throw new ArgumentException(SR.Format("The Mask value cannot be null or empty.", Array.Empty<object>()), "mask");
			}
			for (int i = 0; i < mask.Length; i++)
			{
				if (!MaskedTextProvider.IsPrintableChar(mask[i]))
				{
					throw new ArgumentException("The specified mask contains invalid characters.");
				}
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			this._flagState = default(BitVector32);
			this.Mask = mask;
			this._promptChar = promptChar;
			this._passwordChar = passwordChar;
			if (culture.IsNeutralCulture)
			{
				foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (culture.Equals(cultureInfo.Parent))
					{
						this.Culture = cultureInfo;
						break;
					}
				}
				if (this.Culture == null)
				{
					this.Culture = CultureInfo.InvariantCulture;
				}
			}
			else
			{
				this.Culture = culture;
			}
			if (!this.Culture.IsReadOnly)
			{
				this.Culture = CultureInfo.ReadOnly(this.Culture);
			}
			this._flagState[MaskedTextProvider.s_ALLOW_PROMPT_AS_INPUT] = allowPromptAsInput;
			this._flagState[MaskedTextProvider.s_ASCII_ONLY] = restrictToAscii;
			this._flagState[MaskedTextProvider.s_INCLUDE_PROMPT] = false;
			this._flagState[MaskedTextProvider.s_INCLUDE_LITERALS] = true;
			this._flagState[MaskedTextProvider.s_RESET_ON_PROMPT] = true;
			this._flagState[MaskedTextProvider.s_SKIP_SPACE] = true;
			this._flagState[MaskedTextProvider.s_RESET_ON_LITERALS] = true;
			this.Initialize();
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000C4748 File Offset: 0x000C2948
		private void Initialize()
		{
			this._testString = new StringBuilder();
			this._stringDescriptor = new List<MaskedTextProvider.CharDescriptor>();
			MaskedTextProvider.CaseConversion caseConversion = MaskedTextProvider.CaseConversion.None;
			bool flag = false;
			int num = 0;
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Literal;
			string text = string.Empty;
			int i = 0;
			while (i < this.Mask.Length)
			{
				char c = this.Mask[i];
				if (!flag)
				{
					if (c <= 'C')
					{
						switch (c)
						{
						case '#':
							goto IL_019E;
						case '$':
							text = this.Culture.NumberFormat.CurrencySymbol;
							charType = MaskedTextProvider.CharType.Separator;
							goto IL_01BE;
						case '%':
							goto IL_01B8;
						case '&':
							break;
						default:
							switch (c)
							{
							case ',':
								text = this.Culture.NumberFormat.NumberGroupSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '-':
								goto IL_01B8;
							case '.':
								text = this.Culture.NumberFormat.NumberDecimalSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '/':
								text = this.Culture.DateTimeFormat.DateSeparator;
								charType = MaskedTextProvider.CharType.Separator;
								goto IL_01BE;
							case '0':
								break;
							default:
								switch (c)
								{
								case '9':
								case '?':
								case 'C':
									goto IL_019E;
								case ':':
									text = this.Culture.DateTimeFormat.TimeSeparator;
									charType = MaskedTextProvider.CharType.Separator;
									goto IL_01BE;
								case ';':
								case '=':
								case '@':
								case 'B':
									goto IL_01B8;
								case '<':
									caseConversion = MaskedTextProvider.CaseConversion.ToLower;
									goto IL_022A;
								case '>':
									caseConversion = MaskedTextProvider.CaseConversion.ToUpper;
									goto IL_022A;
								case 'A':
									break;
								default:
									goto IL_01B8;
								}
								break;
							}
							break;
						}
					}
					else if (c <= '\\')
					{
						if (c != 'L')
						{
							if (c != '\\')
							{
								goto IL_01B8;
							}
							flag = true;
							charType = MaskedTextProvider.CharType.Literal;
							goto IL_022A;
						}
					}
					else
					{
						if (c == 'a')
						{
							goto IL_019E;
						}
						if (c != '|')
						{
							goto IL_01B8;
						}
						caseConversion = MaskedTextProvider.CaseConversion.None;
						goto IL_022A;
					}
					this._requiredEditChars++;
					c = this._promptChar;
					charType = MaskedTextProvider.CharType.EditRequired;
					goto IL_01BE;
					IL_019E:
					this._optionalEditChars++;
					c = this._promptChar;
					charType = MaskedTextProvider.CharType.EditOptional;
					goto IL_01BE;
					IL_01B8:
					charType = MaskedTextProvider.CharType.Literal;
					goto IL_01BE;
				}
				flag = false;
				goto IL_01BE;
				IL_022A:
				i++;
				continue;
				IL_01BE:
				MaskedTextProvider.CharDescriptor charDescriptor = new MaskedTextProvider.CharDescriptor(i, charType);
				if (MaskedTextProvider.IsEditPosition(charDescriptor))
				{
					charDescriptor.CaseConversion = caseConversion;
				}
				if (charType != MaskedTextProvider.CharType.Separator)
				{
					text = c.ToString();
				}
				foreach (char c2 in text)
				{
					this._testString.Append(c2);
					this._stringDescriptor.Add(charDescriptor);
					num++;
				}
				goto IL_022A;
			}
			this._testString.Capacity = this._testString.Length;
		}

		/// <summary>Gets a value indicating whether the prompt character should be treated as a valid input character or not.</summary>
		/// <returns>true if the user can enter <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" /> into the control; otherwise, false. The default is true. </returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06003838 RID: 14392 RVA: 0x000C49AD File Offset: 0x000C2BAD
		public bool AllowPromptAsInput
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_ALLOW_PROMPT_AS_INPUT];
			}
		}

		/// <summary>Gets the number of editable character positions that have already been successfully assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions in the input mask that have already been assigned a character value in the formatted string.</returns>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x000C49BF File Offset: 0x000C2BBF
		// (set) Token: 0x0600383A RID: 14394 RVA: 0x000C49C7 File Offset: 0x000C2BC7
		public int AssignedEditPositionCount { get; private set; }

		/// <summary>Gets the number of editable character positions in the input mask that have not yet been assigned an input value.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable character positions that not yet been assigned a character value.</returns>
		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		public int AvailableEditPositionCount
		{
			get
			{
				return this.EditPositionCount - this.AssignedEditPositionCount;
			}
		}

		/// <summary>Creates a copy of the current <see cref="T:System.ComponentModel.MaskedTextProvider" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.MaskedTextProvider" /> object this method creates, cast as an object.</returns>
		// Token: 0x0600383C RID: 14396 RVA: 0x000C49E0 File Offset: 0x000C2BE0
		public object Clone()
		{
			Type type = base.GetType();
			MaskedTextProvider maskedTextProvider;
			if (type == MaskedTextProvider.s_maskTextProviderType)
			{
				maskedTextProvider = new MaskedTextProvider(this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly);
			}
			else
			{
				object[] array = new object[] { this.Mask, this.Culture, this.AllowPromptAsInput, this.PromptChar, this.PasswordChar, this.AsciiOnly };
				maskedTextProvider = SecurityUtils.SecureCreateInstance(type, array) as MaskedTextProvider;
			}
			maskedTextProvider.ResetOnPrompt = false;
			maskedTextProvider.ResetOnSpace = false;
			maskedTextProvider.SkipLiterals = false;
			for (int i = 0; i < this._testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[i];
				if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
				{
					maskedTextProvider.Replace(this._testString[i], i);
				}
			}
			maskedTextProvider.ResetOnPrompt = this.ResetOnPrompt;
			maskedTextProvider.ResetOnSpace = this.ResetOnSpace;
			maskedTextProvider.SkipLiterals = this.SkipLiterals;
			maskedTextProvider.IncludeLiterals = this.IncludeLiterals;
			maskedTextProvider.IncludePrompt = this.IncludePrompt;
			return maskedTextProvider;
		}

		/// <summary>Gets the culture that determines the value of the localizable separators and placeholders in the input mask.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> containing the culture information associated with the input mask.</returns>
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000C4B27 File Offset: 0x000C2D27
		public CultureInfo Culture { get; }

		/// <summary>Gets the default password character used obscure user input. </summary>
		/// <returns>A <see cref="T:System.Char" /> that represents the default password character.</returns>
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x000C4B2F File Offset: 0x000C2D2F
		public static char DefaultPasswordChar
		{
			get
			{
				return '*';
			}
		}

		/// <summary>Gets the number of editable positions in the formatted string.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of editable positions in the formatted string.</returns>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000C4B33 File Offset: 0x000C2D33
		public int EditPositionCount
		{
			get
			{
				return this._optionalEditChars + this._requiredEditChars;
			}
		}

		/// <summary>Gets a newly created enumerator for the editable positions in the formatted string. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that supports enumeration over the editable positions in the formatted string.</returns>
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06003840 RID: 14400 RVA: 0x000C4B44 File Offset: 0x000C2D44
		public IEnumerator EditPositions
		{
			get
			{
				List<int> list = new List<int>();
				int num = 0;
				using (List<MaskedTextProvider.CharDescriptor>.Enumerator enumerator = this._stringDescriptor.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (MaskedTextProvider.IsEditPosition(enumerator.Current))
						{
							list.Add(num);
						}
						num++;
					}
				}
				return ((IEnumerable)list).GetEnumerator();
			}
		}

		/// <summary>Gets or sets a value that indicates whether literal characters in the input mask should be included in the formatted string.</summary>
		/// <returns>true if literals are included; otherwise, false. The default is true. </returns>
		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000C4BB0 File Offset: 0x000C2DB0
		// (set) Token: 0x06003842 RID: 14402 RVA: 0x000C4BC2 File Offset: 0x000C2DC2
		public bool IncludeLiterals
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_INCLUDE_LITERALS];
			}
			set
			{
				this._flagState[MaskedTextProvider.s_INCLUDE_LITERALS] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="P:System.Windows.Forms.MaskedTextBox.PromptChar" /> is used to represent the absence of user input when displaying the formatted string. </summary>
		/// <returns>true if the prompt character is used to represent the positions where no user input was provided; otherwise, false. The default is true.</returns>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000C4BD5 File Offset: 0x000C2DD5
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x000C4BE7 File Offset: 0x000C2DE7
		public bool IncludePrompt
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_INCLUDE_PROMPT];
			}
			set
			{
				this._flagState[MaskedTextProvider.s_INCLUDE_PROMPT] = value;
			}
		}

		/// <summary>Gets a value indicating whether the mask accepts characters outside of the ASCII character set.</summary>
		/// <returns>true if only ASCII is accepted; false if <see cref="T:System.ComponentModel.MaskedTextProvider" /> can accept any arbitrary Unicode character. The default is false.</returns>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000C4BFA File Offset: 0x000C2DFA
		public bool AsciiOnly
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_ASCII_ONLY];
			}
		}

		/// <summary>Gets or sets a value that determines whether password protection should be applied to the formatted string.</summary>
		/// <returns>true if the input string is to be treated as a password string; otherwise, false. The default is false.</returns>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06003846 RID: 14406 RVA: 0x000C4C0C File Offset: 0x000C2E0C
		// (set) Token: 0x06003847 RID: 14407 RVA: 0x000C4C17 File Offset: 0x000C2E17
		public bool IsPassword
		{
			get
			{
				return this._passwordChar > '\0';
			}
			set
			{
				if (this.IsPassword != value)
				{
					this._passwordChar = (value ? MaskedTextProvider.DefaultPasswordChar : '\0');
				}
			}
		}

		/// <summary>Gets the upper bound of the range of invalid indexes.</summary>
		/// <returns>A value representing the largest invalid index, as determined by the provider implementation. For example, if the lowest valid index is 0, this property will return -1.</returns>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003848 RID: 14408 RVA: 0x00052148 File Offset: 0x00050348
		public static int InvalidIndex
		{
			get
			{
				return -1;
			}
		}

		/// <summary>Gets the index in the mask of the rightmost input character that has been assigned to the mask.</summary>
		/// <returns>If at least one input character has been assigned to the mask, an <see cref="T:System.Int32" /> containing the index of rightmost assigned position; otherwise, if no position has been assigned, <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003849 RID: 14409 RVA: 0x000C4C33 File Offset: 0x000C2E33
		public int LastAssignedPosition
		{
			get
			{
				return this.FindAssignedEditPositionFrom(this._testString.Length - 1, false);
			}
		}

		/// <summary>Gets the length of the mask, absent any mask modifier characters.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of positions in the mask, excluding characters that modify mask input. </returns>
		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600384A RID: 14410 RVA: 0x000C4C49 File Offset: 0x000C2E49
		public int Length
		{
			get
			{
				return this._testString.Length;
			}
		}

		/// <summary>Gets the input mask.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the full mask.</returns>
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x000C4C56 File Offset: 0x000C2E56
		public string Mask { get; }

		/// <summary>Gets a value indicating whether all required inputs have been entered into the formatted string.</summary>
		/// <returns>true if all required input has been entered into the mask; otherwise, false.</returns>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x000C4C5E File Offset: 0x000C2E5E
		public bool MaskCompleted
		{
			get
			{
				return this._requiredCharCount == this._requiredEditChars;
			}
		}

		/// <summary>Gets a value indicating whether all required and optional inputs have been entered into the formatted string. </summary>
		/// <returns>true if all required and optional inputs have been entered; otherwise, false. </returns>
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000C4C6E File Offset: 0x000C2E6E
		public bool MaskFull
		{
			get
			{
				return this.AssignedEditPositionCount == this.EditPositionCount;
			}
		}

		/// <summary>Gets or sets the character to be substituted for the actual input characters.</summary>
		/// <returns>The <see cref="T:System.Char" /> value used as the password character.</returns>
		/// <exception cref="T:System.InvalidOperationException">The password character specified when setting this property is the same as the current prompt character, <see cref="P:System.ComponentModel.MaskedTextProvider.PromptChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000C4C7E File Offset: 0x000C2E7E
		// (set) Token: 0x0600384F RID: 14415 RVA: 0x000C4C86 File Offset: 0x000C2E86
		public char PasswordChar
		{
			get
			{
				return this._passwordChar;
			}
			set
			{
				if (value == this._promptChar)
				{
					throw new InvalidOperationException("The PasswordChar and PromptChar values cannot be the same.");
				}
				if (!MaskedTextProvider.IsValidPasswordChar(value) && value != '\0')
				{
					throw new ArgumentException("The specified character value is not allowed for this property.");
				}
				if (value != this._passwordChar)
				{
					this._passwordChar = value;
				}
			}
		}

		/// <summary>Gets or sets the character used to represent the absence of user input for all available edit positions.</summary>
		/// <returns>The character used to prompt the user for input. The default is an underscore (_). </returns>
		/// <exception cref="T:System.InvalidOperationException">The prompt character specified when setting this property is the same as the current password character, <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" />. The two are required to be different.</exception>
		/// <exception cref="T:System.ArgumentException">The character specified when setting this property is not a valid password character, as determined by the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidPasswordChar(System.Char)" /> method.</exception>
		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000C4CC2 File Offset: 0x000C2EC2
		// (set) Token: 0x06003851 RID: 14417 RVA: 0x000C4CCC File Offset: 0x000C2ECC
		public char PromptChar
		{
			get
			{
				return this._promptChar;
			}
			set
			{
				if (value == this._passwordChar)
				{
					throw new InvalidOperationException("The PasswordChar and PromptChar values cannot be the same.");
				}
				if (!MaskedTextProvider.IsPrintableChar(value))
				{
					throw new ArgumentException("The specified character value is not allowed for this property.");
				}
				if (value != this._promptChar)
				{
					this._promptChar = value;
					for (int i = 0; i < this._testString.Length; i++)
					{
						MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[i];
						if (this.IsEditPosition(i) && !charDescriptor.IsAssigned)
						{
							this._testString[i] = this._promptChar;
						}
					}
				}
			}
		}

		/// <summary>Gets or sets a value that determines how an input character that matches the prompt character should be handled.</summary>
		/// <returns>true if the prompt character entered as input causes the current editable position in the mask to be reset; otherwise, false to indicate that the prompt character is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000C4D56 File Offset: 0x000C2F56
		// (set) Token: 0x06003853 RID: 14419 RVA: 0x000C4D68 File Offset: 0x000C2F68
		public bool ResetOnPrompt
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_RESET_ON_PROMPT];
			}
			set
			{
				this._flagState[MaskedTextProvider.s_RESET_ON_PROMPT] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how a space input character should be handled.</summary>
		/// <returns>true if the space input character causes the current editable position in the mask to be reset; otherwise, false to indicate that it is to be processed as a normal input character. The default is true.</returns>
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000C4D7B File Offset: 0x000C2F7B
		// (set) Token: 0x06003855 RID: 14421 RVA: 0x000C4D8D File Offset: 0x000C2F8D
		public bool ResetOnSpace
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_SKIP_SPACE];
			}
			set
			{
				this._flagState[MaskedTextProvider.s_SKIP_SPACE] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether literal character positions in the mask can be overwritten by their same values.</summary>
		/// <returns>true to allow literals to be added back; otherwise, false to not allow the user to overwrite literal characters. The default is true.</returns>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000C4DA0 File Offset: 0x000C2FA0
		// (set) Token: 0x06003857 RID: 14423 RVA: 0x000C4DB2 File Offset: 0x000C2FB2
		public bool SkipLiterals
		{
			get
			{
				return this._flagState[MaskedTextProvider.s_RESET_ON_LITERALS];
			}
			set
			{
				this._flagState[MaskedTextProvider.s_RESET_ON_LITERALS] = value;
			}
		}

		/// <summary>Gets the element at the specified position in the formatted string.</summary>
		/// <returns>The <see cref="T:System.Char" /> at the specified position in the formatted string.</returns>
		/// <param name="index">A zero-based index of the element to retrieve. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than or equal to the <see cref="P:System.ComponentModel.MaskedTextProvider.Length" /> of the mask.</exception>
		// Token: 0x17000D11 RID: 3345
		public char this[int index]
		{
			get
			{
				if (index < 0 || index >= this._testString.Length)
				{
					throw new IndexOutOfRangeException(index.ToString(CultureInfo.CurrentCulture));
				}
				return this._testString[index];
			}
		}

		/// <summary>Adds the specified input character to the end of the formatted string.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string. </param>
		// Token: 0x06003859 RID: 14425 RVA: 0x000C4DF8 File Offset: 0x000C2FF8
		public bool Add(char input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the specified input character to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if the input character was added successfully; otherwise false.</returns>
		/// <param name="input">A <see cref="T:System.Char" /> value to be appended to the formatted string.</param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600385A RID: 14426 RVA: 0x000C4E10 File Offset: 0x000C3010
		public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == this._testString.Length - 1)
			{
				testPosition = this._testString.Length;
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				return false;
			}
			testPosition = lastAssignedPosition + 1;
			testPosition = this.FindEditPositionFrom(testPosition, true);
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this._testString.Length;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x0600385B RID: 14427 RVA: 0x000C4E80 File Offset: 0x000C3080
		public bool Add(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Add(input, out num, out maskedTextResultHint);
		}

		/// <summary>Adds the characters in the specified input string to the end of the formatted string, and then outputs position and descriptive information.</summary>
		/// <returns>true if all the characters from the input string were added successfully; otherwise false to indicate that no characters were added.</returns>
		/// <param name="input">A <see cref="T:System.String" /> containing character values to be appended to the formatted string. </param>
		/// <param name="testPosition">The zero-based position in the formatted string where the attempt was made to add the character. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600385C RID: 14428 RVA: 0x000C4E98 File Offset: 0x000C3098
		public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			testPosition = this.LastAssignedPosition + 1;
			if (input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestSetString(input, testPosition, out testPosition, out resultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters.</summary>
		// Token: 0x0600385D RID: 14429 RVA: 0x000C4ECC File Offset: 0x000C30CC
		public void Clear()
		{
			MaskedTextResultHint maskedTextResultHint;
			this.Clear(out maskedTextResultHint);
		}

		/// <summary>Clears all the editable input characters from the formatted string, replacing them with prompt characters, and then outputs descriptive information.</summary>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter. </param>
		// Token: 0x0600385E RID: 14430 RVA: 0x000C4EE4 File Offset: 0x000C30E4
		public void Clear(out MaskedTextResultHint resultHint)
		{
			if (this.AssignedEditPositionCount == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return;
			}
			resultHint = MaskedTextResultHint.Success;
			for (int i = 0; i < this._testString.Length; i++)
			{
				this.ResetChar(i);
			}
		}

		/// <summary>Returns the position of the first assigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x0600385F RID: 14431 RVA: 0x000C4F20 File Offset: 0x000C3120
		public int FindAssignedEditPositionFrom(int position, bool direction)
		{
			if (this.AssignedEditPositionCount == 0)
			{
				return -1;
			}
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this._testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindAssignedEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first assigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first assigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003860 RID: 14432 RVA: 0x000C4F59 File Offset: 0x000C3159
		public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			if (this.AssignedEditPositionCount == 0)
			{
				return -1;
			}
			return this.FindEditPositionInRange(startPosition, endPosition, direction, 2);
		}

		/// <summary>Returns the position of the first editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003861 RID: 14433 RVA: 0x000C4F70 File Offset: 0x000C3170
		public int FindEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this._testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003862 RID: 14434 RVA: 0x000C4FA0 File Offset: 0x000C31A0
		public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.EditOptional | MaskedTextProvider.CharType.EditRequired;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000C4FBC File Offset: 0x000C31BC
		private int FindEditPositionInRange(int startPosition, int endPosition, bool direction, byte assignedStatus)
		{
			int num;
			for (;;)
			{
				num = this.FindEditPositionInRange(startPosition, endPosition, direction);
				if (num == -1)
				{
					return -1;
				}
				MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[num];
				if (assignedStatus != 1)
				{
					if (assignedStatus != 2)
					{
						break;
					}
					if (charDescriptor.IsAssigned)
					{
						return num;
					}
				}
				else if (!charDescriptor.IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
				if (startPosition > endPosition)
				{
					return -1;
				}
			}
			return num;
		}

		/// <summary>Returns the position of the first non-editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003864 RID: 14436 RVA: 0x000C501C File Offset: 0x000C321C
		public int FindNonEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this._testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindNonEditPositionInRange(num, num2, direction);
		}

		/// <summary>Returns the position of the first non-editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first literal position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003865 RID: 14437 RVA: 0x000C504C File Offset: 0x000C324C
		public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			MaskedTextProvider.CharType charType = MaskedTextProvider.CharType.Separator | MaskedTextProvider.CharType.Literal;
			return this.FindPositionInRange(startPosition, endPosition, direction, charType);
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000C5068 File Offset: 0x000C3268
		private int FindPositionInRange(int startPosition, int endPosition, bool direction, MaskedTextProvider.CharType charTypeFlags)
		{
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (endPosition >= this._testString.Length)
			{
				endPosition = this._testString.Length - 1;
			}
			if (startPosition > endPosition)
			{
				return -1;
			}
			while (startPosition <= endPosition)
			{
				int num;
				if (!direction)
				{
					endPosition = (num = endPosition) - 1;
				}
				else
				{
					startPosition = (num = startPosition) + 1;
				}
				int num2 = num;
				MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[num2];
				if ((charDescriptor.CharType & charTypeFlags) == charDescriptor.CharType)
				{
					return num2;
				}
			}
			return -1;
		}

		/// <summary>Returns the position of the first unassigned editable position after the specified position using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="position">The zero-based position in the formatted string to start the search.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003867 RID: 14439 RVA: 0x000C50D8 File Offset: 0x000C32D8
		public int FindUnassignedEditPositionFrom(int position, bool direction)
		{
			int num;
			int num2;
			if (direction)
			{
				num = position;
				num2 = this._testString.Length - 1;
			}
			else
			{
				num = 0;
				num2 = position;
			}
			return this.FindEditPositionInRange(num, num2, direction, 1);
		}

		/// <summary>Returns the position of the first unassigned editable position between the specified positions using the specified search direction.</summary>
		/// <returns>If successful, an <see cref="T:System.Int32" /> representing the zero-based position of the first unassigned editable position encountered; otherwise <see cref="P:System.ComponentModel.MaskedTextProvider.InvalidIndex" />.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the search starts.</param>
		/// <param name="endPosition">The zero-based position in the formatted string where the search ends.</param>
		/// <param name="direction">A <see cref="T:System.Boolean" /> indicating the search direction; either true to search forward or false to search backward.</param>
		// Token: 0x06003868 RID: 14440 RVA: 0x000C5108 File Offset: 0x000C3308
		public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
		{
			for (;;)
			{
				int num = this.FindEditPositionInRange(startPosition, endPosition, direction, 0);
				if (num == -1)
				{
					break;
				}
				if (!this._stringDescriptor[num].IsAssigned)
				{
					return num;
				}
				if (direction)
				{
					startPosition++;
				}
				else
				{
					endPosition--;
				}
			}
			return -1;
		}

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> denotes success or failure.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value represents a success; otherwise, false if it represents failure.</returns>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> value typically obtained as an output parameter from a previous operation. </param>
		// Token: 0x06003869 RID: 14441 RVA: 0x000C514B File Offset: 0x000C334B
		public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
		{
			return hint > MaskedTextResultHint.Unknown;
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		// Token: 0x0600386A RID: 14442 RVA: 0x000C5151 File Offset: 0x000C3351
		public bool InsertAt(char input, int position)
		{
			return position >= 0 && position < this._testString.Length && this.InsertAt(input.ToString(), position);
		}

		/// <summary>Inserts the specified character at the specified position within the formatted string, returning the last insertion position and the status of the operation.</summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the character.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		// Token: 0x0600386B RID: 14443 RVA: 0x000C5175 File Offset: 0x000C3375
		public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			return this.InsertAt(input.ToString(), position, out testPosition, out resultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x0600386C RID: 14444 RVA: 0x000C5188 File Offset: 0x000C3388
		public bool InsertAt(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.InsertAt(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Inserts the specified string at a specified position within the formatted string, returning the last insertion position and the status of the operation. </summary>
		/// <returns>true if the insertion was successful; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> to be inserted. </param>
		/// <param name="position">The zero-based position in the formatted string to insert the input string.</param>
		/// <param name="testPosition">If the method is successful, the last position where a character was inserted; otherwise, the first position where the insertion failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the insertion operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x0600386D RID: 14445 RVA: 0x000C51A1 File Offset: 0x000C33A1
		public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this._testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.InsertAtInt(input, position, out testPosition, out resultHint, false);
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000C51DC File Offset: 0x000C33DC
		private bool InsertAtInt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			if (input.Length == 0)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			if (!this.TestString(input, position, out testPosition, out resultHint))
			{
				return false;
			}
			int i = this.FindEditPositionFrom(position, true);
			bool flag = this.FindAssignedEditPositionInRange(i, testPosition, true) != -1;
			int lastAssignedPosition = this.LastAssignedPosition;
			if (flag && testPosition == this._testString.Length - 1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this._testString.Length;
				return false;
			}
			int num = this.FindEditPositionFrom(testPosition + 1, true);
			if (flag)
			{
				MaskedTextResultHint maskedTextResultHint = MaskedTextResultHint.Unknown;
				while (num != -1)
				{
					if (this._stringDescriptor[i].IsAssigned && !this.TestChar(this._testString[i], num, out maskedTextResultHint))
					{
						resultHint = maskedTextResultHint;
						testPosition = num;
						return false;
					}
					if (i != lastAssignedPosition)
					{
						i = this.FindEditPositionFrom(i + 1, true);
						num = this.FindEditPositionFrom(num + 1, true);
					}
					else
					{
						if (maskedTextResultHint > resultHint)
						{
							resultHint = maskedTextResultHint;
							goto IL_00EF;
						}
						goto IL_00EF;
					}
				}
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = this._testString.Length;
				return false;
			}
			IL_00EF:
			if (testOnly)
			{
				return true;
			}
			if (flag)
			{
				while (i >= position)
				{
					if (this._stringDescriptor[i].IsAssigned)
					{
						this.SetChar(this._testString[i], num);
					}
					else
					{
						this.ResetChar(num);
					}
					num = this.FindEditPositionFrom(num - 1, false);
					i = this.FindEditPositionFrom(i - 1, false);
				}
			}
			this.SetString(input, position);
			return true;
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000C5335 File Offset: 0x000C3535
		private static bool IsAscii(char c)
		{
			return c >= '!' && c <= '~';
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000C5346 File Offset: 0x000C3546
		private static bool IsAciiAlphanumeric(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000C536D File Offset: 0x000C356D
		private static bool IsAlphanumeric(char c)
		{
			return char.IsLetter(c) || char.IsDigit(c);
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000C537F File Offset: 0x000C357F
		private static bool IsAsciiLetter(char c)
		{
			return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
		}

		/// <summary>Determines whether the specified position is available for assignment.</summary>
		/// <returns>true if the specified position in the formatted string is editable and has not been assigned to yet; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06003873 RID: 14451 RVA: 0x000C539C File Offset: 0x000C359C
		public bool IsAvailablePosition(int position)
		{
			if (position < 0 || position >= this._testString.Length)
			{
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[position];
			return MaskedTextProvider.IsEditPosition(charDescriptor) && !charDescriptor.IsAssigned;
		}

		/// <summary>Determines whether the specified position is editable. </summary>
		/// <returns>true if the specified position in the formatted string is editable; otherwise false.</returns>
		/// <param name="position">The zero-based position in the mask to test.</param>
		// Token: 0x06003874 RID: 14452 RVA: 0x000C53DD File Offset: 0x000C35DD
		public bool IsEditPosition(int position)
		{
			return position >= 0 && position < this._testString.Length && MaskedTextProvider.IsEditPosition(this._stringDescriptor[position]);
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000C5404 File Offset: 0x000C3604
		private static bool IsEditPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired || charDescriptor.CharType == MaskedTextProvider.CharType.EditOptional;
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000C541A File Offset: 0x000C361A
		private static bool IsLiteralPosition(MaskedTextProvider.CharDescriptor charDescriptor)
		{
			return charDescriptor.CharType == MaskedTextProvider.CharType.Literal || charDescriptor.CharType == MaskedTextProvider.CharType.Separator;
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000C5430 File Offset: 0x000C3630
		private static bool IsPrintableChar(char c)
		{
			return char.IsLetterOrDigit(c) || char.IsPunctuation(c) || char.IsSymbol(c) || c == ' ';
		}

		/// <summary>Determines whether the specified character is a valid input character.</summary>
		/// <returns>true if the specified character contains a valid input value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06003878 RID: 14456 RVA: 0x000C5451 File Offset: 0x000C3651
		public static bool IsValidInputChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid mask character.</summary>
		/// <returns>true if the specified character contains a valid mask value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x06003879 RID: 14457 RVA: 0x000C5451 File Offset: 0x000C3651
		public static bool IsValidMaskChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c);
		}

		/// <summary>Determines whether the specified character is a valid password character.</summary>
		/// <returns>true if the specified character contains a valid password value; otherwise false.</returns>
		/// <param name="c">The <see cref="T:System.Char" /> value to test.</param>
		// Token: 0x0600387A RID: 14458 RVA: 0x000C5459 File Offset: 0x000C3659
		public static bool IsValidPasswordChar(char c)
		{
			return MaskedTextProvider.IsPrintableChar(c) || c == '\0';
		}

		/// <summary>Removes the last assigned character from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		// Token: 0x0600387B RID: 14459 RVA: 0x000C546C File Offset: 0x000C366C
		public bool Remove()
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Remove(out num, out maskedTextResultHint);
		}

		/// <summary>Removes the last assigned character from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="testPosition">The zero-based position in the formatted string where the character was actually removed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600387C RID: 14460 RVA: 0x000C5484 File Offset: 0x000C3684
		public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			if (lastAssignedPosition == -1)
			{
				testPosition = 0;
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			this.ResetChar(lastAssignedPosition);
			testPosition = lastAssignedPosition;
			resultHint = MaskedTextResultHint.Success;
			return true;
		}

		/// <summary>Removes the assigned character at the specified position from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="position">The zero-based position of the assigned character to remove.</param>
		// Token: 0x0600387D RID: 14461 RVA: 0x000C54B2 File Offset: 0x000C36B2
		public bool RemoveAt(int position)
		{
			return this.RemoveAt(position, position);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		// Token: 0x0600387E RID: 14462 RVA: 0x000C54BC File Offset: 0x000C36BC
		public bool RemoveAt(int startPosition, int endPosition)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.RemoveAt(startPosition, endPosition, out num, out maskedTextResultHint);
		}

		/// <summary>Removes the assigned characters between the specified positions from the formatted string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully removed; otherwise, false.</returns>
		/// <param name="startPosition">The zero-based index of the first assigned character to remove.</param>
		/// <param name="endPosition">The zero-based index of the last assigned character to remove.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string of where the characters were actually removed; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600387F RID: 14463 RVA: 0x000C54D5 File Offset: 0x000C36D5
		public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this._testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.RemoveAtInt(startPosition, endPosition, out testPosition, out resultHint, false);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000C5510 File Offset: 0x000C3710
		private bool RemoveAtInt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint, bool testOnly)
		{
			int lastAssignedPosition = this.LastAssignedPosition;
			int num = this.FindEditPositionInRange(startPosition, endPosition, true);
			resultHint = MaskedTextResultHint.NoEffect;
			if (num == -1 || num > lastAssignedPosition)
			{
				testPosition = startPosition;
				return true;
			}
			testPosition = startPosition;
			bool flag = endPosition < lastAssignedPosition;
			if (this.FindAssignedEditPositionInRange(startPosition, endPosition, true) != -1)
			{
				resultHint = MaskedTextResultHint.Success;
			}
			if (flag)
			{
				int num2 = this.FindEditPositionFrom(endPosition + 1, true);
				int num3 = num2;
				startPosition = num;
				MaskedTextResultHint maskedTextResultHint;
				for (;;)
				{
					char c = this._testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[num2];
					if ((c != this.PromptChar || charDescriptor.IsAssigned) && !this.TestChar(c, num, out maskedTextResultHint))
					{
						break;
					}
					if (num2 == lastAssignedPosition)
					{
						goto IL_00B0;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				resultHint = maskedTextResultHint;
				testPosition = num;
				return false;
				IL_00B0:
				if (MaskedTextResultHint.SideEffect > resultHint)
				{
					resultHint = MaskedTextResultHint.SideEffect;
				}
				if (testOnly)
				{
					return true;
				}
				num2 = num3;
				num = startPosition;
				for (;;)
				{
					char c2 = this._testString[num2];
					MaskedTextProvider.CharDescriptor charDescriptor2 = this._stringDescriptor[num2];
					if (c2 == this.PromptChar && !charDescriptor2.IsAssigned)
					{
						this.ResetChar(num);
					}
					else
					{
						this.SetChar(c2, num);
						this.ResetChar(num2);
					}
					if (num2 == lastAssignedPosition)
					{
						break;
					}
					num2 = this.FindEditPositionFrom(num2 + 1, true);
					num = this.FindEditPositionFrom(num + 1, true);
				}
				startPosition = num + 1;
			}
			if (startPosition <= endPosition)
			{
				this.ResetString(startPosition, endPosition);
			}
			return true;
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		// Token: 0x06003881 RID: 14465 RVA: 0x000C5658 File Offset: 0x000C3858
		public bool Replace(char input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a single character at or beyond the specified position with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06003882 RID: 14466 RVA: 0x000C5674 File Offset: 0x000C3874
		public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (position < 0 || position >= this._testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			testPosition = position;
			if (!this.TestEscapeChar(input, testPosition))
			{
				testPosition = this.FindEditPositionFrom(testPosition, true);
			}
			if (testPosition == -1)
			{
				resultHint = MaskedTextResultHint.UnavailableEditPosition;
				testPosition = position;
				return false;
			}
			return this.TestSetChar(input, testPosition, out resultHint);
		}

		/// <summary>Replaces a single character between the specified starting and ending positions with the specified character value, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if the character was successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value that replaces the existing value.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06003883 RID: 14467 RVA: 0x000C56D8 File Offset: 0x000C38D8
		public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (endPosition >= this._testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition == endPosition)
			{
				testPosition = startPosition;
				return this.TestSetChar(input, startPosition, out resultHint);
			}
			return this.Replace(input.ToString(), startPosition, endPosition, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06003884 RID: 14468 RVA: 0x000C5738 File Offset: 0x000C3938
		public bool Replace(string input, int position)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Replace(input, position, out num, out maskedTextResultHint);
		}

		/// <summary>Replaces a range of editable characters starting at the specified position with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="position">The zero-based position to search for the first editable character to replace.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06003885 RID: 14469 RVA: 0x000C5754 File Offset: 0x000C3954
		public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (position < 0 || position >= this._testString.Length)
			{
				testPosition = position;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(position, position, out testPosition, out resultHint);
			}
			return this.TestSetString(input, position, out testPosition, out resultHint);
		}

		/// <summary>Replaces a range of editable characters between the specified starting and ending positions with the specified string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully replaced; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to replace the existing editable characters.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the replacement starts. </param>
		/// <param name="endPosition">The zero-based position in the formatted string where the replacement ends. </param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually replaced; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the replacement operation. An output parameter.</param>
		// Token: 0x06003886 RID: 14470 RVA: 0x000C57B0 File Offset: 0x000C39B0
		public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (endPosition >= this._testString.Length)
			{
				testPosition = endPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (startPosition < 0 || startPosition > endPosition)
			{
				testPosition = startPosition;
				resultHint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			if (input.Length == 0)
			{
				return this.RemoveAt(startPosition, endPosition, out testPosition, out resultHint);
			}
			if (!this.TestString(input, startPosition, out testPosition, out resultHint))
			{
				return false;
			}
			if (this.AssignedEditPositionCount > 0)
			{
				if (testPosition < endPosition)
				{
					int num;
					MaskedTextResultHint maskedTextResultHint;
					if (!this.RemoveAtInt(testPosition + 1, endPosition, out num, out maskedTextResultHint, false))
					{
						testPosition = num;
						resultHint = maskedTextResultHint;
						return false;
					}
					if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
				}
				else if (testPosition > endPosition)
				{
					int lastAssignedPosition = this.LastAssignedPosition;
					int i = testPosition + 1;
					int num2 = endPosition + 1;
					MaskedTextResultHint maskedTextResultHint;
					for (;;)
					{
						num2 = this.FindEditPositionFrom(num2, true);
						i = this.FindEditPositionFrom(i, true);
						if (i == -1)
						{
							goto Block_12;
						}
						if (!this.TestChar(this._testString[num2], i, out maskedTextResultHint))
						{
							goto Block_13;
						}
						if (maskedTextResultHint == MaskedTextResultHint.Success && resultHint != maskedTextResultHint)
						{
							resultHint = MaskedTextResultHint.Success;
						}
						if (num2 == lastAssignedPosition)
						{
							break;
						}
						num2++;
						i++;
					}
					while (i > testPosition)
					{
						this.SetChar(this._testString[num2], i);
						num2 = this.FindEditPositionFrom(num2 - 1, false);
						i = this.FindEditPositionFrom(i - 1, false);
					}
					goto IL_0162;
					Block_12:
					testPosition = this._testString.Length;
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
					Block_13:
					testPosition = i;
					resultHint = maskedTextResultHint;
					return false;
				}
			}
			IL_0162:
			this.SetString(input, startPosition);
			return true;
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000C5928 File Offset: 0x000C3B28
		private void ResetChar(int testPosition)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[testPosition];
			if (this.IsEditPosition(testPosition) && charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = false;
				this._testString[testPosition] = this._promptChar;
				int assignedEditPositionCount = this.AssignedEditPositionCount;
				this.AssignedEditPositionCount = assignedEditPositionCount - 1;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this._requiredCharCount--;
				}
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000C5993 File Offset: 0x000C3B93
		private void ResetString(int startPosition, int endPosition)
		{
			startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
			if (startPosition != -1)
			{
				endPosition = this.FindAssignedEditPositionFrom(endPosition, false);
				while (startPosition <= endPosition)
				{
					startPosition = this.FindAssignedEditPositionFrom(startPosition, true);
					this.ResetChar(startPosition);
					startPosition++;
				}
			}
		}

		/// <summary>Sets the formatted string to the specified input string.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x06003889 RID: 14473 RVA: 0x000C59CC File Offset: 0x000C3BCC
		public bool Set(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.Set(input, out num, out maskedTextResultHint);
		}

		/// <summary>Sets the formatted string to the specified input string, and then outputs the removal position and descriptive information.</summary>
		/// <returns>true if all the characters were successfully set; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value used to set the formatted string.</param>
		/// <param name="testPosition">If successful, the zero-based position in the formatted string where the last character was actually set; otherwise, the first position where the operation failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the set operation. An output parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">The<paramref name=" input" /> parameter is null.</exception>
		// Token: 0x0600388A RID: 14474 RVA: 0x000C59E4 File Offset: 0x000C3BE4
		public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = 0;
			if (input.Length == 0)
			{
				this.Clear(out resultHint);
				return true;
			}
			if (!this.TestSetString(input, testPosition, out testPosition, out resultHint))
			{
				return false;
			}
			int num = this.FindAssignedEditPositionFrom(testPosition + 1, true);
			if (num != -1)
			{
				this.ResetString(num, this._testString.Length - 1);
			}
			return true;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000C5A4C File Offset: 0x000C3C4C
		private void SetChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[position];
			this.SetChar(input, position, charDescriptor);
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000C5A70 File Offset: 0x000C3C70
		private void SetChar(char input, int position, MaskedTextProvider.CharDescriptor charDescriptor)
		{
			MaskedTextProvider.CharDescriptor charDescriptor2 = this._stringDescriptor[position];
			if (this.TestEscapeChar(input, position, charDescriptor))
			{
				this.ResetChar(position);
				return;
			}
			if (char.IsLetter(input))
			{
				if (char.IsUpper(input))
				{
					if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToLower)
					{
						input = this.Culture.TextInfo.ToLower(input);
					}
				}
				else if (charDescriptor.CaseConversion == MaskedTextProvider.CaseConversion.ToUpper)
				{
					input = this.Culture.TextInfo.ToUpper(input);
				}
			}
			this._testString[position] = input;
			if (!charDescriptor.IsAssigned)
			{
				charDescriptor.IsAssigned = true;
				int assignedEditPositionCount = this.AssignedEditPositionCount;
				this.AssignedEditPositionCount = assignedEditPositionCount + 1;
				if (charDescriptor.CharType == MaskedTextProvider.CharType.EditRequired)
				{
					this._requiredCharCount++;
				}
			}
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000C5B2C File Offset: 0x000C3D2C
		private void SetString(string input, int testPosition)
		{
			foreach (char c in input)
			{
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
				}
				this.SetChar(c, testPosition);
				testPosition++;
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000C5B78 File Offset: 0x000C3D78
		private bool TestChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (!MaskedTextProvider.IsPrintableChar(input))
			{
				resultHint = MaskedTextResultHint.InvalidInput;
				return false;
			}
			MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[position];
			if (MaskedTextProvider.IsLiteralPosition(charDescriptor))
			{
				if (this.SkipLiterals && input == this._testString[position])
				{
					resultHint = MaskedTextResultHint.CharacterEscaped;
					return true;
				}
				resultHint = MaskedTextResultHint.NonEditPosition;
				return false;
			}
			else
			{
				if (input == this._promptChar)
				{
					if (this.ResetOnPrompt)
					{
						if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
						{
							resultHint = MaskedTextResultHint.SideEffect;
						}
						else
						{
							resultHint = MaskedTextResultHint.CharacterEscaped;
						}
						return true;
					}
					if (!this.AllowPromptAsInput)
					{
						resultHint = MaskedTextResultHint.PromptCharNotAllowed;
						return false;
					}
				}
				if (input == ' ' && this.ResetOnSpace)
				{
					if (MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned)
					{
						resultHint = MaskedTextResultHint.SideEffect;
					}
					else
					{
						resultHint = MaskedTextResultHint.CharacterEscaped;
					}
					return true;
				}
				char c = this.Mask[charDescriptor.MaskPosition];
				if (c <= '0')
				{
					if (c != '#')
					{
						if (c != '&')
						{
							if (c == '0')
							{
								if (!char.IsDigit(input))
								{
									resultHint = MaskedTextResultHint.DigitExpected;
									return false;
								}
							}
						}
						else if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
					else if (!char.IsDigit(input) && input != '-' && input != '+' && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c <= 'C')
				{
					if (c != '9')
					{
						switch (c)
						{
						case '?':
							if (!char.IsLetter(input) && input != ' ')
							{
								resultHint = MaskedTextResultHint.LetterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'A':
							if (!MaskedTextProvider.IsAlphanumeric(input))
							{
								resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
								return false;
							}
							if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						case 'C':
							if (!MaskedTextProvider.IsAscii(input) && this.AsciiOnly && input != ' ')
							{
								resultHint = MaskedTextResultHint.AsciiCharacterExpected;
								return false;
							}
							break;
						}
					}
					else if (!char.IsDigit(input) && input != ' ')
					{
						resultHint = MaskedTextResultHint.DigitExpected;
						return false;
					}
				}
				else if (c != 'L')
				{
					if (c == 'a')
					{
						if (!MaskedTextProvider.IsAlphanumeric(input) && input != ' ')
						{
							resultHint = MaskedTextResultHint.AlphanumericCharacterExpected;
							return false;
						}
						if (!MaskedTextProvider.IsAciiAlphanumeric(input) && this.AsciiOnly)
						{
							resultHint = MaskedTextResultHint.AsciiCharacterExpected;
							return false;
						}
					}
				}
				else
				{
					if (!char.IsLetter(input))
					{
						resultHint = MaskedTextResultHint.LetterExpected;
						return false;
					}
					if (!MaskedTextProvider.IsAsciiLetter(input) && this.AsciiOnly)
					{
						resultHint = MaskedTextResultHint.AsciiCharacterExpected;
						return false;
					}
				}
				if (input == this._testString[position] && charDescriptor.IsAssigned)
				{
					resultHint = MaskedTextResultHint.NoEffect;
				}
				else
				{
					resultHint = MaskedTextResultHint.Success;
				}
				return true;
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000C5DD8 File Offset: 0x000C3FD8
		private bool TestEscapeChar(char input, int position)
		{
			MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[position];
			return this.TestEscapeChar(input, position, charDescriptor);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		private bool TestEscapeChar(char input, int position, MaskedTextProvider.CharDescriptor charDex)
		{
			if (MaskedTextProvider.IsLiteralPosition(charDex))
			{
				return this.SkipLiterals && input == this._testString[position];
			}
			return (this.ResetOnPrompt && input == this._promptChar) || (this.ResetOnSpace && input == ' ');
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000C5E4C File Offset: 0x000C404C
		private bool TestSetChar(char input, int position, out MaskedTextResultHint resultHint)
		{
			if (this.TestChar(input, position, out resultHint))
			{
				if (resultHint == MaskedTextResultHint.Success || resultHint == MaskedTextResultHint.SideEffect)
				{
					this.SetChar(input, position);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000C5E6E File Offset: 0x000C406E
		private bool TestSetString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			if (this.TestString(input, position, out testPosition, out resultHint))
			{
				this.SetString(input, position);
				return true;
			}
			return false;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000C5E88 File Offset: 0x000C4088
		private bool TestString(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
		{
			resultHint = MaskedTextResultHint.Unknown;
			testPosition = position;
			if (input.Length == 0)
			{
				return true;
			}
			MaskedTextResultHint maskedTextResultHint = resultHint;
			foreach (char c in input)
			{
				if (testPosition >= this._testString.Length)
				{
					resultHint = MaskedTextResultHint.UnavailableEditPosition;
					return false;
				}
				if (!this.TestEscapeChar(c, testPosition))
				{
					testPosition = this.FindEditPositionFrom(testPosition, true);
					if (testPosition == -1)
					{
						testPosition = this._testString.Length;
						resultHint = MaskedTextResultHint.UnavailableEditPosition;
						return false;
					}
				}
				if (!this.TestChar(c, testPosition, out maskedTextResultHint))
				{
					resultHint = maskedTextResultHint;
					return false;
				}
				if (maskedTextResultHint > resultHint)
				{
					resultHint = maskedTextResultHint;
				}
				testPosition++;
			}
			testPosition--;
			return true;
		}

		/// <summary>Returns the formatted string in a displayable form.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes prompts and mask literals.</returns>
		// Token: 0x06003894 RID: 14484 RVA: 0x000C5F34 File Offset: 0x000C4134
		public string ToDisplayString()
		{
			if (!this.IsPassword || this.AssignedEditPositionCount == 0)
			{
				return this._testString.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder(this._testString.Length);
			for (int i = 0; i < this._testString.Length; i++)
			{
				MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[i];
				stringBuilder.Append((MaskedTextProvider.IsEditPosition(charDescriptor) && charDescriptor.IsAssigned) ? this._passwordChar : this._testString[i]);
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns the formatted string that includes all the assigned character values.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values.</returns>
		// Token: 0x06003895 RID: 14485 RVA: 0x000C5FC2 File Offset: 0x000C41C2
		public override string ToString()
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, 0, this._testString.Length);
		}

		/// <summary>Returns the formatted string, optionally including password characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes literals, prompts, and optionally password characters.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		// Token: 0x06003896 RID: 14486 RVA: 0x000C5FE3 File Offset: 0x000C41E3
		public string ToString(bool ignorePasswordChar)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, 0, this._testString.Length);
		}

		/// <summary>Returns a substring of the formatted string.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06003897 RID: 14487 RVA: 0x000C6004 File Offset: 0x000C4204
		public string ToString(int startPosition, int length)
		{
			return this.ToString(true, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes literals, prompts, and optionally password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x06003898 RID: 14488 RVA: 0x000C601B File Offset: 0x000C421B
		public string ToString(bool ignorePasswordChar, int startPosition, int length)
		{
			return this.ToString(ignorePasswordChar, this.IncludePrompt, this.IncludeLiterals, startPosition, length);
		}

		/// <summary>Returns the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>The formatted <see cref="T:System.String" /> that includes all the assigned character values and optionally includes literals and prompts.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		// Token: 0x06003899 RID: 14489 RVA: 0x000C6032 File Offset: 0x000C4232
		public string ToString(bool includePrompt, bool includeLiterals)
		{
			return this.ToString(true, includePrompt, includeLiterals, 0, this._testString.Length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt and literal characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals and prompts; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to include literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x0600389A RID: 14490 RVA: 0x000C6049 File Offset: 0x000C4249
		public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			return this.ToString(true, includePrompt, includeLiterals, startPosition, length);
		}

		/// <summary>Returns a substring of the formatted string, optionally including prompt, literal, and password characters.</summary>
		/// <returns>If successful, a substring of the formatted <see cref="T:System.String" />, which includes all the assigned character values and optionally includes literals, prompts, and password characters; otherwise the <see cref="F:System.String.Empty" /> string.</returns>
		/// <param name="ignorePasswordChar">true to return the actual editable characters; otherwise, false to indicate that the <see cref="P:System.ComponentModel.MaskedTextProvider.PasswordChar" /> property is to be honored.</param>
		/// <param name="includePrompt">true to include prompt characters in the return string; otherwise, false.</param>
		/// <param name="includeLiterals">true to return literal characters in the return string; otherwise, false.</param>
		/// <param name="startPosition">The zero-based position in the formatted string where the output begins. </param>
		/// <param name="length">The number of characters to return.</param>
		// Token: 0x0600389B RID: 14491 RVA: 0x000C6058 File Offset: 0x000C4258
		public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
		{
			if (length <= 0)
			{
				return string.Empty;
			}
			if (startPosition < 0)
			{
				startPosition = 0;
			}
			if (startPosition >= this._testString.Length)
			{
				return string.Empty;
			}
			int num = this._testString.Length - startPosition;
			if (length > num)
			{
				length = num;
			}
			if ((!this.IsPassword || ignorePasswordChar) && (includePrompt && includeLiterals))
			{
				return this._testString.ToString(startPosition, length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = startPosition + length - 1;
			if (!includePrompt)
			{
				int num3 = (includeLiterals ? this.FindNonEditPositionInRange(startPosition, num2, false) : MaskedTextProvider.InvalidIndex);
				int num4 = this.FindAssignedEditPositionInRange((num3 == MaskedTextProvider.InvalidIndex) ? startPosition : num3, num2, false);
				num2 = ((num4 != MaskedTextProvider.InvalidIndex) ? num4 : num3);
				if (num2 == MaskedTextProvider.InvalidIndex)
				{
					return string.Empty;
				}
			}
			int i = startPosition;
			while (i <= num2)
			{
				char c = this._testString[i];
				MaskedTextProvider.CharDescriptor charDescriptor = this._stringDescriptor[i];
				MaskedTextProvider.CharType charType = charDescriptor.CharType;
				if (charType - MaskedTextProvider.CharType.EditOptional > 1)
				{
					if (charType != MaskedTextProvider.CharType.Separator && charType != MaskedTextProvider.CharType.Literal)
					{
						goto IL_012F;
					}
					if (includeLiterals)
					{
						goto IL_012F;
					}
				}
				else if (charDescriptor.IsAssigned)
				{
					if (!this.IsPassword || ignorePasswordChar)
					{
						goto IL_012F;
					}
					stringBuilder.Append(this._passwordChar);
				}
				else
				{
					if (includePrompt)
					{
						goto IL_012F;
					}
					stringBuilder.Append(' ');
				}
				IL_0138:
				i++;
				continue;
				IL_012F:
				stringBuilder.Append(c);
				goto IL_0138;
			}
			return stringBuilder.ToString();
		}

		/// <summary>Tests whether the specified character could be set successfully at the specified position.</summary>
		/// <returns>true if the specified character is valid for the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		/// <param name="hint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the operation. An output parameter.</param>
		// Token: 0x0600389C RID: 14492 RVA: 0x000C61B1 File Offset: 0x000C43B1
		public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
		{
			hint = MaskedTextResultHint.NoEffect;
			if (position < 0 || position >= this._testString.Length)
			{
				hint = MaskedTextResultHint.PositionOutOfRange;
				return false;
			}
			return this.TestChar(input, position, out hint);
		}

		/// <summary>Tests whether the specified character would be escaped at the specified position.</summary>
		/// <returns>true if the specified character would be escaped at the specified position; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.Char" /> value to test.</param>
		/// <param name="position">The position in the mask to test the input character against.</param>
		// Token: 0x0600389D RID: 14493 RVA: 0x000C61D7 File Offset: 0x000C43D7
		public bool VerifyEscapeChar(char input, int position)
		{
			return position >= 0 && position < this._testString.Length && this.TestEscapeChar(input, position);
		}

		/// <summary>Tests whether the specified string could be set successfully.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false.</returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		// Token: 0x0600389E RID: 14494 RVA: 0x000C61F8 File Offset: 0x000C43F8
		public bool VerifyString(string input)
		{
			int num;
			MaskedTextResultHint maskedTextResultHint;
			return this.VerifyString(input, out num, out maskedTextResultHint);
		}

		/// <summary>Tests whether the specified string could be set successfully, and then outputs position and descriptive information.</summary>
		/// <returns>true if the specified string represents valid input; otherwise, false. </returns>
		/// <param name="input">The <see cref="T:System.String" /> value to test.</param>
		/// <param name="testPosition">If successful, the zero-based position of the last character actually tested; otherwise, the first position where the test failed. An output parameter.</param>
		/// <param name="resultHint">A <see cref="T:System.ComponentModel.MaskedTextResultHint" /> that succinctly describes the result of the test operation. An output parameter.</param>
		// Token: 0x0600389F RID: 14495 RVA: 0x000C6210 File Offset: 0x000C4410
		public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
		{
			testPosition = 0;
			if (input == null || input.Length == 0)
			{
				resultHint = MaskedTextResultHint.NoEffect;
				return true;
			}
			return this.TestString(input, 0, out testPosition, out resultHint);
		}

		// Token: 0x040020E4 RID: 8420
		private const char SPACE_CHAR = ' ';

		// Token: 0x040020E5 RID: 8421
		private const char DEFAULT_PROMPT_CHAR = '_';

		// Token: 0x040020E6 RID: 8422
		private const char NULL_PASSWORD_CHAR = '\0';

		// Token: 0x040020E7 RID: 8423
		private const bool DEFAULT_ALLOW_PROMPT = true;

		// Token: 0x040020E8 RID: 8424
		private const int INVALID_INDEX = -1;

		// Token: 0x040020E9 RID: 8425
		private const byte EDIT_ANY = 0;

		// Token: 0x040020EA RID: 8426
		private const byte EDIT_UNASSIGNED = 1;

		// Token: 0x040020EB RID: 8427
		private const byte EDIT_ASSIGNED = 2;

		// Token: 0x040020EC RID: 8428
		private const bool FORWARD = true;

		// Token: 0x040020ED RID: 8429
		private const bool BACKWARD = false;

		// Token: 0x040020EE RID: 8430
		private static int s_ASCII_ONLY = BitVector32.CreateMask();

		// Token: 0x040020EF RID: 8431
		private static int s_ALLOW_PROMPT_AS_INPUT = BitVector32.CreateMask(MaskedTextProvider.s_ASCII_ONLY);

		// Token: 0x040020F0 RID: 8432
		private static int s_INCLUDE_PROMPT = BitVector32.CreateMask(MaskedTextProvider.s_ALLOW_PROMPT_AS_INPUT);

		// Token: 0x040020F1 RID: 8433
		private static int s_INCLUDE_LITERALS = BitVector32.CreateMask(MaskedTextProvider.s_INCLUDE_PROMPT);

		// Token: 0x040020F2 RID: 8434
		private static int s_RESET_ON_PROMPT = BitVector32.CreateMask(MaskedTextProvider.s_INCLUDE_LITERALS);

		// Token: 0x040020F3 RID: 8435
		private static int s_RESET_ON_LITERALS = BitVector32.CreateMask(MaskedTextProvider.s_RESET_ON_PROMPT);

		// Token: 0x040020F4 RID: 8436
		private static int s_SKIP_SPACE = BitVector32.CreateMask(MaskedTextProvider.s_RESET_ON_LITERALS);

		// Token: 0x040020F5 RID: 8437
		private static Type s_maskTextProviderType = typeof(MaskedTextProvider);

		// Token: 0x040020F6 RID: 8438
		private BitVector32 _flagState;

		// Token: 0x040020F7 RID: 8439
		private StringBuilder _testString;

		// Token: 0x040020F8 RID: 8440
		private int _requiredCharCount;

		// Token: 0x040020F9 RID: 8441
		private int _requiredEditChars;

		// Token: 0x040020FA RID: 8442
		private int _optionalEditChars;

		// Token: 0x040020FB RID: 8443
		private char _passwordChar;

		// Token: 0x040020FC RID: 8444
		private char _promptChar;

		// Token: 0x040020FD RID: 8445
		private List<MaskedTextProvider.CharDescriptor> _stringDescriptor;

		// Token: 0x020006EB RID: 1771
		private enum CaseConversion
		{
			// Token: 0x04002102 RID: 8450
			None,
			// Token: 0x04002103 RID: 8451
			ToLower,
			// Token: 0x04002104 RID: 8452
			ToUpper
		}

		// Token: 0x020006EC RID: 1772
		[Flags]
		private enum CharType
		{
			// Token: 0x04002106 RID: 8454
			EditOptional = 1,
			// Token: 0x04002107 RID: 8455
			EditRequired = 2,
			// Token: 0x04002108 RID: 8456
			Separator = 4,
			// Token: 0x04002109 RID: 8457
			Literal = 8,
			// Token: 0x0400210A RID: 8458
			Modifier = 16
		}

		// Token: 0x020006ED RID: 1773
		private class CharDescriptor
		{
			// Token: 0x060038A1 RID: 14497 RVA: 0x000C62B0 File Offset: 0x000C44B0
			public CharDescriptor(int maskPos, MaskedTextProvider.CharType charType)
			{
				this.MaskPosition = maskPos;
				this.CharType = charType;
			}

			// Token: 0x060038A2 RID: 14498 RVA: 0x000C62C8 File Offset: 0x000C44C8
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "MaskPosition[{0}] <CaseConversion.{1}><CharType.{2}><IsAssigned: {3}", new object[] { this.MaskPosition, this.CaseConversion, this.CharType, this.IsAssigned });
			}

			// Token: 0x0400210B RID: 8459
			public int MaskPosition;

			// Token: 0x0400210C RID: 8460
			public MaskedTextProvider.CaseConversion CaseConversion;

			// Token: 0x0400210D RID: 8461
			public MaskedTextProvider.CharType CharType;

			// Token: 0x0400210E RID: 8462
			public bool IsAssigned;
		}
	}
}
