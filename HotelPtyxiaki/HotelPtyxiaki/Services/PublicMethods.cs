using System;
using System.Collections.Generic;
using System.Text;

namespace HotelPtyxiaki
{
    public static class PublicMethods
    {
        public static List<DateTime> ConvertStringToListOfDateTime(string datetimeString)
        {
            List<DateTime> dateTimeList = new List<DateTime>();

            string[] datetimeArray = datetimeString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string dtString in datetimeArray)
            {
                if (DateTime.TryParse(dtString.Trim(), out DateTime dt))
                {
                    dateTimeList.Add(dt);
                }
                else
                {
                    Console.WriteLine($"Unable to parse: {dtString}");
                }
            }

            return dateTimeList;
        }

        static string ConvertDateTimeListToString(List<DateTime> dateTimeList)
        {
            List<string> formattedDates = new List<string>();

            foreach (DateTime dt in dateTimeList)
            {
                formattedDates.Add(dt.ToString("dd/MM/yyyy HH:mm:ss"));
            }

            return string.Join(", ", formattedDates);
        }

    }
}
