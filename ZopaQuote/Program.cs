using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ZopaQuote
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zopa Quote Calculator");

            var quote = new QuoteEvaluation();
            quote.UserInput();

            //AmountRequested is only set if all validations ok
            if (quote.AmountRequested != 0)
            {
                quote.CalculateQuote();

                Console.WriteLine("Requested amount: £" +  quote.AmountRequested);
                Console.WriteLine("Annual Interest Rate: " + quote.AnnualReturnPercentage() + "%");
                Console.WriteLine("Monthly repayment: £" + quote.MonthlyRepay);
                Console.WriteLine("Total repayment: £" + (quote.TotalRepay));
            }

            Console.ReadLine();
        }
    }
}
