// <copyright file="FileData.cs" company="Acme">
// Copyright (c) Acme. All rights reserved.
// </copyright>

namespace Acme.Automation.Core.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represent a simple mail attachment.
    /// </summary>
    [DataContract]
    public class FileData
    {
        /// <summary>
        /// Gets or sets the Base64Content.
        /// </summary>
        /// <value>The Base64Content.</value>
        [DataMember(Name = "base64Content")]
        public string Base64Content { get; set; }

        /// <summary>
        /// Gets or sets the FileName.
        /// </summary>
        /// <value>The FileName.</value>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the MimeType.
        /// </summary>
        /// <value>The MimeType.</value>
        [DataMember(Name = "mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets the MediaType.
        /// </summary>
        /// <value>The MediaType.</value>
        [DataMember(Name = "mediaType")]
        public string MediaType { get; set; }
    }
}