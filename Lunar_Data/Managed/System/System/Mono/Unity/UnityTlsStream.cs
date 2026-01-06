using System;
using System.IO;
using System.Net.Security;
using Mono.Net.Security;
using Mono.Security.Interface;

namespace Mono.Unity
{
	// Token: 0x0200007A RID: 122
	internal class UnityTlsStream : MobileAuthenticatedStream
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00005304 File Offset: 0x00003504
		public UnityTlsStream(Stream innerStream, bool leaveInnerStreamOpen, SslStream owner, MonoTlsSettings settings, MobileTlsProvider provider)
			: base(innerStream, leaveInnerStreamOpen, owner, settings, provider)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005313 File Offset: 0x00003513
		protected override MobileTlsContext CreateContext(MonoSslAuthenticationOptions options)
		{
			return new UnityTlsContext(this, options);
		}
	}
}
