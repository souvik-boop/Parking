using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Parking.Tests
{
    public class RandomDataCreator
    {
        ParkingLot lot;
        private readonly ITestOutputHelper output;

        public RandomDataCreator(ITestOutputHelper output)
        {
            lot = new ParkingLot(1000);
            this.output = output;
        }

        [Fact]
        public void ParkCars()
        {
            Random random = new Random();
            for (int i = 1; i <= random.Next(1000); i++)
            {
                lot.Park(new Car
                {
                    RegistrationNumber = Guid.NewGuid().ToString(),
                    Color = $"Color {i % 100}"
                });
            }
            PrintStatus();
        }

        [Fact]
        public void LeaveCars()
        {
            Random random = new Random();
            for (int i = 1; i <= random.Next(1000); i++)
            {
                lot.Park(new Car
                {
                    RegistrationNumber = Guid.NewGuid().ToString(),
                    Color = $"Color {i % 100}"
                });
            }

            var statusResponse = lot.Status();
            foreach (var item in statusResponse.Keys.ToList())
            {
                if (int.Parse(random.Next(2).ToString()) == 1)
                    lot.Leave(item);
            }
            PrintStatus();
        }

        private void PrintStatus()
        {
            output.WriteLine($"{"Slot Number.".ToString().PadRight(20)}\t{"Registration No.".ToString().PadRight(20)}\t{"Color".ToString().PadRight(20)}");
            var statusResponse = lot.Status();
            if (statusResponse != null)
                foreach (var item in statusResponse)
                {
                    if (item.Value != null)
                        output.WriteLine($"{item.Key.ToString().PadRight(20)}\t{item.Value.RegistrationNumber.ToString().PadRight(20)}\t{item.Value.Color.ToString().PadRight(20)}");
                }
        }
    }
}
