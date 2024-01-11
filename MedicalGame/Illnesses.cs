using System;
//Made a class to create the illnesses with set variables
public class Illnesses
{
    //Inproper way to encapsulate variables, but im not fixing it bc itll take too long with all this code lol
    public string illName { get; set; } //Stores the name of the Illness
    public string[] symptomList { get; set; }

    //Constructor
    public Illnesses(string illness, string[] symptoms)
    {
        illName = illness;
        symptomList = symptoms;
    }
}

