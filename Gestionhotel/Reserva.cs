using System;

namespace Gestionhotel
{
    public abstract class Reserva
    {
        public string NombreCliente { get; }
        public int NumeroHabitacion { get; }
        public DateTime FechaReserva { get; }
        public int DuracionEstadia { get; }

        protected Reserva(string nombreCliente, int numeroHabitacion, DateTime fechaReserva, int duracionEstadia)
        {
            // Regla 1: Nombre obligatorio
            if (string.IsNullOrWhiteSpace(nombreCliente))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            // Regla 5: Duración mínima
            if (duracionEstadia < 1)
                throw new ArgumentException("La reserva debe ser de al menos 1 noche.");

            NombreCliente = nombreCliente;
            NumeroHabitacion = numeroHabitacion;
            FechaReserva = fechaReserva;
            DuracionEstadia = duracionEstadia;
        }

        public abstract decimal CalcularCostoTotal();

        // ✅ Nueva sobrecarga: Cálculo con número de noches personalizado
        public decimal CalcularCostoTotal(int noches) => CalcularCostoTotal() / DuracionEstadia * noches;

        // ✅ Nueva sobrecarga: Cálculo con tarifa personalizada
        public decimal CalcularCostoTotal(int noches, decimal tarifa) => tarifa * noches;
    }
}