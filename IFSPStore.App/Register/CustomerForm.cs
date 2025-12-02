using IFSPStore.App.Base;
using IFSPStore.App.ViewModel;
using IFSPStore.Domain.Base;
using IFSPStore.Domain.Entities;
using IFSPStore.Service.Validators;
using MySqlX.XDevAPI;

namespace IFSPStore.App.Register
{
    public partial class CustomerForm : BaseForm
    {
        private IBaseService<Customer> _customerService;
        private IBaseService<City> _cityService;
        private List<CustomerViewModel>? customers;
        public CustomerForm(IBaseService<Customer> customerService, IBaseService<City> cityService)
        {
            _customerService = customerService;
            _cityService = cityService;
            InitializeComponent();
            LoadCities();
        }

        private void LoadCities()
        {
            txtCity.ValueMember = "Id";
            txtCity.DisplayMember = "NameState";
            txtCity.DataSource = _cityService.Get<CityViewModel>().ToList();
        }

        private void FormToObject(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            customer.Document = txtDocument.Text;
            customer.District = txtDistrict.Text;

            if (int.TryParse(txtCity.SelectedValue!.ToString(), out var idGroup))
            {
                var city = _cityService.GetById<City>(idGroup);
                customer.City = city;
            }
        }
        protected override void Save()
        {
            try
            {
                if (IsEditMode)
                {
                    int.TryParse(txtId.Text, out int id);
                    var customer = _customerService.GetById<Customer>(id);
                    FormToObject(customer);
                    customer = _customerService.Update<Customer, Customer, CustomerValidator>(customer);
                }
                else
                {
                    var customer = new Customer();
                    FormToObject(customer);
                    _customerService.Add<Customer, Customer, CustomerValidator>(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                @"IFSP Store", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        protected override void Delete(int id)
        {
            try
            {
                _customerService.Delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                @"IFSP Store", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        protected override void PopulateGrid()
        {
            customers = _customerService.Get<CustomerViewModel>().ToList();
            dataGridViewList.DataSource = customers;
            dataGridViewList.Columns["Name"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewList.Columns["IdCity"]!.Visible = false;
        }

        protected override void GridToForm(DataGridViewRow? record)
        {
            txtId.Text = record!.Cells["Id"].Value.ToString();
            txtName.Text = record.Cells["Name"].Value.ToString();
            txtAddress.Text = record.Cells["Address"].Value.ToString();
            txtDocument.Text = record.Cells["Document"].Value.ToString();
            txtDistrict.Text = record.Cells["District"].Value.ToString();
            txtCity.SelectedValue = record.Cells["IdCity"].Value.ToString();
        }
    }
}
