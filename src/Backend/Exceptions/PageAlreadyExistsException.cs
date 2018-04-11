using System;

namespace Backend.Exceptions
{
    public class PageAlreadyExistsException : Exception
    {
        public PageAlreadyExistsException() : base("Page already exists") { }
    }
}