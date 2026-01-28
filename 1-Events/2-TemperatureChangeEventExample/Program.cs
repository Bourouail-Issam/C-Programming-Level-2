using System;

public class TemperatureChangedEventArgs: EventArgs
{
    public double OldTemperature { get; }
    public double NewTemperature { get; }
    public double Diffrence { get; }

    public TemperatureChangedEventArgs(double OldTemperature, double NewTemperature)
    {
        this.OldTemperature = OldTemperature;
        this.NewTemperature = NewTemperature;
        this.Diffrence = NewTemperature - OldTemperature;
    }
}

public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs> ThermostatChanged;
    private double OldTemperature;
    private double currentTemperature;

    public void SetTemperature(double newTemperature)
    {
        if (this.currentTemperature != newTemperature)
        {
            this.OldTemperature = this.currentTemperature;
            this.currentTemperature = newTemperature;
            OnThermostatChanged(this.OldTemperature, this.currentTemperature);
        }
    }

    public void OnThermostatChanged (double OldTemperature, double NewTemperature)
    {
        OnThermostatChanged(new TemperatureChangedEventArgs(OldTemperature, NewTemperature));
    }

    protected virtual void OnThermostatChanged(TemperatureChangedEventArgs e)
    {
        ThermostatChanged?.Invoke(this, e);
    }

}

public class Display
{
    public void Subscribe(Thermostat thermostat)
    {
        thermostat.ThermostatChanged += HandleTemperatureChange;
    }
    public void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine("\n\nTemperature changed:");
        Console.WriteLine($"Temperature changed from {e.OldTemperature}°C");
        Console.WriteLine($"Temperature changed to {e.NewTemperature}°C");
        Console.WriteLine($"Temperature Differance to {e.Diffrence}°C");
    }
}

public class Program
{
        static void Main()
        {
        Thermostat thermostat = new Thermostat();
        Display display = new Display();

        display.Subscribe(thermostat);

        thermostat.SetTemperature(25);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(30);
        thermostat.SetTemperature(10);
        thermostat.SetTemperature(10);
        thermostat.SetTemperature(20);
        thermostat.SetTemperature(0);

        Console.ReadLine();
    }
}

