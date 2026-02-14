using StoresManager.DAL.Shared;
using System;
using System.Data;
using System.Data.SqlClient;

namespace StoresManager.DAL.Data
{
    public class clsUserData
    {
        public static bool GetUserByID(int UserID, ref string UserName, ref string PasswordHash, ref int Permissions, ref bool IsActive)
        {
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", UserID)
            };

            DataRow row = DbHelper.GetFirstRow(query, parameters);

            if (row != null)
            {
                UserName = row["UserName"].ToString();
                PasswordHash = row["PasswordHash"].ToString();
                Permissions = Convert.ToInt32(row["Permissions"]);
                IsActive = Convert.ToBoolean(row["IsActive"]);
                return true;
            }

            return false;
        }
        public static bool GetUserByUserID(int UserID, ref string UserName, ref string PasswordHash, ref int Permissions, ref bool IsActive)
        {
            return GetUserByID(UserID, ref UserName, ref PasswordHash, ref Permissions, ref IsActive);
        }
        public static bool GetUserByUserName(ref int UserID, string UserName, ref string PasswordHash, ref int Permissions, ref bool IsActive)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", UserName)
            };

            DataRow row = DbHelper.GetFirstRow(query, parameters);

            if (row != null)
            {
                UserID = Convert.ToInt32(row["UserID"]);
                PasswordHash = row["PasswordHash"].ToString();
                Permissions = Convert.ToInt32(row["Permissions"]);
                IsActive = Convert.ToBoolean(row["IsActive"]);
                return true;
            }

            return false;
        }
        public static int AddNewUser(string UserName, string PasswordHash, int Permissions, bool IsActive)
        {
            string query = @"INSERT INTO Users (UserName, PasswordHash, Permissions, IsActive)
                             VALUES (@UserName, @PasswordHash, @Permissions, @IsActive);
                             SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@PasswordHash", PasswordHash),
                new SqlParameter("@Permissions", Permissions),
                new SqlParameter("@IsActive", IsActive)
            };

            object result = DbHelper.GetScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out int insertedID))
                return insertedID;

            return -1;
        }
        public static bool UpdateUser(int UserID, string UserName, string PasswordHash, int Permissions, bool IsActive)
        {
            string query = @"UPDATE Users SET 
                                UserName = @UserName,
                                PasswordHash = @PasswordHash,
                                Permissions = @Permissions,
                                IsActive = @IsActive
                             WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", UserID),
                new SqlParameter("@UserName", UserName),
                new SqlParameter("@PasswordHash", PasswordHash),
                new SqlParameter("@Permissions", Permissions),
                new SqlParameter("@IsActive", IsActive)
            };

            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool DeleteUser(int UserID)
        {
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", UserID)
            };

            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool IsUserExist(int UserID)
        {
            string query = "SELECT 1 FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", UserID)
            };

            object result = DbHelper.GetScalar(query, parameters);
            return result != null;
        }
        public static bool IsUsersTableEmpty()
        {
            string query = "SELECT TOP 1 1 FROM Users";
            object result = DbHelper.GetScalar(query);
            return result == null;
        }
        public static bool IsUserExistByUserID(int UserID)
        {
            return IsUserExist(UserID);
        }
        public static bool IsUserExistByUserName(string UserName)
        {
            string query = "SELECT 1 FROM Users WHERE UserName = @UserName";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", UserName)
            };

            object result = DbHelper.GetScalar(query, parameters);
            return result != null;
        }
        public static DataTable GetAllUsers()
        {
            string query = "SELECT * FROM Users";
            return DbHelper.GetDataTable(query);
        }
    }
}
