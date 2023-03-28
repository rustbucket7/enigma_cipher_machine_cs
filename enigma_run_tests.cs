using Xunit;
using EnigmaMachine;

/*
Tests enigma_run(...), the driving method of the whole program, in main.cs


Message Sources

Encrypted messages found here:
http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Messages

Decrypted messages found here:
http://wiki.franklinheath.co.uk/index.php/Enigma/Sample_Decrypts
*/

namespace EnigmaTests
{

    
    public class enigma_run_tests
    {
        /*
        Enigma Instruction Manual, 1930

        This message is taken from a German army instruction manual for the Enigma I (interoperable with the later navy machine, Enigma M3).
        */
        [Fact]
        public void test_msg1_decrypt()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AM", "FI", "NV", "PS", "TU", "WZ" };
            List<char> initial_rotor_settings = new List<char>(3) { 'A', 'B', 'L' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 24, 13, 22 });
            char reflector = 'A';
            string encrypted_msg = "GCDSEAHUGWTQGRKVLFGXUCALXVYMIGMMNMFDXTGNVHVRMMEVOUYFZSLRHDRRXFJWCFHUHMUNZEFRDISIKBGPMYVXUZ";
            string decrypted_msg = "FEINDLIQEINFANTERIEKOLONNEBEOBAQTETXANFANGSUEDAUSGANGBAERWALDEXENDEDREIKMOSTWAERTSNEUSTADT";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), decrypted_msg);
        }

        /*
        Operation Barbarossa, 1941 Part 1

        Sent from the Russian front on 7th July 1941. The message is in two parts:
        */
        [Fact]
        public void test_msg2_decrypt()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BS", "CG", "DL", "FU", "HZ", "IN", "KM", "OW", "RX" };
            List<char> initial_rotor_settings = new List<char>(3) { 'B', 'L', 'A' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = "EDPUDNRGYSZRCXNUYTPOMRMBOFKTBZREZKMLXLVEFGUEYSIOZVEQMIKUBPMMYLKLTTDEISMDICAGYKUACTCDOMOHWXMUUIAUBSTSLRNBZSZWNRFXWFYSSXJZVIJHIDISHPRKLKAYUPADTXQSPINQMATLPIFSVKDASCTACDPBOPVHJK";
            string decrypted_msg = "AUFKLXABTEILUNGXVONXKURTINOWAXKURTINOWAXNORDWESTLXSEBEZXSEBEZXUAFFLIEGERSTRASZERIQTUNGXDUBROWKIXDUBROWKIXOPOTSCHKAXOPOTSCHKAXUMXEINSAQTDREINULLXUHRANGETRETENXANGRIFFXINFXRGTX";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), decrypted_msg);
        }

        /*
        Operation Barbarossa, 1941 Part 2

        Sent from the Russian front on 7th July 1941. The message is in two parts:
        */
        [Fact]
        public void test_msg3_decrypt()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BS", "CG", "DL", "FU", "HZ", "IN", "KM", "OW", "RX" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = "SFBWDNJUSEGQOBHKRTAREEZMWKPPRBXOHDROEQGBBGTQVPGVKBVVGBIMHUSZYDAJQIROAXSSSNREHYGGRPISEZBOVMQIEMMZCYSGQDGRERVBILEKXYQIRGIRQNRDNVRXCYYTNJR";
            string decrypted_msg = "DREIGEHTLANGSAMABERSIQERVORWAERTSXEINSSIEBENNULLSEQSXUHRXROEMXEINSXINFRGTXDREIXAUFFLIEGERSTRASZEMITANFANGXEINSSEQSXKMXKMXOSTWXKAMENECXK";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), decrypted_msg);
        }

        [Fact]
        // Bad input string (empty string)
        public void test_msg4_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = "";
            string error_msg = "Bad input string. Letters only.";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (only empty spaces)
        public void test_msg4a_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = "  ";
            string error_msg = "Bad input string. Letters only.";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (not strictly using letters)
        public void test_msg4b_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = "12A@#H";
            string error_msg = "Bad input string. Letters only.";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (not strictly using letters)
        public void test_msg4c_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 4, 5 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'B';
            string encrypted_msg = " A   2   ^^^";
            string error_msg = "Bad input string. Letters only.";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (missing rotor)
        public void test_msg5_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (too many rotors)
        public void test_msg5a_bad_input()
        {
            List<int> rotor_choices = new List<int>(4) { 2, 1, 3, 4 };
            List<string> plugboard_pairings = new List<string>() { "AV" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (duplicate plugboard letter Z)
        public void test_msg6_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3};
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ", "ZF"};
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (missing initial rotor setting)
        public void test_msg7_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (too many initial rotor setting)
        public void test_msg7a_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ" };
            List<char> initial_rotor_settings = new List<char>(4) { 'L', 'S', 'D', 'A' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (missing ring setting)
        public void test_msg8_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (too many ring setting)
        public void test_msg8a_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12, 1 });
            char reflector = 'A';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }

        [Fact]
        // Bad input string (incorrect reflector choice)
        public void test_msg9_bad_input()
        {
            List<int> rotor_choices = new List<int>(3) { 2, 1, 3 };
            List<string> plugboard_pairings = new List<string>() { "AV", "BZ" };
            List<char> initial_rotor_settings = new List<char>(3) { 'L', 'S', 'D' };
            List<char> ring_settings = Helpers.list_int_to_list_char(new List<int> { 2, 21, 12 });
            char reflector = 'Z';
            string encrypted_msg = "aaa";
            string error_msg = "Bad Enigma settings";

            Assert.Equal(EnigmaRun.enigma_run(rotor_choices, plugboard_pairings, initial_rotor_settings, ring_settings, reflector, encrypted_msg), error_msg);
        }
    }

}
