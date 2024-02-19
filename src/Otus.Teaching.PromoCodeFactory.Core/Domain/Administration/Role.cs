﻿using System.Collections.Generic; //!!!

namespace Otus.Teaching.PromoCodeFactory.Core.Domain.Administration
{
    public class Role
        : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; } //!!!
    }
}