namespace PrinterWebInterface.Printer
{
    public interface IPrinter
    {
        string GetStatus();
        void Print(string filePath, int numCopies, string pagesRange);
        void CancelAllJobs();
    }
}
