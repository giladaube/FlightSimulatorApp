using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Models
{

    public class Line
    {
        float a, b;

        public Line()
        {
            a = 0;
            b = 0;
        }

        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public double f(double x)
        {
            return a * x + b;
        }


    }

    class Utilities
    {
        public static double calcThreshold(List<System.Windows.Point> points, Line l, int size)
        {
            double maxDistance = 0;
            for (int i = 1; i < size; i++)
            {
                //float pointDistance = fabs(l.f(points[i]->x) - points[i]->y);
                double pointDistance = Math.Abs(l.f(points[i].X) - points[i].Y);
                if (pointDistance > maxDistance)
                {
                    maxDistance = pointDistance;
                }
            }
            return 1.1 * maxDistance;
        }

        // Calculate the value of Mu
        public static float calcMu(float[] x, int size)
        {
            // Declare sum variable
            float sum = 0;
            // For loop: Summing the array's cells.
            for (int i = 0; i < size; i++)
            {
                sum += x[i];
            }
            // return the value of Mu, sum divided by the array's size. (The average).
            return sum / size;
        }

        // returns the variance of X and Y
        public static float var(float[] x, int size)
        {
            // Declare sum variable
            float sum = 0;
            // Calculate Mu value
            float mu = calcMu(x, size);
            // For loop: Summing the squares of the array cells
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            // Return the variance, according to the given formula
            return sum / size - (mu * mu);
        }

        // returns the covariance of X and Y
        public static float cov(float[] x, float[] y, int size)
        {
            // Calculate Mu of X
            float xMu = calcMu(x, size);
            // Calculate Mu of Y
            float yMu = calcMu(y, size);
            // Declare Sum variable
            float sum = 0;
            // Calculate the covariance
            for (int i = 0; i < size; i++)
            {
                sum = sum + (x[i] - xMu) * (y[i] - yMu);
            }
            // Return the covariance
            return sum / size;
        }


        // returns the Pearson correlation coefficient of X and Y
        public static double pearson(float[] x, float[] y, int size)
        {
            // Calculate the covariance of X and Y
            float covXY = cov(x, y, size);
            // Calculate the standard deviation of X and Y
            double xStdDev = Math.Sqrt(var(x, size));
            double yStdDev = Math.Sqrt(var(y, size));
            // Return the Pearson according to the formula given in the instructions
            return covXY / (xStdDev * yStdDev);
        }



        // performs a linear regression and returns the line equation
        public static Line linear_reg(List<System.Windows.Point> points, int size)
        {
            // Declaring arrays for holding x and y values
            //float x[size];
            float[] x = new float[size];
            //float y[size];
            float[] y = new float[size];

            // For each point in points array, feed values to the x and y arrays
            for (int i = 0; i < size; i++)
            {
                System.Windows.Point p = points[i];
                x[i] = Convert.ToSingle(p.X);
                y[i] = Convert.ToSingle(p.Y);
            }
            // Calculate the covariance of X and Y
            float covXY = cov(x, y, size);
            // Calculate the variance of X
            float varX = var(x, size);
            // Calculate the value of a, according to the formula given in the instructions
            float a = covXY / varX;

            // Calculate the means (Mu's) of X and Y
            float xMu = calcMu(x, size);
            float yMu = calcMu(y, size);
            // Calculate the value of b, according to the formula given in the instructions
            float b = yMu - a * xMu;
            return new Line(a, b);
        }


    }
}
