using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace bioProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>


        public string filePath = "";
        public string sequenceName1;
        public string sequenceName2;
        public int seqCount1;
        public int seqCount2;
        public string FirstSequence;
        public string SecondSequence;

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Fasta files (*.fasta)|*.fasta|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
               // string name = (System.IO.Path.GetFileName(filename)); //get file name
                string path = (Path.GetFullPath(filename));
                fileNametxt.Content = path;

                filePath = path;
                startBtn.IsEnabled = true;



            }
        }


        



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string input = "INPUT:\n######\n";
            string sequences = File.ReadAllText(filePath);
            string scores = "\n Scores:    match = +2, mismatch = -2, gap = -2";

            // Split with multiple separators  
            string multiCharString = sequences;
            // Split authors separated by a comma followed by space  
            string[] multiArray = multiCharString.Split(new Char[] { '>', '\n' });

            foreach (string author in multiArray)
            {
                //if (author.Trim() != "")
                  //  Debug.WriteLine(author);
            }

           

            // Get Sequence Names

            for (int i = 0; i < multiArray.Length; i++)
            {
                if (multiArray[i].Contains('1'))
                {
                    
                    sequenceName1 = multiArray[i];
                  
                   
                   // Debug.WriteLine(sequenceName1);
                }
                if (multiArray[i].Contains('2'))
                {

                    sequenceName2 = multiArray[i];


                    //Debug.WriteLine(sequenceName2);
                }
            }

            /// end names
            /// 

            // Get Sequences Chrachters Count

            string replaceSeq = sequences;
            string x1 = replaceSeq.Replace(sequenceName1, "");
            string x2 = x1.Replace(sequenceName2, "");
            string x3 = x2.Replace(sequenceName2, "");
            string x4 = x3.Replace("\r", "");
            string x5 = x4.Replace("\n", "");

            // Split with multiple separators  
            string changedSeq = x5;
            // Split authors separated by a comma followed by space  
            string[] seqArr = changedSeq.Split(new Char[] { '>' });



            for (int i = 0; i < seqArr.Length; i++)
            {
               // Debug.WriteLine(seqArr[i]);
            }

            seqCount1 =  seqArr[1].Length;
            seqCount2 = seqArr[2].Length;

            FirstSequence = seqArr[1];
            SecondSequence = seqArr[2];


            //////////////////////////////////// end count


            ////////////////////////////// Output line Editing ////////////////////////
            string sequence1 = "\n Sequence 1 =" + sequenceName1 + "," + "Length = " + seqCount1 + "Characters";
            string sequence2 = "\n Sequence 2 =" + sequenceName2 + "," + "Length = " + seqCount2 + "Characters";

            string seq1 = sequence1;
            string y1 = seq1.Replace("\n", "");
            string y2 = y1.Replace("\r", "");

            string seq2 = sequence2;
            string y3 = seq2.Replace("\n", "");
            string y4 = y3.Replace("\r", "");

            string needleMan = "\n\nNeedleman-Wunsch-OUTPUT: \n#############################\n\n";

            
            ///////////////////////////////////////////

            //// NeedleMan wunch
            ///
            NeedleAlgorithm n = new NeedleAlgorithm();
            n.SequenceAlignmnet(FirstSequence,SecondSequence);

            string aligned1 = n.align1;
            string aligned2 = n.align2;

            int matched = n.noMatches;
            int mismatched = n.noMismatches;
            int gapes = n.noGaps;
            int maxLenght = n.maxIdentify;
           // maxLenght = matched / maxLenght;

            
            string report = "\n\nReport: \n\n Alignment: Needleman-Wunsch-Algorithm-Global \n\n Number of: matches = " + matched + ", Mismatches = " + mismatched + ", gaps = " + gapes + "\n\n  Identities = " + matched + " / " + maxLenght + " , Gaps = " + gapes + " / " + maxLenght;
           
            ////// Water-Smith


            string waterSmith = "\n\n Smith-Waterman-OUTPUT: \n ***************************\n\n";

            smithAlgo smith = new smithAlgo();
            smith.SequenceAlignmnet(FirstSequence, SecondSequence);

            string waligned1 = smith.align1;
            string waligned2 = smith.align2;

            int wmatched = smith.noMatches;
            int wmismatched = smith.noMismatches;
            int wgapes = smith.noGaps;
            int wmaxLenght = smith.maxIdentify;


            string report2 = "\n\nReport: \n\n Alignment: Smith-Waterman-Algorithm-Local \n\n Number of: matches = " + wmatched + ", Mismatches = " + wmismatched + ", gaps = " + wgapes + "\n\n  Identities = " + wmatched + " / " + wmaxLenght + " , Gaps = " + wgapes + " / " + wmaxLenght;



            outputText.Text = input + sequences + scores + "\n" + y2 + "\n" + y4 + needleMan + "s1   " + aligned1 + "\n" + "s2   " + aligned2 + report + waterSmith + "s1   " + waligned1 + "\n" + "s2   " + waligned2 + report;

            // outputText.Text = File.ReadAllText(filePath);
        }

        
    }
}
