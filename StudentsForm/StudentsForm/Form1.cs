using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StudentsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;




        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (open.ShowDialog() == DialogResult.OK)
            {
                ReadFile(open.FileName);
            }
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
           

        }

        private void ReadFile(String filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++)
            {
                Person student = Person.InputData(lines[i]);
                student.addToGrid(grid);
            }
        }

        private void SaveGridDataToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    writer.Write(grid.Columns[i].HeaderText);
                    if (i < grid.Columns.Count - 1)
                        writer.Write("\t");
                }
                writer.WriteLine();

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        for (int i = 0; i < grid.Columns.Count; i++)
                        {
                            writer.Write(row.Cells[i].Value.ToString());
                            if (i < grid.Columns.Count - 1)
                                writer.Write("\t");
                        }
                        writer.WriteLine();
                    }
                }
            }

            MessageBox.Show("Results saved successfully!");

            try
            {
                Process.Start("notepad.exe", filePath);
            }
            catch (Exception ex)
            {
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (save.ShowDialog() == DialogResult.OK)
            {
                SaveGridDataToFile(save.FileName);
            }

        }
    }
}
