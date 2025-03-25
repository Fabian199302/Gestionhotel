using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gestionhotel
{
    public partial class frmGestionReservas : Form
    {
        private bool _updatingDates = false;
        private Dictionary<string, List<int>> _habitacionesDisponibles;
        private Reserva reservaEnEdicion = null; // 🔹 Variable para almacenar la reserva en edición



        public frmGestionReservas()
        {
            InitializeComponent();


            InicializarHabitaciones();
        }
        

        

        private void frmGestionReservas_Load(object sender, EventArgs e)
        {
            // 🔹 Mostrar solo Tipo de Habitación al iniciar
            lblHabitacionTipo.Visible = true;
            cmbTipoHabitacion.Visible = true;

            
        }
        

        private void dateTimefinal_ValueChanged(object sender, EventArgs e)
        {
            if (!_updatingDates) ActualizarDuracion();

            HABITACIONESDIPONIBLES.Visible = true;
            listhabitaciones.Visible = true;
            lblHabitacion.Visible = true;
            txtHabitacion.Visible = true;
            labelIngreseValor.Visible = true;
            textIngreseValor.Visible = true;
        }


        private void InicializarHabitaciones()
        {
            _habitacionesDisponibles = new Dictionary<string, List<int>>()
            {
                { "vip", Enumerable.Range(201, 9).ToList() },
                { "estándar", Enumerable.Range(301, 9).ToList() }
            };
            listhabitaciones.SelectionMode = SelectionMode.None;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbTipoHabitacion.Items.AddRange(new object[] { "Habitación Estándar", "Habitación VIP" });
            cmbTipoHabitacion.SelectedIndex = -1;
            txtDiasEstadia.ReadOnly = true;
            textvalorpagar.ReadOnly = true;
            LimpiarCampos();
            ActualizarListaHabitaciones();
        }

        private void ActualizarListaHabitaciones()
        {
            listhabitaciones.Items.Clear();
            if (cmbTipoHabitacion.SelectedIndex == -1) return;

            var tipo = cmbTipoHabitacion.SelectedItem.ToString().Contains("Estándar") ? "estándar" : "vip";
            var prefijo = tipo == "vip" ? "VIP" : "Estandar";

            foreach (var numero in _habitacionesDisponibles[tipo])
                listhabitaciones.Items.Add($"{prefijo}{numero}");
        }

        private void LimpiarCampos()
        {
            _updatingDates = true;
            dateTimeinicio.Value = DateTime.Today;
            dateTimefinal.Value = DateTime.Today.AddDays(1);
            _updatingDates = false;

            txtNombre.Clear();
            txtHabitacion.Clear();
            textIngreseValor.Clear();
            cmbTipoHabitacion.SelectedIndex = -1;
            txtDiasEstadia.Text = "1";
            textvalorpagar.Text = "$0";
            listhabitaciones.Items.Clear();
        }

        private void ActualizarDuracion()
        {
            try
            {
                DateTime fechaInicio = dateTimeinicio.Value;
                DateTime fechaFinal = dateTimefinal.Value;
                ValidarFechas(fechaInicio, fechaFinal);

                int dias = (fechaFinal - fechaInicio).Days;
                txtDiasEstadia.Text = dias < 1 ? "1" : dias.ToString();

                if (ValidarCamposPreview())
                {
                    ValidarNumeroHabitacion();
                    Reserva reservaTemporal = CrearReservaParaPreview();
                    textvalorpagar.Text = reservaTemporal.CalcularCostoTotal().ToString("C");
                }
                else textvalorpagar.Text = "$0";
            }
            catch (Exception ex)
            {
                _updatingDates = true;
                dateTimeinicio.Value = DateTime.Today;
                dateTimefinal.Value = DateTime.Today.AddDays(1);
                _updatingDates = false;
                txtDiasEstadia.Text = "1";
                textvalorpagar.Text = "$0";
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidarCamposPreview()
        {
            return cmbTipoHabitacion.SelectedIndex != -1 &&
                   int.TryParse(txtHabitacion.Text, out int _) &&
                   decimal.TryParse(textIngreseValor.Text, out decimal _);
        }

        private void ValidarNumeroHabitacion()
        {
            if (cmbTipoHabitacion.SelectedIndex == -1 || string.IsNullOrEmpty(txtHabitacion.Text)) return;

            var tipo = cmbTipoHabitacion.SelectedItem.ToString().Contains("Estándar") ? "estándar" : "vip";
            var rango = tipo == "vip" ? new { Min = 201, Max = 209 } : new { Min = 301, Max = 309 };

            if (!int.TryParse(txtHabitacion.Text, out int numero) || numero < rango.Min || numero > rango.Max)
                throw new ArgumentException($"Número inválido. Rango permitido: {rango.Min}-{rango.Max}");

            if (GestorReservas.Instancia.ExisteReserva(numero, dateTimeinicio.Value, dateTimefinal.Value))
                throw new ArgumentException("¡Habitación ya reservada en esas fechas!");
        }

        private Reserva CrearReservaParaPreview()
        {
            string tipo = cmbTipoHabitacion.SelectedItem.ToString().Replace("Habitación ", "").ToLower();
            int numeroHabitacion = int.Parse(txtHabitacion.Text);
            DateTime fechaReserva = dateTimeinicio.Value;
            int duracion = (dateTimefinal.Value - dateTimeinicio.Value).Days;
            decimal tarifa = decimal.Parse(textIngreseValor.Text);

            string nombre = string.IsNullOrWhiteSpace(txtNombre.Text) ? "Cliente Temporal" : txtNombre.Text.Trim();

            return ReservaFactory.CrearReserva(
                tipo,
                nombre,
                numeroHabitacion,
                fechaReserva,
                duracion,
                tarifa
            );
        }

        private void ValidarFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            if (fechaInicio > fechaFinal)
                throw new ArgumentException("La fecha inicial no puede ser mayor que la final.");

            if ((fechaFinal - fechaInicio).Days < 1)
                throw new ArgumentException("Duración mínima: 1 noche");
        }

        


        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            if (lstReserva.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione una reserva para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de eliminar esta reserva? Esta acción no se puede deshacer luego.",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    // Obtener la reserva seleccionada
                    string reservaSeleccionada = lstReserva.SelectedItem.ToString();
                    string[] datos = reservaSeleccionada.Split('|');
                    int numeroHabitacion = int.Parse(datos[1].Replace(" Hab.", "").Trim());

                    // Buscar la reserva en la lista de reservas
                    Reserva reservaAEliminar = GestorReservas.Instancia.ObtenerTodasLasReservas()
                        .FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion);

                    if (reservaAEliminar != null)
                    {
                        GestorReservas.Instancia.EliminarReserva(reservaAEliminar);
                        ActualizarListaReservas();
                        MessageBox.Show("Reserva eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la reserva en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar reserva: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (lstReserva.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione una reserva para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show(
                "¿Está seguro de editar la información? Esta acción no se puede revertir.",
                "Confirmar Edición",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("A continuación, llene el formulario con la información que desea cambiar.", "Editar Reserva", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Obtener la reserva seleccionada
                string reservaSeleccionada = lstReserva.SelectedItem.ToString();
                string[] datos = reservaSeleccionada.Split('|');
                int numeroHabitacion = int.Parse(datos[1].Trim().Split(' ')[1]);

                // ✅ Guardar la referencia directa a la reserva en `reservaEnEdicion`
                reservaEnEdicion = GestorReservas.Instancia.ObtenerTodasLasReservas()
                    .FirstOrDefault(r => r.NumeroHabitacion == numeroHabitacion);

                if (reservaEnEdicion != null)
                {
                    // ✅ Llenar el formulario con los datos actuales
                    txtNombre.Text = reservaEnEdicion.NombreCliente;
                    txtHabitacion.Text = reservaEnEdicion.NumeroHabitacion.ToString();
                    dateTimeinicio.Value = reservaEnEdicion.FechaReserva;
                    dateTimefinal.Value = reservaEnEdicion.FechaReserva.AddDays(reservaEnEdicion.DuracionEstadia);
                    textIngreseValor.Text = reservaEnEdicion is HabitacionEstandar
                        ? ((HabitacionEstandar)reservaEnEdicion).TarifaFija.ToString()
                        : ((HabitacionVIP)reservaEnEdicion).TarifaFija.ToString();

                    // ✅ Habilitar el botón "Guardar Cambios"
                    buttonGuardarCa.Enabled = true;

                    buttonGuardarCa.Visible = true;
                    btnAgregar.Visible = false;
                }
                else
                {
                    MessageBox.Show("No se encontró la reserva en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void buttonGuardarCa_Click(object sender, EventArgs e)
        {
            try
            {
                if (reservaEnEdicion == null)
                {
                    MessageBox.Show("No hay ninguna reserva en edición.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ValidarCampos();

                string nombre = txtNombre.Text.Trim();
                int numeroHabitacion = int.Parse(txtHabitacion.Text);
                DateTime fechaReserva = dateTimeinicio.Value;
                int duracion = (dateTimefinal.Value - dateTimeinicio.Value).Days;
                decimal tarifa = decimal.Parse(textIngreseValor.Text);

                // ✅ Llamar a `EditarReserva()` con la referencia a `reservaEnEdicion`
                GestorReservas.Instancia.EditarReserva(reservaEnEdicion, nombre, numeroHabitacion, fechaReserva, duracion, tarifa);

                MessageBox.Show("Los cambios en la reserva se han guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // ✅ Limpiar variable y deshabilitar el botón "Guardar Cambios"
                reservaEnEdicion = null;
                buttonGuardarCa.Enabled = false;

                // ✅ Refrescar la lista de reservas y limpiar el formulario
                ActualizarListaReservas();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ValidarCampos()
        {
            if (cmbTipoHabitacion.SelectedIndex == -1)
                throw new ArgumentException("Seleccione un tipo de habitación.");

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                throw new ArgumentException("Nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(textIngreseValor.Text))
                throw new ArgumentException("La tarifa es obligatoria");

            if (!decimal.TryParse(textIngreseValor.Text, out decimal tarifa) || tarifa <= 0)
                throw new ArgumentException("Tarifa no válida (debe ser > 0)");

            ValidarFechas(dateTimeinicio.Value, dateTimefinal.Value);
            ValidarNumeroHabitacion();
        }

        private Reserva CrearReserva()
        {
            string tipo = cmbTipoHabitacion.SelectedItem.ToString().Replace("Habitación ", "").ToLower();
            int numeroHabitacion = int.Parse(txtHabitacion.Text);
            decimal tarifa = decimal.Parse(textIngreseValor.Text);

            if (!_habitacionesDisponibles[tipo].Contains(numeroHabitacion))
                throw new ArgumentException("Número de habitación no válido para este tipo");

            return ReservaFactory.CrearReserva(
                tipo,
                txtNombre.Text.Trim(),
                numeroHabitacion,
                dateTimeinicio.Value,
                (dateTimefinal.Value - dateTimeinicio.Value).Days,
                tarifa
            );
        }

        private void ActualizarListaReservas()
        {
            lstReserva.Items.Clear();
            foreach (Reserva reserva in GestorReservas.Instancia.ObtenerTodasLasReservas())
            {
                lstReserva.Items.Add(
                    $"{reserva.NombreCliente} | Hab. {reserva.NumeroHabitacion} | " +
                    $"{reserva.FechaReserva:dd/MM/yyyy} | {reserva.DuracionEstadia} noches | " +
                    $"Total: {reserva.CalcularCostoTotal():C}"
                );
            }
        }

        // Evento corregido sin cambiar el nombre original
        private void cmbTipoEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool habilitar = cmbTipoHabitacion.SelectedIndex != -1;
            txtNombre.Enabled = habilitar;
            txtHabitacion.Enabled = habilitar;
            textIngreseValor.Enabled = habilitar;
            txtHabitacion.Clear();
            ActualizarListaHabitaciones();
            ActualizarDuracion();
        }

        private void dateTimeinicio_ValueChanged(object sender, EventArgs e)
        {
            if (!_updatingDates) ActualizarDuracion();
        }

        

        // Evento renombrado en el diseñador (no se cambia el nombre en el código)
        private void txtSalario_TextChanged(object sender, EventArgs e)
        {
            ActualizarDuracion();
        }

        private void textIngreseValor_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(textIngreseValor.Text, out decimal tarifa) && tarifa > 0)
            {
                labelTotal.Visible = true;
                textvalorpagar.Visible = true;
                btnAgregar.Visible = true;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();
                Reserva nuevaReserva = CrearReserva();
                GestorReservas.Instancia.AgregarReserva(nuevaReserva);
                ActualizarListaReservas();
                LimpiarCampos();

                MessageBox.Show("Reserva exitosa!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Ocultar `buttonGuardarCa` y mostrar los demás botones
                buttonGuardarCa.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void lstReserva_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblHabitacion_Click(object sender, EventArgs e)
        {

        }
    }
}