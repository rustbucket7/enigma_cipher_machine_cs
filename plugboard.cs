namespace EnigmaMachine
{
    // Defines a class simulating the function of an Enigma plugboard. The plugboard swaps an input letter with its assigned pair when the Enigma machine was initially set up.
    class Plugboard {

        private readonly Dictionary<char, char> plugboard = new Dictionary<char, char>();

        // constructor
        // Sets up the plugboard based on received settings
        public Plugboard(List<string> settings_plugboard_pairings) {
            // fill plugboard dictionary with letter pairings
            foreach (string letter_pair in settings_plugboard_pairings) {
                char first_letter = letter_pair[0];
                char second_letter = letter_pair[1];

                plugboard[first_letter] = second_letter;
                plugboard[second_letter] = first_letter;
            }
        }

        // Perform substitution cipher of a letter going through the plugboard
        // If a letter was not assigned a cipher pair, return itself
        public char plugboard_cipher(char letter) {
            try {
                return plugboard[letter];
            }

            catch (KeyNotFoundException) {
                return letter;
            }
        }
    }
}
