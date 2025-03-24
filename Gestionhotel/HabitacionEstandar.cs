using System;

namespace Gestionhotel
{
    public class HabitacionEstandar : Reserva
    {
        public decimal TarifaFija { get; }

        public HabitacionEstandar(
            string nombreCliente,
            int numeroHabitacion,
            DateTime fechaReserva,
            int duracionEstadia,
            decimal tarifaFija
        ) : base(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia)
        {
            if (tarifaFija <= 0) throw new ArgumentException("Tarifa debe ser mayor a 0.");
            TarifaFija = tarifaFija;
        }

        public override decimal CalcularCostoTotal() => TarifaFija * DuracionEstadia;
    }
}