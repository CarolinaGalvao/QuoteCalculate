using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ZopaQuote
{
    class Validations
    {

        public static bool IsAmountValid(int requestedVal)
        {
            if (!(requestedVal % 100 == 0 && requestedVal >= 1000 && requestedVal <= 15000))
            {
                Console.WriteLine("Your requested amount must lie " +
                    "in any £100 increment between £1000 and £15000.");
                return false;
            }

            return true;
        }

        //Checks there's enough money available for the loan
        public static bool IsRequestValid(List<Lenders> allLenders, int requestedVal)
        {
            int totalAmount = allLenders.Sum(x => x.AvailableAmount);

            if (!(totalAmount >= requestedVal))
            {
                Console.WriteLine("Oops, it's not possible to provide a quote.");
                return false;
            }

            return true;
        }
    }
}
