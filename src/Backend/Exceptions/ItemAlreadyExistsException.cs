using System;

namespace Backend.Exceptions
{
    /// <summary>
    /// Exception thrown when item already exists in a repository
    /// </summary>
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException() : base("Item already exists") { }
    }
}