using IFSPStore.App.Base;
using IFSPStore.App.ViewModel;
using IFSPStore.Domain.Base;
using IFSPStore.Domain.Entities;
using IFSPStore.Service.Validators;

namespace IFSPStore.App.Register
{
    public partial class UserForm : BaseForm
    {
        private IBaseService<User> _userService;
        private List<UserViewModel>? users;
        public static bool loginUser;
        public UserForm(IBaseService<User> userService)
        {
            _userService = userService;
            InitializeComponent();
        }

        private void FormToObject(User user)
        {
            user.Name = txtName.Text;
            user.Login = txtLogin.Text;
            user.Email = txtEmail.Text;
            user.Password = txtPassword.Text;
            user.RegisterDate = DateTime.Now;
            user.IsActive = true;
        }
        protected override void Save()
        {
            try
            {
                if (IsEditMode)
                {
                    int.TryParse(txtId.Text, out int id);
                    var user = _userService.GetById<User>(id);
                    FormToObject(user);
                    user = _userService.Update<User, User, UserValidator>(user);
                }
                else
                {
                    var user = new User();
                    FormToObject(user);
                    _userService.Add<User, User, UserValidator>(user);
                    
                    if(loginUser)
                    {
                        MainForm.user = user;
                        loginUser = false;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
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
                _userService.Delete(id);
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
            users = _userService.Get<UserViewModel>().ToList();
            dataGridViewList.DataSource = users;
            dataGridViewList.Columns["Name"]!.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        protected override void GridToForm(DataGridViewRow? record)
        {
            txtName.Text = record.Cells["Name"].Value.ToString();
            txtLogin.Text = record.Cells["Login"].Value.ToString();
            txtEmail.Text = record.Cells["Email"].Value.ToString();
            txtPassword.Text = record.Cells["Password"].Value.ToString();
        }
    }
}
