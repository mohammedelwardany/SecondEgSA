using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SecondEgSA.Model
{
    public partial class EgSa : DbContext
    {
        public EgSa()
            : base("name=EgSa")
        {
        }

        public virtual DbSet<ACK> ACKs { get; set; }
        public virtual DbSet<CoM_Param> CoM_Param { get; set; }
        public virtual DbSet<Command> Commands { get; set; }
        public virtual DbSet<param_TB_type> param_TB_type { get; set; }
        public virtual DbSet<Param_Value> Param_Value { get; set; }
        public virtual DbSet<plan_> plan_ { get; set; }
        public virtual DbSet<plan_result> plan_result { get; set; }
        public virtual DbSet<Sat_Station> Sat_Station { get; set; }
        public virtual DbSet<Satellite> Satellites { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<Subsystem> Subsystems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACK>()
                .Property(e => e.ack_dec)
                .IsUnicode(false);

            //modelBuilder.Entity<CoM_Param>()
            //    .HasMany(e => e.Param_Value)
            //    .WithRequired(e => e.CoM_Param)
            //    .HasForeignKey(e => new { e.parm_ID, e.com_id, e.sub_ID })
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Command>()
                .Property(e => e.com_description)
                .IsUnicode(false);

            modelBuilder.Entity<Command>()
                .Property(e => e.sensor_name)
                .IsUnicode(false);

            //modelBuilder.Entity<Command>()
            //    .HasMany(e => e.CoM_Param)
            //    .WithRequired(e => e.Command)
            //    .HasForeignKey(e => new { e.com_id, e.sub_Id })
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Command>()
                .HasMany(e => e.plan_)
                .WithOptional(e => e.Command)
                .HasForeignKey(e => new { e.com_ID, e.sub_ID });

            //modelBuilder.Entity<param_TB_type>()
            //    .Property(e => e.param_type)
            //    .IsUnicode(false);

            //modelBuilder.Entity<param_TB_type>()
            //    .HasMany(e => e.CoM_Param)
            //    .WithOptional(e => e.param_TB_type)
            //    .HasForeignKey(e => e.param_type);

            //modelBuilder.Entity<Param_Value>()
            //    .Property(e => e.description)
            //    .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.EX_time)
                .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.para1_desc)
                .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.para2_desc)
                .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.para3_desc)
                .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.delay)
                .IsUnicode(false);

            modelBuilder.Entity<plan_>()
                .Property(e => e.repeat)
                .IsUnicode(false);

            //modelBuilder.Entity<plan_>()
            //    .HasMany(e => e.plan_result)
            //    .WithRequired(e => e.plan_)
            //    .HasForeignKey(e => new { e.plan_id, e.sequance_id })
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<plan_result>()
                .Property(e => e.value_result)
                .IsUnicode(false);

            modelBuilder.Entity<Satellite>()
                .Property(e => e.Sat_name)
                .IsUnicode(false);

            modelBuilder.Entity<Satellite>()
                .Property(e => e.Orbit_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Station_name)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Station_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Station>()
                .Property(e => e.Longitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Station>()
                .Property(e => e.Latitude)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Station>()
                .HasMany(e => e.Sat_Station)
                .WithOptional(e => e.Station)
                .HasForeignKey(e => e.Station_ID);

            modelBuilder.Entity<Subsystem>()
                .Property(e => e.Sub_name)
                .IsUnicode(false);

            modelBuilder.Entity<Subsystem>()
                .Property(e => e.Sub_type)
                .IsUnicode(false);
        }
    }
}
