using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Media;
using CommunityToolkit.Maui;

namespace SSB_MAUI_Foto
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnCameraClicked(object sender, EventArgs e)
        {
#if WINDOWS
            // Öffnet die Windows Kamera/Foto App
            Process.Start(new ProcessStartInfo
            {
                FileName = "microsoft.windows.camera:",
                UseShellExecute = true
            });
#else
            try
            {
                // Foto aufnehmen
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null)
                {
                    await DisplayAlert("Abgebrochen", "Kein Foto aufgenommen.", "OK");
                    return;
                }

                // Zielverzeichnis bestimmen
                string picturesPath = FileSystem.Current.AppDataDirectory;
                string targetDir = Path.Combine(picturesPath, "Fotos");
                Directory.CreateDirectory(targetDir);

                // Nächste fortlaufende Nummer bestimmen
                string datePrefix = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var existingFiles = Directory.GetFiles(targetDir, $"{datePrefix}_*.jpg");
                int nextNumber = 1;
                if (existingFiles.Length > 0)
                {
                    var numbers = existingFiles.Select(f =>
                    {
                        var name = Path.GetFileNameWithoutExtension(f);
                        var parts = name.Split('_');
                        if (parts.Length == 2 && int.TryParse(parts[1], out int n))
                            return n;
                        return 0;
                    });
                    nextNumber = numbers.Max() + 1;
                }
                string fileName = $"{datePrefix}_{nextNumber:D6}.jpg";
                string filePath = Path.Combine(targetDir, fileName);

                // Foto speichern
                using (var stream = await photo.OpenReadAsync())
                using (var fileStream = File.OpenWrite(filePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                await DisplayAlert("Erfolg", $"Foto gespeichert: {fileName}", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Fehler", $"{ex.Message}", "OK");
            }
#endif
        }
    }
}
