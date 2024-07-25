using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace AutosApp2
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<string, Auto> autos;

        public MainPage()
        {
            InitializeComponent();
            LoadAutosDataAsync();
        }

        private async void LoadAutosDataAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // URL del archivo JSON
                    string url = "https://raw.githubusercontent.com/JUANCITOPENA/ARCHIVO_JSON_FOTOS_AUTOS_CLASE/main/autos";

                    // Obtener el JSON de la URL
                    string json = await client.GetStringAsync(url);

                    // Deserializar el JSON en un JObject
                    JObject jsonObject = JObject.Parse(json);

                    // Extraer la propiedad "autos" del JSON
                    JObject autosObject = (JObject)jsonObject["autos"];

                    // Inicializar el diccionario de autos
                    autos = autosObject.ToObject<Dictionary<string, Auto>>();

                    // Actualizar el Picker con los nombres de los autos
                    autoPicker.ItemsSource = new List<string>(autos.Keys);
                }
            }
            catch (Exception ex)
            {
                // Manejar errores (por ejemplo, mostrar un mensaje al usuario)
                await DisplayAlert("Error", $"No se pudo cargar la información de los autos: {ex.Message}", "OK");
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (autoPicker.SelectedIndex != -1)
            {
                // Obtener el modelo seleccionado
                string selectedModel = autoPicker.SelectedItem.ToString();

                // Obtener la información del auto
                if (autos.TryGetValue(selectedModel, out Auto selectedAuto))
                {
                    // Actualizar las etiquetas y la imagen
                    marcaLabel.Text = $"Marca: {selectedAuto.Marca}";
                    modeloLabel.Text = $"Modelo: {selectedAuto.Modelo}";
                    añoLabel.Text = $"Año: {selectedAuto.Año}";
                    autoImage.Source = selectedAuto.ImagenURL;
                }
            }
        }

        // Evento para el botón Limpiar
        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            // Limpiar las selecciones y datos
            autoPicker.SelectedIndex = -1;
            marcaLabel.Text = "Marca: ";
            modeloLabel.Text = "Modelo: ";
            añoLabel.Text = "Año: ";
            autoImage.Source = null;
        }

        // Evento para el botón Finalizar
        private async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            // Mostrar un mensaje de finalización
            await DisplayAlert("Finalizado", "La aplicación ha terminado. ¡Gracias por usarla!", "OK");

            // Cerrar la aplicación
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }

    // La clase Auto representa un automóvil con varias propiedades.
    public class Auto
    {
        // Propiedad para almacenar la marca del auto.
        public string Marca { get; set; }

        // Propiedad para almacenar el modelo del auto.
        public string Modelo { get; set; }

        // Propiedad para almacenar el año de fabricación del auto.
        public int Año { get; set; }

        // Propiedad para almacenar la URL de la imagen del auto.
        public string ImagenURL { get; set; }

        // Constructor de la clase Auto.
        // Este método se usa para inicializar una nueva instancia de la clase con los valores proporcionados.
        public Auto(string marca, string modelo, int año, string imagenURL)
        {
            // Inicializa la propiedad Marca con el valor proporcionado.
            Marca = marca;

            // Inicializa la propiedad Modelo con el valor proporcionado.
            Modelo = modelo;

            // Inicializa la propiedad Año con el valor proporcionado.
            Año = año;

            // Inicializa la propiedad ImagenURL con el valor proporcionado.
            ImagenURL = imagenURL;
        }
    }
}
