/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Data.Enums;
using UserStatus = ICSLib.Authen.Data.Entities.UserStatus;

namespace ICSLib.Authen.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            //Cot identity cung phai set gia tri

            short sortOrder = 1;
            byte id = 1;

            #region UserStatus sedding
            id = 1;
            sortOrder = 1;
            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus()
                {
                    UserStatusId = id++,
                    UserStatusName = "Activated",
                    UserStatusDesc = "Đang hoạt động"
                },
                new UserStatus()
                {
                    UserStatusId = id++,
                    UserStatusName = "Inactive",
                    UserStatusDesc = "Chưa kích hoạt"
                },
                new UserStatus()
                {
                    UserStatusId = id++,
                    UserStatusName = "Suspend",
                    UserStatusDesc = "Tạm dừng"
                },
                new UserStatus()
                {
                    UserStatusId = id++,
                    UserStatusName = "Locked",
                    UserStatusDesc = "Bị chặn"
                }
            );
            #endregion

            #region Gender sedding
            id = 1;
            sortOrder = 1;
            modelBuilder.Entity<Gender>().HasData(
                new Gender()
                {
                    GenderId = id++,
                    GenderName = "Male",
                    GenderDesc = "Nam"
                },
                new Gender()
                {
                    GenderId = id++,
                    GenderName = "Female",
                    GenderDesc = "Nữ"
                }
            );
            #endregion

            #region RoleGroup sedding
            id = 1;
            sortOrder = 1;
            modelBuilder.Entity<RoleGroup>().HasData(
                new RoleGroup()
                {
                    RoleGroupId = 1,
                    RoleGroupName = "System administrator",
                    RoleGroupDesc = "Quản trị hệ thống",
                    SortOrder = sortOrder++,
                    StatusId = 1
                }
            );
            #endregion

            #region Role sedding
            id = 1;
            sortOrder = 1;
            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    ParentRoleId = 0,
                    LevelId = 1,
                    SortOrder = 1,
                    IsShow = 1,

                    Name = "System",
                    NormalizedName = "System",
                    Description = "Hệ thống",
                    Controler = "",
                    Action = "",
                    Icon = "",
                    StatusId = 1
                },
                    new Role()
                    {
                        Id = 2,
                        ParentRoleId = 1,
                        LevelId = 2,
                        SortOrder = 1,
                        IsShow = 1,

                        Name = "Authorization",
                        NormalizedName = "Authorization",
                        Description = "Phân quyền",
                        Controler = "",
                        Action = "",
                        Icon = "",
                        StatusId = 1
                    },
                        new Role()
                        {
                            Id = 3,
                            ParentRoleId = 2,
                            LevelId = 3,
                            SortOrder = 1,
                            IsShow = 1,

                            Name = "RoleGroup-Index",
                            NormalizedName = "RoleGroup-Index",
                            Description = "Quyền hệ thống",
                            Controler = "RoleGroup",
                            Action = "Index",
                            Icon = "",
                            StatusId = 1
                        },
                            new Role()
                            {
                                Id = 4,
                                ParentRoleId = 3,
                                LevelId = 4,
                                SortOrder = 1,
                                IsShow = 0,

                                Name = "RoleGroup-Create",
                                NormalizedName = "RoleGroup-Create",
                                Description = "Thêm quyền",
                                Controler = "RoleGroup",
                                Action = "Create",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 5,
                                ParentRoleId = 3,
                                LevelId = 4,
                                SortOrder = 2,
                                IsShow = 0,

                                Name = "RoleGroup-Edit",
                                NormalizedName = "RoleGroup-Edit",
                                Description = "Cập nhật quyền",
                                Controler = "RoleGroup",
                                Action = "Edit",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 6,
                                ParentRoleId = 3,
                                LevelId = 4,
                                SortOrder = 3,
                                IsShow = 0,

                                Name = "RoleGroup-Delete",
                                NormalizedName = "RoleGroup-Delete",
                                Description = "Xóa quyền",
                                Controler = "RoleGroup",
                                Action = "Delete",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 7,
                                ParentRoleId = 3,
                                LevelId = 4,
                                SortOrder = 4,
                                IsShow = 0,

                                Name = "RoleGroup-Details",
                                NormalizedName = "RoleGroup-Details",
                                Description = "Thông tin quyền",
                                Controler = "RoleGroup",
                                Action = "Details",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 8,
                                ParentRoleId = 3,
                                LevelId = 4,
                                SortOrder = 5,
                                IsShow = 0,

                                Name = "RoleGroup-AssignRole",
                                NormalizedName = "RoleGroup-AssignRole",
                                Description = "Cập nhật chức năng cho quyền",
                                Controler = "RoleGroup",
                                Action = "AssignRole",
                                Icon = "",
                                StatusId = 1
                            },
                        new Role()
                        {
                            Id = 9,
                            ParentRoleId = 2,
                            LevelId = 3,
                            SortOrder = 2,
                            IsShow = 1,

                            Name = "Role-Index",
                            NormalizedName = "Role-Index",
                            Description = "Chức năng",
                            Controler = "Role",
                            Action = "Index",
                            Icon = "",
                            StatusId = 1
                        },
                            new Role()
                            {
                                Id = 10,
                                ParentRoleId = 9,
                                LevelId = 4,
                                SortOrder = 1,
                                IsShow = 0,

                                Name = "Role-Create",
                                NormalizedName = "Role-Create",
                                Description = "Thêm Chức năng",
                                Controler = "Role",
                                Action = "Create",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 11,
                                ParentRoleId = 9,
                                LevelId = 4,
                                SortOrder = 2,
                                IsShow = 0,

                                Name = "Role-Edit",
                                NormalizedName = "Role-Edit",
                                Description = "Cập nhật Chức năng",
                                Controler = "Role",
                                Action = "Edit",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 12,
                                ParentRoleId = 9,
                                LevelId = 4,
                                SortOrder = 3,
                                IsShow = 0,

                                Name = "Role-Delete",
                                NormalizedName = "Role-Delete",
                                Description = "Xóa Chức năng",
                                Controler = "Role",
                                Action = "Delete",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 13,
                                ParentRoleId = 9,
                                LevelId = 4,
                                SortOrder = 4,
                                IsShow = 0,

                                Name = "Role-Details",
                                NormalizedName = "Role-Details",
                                Description = "Thông tin Chức năng",
                                Controler = "Role",
                                Action = "Details",
                                Icon = "",
                                StatusId = 1
                            },
                        new Role()
                        {
                            Id = 14,
                            ParentRoleId = 2,
                            LevelId = 3,
                            SortOrder = 3,
                            IsShow = 1,

                            Name = "User-Index",
                            NormalizedName = "User-Index",
                            Description = "Người dùng",
                            Controler = "User",
                            Action = "Index",
                            Icon = "",
                            StatusId = 1
                        },
                            new Role()
                            {
                                Id = 15,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 1,
                                IsShow = 0,

                                Name = "User-Create",
                                NormalizedName = "User-Create",
                                Description = "Thêm người dùng",
                                Controler = "User",
                                Action = "Create",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 16,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 2,
                                IsShow = 0,

                                Name = "User-Edit",
                                NormalizedName = "User-Edit",
                                Description = "Cập nhật người dùng",
                                Controler = "User",
                                Action = "Edit",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 17,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 3,
                                IsShow = 0,

                                Name = "User-Delete",
                                NormalizedName = "User-Delete",
                                Description = "Xóa người dùng",
                                Controler = "User",
                                Action = "Delete",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 18,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 4,
                                IsShow = 0,

                                Name = "User-Details",
                                NormalizedName = "User-Details",
                                Description = "Thông tin người dùng",
                                Controler = "User",
                                Action = "Details",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 19,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 5,
                                IsShow = 0,

                                Name = "User-AssignRoleGroup",
                                NormalizedName = "User-AssignRoleGroup",
                                Description = "Cấp quyền cho người dùng",
                                Controler = "User",
                                Action = "AssignRoleGroup",
                                Icon = "",
                                StatusId = 1
                            },
                            new Role()
                            {
                                Id = 20,
                                ParentRoleId = 14,
                                LevelId = 4,
                                SortOrder = 6,
                                IsShow = 0,

                                Name = "User-AssignRole",
                                NormalizedName = "User-AssignRole",
                                Description = "Cấp chức năng cho người dùng",
                                Controler = "User",
                                Action = "AssignRoleGroup",
                                Icon = "",
                                StatusId = 1
                            }
            );
            #endregion

            #region RoleGroupRole sedding
            id = 1;
            sortOrder = 1;
            modelBuilder.Entity<RoleGroupRole>().HasData(
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 1
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 2
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 3
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 4
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 5
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 6
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 7
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 8
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 9
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 10
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 11
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 12
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 13
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 14
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 15
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 16
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 17
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 18
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 19
                },
                new RoleGroupRole()
                {
                    RoleGroupId = 1,
                    RoleId = 20
                }
            );
            #endregion

            #region User sedding
            id = 1;
            sortOrder = 1;
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    UserName = "admin",
                    NormalizedUserName = "admin",
                    Email = "thew0102@gmail.com",
                    NormalizedEmail = "thew0102@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    SecurityStamp = string.Empty,
                    PhoneNumber = "0973214793",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    FirstName = "Thế",
                    LastName = "Vũ Văn",
                    FullName = "Vũ Văn Thế",
                    Avatar = string.Empty,
                    GenderId = 1,
                    DateOfBirth = new DateTime(1988, 2, 1),
                    Address = "Hà Nội",
                    Comments = string.Empty,
                    OAuthId = string.Empty,
                    OAuthName = string.Empty,
                    CrDateTime = DateTime.Now,
                    ActiveDateTime = DateTime.Now,
                    UserStatusId = 1
                }
            );
            #endregion

            #region UserRoleGroup sedding
            modelBuilder.Entity<UserRoleGroup>().HasData(
                new UserRoleGroup()
                {
                    UserId = 1,
                    RoleGoupId = 1
                }
            );
            #endregion

            #region IdentityUserRole sedding
            modelBuilder.Entity<IdentityUserRole<Int32>>().HasData(
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 1
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 2
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 3
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 4
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 5
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 6
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 7
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 8
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 9
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 10
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 11
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 12
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 13
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 14
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 15
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 16
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 17
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 18
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 19
                },
                new IdentityUserRole<Int32>()
                {
                    UserId = 1,
                    RoleId = 20
                }
            );
            #endregion
        }
    }
}
