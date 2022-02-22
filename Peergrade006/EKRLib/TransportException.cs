#nullable enable
using System;
using System.Runtime.Serialization;

namespace EKRLib
{
    /// <summary>
    /// Сереализуемый класс, отвечающий за выбрасывание исключений типа <see cref="TransportException"/>.
    /// </summary>
    [Serializable()]
    public class TransportException : Exception
    {
        /// <summary>
        /// Пустой конструктор исключения типа <see cref="TransportException"/>
        /// </summary>
        public TransportException()
        {
        }

        /// <summary>
        /// Сериализуемый конструктор исключения типа <see cref="TransportException"/>
        /// </summary>
        /// <param name="info">Информация о сериализации обьекта.</param>
        /// <param name="context">Описание источника и точки назначения потока сериализации.</param>
        public TransportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Стандартный конструктор исключения типа <see cref="TransportException"/>.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку.</param>
        public TransportException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор исключения  типа <see cref="TransportException"/>,
        /// принимающий на вход информацию об ошибке, вызвавшей данное исключение.
        /// </summary>
        /// <param name="message">Сообщение, описывающее ошибку.</param>
        /// <param name="innerException">Исключение, вызвавшее данное.</param>
        public TransportException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}