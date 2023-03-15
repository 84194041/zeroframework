﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZeroFramework.DeviceCenter.Infrastructure.EntityFrameworks;

#nullable disable

namespace ZeroFramework.DeviceCenter.Infrastructure.Migrations
{
    [DbContext(typeof(DeviceCenterDbContext))]
    partial class DeviceCenterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Buyers", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid?>("BuyerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTimeOffset>("Expiration")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.ToTable("PaymentMethods", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.Device", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Coordinate")
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("LastOnlineTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Devices", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("DeviceGroups", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGrouping", b =>
                {
                    b.Property<long>("DeviceId")
                        .HasColumnType("bigint");

                    b.Property<int>("DeviceGroupId")
                        .HasColumnType("int");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("DeviceId", "DeviceGroupId");

                    b.HasIndex("DeviceGroupId");

                    b.ToTable("DeviceGroupings", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.MonitoringAggregate.MonitoringFactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<int>("Decimals")
                        .HasColumnType("int");

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("FactorCode")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Remarks")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("Unit")
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("FactorCode")
                        .IsUnique();

                    b.ToTable("MonitoringFactors", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Units")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.PermissionAggregate.PermissionGrant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("OperationName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.Property<Guid?>("ResourceGroupId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ResourceGroupId");

                    b.HasIndex("OperationName", "ProviderName", "ProviderKey", "ResourceGroupId", "TenantId")
                        .IsUnique();

                    b.ToTable("PermissionGrants", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProductAggregate.MeasurementUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("MeasurementUnits", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProductAggregate.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DataFormat")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Features")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NetType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NodeType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ProtocolType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("Sorting")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectGroups", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("Name", "TenantId")
                        .IsUnique();

                    b.ToTable("ResourceGroups", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceGrouping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ResourceGroupId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ResourceGroupId");

                    b.ToTable("ResourceGroupings", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Infrastructure.Idempotency.ClientRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Idempotency", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Infrastructure.IntegrationEvents.IntegrationEventLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EventTypeName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TimesSent")
                        .HasColumnType("int");

                    b.Property<string>("TransactionId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("IntegrationEventLogs", (string)null);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.PaymentMethod", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.Buyer", null)
                        .WithMany("PaymentMethods")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGroup", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGroup", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGrouping", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGroup", "DeviceGroup")
                        .WithMany()
                        .HasForeignKey("DeviceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");

                    b.Navigation("DeviceGroup");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.Buyer", null)
                        .WithMany()
                        .HasForeignKey("BuyerId");

                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.OwnsOne("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.ShippingAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.PermissionAggregate.PermissionGrant", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceGroup", null)
                        .WithMany()
                        .HasForeignKey("ResourceGroupId");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");

                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", "Project")
                        .WithMany("Groups")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceGrouping", b =>
                {
                    b.HasOne("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceGroup", null)
                        .WithMany()
                        .HasForeignKey("ResourceGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ZeroFramework.DeviceCenter.Domain.Aggregates.ResourceGroupAggregate.ResourceDescriptor", "Resource", b1 =>
                        {
                            b1.Property<Guid>("ResourceGroupingId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("ResourceId")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("ResourceId");

                            b1.Property<string>("ResourceType")
                                .IsRequired()
                                .HasMaxLength(255)
                                .HasColumnType("varchar(255)")
                                .HasColumnName("ResourceType");

                            b1.HasKey("ResourceGroupingId");

                            b1.ToTable("ResourceGroupings");

                            b1.WithOwner()
                                .HasForeignKey("ResourceGroupingId");
                        });

                    b.Navigation("Resource")
                        .IsRequired();
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.BuyerAggregate.Buyer", b =>
                {
                    b.Navigation("PaymentMethods");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.DeviceAggregate.DeviceGroup", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.Project", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("ZeroFramework.DeviceCenter.Domain.Aggregates.ProjectAggregate.ProjectGroup", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}