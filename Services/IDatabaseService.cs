using IdeaTrading.Models;
using System.Collections.Generic;
using System.Data;

namespace IdeaTrading.Services
{
    interface IDatabaseService
    {
        DataTable Get(string query, int id);
        DataTable GetAll(string query);
        int Create(string query, IDictionary<string, object> parameters);
        void Edit(string query, IDictionary<string, object> parameters);
        void Delete(string query, int id);
    }
}
