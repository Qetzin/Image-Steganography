using System.Text;
using System.Windows.Forms;

namespace Image_Steganography
{
    public partial class Form1 : Form
    {
        public bool windowEmpty = true;
        public Bitmap bmp;
        public Bitmap result;

        private object lockObject = new object();
        public Form1()
        {
            InitializeComponent();
            if (windowEmpty) 
            { 
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }
        public static int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }

            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose a Photo";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(selectedFileName);
                bmp = (Bitmap)pictureBox1.Image;
                windowEmpty = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose a Photo";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFileName = openFileDialog.FileName;
                pictureBox1.Image = Image.FromFile(selectedFileName);
                bmp = (Bitmap)pictureBox1.Image;
                windowEmpty = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!windowEmpty)
            {
                Form2 inputForm = new Form2(this, bmp);
                DialogResult result = inputForm.ShowDialog();
                if (result == DialogResult.OK)
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string extractedText = String.Empty;
            if (!windowEmpty)
            {
                int colorUnitIndex = 0;
                int charValue = 0;

                int widthShort = (int)bmp.Width / 5;
                int heightShort = (int)bmp.Height / 5;

                for (int i = 0; i < heightShort; i++)
                {
                    for (int j = 0; j < widthShort; j++)
                    {
                        Color pixel = bmp.GetPixel(j, i);

                        for (int n = 0; n < 3; n++)
                        {
                            switch (colorUnitIndex % 3)
                            {
                                case 0:
                                    {
                                        charValue = charValue * 2 + pixel.R % 2;
                                    }
                                    break;
                                case 1:
                                    {
                                        charValue = charValue * 2 + pixel.G % 2;
                                    }
                                    break;
                                case 2:
                                    {
                                        charValue = charValue * 2 + pixel.B % 2;
                                    }
                                    break;
                            }

                            colorUnitIndex++;

                            if (colorUnitIndex % 8 == 0)
                            {

                                charValue = reverseBits(charValue);
                                if (charValue == 0)
                                {

                                }
                                char c = (char)charValue;
                                extractedText += c.ToString();
                            }
                        }
                    }
                }
                MessageBox.Show(extractedText, "Decoded Message");
            }
        }
        public void SetResult(Bitmap image)
        {
            this.result = image;
        }
        public void SetImage()
        {
            pictureBox1.Image = result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!windowEmpty)
            {
                SaveFileDialog ifile = new SaveFileDialog();
                ifile.FileName = ".png";

                if (ifile.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(ifile.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}