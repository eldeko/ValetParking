using System;
using System.Collections.Generic;
using System.Linq;

namespace ValetParking.Dto
{
    //Written by Edward.
    public class ParkingSearchParamsDto
    {
        public DateTime Day { get; set; }
        public IList<HourRangeDto> HourRanges { get; set; }

        public IList<string> Validate()
        {
            List<string> response = new List<string>();

            foreach (var range in HourRanges)
            {
                if (!IsTimeValid(range.From))
                {
                    response.Add($"'From' parameter has an invalid value or format ('{range.From}').");
                }

                if (!IsTimeValid(range.To))
                {
                    response.Add($"'To' parameter has an invalid value or format ('{range.To}').");
                }
            }

            return response;
        }

        private bool IsTimeValid(string timeText)
        {
            System.Text.RegularExpressions.Regex checktime =
                new System.Text.RegularExpressions.Regex(@"(([0-1][0-9])|([2][0-3])):([0-5][0-9])");

            return checktime.IsMatch(timeText);
        }
    }
}