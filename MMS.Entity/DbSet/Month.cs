﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Entities.DbSet
{
    public class Month:BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public string AddedBy { get; set; }

        public Guid MessId { get; set; }
        public Mess Mess { get; set; }
    }
}