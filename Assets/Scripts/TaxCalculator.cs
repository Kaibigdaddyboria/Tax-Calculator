using UnityEngine;
using SpeechLib;
using System.Threading.Tasks;
using TMPro;
using System;

public class TaxCalculator : MonoBehaviour
{
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;

    // Variables
    bool textToSpeechEnabled = true;
    public TextMeshProUGUI grosssalary;
    public TextMeshProUGUI payperiod;
    private void Start()
    {
        //Speak("Welcome to the A.T.O. Tax Calculator");
        print(CalculateIncomeTax(200000));
    }

    // Run this function on the click event of your 'Calculate' button
    public void Calculate()
    {
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;

        // Input
        double grossSalaryInput = GetGrossSalary();
        string salaryPayPeriod = GetSalaryPayPeriod();

        // Calculations
        double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);
        double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);

        // Output
        OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);
    }

    private double GetGrossSalary()
    {

        double grossYearlySalary;
        if (Double.TryParse(grosssalary.text, out grossYearlySalary) && grossYearlySalary > 0)
        {
            return grossYearlySalary;
        }
        else
        {
            grosssalary.text = "Invalid Input";
            return grossYearlySalary = 0;
        }
        
    }

    private string GetSalaryPayPeriod()
    {
        string salaryPayPeriod = payperiod.text;
        return salaryPayPeriod;
    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, string salaryPayPeriod)
    
    {
        double grossYearlySalary = 0;
        if (salaryPayPeriod == "yearly")
        {
            grossYearlySalary = grossSalaryInput;
        }
        else if (salaryPayPeriod == "monthly")
        {
            grossYearlySalary = grossSalaryInput * 12;
        }
        else if (salaryPayPeriod == "weekly")
        {
            grossYearlySalary = grossSalaryInput * 52;
        }
        return grossYearlySalary;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        // This is a stub, replace with the real calculation and return the result
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);
        double netIncome = 33000;        
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double medicareLevyPaid = 2000;        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        // This is a stub, replace with the real calculation and return the result
        double incomeTaxPaid = 15000;
        if (grossYearlySalary < 18200)
        {
            return grossYearlySalary;
        }
        else if (grossYearlySalary > 18200 && grossYearlySalary < 45000)
        {
            grossYearlySalary = (grossYearlySalary - 18200) * 0.19;
            return grossYearlySalary;
        }
        else if (grossYearlySalary > 45000 && grossYearlySalary < 120000)
        {
            grossYearlySalary = (grossYearlySalary - 45000) * 0.325 + 5092;
            return grossYearlySalary;
        }
        else if (grossYearlySalary > 120000 && grossYearlySalary < 180000)
        {
            grossYearlySalary = (grossYearlySalary - 120000) * 0.337 + 29467;
            return grossYearlySalary;
        }
        else if (grossYearlySalary > 180000)
        {
            grossYearlySalary = (grossYearlySalary - 180000) * 0.450 + 51667;
            return grossYearlySalary;
        }

        return incomeTaxPaid;
    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        // "Income tax paid: $" + incomeTaxPaid.ToString("F2");
        // "Net income: $" + netIncome.ToString("F2");
    }

    // Text to Speech
    private async void Speak(string textToSpeech)
    {
        if(textToSpeechEnabled)
        {
            SpVoice voice = new SpVoice();
            await SpeakAsync(textToSpeech);
        }
    }

    private Task SpeakAsync(string textToSpeak)
    {
        return Task.Run(() =>
        {
            SpVoice voice = new SpVoice();
            voice.Speak(textToSpeak);
        });
    }
}
