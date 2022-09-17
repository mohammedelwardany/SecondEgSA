namespace SecondEgSA.Model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class plan_
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public plan_()
        {
            plan_result = new HashSet<plan_result>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int plan_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int squn_command { get; set; }

        public int com_ID { get; set; }

        public string EX_time { get; set; }

        public int sub_ID { get; set; }

        public string para1_desc { get; set; }

        public string para2_desc { get; set; }

        public string para3_desc { get; set; }

        [StringLength(2)]
        public string delay { get; set; }

        public int? ack_id { get; set; }

        [StringLength(2)]
        public string repeat { get; set; }

        public virtual ACK ACK { get; set; }

        public virtual Command Command { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<plan_result> plan_result { get; set; }
    }
}
