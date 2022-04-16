
using System;

namespace Vehicles
{
    //STAGE 3: 1.0 points

    //Create class Factory (in Factory.cs file) with static method Manufacture() which takes two parameters:
    //1.type of vehicle to create (only "car", "bus", "truck")
    //2.name of new vehicle
    //Use array below to create new vehicles in factroy(use for loop to iterate over all orders)
    //    Display appropriate information if vehicle can be created or not.
    //    "New XXX has beed added to fleet." where XXX is an information about created object
    //    "Factory could not manufactured a TYPE."
    class Factory
    {
        public static Vehicle Manufacture(string type, string name)
        {
            switch (type)
            {
                case "car":

                    return new Car(name);

                case "bus": 
                    return new Bus(name);

                case "truck":
                    return new Truck(name);
                default:
                    Console.WriteLine($"Factory could not manufactured a {type}");
                    return null;
            }

        }
        
    }
}
