﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Model.Models
{
    public  class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public int DisplayOrder { get; set; }
    }
}
