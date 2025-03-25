using Gestionhotel;
using System;

public class HabitacionVIP : Reserva
{
    public decimal TarifaFija { get; }
    private const decimal Descuento = 0.20m;

    public HabitacionVIP(
        string nombreCliente,
        int numeroHabitacion,
        DateTime fechaReserva,
        int duracionEstadia,
        decimal tarifaFija
    ) : base(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia)
    {
        if (tarifaFija <= 0)
            throw new ArgumentException("Tarifa VIP no válida");

        TarifaFija = tarifaFija;
    }

    public override decimal CalcularCostoTotal()
    {
        decimal costoBase = TarifaFija * DuracionEstadia;
        return (DuracionEstadia > 5) ? costoBase * (1 - Descuento) : costoBase;
    }

   }
