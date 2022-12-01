﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Resto.Models
{
    public class Menu
    {
        [Key]
        public int Id { get;  set; }

        public int importeTotal { get;  set; } = 0;

        public List<Plato> Platos { get;  set; }


    }
}
