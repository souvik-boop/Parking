using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Parking.Tests
{
    public class ParkingLotTests
    {
        public ParkingLotTests()
        {

        }

        #region Create Lot
        [Theory]
        [InlineData(6)]
        public void CreateParkingLot_Success(int size)
        {
            ParkingLot lot = new ParkingLot(size);

            Assert.NotNull(lot);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateParkingLot_Fail(int size)
        {
            ParkingLot lot;
            var ex = Assert.Throws<Exception>(() => lot = new ParkingLot(size));

            Assert.Equal(ex.Message, ConstantErrorMessages.ERROR_INCORRECT_SIZE);
        }
        #endregion

        #region Park Car
        [Theory]
        [InlineData("KA-01-HH-1234", "White")]
        [InlineData("KA-01-HH-9999", "White")]
        [InlineData("KA-01-BB-0001", "Black")]
        [InlineData("KA-01-HH-7777", "Red")]
        [InlineData("KA-01-HH-2701", "Blue")]
        [InlineData("KA-01-HH-3141", "Black")]
        public void ParkCar_SlotAvailable_Success(string registrationNo, string color)
        {
            ParkingLot lot = new ParkingLot(6);
            int num = lot.Park(new Car { Color = color, RegistrationNumber = registrationNo });

            Assert.True(num > 0);
            Assert.True(num <= 6);
        }

        [Theory]
        [InlineData("KA-01-HH-1234", "White")]
        [InlineData("KA-01-HH-9999", "White")]
        [InlineData("KA-01-BB-0001", "Black")]
        [InlineData("KA-01-HH-7777", "Red")]
        [InlineData("KA-01-HH-2701", "Blue")]
        [InlineData("KA-01-HH-3141", "Black")]
        public void ParkCar_SlotAvailable_Fail(string registrationNo, string color)
        {
            ParkingLot lot = new ParkingLot(1);
            int num1 = lot.Park(new Car { Color = "GRAY", RegistrationNumber = "KA-21-HH-9999" });
            int num2 = lot.Park(new Car { Color = color, RegistrationNumber = registrationNo });

            Assert.True(num2 == -1);
        }
        #endregion

        #region Leave Car
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void LeaveCar_SlotFree_Success(int slot)
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });

            Assert.True(lot.Leave(slot));
        }

        [Theory]
        [InlineData(2)]
        public void LeaveCar_SlotFree_FailInRange(int slot)
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });

            Assert.False(lot.Leave(slot));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void LeaveCar_SlotFree_FailOutOfRange(int slot)
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });

            var ex = Assert.Throws<Exception>(() => lot.Leave(slot));

            Assert.Equal(ex.Message, ConstantErrorMessages.ERROR_NOTFOUND);
        }
        #endregion

        #region Get Status
        [Fact]
        public void GetLotStatus_Success()
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });

            var response = lot.Status();

            Assert.Equal<Dictionary<int, Car>>(response, new Dictionary<int, Car>() {
                {1, new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" }},
                {2, new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" }},
            });
        }

        [Fact]
        public void GetLotStatus_Fail()
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });
            lot.Leave(1);
            lot.Leave(2);

            var response = lot.Status();

            Assert.Equal<Dictionary<int, Car>>(response, new Dictionary<int, Car>() {
                {1, null },
                {2, null },
            });
        }
        #endregion

        #region Get RegistrationNumbers From Color
        [Theory]
        [InlineData("White")]
        public void GetRegistrationNumbersFromColor_Success(string color)
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });

            var response = lot.GetRegistrationNumbers(color);

            Assert.Equal(new List<string> { "KA-01-HH-1234", "KA-01-HH-9999" }, response);
        }
        [Theory]
        [InlineData("Black")]
        public void GetRegistrationNumbersFromColor_Fail(string color)
        {
            ParkingLot lot = new ParkingLot(2);
            int num1 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-1234" });
            int num2 = lot.Park(new Car { Color = "White", RegistrationNumber = "KA-01-HH-9999" });

            var response = lot.GetRegistrationNumbers(color);

            Assert.Equal(new List<string> { }, response);
        }
        #endregion
    }
}
