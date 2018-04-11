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

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }

        public bool Alumni { get; set; }

        public DateTime Birthday { get; set; }

        public string Allergies { get; set; }
        public string Remarks { get; set; }

        // TODO: Remarks (string)
        // TODO: Activities (string / chose from a list)

        public bool PhotoPermission { get; set; }

        public virtual ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
    }

    public class Guardian
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
