using System;

namespace System.CodeDom
{
	/// <summary>Represents a code checksum pragma code entity.  </summary>
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class CodeChecksumPragma : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class. </summary>
		// Token: 0x0600187E RID: 6270 RVA: 0x0005F580 File Offset: 0x0005D780
		public CodeChecksumPragma()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeChecksumPragma" /> class using a file name, a GUID representing the checksum algorithm, and a byte stream representing the checksum data.</summary>
		/// <param name="fileName">The path to the checksum file.</param>
		/// <param name="checksumAlgorithmId">A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</param>
		/// <param name="checksumData">A byte array that contains the checksum data.</param>
		// Token: 0x0600187F RID: 6271 RVA: 0x0005F588 File Offset: 0x0005D788
		public CodeChecksumPragma(string fileName, Guid checksumAlgorithmId, byte[] checksumData)
		{
			this._fileName = fileName;
			this.ChecksumAlgorithmId = checksumAlgorithmId;
			this.ChecksumData = checksumData;
		}

		/// <summary>Gets or sets the path to the checksum file.</summary>
		/// <returns>The path to the checksum file.</returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x0005F5A5 File Offset: 0x0005D7A5
		// (set) Token: 0x06001881 RID: 6273 RVA: 0x0005F5B6 File Offset: 0x0005D7B6
		public string FileName
		{
			get
			{
				return this._fileName ?? string.Empty;
			}
			set
			{
				this._fileName = value;
			}
		}

		/// <summary>Gets or sets a GUID that identifies the checksum algorithm to use.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that identifies the checksum algorithm to use.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x0005F5BF File Offset: 0x0005D7BF
		// (set) Token: 0x06001883 RID: 6275 RVA: 0x0005F5C7 File Offset: 0x0005D7C7
		public Guid ChecksumAlgorithmId { get; set; }

		/// <summary>Gets or sets the value of the data for the checksum calculation.</summary>
		/// <returns>A byte array that contains the data for the checksum calculation.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x0005F5D0 File Offset: 0x0005D7D0
		// (set) Token: 0x06001885 RID: 6277 RVA: 0x0005F5D8 File Offset: 0x0005D7D8
		public byte[] ChecksumData { get; set; }

		// Token: 0x04000D6F RID: 3439
		private string _fileName;
	}
}
