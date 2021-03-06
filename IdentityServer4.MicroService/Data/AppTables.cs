﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer4.MicroService
{
    #region tables
    public class AppUser : IdentityUser<long>
    {
        public long ParentUserID { get; set; }

        public string LineageIDs { get; set; }

        [NotMapped()]
        public string Lineage { get; set; }

        public string Avatar { get; set; }

        /// <summary>
        /// 是否已删除账号
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual List<AppUserRole> Roles { get; } = new List<AppUserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual List<AppUserClaim> Claims { get; } = new List<AppUserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual List<AppUserLogin> Logins { get; } = new List<AppUserLogin>();

        /// <summary>
        /// Navigation property for this users token accounts.
        /// </summary>
        public virtual List<AppUserToken> Tokens { get; } = new List<AppUserToken>();

        #region 2017-11-7 新增
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 联系地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        public double Stature { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        #endregion

        /// <summary>
        /// 2017-11-13 管理file
        /// </summary>
        public virtual List<AspNetUserFile> Files { get; } = new List<AspNetUserFile>();

        /// <summary>
        /// 2017-12-11 所属租户
        /// </summary>
        public virtual List<AspNetUserTenant> Tenants { get; } = new List<AspNetUserTenant>();
    }

    public class AppRole : IdentityRole<long>
    {
        /// <summary>
        /// Navigation property for the claims this role belongs to.
        /// </summary>
        public virtual List<AppRoleClaim> Claims { get; } = new List<AppRoleClaim>();
    }

    public class AppRoleClaim : IdentityRoleClaim<long>
    {
    }

    public class AppUserRole : IdentityUserRole<long>
    {
    }

    public class AppUserClaim : IdentityUserClaim<long>
    {
    }

    public class AppUserLogin : IdentityUserLogin<long>
    {
    }

    public class AppUserToken : IdentityUserToken<long>
    {
    }

    /// <summary>
    /// relation between User and Client
    /// </summary>
    [Table("AspNetUserClients")]
    public class AspNetUserClient
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        // 外键
        public long ClientId { get; set; }
    }

    [Table("AspNetUserFiles")]
    public class AspNetUserFile
    {
        public long Id { get; set; }

        public long AppUserId { get; set; }

        /// <summary>
        /// 0图片
        /// 1视频
        /// 2文档
        /// </summary>
        public FileTypes FileType { get; set; }

        public string Files { get; set; }
    }

    /// <summary>
    ///  relation between User and Tenant
    /// </summary>
    [Table("AspNetUserTenants")]
    public class AspNetUserTenant
    {
        public long Id { get; set; }

        public long AppUserId { get; set; }

        // 外键
        public long AppTenantId { get; set; }
    }

    /// <summary>
    /// relation between User and ApiResource
    /// </summary>
    [Table("AspNetUserApiResources")]
    public class AspNetUserApiResource
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        // 外键
        public long ApiResourceId { get; set; }
    }

    public enum FileTypes
    {
        Image = 0,
        Video = 1,
        Doc = 2
    }
    #endregion

    public class AppConstant
    {
        /// <summary>
        /// https://identityserver4.readthedocs.io/en/release/topics/add_apis.html
        /// Campaign.Core.IdentityServer.AuthenticationScheme
        /// </summary>
        public const string AppAuthenScheme = "token";

        /// <summary>
        /// 策略
        /// 对应Identity的ClaimType
        /// </summary>
        public class ClaimTypes
        {
            /// <summary>
            /// for User
            /// </summary>
            public const string UserPermission = "permission";


            /// <summary>
            /// for client
            /// </summary>
            public const string ClientScope = "scope";
        }

        /// <summary>
        /// 对应ClaimTypes.Scope
        /// Client的Scope权限定义
        /// 格式：resourceName.scopeName
        /// 对应Identity的ClaimValue
        /// </summary>
        public class ClientScopes
        {
            public const string Create = "campaign.core.identity.create";
            public const string Read = "campaign.core.identity.read";
            public const string Update = "campaign.core.identity.update";
            public const string Delete = "campaign.core.identity.delete";
            public const string Approve = "campaign.core.identity.approve";
            public const string Reject = "campaign.core.identity.reject";
            public const string Upload = "campaign.core.identity.upload";
            public const string All = "campaign.core.identity.all";
        }

        /// <summary>
        /// 对应ClaimTypes.Permission
        /// User的Permission权限定义
        /// </summary>
        public class UserPermissions
        {
            public const string Create = "create";
            public const string Read = "read";
            public const string Update = "update";
            public const string Delete = "delete";
            public const string Approve = "approve";
            public const string Reject = "reject";
            public const string Upload = "upload";
            public const string All = "all";
        }

        /// <summary>
        /// 角色
        /// </summary>
        public class Roles
        {
            /// <summary>
            ///  用户
            /// </summary>
            public const string Users = "users";
            public const string UsersNormalizedName = "用户";

            /// <summary>
            /// 合作商
            /// </summary>
            public const string Partners = "partners";
            public const string PartnersNormalizedName = "合作商";

            /// <summary>
            /// 开发者
            /// </summary>
            public const string Developer = "developer";
            public const string DeveloperNormalizedName = "开发者";

            /// <summary>
            /// 管理员
            /// </summary>
            public const string Administrators = "administrators";
            public const string AdministratorsNormalizedName = "管理员";

            /// <summary>
            /// 艺人
            /// </summary>
            public const string Star = "star";
            public const string StarNormalizedName = "艺人";
        }

        public class RedisKeys
        {
            #region 电话号码
            /// <summary>
            /// （缓存KEY）
            /// 24小时内发送短信验证码的次数计数
            /// Key的格式：{手机号}
            /// Value的格式： 次数，调用SDK的increase方法即可，每次加1
            /// </summary>
            public const string Limit_24Hour_Verify_Phone = "PhoneVerifyCode-Limit24Hour:";

            /// <summary>
            /// 24小时内发送短信验证码的上限
            /// </summary>
            public const int Limit_24Hour_Verify_MAX_Phone = 10;

            /// <summary>
            /// （缓存KEY）
            /// Key的格式：{手机号}
            /// Value的格式：当前时间的tick            
            /// </summary>
            public const string LastTime_SendCode_Phone = "PhoneVerifyCode-LastSend:";

            /// <summary>
            /// 每次发送验证码的最小间隔时长（秒）
            /// </summary>
            public const int MinimumTime_SendCode_Phone = 60;

            /// <summary>
            /// （缓存KEY）
            /// Key的格式：{手机号} + ":" + {验证码}
            /// Value的格式：任意
            /// </summary>
            public const string VerifyCode_Phone = "PhoneVerifyCode:";

            /// <summary>
            /// 发送手机验证码后，该验证码在服务器的有效时长（秒）
            /// </summary>
            public const int VerifyCode_Expire_Phone = 300;
            #endregion

            #region 邮箱地址
            /// <summary>
            /// （缓存KEY）
            /// 24小时内发送邮件验证码的次数计数
            /// Key的格式：{手机号}
            /// Value的格式： 次数，调用SDK的increase方法即可，每次加1
            /// </summary>
            public const string Limit_24Hour_Verify_Email = "EmailVerifyCode-Limit24Hour:";

            /// <summary>
            /// 24小时内发送邮件验证码的上限
            /// </summary>
            public const int Limit_24Hour_Verify_MAX_Email = 999999;

            /// <summary>
            /// （缓存KEY）
            /// Key的格式：{邮箱地址}
            /// Value的格式：当前时间的tick            
            /// </summary>
            public const string LastTime_SendCode_Email = "EmailVerifyCode-LastSend:";

            /// <summary>
            /// 每次发送验证码的最小间隔时长（秒）
            /// </summary>
            public const int MinimumTime_SendCode_Email = 60;

            /// <summary>
            /// （缓存KEY）
            /// Key的格式：{邮箱地址} + ":" + {验证码}
            /// Value的格式：任意
            /// </summary>
            public const string VerifyCode_Email = "EmailVerifyCode:";

            /// <summary>
            /// 发送邮箱验证码后，该验证码在服务器的有效时长（秒）
            /// </summary>
            public const int VerifyCode_Expire_Email = 1800;
            #endregion
        }

        public class AzureApiManagementConsts
        {
            /// <summary>
            /// 
            /// </summary>
            public const string Host = "Azure:ApiManagement:Host";

            /// <summary>
            /// 
            /// </summary>
            public const string ApiId = "Azure:ApiManagement:ApiId";

            /// <summary>
            /// 
            /// </summary>
            public const string ApiKey = "Azure:ApiManagement:ApiKey";

            /// <summary>
            /// 
            /// </summary>
            public const string AuthorizationServerId = "Azure:ApiManagement:AuthorizationServerId";

            /// <summary>
            /// 
            /// </summary>
            public const string ProductId = "Azure:ApiManagement:ProductId";

            /// <summary>
            /// 
            /// </summary>
            public const string PortalUris = "Azure:ApiManagement:PortalUris";
        }
    }
}
