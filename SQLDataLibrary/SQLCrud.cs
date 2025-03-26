using SQLDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataLibrary
{

	public class SQLCrud
	{
		SQLDataAccess db = new SQLDataAccess();
		private readonly string _connectionString;

		public SQLCrud(string connectionString)
		{
			_connectionString = connectionString;
		}

		public List<BasicContactModel> GetAllContacts()
		{
			string sql = "select Id, FirstName, LastName from dbo.Contacts";
			return db.LoadData<BasicContactModel, dynamic>(sql, new { }, _connectionString);
		}

		public FullContactModel GetContactById(int id)
        {
            string sql = "select Id, FirstName, LastName from dbo.Contacts where Id = @Id";
            FullContactModel output = new FullContactModel();
            output.BasicInfo = db.LoadData<BasicContactModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            if (output.BasicInfo == null)
            {
                throw new Exception("Contact not found");
                return null;
            }
         sql = @"select e.*
                 from dbo.EmailAddresses e
				 inner join dbo.ContactEmail ce on ce.EmailAddressId = e.Id
				 where ce.ContactId=@Id";

         output.EmailAddresses = db.LoadData<EmailAddressModel, dynamic>(sql, new { Id = id }, _connectionString);

            sql = @"select p.*
					from dbo.PhoneNumbers p
					inner join dbo.ContactPhoneNumbers cp on cp.PhoneNumberId = p.Id
					where cp.ContactId=@Id";

		 output.PhoneNumbers =db.LoadData<PhoneNumberModel, dynamic>(sql, new { Id = id }, _connectionString);	
         return output;

        }
    }
}
