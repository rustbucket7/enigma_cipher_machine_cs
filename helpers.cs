namespace EnigmaMachine
{
    class Helpers
    {
        // modulo operator as C# doesn't have one (sufficient for Enigma machine, but not generalized yet)
        static public int modulo(int dividend, int mod_divisor)
        {
            // if dividend is positive, return the remainder
            if (dividend >= 0) { return dividend % mod_divisor; }

            // else, if dividend is negative, return mod_divisor + dividend
            else { return mod_divisor + dividend; }
        }

        // check if input character's ASCII value is between 65-90 (A-Z) or 97-122 (a-z) (inclusive)
        static public bool char_is_alpha(char character)
        {
            if (((int)character >= 65 && (int)character <= 90) || ((int)character >= 97 && (int)character <= 122)) { return true; }
            else { return false; }
        }
    }
}
