using System;

namespace RabinLib
{
    public class ProgressInfo
    {
        /// <summary>
        /// Положение в данный момент
        /// </summary>
        public readonly long value;
        /// <summary>
        /// Максимальное значение
        /// </summary>
        public readonly long maximum;
        /// <summary>
        /// Инициализирует объект ProgressInfo,
        /// задавая новое положение и максимальное значение
        /// </summary>
        /// <param name="newValue">Новое положение</param>
        /// <param name="newMaximum">Новое максимальное значение</param>
        public ProgressInfo(long newValue, long newMaximum)
        {
            value = newValue;
            maximum = newMaximum;
        }

        public Rabin Rabin
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }
    }
}
