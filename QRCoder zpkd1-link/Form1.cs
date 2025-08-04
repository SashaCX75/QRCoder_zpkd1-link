using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace QRCoder_zpkd1_link
{
    public partial class Form1 : Form
    {
        Bitmap currentQRCode;
        private bool isUpdatingText = false;

        public Form1()
        {
            InitializeComponent();

            Point centrPosition = new Point(0, 0);
            centrPosition.X = (SystemInformation.PrimaryMonitorSize.Width - 340) / 2;
            centrPosition.Y = (SystemInformation.PrimaryMonitorSize.Height - 490) / 2;

            Size size = new Size();
            int screenOffsetX = 0;
            //Получаем высоту главного экрана и сумму ширины двух мониторов.
            size.Height = SystemInformation.PrimaryMonitorSize.Height;
            //size.Width = Screen.AllScreens[0].WorkingArea.Width + Screen.AllScreens[1].WorkingArea.Width;
            foreach (Screen screen in Screen.AllScreens)
            {
                size.Width += screen.WorkingArea.Width;
                if (screen.WorkingArea.X < screenOffsetX) screenOffsetX = screen.WorkingArea.X;
            }
            size.Width -= screenOffsetX;
            if (Properties.Settings.Default.FormLocation.X >= size.Width || Properties.Settings.Default.FormLocation.Y >= size.Height ||
                Properties.Settings.Default.FormLocation.X < screenOffsetX - 340 / 2)
                Properties.Settings.Default.FormLocation = centrPosition;
        }

        private void textBoxUrl_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingText) return;
            labelStatus.Text = "❌";
            labelStatus.ForeColor = Color.Red;

            string input = textBoxUrl.Text.Trim();
            input = DecodeUrl(input);
            if (input.Contains("github.com")) input = ConvertGitHubBlobToPagesUrl(input);
            if (string.IsNullOrEmpty(input))
            {
                pictureBoxQRCode.Image = GetLogo();
                currentQRCode = null;
                return;
            }

            string corrected = CorrectUrl(input);
            CheckFileExists(corrected);

            if (corrected != input)
            {
                isUpdatingText = true;
                textBoxUrl.Text = corrected;
                textBoxUrl.SelectionStart = corrected.Length; // курсор в конец
                isUpdatingText = false;
            }

            currentQRCode = GenerateQRCodeWithLogo(corrected);
            pictureBoxQRCode.Image = currentQRCode;
        }

        public static string DecodeUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return WebUtility.UrlDecode(input);
        }

        public static string ConvertGitHubBlobToPagesUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            try
            {
                Uri uri = new Uri(url);
                string[] segments = uri.AbsolutePath.Trim('/').Split('/');

                if (segments.Length >= 5 &&
                    segments[2] == "blob" &&
                    segments[3] == "main")
                {
                    string user = segments[0];
                    string repo = segments[1];
                    string path = string.Join("/", segments.Skip(4));

                    // Проверка: если репозиторий называется UserName.github.io
                    if (repo.Equals($"{user}.github.io", StringComparison.OrdinalIgnoreCase))
                    {
                        return $"https://{user}.github.io/{path}";
                    }
                    else
                    {
                        return $"https://{user}.github.io/{repo}/{path}";
                    }
                }
            }
            catch
            {
                // Игнорируем ошибки и возвращаем исходную ссылку
            }

            return url;
        }


        private string CorrectUrl(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // если начинается с https:// или http:// — заменяем на zpkd1://
            if (input.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
                input.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
            {
                return input.Replace("https://", "zpkd1://")
                            .Replace("http://", "zpkd1://");
            }

            // если вообще не начинается ни с zpkd1://, ни с http(s)
            if (!input.StartsWith("zpkd1://", StringComparison.OrdinalIgnoreCase))
            {
                return "zpkd1://" + input;
            }

            return input;
        }

        private Bitmap GenerateQRCodeWithLogo(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrRaw = qrCode.GetGraphic(10, Color.Black, Color.White, GetLogo(), 20, 5, false);
                Bitmap qrCodeImage = AddQuietZone(qrRaw, quietZoneModules: 2, pixelsPerModule: 10);
                return qrCodeImage;
            }
        }
        private Bitmap AddQuietZone(Bitmap originalQr, int quietZoneModules, int pixelsPerModule)
        {
            int quietZonePixels = quietZoneModules * pixelsPerModule;

            int newSize = originalQr.Width + quietZonePixels * 2;
            Bitmap result = new Bitmap(newSize, newSize);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.Clear(Color.White); // цвет тихой зоны
                g.DrawImage(originalQr, quietZonePixels, quietZonePixels);
            }

            return result;
        }

        private Bitmap GetLogo()
        {
            // Предполагается, что logo.png добавлен в ресурсы проекта (Properties > Resources)
            return Properties.Resources.logo_qr;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (currentQRCode == null)
            {
                MessageBox.Show("First, enter the link to generate the QR code.");
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                string nameText = textBoxName.Text.Trim();
                string versionText = textBoxVersion.Text.Trim();
                if (!string.IsNullOrWhiteSpace(nameText) && !string.IsNullOrWhiteSpace(versionText)) nameText += " " + versionText;
                string fileName = "QRCode.png";
                if (!string.IsNullOrWhiteSpace(nameText)) fileName = "QRCode " + nameText + ".png";
                fileName = fileName.Replace(' ', '_');

                saveDialog.Filter = "PNG Image|*.png";
                saveDialog.Title = "Save QR code";
                saveDialog.FileName = fileName;

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    bool topText = false;
                    bool bottomText = false;
                    
                    // Добавляем обводку QR-кода
                    using (Graphics g = Graphics.FromImage(currentQRCode))
                    {
                        DrawRoundedBorder(g, currentQRCode.Width, currentQRCode.Height, cornerRadius: 15, borderWidth: 4, borderColor: Color.Black);
                    }
                    Bitmap roundedQRCode = ApplyRoundedCorners(currentQRCode, cornerRadius: 15);

                    // тип приложения
                    string type = "";
                    if (radioButton_TypeWatchFace.Checked)
                    {
                        type = "Watch face";
                    }
                    else if (radioButton_TypeApp.Checked)
                    {
                        type = "App";
                    }
                    int textAreaHeight = 30;
                    if (!string.IsNullOrWhiteSpace(nameText)) textAreaHeight = 25;
                    Bitmap imageWithType = DrawTextWithOptions(
                        roundedQRCode,
                        text: type,
                        position: "top",
                        fontSize: 16,
                        textAreaHeight: textAreaHeight,
                        backgroundColor: Color.FromArgb(255, 255, 224, 178)
                    );

                    // Имя и версия приложения
                    Bitmap imageWithName = DrawTextWithOptions(
                        imageWithType,
                        text: nameText,
                        position: "top",
                        fontSize: 24,
                        textAreaHeight: 35,
                        backgroundColor: Color.FromArgb(255, 255, 224, 178)
                    );
                    if (!string.IsNullOrWhiteSpace(nameText)) 
                        imageWithName = PadImageWithMargins(imageWithName, 0, 5, 0, 0, Color.FromArgb(255, 255, 224, 178));
                    if (imageWithName.Height > currentQRCode.Height) topText = true;

                    string modelsText = textBoxModels.Text.Trim();
                    Bitmap imageWithModels = DrawTextWrapped(
                        imageWithName,
                        text: modelsText,
                        position: "bottom",
                        fontSize: 24,
                        minTextAreaHeight: 40,
                        backgroundColor: Color.FromArgb(255, 255, 224, 178)
                    );
                    if (imageWithModels.Height > imageWithName.Height) bottomText = true;

                    if (imageWithModels.Height > currentQRCode.Height)
                    {
                        int marginLeft = 0;
                        int marginTop = 0;
                        int marginRight = 0;
                        int marginBottom = 0;

                        if (topText && bottomText)
                        {
                            marginLeft = 30;
                            marginRight = 30;
                        }
                        else if (topText && !bottomText)
                        {
                            marginLeft = 20;
                            marginRight = 20;
                            marginBottom = 20;
                        }
                        else if (!topText && bottomText)
                        {
                            marginLeft = 20;
                            marginRight = 20;
                            marginTop = 20;
                        }

                        Bitmap finalSize = PadImageWithMargins(imageWithModels, marginLeft, marginTop, marginRight, marginBottom, Color.FromArgb(255, 255, 224, 178));
                        // Добавляем обводку всего изображения
                        using (Graphics g = Graphics.FromImage(finalSize))
                        {
                            DrawRoundedBorder(g, finalSize.Width, finalSize.Height, cornerRadius: 30, borderWidth: 6, borderColor: Color.Black);
                        }
                        Bitmap finalSizeRounded = ApplyRoundedCorners(finalSize, cornerRadius: 30);
                        finalSizeRounded.Save(saveDialog.FileName, ImageFormat.Png);
                    }
                    else imageWithModels.Save(saveDialog.FileName, ImageFormat.Png);

                    if (File.Exists(saveDialog.FileName))
                    {
                        Process.Start("explorer.exe", $"/select,\"{saveDialog.FileName}\"");
                    }
                }
            }
        }

        private Bitmap DrawTextWithOptions(Bitmap image, string text, string position, int fontSize, int textAreaHeight, Color backgroundColor)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Bitmap(image); // Без текста — просто копия

            int padding = 10;
            int width = image.Width;
            int newHeight = image.Height + textAreaHeight;

            //Font font = new Font("Calibri", fontSize, FontStyle.Regular);
            Font font = GetSafeFont("Calibri", fontSize, FontStyle.Regular);
            Bitmap result = new Bitmap(width, newHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(backgroundColor);
                //g.Clear(Color.White);

                Rectangle textRect;
                Point imagePos;

                if (position.ToLower() == "top")
                {
                    // Текст сверху
                    textRect = new Rectangle(0, 0, width, textAreaHeight);
                    imagePos = new Point(0, textAreaHeight);
                }
                else
                {
                    // Текст снизу (по умолчанию)
                    textRect = new Rectangle(0, image.Height, width, textAreaHeight);
                    imagePos = new Point(0, 0);
                }

                // Отрисовка фона под текстом
                using (Brush bgBrush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(bgBrush, textRect);
                }

                // Обрезка текста при необходимости
                string fittedText = FitTextToWidth(g, text, font, width - padding * 2);

                // Рисуем изображение
                g.DrawImage(image, imagePos);

                // Рисуем текст
                using (Brush textBrush = new SolidBrush(Color.Black))
                using (StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                })
                {
                    g.DrawString(fittedText, font, textBrush, textRect, format);
                }
            }

            return result;
        }

        private Bitmap DrawTextWrapped(Bitmap image, string text, string position, int fontSize, Color backgroundColor, int minTextAreaHeight)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new Bitmap(image);

            Font font = GetSafeFont("Calibri", fontSize, FontStyle.Regular);
            int width = image.Width;
            int textPadding = 10;

            // Определяем высоту текста с переносами
            SizeF textSize;
            using (Bitmap dummyBmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(dummyBmp))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                RectangleF layout = new RectangleF(0, 0, width - textPadding * 2, float.MaxValue);
                textSize = g.MeasureString(text, font, layout.Size.ToSize(), new StringFormat());
            }

            int calculatedTextHeight = (int)Math.Ceiling(textSize.Height) + textPadding * 2;
            int textAreaHeight = Math.Max(calculatedTextHeight, minTextAreaHeight);

            int newHeight = image.Height + textAreaHeight;
            Bitmap result = new Bitmap(width, newHeight);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                //g.Clear(Color.White);
                g.Clear(backgroundColor);

                Rectangle textRect;
                Point imagePos;

                if (position.ToLower() == "top")
                {
                    textRect = new Rectangle(0, 0, width, textAreaHeight);
                    imagePos = new Point(0, textAreaHeight);
                }
                else
                {
                    textRect = new Rectangle(0, image.Height, width, textAreaHeight);
                    imagePos = new Point(0, 0);
                }

                // Фон текста
                using (Brush bgBrush = new SolidBrush(backgroundColor))
                {
                    g.FillRectangle(bgBrush, textRect);
                }

                // Рисуем изображение
                g.DrawImage(image, imagePos);

                // Рисуем текст с переносом
                using (Brush textBrush = new SolidBrush(Color.Black))
                using (StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near,
                    FormatFlags = StringFormatFlags.LineLimit
                })
                {
                    Rectangle paddedRect = new Rectangle(textRect.X + textPadding, textRect.Y + textPadding,
                                                         textRect.Width - textPadding * 2,
                                                         textRect.Height - textPadding * 2);
                    g.DrawString(text, font, textBrush, paddedRect, format);
                }
            }

            return result;
        }

        private Font GetSafeFont(string preferredFontName, float fontSize, FontStyle style, string fallbackFontName = "Segoe UI")
        {
            using (InstalledFontCollection fonts = new InstalledFontCollection())
            {
                bool hasPreferred = fonts.Families.Any(f => f.Name.Equals(preferredFontName, StringComparison.OrdinalIgnoreCase));

                string fontToUse = hasPreferred ? preferredFontName : fallbackFontName;
                return new Font(fontToUse, fontSize, style);
            }
        }

        private string FitTextToWidth(Graphics g, string text, Font font, int maxWidth)
        {
            const string ellipsis = "...";

            if (string.IsNullOrEmpty(text))
                return "";

            // Если весь текст помещается — возвращаем его
            if (g.MeasureString(text, font).Width <= maxWidth)
                return text;

            // Подрезаем посимвольно
            for (int i = text.Length - 1; i > 0; i--)
            {
                string cut = text.Substring(0, i).Trim() + ellipsis;
                if (g.MeasureString(cut, font).Width <= maxWidth)
                    return cut;
            }

            // Если даже один символ + "..." не влезает — возвращаем "..."
            return ellipsis;
        }

        private Bitmap ApplyRoundedCorners(Bitmap original, int cornerRadius)
        {
            Bitmap rounded = new Bitmap(original.Width, original.Height);
            using (Graphics g = Graphics.FromImage(rounded))
            using (GraphicsPath path = RoundedRect(new Rectangle(0, 0, original.Width, original.Height), cornerRadius))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (Brush imageBrush = new TextureBrush(original))
                {
                    g.FillPath(imageBrush, path);
                }
            }

            return rounded;
        }

        private void DrawRoundedBorder(Graphics g, int width, int height, int cornerRadius, int borderWidth, Color borderColor)
        {
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                pen.Alignment = PenAlignment.Center;
                int d = cornerRadius * 2;

                Rectangle topLeftArc = new Rectangle(0, 0, d, d);
                Rectangle topRightArc = new Rectangle(width - d, 0, d, d);
                Rectangle bottomRightArc = new Rectangle(width - d, height - d, d, d);
                Rectangle bottomLeftArc = new Rectangle(0, height - d, d, d);

                // Углы
                g.DrawArc(pen, topLeftArc, 180, 90);
                g.DrawArc(pen, topRightArc, 270, 90);
                g.DrawArc(pen, bottomRightArc, 0, 90);
                g.DrawArc(pen, bottomLeftArc, 90, 90);

                // Прямые линии между углами
                g.DrawLine(pen, cornerRadius, 0, width - cornerRadius, 0);               // Верх
                g.DrawLine(pen, width, cornerRadius, width, height - cornerRadius);      // Право
                g.DrawLine(pen, width - cornerRadius, height, cornerRadius, height);     // Низ
                g.DrawLine(pen, 0, height - cornerRadius, 0, cornerRadius);              // Лево
            }
        }

        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            // 4 дуги
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        private Bitmap PadImageWithMargins(Bitmap original, int marginLeft, int marginTop, int marginRight, int marginBottom, Color backgroundColor)
        {
            int newWidth = original.Width + marginLeft + marginRight;
            int newHeight = original.Height + marginTop + marginBottom;

            Bitmap padded = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(padded))
            {
                g.Clear(backgroundColor);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                g.DrawImage(original, marginLeft, marginTop, original.Width, original.Height);
            }

            return padded;
        }

        //public static bool UrlFileExists(string url)
        //{
        //    url = url.Replace("zpkd1://", "http://");
        //    try
        //    {
        //        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //        request.Method = "HEAD"; // Только заголовки, без загрузки содержимого
        //        request.Timeout = 5000;  // Таймаут 5 секунд
        //        request.AllowAutoRedirect = true;

        //        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //        {
        //            return response.StatusCode == HttpStatusCode.OK;
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        if (ex.Response is HttpWebResponse response)
        //        {
        //            return response.StatusCode == HttpStatusCode.OK;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public static async Task<bool> UrlFileExistsAsync(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(5);

                    using (var response = await client.SendAsync(
                        new HttpRequestMessage(HttpMethod.Head, url)))
                    {
                        return response.IsSuccessStatusCode;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async void CheckFileExists(string url)
        {
            url = url.Replace("zpkd1://", "http://");
            bool exists = await UrlFileExistsAsync(url);

            //labelStatus.Text = exists ? "Файл найден ✅" : "Файл не найден ❌";
            if (exists) {
                labelStatus.Text = "✅";
                labelStatus.ForeColor = Color.Green;
            }
            else
            {
                labelStatus.Text = "❌";
                labelStatus.ForeColor = Color.Red;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label_version.Text = "v " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
        }
    }
}
