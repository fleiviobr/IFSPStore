using IFSPStore.App.Base;
using IFSPStore.App.ViewModel;
using IFSPStore.Domain.Base;
using IFSPStore.Domain.Entities;
using IFSPStore.Service.Validators;

namespace IFSPStore.App.Register
{
    public partial class CityForm : BaseForm
    {
        private IBaseService<City> _cityService;
        private List<CityViewModel>? cities;
        public CityForm(IBaseService<City> cityService)
        {
            _cityService = cityService;
            InitializeComponent();
        }

        private void FormToObject(City city)
        {
            city.Name = txtName.Text;
            city.State = txtState.Text;
        }
        protected override void Save()
        {
            try
            {
                if (IsEditMode)
                {
                    int.TryParse(txtId.Text, out int id);
                    var city = _cityService.GetById<City>(id);
                    FormToObject(city);
                    city = _cityService.Update<City, City, CityValidator>(city);
                }
                else
                {
                    var city = new City();
                    FormToObject(city);
                    _cityService.Add<City, City, CityValidator>(city);
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
                _cityService.Delete(id);
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
            cities = _cityService.Get<CityViewModel>().ToList();
            dataGridViewList.DataSource = cities;
            dataGridViewList.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        protected override void GridToForm(DataGridViewRow? record)
        {
            txtId.Text = record.Cells["Id"].Value.ToString();
            txtName.Text = record.Cells["Name"].Value.ToString();
            txtState.Text = record.Cells["State"].Value.ToString();
        }
    }
}
