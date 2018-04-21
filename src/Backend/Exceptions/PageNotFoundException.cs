using System;

namespace Backend.Exceptions
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException() : base("Page not found") { }
    }
}