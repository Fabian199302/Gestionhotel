using Gestionhotel;
using System;

public static class ReservaFactory
{
    // Añade el parámetro 'tarifa' en la firma del método
    public static Reserva CrearReserva(
        string tipo,
        string nombreCliente,
        int numeroHabitacion,
        DateTime fechaReserva,
        int duracionEstadia,
        decimal tarifa  // Parámetro nuevo
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
                    tarifa  // Pasa la tarifa
                );
            case "vip":
                return new HabitacionVIP(
                    nombreCliente,
                    numeroHabitacion,
                    fechaReserva,
                    duracionEstadia,
                    tarifa  // Pasa la tarifa
                );
            default:
                throw new ArgumentException("Tipo de habitación no válido.");
        }
    }
}