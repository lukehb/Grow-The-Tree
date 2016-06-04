using System;

namespace AssemblyCSharp
{
	//Static Util Class to calculate score within the game (i.e maybe later not all scores will be based on branches)
	public class ScoreCalculator
	{
		
		//Calculate score based on the number of branches destroyed
		public static long CalculateBranchesScore(int nBranches){
			//formulae = 1000 * (nBranches ^ 2)
			long result = 1000L * (long)(Math.Pow(nBranches, 2L)); 
			return result;
		}
		
	}
}

