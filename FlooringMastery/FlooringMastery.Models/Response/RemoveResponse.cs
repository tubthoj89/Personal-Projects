﻿using FlooringMastery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Response
{
    public class RemoveResponse : Response
    {
        public Orders Order { get; set; }
    }
}