using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    /// <summary>
    /// Common user model for volunteers and participants
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique userid used in the backend database
        /// </summary>
        [Key]
        public Guid UserId { get; set; }

        /// <summary>
        /// OpenID connect SubjectID - only releavant if user can login
        /// If not connected to a user it is null
        /// </summary>
        public string SubjectId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
