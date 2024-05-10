using CRUDMongo.DAL;
using CRUDMongo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMongo.BIZ
{
    public  class ClienteBIZ
    {
        private MongoDBContext _dbContext;

        public ClienteBIZ(string connectionString, string databaseName)
        {
            _dbContext = new MongoDBContext(connectionString, databaseName);
        }

        public Task<List<Cliente>> ObtenerTodosClientes()
        {
            return _dbContext.ObtenerTodosClientes();
        }

        public Task InsertarCliente(Cliente cliente)
        {
            return _dbContext.InsertarCliente(cliente);
        }

        public Task ActualizarCliente(int id, Cliente cliente)
        {
            return _dbContext.ActualizarCliente(id, cliente);
        }

        public Task EliminarCliente(int id)
        {
            return _dbContext.EliminarCliente(id);
        }
    }
}
