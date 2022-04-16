using System;

namespace lab06
{

    //## Stage 1 - 1 point 

//Define `struct` named Point2D.
//- It contains `public` fields `x`, `y` of type `double`. 
//- Fields are immutable.
//- It contains constructor that takes from 0, 1 or 2 `double`s.
//  In case when number of passed arguments is 0 or 1 then value of not specified coordinates should be set to  0.
//  Order of passed parameters from left to right is: x, y.
//- It allows to provide value for specific coordinate and set other coordinates to 0.
//- When instance is passed as argument to `Console.WriteLine` method it output message in following format: (x, y)
//- Creating instance of `Point2D` with some explicit coordinates should print following message on `Console`
//  "Point2D(x, y) has been created".
//  Remember about 'Do not repeat yourself (DRY)' rule

    struct Point2D
    {
        public readonly double x;
        public readonly double y;

        public override string ToString()
        {
            return $"({x}, {y})";
        }
        public Point2D(double x=0, double y = 0)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine($"Point2D({x},{y}) has been created.");
        }
    }

//## Stage 2 - 1 point

// Create static `class` named `Geometry`. It contains 2 public methods:
//`Distance` method that takes two `Point2D` points and returns cartesian distance between them.
//`PolygonCircuit` method that takes variable number of `Point2D`s and calculates circuit of created polygon.
// In case where number of passed points is less than 3 method should return -1.0.
//Both methods are not modifying instance of passed arguments and should be annotated with proper keyword for compiler.


    class Geometry
    {
        public static double Distance(Point2D p1, Point2D p2)
        {
            return Math.Sqrt((p2.x - p1.x)*(p2.x - p1.x) + (p2.y - p1.y)*(p2.y - p1.y));
        }

        public static double PolygonCircuit(params Point2D[] points)
        {
            if (points.Length < 3)
                return -1.0;

            double a = Distance(points[0], points[points.Length - 1]);

            for(int i=0; i<points.Length-1; i++)
            {
                a += Distance(points[i], points[i + 1]);
            }
            return a;
        }
    }

    //## Stage 3 - 1 point

    //    Create abstract `class` named `Shape`. 
    //It contains abstract method `Circuit` that calculates circuit of given shape.

    //Create abstract `class` named `Polygon` that inherits from `Shape` class. 
    //It contains protected constructor that takes variable number of `Point2Ds`. 
    //It contains array of points of given polygon - public field.
    //It provides implementation for `Circuit` method that calculates circuit for any polygon.

    //Create `Rectangle` class that inherits from `Polygon`. `Polygon` has two constructors:
    //- constructor that takes single Point2D as left bottom point and 2 double numbers that represent width and height.
    //- Constructor that takes single Point2D as left bottom point and 1 double number that represents side of square - special case of rectangle.
    //`Rectangle` has public field of type `double` that holds length of rectangle's diagonal.

    //Create `Circle` class that inherits from `Shape`.
    //Constructor takes single `Point2D` as circle center and 1 number that represents length of the radius.
    //Values passed are accesible as public fields.

    public abstract class Shape
    {
        public abstract double Circuit();
    }

    abstract class Polygon : Shape
    {
        public Point2D[] points;

        protected Polygon(params Point2D[] points)
        {
            this.points = points;
        }
        public override double Circuit()
        {
            return Geometry.PolygonCircuit(points);
        }
    }

    class Rectangle : Polygon
    {
        public double diagonal;

        public Rectangle(Point2D p, double width, double height) : base(p) 
        {
            diagonal = Math.Sqrt(width * width + height * height);
            Point2D point2 = new Point2D(p.x, p.y + height);
            Point2D point3 = new Point2D(p.x+width, p.y+height );
            Point2D point4 = new Point2D(p.x+width, p.y);

            points = new[] { p, point2, point3, point4 };

        }
        public Rectangle(Point2D p, double s)
        {
            diagonal = s * Math.Sqrt(2);
            Point2D point2 = new Point2D(p.x, p.y + s);
            Point2D point3 = new Point2D(p.x + s, p.y + s);
            Point2D point4 = new Point2D(p.x + s, p.y);

            points = new[] { p, point2, point3, point4 };
        }
    }

    class Circle : Shape
    {
        public Point2D center;
        public double radius;

        public Circle(Point2D center, double r)
        {
            this.center = center;
            radius = r;
        }

        public override double Circuit()
        {
            return Math.PI * 2 * radius;
        }
    }

}
