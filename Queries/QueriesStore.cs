﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Queries
{
    public static class QueriesStore
    {


        public static string Query2(IEnumerable<string> str)
        {
            //There is a sequence of strings.
            //Combine all strings into one.
            // var str2 = str.Aggregate("",(accumulator, currentValue) => accumulator + currentValue);
            var str2 = String.Concat(str);
            return str2;

        }

        public static string Query3(int l, IEnumerable<string> str)
        {

            //Query3. There is an integer number L (> 0) and a string sequence A.
            //Output the first string from A that starts with a digit and has length L.
            //If there are no required strings in the sequence A, then output the string "Not found".
            //Indication. To handle the situation associated with the absence of required rows, use the ?? operation.
            string result = str.FirstOrDefault(str => (str.Length == l && char.IsDigit(str[0]))) ?? "Not found";

            return result;
        }

        public static int Query4(char c, IEnumerable<string> str)
        {
            //Query4. Given a C character and a string sequence A.
            //Find the number of A elements that contain more than one character, provided that these elements start and end with C.

            int result = str.Count(str => (str.Length > 1 && str[0] == c && str[str.Length - 1] == c));
            return result;
        }

        public static int Query5(IEnumerable<string> str)
        {
            //Query5. A string sequence is given.
            //Find the sum of the lengths of all strings in the given sequence.

            int result = str.Sum(stg => stg.Length);
            //var result = str.Aggregate(0, (accumulator, currentValue) => accumulator + currentValue.Length);

            return result;
        }

        public static string Query6(IEnumerable<string> str)
        {
            //Query6. A string sequence is given.
            //Get a string consisting of the initial characters of all strings in the source sequence.
            //var result = str.Aggregate("", (accumulator, currentValue) => accumulator + currentValue[0]);
            //
            var result = str
            .Where(str => !string.IsNullOrEmpty(str))
            .Select(str => str[0]);

            return string.Concat(result);
        }

        public static IEnumerable<int> Query7(int k, IEnumerable<int> a)
        {
            //Query7. An integer K (> 0) and an integer sequence A are given.
            //Find the set-theoretic difference of two fragments A: the first contains all even numbers,
            //and the second - all numbers with ordinal numbers greater than K.
            //In the resulting sequence (not containing identical elements), reverse the order of the elements.

            var first = a.Where(n => n % 2 == 0);
            var second = a.Skip(k);

            var result = first.Except(second).Reverse();

            return result;
        }

        public static IEnumerable<string> Query8(int k, IEnumerable<string> a)
        {
            //Query8. An integer K (> 0) and a string sequence A are given.
            //Sequence strings contain only numbers and capital letters of the Latin alphabet.
            //Extract from A all strings of length K that end in a digit, sorting them in an ascending order.

            var result = a.Where(n => k == n.Length && Char.IsDigit(n[n.Length - 1]));
            result = result.OrderBy(n => n);
            return result;

        }

        public static IEnumerable<int> Query9(int d, int k, IEnumerable<int> a)
        {
            //Query9. Integers D and K (K > 0) and an integer sequence A are given.
            //Find the set-theoretic union of two fragments A: the first contains all elements up to the first element,
            //greater D (not including it), and the second - all elements, starting from the element with the ordinal number K.
            //Sort the resulting sequence (not containing identical elements) in descending order.

            var first = a.TakeWhile(i => i <= d);
            var second = a.Skip(k - 1);

            var result = first.Union(second).Distinct().OrderByDescending(i => i);

            return result;
        }

        public static IEnumerable<string> Query10(IEnumerable<int> n)
        {
            //Query10. A sequence of positive integers is given.
            //Processing only odd numbers, get a sequence of their string representations and sort it in ascending order.
            var result = n.Where(i => i % 2 == 1).
                OrderBy(i => i).
                Select(i => i.ToString());

            return result;
        }

        public static IEnumerable<char> Query11(IEnumerable<string> str)
        {
            //Query11. A sequence of non-empty strings is given.
            //Get a sequence of characters, which is defined as follows:
            //if the corresponding string of the source sequence has an odd length, then as
            //character the first character of this string is taken; otherwise, the last character of the string is taken.
            //Sort the received characters in descending order of their codes.

            var result = str.Select(i => i.Length % 2 == 1 ? i[0] : i[i.Length - 1])
              .OrderByDescending(i => i);
            return result;
        }

        public static IEnumerable<int> Query12(int k1, int k2, IEnumerable<int> a, IEnumerable<int> b)
        {
            //Query12. Integers K1 and K2 and integer sequences A and B are given.
            //Get a sequence containing all numbers from A greater than K1 and all numbers from B less than K2.
            //Sort the resulting sequence in ascending order.

            var first = a.Where(n => n > k1);
            var second = b.Where(n => n < k2);
            var result = first.Concat(second).OrderBy(i => i);

            return result;
        }

        public static IEnumerable<string> Query13(IEnumerable<int> a, IEnumerable<int> b)
        {
            //Query13. Sequences of positive integers A and B are given; all numbers in each sequence are distinct.
            //Find the sequence of all pairs of numbers that satisfy the following conditions: the first element of the pair belongs to
            //the sequence A, the second one belongs to B, and both elements end with the same digit.
            //The resulting sequence is called the inner union of sequences A and B by key,
            //determined by the last digits of the original numbers.
            //Represent the found union as a sequence of strings containing the first and second elements of the pair,
            //separated by a hyphen, e.g. "49-129".

            var result = from A in a
                         from B in b
                         where A % 10 == B % 10
                         select $"{A} - {B}";
            return result;
        }

        public static IEnumerable<string> Query14(IEnumerable<string> A, IEnumerable<string> B)
        {
            //Query14. String sequences A and B are given; all strings in each sequence are distinct,
            //have a non-zero length and contain only digits and capital letters of the Latin alphabet.
            //Find the inner union of A and B, each pair of which must contain strings of the same length.
            //Represent the found union as a sequence of strings containing the first and second elements of the pair,
            //colon-separated, e.g. "AB: CD". The order of the pairs must be determined by the order
            //first elements of pairs (in ascending order), and for equal first elements - by the order of the second elements of pairs (in descending order).

            var result = A.Join(B, a => a.Length, b => b.Length, (a, b) => new { A = a, B = b });
            var result2 = result.OrderBy(pair => pair.A)
                                .ThenByDescending(pair => pair.B)
                                .Select(pair => $"{pair.A}:{pair.B}");

            return result2;
               
        }

        public static IEnumerable<string> Query15(IEnumerable<int> a)
        {
            //Query15.An integer sequence A is given.
            //Group elements of sequence A ending with the same digit and based on this grouping
            //get a sequence of strings of the form "D: S", where D is the grouping key (i.e. some digit that ends with
            //at least one of the numbers in the sequence A), and S is the sum of all numbers from A that end in D.
            //Order the resulting sequence in an ascending order of keys.
            //Indication. Use the GroupBy method.

            var result = a
           .GroupBy(i => i % 10)
           .OrderBy(g => g.Key)
           .Select(g => $"{g.Key}: {g.Sum()}");

            return result;
        }

        public static IDictionary<uint, int> Query16(IEnumerable<Enrollee> enrollees)
        {
            //Query16. The initial sequence contains information about applicants. Each element of the sequence
            //includes the following fields: <School number> <Entry year> <Last name>
            //Return a dictionary, where the key is the year, the value is the number of different schools that applicants graduated from this year.
            //Order the elements of the dictionary in ascending order of the number of schools, and for matching numbers - in ascending order of the year number.

            var result = enrollees.GroupBy(i => i.YearGraduate)
                                  .ToDictionary(
                                      i => i.Key,
                                      i => i.GroupBy(e => e.SchoolNumber).Count()
                                  );
            return result;
        }
    }
}
