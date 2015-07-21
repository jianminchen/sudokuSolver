using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudokuSolver3
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * Latest update: July 20, 2015 
             * Test case:  
             *   
             */
            char[][] board = new char[9][];

            board[0] = new char[] { '.', '.', '9', '7', '4', '8', '.', '.', '.' };
            board[1] = new char[] { '7', '.', '.', '.', '.', '.', '.', '.', '.' };
            board[2] = new char[] { '.', '2', '.', '1', '.', '9', '.', '.', '.' };
            board[3] = new char[] { '.', '.', '7', '.', '.', '.', '2', '4', '.' };
            board[4] = new char[] { '.', '6', '4', '.', '1', '.', '5', '9', '.' };
            board[5] = new char[] { '.', '9', '8', '.', '.', '.', '3', '.', '.' };
            board[6] = new char[] { '.', '.', '.', '8', '.', '3', '.', '2', '.' };
            board[7] = new char[] { '.', '.', '.', '.', '.', '.', '.', '.', '6' };
            board[8] = new char[] { '.', '.', '.', '2', '7', '5', '9', '.', '.' };

            
            solveSudokuRe(board, 0, 0);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    System.Console.Write(board[i][j] + ", ");
                }
                System.Console.WriteLine();
            }
        }
        /*
    void solveSudoku(vector<vector<char>> &board) {
        pair<int, int> next = (board[0][0] == '.') ? make_pair(0, 0) : 
                                                     getNextMissing(board, 0, 0);
        solveSudokuRe(board, next.first, next.second);
    }  */

    /*
     * source code from blog:
     * https://github.com/yinlinglin/LeetCode/blob/master/SudokuSolver.h
     * convert C++ code to C# code
     *  
     */
    
      public static  bool solveSudokuRe(char[][]board, int row, int col) {
            if (row == 9) return true;

            KeyValuePair<int, int> next = getNextMissing(board, row, col);

            ArrayList possible = new ArrayList();

            getPossibleValues(board, row, col, possible);

            for (int i = 0; i < possible.Count; ++i)
            {
                board[row][col] = (char)possible[i];

                if (solveSudokuRe(board, (int)next.Key, (int)next.Value))
                    return true;

                // back tracking 
                board[row][col] = '.';
            }           

            return false;
        }

        /**
         * source code from blog:
         * https://github.com/yinlinglin/LeetCode/blob/master/SudokuSolver.h
         * 
         * 
             * convert it from c++ to C#
         * Julia comment: 
         * 1. C++ pair<int, int> <-> KeyValuePair<string, string>
         * checked the webpage:
         * http://stackoverflow.com/questions/166089/what-is-c-sharp-analog-of-c-stdpair
         * 2. C++ make_pair  <-> C#  KeyValuePair class
         * 3. C++ make_pair(row, col) <->  C# new KeyValuePair<int, int>(row, col)
         * http://stackoverflow.com/questions/15495165/how-to-initialize-keyvaluepair-object-the-proper-way
              learn template syntax again. 
         */
        private static KeyValuePair<int, int> getNextMissing(char[][] board, int row, int col)
        {
            while (true)
            {
                row = (col == 8) ? row + 1 : row;
                col = (col + 1) % 9;

                if (row == 9 || board[row][col] == '.')                                      
                    return  new KeyValuePair<int, int>(row, col);
            }
        }
  
        /**
         * source code from blog: 
         * https://github.com/yinlinglin/LeetCode/blob/master/SudokuSolver.h
         * convert it from c++ to C#
         * julia comment: 
         * 1. Learn difference from C++ and C#
         * 2. this code is fun to play with - 
         *  C++  bool value[9] = {false}; <-> C# Enumerable.Repeat(false, 9).ToArray()
         * 3. retrieve the possible chars for row, col position 
         * 
         */
        private static void getPossibleValues(char[][] board, int row, int col, ArrayList possible)
        {
            //http://stackoverflow.com/questions/1014005/how-to-populate-instantiate-a-c-sharp-array-with-a-single-value
            bool[] value = Enumerable.Repeat(false, 9).ToArray(); 

            for (int i = 0; i < 9; ++i)
            {
                char charInRows = board[i][col];
                if (charInRows != '.')
                    value[charInRows - '1'] = true;

                char charInCols = board[row][i];
                if (charInCols != '.')
                    value[charInCols - '1'] = true;

                int newRow = row / 3 * 3 + i / 3; //  julia's comment: 
                int newCol = col / 3 * 3 + i % 3; // julia's comment: 

                char c = board[newRow][newCol];

                if (c != '.') value[c-'1'] = true;
            }

            for (int i = 0; i < 9; ++i)
                if (!value[i]) 
                    possible.Add(i+'1');
            }
        }
}
