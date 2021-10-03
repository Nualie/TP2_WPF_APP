using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var toDecrypt = ConvertCheckBox.IsChecked ?? false;
            var inputText = InputTextBox.Text;
            var encryptionmethod = EncryptionComboBox.Text;

            if (toDecrypt)
            {
                OutputTextBox.Text = $"{inputText} is gibberish and should be decrypted using {encryptionmethod}";
            }
            else
            {
                OutputTextBox.Text = $"{inputText} was written as an input to be encrypted using {encryptionmethod}";
            }

            if (encryptionmethod == "Caesar")
            {
                OutputTextBox.Text = Caesar.Code(inputText, toDecrypt);
            }

            if (encryptionmethod == "Binary")
            {
                OutputTextBox.Text = Binary.Code(inputText, toDecrypt);
            }

            if (encryptionmethod == "Letter Number")
            {
                OutputTextBox.Text = LetterNumber.Code(inputText, toDecrypt);
            }
        }
    }

    internal static class Binary
    {
        public static string Code(string inputText, bool toDecrypt)
        {
            return toDecrypt ? Decrypt(inputText) : Encrypt(inputText);
        }


        private static string Encrypt(string inputText)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in inputText.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        private static string Decrypt(string inputText)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < inputText.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(inputText.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }
    }
}
internal static class LetterNumber
    {
        public static string Code(string inputText, bool toDecrypt)
        {
            return toDecrypt ? Decrypt(inputText) : Encrypt(inputText);
        }


        public static string cipher(char ch)
        {
            if (!char.IsLetter(ch))
            {
                return ch.ToString();
            }
            int index = (int)ch % 32;
            return index.ToString();

        }

        public static string decipher(int i)
        {
            if (i<0 || i>25)
            {
                return i.ToString();
            }
            char letter = ((char)(i+64));
            return letter.ToString();

        }

        private static string Encrypt(string inputText)
        {
            string output = string.Empty;
            foreach (char ch in inputText)
                output += cipher(ch)+" ";

            return output;
        }

        private static string Decrypt(string inputText)
        {
            string output = string.Empty;
            string[] numbers= inputText.Split(" ");
            bool success;
            foreach (string s in numbers)
            {
                success = int.TryParse(s, out int i);
                output += success ? decipher(i) : s;
            }
                

            return output;
        }
    }


internal static class Caesar
    {
        public static string Code(string inputText, bool toDecrypt)
        {
            return toDecrypt ? Decrypt(inputText) : Encrypt(inputText);
        }


        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }

        private static string Encrypt(string inputText)
        {
            string output = string.Empty;
            int key = 3;
            foreach (char ch in inputText)
                output += cipher(ch, key);

            return output;
        }

        private static string Decrypt(string inputText)
        {
            string output = string.Empty;
            int key = -3;
            foreach (char ch in inputText)
                output += cipher(ch, key);

            return output;
        }
}
