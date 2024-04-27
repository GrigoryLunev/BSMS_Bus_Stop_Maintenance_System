using System;
using System.Collections.Generic;
using BSCare.Models;
using Microsoft.EntityFrameworkCore;

namespace BSCare.Data;

public partial class BscareDbContext : DbContext
{
    public BscareDbContext()
    {
    }

    public BscareDbContext(DbContextOptions<BscareDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Stop> Stops { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BSCare_DB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Hebrew_CI_AI");

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.ExpensesId).HasName("PK__repair__6A5E71BFFE76A7E8");

            entity.ToTable("repair");

            entity.Property(e => e.ExpensesId).HasColumnName("expenses_Id");
            entity.Property(e => e.ActionPrice).HasColumnName("action_price");
            entity.Property(e => e.RepairAction)
                .HasMaxLength(500)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("repair_action");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.StopCode).HasColumnName("stop_code");

            entity.HasOne(d => d.Report).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.ReportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_repair_ToTable");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__report__779E68102E987255");

            entity.ToTable("report");

            entity.Property(e => e.ReportId).HasColumnName("report_Id");
            entity.Property(e => e.CloseDate)
                .HasColumnType("date")
                .HasColumnName("close_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.HazardType).HasColumnName("hazard_type");
            entity.Property(e => e.OpenDate)
                .HasColumnType("date")
                .HasColumnName("open_date");
            entity.Property(e => e.PicPath)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("pic_path");
            entity.Property(e => e.ReportSource).HasColumnName("report_source");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StopId).HasColumnName("stop_id");

            entity.HasOne(d => d.Stop).WithMany(p => p.Reports)
                .HasForeignKey(d => d.StopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_report_ToTable");
        });

        modelBuilder.Entity<Stop>(entity =>
        {
            entity.HasKey(e => e.StopCode).HasName("PK__tmp_ms_x__4162F3823D1B5D90");

            entity.ToTable("stops");

            entity.Property(e => e.StopCode)
                .ValueGeneratedNever()
                .HasColumnName("stop_code");
            entity.Property(e => e.LocationType).HasColumnName("location_type");
            entity.Property(e => e.ParentStation).HasColumnName("parent_station");
            entity.Property(e => e.StopDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stop_desc");
            entity.Property(e => e.StopId).HasColumnName("stop_id");
            entity.Property(e => e.StopLat).HasColumnName("stop_lat");
            entity.Property(e => e.StopLon).HasColumnName("stop_lon");
            entity.Property(e => e.StopName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("stop_name");
            entity.Property(e => e.ZoneId).HasColumnName("zone_id");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Table__779E681086056619");

            entity.ToTable("Table");

            entity.Property(e => e.ReportId).HasColumnName("report_Id");
            entity.Property(e => e.CloseDate)
                .HasColumnType("date")
                .HasColumnName("close_date");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("description");
            entity.Property(e => e.HazardType).HasColumnName("hazard_type");
            entity.Property(e => e.OpenDate)
                .HasColumnType("date")
                .HasColumnName("open_date");
            entity.Property(e => e.PicPath)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("pic_path");
            entity.Property(e => e.ReportSource).HasColumnName("report_source");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.StopId).HasColumnName("stop_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
