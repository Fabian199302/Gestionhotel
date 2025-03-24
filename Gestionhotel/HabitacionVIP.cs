using System;

namespace Gestionhotel
{
    public class HabitacionVIP : Reserva
    {
        private const decimal TarifaBase = 150000m;
        private const decimal Descuento = 0.20m;

        public HabitacionVIP(
            string nombreCliente,
            int numeroHabitacion,
            DateTime fechaReserva,
            int duracionEstadia
        ) : base(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia) { }

        public override decimal CalcularCostoTotal()
        {
            decimal costoBase = TarifaBase * DuracionEstadia;
            return DuracionEstadia > 5 ? costoBase * (1 - Descuento) : costoBase;
        }
    }
}