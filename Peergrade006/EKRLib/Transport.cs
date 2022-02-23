using System.Text.RegularExpressions;

namespace EKRLib
{
    /// <summary>
    /// Абстрактный класс, репрезентующий особености <see cref="Transport"/>.
    /// </summary>
    public abstract class Transport
    {
        /// <summary>
        /// Приватное поле для хранения модели обьекта типа <see cref="Transport"/>.
        /// </summary>
        private string _model;
        
        /// <summary>
        /// Приватное поле для хранения мощности двигателя обьекта типа <see cref="Transport"/>.
        /// </summary>
        private uint _power;
        
        /// <summary>
        /// Свойство, для представдения модели обьекта типа <see cref="Transport"/>.
        /// </summary>
        /// <exception cref="TransportException">Возвращается в случае неподходящего формата модели <see cref="Transport"/>.</exception>
        public string Model
        {
            get => _model;
            private set
            {
                if (IsInvalidModel(value))
                {
                    throw new TransportException($"Недопустимая модель {value}");
                }
                
                _model = value;
            }
        }

        /// <summary>
        /// Свойсво, репрезентующее мощность двигателя обьекта типа <see cref="Transport"/>.
        /// </summary>
        /// <exception cref="TransportException">Возвращается в случае неподходящего формата мощности двигателя.</exception>
        public uint Power
        {
            get => _power;
            private set
            {
                if (value < 20)
                {
                    throw new TransportException("мощность не может быть меньше 20 л.с.");
                }
                
                _power = value;
            }
        }

        /// <summary>
        /// Метод, репрезентующий звук двигателя обьекта <see cref="Transport"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию о звуке, который издаёт двигатель.</returns>
        public abstract string StartEngine();

        /// <summary>
        /// Конструктор обьекта типа <see cref="Transport"/>.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="power"></param>
        protected Transport(string model, uint power)
        {
            Model = model;
            Power = power;
        }
        
        /// <summary>
        /// Метод, проверяющий невалидность модели для создания обьекта типа <see cref="Transport"/>.
        /// </summary>
        /// <param name="value">Название модели</param>
        /// <returns>Boolean значение, не сответствует ли модель стандарту для обьекта типа <see cref="Transport"/></returns>
        private static bool IsInvalidModel(string value) =>
            (string.IsNullOrEmpty(value)) || (value.Length != 5) ||
            (Regex.Matches(value, @"[^A-Z0-9]").Count != 0);

        /// <summary>
        /// Метод, представляющий информацию об обьекте типа <see cref="Transport"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию об обьекте.</returns>
        public override string ToString() => $"Model: {Model}, Power: {Power}";
    }
}