using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace User_Dashboard.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name cannot be left blank.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters in length.")]
        [Display(Name = "First Name")]
        public string firstname { get; set; }

        [Required(ErrorMessage = "Last name cannot be left blank.")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters in length.")]
        public string lastname { get; set; }

        [Required(ErrorMessage = "Username cannot be left blank.")]
        [MinLength(3, ErrorMessage = "Username cannot be less than 3 characters.")]
        [MaxLength(20, ErrorMessage = "Username cannot be greater than 20 characters.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password cannot be left blank.")]
        [Compare("confirm", ErrorMessage = "Password and confirm password do not match.")]
        [MinLength(8, ErrorMessage = "Password cannot be less than 8 characters.")]
        // [RegularExpression(@"(?=^.{8,}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;&#39;?/&gt;.&lt;,])(?!.*\s).*$", ErrorMessage = "Password must be at least 8 characters and include 1 lowercase letter, 1 uppercase letter, 1 number, and 1 special character.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Confirm Password cannot be left blank.")]
        public string confirm { get; set; }
        public int wallet { get; set; }

    public User()
    {
        Auction = new List<Auction>();

    }
        public List<Auction> Auction { get; set; }


}
    
    public class LogUser : BaseEntity
    { 
        [Required(ErrorMessage = "Email address cannot be left blank")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password cannot be left blank")]
        public string password { get; set; }
    }

}