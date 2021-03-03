using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Data
{
    public class UserDataAccess : DbDataAccess<User>
    {
        public override void Insert(User user)
        {
            var insertSqlScript = "Insert into Users (Login,Password) values (@login,@password)";
            using (var transaction = sqlConnection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = sqlConnection;
                command.CommandText = insertSqlScript;
                try
                {
                    command.Transaction = transaction;
                    var loginSqlParameter = command.CreateParameter();
                    loginSqlParameter.DbType = System.Data.DbType.String;
                    loginSqlParameter.Value = user.Login;
                    loginSqlParameter.ParameterName = "login";

                    command.Parameters.Add(loginSqlParameter);

                    var smsCodeSqlParameter = command.CreateParameter();
                    smsCodeSqlParameter.DbType = System.Data.DbType.String;
                    smsCodeSqlParameter.Value = user.Password;
                    smsCodeSqlParameter.ParameterName = "password";

                    command.Parameters.Add(smsCodeSqlParameter);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }
        public override ICollection<User> Select()
        {
            var selectSqlScript = $"Select * from Users";

            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var users = new List<User>();

            while (dataReader.Read())
            {
                users.Add(new User
                {
                    Login = dataReader["login"].ToString(),
                    Password = dataReader["password"].ToString(),
                });
            }

            dataReader.Close();
            command.Dispose();
            return users;
        }
        public override void Delete(User entity)
        {
            throw new NotImplementedException();
        }
        public override void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
