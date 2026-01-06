using System;
using System.Collections.Generic;

// Token: 0x02000024 RID: 36
public class XGameSaveWrapper
{
	// Token: 0x060000E1 RID: 225 RVA: 0x00007068 File Offset: 0x00005268
	~XGameSaveWrapper()
	{
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00007090 File Offset: 0x00005290
	public void InitializeAsync(XGameSaveWrapper.XUserHandle userHandle, string scid, XGameSaveWrapper.InitializeCallback callback)
	{
		callback(0);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00007099 File Offset: 0x00005299
	public void GetQuotaAsync(XGameSaveWrapper.GetQuotaCallback callback)
	{
		callback(0, 0L);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x000070A4 File Offset: 0x000052A4
	public void QueryContainers(string containerNamePrefix, XGameSaveWrapper.QueryContainersCallback callback)
	{
		callback(0, new string[0]);
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x000070B3 File Offset: 0x000052B3
	public void QueryContainerBlobs(string containerName, XGameSaveWrapper.QueryBlobsCallback callback)
	{
		callback(0, new Dictionary<string, uint>());
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x000070C1 File Offset: 0x000052C1
	public void Load(string containerName, string blobName, XGameSaveWrapper.LoadCallback callback)
	{
		callback(0, new byte[0]);
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x000070D0 File Offset: 0x000052D0
	public void Save(string containerName, string blobName, byte[] blobData, XGameSaveWrapper.SaveCallback callback)
	{
		callback(0);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x000070DA File Offset: 0x000052DA
	public void Delete(string containerName, XGameSaveWrapper.DeleteCallback callback)
	{
		callback(0);
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x000070E3 File Offset: 0x000052E3
	public void Delete(string containerName, string blobName, XGameSaveWrapper.DeleteCallback callback)
	{
		callback(0);
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000070EC File Offset: 0x000052EC
	public void Delete(string containerName, string[] blobNames, XGameSaveWrapper.DeleteCallback callback)
	{
		callback(0);
	}

	// Token: 0x060000EB RID: 235 RVA: 0x000070F5 File Offset: 0x000052F5
	private void Update(string containerName, IDictionary<string, byte[]> blobsToSave, IList<string> blobsToDelete, XGameSaveWrapper.UpdateCallback callback)
	{
		callback(0);
	}

	// Token: 0x02000251 RID: 593
	public struct XUserHandle
	{
	}

	// Token: 0x02000252 RID: 594
	// (Invoke) Token: 0x060012E4 RID: 4836
	public delegate void InitializeCallback(int hresult);

	// Token: 0x02000253 RID: 595
	// (Invoke) Token: 0x060012E8 RID: 4840
	public delegate void GetQuotaCallback(int hresult, long remainingQuota);

	// Token: 0x02000254 RID: 596
	// (Invoke) Token: 0x060012EC RID: 4844
	public delegate void QueryContainersCallback(int hresult, string[] containerNames);

	// Token: 0x02000255 RID: 597
	// (Invoke) Token: 0x060012F0 RID: 4848
	public delegate void QueryBlobsCallback(int hresult, Dictionary<string, uint> blobInfos);

	// Token: 0x02000256 RID: 598
	// (Invoke) Token: 0x060012F4 RID: 4852
	public delegate void LoadCallback(int hresult, byte[] blobData);

	// Token: 0x02000257 RID: 599
	// (Invoke) Token: 0x060012F8 RID: 4856
	public delegate void SaveCallback(int hresult);

	// Token: 0x02000258 RID: 600
	// (Invoke) Token: 0x060012FC RID: 4860
	public delegate void DeleteCallback(int hresult);

	// Token: 0x02000259 RID: 601
	// (Invoke) Token: 0x06001300 RID: 4864
	private delegate void UpdateCallback(int hresult);
}
