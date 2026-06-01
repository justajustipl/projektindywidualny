using System;
using System.IO;

namespace Projekt_silka
{
    internal static class EmailService
    {
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ProjektSilka", "email_log.txt");

        public static string SendRegistrationConfirmation(string toEmail, string clientName, string trainingTitle, string date, string time, string trainerName)
        {
            if (string.IsNullOrEmpty(toEmail)) return "";

            string message =
                $"Cześć {clientName}!\n\n" +
                $"Zostałeś/aś zapisany/a na trening:\n" +
                $"Typ: {trainingTitle}\n" +
                $"Trener: {trainerName}\n" +
                $"Data: {date} o {time}\n\n" +
                $"Do zobaczenia!\nZespół Mobilna Siłownia";

            LogEmail(toEmail, "Potwierdzenie zapisu na trening", message);
            return toEmail;
        }

        public static string SendCancellationConfirmation(string toEmail, string clientName, string trainingTitle, string date, string time)
        {
            if (string.IsNullOrEmpty(toEmail)) return "";

            string message =
                $"Cześć {clientName}!\n\n" +
                $"Twój zapis został anulowany:\n" +
                $"Typ: {trainingTitle}\n" +
                $"Data: {date} o {time}\n\n" +
                $"Zespół Mobilna Siłownia";

            LogEmail(toEmail, "Potwierdzenie anulowania zapisu", message);
            return toEmail;
        }

        public static string SendRescheduleConfirmation(string toEmail, string clientName, string oldTitle, string oldDate, string oldTime, string newTitle, string newDate, string newTime)
        {
            if (string.IsNullOrEmpty(toEmail)) return "";

            string message =
                $"Cześć {clientName}!\n\n" +
                $"Twój termin został zmieniony:\n" +
                $"Z: {oldTitle} | {oldDate} o {oldTime}\n" +
                $"Na: {newTitle} | {newDate} o {newTime}\n\n" +
                $"Zespół Mobilna Siłownia";

            LogEmail(toEmail, "Potwierdzenie zmiany terminu", message);
            return toEmail;
        }

        private static void LogEmail(string to, string subject, string body)
        {
            try
            {
                string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]\n" +
                             $"DO: {to}\n" +
                             $"TEMAT: {subject}\n" +
                             $"TREŚĆ:\n{body}\n" +
                             $"{'='.ToString().PadRight(50, '=')}\n\n";
                File.AppendAllText(LogPath, log);
            }
            catch { }
        }
    }
}