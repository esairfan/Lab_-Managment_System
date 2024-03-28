using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBproject
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Studentbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student stu = new Student();
            stu.Show();
        }

        private void Clobtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            CLO cLO = new CLO();
            cLO.Show();
        }

        private void assesmentbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageAssesment manageAssesment = new ManageAssesment();
            manageAssesment.Show();
        }

        private void Rubricbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Rubric rubric = new Rubric();
            rubric.Show();
        }

        private void RLbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            RubricLevel rubricLevel = new RubricLevel();
            rubricLevel.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
