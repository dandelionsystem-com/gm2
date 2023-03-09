﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Levenshtein Distance. 
// In 1965 Vladmir Levenshtein created a distance algorithm.
// This tells us the number of edits needed to turn one string into another.
// With Levenshtein distance, we measure similarity and match approximate strings with fuzzy logic.
// https://www.dotnetperls.com/levenshtein

namespace System.gm
{
    /// <summary>
    /// Contains approximate string matching
    /// </summary>
    public class CompareString
    {
        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Compare(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}