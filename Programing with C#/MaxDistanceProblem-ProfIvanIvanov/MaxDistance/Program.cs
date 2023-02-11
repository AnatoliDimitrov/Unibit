namespace MaxDistance
{

    public class Program
    {
        /////////////// Dynamic variables - start //////////////////
        static int barrelsCount = 3;
        static double tank = 20;
        static double barrelDisplacement = 100;
        static double fuelConsuption = 10;
        /////////////// Dynamic variables - end //////////////////

        static double currentFuel = 0;
        static double temp = 0;
        static decimal distanceTraveled = 0;
        static long wholeDistanceTraveled = 0;
        static long bestWholeDistanceTraveled = 0;
        static List<double> barrels = new List<double>();
        static List<double> barrelsMovedDistance = new List<double>();

        static decimal result = decimal.MinValue;

        public static void Main()
        {
            if (barrelsCount == 0)
            {
                Console.WriteLine("You can not move!!!");
                return;
            }
            if (barrelsCount == 1)
            {
                Console.WriteLine($"Distance traveled is {barrelDisplacement / fuelConsuption * 100} km.");
                return;
            }

            double fuelPerKm = fuelConsuption / 100;

            int maxStep = (int)(((tank / fuelConsuption) * 100) / 2);

            int start = (int)((barrelsCount + barrelDisplacement) / (fuelConsuption));

            if (start < 1)
            {
                start = 1;
            }
            if (start > maxStep)
            {
                start = maxStep - 1;
            }

            for (int i = 200; i <= 200; i++) // maxStep
            {
                barrels = new List<double>();

                currentFuel = 0;

                barrelsMovedDistance = new List<double>();
                for (int j = 0; j < barrelsCount; j++)
                {
                    barrels.Add(barrelDisplacement);
                    barrelsMovedDistance.Add(0);
                }

                int currentBarrel = -1;
                for (int k = 0; k < barrelsCount; k++)
                {
                    if (barrels[k] > 0)
                    {
                        currentBarrel = k;
                        break;
                    }
                }

                MoveForward(i, fuelPerKm, currentBarrel);
            }
            Console.WriteLine($"Distance traveled is {result.ToString("f2")} km.");
            Console.WriteLine($"All distance traveled is {bestWholeDistanceTraveled} km.");
        }

        /// <summary>
        /// Method for moving barrel ahead
        /// </summary>
        /// <param name="step"></param>
        /// <param name="fuelPerKm"></param>
        /// <param name="currentBarrel"></param>
        private static void MoveForward(int step, double fuelPerKm, int currentBarrel)
        {
            PouringFromOneBarrelToOthers(currentBarrel);
            // Wasting empty barrels
            for (int i = 0; i < barrels.Count; i++)
            {
                if (barrels[i] <= 0)
                {
                    if (temp < barrelsMovedDistance.Max())
                    {
                        temp = barrelsMovedDistance.Max();
                    }
                    barrels.Remove(barrels[i]);
                    barrelsMovedDistance.RemoveAt(i);
                    currentBarrel--;
                    if (currentBarrel < 0)
                    {
                        currentBarrel = 0;
                    }
                    break;
                }
            }

            // Bottom of recursion (checks if only one barrel is left)
            if (barrels.Count <= 1)
            {
                var allFuel = barrels.Max() + currentFuel;
                distanceTraveled = (decimal)(temp + (allFuel * fuelConsuption));
                if (result < distanceTraveled)
                {
                    result = distanceTraveled;
                    bestWholeDistanceTraveled = wholeDistanceTraveled + (long)(allFuel * fuelConsuption);
                }
                //Console.WriteLine(bestWholeDistanceTraveled);
                //Console.WriteLine($"distance {distanceTraveled}");
                wholeDistanceTraveled = 0;
                return;
            }

            // Filling the tank
            Refill(currentBarrel);

            if (currentFuel < fuelPerKm * step)
            {
                Refill(currentBarrel + 1);
            }

            // If barrel is empty after refiling put's it to waste
            if (barrels[currentBarrel] == 0)
            {
                var index = barrels.IndexOf(0);
                barrels.Remove(0);
                barrelsMovedDistance.RemoveAt(index);
            }

            // Moves the car ahead
            currentFuel -= fuelPerKm * step;
            wholeDistanceTraveled += step;

            // Checks if the barrew is wasted
            if (currentBarrel < barrelsMovedDistance.Count)
            {
                barrelsMovedDistance[currentBarrel] += step;
            }

            // returns for new barrel if there is such
            MoveBackward(step, fuelPerKm, currentBarrel);
        }

        /// <summary>
        /// Method for returng for a new barrel
        /// </summary>
        /// <param name="step"></param>
        /// <param name="fuelPerKm"></param>
        /// <param name="currentBarrel"></param>
        private static void MoveBackward(int step, double fuelPerKm, int currentBarrel)
        {
            // Checks if no barrrls left behind it is not turning the car back
            if (currentBarrel >= barrels.Count - 1)
            {
                MoveForward(step, fuelPerKm, 0);
                return;
            }

            // Fuel for the return
            currentFuel -= fuelPerKm * step;
            wholeDistanceTraveled += step;

            // Move ahead
            if (currentBarrel >= barrels.Count - 1)
            {
                MoveForward(step, fuelPerKm, 0);
            }
            else
            {
                MoveForward(step, fuelPerKm, currentBarrel + 1);
            }
            return;
        }
        /// <summary>
        /// Method for filling the tank
        /// </summary>
        /// <param name="currentBarrel"></param>
        private static void Refill(int currentBarrel)
        {
            if (barrels[currentBarrel] > currentFuel - tank)
            {
                barrels[currentBarrel] -= tank - currentFuel;
                currentFuel = tank;
            }
            // This refiling empties the barrel
            else
            {
                currentFuel += barrels[currentBarrel];
                barrels[currentBarrel] = 0;
            }
            return;
        }

        /// <summary>
        /// Method for Pouring from one barrel to other barrels
        /// </summary>
        /// <param name="currentBarrel"></param>
        private static void PouringFromOneBarrelToOthers(int currentBarrel)
        {
            for (int i = currentBarrel + 1; i < barrels.Count; i++)
            {
                if (barrels[currentBarrel] < barrelDisplacement)
                {
                    var freeSpace = barrelDisplacement - barrels[i];
                    if (freeSpace > barrels[currentBarrel])
                    {
                        freeSpace = barrels[currentBarrel];
                    }
                    barrels[currentBarrel] -= freeSpace;
                    barrels[i] += freeSpace;

                    if (barrels[currentBarrel] <= 0)
                    {
                        barrels[currentBarrel] = 0;
                        PouringFromOneBarrelToOthers(currentBarrel + 1);
                        break;
                    }
                }
            }

            return;
        }
    }
}