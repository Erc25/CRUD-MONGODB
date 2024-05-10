using CRUDMongo.DAL.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDMongo.DAL
{
    public class MongoDBContext
    {
        private IMongoDatabase _database;
        private IMongoClient _client;

        public MongoDBContext(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");

        public async Task<List<Cliente>> ObtenerTodosClientes()
        {
            return await Clientes.Find(_ => true).ToListAsync();
        }

        public async Task InsertarCliente(Cliente cliente)
        {
            await Clientes.InsertOneAsync(cliente);
        }

        public async Task ActualizarCliente(int id, Cliente cliente)
        {
            await Clientes.ReplaceOneAsync(c => c.IdCliente == id, cliente);
        }

        public async Task EliminarCliente(int id)
        {
            await Clientes.DeleteOneAsync(c => c.IdCliente == id);
        }
    }
}