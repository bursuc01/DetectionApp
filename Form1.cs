using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Data;
using Yolov8Net;
using Color = SixLabors.ImageSharp.Color;
using Font = SixLabors.Fonts.Font;
using Image = SixLabors.ImageSharp.Image;
using PointF = SixLabors.ImageSharp.PointF;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private string PathImage { get; set; }
        private string PathNet { get; set; }
        private DataTable DataTable;

        public Form1()
        {
            InitializeComponent();
            comboBox2.SelectedIndex = 1;
            InitializeDataGridView();
            InitializeCoordinatesLabel();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
        }
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                var x = e.X * pictureBox1.Image.Width / pictureBox1.ClientSize.Width;
                var y = e.Y * pictureBox1.Image.Height / pictureBox1.ClientSize.Height;
                coordLabel.Text = $"Coordinates: X: {x}, Y: {y}";
            }
        }

        private void InitializeCoordinatesLabel()
        {
            coordLabel.AutoSize = true;
            this.Controls.Add(coordLabel);
        }

        private async void detectBtn_Click(object sender, EventArgs e)
        {
            await DetectYolov8();
            sizeDGV(dataGridView1);
        }

        public async Task DetectYolov8()
        {
            string[] labels = new[] { "ship" };
            using var yolo = YoloV8Predictor.Create(PathNet, labels);

            using var image = await Image.LoadAsync<Rgba32>(PathImage);

            var predictions = yolo.Predict(image);

            foreach (var prediction in predictions)
            {
                var font = new Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);
                var score = Math.Round(prediction.Score, 2);

                var (x, y) = (prediction.Rectangle.Left - 3, prediction.Rectangle.Top - 23);

                DataRow row = DataTable.NewRow();
                row["Type"] = prediction.Label.Name;
                row["Confidence"] = score;
                row["X"] = prediction.Rectangle.X;
                row["Y"] = prediction.Rectangle.Y;
                DataTable.Rows.Add(row);

                image.Mutate(a => a.DrawPolygon(new SolidPen(Color.Red, 3),
                    new PointF(prediction.Rectangle.Left, prediction.Rectangle.Top),
                    new PointF(prediction.Rectangle.Right, prediction.Rectangle.Top),
                    new PointF(prediction.Rectangle.Right, prediction.Rectangle.Bottom),
                    new PointF(prediction.Rectangle.Left, prediction.Rectangle.Bottom)
                ));

                image.Mutate(a => a.DrawText($"{prediction.Label.Name} ({score})",
                    font, Color.Green, new PointF(x, y)));

            }

            await image.SaveAsync("../../../Assets/result.jpg");
            pictureBox1.Image = System.Drawing.Image.FromFile("../../../Assets/result.jpg");
        }

        void sizeDGV(DataGridView dgv)
        {
            DataGridViewElementStates states = DataGridViewElementStates.None;
            dgv.ScrollBars = ScrollBars.None;
            var totalHeight = dgv.Rows.GetRowsHeight(states) + dgv.ColumnHeadersHeight;
            totalHeight += dgv.Rows.Count * 4;
            var totalWidth = dgv.Columns.GetColumnsWidth(states) + dgv.RowHeadersWidth;
            dgv.ClientSize = new System.Drawing.Size(totalWidth, totalHeight);
        }

        private void InitializeDataGridView()
        {
            DataTable = new DataTable();
            DataTable.Columns.Add("Type");
            DataTable.Columns.Add("Confidence");
            DataTable.Columns.Add("X");
            DataTable.Columns.Add("Y");

            dataGridView1.ColumnHeadersVisible = true;
            dataGridView1.AutoSize = true;
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
            dataGridView1.DataSource = DataTable;
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            PathNet = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Assets\\" + comboBox2.Text + "\\" + "best.onnx";
        }

        private void selectBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PathImage = openFileDialog.FileName;
                    pictureBox1.Image = System.Drawing.Image.FromFile(PathImage);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

