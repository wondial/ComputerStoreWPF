using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace DB_Store
{
    public class DB
    {
        SqlConnection connection;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        DataGrid dataGrid;
        int rowsCount;

        public DB(DataGrid _dataGrid)
        {
            dataGrid = _dataGrid;
        }

        public void SetConnection(SqlConnection _connection)
        {
            connection = _connection;
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public DataTable FillTable(string table, string fillForCB)
        {
            if (fillForCB == "View_Orders")
                cmd = new SqlCommand("SELECT * FROM View_Orders WHERE Статус='Открыт' AND Is_deleted='0'", connection);
            else if (fillForCB == "View_Applications")
                cmd = new SqlCommand("SELECT * FROM View_Applications WHERE Статус='Оформляется' AND Is_deleted='0'", connection);
            else
                cmd = new SqlCommand("SELECT * FROM " + table + " WHERE Is_deleted='0'", connection);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);

            rowsCount = dt.Rows.Count;

            return dt;
        }

        public void SelectTable(string table)
        {
            dt = FillTable(table, null);

            dataGrid.ItemsSource = dt.DefaultView;
        }

        public void FillComboBox(ComboBox cb, string table, string id, string field)
        {
            if (table == "View_Orders")
                dt = FillTable(table, "View_Orders");
            else if (table == "View_Applications")
                dt = FillTable(table, "View_Applications");
            else
                dt = FillTable(table, null);

            cb.ItemsSource = dt.DefaultView;
            cb.SelectedValuePath = dt.Columns[id].ToString();
            cb.DisplayMemberPath = dt.Columns[field].ToString();
        }

        public void FillComboBoxWithCondition(ComboBox cb, string fk)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM View_RecordsOfApplication WHERE FK_application='" + fk + "' AND Is_deleted='0' AND [-Inactive]='0'", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            
            cb.ItemsSource = dt.DefaultView;
            cb.SelectedValuePath = dt.Columns["ID_record"].ToString();
            cb.DisplayMemberPath = dt.Columns["-Устройство (Количество)"].ToString();
        }

        public void SelectValueForComboBox(ComboBox cb, string columnFK)
        {
            try
            {
                DataRowView row = dataGrid.SelectedItem as DataRowView;
                cb.SelectedValue = row[columnFK].ToString();
            }
            catch (NullReferenceException) { }
        }

        public string GetID(string table, string columnID)
        {
            dt = FillTable(table, null);

            return dt.Rows[rowsCount - 1][columnID].ToString();
        }

        public int GetRowsCount(string table)
        {
            dt = FillTable(table, null);

            return dt.Rows.Count;
        }

        public DataTable GetDataTable(string table)
        {
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM " + table + " WHERE Is_deleted='0'", connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public int CheckNames(string table, string name)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Check_" + table;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", name);
            var returnParameter = cmd.Parameters.Add("@Code", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(returnParameter.Value);
        }

        public int CheckModel(string name, string FKbrand, string FKtype)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Check_Model";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@FK_brand", Convert.ToInt32(FKbrand));
            cmd.Parameters.AddWithValue("@FK_type", Convert.ToInt32(FKtype));
            var returnParameter = cmd.Parameters.Add("@Code", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(returnParameter.Value);
        }

        public void UpdateTableBrandsTypes(string table, string textFromTB, string id)
        {
            string columnID;
            if (table == "Brands")
                columnID = "brand";
            else
                columnID = "type";

            cmd = new SqlCommand("UPDATE " + table + " SET Name='" + textFromTB + "' WHERE ID_" + columnID + "='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_" + table);
        }

        public void UpdateTableModels(string idFromCB1, string idFromCB2, string textFromTB, string id)
        {
            cmd = new SqlCommand("UPDATE Models SET FK_brand='" + idFromCB1 + "', FK_type='" + idFromCB2 + "', Name='" + textFromTB + "' WHERE ID_model='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Models");
        }

        public void UpdateTableDevices(string idFromCB, string textFromTB, string id)
        {
            cmd = new SqlCommand("UPDATE Devices SET FK_model='" + idFromCB + "', Price='" + textFromTB + "' WHERE ID_device='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Devices");
        }

        public void UpdateTableClientsStaff(string table, string textFromTB1, string textFromTB2, string textFromTB3, string textFromTB4, string textFromTB5, string id)
        {
            string columnID;
            if (table == "Clients")
                columnID = "client";
            else
                columnID = "staff";

            cmd = new SqlCommand("UPDATE " + table + " SET Surname='" + textFromTB1 + "', Name='" + textFromTB2 + "', Patronymic='" + textFromTB3 + "', Phone='" + textFromTB4 + "', Addres='" + textFromTB5 + "' WHERE ID_" + columnID + "='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_" + table);
        }

        public void UpdateTableSuppliers(string textFromTB1, string textFromTB2, string textFromTB3, string id)
        {
            cmd = new SqlCommand("UPDATE Suppliers SET Name='" + textFromTB1 + "', Phone='" + textFromTB2 + "', Addres='" + textFromTB3 + "' WHERE ID_supplier='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Suppliers");
        }

        public void UpdateTableApplications(string idFromCB1, string idFromCB2, string d, string id)
        {
            cmd = new SqlCommand("UPDATE Applications SET FK_client='" + idFromCB1 + "', FK_staff='" + idFromCB2 + "', Date_application='" + d + "' WHERE ID_application='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Applications");
        }

        public void UpdateRecordsOfApplication(string idFromCB1, string idFromCB2, string textFromTB, string id)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Update_RecordOfApplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_application", Convert.ToInt32(idFromCB1));
            cmd.Parameters.AddWithValue("@FK_device", Convert.ToInt32(idFromCB2));
            cmd.Parameters.AddWithValue("@Count_devices", Convert.ToInt32(textFromTB));
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfApplication");
        }

        public int GetCountDevicesInStorage(string FK_device)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Get_CountDevicesInStorage";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_device", Convert.ToInt32(FK_device));
            var returnParameter = cmd.Parameters.Add("@Count", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(returnParameter.Value);
        }

        public void UpdateOrders(string idFromCB1, string idFromCB2, string textFromTB1, string textFromTB2, string id)
        {
            cmd = new SqlCommand("UPDATE Orders SET FK_staff='" + idFromCB1 + "', FK_device='" + idFromCB2 + "', Count_devices='" + textFromTB1 + "', Date_order='" + textFromTB2 + "' WHERE ID_order='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Orders");
        }

        public void UpdateSales(string idFromCB, string textFromTB, string id)
        {
            cmd = new SqlCommand("UPDATE Sales SET FK_application='" + idFromCB + "', Date_sale='" + textFromTB + "' WHERE ID_sale='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Sales");
        }

        public void UpdateRecordsOfSale(string idFromCB1, string idFromCB2, string textFromTB, string id)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Update_RecordOfSale";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_sale", Convert.ToInt32(idFromCB1));
            cmd.Parameters.AddWithValue("@FK_record_app", Convert.ToInt32(idFromCB2));
            cmd.Parameters.AddWithValue("@Count_devices", Convert.ToInt32(textFromTB));
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfSale");
        }

        public int GetFkApplication(string FK_sale)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Get_FKapplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_sale", Convert.ToInt32(FK_sale));
            var returnParameter = cmd.Parameters.Add("@FK_application", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(returnParameter.Value);
        }

        public int GetCountDevicesInRecordsOfApplication(string FK_record)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Get_CountDevicesInRecordsOfApplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_record", Convert.ToInt32(FK_record));
            var returnParameter = cmd.Parameters.Add("@Count", SqlDbType.Int);
            returnParameter.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();

            return Convert.ToInt32(returnParameter.Value);
        }

        public void InsertTableBrandsTypes(string table, string textFromTB)
        {
            cmd = new SqlCommand("INSERT INTO " + table + " (Name) VALUES ('" + textFromTB + "')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_" + table);
        }

        public void InsertTableModels(string idFromCB1, string idFromCB2, string textFromTB)
        {
            cmd = new SqlCommand("INSERT INTO Models (FK_brand, FK_type, Name) VALUES ('" + idFromCB1 + "', '" + idFromCB2 + "', '" + textFromTB + "')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Models");
        }

        public void InsertTableDevices(string idFromCB, string textFromTB)
        {
            cmd = new SqlCommand("INSERT INTO Devices (FK_model, Price) VALUES ('" + idFromCB + "', '" + textFromTB + "')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Devices");
        }

        public void InsertTableClientsStaff(string table, string textFromTB1, string textFromTB2, string textFromTB3, string textFromTB4, string textFromTB5, bool select)
        {
            cmd = new SqlCommand("INSERT INTO " + table + " (Surname, Name, Patronymic, Phone, Addres) VALUES ('" + textFromTB1 + "', '" + textFromTB2 + "', '" + textFromTB3 + "', '" + textFromTB4 + "', '" + textFromTB5 + "')", connection);
            cmd.ExecuteNonQuery();

            if (select)
                SelectTable("View_" + table);
        }

        public void InsertTableSuppliers(string textFromTB1, string textFromTB2, string textFromTB3)
        {
            cmd = new SqlCommand("INSERT INTO Suppliers (Name, Phone, Addres) VALUES ('" + textFromTB1 + "', '" + textFromTB2 + "', '" + textFromTB3 + "')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Suppliers");
        }

        public void InsertTableApplications(string idFromCB1, string idFromCB2, string textFromTB)
        {
            cmd = new SqlCommand("INSERT INTO Applications (FK_client, FK_staff, Date_application, Status_application) VALUES ('" + idFromCB1 + "', '" + idFromCB2 + "', '" + textFromTB + "', 'Оформляется')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Applications");
        }

        public void InsertTableRecordsOfApplication(string idFromCB1, string idFromCB2, string textFromTB)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Add_RecordOfApplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_application", Convert.ToInt32(idFromCB1));
            cmd.Parameters.AddWithValue("@FK_device", Convert.ToInt32(idFromCB2));
            cmd.Parameters.AddWithValue("@Count_devices", Convert.ToInt32(textFromTB));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfApplication");
        }

        public void InsertTableOrders(string idFromCB1, string idFromCB2, string textFromTB1, string textFromTB2)
        {
            cmd = new SqlCommand("INSERT INTO Orders (FK_staff, FK_device, Count_devices, Date_order, Status_order) VALUES ('" + idFromCB1 + "', '" + idFromCB2 + "', '" + textFromTB1 + "', '" + textFromTB2 + "', 'Открыт')", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Orders");
        }

        public void InsertTableDelivery(string idFromCB1, string idFromCB2)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Add_Delivery";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_supplier", Convert.ToInt32(idFromCB1));
            cmd.Parameters.AddWithValue("@FK_order", Convert.ToInt32(idFromCB2));
            cmd.ExecuteNonQuery();

            SelectTable("View_Delivery");
        }

        public void InsertTableSales(string idFromCB, string textFromTB)
        {
            cmd = new SqlCommand("INSERT INTO Sales (FK_application, Date_sale) VALUES ('" + idFromCB + "', '" + textFromTB + "')", connection);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("UPDATE Applications SET Status_application='Утверждён' WHERE ID_application='" + idFromCB + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_Sales");
        }

        public void InsertTableRecordsOfSale(string idFromCB1, string idFromCB2, string textFromTB)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Add_RecordOfSale";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FK_sale", Convert.ToInt32(idFromCB1));
            cmd.Parameters.AddWithValue("@FK_record_app", Convert.ToInt32(idFromCB2));
            cmd.Parameters.AddWithValue("@Count_devices", Convert.ToInt32(textFromTB));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfSale");
        }

        public void DeleteFromTable(string table, string idColumn, string id)
        {
            cmd = new SqlCommand("UPDATE " + table + " SET Is_deleted='1' WHERE " + idColumn + "='" + id + "'", connection);
            cmd.ExecuteNonQuery();

            SelectTable("View_" + table);
        }

        public void DeleteRecordOfApplication(string id)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Delete_RecordOfApplication";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfApplication");
        }

        public void DeleteRecordOfSale(string id)
        {
            cmd = connection.CreateCommand();
            cmd.CommandText = "Delete_RecordOfSale";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id));
            cmd.ExecuteNonQuery();

            SelectTable("View_RecordsOfSale");
        }
    }
}