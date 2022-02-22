using System;
using System.IO;
using System.Text;

namespace Peergrade006
{
    /// <summary>
    /// Класс, содержащий методы помогающие в работе программы.
    /// </summary>
    static class Tools
    {
        // Задаём переменную для генерации случайного числа.
        private static Random _random = new Random();
        
        /// <summary>
        /// Метод, подготавливающий файлы для записи информации.
        /// </summary>
        /// <param name="carFileName">Путь до файла для сохранения автомобилей.</param>
        /// <param name="boatFileName">Путь до файла для сохранения лодок.</param>
        internal static void PrepareFilesToWriting(out string carFileName, out string boatFileName)
        {
            // Получаем пути до файлов.
            carFileName = Path.GetFullPath(
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}Cars.txt");
            boatFileName = Path.GetFullPath(
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}MotorBoats.txt");

            // Производим создание пустого файла или его перезапись.
            using (FileStream fs = File.Create(carFileName))
            {
                byte[] info = new UnicodeEncoding().GetBytes(String.Empty);
                fs.Write(info, 0, info.Length);
            }
            using (FileStream fs = File.Create(boatFileName))
            {
                byte[] info = new UnicodeEncoding().GetBytes(String.Empty);
                fs.Write(info, 0, info.Length);
            }
        }
        
        /// <summary>
        /// Метод, генерирующий не всегда валидные модели транспорта.
        /// </summary>
        /// <returns>Строка, содержащая имя модели трансопорта</returns>
        internal static string GenerateModelName()
        {
            var stringBuilder = new StringBuilder();
            int choice = _random.Next(0, 101);
            // В зависимости от шанса, мы выбираем длину названия модели.
            for (int i = 0; i < ((choice > 80) ? 4: 5); i++)
            {
                // Далее путем неравенств, создаём вероятность создания невалидного имени модели.
                if (choice <= 40)
                {
                    stringBuilder.Append((char)_random.Next('A', 'Z'));
                }
                else if (choice > 75)
                {
                    stringBuilder.Append((char)_random.Next(33, 110));
                }
                else
                {
                    stringBuilder.Append(_random.Next(0, 10).ToString());
                }
            }
            // Возвращаем строку.
            return  stringBuilder.ToString();
        }
    }
}