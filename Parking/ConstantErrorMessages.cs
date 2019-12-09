using System;
using System.Collections.Generic;
using System.Text;

namespace Parking
{
    public static class ConstantErrorMessages
    {
        public const string ERROR_INVALID_INPUT = "You entered an invalid command/input!";
        public const string ERROR_PARKINGLOT_ALREADY_EXISTS = "Parking lot already exists!";
        public const string ERROR_PARKING_FULL = "Sorry, parking lot is full!";
        public const string ERROR_PARKINGLOT_DOESNT_EXIST = "Parking lot does not exist, Please create a lot first!";
        public const string ERROR_NOTFOUND = "Not found!";
        public const string ERROR_UNHANDLED = "Something went wrong, Please try again...";
        public const string ERROR_INCORRECT_SIZE = "Size cannot be less than 1!";
    }
}
