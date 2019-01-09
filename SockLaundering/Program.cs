using System;
using System.Collections.Generic;
// you can also use other imports, for example:
// using System.Collections.Generic;

// you can write to stdout for debugging purposes, e.g.
// Console.WriteLine("this is a debug message");

class Solution
{
    public int solution(int K, int[] C, int[] D)
    {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
        //K is the number of socks that can be washed in the washing machine
        //C are already clean socks
        //D are dirty socks
        //Find the max number of matching clean pairs of socks that can be taken after 1 wash

        //Loop through clean socks and add to count array
        //Loop through dirty socks and add to count array

        int totalCleanPairs = 0;
        int remainingAvailableWash = K;

        Dictionary<int, int> cleanCountArray = new Dictionary<int, int>();
        Dictionary<int, int> dirtyCountArray = new Dictionary<int, int>();

        for(int i = 0; i < C.Length; i++)
        {
            int existingCount = 0;
            if(cleanCountArray.TryGetValue(C[i], out existingCount))
            {
                cleanCountArray[C[i]] = existingCount + 1;
                if(existingCount + 1 == 2)
                {
                    totalCleanPairs++;
                    cleanCountArray[C[i]] = 0;
                }
            }
            else
            {
                cleanCountArray.Add(C[i], 1);
            }
        }
        for (int i = 0; i < D.Length; i++)
        {
            int existingCount = 0;
            if (dirtyCountArray.TryGetValue(D[i], out existingCount))
            {
                dirtyCountArray[D[i]] = existingCount + 1;
            }
            else
            {
                dirtyCountArray.Add(D[i], 1);
            }
        }

        //Loop through count array and any non-pairs, look in dirty array. If there is a dirty matching the value, then wash it
        foreach(var sock in cleanCountArray)
        {
            if(remainingAvailableWash == 0)
            {
                break;
            }
            if(sock.Value == 1)
            {
                int dirtyCount = 0;
                if(dirtyCountArray.TryGetValue(sock.Key, out dirtyCount))
                {
                    if(dirtyCount > 0)
                    {
                        dirtyCountArray[sock.Key]--;
                        totalCleanPairs++;
                        remainingAvailableWash--;
                    }
                }
            }
        }

        //We now only have chance of getting more pairs by cleaning 2 from dirty
        foreach (var dirtySock in dirtyCountArray)
        {
            if(remainingAvailableWash < 2)
            {
                break;
            }
            int numberOfThisSock = dirtySock.Value;
            while (remainingAvailableWash >= 2 && numberOfThisSock >= 2)
            {
                //Clean 2 socks
                remainingAvailableWash -= 2;
                numberOfThisSock -= 2;
                totalCleanPairs++;
            }
        }
        return totalCleanPairs;
    }
}