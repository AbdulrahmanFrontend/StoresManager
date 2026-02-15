using StoresManager.DAL.Data;
using BLL;
using Core;
using StoresManager.DAL.Shared;
using System;
using System.Data;
using Core.Entities;

namespace StoresManager.BLL.Users
{
    public class clsUser : clsBaseBusiness
    {
        public clsUserEntity UserInfo { get; private set; }
        public clsUser()
        {
            UserInfo.UserID = -1;
            UserInfo.UserName = "";
            UserInfo.PasswordHash = "";
            UserInfo.Permissions = clsUserEntity.enPermissions.None;
            UserInfo.IsActive = false;
            Mode = clsEnums.enMode.enAddNew;
        }
        // private
        private clsUser(clsUserEntity UserInfo)
        {
            this.UserInfo.UserID = UserInfo.UserID;
            this.UserInfo.UserName = UserInfo.UserName;
            this.UserInfo.PasswordHash = UserInfo.PasswordHash;
            this.UserInfo.Permissions = (clsUserEntity.enPermissions)UserInfo.Permissions;
            this.UserInfo.IsActive = UserInfo.IsActive;
            Mode = clsEnums.enMode.enUpdate;
        }
        protected override bool _AddNew()
        {
            string hashedPassword = clsHashGenerator.Hash(this.UserInfo.PasswordHash);
            this.UserInfo.UserID = (int)clsUserData.AddNewUser(UserInfo);
            return (this.UserInfo.UserID != -1);
        }
        protected override bool _Update()
        {
            return clsUserData.UpdateUser(this.UserInfo);
        }
        private bool _IsUserNameUnique()
        {
            return clsUserData.IsUserNameUnique(UserInfo.UserName, UserInfo.UserID);
        }
        public clsResult ValidateName()
        {
            if(clsValidator.IsWithinLength(UserInfo.UserName, 2, 500) 
                && _IsUserNameUnique())
            {
                return clsResult.Success();
            }
            return clsResult.Failure("User name must be unique and between 2 and " +
                "500 characters.");

        }
        public clsResult ValidatePassword()
        {
            if(clsValidator.IsWithinLength(UserInfo.PasswordHash, 3, 50))
            {
                return clsResult.Success();
            }
            return clsResult.Failure("Password must be between 3 and 50 characters.");
        }
        public override clsResult Validate()
        {
            if (!ValidateName().IsSuccess)
            {
                return clsResult.Failure(ValidateName().Message);
            }
            if (!ValidatePassword().IsSuccess)
            {
                return clsResult.Failure(ValidatePassword().Message);
            }
            return clsResult.Success();
        }
        // public
        public static bool DeleteUser(int UserID)
        {
            if(!clsUserData.IsUserExistByUserID(UserID))
            {
                return false;
            }
            return clsUserData.DeleteUser(UserID);
        }
        public static bool IsUserExistByUserID(int UserID)
        {
            return clsUserData.IsUserExistByUserID(UserID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUserData.IsUserExistByUserName(UserName);
        }
        public static clsUser FindByUserID(int UserID)
        {
            clsUserEntity UserInfo = clsUserData.GetUserByUserID(UserID);
            if (UserInfo != null)
                return new clsUser(UserInfo);
            else
                return null;
        }
        public static clsUser FindByUserName(string UserName)
        {
            clsUserEntity UserInfo = clsUserData.GetUserByUserName(UserName);
            if (UserInfo != null)
                return new clsUser(UserInfo);
            else
                return null;
        }
        public static DataTable GetUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool RegisterTheAdmin(string Username, string Password)
        {
            if (!clsUserData.IsUsersTableEmpty())
                return false;

            clsUserEntity UserInfo = new clsUserEntity
            {
                UserID = -1,
                UserName = Username,
                PasswordHash = Password,
                Permissions = (clsUserEntity.enPermissions)(-1),
                IsActive = true
            };
            clsUser Admin = new clsUser(UserInfo);
            Admin.Mode = clsEnums.enMode.enAddNew;
            if (Admin.Save().IsSuccess)
                return true;
            return false;
        }
        public static bool Login(string Username, string Password)
        {
            clsUser user = new clsUser();
            user = FindByUserName(Username);
            if (user != null)
            {
                if (user.UserInfo.IsActive == true)
                    if (clsHashGenerator.Hash(Password) == user.UserInfo.PasswordHash) 
                        return true;
            }
            return false;
        }
        public bool AddNewUser(clsUser newUser)
        {
            if (UserInfo.Permissions != clsUserEntity.enPermissions.All)
            {
                Logger.LogError("You don`t have an access to add users!", null);
                return false;
            }
            if (newUser._AddNew())
                return true;
            return false;
        }
        public void AddPermission(clsUserEntity.enPermissions permissionToAdd)
        {
            UserInfo.Permissions |= permissionToAdd;
        }
        public void RemovePermission(clsUserEntity.enPermissions permissionToRemove)
        {
            UserInfo.Permissions &= ~permissionToRemove;
        }
        public bool HasPermission(clsUserEntity.enPermissions permissionToCheck)
        {
            return (UserInfo.Permissions & permissionToCheck) == permissionToCheck;
        }
    }
}
