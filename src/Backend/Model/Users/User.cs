using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model.Users
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
        public Guid UserId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// OpenID connect SubjectID - only releavant if user can login
        /// If not connected to a user it is null
        /// </summary>
        public string SubjectId { get; set; }
        
        /// <summary>
        /// The firstname of the user. The name which the user identifies it self with
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        
        /// <summary>
        /// The lastname of the user. Also some places called the family name
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        /// <summary>
        /// An electronic adress where we can send them mails electronic
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// A number for the user's phone s.t. we can call them if they do not answer their mail
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
        
        /// <summary>
        /// If set to true, the user do not mind that we take pictures of them.
        /// If set to false, the user do mind that we take pictures of them
        /// </summary>
        public bool PhotoPermission { get; set; }

        /// <summary>
        /// Activities which the user has registrered to
        /// </summary>
        public virtual ICollection<ActivityRegistration> ActivityRegistrations { get; set; } = new List<ActivityRegistration>();

        /// <summary>
        /// All the Guardians that are responsible for the user.
        /// </summary>
        public virtual ICollection<Guardian> Guardians { get; set; } = new List<Guardian>();
    }
}
