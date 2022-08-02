using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WebApi.Models.Data;
using WebApi.Services.Interface;

namespace WebApi.Services.Instance
{
    public class ContactInfoService : IContactInfoService
    {
        private readonly string _connectString;

        public ContactInfoService(IConfiguration configuration)
        {
            _connectString = configuration.GetValue<string>("ConnectionStrings:MsSql");
        }

        public ContactInfo Query(long id)
        {
            try
            {
                var sbSQL = new StringBuilder();
                sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                sbSQL.AppendLine("WHERE ContactInfoID=@ContactInfoID");
                sbSQL.AppendLine("ORDER BY ContactInfoID DESC");

                using (var db = new SqlConnection(_connectString))
                {
                    return db.QueryFirstOrDefault<ContactInfo>(sbSQL.ToString(), new { ContactInfoID = id });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ContactInfo> Query(Dictionary<string, object> dicParams)
        {
            try
            {
                var sbSQL = new StringBuilder();
                sbSQL.AppendLine("SELECT * FROM Tbl_ContactInfo");
                sbSQL.AppendLine("WHERE 1 = 1");

                #region [Query Condition]
                foreach (var key in dicParams.Keys)
                {
                    switch (key)
                    {
                        case "Name":
                            sbSQL.AppendLine("AND Name = @Name");
                            break;

                        case "Nickname":
                            sbSQL.AppendLine("AND Nickname LIKE @Nickname");
                            break;

                        case "Gender":
                            sbSQL.AppendLine("AND Gender = @Gender");
                            break;
                    }
                }
                #endregion

                #region [Order]
                sbSQL.AppendLine("ORDER BY ContactInfoID DESC");
                #endregion

                #region[Paging]
                sbSQL.AppendLine("OFFSET @RowStart ROWS FETCH NEXT @RowLength ROWS ONLY");
                #endregion

                using (var db = new SqlConnection(_connectString))
                {
                    return db.Query<ContactInfo>(sbSQL.ToString(), dicParams);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(ContactInfo objContactInfo)
        {
            try
            {
                var sbSQL = new StringBuilder();
                sbSQL.AppendLine("INSERT INTO dbo.Tbl_ContactInfo (Name, Nickname, Gender, Age, PhoneNo, Address)");
                sbSQL.AppendLine("VALUES (@Name, @Nickname, @Gender, @Age, @PhoneNo, @Address)");
                sbSQL.AppendLine("SELECT SCOPE_IDENTITY()");

                using (var db = new SqlConnection(_connectString))
                {
                    objContactInfo.ContactInfoID = db.ExecuteScalar<long?>(sbSQL.ToString(), objContactInfo) ?? 0;
                    return objContactInfo.ContactInfoID > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(ContactInfo objContactInfo)
        {
            try
            {
                var sbSQL = new StringBuilder();
                sbSQL.AppendLine("UPDATE dbo.Tbl_ContactInfo SET");
                sbSQL.AppendLine("Name = @Name, Nickname = @Nickname, Gender = @Gender, Age = @Age, PhoneNo = @PhoneNo, Address = @Address, UpdateTime = GETDATE()");
                sbSQL.AppendLine("WHERE ContactInfoID = @ContactInfoID");

                using (var db = new SqlConnection(_connectString))
                {
                    return db.Execute(sbSQL.ToString(), objContactInfo) > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(IEnumerable<long> liID)
        {
            try
            {
                var sbSQL = new StringBuilder();
                sbSQL.AppendLine("DELETE FROM dbo.Tbl_ContactInfo");
                sbSQL.AppendLine("WHERE ContactInfoID IN @ContactInfoIDs");

                using (var db = new SqlConnection(_connectString))
                {
                    return db.Execute(sbSQL.ToString(), new { ContactInfoIDs = liID }) > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
