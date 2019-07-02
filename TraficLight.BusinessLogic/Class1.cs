//using System;
//using System.Collections.Generic;

//namespace TraficLight.BusinessLogic
//{
//    public class ClockFace
//    {
//        public static Dictionary<byte, byte> numbers = new Dictionary<byte, byte>
//        {
//            { 0, 0b111_0111 },
//            { 1, 0b001_0010 },
//            { 2, 0b101_1101 },
//            { 3, 0b101_1011 },
//            { 4, 0b011_1010 },
//            { 5, 0b110_1011 },
//            { 6, 0b110_1111 },
//            { 7, 0b101_0010 },
//            { 8, 0b111_1111 },
//            { 9, 0b111_1011 }
//        };

//        public static Response IsInside(Request request)
//        {
//            if (request.Color == "green")
//            {
//                byte first = request.Numbers[0];
//                byte second = request.Numbers[1];

//                var tempStart = new List<byte>();

//                foreach (var temp in numbers)
//                {
//                    if ((temp.Value & first) == first)
//                    {
//                        //tempStart.Add(temp.Key);
//                    }
//                }
//            }
//        }

//        public static void Finder(byte tt)
//        {
//            foreach (var temp in numbers)
//            {
//                Console.Write
//            }
//            if ((b & pos) == pos)
//            {

//            }
//        }
//    }
//    public class Response
//    {
//        public int[] Start { get; set; }
//        public byte[] Missing { get; set; }
//    }
//    public class Request
//    {
//        public string Color { get; set; }
//        public byte[] Numbers { get; set; }
//    }
//}
