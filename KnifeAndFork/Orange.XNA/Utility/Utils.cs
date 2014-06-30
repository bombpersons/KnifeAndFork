using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orange.XNA.Utility
{
    static class Utils
    {
        public static T[] Convert2Dto1D<T>(T[,] _array)
        {
            T[] newArray = new T[_array.GetLength(0) * _array.GetLength(1)];
            for (int i = 0; i < _array.GetLength(0); i++)
            {
                for (int j = 0; i < _array.GetLength(1); j++)
                {
                    newArray[i * _array.GetLength(1) + j] = _array[i, j];
                }
            }

            return newArray;
        }
    }
}
