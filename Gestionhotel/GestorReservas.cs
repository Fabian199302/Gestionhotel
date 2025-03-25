using System;
using System.Collections.Generic;
using System.Linq;
using Gestionhotel;

public class GestorReservas
{
    private static GestorReservas _instancia;
    private List<Reserva> _reservas = new List<Reserva>();

    private GestorReservas() { }
    public static GestorReservas Instancia => _instancia ?? (_instancia = new GestorReservas());

    // Método para agregar reservas (existente)
    public void AgregarReserva(Reserva reserva)
    {
        if (_reservas.Any(r =>
            r.NumeroHabitacion == reserva.NumeroHabitacion &&
            reserva.FechaReserva < r.FechaReserva.AddDays(r.DuracionEstadia) &&
            reserva.FechaReserva.AddDays(reserva.DuracionEstadia) > r.FechaReserva
        )) throw new ArgumentException("Habitación ya reservada en esas fechas.");

        _reservas.Add(reserva);
    }

    // Método para eliminar reservas (nuevo)
    public void EliminarReserva(Reserva reserva)
    {
        if (!_reservas.Contains(reserva))
            throw new ArgumentException("Reserva no encontrada en el sistema");

        _reservas.Remove(reserva);
    }

    // Método para listar reservas (existente)
    public List<Reserva> ObtenerTodasLasReservas() => new List<Reserva>(_reservas);

    // Método para validar disponibilidad (existente)
    public bool ExisteReserva(int numeroHabitacion, DateTime fechaInicio, DateTime fechaFinal)
    {
        return _reservas.Any(r =>
            r.NumeroHabitacion == numeroHabitacion &&
            fechaInicio < r.FechaReserva.AddDays(r.DuracionEstadia) &&
            fechaFinal > r.FechaReserva
        );
    }

    //Método para editar reservas (nuevo)

    public void EditarReserva(Reserva reserva, string nuevoNombre, int nuevoNumeroHabitacion, DateTime nuevaFecha, int nuevaDuracion, decimal nuevaTarifa)
    {
        if (reserva == null)
            throw new ArgumentException("La reserva seleccionada no es válida.");

        _reservas.Remove(reserva); // 🔹 Elimina la reserva anterior

        // 🔹 Crear una nueva reserva con los datos actualizados
        Reserva reservaEditada;

        if (reserva is HabitacionEstandar)
        {
            reservaEditada = new HabitacionEstandar(nuevoNombre, nuevoNumeroHabitacion, nuevaFecha, nuevaDuracion, nuevaTarifa);
        }
        else if (reserva is HabitacionVIP)
        {
            reservaEditada = new HabitacionVIP(nuevoNombre, nuevoNumeroHabitacion, nuevaFecha, nuevaDuracion, nuevaTarifa);
        }
        else
        {
            throw new ArgumentException("Tipo de habitación no válido.");
        }

        _reservas.Add(reservaEditada); // 🔹 Agrega la reserva editada
    }


} 