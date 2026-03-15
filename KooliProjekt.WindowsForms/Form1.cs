using KooliProjekt.Application.Features.Customers;
using KooliProjekt.WindowsForms.Api;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {

        private readonly HttpClient _httpClient = new HttpClient 
        { 
            BaseAddress = new Uri("http://localhost:5086/")

        };

        public Form1()
        {
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            await LoadData();

            base.OnLoad(e);
        }

        private async Task LoadData()
        {
            var url = "http://localhost:5086/api/Customer/List";
            url += "?page=1&pageSize=25";

            using var client = new HttpClient(); // Using statement ensures proper disposal of HttpClient

            var response = await client.GetFromJsonAsync<OperationResult<PagedResult<Customer>>>(url);
            // response: OperationResult<PagedResult<Customer>>

            CustomerGridView1.DataSource = response.Value.Results;
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddressBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void CityBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PhoneBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EmailBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void DicountUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private async void SubmitBtn_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:5086/api/customer/Save";

            var command = new SaveCustomerCommand
            {
                Name = NameBox.Text,
                Address = AddressBox.Text,
                Phone = PhoneBox.Text,
                Email = EmailBox.Text,
                City = CityBox.Text,
                Discount = DicountUpDown.Value,
            };

            var response = await _httpClient.PostAsJsonAsync(url, command);

            if (response.IsSuccessStatusCode)
                MessageBox.Show("Customer saved!");
            else
                MessageBox.Show("Error saving customer");

            await LoadData();
        }

        private async void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (CustomerGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "Are you sure you want to delete this customer?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (confirmResult != DialogResult.Yes)
                return;

            var url = "api/customer/delete"; // Adjust the URL as needed

            int id = (int)CustomerGridView1.SelectedRows[0].Cells["Id"].Value;

            var command = new DeleteCustomerCommand { Id = id };
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = JsonContent.Create(command)
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Customer deleted!");
                await LoadData(); // Refresh the data grid after deletion
            }
            else
            {
                MessageBox.Show("Delete failed.");
            }
        }

        private void CustomerGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
