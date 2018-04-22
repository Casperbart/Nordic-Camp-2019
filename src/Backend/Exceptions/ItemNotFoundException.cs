using System;

namespace Backend.Exceptions
{
    /// <summary>
    /// Thrown if the item requested from the repository could not be found
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base("Item not found") { }
    }
}