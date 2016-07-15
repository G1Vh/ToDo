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
            if (!File.Exists("db.sdf"))
            {
                MessageBox.Show("База данных не обнаружена! Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            RefreshList();
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
            FillTable("SELECT * FROM List");
            ToDoList.DataSource = _bindingSource;
            ToDoList.Columns[0].Visible = false;
            ToDoList.Columns[1].HeaderText = "Наименование";
            ToDoList.Columns[2].HeaderText = "Крайний срок";
            ToDoList.Columns[3].HeaderText = "Периодичность";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (ToDoList.SelectedRows.Count == 0) return;
            string cell1 = ToDoList.SelectedRows[0].Cells[1].Value.ToString();
            string cell3 = ToDoList.SelectedRows[0].Cells[3].Value.ToString();

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
            if (fromTextBox.Text == "" || toTextBox.Text == "") return;
            try
            {
                string fromDate = Convert.ToDateTime(fromTextBox.Text).ToString("yyyyMMdd");
                string toDate = Convert.ToDateTime(toTextBox.Text).ToString("yyyyMMdd");
                FillTable("SELECT * FROM List WHERE Deadline BETWEEN '" + fromDate + "' AND '" + toDate + "'");
                ToDoList.DataSource = _bindingSource;
            }
            catch (FormatException ex)
            {
                ErrorLog(ex);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != "")
            {
                DateTime deadline = Convert.ToDateTime(deadlineCalendar.SelectionRange.Start.ToShortDateString());
                int daysLeft = DateTime.DaysInMonth(deadlineCalendar.SelectionRange.Start.Year, deadlineCalendar.SelectionRange.Start.Month) - deadlineCalendar.SelectionRange.Start.Day;
                int monthsLeft = 12 - deadlineCalendar.SelectionRange.Start.Month;
                switch (repeatBox.Text)
                {
                    case "Каждый день":
                        for (int i = 0; i < daysLeft+1; i++)
                        {
                            deadline = Convert.ToDateTime(deadlineCalendar.SelectionRange.Start.ToShortDateString()).AddDays(i);
                            AddToDb(deadline);
                        }
                        break;
                    case "Каждую неделю":
                        for (int i = 0; i < daysLeft+1;i++)
                        {
                            deadline = Convert.ToDateTime(deadlineCalendar.SelectionRange.Start.ToShortDateString()).AddDays(i);
                            AddToDb(deadline);
                            i += 6;
                        }
                        break;
                    case "Каждый месяц":
                        for (int i = 0; i < monthsLeft+1; i++)
                        {
                            deadline = Convert.ToDateTime(deadlineCalendar.SelectionRange.Start.ToShortDateString()).AddMonths(i);
                            AddToDb(deadline);
                        }
                        break;
                    default:
                        AddToDb(deadline);
                        break;
                }
                nameTextBox.Text = "";
                deadlineCalendar.SetDate(DateTime.Now);
                repeatBox.Text = "";
                addBox.Text = "Добавление дела";
                addButton.Text = "Добавить";
            }
            else
                MessageBox.Show("Заполните поле 'Наименование'", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

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
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex);
            }
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (ToDoList.SelectedRows.Count == 0) return;
            addBox.Text = "Изменение дела";
            addButton.Text = "Изменить";

            nameTextBox.Text = ToDoList.SelectedRows[0].Cells[1].Value.ToString();
            deadlineCalendar.SetSelectionRange(Convert.ToDateTime(ToDoList.SelectedRows[0].Cells[2].Value.ToString()),
                Convert.ToDateTime(ToDoList.SelectedRows[0].Cells[2].Value.ToString()));
            repeatBox.Text = ToDoList.SelectedRows[0].Cells[3].Value.ToString();

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
            fromTextBox.Clear();
            toTextBox.Clear();
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
    }
}
