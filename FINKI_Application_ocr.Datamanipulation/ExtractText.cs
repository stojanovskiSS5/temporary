using FINKI_Application_ocr.Datamanipulation.Constants;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Tesseract;

namespace FINKI_Application_ocr.Datamanipulation
{
    public class ExtractText
    {
        private ImagePreprocessing preprocessing;
        private string _textFromImage;
        private string _preparedTextForProcessing;
        private List<string> wrongWords;
        private string _textFromImageTesseract;

        public string TextFromImageTesseract
        {
            get { return _textFromImageTesseract; }
            set { _textFromImageTesseract = value; }
        }

        public List<string> WrongWords
        {
            get { return wrongWords; }
            set { wrongWords = value; }
        }

        public string PreparedTextForProcessing
        {
            get { return _preparedTextForProcessing; }
            set { _preparedTextForProcessing = value; }
        }

        public string TextFromImage
        {
            get { return _textFromImage; }
            set { _textFromImage = value; }
        }

        public ImagePreprocessing Preprocessing
        {
            get { return preprocessing; }
            set { preprocessing = value; }
        }

        //Constructor
        public ExtractText(ImagePreprocessing imagePreprocessing)
        {
            Preprocessing = imagePreprocessing;
        }

        public void ExtractTextFromImage()
        {
            Preprocessing.StartImagePreprocessing();
            TesseractEngine tesseract = new TesseractEngine(Paths.TesseractDataFolderPath, "eng");
            var imgText = tesseract.Process(Preprocessing.Image);
            WrongWords = new List<string>();
            TextFromImageTesseract = imgText.GetText();
            PreparedTextForProcessing = TextFromImageTesseract;
            TextFromImage = TextFromImageTesseract;
        }

        public string TrimText(string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.TrimStart().TrimEnd();

            return text;
        }

        public string ReplaceMoreThanOneNewLine(string text)
        {
            var regex = new Regex("(\\n)\\1+");
            return regex.Replace(text, "$1");
        }

        public string ReplaceMoreThanOneWhiteSpace(string text)
        {
            var regex = new Regex("( )\\1+");
            return regex.Replace(text, "$1");
        }

        public string ReplaceSpecialCharachtersWithWhiteSpace(string text)
        {
            return new StringBuilder(text)
                .Replace("-", " ")
                .Replace("_", " ")
                .Replace("@", " ")
                .Replace(".", " ")
                .Replace(",", " ")
                .Replace("(", " ")
                .Replace(")", " ")
                .Replace("'", " ")
                .Replace("&", " ")
                .Replace("^", " ")
                .ToString();
        }

        public void ReplaceTheTextWithTheCorrectionFound(string value, string replace)
        {
            string pattern = $@"\b{value}\b";
            TextFromImage = Regex.Replace(TextFromImage, pattern, replace);
        }


        public void GetBetterText(string txt)
        {
            Application app = null;
            try
            {
                app = new Application();
                PreparedTextForProcessingPreparationFunctions();
                TextForImagePreparationFunctions();

                string[] textLines = PreparedTextForProcessing.Split('\n');

                foreach (var line in textLines)
                {
                    string[] words = line.Split(' ');

                    for (int i = 0; i < words.Length; i++)
                    {

                        if (words[i].Equals("|"))
                        {
                            TextFromImage = new StringBuilder(TextFromImage).Replace("|", "I").ToString();
                        }

                        if (!app.CheckSpelling(words[i]))
                        {

                            if (i - 1 >= 0)
                            {
                                var potentialStringForReplace = new StringBuilder().Append(words[i - 1]).Append(words[i]).ToString();
                                if (app.CheckSpelling(potentialStringForReplace))
                                {
                                    var formatedPotentialString = new StringBuilder().Append(words[i - 1]).Append(" ").Append(words[i]).ToString();
                                    ReplaceTheTextWithTheCorrectionFound(formatedPotentialString, potentialStringForReplace);
                                    i += 2;
                                    continue;
                                }
                            }

                            if (i + 1 < words.Length)
                            {
                                var potentialStringForReplace = new StringBuilder().Append(words[i]).Append(words[i + 1]).ToString();
                                if (app.CheckSpelling(potentialStringForReplace))
                                {
                                    var formatedPotentialString = new StringBuilder().Append(words[i]).Append(" ").Append(words[i + 1]).ToString();
                                    ReplaceTheTextWithTheCorrectionFound(formatedPotentialString, potentialStringForReplace);
                                    i += 2;
                                    continue;
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                //log errors
            }
            finally
            {
                app.Quit();
                if (app != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
                GC.Collect();
            }
        }

        public void TextFromImageSpellSuggestionCorrection()
        {
            Application app = null;
            Document document = null;
            try
            {
                app = new Application { Visible = false };
                document = app.Documents.Add();
                PreparedTextForProcessingPreparationFunctions();

                string[] textLines = PreparedTextForProcessing.Split('\n');

                foreach (var line in textLines)
                {
                    string[] words = line.Split(' ');

                    for (int i = 0; i < words.Length; i++)
                    {

                        if (!app.CheckSpelling(words[i]))
                        {
                            var a = app.GetSpellingSuggestions(words[i]).SpellingErrorType;
                            SpellingSuggestions listWithSpellingSuggestions = app.GetSpellingSuggestions(words[i]);

                            foreach (SpellingSuggestion spellingSugg in listWithSpellingSuggestions)
                            {
                                if (spellingSugg.Name.Length >= words[i].Length)
                                {
                                    ReplaceTheTextWithTheCorrectionFound(words[i], spellingSugg.Name);
                                    break;
                                }

                            }

                        }

                    }
                }

            }
            catch (Exception err)
            {
                //log Exception
            }
            finally
            {
                document.Close(WdSaveOptions.wdDoNotSaveChanges);
                app.Quit();
                if (app != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                if (document != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(document);
                app = null;
                document = null;
                GC.Collect();
            }
        }


        public void PreparedTextForProcessingPreparationFunctions()
        {
            PreparedTextForProcessing = TrimText(PreparedTextForProcessing);
            PreparedTextForProcessing = ReplaceMoreThanOneNewLine(PreparedTextForProcessing);
            PreparedTextForProcessing = ReplaceMoreThanOneWhiteSpace(PreparedTextForProcessing);
            PreparedTextForProcessing = ReplaceSpecialCharachtersWithWhiteSpace(PreparedTextForProcessing);
        }

        public void TextForImagePreparationFunctions()
        {
            TextFromImage = TrimText(TextFromImage);
            TextFromImage = ReplaceMoreThanOneNewLine(TextFromImage);
            TextFromImage = ReplaceMoreThanOneWhiteSpace(TextFromImage);
        }

        public void WordsCheck()
        {
            Application app = null;
            try
            {
                app = new Application();
                PreparedTextForProcessing = TextFromImage;

                PreparedTextForProcessingPreparationFunctions();

                string[] wordsSplit = PreparedTextForProcessing.Split(new char[] { ' ', '\n' });

                foreach (var word in wordsSplit)
                {
                    if (!app.CheckSpelling(word))
                    {
                        wrongWords.Add(word);
                    }
                }
            }
            catch (Exception err)
            {
                //log Exception
            }
            finally
            {
                app.Quit();
                if (app != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                app = null;
                GC.Collect();
            }
        }


    }
}
