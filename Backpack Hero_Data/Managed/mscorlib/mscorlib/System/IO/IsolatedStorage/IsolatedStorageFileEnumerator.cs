using System;
using System.Collections;

namespace System.IO.IsolatedStorage
{
	// Token: 0x02000B75 RID: 2933
	internal class IsolatedStorageFileEnumerator : IEnumerator
	{
		// Token: 0x06006AD1 RID: 27345 RVA: 0x0016DD33 File Offset: 0x0016BF33
		public IsolatedStorageFileEnumerator(IsolatedStorageScope scope, string root)
		{
			this._scope = scope;
			if (Directory.Exists(root))
			{
				this._storages = Directory.GetDirectories(root, "d.*");
			}
			this._pos = -1;
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x0016DD62 File Offset: 0x0016BF62
		public object Current
		{
			get
			{
				if (this._pos < 0 || this._storages == null || this._pos >= this._storages.Length)
				{
					return null;
				}
				return new IsolatedStorageFile(this._scope, this._storages[this._pos]);
			}
		}

		// Token: 0x06006AD3 RID: 27347 RVA: 0x0016DDA0 File Offset: 0x0016BFA0
		public bool MoveNext()
		{
			if (this._storages == null)
			{
				return false;
			}
			int num = this._pos + 1;
			this._pos = num;
			return num < this._storages.Length;
		}

		// Token: 0x06006AD4 RID: 27348 RVA: 0x0016DDD2 File Offset: 0x0016BFD2
		public void Reset()
		{
			this._pos = -1;
		}

		// Token: 0x04003DA3 RID: 15779
		private IsolatedStorageScope _scope;

		// Token: 0x04003DA4 RID: 15780
		private string[] _storages;

		// Token: 0x04003DA5 RID: 15781
		private int _pos;
	}
}
