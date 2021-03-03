using Models;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Data
{
    public class ProfileDataAccess : DbDataAccess<Profile>
    {
        public override void Insert(Profile profile)
        {
            var insertSqlScript = "Insert into Users (fullName,email,pathToAvatar) values (@fullName,@email,@pathToAvatar)";
            using (var transaction = sqlConnection.BeginTransaction())
            using (var command = factory.CreateCommand())
            {
                command.Connection = sqlConnection;
                command.CommandText = insertSqlScript;
                try
                {
                    command.Transaction = transaction;
                    var fullNameSqlParameter = command.CreateParameter();
                    fullNameSqlParameter.DbType = System.Data.DbType.String;
                    fullNameSqlParameter.Value = profile.FullName;
                    fullNameSqlParameter.ParameterName = "fullName";

                    command.Parameters.Add(fullNameSqlParameter);

                    var emailSqlParameter = command.CreateParameter();
                    emailSqlParameter.DbType = System.Data.DbType.String;
                    emailSqlParameter.Value = profile.Email;
                    emailSqlParameter.ParameterName = "email";

                    command.Parameters.Add(emailSqlParameter);

                    var pathToAvatarSqlParameter = command.CreateParameter();
                    pathToAvatarSqlParameter.DbType = System.Data.DbType.String;
                    pathToAvatarSqlParameter.Value = profile.Email;
                    pathToAvatarSqlParameter.ParameterName = "pathToAvatar";

                    command.Parameters.Add(pathToAvatarSqlParameter);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException)
                {
                    transaction.Rollback();
                }
            }
        }

        public override ICollection<Profile> Select()
        {
            var selectSqlScript = $"Select * from Profiles";

            var command = factory.CreateCommand();
            command.Connection = sqlConnection;
            command.CommandText = selectSqlScript;
            var dataReader = command.ExecuteReader();

            var profiles = new List<Profile>();

            while (dataReader.Read())
            {
                profiles.Add(new Profile
                {
                    FullName = dataReader["fullName"].ToString(),
                    Email = dataReader["email"].ToString(),
                    PathToAvatar = dataReader["pathToAvatar"].ToString()
                });
            }

            dataReader.Close();
            command.Dispose();
            return profiles;
        }

        public override void Update(Profile entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(Profile entity)
        {
            throw new NotImplementedException();
        }
    }
}
