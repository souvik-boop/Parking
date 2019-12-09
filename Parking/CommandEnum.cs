using System;
using System.Collections.Generic;
using System.Text;

namespace Parking
{
    public enum CommandEnum
    {
        create_parking_lot,
        park,
        leave,
        status,
        registration_numbers_for_cars_with_colour,
        slot_numbers_for_cars_with_colour,
        slot_number_for_registration_number
    }
}
