using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Model
{
    public abstract class BaseEntity<T>
    {
        [Key, Required(AllowEmptyStrings = false)]
        public T Id { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<string>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}