﻿/*
    Author: Vu.The
    Email: thew0102@gmail.com
    Date: December 18, 2021
 */

using ICSLib.Authen.Data.Entities;
using ICSLib.Authen.Models.RoleClaims;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ICSLib.Authen.Models.Roles
{
    public class RoleViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { set; get; }
        [Display(Name = "Controller")]
        public string Controler { set; get; }
        [Display(Name = "Action")]
        public string Action { set; get; }
        [Display(Name = "Icon")]
        public string Icon { set; get; }
        [Display(Name = "Thứ tự")]
        public short SortOrder { set; get; }
        [Display(Name = "Hiển thị")]
        public byte IsShow { set; get; }
        [Display(Name = "Chức năng cha")]
        public int ParentRoleId { set; get; }
        [Display(Name = "Level")]
        public byte LevelId { set; get; }
        [Display(Name = "Trạng thái")]
        public byte StatusId { set; get; }


        [Display(Name = "Chọn")]
        public bool Selected { set; get; }
        [Display(Name = "Claims")]
        public List<RoleClaimViewModel> Claims { get; set; }

        public RoleViewModel() { 
        
        }

        public RoleViewModel(Role appRole) {
            Id = appRole.Id;
            Name = appRole.Name;
            Description = appRole.Description;
            Controler = appRole.Controler;
            Action = appRole.Action;
            Icon = appRole.Icon;
            SortOrder = appRole.SortOrder;
            IsShow = appRole.IsShow;
            ParentRoleId = appRole.ParentRoleId;
            LevelId = appRole.LevelId;
            StatusId = appRole.StatusId;
        }

        public void addClaim(RoleClaimViewModel claim) {
            if (Claims == null) Claims = new List<RoleClaimViewModel>();
            Claims.Add(claim);
        }
    }
}
