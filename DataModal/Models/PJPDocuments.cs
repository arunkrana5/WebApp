using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DataModal.Models
{
    public class PJPDocuments
    {
        public List<PJPDocumentList> DocumentList { get; set; }
        public string Status { get; set; }
    }
    public class PJPDocumentList
    {
        public List<PJPDocumentList> DocumentList { get; set; }
        public long? Attach_ID { get; set; }
        public long? PJPExpensesID { get; set; }
        public long? PJPEntryID { get; set; }
        [Required(ErrorMessage = "File Name Can't be Blank")]
        public string FileName { get; set; }
        [Required(ErrorMessage = "Expense Type Can't be Blank")]
        public string ExpenseType { get; set; }
        public string EMPName { get; set; }
        public string EMPCode { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        [Required(ErrorMessage = "Amount Can't be Blank")]
        public string Amount { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "File Can't be Blank")]
        public HttpPostedFileBase upload { get; set; }
        public string VisitType { get; set; }
        public string Status { get; set; }
    }
}
