using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Gestionhotel
{
    public partial class frmGestionReservas : Form // Nombre corregido
    {
        private bool _updatingDates = false;
        private Dictionary<string, List<int>> _habitacionesDisponibles;

        public frmGestionReservas() // Constructor actualizado
        {
            InitializeComponent();
            InicializarHabitaciones();
        }

        private void InicializarHabitaciones()
        {
            _habitacionesDisponibles = new Dictionary<string, List<int>>()
            {
                { "vip", Enumerable.Range(201, 9).ToList() },       // VIP201-VIP209
                { "estándar", Enumerable.Range(301, 9).ToList() }   // Estandar301-Estandar309
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
            {
                listhabitaciones.Items.Add($"{prefijo}{numero}");
            }
        }

        private void LimpiarCampos()
        {
            _updatingDates = true;
            dateTimeinicio.Value = DateTime.Today;
            dateTimefinal.Value = DateTime.Today.AddDays(1);
            _updatingDates = false;

            txtNombre.Clear();
            txtHabitacion.Clear();
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

                TimeSpan diferencia = fechaFinal - fechaInicio;
                int dias = diferencia.Days;
                txtDiasEstadia.Text = dias < 0 ? "0" : dias.ToString();

                if (ValidarCamposPreview())
                {
                    ValidarNumeroHabitacion();
                    Reserva reservaTemporal = CrearReservaParaPreview();
                    textvalorpagar.Text = reservaTemporal.CalcularCostoTotal().ToString("C");
                }
                else
                {
                    textvalorpagar.Text = "$0";
                }
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
            // Solo validar tipo y número de habitación para el cálculo previo
            return cmbTipoHabitacion.SelectedIndex != -1 && int.TryParse(txtHabitacion.Text, out int _);
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

            string nombre = string.IsNullOrWhiteSpace(txtNombre.Text) ? "Cliente Temporal" : txtNombre.Text.Trim();

            return ReservaFactory.CrearReserva(
                tipo,
                nombre,
                numeroHabitacion,
                fechaReserva,
                duracion
            );
        }

        private void ValidarFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            if (fechaInicio > fechaFinal)
                throw new ArgumentException("La fecha inicial no puede ser mayor que la final.");

            if ((fechaFinal - fechaInicio).Days < 1)
                throw new ArgumentException("Duración mínima: 1 noche");
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

            ValidarFechas(dateTimeinicio.Value, dateTimefinal.Value);
            ValidarNumeroHabitacion();
        }

        private Reserva CrearReserva()
        {
            string tipo = cmbTipoHabitacion.SelectedItem.ToString().Replace("Habitación ", "").ToLower();
            int numeroHabitacion = int.Parse(txtHabitacion.Text);

            if (!_habitacionesDisponibles[tipo].Contains(numeroHabitacion))
                throw new ArgumentException("Número de habitación no válido para este tipo");

            return ReservaFactory.CrearReserva(
                tipo,
                txtNombre.Text.Trim(),
                numeroHabitacion,
                dateTimeinicio.Value,
                (dateTimefinal.Value - dateTimeinicio.Value).Days
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

        private void cmbTipoEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool habilitar = cmbTipoHabitacion.SelectedIndex != -1;
            txtNombre.Enabled = habilitar;
            txtHabitacion.Enabled = habilitar;
            txtHabitacion.Clear();
            ActualizarListaHabitaciones();
            ActualizarDuracion();
        }

        private void dateTimeinicio_ValueChanged(object sender, EventArgs e)
        {
            if (!_updatingDates) ActualizarDuracion();
        }

        private void dateTimefinal_ValueChanged(object sender, EventArgs e)
        {
            if (!_updatingDates) ActualizarDuracion();
        }

        private void txtSalario_TextChanged(object sender, EventArgs e)
        {
            ActualizarDuracion();
        }
    }
}