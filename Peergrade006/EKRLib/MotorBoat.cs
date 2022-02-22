namespace EKRLib
{
    /// <summary>
    /// Класс, репрезентующий особености <see cref="MotorBoat"/>.
    /// </summary>
    public class MotorBoat: Transport
    {
        /// <summary>
        /// Конструктор обьекта типа <see cref="MotorBoat"/>.
        /// </summary>
        /// <param name="model">Модель обьекта типа <see cref="MotorBoat"/></param>
        /// <param name="power">Мощность двигателя обьекта типа <see cref="MotorBoat"/></param>
        public MotorBoat(string model, uint power) : base(model, power)
        {
        }

        /// <summary>
        /// Метод, репрезентующий звук двигателя обьекта <see cref="MotorBoat"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию о звуке, который издаёт двигатель.</returns>
        public override string StartEngine() => $"{Model}: Brrrbrr";

        /// <summary>
        /// Метод, представляющий информацию об обьекте типа <see cref="MotorBoat"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию об обьекте.</returns>
        public override string ToString() => $"MotorBoat. {base.ToString()}";
    }
}