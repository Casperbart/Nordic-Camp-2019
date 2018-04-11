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
        
        /// <summary>
        /// The name which the user identifies it self with
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        
        /// <summary>
        /// Also some places called the family name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// A electronic adress where we can send them mails electronic
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// A number to the users phone s.t. we can call them if they do not anwser their mail
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Where the person lives, including zip code, city,  street name, number, floor
        /// and left or right-hand-side
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// The Country where the user is currently living
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Is the user member of the Norwegian Alumni club
        /// </summary>
        public bool Alumni { get; set; }

        /// <summary>
        /// The date of when the user was born including, day, month and year
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Describes if the user are having problems eating a type of food
        /// e.g. lactose intolerance, vegan, nut allergies etc.
        /// </summary>
        public string Allergies { get; set; }

        /// <summary>
        /// A place where all sort of stuff can be added, which are relevant for the user
        /// </summary>
        public string Remarks { get; set; }

        // TODO: Remarks (string)
        // TODO: Activities (string / chose from a list)
        /// <summary>
        /// If set to true, the user do not mind that we take pictures of them.
        /// If set to false, the user do mind that we take pictures of them
        /// </summary>
        public bool PhotoPermission { get; set; }

        /// <summary>
        /// All the Guardians that are responsible for the user.
        /// </summary>
        public virtual ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
    }

    public class Guardian
    {
        /// <summary>
        /// The unique userid used in the backend database
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The name which the user identifies it self with
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// Also some places called the family name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// A electronic adress where we can send them mails electronic
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// A number to the users phone s.t. we can call them if they do not anwser their mail
        /// </summary>
        [Phone]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Where the Guardian lives, including zip code, city,  street name, number, floor
        /// and left or right-hand-side
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// The Country where the Guardian is currently living
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}
