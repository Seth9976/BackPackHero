using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	/// <summary>Provides a container for content encoded using multipart/form-data MIME type.</summary>
	// Token: 0x0200002F RID: 47
	public class MultipartFormDataContent : MultipartContent
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		// Token: 0x0600017C RID: 380 RVA: 0x00006742 File Offset: 0x00004942
		public MultipartFormDataContent()
			: base("form-data")
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Http.MultipartFormDataContent" /> class.</summary>
		/// <param name="boundary">The boundary string for the multipart form data content.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="boundary" /> was null or contains only white space characters.-or-The <paramref name="boundary" /> ends with a space character.</exception>
		/// <exception cref="T:System.OutOfRangeException">The length of the <paramref name="boundary" /> was greater than 70.</exception>
		// Token: 0x0600017D RID: 381 RVA: 0x0000674F File Offset: 0x0000494F
		public MultipartFormDataContent(string boundary)
			: base("form-data", boundary)
		{
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was null.</exception>
		// Token: 0x0600017E RID: 382 RVA: 0x0000675D File Offset: 0x0000495D
		public override void Add(HttpContent content)
		{
			base.Add(content);
			this.AddContentDisposition(content, null, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was null or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was null.</exception>
		// Token: 0x0600017F RID: 383 RVA: 0x0000676F File Offset: 0x0000496F
		public void Add(HttpContent content, string name)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			this.AddContentDisposition(content, name, null);
		}

		/// <summary>Add HTTP content to a collection of <see cref="T:System.Net.Http.HttpContent" /> objects that get serialized to multipart/form-data MIME type.</summary>
		/// <param name="content">The HTTP content to add to the collection.</param>
		/// <param name="name">The name for the HTTP content to add.</param>
		/// <param name="fileName">The file name for the HTTP content to add to the collection.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> was null or contains only white space characters.-or-The <paramref name="fileName" /> was null or contains only white space characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="content" /> was null.</exception>
		// Token: 0x06000180 RID: 384 RVA: 0x00006794 File Offset: 0x00004994
		public void Add(HttpContent content, string name, string fileName)
		{
			base.Add(content);
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("name");
			}
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException("fileName");
			}
			this.AddContentDisposition(content, name, fileName);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000067CC File Offset: 0x000049CC
		private void AddContentDisposition(HttpContent content, string name, string fileName)
		{
			HttpContentHeaders headers = content.Headers;
			if (headers.ContentDisposition != null)
			{
				return;
			}
			headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
			{
				Name = name,
				FileName = fileName,
				FileNameStar = fileName
			};
		}
	}
}
