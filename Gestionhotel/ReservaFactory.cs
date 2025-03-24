using Gestionhotel;
using System;

public static class ReservaFactory
{
    public static Reserva CrearReserva(
        string tipo,
        string nombreCliente,
        int numeroHabitacion,
        DateTime fechaReserva,
        int duracionEstadia
    )
    {
        switch (tipo.ToLower())
        {
            case "estándar":
                return new HabitacionEstandar(
                    nombreCliente,
                    numeroHabitacion,
                    fechaReserva,
                    duracionEstadia,
                    75000
                );
            case "vip":
                return new HabitacionVIP(
                    nombreCliente,
                    numeroHabitacion,
                    fechaReserva,
                    duracionEstadia
                );
            default:
                throw new ArgumentException("Tipo de habitación no válido.");
        }
    }
}