using System;
using System.Collections.Generic;
using System.Text;

namespace Parking
{
    public class Car : IEquatable<Car>
    {
        public string RegistrationNumber { get; set; }
        public string Color { get; set; }

        #region Overriden methods for comparison
        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Car c = (Car)obj;
                return (RegistrationNumber == c.RegistrationNumber) && (Color == c.Color);
            }
        }
        public bool Equals(Car other)
        {
            return other != null &&
                   RegistrationNumber == other.RegistrationNumber &&
                   Color == other.Color;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(RegistrationNumber, Color);
        }
        public static bool operator ==(Car left, Car right)
        {
            return EqualityComparer<Car>.Default.Equals(left, right);
        }
        public static bool operator !=(Car left, Car right)
        {
            return !(left == right);
        } 
        #endregion
    }
}
