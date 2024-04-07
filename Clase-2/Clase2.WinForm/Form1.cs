using Clase2.Entidades;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Clase2.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCargarResultado_Click(object sender, EventArgs e)
        {
            //ResultadoServicio resultadoServicio = new ResultadoServicio();
            //resultadoServicio.Agregar(new Resultado
            //{
            //    EquipoLocal = txtEquipoLocal.Text,
            //    EquipoVisitante = txtEquipoVisitante.Text,
            //    GolesLocal = cboGolesLocal.Text,
            //    GolesVisitante = cboGolesVisitante.Text
            //});

            EnviarResultadoAApi();


            AgregarFila(gvResultados, txtEquipoLocal.Text, $"{cboGolesLocal.Text}-{cboGolesVisitante.Text}", txtEquipoVisitante.Text);
            txtEquipoLocal.Text = txtEquipoVisitante.Text = "";
        }

        private void AgregarFila(DataGridView gv, string equipoLocal, string goles, string equipoVisitante)
        {
            // Crear una nueva fila
            DataGridViewRow fila = new DataGridViewRow();
            fila.CreateCells(gv);

            // Asignar valores a las celdas de la fila
            fila.Cells[0].Value = equipoLocal;
            fila.Cells[1].Value = goles;
            fila.Cells[2].Value = equipoVisitante;

            // Agregar la fila al DataGridView
            gv.Rows.Add(fila);
        }

        private async Task EnviarResultadoAApi()
        {
            // URL de destino
            string url = "https://localhost:7169/api/Resultados";

            // Contenido del cuerpo (en este caso, un JSON)
            string jsonBody = $"{{\"equipoLocal\": \"{txtEquipoLocal.Text}\"," +
                $"\"equipoVisitante\": \"{txtEquipoVisitante.Text}\"," +
                $"\"golesLocal\": \"{cboGolesLocal.Text}\"," +
                $"\"golesVisitante\": \"{cboGolesVisitante.Text}\"}}";

            // Crear cliente HTTP
            using (HttpClient client = new HttpClient())
            {
                // Configurar encabezados si es necesario (por ejemplo, para indicar el tipo de contenido)
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "MyApp");

                // Crear el contenido del cuerpo
                HttpContent bodyContent = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    // Realizar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(url, bodyContent);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta del servidor:");
                        Console.WriteLine(responseBody);
                    }
                    else
                    {
                        // Manejar errores de solicitud
                        Console.WriteLine($"La solicitud fall� con el c�digo de estado: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores de red o de la solicitud
                    Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                }
            }
        }

        private void btnCargarEquipo_Click(object sender, EventArgs e)
        {
            AgregarEquiposApi();
        }


        private void btnObtenerEquipos_Click(object sender, EventArgs e)
        {
            ObtenerEquiposApi();
        }

        private void btnEliminarEquipo_Click(object sender, EventArgs e)
        {
            EliminarEquiposApi();
        }

        private async Task ObtenerEquiposApi()
        {
            // URL de destino
            string url = "https://localhost:7169/api/Equipos";

            try
            {
                // Crear cliente HTTP
                using (HttpClient client = new HttpClient())
                {
                    // Configurar encabezados si es necesario (por ejemplo, para indicar el tipo de contenido)
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("User-Agent", "MyApp");

                    // Realizar la solicitud GET
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();

                        // Aqu� puedes manejar la respuesta, por ejemplo, deserializar el JSON si es necesario
                        // Supongamos que la respuesta es una lista de objetos de tipo Equipo en formato JSON
                        List<Equipo> equipos = JsonConvert.DeserializeObject<List<Equipo>>(responseBody);

                        gvEquipos.Rows.Clear();

                        // Puedes hacer lo que necesites con la lista de equipos, como mostrarla en un DataGridView
                        foreach (var equipo in equipos)
                        {
                            // Por ejemplo, agregar cada equipo a un DataGridView llamado gvEquipos
                            AgregarEquipoAGrilla(gvEquipos, equipo.nombre_equipo, equipo.pais);
                        }
                    }
                    else
                    {
                        // Manejar errores de solicitud
                        Console.WriteLine($"La solicitud fall� con el c�digo de estado: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de red o de la solicitud
                Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
            }
        }

        private async Task AgregarEquiposApi()
        {
            // URL de destino
            string url = "https://localhost:7169/api/Equipos";

            // Contenido del cuerpo (en este caso, un JSON)
            string jsonBody = $"{{\"nombre_equipo\": \"{txtEquipoACargar.Text}\"," +
                $"\"pais\": \"{txtPais.Text}\"}}";

            // Crear cliente HTTP
            using (HttpClient client = new HttpClient())
            {
                // Configurar encabezados si es necesario (por ejemplo, para indicar el tipo de contenido)
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "MyApp");

                // Crear el contenido del cuerpo
                HttpContent bodyContent = new StringContent(jsonBody, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    // Realizar la solicitud POST
                    HttpResponseMessage response = await client.PostAsync(url, bodyContent);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 200 OK)
                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine("Respuesta del servidor:");
                        Console.WriteLine(responseBody);
                        await ObtenerEquiposApi();
                    }
                    else
                    {
                        // Manejar errores de solicitud
                        Console.WriteLine($"La solicitud fall� con el c�digo de estado: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores de red o de la solicitud
                    Console.WriteLine($"Error al realizar la solicitud: {ex.Message}");
                }
            }
        }

        private async Task EliminarEquiposApi()
        {
            string nombreEquipoAEliminar = ObtenerNombreEquipoSeleccionado();

            if (string.IsNullOrEmpty(nombreEquipoAEliminar))
            {
                MessageBox.Show("Por favor, seleccione un equipo para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // URL de destino
                string url = $"https://localhost:7169/api/Equipos/{nombreEquipoAEliminar}";

                // Crear cliente HTTP
                using (HttpClient client = new HttpClient())
                {
                    // Realizar la solicitud DELETE
                    HttpResponseMessage response = await client.DeleteAsync(url);

                    // Verificar si la solicitud fue exitosa (c�digo de estado 204 No Content)
                    if (response.IsSuccessStatusCode)
                    {
                        // Informar al usuario que el equipo se elimin� correctamente
                        MessageBox.Show("Equipo eliminado correctamente.", "�xito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Actualizar la lista de equipos despu�s de eliminar
                        await ObtenerEquiposApi();
                    }
                    else
                    {
                        // Manejar errores de solicitud
                        MessageBox.Show($"La solicitud fall� con el c�digo de estado: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores de red o de la solicitud
                MessageBox.Show($"Error al realizar la solicitud: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private String ObtenerNombreEquipoSeleccionado()
        {
            if (gvEquipos.CurrentRow != null)
            {
                // Suponiendo que la primera columna de la grilla contiene el nombre del equipo
                return gvEquipos.CurrentRow.Cells[0].Value.ToString();
            }
            else
            {
                // Si no hay una fila seleccionada, devuelve una cadena vac�a o maneja la situaci�n seg�n tus necesidades
                return string.Empty;
            }
        }

        private void AgregarEquipoAGrilla(DataGridView gv, string nombreEquipo, string paisEquipo)
        {
            // Crear una nueva fila
            DataGridViewRow fila = new DataGridViewRow();
            fila.CreateCells(gv);

            // Asignar valores a las celdas de la fila
            fila.Cells[0].Value = nombreEquipo;
            fila.Cells[1].Value = paisEquipo;

            // Agregar la fila al DataGridView
            gv.Rows.Add(fila);
        }
    }
}