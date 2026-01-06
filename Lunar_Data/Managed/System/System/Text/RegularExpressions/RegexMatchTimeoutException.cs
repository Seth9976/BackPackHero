using System;
using System.Runtime.Serialization;

namespace System.Text.RegularExpressions
{
	/// <summary>The exception that is thrown when the execution time of a regular expression pattern-matching method exceeds its time-out interval.</summary>
	// Token: 0x02000206 RID: 518
	[Serializable]
	public class RegexMatchTimeoutException : TimeoutException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with information about the regular expression pattern, the input text, and the time-out interval.</summary>
		/// <param name="regexInput">The input text processed by the regular expression engine when the time-out occurred.</param>
		/// <param name="regexPattern">The pattern used by the regular expression engine when the time-out occurred.</param>
		/// <param name="matchTimeout">The time-out interval.</param>
		// Token: 0x06000E9A RID: 3738 RVA: 0x0003FEA8 File Offset: 0x0003E0A8
		public RegexMatchTimeoutException(string regexInput, string regexPattern, TimeSpan matchTimeout)
			: base("The RegEx engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors.")
		{
			this.Input = regexInput;
			this.Pattern = regexPattern;
			this.MatchTimeout = matchTimeout;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a system-supplied message.</summary>
		// Token: 0x06000E9B RID: 3739 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		public RegexMatchTimeoutException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with the specified message string.</summary>
		/// <param name="message">A string that describes the exception.</param>
		// Token: 0x06000E9C RID: 3740 RVA: 0x0003FF23 File Offset: 0x0003E123
		public RegexMatchTimeoutException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">A string that describes the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception.</param>
		// Token: 0x06000E9D RID: 3741 RVA: 0x0003FF4F File Offset: 0x0003E14F
		public RegexMatchTimeoutException(string message, Exception inner)
			: base(message, inner)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> class with serialized data.</summary>
		/// <param name="info">The object that contains the serialized data.</param>
		/// <param name="context">The stream that contains the serialized data.</param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x0003FF7C File Offset: 0x0003E17C
		protected RegexMatchTimeoutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Input = info.GetString("regexInput");
			this.Pattern = info.GetString("regexPattern");
			this.MatchTimeout = new TimeSpan(info.GetInt64("timeoutTicks"));
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize a <see cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException" /> object.</summary>
		/// <param name="si">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		// Token: 0x06000E9F RID: 3743 RVA: 0x0003FFEC File Offset: 0x0003E1EC
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("regexInput", this.Input);
			info.AddValue("regexPattern", this.Pattern);
			info.AddValue("timeoutTicks", this.MatchTimeout.Ticks);
		}

		/// <summary>Gets the input text that the regular expression engine was processing when the time-out occurred.</summary>
		/// <returns>The regular expression input text.</returns>
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x0004003C File Offset: 0x0003E23C
		public string Input { get; } = string.Empty;

		/// <summary>Gets the regular expression pattern that was used in the matching operation when the time-out occurred.</summary>
		/// <returns>The regular expression pattern.</returns>
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00040044 File Offset: 0x0003E244
		public string Pattern { get; } = string.Empty;

		/// <summary>Gets the time-out interval for a regular expression match.</summary>
		/// <returns>The time-out interval.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0004004C File Offset: 0x0003E24C
		public TimeSpan MatchTimeout { get; } = TimeSpan.FromTicks(-1L);
	}
}
