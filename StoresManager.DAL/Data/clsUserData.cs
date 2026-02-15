using Core.Entities;
using StoresManager.DAL.Shared;
using System; 
using System.Data;
using System.Data.SqlClient;

namespace StoresManager.DAL.Data
{
    public class clsUserData
    {
        public static clsUserEntity GetUserByUserID(int UserID)
        {
            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID}
            };

            DataRow row = DbHelper.GetFirstRow(query, parameters);

            if (row != null)
            {
                return new clsUserEntity
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    UserName = row["UserName"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString(),
                    Permissions = 
                    (clsUserEntity.enPermissions)Convert.ToInt32(row["Permissions"]),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                };
            }

            return null;
        }
        public static clsUserEntity GetUserByUserName(string UserName)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 500) 
                { Value = UserName }
            };

            DataRow row = DbHelper.GetFirstRow(query, parameters);

            if (row != null)
            {
                return new clsUserEntity
                {
                    UserID = Convert.ToInt32(row["UserID"]),
                    UserName = row["UserName"].ToString(),
                    PasswordHash = row["PasswordHash"].ToString(),
                    Permissions = 
                    (clsUserEntity.enPermissions)Convert.ToInt32(row["Permissions"]),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                };
            }

            return null;
        }
        public static int AddNewUser(clsUserEntity UserInfo)
        {
            string query = @"INSERT INTO Users (UserName, PasswordHash, Permissions)
                             VALUES (@UserName, @PasswordHash, @Permissions);
                             SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 500)
                { Value = UserInfo.UserName },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 50) 
                { Value = UserInfo.PasswordHash },
                new SqlParameter("@Permissions", SqlDbType.Int) 
                { Value = UserInfo.Permissions },
            };

            object result = DbHelper.GetScalar(query, parameters);

            if (result != null && int.TryParse(result.ToString(), out int insertedID))
                return insertedID;

            return -1;
        }
        public static bool UpdateUser(clsUserEntity UserInfo)
        {
            string query = @"UPDATE Users SET 
                                UserName = @UserName,
                                PasswordHash = @PasswordHash,
                                Permissions = @Permissions,
                                IsActive = @IsActive
                             WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserInfo.UserID },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 500)
                { Value = UserInfo.UserName },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 50)
                { Value = UserInfo.PasswordHash },
                new SqlParameter("@Permissions", SqlDbType.Int)
                { Value = UserInfo.Permissions },
                new SqlParameter("@IsActive", SqlDbType.Bit) 
                { Value = UserInfo.IsActive },
            };

            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool DeleteUser(int UserID)
        {
            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID }
            };

            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
        public static bool IsUserExist(int UserID)
        {
            string query = "SELECT 1 FROM Users WHERE UserID = @UserID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID }
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
                new SqlParameter("@UserName", SqlDbType.NVarChar, 500)
                { Value = UserName },
            };

            object result = DbHelper.GetScalar(query, parameters);
            return result != null;
        }
        public static DataTable GetAllUsers()
        {
            string query = "SELECT * FROM Users";
            return DbHelper.GetDataTable(query);
        }
        public static bool IsUserNameUnique(string UserName, int UserID)
        {
            string Query = $"SELECT IsFound = 1 FROM Users WHERE UserName = @UserName " +
                $"AND UserID != @UserID";
            SqlParameter[] Parameters = new SqlParameter[]
            {
                new SqlParameter(@"@UserName", SqlDbType.NVarChar, 500)
                { Value = UserName },
                new SqlParameter("@UserID", SqlDbType.Int) { Value = UserID },
            };
            object Result = DbHelper.GetScalar(Query, Parameters);
            return Result == null;
        }
    }
}
