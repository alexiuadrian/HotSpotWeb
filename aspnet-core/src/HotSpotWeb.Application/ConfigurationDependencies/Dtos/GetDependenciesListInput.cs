﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotSpotWeb.ConfigurationDependencies.Dtos
{
    public class GetDependenciesListInput
    {
        public string Filter { get; set; }
        public string Sorting { get; set; }
        public int MaxResultCount { get; set; }
    }
}