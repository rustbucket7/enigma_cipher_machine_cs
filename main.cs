using EnigmaMachine;

class EnigmaRun
{
    // Main method for when running the program through an IDE or command line
    static public void Main()
    {
        List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
        List<string> plugboard_pairings = new List<string>() { "AV", "BS", "CG", "DL", "FU", "HZ", "IN", "KM", "OW", "RX" };
        List<char> initial_rotor_settings = new List<char>(3) { 'B', 'L', 'A' };
        List<char> ring_settings = new List<char>(3) { 'B', 'U', 'L' };
        char reflector = 'B';

        Console.WriteLine("Enter a string of letters and spaces only: ");
        string user_input = Console.ReadLine();

        string output_text = enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, user_input);
        Console.WriteLine("Output text is: " + output_text);
    }

    /*
    enigma_run(...) is the program's driving function:

    1) Check if input string is valid
    2) Send input string to sanitize_input_text()
    3) Check if Enigma settings are valid
    4) Finalize formatting of Enigma settings
    5) Initialize an Enigma machine
    6) Perform encrypt_decrypt() on sanitized text
    7) Output the ciphertext
    */
    static public string enigma_run(List<int> rotor_choices, List<string> plugboard_pairings, List<char> initial_rotor_settings, List<char> ring_settings, char reflector, string input_str)
    {
        Enigma enigma_machine;

        // check if input_str is valid, if it is invalid, return a message saying input is bad
        string text = sanitize_input_text(input_str);

        if (string.IsNullOrEmpty(text)) { return "Bad input string. Letters only."; }

        // check if Enigma settings are valid
        // if they are, initialize an enigma_machine
        if (sanitize_enigma_settings(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector))
        {
            // finalize formatting of Enigma settings
            for (int i = 0; i < plugboard_pairings.Count; i++) { plugboard_pairings[i] = plugboard_pairings[i].ToUpper(); }

            // uppercase the reflector letter
            reflector = char.ToUpper(reflector);

            enigma_machine = new Enigma(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector);
        }

        // otherwise, we have bad Enigma settings
        else { return "Bad Enigma settings"; }

        // finally, run enigma_machine, return encrypted/decrypted text
        return enigma_machine.encrypt_decrypt(text);
    }

    // User input sanitization and check
    // Check input_text for invalid characters and only output uppercase letters without any empty spaces
    static private string sanitize_input_text(string input_text)
    {
        // if no input_text received, return empty string (false)
        if (string.IsNullOrEmpty(input_text)) { return ""; }

        // remove all spaces from input_text
        input_text = string.Join("", input_text.Split());  // ************ may need to use regex to get rid of all whitespace...

        // if after removing all empty spaces, there is no message left, return empty string (false)
        if (string.IsNullOrEmpty(input_text)) { return ""; }

        string sanitized_text = "";

        // check every character in input_text for any invalid characters
        foreach (char letter in input_text)
        {
            // if an incorrect character is found, return empty string (false)
            if (!Helpers.char_is_alpha(letter)) { return ""; }

            // if not a bad character, capitalize it and add it to sanitized_text
            sanitized_text += char.ToUpper(letter);
        }

        // if no bad characters found, return sanitized_text
        return sanitized_text;
    }

    // Enigma settings checks
    // Check if Enigma settings are valid
    static private bool sanitize_enigma_settings(List<int> rotor_choices, List<string> plugboard_pairings, List<char> initial_rotor_settings, List<char> ring_settings, char reflector)
    {
        // check rotor_choices
        if (!check_rotor_choices(rotor_choices)) { 
            Console.WriteLine("Bad rotor_choices");  // for debugging...
            return false; }

        // check plugboard_pairings
        else if (!check_plugboard_pairings(plugboard_pairings)) {
            Console.WriteLine("Bad plugboard_pairings");  // for debugging...
            return false; }

        // check initial_rotor_settings
        else if (!check_rotor_ring_settings(initial_rotor_settings)) { 
            Console.WriteLine("Bad initial_rotor_settings");  // for debugging...
            return false; }

        // check ring_settings
        else if (!check_rotor_ring_settings(ring_settings)) { 
            Console.WriteLine("Bad ring_settings");  // for debugging...
            return false; }

        // check reflector
        else if (!check_reflector(reflector)) { 
            Console.WriteLine("Bad reflector");  // for debugging...
            return false; }

        // if no checks failed, then Enigma settings must be correct, return true
        return true;
    }

    // Check if rotor_choices are valid
    static private bool check_rotor_choices(List<int> rotor_choices)
    {
        // if 3 rotors were not chosen, return false
        if (rotor_choices.Count != 3) { return false; }

        // check for repeated rotors (e.g. rotors_chosen are 1,1,3)
        HashSet<int> rotors_used = new HashSet<int>();
        foreach (int rotor in rotor_choices)
        {
            // if rotor is not numbered 1-5, return false
            if (rotor < 1 || rotor > 5) { return false; }

            // else if a rotor repeats, return false
            else if (rotors_used.Contains(rotor)) { return false; }

            else { rotors_used.Add(rotor); }
        }

        // if rotor_choices are all valid, return true
        return true;
    }

    // Check if plugboard_pairings are valid
    static private bool check_plugboard_pairings(List<string> plugboard_pairings)
    {
        Dictionary<char, char> plugboard_pairings_checker = new Dictionary<char, char>();

        foreach (string pairing in plugboard_pairings)
        {
            // if pairing letters are not a letter, return false
            if (!Helpers.char_is_alpha(pairing[0]) && !Helpers.char_is_alpha(pairing[1])) { return false; }

            // if both letters in the pairing are the same, return False
            else if (pairing[0] == pairing[1]) { return false; }

            // if either pairing letter has not been seen yet, add it to plugboard_pairings_checker
            // otherwise, return false
            if (!plugboard_pairings_checker.ContainsKey(pairing[0]) && !plugboard_pairings_checker.ContainsKey(pairing[1]))
            {
                plugboard_pairings_checker.Add(pairing[0], pairing[1]);
                plugboard_pairings_checker.Add(pairing[1], pairing[0]);
            }

            else { return false; }
        }

        // if plugboard_pairings are all valid, return true
        return true;
    }

    // Check if rotor or ring settings are valid
    // Also convert them from an int to their corresponding ASCII letters if needed (this functionality TBC)
    static private bool check_rotor_ring_settings(List<char> rotor_ring_settings)
    {
        // if 3 rotor or rings were not set, return false
        if (rotor_ring_settings.Count != 3) { return false; }

        // check each element to see if it is a valid element, and convert any int to their ASCII letters
        foreach (char rotor_ring_el in rotor_ring_settings)
        {
            // check if it's a letter
            // if not, return false
            if (!Helpers.char_is_alpha(rotor_ring_el)) { return false; }

            // else if rotor_ring_el is an int and between 1 - 26, convert it to a capital letter (TBC)
        }

        // at this point, nothing invalid found with rotor or ring settings, return true
        return true;
    }

    // Check if reflector is valid
    static private bool check_reflector(char reflector)
    {
        // if reflector is not 'A', 'B', or 'C', return false
        // otherwise, reflector is valid, return true
        if (!((int)reflector >= 65 && (int)reflector <= 67)) { return false; }
        else { return true; }
    }


}