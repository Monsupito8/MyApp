using MyWpfApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MyWpfApp.MeAppClass;

namespace MyWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\myAppDataList.json";
        private BindingList<MyAppModel> _myAppDateList;
        private FileIOService _fileIOService;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _myAppDateList = _fileIOService.LoadDate();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                Close();
            }

            dgTodoList.ItemsSource = _myAppDateList;
            _myAppDateList.ListChanged += _myAppDateList_ListChanged;
        }

        private void _myAppDateList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveDate(sender);

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}
