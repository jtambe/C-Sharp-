using System;
using System.Collections.Generic;
					
public class Program
{
	// Given rod of length n units, and price of all pieces smaller than n 
	// find most profitable way of cutting the rod
	// https://www.youtube.com/watch?v=zFe-SX7jzDs
  
  
	public static void Main()
	{
		//length of piece , price
		Dictionary<int,int> rodPrices = new Dictionary<int,int>();
		rodPrices.Add(1, 2);
		rodPrices.Add(2, 5);
		rodPrices.Add(3, 9);
		rodPrices.Add(4, 6);
		
		int maxNumberOfPiecesCanBeCut = 0;
		foreach(var pair in rodPrices)
		{
			if (pair.Key > maxNumberOfPiecesCanBeCut)
			{
				maxNumberOfPiecesCanBeCut = pair.Key;
			}
		}
	
		int rodLength = 5;
		
		int [,] memo = new int [maxNumberOfPiecesCanBeCut+1, rodLength+1];
		
		//populate zeros
		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			memo[i,0] = 0;
		}
		for(var j = 0; j <= rodLength; j++)
		{
			memo[0,j] = 0;
		}
		
		for(var j = 1; j <= rodLength; j++)
		{
			memo[1,j] = rodPrices[1] * j;
		}
		
		// i = rod can be cut into pieces of size i
		// j = length of rod for current interation
		for(var i = 2; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			for(var j = 1; j <= rodLength; j++)
			{
				if(j < i) // length of rod 1 cannot be cut into pieces of size 2
				{
					memo[i,j] = memo[i -1, j];
				}
				else
				{
					var PriceIncludingCurrentIterationSizeOfRod = rodPrices[i] + memo[i, j - i];
					var PriceExcludingCurrentIterationSizeOfRod = memo[i-1, j];
					memo[i,j] = Math.Max(PriceIncludingCurrentIterationSizeOfRod , PriceExcludingCurrentIterationSizeOfRod);
				}
			}
		}
		
		for(var i = 0; i <= maxNumberOfPiecesCanBeCut; i++)
		{
			Console.Write("[ ");
			for(var j = 0; j <= rodLength; j++)
			{
				Console.Write( memo[i,j] + " ");
			}
			Console.Write("]");
			Console.WriteLine();
		}

		Console.WriteLine();
		
		Console.WriteLine("Max profit: " + memo[maxNumberOfPiecesCanBeCut, rodLength]);		
		
	}
}
