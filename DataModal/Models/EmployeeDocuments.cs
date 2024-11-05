using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class EmployeeDocuments
    {
        public List<EMPDocumentList> DocumentList { get; set; }

    }
    public class EMPDocumentList
    {
        public long? Attach_ID { get; set; }
        public long EMPID { get; set; }
        [Required(ErrorMessage = "File Name Can't be Blank")]
        public string FileName { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase upload { get; set; }
    }
}
