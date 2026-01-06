using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when binding to a member results in more than one member matching the binding criteria. This class cannot be inherited.</summary>
	// Token: 0x0200087C RID: 2172
	[Serializable]
	public sealed class AmbiguousMatchException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with an empty message string and the root cause exception set to null.</summary>
		// Token: 0x0600484A RID: 18506 RVA: 0x000EDF80 File Offset: 0x000EC180
		public AmbiguousMatchException()
			: base("Ambiguous match found.")
		{
			base.HResult = -2147475171;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with its message string set to the given message and the root cause exception set to null.</summary>
		/// <param name="message">A string indicating the reason this exception was thrown. </param>
		// Token: 0x0600484B RID: 18507 RVA: 0x000EDF98 File Offset: 0x000EC198
		public AmbiguousMatchException(string message)
			: base(message)
		{
			base.HResult = -2147475171;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AmbiguousMatchException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600484C RID: 18508 RVA: 0x000EDFAC File Offset: 0x000EC1AC
		public AmbiguousMatchException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147475171;
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x00020A69 File Offset: 0x0001EC69
		internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
