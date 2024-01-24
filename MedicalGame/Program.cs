using System.Xml.Schema;

namespace MedicalGame
{

    /*
     *  ___                      ___           _              _  _ 
       / __| _  _  _ _  ___     / __| __ _  __| | ___  _ __  | || |
      | (__ | || || '_|/ -_)   | (__ / _` |/ _` |/ -_)| '  \  \_. |
       \___| \_._||_|  \___|    \___|\__/_|\__/_|\___||_|_|_| |__/ 
            __   __   _             ___            _   
            \ \ / /  / |           |_  )          / |  
             \   /   | |    _       / /    _      | |  
              \_/    |_|   (_)     /___|  (_)     |_|   


    1.2.1 Update: Strep

    Last updated 1/12/24
    Check my Github for updates! @ Jomigack
    Repository Link: https://github.com/Jomigack/CureCademy-Game

     What is this game about?
    An endless Game where the Patient that the user needs to cure has a set of symptoms that correlate to an illness
    Have the user conduct tests for the illnesses to see which one matches the symptoms,
    If they get it right within 5 tests, the patient recovers, if not they die and they lose the game
     */


    internal class Program
    {

        static void Main(string[] args)
        {
            List<Illnesses> illnessList = new List<Illnesses>(); //Creates a list to store all the illnesses and their symptoms to be stored (Class objects)

            ///Creeating an object example:
            Illnesses[] illnessCatalouge =
            {
                    /*Cold*/        new Illnesses("Cold", new string[] { "Muscle and body aches", "Runny nose", "Sore throat"}),//For the array, you must specify new string[] in order for it to pass through
                    /*flu*/         new Illnesses("Flu", new string[] { "Fever", "Sore throat", "Muscle and body aches" }),
                    /*Bronchitis*/  new Illnesses("Bronchitis", new string[] { "Shortness of breath", "Chest discomfort", "Mucus" }),
                    /*Pneumonia*/   new Illnesses("Pneumonia", new string[] { "Chest discomfort", "Fever", "Cough"}),
                    /*Pharyngitis*/ new Illnesses("Pharyngitis", new string[] {"Fever", "Cough", "Sore throat"}),
                    /*Rhinitis*/    new Illnesses("Rhinitis", new string[] {"Runny nose", "Mucus", "Sneezing"}),
                    /*Strep*/       new Illnesses("Strep", new string[] {"Sore throat", "Mucus", "Swollen tonsils"})
                };

            //Adding the objects into the object list:
            illnessList.AddRange(illnessCatalouge); //Adds all values stored in the illnessCatalouge array to the object list, as both are based off of the same class

            bool playing = true;
            int patientCount = 0;
            int amtofTestsDone = 0;
            while (playing)
            {
                bool patientDone = false;
                patientCount++;
                Console.WriteLine("Welcome Doctor, to Cure-Cadamey!" +
                    " A Text-based medical game where you need to cure a patient by conducting tests to see what their illness is, are you ready? (Press any key to continue)");
                Console.ReadKey();
                Console.Clear();
                Random randomIndex = new Random(); //init a random number generator
                int chosenIllness = randomIndex.Next(illnessList.Count); //Grabs a random index from a value stores in the list
                Illnesses selectedIllness = illnessList[chosenIllness]; //Creates an object hat holds the selected illness's values
                                                                        //DELETE UP TO THE FOREACH SYMPTOM BEFORE GAME IS COMPLETE< THIS IS TO HELP DEBUG!
                /*Console.WriteLine($"Selected Illness: {selectedIllness.illName}");
                Console.WriteLine("Symptoms:");

                foreach (string symptom in selectedIllness.symptomList)
                {
                    Console.WriteLine($"- {symptom}");
                }*/

                Console.WriteLine($"Patient #{patientCount} Walks into the ER: \nGoal: Find his symptoms and cure him in the right amount of tires. To see the list of commands you can use type (cmds)");
                string[] knownSymptoms = new string[3]; //Stores the symptoms gathered by the player
                bool patientCuring = true;
                while (patientCuring)
                {
                    string userInput = Console.ReadLine().ToLower();
                    //Storing all possible test tools/ labs
                    string[] testTools = { "Esc", "Ask", "Thermometer", "Tongue Depressor", "Breathing Test", "Endoscope" };
                    string[] testDesc = { "Quits out of the Test command menu",
                            "Ask the patient what their current symptoms are (works with low-tier symptoms, has a chance of failure if asking for general symptoms)",
                            "See what their Tempurature is (Used to see if a fever is probable or not)",
                            "Allows you to examine the parients oral cavity",
                            "A short test that evaluates how deep the patient can breathe",
                            "A device used to look into someone's nose"};
                    int testsLeft = 5;
                    do
                    {
                        if (testsLeft != 0) //WHen you can still conduxt tests
                        {   //Add a command for the user to see wha tthe symptoms correlate to to get a proper diagnosis
                            if (userInput == "cmds")
                            {
                                printCommands();
                                userInput = Console.ReadLine().ToLower();
                            }
                            else if (userInput == "test")
                            {
                                bool underGoingTest = true;
                                Console.WriteLine($"Here is a list of all the possible tests you can do (You have {testsLeft} tests left before you must conclude the diagnosis):");
                                for (int i = 0; i < testTools.Length; i++)
                                {
                                    blueText(testTools[i]);
                                    Console.WriteLine(testDesc[i]);

                                }
                                string userTest = Console.ReadLine().ToLower();
                                do
                                {
                                    if (userTest == testTools[0].ToLower()) //esc
                                    {
                                        Console.Clear();
                                        printCommands();
                                        userInput = Console.ReadLine().ToLower();
                                        underGoingTest = false;
                                        break;
                                    }
                                    else if (userTest == "ask") //Ask Command, detects colds and muscle aches
                                    {
                                        amtofTestsDone++;
                                        Console.Clear();
                                        Console.WriteLine("What do you want to ask the patient?");
                                        string[] askCmds = {"Aches", "Cough" ,"Symptoms"};
                                        string[] askDesc = {"Ask the patient if they have been experiencing muscle or body aches",
                                            "Ask the patient if they have been coughing recently",
                                            "Ask the patient what general sympotoms they are experiencing (chance of failure)"};
                                        for(int i = 0; i < askCmds.Length; i++) //Prints all possible ask commands from the array
                                        {
                                            blueText(askCmds[i]);
                                            Console.WriteLine($" {askDesc[i]}");
                                        }
                                        string userAsk = Console.ReadLine().ToLower();
                                        bool isAsk = true;
                                        do
                                        {
                                            Console.Clear();
                                            if (userAsk == "symptoms")
                                            {
                                                Random patientTruthGenerator = new Random(); //A random var to see if the user is correct about their own symptoms or not
                                                int patientTruth = patientTruthGenerator.Next(1, 101);
                                                if (patientTruth <= 50) //Grabs a random symptom
                                                {
                                                    int randSymptom = randomIndex.Next(illnessList.Count);
                                                    Illnesses selectedRandIllness = illnessList[randSymptom];
                                                    Console.WriteLine($"You ask what symptoms the patient is experiencing, they say they might have the symptom of:{selectedRandIllness.symptomList[0]} But they aren't very sure (Probability is {patientTruth})");
                                                    underGoingTest = false;
                                                }
                                                else if (patientTruth > 50 && patientTruth <= 75) //Grabs a symptom from a smaller range of illnesses so the likelehood is larger
                                                {
                                                    int randSymptom = randomIndex.Next(chosenIllness, chosenIllness + 3);
                                                    if (randSymptom > illnessList.Count()) //So the random is not out of the list index
                                                    {
                                                        randSymptom -= 4;
                                                    }
                                                    Illnesses selectedRandIllness = illnessList[randSymptom];
                                                    Console.WriteLine($"You ask what symptoms the patient is experiencing, they say they might have the symptom of:{selectedRandIllness.symptomList[0]}, they seem to have some confidence. (Probability is {patientTruth})");
                                                    underGoingTest = false;
                                                }
                                                else if (patientTruth > 75)//The symptom is the first one out of the symptom list for that illness
                                                {
                                                    Console.WriteLine($"You ask what symptoms the patient is experiencing, they say they might have the symptom of:{selectedIllness.symptomList[0]}, they seem to have confidence. (Probability is {patientTruth})");
                                                    symptomListChecker(knownSymptoms, selectedIllness.symptomList[0]);
                                                    underGoingTest = false;
                                                }

                                                isAsk = false;
                                                break;
                                            }
                                            else if (userAsk == "aches")
                                            {
                                                //Add code here so when the user has muscle or body aches 
                                                Console.WriteLine("You ask the patient if they are having any muscle or body aches");
                                                bool isAches = getSymptom(selectedIllness, "Muscle and body aches");
                                                if (isAches)
                                                {
                                                    Console.WriteLine("The patient does state that they have had muscle and body aches.");
                                                    symptomListChecker(knownSymptoms, "Muscle and body aches");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The patient states that they have not had muscle and body aches.");
                                                }

                                                isAsk = false;
                                                underGoingTest = false;
                                                break;
                                            }
                                            else if (userAsk == "cough")
                                            {
                                                //Add code here so when the user has muscle or body aches 
                                                Console.WriteLine("You ask the patient if they have been coughing");
                                                bool isAches = getSymptom(selectedIllness, "Cough");
                                                if (isAches)
                                                {
                                                    Console.WriteLine("The patient does state that they have been coughing recently");
                                                    symptomListChecker(knownSymptoms, "Cough");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("The patient states that they have not been coughing recently.");
                                                }

                                                isAsk = false;
                                                underGoingTest = false;
                                                break;
                                            }
                                            else
                                            {
                                               
                                                Console.WriteLine("Input invalid: What do you want to ask the patient?\n" +
                                                    "if they have any muscle aches ('aches')" +
                                                    "\nOr what symptoms they're experiencing ('symp')");
                                                userAsk = Console.ReadLine().ToLower();
                                            }
                                        } while (isAsk);

                                        testsLeft--;
                                        break;
                                    }
                                    else if (userTest == "thermometer") // Thermometer detects fever
                                    {
                                        amtofTestsDone++;
                                        Console.Clear();
                                        Console.WriteLine("You take the patients temperature:");
                                        bool isFever = getSymptom(selectedIllness, "Fever");
                                        Random temperature = new Random();
                                        if (isFever) //If the user has a fever
                                        {
                                            double temp = 99 + (115.0 - 99.0) * temperature.NextDouble();
                                            double roundTemp = Math.Round(temp, 1); //Rounds temp to the tenths place
                                            Console.WriteLine($"The patient has a temperature of {roundTemp}°F, you take note of this as them having a fever");
                                            symptomListChecker(knownSymptoms, "Fever");
                                        }
                                        else //No fever
                                        {
                                            double temp = 97 + (99.0 - 97.0) * temperature.NextDouble();
                                            double roundTemp = Math.Round(temp, 1); //Rounds temp to the tenths place
                                            Console.WriteLine($"The patient has a tempurature of {roundTemp}°F, you take note of this as them not having a fever");
                                        }
                                        testsLeft--;
                                        break;
                                    }
                                    else if (userTest == "tongue depressor") //Detects Sore throat, mucus , swollen tonsils
                                    {
                                        amtofTestsDone++;
                                        Console.Clear();
                                        Console.WriteLine("You decide to use the tongue depressor on the patient, say 'ahh!'");
                                        bool isMucus = getSymptom(selectedIllness, "Mucus");
                                        bool isSoreThroat = getSymptom(selectedIllness, "Sore throat");
                                        bool isSwollenTonsils = getSymptom(selectedIllness, "Swollen tonsils");
                                        if (isSoreThroat)
                                        {
                                            Console.WriteLine("You use the tongue depressor on the patient, you see signs that indicate a sore throat.");
                                            symptomListChecker(knownSymptoms, "Sore throat");
                                        }
                                        if (isMucus)
                                        {
                                            Console.WriteLine("You use the tongue depressor on the patient, you see signs that indicate a Mucus build-up.");
                                            symptomListChecker(knownSymptoms, "Mucus");

                                        }
                                        if(isSwollenTonsils)
                                        {
                                            Console.WriteLine("You use the tongue depressor on the patient, you see that their tonsils are swollen.");
                                            symptomListChecker(knownSymptoms, "Swollen tonsils");
                                        }
                                        //If there were no symptoms shown:
                                        if (!isMucus && !isSoreThroat && !isSwollenTonsils)
                                        {
                                            Console.WriteLine("The patient's throat was normal");
                                        }
                                        testsLeft--;
                                        break;
                                    }
                                    else if (userTest == "breathing test") //Detects shortness of breath, and chest discomfort, use code as base if a tool tests for more than one symptom
                                    {
                                        amtofTestsDone++;
                                        Console.Clear();
                                        bool isShortBreath = getSymptom(selectedIllness, "Shortness of breath");
                                        bool isChestDiscomfort = getSymptom(selectedIllness, "Chest discomfort");
                                        Console.WriteLine("You have the patient take a couple of deep breaths to evaluate the depth of their breathing");
                                        if (isChestDiscomfort)
                                        {
                                            Console.WriteLine("While doing the test, the patient states their chest aches, you write this down.");
                                            symptomListChecker(knownSymptoms, "Chest discomfort");
                                        }
                                        if (isShortBreath)
                                        {
                                            Console.WriteLine("After the test, you note that the patient failed the breathing test, they have Shortness of Breath");
                                            symptomListChecker(knownSymptoms, "Shortness of breath");
                                        }

                                        //If there were no symptoms shown:
                                        if (!isChestDiscomfort && !isShortBreath)
                                        {
                                            Console.WriteLine("The patient passed the breathing test.");
                                        }
                                        testsLeft--;
                                        break;
                                    }
                                    else if (userTest == "endoscope") //Detects Runny Nose, Sneezing
                                    {
                                        amtofTestsDone++;
                                        Console.Clear();
                                        Console.WriteLine("You decide to use the endoscope to inspect the inside of the patient's nose.");
                                        bool isRunny = getSymptom(selectedIllness, "Runny nose");
                                        bool isSneezing = getSymptom(selectedIllness, "Sneezing");
                                        if (isRunny)
                                        {
                                            Console.WriteLine("You inspect the patients nostrils and see that they have a runny nose");
                                            symptomListChecker(knownSymptoms, "Runny nose");
                                        }
                                        if (isSneezing)
                                        {
                                            Console.WriteLine("While inspecting the patient's nose, he sneezes in the middle of the inspection, you note this as them sneezing");
                                            symptomListChecker(knownSymptoms, "Sneezing");
                                        }
                                        testsLeft--;
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Tool, here is the list of all the test tools:");
                                        for (int i = 0; i < testTools.Length; i++)
                                        {
                                            blueText(testTools[i]);
                                            Console.WriteLine(testDesc[i]);

                                        }
                                        userTest = Console.ReadLine().ToLower();
                                    }
                                    if (patientDone)
                                    {
                                        break;
                                    }
                                } while (underGoingTest);
                                if (patientDone)
                                {
                                    break;
                                }
                                //Have the user choose the tool and add if/elses for if they work or not with corresponding symptoms
                            }

                            else if (userInput == "conclude")
                            {
                                Console.Clear();
                                Console.WriteLine("Okay, what illness do you think the patient has? You only have one chance to guess! (if you accidently typed this command enter 'esc')");
                                string userGuess = Console.ReadLine().ToLower();
                                bool isConclude = true;
                                do
                                {
                                    Console.Clear();
                                    if (userGuess == "esc")
                                    {
                                        printCommands();
                                        userInput = Console.ReadLine().ToLower();
                                        isConclude = false;
                                        break;
                                    }
                                    else if (userGuess == selectedIllness.illName.ToLower())
                                    {
                                        
                                        Console.WriteLine($"Your diagnosis was correct! The patient has {selectedIllness.illName}! You saved someone and you should be happy about that!");
                                        Console.WriteLine("Do you want to continue diagnosing patients?\n'y' or 'n'");
                                        string userCont = Console.ReadLine().ToLower();
                                        bool isDeciding = true;
                                        do
                                        {
                                            Console.Clear();
                                            if (userCont == "y")
                                            {
                                                patientDone = true;
                                                isDeciding = false;
                                                isConclude = false;
                                                patientCuring = false;
                                                break;
                                            }
                                            else if (userCont == "n")
                                            {
                                                Console.WriteLine("Okay, well, thanks for playing!");
                                                
                                                playing = false;
                                                printGameStats(patientCount, amtofTestsDone);
                                                Environment.Exit(0); //Stops application
                                            }
                                            else
                                            {
                                                Console.WriteLine("ERROR: Input 'y' or 'n' for your choice to continue playing or not:");
                                                userCont = Console.ReadLine().ToLower();
                                            }
                                        } while (isDeciding);
                                        if (patientDone)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"That's incorrect! The correct illness was {selectedIllness.illName}! You lose your doctor license and are fired!");
                                        printGameStats(patientCount, amtofTestsDone);
                                        Environment.Exit(0); //Stops application
                                    }

                                } while (isConclude);
                                if (patientDone)
                                {
                                    break;
                                }
                                //Add code here for the esc command and so when the user enters the correct illness name, the game continues:
                            }

                            else if (userInput == "symptoms")
                            {
                                Console.Clear();
                                Console.WriteLine("Here are all of the symptoms discovered that the patient is showing:");
                                foreach (string symptom in knownSymptoms)
                                {
                                    Console.WriteLine(symptom);
                                }
                                printCommands();
                                userInput = Console.ReadLine().ToLower();

                            }
                            else if (userInput == "reference")
                            {
                                Console.Clear();
                                int illCount = 0;
                                foreach (var illness in illnessCatalouge) //Loops through all illnesses in the 2D array, every 3rd one it ask if they need to continue or not
                                {
                                    Console.WriteLine($"Illness:");
                                    blueText($"  {illness.illName}");
                                    Console.WriteLine("Symptoms:");

                                    // Using another foreach loop to iterate through the symptoms array
                                    foreach (var symptom in illness.symptomList)
                                    {
                                        Console.WriteLine($"- {symptom}");
                                    }
                                    illCount++;
                                    if (illCount % 3 == 0 || illCount == illnessCatalouge.Length) //Asks the user for every 3rd illness or last one printed, if they want to exit this or not
                                    {
                                        bool contorNot = true; //For flexibility so the user can take as much time as needed
                                        while (contorNot)
                                        {
                                            Console.WriteLine("Type 'cont' to continue, to exit enter 'esc'");
                                            string contLoop = Console.ReadLine().ToLower();
                                            if (contLoop == "esc")
                                            {
                                                Console.Clear();
                                                printCommands();
                                                userInput = Console.ReadLine().ToLower();
                                                contorNot = false;
                                                break;
                                            }
                                            else if (contLoop == "cont")
                                            {
                                                Console.Clear();
                                                contorNot = false;
                                                break;

                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }
                            }

                            else
                            {
                                Console.WriteLine("ERROR: Invalid command has been entered, enter again from this list: \n(cmds):");
                                userInput = Console.ReadLine().ToLower();
                            }
                        }
                        else //When you can do no more tests
                        {
                            Console.WriteLine("NO MORE TESTS CAN BE CONDUCTED!");
                        }

                    } while (!patientDone);
                }


            }
            //Method called to edit the values of the knownSymptom array
            static string[] symptomListChecker(string[] knownSymptoms, string addingSymptom)
            {

                for (int i = 0; i < knownSymptoms.Length; i++)
                {
                    if (knownSymptoms[i] == null)//If there is no symptom there:
                    {
                        knownSymptoms[i] = addingSymptom;
                        break;
                    }
                    else if (knownSymptoms[i] == addingSymptom)
                    {
                        break; //If the symptom is already on the list, stops the loop
                    }
                }
                return knownSymptoms;
            }
            static void printCommands()
            {
                Console.WriteLine("Here is the list of commands to use:\n");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("Test: Conduct a test on the patient to evaluate their symptoms.\n" +
                    "Conclude:If you are ready to finalize your decision on what sickness the patient has, use this command.\n" +
                    "Symptoms: Prints what symptoms you have discovered that the patient is showing.\n" +
                    "Reference: A list of illnesses with their accompying symptoms to help diagnose.");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            static void waitingDots() //QoL method to add the 3 loading dot-like printing. Used so the program doesnt feel too fast
            {
                Console.WriteLine("Conducting Test:");
                string dots = ".";
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(1250);
                    Console.Write(dots);
                }
                Thread.Sleep(1250);
                Console.Clear();

            }

            static bool getSymptom(Illnesses selectedIllness, string illnessToBeChecked) //Called to assign a t/f value to a bool if the patient has that symptom or not
                                                                                         //It does this by checking if the symptom is the same as the one being checked in the method
            {
                string[] noDotIllnesses = { "Sore throat", "Chest discomfort", "Sneezing", "Swollen tonsils" };
                bool isnoDot = false;
                for(int i = 0; i < noDotIllnesses.Length; i++) //Checks out of the symptoms that shouldnt show dots and if it is that symptom it doesnt show dots bc of the bool
                {
                    if (illnessToBeChecked == noDotIllnesses[i]) //Add test symptoms tested multiple times in one test here so it doesnt take too long to get results
                    {
                        isnoDot = true;
                        break;
                    }
                }
                foreach (string symptom in selectedIllness.symptomList) //Checks to see if the user has a fever symptom to act accordingly
                {
                    if (!isnoDot) //Add test symptoms tested multiple times in one test here so it doesnt take too long to get results
                    {
                        waitingDots();
                    }
                    if (symptom == illnessToBeChecked)
                    {
                        return true; //Must specify what value to return for bools
                    }
                }
                return false;
            }

            static void printGameStats(int patientsWorkedOn, int totalTests)
            {
                blueText("Amount of patients worked on:");
                Console.WriteLine(patientsWorkedOn);
                blueText("Total amount of tests conducted:");
                Console.WriteLine(totalTests);
            }
            static void blueText(string input) //Prints the background of the text to be blue
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(input);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }


    }
}
