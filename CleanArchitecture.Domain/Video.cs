﻿using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain
{
    public class Video : BaseDomainModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
    }
}
