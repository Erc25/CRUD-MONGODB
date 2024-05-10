using CRUDMongo.BIZ;
using CRUDMongo.DAL.Entities;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CRUDMongo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClienteBIZ _clienteBIZ;
        public ObservableCollection<Cliente> Clientes { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _clienteBIZ = new ClienteBIZ("mongodb+srv://Eric:emqjjyjmg250898@crud.czoltdp.mongodb.net/?retryWrites=true&w=majority&appName=CRUD", "CRUD");
            CargarClientes();
        }

        private async void CargarClientes()
        {
            var clientes = await _clienteBIZ.ObtenerTodosClientes();
            Clientes = new ObservableCollection<Cliente>(clientes);
            DataContext = this;
        }

        private async void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtIdCliente.Text, out int idCliente) && int.TryParse(txtTelefono.Text, out int telefono))
            {
                var cliente = new Cliente
                {
                    IdCliente = idCliente,
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = telefono
                };

                await _clienteBIZ.InsertarCliente(cliente);
                CargarClientes();
            }
            else
            {
                MessageBox.Show("Por favor, introduce números enteros válidos para el Id del Cliente y el Teléfono.");
            }
        }

        private async void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem != null)
            {
                var clienteSeleccionado = (Cliente)dgClientes.SelectedItem;
                var clienteActualizado = new Cliente
                {
                    IdCliente = clienteSeleccionado.IdCliente,
                    Nombre = txtNombre.Text,
                    Correo = txtCorreo.Text,
                    Telefono = int.Parse(txtTelefono.Text)
                };

                await _clienteBIZ.ActualizarCliente(clienteSeleccionado.IdCliente, clienteActualizado);
                CargarClientes();
            }
        }

        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgClientes.SelectedItem != null)
            {
                var clienteSeleccionado = (Cliente)dgClientes.SelectedItem;
                await _clienteBIZ.EliminarCliente(clienteSeleccionado.IdCliente);
                CargarClientes();
            }
        }
    }
}