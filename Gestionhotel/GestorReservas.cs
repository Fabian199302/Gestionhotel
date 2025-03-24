using System.Linq;
using Gestionhotel;
using System.Collections.Generic;
using System;

public class GestorReservas
{
    private static GestorReservas _instancia;
    private List<Reserva> _reservas = new List<Reserva>();

    private GestorReservas() { }
    public static GestorReservas Instancia => _instancia ?? (_instancia = new GestorReservas());

    public void AgregarReserva(Reserva reserva)
    {
        if (_reservas.Any(r =>
            r.NumeroHabitacion == reserva.NumeroHabitacion &&
            reserva.FechaReserva < r.FechaReserva.AddDays(r.DuracionEstadia) &&
            reserva.FechaReserva.AddDays(reserva.DuracionEstadia) > r.FechaReserva
        )) throw new ArgumentException("Habitación ya reservada en esas fechas.");

        _reservas.Add(reserva);
    }

    public List<Reserva> ObtenerTodasLasReservas() => new List<Reserva>(_reservas);

    public bool ExisteReserva(int numeroHabitacion, DateTime fechaInicio, DateTime fechaFinal)
    {
        return _reservas.Any(r =>
            r.NumeroHabitacion == numeroHabitacion &&
            fechaInicio < r.FechaReserva.AddDays(r.DuracionEstadia) &&
            fechaFinal > r.FechaReserva
        );
    }

}