using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entity_framwork_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        public void LoadData()
        {
            Model1 db = new Model1();

            var student = db.Students.Select(s => new
            {
                s.mssv,
                s.tenSinhVien,
                s.Class.tenLop,
            }).ToList();

            dataGridView1.DataSource = student;

            dataGridView1.Columns["mssv"].HeaderText = "Mã Sinh Viên";
            dataGridView1.Columns["tenSinhVien"].HeaderText = "Tên Sinh Viên";
            dataGridView1.Columns["tenLop"].HeaderText = "Tên Lớp";

        }

        public void Add()
        {
            string mssv = txbId.Text;
            string tenSinhVien = txbName.Text;
            string tenLop = txbClass.Text;

            if (string.IsNullOrWhiteSpace(mssv) || string.IsNullOrWhiteSpace(tenSinhVien) || string.IsNullOrWhiteSpace(tenLop))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            Model1 db = new Model1();
            var lop = db.Classes.FirstOrDefault(c => c.tenLop == tenLop);

            if (lop == null)
            {
                lop = new Class
                {
                    tenLop = tenLop,
                };
                db.Classes.Add(lop);
                db.SaveChanges();
            }

            var student = new Student
            {
                mssv = mssv,
                tenSinhVien = tenSinhVien,
                maLop = lop.maLop,
            };
            db.Students.Add(student);
            db.SaveChanges();

            txbId.Clear();
            txbName.Clear();
            txbClass.Clear();

            LoadData();

        }

        public void Delete()
        {
            Model1 db = new Model1();
            if (dataGridView1.SelectedRows.Count > 0)
            {

                string studentMssv = dataGridView1.SelectedRows[0].Cells["mssv"].Value.ToString();
                var student = db.Students.FirstOrDefault(s => s.mssv == studentMssv);
                if(student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                    MessageBox.Show("Xoa sinh vien thanh cong");

                    LoadData();
                }
                else
                {
                    MessageBox.Show("sinh vien khong ton tai");
                }
            }
            else
            {
                MessageBox.Show("Chon mot dong de xoa");
            }
        }

        public void Update()
        {
            Model1 db = new Model1();
            string studentMssv = txbId.Text;

            if (string.IsNullOrWhiteSpace(studentMssv) || string.IsNullOrWhiteSpace(txbName.Text) || string.IsNullOrWhiteSpace(txbClass.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            var student = db.Students.FirstOrDefault(s => s.mssv == studentMssv);

            if (student != null)
            {
                student.tenSinhVien = txbName.Text;
                var lop = db.Classes.FirstOrDefault(c => c.tenLop == txbClass.Text);
                if (lop == null)
                {
                    lop = new Class { tenLop = txbClass.Text };
                    db.Classes.Add(lop);
                    db.SaveChanges();
                }

                student.maLop = lop.maLop;  

                db.SaveChanges();

                MessageBox.Show("Cap nhat thong tin sinh vien thanh cong");

                LoadData();
            }
            else
            {
                MessageBox.Show("Sinh vien khong ton tai");
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ dòng được chọn và hiển thị lên TextBox
                txbId.Text = dataGridView1.SelectedRows[0].Cells["mssv"].Value.ToString();
                txbName.Text = dataGridView1.SelectedRows[0].Cells["tenSinhVien"].Value.ToString();
                txbClass.Text = dataGridView1.SelectedRows[0].Cells["tenLop"].Value.ToString();
            }
        }
    }
}
