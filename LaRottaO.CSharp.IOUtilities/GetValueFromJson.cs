using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LaRottaO.CSharp.IOUtilities
{
    public class GetValueFromJson
    {
        /***************************************************************************************************************************/
        /// <summary>
        ///
        /// 2022/06/15
        /// A little experiment to get values from a JSON without using an external library
        /// by Luis Felipe La Rotta

        ///Usage Tuple<Boolean, possibleResults, List<Tuple<String, String>>> result = getKey("subscriberName", textBoxJson.Text);
        ///
        /// </summary>

        /***************************************************************************************************************************/

        public enum possibleResults

        { SUCCESS, JSON_IS_EMPTY, KEY_NOT_FOUND_ON_JSON, INVALID_JSON_STRUCTURE };

        public Tuple<Boolean, possibleResults, List<Tuple<String, String>>> getKey(String argRequiredkey, String argJsonAsPlanText, Boolean removeCarriageReturn = false, Boolean removeDuplicatedWhitespaces = false)
        {
            try
            {
                if (removeCarriageReturn)
                {
                    argJsonAsPlanText = argJsonAsPlanText.Replace("\n", "");
                    argJsonAsPlanText = argJsonAsPlanText.Replace("\r", "");
                }

                if (removeDuplicatedWhitespaces)
                {
                    argJsonAsPlanText = removeRepeatedCharacter(argJsonAsPlanText, " ");
                }

                //Check that the JSON is not empty

                if (String.IsNullOrEmpty(argJsonAsPlanText))
                {
                    return new Tuple<Boolean, possibleResults, List<Tuple<String, String>>>(false, possibleResults.JSON_IS_EMPTY, new List<Tuple<String, String>>());
                }

                //Check that the JSON contains at least one instance of the desired key

                if (!argJsonAsPlanText.ToLower().Contains(argRequiredkey.ToLower()))
                {
                    return new Tuple<Boolean, possibleResults, List<Tuple<String, String>>>(false, possibleResults.KEY_NOT_FOUND_ON_JSON, new List<Tuple<String, String>>());
                }

                List<Tuple<String, String>> resultsList = new List<Tuple<String, String>>();

                for (int pos = 0; pos < argJsonAsPlanText.Length; pos++)
                {
                    String character = argJsonAsPlanText.Substring(pos, 1);

                    //Find a colon :

                    if (character.Equals(":"))
                    {
                        //Go backwards from the colon to find if there's a quote

                        int letfEndingQuote = argJsonAsPlanText.LastIndexOf('"', pos);

                        if (letfEndingQuote == -1)
                        {
                            continue;
                        }

                        //If there is a quote, keep going backwards to see if there's the opening quote.

                        int leftStartingQuote = argJsonAsPlanText.LastIndexOf('"', letfEndingQuote - 1);

                        if (leftStartingQuote == -1)
                        {
                            continue;
                        }

                        //We have the key! Is the key equal to the desired value? If the key was found but does not match the required key, keep going

                        String key = argJsonAsPlanText.Substring((leftStartingQuote + 1), (letfEndingQuote - leftStartingQuote - 1));

                        if (!key.ToLower().Equals(argRequiredkey.ToLower()))
                        {
                            continue;
                        }

                        String resultBetweenSquareBraces = detectTextBetweenSquareBraces(argJsonAsPlanText, pos);

                        if (resultBetweenSquareBraces != null)
                        {
                            resultsList.Add(new Tuple<String, String>(argRequiredkey, resultBetweenSquareBraces));
                        }

                        String resultBetweenCurlyBraces = detectTextBetweenCurlyBraces(argJsonAsPlanText, pos);

                        if (resultBetweenCurlyBraces != null)
                        {
                            resultsList.Add(new Tuple<String, String>(argRequiredkey, resultBetweenCurlyBraces));
                        }

                        String resultBetweenQuotes = detectTextBetweenQuotes(argJsonAsPlanText, pos);

                        if (resultBetweenQuotes != null)
                        {
                            resultsList.Add(new Tuple<String, String>(argRequiredkey, resultBetweenQuotes));
                        }
                    }
                }

                return new Tuple<Boolean, possibleResults, List<Tuple<String, String>>>(true, possibleResults.SUCCESS, resultsList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return new Tuple<Boolean, possibleResults, List<Tuple<String, String>>>(false, possibleResults.INVALID_JSON_STRUCTURE, new List<Tuple<String, String>>());
            }
        }

        private String detectTextBetweenSquareBraces(String argJsonAsPlanText, int pos)
        {
            int rightStartingSquare = argJsonAsPlanText.IndexOf('[', pos);
            int rightStartingCurly = argJsonAsPlanText.IndexOf('{', pos);
            int rightStartingQuote = argJsonAsPlanText.IndexOf('"', pos);

            if (rightStartingSquare == -1)
            {
                //No [ found
                return null;
            }

            if (rightStartingCurly > -1 && rightStartingCurly < rightStartingSquare)

            {
                //Getting the value between { } is a better option
                return null;
            }

            if (rightStartingQuote > -1 && rightStartingQuote < rightStartingSquare)

            {
                //Getting the value between " " is a better option;
                return null;
            }

            String character = null;
            int remainingOpenSquareBrackets = 1;
            int rightEndingSquare = -1;

            for (int i = rightStartingSquare + 1; i < argJsonAsPlanText.Length; i++)
            {
                character = argJsonAsPlanText.Substring(i, 1);

                if (character.Equals("["))
                {
                    remainingOpenSquareBrackets++;
                }

                if (character.Equals("]"))
                {
                    remainingOpenSquareBrackets--;

                    if (remainingOpenSquareBrackets == 0)
                    {
                        rightEndingSquare = i;

                        String value = argJsonAsPlanText.Substring((rightStartingSquare), (rightEndingSquare - rightStartingSquare + 1));

                        return value;
                    }
                }
            }

            return null;
        }

        private String detectTextBetweenCurlyBraces(String argJsonAsPlanText, int pos)
        {
            int rightStartingSquare = argJsonAsPlanText.IndexOf('[', pos);
            int rightStartingCurly = argJsonAsPlanText.IndexOf('{', pos);
            int rightStartingQuote = argJsonAsPlanText.IndexOf('"', pos);

            if (rightStartingCurly == -1)
            {
                //No { found

                return null;
            }

            if (rightStartingSquare > -1 && rightStartingSquare < rightStartingCurly)

            {
                //Getting the value between [ ] is a better option
                return null;
            }

            if (rightStartingQuote > -1 && rightStartingQuote < rightStartingCurly)

            {
                //Getting the value between " " is a better option;
                return null;
            }

            String character = null;
            int remainingOpenCurlyBraces = 1;
            int rightEndingSquare = -1;

            for (int i = rightStartingCurly + 1; i < argJsonAsPlanText.Length; i++)
            {
                character = argJsonAsPlanText.Substring(i, 1);

                if (character.Equals("{"))
                {
                    remainingOpenCurlyBraces++;
                }

                if (character.Equals("}"))
                {
                    remainingOpenCurlyBraces--;

                    if (remainingOpenCurlyBraces == 0)
                    {
                        rightEndingSquare = i;

                        String value = argJsonAsPlanText.Substring((rightStartingCurly), (rightEndingSquare - rightStartingCurly + 1));

                        return value;
                    }
                }
            }

            return null;
        }

        private String detectTextBetweenQuotes(String argJsonAsPlanText, int pos)
        {
            int rightStartingSquare = argJsonAsPlanText.IndexOf('[', pos);
            int rightStartingCurly = argJsonAsPlanText.IndexOf('{', pos);
            int rightStartingQuote = argJsonAsPlanText.IndexOf('"', pos);

            if (rightStartingQuote == -1)
            {
                //No " found
                return null;
            }

            if (rightStartingSquare > -1 && rightStartingSquare < rightStartingQuote)

            {
                //Getting the value between [ ] is a better option
                return null;
            }

            if (rightStartingCurly > -1 && rightStartingCurly < rightStartingQuote)

            {
                //Getting the value between { } is a better option
                return null;
            }

            int rightEndingQuote = argJsonAsPlanText.IndexOf('"', rightStartingQuote + 1);

            String value = argJsonAsPlanText.Substring(rightStartingQuote, (rightEndingQuote - rightStartingQuote + 1));

            return value;
        }

        public String removeRepeatedCharacter(String originalText, String characterToReplace)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[" + characterToReplace + "]{2,}", options);
            originalText = regex.Replace(originalText, characterToReplace);
            return originalText;
        }
    }
}