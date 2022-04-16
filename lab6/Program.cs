using System;

namespace lab06
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\n== STAGE 1 ==\n");

            Point2D origin = new Point2D();
            Point2D p1 = new Point2D(1, 2);
            Point2D p2 = new Point2D(1);
            Point2D p3 = new Point2D(x: 1, y: -1);
            Point2D p4 = new Point2D(y: 1);
            Point2D p5 = new Point2D(y: 1, x: 1);
            Point2D p6 = new Point2D(x: 1);

            Console.WriteLine("\n== STAGE 2 ==\n");

            Point2D target = new Point2D(y: 4, x: 3);

            Console.WriteLine("Distance between {0} and {1} is {2:n2}", origin, target, Geometry.Distance(origin, target));
            Console.WriteLine("Distance between {0} and {0} is {2:n2}", origin, origin, Geometry.Distance(origin, origin));
            Console.WriteLine("Circuit of polygon created from p1, p2, p3 is {0:n2}", Geometry.PolygonCircuit(p1, p2, p3));
            Console.WriteLine("Circuit of polygon created from p4, p5, p6 is {0:n2}", Geometry.PolygonCircuit(new Point2D[] { p4, p5, p6 }));

            Console.WriteLine("\n== STAGE 3 ==\n");

            var c1 = new Circle(p2, 5.0);
            var r1 = new Rectangle(p3, 2, 4);
            var r2 = new Rectangle(p4, 2);

            object[] objects = new object[]
            {
                string.Empty, c1, r1, r2, 42, p1,
            };

            Console.WriteLine("\n== STAGE 4 ==\n");


            for (int i = 0; i < objects.Length; i++)
            {
                Console.WriteLine($"\nDescribing object {i + 1}:");
                if (!(objects[i] is Shape))
                {
                    Console.WriteLine($"Object is not a shape");
                    continue;
                }
                else
                    Console.WriteLine($"Object is a shape.\n Object has circuit {((Shape)objects[i]).Circuit()}.");

                if (objects[i] is Polygon)
                {
                    Console.WriteLine($"Shape is a Polygon.\n List of polygon points: \n");
                    foreach (Point2D p in ((Polygon)objects[i]).points)
                    {
                        Console.WriteLine($"{p}\n");
                    }

                    if (objects[i] is Rectangle)
                    {
                        Console.WriteLine($"Shape is a Rectangle and has a diagonal of {((Rectangle)objects[i]).diagonal}");
                    }
                }
                else
                {
                    Console.WriteLine($"Shape is a Polygon.\n");
                }
                if (objects[i] is Circle)
                {

                    Console.WriteLine($"Shape is a Circle that has center at ({((Circle)objects[i]).center}) and radius of {((Circle)objects[i]).radius}");

                }

            }
                Console.WriteLine("\n== STAGE 5 ==\n");
                //{
                //    double x, y, r; 

                //    ((x, y), r) = c1;

                //    Console.WriteLine("Distance of the closest point on the circle to origin point is {0:n2}", Geometry.ToOriginDistance((x, y), r));
                //}

                Console.WriteLine("\n== END ==\n");
            }

        }
    
}
