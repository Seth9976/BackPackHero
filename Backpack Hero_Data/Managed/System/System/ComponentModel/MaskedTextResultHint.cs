using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values that succinctly describe the results of a masked text parsing operation.</summary>
	// Token: 0x020006EE RID: 1774
	public enum MaskedTextResultHint
	{
		/// <summary>Unknown. The result of the operation could not be determined.</summary>
		// Token: 0x04002110 RID: 8464
		Unknown,
		/// <summary>Success. The operation succeeded because a literal, prompt or space character was an escaped character. For more information about escaped characters, see the <see cref="M:System.ComponentModel.MaskedTextProvider.VerifyEscapeChar(System.Char,System.Int32)" /> method.</summary>
		// Token: 0x04002111 RID: 8465
		CharacterEscaped,
		/// <summary>Success. The primary operation was not performed because it was not needed; therefore, no side effect was produced.</summary>
		// Token: 0x04002112 RID: 8466
		NoEffect,
		/// <summary>Success. The primary operation was not performed because it was not needed, but the method produced a side effect. For example, the <see cref="Overload:System.ComponentModel.MaskedTextProvider.RemoveAt" /> method can delete an unassigned edit position, which causes left-shifting of subsequent characters in the formatted string. </summary>
		// Token: 0x04002113 RID: 8467
		SideEffect,
		/// <summary>Success. The primary operation succeeded.</summary>
		// Token: 0x04002114 RID: 8468
		Success,
		/// <summary>Operation did not succeed.An input character was encountered that was not a member of the ASCII character set.</summary>
		// Token: 0x04002115 RID: 8469
		AsciiCharacterExpected = -1,
		/// <summary>Operation did not succeed.An input character was encountered that was not alphanumeric. .</summary>
		// Token: 0x04002116 RID: 8470
		AlphanumericCharacterExpected = -2,
		/// <summary>Operation did not succeed. An input character was encountered that was not a digit.</summary>
		// Token: 0x04002117 RID: 8471
		DigitExpected = -3,
		/// <summary>Operation did not succeed. An input character was encountered that was not a letter.</summary>
		// Token: 0x04002118 RID: 8472
		LetterExpected = -4,
		/// <summary>Operation did not succeed. An input character was encountered that was not a signed digit.</summary>
		// Token: 0x04002119 RID: 8473
		SignedDigitExpected = -5,
		/// <summary>Operation did not succeed. The program encountered an  input character that was not valid. For more information about characters that are not valid, see the <see cref="M:System.ComponentModel.MaskedTextProvider.IsValidInputChar(System.Char)" /> method.</summary>
		// Token: 0x0400211A RID: 8474
		InvalidInput = -51,
		/// <summary>Operation did not succeed. The prompt character is not valid at input, perhaps because the <see cref="P:System.ComponentModel.MaskedTextProvider.AllowPromptAsInput" /> property is set to false. </summary>
		// Token: 0x0400211B RID: 8475
		PromptCharNotAllowed = -52,
		/// <summary>Operation did not succeed. There were not enough edit positions available to fulfill the request.</summary>
		// Token: 0x0400211C RID: 8476
		UnavailableEditPosition = -53,
		/// <summary>Operation did not succeed. The current position in the formatted string is a literal character. </summary>
		// Token: 0x0400211D RID: 8477
		NonEditPosition = -54,
		/// <summary>Operation did not succeed. The specified position is not in the range of the target string; typically it is either less than zero or greater then the length of the target string.</summary>
		// Token: 0x0400211E RID: 8478
		PositionOutOfRange = -55
	}
}
