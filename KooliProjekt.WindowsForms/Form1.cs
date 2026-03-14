using KooliProjekt.WindowsForms.Api;
using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms
{
    public partial class Form1 : Form
    {
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
            url += "?page=1&pageSize=10";

            using var client = new HttpClient(); // Using statement ensures proper disposal of HttpClient

            var response = await client.GetFromJsonAsync<OperationResult<PagedResult<Customer>>>(url);
            // response: OperationResult<PagedResult<Customer>>

            dataGridView1.DataSource = response.Value.Results;
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

        private void DiscountBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
