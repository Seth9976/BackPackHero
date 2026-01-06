using System;

namespace System.Net
{
	/// <summary>Container class for <see cref="T:System.Net.WebRequestMethods.Ftp" />, <see cref="T:System.Net.WebRequestMethods.File" />, and <see cref="T:System.Net.WebRequestMethods.Http" /> classes. This class cannot be inherited</summary>
	// Token: 0x02000429 RID: 1065
	public static class WebRequestMethods
	{
		/// <summary>Represents the types of FTP protocol methods that can be used with an FTP request. This class cannot be inherited.</summary>
		// Token: 0x0200042A RID: 1066
		public static class Ftp
		{
			/// <summary>Represents the FTP RETR protocol method that is used to download a file from an FTP server.</summary>
			// Token: 0x04001381 RID: 4993
			public const string DownloadFile = "RETR";

			/// <summary>Represents the FTP NLIST protocol method that gets a short listing of the files on an FTP server.</summary>
			// Token: 0x04001382 RID: 4994
			public const string ListDirectory = "NLST";

			/// <summary>Represents the FTP STOR protocol method that uploads a file to an FTP server.</summary>
			// Token: 0x04001383 RID: 4995
			public const string UploadFile = "STOR";

			/// <summary>Represents the FTP DELE protocol method that is used to delete a file on an FTP server.</summary>
			// Token: 0x04001384 RID: 4996
			public const string DeleteFile = "DELE";

			/// <summary>Represents the FTP APPE protocol method that is used to append a file to an existing file on an FTP server.</summary>
			// Token: 0x04001385 RID: 4997
			public const string AppendFile = "APPE";

			/// <summary>Represents the FTP SIZE protocol method that is used to retrieve the size of a file on an FTP server.</summary>
			// Token: 0x04001386 RID: 4998
			public const string GetFileSize = "SIZE";

			/// <summary>Represents the FTP STOU protocol that uploads a file with a unique name to an FTP server.</summary>
			// Token: 0x04001387 RID: 4999
			public const string UploadFileWithUniqueName = "STOU";

			/// <summary>Represents the FTP MKD protocol method creates a directory on an FTP server.</summary>
			// Token: 0x04001388 RID: 5000
			public const string MakeDirectory = "MKD";

			/// <summary>Represents the FTP RMD protocol method that removes a directory.</summary>
			// Token: 0x04001389 RID: 5001
			public const string RemoveDirectory = "RMD";

			/// <summary>Represents the FTP LIST protocol method that gets a detailed listing of the files on an FTP server.</summary>
			// Token: 0x0400138A RID: 5002
			public const string ListDirectoryDetails = "LIST";

			/// <summary>Represents the FTP MDTM protocol method that is used to retrieve the date-time stamp from a file on an FTP server.</summary>
			// Token: 0x0400138B RID: 5003
			public const string GetDateTimestamp = "MDTM";

			/// <summary>Represents the FTP PWD protocol method that prints the name of the current working directory.</summary>
			// Token: 0x0400138C RID: 5004
			public const string PrintWorkingDirectory = "PWD";

			/// <summary>Represents the FTP RENAME protocol method that renames a directory.</summary>
			// Token: 0x0400138D RID: 5005
			public const string Rename = "RENAME";
		}

		/// <summary>Represents the types of HTTP protocol methods that can be used with an HTTP request.</summary>
		// Token: 0x0200042B RID: 1067
		public static class Http
		{
			/// <summary>Represents an HTTP GET protocol method. </summary>
			// Token: 0x0400138E RID: 5006
			public const string Get = "GET";

			/// <summary>Represents the HTTP CONNECT protocol method that is used with a proxy that can dynamically switch to tunneling, as in the case of SSL tunneling.</summary>
			// Token: 0x0400138F RID: 5007
			public const string Connect = "CONNECT";

			/// <summary>Represents an HTTP HEAD protocol method. The HEAD method is identical to GET except that the server only returns message-headers in the response, without a message-body.</summary>
			// Token: 0x04001390 RID: 5008
			public const string Head = "HEAD";

			/// <summary>Represents an HTTP PUT protocol method that is used to replace an entity identified by a URI.</summary>
			// Token: 0x04001391 RID: 5009
			public const string Put = "PUT";

			/// <summary>Represents an HTTP POST protocol method that is used to post a new entity as an addition to a URI.</summary>
			// Token: 0x04001392 RID: 5010
			public const string Post = "POST";

			/// <summary>Represents an HTTP MKCOL request that creates a new collection (such as a collection of pages) at the location specified by the request-Uniform Resource Identifier (URI).</summary>
			// Token: 0x04001393 RID: 5011
			public const string MkCol = "MKCOL";
		}

		/// <summary>Represents the types of file protocol methods that can be used with a FILE request. This class cannot be inherited.</summary>
		// Token: 0x0200042C RID: 1068
		public static class File
		{
			/// <summary>Represents the FILE GET protocol method that is used to retrieve a file from a specified location.</summary>
			// Token: 0x04001394 RID: 5012
			public const string DownloadFile = "GET";

			/// <summary>Represents the FILE PUT protocol method that is used to copy a file to a specified location.</summary>
			// Token: 0x04001395 RID: 5013
			public const string UploadFile = "PUT";
		}
	}
}
