using System;
using System.Windows;
using System.Windows.Input;
using System.Data;
using System.Data.SqlClient;

namespace DB_Store
{
    public partial class LoginWindow : Window
    {
        bool close = true;
        SqlConnection connecion;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Animation animation = new Animation();
            animation.MoveLoginWindow(this);
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch (InvalidOperationException) { }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            close = true;
            Close();
        }

        public bool GetStatus()
        {
            return close;
        }

        public SqlConnection GetConnection()
        {
            return connecion;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            connecion = new SqlConnection("Data Source=ALEXANDER;Initial Catalog=DB_Store;User ID=" + textBoxLogin.Text + ";Password=" + passwordBox.Password + ";");

            try
            {
                connecion.Open();
            }
            catch (SqlException)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (connecion.State == ConnectionState.Open)
            {
                close = false;
                Close();
            }
        }

        private void textBoxLogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int code = Convert.ToChar(e.Text);
            if ((code >= 65 && code <= 90) || (code >= 97 && code <= 122))
                return;
            else
                e.Handled = true;
        }

        private void textBoxLogin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void textBoxLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
                textBoxLogin.Text = "Имя пользователя";
        }

        private void textBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "Имя пользователя")
                textBoxLogin.Text = "";
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "Пароль")
                passwordBox.Password = "";
        }

        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "")
                passwordBox.Password = "Пароль";
        }
    }
}