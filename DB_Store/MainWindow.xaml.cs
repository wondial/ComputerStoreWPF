using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Data;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace DB_Store
{
    public partial class MainWindow : Window
    {
        DB db;
        DispatcherTimer timer;
        DateTime time;
        Grid currGrid = new Grid();
        Button currButton = new Button();
        string currStyle;
        DataRowView row;
        bool canSelect = true;
        int countDevices;
        DataTable dt = new DataTable();

        public MainWindow()
        {
            InitializeComponent();

            LoginWindow form = new LoginWindow();
            form.ShowDialog();

            if (form.GetStatus())
                Close();
            else
            {
                db = new DB(dataGrid);

                db.SetConnection(form.GetConnection());

                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timerTick);
                timer.Interval = new TimeSpan(0, 0, 0, 1);

                time = DateTime.Now;
                timer.Start();

                labelDate.Content = time.ToLongDateString();
                buttonBrands_Click(null, new RoutedEventArgs());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animation animation = new Animation();
            animation.MoveMainWindow(this);
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException) { }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            db.CloseConnection();
            timer.Stop();
            Close();
        }

        private void buttonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void timerTick(object sender, EventArgs e)
        {
            labelTime.Content = DateTime.Now.Subtract(time).ToString(@"hh\:mm\:ss");

            if (currGrid.Name == "gridApplications" && !canSelect)
            {
                textBoxDate.Clear();
                textBoxDate.AppendText(DateTime.Now.ToString());
            }

            if (currGrid.Name == "gridOrders" && !canSelect)
            {
                textBoxDateOrder.Clear();
                textBoxDateOrder.AppendText(DateTime.Now.ToString());
            }

            if (currGrid.Name == "gridSales" && !canSelect)
            {
                textBoxDateSale.Clear();
                textBoxDateSale.AppendText(DateTime.Now.ToString());
            }
        }

        private void GridVisibility(Grid newGrid)
        {
            currGrid.Visibility = Visibility.Hidden;
            newGrid.Visibility = Visibility.Visible;
            currGrid = newGrid;
        }

        private void ButtonStyle(Button button, string style)
        {
            try
            {
                currButton.Style = (Style)button.FindResource(currStyle);
            }
            catch (ArgumentNullException) { }

            button.Style = (Style)button.FindResource(style + "Active");
            currButton = button;
            currStyle = style;
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.Contains("ID") || e.PropertyName.Contains("FK") || e.PropertyName.Contains("-") || e.PropertyName == "Is_deleted")
                e.Column.Visibility = Visibility.Hidden;

            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";

            if (currGrid.Name == "gridBrands" || currGrid.Name == "gridTypes")
                e.Column.Width = dataGrid.Width - 19;
            if (currGrid.Name == "gridModels" || currGrid.Name == "gridSuppliers")
                e.Column.Width = (dataGrid.Width - 19) / 3;
            if (currGrid.Name == "gridDevices")
                if (e.PropertyName == "Устройство")
                    e.Column.Width = (dataGrid.Width - 19) / 1.5;
                else
                    e.Column.Width = (dataGrid.Width - 19) / 3;
            if (currGrid.Name == "gridClients" || currGrid.Name == "gridStaff")
                if (e.PropertyName == "Адрес")
                    e.Column.Width = (dataGrid.Width - 19) / 2.5;
                else
                    e.Column.Width = (dataGrid.Width - 19) / 5;
            if (currGrid.Name == "gridOrders" || currGrid.Name == "gridRecordsOfSales" || currGrid.Name == "gridRecords")
                if (e.PropertyName == "Устройство")
                    e.Column.Width = (dataGrid.Width - 19) / 2;
                else
                    e.Column.Width = (dataGrid.Width - 19) / 5;
            if (currGrid.Name == "gridApplications")
                e.Column.Width = (dataGrid.Width - 19) / 5;
            if (currGrid.Name == "gridDelivery" || currGrid.Name == "gridStorage")
                if (e.PropertyName == "Заказ" || e.PropertyName == "Устройство")
                    e.Column.Width = (dataGrid.Width - 19) / 2.5;
                else
                    e.Column.Width = (dataGrid.Width - 19) / 3.34;
            if (currGrid.Name == "gridSales")
                e.Column.Width = (dataGrid.Width - 19) / 4;
        }

        private void buttonBrands_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonBrands, "TopButton");
            GridVisibility(gridBrands);
            db.SelectTable("View_Brands");

            labelCount.Content = db.GetRowsCount("View_Brands").ToString();

            HideButton(0);
        }

        private void buttonTypes_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonTypes, "TopButton");
            GridVisibility(gridTypes);
            db.SelectTable("View_TypesOfDevices");

            labelCount.Content = db.GetRowsCount("View_TypesOfDevices").ToString();

            HideButton(0);
        }

        private void buttonModels_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonModels, "TopButton");
            GridVisibility(gridModels);
            db.SelectTable("View_Models");

            labelCount.Content = db.GetRowsCount("View_Models").ToString();

            HideButton(0);
        }

        private void buttonDevices_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonDevices, "TopButton");
            GridVisibility(gridDevices);
            db.SelectTable("View_Devices");

            labelCount.Content = db.GetRowsCount("View_Devices").ToString();

            HideButton(0);
        }

        private void buttonClients_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonClients, "TopButton");
            GridVisibility(gridClients);
            db.SelectTable("View_Clients");

            labelCount.Content = db.GetRowsCount("View_Clients").ToString();

            HideButton(0);
        }

        private void buttonStaff_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonStaff, "TopButton");
            GridVisibility(gridStaff);
            db.SelectTable("View_Staff");

            labelCount.Content = db.GetRowsCount("View_Staff").ToString();

            HideButton(0);
        }

        private void buttonSuppliers_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonSuppliers, "TopButton");
            GridVisibility(gridSuppliers);
            db.SelectTable("View_Suppliers");

            labelCount.Content = db.GetRowsCount("View_Suppliers").ToString();

            HideButton(0);
        }

        private void buttonApplications_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonApplications, "LeftMenuButtonApplication");
            GridVisibility(gridApplications);
            db.SelectTable("View_Applications");

            labelCount.Content = db.GetRowsCount("View_Applications").ToString();

            HideButton(0);
        }

        private void buttonRecords_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonRecords, "LeftMenuButtonRecords");
            GridVisibility(gridRecords);
            db.SelectTable("View_RecordsOfApplication");

            labelCount.Content = db.GetRowsCount("View_RecordsOfApplication").ToString();

            HideButton(0);
        }

        private void buttonOrders_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonOrders, "LeftMenuButtonOrders");
            GridVisibility(gridOrders);
            db.SelectTable("View_Orders");

            labelCount.Content = db.GetRowsCount("View_Orders").ToString();

            HideButton(0);
        }

        private void buttonDelivery_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonDelivery, "LeftMenuButtonDelivery");
            GridVisibility(gridDelivery);
            db.SelectTable("View_Delivery");

            labelCount.Content = db.GetRowsCount("View_Delivery").ToString();

            HideButton(0);
        }

        private void buttonStorage_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonStorage, "LeftMenuButtonStorage");
            GridVisibility(gridStorage);
            db.SelectTable("View_Storage");

            labelCount.Content = db.GetRowsCount("View_Storage").ToString();

            HideButton(0);
        }

        private void buttonSale_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonSale, "LeftMenuButtonSale");
            GridVisibility(gridSales);
            db.SelectTable("View_Sales");

            labelCount.Content = db.GetRowsCount("View_Sales").ToString();

            HideButton(0);
        }

        private void buttonRecordsOfSales_Click(object sender, RoutedEventArgs e)
        {
            ButtonStyle(buttonRecordsOfSales, "LeftMenuButtonRecordsOfSales");
            GridVisibility(gridRecordsOfSales);
            db.SelectTable("View_RecordsOfSale");

            labelCount.Content = db.GetRowsCount("View_RecordsOfSale").ToString();

            HideButton(0);
        }

        private void buttonUpdateBrands_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxBrand.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableBrandsTypes("Brands", textBoxBrand.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckNames("Brand", textBoxBrand.Text) != 1)
                    {
                        db.InsertTableBrandsTypes("Brands", textBoxBrand.Text);

                        int count = db.GetRowsCount("View_Brands");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Марка с таким названием уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateTypes_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxType.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableBrandsTypes("TypesOfDevices", textBoxType.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckNames("Type", textBoxType.Text) != 1)
                    {
                        db.InsertTableBrandsTypes("TypesOfDevices", textBoxType.Text);

                        int count = db.GetRowsCount("View_TypesOfDevices");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Такой тип уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateModels_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxModelsBrands.Background == Brushes.White && comboBoxModelsTypes.Background == Brushes.White && textBoxModel.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableModels(comboBoxModelsBrands.SelectedValue.ToString(), comboBoxModelsTypes.SelectedValue.ToString(), textBoxModel.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckModel(textBoxModel.Text, comboBoxModelsBrands.SelectedValue.ToString(), comboBoxModelsTypes.SelectedValue.ToString()) != 1)
                    {
                        db.InsertTableModels(comboBoxModelsBrands.SelectedValue.ToString(), comboBoxModelsTypes.SelectedValue.ToString(), textBoxModel.Text);

                        int count = db.GetRowsCount("View_Models");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Такая модель уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateDevices_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDevicesModels.Background == Brushes.White && textBoxPrice.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableDevices(comboBoxDevicesModels.SelectedValue.ToString(), textBoxPrice.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableDevices(comboBoxDevicesModels.SelectedValue.ToString(), textBoxPrice.Text);

                    int count = db.GetRowsCount("View_Devices");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateClients_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSurnameClients.Background == Brushes.White && textBoxNameClients.Background == Brushes.White && textBoxPatronymicClients.Background == Brushes.White && textBoxPhoneClients.Background == Brushes.White && textBoxAddresClients.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableClientsStaff("Clients", textBoxSurnameClients.Text, textBoxNameClients.Text, textBoxPatronymicClients.Text, textBoxPhoneClients.Text, textBoxAddresClients.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckNames("ClientsAddres", textBoxAddresClients.Text) != 1)
                    {
                        db.InsertTableClientsStaff("Clients", textBoxSurnameClients.Text, textBoxNameClients.Text, textBoxPatronymicClients.Text, textBoxPhoneClients.Text, textBoxAddresClients.Text, true);

                        int count = db.GetRowsCount("View_Clients");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Клиент с таким адресом уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateStaff_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSurnameStaff.Background == Brushes.White && textBoxNameStaff.Background == Brushes.White && textBoxPatronymicStaff.Background == Brushes.White && textBoxPhoneStaff.Background == Brushes.White && textBoxAddresStaff.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableClientsStaff("Staff", textBoxSurnameStaff.Text, textBoxNameStaff.Text, textBoxPatronymicStaff.Text, textBoxPhoneStaff.Text, textBoxAddresStaff.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckNames("StaffAddres", textBoxAddresStaff.Text) != 1)
                    {
                        db.InsertTableClientsStaff("Staff", textBoxSurnameStaff.Text, textBoxNameStaff.Text, textBoxPatronymicStaff.Text, textBoxPhoneStaff.Text, textBoxAddresStaff.Text, true);

                        int count = db.GetRowsCount("View_Staff");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Сотрудник с таким адресом уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSupplier.Background == Brushes.White && textBoxPhoneSupplier.Background == Brushes.White && textBoxAddresSupplier.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableSuppliers(textBoxSupplier.Text, textBoxPhoneSupplier.Text, textBoxAddresSupplier.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    if (db.CheckNames("Supplier", textBoxSupplier.Text) != 1)
                    {
                        db.InsertTableSuppliers(textBoxSupplier.Text, textBoxPhoneSupplier.Text, textBoxAddresSupplier.Text);

                        int count = db.GetRowsCount("View_Suppliers");
                        HideButton(count - 1);
                        labelCount.Content = count.ToString();
                    }
                    else
                        MessageBox.Show("Поставщик с таким названием уже существует!", "Ошибка данных", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateApplication_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAppClients.Background == Brushes.White && comboBoxAppStaff.Background == Brushes.White && textBoxDate.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateTableApplications(comboBoxAppClients.SelectedValue.ToString(), comboBoxAppStaff.SelectedValue.ToString(), textBoxDate.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableApplications(comboBoxAppClients.SelectedValue.ToString(), comboBoxAppStaff.SelectedValue.ToString(), textBoxDate.Text);

                    int count = db.GetRowsCount("View_Applications");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateRecords_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxRecordsApplications.Background == Brushes.White && comboBoxRecordsDevices.Background == Brushes.White && textBoxCount.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateRecordsOfApplication(comboBoxRecordsApplications.SelectedValue.ToString(), comboBoxRecordsDevices.SelectedValue.ToString(), textBoxCount.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableRecordsOfApplication(comboBoxRecordsApplications.SelectedValue.ToString(), comboBoxRecordsDevices.SelectedValue.ToString(), textBoxCount.Text);

                    int count = db.GetRowsCount("View_RecordsOfApplication");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateOrders_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxOrdersStaff.Background == Brushes.White && comboBoxOrdersDevices.Background == Brushes.White && textBoxCountOrder.Background == Brushes.White && textBoxDateOrder.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateOrders(comboBoxOrdersStaff.SelectedValue.ToString(), comboBoxOrdersDevices.SelectedValue.ToString(), textBoxCountOrder.Text, textBoxDateOrder.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableOrders(comboBoxOrdersStaff.SelectedValue.ToString(), comboBoxOrdersDevices.SelectedValue.ToString(), textBoxCountOrder.Text, textBoxDateOrder.Text);

                    int count = db.GetRowsCount("View_Orders");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateDelivery_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDeliverySupplier.Background == Brushes.White && comboBoxDeliveryOrder.Background == Brushes.White)
            {
                if (!canSelect)
                {
                    db.InsertTableDelivery(comboBoxDeliverySupplier.SelectedValue.ToString(), comboBoxDeliveryOrder.SelectedValue.ToString());

                    int count = db.GetRowsCount("View_Delivery");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateSales_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxSaleApp.Background == Brushes.White && textBoxDateSale.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateSales(comboBoxSaleApp.SelectedValue.ToString(), textBoxDateSale.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableSales(comboBoxSaleApp.SelectedValue.ToString(), textBoxDateSale.Text);

                    int count = db.GetRowsCount("View_Sales");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void buttonUpdateRecordsOfSales_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxRecordsOfSalesSale.Background == Brushes.White && comboBoxRecordsOfSalesRecords.Background == Brushes.White && textBoxCountRecords.Background == Brushes.White)
            {
                if (canSelect)
                {
                    row = dataGrid.SelectedItem as DataRowView;
                    db.UpdateRecordsOfSale(comboBoxRecordsOfSalesSale.SelectedValue.ToString(), comboBoxRecordsOfSalesRecords.SelectedValue.ToString(), textBoxCountRecords.Text, row[0].ToString());
                    dataGrid.SelectedIndex = 0;
                }
                else
                {
                    db.InsertTableRecordsOfSale(comboBoxRecordsOfSalesSale.SelectedValue.ToString(), comboBoxRecordsOfSalesRecords.SelectedValue.ToString(), textBoxCountRecords.Text);

                    int count = db.GetRowsCount("View_RecordsOfSale");
                    HideButton(count - 1);
                    labelCount.Content = count.ToString();
                }
            }
            else
                MessageBox.Show("Некорректный ввод данных! Повторите ввод!", "Ошибка обновления таблицы", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (canSelect)
            {
                if (currGrid.Name == "gridModels")
                {
                    db.FillComboBox(comboBoxModelsTypes, "TypesOfDevices", "ID_type", "Name");
                    db.FillComboBox(comboBoxModelsBrands, "Brands", "ID_brand", "Name");

                    db.SelectValueForComboBox(comboBoxModelsTypes, "FK_type");
                    db.SelectValueForComboBox(comboBoxModelsBrands, "FK_brand");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxModelsTypes.Text == "" || comboBoxModelsTypes.Text.Contains("(удалено)"))
                            comboBoxModelsTypes.Text = row["Тип"].ToString() + " (удалено)";
                        if (comboBoxModelsBrands.Text == "" || comboBoxModelsBrands.Text.Contains("(удалено)"))
                            comboBoxModelsBrands.Text = row["Марка"].ToString() + " (удалено)";
                    }
                    catch (NullReferenceException) { }
                }
                else if (currGrid.Name == "gridDevices")
                {
                    db.FillComboBox(comboBoxDevicesModels, "View_Models", "ID_model", "-Устройство");

                    db.SelectValueForComboBox(comboBoxDevicesModels, "FK_model");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxDevicesModels.Text == "" || comboBoxDevicesModels.Text.Contains("(удалено)"))
                            comboBoxDevicesModels.Text = row["-For_CB"].ToString() + " (удалено)";
                    }
                    catch (NullReferenceException) { }
                }
                else if (currGrid.Name == "gridApplications")
                {
                    db.FillComboBox(comboBoxAppClients, "View_Clients", "ID_client", "-ФИО");
                    db.FillComboBox(comboBoxAppStaff, "View_Staff", "ID_staff", "-ФИО");

                    db.SelectValueForComboBox(comboBoxAppClients, "FK_client");
                    db.SelectValueForComboBox(comboBoxAppStaff, "FK_staff");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxAppClients.Text == "" || comboBoxAppClients.Text.Contains("(удалено)"))
                            comboBoxAppClients.Text = row["Клиент"].ToString() + " (удалено)";
                        if (comboBoxAppStaff.Text == "" || comboBoxAppStaff.Text.Contains("(удалено)"))
                            comboBoxAppStaff.Text = row["Сотрудник"].ToString() + " (удалено)";

                        if (row["Статус"].ToString() == "Утверждён")
                        {
                            comboBoxAppStaff.IsEnabled = false;
                            comboBoxAppClients.IsEnabled = false;
                            textBoxDate.IsReadOnly = true;
                            buttonNewClient.IsEnabled = false;
                            buttonUpdateApplication.IsEnabled = false;
                        }
                        else
                        {
                            comboBoxAppStaff.IsEnabled = true;
                            comboBoxAppClients.IsEnabled = true;
                            textBoxDate.IsReadOnly = false;
                            buttonNewClient.IsEnabled = true;
                            buttonUpdateApplication.IsEnabled = true;
                        }
                    }
                    catch (NullReferenceException) { }

                    try
                    {
                        DateTime dt = DateTime.Parse(textBoxDate.Text, CultureInfo.CreateSpecificCulture("en-us"));
                        textBoxDate.Clear();
                        textBoxDate.AppendText(dt.ToString());
                    }
                    catch (FormatException) { }
                }
                else if (currGrid.Name == "gridRecords")
                {
                    db.FillComboBox(comboBoxRecordsApplications, "View_Applications", "ID_application", "-Дата (Клиент)");
                    db.FillComboBox(comboBoxRecordsDevices, "View_Devices", "ID_device", "-Устройство (Цена)");

                    db.SelectValueForComboBox(comboBoxRecordsApplications, "FK_application");
                    db.SelectValueForComboBox(comboBoxRecordsDevices, "FK_device");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxRecordsApplications.Text == "" || comboBoxRecordsApplications.Text.Contains("(отсутствует)"))
                        {
                            comboBoxRecordsApplications.Text = row["-For_CB1"].ToString() + " (отсутствует)";

                            comboBoxRecordsApplications.IsEnabled = false;
                            comboBoxRecordsDevices.IsEnabled = false;
                            textBoxCount.IsReadOnly = true;
                            buttonUpdateRecords.IsEnabled = false;
                        }
                        else
                        {
                            comboBoxRecordsApplications.IsEnabled = true;
                            comboBoxRecordsDevices.IsEnabled = true;
                            textBoxCount.IsReadOnly = false;
                            buttonUpdateRecords.IsEnabled = true;
                        }
                        if (comboBoxRecordsDevices.Text == "" || comboBoxRecordsDevices.Text.Contains("(удалено)"))
                            comboBoxRecordsDevices.Text = row["-For_CB2"].ToString() + " (удалено)";
                    }
                    catch (NullReferenceException) { }
                }
                else if (currGrid.Name == "gridOrders")
                {
                    db.FillComboBox(comboBoxOrdersStaff, "View_Staff", "ID_staff", "-ФИО");
                    db.FillComboBox(comboBoxOrdersDevices, "View_Devices", "ID_device", "-Устройство (Цена)");

                    db.SelectValueForComboBox(comboBoxOrdersStaff, "FK_staff");
                    db.SelectValueForComboBox(comboBoxOrdersDevices, "FK_device");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxOrdersStaff.Text == "" || comboBoxOrdersStaff.Text.Contains("(удалено)"))
                            comboBoxOrdersStaff.Text = row["Сотрудник"].ToString() + " (удалено)";
                        if (comboBoxOrdersDevices.Text == "" || comboBoxOrdersDevices.Text.Contains("(удалено)"))
                            comboBoxOrdersDevices.Text = row["-For_CB"].ToString() + " (удалено)";

                        if (row["Статус"].ToString() == "Закрыт")
                        {
                            comboBoxOrdersStaff.IsEnabled = false;
                            comboBoxOrdersDevices.IsEnabled = false;
                            textBoxCountOrder.IsReadOnly = true;
                            textBoxDateOrder.IsReadOnly = true;
                        }
                        else
                        {
                            comboBoxOrdersStaff.IsEnabled = true;
                            comboBoxOrdersDevices.IsEnabled = true;
                            textBoxCountOrder.IsReadOnly = false;
                            textBoxDateOrder.IsReadOnly = false;
                        }
                    }
                    catch (NullReferenceException) { }

                    try
                    {
                        DateTime dt = DateTime.Parse(textBoxDateOrder.Text, CultureInfo.CreateSpecificCulture("en-us"));
                        textBoxDateOrder.Clear();
                        textBoxDateOrder.AppendText(dt.ToString());
                    }
                    catch (FormatException) { }
                }
                else if (currGrid.Name == "gridDelivery")
                {
                    db.FillComboBox(comboBoxDeliverySupplier, "Suppliers", "ID_supplier", "Name");
                    db.FillComboBox(comboBoxDeliveryOrder, "View_Orders", "ID_order", "-Устройство (Дата)");

                    db.SelectValueForComboBox(comboBoxDeliverySupplier, "FK_supplier");
                    db.SelectValueForComboBox(comboBoxDeliveryOrder, "FK_order");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;

                        if (comboBoxDeliverySupplier.Text == "" || comboBoxDeliverySupplier.Text.Contains("(удалено)"))
                            comboBoxDeliverySupplier.Text = row["Поставщик"].ToString() + " (удалено)";
                        comboBoxDeliveryOrder.Text = row["Заказ"].ToString();
                    }
                    catch (NullReferenceException) { }
                }
                else if (currGrid.Name == "gridStorage")
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(textBoxStorageDate.Text, CultureInfo.CreateSpecificCulture("en-us"));
                        textBoxStorageDate.Clear();
                        textBoxStorageDate.AppendText(dt.ToString());
                    }
                    catch (FormatException) { }
                }
                else if (currGrid.Name == "gridSales")
                {
                    db.FillComboBox(comboBoxSaleApp, "View_Applications", "ID_application", "-Дата (Клиент)");

                    db.SelectValueForComboBox(comboBoxSaleApp, "FK_application");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;
                        comboBoxSaleApp.Text = row["-For_CB"].ToString();
                    }
                    catch (NullReferenceException) { }

                    try
                    {
                        DateTime dt = DateTime.Parse(textBoxDateSale.Text, CultureInfo.CreateSpecificCulture("en-us"));
                        textBoxDateSale.Clear();
                        textBoxDateSale.AppendText(dt.ToString());
                    }
                    catch (FormatException) { }
                }
                else if (currGrid.Name == "gridRecordsOfSales")
                {
                    db.FillComboBox(comboBoxRecordsOfSalesSale, "View_Sales", "ID_sale", "-Дата (Клиент)");

                    db.SelectValueForComboBox(comboBoxRecordsOfSalesSale, "FK_sale");
                    db.SelectValueForComboBox(comboBoxRecordsOfSalesRecords, "FK_record_app");

                    try
                    {
                        row = dataGrid.SelectedItem as DataRowView;
                        comboBoxRecordsOfSalesRecords.Text = row["-For_CB"].ToString();
                    }
                    catch (NullReferenceException) { }
                }
            }
            else
                dataGrid.SelectedItem = null;
        }

        private void buttonNewClient_Click(object sender, RoutedEventArgs e)
        {
            NewClientWindow form = new NewClientWindow(db);
            form.ShowDialog();

            if (form.GetStatus())
            {
                db.FillComboBox(comboBoxAppClients, "View_Clients", "ID_client", "-ФИО");

                comboBoxAppClients.SelectedValue = db.GetID("Clients", "ID_client");
            }
        }

        private void HideButton(int row)
        {
            buttonCancel.Visibility = Visibility.Hidden;
            canSelect = true;
            dataGrid.SelectedIndex = row;

            if (currGrid.Name == "gridApplications")
                textBoxDate.IsReadOnly = false;
            else if (currGrid.Name == "gridDelivery")
            {
                comboBoxDeliverySupplier.IsEnabled = false;
                comboBoxDeliveryOrder.IsEnabled = false;
                buttonUpdateDelivery.Visibility = Visibility.Hidden;
            }
            else if (currGrid.Name == "gridSales")
            {
                comboBoxSaleApp.IsEnabled = false;
                textBoxDateSale.IsReadOnly = true;
                buttonUpdateSales.Visibility = Visibility.Hidden;
            }
            else if (currGrid.Name == "gridOrders")
                textBoxDateOrder.IsReadOnly = false;
            else if (currGrid.Name == "gridRecordsOfSales")
            {
                comboBoxRecordsOfSalesSale.IsEnabled = false;
                comboBoxRecordsOfSalesRecords.IsEnabled = false;
                textBoxCountRecords.IsReadOnly = true;
                buttonUpdateRecordsOfSales.Visibility = Visibility.Hidden;
                labelCountDevicesInRecords.Content = "Количество (доступно 0 шт.)";
            }

            if (currGrid.Name == "gridBrands")
                dt = db.GetDataTable("View_Brands");
            else if (currGrid.Name == "gridTypes")
                dt = db.GetDataTable("View_TypesOfDevices");
            else if (currGrid.Name == "gridModels")
                dt = db.GetDataTable("View_Models");
            else if (currGrid.Name == "gridDevices")
                dt = db.GetDataTable("View_Devices");
            else if (currGrid.Name == "gridClients")
                dt = db.GetDataTable("View_Clients");
            else if (currGrid.Name == "gridStaff")
                dt = db.GetDataTable("View_Staff");
            else if (currGrid.Name == "gridSuppliers")
                dt = db.GetDataTable("View_Suppliers");
            else if (currGrid.Name == "gridApplications")
                dt = db.GetDataTable("View_Applications");
            else if (currGrid.Name == "gridRecords")
                dt = db.GetDataTable("View_RecordsOfApplication");
            else if (currGrid.Name == "gridOrders")
                dt = db.GetDataTable("View_Orders");
            else if (currGrid.Name == "gridStorage")
                dt = db.GetDataTable("View_Storage");
            else if (currGrid.Name == "gridDelivery")
                dt = db.GetDataTable("View_Delivery");
            else if (currGrid.Name == "gridSales")
                dt = db.GetDataTable("View_Sales");
            else if (currGrid.Name == "gridRecordsOfSales")
                dt = db.GetDataTable("View_RecordsOfSale");
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (currGrid.Name != "gridStorage")
            {
                buttonCancel.Visibility = Visibility.Visible;
                canSelect = false;
                dataGrid.SelectedIndex = -1;
            }

            if (currGrid.Name == "gridBrands")
                textBoxBrand.Clear();
            else if (currGrid.Name == "gridTypes")
                textBoxType.Clear();
            else if (currGrid.Name == "gridModels")
            {
                comboBoxModelsTypes.Text = "";
                comboBoxModelsBrands.Text = "";
                textBoxModel.Clear();
            }
            else if (currGrid.Name == "gridDevices")
            {
                comboBoxDevicesModels.Text = "";
                textBoxPrice.Clear();
            }
            else if (currGrid.Name == "gridClients")
            {
                textBoxSurnameClients.Clear();
                textBoxNameClients.Clear();
                textBoxPatronymicClients.Clear();
                textBoxPhoneClients.Clear();
                textBoxAddresClients.Clear();
            }
            else if (currGrid.Name == "gridStaff")
            {
                textBoxSurnameStaff.Clear();
                textBoxNameStaff.Clear();
                textBoxPatronymicStaff.Clear();
                textBoxPhoneStaff.Clear();
                textBoxAddresStaff.Clear();
            }
            else if (currGrid.Name == "gridSuppliers")
            {
                textBoxSupplier.Clear();
                textBoxPhoneSupplier.Clear();
                textBoxAddresSupplier.Clear();
            }
            else if (currGrid.Name == "gridApplications")
            {
                comboBoxAppStaff.IsEnabled = true;
                comboBoxAppClients.IsEnabled = true;
                textBoxDate.IsReadOnly = true;
                buttonNewClient.IsEnabled = true;
                buttonUpdateApplication.IsEnabled = true;

                comboBoxAppClients.Text = "";
                comboBoxAppStaff.Text = "";
            }
            else if (currGrid.Name == "gridRecords")
            {
                comboBoxRecordsApplications.IsEnabled = true;
                comboBoxRecordsDevices.IsEnabled = true;
                textBoxCount.IsReadOnly = false;
                buttonUpdateRecords.IsEnabled = true;

                comboBoxRecordsApplications.Text = "";
                comboBoxRecordsDevices.Text = "";
                textBoxCount.Clear();

                labelCountDevices.Content = "Количество (доступно 0 шт.)";
            }
            else if (currGrid.Name == "gridOrders")
            {
                comboBoxOrdersStaff.IsEnabled = true;
                comboBoxOrdersDevices.IsEnabled = true;
                textBoxCountOrder.IsReadOnly = false;
                textBoxDateOrder.IsReadOnly = true;

                comboBoxOrdersStaff.Text = "";
                comboBoxOrdersDevices.Text = "";
            }
            else if (currGrid.Name == "gridDelivery")
            {
                comboBoxDeliverySupplier.IsEnabled = true;
                comboBoxDeliveryOrder.IsEnabled = true;
                buttonUpdateDelivery.Visibility = Visibility.Visible;

                comboBoxDeliverySupplier.Text = "";
                comboBoxDeliveryOrder.Text = "";
            }
            else if (currGrid.Name == "gridSales")
            {
                comboBoxSaleApp.IsEnabled = true;
                buttonUpdateSales.Visibility = Visibility.Visible;

                comboBoxSaleApp.Text = "";
                textBoxDateSale.IsReadOnly = true;
            }
            else if (currGrid.Name == "gridRecordsOfSales")
            {
                comboBoxRecordsOfSalesSale.IsEnabled = true;
                comboBoxRecordsOfSalesRecords.IsEnabled = true;
                textBoxCountRecords.IsReadOnly = false;
                buttonUpdateRecordsOfSales.Visibility = Visibility.Visible;

                comboBoxRecordsOfSalesSale.Text = "";
                comboBoxRecordsOfSalesRecords.Text = "";
                textBoxCountRecords.Clear();

                labelCountDevicesInRecords.Content = "Количество (доступно 0 шт.)";
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            HideButton(0);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (currGrid.Name == "gridBrands")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Brands", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Brands").ToString();
            }
            else if (currGrid.Name == "gridTypes")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("TypesOfDevices", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_TypesOfDevices").ToString();
            }
            else if (currGrid.Name == "gridModels")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Models", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Models").ToString();
            }
            else if (currGrid.Name == "gridDevices")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Devices", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Devices").ToString();
            }
            else if (currGrid.Name == "gridClients")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Clients", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Clients").ToString();
            }
            else if (currGrid.Name == "gridStaff")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Staff", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Staff").ToString();
            }
            else if (currGrid.Name == "gridSuppliers")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Suppliers", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Suppliers").ToString();
            }
            else if (currGrid.Name == "gridApplications")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Applications", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Applications").ToString();
            }
            else if (currGrid.Name == "gridRecords")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteRecordOfApplication(row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_RecordsOfApplication").ToString();
            }
            else if (currGrid.Name == "gridOrders")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Orders", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Orders").ToString();
            }
            else if (currGrid.Name == "gridDelivery")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Delivery", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Delivery").ToString();
            }
            else if (currGrid.Name == "gridSales")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteFromTable("Sales", dataGrid.Columns[0].Header.ToString(), row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_Sales").ToString();
            }
            else if (currGrid.Name == "gridRecordsOfSales")
            {
                row = dataGrid.SelectedItem as DataRowView;
                db.DeleteRecordOfSale(row[0].ToString());
                HideButton(0);

                labelCount.Content = db.GetRowsCount("View_RecordsOfSale").ToString();
            }
        }

        private void PreviewTextInputCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Checks.TextInput(sender as TextBox, e))
                e.Handled = true;
        }

        private void PreviewKeyDownCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void TextChangedCheck(object sender, TextChangedEventArgs e)
        {
            Checks.TextChanged(sender as TextBox, countDevices);
        }

        private void SelectionChangedCheck(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).Name == "comboBoxRecordsDevices")
            {
                try
                {
                    countDevices = db.GetCountDevicesInStorage((e.AddedItems[0] as DataRowView).Row["ID_device"].ToString());

                    int count;
                    if (textBoxCount.Text == "")
                        count = 0;
                    else count = Convert.ToInt32(textBoxCount.Text);

                    if (count > countDevices || count == 0)
                        textBoxCount.Background = new SolidColorBrush(Color.FromRgb(255, 197, 197));
                    else
                        textBoxCount.Background = Brushes.White;
                }
                catch (IndexOutOfRangeException) { }

                labelCountDevices.Content = "Количество (доступно " + countDevices.ToString() + " шт.)";
            }

            if ((sender as ComboBox).Name == "comboBoxRecordsOfSalesSale")
            {
                try
                {
                    int fk = db.GetFkApplication((e.AddedItems[0] as DataRowView).Row["ID_sale"].ToString());

                    db.FillComboBoxWithCondition(comboBoxRecordsOfSalesRecords, fk.ToString());
                    db.SelectValueForComboBox(comboBoxRecordsOfSalesRecords, "FK_record_app");
                }
                catch (IndexOutOfRangeException) { }
            }

            if ((sender as ComboBox).Name == "comboBoxRecordsOfSalesRecords")
            {
                try
                {
                    countDevices = db.GetCountDevicesInRecordsOfApplication((e.AddedItems[0] as DataRowView).Row["ID_record"].ToString());
                }
                catch (IndexOutOfRangeException) { }

                labelCountDevicesInRecords.Content = "Количество (доступно " + countDevices.ToString() + " шт.)";
            }

            Checks.SelectionChanged(sender as ComboBox, e);
        }

        private void textBoxFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFilter.Text == "Поиск...")
                textBoxFilter.Text = "";
        }

        private void textBoxFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxFilter.Text == "")
                textBoxFilter.Text = "Поиск...";
        }

        private void Search(string value)
        {
            DataTable newDT = dt.Clone();
            newDT.Clear();

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    try
                    {
                        if (column.DataType == typeof(string) && !column.ColumnName.Contains("-") && !column.ColumnName.Contains("_"))
                        {
                            if (row.Field<string>(column.ColumnName).ToLower().Contains(value.ToLower()))
                            {
                                newDT.ImportRow(row);
                                break;
                            }
                        }

                        if (column.DataType == typeof(DateTime) && !column.ColumnName.Contains("-") && !column.ColumnName.Contains("_"))
                        {
                            if (row.Field<DateTime>(column.ColumnName) > DateTime.Parse(value + " 00:00:00") && row.Field<DateTime>(column.ColumnName) < DateTime.Parse(value + " 23:59:59"))
                            {
                                newDT.ImportRow(row);
                                break;
                            }
                        }

                        if (column.DataType == typeof(int) && !column.ColumnName.Contains("-") && !column.ColumnName.Contains("_"))
                        {
                            if (value.StartsWith("="))
                            {
                                if (row.Field<int>(column.ColumnName) == int.Parse(value.Substring(1)))
                                {
                                    newDT.ImportRow(row);
                                    break;
                                }
                            }
                            else if (value.StartsWith(">"))
                            {
                                if (row.Field<int>(column.ColumnName) > int.Parse(value.Substring(1)))
                                {
                                    newDT.ImportRow(row);
                                    break;
                                }
                            }
                            else if (value.StartsWith("<"))
                            {
                                if (row.Field<int>(column.ColumnName) < int.Parse(value.Substring(1)))
                                {
                                    newDT.ImportRow(row);
                                    break;
                                }
                            }
                            else
                            {
                                if (row.Field<int>(column.ColumnName).ToString().Contains(value))
                                {
                                    newDT.ImportRow(row);
                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }

            dataGrid.ItemsSource = newDT.DefaultView;
            dataGrid.SelectedIndex = 0;
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dt.DefaultView;
            dataGrid.SelectedIndex = 0;

            textBoxFilter.Text = "Поиск...";
        }

        private void textBoxFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (textBoxFilter.Text == "" || textBoxFilter.Text == "Поиск..." || (textBoxFilter.Text.StartsWith(">") && textBoxFilter.Text.Length == 1) || (textBoxFilter.Text.StartsWith("<") && textBoxFilter.Text.Length == 1) || (textBoxFilter.Text.StartsWith("=") && textBoxFilter.Text.Length == 1))
                {
                    dataGrid.ItemsSource = dt.DefaultView;
                    dataGrid.SelectedIndex = 0;
                }
                else
                    Search(textBoxFilter.Text);
            }
            catch (Exception) { }
        }

        private void buttonReport_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Формирование отчета может занять некоторое время! Продолжить?", "Формирование отчета", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                bool isHeader = true;

                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet ExcelWorkSheet = (Excel.Worksheet)ExcelBook.Sheets[1];

                int i = 3;
                int j = 2;

                foreach (DataRow row in (dataGrid.ItemsSource as DataView).Table.Rows)
                {
                    foreach (DataColumn column in (dataGrid.ItemsSource as DataView).Table.Columns)
                    {
                        if (!column.ColumnName.Contains("-") && !column.ColumnName.Contains("_"))
                        {
                            if (isHeader)
                            {
                                ExcelWorkSheet.Cells[i - 1, j].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                ExcelWorkSheet.Cells[i - 1, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(111, 126, 149));
                                ExcelWorkSheet.Cells[i - 1, j].Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                                ExcelWorkSheet.Cells[i - 1, j] = column.ColumnName;
                            }

                            if (column.DataType == typeof(string))
                            {
                                if (i % 2 == 0)
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(251, 252, 254));
                                else
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));

                                ExcelWorkSheet.Cells[i, j] = row.Field<string>(column);
                            }

                            if (column.DataType == typeof(int))
                            {
                                if (i % 2 == 0)
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(251, 252, 254));
                                else
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));

                                ExcelWorkSheet.Cells[i, j] = row.Field<int>(column);
                            }

                            if (column.DataType == typeof(DateTime))
                            {
                                if (i % 2 == 0)
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(251, 252, 254));
                                else
                                    ExcelWorkSheet.Cells[i, j].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));

                                ExcelWorkSheet.Cells[i, j] = row.Field<DateTime>(column);
                            }

                            j++;
                        }
                    }

                    i++;
                    j = 2;
                    isHeader = false;
                }

                ExcelWorkSheet.Columns.AutoFit();
                ExcelWorkSheet.Rows.RowHeight = 20;
                ExcelWorkSheet.Rows.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                ExcelApp.Visible = true;
                ExcelApp.UserControl = true;
            }
        }
    }
}