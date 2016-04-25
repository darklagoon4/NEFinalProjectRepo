using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T :IHeapEntry<T> {

    T[] heap;
    int currentHeapCount;

    public Heap(int maxHeapSize)
    {
        heap = new T[maxHeapSize];
        currentHeapCount = 0;
    }

    public void add(T entry)
    {
        
        entry.heapIndex = currentHeapCount;
        
        heap[currentHeapCount] = entry;
        bubbleUp(entry);
        currentHeapCount++;
        
    }

    public T pop()
    {
        T topEntry = heap[0];
        currentHeapCount--;
        heap[0] = heap[currentHeapCount];
        heap[0].heapIndex = 0;
        bubbleDown(heap[0]);
        return topEntry;
    }

    public bool contains(T entry)
    {
        return Equals(heap[entry.heapIndex], entry);
    }

    void bubbleDown(T entry)
    {
        while (true)
        {
            
            int childIndexLeft = entry.heapIndex * 2 + 1;
            int childIndexRight = entry.heapIndex * 2 + 2;
            int swapIndex = 0;

            if (childIndexLeft < currentHeapCount)
            {
                swapIndex = childIndexLeft;
                if (childIndexRight < currentHeapCount)
                {
                    if (heap[childIndexLeft].CompareTo(heap[childIndexRight]) < 0)
                    {
                        swapIndex = childIndexRight;
                    }
                }
                
                if (entry.CompareTo(heap[swapIndex]) < 0)
                {
                    swap(entry, heap[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void bubbleUp(T entry)
    {
        int parentIndex = (entry.heapIndex - 1) / 2;
        while (true)
        {
            T parentEntry = heap[parentIndex];
            if (entry.CompareTo(parentEntry) > 0)
            {
                swap(entry, parentEntry);
            }
            else
            {
                break;
            }
            parentIndex = (entry.heapIndex - 1) / 2;
        }
    }

    void swap(T entryA, T entryB)
    {
        
        heap[entryA.heapIndex] = entryB;
        heap[entryB.heapIndex] = entryA;
        int tempEntryIndex = entryA.heapIndex;
        entryA.heapIndex = entryB.heapIndex;
        entryB.heapIndex = tempEntryIndex;
    }

    public void updateEntry(T entry)
    {
        bubbleUp(entry);
    }

    public int count
    {
        get { return currentHeapCount; }
    }
}

public interface IHeapEntry<T>:IComparable<T>
{
    int heapIndex
    {
        get;
        set;
    }
}
