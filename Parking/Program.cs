using System;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot lot = null;

            // Keep the console running for inputs
            while (true)
            {
                try
                {
                    Console.Write("ENTER COMMAND : ");

                    // If input is invalid then rerun the loop
                    string input_line = Console.ReadLine();
                    string input_command = input_line.Split(' ')[0];

                    if ((!Enum.TryParse(input_command, out CommandEnum c)) ||
                        (!Enum.IsDefined(typeof(CommandEnum), Enum.Parse(typeof(CommandEnum), input_command))))
                    {
                        Console.WriteLine(ConstantErrorMessages.ERROR_INVALID_INPUT);
                        continue;
                    }

                    // switch through the commands
                    switch (c)
                    {
                        case CommandEnum.create_parking_lot:
                            {
                                if (lot == null)
                                {
                                    int input_value = int.Parse(input_line.Split(' ')[1]);
                                    lot = new ParkingLot(input_value);
                                    Console.WriteLine($"Created a parking lot with {input_value} slots");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_ALREADY_EXISTS);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.park:
                            {
                                if (lot != null)
                                {
                                    string input_value1 = input_line.Split(' ')[1];
                                    string input_value2 = input_line.Split(' ')[2];
                                    int parking_spot = lot.Park(new Car
                                    {
                                        RegistrationNumber = input_value1,
                                        Color = input_value2
                                    });
                                    if (parking_spot != -1)
                                        Console.WriteLine($"Allocated slot number: {parking_spot}");
                                    else
                                        Console.WriteLine(ConstantErrorMessages.ERROR_PARKING_FULL);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.leave:
                            {
                                if (lot != null)
                                {
                                    int input_value = int.Parse(input_line.Split(' ')[1]);
                                    bool leaveResponse = lot.Leave(input_value);
                                    if (leaveResponse)
                                        Console.WriteLine($"Slot number {input_value} is free");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.status:
                            {
                                if (lot != null)
                                {
                                    Console.WriteLine($"{"Slot Number.".ToString().PadRight(20)}\t{"Registration No.".ToString().PadRight(20)}\t{"Color".ToString().PadRight(20)}");
                                    var statusResponse = lot.Status();
                                    if (statusResponse != null)
                                        foreach (var item in statusResponse)
                                        {
                                            if (item.Value != null)
                                                Console.WriteLine($"{item.Key.ToString().PadRight(20)}\t{item.Value.RegistrationNumber.ToString().PadRight(20)}\t{item.Value.Color.ToString().PadRight(20)}");
                                        }
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.registration_numbers_for_cars_with_colour:
                            {
                                if (lot != null)
                                {
                                    string input_value = input_line.Split(' ')[1];
                                    var statusResponse = lot.GetRegistrationNumbers(input_value);
                                    string finalResp = string.Empty;
                                    if (statusResponse != null)
                                        foreach (var item in statusResponse)
                                        {
                                            finalResp = finalResp + item + ", ";
                                        }
                                    if (!string.IsNullOrEmpty(finalResp))
                                        Console.WriteLine(finalResp.Remove(finalResp.Length - 2));
                                    else
                                        Console.WriteLine(ConstantErrorMessages.ERROR_NOTFOUND);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.slot_numbers_for_cars_with_colour:
                            {
                                if (lot != null)
                                {
                                    string input_value = input_line.Split(' ')[1];
                                    var statusResponse = lot.GetSlotNumbersC(input_value);
                                    string finalResp = string.Empty;
                                    if (statusResponse != null)
                                        foreach (var item in statusResponse)
                                        {
                                            finalResp = finalResp + item + ", ";
                                        }
                                    if (!string.IsNullOrEmpty(finalResp))
                                        Console.WriteLine(finalResp.Remove(finalResp.Length - 2));
                                    else
                                        Console.WriteLine(ConstantErrorMessages.ERROR_NOTFOUND);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        case CommandEnum.slot_number_for_registration_number:
                            {
                                if (lot != null)
                                {
                                    string input_value = input_line.Split(' ')[1];
                                    var statusResponse = lot.GetSlotNumbersR(input_value);
                                    string finalResp = string.Empty;
                                    if (statusResponse != null)
                                        foreach (var item in statusResponse)
                                        {
                                            finalResp = finalResp + item + ", ";
                                        }
                                    if (!string.IsNullOrEmpty(finalResp))
                                        Console.WriteLine(finalResp.Remove(finalResp.Length - 2));
                                    else
                                        Console.WriteLine(ConstantErrorMessages.ERROR_NOTFOUND);
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine(ConstantErrorMessages.ERROR_PARKINGLOT_DOESNT_EXIST);
                                    continue;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(ConstantErrorMessages.ERROR_UNHANDLED);   
                }
            }
        }
    }
}
