namespace EKRLib
{
    /// <summary>
    /// Класс, репрезентующий особености <see cref="Car"/>.
    /// </summary>
    public class Car: Transport
    {
        /// <summary>
        /// Конструктор обьекта типа <see cref="Car"/>.
        /// </summary>
        /// <param name="model">Модель обьекта типа <see cref="Car"/></param>
        /// <param name="power">Мощность двигателя обьекта типа <see cref="Car"/></param>
        public Car(string model, uint power) : base(model, power)
        {
        }

        /// <summary>
        /// Метод, репрезентующий звук двигателя обьекта <see cref="Car"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию о звуке, который издаёт двигатель.</returns>
        public override string StartEngine() => $"{Model}: Vroom";

        /// <summary>
        /// Метод, представляющий информацию об обьекте типа <see cref="Car"/>.
        /// </summary>
        /// <returns>Строку, содержащую информацию об обьекте.</returns>
        public override string ToString() => $"Car. {base.ToString()}";
    }
}