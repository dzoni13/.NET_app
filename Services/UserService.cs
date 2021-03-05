using IdeaTrading.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace IdeaTrading.Services
{
    public class UserService : IUserService
    {
        private IDatabaseService databaseService;
        
        public UserService()
        {
            databaseService = new DatabaseService();
        }

        public int CreateUser(User user)
        {
            string query = "Insert into [User] (FirstName, LastName, Address, DateOfEmployment, City, Position, Phone) values(@firstName, @lastName, @address, @dateOfEmployment, @city, @position, @phone)";
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@firstName", user.FirstName);
            parameters.Add("@lastName", user.LastName);
            parameters.Add("@address", user.Address);
            parameters.Add("@dateOfEmployment", user.DateOfEmployment);
            parameters.Add("@position", user.Position);
            parameters.Add("@phone", user.Phone);
            parameters.Add("@city", user.City);
            int id = databaseService.Create(query, parameters);
            return id;
        }

        public void DeleteUser(int id)
        {
            string query = "Delete from [User] where ID=@id";
            databaseService.Delete(query, id);                       
        }

        public void EditUser(User user)
        {
            string query = "Update [User] SET FirstName=@firstName, LastName=@lastName, Address=@address, DateOfEmployment=@dateOfEmployment, City=@city, Position=@position, Phone=@phone where ID=@id";

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@firstName", user.FirstName);
            parameters.Add("@lastName", user.LastName);
            parameters.Add("@address", user.Address);
            parameters.Add("@dateOfEmployment", user.DateOfEmployment);
            parameters.Add("@position", user.Position);
            parameters.Add("@phone", user.Phone);
            parameters.Add("@city", user.City);
            parameters.Add("@id", user.Id);
            databaseService.Edit(query, parameters);                                   
        }

        public User GetUser(int id)
        {

            string query = "Select * from [User] where ID=@id";
            DataTable dt = databaseService.Get(query, id);
            IEnumerable<User> users = ConvertDataTableToUsers(dt);
            return users.First();
        }

        public IEnumerable<User> GetUsers()
        {
            string query = "Select * from [User]";
            DataTable dt = databaseService.GetAll(query);
            IEnumerable<User> users = ConvertDataTableToUsers(dt);
            return users;
        }

        public IEnumerable<object> GetCities()
        {
            string query = "SELECT City, COUNT(*) FROM [User] GROUP BY City";
            DataTable dt = databaseService.GetAll(query);
            IEnumerable<object> cities = ConvertDataTableToObjects(dt);
            return cities;
        }

        private IEnumerable<User> ConvertDataTableToUsers(DataTable dataTable)
        {
            List<User> users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User();
                user.Id = (int)row["ID"];
                user.FirstName = (string)row["FirstName"];
                user.LastName = (string)row["LastName"];
                user.Address = (string)row["Address"];
                user.City = (string)row["City"];
                if (!row.IsNull("DateOfEmployment"))
                    user.DateOfEmployment = Convert.ToDateTime(row["DateOfEmployment"]);
                
                user.Position = (string)row["Position"];
                user.Phone = (string)row["Phone"];
                users.Add(user);
            }
            return users;
        }

        private IEnumerable<object> ConvertDataTableToObjects(DataTable dataTable)
        {
            List<object> cities = new List<object>();
            foreach (DataRow row in dataTable.Rows)
            {
                object city = new {name = row.ItemArray[0], count= row.ItemArray[1]  } ;
                cities.Add(city);
            }
            return cities;
        }
    }
}