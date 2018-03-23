using System;
using System.Linq;
using System.Numerics;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RabinLib
{
    public class Rabin
    {
        private BigInteger _p, _q, _n;
        private int _bitLength;
        private const int CheckByteCount = 13;
        static Random rnd = new Random();
        /// <summary>
        /// Случайным образом инициализирует закрытые ключи,
        /// открытый ключ и находит получившуюся длину в битах 
        /// открытого ключа
        /// </summary>
        /// <param name="bitLength">Длина открытого ключа в битах</param>
        public Rabin(int bitLength)
        {
            GeneratePrivateKeys(bitLength);
            GeneratePublicKey();
        }
        public Rabin(int bitLength, IProgress<ProgressInfo> progress)
        {
            GeneratePrivateKeys(bitLength, progress);
            GeneratePublicKey();
        } 
        /// <summary>
        /// Инициализирует поля N и Length
        /// </summary>
        /// <param name="n">Открытый ключ</param>
        public Rabin(BigInteger n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException("Отрицательное значение открытого ключа", "Открытый ключ должен быть положителен");
            _n = n;
            GetBitLength();
        }
        /// <summary>
        /// Создает шифровальщик с 1-ым и 2-ым закрытыми ключами
        /// </summary>
        /// <param name="p">1-ый закрытый ключ</param>
        /// <param name="q">2-ой закрытый ключ</param>
        public Rabin(BigInteger p, BigInteger q)
        {
            if (p < 0 || q < 0)
                throw new ArgumentOutOfRangeException("Отрицательное значение закрытого ключа", "Открытые ключи должен быть положителен");
            if (p == q)
                throw new ArgumentException("p == q", "Закрытые ключи не могут быть равны между собой");
            _p = p;
            _q = q;
            GeneratePublicKey();
        }
        /// <summary>
        /// Считывает из файла открытый или закрытые ключ
        /// </summary>
        /// <param name="keyPath">Путь к файлу с ключами</param>
        public Rabin(string keyPath)
        {
            if (Path.GetExtension(keyPath).CompareTo(".publicKey") == 0)
            {
                _n = ReadPublicKey(keyPath);
            }
            else
            {
                BigInteger[] pq = ReadPrivateKey(keyPath);
                _p = pq[0];
                _q = pq[1];
                GeneratePublicKey();
            }
            GetBitLength();
        }
        /// <summary>
        /// Возвращает или задает объект, содержащий данные об объекте.
        /// </summary>
        public object Tag { get; set; }
        private void GeneratePrivateKeys(int bitLength, IProgress<ProgressInfo> progress = null)
        {
            if (bitLength <= 22)
                throw new ArgumentOutOfRangeException("Длина открытого ключа слишком мала", "Выберите лисло с бинарной длиной большее 22");
            int len;
            do
            {
                progress?.Report(new ProgressInfo(0, bitLength * 20));
                len = bitLength / 2 - rnd.Next((int)(bitLength * 0.05), (int)(bitLength * 0.1));
                _p = BlumPrime(bitLength - len);
                progress?.Report(new ProgressInfo(bitLength * 10, bitLength * 20));
                _q = BlumPrime(len);
                progress?.Report(new ProgressInfo(bitLength * 20, bitLength * 20));
            } while (_p == _q);
        }
        private void GeneratePublicKey()
        {
            _n = _p * _q;
            GetBitLength();
        }
        private void GetBitLength()
        {
            _bitLength = (int)(BigInteger.Log(_n) / BigInteger.Log(2));
        }
        /// <summary>
        /// Возвращает массив BigInteger, который содержит 2 закрытых ключа 
        /// </summary>
        public BigInteger[] PQ
        {
            get { return new BigInteger[] { _p, _q }; }
        }
        /// <summary>
        /// Возвращает открытый ключ
        /// </summary>
        public BigInteger N
        {
            get { return _n; }
        }
        /// <summary>
        /// Возвращает бинарную длину открытого ключа
        /// </summary>
        public int BitLength
        {
            get { return _bitLength; }
        }
        /// <summary>
        /// Генерируем число Блюма
        /// </summary>
        /// <param name="bitLength">длина числа</param>
        /// <returns>само число</returns>
        private static BigInteger BlumPrime(int bitLength)
        {
            Mono.Math.BigInteger p;
            do
            {
                p = Mono.Math.BigInteger.GeneratePseudoPrime(bitLength);
            } while (!Mono.Math.BigInteger.Equals(p % 4, 3));
            byte[] p_array = p.GetBytes().Reverse().ToArray();
            if (p > 0 && (p_array[p_array.Length - 1] & 0x80) > 0)
            {
                byte[] temp = new byte[p_array.Length];
                Array.Copy(p_array, temp, p_array.Length);
                p_array = new byte[temp.Length + 1];
                Array.Copy(temp, p_array, temp.Length);
            }
            BigInteger q = new BigInteger(p_array);
            return q;
        }
        /// <summary>
        /// Дешифрует сообщение 
        /// </summary>
        /// <param name="c">Шифротекст</param>
        /// <returns>массив BigInteger, который содержит 4 корня</returns>
        public BigInteger[] Decrypt(BigInteger c)
        {
            if (c > _n)
                throw new ArgumentException("c","Длина шифротекста превосходит длину открытого ключа");
            BigInteger r = BigInteger.ModPow(c, (_p + 1) / 4, _p);
            BigInteger s = BigInteger.ModPow(c, (_q + 1) / 4, _q);

            BigInteger a, b;
            Gcd(_p, _q, out a, out b);

            //(a*p*r + b*q*s) % n

            BigInteger m1 = (a * _p * s + b * _q * r) % _n;
            if (m1 < 0)
                m1 += _n;
            BigInteger m2 = _n - m1;
            BigInteger m3 = (a * _p * s - b * _q * r) % _n;
            if (m3 < 0)
                m3 += _n;
            BigInteger m4 = _n - m3;
            return new BigInteger[] { m1, m2, m3, m4 };
        }
        /// <summary>
        /// Находит коэффиценты расширенного алгоритма Эвклида
        /// </summary>
        /// <param name="a">Первой число</param>
        /// <param name="b">Второе число</param>
        /// <param name="x">Первый коэффициент</param>
        /// <param name="y">Второй коэффициент</param>
        /// <returns>НОД a и b</returns>
        private static BigInteger Gcd(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            BigInteger x1, y1;
            BigInteger d = Gcd(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
        /// <summary>
        /// Шифрует сообщение
        /// </summary>
        /// <param name="m">сообщение</param>
        /// <returns>шифротекст</returns>
        public BigInteger Encrypt(BigInteger m)
        {
            if (m > _n)
                throw new ArgumentException("m", "Длина сообщения превосходит длину открытого ключа");
            BigInteger с = BigInteger.ModPow(m, 2, _n);
            return с;
        }
        /// <summary>
        /// Переводит строку в число с кодировкой Unicode
        /// </summary>
        /// <param name="str">строка</param>
        /// <returns>число</returns>
        public BigInteger ToBigInteger(string str)
        {
            byte[] temp = Encoding.Unicode.GetBytes(str);
            byte[] array = new byte[temp.Length + 1];
            Array.Copy(temp, array, temp.Length);
            array[array.Length - 1] = 1;
            BigInteger m = new BigInteger(array);
            return m;
        }
        /// <summary>
        /// Переводит число в строку с кодировкой Unicode
        /// </summary>
        /// <param name="m">число</param>
        /// <returns>строка</returns>
        public string ToString(BigInteger m)
        {
            byte[] temp = m.ToByteArray();
            byte[] array = new byte[temp.Length - 1];
            Array.Copy(temp, array, temp.Length - 1);
            return Encoding.Unicode.GetString(array);
        }
        /// <summary>
        /// Сохраняет открытый и закрытый ключи в указанное место на диске.
        /// </summary>
        /// <param name="keysPath">место, куда производится запись</param>
        public void KeyToFile(string keysPath)
        {
            CreateOrCreateNewFolder(keysPath);
            WritePublicKey(keysPath + "\\"  + Path.GetFileName(keysPath) + ".publicKey");
            WritePrivateKeys(keysPath + "\\" + Path.GetFileName(keysPath) + ".privateKey");
        }
        /// <summary>
        /// Записывает открытый ключ в указанный файл в 
        /// кодировке RabinEncoding.
        /// </summary>
        /// <param name="publicKeyPath">место, куда производится запись</param>
        public void WritePublicKey(string publicKeyPath)
        {
            if (Path.GetExtension(publicKeyPath).CompareTo(".publicKey") != 0)
                throw new ArgumentException("Неверное расширение");
            using (StreamWriter sw = new StreamWriter(new FileStream(publicKeyPath, FileMode.Create), new RabinEncoding()))
                sw.Write(N.ToString());
        }
        /// <summary>
        /// Записывает закрытый ключ в указанный файл в 
        /// кодировке RabinEncoding.
        /// </summary>
        /// <param name="privateKeyPath">место, куда производится запись</param>
        public void WritePrivateKeys(string privateKeyPath)
        {
            if (Path.GetExtension(privateKeyPath).CompareTo(".privateKey") != 0)
                throw new ArgumentException("Неверное расширение");
            using (StreamWriter sw = new StreamWriter(new FileStream(privateKeyPath, FileMode.Create), new RabinEncoding()))
                sw.WriteLine(_p.ToString() + "\r\n" + _q.ToString());
        }
        /// <summary>
        /// Создает новую папку в указанном месте с указанным названием
        /// </summary>
        /// <param name="folderPath">указанное место</param>
        private void CreateOrCreateNewFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            else
            {
                int i = 1;
                while (Directory.Exists(folderPath + "_" + i))
                    i++;
                folderPath += "_" + i;
                Directory.CreateDirectory(folderPath);
            }
        }
        /// <summary>
        /// Считывает из указанного файла открытый ключ и 
        /// возвращает его численное значение
        /// </summary>
        /// <param name="publicKeyPath">Путь к файлу</param>
        /// <returns>Численное значение открытого числа</returns>
        public static BigInteger ReadPublicKey(string publicKeyPath)
        {
            if (Path.GetExtension(publicKeyPath).CompareTo(".publicKey") != 0)
                throw new ArgumentException("Неверное расширение");
            using (StreamReader sr = new StreamReader(new FileStream(publicKeyPath, FileMode.Open), new RabinEncoding()))
            {
                return BigInteger.Parse(sr.ReadLine());
            }
        }
        /// <summary>
        /// Считывает из указанного файла закрытые ключи и
        /// возвращает массив с их численными значениями
        /// </summary>
        /// <param name="privateKeyPath>Путь к файлу</param>
        /// <returns>Массив с закрытыми ключами</returns>
        public static BigInteger[] ReadPrivateKey(string privateKeyPath)
        {
            if (Path.GetExtension(privateKeyPath).CompareTo(".privateKey") != 0)
                throw new ArgumentException("Неверное расширение");
            BigInteger[] pq = new BigInteger[2];
            using (StreamReader sr = new StreamReader(new FileStream(privateKeyPath, FileMode.Open), new RabinEncoding()))
            {
                pq[0] = BigInteger.Parse(sr.ReadLine());
                pq[1] = BigInteger.Parse(sr.ReadLine());
            }
            return pq; 
        }
        /// <summary>
        /// Шифрует файл с именем filename
        /// и сохраняет зашифрованный файл в указанном месте.
        /// Отсылает информацию о процессе, если 
        /// progress инициализирован пользователем.
        /// </summary>
        /// <param name="filename">путь к файлу</param>
        /// <param name="encryptPath">место, куда производится запись</param>
        /// <param name="progress">прогресс процесса</param>
        public void Encrypt(string filename, string encryptPath, IProgress<ProgressInfo> progress = null)
        {
            using (FileStream read = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (FileStream write = new FileStream(encryptPath, FileMode.Create, FileAccess.Write))
                {
                    Encrypt(read, write, progress);
                }
            }
        }
        /// <summary>
        /// Асинхронно шифрует файл с именем filename
        /// и сохраняет зашифрованный файл в указанном месте. 
        /// Отсылает информацию о процессе.
        /// </summary>
        /// <param name="filename">путь к файлу</param>
        /// <param name="encryptPath">место, куда производится запись</param>
        /// <param name="progress">прогресс процесса</param>
        /// <returns></returns>
        public async Task EncryptAsync(string filename, string encryptPath, IProgress<ProgressInfo> progress)
        {
            using (FileStream read = new FileStream(filename, FileMode.Open))
            {
                using (FileStream write = new FileStream(encryptPath, FileMode.Create))
                {
                    await Task.Run (() => Encrypt(read, write, progress));
                }
            }
        }
        /// <summary>
        /// Кодирует информацию из первого объекта Stream и записывает ее во второй объект Stream
        /// </summary>
        /// <param name="read">Объект Stream, откуда считывается кодируемая информация</param>
        /// <param name="write">Объект Stream, куда записывается закодированная информация</param>
        /// <param name="progress">Объект куда надо передавать прогресс</param>
        public void Encrypt(Stream read, Stream write, IProgress<ProgressInfo> progress)
        {
            int byteLength = _bitLength % 8 == 0 ? _bitLength / 8 - 1 : _bitLength / 8;
            byte[] chunk;
            using (BinaryReader br = new BinaryReader(read))
            {
                using (StreamWriter sw = new StreamWriter(write, new RabinEncoding()))
                {
                    chunk = ReadFileChunk(br, byteLength);

                    while (chunk.Length > 0)
                    {
                        sw.Write(Encrypt(new BigInteger(chunk)) + ":");
                        progress?.Report(new ProgressInfo(read.Position, read.Length));
                        chunk = ReadFileChunk(br, byteLength);
                    } 
                }
            }
        }
        private byte[] ReadFileChunk(BinaryReader br, int byteLength)
        {
            byte[] temp = br.ReadBytes(byteLength - CheckByteCount);
            if (temp.Length == 0)
                return new byte[0];
            byte[] chunk = new byte[temp.Length + CheckByteCount];
            Array.Copy(temp, 0, chunk, CheckByteCount - 1, temp.Length);
            chunk[chunk.Length - 1] = 1;
            return chunk;
        }
        /// <summary>
        /// Дешифрует файл с именем filename
        /// и сохраняет зашифрованный файл в указанном месте.
        /// Отсылает информацию о процессе, если
        /// progress инициализирован пользователем.
        /// </summary>
        /// <param name="filename">путь к файлу</param>
        /// <param name="decryptPath">место куда производится запись</param>
        /// <param name="progress">прогресс процесса</param>
        public void Decrypt(string filename, string decryptPath, IProgress<ProgressInfo> progress = null)
        {
            using (FileStream read = new FileStream(filename, FileMode.Open))
            {
                using (FileStream write = new FileStream(decryptPath, FileMode.Create))
                {
                    Decrypt(read, write, progress);
                }
            }
        }
        /// <summary>
        /// Асинхронно шифрует файл с именем filename
        /// и сохраняет зашифрованный файл в указанном месте.
        /// Отсылает информацию о процессе.
        /// </summary>
        /// <param name="filename">путь к файлу</param>
        /// <param name="decryptPath">место куда производится запись</param>
        /// <param name="progress">прогресс процесса</param>
        /// <returns></returns>
        public async Task DecryptAsync(string filename, string decryptPath, IProgress<ProgressInfo> progress)
        {
            using (FileStream read = new FileStream(filename, FileMode.Open))
            {
                using (FileStream write = new FileStream(decryptPath, FileMode.Create))
                {
                    await Task.Run(() => Decrypt(read, write, progress));
                }
            }
        }
        /// <summary>
        /// Декодирует сообщение из первого объекта Stream и записывает ее во второй объект Stream
        /// </summary>
        /// <param name="read">Объект Stream, откуда считывается закодированная информация</param>
        /// <param name="write">>Объект Stream, куда записывается раскодированная информация</param>
        public void Decrypt(Stream read, Stream write, IProgress<ProgressInfo> progress)
        {
            string encryptedChunk;
            using (BinaryWriter bw = new BinaryWriter(write))
            {
                using (StreamReader sr = new StreamReader(read, new RabinEncoding()))
                {
                    while (!string.IsNullOrEmpty(encryptedChunk = ReadEncryptedChunk(sr)))
                    {
                        var decryptedNumbers = Decrypt(BigInteger.Parse(encryptedChunk));
                        bw.Write(GetDecryptedChunk(decryptedNumbers));
                        progress?.Report(new ProgressInfo(read.Position, read.Length));
                    }
                }
            }
        }

        private string ReadEncryptedChunk(StreamReader sr)
        {
            string chunk = string.Empty;
            int nextSymbol;
            while ((nextSymbol = sr.Read()) != ':')
            {
                if (!char.IsDigit((char)nextSymbol))
                    break;
                chunk += (char)nextSymbol;
            }
            return chunk;
        }

        byte[] GetDecryptedChunk(BigInteger[] numbers)
        {
            var chunk = numbers.Select(x => x.ToByteArray()).FirstOrDefault(IsChunkValid);
            if (chunk == null)
                throw new InvalidDataException("Файл не может быть расшифрован");
            var dataLength = chunk.Length - CheckByteCount;
            var clippedChunk = new byte[dataLength];
            Array.Copy(chunk, CheckByteCount - 1, clippedChunk, 0, dataLength);
            return clippedChunk;
        }
        bool IsChunkValid(byte[] chunk)
        {
            return chunk.Last() == 1 && chunk.Take(CheckByteCount - 1).All(x => x == 0);
        }
    }
}