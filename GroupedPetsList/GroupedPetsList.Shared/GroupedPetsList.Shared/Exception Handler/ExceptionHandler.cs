using Newtonsoft.Json;
using System;
using System.IO;

namespace GroupedPetsList.Shared
{
    public class ExceptionHandler
    {
        #region Constants

        const string Errorlogfile = "errorlog.txt";
        readonly string FolderName = "9B4F5B62-C75E-AXP4-BC09-698A54642674";

        #endregion

        #region singleton Instance

        static ExceptionHandler instance;

        public static ExceptionHandler Instance
        {
            get { return instance ?? (instance = new ExceptionHandler()); }
        }

        #endregion


        #region Helper methods

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="ex">Ex.</param>
        public void LogError(CustomException ex)
        {
            if (null != ex)
            {
                var errorMessage = JsonConvert.SerializeObject(ex);
                errorMessage += Environment.NewLine + Environment.NewLine;
                WriteContentToFile(errorMessage);
            }
        }


        /// <summary>
        /// Method to write / append error contents to file
        /// </summary>a1
        /// <param name="errorMessage"></param>
        void WriteContentToFile(string errorMessage)
        {
            var document = Android.OS.Environment.ExternalStorageDirectory;
            var file = new Java.IO.File(document, FolderName);
            if (!file.Exists())
            {
                file.Mkdirs();
            }
            var filePath = string.Format("{0}/{1}", Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, FolderName);
            var fileName = Path.Combine(filePath, Errorlogfile);

            if (File.Exists(fileName))
            {

                File.AppendAllText(fileName, errorMessage);
            }
            else
            {
                File.WriteAllText(fileName, errorMessage);
            }
        }

        #endregion
    }
}
