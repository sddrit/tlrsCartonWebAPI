using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using tlrsCartonManager.DAL.Models;

namespace tlrsCartonManager.DAL.Dtos
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string EmpId { get; set; }
        public int? AppId { get; set; }
        public string UserPassword { get; set; }
        public int UserRoleId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int TransactionUserId { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public int? LoginAttempts { get; set; }
        public bool Lock { get; set; }
        public string CustomerCode { get; set; }
        public string Type { get; set; }
        public int AuthorizationId { get; set; }
        public int CustomerPortalRole { get; set; }


        public DateTime? PasswordLockedDate { get; set; }
       

        public ICollection<UserRoleDto> UserRoles { get; set; }
    }

    public class UserCustomerPortalDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }        
        public string Email { get; set; }
        public bool Active { get; set; }
        public string UserPassword { get; set; }
        public string CustomerCode { get; set; }
        public string UserType { get; set; }
        public int AuthorizationId { get; set; }
        public int CustomerPortalRole { get; set; }
        
    }

    public class UserRoleDto
    {
        public int Id { get; set; }

    }
    public class UserSerachDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string EmpId { get; set; }
        public string DepartmentName { get; set; }

    }

    public class UserSerachCustomerPortalDto
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string CustomerCode { get; set; }
        public string UserType { get; set; }        
        public string Email { get; set; }
        public bool Active { get; set; }      
        public int AuthorizationId { get; set; }
        public int CustomerPortalRole { get; set; }

    }

    public class UserResponse
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string EmpId { get; set; }
        public int? AppId { get; set; }
        public string UserRoles { get; set; }
        public int? DepartmentId { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int TransactionUserId { get; set; }
    }
    public class UserChangePassword
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class UserPasswordExpiredModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string OldPassword { get; set; }       
        public string NewPassword { get; set; }       
        public string ConfirmPassword { get; set; }
    }

}
