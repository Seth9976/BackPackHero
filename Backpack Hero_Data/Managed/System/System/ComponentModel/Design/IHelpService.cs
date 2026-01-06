using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods for showing Help topics and adding and removing Help keywords at design time.</summary>
	// Token: 0x02000778 RID: 1912
	public interface IHelpService
	{
		/// <summary>Adds a context attribute to the document.</summary>
		/// <param name="name">The name of the attribute to add. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <param name="keywordType">The type of the keyword, from the enumeration <see cref="T:System.ComponentModel.Design.HelpKeywordType" />. </param>
		// Token: 0x06003CD1 RID: 15569
		void AddContextAttribute(string name, string value, HelpKeywordType keywordType);

		/// <summary>Removes all existing context attributes from the document.</summary>
		// Token: 0x06003CD2 RID: 15570
		void ClearContextAttributes();

		/// <summary>Creates a local <see cref="T:System.ComponentModel.Design.IHelpService" /> to manage subcontexts.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.Design.IHelpService" />.</returns>
		/// <param name="contextType">The priority type of the subcontext to add. </param>
		// Token: 0x06003CD3 RID: 15571
		IHelpService CreateLocalContext(HelpContextType contextType);

		/// <summary>Removes a previously added context attribute.</summary>
		/// <param name="name">The name of the attribute to remove. </param>
		/// <param name="value">The value of the attribute to remove. </param>
		// Token: 0x06003CD4 RID: 15572
		void RemoveContextAttribute(string name, string value);

		/// <summary>Removes a context created with <see cref="M:System.ComponentModel.Design.IHelpService.CreateLocalContext(System.ComponentModel.Design.HelpContextType)" />.</summary>
		/// <param name="localContext">The local context <see cref="T:System.ComponentModel.Design.IHelpService" /> to remove. </param>
		// Token: 0x06003CD5 RID: 15573
		void RemoveLocalContext(IHelpService localContext);

		/// <summary>Shows the Help topic that corresponds to the specified keyword.</summary>
		/// <param name="helpKeyword">The keyword of the Help topic to display. </param>
		// Token: 0x06003CD6 RID: 15574
		void ShowHelpFromKeyword(string helpKeyword);

		/// <summary>Shows the Help topic that corresponds to the specified URL.</summary>
		/// <param name="helpUrl">The URL of the Help topic to display. </param>
		// Token: 0x06003CD7 RID: 15575
		void ShowHelpFromUrl(string helpUrl);
	}
}
