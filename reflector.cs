namespace EnigmaMachine
{
    // Defines a class simulating the function of an Enigma reflector. The reflector acts like another rotor that takes in a letter as input from the left rotor and outputs the another letter back to the left rotor for another round of substitutions
    class Reflector
    {

        private readonly string reflector_chosen;

        // constructor
        // Set reflector variable to the desired reflector output pairings
        public Reflector(char settings_reflector)
        {
            var reflector_options = new Dictionary<char, string>()
            {
                {'A', "EJMZALYXVBWFCRQUONTSPIKHGD"},
                {'B', "YRUHQSLDPXNGOKMIEBFZCWVJAT"},
                {'C', "FVPJIAOYEDRZXWGCTKUQSBNMHL"}
            };

            reflector_chosen = reflector_options[settings_reflector];
        }

        // Return the reflector letter at the desired index
        private char get_reflector_letter_at_i(int i) { return reflector_chosen[i]; }

        // Perform substitution cipher of the output letter from the left rotor to the reflector
        public char reflector_cipher(char left_rotor_letter, int left_rotor_pos_i)
        {
            int input_letter_i = (int)left_rotor_letter - 65;
            int reflector_letter_i = Helpers.modulo((input_letter_i - left_rotor_pos_i), 26);
            char reflector_letter = get_reflector_letter_at_i(reflector_letter_i);

            return reflector_letter;
        }
    }
}
