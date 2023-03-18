namespace EnigmaMachine
{
    // Defines a class simulating the function of an Enigma rotor. Takes in a letter as input and outputs another letter to fed into another rotor, the reflector, or the input wheel.
    class Rotor
    {
        private char curr_rotor_pos_letter;
        private RotorOutputs<string, string, char> rotor_outputs;

        // constructor
        // Set the starting positions of the Enigma rotors and determine which rotor outputs to use as well as their ring setting alteration based on user settings.
        public Rotor(int rotor_chosen, char starting_rotor_pos_letter, char ring_setting_letter)
        {
            // rotor 1 to 5's output pairings organized as:
            // regular output (R-L), reverse output (L-R), turnover point
            var rotor_options = new Dictionary<int, RotorOutputs<string, string, char>>()
            {
                {1, new RotorOutputs<string, string, char>("EKMFLGDQVZNTOWYHXUSPAIBRCJ", "UWYGADFPVZBECKMTHXSLRINQOJ", 'Q')},
                {2, new RotorOutputs<string, string, char>("AJDKSIRUXBLHWTMCQGZNPYFVOE", "AJPCZWRLFBDKOTYUQGENHXMIVS", 'E')},
                {3, new RotorOutputs<string, string, char>("BDFHJLCPRTXVZNYEIWGAKMUSQO", "TAGBPCSDQEUFVNZHYIXJWLRKOM", 'V')},
                {4, new RotorOutputs<string, string, char>("ESOVPZJAYQUIRHXLNFTGKDCMWB", "HZWVARTNLGUPXQCEJMBSKDYOIF", 'J')},
                {5, new RotorOutputs<string, string, char>("VZBRGITYUPSDNHLXAWMJQOFECK", "QCYLXWENFTZOSMVJUDKGIARPHB", 'Z')},
            };

            // set the rotor to the appropriate starting position and select the rotor output strings
            curr_rotor_pos_letter = starting_rotor_pos_letter;
            rotor_outputs = rotor_options[rotor_chosen];

            // rewire both output and reverse output of the rotor based on ring setting letter
            rotor_outputs.set_reg_output(ring_setting_rewiring(rotor_outputs.get_reg_output(), ring_setting_letter));
            rotor_outputs.set_rev_output(ring_setting_rewiring(rotor_outputs.get_rev_output(), ring_setting_letter));
        }

        // Alters a rotor's output pairings based on desired ring setting letter
        public string ring_setting_rewiring(string rotor_output_string, char ring_letter)
        {
            int ring_letter_i = (int)ring_letter - 65;
            int dot_position = (rotor_output_string.IndexOf('A') + ring_letter_i) % 26;
            string shifted_str = "";

            // shift up each letter in rotor_output_str
            for (int i = 0; i < rotor_output_string.Length; i++)
            {
                int letter_i = (int)rotor_output_string[i] - 65;
                shifted_str += (char)(((letter_i + ring_letter_i) % 26) + 65);
            }

            // rotate letters in shifted rotor_output_str until ring_letter is in the index position of dot_position
            string rotated_str = shifted_str;
            while (rotated_str[dot_position] != ring_letter)
            {
                char last_letter = rotated_str[rotated_str.Length - 1];
                string all_other_letters = rotated_str.Substring(0, rotated_str.Length - 1);
                rotated_str = last_letter + all_other_letters;
            }

            return rotated_str;
        }

        // Outputs a letter, from the regular or reserve output strings, at the desired index
        public char get_output_letter(int reg0_or_rev1, int output_letter_i)
        {
            if (reg0_or_rev1 == 0) { return rotor_outputs.get_reg_output_letter(output_letter_i); }
            else { return rotor_outputs.get_rev_output_letter(output_letter_i); }
        }

        // Output the rotor's current letter position by its ordinal value (ex. 'A' is 0, 'Z' is 25)
        public int get_rotor_pos_i() { return (int)curr_rotor_pos_letter - 65; }

        // Returns a bool value based on whether stepping up the rotor will cause the adjacent rotor to step up as well using the rotor's "turnover point"
        public bool check_to_step_adjacent_rotor() { return curr_rotor_pos_letter == rotor_outputs.get_turnover_point(); }

        // Turn the rotor forward 1 step/position
        public void step_rotor()
        {
            int curr_rotor_pos_i = (get_rotor_pos_i() + 1) % 26;
            curr_rotor_pos_letter = (char)(curr_rotor_pos_i + 65);
        }

        // helper class to allow a List-like objects within Rotor class to have 3 arguments
        internal class RotorOutputs<T1, T2, T3>
        {
            private string reg_output;
            private string rev_output;
            private char turnover_point;

            public RotorOutputs(string reg_output, string rev_output, char turnover_point)
            {
                this.reg_output = reg_output;
                this.rev_output = rev_output;
                this.turnover_point = turnover_point;
            }

            // get, set methods for reg_output
            public char get_reg_output_letter(int output_letter_i) { return reg_output[output_letter_i]; }
            public string get_reg_output() { return reg_output; }
            public void set_reg_output(string new_reg_output) { reg_output = new_reg_output; }

            // get, set methods for rev_output
            public char get_rev_output_letter(int output_letter_i) { return rev_output[output_letter_i]; }
            public string get_rev_output() { return rev_output; }
            public void set_rev_output(string new_rev_output) { rev_output = new_rev_output; }

            // get method for turnover_point
            public char get_turnover_point() { return turnover_point; }
        }
    }
}
