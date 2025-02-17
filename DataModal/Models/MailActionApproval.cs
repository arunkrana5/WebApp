using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.Models
{
    public class MailActionApproval
    {
        [Required(ErrorMessage = "Remarks Can't be Blank")]
        public string Remarks {  get; set; }
        public string Doctype { get; set; }
        public int ID {  get; set; }
        public int LoginID {  get; set; }
        public int Approved {  get; set; }
    }
}
