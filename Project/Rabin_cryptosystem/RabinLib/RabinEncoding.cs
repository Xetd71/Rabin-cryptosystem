using System;
using System.Text;

namespace RabinLib
{
    class RabinEncoding : Encoding
    {
        byte[] encoding = new byte[11];
        public RabinEncoding() : this(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }) { }
        public RabinEncoding(byte[] newEncoding)
        {
            if (newEncoding.Length != 11)
                throw new ArgumentException("Неверная длина массива", "Длина массива должна быть равна 11");
            for (int i = 0; i < 11; i++)
                if (newEncoding[i] < 0 || newEncoding[i] > 12)
                    throw new ArgumentException("Неверные значения", "Один из элементов не принадлежит промежутку [0, 12]");
            for (int i = 0; i < 10; i++)
                for (int j = i + 1; j < 11; j++)
                    if (newEncoding[i] == newEncoding[j])
                        throw new ArgumentException("Неверные значения", "Один из элементов равен другому");
            newEncoding.CopyTo(encoding, 0);
        }

        /// <summary>
        /// Переводит символ в его числовое представление
        /// в кодировке RabinEncoding.
        /// </summary>
        /// <param name="simbol">переводимый символ</param>
        /// <returns>число, соответствующее символу</returns>
        public byte ToByte(char simbol)
        {
            if (simbol >= '0' && simbol <= '9')
                return encoding[byte.Parse(simbol.ToString())];
            if (simbol == ':')
                return encoding[10];
            if (simbol == '\n')
                return 13;
            if (simbol == '\r')
                return 14;
            throw new EncoderFallbackException("В кодировке нет такого значения");
        }
        /// <summary>
        /// Переводит число в символ, используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="number">переводимое число</param>
        /// <returns>символ, соответствующий числу</returns>
        public char ToChar(byte number)
        {
            if (number == 15)
                return '\0';
            for (int i = 0; i < 10; i++)
                if (encoding[i] == number)
                    return i.ToString()[0];
            if (encoding[10] == number)
                return ':';
            if (number == 13)
                return '\n';
            if (number == 14)
                return '\r';
            throw new DecoderFallbackException("В кодировке нет такого значения");
        }
        /// <summary>
        /// Переводит строку в массив байтов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="input">переводимая строка</param>
        /// <returns>декодированный массив байтов</returns>
        public byte[] ToByteArray(string input)
        {
            byte[] output = new byte[(int)(input.Length / 2.0 + 0.5)];
            int k = 0;
            for (int i = 1; i < input.Length; i += 2)
                output[k++] = (byte)(ToByte(input[i - 1]) * 16 + ToByte(input[i]));
            if (input.Length % 2 == 1)
                output[k] = (byte)(ToByte(input[input.Length - 1]) * 16 + 15);
            return output;
        }
        /// <summary>
        /// Переводит массив байтов в строку,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="input">получаемый массив байтов</param>
        /// <returns>полученная строка</returns>
        public string ToString(byte[] input)
        {
            string output = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                output += ToChar((byte)(input[i] / 16));
                output += ToChar((byte)(input[i] % 16));
            }
            return output;
        }
        /// <summary>
        /// Кодирует набор символов из указанного
        /// массива символов в указанный массив байтов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="chars">массив байтов</param>
        /// <param name="charIndex">начальный индекс</param>
        /// <param name="charCount">колличество символов</param>
        /// <param name="bytes">массив байтов, куда производится запись</param>
        /// <param name="byteIndex">начальный индекс в массиве байтов</param>
        /// <returns>длина записанных символов</returns>
        public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
        {
            char[] chunkOfChars = new char[charCount];
            Array.Copy(chars, charIndex, chunkOfChars, 0, charCount);
            string input = new string(chunkOfChars);
            byte[] chunkOfBytes = ToByteArray(input);
            int len = chunkOfBytes.Length;
            Array.Copy(chunkOfBytes, 0, bytes, byteIndex, len);
            return len;
        }
        /// <summary>
        /// Вычисляет количество символов, полученных при декодировании
        /// последовательности байтов из заданного массива байтов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="bytes">массив байтов</param>
        /// <param name="index">начальный индекс</param>
        /// <param name="count">колличество символов</param>
        /// <returns>колличество символов</returns>
        public override int GetCharCount(byte[] bytes, int index, int count)
        {
            int sum = 0;
            for (int i = index; i < count + index; i++)
                if (bytes[i] % 16 == 15)
                    sum++;
            return sum;
        }
        /// <summary>
        /// Вычисляет количество байтов, полученных
        /// при кодировании набора символов из указанного массива символов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="chars">массив символов</param>
        /// <param name="index">стартовый индекс</param>
        /// <param name="count">колличество кодируемых символов</param>
        /// <returns>колличество байтов</returns>
        public override int GetByteCount(char[] chars, int index, int count)
        {
            return count * 2;
        }
        /// <summary>
        /// Декодирует последовательность байтов
        /// из указанного массива байтов в указанный массив символов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="bytes">кодируемый массив байтов</param>
        /// <param name="byteIndex">начальный индекс</param>
        /// <param name="byteCount">колличество символов</param>
        /// <param name="chars">массив символов</param>
        /// <param name="charIndex">начальный индекс для массива chars</param>
        /// <returns>колличество символов</returns>
        public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
        {
            byte[] chunkOfBytes = new byte[byteCount];
            Array.Copy(bytes, byteIndex, chunkOfBytes, 0, byteCount);
            string output = ToString(chunkOfBytes);
            int len = output.Length;
            char[] chunkOfChars = output.ToCharArray();
            Array.Copy(chunkOfChars, 0, chars, charIndex, len);
            return len;
        }
        /// <summary>
        /// Вычисляет максимальное количество байтов,
        /// полученных при кодировании заданного количества символов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="charCount">колличество символов</param>
        /// <returns>колличество байтов</returns>
        public override int GetMaxByteCount(int charCount)
        {
            return (int)(charCount / 2.0 + 0.5);
        }
        /// <summary>
        /// Вычисляет максимальное количество символов,
        /// полученных при декодировании заданного количества байтов,
        /// используя кодировку RabinEncoding.
        /// </summary>
        /// <param name="byteCount">колличество байтов</param>
        /// <returns>колличество символов</returns>
        public override int GetMaxCharCount(int byteCount)
        {
            return byteCount * 2;
        }
    }
}
