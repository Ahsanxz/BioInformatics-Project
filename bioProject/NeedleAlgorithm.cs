using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace bioProject
{
    public class NeedleAlgorithm
    {

        public string align1 = "";
        public string align2 = "";

        public int noMatches =0;
        public int noMismatches=0;
        public int noGaps=0;

        public int maxIdentify;

        public void SequenceAlignmnet(string seq1, string seq2)
        {




            string str1 = seq1;                       //"ACGCATCA";
            str1.ToUpper();
            string str2 = seq2;                            //"ACTGATTCA";
            str2.ToUpper();

            int s1Length = str1.Length +1;                  //seq1.Length;
            int s2Length = str2.Length +1;                 //seq2.Length;



            //identitfy max length
            int max1 = str1.Length;
            int max2 = str2.Length;

            int[] maxArryx = { max1, max2 };
            int maxIdenity = maxArryx.Max();
            maxIdentify = maxIdenity;

            char[] s1 = str1.ToCharArray();             //seq1.ToCharArray();
            char[] s2 = str2.ToCharArray();                 //seq2.ToCharArray();

            char[] arrng = { ' ' };
            

            int match = 2;
            int mismatch = -2;
            int gap = -1;

            ///////
            int s1gap = 0;
            int s2gap = 0;
            ///////
            
            int[,] Arr = new int[s1Length,s2Length];

            for(int i =0; i<s1Length; i++)
            {
                int j = 0;
                
                Arr[i, j] = s1gap;
                s1gap = s1gap - 2;

                //Debug.Write(Arr[i,j]);
                
            }
            for (int j = 0; j < s2Length; j++)
            {
                int i = 0;

                Arr[i, j] = s2gap;
                s2gap = s2gap - 2;

                //Debug.WriteLine(Arr[i, j]);
              
            }

            // making matrix

            for(int i =1; i<s1Length; i++)
            {
                int toTop;
                int toLeft;
                int toTopleft;

                for(int j =1; j<s2Length; j++)
                {
                    if (s1[i-1] == s2[j-1])
                    {
                        toTop = Arr[i, j-1];
                        toLeft = Arr[i-1, j];
                        toTopleft = Arr[i-1,j-1];

                        toTop = toTop + gap;
                        toLeft = toLeft + gap;
                        toTopleft = toTopleft + match;

                        int[] maxArr = { toTop, toLeft, toTopleft };
                        int maxValue = maxArr.Max();
                        Arr[i, j] = maxValue;

                    if(i==j)
                        {
                            if (maxArr[0] == maxValue)
                            {
                                noGaps++;
                            }
                            if (maxArr[1] == maxValue)
                            {
                                noGaps++;
                            }
                            if (maxArr[2] == maxValue)
                            {
                                noMatches++;
                            }
                        }
                      


                    }
                   else if (s1[i-1] != s2[j-1])
                        {
                        toTop = Arr[i, j - 1];
                        toLeft = Arr[i - 1, j];
                        toTopleft = Arr[i - 1, j - 1];

                        toTop = toTop + gap;
                        toLeft = toLeft + gap;
                        toTopleft = toTopleft + mismatch;

                        int[] maxArr = { toTop, toLeft, toTopleft };
                        int maxValue = maxArr.Max();
                        Arr[i, j] = maxValue;


                        if (i == j)
                        {
                            if (maxArr[0] == maxValue)
                            {
                                noGaps++;
                            }
                            if (maxArr[1] == maxValue)
                            {
                                noGaps++;
                            }
                            if (maxArr[2] == maxValue)
                            {
                                noMismatches++;
                            }
                        }


                    }



                   
                }
            }


            //Display Matrix
            for(int i=0; i<s1Length; i++)
            {
                for(int j=0; j<s2Length; j++)
                {
                    Debug.Write(" ");
                    
                    Debug.Write(Arr[i,j]);
                }
               
                Debug.Write("\n");
               

            }



            //Sequence ALignment
        
            //Traceback Step
            char[] alineSeqArray = s1;
            char[] refSeqArray = s2;

            string AlignmentA = string.Empty;
            string AlignmentB = string.Empty;
            int m = s1Length - 1;
            int n = s2Length - 1;
            while (m > 0 || n > 0)
            {
                int scroeDiag = 0;

                if (m == 0 && n > 0)
                {
                    AlignmentA = refSeqArray[n - 1] + AlignmentA;
                    AlignmentB = "-" + AlignmentB;
                    n = n - 1;
                }
                else if (n == 0 && m > 0)
                {
                    AlignmentA = "-" + AlignmentA;
                    AlignmentB = alineSeqArray[m - 1] + AlignmentB;
                    m = m - 1;
                }
                else
                {
                    //Remembering that the scoring scheme is +2 for a match, -1 for a mismatch, and -2 for a gap
                    if (alineSeqArray[m - 1] == refSeqArray[n - 1])
                        scroeDiag = 2;
                    else
                        scroeDiag = -2;

                    if (m > 0 && n > 0 && Arr[m, n] == Arr[m - 1, n - 1] + scroeDiag)
                    {
                        AlignmentA = refSeqArray[n - 1] + AlignmentA;
                        AlignmentB = alineSeqArray[m - 1] + AlignmentB;
                        m = m - 1;
                        n = n - 1;
                    }
                    else if (n > 0 && Arr[m, n] == Arr[m, n - 1] - 2)
                    {
                        AlignmentA = refSeqArray[n - 1] + AlignmentA;
                        AlignmentB = "-" + AlignmentB;
                        n = n - 1;
                    }
                    else //if (m > 0 && scoringMatrix[m, n] == scoringMatrix[m - 1, n] + -2)
                    {
                        AlignmentA = "-" + AlignmentA;
                        AlignmentB = alineSeqArray[m - 1] + AlignmentB;
                        m = m - 1;
                    }
                }
            }


            align1 = AlignmentA;
            align2 = AlignmentB;


            Debug.Write(Environment.NewLine);
            Debug.WriteLine(AlignmentA);
            Debug.WriteLine(AlignmentB);
            Debug.WriteLine("\n");







        }

    }
}
