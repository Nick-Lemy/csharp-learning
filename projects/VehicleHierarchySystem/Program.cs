using System;

public class Program
{
  public static void Main(string[] args)
  {
    ElectricCar ec1 = new ElectricCar("Random Make", "Random Model");
    ec1.Describe();
    ec1.ChargeBattery();
  }

  public class Vehicle
  {
    public string Make { get; set; }
    public string Model { get; set; }

    public Vehicle(string make, string model)
    {
      Make = make;
      Model = model;
    }
    
    public virtual void Describe()
    {
      Console.WriteLine($"Nice Vehicle of Make: {Make}; Model: {Model}");
    }
  }

  public class Car : Vehicle
  {
    public Car(string make, string model) : base(make, model) { }

    public override void Describe()
    {
      Console.WriteLine($"Nice Car of Make: {Make}; Model: {Model}");
    }
  }

  public class Truck: Vehicle
  {
    public Truck(string make, string model) : base(make, model) { }

    public override void Describe()
    {
      Console.WriteLine($"Nice Truck of Make: {Make}; Model: {Model}");
    }
  }



  public class ElectricCar : Car, IElectric {

    public ElectricCar(string make, string model) : base(make, model) {}
    public void ChargeBattery()
    {
      Console.WriteLine($"{Make} {Model} is charing...");
    }
  }
}

public interface IElectric
{
  void ChargeBattery();
}

