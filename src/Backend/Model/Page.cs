using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    /// <summary>
    /// Contains information about a page on the website
    /// </summary>
    public class Page
    {
        private string _url;

        /// <summary>
        /// Url for the page
        /// </summary>
        [Key, Required(AllowEmptyStrings = false)]

        public string Url { get => _url; set => _url = value.ToLower(); }

        /// <summary>
        /// Content on the page
        /// </summary>
        public string Content { get; set; }
    }
}
