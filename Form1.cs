using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Data;
using System.Diagnostics;
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
            Detect();
            DrawBBAsync();
            sizeDGV(dataGridView1);
        }
        private void Detect()
        {
            if (comboBox2.SelectedIndex == 1)
            {
                return;
            }

        }

        private async Task DrawBBAsync()
        {
            DataTable.Rows.Clear();
            using var image = await Image.LoadAsync<Rgba32>(PathImage);
            string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\coord.txt";

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            // Read all lines from the file
            string[] lines = File.ReadAllLines(filePath);

            // List to store parsed coordinates
            List<BoundingBox> boundingBoxes = new List<BoundingBox>();

            // Parse each line
            foreach (var line in lines)
            {
                // Split the line into parts
                string[] parts = line.Split(' ');

                if (parts.Length == 6)
                {
                    string className = parts[0];
                    float confidence;
                    int x1, y1, x2, y2;

                    // Parse the values
                    if (float.TryParse(parts[1], out confidence) &&
                        int.TryParse(parts[2], out x1) &&
                        int.TryParse(parts[3], out y1) &&
                        int.TryParse(parts[4], out x2) &&
                        int.TryParse(parts[5], out y2))
                    {
                        // Create a BoundingBox object and add it to the list
                        boundingBoxes.Add(new BoundingBox
                        {
                            ClassName = className,
                            Confidence = confidence,
                            X1 = x1,
                            Y1 = y1,
                            X2 = x2,
                            Y2 = y2
                        });
                    }
                }
            }

            // Process the bounding boxes as needed
            foreach (var box in boundingBoxes)
            {
                var font = new Font(new FontCollection().Add("C:/Windows/Fonts/consola.ttf"), 16);
                var (x, y) = (box.X1 - 3, box.Y1 - 23);
                DataRow row = DataTable.NewRow();
                row["Type"] = "ship";
                row["Confidence"] = box.Confidence;
                row["X1"] = box.X1;
                row["Y1"] = box.Y1;
                row["X2"] = box.X2;
                row["Y2"] = box.Y2;
                DataTable.Rows.Add(row);

                image.Mutate(a => a.DrawPolygon(new SolidPen(Color.Red, 3),
                    new PointF(box.X1, box.Y1),
                    new PointF(box.X2, box.Y1),
                    new PointF(box.X2, box.Y2),
                    new PointF(box.X1, box.Y2)
                ));

                image.Mutate(a => a.DrawText($"ship: ({box.Confidence})",
                    font, Color.Red, new PointF(x, y)));

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
            DataTable.Columns.Add("X1");
            DataTable.Columns.Add("Y1");
            DataTable.Columns.Add("X2");
            DataTable.Columns.Add("Y2");

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
            DataTable.Rows.Clear();

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

