/* 
 * DocuSign REST API
 *
 * The DocuSign REST API provides you with a powerful, convenient, and simple Web services API for interacting with DocuSign.
 *
 * OpenAPI spec version: v2
 * Contact: devcenter@docusign.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace DocuSign.eSign.Model
{
    /// <summary>
    /// CompositeTemplate
    /// </summary>
    [DataContract]
    public partial class CompositeTemplate :  IEquatable<CompositeTemplate>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeTemplate" /> class.
        /// </summary>
        /// <param name="CompositeTemplateId">The identify of this composite template. It is used as a reference when adding document object information. If used, the document’s &#x60;content-disposition&#x60; must include the composite template ID to which the document should be added. If a composite template ID is not specified in the content-disposition, the document is applied based on the value of the &#x60;documentId&#x60; property only. If no document object is specified, the composite template inherits the first document..</param>
        /// <param name="Document">Document.</param>
        /// <param name="InlineTemplates"> Zero or more inline templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value..</param>
        /// <param name="PdfMetaDataTemplateSequence">.</param>
        /// <param name="ServerTemplates">0 or more server-side templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value.</param>
        public CompositeTemplate(string CompositeTemplateId = default(string), Document Document = default(Document), List<InlineTemplate> InlineTemplates = default(List<InlineTemplate>), string PdfMetaDataTemplateSequence = default(string), List<ServerTemplate> ServerTemplates = default(List<ServerTemplate>))
        {
            this.CompositeTemplateId = CompositeTemplateId;
            this.Document = Document;
            this.InlineTemplates = InlineTemplates;
            this.PdfMetaDataTemplateSequence = PdfMetaDataTemplateSequence;
            this.ServerTemplates = ServerTemplates;
        }
        
        /// <summary>
        /// The identify of this composite template. It is used as a reference when adding document object information. If used, the document’s &#x60;content-disposition&#x60; must include the composite template ID to which the document should be added. If a composite template ID is not specified in the content-disposition, the document is applied based on the value of the &#x60;documentId&#x60; property only. If no document object is specified, the composite template inherits the first document.
        /// </summary>
        /// <value>The identify of this composite template. It is used as a reference when adding document object information. If used, the document’s &#x60;content-disposition&#x60; must include the composite template ID to which the document should be added. If a composite template ID is not specified in the content-disposition, the document is applied based on the value of the &#x60;documentId&#x60; property only. If no document object is specified, the composite template inherits the first document.</value>
        [DataMember(Name="compositeTemplateId", EmitDefaultValue=false)]
        public string CompositeTemplateId { get; set; }
        /// <summary>
        /// Gets or Sets Document
        /// </summary>
        [DataMember(Name="document", EmitDefaultValue=false)]
        public Document Document { get; set; }
        /// <summary>
        ///  Zero or more inline templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value.
        /// </summary>
        /// <value> Zero or more inline templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value.</value>
        [DataMember(Name="inlineTemplates", EmitDefaultValue=false)]
        public List<InlineTemplate> InlineTemplates { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        [DataMember(Name="pdfMetaDataTemplateSequence", EmitDefaultValue=false)]
        public string PdfMetaDataTemplateSequence { get; set; }
        /// <summary>
        /// 0 or more server-side templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value
        /// </summary>
        /// <value>0 or more server-side templates and their position in the overlay. If supplied, they are overlaid into the envelope in the order of their Sequence value</value>
        [DataMember(Name="serverTemplates", EmitDefaultValue=false)]
        public List<ServerTemplate> ServerTemplates { get; set; }
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CompositeTemplate {\n");
            sb.Append("  CompositeTemplateId: ").Append(CompositeTemplateId).Append("\n");
            sb.Append("  Document: ").Append(Document).Append("\n");
            sb.Append("  InlineTemplates: ").Append(InlineTemplates).Append("\n");
            sb.Append("  PdfMetaDataTemplateSequence: ").Append(PdfMetaDataTemplateSequence).Append("\n");
            sb.Append("  ServerTemplates: ").Append(ServerTemplates).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            return this.Equals(obj as CompositeTemplate);
        }

        /// <summary>
        /// Returns true if CompositeTemplate instances are equal
        /// </summary>
        /// <param name="other">Instance of CompositeTemplate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CompositeTemplate other)
        {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return 
                (
                    this.CompositeTemplateId == other.CompositeTemplateId ||
                    this.CompositeTemplateId != null &&
                    this.CompositeTemplateId.Equals(other.CompositeTemplateId)
                ) && 
                (
                    this.Document == other.Document ||
                    this.Document != null &&
                    this.Document.Equals(other.Document)
                ) && 
                (
                    this.InlineTemplates == other.InlineTemplates ||
                    this.InlineTemplates != null &&
                    this.InlineTemplates.SequenceEqual(other.InlineTemplates)
                ) && 
                (
                    this.PdfMetaDataTemplateSequence == other.PdfMetaDataTemplateSequence ||
                    this.PdfMetaDataTemplateSequence != null &&
                    this.PdfMetaDataTemplateSequence.Equals(other.PdfMetaDataTemplateSequence)
                ) && 
                (
                    this.ServerTemplates == other.ServerTemplates ||
                    this.ServerTemplates != null &&
                    this.ServerTemplates.SequenceEqual(other.ServerTemplates)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks etc, of course :)
                if (this.CompositeTemplateId != null)
                    hash = hash * 59 + this.CompositeTemplateId.GetHashCode();
                if (this.Document != null)
                    hash = hash * 59 + this.Document.GetHashCode();
                if (this.InlineTemplates != null)
                    hash = hash * 59 + this.InlineTemplates.GetHashCode();
                if (this.PdfMetaDataTemplateSequence != null)
                    hash = hash * 59 + this.PdfMetaDataTemplateSequence.GetHashCode();
                if (this.ServerTemplates != null)
                    hash = hash * 59 + this.ServerTemplates.GetHashCode();
                return hash;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        { 
            yield break;
        }
    }

}
