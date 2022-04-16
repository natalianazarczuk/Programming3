using System;
using System.Security.Cryptography.X509Certificates;

namespace Vehicles
{

    //    STAGE 1: 3.0 points

    //Create abstract class Vehicle and three child classes: Car, Bus and Truck.
    //Every vehicle has an unique(consecutive) ID which is assign when object is created(common for all vehicles).
    //Vehicle has an abstract method Travel() which takes as parametr distance in kilometres and for each class displays proper information:
    //    Car NAME traveled DISTANCE km.
    //    Bus NAME traveled DISTANCE km with PASSENGER_COUNT passengers.
    //    Truck NAME traveled DISTANCE km with load of LOAD kg.
    //Bus has a limited number of passenger which can be transported (passengerLimit) equals 42. 
    //It is common for all Bus instances and can not be changed but should be accessible to read within main program.
    //For each Bus, the number of passengers travelling on it can be set using the function SetPassengerCount() and read PassengerCount(). 
    //If the value is larger then limit then value is not updated and false is return.
    //Likewise Truck has a limited amount of load which can be transported(capacity) equals 2500 kg and fuctions SetLoad(), Load().
    //Each class implements ToString() method which returns information about object:
    //    Vehicle(ID) { NAME }
    //    Car(ID) { NAME }
    //    Bus(ID) { NAME }
    //    [PASSENGER_COUNT/PASSENGER_LIMIT]
    //    Truck(ID) { NAME }
    //    [LOAD of CAPACITY kg]
    //    When Car, Bus and Truck is created message "XXX has been created." where XXX is string with information about vehicle

    public abstract class Vehicle
    {
        string name;
        private static int counter;
        protected int ID;

        public Vehicle()
        {
            counter++;
            ID = counter;
        }
        public Vehicle(string _name) : this()
        {
            name = _name;
            Console.WriteLine($"{ToString()}");
        }

        public override string ToString()
        {
            return $"Vehicle ({ID}) { name }";
        }
        public abstract void Travel(double distance);

        public virtual void Beep()
        {
            Console.WriteLine($"Vehicle {name} honks!");
        }
    }

    class Car : Vehicle
    {
        string name;

        public Car(string _name) : base()
        {
            name = _name;
            Console.WriteLine($"{ToString()} has been created.");
        }

        public override string ToString()
        {
            return $"Car ({ID}) { name } ";
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($" Car {name} traveled {distance} km.");
        }
        public new void Beep()
        {
            Console.WriteLine($"Car {name} beeps!");
        }
    }

    class Bus : Vehicle
    {
        string name;

        public static readonly uint passengerLimit = 48;
        private uint passengerCount;

        public Bus(string _name) : base()
        {
            name = _name;
            Console.WriteLine( $"{ToString()} has been created.");
        }

        public override string ToString()
        {
            return $"Bus ({ID}) {name} [{passengerCount}/{passengerLimit}].";
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Bus {name} traveled {distance} km with {passengerCount} passengers.");
        }

        public bool SetPassengerCount(uint x)
        {
            if (x <= passengerLimit)
            {
                passengerCount = x;
                return true;
            }
            else
                return false;
        } 
        public uint PassengerCount()
        {
            return passengerCount;
        }

        public override void Beep()
        {
            Console.WriteLine($"Bus {name} beeps!");
        }
    }
    class Truck : Vehicle
    {
        string name;

        public static readonly double capacity = 2500;
        private double load;

        public Truck(string _name) : base()
        {
            name = _name;
            Console.WriteLine($"{ToString()} has been created.");
        }


        public override string ToString()
        {
            return $"Truck ({ID}) {name} [{load} of {capacity} kg].";
        }

        public override void Travel(double distance)
        {
            Console.WriteLine($"Truck {name} traveled {distance} km with load of {load} kg.");
        }

        public bool SetLoad(double x)
        {
            if (x <= capacity)
            {
                load = x;
                return true;
            }
            else
                return false;
        }
        public double Load()
        {
            return load;
        }

        public override void Beep()
        {
            Console.WriteLine($"Truck {name} beeps with loud trumpet!");
        }
    }
}

//STAGE 2: 1.0 points

//Add Beep method which outputs apropriate message:
//    Car NAME beeps!
//    Bus NAME beeps!
//    Truck NAME beeps with loud trumpet!
//But when Car (and only Car) object is reference as Vehicle it returns a message "Vehicle NAME honks!"
//where NAME is a vehicle name.
