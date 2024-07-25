# AutosApp2: Aplicación Interactiva de Información de Autos

![Portada de AutosApp2](CLASE_AUTO.png)

autos_json
## Descripción

AutosApp2 es una aplicación Xamarin.Forms que demuestra el uso de clases, propiedades, constructores y recuperación de datos JSON desde una URL. Esta aplicación proporciona una interfaz interactiva para que los usuarios exploren información sobre varios modelos de autos.

## Características

- Menú desplegable para seleccionar diferentes modelos de autos
- Visualización de detalles del auto incluyendo marca, modelo y año
- Visualización de imagen del auto seleccionado
- Recuperación de datos desde un archivo JSON remoto
- Interfaz de usuario limpia e intuitiva con fondo degradado
- Opciones para limpiar selecciones y salir de la aplicación

## Aspectos Técnicos Destacados

1. **Diseño de UI en XAML:**
   - Utiliza XAML para crear una interfaz de usuario responsiva y visualmente atractiva
   - Implementa estilos y recursos personalizados para un diseño consistente
   - Usa un fondo degradado para mejorar el atractivo visual

2. **Backend en C#:**
   - Implementa la clase `Auto` para representar objetos de autos
   - Utiliza programación asíncrona para la carga de datos
   - Maneja la deserialización JSON usando Newtonsoft.Json
   - 
![Portada de AutosApp2](autos_json.png)

3. **Gestión de Datos:**
   - Recupera datos de autos desde un archivo JSON remoto
   - Almacena información de autos en un Diccionario para un acceso eficiente
   - Actualiza dinámicamente la UI basada en las selecciones del usuario

4. **Interacción del Usuario:**
   - Proporciona elementos interactivos como un Picker para la selección de autos
   - Implementa manejadores de eventos para las acciones del usuario
   - Ofrece opciones para limpiar selecciones y salir de la aplicación

## Cómo Funciona

1. Al iniciar, la aplicación carga datos de autos desde un archivo JSON remoto.
2. Los usuarios pueden seleccionar un modelo de auto del menú desplegable.
3. Al seleccionar, la aplicación muestra los detalles del auto y una imagen.
4. Los usuarios pueden limpiar su selección o salir de la aplicación usando los botones proporcionados.

## Propósito

Esta aplicación sirve como una demostración de:
- Construcción de interfaces de usuario con Xamarin.Forms y XAML
- Implementación de conceptos de programación orientada a objetos en C#
- Manejo de recuperación de datos remotos y análisis JSON
- Creación de una aplicación móvil interactiva y responsiva

## Público Objetivo

Este proyecto es ideal para:
- Desarrolladores de Xamarin.Forms principiantes a intermedios
- Estudiantes aprendiendo sobre desarrollo de aplicaciones móviles
- Cualquiera interesado en ver un ejemplo práctico de C# y XAML en acción

## Cómo Clonar el Repositorio

Para clonar este repositorio y ejecutar el proyecto en tu máquina local, sigue estos pasos:

1. Abre una terminal o línea de comandos.
2. Navega al directorio donde deseas clonar el repositorio.
3. Ejecuta el siguiente comando:

```
git clone https://github.com/tu-usuario/AutosApp2.git
```

4. Navega al directorio del proyecto:

```
cd AutosApp2
```

5. Abre la solución en Visual Studio o tu IDE preferido para Xamarin.Forms.

## Como Crear el Proyecto desde Cero: 

Esta guía te ayudará a crear una aplicación Xamarin.Forms llamada `AutosApp2`, que muestra información de autos desde un archivo JSON alojado en una URL y permite cerrar la aplicación mediante un botón.

## Requisitos Previos

- Visual Studio con el workload de desarrollo móvil .NET instalado.
- Conocimiento básico de C# y Xamarin.Forms.

## Pasos para Crear la Aplicación

### 1. Crear un Nuevo Proyecto Xamarin.Forms

1. Abre Visual Studio.
2. Selecciona **Crear un nuevo proyecto**.
3. Busca y selecciona **Aplicación Móvil con Xamarin.Forms**.
4. Asigna el nombre del proyecto como `AutosApp2`.
5. Elige una ubicación para guardar el proyecto y haz clic en **Crear**.
6. Selecciona **Plantilla en blanco** y asegúrate de que la plataforma de destino sea **Android y iOS**.
7. Haz clic en **Crear**.

### 2. Configurar el Archivo `MainPage.xaml`

Crea un diseño básico en `MainPage.xaml`:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="AutosApp2.MainPage">

    <StackLayout Padding="10">
        <Frame>
            <Label Text="Uso de Clases, Propiedades, Constructor y Json URL"
                   FontSize="24"
                   FontAttributes="Bold"
                   HorizontalTextAlignment="Center"
                   VerticalTextAlignment="Center"
                   TextColor="#333333"/>
        </Frame>
        
        <Frame>
            <StackLayout>
                <Picker x:Name="autoPicker" SelectedIndexChanged="OnPickerSelectedIndexChanged">
                    <Picker.Items>
                        <x:String>Toyota Corolla</x:String>
                        <x:String>Honda Civic</x:String>
                        <!-- Añadir más autos aquí -->
                    </Picker.Items>
                </Picker>
                <Label x:Name="marcaLabel" Text="Marca: " />
                <Label x:Name="modeloLabel" Text="Modelo: " />
                <Label x:Name="añoLabel" Text="Año: " />
                <Image x:Name="autoImage" WidthRequest="300" HeightRequest="200" />
                <Button Text="Limpiar" Clicked="OnClearButtonClicked" />
                <Button Text="Finalizar" Clicked="OnFinishButtonClicked" />
            </StackLayout>
        </Frame>
    </StackLayout>
</ContentPage>

## 3. Configurar el Archivo MainPage.xaml.cs
En el archivo MainPage.xaml.cs, añade la lógica para cargar los datos desde la URL y manejar los eventos de los botones.

## Codigo MainPage.xaml.cs:

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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
                    string url = "https://raw.githubusercontent.com/JUANCITOPENA/ARCHIVO_JSON_FOTOS_AUTOS_CLASE/main/autos";
                    string json = await client.GetStringAsync(url);
                    JObject jsonObject = JObject.Parse(json);
                    JObject autosObject = (JObject)jsonObject["autos"];
                    autos = autosObject.ToObject<Dictionary<string, Auto>>();
                    autoPicker.ItemsSource = new List<string>(autos.Keys);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo cargar la información de los autos: {ex.Message}", "OK");
            }
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (autoPicker.SelectedIndex != -1)
            {
                string selectedModel = autoPicker.SelectedItem.ToString();
                if (autos.TryGetValue(selectedModel, out Auto selectedAuto))
                {
                    marcaLabel.Text = $"Marca: {selectedAuto.Marca}";
                    modeloLabel.Text = $"Modelo: {selectedAuto.Modelo}";
                    añoLabel.Text = $"Año: {selectedAuto.Año}";
                    autoImage.Source = selectedAuto.ImagenURL;
                }
            }
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            autoPicker.SelectedIndex = -1;
            marcaLabel.Text = "Marca: ";
            modeloLabel.Text = "Modelo: ";
            añoLabel.Text = "Año: ";
            autoImage.Source = null;
        }

        private async void OnFinishButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Finalizado", "La aplicación ha terminado. ¡Gracias por usarla!", "OK");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }

    public class Auto
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string ImagenURL { get; set; }

        public Auto(string marca, string modelo, int año, string imagenURL)
        {
            Marca = marca;
            Modelo = modelo;
            Año = año;
            ImagenURL = imagenURL;
        }
    }
}
## 4. Ejecutar la Aplicación
Conecta un dispositivo Android o utiliza un emulador.
Selecciona la plataforma Android en Visual Studio.
Haz clic en Ejecutar para compilar y ejecutar la aplicación.
5. Usar la Aplicación
Selecciona un auto en el Picker para ver su información.
Presiona el botón Limpiar para restablecer los campos.
Presiona el botón Finalizar para cerrar la aplicación.
¡Y eso es todo! Ahora tienes una aplicación funcional que carga datos desde una URL y permite cerrar la aplicación desde un botón.


## Licencia

Este proyecto está licenciado bajo la Licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más detalles.

## ¡Apoya este Proyecto!

Si encuentras útil este proyecto, por favor considera:

- ⭐ Darle una estrella en GitHub
- 🔗 Compartirlo con otros desarrolladores
- 🐛 Reportar problemas o sugerir mejoras
- 🍴 Hacer un fork y contribuir al proyecto

Tu apoyo es muy apreciado y nos ayuda a seguir mejorando.

¡Siéntete libre de explorar, modificar y aprender de este proyecto!
