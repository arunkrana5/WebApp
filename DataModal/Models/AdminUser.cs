using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class AdminUser
    {
        public class Details
        {
            public bool status { get; set; }
            public long LoginID { get; set; }
            public string UserID { get; set; }
            public long RoleID { get; set; }
            public string RoleName { get; set; }
            public long EMPID { get; set; }
            public string DesignName { get; set; }
            public string DeptName { get; set; }
            public string EMPName { get; set; }
            public string EMPCode { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public string AttendenceStatus { get; set; }

            public long DealerID { get; set; }
            public string DealerName { get; set; }
            public string DealerCode { get; set; }
            public string DealerArea { get; set; }
            public string DealerAddress { get; set; }
            public string ImageURL { get; set; }
            public string CompanyCode { get; set; }
            public string ConfigToken { get; set; }
            public string Message { get; set; }


        }
        public class Login
        {

            [Required(ErrorMessage = "User Name Can't Be Blank")]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Password Can't Be Blank")]
            public string Password { get; set; }
            public string IPAddress { get; set; }
            public string SessionID { get; set; }
            public string grant_type { get; set; }
            public string LoginInfo { get; set; }

            public bool RememberMe { get; set; }
            public Login()
            {
                grant_type = "password";
            }

        }

        public class Role
        {
            public class List
            {
                public long RoleID { get; set; }
                public string rolename { get; set; }
                public string DisplayName { get; set; }
                public string description { get; set; }

                public bool IsActive { get; set; }
                public string CreatedBy { get; set; }
                public string CreatedDate { get; set; }
                public string ModifiedDate { get; set; }
                public string ModifiedBy { get; set; }
                public string IPAddress { get; set; }
            }
            public class Add
            {
                public long RoleID { get; set; }
                [Required(ErrorMessage = "Role Name Can't Be Blank")]
                public string rolename { get; set; }
                [Required(ErrorMessage = "Display Name Can't Be Blank")]
                public string DisplayName { get; set; }
                public string description { get; set; }
                public string IPAddress { get; set; }
                public long LoginID { get; set; }
                public int? Priority { get; set; }
                public bool IsActive { get; set; }
                public string StateIDs { get; set; }
                public List<DropDownlist> StateList { get; set; }
            }
        }
        public class TokenDetails
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string client_id { get; set; }
            public string userName { get; set; }
            public string userDetails { get; set; }
            public string issued { get; set; }
            public string expires { get; set; }
        }
    }
    public class Users
    {
        public class List
        {
            public int RowNum { get; set; }
            public long LoginID { get; set; }
            public string UserID { get; set; }
            public int RoleID { get; set; }
            public string rolename { get; set; }
            public string AllowLogin { get; set; }
            public string Phone { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
            public string Description { get; set; }
            public string email { get; set; }
            public int TotalCount { get; set; }
            public int Priority { get; set; }
            public bool IsActive { get; set; }
            public string CreatedBy { get; set; }
            public string CreatedDate { get; set; }
            public string ModifiedDate { get; set; }
            public string ModifiedBy { get; set; }
            public string IPAddress { get; set; }
        }
        public class Add
        {
            public long ID { get; set; }
            [Required(ErrorMessage = "User ID Can't Be Blank")]
            [RegularExpression(@"^.{5,}$", ErrorMessage = "Minimum 5 characters required")]
            public string UserID { get; set; }

            [Required(ErrorMessage = "Role Can't Be Blank")]
            public int RoleID { get; set; }
            public string RoleName { get; set; }

            [Required(ErrorMessage = "Name Can't Be Blank")]
            public string Name { get; set; }
            [Required(ErrorMessage = "Password Can't Be Blank")]
            public string Password { get; set; }
            public string Description { get; set; }

            [Required(ErrorMessage = "Phone Can't Be Blank")]
            public string Phone { get; set; }


            [StringLength(50, ErrorMessage = "Allowed Maximum Length 50 Characters")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress]
            public string email { get; set; }
            public int? Priority { get; set; }
            public long LoginID { get; set; }

            public string IPAddress { set; get; }
            public string AllowLogin { get; set; }
        }
    }


    public class ChangePassword
    {
        public long LoginID { get; set; }
        [Required(ErrorMessage = "New Password Can't Be Blank")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Can't Be Blank")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Old Password Can't Be Blank")]
        public string OldPassword { get; set; }

        public string Doctype { get; set; }
        public string IPAddress { get; set; }
    }

    public class ForgotPassword
    {
        [Required(ErrorMessage = "User ID Can't Be Blank")]
        public string UserID { get; set; }
    }

    public class SetPassword
    {
        [Required(ErrorMessage = "New Password Can't Be Blank")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Can't Be Blank")]
        public string ConfirmPassword { get; set; }
        public string IPAddress { get; set; }

    }
}
