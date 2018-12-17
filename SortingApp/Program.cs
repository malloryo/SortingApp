using System;
using System.Collections.Generic;

namespace Sorting
{
    class Program
    {
        const int minArrSize = 8;
        const int maxArrSize = 10;
        const int minNum = -50;
        const int maxNum = 50;

        static void Main(string[] args)
        {
            List<int> unsortedNums = GetUnsortedNums();
            var sortedNums = new List<int>();

            Console.WriteLine("Original Array: ");
            DisplayArray(unsortedNums);

            // QuickSort
            Console.WriteLine();
            Console.WriteLine("QuickSort: ");
            DisplayArray(unsortedNums);
            sortedNums = QuickSort(unsortedNums);
            DisplayArray(sortedNums);

            // BubbleSort
            Console.WriteLine();
            Console.WriteLine("BubbleSort: ");
            DisplayArray(unsortedNums);
            sortedNums = BubbleSort(unsortedNums);

            // MergeSort
            Console.WriteLine();
            Console.WriteLine("MergeSort: ");
            DisplayArray(unsortedNums);
            sortedNums = MergeSort(unsortedNums);

            Console.ReadLine();
        }

        static List<int> GetUnsortedNums()
        {
            var random = new Random();
            List<int> unsortedArray = new List<int>();

            var arraySize = random.Next(minArrSize, maxArrSize);

            for (int i = 0; i < arraySize; i++)
            {
                unsortedArray.Add(random.Next(minNum, maxNum));
            }

            return unsortedArray;
        }

        static List<int> QuickSort(List<int> unsortedArr)
        {
            // Copy to New Array
            var arr = new List<int>(unsortedArr);

            // Already Sorted, Base Case
            if (arr.Count <= 1)
                return arr;

            // Select And Remove Pivot
            var random = new Random();
            var pivotPos = random.Next(arr.Count);
            var pivotInt = arr[pivotPos];
            arr.RemoveAt(pivotPos);

            // Place Values More/Less Than Pivot in More/Less Arrays
            List<int> less = new List<int>();
            List<int> more = new List<int>();
            foreach (int val in arr)
            {
                if (val <= pivotInt)
                    less.Add(val);
                else
                    more.Add(val);
            }

            Console.WriteLine("Pivot: {0}  | [{1}] < [{2}]", pivotInt.ToString().PadLeft(4, ' '), String.Join(" ", less), String.Join(" ", more));

            // Recursively Sort Less and More Arrays, Result is Merged List
            var result = QuickSort(less);
            result.Add(pivotInt);
            result.AddRange(QuickSort(more));
            return result;
        }

        static List<int> BubbleSort(List<int> unsortedArr)
        {
            // Copy to New Array
            var arr = new List<int>(unsortedArr);

            for (int i = 1; i < arr.Count; i++)
            {
                for (int j = 0; j < arr.Count - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        var temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                        DisplayArray(arr);
                    }
                }
            }

            return arr;
        }

        static List<int> MergeSort(List<int> unsortedArr)
        {
            // Copy to New Array
            var arr = new List<int>(unsortedArr);

            // Already Sorted, Base Case
            if (arr.Count <= 1)
                return arr;

            var mid = arr.Count / 2;
            var left = new List<int>();
            var right = new List<int>();

            // Split Into Left/Right Arrays By Midpoint
            for (int i = 0; i < mid; i++)
            {
                left.Add(arr[i]);
            }
            for (int i = mid; i < arr.Count; i++)
            {
                right.Add(arr[i]);
            }

            Console.WriteLine("Left: " + String.Join(" ", left) + " | Right: " + String.Join(" ", right));

            // MergeSort Left and Right
            left = MergeSort(left);
            right = MergeSort(right);

            // If Last Elem of Left is Before Start of Right, Left + Right is Sorted
            if (left[left.Count - 1] <= right[0])
            {
                left.AddRange(right);
                DisplayArray(left);
                return left;
            }

            // Else Merge Lists
            var result = MergeLists(left, right);

            DisplayArray(result);

            return result;
        }

        static List<int> MergeLists(List<int> left, List<int> right)
        {
            List<int> merged = new List<int>();

            // While Both Arrs Have Elements, Add to Merged In Order
            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] <= right[0])
                {
                    merged.Add(left[0]);
                    left.RemoveAt(0);
                }
                else
                {
                    merged.Add(right[0]);
                    right.RemoveAt(0);
                }
            }

            // Deal With Leftover Elements
            while (left.Count > 0)
            {
                merged.Add(left[0]);
                left.RemoveAt(0);
            }
            while (right.Count > 0)
            {
                merged.Add(right[0]);
                right.RemoveAt(0);
            }

            return merged;
        }

        static void DisplayArray(List<int> arr)
        {
            var printMe = String.Join(" ", arr);
            Console.WriteLine(printMe);
        }

    }
}