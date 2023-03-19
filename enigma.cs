namespace EnigmaMachine {

    // Defines a class simulating the functionality of the German Enigma I and M3 cipher machines used by the German Army and Air Force during WW2.
    // Uses rotors 1-5 and reflectors A-C.
    class Enigma {

        private readonly Plugboard plugboard;
        private readonly Reflector reflector;
        private readonly List<Rotor> rotors_used;

        // Define initial variable values of an Enigma object
        public Enigma(List<int> settings_rotor_choices, List<string> settings_plugboard_pairings, List<char> settings_starting_rotor_pos, List<char> settings_ring, char settings_reflector) {

            plugboard = new Plugboard(settings_plugboard_pairings);
            reflector = new Reflector(settings_reflector);
            rotors_used = set_rotors(settings_rotor_choices, settings_starting_rotor_pos, settings_ring);
        }


        // enigma machine methods here

        // Set the starting positions of the Enigma rotors and determine which rotor outputs to use as well as their ring setting alteration based on user settings
        public List<Rotor> set_rotors(List<int> settings_rotor_choices, List<char> settings_starting_rotor_pos, List<char> settings_ring)
        {
            List<Rotor> rotor_outputs_to_use = new List<Rotor>();

            // configure each chosen rotor based on desired starting position and ring setting letter
            for (int i = 0; i < 3; i++)
            {
                Rotor new_rotor = new Rotor(settings_rotor_choices[i], settings_starting_rotor_pos[i], settings_ring[i]);
                rotor_outputs_to_use.Add(new_rotor);
            }

            return rotor_outputs_to_use;
        }

        // Check if more than 1 rotor needs to move
        // Then move right rotor forward 1 step
        public void advance_rotors()
        {
            bool middle_rotor_step = rotors_used[2].check_to_step_adjacent_rotor();
            bool left_rotor_step = rotors_used[1].check_to_step_adjacent_rotor();

            // step right rotor (always occurs)
            rotors_used[2].step_rotor();

            // step the middle rotor if possible
            if (middle_rotor_step) { rotors_used[1].step_rotor(); }

            // step the left rotor if possible
            if (left_rotor_step) { rotors_used[0].step_rotor(); }
        }

        // First set of letter substitutions from the input wheel to just before the reflector
        public char right_to_left_cipher(char letter, int prev_rotor_pos = 0, int curr_rotor_i = 0)
        {
            // Base case - when all rotors have been performed their ciphers
            if (curr_rotor_i < 0) { return letter; }

            // Find index of letter on input-side
            int input_letter_i = (int)letter - 65;

            // calculate index of output letter
            int curr_rotor_pos_i = rotors_used[curr_rotor_i].get_rotor_pos_i();
            int output_letter_i = (input_letter_i + curr_rotor_pos_i - prev_rotor_pos) % 26;
            char output_letter = rotors_used[curr_rotor_i].get_output_letter(0, output_letter_i);

            // recursive calls to go through each rotor to get the output letter to feed into reflector
            return right_to_left_cipher(output_letter, curr_rotor_pos_i, curr_rotor_i - 1);
        }

        // Second set of letter substitutions from the output of the reflector to the input wheel
        public char left_to_right_cipher(char letter, int prev_rotor_pos = 0, int curr_rotor_i = 0)
        {
            // Base case - when all rotors have been performed their ciphers
            // Now perform the cipher between right rotor and input wheel
            if (curr_rotor_i > 2)
            {
                // Find index of letter
                int output_input_letter_i = (int)letter - 65;

                // calculate index of input letter
                int output_letter_i = (output_input_letter_i - prev_rotor_pos) % 26;
                char output_letter = (char)(output_letter_i + 65);

                return output_letter;
            }

            // Find index of letter
            int input_letter_i = (int)letter - 65;

            // calculate index of input letter
            int curr_rotor_pos = rotors_used[curr_rotor_i].get_rotor_pos_i();
            int rev_output_i = (input_letter_i + curr_rotor_pos - prev_rotor_pos) % 26;
            char rev_output_letter = rotors_used[curr_rotor_i].get_output_letter(1, rev_output_i);

            // recursive calls to go through each rotor to get the output letter to feed into input wheel
            return left_to_right_cipher(rev_output_letter, curr_rotor_pos, curr_rotor_i + 1);
        }

        // Perform encryption/decryption on the input_text
        // The "main" method in an Enigma object
        public string encrypt_decrypt(string input_text)
        {
            string output_text = "";

            foreach (char letter in input_text)
            {
                // advance rotor
                advance_rotors();

                // 1st plugboard substitution
                char converted_letter = plugboard.plugboard_cipher(letter);

                // 1st pass substitution through rotors
                converted_letter = right_to_left_cipher(converted_letter);

                // reflector substitution
                converted_letter = reflector.reflector_cipher(converted_letter, rotors_used[0].get_rotor_pos_i());

                // 2nd pass substitution through rotors
                converted_letter = left_to_right_cipher(converted_letter);

                // 2nd plugboard substitution
                converted_letter = plugboard.plugboard_cipher(converted_letter);

                // add converted_letter to output_text
                output_text += converted_letter;
            }

            return output_text;
        }
    }

}
