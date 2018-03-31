using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Achun
{
    public static class SortUtil
    {
        private static void Swap<T>(List<T> list, int a, int b) where T : IComparable
        {
            T tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }


        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSort<T>(List<T> list) where T : IComparable
        {
            int i = 0;
            while (i < list.Count - 1)
            {
                int j = i + 1;
                while (j > 0)
                {
                    if (list[j - 1].CompareTo(list[j]) > 0)
                    {
                        Swap(list,j - 1,j);
                        j--;
                    }
                    else
                        break;
                }
                i++;
            }
        }

        /// <summary>
        /// 简单选择排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void SimpleSelectionSort<T>(List<T> list) where T : IComparable
        {
            int i = 0;
            while (i <= list.Count - 1)
            {
                int j = i;
                int minIndex = i;
                while (j <= list.Count - 1)
                {
                    if (list[j].CompareTo(list[minIndex]) <= 0)
                    {
                        minIndex = j;
                    }
                    j++;
                }
                Swap(list,i,minIndex);
                i++;
            }
        }

        /// <summary>
        /// 冒泡排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void BubbleSort<T>(List<T> list) where T : IComparable
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool isSorted = true;
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j].CompareTo(list[j + 1]) > 0)
                    {
                        isSorted = false;
                        T tmp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = tmp;
                    }
                }
                if (isSorted)
                    break;
            }
        }

        /// <summary>
        /// 鸡尾酒排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void CooktailSort<T>(List<T> list) where T : IComparable
        {
            int left = 0;
            int right = list.Count - 1;
            bool isSorted = true;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (list[i].CompareTo(list[i + 1]) > 0)
                    {
                        isSorted = false;
                        T tmp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = tmp;
                    }
                }
                if (isSorted)
                    break;
                right--;
                isSorted = true;
                for (int i = right; i > left; i--)
                {
                    if (list[i - 1].CompareTo(list[i]) > 0)
                    {
                        isSorted = false;
                        T tmp = list[i];
                        list[i] = list[i - 1];
                        list[i - 1] = tmp;
                    }
                }

                if (isSorted)
                    break;
                left++;
            }
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void FastSort<T>(List<T> list, int left, int right) where T : IComparable
        {
            if (left >= right)
                return;
            int low = left;
            int hight = right;
            int pivot = left;
            T tmp = list[pivot];
            while (left < right)
            {
                while (left < right && list[right].CompareTo(tmp) > 0)
                {
                    right--;
                }
                Swap<T>(list, left, right);
                while (left < right && list[left].CompareTo(tmp) <= 0)
                {
                    left++;
                }
                Swap<T>(list, left, right);
            }
            pivot = left;
            FastSort<T>(list, low, pivot - 1);
            FastSort<T>(list, pivot + 1, hight);
        }


        public static void MFastSort<T>(List<T> list, int left, int right) where T : IComparable
        {
            if (left >= right)
                return;
            int low = left;
            int hight = right;
            int pivot = left;
            T tmp = list[pivot];
            while (low > hight)
            {                
                while (left < right)
                {
                    while (left < right && list[right].CompareTo(tmp) > 0)
                    {
                        right--;
                    }
                    Swap<T>(list, left, right);
                    while (left < right && list[left].CompareTo(tmp) <= 0)
                    {
                        left++;
                    }
                    Swap<T>(list, left, right);
                }
                low = left;
                hight = right;
                pivot = left;
                tmp = list[pivot];
            }
        }

        /// <summary>
        /// 堆排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="n"></param>
        public static void HeapSort<T>(List<T> list,int n) where T : IComparable
        {
            int heapSize = BuildHeap(list,n);
            while (heapSize > 1)
            {
                Swap(list,0,--heapSize);
                Heapify(list,0,heapSize);//将新的无序序列再次调整
            }
        }

        /// <summary>
        /// 创建堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int BuildHeap<T>(List<T> list,int n) where T : IComparable
        {
            int heapSize = n;
            for (int i = heapSize / 2 - 1; i >= 0; i--)//从最后一个非叶子节点（最右非叶子结点）开始
            {
                Heapify(list,i,heapSize);
            }
            return heapSize;
        }

        /// <summary>
        /// 调整堆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="size"></param>
        private static void Heapify<T>(List<T> list,int i,int size) where T : IComparable
        {
            int leftChild = 2 * i + 1;
            int rightChilde = 2 * i + 2;
            int max = i;
            if (leftChild < size && list[leftChild].CompareTo(list[max]) > 0)
            {
                max = leftChild;
            }
            if (rightChilde < size && list[rightChilde].CompareTo(list[max]) > 0)
            {
                max = rightChilde;
            }
            if (max != i)
            {
                Swap<T>(list,i,max);
                Heapify(list,max,size);//替换后的节点继续调整，没有替换的节点不需要
            }
        }

        /// <summary>
        /// 希尔排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void ShellSort<T>(List<T> list) where T : IComparable
        {
            int increment = list.Count / 2;
            while (increment >= 1)
            {
                for (int i = increment; i < list.Count; i++)
                {
                    if (list[i].CompareTo(list[i - increment]) < 0)
                    {
                        int j = i - increment;
                        T tmp = list[i];
                        list[i] = list[i - increment];
                        while (list[j].CompareTo(tmp) > 0)
                        {
                            //list[j + increment] = list[j];
                            j -= increment;
                            if (j < 0)
                                break;
                        }
                        list[j + increment] = tmp;
                    }
                }
                increment /= 2;
            }
        }


        public static void MergeSort<T>(List<T> list) where T : IComparable
        {
            int i = list.Count / 2;
            while (i >= 1)
            {

                i /= 2;
            }
        }
    }
}
