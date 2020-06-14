using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Assembler
{
    public partial class SplachScreen : Form
    {
        Thread mThread;

        public SplachScreen()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                rectangleShape2.Width += 5;
                if (rectangleShape2.Width >= 516) 
                {
                    timer1.Stop();
                    this.Close();
                    mThread = new Thread(openMain);
                    mThread.SetApartmentState(ApartmentState.STA);
                    mThread.Start();
                }
            }
            catch (Exception)
            {
                return;
            }

        }

        private void openMain(object obj)
        {
            Application.Run(new Main());
        }
    }
}
