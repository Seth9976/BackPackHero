using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides the managed definition of the TYMED structure.</summary>
	// Token: 0x02000191 RID: 401
	[Flags]
	public enum TYMED
	{
		/// <summary>The storage medium is a global memory handle (HGLOBAL). Allocate the global handle with the GMEM_SHARE flag. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use GlobalFree to release the memory.</summary>
		// Token: 0x0400070C RID: 1804
		TYMED_HGLOBAL = 1,
		/// <summary>The storage medium is a disk file identified by a path. If the STGMEDIUM<see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use OpenFile to delete the file.</summary>
		// Token: 0x0400070D RID: 1805
		TYMED_FILE = 2,
		/// <summary>The storage medium is a stream object identified by an IStream pointer. Use ISequentialStream::Read to read the data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not null, the destination process should use IStream::Release to release the stream component.</summary>
		// Token: 0x0400070E RID: 1806
		TYMED_ISTREAM = 4,
		/// <summary>The storage medium is a storage component identified by an IStorage pointer. The data is in the streams and storages contained by this IStorage instance. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is not null, the destination process should use IStorage::Release to release the storage component.</summary>
		// Token: 0x0400070F RID: 1807
		TYMED_ISTORAGE = 8,
		/// <summary>The storage medium is a Graphics Device Interface (GDI) component (HBITMAP). If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteObject to delete the bitmap.</summary>
		// Token: 0x04000710 RID: 1808
		TYMED_GDI = 16,
		/// <summary>The storage medium is a metafile (HMETAFILE). Use the Windows or WIN32 functions to access the metafile's data. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteMetaFile to delete the bitmap.</summary>
		// Token: 0x04000711 RID: 1809
		TYMED_MFPICT = 32,
		/// <summary>The storage medium is an enhanced metafile. If the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /><see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member is null, the destination process should use DeleteEnhMetaFile to delete the bitmap.</summary>
		// Token: 0x04000712 RID: 1810
		TYMED_ENHMF = 64,
		/// <summary>No data is being passed.</summary>
		// Token: 0x04000713 RID: 1811
		TYMED_NULL = 0
	}
}
