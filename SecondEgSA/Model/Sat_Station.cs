namespace SecondEgSA.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sat_Station
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Sat_Sta_ID { get; set; }

        public int? Sat_ID { get; set; }

        public int? Station_ID { get; set; }

        public virtual Satellite Satellite { get; set; }

        public virtual Station Station { get; set; }
    }
}
