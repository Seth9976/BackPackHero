using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Corresponds to the Distributed Transaction Coordinator (DTC) ITransaction interface and is supported by objects obtained through <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" />.</summary>
	// Token: 0x0200002D RID: 45
	[Guid("0FB15084-AF41-11CE-BD2B-204C4F4F5020")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ITransaction
	{
		/// <summary>Aborts the transaction.</summary>
		/// <param name="pboidReason">An optional <see cref="T:System.EnterpriseServices.BOID" /> that indicates why the transaction is being aborted. This parameter can be null, indicating that no reason for the abort is provided. </param>
		/// <param name="fRetaining">Must be false. </param>
		/// <param name="fAsync">When <paramref name="fAsync" /> is true, an asynchronous abort is performed and the caller must use ITransactionOutcomeEvents to learn the outcome of the transaction. </param>
		// Token: 0x0600008F RID: 143
		void Abort(ref BOID pboidReason, int fRetaining, int fAsync);

		/// <summary>Commits the transaction.</summary>
		/// <param name="fRetaining">Must be false. </param>
		/// <param name="grfTC">A value taken from the OLE DB enumeration XACTTC. </param>
		/// <param name="grfRM">Must be zero. </param>
		// Token: 0x06000090 RID: 144
		void Commit(int fRetaining, int grfTC, int grfRM);

		/// <summary>Returns information about a transaction object.</summary>
		/// <param name="pinfo">Pointer to the caller-allocated <see cref="T:System.EnterpriseServices.XACTTRANSINFO" /> structure that will receive information about the transaction. Must not be null. </param>
		// Token: 0x06000091 RID: 145
		void GetTransactionInfo(out XACTTRANSINFO pinfo);
	}
}
