using StoresManager.DAL.Data;
using StoresManager.DAL.Shared;
using System;
using System.Data;

namespace StoresManager.BLL.Users
{
    public class clsUser
    {
        [Flags]
        public enum enPermissions
        {
            None = 0,
            AdminsManagment = 1,
            UsersManagment = 2,
            ClientsManagment = 4,
            PersonalAccountManagment = 8,
            All = -1
        }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string PasswordHash { set; get; }
        public enPermissions Permissions { set; get; }
        public bool IsActive { set; get; }

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.PasswordHash = "";
            this.Permissions = enPermissions.None;
            this.IsActive = false;
            Mode = enMode.AddNew;
        }
        // private
        private clsUser(int UserID, string UserName, string PasswordHash, int Permissions, bool IsActive)
        {
            this.UserID = UserID;
            this.UserName = UserName;
            this.PasswordHash = PasswordHash;
            this.Permissions = (enPermissions)Permissions;
            this.IsActive = IsActive;
            Mode = enMode.Update;
        }
        private bool _AddNewUser()
        {
            string hashedPassword = clsHashGenerator.Hash(this.PasswordHash);
            this.UserID = (int)clsUserData.AddNewUser(this.UserName, hashedPassword, (int)this.Permissions, this.IsActive);
            return (this.UserID != -1);
        }
        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.PasswordHash, (int)this.Permissions, this.IsActive);
        }
        // public
        public static bool DeleteUser(int UserID)
        {
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
            string UserName = "";
            string PasswordHash = "";
            int Permissions = -1;
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserByUserID(UserID, ref UserName, ref PasswordHash, ref Permissions, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, UserName, PasswordHash, Permissions, IsActive);
            else
                return null;
        }
        public static clsUser FindByUserName(string UserName)
        {
            int UserID = -1;
            string PasswordHash = "";
            int Permissions = -1;
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserByUserName(ref UserID, UserName, ref PasswordHash, ref Permissions, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, UserName, PasswordHash, Permissions, IsActive);
            else
                return null;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }
        public static DataTable GetUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool RegisterTheAdmin(string Username, string Password)
        {
            if (!clsUserData.IsUsersTableEmpty())
                return false;
            clsUser Admin = new clsUser(-1, Username, Password, -1, true);
            Admin.Mode = enMode.AddNew;
            if (Admin.Save())
                return true;
            return false;
        }
        public static bool Login(string Username, string Password)
        {
            clsUser user = new clsUser();
            user = FindByUserName(Username);
            if (user != null)
            {
                if (user.IsActive == true)
                    if (clsHashGenerator.Hash(Password) == user.PasswordHash) return true;
            }
            return false;
        }
        public bool AddNewUser(clsUser newUser)
        {
            if (Permissions != enPermissions.All)
            {
                Logger.LogError("You don`t have an access to add users!", null);
                return false;
            }
            if (newUser._AddNewUser())
                return true;
            return false;
        }
        public void AddPermission(enPermissions permissionToAdd)
        {
            Permissions |= permissionToAdd;
        }
        public void RemovePermission(enPermissions permissionToRemove)
        {
            Permissions &= ~permissionToRemove;
        }
        public bool HasPermission(enPermissions permissionToCheck)
        {
            return (Permissions & permissionToCheck) == permissionToCheck;
        }
    }
}
