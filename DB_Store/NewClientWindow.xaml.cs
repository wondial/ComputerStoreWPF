using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DB_Store
{
    public partial class NewClientWindow : Window
    {
        DB db;
        bool added;

        public NewClientWindow(DB _db)
        {
            InitializeComponent();

            db = _db;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animation animation = new Animation();
            animation.MoveNewClientWindow(this);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            added = false;
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            db.InsertTableClientsStaff("Clients", textBoxSurnameClients.Text, textBoxNameClients.Text, textBoxPatronymicClients.Text, textBoxPhoneClients.Text, textBoxAddresClients.Text, false);

            added = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            added = false;
            this.Close();
        }

        public bool GetStatus()
        {
            return added;
        }

        private void PreviewKeyDownCheck(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PreviewTextInputCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Checks.TextInput(sender as TextBox, e))
                e.Handled = true;
        }

        private void TextChangedCheck(object sender, TextChangedEventArgs e)
        {
            Checks.TextChanged(sender as TextBox, 0);
        }
    }
}