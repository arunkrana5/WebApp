﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DataModal.CommanClass
{
    public class MyCustomData
    {
        public int Foo { get; set; }
        public string Bar { get; set; }
    }

    public class CustomMultipartFileStreamProvider : MultipartMemoryStreamProvider
    {

        private NameValueCollection _formData = new NameValueCollection();
        private List<HttpContent> _fileContents = new List<HttpContent>();

        // Set of indexes of which HttpContents we designate as form data
        private List<bool> _isFormData = new List<bool>();

        /// <summary>
        /// Gets a <see cref="NameValueCollection"/> of form data passed as part of the multipart form data.
        /// </summary>
        public NameValueCollection FormData
        {
            get { return _formData; }
        }

        /// <summary>
        /// Gets list of <see cref="HttpContent"/>s which contain uploaded files as in-memory representation.
        /// </summary>
        public List<HttpContent> Files
        {
            get { return _fileContents; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            // For form data, Content-Disposition header is a requirement
            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null)
            {
                // We will post process this as form data
                _isFormData.Add(String.IsNullOrEmpty(contentDisposition.FileName));

                return new MemoryStream();
            }

            // If no Content-Disposition header was present.
            throw new InvalidOperationException(string.Format("Did not find required '{0}' header field in MIME multipart body part..", "Content-Disposition"));
        }

        /// <summary>
        /// Read the non-file contents as form data.
        /// </summary>
        /// <returns></returns>
        public override async Task ExecutePostProcessingAsync()
        {
            // Find instances of non-file HttpContents and read them asynchronously
            // to get the string content and then add that as form data
            for (int index = 0; index < Contents.Count; index++)
            {
                if (_isFormData[index])
                {
                    HttpContent formContent = Contents[index];
                    // Extract name from Content-Disposition header. We know from earlier that the header is present.
                    ContentDispositionHeaderValue contentDisposition = formContent.Headers.ContentDisposition;
                    string formFieldName = UnquoteToken(contentDisposition.Name) ?? String.Empty;

                    // Read the contents as string data and add to form data
                    string formFieldValue = await formContent.ReadAsStringAsync();
                    FormData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    _fileContents.Add(Contents[index]);
                }
            }
        }

        /// <summary>
        /// Remove bounding quotes on a token if present
        /// </summary>
        /// <param name="token">Token to unquote.</param>
        /// <returns>Unquoted token.</returns>
        private static string UnquoteToken(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
        //public List<MyCustomData> CustomData { get; set; }

        //public CustomMultipartFileStreamProvider()
        //{
        //    CustomData = new List<MyCustomData>();
        //}

        //public override Task ExecutePostProcessingAsync()
        //{
        //    foreach (var file in Contents)
        //    {
        //        var parameters = file.Headers.ContentDisposition.Parameters;
        //        var data = new MyCustomData
        //        {
        //            Foo = int.Parse(GetNameHeaderValue(parameters, "Foo")),
        //            Bar = GetNameHeaderValue(parameters, "Bar"),
        //        };

        //        CustomData.Add(data);
        //    }

        //    return base.ExecutePostProcessingAsync();
        //}

        //private static string GetNameHeaderValue(ICollection<NameValueHeaderValue> headerValues, string name)
        //{
        //    var nameValueHeader = headerValues.FirstOrDefault(
        //        x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        //    return nameValueHeader != null ? nameValueHeader.Value : null;
        //}
    }
}
