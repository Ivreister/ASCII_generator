namespace ASCII_Art_Generator
{
    internal class Program
    {
        private const double WIDTH_OFFSET = 1.7;
        private const int MAX_WIDTH = 474;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            { 
                Filter = "Images | *.bmp; *.png; *jpg; *.JPEG"
            };

            Console.WriteLine("Press enter to start...\n");

            while (true) 
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                var bitmap = new Bitmap (openFileDialog.FileName);
                bitmap = ResizeBitmap (bitmap);
                bitmap.ToGrayscale ();

                var converter = new BitmapToASCIIConverter (bitmap);
                var rows = converter.Convert();

                foreach (var row in rows)
                {
                    Console.WriteLine(row);
                }

                File.WriteAllLines("img.txt", rows.Select(r => new string(r)));

                Console.SetCursorPosition (0, 0);
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            }
            return bitmap;
        }
    }
}
