using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_safe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            checkBoxes = new List<List<CheckBox>>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Кнопка начать");
            startGame();
        }
        private void startGame()
        {
            this.label1.Text = "Сейф закрыт";
            for (int x = 0; x < checkBoxesSize; ++x)
            {
                for (int y = 0; y < checkBoxesSize; ++y)
                {
                    this.Controls.Remove(checkBoxes[x][y]);
                    checkBoxes[x][y].Dispose();
                }
            }

            checkBoxes.Clear();
            checkBoxesSize = int.Parse(numericUpDownSize.Value.ToString());
            safeLogick.SetSize(checkBoxesSize);
            safeLogick.Randomization();

            for (int x = 0; x < checkBoxesSize; ++x)
            {
                List<CheckBox> column = new List<CheckBox>();
                for (int y = 0; y < checkBoxesSize; ++y)
                {
                    CheckBox cb = new CheckBox();
                    cb.Checked = safeLogick.handles[x][y];
                    cb.Location = new Point(x * 20, y * 20 + 40);
                    cb.Size = new Size(20, 20);
                    cb.CheckedChanged += new System.EventHandler(checkBoxChanged);
                    column.Add(cb);
                }
                checkBoxes.Add(column);
            }

            for (int x = 0; x < checkBoxesSize; ++x)
            {
                for (int y = 0; y < checkBoxesSize; ++y)
                {
                    this.Controls.Add(checkBoxes[x][y]);
                }
            }
        }

        private void checkBoxChanged(object sender, System.EventArgs e)
        {
            if (!checkBoxesEventActive)
                return;
            checkBoxesEventActive = false;

            Console.WriteLine("Тык галочку ");

            int pos_x = 0, pos_y = 0;
            for (int x = 0; x < checkBoxesSize; ++x)
            {
                for (int y = 0; y < checkBoxesSize; ++y)
                {
                    if (checkBoxes[x][y] == sender)
                    {
                        pos_x = x;
                        pos_y = y;
                        break;
                    }
                }
            }
            safeLogick.Turn(pos_x, pos_y);

            for (int x = 0; x < checkBoxesSize; ++x)
            {
                for (int y = 0; y < checkBoxesSize; ++y)
                {
                    checkBoxes[x][y].Checked = safeLogick.handles[x][y];
                }
            }

            //проверка выигрыша
            if (safeLogick.IsOpen())
                this.label1.Text = "Сейф открыт";
            else
                this.label1.Text = "Сейф закрыт";

            checkBoxesEventActive = true;
        }


        private List<List<CheckBox>> checkBoxes;
        private int checkBoxesSize = 0;
        private bool checkBoxesEventActive = true;
        private SafeLogick safeLogick = new SafeLogick();
    }
}
