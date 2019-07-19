using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace ZopaQuote
{
    class QuoteEvaluation
    {
        private static string filePath = @"C:\MarketDataforExercise.csv";
        private int totalPayments = 36;

        //Properties
        private List<Lenders> AllLenders { get; set; }
        public int AmountRequested { get; set; }
        public double MonthlyRepay { get; set; }
        public double TotalRepay { get; set; }
        public double InterestRate { get; set; }
        

        public QuoteEvaluation()
        {
            //if market file updated always gets latest version
            List<string> filedata = FileHandle.ExtractData(filePath);
            AllLenders = FileHandle.GetLendersData(filedata);
            AllLenders = AllLenders.OrderBy(x => x.Rate).ToList();

            AmountRequested = 0;
        }


        public void UserInput()
        {
            Console.WriteLine("Please insert your GBP loan amount: ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int amount))
                Validate(amount);
        }


        private void Validate(int amount)
        {
            if (!Validations.IsAmountValid(amount))
                UserInput();
            else
            {
                if(Validations.IsRequestValid(AllLenders, amount))
                    AmountRequested = amount;
            }
        }

        public void CalculateQuote()
        {
            GetAnualInterestRate();
            MonthlyRepayment();
            TotalRepayment();
        }


        //Calculated as the average annual interest from the necessary lenders
        private void GetAnualInterestRate()
        {
            var neededLenders = new List<Lenders>();
            int amountSum = 0;

            foreach (var lender in AllLenders)
            {
                if (AmountRequested > amountSum)
                    neededLenders.Add(lender);
                else
                    break;

                amountSum = amountSum + lender.AvailableAmount;
            }

            double avgInterest = (double)neededLenders.Average(x => x.Rate);
            InterestRate =  Math.Round(avgInterest, 3);
        }


        private void MonthlyRepayment()
        {
            double periodicInterest = InterestRate / 12;

            double A = AmountRequested * (periodicInterest + 
                (periodicInterest / (Math.Pow(1 + periodicInterest, (double)totalPayments) - 1)));

            MonthlyRepay =  Math.Round(A, 2);
        }


        private void TotalRepayment()
        {
            TotalRepay = MonthlyRepay * totalPayments;
        }

        public double AnnualReturnPercentage()
        {
            return (double)Math.Round((InterestRate*100), 1);
        }
    }
}
