using System.Diagnostics;

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
            await DisplayAlert("Nicht unterstützt", "Die Kamera-Funktion ist nur unter Windows implementiert.", "OK");
#endif
        }
    }
}
