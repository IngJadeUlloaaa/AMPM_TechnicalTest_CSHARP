﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Model
{
    public class UpdateActiveProductsModel
    {
        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Existencias { get; set; }

        public string Opcion { get; set; }
    }
}
