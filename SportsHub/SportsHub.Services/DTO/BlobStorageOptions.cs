﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Services.DTO
{
    public class BlobStorageOptions
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
