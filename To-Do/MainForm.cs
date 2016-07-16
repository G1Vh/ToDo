using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Windows.Forms;

namespace To_Do
{
    public partial class MainForm : Form
    {
        private const string ConnectionString = "Data Source=|DataDirectory|\\db.sdf";
        private SqlCeDataAdapter _dataAdapter;
        private SqlCeCommandBuilder _commandBuilder;
        private BindingSource _bindingSource;
        private DataTable _dataTable;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckDB();
            RefreshList();

            //Default repeat value - Нет.
            //Значение повторяемости по умолчанию - Нет.

            repeatBox.SelectedIndex = 0;
        }

        private void CheckDB()
        {
            if (File.Exists("db.sdf")) return;
            using (SqlCeEngine engine = new SqlCeEngine(ConnectionString))
            {
                try
                {
                    engine.CreateDatabase();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"При создании базы возникла ошибка!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorLog(ex);
                    Application.Exit();
                }
            }
            using (SqlCeConnection connection = new SqlCeConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCeCommand command = new SqlCeCommand())
                {
                    command.Connection = connection;
                    command.CommandText =
                        "CREATE TABLE List (id int IDENTITY (1,1) NOT NULL PRIMARY KEY, Name nvarchar(100) NOT NULL, Deadline datetime NOT NULL, Repeat nvarchar(13) NOT NULL)";
                    command.ExecuteNonQuery();
                }
            }
        }

        private void FillTable(string selectCommand)
        {
            try
            {
                _dataAdapter = new SqlCeDataAdapter(selectCommand, ConnectionString);
                _commandBuilder = new SqlCeCommandBuilder(_dataAdapter);
                _dataTable = new DataTable();
                _dataAdapter.Fill(_dataTable);
                _bindingSource = new BindingSource {DataSource = _dataTable};
            }
            catch (SqlCeException ex)
            {
                ErrorLog(ex);
            }
            catch (InvalidOperationException ex)
            {
                ErrorLog(ex);
            }
        }

        private void RefreshList()
        {
            try
            {
                FillTable("SELECT * FROM List");
                ToDoList.DataSource = _bindingSource;
                ToDoList.Columns[0].Visible = false;
                ToDoList.Columns[1].HeaderText = @"Наименование";
                ToDoList.Columns[2].HeaderText = @"Крайний срок";
                ToDoList.Columns[3].HeaderText = @"Периодичность";
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ErrorLog(ex);
                if (MessageBox.Show(@"При чтении из базы возникла ошибка, вероятно, база повреждена! Пересоздать базу?",
                    @"Ошибка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    File.Delete("db.sdf");
                    CheckDB();
                    RefreshList();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ToDoList.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"Выберите дело для удаления.", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            string cell1 = ToDoList.SelectedRows[0].Cells[1].Value.ToString();
            string cell3 = ToDoList.SelectedRows[0].Cells[3].Value.ToString();

            //Search for identical strings
            //Поиск одинаковых строк

            for (int i = ToDoList.Rows.Count - 1; i > -1; i--)
            {
                DataGridViewRow row = ToDoList.Rows[i];
                if (cell1 == row.Cells[1].Value.ToString() &
                    cell3 == row.Cells[3].Value.ToString())
                {
                    try
                    {
                        ToDoList.Rows.Remove(row);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog(ex);
                    }
                }
            }
            _dataAdapter.Update((DataTable) _bindingSource.DataSource);
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            string fromDate = fromDatePicker.Value.ToString("yyyyMMdd");
            string toDate = toDatePicker.Value.ToString("yyyyMMdd");
            FillTable($"SELECT * FROM List WHERE Deadline BETWEEN '{fromDate}' AND '{toDate}'");
            ToDoList.DataSource = _bindingSource;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                MessageBox.Show(@"Заполните поле ""Наименование"".", @"Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                return;
            }

            string selectedShortDate = deadlineCalendar.SelectionRange.Start.ToShortDateString();
            int selectedDay = deadlineCalendar.SelectionRange.Start.Day;
            int selectedMonth = deadlineCalendar.SelectionRange.Start.Month;
            int selectedYear = deadlineCalendar.SelectionRange.Start.Year;
            int daysLeft = DateTime.DaysInMonth(selectedYear, selectedMonth) - selectedDay;
            int monthsLeft = 12 - selectedMonth;
            DateTime deadline = Convert.ToDateTime(selectedShortDate);

            switch (repeatBox.Text)
            {
                case "Каждый день":
                    for (int i = 0; i < daysLeft + 1; i++)
                    {
                        deadline = Convert.ToDateTime(selectedShortDate).AddDays(i);
                        AddToDb(deadline);
                    }
                    break;
                case "Каждую неделю":
                    for (int i = 0; i < daysLeft + 1; i++)
                    {
                        deadline = Convert.ToDateTime(selectedShortDate).AddDays(i);
                        AddToDb(deadline);
                        i += 6;
                    }
                    break;
                case "Каждый месяц":
                    for (int i = 0; i < monthsLeft + 1; i++)
                    {
                        deadline = Convert.ToDateTime(selectedShortDate).AddMonths(i);
                        AddToDb(deadline);
                    }
                    break;
                default:
                    AddToDb(deadline);
                    break;
            }
            nameTextBox.Clear();
            deadlineCalendar.SetDate(DateTime.Now);

            //Multitasking labels
            //Многозадачные лэйблы

            addBox.Text = @"Добавление дела";
            addButton.Text = @"Добавить";



            RefreshList();
        }

        private void AddToDb(DateTime deadline)
        {
            try
            {
                using (SqlCeConnection connection = new SqlCeConnection(ConnectionString))
                {
                    connection.Open();
                    string sqlquery = ("INSERT INTO List ([Name],[Deadline],[Repeat]) " +
                                       "VALUES (@name, @deadline, @repeat)");
                    SqlCeCommand cmd = new SqlCeCommand(sqlquery, connection);
                    cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
                    cmd.Parameters.AddWithValue("@deadline", deadline);
                    cmd.Parameters.AddWithValue("@repeat", repeatBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Ошибка записи в базу!", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorLog(ex);
            }
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (ToDoList.SelectedRows.Count == 0)
            {
                MessageBox.Show(@"Выберите дело для изменения.", @"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            //Multitasking labels
            //Многозадачные лэйблы

            addBox.Text = @"Изменение дела";
            addButton.Text = @"Изменить";

            //Запись значений таблицы в текстбоксы.
            //Copy table values to textboxes.
            string selectionCells1 = ToDoList.SelectedRows[0].Cells[1].Value.ToString();
            string selectionCells2 = ToDoList.SelectedRows[0].Cells[2].Value.ToString();
            string selectionCells3 = ToDoList.SelectedRows[0].Cells[3].Value.ToString();

            nameTextBox.Text = selectionCells1;
            deadlineCalendar.SetSelectionRange(Convert.ToDateTime(selectionCells2), Convert.ToDateTime(selectionCells2));
            repeatBox.Text = selectionCells3;

            //Remove old data from table.
            //Удаление из таблицы устаревших данных.
            
            for (int i = ToDoList.Rows.Count-1; i >-1; i--)
            {
                DataGridViewRow row = ToDoList.Rows[i];
                if (nameTextBox.Text == row.Cells[1].Value.ToString() &
                    repeatBox.Text == row.Cells[3].Value.ToString())
                {
                    ToDoList.Rows.Remove(row);
                }
            }
            _dataAdapter.Update((DataTable)_bindingSource.DataSource);
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            RefreshList();
            fromDatePicker.Value = DateTime.Today;
            toDatePicker.Value = DateTime.Today;
        }

        private static void ErrorLog(Exception ex)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("Error.log", true))
                {
                    writer.WriteLine("Time: " + DateTime.Now);
                    writer.WriteLine("Message: " + ex.Message);
                    writer.WriteLine("Trace: " + ex.StackTrace);
                    writer.WriteLine("-----------------------------------------------------------------------------");
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void fromDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (toDatePicker.Value < fromDatePicker.Value) toDatePicker.Value = fromDatePicker.Value;
        }

        private void toDatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (toDatePicker.Value < fromDatePicker.Value) fromDatePicker.Value = toDatePicker.Value;
        }
    }
}
