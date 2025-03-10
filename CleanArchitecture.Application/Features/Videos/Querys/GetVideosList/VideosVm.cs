﻿using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Videos.Querys.GetVideosList
{
    public class VideosVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StreamerId { get; set; }
        public virtual Streamer? Streamer { get; set; }
    }
}
