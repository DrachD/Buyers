using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllSort
{
    ///<summary>
    /// Sort all figures in ascending order
    /// this is necessary in order to make it easier to check 
    /// if the tray has the necessary figures for the buyer
    ///</summary>
    public class Sort
    {
        public static void SortAscending(FigureType[] arr)
        {
            for (int startIndex = 0; startIndex < arr.Length - 1; startIndex++)
            {
                int smallestIndex = startIndex;
                
                for (int currentIndex = startIndex + 1; currentIndex < arr.Length; currentIndex++)
                {
                    if (arr[currentIndex] < arr[smallestIndex])
                    {
                        smallestIndex = currentIndex;
                    }
                }
                
                FigureType temp = arr[startIndex];
                arr[startIndex] = arr[smallestIndex];
                arr[smallestIndex] = temp;
            }
        }

        public static void SortAscending(List<FigureType> arr)
        {
            for (int startIndex = 0; startIndex < arr.Count - 1; startIndex++)
            {
                int smallestIndex = startIndex;
                
                for (int currentIndex = startIndex + 1; currentIndex < arr.Count; currentIndex++)
                {
                    if (arr[currentIndex] < arr[smallestIndex])
                    {
                        smallestIndex = currentIndex;
                    }
                }
                
                FigureType temp = arr[startIndex];
                arr[startIndex] = arr[smallestIndex];
                arr[smallestIndex] = temp;
            }
        }
    }
}
