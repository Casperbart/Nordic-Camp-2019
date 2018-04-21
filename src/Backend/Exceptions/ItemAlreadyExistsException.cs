using System;

namespace Backend.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException() : base("Item already exists") { }
    }
}