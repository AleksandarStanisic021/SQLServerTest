
using static System.Net.Mime.MediaTypeNames;
using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using SQLDataLibrary;
using SQLDataLibrary.Models;


internal class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("*Data Test*");
		

		SQLCrud sql = new SQLCrud(GetConnectionString_1());

		var contacts = sql.GetAllContacts();
		foreach (var contact in contacts)
		{
			Console.WriteLine($"{contact.Id}: {contact.FirstName} {contact.LastName}");
		}
		Console.WriteLine("*******************");

		var fullContacta=sql.GetContactById(1);
        Console.WriteLine($"{fullContacta.BasicInfo.Id}: {fullContacta.BasicInfo.FirstName} {fullContacta.BasicInfo.LastName}");
        foreach (var email in fullContacta.EmailAddresses)
        {
            Console.WriteLine($"Email: {email.EmailAddress}");
        }
        foreach (var phone in fullContacta.PhoneNumbers)
        {
            Console.WriteLine($"Phone: {phone.PhoneNumber}");
        }
		

        Console.ReadLine();

		static string GetConnectionString_1(string connectionString = "Default")
		{
			string output;

			var builder = new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("app.json");
			var config = builder.Build();
			output= config.GetConnectionString(connectionString);
			return output;
		}
	}
}