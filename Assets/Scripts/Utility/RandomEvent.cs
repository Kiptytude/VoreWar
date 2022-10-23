using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class RandomEvent
{
    internal string MainText;

    internal string FirstChoiceText;
    internal Action FirstChoice;
    internal string SecondChoiceText;
    internal Action SecondChoice;
    internal string ThirdChoiceText;
    internal Action ThirdChoice;

    public RandomEvent(string mainText, string firstChoiceText, Action firstChoice, string secondChoiceText, Action secondChoice, string thirdChoiceText, Action thirdChoice)
    {
        MainText = mainText;
        FirstChoiceText = firstChoiceText;
        FirstChoice = firstChoice;
        SecondChoiceText = secondChoiceText;
        SecondChoice = secondChoice;
        ThirdChoiceText = thirdChoiceText;
        ThirdChoice = thirdChoice;
    }
}
