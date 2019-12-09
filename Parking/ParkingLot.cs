using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parking
{
    public class ParkingLot
    {
        private readonly int parking_lot_size;
        private readonly Dictionary<int, Car> carCollection;

        /// <summary>
        /// initialize the class with the parking lot size
        /// </summary>
        /// <param name="size"></param>
        public ParkingLot(int size)
        {
            if (size < 1)
                throw new Exception(ConstantErrorMessages.ERROR_INCORRECT_SIZE);

            this.parking_lot_size = size;
            carCollection = new Dictionary<int, Car>();

            // instantiate the collection as well
            for (int i = 1; i <= parking_lot_size; i++)
            {
                carCollection.Add(i, null);
            }
        }

        /// <summary>
        /// call func to park car
        /// </summary>
        /// <param name="car"></param>
        /// <returns>parking slot number</returns>
        public int Park(Car car)
        {
            foreach (var item in carCollection.Keys)
            {
                if (carCollection[item] == null)
                {
                    carCollection[item] = car;
                    return item;
                }
            }
            return -1;
        }

        /// <summary>
        /// func to free up parking slot
        /// </summary>
        /// <param name="slot_number"></param>
        /// <returns></returns>
        public bool Leave(int slot_number)
        {
            if (slot_number > parking_lot_size)
                throw new Exception(ConstantErrorMessages.ERROR_NOTFOUND);
            if (carCollection[slot_number] != null)
            {
                carCollection[slot_number] = null;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns all the cars present in the parking lot
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Car> Status()
        {
            return carCollection;
        }

        /// <summary>
        /// Returns all the registration number of cars present in the parking lot with specific color
        /// </summary>
        /// <returns></returns>
        public List<string> GetRegistrationNumbers(string color)
        {
            return carCollection
                .Values
                .Where(s => s.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.RegistrationNumber)
                .ToList();
        }

        /// <summary>
        /// Returns all the slot number of cars present in the parking lot with specific color
        /// </summary>
        /// <returns></returns>
        public List<int> GetSlotNumbersC(string color)
        {
            return carCollection
                .Where(s => s.Value.Color.Equals(color, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.Key)
                .ToList();
        }

        /// <summary>
        /// Returns all the slot number of cars present in the parking lot with specific rn
        /// </summary>
        /// <returns></returns>
        public List<int> GetSlotNumbersR(string registrationNumber)
        {
            return carCollection
                .Where(s => s.Value.RegistrationNumber.Equals(registrationNumber, StringComparison.InvariantCultureIgnoreCase))
                .Select(s => s.Key)
                .ToList();
        }

    }

}
