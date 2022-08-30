using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Badge2022EF.DAL.Entities
{ 
    public partial class CoursEntity
        {
        public int cid { get; set; } = int.MinValue;
        public string cnom { get; set; } = string.Empty;
        public virtual ICollection<ExamenEntity>? examens { get; set; }
        public virtual ICollection<FormationStockEntity>? formation { get; set; }

    }
}
